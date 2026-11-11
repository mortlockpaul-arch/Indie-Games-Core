using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering.Forward;

public class SunBurnGameComponent : DrawableGameComponent
{
	private Matrix l8a = Matrix.Identity;

	private Matrix l8_u0013_ = Matrix.Identity;

	private Matrix l8N = Matrix.Identity;

	private SceneState l8l = new SceneState();

	private SceneEnvironment l8L = new SceneEnvironment();

	private SystemPreferences l8w = new SystemPreferences();

	private SunBurnCoreSystem l8J;

	private SceneInterface l8B;

	private FrameBuffers l8s;

	public SceneInterface SceneInterface => l8B;

	public FrameBuffers FrameBuffers => l8s;

	public Matrix View
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
		}
	}

	public Matrix Projection
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
		}
	}

	public SceneEnvironment Environment
	{
		get
		{
			return l8L;
		}
		set
		{
			l8L = value;
		}
	}

	public SunBurnGameComponent(Game game, ContentManager managerwithactivationfile, RenderingSystemType renderingsystemtype)
		: base(game)
	{
		l8J = new SunBurnCoreSystem(Game.Services, managerwithactivationfile);
		l8B = new SceneInterface();
		l8B.CreateDefaultManagers(renderingsystemtype, includeautoloadedplugins: true);
		l8s = new FrameBuffers(DetailPreference.Medium, DetailPreference.Medium);
		l8B.ResourceManager.AssignOwnership(l8s);
		l8B.ApplyPreferences(l8w);
	}

	public void ApplyPreferences(ISystemPreferences preferences)
	{
		l8B.ApplyPreferences(preferences);
	}

	public void Clear()
	{
		l8B.Clear();
	}

	public void Unload()
	{
		l8B.Unload();
		l8J.Unload();
	}

	protected override void UnloadContent()
	{
		Unload();
		base.UnloadContent();
	}

	protected override void Dispose(bool disposing)
	{
		Unload();
		base.Dispose(disposing);
	}

	public override void Update(GameTime gameTime)
	{
		l8B.Update(gameTime);
		base.Update(gameTime);
	}

	public override void Draw(GameTime gameTime)
	{
		if (SplashScreenGameComponent.DisplayComplete)
		{
			l8l.BeginFrameRendering(l8_u0013_, l8N, gameTime, l8L, l8s, renderingtoscreen: true);
			l8B.BeginFrameRendering(l8l);
			l8B.RenderManager.Render();
			l8B.EndFrameRendering();
			l8l.EndFrameRendering();
		}
		base.Draw(gameTime);
	}
}
