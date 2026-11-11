using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Effects;

public class EffectHelper
{
	internal static void l_u0013_(Matrix param_0, ref Matrix param_1, ref EffectParameter param_2)
	{
		if (param_2 == null || param_0 == param_1)
		{
			param_1 = param_0;
			return;
		}
		param_1 = param_0;
		param_2.SetValue(param_1);
	}

	internal static void lN(Matrix[] param_0, ref Matrix[] param_1, ref EffectParameter param_2)
	{
		param_1 = param_0;
		if (param_2 != null && param_0 != null)
		{
			Math.Min(param_0.Length, param_2.Elements.Count);
			param_2.SetValue(param_0);
		}
	}

	internal static void ll(Matrix param_0, ref Matrix param_1, ref Matrix param_2, ref EffectParameter param_3, ref EffectParameter param_4)
	{
		if (!(param_0 == param_1))
		{
			param_1 = param_0;
			if (param_3 != null)
			{
				param_3.SetValue(param_1);
			}
			if (param_4 != null)
			{
				param_2 = Matrix.Invert(param_1);
				param_4.SetValue(param_2);
			}
		}
	}

	internal static void lL(Vector4 param_0, ref Vector4 param_1, ref EffectParameter param_2)
	{
		if (param_2 != null && !(param_0 == param_1))
		{
			param_1 = param_0;
			param_2.SetValue(param_1);
		}
	}

	internal static void lw(Vector3 param_0, ref Vector3 param_1, ref EffectParameter param_2)
	{
		if (param_2 != null && !(param_0 == param_1))
		{
			param_1 = param_0;
			param_2.SetValue(param_1);
		}
	}

	internal static void lJ(int param_0, ref int param_1, ref EffectParameter param_2)
	{
		if (param_2 != null && param_0 != param_1)
		{
			param_1 = param_0;
			param_2.SetValue(param_1);
		}
	}

	internal static void lB(float param_0, ref float param_1, ref EffectParameter param_2)
	{
		if (param_2 != null && param_0 != param_1)
		{
			param_1 = param_0;
			param_2.SetValue(param_1);
		}
	}

	internal static void ls(Vector2 param_0, ref Vector2 param_1, ref EffectParameter param_2)
	{
		if (param_2 != null && !(param_0 == param_1))
		{
			param_1 = param_0;
			param_2.SetValue(param_1);
		}
	}

	internal static void lh(Texture2D param_0, ref Texture2D param_1, ref EffectParameter param_2)
	{
		if (param_2 != null && param_0 != param_1)
		{
			param_1 = param_0;
			param_2.SetValue(param_1);
		}
	}

	internal static void lh(Texture2D param_0, Texture2D param_1, ref Texture2D param_2, ref EffectParameter param_3)
	{
		if (param_0 == null)
		{
			param_0 = param_1;
		}
		if (param_3 != null && param_0 != param_2)
		{
			param_2 = param_0;
			param_3.SetValue(param_2);
		}
	}

	internal static void lL(List<EffectParameter> param_0, Vector4 param_1)
	{
		if (param_0 == null || param_0.Count < 1)
		{
			return;
		}
		for (int i = 0; i < param_0.Count; i++)
		{
			EffectParameter effectParameter = param_0[i];
			if (effectParameter.ParameterType == EffectParameterType.Int32 && effectParameter.RowCount == 1)
			{
				effectParameter.SetValue((int)param_1.X);
			}
			if (effectParameter.ParameterType == EffectParameterType.Single && effectParameter.RowCount <= 1)
			{
				if (effectParameter.ColumnCount == 1)
				{
					effectParameter.SetValue(param_1.X);
				}
				else if (effectParameter.ColumnCount == 2)
				{
					effectParameter.SetValue(new Vector2(param_1.X, param_1.Y));
				}
				else if (effectParameter.ColumnCount == 3)
				{
					effectParameter.SetValue(new Vector3(param_1.X, param_1.Y, param_1.Z));
				}
				else
				{
					effectParameter.SetValue(param_1);
				}
			}
		}
	}

	internal static void l_u0013_(List<EffectParameter> param_0, Matrix param_1)
	{
		if (param_0 != null && param_0.Count >= 1)
		{
			for (int i = 0; i < param_0.Count; i++)
			{
				param_0[i].SetValue(param_1);
			}
		}
	}

	internal static void l_u0002_(List<EffectParameter> param_0, Matrix param_1)
	{
		if (param_0 != null && param_0.Count >= 1)
		{
			l_u0013_(param_0, Matrix.Transpose(param_1));
		}
	}

	public static void SyncObjectAndShadowEffects(Effect objeffect, Effect shadoweffect)
	{
		EffectTypeCaster param_ = OptimizationSystem.EffectTypeCasters.Get(objeffect);
		EffectTypeCaster param_2 = OptimizationSystem.EffectTypeCasters.Get(shadoweffect);
		lv(param_, param_2);
	}

	internal static void lv(EffectTypeCaster param_0, EffectTypeCaster param_1)
	{
		if (param_1.RenderableEffect != null)
		{
			if (param_0.RenderableEffect != null)
			{
				param_1.RenderableEffect.DoubleSided = param_0.RenderableEffect.DoubleSided;
			}
			else
			{
				param_1.RenderableEffect.DoubleSided = false;
			}
		}
		if (param_1.TransparentEffect != null)
		{
			ITransparentEffect transparentEffect = param_1.TransparentEffect;
			if (param_0.TransparentEffect != null)
			{
				ITransparentEffect transparentEffect2 = param_0.TransparentEffect;
				if (transparentEffect2.TransparencyMode != TransparencyMode.None)
				{
					transparentEffect.SetTransparencyModeAndMap(transparentEffect2.TransparencyMode, transparentEffect2.TransparencyThreshold, transparentEffect2.TransparencyMap);
				}
				else
				{
					transparentEffect.SetTransparencyModeAndMap(TransparencyMode.None, transparentEffect.TransparencyThreshold, null);
				}
			}
			else
			{
				transparentEffect.SetTransparencyModeAndMap(TransparencyMode.None, transparentEffect.TransparencyThreshold, null);
			}
		}
		if (param_1.AddressableEffect != null)
		{
			IAddressableEffect addressableEffect = param_1.AddressableEffect;
			if (param_0.AddressableEffect != null)
			{
				IAddressableEffect addressableEffect2 = param_0.AddressableEffect;
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
		if (param_1.TerrainEffect != null)
		{
			ITerrainEffect terrainEffect = param_1.TerrainEffect;
			if (param_0.TerrainEffect != null)
			{
				ITerrainEffect terrainEffect2 = param_0.TerrainEffect;
				terrainEffect.HeightMapTexture = terrainEffect2.HeightMapTexture;
				terrainEffect.HeightScale = terrainEffect2.HeightScale;
				terrainEffect.MeshSegments = terrainEffect2.MeshSegments;
				terrainEffect.TileRepeatCount = terrainEffect2.TileRepeatCount;
				terrainEffect.Tiling = terrainEffect2.Tiling;
			}
			else
			{
				terrainEffect.HeightMapTexture = null;
			}
		}
	}
}
