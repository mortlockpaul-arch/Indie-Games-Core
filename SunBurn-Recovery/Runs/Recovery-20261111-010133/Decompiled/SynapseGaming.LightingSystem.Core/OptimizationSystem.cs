using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Rendering;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Core;

public class OptimizationSystem
{
	public static TypeCasters<LightTypeCaster, BaseLight> LightTypeCasters = new TypeCasters<LightTypeCaster, BaseLight>();

	public static TypeCasters<ShadowSourceTypeCaster, IShadowSource> ShadowSourceTypeCasters = new TypeCasters<ShadowSourceTypeCaster, IShadowSource>();

	public static TypeCasters<EffectTypeCaster, Effect> EffectTypeCasters = new TypeCasters<EffectTypeCaster, Effect>();

	public static TypeCasters<SceneEntityTypeCaster, SceneEntity> SceneEntityTypeCasters = new TypeCasters<SceneEntityTypeCaster, SceneEntity>();

	public static TypeCasters<SceneObjectTypeCaster, ISceneObject> SceneObjectTypeCasters = new TypeCasters<SceneObjectTypeCaster, ISceneObject>();

	public static void Clear()
	{
		LightTypeCasters.Clear();
		ShadowSourceTypeCasters.Clear();
		EffectTypeCasters.Clear();
		SceneEntityTypeCasters.Clear();
		SceneObjectTypeCasters.Clear();
	}
}
