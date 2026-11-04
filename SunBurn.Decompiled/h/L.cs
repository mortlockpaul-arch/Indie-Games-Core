using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Rendering;

namespace h;

internal class L
{
	internal static bool _0003e(List<RenderableMesh> P_0, BoundingBox P_1)
	{
		bool flag = false;
		foreach (RenderableMesh item in P_0)
		{
			ISceneObject fNa = item.fNa;
			item.fNG = P_1.Contains(fNa.WorldBoundingBox) != ContainmentType.Disjoint;
			flag |= item.fNG;
		}
		return flag;
	}

	internal static bool _00035(List<RenderableMesh> P_0, BoundingFrustum P_1)
	{
		bool flag = false;
		foreach (RenderableMesh item in P_0)
		{
			ISceneObject fNa = item.fNa;
			item.fNG = fNa.CastShadows && P_1.Contains(fNa.WorldBoundingBox) != ContainmentType.Disjoint;
			flag |= item.fNG;
		}
		return flag;
	}
}
