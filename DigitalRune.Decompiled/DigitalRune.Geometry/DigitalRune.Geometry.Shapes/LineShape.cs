using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents an infinite line.
///       </summary>
[Serializable]
public class LineShape : Shape
{
	private Vector3F _pointOnLine;

	private Vector3F _direction;

	/// <summary>
	///       Gets or sets a point on the line.
	///       </summary>
	public Vector3F PointOnLine
	{
		get
		{
			return _pointOnLine;
		}
		set
		{
			if (!(_pointOnLine != value))
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
				if (1 == 0)
				{
					__ldtoken(LineShape.set_PointOnLine(Vector3F));
				}
				_pointOnLine = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the direction.
	///       </summary>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> is not normalized.
	///       </exception>
	public Vector3F Direction
	{
		get
		{
			return _direction;
		}
		set
		{
			if (!value.IsNumericallyNormalized)
			{
				throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12522));
			}
			if (!(_direction != value))
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
					__ldtoken(LineShape.set_Direction(Vector3F));
				}
				_direction = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint => _pointOnLine;

	/// <summary>
	///       Gets or sets the length of the mesh that represents a <see cref="T:DigitalRune.Geometry.Shapes.LineShape" />.
	///       </summary>
	public static float MeshSize { get; set; }

	static LineShape()
	{
		MeshSize = 1000f;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.LineShape" /> class.
	///       </summary>
	public LineShape()
		: this(Vector3F.Zero, Vector3F.UnitX)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.LineShape" /> class from a point and direction.
	///       </summary>
	/// <param name="pointOnLine">A point on the line.</param>
	/// <param name="direction">The direction.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="direction" /> is not normalized.
	///       </exception>
	public LineShape(Vector3F pointOnLine, Vector3F direction)
	{
		if (!direction.IsNumericallyNormalized)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12522), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12686));
		}
		_pointOnLine = pointOnLine;
		_direction = direction;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.LineShape" /> class from a <see cref="T:DigitalRune.Geometry.Shapes.Line" />.
	///       </summary>
	/// <param name="line">The line structure from which properties are copied.</param>
	/// <exception cref="T:System.ArgumentException">
	///       The line direction is not normalized.
	///       </exception>
	public LineShape(Line line)
	{
		if (!line.Direction.IsNumericallyNormalized)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12522), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12705));
		}
		_pointOnLine = line.PointOnLine;
		_direction = line.Direction;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new LineShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		LineShape lineShape = (LineShape)sourceShape;
		_pointOnLine = lineShape.PointOnLine;
		_direction = lineShape.Direction;
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
		Vector3F vector3F = pose.ToWorldDirection(_direction * scale);
		Vector3F vector3F2 = pose.ToWorldPosition(_pointOnLine * scale);
		Vector3F minimum = new Vector3F(float.NegativeInfinity);
		Vector3F maximum = new Vector3F(float.PositiveInfinity);
		if (Numeric.IsZero(vector3F.X))
		{
			minimum.X = vector3F2.X;
			maximum.X = vector3F2.X;
		}
		if (Numeric.IsZero(vector3F.Y))
		{
			minimum.Y = vector3F2.Y;
			maximum.Y = vector3F2.Y;
		}
		if (Numeric.IsZero(vector3F.Z))
		{
			minimum.Z = vector3F2.Z;
			maximum.Z = vector3F2.Z;
		}
		return new Aabb(minimum, maximum);
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
		Vector3F vector3F = PointOnLine - Direction * (MeshSize / 2f);
		Vector3F vertex = PointOnLine + Direction * (MeshSize / 2f);
		TriangleMesh triangleMesh = new TriangleMesh();
		triangleMesh.Add(new Triangle
		{
			Vertex0 = vector3F,
			Vertex1 = vector3F,
			Vertex2 = vertex
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
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12599), _pointOnLine, _direction);
	}
}
