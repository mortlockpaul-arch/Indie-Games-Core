using System;

namespace SynapseGaming.LightingSystem.Core;

[Serializable]
public enum SamplingPreference
{
	Point,
	Bilinear,
	Trilinear,
	Anisotropic
}
