using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SynapseGaming.LightingSystem.Core;
using _u0002_;
using s;

namespace SynapseGaming.LightingSystem.Editor;

public class SunBurnEditor : IManagerService, IRenderableManager, IUpdatableManager, IManager, IUnloadable
{
	internal delegate void w8a(IDisposable resource);

	internal delegate void w8_u0013_(object obj, s.N updatetype);

	public delegate void EffectReplaceDelegate(Effect currenteffect, Effect neweffect);

	public delegate void ReloadAssetsDelegate();

	private IManagerServiceProvider l8a;

	private int l8_u0013_ = 25;

	private Keys l8N;

	private _u0002_._u0013_ l8l;

	private _u0002_.a l8L;

	private static w8a l8w;

	private static w8a l8J;

	private static w8_u0013_ l8B;

	public static EffectReplaceDelegate ReplaceEffect;

	public static ReloadAssetsDelegate ReloadAssets;

	internal static bool EditorAttachedStatic => false;

	public Type ManagerType => SceneInterface.EditorType;

	public int ManagerProcessOrder
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
		}
	}

	public IManagerServiceProvider OwnerSceneInterface => l8a;

	public Keys LaunchKey
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

	internal float EditorIconSize
	{
		get
		{
			return l8L.IconScale;
		}
		set
		{
			l8L.IconScale = value;
			l8l.IconScale = value;
		}
	}

	public bool UserHandledView
	{
		get
		{
			return l8L.UserHandledView;
		}
		set
		{
			l8L.UserHandledView = value;
		}
	}

	public bool EditorAttached => false;

	public bool GameHasFocus => true;

	public SunBurnEditor(IManagerServiceProvider sceneinterface)
	{
		l8a = sceneinterface;
		l8l = new _u0002_._u0013_();
		l8L = new _u0002_.a();
	}

	~SunBurnEditor()
	{
	}

	public virtual void ApplyPreferences(ISystemPreferences preferences)
	{
	}

	public virtual void Update(GameTime gametime)
	{
	}

	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
	}

	public virtual void EndFrameRendering()
	{
	}

	public virtual void Unload()
	{
	}

	public virtual void Clear()
	{
	}

	public virtual void LaunchEditor()
	{
	}

	private void Nv()
	{
	}

	protected internal virtual void CloseEditor()
	{
	}

	internal static void NY(w8a param_0)
	{
		l8w = (w8a)Delegate.Combine(l8w, param_0);
	}

	internal static void N_u0005_(w8a param_0)
	{
		l8J = (w8a)Delegate.Combine(l8J, param_0);
	}

	internal static void Nb(w8_u0013_ param_0)
	{
		l8B = (w8_u0013_)Delegate.Combine(l8B, param_0);
	}

	internal static void NI(w8a param_0)
	{
		l8w = (w8a)Delegate.Remove(l8w, param_0);
	}

	internal static void N5(w8a param_0)
	{
		l8J = (w8a)Delegate.Remove(l8J, param_0);
	}

	internal static void NW(w8_u0013_ param_0)
	{
		l8B = (w8_u0013_)Delegate.Remove(l8B, param_0);
	}

	public static void RegisterOnReplaceEffect(EffectReplaceDelegate del)
	{
	}

	public static void UnregisterOnReplaceEffect(EffectReplaceDelegate del)
	{
	}

	public static void OnCreateResource(IDisposable resource)
	{
	}

	public static void OnDisposeResource(IDisposable resource)
	{
	}

	internal static void NZ(object param_0, s.N param_1)
	{
	}
}
