using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Collision;

/// <summary>
/// Interface that provides access to the scene's collision manager. The collision manager
/// provides automatic collision and physics for scene objects.
/// </summary>
public interface ICollisionManager : IManagerService, IRenderableManager, IUpdatableManager, IManager, IUnloadable
{
}
