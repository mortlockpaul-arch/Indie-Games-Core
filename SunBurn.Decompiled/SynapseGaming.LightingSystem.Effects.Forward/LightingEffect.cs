using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects.Forward;

/// <summary>
/// Effect provides SunBurn's built-in lighting and material support.
///
/// Including:
/// -Diffuse mapping
/// -Bump mapping
/// -Specular mapping (with specular intensity mapping)
/// -Point, spot, directional, and ambient lighting
/// </summary>
public class LightingEffect : BaseMaterialEffect, ILightingEffect
{
	private const int fNB = 1;

	private EffectParameter fNa;

	private EffectParameter fNg;

	private EffectParameter fNf;

	private static Vector4 fN_0003 = default;

	private static Vector4 fNn = default;

	private static Vector4 fN_0019 = default;

	private static Vector4 fNL = default;

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
			fm(value);
			fNm.fNg.AccumulationValue++;
		}
	}

	/// <summary>
	/// Clears the effect ambient lighting based on the provided value.
	/// </summary>
	/// <param name="clearambient"></param>
	protected override void SyncAmbientLighting(bool clearambient)
	{
		if (fNa != null)
		{
			if (clearambient)
			{
				fNa.SetValue(fN_0003);
			}
			else
			{
				fNa.SetValue(fNn);
			}
			_UpdatedByBatch = true;
		}
	}

	private void fm(List<BaseLight> P_0)
	{
		if (fNa == null || fNg == null || fNf == null || P_0 == null)
		{
			return;
		}
		if (P_0.Count != 1)
		{
			throw new ArgumentException("LightingEffect only supports a single light per-pass at this time.");
		}
		_CurrentLight = P_0[0];
		Vector3 compositeColorAndIntensity = _CurrentLight.CompositeColorAndIntensity;
		fNn = new Vector4(compositeColorAndIntensity, 0f);
		fNL = default;
		L.o o2 = OptimizationSystem.fNB.g_0011(_CurrentLight);
		IPointSource pointSource = o2.fNB;
		if (pointSource != null)
		{
			ISpotSource spotSource = o2.fNa;
			if (spotSource != null)
			{
				float value = spotSource.Angle * 0.5f;
				float num = (float)Math.Cos(MathHelper.ToRadians(MathHelper.Clamp(value, 0.01f, 89.99f)));
				float num2 = 1f / (1f - num);
				fNn.W = num2;
				fNL = new Vector4(spotSource.Direction, num);
				fN_0019 = new Vector4(spotSource.Position, spotSource.Radius);
			}
			else
			{
				fN_0019 = new Vector4(pointSource.Position, pointSource.Radius);
			}
		}
		else if (o2.fN_0003 != null)
		{
			fN_0019 = new Vector4(o2.fN_0003.ShadowPosition, 1E+09f);
		}
		fNa.SetValue(fNn);
		fNg.SetValue(fN_0019);
		fNf.SetValue(fNL);
		SetTechnique();
	}

	/// <summary>
	/// Creates a new LightingEffect instance.
	/// </summary>
	/// <param name="graphicsdevice"></param>
	public LightingEffect(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "LightingEffect")
	{
		B(graphicsdevice);
	}

	internal LightingEffect(GraphicsDevice P_0, bool P_1)
		: base(P_0, "LightingEffect", P_1)
	{
		B(P_0);
	}

	/// <summary>
	/// Creates a new empty effect of the same class type and using the same effect file as this object.
	/// </summary>
	/// <returns></returns>
	protected override Effect Create()
	{
		return new LightingEffect(GraphicsDevice);
	}

	private void B(GraphicsDevice P_0)
	{
		fNa = Parameters["_DiffuseColor_And_SpotAngleInv"];
		fNg = Parameters["_Position_And_Radius"];
		fNf = Parameters["_SpotDirection_And_SpotAngle"];
	}
}
