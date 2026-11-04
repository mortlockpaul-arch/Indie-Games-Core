using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Interface used by objects that manage rendering and scene resources.
/// </summary>
public interface IRenderableManager : IManager, IUnloadable
{
	/// <summary>
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	IGraphicsDeviceService GraphicsDeviceManager { get; }

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	void BeginFrameRendering(ISceneState scenestate);

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	void EndFrameRendering();
}
