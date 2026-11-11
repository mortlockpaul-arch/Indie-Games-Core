using System.Collections.Generic;
using B;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace x;

internal class s
{
	private static global::B.h<J> l8a = new global::B.h<J>();

	internal void L7(Matrix param_0, Matrix param_1, Matrix param_2, Matrix param_3, List<J> param_4, List<RenderableMesh> param_5, N param_6)
	{
		param_4.Clear();
		for (int i = 0; i < param_5.Count; i++)
		{
			RenderableMesh renderableMesh = param_5[i];
			if (renderableMesh != null)
			{
				renderableMesh.l8_u0014_ = false;
			}
		}
		for (int j = 0; j < param_5.Count; j++)
		{
			RenderableMesh renderableMesh2 = param_5[j];
			if (renderableMesh2 == null || renderableMesh2.l8_u0014_)
			{
				continue;
			}
			EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(renderableMesh2.l8_u0002_);
			if (effectTypeCaster.LightingEffect != null)
			{
				if ((param_6 & N.LightingEffect) == 0)
				{
					continue;
				}
			}
			else if (effectTypeCaster.EffectMatrices == null && (param_6 & N.MiscEffect) == 0)
			{
				continue;
			}
			bool flag = false;
			if (effectTypeCaster.RenderableEffect != null)
			{
				effectTypeCaster.RenderableEffect.SetViewAndProjection(param_0, param_1, param_2, param_3);
				flag = true;
			}
			else if (effectTypeCaster.EffectLights != null)
			{
				bool lightingEnabled = effectTypeCaster.EffectLights.LightingEnabled;
				if ((lightingEnabled && (param_6 & N.BasicEffect_Lighting) == 0) || (!lightingEnabled && (param_6 & N.BasicEffect_NonLighting) == 0))
				{
					continue;
				}
			}
			if (effectTypeCaster.EffectMatrices != null)
			{
				effectTypeCaster.EffectMatrices.View = param_0;
				effectTypeCaster.EffectMatrices.Projection = param_2;
				flag = true;
			}
			J j2 = l8a.New();
			j2._id_3();
			j2.l8w = renderableMesh2.l8_u0002_;
			param_4.Add(j2);
			j2.Objects.Add(renderableMesh2);
			renderableMesh2.l8_u0014_ = true;
			if (!flag)
			{
				continue;
			}
			for (int k = j + 1; k < param_5.Count; k++)
			{
				RenderableMesh renderableMesh3 = param_5[k];
				if (renderableMesh3 != null && !renderableMesh3.l8_u0014_ && renderableMesh2.l8W == renderableMesh3.l8W)
				{
					j2.Objects.Add(renderableMesh3);
					renderableMesh3.l8_u0014_ = true;
				}
			}
		}
	}

	internal void LQ(List<J> param_0, List<RenderableMesh> param_1, bool param_2, bool param_3)
	{
		param_0.Clear();
		for (int i = 0; i < param_1.Count; i++)
		{
			RenderableMesh renderableMesh = param_1[i];
			if (renderableMesh != null)
			{
				renderableMesh.l8_u0014_ = false;
			}
		}
		for (int j = 0; j < param_1.Count; j++)
		{
			RenderableMesh renderableMesh2 = param_1[j];
			if (renderableMesh2 == null || renderableMesh2.l8_u0014_ || (param_2 && !renderableMesh2.l85))
			{
				continue;
			}
			J j2 = l8a.New();
			j2._id_3();
			j2.l8w = renderableMesh2.l8_u0002_;
			j2.l8_u0013_ = renderableMesh2.l8E;
			j2.l8N = renderableMesh2.l8f;
			j2.l8l = renderableMesh2.l8_u0016_;
			j2.l8L = renderableMesh2.l86;
			j2.Objects.Add(renderableMesh2);
			param_0.Add(j2);
			renderableMesh2.l8_u0014_ = true;
			for (int k = j + 1; k < param_1.Count; k++)
			{
				RenderableMesh renderableMesh3 = param_1[k];
				if (renderableMesh3 != null && !renderableMesh3.l8_u0014_ && (!param_2 || renderableMesh3.l85) && (renderableMesh2.l8W == renderableMesh3.l8W || (!renderableMesh2.l8f && !renderableMesh3.l8f && !renderableMesh2.l86 && !renderableMesh3.l86 && renderableMesh2.l8E == renderableMesh3.l8E && renderableMesh2.l8_u0016_ == renderableMesh3.l8_u0016_ && !renderableMesh2.l8x && !renderableMesh3.l8x)))
				{
					j2.Objects.Add(renderableMesh3);
					renderableMesh3.l8_u0014_ = true;
				}
			}
		}
	}

	internal void _id_3()
	{
		l8a.FreeAllTracked();
	}
}
