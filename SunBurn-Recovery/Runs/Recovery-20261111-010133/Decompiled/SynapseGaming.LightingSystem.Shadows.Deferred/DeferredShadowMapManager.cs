using B;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Shadows.Deferred;

public class DeferredShadowMapManager : BaseShadowMapManager
{
	private B._u0002_<DeferredShadowCubeMap> l8a = new B._u0002_<DeferredShadowCubeMap>();

	private B._u0002_<DeferredShadowDirectionalMap> l8_u0013_ = new B._u0002_<DeferredShadowDirectionalMap>();

	public DeferredShadowMapManager(IManagerServiceProvider sceneinterface, int pagesize, int maxmemoryusage, bool preferhalffloat)
		: base(sceneinterface, pagesize, maxmemoryusage, preferhalffloat)
	{
	}

	public DeferredShadowMapManager(IManagerServiceProvider sceneinterface, ShadowMapCache shadowmapcache)
		: base(sceneinterface, shadowmapcache)
	{
	}

	public DeferredShadowMapManager(IManagerServiceProvider sceneinterface)
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
