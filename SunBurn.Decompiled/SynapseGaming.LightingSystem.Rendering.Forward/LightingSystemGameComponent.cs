using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering.Forward;

/// <summary>
/// Provides a self-contained SunBurn rendering environment. For quickly adding
/// SunBurn to a project with minimal changes and nearly pure XNA code interaction.
/// </summary>
public class LightingSystemGameComponent : DrawableGameComponent
{
	private Matrix fNB = Matrix.Identity;

	private Matrix fNa = Matrix.Identity;

	private Matrix fNg = Matrix.Identity;

	private GraphicsDeviceManager fNf;

	private SceneState fN_0003 = new SceneState();

	private SceneEnvironment fNn = new SceneEnvironment();

	private LightingSystemPreferences fN_0019 = new LightingSystemPreferences();

	private LightingSystemManager fNL;

	private SceneInterface fNv;

	private FrameBuffers fN_0012;

	/// <summary>
	/// Rendering environment's SceneInterface. Use to add scene objects and lights for rendering.
	/// </summary>
	public SceneInterface SceneInterface => fNv;

	/// <summary>
	/// Rendering environment's shared buffers.
	/// </summary>
	public FrameBuffers FrameBuffers => fN_0012;

	/// <summary>
	/// The scene's current view matrix.
	/// </summary>
	public Matrix View
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
		}
	}

	/// <summary>
	/// The scene's current projection matrix.
	/// </summary>
	public Matrix Projection
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
		}
	}

	/// <summary>
	/// The scene's current environment such as fog, viewing distance, and HDR information.
	/// </summary>
	public SceneEnvironment Environment
	{
		get
		{
			return fNn;
		}
		set
		{
			fNn = value;
		}
	}

	/// <summary>
	/// Creates a new LightingSystemGameComponent instance.
	/// </summary>
	/// <param name="game"></param>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="managerwithactivationfile">Content manager that contains the SunBurn activation file.</param>
	/// <param name="usedeferredrendering">Determines if the component should use deferred or forward rendering.</param>
	public LightingSystemGameComponent(Game game, GraphicsDeviceManager graphicsdevicemanager, ContentManager managerwithactivationfile, bool usedeferredrendering)
		: base(game)
	{
		fNf = graphicsdevicemanager;
		fNL = new LightingSystemManager(Game.Services, managerwithactivationfile);
		fNv = new SceneInterface(graphicsdevicemanager);
		fNv.CreateDefaultManagers(usedeferredrendering);
		fN_0012 = new FrameBuffers(graphicsdevicemanager, DetailPreference.Medium, DetailPreference.Medium);
		fNv.ResourceManager.AssignOwnership(fN_0012);
		fNv.ApplyPreferences(fN_0019);
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public void ApplyPreferences(ILightingSystemPreferences preferences)
	{
		fNv.ApplyPreferences(preferences);
	}

	/// <summary>
	/// Removes resources managed by this object. Commonly used while clearing the scene.
	/// </summary>
	public void Clear()
	{
		fNv.Clear();
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public void Unload()
	{
		fNv.Unload();
		fNL.Unload();
	}

	/// <summary>
	/// Called when graphics resources need to be unloaded. Override this method
	/// to unload any component-specific graphics resources.
	/// </summary>
	protected override void UnloadContent()
	{
		Unload();
		base.UnloadContent();
	}

	/// <summary>
	/// Releases the unmanaged resources used by the DrawableGameComponent and
	/// optionally releases the managed resources.
	/// </summary>
	/// <param name="disposing"></param>
	protected override void Dispose(bool disposing)
	{
		Unload();
		base.Dispose(disposing);
	}

	/// <summary>
	/// Called when the GameComponent needs to be updated. Override this
	/// method with component-specific update code.
	/// </summary>
	/// <param name="gameTime"></param>
	public override void Update(GameTime gameTime)
	{
		fNv.Update(gameTime);
		base.Update(gameTime);
	}

	/// <summary>
	/// Called when the DrawableGameComponent needs to be drawn. Override
	/// this method with component-specific drawing code.
	/// </summary>
	/// <param name="gameTime"></param>
	public override void Draw(GameTime gameTime)
	{
		if (SplashScreenGameComponent.DisplayComplete)
		{
			fN_0003.BeginFrameRendering(fNa, fNg, gameTime, fNn, fN_0012, renderingtoscreen: true);
			fNv.BeginFrameRendering(fN_0003);
			fNv.RenderManager.Render();
			fNv.EndFrameRendering();
			fN_0003.EndFrameRendering();
		}
		base.Draw(gameTime);
	}
}
