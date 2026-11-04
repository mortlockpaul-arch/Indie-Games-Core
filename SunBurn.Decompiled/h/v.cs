using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace h;

internal class v
{
	private static global::L.i<_0019> fNB = new global::L.i<_0019>();

	internal void _0003E(Matrix P_0, Matrix P_1, Matrix P_2, Matrix P_3, List<_0019> P_4, List<RenderableMesh> P_5, g P_6)
	{
		P_4.Clear();
		for (int k = 0; k < P_5.Count; k++)
		{
			RenderableMesh renderableMesh = P_5[k];
			if (renderableMesh != null)
			{
				renderableMesh.fNR = false;
			}
		}
		for (int l = 0; l < P_5.Count; l++)
		{
			RenderableMesh renderableMesh2 = P_5[l];
			if (renderableMesh2 == null || renderableMesh2.fNR)
			{
				continue;
			}
			global::L.d d2 = OptimizationSystem.fNg.g_0011(renderableMesh2.fN_0015);
			if (d2.fNa != null)
			{
				if ((P_6 & g.LightingEffect) == 0)
				{
					continue;
				}
			}
			else if (d2.fNo == null && (P_6 & g.MiscEffect) == 0)
			{
				continue;
			}
			bool flag = false;
			if (d2.fNB != null)
			{
				d2.fNB.SetViewAndProjection(P_0, P_1, P_2, P_3);
				flag = true;
			}
			else if (d2.fNj != null)
			{
				bool lightingEnabled = d2.fNj.LightingEnabled;
				if ((lightingEnabled && (P_6 & g.BasicEffect_Lighting) == 0) || (!lightingEnabled && (P_6 & g.BasicEffect_NonLighting) == 0))
				{
					continue;
				}
			}
			if (d2.fNo != null)
			{
				d2.fNo.View = P_0;
				d2.fNo.Projection = P_2;
				flag = true;
			}
			_0019 obj = fNB.New();
			obj._0013();
			obj.fNn = renderableMesh2.fN_0015;
			P_4.Add(obj);
			obj.Objects.Add(renderableMesh2);
			renderableMesh2.fNR = true;
			if (!flag)
			{
				continue;
			}
			for (int m = l + 1; m < P_5.Count; m++)
			{
				RenderableMesh renderableMesh3 = P_5[m];
				if (renderableMesh3 != null && !renderableMesh3.fNR && renderableMesh2.fN_0017 == renderableMesh3.fN_0017)
				{
					obj.Objects.Add(renderableMesh3);
					renderableMesh3.fNR = true;
				}
			}
		}
	}

	internal void _0003Y(List<_0019> P_0, List<RenderableMesh> P_1, bool P_2, bool P_3)
	{
		P_0.Clear();
		for (int k = 0; k < P_1.Count; k++)
		{
			RenderableMesh renderableMesh = P_1[k];
			if (renderableMesh != null)
			{
				renderableMesh.fNR = false;
			}
		}
		for (int l = 0; l < P_1.Count; l++)
		{
			RenderableMesh renderableMesh2 = P_1[l];
			if (renderableMesh2 == null || renderableMesh2.fNR || (P_2 && !renderableMesh2.fNG))
			{
				continue;
			}
			_0019 obj = fNB.New();
			obj._0013();
			obj.fNn = renderableMesh2.fN_0015;
			obj.fNa = renderableMesh2.fNW;
			obj.fNg = renderableMesh2.fN9;
			obj.fNf = renderableMesh2.fN_0013;
			obj.fN_0003 = renderableMesh2.fNq;
			obj.Objects.Add(renderableMesh2);
			P_0.Add(obj);
			renderableMesh2.fNR = true;
			for (int m = l + 1; m < P_1.Count; m++)
			{
				RenderableMesh renderableMesh3 = P_1[m];
				if (renderableMesh3 != null && !renderableMesh3.fNR && (!P_2 || renderableMesh3.fNG) && (renderableMesh2.fN_0017 == renderableMesh3.fN_0017 || (!renderableMesh2.fN9 && !renderableMesh3.fN9 && !renderableMesh2.fNq && !renderableMesh3.fNq && renderableMesh2.fNW == renderableMesh3.fNW && renderableMesh2.fN_0013 == renderableMesh3.fN_0013 && !renderableMesh2.fN_000F && !renderableMesh3.fN_000F)))
				{
					obj.Objects.Add(renderableMesh3);
					renderableMesh3.fNR = true;
				}
			}
		}
	}

	internal void _0013()
	{
		fNB.FreeAllTracked();
	}
}
