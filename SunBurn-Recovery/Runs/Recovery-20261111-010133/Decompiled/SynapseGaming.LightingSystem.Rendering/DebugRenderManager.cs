using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using x;
using y;

namespace SynapseGaming.LightingSystem.Rendering;

public class DebugRenderManager : IManagerService, IRenderableManager, IManager, IUnloadable
{
	private int l8a = 100;

	private ISceneState l8_u0013_;

	private IManagerServiceProvider l8N;

	private BasicEffect l8l;

	private BoundingBoxRenderHelper l8L;

	private List<SceneEntity> l8w = new List<SceneEntity>();

	private List<BaseLight> l8J = new List<BaseLight>();

	[CompilerGenerated]
	private bool l8B;

	public Type ManagerType => typeof(DebugRenderManager);

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

	public IManagerServiceProvider OwnerSceneInterface => l8N;

	public bool RenderInEditor
	{
		[CompilerGenerated]
		get
		{
			return l8B;
		}
		[CompilerGenerated]
		set
		{
			l8B = value;
		}
	}

	public DebugRenderManager(IManagerServiceProvider sceneinterface)
	{
		l8N = sceneinterface;
		l8L = new BoundingBoxRenderHelper();
	}

	public void ApplyPreferences(ISystemPreferences preferences)
	{
	}

	public void BeginFrameRendering(ISceneState scenestate)
	{
		l8_u0013_ = scenestate;
	}

	public void EndFrameRendering()
	{
		if (!RenderInEditor && SunBurnEditor.EditorAttachedStatic)
		{
			return;
		}
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		IObjectManager objectManager = (IObjectManager)l8N.GetManager(SceneInterface.ObjectManagerType, required: false);
		ILightManager lightManager = (ILightManager)l8N.GetManager(SceneInterface.LightManagerType, required: false);
		x.h.LO(graphicsDevice);
		if (l8l == null)
		{
			l8l = new BasicEffect(graphicsDevice);
			l8l.FogEnabled = false;
			l8l.LightingEnabled = false;
			l8l.PreferPerPixelLighting = false;
			l8l.TextureEnabled = false;
			l8l.SpecularColor = Vector3.Zero;
			l8l.VertexColorEnabled = true;
		}
		l8l.World = Matrix.Identity;
		l8l.View = l8_u0013_.View;
		l8l.Projection = l8_u0013_.Projection;
		if (objectManager != null)
		{
			l8w.Clear();
			objectManager.Find(l8w, l8_u0013_.ViewFrustum, ObjectFilter.All);
			foreach (SceneEntity item in l8w)
			{
				if (item != null && !(item is SceneObject { Visible: false }))
				{
					l8L.Submit(item.WorldBoundingBox, Color.LimeGreen);
				}
			}
		}
		if (lightManager != null)
		{
			l8J.Clear();
			lightManager.Find(l8J, l8_u0013_.ViewFrustum, ObjectFilter.EnabledDynamicAndStatic);
			foreach (BaseLight item2 in l8J)
			{
				if (item2 != null && item2.Enabled && item2 is IPointSource)
				{
					l8L.Submit(item2.WorldBoundingBox, Color.Yellow);
				}
			}
		}
		l8L.Render(l8l);
		l8L.Clear();
	}

	public void Clear()
	{
	}

	public void Unload()
	{
		Clear();
		y.a.w_u0005_(ref l8l);
	}
}
