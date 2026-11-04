using System.Collections.Generic;
using SynapseGaming.LightingSystem.Rendering;

namespace h;

internal class n : IComparer<RenderableMesh>
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
		return a.fNh - b.fNh;
	}
}
