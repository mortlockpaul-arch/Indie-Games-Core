using System;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Defines an axis-aligned bounding box (AABB).
///       </summary>
[Serializable]
public struct Aabb : IEquatable<Aabb>
{
	/// <summary>
	///       The minimum position.
	///       </summary>
	public Vector3F Minimum;

	/// <summary>
	///       The maximum position.
	///       </summary>
	public Vector3F Maximum;

	/// <summary>
	///       Gets the center.
	///       </summary>
	public Vector3F Center => (Minimum + Maximum) / 2f;

	/// <summary>
	///       Gets the extent vector (<see cref="F:DigitalRune.Geometry.Shapes.Aabb.Maximum" /> - <see cref="F:DigitalRune.Geometry.Shapes.Aabb.Minimum" />).
	///       </summary>
	public Vector3F Extent => Maximum - Minimum;

	/// <summary>
	///       Gets the volume of this axis-aligned bounding box.
	///       </summary>
	public float Volume => (Maximum.X - Minimum.X) * (Maximum.Y - Minimum.Y) * (Maximum.Z - Minimum.Z);

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.Aabb" />.
	///       </summary>
	/// <param name="minimum">The minimum.</param>
	/// <param name="maximum">The maximum.</param>
	public Aabb(Vector3F minimum, Vector3F maximum)
	{
		Minimum = minimum;
		Maximum = maximum;
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
		if (!(obj is Aabb aabb))
		{
			return false;
		}
		if (Minimum == aabb.Minimum)
		{
			return Maximum == aabb.Maximum;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified <see cref="T:DigitalRune.Geometry.Shapes.Aabb" /> is equal to the current 
	///       <see cref="T:DigitalRune.Geometry.Shapes.Aabb" />.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(Aabb other)
	{
		if (Minimum == other.Minimum)
		{
			return Maximum == other.Maximum;
		}
		return false;
	}

	/// <summary>
	///       Determines whether two AABBs are equal (regarding the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="first">The first AABB.</param>
	/// <param name="second">The second AABB.</param>
	/// <returns>
	///   <see langword="true" /> if the AABBs are equal (within the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />); otherwise, <see langword="false" />.
	///       </returns>
	public static bool AreNumericallyEqual(Aabb first, Aabb second)
	{
		if (Vector3F.AreNumericallyEqual(first.Minimum, second.Minimum))
		{
			return Vector3F.AreNumericallyEqual(first.Maximum, second.Maximum);
		}
		return false;
	}

	/// <summary>
	///       Determines whether two AABBs are equal (regarding the given tolerance).
	///       </summary>
	/// <param name="first">The first AABB.</param>
	/// <param name="second">The second AABB.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if the AABBs are equal (within the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />); otherwise, <see langword="false" />.
	///       </returns>
	public static bool AreNumericallyEqual(Aabb first, Aabb second, float epsilon)
	{
		if (Vector3F.AreNumericallyEqual(first.Minimum, second.Minimum, epsilon))
		{
			return Vector3F.AreNumericallyEqual(first.Maximum, second.Maximum, epsilon);
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.Aabb" />s are equal.
	///       </summary>
	/// <param name="aabbA">The first <see cref="T:DigitalRune.Geometry.Shapes.Aabb" />.</param>
	/// <param name="aabbB">The second <see cref="T:DigitalRune.Geometry.Shapes.Aabb" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.Aabb" />s are equal; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator ==(Aabb aabbA, Aabb aabbB)
	{
		if (aabbA.Minimum == aabbB.Minimum)
		{
			return aabbA.Maximum == aabbB.Maximum;
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.Aabb" />s are different.
	///       </summary>
	/// <param name="aabbA">The first <see cref="T:DigitalRune.Geometry.Shapes.Aabb" />.</param>
	/// <param name="aabbB">The second <see cref="T:DigitalRune.Geometry.Shapes.Aabb" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.Aabb" />s are different; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator !=(Aabb aabbA, Aabb aabbB)
	{
		if (!(aabbA.Minimum != aabbB.Minimum))
		{
			return aabbA.Maximum != aabbB.Maximum;
		}
		return true;
	}

	/// <summary>
	///       Determines whether the current AABB contains a given AABB.
	///       </summary>
	/// <param name="other">The other AABB.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="other" /> is fully contained in this AABB; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public bool Contains(Aabb other)
	{
		if (Minimum.X <= other.Minimum.X && Minimum.Y <= other.Minimum.Y && Minimum.Z <= other.Minimum.Z && Maximum.X >= other.Maximum.X && Maximum.Y >= other.Maximum.Y)
		{
			return Maximum.Z >= other.Maximum.Z;
		}
		return false;
	}

	/// <summary>
	///       Computes the world space AABB for a scaled, rotated and translated AABB.
	///       </summary>
	/// <param name="scale">The scale of the AABB.</param>
	/// <param name="pose">
	///       The <see cref="T:DigitalRune.Geometry.Pose" /> that defines the rotation and translation. This pose defines how
	///       this <see cref="T:DigitalRune.Geometry.Shapes.Aabb" /> should be positioned in world space.
	///       </param>
	/// <returns>
	///       The world space AABB that encloses this scaled, rotated and translated local space AABB.
	///       </returns>
	public Aabb GetAabb(Vector3F scale, Pose pose)
	{
		Aabb aabb = this;
		aabb.Scale(scale);
		return aabb.GetAabb(pose);
	}

	/// <summary>
	///       Computes the world space AABB for a rotated and translated AABB.
	///       </summary>
	/// <param name="pose">
	///       The <see cref="T:DigitalRune.Geometry.Pose" /> that defines the rotation and translation. This pose defines how 
	///       this <see cref="T:DigitalRune.Geometry.Shapes.Aabb" /> should be positioned in world space.
	///       </param>
	/// <returns>
	///       The world space AABB that encloses this rotated and translated local space AABB.
	///       </returns>
	public Aabb GetAabb(Pose pose)
	{
		if (pose == Pose.Identity)
		{
			return new Aabb(Minimum, Maximum);
		}
		Vector3F vector = (Maximum - Minimum) / 2f;
		Vector3F localPosition = (Minimum + Maximum) / 2f;
		Vector3F vector3F = pose.ToWorldPosition(localPosition);
		Matrix33F orientation = pose.Orientation;
		Vector3F row = orientation.GetRow(0);
		Vector3F row2 = orientation.GetRow(1);
		Vector3F row3 = orientation.GetRow(2);
		row = Vector3F.Absolute(row);
		row2 = Vector3F.Absolute(row2);
		row3 = Vector3F.Absolute(row3);
		Vector3F vector3F2 = new Vector3F(Vector3F.Dot(vector, row), Vector3F.Dot(vector, row2), Vector3F.Dot(vector, row3));
		return new Aabb(vector3F - vector3F2, vector3F + vector3F2);
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
		int hashCode = Minimum.GetHashCode();
		return (hashCode * 397) ^ Maximum.GetHashCode();
	}

	/// <summary>
	///       Grows the AABB so that it includes the given point.
	///       </summary>
	/// <param name="point">The point to include.</param>
	public void Grow(Vector3F point)
	{
		Minimum = Vector3F.Min(Minimum, point);
		Maximum = Vector3F.Max(Maximum, point);
	}

	/// <summary>
	///       Grows the AABB so that it includes the given AABB.
	///       </summary>
	/// <param name="aabb">The AABB to include.</param>
	public void Grow(Aabb aabb)
	{
		Minimum = Vector3F.Min(Minimum, aabb.Minimum);
		Maximum = Vector3F.Max(Maximum, aabb.Maximum);
	}

	/// <summary>
	///       Grows the AABB so that it includes the AABB of the given geometric object.
	///       </summary>
	/// <param name="geometricObject">The geometric object to include.</param>
	public void Grow(IGeometricObject geometricObject)
	{
		Aabb aabb = geometricObject.Aabb;
		Minimum = Vector3F.Min(Minimum, aabb.Minimum);
		Maximum = Vector3F.Max(Maximum, aabb.Maximum);
	}

	/// <summary>
	///       Merges the specified AABBs.
	///       </summary>
	/// <param name="first">The first AABB.</param>
	/// <param name="second">The second AABB.</param>
	/// <returns>The AABB that includes both child AABBs.</returns>
	public static Aabb Merge(Aabb first, Aabb second)
	{
		first.Grow(second);
		return first;
	}

	/// <summary>
	///       Scales the AABB.
	///       </summary>
	/// <param name="scale">
	///       The scale factors for the dimensions x, y and z.
	///       The scale factors can be negative to mirror the AABB.
	///       </param>
	public void Scale(Vector3F scale)
	{
		Vector3F minimum = Minimum;
		Vector3F maximum = Maximum;
		if (scale.X >= 0f)
		{
			Minimum.X = minimum.X * scale.X;
			Maximum.X = maximum.X * scale.X;
		}
		else
		{
			Minimum.X = maximum.X * scale.X;
			Maximum.X = minimum.X * scale.X;
		}
		if (scale.Y >= 0f)
		{
			Minimum.Y = minimum.Y * scale.Y;
			Maximum.Y = maximum.Y * scale.Y;
		}
		else
		{
			Minimum.Y = maximum.Y * scale.Y;
			Maximum.Y = minimum.Y * scale.Y;
		}
		if (scale.Z >= 0f)
		{
			Minimum.Z = minimum.Z * scale.Z;
			Maximum.Z = maximum.Z * scale.Z;
		}
		else
		{
			Minimum.Z = maximum.Z * scale.Z;
			Maximum.Z = minimum.Z * scale.Z;
		}
	}

	/// <summary>
	///       Translates the AABB.
	///       </summary>
	/// <param name="translation">The displacement vector.</param>
	public void Translate(Vector3F translation)
	{
		Minimum += translation;
		Maximum += translation;
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12714), Minimum, Maximum);
	}
}
