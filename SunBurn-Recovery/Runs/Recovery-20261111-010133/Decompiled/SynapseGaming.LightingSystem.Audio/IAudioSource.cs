using Microsoft.Xna.Framework.Audio;
using SynapseGaming.LightingSystem.Components;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Audio;

public interface IAudioSource : ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject
{
	bool Loop { get; set; }

	float Volume { get; set; }

	float Radius { get; set; }

	float DopplerScale { get; set; }

	AudioType AudioType { get; set; }

	AudioState AudioState { get; set; }

	SoundEffect SoundEffect { get; }

	SoundEffectAsset SoundEffectAsset { get; set; }

	void Play();

	void Stop();
}
