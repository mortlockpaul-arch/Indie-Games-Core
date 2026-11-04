using System;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Defines a line.
///       </summary>
[Serializable]
public struct Line : IEquatable<Line>
{
	/// <summary>
	///       A point on the line.
	///       </summary>
	public Vector3F PointOnLine;

	/// <summary>
	///       The normalized direction vector.
	///       </summary>
	public Vector3F Direction;

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.Line" /> from a point and a direction.
	///       </summary>
	/// <param name="pointOnLine">A point on the line.</param>
	/// <param name="direction">The direction. (Must be normalized.)</param>
	public Line(Vector3F pointOnLine, Vector3F direction)
	{
		PointOnLine = pointOnLine;
		Direction = direction;
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Geometry.Shapes.Line" /> from a <see cref="T:DigitalRune.Geometry.Shapes.LineShape" />.
	///       </summary>
	/// <param name="lineShape">The <see cref="T:DigitalRune.Geometry.Shapes.LineShape" /> from which properties are copied.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="lineShape" /> is <see langword="null" />.
	///       </exception>
	public Line(LineShape lineShape)
	{
		if (lineShape == null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Line._002Ector(LineShape));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5267));
				}
			}
		}
		PointOnLine = lineShape.PointOnLine;
		Direction = lineShape.Direction;
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
		if (!(obj is Line line))
		{
			return false;
		}
		if (PointOnLine == line.PointOnLine)
		{
			return Direction == line.Direction;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified <see cref="T:DigitalRune.Geometry.Shapes.Line" /> is equal to the current 
	///       <see cref="T:DigitalRune.Geometry.Shapes.Line" />.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(Line other)
	{
		if (PointOnLine == other.PointOnLine)
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
						__ldtoken(Line.Equals(Line));
					}
					return Direction == other.Direction;
				}
			}
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.Line" />s are equal.
	///       </summary>
	/// <param name="line1">The first <see cref="T:DigitalRune.Geometry.Shapes.Line" />.</param>
	/// <param name="line2">The second <see cref="T:DigitalRune.Geometry.Shapes.Line" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.Line" />s are equal; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator ==(Line line1, Line line2)
	{
		if (line1.PointOnLine == line2.PointOnLine)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Line.op_Equality(Line, Line));
					}
					return line1.Direction == line2.Direction;
				}
			}
		}
		return false;
	}

	/// <summary>
	///       Tests if two <see cref="T:DigitalRune.Geometry.Shapes.Line" />s are different.
	///       </summary>
	/// <param name="line1">The first <see cref="T:DigitalRune.Geometry.Shapes.Line" />.</param>
	/// <param name="line2">The second <see cref="T:DigitalRune.Geometry.Shapes.Line" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Shapes.Line" />s are different; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool operator !=(Line line1, Line line2)
	{
		if (!(line1.PointOnLine != line2.PointOnLine))
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
						__ldtoken(Line.op_Inequality(Line, Line));
					}
					return line1.Direction != line2.Direction;
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
		int hashCode = PointOnLine.GetHashCode();
		return (hashCode * 397) ^ Direction.GetHashCode();
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5042), PointOnLine, Direction);
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
				__ldtoken(Line.c22463d4a85fcdaa7d4ffbd8669084d72(ref Vector3F));
			}
			if (ceabb47d7d959545d89c4300eabbdd7c2.Y == ceabb47d7d959545d89c4300eabbdd7c2.Z)
			{
				PointOnLine *= ceabb47d7d959545d89c4300eabbdd7c2.X;
				return;
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
		throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5119));
	}

	internal void c77d310c5ae14ed7e7ed360995b1867bb(ref Pose cbc1b7e0b011ad6dcdb8beb392ec7a646)
	{
		PointOnLine = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToWorldPosition(PointOnLine);
		Direction = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToWorldDirection(Direction);
	}

	internal void c0d5e70cac6e675cb3df8faaefbfd7052(ref Pose cbc1b7e0b011ad6dcdb8beb392ec7a646)
	{
		PointOnLine = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToLocalPosition(PointOnLine);
		Direction = cbc1b7e0b011ad6dcdb8beb392ec7a646.ToLocalDirection(Direction);
	}
}
