using System;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Effects.Forward;

public class XSIEffect : SasLightingEffect
{
	protected override void SetTechnique()
	{
		l8h.l8a.AccumulationValue++;
		if (Skinned)
		{
			SetTechnique("Skinned");
		}
		else
		{
			SetTechnique("Static");
		}
	}

	internal XSIEffect(GraphicsDevice param_0, byte[] param_1)
		: base(param_0, param_1, param_2: false)
	{
		BindBySasAddress(FindByName("AmbientColor"), BaseSasBindEffect.SASAddress_AmbientLight_Color[0]);
		SkinBonesEffectParameter = FindByName("Bones");
		int num = BaseSasBindEffect.SASAddress_PointLight_Position.Length;
		for (int i = 0; i < num; i++)
		{
			BindBySasAddress(FindBySasAddress("Sas.PointLights[" + i + "].Position"), BaseSasBindEffect.SASAddress_PointLight_Position[i]);
			BindBySasAddress(FindBySasAddress("Sas.PointLights[" + i + "].Color"), BaseSasBindEffect.SASAddress_PointLight_Color[i]);
		}
		FindMaxLightCount();
		SetMaxLightCount(Math.Min(MaxLightSources, 3));
	}
}
