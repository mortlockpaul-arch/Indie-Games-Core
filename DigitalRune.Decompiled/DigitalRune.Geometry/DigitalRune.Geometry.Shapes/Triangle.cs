using System;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Defines a triangle.
///       </summary>
[Serializable]
public struct Triangle : IEquatable<Triangle>
{
	/// <summary>
	///       The first vertex.
	///       </summary>
	public Vector3F Vertex0;

	/// <summary>
	///       The second vertex.
	///       </summary>
	public Vector3F Vertex1;

	/// <summary>
	///       The third vertex.
	///       </summary>
	public Vector3F Vertex2;

	/// <summary>
	///       Gets the axis-aligned bounding box (AABB) for this triangle.
	///       </summary>
	public Aabb Aabb
	{
		get
		{
			Vector3F minimum = Vector3F.Min(Vertex0, Vector3F.Min(Vertex1, Vertex2));
			Vector3F maximum = Vector3F.Max(Vertex0, Vector3F.Max(Vertex1, Vertex2));
			return new Aabb(minimum, maximum);
		}
	}

	/// <summary>
	///       Gets the normal.
	///       </summary>
	public Vector3F Normal
	{
		get
		{
			Vector3F result = Vector3F.Cross(Vertex1 - Vertex0, Vertex2 - Vertex0);
			if (!result.TryNormalize())
			{
				result = Vector3F.UnitY;
			}
			return result;
		}
	}

	/// <summary>
	///       Gets or sets the vertex at the specified index.
	///       </summary>
	/// <param name="index">The index of the triangle point.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is out of range.
	///       </exception>
	public Vector3F this[int index]
	{
		get
		{
			return index switch
			{
				0 => Vertex0, 
				1 => Vertex1, 
				2 => Vertex2, 
				_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(549)), 
			};
		}
		set
		{
			switch (index)
			{
			case 0:
				Vertex0 = value;
				break;
			case 1:
				Vertex1 = value;
				break;
			case 2:
				Vertex2 = value;
				break;
			default:
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(549));
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.Triangle" /> from three points.
	///       </summary>
	/// <param name="vertex0">The first vertex.</param>
	/// <param name="vertex1">The second vertex.</param>
	/// <param name="vertex2">The third vertex.</param>
	public Triangle(Vector3F vertex0, Vector3F vertex1, Vector3F vertex2)
	{
		Vertex0 = vertex0;
		Vertex1 = vertex1;
		Vertex2 = vertex2;
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.Triangle" /> from a <see cref="T:DigitalRune.Geometry.Shapes.TriangleShape" />.
	///       </summary>
	/// <param name="triangleShape">
	///       The <see cref="T:DigitalRune.Geometry.Shapes.TriangleShape" /> from which vertices are copied.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="triangleShape" /> is <see langword="null" />.
	///       </exception>
	public Triangle(TriangleShape triangleShape)
	{
		if (triangleShape == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12416));
		}
		Vertex0 = triangleShape.Vertex0;
		Vertex1 = triangleShape.Vertex1;
		Vertex2 = triangleShape.Vertex2;
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
		if (!(obj is Triangle triangle))
		{
			return false;
		}
		if (Vertex0 == triangle.Vertex0 && Vertex1 == triangle.Vertex1)
		{
			return Vertex2 == triangle.Vertex2;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified <see cref="T:DigitalRune.Geometry.Shapes.Triangle" /> is equal to the current 
	///       <see cref="T:DigitalRune.Geometry.Shapes.Triangle" />.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(Triangle other)
	{
		if (Vertex0 == other.Vertex0 && Vertex1 == other.Vertex1)
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
						__ldtoken(Triangle.Equals(Triangle));
					}
					return Vertex2 == other.Vertex2;
				}
			}
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.Triangle" />s are equal.
	///       </summary>
	/// <param name="triangle1">The first <see cref="T:DigitalRune.Geometry.Shapes.Triangle" />.</param>
	/// <param name="triangle2">The second <see cref="T:DigitalRune.Geometry.Shapes.Triangle" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.Triangle" />s are equal; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator ==(Triangle triangle1, Triangle triangle2)
	{
		if (triangle1.Vertex0 == triangle2.Vertex0)
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
			if (1 == 0)
			{
				__ldtoken(Triangle.op_Equality(Triangle, Triangle));
			}
			if (triangle1.Vertex1 == triangle2.Vertex1)
			{
				return triangle1.Vertex2 == triangle2.Vertex2;
			}
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.Triangle" />s are different.
	///       </summary>
	/// <param name="triangle1">The first <see cref="T:DigitalRune.Geometry.Shapes.Triangle" />.</param>
	/// <param name="triangle2">The second <see cref="T:DigitalRune.Geometry.Shapes.Triangle" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.Triangle" />s are different; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator !=(Triangle triangle1, Triangle triangle2)
	{
		if (!(triangle1.Vertex0 != triangle2.Vertex0) && !(triangle1.Vertex1 != triangle2.Vertex1))
		{
			return triangle1.Vertex2 != triangle2.Vertex2;
		}
		return true;
	}

	/// <summary>
	///       Computes the axis-aligned bounding box (AABB) for this triangle positioned in world space
	///       using the given <see cref="T:DigitalRune.Geometry.Pose" />.
	///       </summary>
	/// <param name="pose">
	///       The <see cref="T:DigitalRune.Geometry.Pose" /> of the shape. This pose defines how the shape should be positioned in
	///       world space.
	///       </param>
	/// <returns>
	///       The AABB of the shape positioned in world space.
	///       </returns>
	public Aabb GetAabb(Pose pose)
	{
		Vector3F vector = pose.ToWorldPosition(Vertex0);
		Vector3F vector2 = pose.ToWorldPosition(Vertex1);
		Vector3F vector3 = pose.ToWorldPosition(Vertex2);
		Vector3F minimum = Vector3F.Min(vector, Vector3F.Min(vector2, vector3));
		Vector3F maximum = Vector3F.Max(vector, Vector3F.Max(vector2, vector3));
		return new Aabb(minimum, maximum);
	}

	/// <summary>
	///       Returns a hash code for this instance.
	///       </summary>
	/// <returns>
	///       A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
	///       </returns>
	public override int GetHashCode()
	{
		int hashCode = Vertex0.GetHashCode();
		hashCode = (hashCode * 397) ^ Vertex1.GetHashCode();
		return (hashCode * 397) ^ Vertex2.GetHashCode();
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12317), Vertex0, Vertex1, Vertex2);
	}
}
