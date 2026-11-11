using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects.Forward;

public class TerrainEffect : BaseTerrainEffect, ILightingEffect
{
	private const int l8a = 1;

	private BaseLight l8_u0013_ = new AmbientLight();

	private EffectParameter l8N;

	private EffectParameter l8l;

	private EffectParameter l8L;

	private EffectParameter l8w;

	private static Vector4 l8J;

	private static Vector4 l8B;

	private static Vector4 l8s;

	public int MaxLightSources => 1;

	public List<BaseLight> LightSources
	{
		set
		{
			if (value.Count != 1)
			{
				throw new ArgumentException("TerrainEffect only supports a single light per-pass at this time.");
			}
			l8_u0013_ = value[0];
			N8();
			l8_u0016_.l8N.AccumulationValue++;
		}
	}

	private void N8()
	{
		if (l8l == null || l8L == null || l8w == null || l8_u0013_ == null)
		{
			return;
		}
		l8J = new Vector4(l8_u0013_.CompositeColorAndIntensity, 0f);
		l8s = default;
		LightTypeCaster lightTypeCaster = OptimizationSystem.LightTypeCasters.Get(l8_u0013_);
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
		SetTechnique();
	}

	protected override void SetTechnique()
	{
		if (l8_u0013_ is IAmbientSource)
		{
			CurrentTechnique = Techniques["Terrain_Ambient_Technique"];
		}
		else
		{
			CurrentTechnique = Techniques["Terrain_Technique"];
		}
	}

	public TerrainEffect(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "TerrainEffect")
	{
		a(graphicsdevice);
	}

	internal TerrainEffect(GraphicsDevice param_0, bool param_1)
		: base(param_0, "TerrainEffect", param_1)
	{
		a(param_0);
	}

	private void a(GraphicsDevice param_0)
	{
		l8N = Parameters["_LightingTexture"];
		l8l = Parameters["_DiffuseColor_And_SpotAngleInv"];
		l8L = Parameters["_Position_And_Radius"];
		l8w = Parameters["_SpotDirection_And_SpotAngle"];
	}

	protected override Effect Create()
	{
		return new TerrainEffect(GraphicsDevice);
	}
}
