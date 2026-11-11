using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace x;

internal class _u0002_
{
	protected class w8a
	{
		public SystemStatistic BatchVertexBufferChanges = SystemConsole.GetStatistic("Renderer_BatchVertexBufferChanges", SystemStatisticCategory.Rendering);
	}

	protected w8a Statistics = new w8a();

	private RenderableMesh l8a;

	internal void L_u0012_()
	{
		l8a = null;
	}

	internal void _u0004_(GraphicsDevice param_0, RenderableMesh param_1)
	{
		if (l8a == null || l8a.Index.BufferIndex.l8_u0013_ != param_1.Index.BufferIndex.l8_u0013_ || l8a.Index.l8l != param_1.Index.l8l)
		{
			param_0.SetVertexBuffer(param_1.Index.BufferIndex.l8_u0013_, param_1.Index.l8l);
			Statistics.BatchVertexBufferChanges.AccumulationValue++;
		}
		if (l8a == null || l8a.Index.BufferIndex.l8a != param_1.Index.BufferIndex.l8a)
		{
			param_0.Indices = param_1.Index.BufferIndex.l8a;
		}
		l8a = param_1;
	}
}
