using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public interface IObjectManager : IWorldRenderableManager, IRenderableManager, IManagerService, IUpdatableManager, IManager, IUnloadable, IQuery<SceneEntity>, ISubmit<SceneEntity>, IQuery<RenderableMesh>, ISubmit<IScene>
{
	void SubmitStaticSceneObject(Model model, Matrix world, ObjectVisibility visibility);

	void SubmitStaticSceneObjectPerMesh(Model model, Matrix world, ObjectVisibility visibility);

	void SubmitStaticSceneObject(ModelMesh mesh, Matrix world, ObjectVisibility visibility);

	void SubmitStaticSceneObject(Model model, Effect overrideeffect, Matrix world, ObjectVisibility visibility);

	void SubmitStaticSceneObjectPerMesh(Model model, Effect overrideeffect, Matrix world, ObjectVisibility visibility);

	void SubmitStaticSceneObject(ModelMesh mesh, Effect overrideeffect, Matrix world, ObjectVisibility visibility);

	new void Clear();

	new TCastType Find<TCastType>(string name, bool onlysearchdynamicobjects) where TCastType : class;

	new TCastType Find<TCastType>(int uniqueid) where TCastType : class;

	new bool Find<TCastType>(string name, bool onlysearchdynamicobjects, out TCastType obj) where TCastType : class;

	new bool Find<TCastType>(int uniqueid, out TCastType obj) where TCastType : class;
}
