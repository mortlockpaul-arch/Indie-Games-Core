using System;
using System.Collections.Generic;
using _0013;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using R;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Effects.Forward;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Manages scene avatars and provides support for rendering
/// and finding avatars by bounding volume.
/// </summary>
public class AvatarManager : IAvatarManager, IRenderableManager, IManagerService, IShadowRenderer, IUpdatableManager, IManager, IUnloadable, IQuery<Avatar>, ISubmit<Avatar>
{
	private class nNB
	{
		public SystemStatistic ObjectsSubmitted = SystemConsole.GetStatistic("SceneGraph_ObjectsSubmitted", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsRemoved = SystemConsole.GetStatistic("SceneGraph_ObjectsRemoved", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsRetrieved = SystemConsole.GetStatistic("SceneGraph_ObjectsRetrieved", SystemStatisticCategory.SceneGraph);

		public SystemStatistic AvatarsRendered = SystemConsole.GetStatistic("Renderer_AvatarsRendered", SystemStatisticCategory.Rendering);

		public SystemStatistic AvatarProxiesRendered = SystemConsole.GetStatistic("Renderer_AvatarProxiesRendered", SystemStatisticCategory.Rendering);
	}

	private int fNB = 70;

	private float fNa = 0.55f;

	private float fNg = 1f;

	private ISceneState fNf;

	private IManagerServiceProvider fN_0003;

	private IGraphicsDeviceService fNn;

	private List<Avatar> fN_0019 = new List<Avatar>(16);

	private Dictionary<Avatar, int> fNL = new Dictionary<Avatar, int>(16);

	private Dictionary<string, Avatar> fNv = new Dictionary<string, Avatar>(32);

	private List<Avatar> fN_0012 = new List<Avatar>(16);

	private List<Avatar> fN_0015 = new List<Avatar>(32);

	private FogEffect fN8;

	private List<Avatar> fNi = new List<Avatar>(16);

	private L.g fN_0005;

	private List<Avatar> fNj = new List<Avatar>(16);

	private nNB fNo = new nNB();

	/// <summary>
	/// Gets the manager specific Type used as a unique key for storing and
	/// requesting the manager from the IManagerServiceProvider.
	/// </summary>
	public Type ManagerType => SceneInterface.AvatarManagerType;

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
	public IGraphicsDeviceService GraphicsDeviceManager => fNn;

	/// <summary>
	/// Controls avatar lighting by blending between approximate directional
	/// and ambient lighting.  A blending value of 0.0f makes avatar lighting
	/// highly directional, while a value of 1.0f makes avatar lighting highly
	/// ambient.
	/// </summary>
	public float AmbientBlend
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
		}
	}

	/// <summary>
	/// Controls avatar lighting intensity, providing a means to tune avatar
	/// lighting to the rest of the scene. An intensity of 1.0f keeps
	/// avatar lighting the same, a value of 0.5f halves the lighting
	/// intensity, while 2.0f doubles it.
	/// </summary>
	public float LightingIntensity
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
	/// Creates a new AvatarManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="sceneinterface">Service provider used to access all other manager services in this scene.</param>
	public AvatarManager(IGraphicsDeviceService graphicsdevicemanager, IManagerServiceProvider sceneinterface)
	{
		fNn = graphicsdevicemanager;
		fN_0003 = sceneinterface;
	}

	/// <summary>
	/// Updates the object and its contained resources.
	/// </summary>
	/// <param name="gametime"></param>
	public virtual void Update(GameTime gametime)
	{
		fN_0015.Clear();
		foreach (KeyValuePair<Avatar, int> item in fNL)
		{
			fN_0015.Add(item.Key);
		}
		foreach (Avatar item2 in fN_0015)
		{
			item2.Update(gametime);
		}
	}

	/// <summary>
	/// Adds an object to the container. This does not transfer ownership, disposable
	/// objects should be maintained and disposed separately.
	/// </summary>
	/// <param name="avatar"></param>
	public void Submit(Avatar avatar)
	{
		fN_0019.Add(avatar);
		if (avatar.UpdateType == UpdateType.Automatic)
		{
			if (fNL.ContainsKey(avatar))
			{
				return;
			}
			fNL.Add(avatar, avatar.MoveId);
			string name = avatar.Name;
			if (!string.IsNullOrEmpty(name))
			{
				fNv[name] = avatar;
			}
		}
		fNo.ObjectsSubmitted.AccumulationValue++;
	}

	/// <summary>
	/// Repositions an object within the container. This method is used when a static object
	/// moves to reposition it in the storage tree / scenegraph.
	/// </summary>
	/// <param name="avatar"></param>
	public virtual void Move(Avatar avatar)
	{
	}

	/// <summary>
	/// Auto-detects moved dynamic objects and repositions them in the storage tree / scenegraph.
	/// </summary>
	public void MoveDynamicObjects()
	{
	}

	/// <summary>
	/// Removes an object from the container.
	/// </summary>
	/// <param name="avatar"></param>
	public virtual void Remove(Avatar avatar)
	{
		fN_0019.Remove(avatar);
		fNL.Remove(avatar);
		fNv.Remove(avatar.Name);
		fNo.ObjectsRemoved.AccumulationValue++;
	}

	/// <summary>
	/// Retrieves a dynamic object of a specific type by name.
	/// </summary>
	/// <typeparam name="TCastType">Type of object to find.</typeparam>
	/// <param name="name">Name of the object to find.</param>
	/// <param name="obj">Returned object.</param>
	/// <returns></returns>
	public bool Find<TCastType>(string name, out TCastType obj) where TCastType : class
	{
		fNv.TryGetValue(name, out var value);
		obj = value as TCastType;
		return obj != null;
	}

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes
	/// and overlap with or are contained in a bounding area.
	/// </summary>
	/// <param name="foundavatars">List used to store found objects during the query.</param>
	/// <param name="worldbounds">Bounding area used to limit query results.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	public void Find(List<Avatar> foundavatars, BoundingFrustum worldbounds, ObjectFilter objectfilter)
	{
		int count = foundavatars.Count;
		foreach (Avatar item in fN_0019)
		{
			if (worldbounds.Contains(item.WorldBoundingBox) != ContainmentType.Disjoint)
			{
				foundavatars.Add(item);
			}
		}
		fNo.ObjectsRetrieved.AccumulationValue += foundavatars.Count - count;
	}

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes
	/// and overlap with or are contained in a bounding area.
	/// </summary>
	/// <param name="foundavatars">List used to store found objects during the query.</param>
	/// <param name="worldbounds">Bounding area used to limit query results.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	public void Find(List<Avatar> foundavatars, BoundingBox worldbounds, ObjectFilter objectfilter)
	{
		int count = foundavatars.Count;
		foreach (Avatar item in fN_0019)
		{
			if (worldbounds.Contains(item.WorldBoundingBox) != ContainmentType.Disjoint)
			{
				foundavatars.Add(item);
			}
		}
		fNo.ObjectsRetrieved.AccumulationValue += foundavatars.Count - count;
	}

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes.
	/// </summary>
	/// <param name="foundavatars">List used to store found objects during the query.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	public void Find(List<Avatar> foundavatars, ObjectFilter objectfilter)
	{
		int count = foundavatars.Count;
		foreach (Avatar item in fN_0019)
		{
			foundavatars.Add(item);
		}
		fNo.ObjectsRetrieved.AccumulationValue += foundavatars.Count - count;
	}

	/// <summary>
	/// Quickly finds all objects near a bounding area without the overhead of
	/// filtering by object type, checking if objects are enabled, or verifying
	/// containment within the bounds.
	/// </summary>
	/// <param name="foundavatars">List used to store found objects during the query.</param>
	/// <param name="worldbounds">Bounding area used to limit query results.</param>
	public void FindFast(List<Avatar> foundavatars, BoundingBox worldbounds)
	{
		Find(foundavatars, worldbounds, ObjectFilter.All);
	}

	/// <summary>
	/// Quickly finds all objects without the overhead of filtering by object
	/// type or checking if objects are enabled.
	/// </summary>
	/// <param name="foundavatars">List used to store found objects during the query.</param>
	public void FindFast(List<Avatar> foundavatars)
	{
		Find(foundavatars, ObjectFilter.All);
	}

	/// <summary>
	/// Prepares for shadow map rendering.
	/// </summary>
	/// <param name="shadowgroup"></param>
	public void BeginShadowGroupRendering(ShadowGroup shadowgroup)
	{
		fNj.Clear();
		fNi.Clear();
		ObjectFilter objectFilter = ObjectFilter.Static;
		if (shadowgroup.ShadowSource.ShadowType == ShadowType.AllObjects)
		{
			objectFilter |= ObjectFilter.Dynamic;
		}
		Find(fNj, shadowgroup.BoundingBox, objectFilter);
		for (int k = 0; k < fNj.Count; k++)
		{
			Avatar avatar = fNj[k];
			if (avatar.CastShadows)
			{
				fNi.Add(avatar);
			}
		}
	}

	/// <summary>
	/// Finalizes shadow map rendering.
	/// </summary>
	/// <param name="shadowgroup"></param>
	public void EndShadowGroupRendering(ShadowGroup shadowgroup)
	{
	}

	/// <summary>
	/// Performs shadow map rendering.
	/// </summary>
	/// <param name="shadowgroup"></param>
	/// <param name="surface"></param>
	/// <param name="shadoweffect"></param>
	public bool RenderToShadowMapSurface(ShadowGroup shadowgroup, ShadowMapSurface surface, Effect shadoweffect)
	{
		LightingSystemPerformance.Begin("AvatarManager.RenderToShadowMapSurface (filter)");
		fNj.Clear();
		foreach (Avatar item in fNi)
		{
			if (surface.Frustum.Contains(item.WorldBoundingSphere) != ContainmentType.Disjoint)
			{
				fNj.Add(item);
			}
		}
		if (fNj.Count < 1)
		{
			return false;
		}
		if (!(shadoweffect is IRenderableEffect) || !(shadoweffect is ISkinnedEffect))
		{
			return false;
		}
		LightingSystemPerformance.Begin("AvatarManager.RenderToShadowMapSurface (render)");
		GraphicsDevice graphicsDevice = fNn.GraphicsDevice;
		IRenderableEffect renderableEffect = shadoweffect as IRenderableEffect;
		ISkinnedEffect skinnedEffect = shadoweffect as ISkinnedEffect;
		if (fN_0005 == null)
		{
			fN_0005 = new L.g(graphicsDevice);
		}
		skinnedEffect.Skinned = false;
		EffectHelper.SyncObjectAndShadowEffects(fN_0005.DefaultEffect, shadoweffect);
		bool result = false;
		int num = 1;
		foreach (Avatar item2 in fNj)
		{
			if (item2.CastShadows)
			{
				AvatarRenderer renderer = item2.Renderer;
				graphicsDevice.DepthStencilState = R.B.fN8;
				graphicsDevice.ReferenceStencil = num;
				graphicsDevice.BlendState = R.B.fN_0015;
				renderer.World = item2.World;
				renderer.View = surface.WorldToSurfaceView;
				renderer.Projection = surface.Projection;
				renderer.Draw(item2.SkinBones, item2.Expression);
				fNo.AvatarsRendered.AccumulationValue++;
				graphicsDevice.DepthStencilState = R.B.fNi;
				graphicsDevice.ReferenceStencil = num;
				graphicsDevice.BlendState = BlendState.Opaque;
				graphicsDevice.RasterizerState = RasterizerState.CullNone;
				renderableEffect.World = fN_0005.e(item2.WorldBoundingBoxProxy);
				skinnedEffect.SkinBones = null;
				shadoweffect.CurrentTechnique.Passes[0].Apply();
				fN_0005._5();
				fNo.AvatarProxiesRendered.AccumulationValue++;
				graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
				result = true;
				num++;
			}
		}
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		return result;
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
		fNf = scenestate;
		fN_0012.Clear();
		Find(fN_0012, scenestate.ViewFrustum, ObjectFilter.All);
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public void EndFrameRendering()
	{
		if (fN_0012.Count < 1)
		{
			return;
		}
		LightingSystemPerformance.Begin("AvatarManager.EndFrameRendering");
		GraphicsDevice graphicsDevice = fNn.GraphicsDevice;
		ILightManager lightManager = (ILightManager)fN_0003.GetManager(SceneInterface.LightManagerType, required: false);
		bool fogEnabled = fNf.Environment.FogEnabled;
		if (fogEnabled)
		{
			if (fN8 == null)
			{
				fN8 = new FogEffect(graphicsDevice);
			}
			if (fN_0005 == null)
			{
				fN_0005 = new L.g(graphicsDevice);
			}
			graphicsDevice.Clear(ClearOptions.Stencil, Color.Black, 0f, 0);
			fN8.Color = fNf.Environment.FogColor;
			fN8.StartDistance = fNf.Environment.FogStartDistance;
			fN8.EndDistance = fNf.Environment.FogEndDistance;
		}
		int num = 1;
		float visibleDistance = fNf.Environment.VisibleDistance;
		float fogStartDistance = fNf.Environment.FogStartDistance;
		Vector3 value = fNf.ViewToWorld.Translation;
		foreach (Avatar item in fN_0012)
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
					graphicsDevice.DepthStencilState = R.B.fN_0005;
					graphicsDevice.ReferenceStencil = num;
				}
				AvatarRenderer renderer = item.Renderer;
				renderer.World = item.World;
				renderer.View = fNf.View;
				renderer.Projection = fNf.Projection;
				if (lightManager != null)
				{
					CompositeLighting compositeLighting = lightManager.GetCompositeLighting(item.WorldBoundingBox, fNa, LightingType.RealTime | LightingType.BakedDown);
					renderer.LightColor = compositeLighting.DiffuseColor * fNg;
					renderer.LightDirection = compositeLighting.Direction;
					renderer.AmbientLightColor = compositeLighting.AmbientColor * fNg * 0.25f;
				}
				else
				{
					renderer.LightColor = new Vector3(0f);
					renderer.AmbientLightColor = new Vector3(0.25f);
				}
				renderer.Draw(item.SkinBones, item.Expression);
				fNo.AvatarsRendered.AccumulationValue++;
				if (flag)
				{
					graphicsDevice.DepthStencilState = R.B.fNj;
					graphicsDevice.ReferenceStencil = num;
					graphicsDevice.RasterizerState = RasterizerState.CullNone;
					graphicsDevice.BlendState = R.B.fNo;
					fN8.World = fN_0005.e(item.WorldBoundingBoxProxy);
					fN8.View = fNf.View;
					fN8.Projection = fNf.Projection;
					fN8.CurrentTechnique.Passes[0].Apply();
					fN_0005._5();
					fNo.AvatarProxiesRendered.AccumulationValue++;
					graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
				}
				num++;
			}
		}
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		graphicsDevice.BlendState = BlendState.Opaque;
		foreach (Avatar item2 in fN_0012)
		{
			item2.RenderCustomPass(fNf);
		}
	}

	/// <summary>
	/// Removes resources managed by this object. Commonly used while clearing the scene.
	/// </summary>
	public void Clear()
	{
		fN_0019.Clear();
		fNL.Clear();
		fNv.Clear();
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public void Unload()
	{
		Clear();
		_0013.B.nu(ref fN_0005);
		_0013.B.nu(ref fN8);
	}
}
