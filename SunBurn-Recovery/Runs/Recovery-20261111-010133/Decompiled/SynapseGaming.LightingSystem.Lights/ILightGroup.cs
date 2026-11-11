using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Lights;

public interface ILightGroup : IGroup<ILight>, IShadowSource, IEditorCreatedObject<ILightGroup>, IEditorObject, INamedObject
{
	bool ShadowGroup { get; set; }

	Vector3 Position { get; set; }

	IList<ILight> Lights { get; }
}
