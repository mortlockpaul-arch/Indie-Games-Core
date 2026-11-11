using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public abstract class BaseOverlayPostProcessor : IPostProcessor
{
	private IGraphicsDeviceService l8a;

	private ISceneState l8_u0013_;

	protected IGraphicsDeviceService GraphicsDeviceManager => l8a;

	protected ISceneState SceneState => l8_u0013_;

	public BaseOverlayPostProcessor()
	{
		l8a = SunBurnCoreSystem.Instance.GraphicsDeviceManager;
	}

	public abstract void ApplyPreferences(ISystemPreferences preferences);

	public bool BeginFrameRendering(ISceneState scenestate, int rendertargetindex)
	{
		l8_u0013_ = scenestate;
		return false;
	}

	public virtual void EndFrameRendering()
	{
	}

	public virtual void Unload()
	{
	}
}
