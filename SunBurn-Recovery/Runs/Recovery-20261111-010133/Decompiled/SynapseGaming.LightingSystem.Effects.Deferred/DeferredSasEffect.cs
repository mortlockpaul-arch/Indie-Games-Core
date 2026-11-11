using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects.Deferred;

public class DeferredSasEffect : BaseSasEffect, IDeferredObjectEffect, IShadowGenerateEffect
{
	private bool l8a;

	private float l8_u0013_;

	private float l8N;

	private Vector3 l8l;

	private float l8L;

	private Vector4 l8w;

	private Vector4 l8J;

	private Vector4 l8B;

	private Vector2 l8s;

	private new Texture2D l8h;

	private Texture2D l8_u0002_;

	private EffectParameter l8v;

	private EffectParameter l8Y;

	private EffectParameter l8_u0005_;

	private EffectParameter l8b;

	private EffectParameter l8I;

	private EffectParameter l85;

	private EffectParameter l8W;

	private EffectParameter l8Z;

	private EffectParameter l8T;

	private DeferredEffectOutput l8_u0014_ = DeferredEffectOutput.GBuffer;

	public float ShadowPrimaryBias
	{
		get
		{
			return l8w.X;
		}
		set
		{
			EffectHelper.lL(new Vector4(value, l8w.Y, l8w.Z, l8w.W), ref l8w, ref l8b);
		}
	}

	public float ShadowSecondaryBias
	{
		get
		{
			return l8w.Y;
		}
		set
		{
			EffectHelper.lL(new Vector4(l8w.X, value, l8w.Z, l8w.W), ref l8w, ref l8b);
		}
	}

	public BoundingSphere ShadowArea
	{
		set
		{
			Vector4 param_ = new Vector4(value.Center, value.Radius);
			EffectHelper.lL(param_, ref l8B, ref l85);
		}
	}

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

	public DeferredEffectOutput DeferredEffectOutput
	{
		get
		{
			return l8_u0014_;
		}
		set
		{
			l8_u0014_ = value;
			SetTechnique();
		}
	}

	public Texture2D SceneLightingDiffuseMap
	{
		get
		{
			return l8h;
		}
		set
		{
			EffectHelper.lh(value, ref l8h, ref l8Z);
			if (l8W != null && l8h != null)
			{
				EffectHelper.ls(new Vector2(l8h.Width, l8h.Height), ref l8s, ref l8W);
			}
		}
	}

	public Texture2D SceneLightingSpecularMap
	{
		get
		{
			return l8_u0002_;
		}
		set
		{
			EffectHelper.lh(value, ref l8_u0002_, ref l8T);
		}
	}

	public bool FogEnabled
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
			NP(float.MaxValue, float.MaxValue);
		}
	}

	public float FogStartDistance
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			NP(value, l8N);
		}
	}

	public float FogEndDistance
	{
		get
		{
			return l8N;
		}
		set
		{
			NP(l8_u0013_, value);
		}
	}

	public Vector3 FogColor
	{
		get
		{
			return l8l;
		}
		set
		{
			EffectHelper.lw(value, ref l8l, ref l8Y);
		}
	}

	public void SetAmbientLighting(IAmbientSource light, Vector3 directionhint)
	{
		if (light is ILight)
		{
			EffectHelper.lL(param_1: new Vector4((light as ILight).CompositeColorAndIntensity, 1f), param_0: SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_AmbientLight_Color[0]));
		}
	}

	public void SetCameraView(Matrix view, Matrix viewtoworld)
	{
	}

	internal DeferredSasEffect(GraphicsDevice param_0, byte[] param_1, bool param_2)
		: base(param_0, param_1, param_2)
	{
		EffectByteCode = param_1;
		a();
	}

	private void a()
	{
		l8_u0005_ = FindBySemantic("FARCLIPDIST");
		l8b = FindBySemantic("SHADOWINFO");
		l85 = FindBySemantic("SHADOWSOURCE");
		l8W = FindBySemantic("TARGETINFO");
		l8I = FindBySemantic("RENDERINFO");
		l8v = FindBySemantic("FOGINFO");
		l8Y = FindBySemantic("FOGCOLOR");
		l8Z = FindBySemantic("SCENELIGHTINGDIFFUSEMAP");
		l8T = FindBySemantic("SCENELIGHTINGSPECULARMAP");
		FogEnabled = false;
	}

	protected override Effect Create()
	{
		return new DeferredSasEffect(GraphicsDevice, EffectByteCode, param_2: true);
	}

	protected override void SetTechnique()
	{
		base.l8h.l8a.AccumulationValue++;
		EffectTechnique effectTechnique = null;
		switch (l8_u0014_)
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

	private void NP(float param_0, float param_1)
	{
		if (l8v != null && (l8_u0013_ != param_0 || l8N != param_1))
		{
			l8_u0013_ = Math.Max(param_0, 0f);
			l8N = Math.Max(l8_u0013_ * 1.01f, param_1);
			float num = l8N - l8_u0013_;
			if (num != 0f)
			{
				num = 1f / num;
			}
			l8v.SetValue(new Vector2(l8_u0013_, num));
		}
	}

	protected override void SyncTransformEffectData()
	{
		base.SyncTransformEffectData();
		if (l8_u0005_ != null)
		{
			Vector4 vector = Vector4.Transform(new Vector4(0f, 0f, 1f, 1f), ProjectionToView);
			float num = 0f;
			if (vector.W != 0f)
			{
				num = Math.Abs(vector.Z / vector.W);
			}
			if (l8L != num)
			{
				l8L = num;
				l8_u0005_.SetValue(num);
			}
		}
	}

	protected override void SyncTransparency()
	{
		Vector4 param_ = l8w;
		Vector4 param_2 = l8J;
		if (TransparencyMode != TransparencyMode.None)
		{
			param_.Z = TransparencyThreshold;
			param_2.X = TransparencyThreshold;
		}
		else
		{
			param_.Z = 0f;
			param_2.X = 0f;
		}
		EffectHelper.lL(param_, ref l8w, ref l8b);
		EffectHelper.lL(param_2, ref l8J, ref l8I);
	}
}
