using System;

namespace SynapseGaming.LightingSystem.Core;

[Flags]
public enum ObjectFilter
{
	Dynamic = 1,
	Static = 2,
	Enabled = 4,
	Disabled = 8,
	DynamicAndStatic = Dynamic | Static,
	EnabledAndDisabled = Enabled | Disabled,
	EnabledDynamicAndStatic = DynamicAndStatic | Enabled,
	All = 0xFFFF
}
