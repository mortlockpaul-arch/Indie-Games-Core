using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Editor;

public interface IEditorCreatedObject<T> : IEditorObject, INamedObject
{
	T Clone();

	void OnCreatedInEditor();
}
