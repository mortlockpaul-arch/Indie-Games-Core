using System.Collections.Generic;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;

namespace SynapseGaming.LightingSystem.Rendering;

public interface ISceneEntityGroup : IGroup<ISceneEntity>, IEditorCreatedObject<ISceneEntityGroup>, IEditorObject, INamedObject
{
	IList<ISceneEntity> Entities { get; }
}
