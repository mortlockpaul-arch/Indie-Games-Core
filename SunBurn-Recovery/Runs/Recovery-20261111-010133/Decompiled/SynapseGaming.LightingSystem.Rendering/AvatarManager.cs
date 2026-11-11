using System;
using System.Collections.Generic;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Effects.Forward;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;
using _u0004_;
using y;

namespace SynapseGaming.LightingSystem.Rendering;

public class AvatarManager : BaseObjectGraphManager<Avatar, IAvatarManager>, IAvatarManager, IRenderableManager, IManagerService, IShadowRenderer, IUpdatableManager, IManager, IUnloadable, IQuery<Avatar>, ISubmit<Avatar>
{
	private class w8a
	{
		public SystemStatistic ObjectsSubmitted = SystemConsole.GetStatistic("SceneGraph_ObjectsSubmitted", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsRemoved = SystemConsole.GetStatistic("SceneGraph_ObjectsRemoved", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsRetrieved = SystemConsole.GetStatistic("SceneGraph_ObjectsRetrieved", SystemStatisticCategory.SceneGraph);

		public SystemStatistic AvatarsRendered = SystemConsole.GetStatistic("Renderer_AvatarsRendered", SystemStatisticCategory.Rendering);

		public SystemStatistic AvatarProxiesRendered = SystemConsole.GetStatistic("Renderer_AvatarProxiesRendered", SystemStatisticCategory.Rendering);
	}

	private int l8a = 70;

	private float l8_u0013_ = 0.55f;

	private float l8N = 1f;

	private ISceneState l8l;

	private List<Avatar> l8L = new List<Avatar>(16);

	private List<Avatar> l8w = new List<Avatar>(32);

	private FogEffect l8J;

	private List<Avatar> l8B = new List<Avatar>(16);

	private B.N l8s;

	private List<Avatar> l8h = new List<Avatar>(16);

	private w8a l8_u0002_ = new w8a();

	public override Type ManagerType => SceneInterface.AvatarManagerType;

	public override int ManagerProcessOrder
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

	public float AmbientBlend
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

	public float LightingIntensity
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

	public AvatarManager(IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
	}

	public void BeginShadowGroupRendering(ShadowGroup shadowgroup)
	{
		l8h.Clear();
		l8B.Clear();
		ObjectFilter objectFilter = ObjectFilter.Static;
		if (shadowgroup.ShadowSource.ShadowType == ShadowType.AllObjects)
		{
			objectFilter |= ObjectFilter.Dynamic;
		}
		Find(l8h, shadowgroup.BoundingBox, objectFilter);
		for (int i = 0; i < l8h.Count; i++)
		{
			Avatar avatar = l8h[i];
			if (avatar.CastShadows)
			{
				l8B.Add(avatar);
			}
		}
	}

	public void EndShadowGroupRendering(ShadowGroup shadowgroup)
	{
	}

	public bool RenderToShadowMapSurface(ShadowGroup shadowgroup, ShadowMapSurface surface, Effect shadoweffect)
	{
		LightingSystemPerformance.Begin("AvatarManager.RenderToShadowMapSurface (filter)");
		l8h.Clear();
		foreach (Avatar item in l8B)
		{
			if (surface.Frustum.Contains(item.WorldBoundingSphere) != ContainmentType.Disjoint)
			{
				l8h.Add(item);
			}
		}
		if (l8h.Count < 1)
		{
			return false;
		}
		if (!(shadoweffect is IRenderableEffect) || !(shadoweffect is ISkinnedEffect))
		{
			return false;
		}
		LightingSystemPerformance.Begin("AvatarManager.RenderToShadowMapSurface (render)");
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		IRenderableEffect renderableEffect = shadoweffect as IRenderableEffect;
		ISkinnedEffect skinnedEffect = shadoweffect as ISkinnedEffect;
		if (l8s == null)
		{
			l8s = new B.N(graphicsDevice);
		}
		skinnedEffect.Skinned = false;
		EffectHelper.SyncObjectAndShadowEffects(l8s.DefaultEffect, shadoweffect);
		bool result = false;
		int num = 1;
		foreach (Avatar item2 in l8h)
		{
			if (item2.CastShadows)
			{
				AvatarRenderer renderer = item2.Renderer;
				graphicsDevice.DepthStencilState = _u0004_.a.l8v;
				graphicsDevice.ReferenceStencil = num;
				graphicsDevice.BlendState = _u0004_.a.l8_u0002_;
				renderer.World = item2.World;
				renderer.View = surface.WorldToSurfaceView;
				renderer.Projection = surface.Projection;
				renderer.Draw(item2.SkinBones, item2.Expression);
				l8_u0002_.AvatarsRendered.AccumulationValue++;
				graphicsDevice.DepthStencilState = _u0004_.a.l8Y;
				graphicsDevice.ReferenceStencil = num;
				graphicsDevice.BlendState = BlendState.Opaque;
				graphicsDevice.RasterizerState = RasterizerState.CullNone;
				renderableEffect.World = l8s._u0018_(item2.WorldBoundingBoxProxy);
				skinnedEffect.SkinBones = null;
				shadoweffect.CurrentTechnique.Passes[0].Apply();
				l8s.S();
				l8_u0002_.AvatarProxiesRendered.AccumulationValue++;
				graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
				result = true;
				num++;
			}
		}
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		return result;
	}

	public void BeginFrameRendering(ISceneState scenestate)
	{
		l8l = scenestate;
		l8L.Clear();
		Find(l8L, scenestate.ViewFrustum, ObjectFilter.All);
	}

	public void EndFrameRendering()
	{
		if (l8L.Count < 1)
		{
			return;
		}
		LightingSystemPerformance.Begin("AvatarManager.EndFrameRendering");
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		ILightManager lightManager = (ILightManager)OwnerSceneInterface.GetManager(SceneInterface.LightManagerType, required: false);
		bool fogEnabled = l8l.Environment.FogEnabled;
		if (fogEnabled)
		{
			if (l8J == null)
			{
				l8J = new FogEffect(graphicsDevice);
			}
			if (l8s == null)
			{
				l8s = new B.N(graphicsDevice);
			}
			graphicsDevice.Clear(ClearOptions.Stencil, Color.Black, 0f, 0);
			l8J.Color = l8l.Environment.FogColor;
			l8J.StartDistance = l8l.Environment.FogStartDistance;
			l8J.EndDistance = l8l.Environment.FogEndDistance;
		}
		int num = 1;
		float visibleDistance = l8l.Environment.VisibleDistance;
		float fogStartDistance = l8l.Environment.FogStartDistance;
		Vector3 value = l8l.ViewToWorld.Translation;
		foreach (Avatar item in l8L)
		{
			if (!item.Visible)
			{
				continue;
			}
			BoundingSphere worldBoundingSphere = item.WorldBoundingSphere;
			float num2 = visibleDistance + worldBoundingSphere.Radius;
			Vector3.DistanceSquared(ref value, ref worldBoundingSphere.Center, out var result);
			if (!(result > num2 * num2))
			{
				bool flag = false;
				if (fogEnabled)
				{
					num2 = fogStartDistance - worldBoundingSphere.Radius;
					flag = result > num2 * num2;
				}
				if (fogEnabled)
				{
					graphicsDevice.DepthStencilState = _u0004_.a.l8_u0005_;
					graphicsDevice.ReferenceStencil = num;
				}
				AvatarRenderer renderer = item.Renderer;
				renderer.World = item.World;
				renderer.View = l8l.View;
				renderer.Projection = l8l.Projection;
				if (lightManager != null)
				{
					CompositeLighting compositeLighting = lightManager.GetCompositeLighting(item.WorldBoundingBox, l8_u0013_, LightingType.RealTime | LightingType.BakedDown);
					renderer.LightColor = compositeLighting.DiffuseColor * l8N;
					renderer.LightDirection = compositeLighting.Direction;
					renderer.AmbientLightColor = compositeLighting.AmbientColor * l8N * 0.25f;
				}
				else
				{
					renderer.LightColor = new Vector3(0f);
					renderer.AmbientLightColor = new Vector3(0.25f);
				}
				renderer.Draw(item.SkinBones, item.Expression);
				l8_u0002_.AvatarsRendered.AccumulationValue++;
				if (flag)
				{
					graphicsDevice.DepthStencilState = _u0004_.a.l8b;
					graphicsDevice.ReferenceStencil = num;
					graphicsDevice.RasterizerState = RasterizerState.CullNone;
					graphicsDevice.BlendState = _u0004_.a.l8I;
					l8J.World = l8s._u0018_(item.WorldBoundingBoxProxy);
					l8J.View = l8l.View;
					l8J.Projection = l8l.Projection;
					l8J.CurrentTechnique.Passes[0].Apply();
					l8s.S();
					l8_u0002_.AvatarProxiesRendered.AccumulationValue++;
					graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
				}
				num++;
			}
		}
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		graphicsDevice.BlendState = BlendState.Opaque;
		foreach (Avatar item2 in l8L)
		{
			item2.RenderCustomPass(l8l);
		}
	}

	public override void Unload()
	{
		base.Unload();
		y.a.w_u0005_(ref l8s);
		y.a.w_u0005_(ref l8J);
	}
}
