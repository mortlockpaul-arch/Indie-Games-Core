using System;
using A;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Defines a ray.
///       </summary>
[Serializable]
public struct Ray : IEquatable<Ray>
{
	/// <summary>
	///       The origin of the ray.
	///       </summary>
	public Vector3F Origin;

	/// <summary>
	///       The normalized direction of the ray.
	///       </summary>
	public Vector3F Direction;

	/// <summary>
	///       The finite length of the ray.
	///       </summary>
	public float Length;

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.Line" /> with the given origin and direction.
	///       </summary>
	/// <param name="origin">The origin.</param>
	/// <param name="direction">The direction.</param>
	/// <param name="length">The finite length.</param>
	public Ray(Vector3F origin, Vector3F direction, float length)
	{
		Origin = origin;
		Direction = direction;
		Length = length;
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.Ray" /> from a <see cref="T:DigitalRune.Geometry.Shapes.RayShape" />.
	///       </summary>
	/// <param name="rayShape">
	///       The <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> from which origin and direction are copied.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="rayShape" /> is <see langword="null" />.
	///       </exception>
	public Ray(RayShape rayShape)
	{
		if (rayShape == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11791));
		}
		Origin = rayShape.Origin;
		Direction = rayShape.Direction;
		Length = rayShape.Length;
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
		if (!(obj is Ray ray))
		{
			return false;
		}
		if (Origin == ray.Origin)
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
			if (1 == 0)
			{
				__ldtoken(Ray.Equals(object));
			}
			if (Direction == ray.Direction)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						return Length == ray.Length;
					}
				}
			}
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified <see cref="T:DigitalRune.Geometry.Shapes.Ray" /> is equal to the current 
	///       <see cref="T:DigitalRune.Geometry.Shapes.Ray" />.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(Ray other)
	{
		if (Origin == other.Origin && Direction == other.Direction)
		{
			return Length == other.Length;
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.Ray" />s are equal.
	///       </summary>
	/// <param name="ray1">The first <see cref="T:DigitalRune.Geometry.Shapes.Ray" />.</param>
	/// <param name="ray2">The second <see cref="T:DigitalRune.Geometry.Shapes.Ray" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.Ray" />s are equal; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator ==(Ray ray1, Ray ray2)
	{
		if (ray1.Origin == ray2.Origin && ray1.Direction == ray2.Direction)
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
						__ldtoken(Ray.op_Equality(Ray, Ray));
					}
					return ray1.Length == ray2.Length;
				}
			}
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.Ray" />s are different.
	///       </summary>
	/// <param name="ray1">The first <see cref="T:DigitalRune.Geometry.Shapes.Ray" />.</param>
	/// <param name="ray2">The second <see cref="T:DigitalRune.Geometry.Shapes.Ray" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.Ray" />s are different; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator !=(Ray ray1, Ray ray2)
	{
		if (!(ray1.Origin != ray2.Origin))
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
			if (1 == 0)
			{
				__ldtoken(Ray.op_Inequality(Ray, Ray));
			}
			if (!(ray1.Direction != ray2.Direction))
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						return ray1.Length != ray2.Length;
					}
				}
			}
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
		int hashCode = Origin.GetHashCode();
		hashCode = (hashCode * 397) ^ Direction.GetHashCode();
		return (hashCode * 397) ^ Length.GetHashCode();
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11702), Origin, Direction, Length);
	}

	internal void c22463d4a85fcdaa7d4ffbd8669084d72(ref Vector3F ceabb47d7d959545d89c4300eabbdd7c2)
	{
		if (ceabb47d7d959545d89c4300eabbdd7c2.X == ceabb47d7d959545d89c4300eabbdd7c2.Y && ceabb47d7d959545d89c4300eabbdd7c2.Y == ceabb47d7d959545d89c4300eabbdd7c2.Z)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref Vector3F));
					}
					if (ceabb47d7d959545d89c4300eabbdd7c2.X < 0f)
					{
						while (true)
						{
							switch (2)
							{
							case 0:
								break;
							default:
								Origin *= ceabb47d7d959545d89c4300eabbdd7c2.X;
								Length *= 0f - ceabb47d7d959545d89c4300eabbdd7c2.X;
								Direction = -Direction;
								return;
							}
						}
					}
					Origin *= ceabb47d7d959545d89c4300eabbdd7c2.X;
					Length *= ceabb47d7d959545d89c4300eabbdd7c2.X;
					return;
				}
			}
		}
		Vector3F vector3F = Origin + Length * Direction;
		Origin *= ceabb47d7d959545d89c4300eabbdd7c2;
		vector3F *= ceabb47d7d959545d89c4300eabbdd7c2;
		Vector3F vector3F2 = vector3F - Origin;
		Length = vector3F2.Length;
		if (Numeric.IsZero(Length))
		{
			return;
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			Direction = vector3F2 / Length;
			return;
		}
	}

	internal void c77d310c5ae14ed7e7ed360995b1867bb(ref Pose cbc1b7e0b011ad6dcdb8beb392ec7a646)
	{
		Origin = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToWorldPosition(Origin);
		Direction = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToWorldDirection(Direction);
	}

	internal void c0d5e70cac6e675cb3df8faaefbfd7052(ref Pose cbc1b7e0b011ad6dcdb8beb392ec7a646)
	{
		Origin = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToLocalPosition(Origin);
		Direction = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToLocalDirection(Direction);
	}
}
