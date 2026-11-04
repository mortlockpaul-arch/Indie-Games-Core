using System;
using System.Collections.Generic;
using _0013;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using h;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Helper renderer that displays the bounding boxes of all
/// rendered scene objects and lights.
///
/// Can help tune performance and work out bugs by seeing how
/// objects and lights within the scene overlap and interact
/// with each other.
/// </summary>
public class DebugRenderManager : IManagerService, IRenderableManager, IManager, IUnloadable
{
	private int fNB = 100;

	private ISceneState fNa;

	private IGraphicsDeviceService fNg;

	private IManagerServiceProvider fNf;

	private BasicEffect fN_0003;

	private BoundingBoxRenderHelper fNn;

	private List<SceneEntity> fN_0019 = new List<SceneEntity>();

	private List<BaseLight> fNL = new List<BaseLight>();

	/// <summary>
	/// Gets the manager specific Type used as a unique key for storing and
	/// requesting the manager from the IManagerServiceProvider.
	/// </summary>
	public Type ManagerType => typeof(DebugRenderManager);

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
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	public IGraphicsDeviceService GraphicsDeviceManager => fNg;

	/// <summary>
	/// Creates a new DebugRenderManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="sceneinterface">Service provider used to access all other manager services in this scene.</param>
	public DebugRenderManager(IGraphicsDeviceService graphicsdevicemanager, IManagerServiceProvider sceneinterface)
	{
		fNg = graphicsdevicemanager;
		fNf = sceneinterface;
		fNn = new BoundingBoxRenderHelper(graphicsdevicemanager);
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public void ApplyPreferences(ILightingSystemPreferences preferences)
	{
	}

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	public void BeginFrameRendering(ISceneState scenestate)
	{
		fNa = scenestate;
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public void EndFrameRendering()
	{
		if (LightingSystemEditor.EditorAttachedStatic)
		{
			return;
		}
		GraphicsDevice graphicsDevice = fNg.GraphicsDevice;
		IObjectManager objectManager = (IObjectManager)fNf.GetManager(SceneInterface.ObjectManagerType, required: false);
		ILightManager lightManager = (ILightManager)fNf.GetManager(SceneInterface.LightManagerType, required: false);
		h._0012._0003T(graphicsDevice);
		if (fN_0003 == null)
		{
			fN_0003 = new BasicEffect(graphicsDevice);
			fN_0003.TextureEnabled = false;
			fN_0003.SpecularColor = Vector3.Zero;
			fN_0003.VertexColorEnabled = true;
		}
		fN_0003.World = Matrix.Identity;
		fN_0003.View = fNa.View;
		fN_0003.Projection = fNa.Projection;
		if (objectManager != null)
		{
			fN_0019.Clear();
			objectManager.Find(fN_0019, fNa.ViewFrustum, ObjectFilter.All);
			foreach (SceneEntity item in fN_0019)
			{
				if (item != null && !(item is SceneObject { Visible: false }))
				{
					fNn.Submit(item.WorldBoundingBox, Color.LimeGreen);
				}
			}
		}
		if (lightManager != null)
		{
			fNL.Clear();
			lightManager.Find(fNL, fNa.ViewFrustum, ObjectFilter.EnabledDynamicAndStatic);
			foreach (BaseLight item2 in fNL)
			{
				if (item2 != null && item2.Enabled && item2 is IPointSource)
				{
					fNn.Submit(item2.WorldBoundingBox, Color.Yellow);
				}
			}
		}
		fNn.Render(fN_0003);
		fNn.Clear();
	}

	/// <summary>
	/// Removes resources managed by this object. Commonly used while clearing the scene.
	/// </summary>
	public void Clear()
	{
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public void Unload()
	{
		Clear();
		_0013.B.nu(ref fN_0003);
	}
}
