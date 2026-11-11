namespace SynapseGaming.LightingSystem.Core;

public interface IPlugin
{
	void Initialize(IManagerServiceProvider sceneinterface);

	void Unload();
}
