using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Rendering;

namespace x;

internal class B
{
	internal static bool L_u0017_(List<RenderableMesh> param_0, BoundingBox param_1)
	{
		bool flag = false;
		foreach (RenderableMesh item in param_0)
		{
			ISceneObject l8_u0013_ = item.l8_u0013_;
			item.l85 = param_1.Contains(l8_u0013_.WorldBoundingBox) != ContainmentType.Disjoint;
			flag |= item.l85;
		}
		return flag;
	}

	internal static bool L2(List<RenderableMesh> param_0, BoundingFrustum param_1)
	{
		bool flag = false;
		foreach (RenderableMesh item in param_0)
		{
			ISceneObject l8_u0013_ = item.l8_u0013_;
			item.l85 = l8_u0013_.CastShadows && param_1.Contains(l8_u0013_.WorldBoundingBox) != ContainmentType.Disjoint;
			flag |= item.l85;
		}
		return flag;
	}
}
