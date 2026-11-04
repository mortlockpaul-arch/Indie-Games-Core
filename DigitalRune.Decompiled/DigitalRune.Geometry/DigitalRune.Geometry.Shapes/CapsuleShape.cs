using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a capsule centered at the local origin and upright along the y-axis.
///       </summary>
[Serializable]
public class CapsuleShape : ConvexShape
{
	private float _height;

	private float _radius;

	/// <summary>
	///       Gets an inner point (center of capsule).
	///       </summary>
	public override Vector3F InnerPoint => Vector3F.Zero;

	/// <summary>
	///       Gets or sets the total height (including the spherical caps).
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is less than 2 * <see cref="P:DigitalRune.Geometry.Shapes.CapsuleShape.Radius" />. (The height must be greater 
	///       than or equal to 2 * radius.)
	///       </exception>
	public float Height
	{
		get
		{
			return _height;
		}
		set
		{
			if (value < 2f * _radius)
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
							__ldtoken(CapsuleShape.set_Height(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5561));
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
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the radius.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is either negative or greater than <see cref="P:DigitalRune.Geometry.Shapes.CapsuleShape.Height" /> / 2. (The 
	///       radius must be less than or equal to height / 2.)
	///       </exception>
	public float Radius
	{
		get
		{
			return _radius;
		}
		set
		{
			if (!(value < 0f))
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
					__ldtoken(CapsuleShape.set_Radius(float));
				}
				if (!(2f * value > _height))
				{
					if (_radius == value)
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
						_radius = value;
						OnChanged(ShapeChangedEventArgs.Empty);
						return;
					}
				}
			}
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5656));
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.CapsuleShape" /> class.
	///       </summary>
	public CapsuleShape()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.CapsuleShape" /> class with the given radius and 
	///       height.
	///       </summary>
	/// <param name="radius">The radius.</param>
	/// <param name="height">The height (including the spherical caps).</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="height" /> is less than 2 * <see cref="P:DigitalRune.Geometry.Shapes.CapsuleShape.Radius" />. (The height must be greater 
	///       than or equal to 2 * radius.)
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="radius" /> is negative.
	///       </exception>
	public CapsuleShape(float radius, float height)
	{
		if (height < 2f * radius)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4813), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5561));
		}
		if (radius < 0f)
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
						__ldtoken(CapsuleShape._002Ector(float, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2857), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2713));
				}
			}
		}
		_height = height;
		_radius = radius;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new CapsuleShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		CapsuleShape capsuleShape = (CapsuleShape)sourceShape;
		_radius = capsuleShape.Radius;
		_height = capsuleShape.Height;
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
		if (scale.X == scale.Y && scale.Y == scale.Z)
		{
			float num = Math.Abs(scale.X);
			float num2 = num * _height;
			float num3 = num * _radius;
			float num4 = num2 / 2f - num3;
			Vector3F vector = pose.ToWorldPosition(new Vector3F(0f, num4, 0f));
			Vector3F vector2 = pose.ToWorldPosition(new Vector3F(0f, 0f - num4, 0f));
			Vector3F vector3F = new Vector3F(num3);
			Vector3F minimum = Vector3F.Min(vector, vector2) - vector3F;
			Vector3F maximum = Vector3F.Max(vector, vector2) + vector3F;
			return new Aabb(minimum, maximum);
		}
		return base.GetAabb(scale, pose);
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
		Vector3F vector3F = new Vector3F(0f, _height / 2f - _radius, 0f);
		if (directionNormalized.Y < 0f)
		{
			vector3F = -vector3F;
		}
		return vector3F + directionNormalized * _radius;
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <returns>The volume of this shape.</returns>
	public float GetVolume()
	{
		float radius = Radius;
		float num = 4.1887903f * radius * radius * radius;
		float num2 = (float)Math.PI * radius * radius * (Height - 2f * radius);
		return num + num2;
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
		float num = (float)Math.Acos((Radius - absoluteDistanceThreshold) / Radius) * 2f;
		int val = (int)Math.Ceiling((float)Math.PI / 2f / num) * 4;
		val = Math.Min(val, 2 << iterationLimit);
		num = (float)Math.PI * 2f / (float)val;
		TriangleMesh triangleMesh = new TriangleMesh();
		QuaternionF quaternionF = QuaternionF.CreateRotationY(num);
		QuaternionF quaternionF2 = QuaternionF.CreateRotationZ(num);
		Vector3F vector3F = Vector3F.UnitX * Radius;
		for (int i = 1; i <= val / 4; i++)
		{
			Vector3F vector3F2 = quaternionF2.Rotate(vector3F);
			Vector3F vector3F3 = vector3F;
			Vector3F vector3F4 = vector3F2;
			for (int j = 1; j <= val; j++)
			{
				Vector3F vector3F5 = quaternionF.Rotate(vector3F3);
				Vector3F vector3F6 = quaternionF.Rotate(vector3F4);
				triangleMesh.Add(new Triangle
				{
					Vertex0 = new Vector3F(0f, Height / 2f - Radius, 0f) + vector3F3,
					Vertex1 = new Vector3F(0f, Height / 2f - Radius, 0f) + vector3F5,
					Vertex2 = new Vector3F(0f, Height / 2f - Radius, 0f) + vector3F4
				}, weldVerticesBruteForce: false);
				if (i < val / 4)
				{
					triangleMesh.Add(new Triangle
					{
						Vertex0 = new Vector3F(0f, Height / 2f - Radius, 0f) + vector3F5,
						Vertex1 = new Vector3F(0f, Height / 2f - Radius, 0f) + vector3F6,
						Vertex2 = new Vector3F(0f, Height / 2f - Radius, 0f) + vector3F4
					}, weldVerticesBruteForce: false);
				}
				triangleMesh.Add(new Triangle
				{
					Vertex0 = new Vector3F(0f, (0f - Height) / 2f + Radius, 0f) - vector3F3,
					Vertex1 = new Vector3F(0f, (0f - Height) / 2f + Radius, 0f) - vector3F4,
					Vertex2 = new Vector3F(0f, (0f - Height) / 2f + Radius, 0f) - vector3F5
				}, weldVerticesBruteForce: false);
				if (i < val / 4)
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
						__ldtoken(CapsuleShape.OnGetMesh(float, int));
					}
					triangleMesh.Add(new Triangle
					{
						Vertex0 = new Vector3F(0f, (0f - Height) / 2f + Radius, 0f) - vector3F5,
						Vertex1 = new Vector3F(0f, (0f - Height) / 2f + Radius, 0f) - vector3F4,
						Vertex2 = new Vector3F(0f, (0f - Height) / 2f + Radius, 0f) - vector3F6
					}, weldVerticesBruteForce: false);
				}
				if (i == 1)
				{
					triangleMesh.Add(new Triangle
					{
						Vertex0 = new Vector3F(0f, (0f - Height) / 2f + Radius, 0f) + vector3F3,
						Vertex1 = new Vector3F(0f, (0f - Height) / 2f + Radius, 0f) + vector3F5,
						Vertex2 = new Vector3F(0f, Height / 2f - Radius, 0f) + vector3F3
					}, weldVerticesBruteForce: false);
					triangleMesh.Add(new Triangle
					{
						Vertex0 = new Vector3F(0f, (0f - Height) / 2f + Radius, 0f) + vector3F5,
						Vertex1 = new Vector3F(0f, Height / 2f - Radius, 0f) + vector3F5,
						Vertex2 = new Vector3F(0f, Height / 2f - Radius, 0f) + vector3F3
					}, weldVerticesBruteForce: false);
				}
				vector3F3 = vector3F5;
				vector3F4 = vector3F6;
			}
			vector3F = vector3F2;
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			triangleMesh.WeldVertices();
			return triangleMesh;
		}
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5808), _radius, _height);
	}
}
