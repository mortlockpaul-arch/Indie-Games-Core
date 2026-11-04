using System;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Effects.Forward;

/// <summary>
/// Provides support for XSI shaders.
/// </summary>
public class XSIEffect : SasLightingEffect
{
	/// <summary>
	/// Sets the effect technique based on its current property values.
	/// </summary>
	protected override void SetTechnique()
	{
		fN_0019.fNB.AccumulationValue++;
		if (Skinned)
		{
			SetTechnique("Skinned");
		}
		else
		{
			SetTechnique("Static");
		}
	}

	/// <summary>
	/// Creates a new XSIEffect instance from an effect containing an XSI shader
	/// (often loaded through the content pipeline or from disk).
	/// </summary>
	/// <param name="graphicsdevice"></param>
	/// <param name="effectbytecode"></param>
	internal XSIEffect(GraphicsDevice P_0, byte[] P_1)
		: base(P_0, P_1, false)
	{
		BindBySasAddress(FindByName("AmbientColor"), BaseSasBindEffect.SASAddress_AmbientLight_Color[0]);
		SkinBonesEffectParameter = FindByName("Bones");
		int num = BaseSasBindEffect.SASAddress_PointLight_Position.Length;
		for (int k = 0; k < num; k++)
		{
			BindBySasAddress(FindBySasAddress("Sas.PointLights[" + k + "].Position"), BaseSasBindEffect.SASAddress_PointLight_Position[k]);
			BindBySasAddress(FindBySasAddress("Sas.PointLights[" + k + "].Color"), BaseSasBindEffect.SASAddress_PointLight_Color[k]);
		}
		FindMaxLightCount();
		SetMaxLightCount(Math.Min(MaxLightSources, 3));
	}
}
