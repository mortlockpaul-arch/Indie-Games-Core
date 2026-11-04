using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Interface that provides access to the scene's post processing manager. The post processing manager
/// provides methods for rendering full-screen post processing effects.
/// </summary>
public interface IPostProcessManager : IRenderableManager, IManagerService, IManager, IUnloadable
{
	/// <summary>
	/// Adds a new post processor to the processing chain. The last processor added
	/// to the chain is the first to apply its visual effects.
	/// </summary>
	/// <param name="postprocessor"></param>
	void AddPostProcessor(IPostProcessor postprocessor);
}
