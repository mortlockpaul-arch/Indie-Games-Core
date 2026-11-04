using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a ray, which can be used for ray casting.
///       </summary>
[Serializable]
public class RayShape : ConvexShape
{
	private Vector3F _origin;

	private Vector3F _direction = Vector3F.UnitX;

	private float _length;

	private bool _stopsAtFirstHit;

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint => _origin + _direction * (_length / 2f);

	/// <summary>
	///       Gets or sets the origin of the ray.
	///       </summary>
	public Vector3F Origin
	{
		get
		{
			return _origin;
		}
		set
		{
			if (!(_origin != value))
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
					__ldtoken(RayShape.set_Origin(Vector3F));
				}
				_origin = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the direction of the ray.
	///       </summary>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> is not a normalized.
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
				throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13552));
			}
			if (_direction != value)
			{
				_direction = value;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the finite length.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is ≤ 0 or infinite.
	///       </exception>
	public float Length
	{
		get
		{
			return _length;
		}
		set
		{
			if (!(value <= 0f))
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
					__ldtoken(RayShape.set_Length(float));
				}
				if (!Numeric.IsZero(value))
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
					if (!float.IsInfinity(value))
					{
						if (_length == value)
						{
							return;
						}
						while (true)
						{
							switch (6)
							{
							case 0:
								continue;
							}
							_length = value;
							OnChanged(ShapeChangedEventArgs.Empty);
							return;
						}
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
			}
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13637));
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether the ray stops at the first (closest) object that was
	///       hit.
	///       </summary>
	public bool StopsAtFirstHit
	{
		get
		{
			return _stopsAtFirstHit;
		}
		set
		{
			if (_stopsAtFirstHit != value)
			{
				_stopsAtFirstHit = value;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> class.
	///       </summary>
	public RayShape()
		: this(Vector3F.Zero, Vector3F.UnitX, 100f)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Line" /> class with the given origin, 
	///       direction and length.
	///       </summary>
	/// <param name="origin">The origin.</param>
	/// <param name="direction">The direction.</param>
	/// <param name="length">The finite length.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="direction" /> is not normalized.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="length" /> is ≤ 0 or infinite.
	///       </exception>
	public RayShape(Vector3F origin, Vector3F direction, float length)
	{
		if (!direction.IsNumericallyNormalized)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13888));
		}
		if (!(length <= 0f))
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(RayShape._002Ector(Vector3F, Vector3F, float));
			}
			if (!Numeric.IsZero(length))
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				if (!float.IsInfinity(length))
				{
					_origin = origin;
					_direction = direction;
					_length = length;
					return;
				}
			}
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13963), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13637));
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> class from a <see cref="T:DigitalRune.Geometry.Shapes.Ray" />.
	///       </summary>
	/// <param name="ray">The ray.</param>
	/// <exception cref="T:System.ArgumentException">
	///       The direction of <paramref name="ray" /> is not normalized.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The length of <paramref name="ray" /> is ≤ 0 or infinite.
	///       </exception>
	public RayShape(Ray ray)
	{
		if (!ray.Direction.IsNumericallyNormalized)
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
						__ldtoken(RayShape._002Ector(Ray));
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13888), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13976));
				}
			}
		}
		if (!(ray.Length <= 0f) && !Numeric.IsZero(ray.Length))
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
			if (!float.IsInfinity(ray.Length))
			{
				_origin = ray.Origin;
				_direction = ray.Direction;
				_length = ray.Length;
				return;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13976), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13637));
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new RayShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		RayShape rayShape = (RayShape)sourceShape;
		_origin = rayShape.Origin;
		_direction = rayShape.Direction;
		_length = rayShape.Length;
		_stopsAtFirstHit = rayShape.StopsAtFirstHit;
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
		Vector3F vector3F = _origin * scale;
		Vector3F vector = pose.ToWorldPosition(vector3F);
		Vector3F vector2 = pose.ToWorldPosition(vector3F + scale * _direction * _length);
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
		Vector3F vector3F = _origin + _length * _direction;
		if (Vector3F.Dot(_origin, direction) > Vector3F.Dot(vector3F, direction))
		{
			return _origin;
		}
		return vector3F;
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
		Vector3F vector3F = _origin + _length * _direction;
		if (Vector3F.Dot(_origin, directionNormalized) > Vector3F.Dot(vector3F, directionNormalized))
		{
			return _origin;
		}
		return vector3F;
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
			Vertex0 = Origin,
			Vertex1 = Origin,
			Vertex2 = Origin + Direction * Length
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
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13746), _origin, _direction, _length, _stopsAtFirstHit);
	}
}
