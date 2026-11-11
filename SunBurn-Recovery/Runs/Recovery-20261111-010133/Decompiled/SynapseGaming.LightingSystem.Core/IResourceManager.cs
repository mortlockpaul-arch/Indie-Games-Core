using System;

namespace SynapseGaming.LightingSystem.Core;

public interface IResourceManager : IManagerService, IUpdatableManager, IManager, IUnloadable
{
	void AssignOwnership(IDisposable resource);

	void AssignOwnership(IUnloadable resource);

	void LinkOwnership(IDisposable resource, object linkedobject);
}
