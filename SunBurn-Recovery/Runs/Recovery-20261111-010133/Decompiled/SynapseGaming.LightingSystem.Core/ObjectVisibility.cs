using System;

namespace SynapseGaming.LightingSystem.Core;

[Flags]
public enum ObjectVisibility
{
	None = 0,
	Rendered = 1,
	CastShadows = 2,
	RenderedInEditor = 4,
	RenderedAndCastShadows = Rendered | CastShadows | RenderedInEditor
}
