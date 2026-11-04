using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;

namespace L;

internal class o : global::L.j<BaseLight>
{
	internal IPointSource fNB;

	internal ISpotSource fNa;

	internal IDirectionalSource fNg;

	internal IAmbientSource fNf;

	internal IShadowSource fN_0003;

	public void Set(BaseLight light)
	{
		fNB = light as IPointSource;
		fNa = light as ISpotSource;
		fNg = light as IDirectionalSource;
		fNf = light as IAmbientSource;
		fN_0003 = light as IShadowSource;
	}
}
