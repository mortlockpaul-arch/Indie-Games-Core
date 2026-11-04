using System;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Defines a line segment.
///       </summary>
[Serializable]
public struct LineSegment : IEquatable<LineSegment>
{
	/// <summary>
	///       The start point.
	///       </summary>
	public Vector3F Start;

	/// <summary>
	///       The end point.
	///       </summary>
	public Vector3F End;

	/// <summary>
	///       Gets the length.
	///       </summary>
	public float Length => (End - Start).Length;

	/// <summary>
	///       Gets the squared length.
	///       </summary>
	public float LengthSquared => (End - Start).LengthSquared;

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" /> from two points.
	///       </summary>
	/// <param name="start">The start point.</param>
	/// <param name="end">The end point.</param>
	public LineSegment(Vector3F start, Vector3F end)
	{
		Start = start;
		End = end;
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" /> from a 
	///       <see cref="T:DigitalRune.Geometry.Shapes.LineSegmentShape" />.
	///       </summary>
	/// <param name="lineSegmentShape">
	///       The line segment from which properties are copied.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="lineSegmentShape" /> is <see langword="null" />.
	///       </exception>
	public LineSegment(LineSegmentShape lineSegmentShape)
	{
		if (lineSegmentShape == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(LineSegment._002Ector(LineSegmentShape));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10532));
				}
			}
		}
		Start = lineSegmentShape.Start;
		End = lineSegmentShape.End;
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
		if (!(obj is LineSegment lineSegment))
		{
			return false;
		}
		if (Start == lineSegment.Start)
		{
			return End == lineSegment.End;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" /> is equal to the current 
	///       <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" />.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(LineSegment other)
	{
		if (Start == other.Start)
		{
			return End == other.End;
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" />s are equal.
	///       </summary>
	/// <param name="lineSegment1">The first <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" />.</param>
	/// <param name="lineSegment2">The second <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" />s are equal; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator ==(LineSegment lineSegment1, LineSegment lineSegment2)
	{
		if (lineSegment1.Start == lineSegment2.Start)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(LineSegment.op_Equality(LineSegment, LineSegment));
					}
					return lineSegment1.End == lineSegment2.End;
				}
			}
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" />s are different.
	///       </summary>
	/// <param name="lineSegment1">The first <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" />.</param>
	/// <param name="lineSegment2">The second <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" />s are different; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator !=(LineSegment lineSegment1, LineSegment lineSegment2)
	{
		if (!(lineSegment1.Start != lineSegment2.Start))
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
						__ldtoken(LineSegment.op_Inequality(LineSegment, LineSegment));
					}
					return lineSegment1.End != lineSegment2.End;
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
		int hashCode = Start.GetHashCode();
		return (hashCode * 397) ^ End.GetHashCode();
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10465), Start, End);
	}
}
