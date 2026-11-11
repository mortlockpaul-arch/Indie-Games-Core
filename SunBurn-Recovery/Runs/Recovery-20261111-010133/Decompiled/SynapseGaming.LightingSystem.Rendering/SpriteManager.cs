using System;
using B;
using SynapseGaming.LightingSystem.Core;
using x;

namespace SynapseGaming.LightingSystem.Rendering;

public class SpriteManager : IManagerService, IManager, IUnloadable
{
	private int l8a = 100;

	private IManagerServiceProvider l8_u0013_;

	private B.h<RenderableMesh> l8N = new B.h<RenderableMesh>();

	private B._u0002_<x.Y> l8l = new B._u0002_<x.Y>();

	public Type ManagerType => typeof(SpriteManager);

	public int ManagerProcessOrder
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

	public IManagerServiceProvider OwnerSceneInterface => l8_u0013_;

	public SpriteManager(IManagerServiceProvider sceneinterface)
	{
		l8_u0013_ = sceneinterface;
	}

	public SpriteContainer CreateSpriteContainer()
	{
		SpriteContainer spriteContainer = new SpriteContainer(SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice, l8N, l8l);
		spriteContainer.UpdateType = UpdateType.Automatic;
		return spriteContainer;
	}

	public void ApplyPreferences(ISystemPreferences preferences)
	{
	}

	public void Clear()
	{
		l8N.FreeAllTracked();
		l8l.FreeAllTracked();
	}

	public void Unload()
	{
		l8N.Clear();
		l8l.Unload();
	}
}
