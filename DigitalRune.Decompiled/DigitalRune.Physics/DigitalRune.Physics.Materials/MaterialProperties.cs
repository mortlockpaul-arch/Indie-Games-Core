using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Materials;

/// <summary>
///       Defines material properties of a rigid body.
///       </summary>
public struct MaterialProperties
{
	private float c75e8d5254c8b573a529df00477042b97;

	private float cd3d90de535d3e61ac1f4aa8982379a33;

	private float c3029b3eb4d165025b85c5363274411cc;

	private bool ca4ca8bb2733f7052d87ae12c5eea4937;

	private Vector3F c290d35f3db3252d3284ac100ea993e23;

	/// <summary>
	///       Gets or sets the static friction coefficient.
	///       </summary>
	public float StaticFriction
	{
		get
		{
			return c75e8d5254c8b573a529df00477042b97;
		}
		set
		{
			c75e8d5254c8b573a529df00477042b97 = value;
		}
	}

	/// <summary>
	///       Gets or sets the dynamic friction (kinetic friction) coefficient.
	///       </summary>
	public float DynamicFriction
	{
		get
		{
			return cd3d90de535d3e61ac1f4aa8982379a33;
		}
		set
		{
			cd3d90de535d3e61ac1f4aa8982379a33 = value;
		}
	}

	/// <summary>
	///       Gets or sets the coefficient of restitution (bounciness).
	///       </summary>
	public float Restitution
	{
		get
		{
			return c3029b3eb4d165025b85c5363274411cc;
		}
		set
		{
			c3029b3eb4d165025b85c5363274411cc = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether this material supports surface motion.
	///       </summary>
	public bool SupportsSurfaceMotion
	{
		get
		{
			return ca4ca8bb2733f7052d87ae12c5eea4937;
		}
		set
		{
			ca4ca8bb2733f7052d87ae12c5eea4937 = value;
		}
	}

	/// <summary>
	///       Gets or sets the velocity of the rigid body surface (in local space of the body).
	///       </summary>
	public Vector3F SurfaceMotion
	{
		get
		{
			return c290d35f3db3252d3284ac100ea993e23;
		}
		set
		{
			c290d35f3db3252d3284ac100ea993e23 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Materials.MaterialProperties" /> structure.
	///       </summary>
	/// <param name="staticFriction">The static friction.</param>
	/// <param name="dynamicFriction">The dynamic friction.</param>
	/// <param name="restitution">The coefficient of restitution.</param>
	public MaterialProperties(float staticFriction, float dynamicFriction, float restitution)
	{
		c75e8d5254c8b573a529df00477042b97 = staticFriction;
		cd3d90de535d3e61ac1f4aa8982379a33 = dynamicFriction;
		c3029b3eb4d165025b85c5363274411cc = restitution;
		ca4ca8bb2733f7052d87ae12c5eea4937 = false;
		c290d35f3db3252d3284ac100ea993e23 = Vector3F.Zero;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Materials.MaterialProperties" /> structure.
	///       </summary>
	/// <param name="staticFriction">The static friction.</param>
	/// <param name="dynamicFriction">The dynamic friction.</param>
	/// <param name="restitution">The coefficient of restitution.</param>
	/// <param name="supportsSurfaceMotion">
	///       If set to <see langword="true" /> the material supports surface motion.
	///       </param>
	/// <param name="surfaceMotion">The surface motion velocity.</param>
	public MaterialProperties(float staticFriction, float dynamicFriction, float restitution, bool supportsSurfaceMotion, Vector3F surfaceMotion)
	{
		c75e8d5254c8b573a529df00477042b97 = staticFriction;
		cd3d90de535d3e61ac1f4aa8982379a33 = dynamicFriction;
		c3029b3eb4d165025b85c5363274411cc = restitution;
		ca4ca8bb2733f7052d87ae12c5eea4937 = supportsSurfaceMotion;
		c290d35f3db3252d3284ac100ea993e23 = surfaceMotion;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Materials.MaterialProperties" /> structure.
	///       </summary>
	/// <param name="staticFriction">The static friction.</param>
	/// <param name="dynamicFriction">The dynamic friction.</param>
	/// <param name="restitution">The coefficient of restitution.</param>
	/// <param name="surfaceMotion">The surface motion velocity.</param>
	public MaterialProperties(float staticFriction, float dynamicFriction, float restitution, Vector3F surfaceMotion)
	{
		c75e8d5254c8b573a529df00477042b97 = staticFriction;
		cd3d90de535d3e61ac1f4aa8982379a33 = dynamicFriction;
		c3029b3eb4d165025b85c5363274411cc = restitution;
		ca4ca8bb2733f7052d87ae12c5eea4937 = true;
		c290d35f3db3252d3284ac100ea993e23 = surfaceMotion;
	}
}
