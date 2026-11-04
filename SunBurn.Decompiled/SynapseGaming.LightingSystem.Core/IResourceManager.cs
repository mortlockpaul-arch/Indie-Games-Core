using System;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Interface that provides access to the scene's resource manager. The resource manager
/// tracks disposable and unloadable resources, freeing them when the scene is unloaded.
/// </summary>
public interface IResourceManager : IManagerService, IManager, IUnloadable
{
	/// <summary>
	/// Assigns ownership of the resource to the resource manager, this means the manager
	/// will handle disposing and removing (IDisposable), or unloading (IUnloadable) the
	/// resource when the scene is unloaded (when [manager].Unload() is called).
	/// </summary>
	/// <param name="resource"></param>
	void AssignOwnership(IDisposable resource);

	/// <summary>
	/// Assigns ownership of the resource to the resource manager, this means the manager
	/// will handle disposing and removing (IDisposable), or unloading (IUnloadable) the
	/// resource when the scene is unloaded (when [manager].Unload() is called).
	/// </summary>
	/// <param name="resource"></param>
	void AssignOwnership(IUnloadable resource);
}
