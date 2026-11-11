using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

public interface IUpdatableManager : IManager, IUnloadable
{
	void Update(GameTime gameTime);
}
