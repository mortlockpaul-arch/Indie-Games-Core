using SynapseGaming.LightingSystem.Editor;

namespace SynapseGaming.LightingSystem.Shadows;

public enum ShadowType
{
	[EditorEnumDescription("No Shadows")]
	None,
	[EditorEnumDescription("Static Objects")]
	StaticObjects,
	[EditorEnumDescription("All Objects")]
	AllObjects
}
