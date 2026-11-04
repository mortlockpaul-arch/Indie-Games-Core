using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a cone standing on the xz plane, pointing up in the +y direction.
///       </summary>
[Serializable]
public class ConeShape : ConvexShape
{
	private float? _sinθ;

	private float _height;

	private float _radius;

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint => new Vector3F(0f, _height / 2f, 0f);

	/// <summary>
	///       Gets or sets the height.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float Height
	{
		get
		{
			return _height;
		}
		set
		{
			if (value < 0f)
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
							__ldtoken(ConeShape.set_Height(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4649));
					}
				}
			}
			if (_height == value)
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
				_height = value;
				if (_height == 0f && _radius == 0f)
				{
					while (true)
					{
						switch (7)
						{
						case 0:
							continue;
						}
						break;
					}
					_sinθ = null;
				}
				else
				{
					_sinθ = _radius / (float)Math.Sqrt(_radius * _radius + _height * _height);
				}
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the radius.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float Radius
	{
		get
		{
			return _radius;
		}
		set
		{
			if (value < 0f)
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
							__ldtoken(ConeShape.set_Radius(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2713));
					}
				}
			}
			if (_radius == value)
			{
				return;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				_radius = value;
				if (_height == 0f)
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
					if (_radius == 0f)
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
						_sinθ = null;
						goto IL_00c2;
					}
				}
				_sinθ = _radius / (float)Math.Sqrt(_radius * _radius + _height * _height);
				goto IL_00c2;
				IL_00c2:
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.ConeShape" /> class.
	///       </summary>
	public ConeShape()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.ConeShape" /> class with the given radius and 
	///       height.
	///       </summary>
	/// <param name="radius">The radius.</param>
	/// <param name="height">The height.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="radius" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="height" /> is negative.
	///       </exception>
	public ConeShape(float radius, float height)
	{
		if (radius < 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2857), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2713));
		}
		if (height < 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4813), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4649));
		}
		_height = height;
		_radius = radius;
		if (_height == 0f)
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
				__ldtoken(ConeShape._002Ector(float, float));
			}
			if (_radius == 0f)
			{
				_sinθ = null;
				return;
			}
		}
		_sinθ = _radius / (float)Math.Sqrt(_radius * _radius + _height * _height);
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new ConeShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		ConeShape coneShape = (ConeShape)sourceShape;
		_radius = coneShape.Radius;
		_height = coneShape.Height;
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
		if (!_sinθ.HasValue)
		{
			return Vector3F.Zero;
		}
		if (directionNormalized.Y > _sinθ)
		{
			return new Vector3F(0f, _height, 0f);
		}
		Vector3F vector3F = new Vector3F(directionNormalized.X, 0f, directionNormalized.Z);
		float length = vector3F.Length;
		if (Numeric.IsZero(length))
		{
			return new Vector3F(_radius, 0f, 0f);
		}
		return _radius * vector3F / length;
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <returns>The volume of this shape.</returns>
	public float GetVolume()
	{
		float radius = Radius;
		return (float)Math.PI / 3f * radius * radius * Height;
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <param name="relativeError">Not used.</param>
	/// <param name="iterationLimit">Not used.</param>
	/// <returns>The volume of this shape.</returns>
	public override float GetVolume(float relativeError, int iterationLimit)
	{
		return GetVolume();
	}

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected override TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit)
	{
		float num = (float)Math.Acos((_radius - absoluteDistanceThreshold) / _radius) * 2f;
		int val = (int)Math.Ceiling((float)Math.PI * 2f / num);
		val = Math.Min(val, 2 << iterationLimit);
		num = (float)Math.PI * 2f / (float)val;
		Vector3F vector3F = new Vector3F(_radius, 0f, 0f);
		Vector3F vertex = new Vector3F(0f, _height, 0f);
		QuaternionF quaternionF = QuaternionF.CreateRotationY(num);
		TriangleMesh triangleMesh = new TriangleMesh();
		for (int i = 1; i <= val; i++)
		{
			Vector3F vector3F2 = quaternionF.Rotate(vector3F);
			triangleMesh.Add(new Triangle
			{
				Vertex0 = Vector3F.Zero,
				Vertex1 = vector3F2,
				Vertex2 = vector3F
			}, weldVerticesBruteForce: false);
			triangleMesh.Add(new Triangle
			{
				Vertex0 = vector3F,
				Vertex1 = vector3F2,
				Vertex2 = vertex
			}, weldVerticesBruteForce: false);
			vector3F = vector3F2;
		}
		triangleMesh.WeldVertices();
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
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4742), _radius, _height);
	}
}
