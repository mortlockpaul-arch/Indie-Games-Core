using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects.Forward;

public class LightingEffect : BaseMaterialEffect, ILightingEffect
{
	private const int l8a = 1;

	private int l8_u0013_;

	private EffectParameter l8N;

	private EffectParameter l8l;

	private EffectParameter l8L;

	private EffectParameter l8w;

	private static Vector4 l8J = default;

	private static Vector4 l8B = default;

	private static Vector4 l8s = default;

	public int MaxLightSources => 1;

	public List<BaseLight> LightSources
	{
		set
		{
			N8(value);
			l8_u0016_.l8N.AccumulationValue++;
		}
	}

	private void N8(List<BaseLight> param_0)
	{
		if (l8l == null || l8L == null || l8w == null || param_0 == null)
		{
			return;
		}
		if (param_0.Count != 1)
		{
			throw new ArgumentException("LightingEffect only supports a single light per-pass at this time.");
		}
		BaseLight baseLight = param_0[0];
		bool flag = baseLight == _CurrentLight;
		_CurrentLight = baseLight;
		Vector3 compositeColorAndIntensity = _CurrentLight.CompositeColorAndIntensity;
		l8J = new Vector4(compositeColorAndIntensity, 0f);
		l8s = default;
		LightTypeCaster lightTypeCaster = OptimizationSystem.LightTypeCasters.Get(_CurrentLight);
		IPointSource pointSource = lightTypeCaster.PointSource;
		if (pointSource != null)
		{
			ISpotSource spotSource = lightTypeCaster.SpotSource;
			if (spotSource != null)
			{
				float value = spotSource.Angle * 0.5f;
				float num = (float)Math.Cos(MathHelper.ToRadians(MathHelper.Clamp(value, 0.01f, 89.99f)));
				float num2 = 1f / (1f - num);
				l8J.W = num2;
				l8s = new Vector4(spotSource.Direction, num);
				l8B = new Vector4(spotSource.Position, spotSource.Radius);
			}
			else
			{
				l8B = new Vector4(pointSource.Position, pointSource.Radius);
			}
		}
		else if (lightTypeCaster.ShadowSource != null)
		{
			l8B = new Vector4(lightTypeCaster.ShadowSource.ShadowPosition, 1E+09f);
		}
		l8l.SetValue(l8J);
		l8L.SetValue(l8B);
		l8w.SetValue(l8s);
		if (!flag)
		{
			SetTechnique();
		}
	}

	protected override void SetTechniqueShaderArrayIndices()
	{
		if (l8N == null)
		{
			return;
		}
		int num = 0;
		if (TransparencyMap != null)
		{
			_ = TransparencyMode;
		}
		bool flag = EffectDetail <= DetailPreference.Medium && TransparencyMode != TransparencyMode.Clip;
		if (_NormalMapTexture != null)
		{
			if (_EmissiveMapTexture != _DefaultEmissiveMapTexture)
			{
				num |= 1;
			}
			if (flag && _ParallaxMapTexture != null)
			{
				num |= 2;
			}
			num |= (int)_CurrentStaticLightingEffectMode << 2;
		}
		else
		{
			if (_EmissiveMapTexture != null)
			{
				num |= 1;
			}
			num |= (int)_CurrentStaticLightingEffectMode << 1;
		}
		if (num != l8_u0013_)
		{
			l8_u0013_ = num;
			l8N.SetValue(l8_u0013_);
			_UpdatedByBatch = true;
		}
	}

	public LightingEffect(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "LightingEffect")
	{
		a(graphicsdevice);
	}

	internal LightingEffect(GraphicsDevice param_0, bool param_1)
		: base(param_0, "LightingEffect", param_1)
	{
		a(param_0);
	}

	protected override Effect Create()
	{
		return new LightingEffect(GraphicsDevice);
	}

	private void a(GraphicsDevice param_0)
	{
		l8N = Parameters["_PixelShaderIndex"];
		l8l = Parameters["_DiffuseColor_And_SpotAngleInv"];
		l8L = Parameters["_Position_And_Radius"];
		l8w = Parameters["_SpotDirection_And_SpotAngle"];
	}
}
