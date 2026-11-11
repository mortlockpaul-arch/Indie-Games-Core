using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;
using b;

namespace SynapseGaming.LightingSystem.Effects.Deferred;

public class DeferredObjectEffect : BaseMaterialEffect, IShadowGenerateEffect, IDeferredObjectEffect
{
	private Vector4 l8a;

	private Vector4 l8_u0013_;

	private Vector4 l8N;

	private DeferredEffectOutput l8l;

	private Texture2D l8L;

	private Texture2D l8w;

	private Vector2 l8J;

	private bool l8B;

	private float l8s;

	private float l8h;

	private Vector3 l8_u0002_;

	private Vector3 l8v;

	private Vector3[] l8Y = new Vector3[3];

	private EffectParameter l8_u0005_;

	private EffectParameter l8b;

	private int l8I;

	private EffectParameter l85;

	private EffectParameter l8W;

	private EffectParameter l8Z;

	private EffectParameter l8T;

	private EffectParameter l8_u0014_;

	private EffectParameter l8E;

	private EffectParameter l8f;

	private new EffectParameter l8_u0016_;

	private EffectParameter l86;

	public float ShadowPrimaryBias
	{
		get
		{
			return l8_u0013_.X;
		}
		set
		{
			EffectHelper.lL(new Vector4(value, l8_u0013_.Y, l8_u0013_.Z, l8_u0013_.W), ref l8_u0013_, ref l8T);
		}
	}

	public float ShadowSecondaryBias
	{
		get
		{
			return l8_u0013_.Y;
		}
		set
		{
			EffectHelper.lL(new Vector4(l8_u0013_.X, value, l8_u0013_.Z, l8_u0013_.W), ref l8_u0013_, ref l8T);
		}
	}

	public BoundingSphere ShadowArea
	{
		set
		{
			Vector4 param_ = new Vector4(value.Center, value.Radius);
			EffectHelper.lL(param_, ref l8N, ref l8_u0014_);
		}
	}

	public DeferredEffectOutput DeferredEffectOutput
	{
		get
		{
			return l8l;
		}
		set
		{
			if (value != l8l)
			{
				l8l = value;
				SetTechnique();
			}
		}
	}

	public Texture2D SceneLightingDiffuseMap
	{
		get
		{
			return l8L;
		}
		set
		{
			EffectHelper.lh(value, ref l8L, ref l8E);
			if (l8W != null && l8L != null)
			{
				EffectHelper.ls(new Vector2(l8L.Width, l8L.Height), ref l8J, ref l8W);
			}
		}
	}

	public Texture2D SceneLightingSpecularMap
	{
		get
		{
			return l8w;
		}
		set
		{
			EffectHelper.lh(value, ref l8w, ref l8f);
		}
	}

	public bool SupportsShadowGeneration => false;

	public bool FogEnabled
	{
		get
		{
			return l8B;
		}
		set
		{
			l8B = value;
			SetTechnique();
		}
	}

	public float FogStartDistance
	{
		get
		{
			return l8s;
		}
		set
		{
			NP(value, l8h);
		}
	}

	public float FogEndDistance
	{
		get
		{
			return l8h;
		}
		set
		{
			NP(l8s, value);
		}
	}

	public Vector3 FogColor
	{
		get
		{
			return l8_u0002_;
		}
		set
		{
			EffectHelper.lw(value, ref l8_u0002_, ref l86);
		}
	}

	public void SetAmbientLighting(IAmbientSource light, Vector3 directionhint)
	{
		if (l8_u0005_ != null && l8b != null && light is ILight)
		{
			ILight light2 = light as ILight;
			CoreHelper.DivergeLighting(light2.CompositeColorAndIntensity, light.Depth, 0.65f, out var color_high, out var color_low);
			l8Y[0] = color_high;
			l8Y[1] = color_low;
			ref Vector3 reference = ref l8Y[2];
			reference = (color_high + color_low) * 0.2f;
			l8_u0005_.SetValue(l8Y);
			EffectHelper.lw(directionhint, ref l8v, ref l8b);
		}
	}

	public void SetCameraView(Matrix view, Matrix viewtoworld)
	{
	}

	public DeferredObjectEffect(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "DeferredObjectEffect")
	{
		a(graphicsdevice);
	}

	internal DeferredObjectEffect(GraphicsDevice param_0, bool param_1)
		: base(param_0, "DeferredObjectEffect", param_1)
	{
		a(param_0);
	}

	protected override Effect Create()
	{
		return new DeferredObjectEffect(GraphicsDevice);
	}

	private void a(GraphicsDevice param_0)
	{
		l85 = Parameters["_PixelShaderIndex"];
		l8Z = Parameters["_TransClipRef"];
		l8T = Parameters["_OffsetBias_DepthBias_TransClipRef"];
		l8_u0014_ = Parameters["_Direction_Or_Position_And_Radius"];
		l8W = Parameters["_TargetWidthHeight"];
		l8_u0005_ = Parameters["_AmbientColor"];
		l8b = Parameters["_AmbientDirection"];
		l8E = Parameters["_SceneLightingDiffuseMap"];
		l8f = Parameters["_SceneLightingSpecularMap"];
		l8_u0016_ = Parameters["_FogStartDist_EndDistInv"];
		l86 = Parameters["_FogColor"];
		ShadowPrimaryBias = 1f;
		ShadowSecondaryBias = 0.2f;
		SetTechnique();
	}

	protected override void SetTechnique()
	{
		base.l8_u0016_.l8a.AccumulationValue++;
		if (l8l == DeferredEffectOutput.ShadowDepth)
		{
			bool param_ = TransparencyMap != null && TransparencyMode != TransparencyMode.None;
			b.l.w8_u0013_ w8_u0013_ = b.l.w8_u0013_.Point;
			w8_u0013_ = b.l.w8_u0013_.Point;
			CurrentTechnique = Techniques[b.l.lb(b.l.w8a.ShadowGen, w8_u0013_, 0, param_3: false, param_, Skinned, param_6: false)];
			return;
		}
		bool flag = TransparencyMap != null && TransparencyMode == TransparencyMode.Clip;
		_ = EffectDetail;
		bool flag2 = EffectDetail <= DetailPreference.Medium && TransparencyMode != TransparencyMode.Clip;
		_ = EffectDetail;
		if (l8l == DeferredEffectOutput.GBuffer)
		{
			b.l.w8_u0013_ w8_u0013_2 = b.l.w8_u0013_.Diffuse;
			w8_u0013_2 = ((!flag2 || _ParallaxMapTexture == null) ? b.l.w8_u0013_.DiffuseBump : b.l.w8_u0013_.DiffuseParallax);
			CurrentTechnique = Techniques[b.l.lb(b.l.w8a.DeferredGBuffer, w8_u0013_2, 0, DoubleSided, flag, Skinned, param_6: false)];
		}
		else if (l8l == DeferredEffectOutput.Depth)
		{
			b.l.w8_u0013_ param_2 = b.l.w8_u0013_.DiffuseBump;
			if (flag && flag2 && _ParallaxMapTexture != null)
			{
				param_2 = b.l.w8_u0013_.DiffuseParallax;
			}
			CurrentTechnique = Techniques[b.l.lb(b.l.w8a.DeferredDepth, param_2, 0, param_3: false, flag, Skinned, param_6: false)];
		}
		else if (l8l == DeferredEffectOutput.Final)
		{
			CurrentTechnique = Techniques[b.l.lb(b.l.w8a.DeferredFinal, b.l.w8_u0013_.None, 0, param_3: false, param_4: false, Skinned, param_6: false)];
			SetTechniqueShaderArrayIndices();
		}
	}

	protected override void SetTechniqueShaderArrayIndices()
	{
		if (l85 != null)
		{
			int num = 0;
			bool flag = TransparencyMap != null && TransparencyMode == TransparencyMode.Clip;
			if (EffectDetail <= DetailPreference.Medium && TransparencyMode != TransparencyMode.Clip && _ParallaxMapTexture != null)
			{
				num |= 1;
			}
			if (flag)
			{
				num |= 2;
			}
			if (l8B)
			{
				num |= 4;
			}
			num |= (int)_CurrentStaticLightingEffectMode << 3;
			if (num != l8I)
			{
				l8I = num;
				l85.SetValue(l8I);
				_UpdatedByBatch = true;
			}
		}
	}

	private void NP(float param_0, float param_1)
	{
		if (l8_u0016_ != null && (l8s != param_0 || l8h != param_1))
		{
			l8s = Math.Max(param_0, 0f);
			l8h = Math.Max(l8s * 1.01f, param_1);
			float num = l8h - l8s;
			if (num != 0f)
			{
				num = 1f / num;
			}
			l8_u0016_.SetValue(new Vector2(l8s, num));
		}
	}

	protected override void SyncTransparency(bool changedmode)
	{
		Vector4 param_ = l8_u0013_;
		Vector4 param_2 = l8a;
		TransparencyMode transparencyMode = TransparencyMode;
		Vector3 param_3 = Vector3.UnitY;
		if (transparencyMode != TransparencyMode.None)
		{
			param_.Z = TransparencyThreshold;
			param_2.X = TransparencyThreshold;
			switch (transparencyMode)
			{
			case TransparencyMode.Blend:
				param_3 = Vector3.UnitX;
				break;
			case TransparencyMode.Additive:
				param_3 = new Vector3(1f, 0f, 1f);
				break;
			}
		}
		else
		{
			param_.Z = 0f;
			param_2.X = 0f;
		}
		EffectHelper.lL(param_, ref l8_u0013_, ref l8T);
		EffectHelper.lL(param_2, ref l8a, ref l8Z);
		EffectHelper.lw(param_3, ref _AlphaPreblend_NoPreBlend_Additive, ref _AlphaPreblend_NoPreBlend_AdditiveParam);
		if (changedmode)
		{
			SetTechnique();
		}
	}

	protected override void SyncDiffuseAndNormalData(Vector4 diffusecolor, Texture2D diffusemap, Texture2D normalmap)
	{
		_DiffuseColorOriginal = diffusecolor;
		if (diffusemap == null || diffusemap == _DefaultDiffuseMapTexture)
		{
			EffectHelper.lh(_DefaultDiffuseMapTexture, ref _DiffuseMapTexture, ref _DiffuseMapTextureIndirectParam);
			EffectHelper.lL(diffusecolor, ref _DiffuseColorCached, ref _DiffuseColorIndirectParam);
		}
		else
		{
			EffectHelper.lh(diffusemap, ref _DiffuseMapTexture, ref _DiffuseMapTextureIndirectParam);
			EffectHelper.lL(new Vector4(Vector3.One, diffusecolor.W), ref _DiffuseColorCached, ref _DiffuseColorIndirectParam);
		}
		if (normalmap == null || normalmap == _DefaultNormalMapTexture)
		{
			EffectHelper.lh(_DefaultNormalMapTexture, ref _NormalMapTexture, ref _NormalMapTextureIndirectParam);
		}
		else
		{
			EffectHelper.lh(normalmap, ref _NormalMapTexture, ref _NormalMapTextureIndirectParam);
		}
	}
}
