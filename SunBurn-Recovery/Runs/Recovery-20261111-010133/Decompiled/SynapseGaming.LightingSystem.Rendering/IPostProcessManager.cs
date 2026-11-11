using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public interface IPostProcessManager : IRenderableManager, IManagerService, IManager, IUnloadable
{
	void AddPostProcessor(IPostProcessor postprocessor);

	void RemovePostProcessor(IPostProcessor postprocessor);
}
