using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects.Forward;

/// <summary>
/// Provides SunBurn's built-in forward terrain rendering.
/// </summary>
public class TerrainEffect : BaseTerrainEffect, ILightingEffect
{
	private const int fNB = 1;

	private BaseLight fNa = new AmbientLight();

	private EffectParameter fNg;

	private EffectParameter fNf;

	private EffectParameter fN_0003;

	private EffectParameter fNn;

	private static Vector4 fN_0019;

	private static Vector4 fNL;

	private static Vector4 fNv;

	/// <summary>
	/// Maximum number of light sources the effect supports.
	/// </summary>
	public int MaxLightSources => 1;

	/// <summary>
	/// Light sources that apply lighting to the effect during rendering.
	/// </summary>
	public List<BaseLight> LightSources
	{
		set
		{
			if (value.Count != 1)
			{
				throw new ArgumentException("TerrainEffect only supports a single light per-pass at this time.");
			}
			fNa = value[0];
			fm();
			fNm.fNg.AccumulationValue++;
		}
	}

	private void fm()
	{
		if (fNf == null || fN_0003 == null || fNn == null || fNa == null)
		{
			return;
		}
		fN_0019 = new Vector4(fNa.CompositeColorAndIntensity, 0f);
		fNv = default;
		L.o o2 = OptimizationSystem.fNB.g_0011(fNa);
		IPointSource pointSource = o2.fNB;
		if (pointSource != null)
		{
			ISpotSource spotSource = o2.fNa;
			if (spotSource != null)
			{
				float value = spotSource.Angle * 0.5f;
				float num = (float)Math.Cos(MathHelper.ToRadians(MathHelper.Clamp(value, 0.01f, 89.99f)));
				float num2 = 1f / (1f - num);
				fN_0019.W = num2;
				fNv = new Vector4(spotSource.Direction, num);
				fNL = new Vector4(spotSource.Position, spotSource.Radius);
			}
			else
			{
				fNL = new Vector4(pointSource.Position, pointSource.Radius);
			}
		}
		else if (o2.fN_0003 != null)
		{
			fNL = new Vector4(o2.fN_0003.ShadowPosition, 1E+09f);
		}
		fNf.SetValue(fN_0019);
		fN_0003.SetValue(fNL);
		fNn.SetValue(fNv);
		SetTechnique();
	}

	/// <summary>
	/// Sets the effect technique based on its current property values.
	/// </summary>
	protected override void SetTechnique()
	{
		if (fNa is IAmbientSource)
		{
			CurrentTechnique = Techniques["Terrain_Ambient_Technique"];
		}
		else
		{
			CurrentTechnique = Techniques["Terrain_Technique"];
		}
	}

	/// <summary>
	/// Creates a new TerrainEffect instance.
	/// </summary>
	/// <param name="graphicsdevice"></param>
	public TerrainEffect(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "TerrainEffect")
	{
		B(graphicsdevice);
	}

	internal TerrainEffect(GraphicsDevice P_0, bool P_1)
		: base(P_0, "TerrainEffect", P_1)
	{
		B(P_0);
	}

	private void B(GraphicsDevice P_0)
	{
		fNg = Parameters["_LightingTexture"];
		fNf = Parameters["_DiffuseColor_And_SpotAngleInv"];
		fN_0003 = Parameters["_Position_And_Radius"];
		fNn = Parameters["_SpotDirection_And_SpotAngle"];
	}

	/// <summary>
	/// Creates a new empty effect of the same class type and using the same effect file as this object.
	/// </summary>
	/// <returns></returns>
	protected override Effect Create()
	{
		return new TerrainEffect(GraphicsDevice);
	}
}
