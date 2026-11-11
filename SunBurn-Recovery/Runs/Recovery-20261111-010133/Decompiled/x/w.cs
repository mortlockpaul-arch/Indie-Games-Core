using System.Collections.Generic;
using SynapseGaming.LightingSystem.Rendering;

namespace x;

internal class w : IComparer<RenderableMesh>
{
	public int Compare(RenderableMesh a, RenderableMesh b)
	{
		if (a == b)
		{
			return 0;
		}
		if (a == null)
		{
			return -1;
		}
		if (b == null)
		{
			return 1;
		}
		return a.l8T - b.l8T;
	}
}
