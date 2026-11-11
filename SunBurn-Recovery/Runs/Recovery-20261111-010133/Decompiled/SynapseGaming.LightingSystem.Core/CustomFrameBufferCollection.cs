using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

public class CustomFrameBufferCollection : List<RenderTarget2D>
{
	public CustomFrameBufferCollection()
		: base(8)
	{
	}
}
