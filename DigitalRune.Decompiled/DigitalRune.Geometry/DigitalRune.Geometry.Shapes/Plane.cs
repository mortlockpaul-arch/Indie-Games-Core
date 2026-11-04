using System;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Defines a plane.
///       </summary>
[Serializable]
public struct Plane : IEquatable<Plane>
{
	/// <summary>
	///       The normalized, outward pointing normal vector.
	///       </summary>
	public Vector3F Normal;

	/// <summary>
	///       The distance of the plane from the origin (also known as the "plane constant").
	///       </summary>
	public float DistanceFromOrigin;

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.Plane" /> from normal vector and distance to the 
	///       origin.
	///       </summary>
	/// <param name="normal">
	///       The normalized, outward pointing normal vector of the plane. 
	///       </param>
	/// <param name="distanceFromOrigin">
	///       The distance from the origin.
	///       </param>
	public Plane(Vector3F normal, float distanceFromOrigin)
	{
		Normal = normal;
		DistanceFromOrigin = distanceFromOrigin;
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.Plane" /> from three points.
	///       </summary>
	/// <param name="point0">A point on the plane.</param>
	/// <param name="point1">A point on the plane.</param>
	/// <param name="point2">A point on the plane.</param>
	public Plane(Vector3F point0, Vector3F point1, Vector3F point2)
	{
		Normal = Vector3F.Cross(point1 - point0, point2 - point0).Normalized;
		DistanceFromOrigin = Vector3F.Dot(point0, Normal);
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.Plane" /> from normal vector and a point on the 
	///       plane.
	///       </summary>
	/// <param name="normal">
	///       The normalized, outward pointing normal vector of the plane.
	///       </param>
	/// <param name="pointOnPlane">A point on the plane.</param>
	public Plane(Vector3F normal, Vector3F pointOnPlane)
	{
		Normal = normal;
		DistanceFromOrigin = Vector3F.Dot(pointOnPlane, Normal);
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.Plane" /> from a <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" />.
	///       </summary>
	/// <param name="planeShape">
	///       The <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" /> from which normal vector and distance from origin are copied.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="planeShape" /> is <see langword="null" />.
	///       </exception>
	public Plane(PlaneShape planeShape)
	{
		if (planeShape == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(16777));
		}
		Normal = planeShape.Normal;
		DistanceFromOrigin = planeShape.DistanceFromOrigin;
	}

	/// <summary>
	///       Determines whether the specified <see cref="T:System.Object" /> is equal to the current 
	///       <see cref="T:System.Object" />.
	///       </summary>
	/// <param name="obj">
	///       The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current 
	///       <see cref="T:System.Object" />; otherwise, <see langword="false" />.
	///       </returns>
	public override bool Equals(object obj)
	{
		if (!(obj is Plane plane))
		{
			return false;
		}
		if (Normal == plane.Normal)
		{
			return DistanceFromOrigin == plane.DistanceFromOrigin;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified <see cref="T:DigitalRune.Geometry.Shapes.Plane" /> is equal to the current 
	///       <see cref="T:DigitalRune.Geometry.Shapes.Plane" />.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(Plane other)
	{
		if (Normal == other.Normal)
		{
			return DistanceFromOrigin == other.DistanceFromOrigin;
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.Plane" />s are equal.
	///       </summary>
	/// <param name="plane1">The first <see cref="T:DigitalRune.Geometry.Shapes.Plane" />.</param>
	/// <param name="plane2">The second <see cref="T:DigitalRune.Geometry.Shapes.Plane" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.Plane" />s are equal; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator ==(Plane plane1, Plane plane2)
	{
		if (plane1.Normal == plane2.Normal)
		{
			return plane1.DistanceFromOrigin == plane2.DistanceFromOrigin;
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.Plane" />s are different.
	///       </summary>
	/// <param name="plane1">The first <see cref="T:DigitalRune.Geometry.Shapes.Plane" />.</param>
	/// <param name="plane2">The second <see cref="T:DigitalRune.Geometry.Shapes.Plane" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.Plane" />s are different; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator !=(Plane plane1, Plane plane2)
	{
		if (!(plane1.Normal != plane2.Normal))
		{
			return plane1.DistanceFromOrigin != plane2.DistanceFromOrigin;
		}
		return true;
	}

	/// <summary>
	///       Returns a hash code for this instance.
	///       </summary>
	/// <returns>
	///       A hash code for this instance, suitable for use in hashing algorithms and data structures
	///       like a hash table. 
	///       </returns>
	public override int GetHashCode()
	{
		int hashCode = Normal.GetHashCode();
		return (hashCode * 397) ^ DistanceFromOrigin.GetHashCode();
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(16540), Normal, DistanceFromOrigin);
	}

	internal void c22463d4a85fcdaa7d4ffbd8669084d72(ref Vector3F ceabb47d7d959545d89c4300eabbdd7c2)
	{
		if (ceabb47d7d959545d89c4300eabbdd7c2.X == ceabb47d7d959545d89c4300eabbdd7c2.Y)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(Plane.c22463d4a85fcdaa7d4ffbd8669084d72(ref Vector3F));
			}
			if (ceabb47d7d959545d89c4300eabbdd7c2.Y == ceabb47d7d959545d89c4300eabbdd7c2.Z)
			{
				if (ceabb47d7d959545d89c4300eabbdd7c2.X < 0f)
				{
					DistanceFromOrigin *= 0f - ceabb47d7d959545d89c4300eabbdd7c2.X;
					Normal = -Normal;
				}
				else
				{
					DistanceFromOrigin *= ceabb47d7d959545d89c4300eabbdd7c2.X;
				}
				return;
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(16627));
	}

	internal void c77d310c5ae14ed7e7ed360995b1867bb(ref Pose cbc1b7e0b011ad6dcdb8beb392ec7a646)
	{
		Normal = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToWorldDirection(Normal);
		Vector3F vector = cbc1b7e0b011ad6dcdb8beb392ec7a646.Position + Normal * DistanceFromOrigin;
		DistanceFromOrigin = Vector3F.Dot(vector, Normal);
	}

	internal void c0d5e70cac6e675cb3df8faaefbfd7052(ref Pose cbc1b7e0b011ad6dcdb8beb392ec7a646)
	{
		Vector3F worldPosition = Normal * DistanceFromOrigin;
		Normal = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToLocalDirection(Normal);
		worldPosition = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToLocalPosition(worldPosition);
		DistanceFromOrigin = Vector3F.Dot(worldPosition, Normal);
	}
}
