using System;

namespace SynapseGaming.LightingSystem.Core;

[Flags]
public enum SystemStatisticCategory
{
	None = 0,
	Rendering = 1,
	Lighting = 2,
	Shadowing = 4,
	SceneGraph = 8,
	Collision = 0x10,
	Performance = 0x20,
	UserDefined1 = 0x10000,
	UserDefined2 = 0x20000,
	UserDefined3 = 0x40000,
	UserDefined4 = 0x80000,
	UserDefined5 = 0x100000,
	UserDefined6 = 0x200000,
	UserDefined7 = 0x400000,
	UserDefined8 = 0x800000,
	All = 0x3FFFFFFF
}
