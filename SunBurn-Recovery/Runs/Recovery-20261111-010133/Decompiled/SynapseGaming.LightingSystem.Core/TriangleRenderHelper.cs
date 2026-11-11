using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

public class TriangleRenderHelper : BasePrimitiveRenderHelper
{
	protected override PrimitiveType PrimitiveType => PrimitiveType.TriangleList;

	public void Submit(Vector3 a, Vector3 b, Vector3 c, Color acolor, Color bcolor, Color ccolor)
	{
		SubmitVertex(ref a, ref acolor);
		SubmitVertex(ref b, ref bcolor);
		SubmitVertex(ref c, ref ccolor);
	}

	public void Submit(Vector3 a, Vector3 b, Vector3 c, Color color)
	{
		SubmitVertex(ref a, ref color);
		SubmitVertex(ref b, ref color);
		SubmitVertex(ref c, ref color);
	}
}
