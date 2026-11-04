using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.ForceEffects;

/// <summary>
///       Applies a buoyancy force to create swimming bodies.
///       </summary>
public class Buoyancy : ForceField
{
	[CompilerGenerated]
	private float c1f9351ac7d27ba0765be0fc46ba80a3c;

	[CompilerGenerated]
	private float c132d8361466ca3cd896b273180eae0cc;

	[CompilerGenerated]
	private Plane cba144058059d382e7724cdb122abf2c1;

	[CompilerGenerated]
	private float c32e638c4f0a8da07655278a112b4ffbe;

	[CompilerGenerated]
	private float c374d9b669056088b2531f08f4da45e87;

	[CompilerGenerated]
	private Vector3F c2b0a8afaa8b8dfc0f0847c036401616a;

	/// <summary>
	///       Gets or sets the density of the liquid.
	///       </summary>
	public float Density
	{
		[CompilerGenerated]
		get
		{
			return c1f9351ac7d27ba0765be0fc46ba80a3c;
		}
		[CompilerGenerated]
		set
		{
			c1f9351ac7d27ba0765be0fc46ba80a3c = value;
		}
	}

	/// <summary>
	///       Gets or sets the gravity acceleration.
	///       </summary>
	public float Gravity
	{
		[CompilerGenerated]
		get
		{
			return c132d8361466ca3cd896b273180eae0cc;
		}
		[CompilerGenerated]
		set
		{
			c132d8361466ca3cd896b273180eae0cc = value;
		}
	}

	/// <summary>
	///       Gets or sets the water surface plane (in world space).
	///       </summary>
	public Plane Surface
	{
		[CompilerGenerated]
		get
		{
			return cba144058059d382e7724cdb122abf2c1;
		}
		[CompilerGenerated]
		set
		{
			cba144058059d382e7724cdb122abf2c1 = value;
		}
	}

	/// <summary>
	///       Gets or sets the angular drag coefficient.
	///       </summary>
	public float AngularDrag
	{
		[CompilerGenerated]
		get
		{
			return c32e638c4f0a8da07655278a112b4ffbe;
		}
		[CompilerGenerated]
		set
		{
			c32e638c4f0a8da07655278a112b4ffbe = value;
		}
	}

	/// <summary>
	///       Gets or sets the linear drag coefficient.
	///       </summary>
	public float LinearDrag
	{
		[CompilerGenerated]
		get
		{
			return c374d9b669056088b2531f08f4da45e87;
		}
		[CompilerGenerated]
		set
		{
			c374d9b669056088b2531f08f4da45e87 = value;
		}
	}

	/// <summary>
	///       Gets or sets the linear velocity of the water.
	///       </summary>
	public Vector3F Velocity
	{
		[CompilerGenerated]
		get
		{
			return c2b0a8afaa8b8dfc0f0847c036401616a;
		}
		[CompilerGenerated]
		set
		{
			c2b0a8afaa8b8dfc0f0847c036401616a = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.Buoyancy" /> class.
	///       </summary>
	public Buoyancy()
	{
		cfcf5000392556fa5f33e81db7674f9ed();
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.Buoyancy" /> class.
	///       </summary>
	/// <param name="areaOfEffect">The area of effect.</param>
	public Buoyancy(IAreaOfEffect areaOfEffect)
		: base(areaOfEffect)
	{
		cfcf5000392556fa5f33e81db7674f9ed();
	}

	private void cfcf5000392556fa5f33e81db7674f9ed()
	{
		Density = 1000f;
		Gravity = 10f;
		Surface = new Plane(Vector3F.UnitY, 0f);
		AngularDrag = 0.5f;
		LinearDrag = 5f;
		Velocity = Vector3F.Zero;
	}

	/// <summary>
	///       Applies the force effect to the specified body.
	///       </summary>
	/// <param name="body">The rigid body.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="body" /> is <see langword="null" />.
	///       </exception>
	public override void Apply(RigidBody body)
	{
		if (body == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2846), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2855));
		}
		c2963fb90db42c0068cb273726df5881d buoyancyData = body.BuoyancyData;
		if (buoyancyData == null)
		{
			Prepare(body);
			buoyancyData = body.BuoyancyData;
		}
		float num = c8b51ed7a278b5b37e5407753a7f8b693(body.Scale, body.Pose, buoyancyData.Mesh, out var cf94628f848fdb1b294ef4b3736d0f3bc);
		if (num > 0f)
		{
			Vector3F vector3F = Density * num * Gravity * Surface.Normal;
			float num2 = buoyancyData.Volume * body.Scale.X * body.Scale.Y * body.Scale.Z;
			float num3 = body.MassFrame.Mass * num / num2;
			Vector3F velocityOfWorldPoint = body.GetVelocityOfWorldPoint(cf94628f848fdb1b294ef4b3736d0f3bc);
			Vector3F vector3F2 = num3 * LinearDrag * (Velocity - velocityOfWorldPoint);
			Vector3F forceWorld = vector3F + vector3F2;
			ForceEffect.AddForce(body, forceWorld, cf94628f848fdb1b294ef4b3736d0f3bc);
			float num4 = buoyancyData.Length * 1f / 3f * (body.Scale.X + body.Scale.Y + body.Scale.Z);
			float num5 = num4 * num4;
			Vector3F torqueWorld = (0f - num3) * AngularDrag * num5 * body.AngularVelocity;
			ForceEffect.AddTorque(body, torqueWorld);
		}
	}

	/// <summary>
	///       Prepares the specified rigid body for the buoyancy effect.
	///       </summary>
	/// <param name="body">The rigid body.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="body" /> is <see langword="null" />.
	///       </exception>
	public static void Prepare(RigidBody body)
	{
		if (body == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Buoyancy.Prepare(RigidBody));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2846));
				}
			}
		}
		Shape shape = body.Shape;
		Simulation simulation = body.Simulation;
		if (simulation != null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			int count = simulation.RigidBodies.Count;
			for (int i = 0; i < count; i++)
			{
				RigidBody rigidBody = simulation.RigidBodies[i];
				if (rigidBody.Shape != shape || rigidBody.BuoyancyData == null)
				{
					continue;
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					body.BuoyancyData = rigidBody.BuoyancyData;
					return;
				}
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		c2963fb90db42c0068cb273726df5881d c2963fb90db42c0068cb273726df5881d2 = new c2963fb90db42c0068cb273726df5881d();
		c2963fb90db42c0068cb273726df5881d2.Mesh = shape.GetMesh(0.01f, 4);
		c2963fb90db42c0068cb273726df5881d2.Volume = c2963fb90db42c0068cb273726df5881d2.Mesh.GetVolume();
		c2963fb90db42c0068cb273726df5881d2.Length = shape.GetAabb(Pose.Identity).Extent.LargestComponent;
		body.BuoyancyData = c2963fb90db42c0068cb273726df5881d2;
	}

	private float c8b51ed7a278b5b37e5407753a7f8b693(Vector3F ceabb47d7d959545d89c4300eabbdd7c2, Pose cbc1b7e0b011ad6dcdb8beb392ec7a646, TriangleMesh cf1674ba51e57b7e621e83565f0591384, out Vector3F cf94628f848fdb1b294ef4b3736d0f3bc)
	{
		cf94628f848fdb1b294ef4b3736d0f3bc = Vector3F.Zero;
		Plane plane = new Plane
		{
			Normal = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToLocalDirection(Surface.Normal),
			DistanceFromOrigin = Surface.DistanceFromOrigin - Vector3F.Dot(Surface.Normal, cbc1b7e0b011ad6dcdb8beb392ec7a646.Position)
		};
		int count = cf1674ba51e57b7e621e83565f0591384.Vertices.Count;
		List<float> list = ResourcePools<float>.Lists.Obtain();
		try
		{
			int num = 0;
			int index = 0;
			for (int i = 0; i < count; i++)
			{
				float num2 = Vector3F.Dot(plane.Normal, cf1674ba51e57b7e621e83565f0591384.Vertices[i] * ceabb47d7d959545d89c4300eabbdd7c2) - plane.DistanceFromOrigin;
				if (num2 < -1E-06f)
				{
					num++;
					index = i;
				}
				list.Add(num2);
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Buoyancy.c8b51ed7a278b5b37e5407753a7f8b693(Vector3F, Pose, TriangleMesh, ref Vector3F));
				}
				if (num == 0)
				{
					while (true)
					{
						switch (4)
						{
						case 0:
							break;
						default:
							return 0f;
						}
					}
				}
				Vector3F vector3F = cf1674ba51e57b7e621e83565f0591384.Vertices[index] - list[index] * plane.Normal;
				float num3 = 0f;
				int numberOfTriangles = cf1674ba51e57b7e621e83565f0591384.NumberOfTriangles;
				for (int j = 0; j < numberOfTriangles; j++)
				{
					int index2 = cf1674ba51e57b7e621e83565f0591384.Indices[j * 3];
					int index3 = cf1674ba51e57b7e621e83565f0591384.Indices[j * 3 + 1];
					int index4 = cf1674ba51e57b7e621e83565f0591384.Indices[j * 3 + 2];
					Vector3F vector3F2 = cf1674ba51e57b7e621e83565f0591384.Vertices[index2] * ceabb47d7d959545d89c4300eabbdd7c2;
					float num4 = list[index2];
					Vector3F vector3F3 = cf1674ba51e57b7e621e83565f0591384.Vertices[index3] * ceabb47d7d959545d89c4300eabbdd7c2;
					float num5 = list[index3];
					Vector3F vector3F4 = cf1674ba51e57b7e621e83565f0591384.Vertices[index4] * ceabb47d7d959545d89c4300eabbdd7c2;
					float num6 = list[index4];
					if (num4 * num5 < 0f)
					{
						while (true)
						{
							switch (7)
							{
							case 0:
								continue;
							}
							break;
						}
						num3 += cb8827b2cf806add137990044c72a37be(vector3F, vector3F2, vector3F3, vector3F4, num4, num5, num6, ref cf94628f848fdb1b294ef4b3736d0f3bc);
						continue;
					}
					if (num4 * num6 < 0f)
					{
						while (true)
						{
							switch (4)
							{
							case 0:
								continue;
							}
							break;
						}
						num3 += cb8827b2cf806add137990044c72a37be(vector3F, vector3F4, vector3F2, vector3F3, num6, num4, num5, ref cf94628f848fdb1b294ef4b3736d0f3bc);
						continue;
					}
					if (num5 * num6 < 0f)
					{
						num3 += cb8827b2cf806add137990044c72a37be(vector3F, vector3F3, vector3F4, vector3F2, num5, num6, num4, ref cf94628f848fdb1b294ef4b3736d0f3bc);
						continue;
					}
					if (!(num4 < 0f) && !(num5 < 0f))
					{
						if (!(num6 < 0f))
						{
							continue;
						}
						while (true)
						{
							switch (2)
							{
							case 0:
								continue;
							}
							break;
						}
					}
					num3 += c05219c593ba1d41d3ff74af852f0a272(vector3F, vector3F2, vector3F3, vector3F4, ref cf94628f848fdb1b294ef4b3736d0f3bc);
				}
				if (num3 <= 1E-06f)
				{
					while (true)
					{
						switch (1)
						{
						case 0:
							break;
						default:
							cf94628f848fdb1b294ef4b3736d0f3bc = Vector3F.Zero;
							return 0f;
						}
					}
				}
				cf94628f848fdb1b294ef4b3736d0f3bc /= num3;
				cf94628f848fdb1b294ef4b3736d0f3bc = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToWorldPosition(cf94628f848fdb1b294ef4b3736d0f3bc);
				return num3;
			}
		}
		finally
		{
			ResourcePools<float>.Lists.Recycle(list);
		}
	}

	private static float cb8827b2cf806add137990044c72a37be(Vector3F cfef46e983928cc4e6880c424d1468b93, Vector3F cbe6829ca974177ec6282270ed6d4e5c9, Vector3F cc1f9d7339a612e6210b583889d64d189, Vector3F cacc95518f107238003f89fa099ede27a, float c9ebb874b51888f0aa88a7cb2c347afa2, float c69911083449e53ec686ff6c2d92006e0, float c5f4eec6ba629c2ad8a579dbb24c74edf, ref Vector3F cf94628f848fdb1b294ef4b3736d0f3bc)
	{
		Vector3F cbe6829ca974177ec6282270ed6d4e5c10 = cbe6829ca974177ec6282270ed6d4e5c9 + c9ebb874b51888f0aa88a7cb2c347afa2 / (c9ebb874b51888f0aa88a7cb2c347afa2 - c69911083449e53ec686ff6c2d92006e0) * (cc1f9d7339a612e6210b583889d64d189 - cbe6829ca974177ec6282270ed6d4e5c9);
		float num = 0f;
		if (c9ebb874b51888f0aa88a7cb2c347afa2 < 0f)
		{
			if (c5f4eec6ba629c2ad8a579dbb24c74edf < 0f)
			{
				Vector3F vector3F = cc1f9d7339a612e6210b583889d64d189 + c69911083449e53ec686ff6c2d92006e0 / (c69911083449e53ec686ff6c2d92006e0 - c5f4eec6ba629c2ad8a579dbb24c74edf) * (cacc95518f107238003f89fa099ede27a - cc1f9d7339a612e6210b583889d64d189);
				num += c05219c593ba1d41d3ff74af852f0a272(cfef46e983928cc4e6880c424d1468b93, cbe6829ca974177ec6282270ed6d4e5c10, vector3F, cbe6829ca974177ec6282270ed6d4e5c9, ref cf94628f848fdb1b294ef4b3736d0f3bc);
				return num + c05219c593ba1d41d3ff74af852f0a272(cfef46e983928cc4e6880c424d1468b93, vector3F, cacc95518f107238003f89fa099ede27a, cbe6829ca974177ec6282270ed6d4e5c9, ref cf94628f848fdb1b294ef4b3736d0f3bc);
			}
			Vector3F cc1f9d7339a612e6210b583889d64d190 = cbe6829ca974177ec6282270ed6d4e5c9 + c9ebb874b51888f0aa88a7cb2c347afa2 / (c9ebb874b51888f0aa88a7cb2c347afa2 - c5f4eec6ba629c2ad8a579dbb24c74edf) * (cacc95518f107238003f89fa099ede27a - cbe6829ca974177ec6282270ed6d4e5c9);
			return num + c05219c593ba1d41d3ff74af852f0a272(cfef46e983928cc4e6880c424d1468b93, cbe6829ca974177ec6282270ed6d4e5c10, cc1f9d7339a612e6210b583889d64d190, cbe6829ca974177ec6282270ed6d4e5c9, ref cf94628f848fdb1b294ef4b3736d0f3bc);
		}
		if (c5f4eec6ba629c2ad8a579dbb24c74edf < 0f)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
				{
					if (1 == 0)
					{
						__ldtoken(Buoyancy.cb8827b2cf806add137990044c72a37be(Vector3F, Vector3F, Vector3F, Vector3F, float, float, float, ref Vector3F));
					}
					Vector3F cacc95518f107238003f89fa099ede27a2 = cbe6829ca974177ec6282270ed6d4e5c9 + c9ebb874b51888f0aa88a7cb2c347afa2 / (c9ebb874b51888f0aa88a7cb2c347afa2 - c5f4eec6ba629c2ad8a579dbb24c74edf) * (cacc95518f107238003f89fa099ede27a - cbe6829ca974177ec6282270ed6d4e5c9);
					num += c05219c593ba1d41d3ff74af852f0a272(cfef46e983928cc4e6880c424d1468b93, cbe6829ca974177ec6282270ed6d4e5c10, cc1f9d7339a612e6210b583889d64d189, cacc95518f107238003f89fa099ede27a, ref cf94628f848fdb1b294ef4b3736d0f3bc);
					return num + c05219c593ba1d41d3ff74af852f0a272(cfef46e983928cc4e6880c424d1468b93, cbe6829ca974177ec6282270ed6d4e5c10, cacc95518f107238003f89fa099ede27a, cacc95518f107238003f89fa099ede27a2, ref cf94628f848fdb1b294ef4b3736d0f3bc);
				}
				}
			}
		}
		Vector3F cacc95518f107238003f89fa099ede27a3 = cc1f9d7339a612e6210b583889d64d189 + c69911083449e53ec686ff6c2d92006e0 / (c69911083449e53ec686ff6c2d92006e0 - c5f4eec6ba629c2ad8a579dbb24c74edf) * (cacc95518f107238003f89fa099ede27a - cc1f9d7339a612e6210b583889d64d189);
		return num + c05219c593ba1d41d3ff74af852f0a272(cfef46e983928cc4e6880c424d1468b93, cbe6829ca974177ec6282270ed6d4e5c10, cc1f9d7339a612e6210b583889d64d189, cacc95518f107238003f89fa099ede27a3, ref cf94628f848fdb1b294ef4b3736d0f3bc);
	}

	private static float c05219c593ba1d41d3ff74af852f0a272(Vector3F c09fd01352a8161308ebdc5dbb04ebc15, Vector3F cbe6829ca974177ec6282270ed6d4e5c9, Vector3F cc1f9d7339a612e6210b583889d64d189, Vector3F cacc95518f107238003f89fa099ede27a, ref Vector3F cf94628f848fdb1b294ef4b3736d0f3bc)
	{
		Vector3F vector = cc1f9d7339a612e6210b583889d64d189 - cbe6829ca974177ec6282270ed6d4e5c9;
		Vector3F vector2 = cacc95518f107238003f89fa099ede27a - cbe6829ca974177ec6282270ed6d4e5c9;
		Vector3F vector3 = c09fd01352a8161308ebdc5dbb04ebc15 - cbe6829ca974177ec6282270ed6d4e5c9;
		float num = 1f / 6f * Vector3F.Dot(Vector3F.Cross(vector2, vector), vector3);
		cf94628f848fdb1b294ef4b3736d0f3bc += 0.25f * num * (cbe6829ca974177ec6282270ed6d4e5c9 + cc1f9d7339a612e6210b583889d64d189 + cacc95518f107238003f89fa099ede27a + c09fd01352a8161308ebdc5dbb04ebc15);
		return num;
	}
}
