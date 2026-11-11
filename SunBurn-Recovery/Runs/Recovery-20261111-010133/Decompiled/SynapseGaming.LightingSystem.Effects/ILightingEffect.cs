using System.Collections.Generic;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects;

public interface ILightingEffect
{
	int MaxLightSources { get; }

	List<BaseLight> LightSources { set; }
}
