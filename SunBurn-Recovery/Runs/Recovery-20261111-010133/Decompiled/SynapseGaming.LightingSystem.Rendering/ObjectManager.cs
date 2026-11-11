using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public class ObjectManager : BaseObjectGraphManager<SceneEntity, IObjectManager>, IObjectManager, IWorldRenderableManager, IRenderableManager, IManagerService, IUpdatableManager, IManager, IUnloadable, IQuery<SceneEntity>, ISubmit<SceneEntity>, IQuery<RenderableMesh>, ISubmit<IScene>
{
	private int l8a = 50;

	private List<SceneEntity> l8_u0013_ = new List<SceneEntity>();

	public override Type ManagerType => SceneInterface.ObjectManagerType;

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

	public ObjectManager(IManagerServiceProvider sceneinterface, BoundingBox worldboundingbox, int worldtreemaxdepth)
		: base(sceneinterface, worldboundingbox, worldtreemaxdepth)
	{
	}

	public ObjectManager(IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
	}

	~ObjectManager()
	{
	}

	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
	}

	public virtual void EndFrameRendering()
	{
		SplashScreen.L_u0015_();
	}

	public override void Update(GameTime gametime)
	{
		LightingSystemPerformance.Begin("ObjectManager.Update");
		base.Update(gametime);
	}

	private void L0(Effect param_0, Effect param_1)
	{
		l8_u0013_.Clear();
		Find(l8_u0013_, ObjectFilter.All);
		foreach (SceneEntity item in l8_u0013_)
		{
			if (item is ISceneObject sceneObject)
			{
				for (int i = 0; i < sceneObject.RenderableMeshes.Count; i++)
				{
					sceneObject.RenderableMeshes[i].RemapEffect();
				}
			}
		}
	}

	public void SubmitStaticSceneObject(Model model, Matrix world, ObjectVisibility visibility)
	{
		SceneObject sceneObject = new SceneObject(model);
		sceneObject.UpdateType = UpdateType.None;
		sceneObject.Visibility = visibility;
		sceneObject.World = world;
		Submit(sceneObject);
	}

	public void SubmitStaticSceneObjectPerMesh(Model model, Matrix world, ObjectVisibility visibility)
	{
		int count = model.Meshes.Count;
		for (int i = 0; i < count; i++)
		{
			SubmitStaticSceneObject(model.Meshes[i], world, visibility);
		}
	}

	public void SubmitStaticSceneObject(ModelMesh mesh, Matrix world, ObjectVisibility visibility)
	{
		SceneObject sceneObject = new SceneObject(mesh);
		sceneObject.UpdateType = UpdateType.None;
		sceneObject.Visibility = visibility;
		sceneObject.World = world;
		Submit(sceneObject);
	}

	public void SubmitStaticSceneObject(Model model, Effect overrideeffect, Matrix world, ObjectVisibility visibility)
	{
		SceneObject sceneObject = new SceneObject(model, overrideeffect, model.Root.Name);
		sceneObject.UpdateType = UpdateType.None;
		sceneObject.Visibility = visibility;
		sceneObject.World = world;
		Submit(sceneObject);
	}

	public void SubmitStaticSceneObjectPerMesh(Model model, Effect overrideeffect, Matrix world, ObjectVisibility visibility)
	{
		int count = model.Meshes.Count;
		for (int i = 0; i < count; i++)
		{
			SubmitStaticSceneObject(model.Meshes[i], overrideeffect, world, visibility);
		}
	}

	public void SubmitStaticSceneObject(ModelMesh mesh, Effect overrideeffect, Matrix world, ObjectVisibility visibility)
	{
		SceneObject sceneObject = new SceneObject(mesh, overrideeffect, mesh.ParentBone.Name);
		sceneObject.UpdateType = UpdateType.None;
		sceneObject.Visibility = visibility;
		sceneObject.World = world;
		Submit(sceneObject);
	}

	public virtual void Find(List<RenderableMesh> foundmeshes, BoundingFrustum worldbounds, ObjectFilter objectfilter)
	{
		l8_u0013_.Clear();
		Find(l8_u0013_, worldbounds, objectfilter);
		foreach (SceneEntity item in l8_u0013_)
		{
			if (item == null)
			{
				continue;
			}
			SceneEntityTypeCaster sceneEntityTypeCaster = OptimizationSystem.SceneEntityTypeCasters.Get(item);
			ISceneObject sceneObject = sceneEntityTypeCaster.SceneObject;
			if (sceneObject != null)
			{
				for (int i = 0; i < sceneObject.RenderableMeshes.Count; i++)
				{
					foundmeshes.Add(sceneObject.RenderableMeshes[i]);
				}
			}
		}
	}

	public virtual void Find(List<RenderableMesh> foundmeshes, BoundingBox worldbounds, ObjectFilter objectfilter)
	{
		l8_u0013_.Clear();
		Find(l8_u0013_, worldbounds, objectfilter);
		foreach (SceneEntity item in l8_u0013_)
		{
			if (item == null)
			{
				continue;
			}
			SceneEntityTypeCaster sceneEntityTypeCaster = OptimizationSystem.SceneEntityTypeCasters.Get(item);
			ISceneObject sceneObject = sceneEntityTypeCaster.SceneObject;
			if (sceneObject != null)
			{
				for (int i = 0; i < sceneObject.RenderableMeshes.Count; i++)
				{
					foundmeshes.Add(sceneObject.RenderableMeshes[i]);
				}
			}
		}
	}

	public virtual void Find(List<RenderableMesh> foundmeshes, ObjectFilter objectfilter)
	{
		l8_u0013_.Clear();
		Find(l8_u0013_, objectfilter);
		foreach (SceneEntity item in l8_u0013_)
		{
			if (item == null)
			{
				continue;
			}
			SceneEntityTypeCaster sceneEntityTypeCaster = OptimizationSystem.SceneEntityTypeCasters.Get(item);
			ISceneObject sceneObject = sceneEntityTypeCaster.SceneObject;
			if (sceneObject != null)
			{
				for (int i = 0; i < sceneObject.RenderableMeshes.Count; i++)
				{
					foundmeshes.Add(sceneObject.RenderableMeshes[i]);
				}
			}
		}
	}

	public void FindFast(List<RenderableMesh> foundmeshes, BoundingBox worldbounds)
	{
		l8_u0013_.Clear();
		FindFast(l8_u0013_, worldbounds);
		foreach (SceneEntity item in l8_u0013_)
		{
			if (item == null)
			{
				continue;
			}
			SceneEntityTypeCaster sceneEntityTypeCaster = OptimizationSystem.SceneEntityTypeCasters.Get(item);
			ISceneObject sceneObject = sceneEntityTypeCaster.SceneObject;
			if (sceneObject != null)
			{
				for (int i = 0; i < sceneObject.RenderableMeshes.Count; i++)
				{
					foundmeshes.Add(sceneObject.RenderableMeshes[i]);
				}
			}
		}
	}

	public void FindFast(List<RenderableMesh> foundmeshes)
	{
		l8_u0013_.Clear();
		FindFast(l8_u0013_);
		foreach (SceneEntity item in l8_u0013_)
		{
			if (item == null)
			{
				continue;
			}
			SceneEntityTypeCaster sceneEntityTypeCaster = OptimizationSystem.SceneEntityTypeCasters.Get(item);
			ISceneObject sceneObject = sceneEntityTypeCaster.SceneObject;
			if (sceneObject != null)
			{
				for (int i = 0; i < sceneObject.RenderableMeshes.Count; i++)
				{
					foundmeshes.Add(sceneObject.RenderableMeshes[i]);
				}
			}
		}
	}
}
