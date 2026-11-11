using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Core;

public class LightTypeCaster : ITypeCaster<BaseLight>
{
	public IPointSource PointSource;

	public ISpotSource SpotSource;

	public IDirectionalSource DirectionalSource;

	public IAmbientSource AmbientSource;

	public IShadowSource ShadowSource;

	public void Set(BaseLight light)
	{
		PointSource = light as IPointSource;
		SpotSource = light as ISpotSource;
		DirectionalSource = light as IDirectionalSource;
		AmbientSource = light as IAmbientSource;
		ShadowSource = light as IShadowSource;
	}
}
