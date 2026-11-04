using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Effects;

/// <summary>
/// Provides methods that help with effect setup and data synchronization.
/// </summary>
public class EffectHelper
{
	internal static void fR(Matrix P_0, ref Matrix P_1, ref EffectParameter P_2)
	{
		if (P_2 == null || P_0 == P_1)
		{
			P_1 = P_0;
			return;
		}
		P_1 = P_0;
		P_2.SetValue(P_1);
	}

	internal static void fW(Matrix[] P_0, ref Matrix[] P_1, ref EffectParameter P_2)
	{
		P_1 = P_0;
		if (P_2 != null && P_0 != null)
		{
			Math.Min(P_0.Length, P_2.Elements.Count);
			P_2.SetValue(P_0);
		}
	}

	internal static void f9(Matrix P_0, ref Matrix P_1, ref Matrix P_2, ref EffectParameter P_3, ref EffectParameter P_4)
	{
		if (!(P_0 == P_1))
		{
			P_1 = P_0;
			if (P_3 != null)
			{
				P_3.SetValue(P_1);
			}
			if (P_4 != null)
			{
				P_2 = Matrix.Invert(P_1);
				P_4.SetValue(P_2);
			}
		}
	}

	internal static void f_0013(Vector4 P_0, ref Vector4 P_1, ref EffectParameter P_2)
	{
		if (P_2 != null && !(P_0 == P_1))
		{
			P_1 = P_0;
			P_2.SetValue(P_1);
		}
	}

	internal static void fq(Vector3 P_0, ref Vector3 P_1, ref EffectParameter P_2)
	{
		if (P_2 != null && !(P_0 == P_1))
		{
			P_1 = P_0;
			P_2.SetValue(P_1);
		}
	}

	internal static void f_000F(int P_0, ref int P_1, ref EffectParameter P_2)
	{
		if (P_2 != null && P_0 != P_1)
		{
			P_1 = P_0;
			P_2.SetValue(P_1);
		}
	}

	internal static void f_0014(float P_0, ref float P_1, ref EffectParameter P_2)
	{
		if (P_2 != null && P_0 != P_1)
		{
			P_1 = P_0;
			P_2.SetValue(P_1);
		}
	}

	internal static void fr(Vector2 P_0, ref Vector2 P_1, ref EffectParameter P_2)
	{
		if (P_2 != null && !(P_0 == P_1))
		{
			P_1 = P_0;
			P_2.SetValue(P_1);
		}
	}

	internal static void fO(Texture2D P_0, ref Texture2D P_1, ref EffectParameter P_2)
	{
		if (P_2 != null && P_0 != P_1)
		{
			P_1 = P_0;
			P_2.SetValue(P_1);
		}
	}

	internal static void fO(Texture2D P_0, Texture2D P_1, ref Texture2D P_2, ref EffectParameter P_3)
	{
		if (P_0 == null)
		{
			P_0 = P_1;
		}
		if (P_3 != null && P_0 != P_2)
		{
			P_2 = P_0;
			P_3.SetValue(P_2);
		}
	}

	internal static void f_0013(List<EffectParameter> P_0, Vector4 P_1)
	{
		if (P_0 == null || P_0.Count < 1)
		{
			return;
		}
		for (int k = 0; k < P_0.Count; k++)
		{
			EffectParameter effectParameter = P_0[k];
			if (effectParameter.ParameterType == EffectParameterType.Int32 && effectParameter.RowCount == 1)
			{
				effectParameter.SetValue((int)P_1.X);
			}
			if (effectParameter.ParameterType == EffectParameterType.Single && effectParameter.RowCount <= 1)
			{
				if (effectParameter.ColumnCount == 1)
				{
					effectParameter.SetValue(P_1.X);
				}
				else if (effectParameter.ColumnCount == 2)
				{
					effectParameter.SetValue(new Vector2(P_1.X, P_1.Y));
				}
				else if (effectParameter.ColumnCount == 3)
				{
					effectParameter.SetValue(new Vector3(P_1.X, P_1.Y, P_1.Z));
				}
				else
				{
					effectParameter.SetValue(P_1);
				}
			}
		}
	}

	internal static void fR(List<EffectParameter> P_0, Matrix P_1)
	{
		if (P_0 != null && P_0.Count >= 1)
		{
			for (int k = 0; k < P_0.Count; k++)
			{
				P_0[k].SetValue(P_1);
			}
		}
	}

	internal static void fu(List<EffectParameter> P_0, Matrix P_1)
	{
		if (P_0 != null && P_0.Count >= 1)
		{
			fR(P_0, Matrix.Transpose(P_1));
		}
	}

	/// <summary>
	/// Synchronizes all recognized object effect properties with the shadow effect.
	/// Allows shadow effects to support material transparency.
	/// </summary>
	/// <param name="objeffect">The object's effect</param>
	/// <param name="shadoweffect">The shadow effect</param>
	public static void SyncObjectAndShadowEffects(Effect objeffect, Effect shadoweffect)
	{
		L.d d2 = OptimizationSystem.fNg.g_0011(objeffect);
		L.d d3 = OptimizationSystem.fNg.g_0011(shadoweffect);
		f1(d2, d3);
	}

	internal static void f1(L.d P_0, L.d P_1)
	{
		if (P_1.fNB != null)
		{
			if (P_0.fNB != null)
			{
				P_1.fNB.DoubleSided = P_0.fNB.DoubleSided;
			}
			else
			{
				P_1.fNB.DoubleSided = false;
			}
		}
		if (P_1.fN_0003 != null)
		{
			ITransparentEffect transparentEffect = P_1.fN_0003;
			if (P_0.fN_0003 != null)
			{
				ITransparentEffect transparentEffect2 = P_0.fN_0003;
				if (transparentEffect2.TransparencyMode != TransparencyMode.None)
				{
					transparentEffect.SetTransparencyModeAndMap(transparentEffect2.TransparencyMode, transparentEffect2.Transparency, transparentEffect2.TransparencyMap);
				}
				else
				{
					transparentEffect.SetTransparencyModeAndMap(TransparencyMode.None, transparentEffect.Transparency, null);
				}
			}
			else
			{
				transparentEffect.SetTransparencyModeAndMap(TransparencyMode.None, transparentEffect.Transparency, null);
			}
		}
		if (P_1.fN_0019 != null)
		{
			IAddressableEffect addressableEffect = P_1.fN_0019;
			if (P_0.fN_0019 != null)
			{
				IAddressableEffect addressableEffect2 = P_0.fN_0019;
				addressableEffect.AddressModeU = addressableEffect2.AddressModeU;
				addressableEffect.AddressModeV = addressableEffect2.AddressModeV;
				addressableEffect.AddressModeW = addressableEffect2.AddressModeW;
			}
			else
			{
				addressableEffect.AddressModeU = TextureAddressMode.Wrap;
				addressableEffect.AddressModeV = TextureAddressMode.Wrap;
				addressableEffect.AddressModeW = TextureAddressMode.Wrap;
			}
		}
		if (P_1.fNn != null)
		{
			ITerrainEffect fNn = P_1.fNn;
			if (P_0.fNn != null)
			{
				ITerrainEffect fNn2 = P_0.fNn;
				fNn.HeightMapTexture = fNn2.HeightMapTexture;
				fNn.HeightScale = fNn2.HeightScale;
				fNn.MeshSegments = fNn2.MeshSegments;
				fNn.Tiling = fNn2.Tiling;
			}
			else
			{
				fNn.HeightMapTexture = null;
			}
		}
	}
}
