using System;

namespace SynapseGaming.LightingSystem.Core;

public interface IManagerService : IManager, IUnloadable
{
	Type ManagerType { get; }

	int ManagerProcessOrder { get; set; }
}
