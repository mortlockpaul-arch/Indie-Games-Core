using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

public class LineRenderHelper : BasePrimitiveRenderHelper
{
	protected override PrimitiveType PrimitiveType => PrimitiveType.LineList;

	public void Submit(Vector3 start, Vector3 end, Color startcolor, Color endcolor)
	{
		SubmitVertex(ref start, ref startcolor);
		SubmitVertex(ref end, ref endcolor);
	}

	public void Submit(Vector3 start, Vector3 end, Color color)
	{
		SubmitVertex(ref start, ref color);
		SubmitVertex(ref end, ref color);
	}
}
