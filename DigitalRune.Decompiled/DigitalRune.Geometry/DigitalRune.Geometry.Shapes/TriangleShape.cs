using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a triangle.
///       </summary>
[Serializable]
public class TriangleShape : ConvexShape
{
	private Vector3F _vertex0;

	private Vector3F _vertex1;

	private Vector3F _vertex2;

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint => (_vertex0 + _vertex1 + _vertex2) / 3f;

	/// <summary>
	///       Gets the normal.
	///       </summary>
	public Vector3F Normal
	{
		get
		{
			Vector3F result = Vector3F.Cross(_vertex1 - _vertex0, _vertex2 - _vertex0);
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
				0 => _vertex0, 
				1 => _vertex1, 
				2 => _vertex2, 
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
	///       Gets or sets the first vertex.
	///       </summary>
	public Vector3F Vertex0
	{
		get
		{
			return _vertex0;
		}
		set
		{
			if (!(_vertex0 != value))
			{
				return;
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(TriangleShape.set_Vertex0(Vector3F));
				}
				_vertex0 = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the second vertex.
	///       </summary>
	public Vector3F Vertex1
	{
		get
		{
			return _vertex1;
		}
		set
		{
			if (_vertex1 != value)
			{
				_vertex1 = value;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the third vertex.
	///       </summary>
	public Vector3F Vertex2
	{
		get
		{
			return _vertex2;
		}
		set
		{
			if (_vertex2 != value)
			{
				_vertex2 = value;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.TriangleShape" /> class.
	///       </summary>
	public TriangleShape()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.TriangleShape" /> class from the given vertices.
	///       </summary>
	/// <param name="vertex0">The first vertex.</param>
	/// <param name="vertex1">The second vertex.</param>
	/// <param name="vertex2">The third vertex.</param>
	public TriangleShape(Vector3F vertex0, Vector3F vertex1, Vector3F vertex2)
	{
		_vertex0 = vertex0;
		_vertex1 = vertex1;
		_vertex2 = vertex2;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.TriangleShape" /> class from a 
	///       <see cref="T:DigitalRune.Geometry.Shapes.Triangle" />.
	///       </summary>
	/// <param name="triangle">
	///       The <see cref="T:DigitalRune.Geometry.Shapes.Triangle" /> structure from which vertices are copied.
	///       </param>
	public TriangleShape(Triangle triangle)
	{
		_vertex0 = triangle.Vertex0;
		_vertex1 = triangle.Vertex1;
		_vertex2 = triangle.Vertex2;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new TriangleShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		TriangleShape triangleShape = (TriangleShape)sourceShape;
		_vertex0 = triangleShape.Vertex0;
		_vertex1 = triangleShape.Vertex1;
		_vertex2 = triangleShape.Vertex2;
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
		Vector3F vector = pose.ToWorldPosition(_vertex0 * scale);
		Vector3F vector2 = pose.ToWorldPosition(_vertex1 * scale);
		Vector3F vector3 = pose.ToWorldPosition(_vertex2 * scale);
		Vector3F minimum = Vector3F.Min(vector, Vector3F.Min(vector2, vector3));
		Vector3F maximum = Vector3F.Max(vector, Vector3F.Max(vector2, vector3));
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
		float num = Vector3F.Dot(direction, _vertex0);
		float num2 = Vector3F.Dot(direction, _vertex1);
		float num3 = Vector3F.Dot(direction, _vertex2);
		if (num >= num2 && num >= num3)
		{
			return _vertex0;
		}
		if (num2 >= num3)
		{
			return _vertex1;
		}
		return _vertex2;
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
		float num = Vector3F.Dot(directionNormalized, _vertex0);
		float num2 = Vector3F.Dot(directionNormalized, _vertex1);
		float num3 = Vector3F.Dot(directionNormalized, _vertex2);
		if (num >= num2 && num >= num3)
		{
			return _vertex0;
		}
		if (num2 >= num3)
		{
			return _vertex1;
		}
		return _vertex2;
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
		triangleMesh.Add(new Triangle(this), weldVerticesBruteForce: true);
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
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(560), _vertex0, _vertex1, _vertex2);
	}
}
