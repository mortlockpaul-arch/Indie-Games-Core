using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Shadows;

/// <summary>
/// Class that represents one surface in a shadow map, which can be
/// used for multi-part rendering and level-of-detail. The surface
/// contains its own section within a render target.
/// </summary>
public class ShadowMapSurface
{
	private bool fNB = true;

	private Matrix fNa = Matrix.Identity;

	private Matrix fNg = Matrix.Identity;

	private bool fNf = true;

	private BoundingFrustum fN_0003 = new BoundingFrustum(Matrix.Identity);

	private Viewport fNn = default;

	private float fN_0019 = 1f;

	private Rectangle fNL = default;

	/// <summary>
	/// View transform used to project the scene into the
	/// surface and the surface onto the scene.
	/// </summary>
	public Matrix WorldToSurfaceView
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
			fNf = true;
		}
	}

	/// <summary>
	/// Projection transform used to project the scene into
	/// the surface and the surface onto the scene.
	/// </summary>
	public Matrix Projection
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
			fNf = true;
		}
	}

	/// <summary>
	/// The surface projection frustum.
	/// </summary>
	public BoundingFrustum Frustum
	{
		get
		{
			if (fNf)
			{
				fN_0003.Matrix = fNa * fNg;
				fNf = false;
			}
			return fN_0003;
		}
	}

	/// <summary>
	/// Viewport used when rendering to the surface render target location.
	/// </summary>
	public Viewport Viewport => fNn;

	/// <summary>
	/// Level-of-detail applied to the surface.
	/// </summary>
	public float LevelOfDetail
	{
		get
		{
			return fN_0019;
		}
		set
		{
			fN_0019 = value;
		}
	}

	/// <summary>
	/// The surface location in the render target.
	/// </summary>
	public Rectangle RenderTargetLocation
	{
		get
		{
			return fNL;
		}
		set
		{
			fNL = value;
			fNn.X = fNL.X;
			fNn.Y = fNL.Y;
			fNn.Width = fNL.Width;
			fNn.Height = fNL.Height;
			fNn.MinDepth = 0f;
			fNn.MaxDepth = 1f;
		}
	}

	/// <summary>
	/// Determines if the shadow map contents should be generated for this face.
	/// </summary>
	public bool Enabled
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
		}
	}

	/// <summary>
	/// Creates a new ShadowMapSurface instance.
	/// </summary>
	public ShadowMapSurface()
	{
	}

	internal Rectangle n_0005(int P_0)
	{
		return new Rectangle(fNL.X + P_0, fNL.Y + P_0, fNL.Width - P_0 * 2, fNL.Height - P_0 * 2);
	}

	internal void nj(Vector3 P_0)
	{
		fNa.Translation = P_0;
	}
}
