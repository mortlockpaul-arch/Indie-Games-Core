using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Rendering;

namespace x;

internal class J
{
	internal bool l8a = true;

	internal bool l8_u0013_;

	internal bool l8N;

	internal bool l8l;

	internal bool l8L;

	internal Effect l8w;

	private List<RenderableMesh> l8J = new List<RenderableMesh>(32);

	internal List<RenderableMesh> Objects => l8J;

	internal void _id_3()
	{
		l8a = true;
		l8_u0013_ = false;
		l8N = false;
		l8l = false;
		l8L = false;
		l8w = null;
		l8J.Clear();
	}
}
