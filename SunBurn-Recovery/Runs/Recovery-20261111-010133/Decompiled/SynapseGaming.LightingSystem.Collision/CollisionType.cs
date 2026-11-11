using SynapseGaming.LightingSystem.Editor;

namespace SynapseGaming.LightingSystem.Collision;

public enum CollisionType
{
	[EditorEnumDescription("No Collisions")]
	None,
	[EditorEnumDescription("Solid Object")]
	Collide,
	[EditorEnumDescription("Trigger")]
	Trigger
}
