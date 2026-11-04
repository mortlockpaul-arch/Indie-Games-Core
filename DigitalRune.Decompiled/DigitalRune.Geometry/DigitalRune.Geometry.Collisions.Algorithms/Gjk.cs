using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       The Gilbert-Johnson-Keerthi (GJK) algorithm for computing closest points of convex objects.
///       </summary>
public class Gjk : CollisionAlgorithm
{
	private const int ccc81a89e5837c5d9e080ee22b5a22784 = 64;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.Gjk" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public Gjk(CollisionDetection collisionDetection)
		: base(collisionDetection)
	{
	}

	/// <summary>
	///       Computes the collision. - This method should only be used by 
	///       <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> instances!
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="type">The type of collision query.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="contactSet" /> does not contain two <see cref="T:DigitalRune.Geometry.Shapes.ConvexShape" />s.
	///       </exception>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///   <paramref name="type" /> is set to <see cref="F:DigitalRune.Geometry.Collisions.CollisionQueryType.Contacts" />. This collision 
	///       algorithm cannot handle contact queries. Use <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.MinkowskiPortalRefinement" /> 
	///       instead.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		if (type == CollisionQueryType.Contacts)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Gjk.ComputeCollision(ContactSet, CollisionQueryType));
					}
					throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11504));
				}
			}
		}
		IGeometricObject geometricObject = contactSet.ObjectA.GeometricObject;
		ConvexShape convexShape = geometricObject.Shape as ConvexShape;
		Vector3F scale = geometricObject.Scale;
		Pose pose = geometricObject.Pose;
		IGeometricObject geometricObject2 = contactSet.ObjectB.GeometricObject;
		ConvexShape convexShape2 = geometricObject2.Shape as ConvexShape;
		Vector3F scale2 = geometricObject2.Scale;
		Pose pose2 = geometricObject2.Pose;
		if (convexShape == null || convexShape2 == null)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11607), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
		}
		cc49dcd41ee82253a805139c508e0926f cc49dcd41ee82253a805139c508e0926f2 = cc49dcd41ee82253a805139c508e0926f.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		try
		{
			Vector3F vector3F;
			if (contactSet.Count > 0)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				vector3F = -contactSet[0].Normal;
			}
			else
			{
				Vector3F vector3F2 = pose.ToWorldPosition(convexShape.InnerPoint * scale);
				Vector3F vector3F3 = pose2.ToWorldPosition(convexShape2.InnerPoint * scale2);
				vector3F = vector3F2 - vector3F3;
			}
			if (vector3F.IsNumericallyZero)
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
				vector3F = Vector3F.UnitZ;
			}
			Matrix33F transposed = pose.Orientation.Transposed;
			Matrix33F transposed2 = pose2.Orientation.Transposed;
			int num = 0;
			float num2 = float.MaxValue;
			contactSet.HaveContact = false;
			float num5;
			do
			{
				Vector3F vector3F4 = pose.ToWorldPosition(convexShape.GetSupportPoint(transposed * -vector3F, scale));
				Vector3F vector3F5 = pose2.ToWorldPosition(convexShape2.GetSupportPoint(transposed2 * vector3F, scale2));
				Vector3F vector3F6 = vector3F4 - vector3F5;
				float num3 = Vector3F.Dot(vector3F6, vector3F);
				if (num3 > 0f)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
					if (type == CollisionQueryType.Boolean)
					{
						return;
					}
					if (type == CollisionQueryType.Contacts)
					{
						while (true)
						{
							switch (1)
							{
							case 0:
								break;
							default:
								return;
							}
						}
					}
				}
				if (cc49dcd41ee82253a805139c508e0926f2.ce475b78d124a2feef9cd2aab44c161c4(vector3F6) || num2 - num3 <= num2 * Numeric.EpsilonF)
				{
					break;
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				cc49dcd41ee82253a805139c508e0926f2.c03d72a32723924cb5b81c585d5df7d9d(vector3F6, vector3F4, vector3F5);
				cc49dcd41ee82253a805139c508e0926f2.c322ef1ce205c6dbcf3a93ad899943435();
				if (!cc49dcd41ee82253a805139c508e0926f2.IsValid)
				{
					break;
				}
				vector3F = cc49dcd41ee82253a805139c508e0926f2.c596154f5b0c243d05e4fa792fd81b4bb;
				float num4 = num2;
				num2 = vector3F.LengthSquared;
				if (num4 < num2)
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
					num2 = num4;
					cc49dcd41ee82253a805139c508e0926f2.ca501566257a4c82d152a429f5a74ecb6();
					break;
				}
				if (num4 - num2 <= num4 * Numeric.EpsilonF)
				{
					break;
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				num++;
				if (num > 64)
				{
					break;
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				num5 = Numeric.EpsilonFSquared * Math.Max(1f, cc49dcd41ee82253a805139c508e0926f2.c8a14f47232b640512ed789adc8c198a2);
			}
			while (!cc49dcd41ee82253a805139c508e0926f2.c8cfa49e753c046fa77edb85a6fcf2975 && num2 > num5);
			Vector3F c79a3b94c816fc4e87d6348c3713a = cc49dcd41ee82253a805139c508e0926f2.c8db9d816e59b2168612a23097569af14 - cc49dcd41ee82253a805139c508e0926f2.cfaa13c3933478652e1d13c21f7cfece2;
			num5 = Numeric.EpsilonFSquared * Math.Max(1f, cc49dcd41ee82253a805139c508e0926f2.c8a14f47232b640512ed789adc8c198a2);
			float num6;
			if (num2 <= num5)
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
				if (!c79a3b94c816fc4e87d6348c3713a.TryNormalize())
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
					c79a3b94c816fc4e87d6348c3713a = Vector3F.UnitY;
				}
				num6 = 0f;
				contactSet.HaveContact = true;
			}
			else
			{
				num6 = (float)Math.Sqrt(num2);
				c79a3b94c816fc4e87d6348c3713a /= num6;
				if (cc49dcd41ee82253a805139c508e0926f2.c8cfa49e753c046fa77edb85a6fcf2975)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
					if (cc49dcd41ee82253a805139c508e0926f2.IsValid)
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
						contactSet.HaveContact = true;
						num6 = 0f;
					}
				}
			}
			if (type == CollisionQueryType.Boolean)
			{
				return;
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				Vector3F c2fed316f5bcf4dd4ea7883930f3822f = (cc49dcd41ee82253a805139c508e0926f2.cfaa13c3933478652e1d13c21f7cfece2 + cc49dcd41ee82253a805139c508e0926f2.c8db9d816e59b2168612a23097569af14) / 2f;
				Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, c79a3b94c816fc4e87d6348c3713a, 0f - num6, c72057f882b90cac0b4126f84e6c9aa28: false);
				c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
				return;
			}
		}
		finally
		{
			cc49dcd41ee82253a805139c508e0926f2.ca18b9b5faa9851d6355f617787bb5859();
		}
	}
}
