using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

public interface IWorldRenderableManager : IRenderableManager, IManager, IUnloadable
{
	BoundingBox WorldBoundingBox { get; }

	bool AutoOptimize { get; set; }

	bool RequiresOptimization { get; }

	void Resize(BoundingBox worldboundingbox, int worldtreemaxdepth);

	void Optimize();

	void Optimize(int worldtreemaxdepth);
}
