using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Editor;

public interface IEditorObject : INamedObject
{
	bool AffectedInCode { get; set; }
}
