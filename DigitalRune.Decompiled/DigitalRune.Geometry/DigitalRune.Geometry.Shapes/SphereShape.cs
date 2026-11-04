using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a sphere.
///       </summary>
[Serializable]
public class SphereShape : ConvexShape
{
	private float _radius;

	/// <summary>
	///       Gets an inner point (center of sphere).
	///       </summary>
	public override Vector3F InnerPoint => Vector3F.Zero;

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
					switch (5)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(SphereShape.set_Radius(float));
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
				switch (4)
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

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.SphereShape" /> class.
	///       </summary>
	public SphereShape()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.SphereShape" /> class with the given radius.
	///       </summary>
	/// <param name="radius">The radius.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="radius" /> is negative.
	///       </exception>
	public SphereShape(float radius)
	{
		if (radius < 0f)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(SphereShape._002Ector(float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2857), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2713));
				}
			}
		}
		_radius = radius;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new SphereShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		SphereShape sphereShape = (SphereShape)sourceShape;
		_radius = sphereShape.Radius;
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
			Vector3F vector3F = new Vector3F(_radius * Math.Abs(scale.X));
			return new Aabb(pose.Position - vector3F, pose.Position + vector3F);
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
		return directionNormalized * _radius;
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <returns>
	///       The volume of this shape.
	///       </returns>
	public float GetVolume()
	{
		return (float)Math.PI * 4f * _radius * _radius * _radius / 3f;
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
		int val = (int)Math.Ceiling((float)Math.PI / 2f / num) * 4;
		val = Math.Min(val, 2 << iterationLimit);
		num = (float)Math.PI * 2f / (float)val;
		TriangleMesh triangleMesh = new TriangleMesh();
		QuaternionF quaternionF = QuaternionF.CreateRotationY(num);
		QuaternionF quaternionF2 = QuaternionF.CreateRotationZ(num);
		Vector3F vector3F = Vector3F.UnitX * _radius;
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
					Vertex0 = vector3F3,
					Vertex1 = vector3F5,
					Vertex2 = vector3F4
				}, weldVerticesBruteForce: false);
				if (i < val / 4)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
					if (1 == 0)
					{
						__ldtoken(SphereShape.OnGetMesh(float, int));
					}
					triangleMesh.Add(new Triangle
					{
						Vertex0 = vector3F5,
						Vertex1 = vector3F6,
						Vertex2 = vector3F4
					}, weldVerticesBruteForce: false);
				}
				triangleMesh.Add(new Triangle
				{
					Vertex0 = -vector3F3,
					Vertex1 = -vector3F4,
					Vertex2 = -vector3F5
				}, weldVerticesBruteForce: false);
				if (i < val / 4)
				{
					triangleMesh.Add(new Triangle
					{
						Vertex0 = -vector3F5,
						Vertex1 = -vector3F4,
						Vertex2 = -vector3F6
					}, weldVerticesBruteForce: false);
				}
				vector3F3 = vector3F5;
				vector3F4 = vector3F6;
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					goto end_IL_01bb;
				}
				continue;
				end_IL_01bb:
				break;
			}
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
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9382), _radius);
	}
}
