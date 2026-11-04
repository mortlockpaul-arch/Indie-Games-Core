using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;

namespace L;

internal class c : global::L.j<IShadowSource>
{
	internal IPointSource fNB;

	internal ISpotSource fNa;

	internal IDirectionalSource fNg;

	public void Set(IShadowSource light)
	{
		fNB = light as IPointSource;
		fNa = light as ISpotSource;
		fNg = light as IDirectionalSource;
	}
}
