using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Components;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Lights;

public interface ILight : IMovableObject, IWorldBoundingBoxObject, IComponentObject<ILight>, IEditorCreatedObject<ILight>, IEditorObject, INamedObject
{
	bool Enabled { get; set; }

	LightingType LightingType { get; set; }

	Vector3 DiffuseColor { get; set; }

	float Intensity { get; set; }

	bool FillLight { get; set; }

	float FalloffStrength { get; set; }

	Vector3 CompositeColorAndIntensity { get; }

	IShadowSource ShadowSource { get; set; }

	BoundingSphere WorldBoundingSphere { get; }
}
