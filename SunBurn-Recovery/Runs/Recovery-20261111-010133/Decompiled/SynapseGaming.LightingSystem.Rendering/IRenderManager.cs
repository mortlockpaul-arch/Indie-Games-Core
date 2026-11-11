using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public interface IRenderManager : IRenderableManager, IManagerService, IManager, IUnloadable
{
	void Render();
}
