using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects.Forward;

/// <summary>
/// Effect class with complete support for, and binding of, FX Standard Annotations and Semantics (SAS).
/// </summary>
public class SasLightingEffect : SasEffect, ILightingEffect
{
	private int fNB = 1;

	private List<BaseLight> fNa = new List<BaseLight>();

	/// <summary>
	/// Maximum number of light sources the effect supports.
	/// </summary>
	public int MaxLightSources => fNB;

	/// <summary>
	/// Light sources that apply lighting to the effect during rendering.
	/// </summary>
	public List<BaseLight> LightSources
	{
		set
		{
			fNa.Clear();
			foreach (BaseLight item in value)
			{
				fNa.Add(item);
			}
			SyncLightSourceEffectData();
			fN_0019.fNa.AccumulationValue++;
		}
	}

	/// <summary>
	/// Sets the max light count supported by the effect.
	/// </summary>
	/// <param name="maxlights"></param>
	protected virtual void SetMaxLightCount(int maxlights)
	{
		fNB = maxlights;
	}

	/// <summary>
	/// Finds the max light count supported by the effect's shader.
	/// </summary>
	protected virtual void FindMaxLightCount()
	{
		fNB = 0;
		int num = 0;
		int num2 = 0;
		int num3 = BaseSasBindEffect.SASAddress_AmbientLight_Color.Length;
		for (int k = 0; k < num3; k++)
		{
			if (SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_DirectionalLight_Color[k]) != null)
			{
				num = k + 1;
			}
			if (SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Color[k]) != null)
			{
				num2 = k + 1;
			}
		}
		if (num2 < 1)
		{
			fNB = num;
		}
		else if (num < 1)
		{
			fNB = num2;
		}
		else
		{
			fNB = Math.Min(num, num2);
		}
	}

	/// <summary>
	/// Applies the current lighting information to the bound effect parameters.
	/// </summary>
	protected virtual void SyncLightSourceEffectData()
	{
		if (fNa.Count < 1)
		{
			return;
		}
		bool flag = SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_DirectionalLight_Color[0]) != null;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int k = 0; k < fNa.Count; k++)
		{
			BaseLight baseLight = fNa[0];
			L.o o2 = OptimizationSystem.fNB.g_0011(baseLight);
			if (o2.fNf != null)
			{
				EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_AmbientLight_Color[num]), new Vector4(baseLight.CompositeColorAndIntensity, 1f));
				num++;
			}
			else if (o2.fNB != null)
			{
				EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Color[num3]), new Vector4(baseLight.CompositeColorAndIntensity, 1f));
				EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Position[num3]), new Vector4(o2.fNB.Position, 1f));
				EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Range[num3]), new Vector4(o2.fNB.Radius));
				num3++;
			}
			else if (o2.fNg != null)
			{
				if (flag)
				{
					EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_DirectionalLight_Color[num2]), new Vector4(baseLight.CompositeColorAndIntensity, 1f));
					EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_DirectionalLight_Direction[num2]), new Vector4(o2.fNg.Direction, 1f));
					num2++;
				}
				else if (o2.fN_0003 != null)
				{
					EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Color[num3]), new Vector4(baseLight.CompositeColorAndIntensity, 1f));
					EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Position[num3]), new Vector4(o2.fN_0003.ShadowPosition, 1f));
					EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Range[num3]), new Vector4(1E+09f));
					num3++;
				}
			}
		}
		int num4 = BaseSasBindEffect.SASAddress_AmbientLight_Color.Length;
		for (int l = num; l < num4; l++)
		{
			EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_AmbientLight_Color[l]), new Vector4(0f));
		}
		for (int m = num2; m < num4; m++)
		{
			EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_DirectionalLight_Color[m]), new Vector4(0f));
		}
		for (int num5 = num3; num5 < num4; num5++)
		{
			EffectHelper.f_0013(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Color[num5]), new Vector4(0f));
		}
		EffectHelper.f_0013(SasAutoBindTable.Find("Sas.NumAmbientLights"), new Vector4(num));
		EffectHelper.f_0013(SasAutoBindTable.Find("Sas.NumDirectionalLights"), new Vector4(num2));
		EffectHelper.f_0013(SasAutoBindTable.Find("Sas.NumPointLights"), new Vector4(num3));
	}

	internal SasLightingEffect(GraphicsDevice P_0, byte[] P_1, bool P_2)
		: base(P_0, P_1, P_2)
	{
		EffectByteCode = P_1;
		FindMaxLightCount();
	}

	/// <summary>
	/// Creates a new empty effect of the same class type and using the same effect file as this object.
	/// </summary>
	/// <returns></returns>
	protected override Effect Create()
	{
		return new SasLightingEffect(GraphicsDevice, EffectByteCode, true);
	}
}
