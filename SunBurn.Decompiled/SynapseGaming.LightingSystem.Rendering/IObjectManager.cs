using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Interface that provides access to the scene's object manager. The object manager
/// provides methods for storing and querying scene objects.
/// </summary>
public interface IObjectManager : IWorldRenderableManager, IRenderableManager, IManagerService, IUpdatableManager, IManager, IUnloadable, IQuery<SceneEntity>, ISubmit<SceneEntity>, IQuery<RenderableMesh>, ISubmit<IScene>
{
	/// <summary>
	/// Helper method that creates and submits a static scene
	/// object using a method layout similar to SunBurn 1.2.x.
	///
	/// NOTE: This method creates a single scene object for an
	/// entire model.  This is ideal for small models such as
	/// props, however large models that represent entire rooms
	/// or scenes need to be split into separate objects per
	/// model mesh using SubmitStaticSceneObjectPerMesh.
	/// </summary>
	/// <param name="model">Source model.</param>
	/// <param name="world">Scene object world transform.</param>
	/// <param name="visibility">Defines how the object is rendered.
	/// This enumeration is a Flag, which allows combining multiple values using the
	/// Logical OR operator (example: "ObjectVisibility.Rendered | ObjectVisibility.CastShadows",
	/// both renders objects and casts shadows from them).</param>
	void SubmitStaticSceneObject(Model model, Matrix world, ObjectVisibility visibility);

	/// <summary>
	/// Helper method that creates and submits a static scene
	/// object using a method layout similar to SunBurn 1.2.x.
	///
	/// NOTE: This method creates a scene object for each mesh
	/// contained in the model.  This is ideal for large models
	/// that represent entire rooms or scenes, however small
	/// models such as props should be contained in a single scene
	/// object using SubmitStaticSceneObject.
	/// </summary>
	/// <param name="model">Source model.</param>
	/// <param name="world">Scene object world transform.</param>
	/// <param name="visibility">Defines how the object is rendered.
	/// This enumeration is a Flag, which allows combining multiple values using the
	/// Logical OR operator (example: "ObjectVisibility.Rendered | ObjectVisibility.CastShadows",
	/// both renders objects and casts shadows from them).</param>
	void SubmitStaticSceneObjectPerMesh(Model model, Matrix world, ObjectVisibility visibility);

	/// <summary>
	/// Helper method that creates and submits a static scene
	/// object using a method layout similar to SunBurn 1.2.x.
	/// </summary>
	/// <param name="mesh">Source model mesh.</param>
	/// <param name="world">Scene object world transform.</param>
	/// <param name="visibility">Defines how the object is rendered.
	/// This enumeration is a Flag, which allows combining multiple values using the
	/// Logical OR operator (example: "ObjectVisibility.Rendered | ObjectVisibility.CastShadows",
	/// both renders objects and casts shadows from them).</param>
	void SubmitStaticSceneObject(ModelMesh mesh, Matrix world, ObjectVisibility visibility);

	/// <summary>
	/// Helper method that creates and submits a static scene
	/// object using a method layout similar to SunBurn 1.2.x.
	///
	/// NOTE: This method creates a single scene object for an
	/// entire model.  This is ideal for small models such as
	/// props, however large models that represent entire rooms
	/// or scenes need to be split into separate objects per
	/// model mesh using SubmitStaticSceneObjectPerMesh.
	/// </summary>
	/// <param name="model">Source model.</param>
	/// <param name="overrideeffect">User defined effect used to render the object.</param>
	/// <param name="world">Scene object world transform.</param>
	/// <param name="visibility">Defines how the object is rendered.
	/// This enumeration is a Flag, which allows combining multiple values using the
	/// Logical OR operator (example: "ObjectVisibility.Rendered | ObjectVisibility.CastShadows",
	/// both renders objects and casts shadows from them).</param>
	void SubmitStaticSceneObject(Model model, Effect overrideeffect, Matrix world, ObjectVisibility visibility);

	/// <summary>
	/// Helper method that creates and submits a static scene
	/// object using a method layout similar to SunBurn 1.2.x.
	///
	/// NOTE: This method creates a scene object for each mesh
	/// contained in the model.  This is ideal for large models
	/// that represent entire rooms or scenes, however small
	/// models such as props should be contained in a single scene
	/// object using SubmitStaticSceneObject.
	/// </summary>
	/// <param name="model">Source model.</param>
	/// <param name="overrideeffect">User defined effect used to render the object.</param>
	/// <param name="world">Scene object world transform.</param>
	/// <param name="visibility">Defines how the object is rendered.
	/// This enumeration is a Flag, which allows combining multiple values using the
	/// Logical OR operator (example: "ObjectVisibility.Rendered | ObjectVisibility.CastShadows",
	/// both renders objects and casts shadows from them).</param>
	void SubmitStaticSceneObjectPerMesh(Model model, Effect overrideeffect, Matrix world, ObjectVisibility visibility);

	/// <summary>
	/// Helper method that creates and submits a static scene
	/// object using a method layout similar to SunBurn 1.2.x.
	/// </summary>
	/// <param name="mesh">Source model mesh.</param>
	/// <param name="overrideeffect">User defined effect used to render the object.</param>
	/// <param name="world">Scene object world transform.</param>
	/// <param name="visibility">Defines how the object is rendered.
	/// This enumeration is a Flag, which allows combining multiple values using the
	/// Logical OR operator (example: "ObjectVisibility.Rendered | ObjectVisibility.CastShadows",
	/// both renders objects and casts shadows from them).</param>
	void SubmitStaticSceneObject(ModelMesh mesh, Effect overrideeffect, Matrix world, ObjectVisibility visibility);

	/// <summary>
	/// Removes all objects from the container. Commonly used while clearing the scene.
	/// </summary>
	new void Clear();

	/// <summary>
	/// Retrieves a dynamic object of a specific type by name.
	/// </summary>
	/// <typeparam name="TCastType">Type of object to find.</typeparam>
	/// <param name="name">Name of the object to find.</param>
	/// <param name="obj">Returned object.</param>
	/// <returns></returns>
	new bool Find<TCastType>(string name, out TCastType obj) where TCastType : class;
}
