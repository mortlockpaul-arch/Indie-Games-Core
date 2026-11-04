using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects.Deferred;

/// <summary>
/// Provides custom deferred object rendering from user FX shaders, with full support for, and binding of, FX Standard
/// Annotations and Semantics (SAS). Potentially supplying depth, diffuse, normal, specular,
/// fresnel, and emissive information to the deferred buffers for later lighting, shadow, and
/// material calculation.
/// </summary>
public class DeferredSasEffect : BaseSasEffect, IDeferredObjectEffect, IShadowGenerateEffect
{
	private bool fNB;

	private float fNa;

	private float fNg;

	private Vector3 fNf;

	private float fN_0003;

	private Vector4 fNn;

	private new Vector4 fN_0019;

	private Vector4 fNL;

	private Vector2 fNv;

	private Texture2D fN_0012;

	private Texture2D fN_0015;

	private EffectParameter fN8;

	private EffectParameter fNi;

	private EffectParameter fN_0005;

	private EffectParameter fNj;

	private EffectParameter fNo;

	private EffectParameter fNc;

	private EffectParameter fNd;

	private EffectParameter fNw;

	private EffectParameter fNs;

	private DeferredEffectOutput fN_0011 = DeferredEffectOutput.GBuffer;

	/// <summary>
	/// Main property used to eliminate shadow artifacts.
	/// </summary>
	public float ShadowPrimaryBias
	{
		get
		{
			return fNn.X;
		}
		set
		{
			EffectHelper.f_0013(new Vector4(value, fNn.Y, fNn.Z, fNn.W), ref fNn, ref fNj);
		}
	}

	/// <summary>
	/// Additional fine-tuned property used to eliminate shadow artifacts.
	/// </summary>
	public float ShadowSecondaryBias
	{
		get
		{
			return fNn.Y;
		}
		set
		{
			EffectHelper.f_0013(new Vector4(fNn.X, value, fNn.Z, fNn.W), ref fNn, ref fNj);
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
			EffectHelper.f_0013(vector, ref fNL, ref fNc);
		}
	}

	/// <summary>
	/// Determines if the effect is capable of generating shadow maps. Objects using effects unable to
	/// generate shadow maps automatically use the built-in shadow effect, however this puts
	/// heavy restrictions on how the effects handle rendering (only basic vertex transforms are supported).
	/// </summary>
	public bool SupportsShadowGeneration
	{
		get
		{
			if (!Properties.ContainsKey("ShadowGenerationTechnique"))
			{
				return false;
			}
			return Techniques[(string)Properties["ShadowGenerationTechnique"]] != null;
		}
	}

	/// <summary>
	/// Determines the type of shader output for the effects to generate.
	/// </summary>
	public DeferredEffectOutput DeferredEffectOutput
	{
		get
		{
			return fN_0011;
		}
		set
		{
			fN_0011 = value;
			SetTechnique();
		}
	}

	/// <summary>
	/// Texture containing the screen-space lighting generated during deferred rendering (used during the Final rendering pass).
	/// </summary>
	public Texture2D SceneLightingDiffuseMap
	{
		get
		{
			return fN_0012;
		}
		set
		{
			EffectHelper.fO(value, ref fN_0012, ref fNw);
			if (fNd != null && fN_0012 != null)
			{
				EffectHelper.fr(new Vector2(fN_0012.Width, fN_0012.Height), ref fNv, ref fNd);
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
			return fN_0015;
		}
		set
		{
			EffectHelper.fO(value, ref fN_0015, ref fNs);
		}
	}

	/// <summary>
	/// Enables scene fog.
	/// </summary>
	public bool FogEnabled
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
			fQ(float.MaxValue, float.MaxValue);
		}
	}

	/// <summary>
	/// Distance from the camera in world space that fog begins.
	/// </summary>
	public float FogStartDistance
	{
		get
		{
			return fNa;
		}
		set
		{
			fQ(value, fNg);
		}
	}

	/// <summary>
	/// Distance from the camera in world space that fog ends.
	/// </summary>
	public float FogEndDistance
	{
		get
		{
			return fNg;
		}
		set
		{
			fQ(fNa, value);
		}
	}

	/// <summary>
	/// Color applied to scene fog.
	/// </summary>
	public Vector3 FogColor
	{
		get
		{
			return fNf;
		}
		set
		{
			EffectHelper.fq(value, ref fNf, ref fNi);
		}
	}

	/// <summary>
	/// Sets scene ambient lighting (used during the Final rendering pass).
	/// </summary>
	public void SetAmbientLighting(IAmbientSource light, Vector3 directionhint)
	{
		if (light is ILight)
		{
			Vector4 vector = new Vector4((light as ILight).CompositeColorAndIntensity, 1f);
			EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_AmbientLight_Color[0]), vector);
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
	}

	internal DeferredSasEffect(GraphicsDevice P_0, byte[] P_1, bool P_2)
		: base(P_0, P_1, P_2)
	{
		EffectByteCode = P_1;
		B();
	}

	private void B()
	{
		fN_0005 = FindBySemantic("FARCLIPDIST");
		fNj = FindBySemantic("SHADOWINFO");
		fNc = FindBySemantic("SHADOWSOURCE");
		fNd = FindBySemantic("TARGETINFO");
		fNo = FindBySemantic("RENDERINFO");
		fN8 = FindBySemantic("FOGINFO");
		fNi = FindBySemantic("FOGCOLOR");
		fNw = FindBySemantic("SCENELIGHTINGDIFFUSEMAP");
		fNs = FindBySemantic("SCENELIGHTINGSPECULARMAP");
		FogEnabled = false;
	}

	/// <summary>
	/// Creates a new empty effect of the same class type and using the same effect file as this object.
	/// </summary>
	/// <returns></returns>
	protected override Effect Create()
	{
		return new DeferredSasEffect(GraphicsDevice, EffectByteCode, true);
	}

	/// <summary>
	/// Sets the effect technique based on its current property values.
	/// </summary>
	protected override void SetTechnique()
	{
		base.fN_0019.fNB.AccumulationValue++;
		EffectTechnique effectTechnique = null;
		switch (fN_0011)
		{
		case DeferredEffectOutput.GBuffer:
			if (Properties.ContainsKey("GBufferTechnique"))
			{
				effectTechnique = Techniques[(string)Properties["GBufferTechnique"]];
			}
			break;
		case DeferredEffectOutput.Final:
			if (Properties.ContainsKey("FinalTechnique"))
			{
				effectTechnique = Techniques[(string)Properties["FinalTechnique"]];
			}
			break;
		case DeferredEffectOutput.Depth:
			if (Properties.ContainsKey("DepthTechnique"))
			{
				effectTechnique = Techniques[(string)Properties["DepthTechnique"]];
			}
			break;
		case DeferredEffectOutput.ShadowDepth:
			if (Properties.ContainsKey("ShadowGenerationTechnique"))
			{
				effectTechnique = Techniques[(string)Properties["ShadowGenerationTechnique"]];
			}
			break;
		}
		if (effectTechnique != null)
		{
			CurrentTechnique = effectTechnique;
		}
	}

	private void fQ(float P_0, float P_1)
	{
		if (fN8 != null && (fNa != P_0 || fNg != P_1))
		{
			fNa = Math.Max(P_0, 0f);
			fNg = Math.Max(fNa * 1.01f, P_1);
			float num = fNg - fNa;
			if (num != 0f)
			{
				num = 1f / num;
			}
			fN8.SetValue(new Vector2(fNa, num));
		}
	}

	/// <summary>
	/// Applies the current transform information to the bound effect parameters.
	/// </summary>
	protected override void SyncTransformEffectData()
	{
		base.SyncTransformEffectData();
		if (fN_0005 != null)
		{
			Vector4 vector = Vector4.Transform(new Vector4(0f, 0f, 1f, 1f), ProjectionToView);
			float num = 0f;
			if (vector.W != 0f)
			{
				num = Math.Abs(vector.Z / vector.W);
			}
			if (fN_0003 != num)
			{
				fN_0003 = num;
				fN_0005.SetValue(num);
			}
		}
	}

	/// <summary>
	/// Applies the object's transparency information to its effect parameters.
	/// </summary>
	protected override void SyncTransparency()
	{
		Vector4 vector = fNn;
		Vector4 vector2 = fN_0019;
		if (TransparencyMode == TransparencyMode.Clip)
		{
			vector.Z = Transparency;
			vector2.X = Transparency;
		}
		else
		{
			vector.Z = 0f;
			vector2.X = 0f;
		}
		EffectHelper.f_0013(vector, ref fNn, ref fNj);
		EffectHelper.f_0013(vector2, ref fN_0019, ref fNo);
	}
}
