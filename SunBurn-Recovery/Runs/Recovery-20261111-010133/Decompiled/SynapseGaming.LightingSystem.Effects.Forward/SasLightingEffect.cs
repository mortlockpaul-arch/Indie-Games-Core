using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects.Forward;

public class SasLightingEffect : SasEffect, ILightingEffect
{
	private int l8a = 1;

	private List<BaseLight> l8_u0013_ = new List<BaseLight>();

	public int MaxLightSources => l8a;

	public List<BaseLight> LightSources
	{
		set
		{
			l8_u0013_.Clear();
			foreach (BaseLight item in value)
			{
				l8_u0013_.Add(item);
			}
			SyncLightSourceEffectData();
			l8h.l8_u0013_.AccumulationValue++;
		}
	}

	protected virtual void SetMaxLightCount(int maxlights)
	{
		l8a = maxlights;
	}

	protected virtual void FindMaxLightCount()
	{
		l8a = 0;
		int num = 0;
		int num2 = 0;
		int num3 = BaseSasBindEffect.SASAddress_AmbientLight_Color.Length;
		for (int i = 0; i < num3; i++)
		{
			if (SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_DirectionalLight_Color[i]) != null)
			{
				num = i + 1;
			}
			if (SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Color[i]) != null)
			{
				num2 = i + 1;
			}
		}
		if (num2 < 1)
		{
			l8a = num;
		}
		else if (num < 1)
		{
			l8a = num2;
		}
		else
		{
			l8a = Math.Min(num, num2);
		}
	}

	protected virtual void SyncLightSourceEffectData()
	{
		if (l8_u0013_.Count < 1)
		{
			return;
		}
		bool flag = SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_DirectionalLight_Color[0]) != null;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < l8_u0013_.Count; i++)
		{
			BaseLight baseLight = l8_u0013_[0];
			LightTypeCaster lightTypeCaster = OptimizationSystem.LightTypeCasters.Get(baseLight);
			if (lightTypeCaster.AmbientSource != null)
			{
				EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_AmbientLight_Color[num]), new Vector4(baseLight.CompositeColorAndIntensity, 1f));
				num++;
			}
			else if (lightTypeCaster.PointSource != null)
			{
				EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Color[num3]), new Vector4(baseLight.CompositeColorAndIntensity, 1f));
				EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Position[num3]), new Vector4(lightTypeCaster.PointSource.Position, 1f));
				EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Range[num3]), new Vector4(lightTypeCaster.PointSource.Radius));
				num3++;
			}
			else if (lightTypeCaster.DirectionalSource != null)
			{
				if (flag)
				{
					EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_DirectionalLight_Color[num2]), new Vector4(baseLight.CompositeColorAndIntensity, 1f));
					EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_DirectionalLight_Direction[num2]), new Vector4(lightTypeCaster.DirectionalSource.Direction, 1f));
					num2++;
				}
				else if (lightTypeCaster.ShadowSource != null)
				{
					EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Color[num3]), new Vector4(baseLight.CompositeColorAndIntensity, 1f));
					EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Position[num3]), new Vector4(lightTypeCaster.ShadowSource.ShadowPosition, 1f));
					EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Range[num3]), new Vector4(1E+09f));
					num3++;
				}
			}
		}
		int num4 = BaseSasBindEffect.SASAddress_AmbientLight_Color.Length;
		for (int j = num; j < num4; j++)
		{
			EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_AmbientLight_Color[j]), new Vector4(0f));
		}
		for (int k = num2; k < num4; k++)
		{
			EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_DirectionalLight_Color[k]), new Vector4(0f));
		}
		for (int m = num3; m < num4; m++)
		{
			EffectHelper.lL(SasAutoBindTable.Find(BaseSasBindEffect.SASAddress_PointLight_Color[m]), new Vector4(0f));
		}
		EffectHelper.lL(SasAutoBindTable.Find("Sas.NumAmbientLights"), new Vector4(num));
		EffectHelper.lL(SasAutoBindTable.Find("Sas.NumDirectionalLights"), new Vector4(num2));
		EffectHelper.lL(SasAutoBindTable.Find("Sas.NumPointLights"), new Vector4(num3));
	}

	internal SasLightingEffect(GraphicsDevice param_0, byte[] param_1, bool param_2)
		: base(param_0, param_1, param_2)
	{
		EffectByteCode = param_1;
		FindMaxLightCount();
	}

	protected override Effect Create()
	{
		return new SasLightingEffect(GraphicsDevice, EffectByteCode, param_2: true);
	}
}
