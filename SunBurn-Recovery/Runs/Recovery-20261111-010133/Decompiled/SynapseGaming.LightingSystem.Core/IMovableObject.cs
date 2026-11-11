using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

public interface IMovableObject : IWorldBoundingBoxObject, INamedObject
{
	int UniqueId { get; }

	bool InfiniteBounds { get; }

	int MoveId { get; }

	UpdateType UpdateType { get; set; }

	Matrix World { get; set; }

	TypeDictionary<IManagerService> ContainingManagers { get; }

	event UpdateDelegate UpdateEvent;

	event SubmitRemoveManagerDelegate SubmittedToManagerEvent;

	event SubmitRemoveManagerDelegate RemovedFromManagerEvent;

	void OnSubmittedToManager(IManagerService manager);

	void OnRemovedFromManager(IManagerService manager);

	void Update(GameTime gametime);
}
