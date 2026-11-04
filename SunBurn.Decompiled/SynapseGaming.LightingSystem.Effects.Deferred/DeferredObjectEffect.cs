using System;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;
using j;

namespace SynapseGaming.LightingSystem.Effects.Deferred;

/// <summary>
/// Provides SunBurn's built-in deferred object rendering. Supplies depth, diffuse,
/// normal, specular, fresnel, and emissive information to the deferred buffers for
/// later lighting, shadow, and material calculation.
/// </summary>
public class DeferredObjectEffect : BaseMaterialEffect, IShadowGenerateEffect, IDeferredObjectEffect
{
	private Vector4 fNB;

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

	private Vector3[] fN_0005 = new Vector3[3];

	private EffectParameter fNj;

	private EffectParameter fNo;

	private EffectParameter fNc;

	private EffectParameter fNd;

	private EffectParameter fNw;

	private EffectParameter fNs;

	private EffectParameter fN_0011;

	private EffectParameter fNQ;

	private EffectParameter fNG;

	private EffectParameter fN_0017;

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
			EffectHelper.f_0013(new Vector4(value, fNa.Y, fNa.Z, fNa.W), ref fNa, ref fNw);
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
			EffectHelper.f_0013(new Vector4(fNa.X, value, fNa.Z, fNa.W), ref fNa, ref fNw);
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
			EffectHelper.f_0013(vector, ref fNg, ref fNs);
		}
	}

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
			EffectHelper.fO(value, ref fN_0003, ref fN_0011);
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
			EffectHelper.fO(value, ref fNn, ref fNQ);
		}
	}

	/// <summary>
	/// Determines if the effect is capable of generating shadow maps. Objects using effects unable to
	/// generate shadow maps automatically use the built-in shadow effect, however this puts
	/// heavy restrictions on how the effects handle rendering (only basic vertex transforms are supported).
	/// </summary>
	public bool SupportsShadowGeneration => false;

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
			EffectHelper.fq(value, ref fN_0015, ref fN_0017);
		}
	}

	/// <summary>
	/// Sets scene ambient lighting (used during the Final rendering pass).
	/// </summary>
	public void SetAmbientLighting(IAmbientSource light, Vector3 directionhint)
	{
		if (fNj != null && fNo != null && light is ILight)
		{
			ILight light2 = light as ILight;
			L._0019.aQ(light2.CompositeColorAndIntensity, light.Depth, 0.65f, out var vector, out var vector2);
			fNi[0] = vector;
			fNi[1] = vector2;
			ref Vector3 reference = ref fNi[2];
			reference = (vector + vector2) * 0.2f;
			fNj.SetValue(fNi);
			EffectHelper.fq(directionhint, ref fN8, ref fNo);
		}
	}

	/// <summary>
	/// Clears the effect ambient lighting based on the provided value.
	/// </summary>
	/// <param name="clearambient"></param>
	protected override void SyncAmbientLighting(bool clearambient)
	{
		if (fNj != null)
		{
			if (clearambient)
			{
				fNj.SetValue(fN_0005);
			}
			else
			{
				fNj.SetValue(fNi);
			}
			_UpdatedByBatch = true;
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

	/// <summary>
	/// Creates a new DeferredObjectEffect instance.
	/// </summary>
	/// <param name="graphicsdevice"></param>
	public DeferredObjectEffect(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "DeferredObjectEffect")
	{
		B(graphicsdevice);
	}

	internal DeferredObjectEffect(GraphicsDevice P_0, bool P_1)
		: base(P_0, "DeferredObjectEffect", P_1)
	{
		B(P_0);
	}

	/// <summary>
	/// Creates a new empty effect of the same class type and using the same effect file as this object.
	/// </summary>
	/// <returns></returns>
	protected override Effect Create()
	{
		return new DeferredObjectEffect(GraphicsDevice);
	}

	private void B(GraphicsDevice P_0)
	{
		fNd = Parameters["_TransClipRef"];
		fNw = Parameters["_OffsetBias_DepthBias_TransClipRef"];
		fNs = Parameters["_Direction_Or_Position_And_Radius"];
		fNc = Parameters["_TargetWidthHeight"];
		fNj = Parameters["_AmbientColor"];
		fNo = Parameters["_AmbientDirection"];
		fN_0011 = Parameters["_SceneLightingDiffuseMap"];
		fNQ = Parameters["_SceneLightingSpecularMap"];
		fNG = Parameters["_FogStartDist_EndDistInv"];
		fN_0017 = Parameters["_FogColor"];
		ShadowPrimaryBias = 1f;
		ShadowSecondaryBias = 0.2f;
		SetTechnique();
	}

	/// <summary>
	/// Sets the effect technique based on its current property values.
	/// </summary>
	protected override void SetTechnique()
	{
		fNm.fNB.AccumulationValue++;
		bool flag = TransparencyMap != null && TransparencyMode != TransparencyMode.None;
		if (fNf == DeferredEffectOutput.ShadowDepth)
		{
			j.f.nNa nNa = j.f.nNa.Point;
			nNa = j.f.nNa.Point;
			CurrentTechnique = Techniques[j.f.fF(j.f.nNB.ShadowGen, nNa, 0, false, flag, Skinned, false)];
			return;
		}
		_ = EffectDetail;
		bool flag2 = EffectDetail <= DetailPreference.Medium && TransparencyMode == TransparencyMode.None;
		_ = EffectDetail;
		bool flag3 = _EmissiveMapTexture != null;
		if (fNf == DeferredEffectOutput.GBuffer)
		{
			j.f.nNa nNa2 = j.f.nNa.Diffuse;
			nNa2 = ((!flag2 || _ParallaxMapTexture == null) ? j.f.nNa.DiffuseBump : j.f.nNa.DiffuseParallax);
			CurrentTechnique = Techniques[j.f.fF(j.f.nNB.DeferredGBuffer, nNa2, 0, DoubleSided, flag, Skinned, false)];
		}
		else if (fNf == DeferredEffectOutput.Depth)
		{
			j.f.nNa nNa3 = j.f.nNa.DiffuseBump;
			if (flag && flag2 && _ParallaxMapTexture != null)
			{
				nNa3 = j.f.nNa.DiffuseParallax;
			}
			CurrentTechnique = Techniques[j.f.fF(j.f.nNB.DeferredDepth, nNa3, 0, false, flag, Skinned, false)];
		}
		else if (fNf == DeferredEffectOutput.Final)
		{
			j.f.nNa nNa4 = j.f.nNa.Diffuse;
			if (flag2 && _ParallaxMapTexture != null)
			{
				nNa4 = ((!flag3) ? j.f.nNa.DiffuseParallax : j.f.nNa.DiffuseParallaxEmissive);
			}
			else
			{
				nNa4 = ((!flag3) ? j.f.nNa.DiffuseBump : j.f.nNa.DiffuseBumpEmissive);
			}
			if (fNL)
			{
				CurrentTechnique = Techniques[j.f.fF(j.f.nNB.DeferredFinalFog, nNa4, 0, false, flag, Skinned, false)];
			}
			else
			{
				CurrentTechnique = Techniques[j.f.fF(j.f.nNB.DeferredFinal, nNa4, 0, false, flag, Skinned, false)];
			}
		}
	}

	private void fQ(float P_0, float P_1)
	{
		if (fNG != null && (fNv != P_0 || fN_0012 != P_1))
		{
			fNv = Math.Max(P_0, 0f);
			fN_0012 = Math.Max(fNv * 1.01f, P_1);
			float num = fN_0012 - fNv;
			if (num != 0f)
			{
				num = 1f / num;
			}
			fNG.SetValue(new Vector2(fNv, num));
		}
	}

	/// <summary>
	/// Applies the object's transparency information to its effect parameters.
	/// </summary>
	protected override void SyncTransparency(bool changedmode)
	{
		Vector4 vector = fNa;
		Vector4 vector2 = fNB;
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
		EffectHelper.f_0013(vector, ref fNa, ref fNw);
		EffectHelper.f_0013(vector2, ref fNB, ref fNd);
		if (changedmode)
		{
			SetTechnique();
		}
	}

	/// <summary>
	/// Applies the provided diffuse information to the object and its effect parameters.
	/// </summary>
	/// <param name="diffusecolor"></param>
	/// <param name="diffusemap"></param>
	/// <param name="normalmap"></param>
	protected override void SyncDiffuseAndNormalData(Vector4 diffusecolor, Texture2D diffusemap, Texture2D normalmap)
	{
		_DiffuseColorOriginal = diffusecolor;
		if (diffusemap == null || diffusemap == _DefaultDiffuseMapTexture)
		{
			EffectHelper.fO(_DefaultDiffuseMapTexture, ref _DiffuseMapTexture, ref _DiffuseMapTextureIndirectParam);
			EffectHelper.f_0013(diffusecolor, ref _DiffuseColorCached, ref _DiffuseColorIndirectParam);
		}
		else
		{
			EffectHelper.fO(diffusemap, ref _DiffuseMapTexture, ref _DiffuseMapTextureIndirectParam);
			EffectHelper.f_0013(Vector4.One, ref _DiffuseColorCached, ref _DiffuseColorIndirectParam);
		}
		if (normalmap == null || normalmap == _DefaultNormalMapTexture)
		{
			EffectHelper.fO(_DefaultNormalMapTexture, ref _NormalMapTexture, ref _NormalMapTextureIndirectParam);
		}
		else
		{
			EffectHelper.fO(normalmap, ref _NormalMapTexture, ref _NormalMapTextureIndirectParam);
		}
	}
}
