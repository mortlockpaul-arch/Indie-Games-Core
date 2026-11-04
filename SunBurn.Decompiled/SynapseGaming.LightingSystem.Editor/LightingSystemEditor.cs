using System;
using _0015;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SynapseGaming.LightingSystem.Core;
using v;

namespace SynapseGaming.LightingSystem.Editor;

/// <summary>
/// Adds editor support to SunBurn projects.
/// </summary>
public class LightingSystemEditor : IManagerService, IRenderableManager, IUpdatableManager, IManager, IUnloadable
{
	internal delegate void nNB(IDisposable resource);

	internal delegate void nNa(object obj, v.g updatetype);

	/// <summary>
	/// Used to remap effects that are replaced in editor.
	/// </summary>
	/// <param name="currenteffect">The effect to replace.</param>
	/// <param name="neweffect">The new effect.</param>
	public delegate void EffectReplaceDelegate(Effect currenteffect, Effect neweffect);

	/// <summary>
	/// Used to reload all scene assets when requested by the editor.
	/// </summary>
	public delegate void ReloadAssetsDelegate();

	private IManagerServiceProvider fNB;

	private IGraphicsDeviceService fNa;

	private int fNg = 25;

	private Keys fNf;

	private _0015.a fN_0003;

	private _0015.B fNn;

	private static nNB fN_0019;

	private static nNB fNL;

	private static nNa fNv;

	/// <summary>
	/// Used to remap effects that are replaced in editor.
	/// </summary>
	public static EffectReplaceDelegate ReplaceEffect;

	/// <summary>
	/// Used to reload all scene assets when requested by the editor.
	/// </summary>
	public static ReloadAssetsDelegate ReloadAssets;

	internal static bool EditorAttachedStatic => false;

	/// <summary>
	/// Gets the manager specific Type used as a unique key for storing and
	/// requesting the manager from the IManagerServiceProvider.
	/// </summary>
	public Type ManagerType => SceneInterface.EditorType;

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
			return fNg;
		}
		set
		{
			fNg = value;
		}
	}

	/// <summary>
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	public IGraphicsDeviceService GraphicsDeviceManager => fNa;

	/// <summary>
	/// The assigned key that, when pressed, will be used to launch the in-game editor.
	/// </summary>
	public Keys LaunchKey
	{
		get
		{
			return fNf;
		}
		set
		{
			fNf = value;
		}
	}

	internal float EditorIconSize
	{
		get
		{
			return fNn.IconScale;
		}
		set
		{
			fNn.IconScale = value;
			fN_0003.IconScale = value;
		}
	}

	/// <summary>
	/// Determines if user defined code handles in editor camera movement.
	/// If so only object selection and object movement is processed.
	/// </summary>
	public bool UserHandledView
	{
		get
		{
			return fNn.UserHandledView;
		}
		set
		{
			fNn.UserHandledView = value;
		}
	}

	/// <summary>
	/// Allows specific processing when the editor attached. Commonly used for editor specific input processing.
	/// </summary>
	public bool EditorAttached => false;

	/// <summary>
	/// Allows specific processing when the game window has input focus, not the editor's controls.
	/// </summary>
	public bool GameHasFocus => true;

	/// <summary>
	/// Creates a LightingSystemEditor instance.
	/// </summary>
	/// <param name="sceneinterface"></param>
	/// <param name="graphicsdevicemanager"></param>
	public LightingSystemEditor(IGraphicsDeviceService graphicsdevicemanager, IManagerServiceProvider sceneinterface)
	{
		fNB = sceneinterface;
		fNa = graphicsdevicemanager;
		fN_0003 = new _0015.a();
		fNn = new _0015.B();
	}

	/// <summary />
	~LightingSystemEditor()
	{
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public void ApplyPreferences(ILightingSystemPreferences preferences)
	{
	}

	/// <summary>
	/// Processes in editor input control, object selection, and camera movement.
	/// </summary>
	/// <param name="gametime"></param>
	public void Update(GameTime gametime)
	{
	}

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	public void BeginFrameRendering(ISceneState scenestate)
	{
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public void EndFrameRendering()
	{
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public void Unload()
	{
	}

	/// <summary>
	/// Removes resources managed by this object. Commonly used while clearing the scene.
	/// </summary>
	public void Clear()
	{
	}

	/// <summary>
	/// Opens the SunBurn editor manually.
	/// </summary>
	public void LaunchEditor()
	{
	}

	private void gF()
	{
	}

	internal static void gV(nNB P_0)
	{
		fN_0019 = (nNB)Delegate.Combine(fN_0019, P_0);
	}

	internal static void gA(nNB P_0)
	{
		fNL = (nNB)Delegate.Combine(fNL, P_0);
	}

	internal static void g0(nNa P_0)
	{
		fNv = (nNa)Delegate.Combine(fNv, P_0);
	}

	internal static void gK(nNB P_0)
	{
		fN_0019 = (nNB)Delegate.Remove(fN_0019, P_0);
	}

	internal static void gM(nNB P_0)
	{
		fNL = (nNB)Delegate.Remove(fNL, P_0);
	}

	internal static void g_0006(nNa P_0)
	{
		fNv = (nNa)Delegate.Remove(fNv, P_0);
	}

	/// <summary>
	/// Register delegate used to reload scene assets when requested by the editor.
	/// </summary>
	/// <param name="del"></param>
	public static void RegisterOnReplaceEffect(EffectReplaceDelegate del)
	{
	}

	/// <summary>
	/// Unregister delegate used to reload scene assets when requested by the editor.
	/// </summary>
	/// <param name="del"></param>
	public static void UnregisterOnReplaceEffect(EffectReplaceDelegate del)
	{
	}

	/// <summary>
	/// Call to start tracking user defined resources in the editor.
	/// </summary>
	/// <param name="resource"></param>
	public static void OnCreateResource(IDisposable resource)
	{
	}

	/// <summary>
	/// Call to stop tracking user defined resources in the editor.
	/// </summary>
	/// <param name="resource"></param>
	public static void OnDisposeResource(IDisposable resource)
	{
	}

	internal static void gJ(object P_0, v.g P_1)
	{
	}
}
