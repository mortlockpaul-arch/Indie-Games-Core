using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

public class BoundingBoxRenderHelper : LineRenderHelper
{
	private static Vector3[] l8a = new Vector3[8];

	public void Submit(BoundingBox bounds, Color color)
	{
		bounds.GetCorners(l8a);
		for (int i = 0; i < 3; i++)
		{
			Submit(l8a[i], l8a[i + 1], color);
			Submit(l8a[i + 4], l8a[i + 5], color);
			Submit(l8a[i], l8a[i + 4], color);
		}
		Submit(l8a[0], l8a[3], color);
		Submit(l8a[4], l8a[7], color);
		Submit(l8a[3], l8a[7], color);
	}
}
