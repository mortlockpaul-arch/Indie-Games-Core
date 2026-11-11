using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Shadows;

public class ShadowMapSurface
{
	private bool l8a = true;

	private Matrix l8_u0013_ = Matrix.Identity;

	private Matrix l8N = Matrix.Identity;

	private bool l8l = true;

	private BoundingFrustum l8L = new BoundingFrustum(Matrix.Identity);

	private Viewport l8w = default;

	private float l8J = 1f;

	private Rectangle l8B = default;

	public Matrix WorldToSurfaceView
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
			l8l = true;
		}
	}

	public Matrix Projection
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
			l8l = true;
		}
	}

	public BoundingFrustum Frustum
	{
		get
		{
			if (l8l)
			{
				l8L.Matrix = l8_u0013_ * l8N;
				l8l = false;
			}
			return l8L;
		}
	}

	public Viewport Viewport => l8w;

	public float LevelOfDetail
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = value;
		}
	}

	public Rectangle RenderTargetLocation
	{
		get
		{
			return l8B;
		}
		set
		{
			l8B = value;
			l8w.X = l8B.X;
			l8w.Y = l8B.Y;
			l8w.Width = l8B.Width;
			l8w.Height = l8B.Height;
			l8w.MinDepth = 0f;
			l8w.MaxDepth = 1f;
		}
	}

	public bool Enabled
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
		}
	}

	internal Rectangle L_u0011_(int param_0)
	{
		return new Rectangle(l8B.X + param_0, l8B.Y + param_0, l8B.Width - param_0 * 2, l8B.Height - param_0 * 2);
	}

	internal void L_u0003_(Vector3 param_0)
	{
		l8_u0013_.Translation = param_0;
	}
}
