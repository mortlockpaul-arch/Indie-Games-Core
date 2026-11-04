using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;

namespace L;

internal class a<T> : global::L.B<T> where T : IWorldBoundingBoxObject
{
	internal Dictionary<T, global::L.B<T>> fNB = new Dictionary<T, global::L.B<T>>(128);

	internal a(BoundingBox P_0, int P_1)
	{
		Init(ref P_0, P_1, this);
	}

	public a()
	{
	}

	internal void B(ref BoundingBox P_0, int P_1)
	{
		Init(ref P_0, P_1, this);
	}

	internal override void _0013()
	{
		fNB.Clear();
		base._0013();
	}
}
