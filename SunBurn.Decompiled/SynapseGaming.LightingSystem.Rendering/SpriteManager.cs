using System;
using L;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using h;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Acts as a resource manager for arrays and buffers used during sprite creation.
/// </summary>
public class SpriteManager : IManagerService, IManager, IUnloadable
{
	private int fNB = 100;

	private IGraphicsDeviceService fNa;

	private L.i<RenderableMesh> fNg = new L.i<RenderableMesh>();

	private L._0005<h.i> fNf = new L._0005<h.i>();

	/// <summary>
	/// Gets the manager specific Type used as a unique key for storing and
	/// requesting the manager from the IManagerServiceProvider.
	/// </summary>
	public Type ManagerType => typeof(SpriteManager);

	/// <summary>
	/// Sets the order this manager is processed relative to other managers
	/// in the IManagerServiceProvider. Managers with lower processing order
	/// values are processed first.
	///
	/// In the case of BeginFrameRendering and EndFrameRendering, BeginFrameRendering
	/// is processed in the normal order (lowest order value to highest), however
	/// EndFrameRendering is processed in reverse order (highest to lowest) to ensure
	/// the first manager begun is the last one ended (FILO).
	/// </summary>
	public int ManagerProcessOrder
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
	/// Creates a new SpriteManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	public SpriteManager(IGraphicsDeviceService graphicsdevicemanager)
	{
		fNa = graphicsdevicemanager;
	}

	/// <summary>
	/// Creates a new SpriteContainer instance for storing and rendering 2D sprites.
	/// </summary>
	/// <returns></returns>
	public SpriteContainer CreateSpriteContainer()
	{
		SpriteContainer spriteContainer = new SpriteContainer(fNa.GraphicsDevice, fNg, fNf);
		spriteContainer.UpdateType = UpdateType.Automatic;
		return spriteContainer;
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public void ApplyPreferences(ILightingSystemPreferences preferences)
	{
	}

	/// <summary>
	/// Removes all objects from the container. Commonly used while clearing the scene.
	/// </summary>
	public void Clear()
	{
		fNg.FreeAllTracked();
		fNf.FreeAllTracked();
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public void Unload()
	{
		fNg.Clear();
		fNf.Unload();
	}
}
