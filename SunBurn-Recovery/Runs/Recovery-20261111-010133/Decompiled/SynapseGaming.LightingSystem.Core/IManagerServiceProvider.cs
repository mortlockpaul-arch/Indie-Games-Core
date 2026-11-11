using System;
using System.Collections.Generic;

namespace SynapseGaming.LightingSystem.Core;

public interface IManagerServiceProvider
{
	void AddManager(IManagerService manager);

	void RemoveManager(IManagerService manager);

	IManagerService GetManager(Type managertype, bool required);

	T GetManager<T>(bool required) where T : class;

	void GetManagers(List<IManagerService> managers);

	void ResortServices();
}
