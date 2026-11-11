using B;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Shadows.Forward;

public class ShadowMapManager : BaseShadowMapManager
{
	private B._u0002_<ShadowCubeMap> l8a = new B._u0002_<ShadowCubeMap>();

	private B._u0002_<ShadowDirectionalMap> l8_u0013_ = new B._u0002_<ShadowDirectionalMap>();

	public ShadowMapManager(IManagerServiceProvider sceneinterface, int pagesize, int maxmemoryusage, bool preferhalffloat)
		: base(sceneinterface, pagesize, maxmemoryusage, preferhalffloat)
	{
	}

	public ShadowMapManager(IManagerServiceProvider sceneinterface, ShadowMapCache shadowmapcache)
		: base(sceneinterface, shadowmapcache)
	{
	}

	public ShadowMapManager(IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
	}

	protected override IShadowMap CreateDirectionalShadowMap(IShadowSource shadowsource)
	{
		return l8_u0013_.New();
	}

	protected override IShadowMap CreatePointShadowMap(IShadowSource shadowsource)
	{
		return l8a.New();
	}

	protected override IShadowMap CreateSpotShadowMap(IShadowSource shadowsource)
	{
		return l8a.New();
	}

	public override void EndFrameRendering()
	{
		base.EndFrameRendering();
		l8a.FreeAllTracked();
		l8_u0013_.FreeAllTracked();
	}

	public override void Unload()
	{
		base.Unload();
		l8a.Unload();
		l8_u0013_.Unload();
	}
}
