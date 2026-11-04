using System;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects.Deferred;

/// <summary>
/// Provides SunBurn's built-in deferred terrain rendering.
/// </summary>
public class DeferredTerrainEffect : BaseTerrainEffect, IDeferredObjectEffect, IShadowGenerateEffect
{
	private Matrix fNB;

	private Vector4 fNa;

	private Vector4 fNg;

	private DeferredEffectOutput fNf;

	private Texture2D fN_0003;

	private Texture2D fNn;

	private Vector2 fN_0019;

	private bool fNL;

	private float fNv;

	private float fN_0012;

	private Vector3 fN_0015;

	private Vector3 fN8;

	private Vector3[] fNi = new Vector3[3];

	private EffectParameter fN_0005;

	private EffectParameter fNj;

	private EffectParameter fNo;

	private EffectParameter fNc;

	private EffectParameter fNd;

	private EffectParameter fNw;

	private EffectParameter fNs;

	private EffectParameter fN_0011;

	private EffectParameter fNQ;

	private EffectParameter fNG;

	/// <summary>
	/// Main property used to eliminate shadow artifacts.
	/// </summary>
	public float ShadowPrimaryBias
	{
		get
		{
			return fNa.X;
		}
		set
		{
			EffectHelper.f_0013(new Vector4(value, fNa.Y, fNa.Z, fNa.W), ref fNa, ref fNd);
		}
	}

	/// <summary>
	/// Additional fine-tuned property used to eliminate shadow artifacts.
	/// </summary>
	public float ShadowSecondaryBias
	{
		get
		{
			return fNa.Y;
		}
		set
		{
			EffectHelper.f_0013(new Vector4(fNa.X, value, fNa.Z, fNa.W), ref fNa, ref fNd);
		}
	}

	/// <summary>
	/// Bounding area of the shadow source, where the bounds center is the actual shadow source location,
	/// and the radius is either the source radius (for point sources) or the maximum view based casting
	/// distance (for directional sources).
	/// </summary>
	public BoundingSphere ShadowArea
	{
		set
		{
			Vector4 vector = new Vector4(value.Center, value.Radius);
			EffectHelper.f_0013(vector, ref fNg, ref fNw);
		}
	}

	/// <summary>
	/// Determines if the effect is capable of generating shadow maps. Objects using effects unable to
	/// generate shadow maps automatically use the built-in shadow effect, however this puts
	/// heavy restrictions on how the effects handle rendering (only basic vertex transforms are supported).
	/// </summary>
	public bool SupportsShadowGeneration => true;

	/// <summary>
	/// Determines the type of shader output for the effects to generate.
	/// </summary>
	public DeferredEffectOutput DeferredEffectOutput
	{
		get
		{
			return fNf;
		}
		set
		{
			if (value != fNf)
			{
				fNf = value;
				SetTechnique();
			}
		}
	}

	/// <summary>
	/// Texture containing the screen-space lighting generated during deferred rendering (used during the Final rendering pass).
	/// </summary>
	public Texture2D SceneLightingDiffuseMap
	{
		get
		{
			return fN_0003;
		}
		set
		{
			EffectHelper.fO(value, ref fN_0003, ref fNs);
			if (fNc != null && fN_0003 != null)
			{
				EffectHelper.fr(new Vector2(fN_0003.Width, fN_0003.Height), ref fN_0019, ref fNc);
			}
		}
	}

	/// <summary>
	/// Texture containing the screen-space specular generated during deferred rendering (used during the Final rendering pass).
	/// </summary>
	public Texture2D SceneLightingSpecularMap
	{
		get
		{
			return fNn;
		}
		set
		{
			EffectHelper.fO(value, ref fNn, ref fN_0011);
		}
	}

	/// <summary>
	/// Enables scene fog.
	/// </summary>
	public bool FogEnabled
	{
		get
		{
			return fNL;
		}
		set
		{
			fNL = value;
			SetTechnique();
		}
	}

	/// <summary>
	/// Distance from the camera in world space that fog begins.
	/// </summary>
	public float FogStartDistance
	{
		get
		{
			return fNv;
		}
		set
		{
			fQ(value, fN_0012);
		}
	}

	/// <summary>
	/// Distance from the camera in world space that fog ends.
	/// </summary>
	public float FogEndDistance
	{
		get
		{
			return fN_0012;
		}
		set
		{
			fQ(fNv, value);
		}
	}

	/// <summary>
	/// Color applied to scene fog.
	/// </summary>
	public Vector3 FogColor
	{
		get
		{
			return fN_0015;
		}
		set
		{
			EffectHelper.fq(value, ref fN_0015, ref fNG);
		}
	}

	/// <summary>
	/// Sets scene ambient lighting (used during the Final rendering pass).
	/// </summary>
	public void SetAmbientLighting(IAmbientSource light, Vector3 directionhint)
	{
		if (fN_0005 != null && fNj != null && light is ILight)
		{
			ILight light2 = light as ILight;
			L._0019.aQ(light2.CompositeColorAndIntensity, light.Depth, 0.65f, out var vector, out var vector2);
			fNi[0] = vector;
			fNi[1] = vector2;
			ref Vector3 reference = ref fNi[2];
			reference = (vector + vector2) * 0.2f;
			fN_0005.SetValue(fNi);
			EffectHelper.fq(directionhint, ref fN8, ref fNj);
		}
	}

	/// <summary>
	/// Sets the camera view and inverse camera view matrices. These are the matrices used in the final
	/// on-screen render from the camera / player point of view.
	///
	/// These matrices will differ from the standard view and inverse view matrices when rendering from
	/// an alternate point of view (for instance during shadow map and cube map generation).
	/// </summary>
	/// <param name="view">Camera view matrix applied to geometry using this effect.</param>
	/// <param name="viewtoworld">Camera inverse view matrix applied to geometry using this effect.</param>
	public void SetCameraView(Matrix view, Matrix viewtoworld)
	{
		EffectHelper.fR(viewtoworld, ref fNB, ref fNo);
	}

	/// <summary>
	/// Sets the effect technique based on its current property values.
	/// </summary>
	protected override void SetTechnique()
	{
		switch (fNf)
		{
		case DeferredEffectOutput.Depth:
			CurrentTechnique = Techniques["Terrain_Depth_Technique"];
			break;
		case DeferredEffectOutput.GBuffer:
			CurrentTechnique = Techniques["Terrain_GBuffer_Technique"];
			break;
		case DeferredEffectOutput.Final:
			if (fNL)
			{
				CurrentTechnique = Techniques["Terrain_FinalFog_Technique"];
			}
			else
			{
				CurrentTechnique = Techniques["Terrain_Final_Technique"];
			}
			break;
		case DeferredEffectOutput.ShadowDepth:
			CurrentTechnique = Techniques["Terrain_Shadow_Technique"];
			break;
		}
	}

	/// <summary>
	/// Creates a new DeferredTerrainEffect instance.
	/// </summary>
	/// <param name="graphicsdevice"></param>
	public DeferredTerrainEffect(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "DeferredTerrainEffect")
	{
		B();
	}

	internal DeferredTerrainEffect(GraphicsDevice P_0, bool P_1)
		: base(P_0, "DeferredTerrainEffect", P_1)
	{
		B();
	}

	private void B()
	{
		fNo = Parameters["_CameraViewToWorld"];
		fN_0005 = Parameters["_AmbientColor"];
		fNj = Parameters["_AmbientDirection"];
		fNs = Parameters["_SceneLightingDiffuseMap"];
		fN_0011 = Parameters["_SceneLightingSpecularMap"];
		fNc = Parameters["_TargetWidthHeight"];
		fNd = Parameters["_OffsetBias_DepthBias_TransClipRef"];
		fNw = Parameters["_Direction_Or_Position_And_Radius"];
		fNQ = Parameters["_FogStartDist_EndDistInv"];
		fNG = Parameters["_FogColor"];
	}

	/// <summary>
	/// Creates a new empty effect of the same class type and using the same effect file as this object.
	/// </summary>
	/// <returns></returns>
	protected override Effect Create()
	{
		return new DeferredTerrainEffect(GraphicsDevice);
	}

	private void fQ(float P_0, float P_1)
	{
		if (fNQ != null && (fNv != P_0 || fN_0012 != P_1))
		{
			fNv = Math.Max(P_0, 0f);
			fN_0012 = Math.Max(fNv * 1.01f, P_1);
			float num = fN_0012 - fNv;
			if (num != 0f)
			{
				num = 1f / num;
			}
			fNQ.SetValue(new Vector2(fNv, num));
		}
	}
}
