using System.Collections.Generic;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Rendering;

public interface IScene : IEditorActiveObject, IEditorObject, INamedObject
{
	List<ILightGroup> LightGroups { get; }

	List<ISceneEntityGroup> EntityGroups { get; }

	void SetContainingManager<T>(IManager manager);

	void Apply();

	void Clear();
}
