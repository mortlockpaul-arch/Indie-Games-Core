using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a plane.
///       </summary>
[Serializable]
public class PlaneShape : Shape
{
	private Vector3F _normal;

	private float _distanceFromOrigin;

	/// <summary>
	///       Gets or sets the outward pointing normal vector.
	///       </summary>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> is not normalized.</exception>
	public Vector3F Normal
	{
		get
		{
			return _normal;
		}
		set
		{
			if (!value.IsNumericallyNormalized)
			{
				throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5286));
			}
			if (!(_normal != value))
			{
				return;
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(PlaneShape.set_Normal(Vector3F));
				}
				_normal = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the distance of the plane from the origin (also known as the "plane constant").
	///       </summary>
	public float DistanceFromOrigin
	{
		get
		{
			return _distanceFromOrigin;
		}
		set
		{
			if (_distanceFromOrigin != value)
			{
				_distanceFromOrigin = value;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint => _normal * _distanceFromOrigin;

	/// <summary>
	///       Gets or sets the size of the mesh that represents a <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" />.
	///       </summary>
	public static float MeshSize { get; set; }

	static PlaneShape()
	{
		MeshSize = 100f;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" /> class.
	///       </summary>
	public PlaneShape()
		: this(Vector3F.UnitY, 0f)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" /> class from a normal vector and
	///       the distance from the origin.
	///       </summary>
	/// <param name="normal">
	///       The outward pointing normal vector of the plane. Must be normalized.
	///       </param>
	/// <param name="distanceFromOrigin">The distance from the origin.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="normal" /> is not normalized.
	///       </exception>
	public PlaneShape(Vector3F normal, float distanceFromOrigin)
	{
		if (!normal.IsNumericallyNormalized)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(PlaneShape._002Ector(Vector3F, float));
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5286), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5456));
				}
			}
		}
		_normal = normal;
		_distanceFromOrigin = distanceFromOrigin;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" /> class from three points.
	///       </summary>
	/// <param name="point0">A point on the plane.</param>
	/// <param name="point1">A point on the plane.</param>
	/// <param name="point2">A point on the plane.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="point0" />, <paramref name="point1" />, and <paramref name="point2" /> do not 
	///       form a valid triangle.
	///       </exception>
	public PlaneShape(Vector3F point0, Vector3F point1, Vector3F point2)
	{
		if (Vector3F.AreNumericallyEqual(point0, point1) || Vector3F.AreNumericallyEqual(point0, point2) || Vector3F.AreNumericallyEqual(point1, point2))
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5469));
		}
		_normal = Vector3F.Cross(point1 - point0, point2 - point0);
		if (!_normal.TryNormalize())
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
						__ldtoken(PlaneShape._002Ector(Vector3F, Vector3F, Vector3F));
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5469));
				}
			}
		}
		_distanceFromOrigin = Vector3F.Dot(point0, _normal);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" /> class.
	///       </summary>
	/// <param name="normal">
	///       The outward pointing normal vector of the plane. Must be normalized.</param>
	/// <param name="pointOnPlane">A point on the plane.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="normal" /> is not normalized.
	///       </exception>
	public PlaneShape(Vector3F normal, Vector3F pointOnPlane)
	{
		if (!normal.IsNumericallyNormalized)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5286), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5456));
		}
		_normal = normal;
		_distanceFromOrigin = Vector3F.Dot(pointOnPlane, _normal);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" /> class.
	///       </summary>
	/// <param name="plane">
	///       The plane from which normal vector and distance from origin are copied.
	///       </param>
	/// <exception cref="T:System.ArgumentException">The plane normal is not normalized.</exception>
	public PlaneShape(Plane plane)
	{
		if (!plane.Normal.IsNumericallyNormalized)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5286), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5550));
		}
		_normal = plane.Normal;
		_distanceFromOrigin = plane.DistanceFromOrigin;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new PlaneShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		PlaneShape planeShape = (PlaneShape)sourceShape;
		_normal = planeShape.Normal;
		_distanceFromOrigin = planeShape.DistanceFromOrigin;
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
		if (scale.X != scale.Y || scale.Y != scale.Z)
		{
			return new Aabb(new Vector3F(float.NegativeInfinity), new Vector3F(float.PositiveInfinity));
		}
		Vector3F vector3F = pose.ToWorldDirection(_normal);
		if (scale.X < 0f)
		{
			vector3F = -vector3F;
		}
		float num = _distanceFromOrigin * scale.X;
		if (Vector3F.AreNumericallyEqual(vector3F, Vector3F.UnitX))
		{
			Vector3F minimum = new Vector3F(float.NegativeInfinity);
			Vector3F maximum = new Vector3F(pose.Position.X + num, float.PositiveInfinity, float.PositiveInfinity);
			return new Aabb(minimum, maximum);
		}
		if (Vector3F.AreNumericallyEqual(vector3F, Vector3F.UnitY))
		{
			Vector3F minimum2 = new Vector3F(float.NegativeInfinity);
			Vector3F maximum2 = new Vector3F(float.PositiveInfinity, pose.Position.Y + num, float.PositiveInfinity);
			return new Aabb(minimum2, maximum2);
		}
		if (Vector3F.AreNumericallyEqual(vector3F, Vector3F.UnitZ))
		{
			Vector3F minimum3 = new Vector3F(float.NegativeInfinity);
			Vector3F maximum3 = new Vector3F(float.PositiveInfinity, float.PositiveInfinity, pose.Position.Z + num);
			return new Aabb(minimum3, maximum3);
		}
		if (Vector3F.AreNumericallyEqual(vector3F, -Vector3F.UnitX))
		{
			Vector3F minimum4 = new Vector3F(pose.Position.X - num, float.NegativeInfinity, float.NegativeInfinity);
			Vector3F maximum4 = new Vector3F(float.PositiveInfinity);
			return new Aabb(minimum4, maximum4);
		}
		if (Vector3F.AreNumericallyEqual(vector3F, -Vector3F.UnitY))
		{
			Vector3F minimum5 = new Vector3F(float.NegativeInfinity, pose.Position.Y - num, float.NegativeInfinity);
			Vector3F maximum5 = new Vector3F(float.PositiveInfinity);
			return new Aabb(minimum5, maximum5);
		}
		if (Vector3F.AreNumericallyEqual(vector3F, -Vector3F.UnitZ))
		{
			Vector3F minimum6 = new Vector3F(float.NegativeInfinity, float.NegativeInfinity, pose.Position.Z - num);
			Vector3F maximum6 = new Vector3F(float.PositiveInfinity);
			return new Aabb(minimum6, maximum6);
		}
		return new Aabb(new Vector3F(float.NegativeInfinity), new Vector3F(float.PositiveInfinity));
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <param name="relativeError">Not used.</param>
	/// <param name="iterationLimit">Not used</param>
	/// <returns>Positive infinity (<see cref="F:System.Single.PositiveInfinity" />)</returns>
	public override float GetVolume(float relativeError, int iterationLimit)
	{
		return float.PositiveInfinity;
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5359), _normal, _distanceFromOrigin);
	}

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected override TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit)
	{
		Vector3F vector3F = Normal * DistanceFromOrigin;
		Vector3F orthonormal = Normal.Orthonormal1;
		Vector3F orthonormal2 = Normal.Orthonormal2;
		TriangleMesh triangleMesh = new TriangleMesh();
		triangleMesh.Add(new Triangle
		{
			Vertex0 = vector3F,
			Vertex1 = vector3F + orthonormal * MeshSize / 2f - orthonormal2 * MeshSize / 2f,
			Vertex2 = vector3F + orthonormal * MeshSize / 2f + orthonormal2 * MeshSize / 2f
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = vector3F,
			Vertex1 = vector3F + orthonormal * MeshSize / 2f + orthonormal2 * MeshSize / 2f,
			Vertex2 = vector3F - orthonormal * MeshSize / 2f + orthonormal2 * MeshSize / 2f
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = vector3F,
			Vertex1 = vector3F - orthonormal * MeshSize / 2f + orthonormal2 * MeshSize / 2f,
			Vertex2 = vector3F - orthonormal * MeshSize / 2f - orthonormal2 * MeshSize / 2f
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = vector3F,
			Vertex1 = vector3F - orthonormal * MeshSize / 2f - orthonormal2 * MeshSize / 2f,
			Vertex2 = vector3F + orthonormal * MeshSize / 2f - orthonormal2 * MeshSize / 2f
		}, weldVerticesBruteForce: true);
		return triangleMesh;
	}
}
