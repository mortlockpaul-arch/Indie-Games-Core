using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects.Deferred;

public class DeferredTerrainEffect : BaseTerrainEffect, IDeferredObjectEffect, IShadowGenerateEffect
{
	private Matrix l8a;

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

	private EffectParameter l8I;

	private EffectParameter l85;

	private EffectParameter l8W;

	private EffectParameter l8Z;

	private EffectParameter l8T;

	private EffectParameter l8_u0014_;

	private EffectParameter l8E;

	private EffectParameter l8f;

	public float ShadowPrimaryBias
	{
		get
		{
			return l8_u0013_.X;
		}
		set
		{
			EffectHelper.lL(new Vector4(value, l8_u0013_.Y, l8_u0013_.Z, l8_u0013_.W), ref l8_u0013_, ref l8W);
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
			EffectHelper.lL(new Vector4(l8_u0013_.X, value, l8_u0013_.Z, l8_u0013_.W), ref l8_u0013_, ref l8W);
		}
	}

	public BoundingSphere ShadowArea
	{
		set
		{
			Vector4 param_ = new Vector4(value.Center, value.Radius);
			EffectHelper.lL(param_, ref l8N, ref l8Z);
		}
	}

	public bool SupportsShadowGeneration => true;

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
			EffectHelper.lh(value, ref l8L, ref l8T);
			if (l85 != null && l8L != null)
			{
				EffectHelper.ls(new Vector2(l8L.Width, l8L.Height), ref l8J, ref l85);
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
			EffectHelper.lh(value, ref l8w, ref l8_u0014_);
		}
	}

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
			EffectHelper.lw(value, ref l8_u0002_, ref l8f);
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
		EffectHelper.l_u0013_(viewtoworld, ref l8a, ref l8I);
	}

	protected override void SetTechnique()
	{
		switch (l8l)
		{
		case DeferredEffectOutput.Depth:
			CurrentTechnique = Techniques["Terrain_Depth_Technique"];
			break;
		case DeferredEffectOutput.GBuffer:
			CurrentTechnique = Techniques["Terrain_GBuffer_Technique"];
			break;
		case DeferredEffectOutput.Final:
			if (l8B)
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

	public DeferredTerrainEffect(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "DeferredTerrainEffect")
	{
		a();
	}

	internal DeferredTerrainEffect(GraphicsDevice param_0, bool param_1)
		: base(param_0, "DeferredTerrainEffect", param_1)
	{
		a();
	}

	private void a()
	{
		l8I = Parameters["_CameraViewToWorld"];
		l8_u0005_ = Parameters["_AmbientColor"];
		l8b = Parameters["_AmbientDirection"];
		l8T = Parameters["_SceneLightingDiffuseMap"];
		l8_u0014_ = Parameters["_SceneLightingSpecularMap"];
		l85 = Parameters["_TargetWidthHeight"];
		l8W = Parameters["_OffsetBias_DepthBias_TransClipRef"];
		l8Z = Parameters["_Direction_Or_Position_And_Radius"];
		l8E = Parameters["_FogStartDist_EndDistInv"];
		l8f = Parameters["_FogColor"];
	}

	protected override Effect Create()
	{
		return new DeferredTerrainEffect(GraphicsDevice);
	}

	private void NP(float param_0, float param_1)
	{
		if (l8E != null && (l8s != param_0 || l8h != param_1))
		{
			l8s = Math.Max(param_0, 0f);
			l8h = Math.Max(l8s * 1.01f, param_1);
			float num = l8h - l8s;
			if (num != 0f)
			{
				num = 1f / num;
			}
			l8E.SetValue(new Vector2(l8s, num));
		}
	}
}
