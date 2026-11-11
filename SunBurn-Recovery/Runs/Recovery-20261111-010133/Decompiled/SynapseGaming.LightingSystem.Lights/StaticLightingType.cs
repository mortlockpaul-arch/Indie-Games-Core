using SynapseGaming.LightingSystem.Editor;

namespace SynapseGaming.LightingSystem.Lights;

public enum StaticLightingType
{
	[EditorEnumDescription("None")]
	None,
	[EditorEnumDescription("Approximate")]
	Composite,
	[EditorEnumDescription("Light Mapped")]
	BakedDown,
	[EditorEnumDescription("Custom")]
	Custom
}
