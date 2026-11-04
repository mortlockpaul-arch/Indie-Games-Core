using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Interface used by game objects that implement moving while contained
/// within a manager / container object.
/// </summary>
public interface IMovableObject : IWorldBoundingBoxObject, INamedObject
{
	/// <summary>
	/// Unique id used to identify the object across multiple scene loads / reloads.
	/// </summary>
	int UniqueId { get; }

	/// <summary>
	/// Indicates the object bounding area spans the entire world and
	/// the object is always visible.
	/// </summary>
	bool InfiniteBounds { get; }

	/// <summary>
	/// Indicates the current move. This value increments each time the object
	/// is moved (when the World transform changes).
	/// </summary>
	int MoveId { get; }

	/// <summary>
	/// Determines if objects receive update events from the engine and are tracked
	/// by the scenegraph.
	///
	/// Automatic update events are necessary to be affected by gravity, for
	/// components, and for the scenegraph to track moving objects.  Objects without
	/// Automatic update events can still move, however the containing scenegraph
	/// (ObjectManager or LightManager) must be notified using Manager.Move(object).
	/// </summary>
	UpdateType UpdateType { get; set; }

	/// <summary>
	/// World space transform of the object.
	/// </summary>
	Matrix World { get; set; }

	/// <summary>
	/// Event used to update the object at regular intervals. This and all
	/// events are only called on dynamic objects.
	/// </summary>
	event UpdateDelegate UpdateEvent;

	/// <summary>
	/// Updates the object using the provided game time.
	/// </summary>
	/// <param name="gametime"></param>
	void Update(GameTime gametime);
}
