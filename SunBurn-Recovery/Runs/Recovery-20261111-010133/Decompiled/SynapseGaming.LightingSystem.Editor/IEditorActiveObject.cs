using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Editor;

public interface IEditorActiveObject : IEditorObject, INamedObject
{
	bool AssetInUse { get; }
}
