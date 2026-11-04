using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Helper class that provides quick and easy BoundingBox rendering.
/// </summary>
public class BoundingBoxRenderHelper : LineRenderHelper
{
	private static Vector3[] fNB = new Vector3[8];

	/// <summary>
	/// Creates a new BoundingBoxRenderHelper instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	public BoundingBoxRenderHelper(IGraphicsDeviceService graphicsdevicemanager)
		: base(graphicsdevicemanager)
	{
	}

	/// <summary>
	/// Submits a single BoundingBox to the render helper.
	///
	/// Please note: all BoundingBoxes contained in the render helper
	/// *must* be in the same space (ie: object space, world space, ...), as they
	/// are rendered all at once in the Render method using the same effect
	/// property values (including world, view, and projection transforms).
	/// </summary>
	/// <param name="bounds"></param>
	/// <param name="color"></param>
	public void Submit(BoundingBox bounds, Color color)
	{
		bounds.GetCorners(fNB);
		for (int i = 0; i < 3; i++)
		{
			Submit(fNB[i], fNB[i + 1], color);
			Submit(fNB[i + 4], fNB[i + 5], color);
			Submit(fNB[i], fNB[i + 4], color);
		}
		Submit(fNB[0], fNB[3], color);
		Submit(fNB[4], fNB[7], color);
		Submit(fNB[3], fNB[7], color);
	}
}
