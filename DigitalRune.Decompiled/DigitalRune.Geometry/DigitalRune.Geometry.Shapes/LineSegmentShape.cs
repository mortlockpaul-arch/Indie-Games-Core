using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a line segment.
///       </summary>
[Serializable]
public class LineSegmentShape : ConvexShape
{
	private Vector3F _start;

	private Vector3F _end;

	/// <summary>
	///       Gets an inner point (center of line segment).
	///       </summary>
	public override Vector3F InnerPoint => (_start + _end) / 2f;

	/// <summary>
	///       Gets or sets the start point.
	///       </summary>
	public Vector3F Start
	{
		get
		{
			return _start;
		}
		set
		{
			if (_start != value)
			{
				_start = value;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the end point.
	///       </summary>
	public Vector3F End
	{
		get
		{
			return _end;
		}
		set
		{
			if (!(_end != value))
			{
				return;
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(LineSegmentShape.set_End(Vector3F));
				}
				_end = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets the length.
	///       </summary>
	public float Length => (_end - _start).Length;

	/// <summary>
	///       Gets the squared length.
	///       </summary>
	public float LengthSquared => (_end - _start).LengthSquared;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.LineSegmentShape" /> class.
	///       </summary>
	public LineSegmentShape()
		: this(Vector3F.Zero, Vector3F.Zero)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.LineSegmentShape" /> class from two points.
	///       </summary>
	/// <param name="start">The start point.</param>
	/// <param name="end">The end point.</param>
	public LineSegmentShape(Vector3F start, Vector3F end)
	{
		_start = start;
		_end = end;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.LineSegmentShape" /> class from a 
	///       <see cref="T:DigitalRune.Geometry.Shapes.LineSegment" />.
	///       </summary>
	/// <param name="lineSegment">
	///       The line segment from which properties are copied.
	///       </param>
	public LineSegmentShape(LineSegment lineSegment)
	{
		Start = lineSegment.Start;
		End = lineSegment.End;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new LineSegmentShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		LineSegmentShape lineSegmentShape = (LineSegmentShape)sourceShape;
		_start = lineSegmentShape.Start;
		_end = lineSegmentShape.End;
	}

	/// <summary>
	///       Computes the axis-aligned bounding box (AABB) for this shape positioned in world space using
	///       the given scale and <see cref="T:DigitalRune.Geometry.Pose" />.
	///       </summary>
	/// <param name="scale">
	///       The scale factor by which the shape should be scaled. The scaling is applied in the
	///       shape's local space before the pose is applied.
	///       </param>
	/// <param name="pose">
	///       The <see cref="T:DigitalRune.Geometry.Pose" /> of the shape. This pose defines how the shape should be positioned
	///       in world space.
	///       </param>
	/// <returns>
	///       The AABB of the shape positioned in world space.
	///       </returns>
	public override Aabb GetAabb(Vector3F scale, Pose pose)
	{
		Vector3F vector = pose.ToWorldPosition(_start * scale);
		Vector3F vector2 = pose.ToWorldPosition(_end * scale);
		Vector3F minimum = Vector3F.Min(vector, vector2);
		Vector3F maximum = Vector3F.Max(vector, vector2);
		return new Aabb(minimum, maximum);
	}

	/// <summary>
	///       Gets a support point for a given direction.
	///       </summary>
	/// <param name="direction">
	///       The direction for which to get the support point. The vector does not need to be normalized.
	///       The result is undefined if the vector is a zero vector.
	///       </param>
	/// <returns>
	///       A support point regarding the given direction.
	///       </returns>
	public override Vector3F GetSupportPoint(Vector3F direction)
	{
		if (Vector3F.Dot(_start, direction) > Vector3F.Dot(_end, direction))
		{
			return _start;
		}
		return _end;
	}

	/// <summary>
	///       Gets a support point for a given normalized direction vector.
	///       </summary>
	/// <param name="directionNormalized">
	///       The normalized direction vector for which to get the support point.
	///       </param>
	/// <returns>
	///       A support point regarding the given direction.
	///       </returns>
	public override Vector3F GetSupportPointNormalized(Vector3F directionNormalized)
	{
		if (Vector3F.Dot(_start, directionNormalized) > Vector3F.Dot(_end, directionNormalized))
		{
			return _start;
		}
		return _end;
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <param name="relativeError">Not used.</param>
	/// <param name="iterationLimit">Not used</param>
	/// <returns>0</returns>
	public override float GetVolume(float relativeError, int iterationLimit)
	{
		return 0f;
	}

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected override TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit)
	{
		TriangleMesh triangleMesh = new TriangleMesh();
		triangleMesh.Add(new Triangle
		{
			Vertex0 = Start,
			Vertex1 = Start,
			Vertex2 = End
		}, weldVerticesBruteForce: true, Numeric.EpsilonF, removeDegenerateTriangles: false);
		return triangleMesh;
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4572), _start, _end);
	}
}
