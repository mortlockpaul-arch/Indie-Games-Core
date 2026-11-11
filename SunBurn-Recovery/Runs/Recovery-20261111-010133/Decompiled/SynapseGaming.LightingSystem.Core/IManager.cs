namespace SynapseGaming.LightingSystem.Core;

public interface IManager : IUnloadable
{
	IManagerServiceProvider OwnerSceneInterface { get; }

	void ApplyPreferences(ISystemPreferences preferences);

	void Clear();
}
