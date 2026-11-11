using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Core;

public class ShadowSourceTypeCaster : ITypeCaster<IShadowSource>
{
	public IPointSource PointSource;

	public ISpotSource SpotSource;

	public IDirectionalSource DirectionalSource;

	public void Set(IShadowSource light)
	{
		PointSource = light as IPointSource;
		SpotSource = light as ISpotSource;
		DirectionalSource = light as IDirectionalSource;
	}
}
