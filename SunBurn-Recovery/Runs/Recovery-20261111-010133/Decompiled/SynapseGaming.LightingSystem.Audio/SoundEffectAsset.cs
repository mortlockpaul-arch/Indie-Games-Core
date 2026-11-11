using Microsoft.Xna.Framework.Audio;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Audio;

public class SoundEffectAsset : ContentRepositoryAsset<SoundEffect>
{
	public static readonly SoundEffectAsset Empty = new SoundEffectAsset();

	private SoundEffectAsset()
	{
	}

	public SoundEffectAsset(string repositoryname, string sourceassetfilepath)
		: base(repositoryname, sourceassetfilepath)
	{
	}
}
