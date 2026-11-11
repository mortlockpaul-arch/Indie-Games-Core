using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public interface IAvatarManager : IRenderableManager, IManagerService, IShadowRenderer, IUpdatableManager, IManager, IUnloadable, IQuery<Avatar>, ISubmit<Avatar>
{
	float AmbientBlend { get; set; }

	float LightingIntensity { get; set; }

	new void Clear();
}
