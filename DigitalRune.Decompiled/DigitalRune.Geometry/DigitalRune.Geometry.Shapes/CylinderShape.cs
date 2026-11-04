using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a cylinder centered at the local origin and upright along the y-axis.
///       </summary>
[Serializable]
public class CylinderShape : ConvexShape
{
	private float _height;

	private float _radius;

	/// <summary>
	///       Gets an inner point (center of cylinder).
	///       </summary>
	public override Vector3F InnerPoint => Vector3F.Zero;

	/// <summary>
	///       Gets or sets the height (which is along the y-axis).
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
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4649));
			}
			if (_height == value)
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
				if (1 == 0)
				{
					__ldtoken(CylinderShape.set_Height(float));
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
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2713));
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
				if (1 == 0)
				{
					__ldtoken(CylinderShape.set_Radius(float));
				}
				_radius = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.CylinderShape" /> class.
	///       </summary>
	public CylinderShape()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.CylinderShape" /> class with the given radius
	///       and height.
	///       </summary>
	/// <param name="radius">The radius.</param>
	/// <param name="height">The height (which is along the y-axis).</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="radius" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="height" /> is negative.
	///       </exception>
	public CylinderShape(float radius, float height)
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
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new CylinderShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		CylinderShape cylinderShape = (CylinderShape)sourceShape;
		_radius = cylinderShape.Radius;
		_height = cylinderShape.Height;
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
			Matrix33F orientation = pose.Orientation;
			Vector3F row = orientation.GetRow(0);
			Vector3F row2 = orientation.GetRow(1);
			Vector3F row3 = orientation.GetRow(2);
			Vector3F vector3F = new Vector3F(Vector3F.Dot(GetSupportPointNormalized(row), row), Vector3F.Dot(GetSupportPointNormalized(row2), row2), Vector3F.Dot(GetSupportPointNormalized(row3), row3));
			vector3F *= Math.Abs(scale.X);
			Vector3F minimum = pose.Position - vector3F;
			Vector3F maximum = pose.Position + vector3F;
			return new Aabb(minimum, maximum);
		}
		return base.GetAabb(scale, pose);
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
		Vector3F vector3F = new Vector3F(direction.X, 0f, direction.Z);
		if (vector3F.TryNormalize())
		{
			if (direction.Y >= 0f)
			{
				return Vector3F.UnitY * _height / 2f + _radius * vector3F;
			}
			return -Vector3F.UnitY * _height / 2f + _radius * vector3F;
		}
		if (direction.Y >= 0f)
		{
			return new Vector3F(_radius, _height / 2f, 0f);
		}
		return new Vector3F(_radius, (0f - _height) / 2f, 0f);
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
		Vector3F vector3F = new Vector3F(directionNormalized.X, 0f, directionNormalized.Z);
		if (vector3F.TryNormalize())
		{
			if (directionNormalized.Y >= 0f)
			{
				return Vector3F.UnitY * _height / 2f + _radius * vector3F;
			}
			return -Vector3F.UnitY * _height / 2f + _radius * vector3F;
		}
		if (directionNormalized.Y >= 0f)
		{
			return new Vector3F(_radius, _height / 2f, 0f);
		}
		return new Vector3F(_radius, (0f - _height) / 2f, 0f);
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <returns>
	///       The volume of this shape.
	///       </returns>
	public float GetVolume()
	{
		float radius = Radius;
		return (float)Math.PI * radius * radius * Height;
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
		int val = (int)Math.Ceiling((float)Math.PI * 2f / num);
		val = Math.Min(val, 2 << iterationLimit);
		num = (float)Math.PI * 2f / (float)val;
		Vector3F vector3F = new Vector3F(Radius, 0f, 0f);
		QuaternionF quaternionF = QuaternionF.CreateRotationY(num);
		TriangleMesh triangleMesh = new TriangleMesh();
		for (int i = 1; i <= val; i++)
		{
			Vector3F vector3F2 = quaternionF.Rotate(vector3F);
			triangleMesh.Add(new Triangle
			{
				Vertex0 = new Vector3F(0f, (0f - Height) / 2f, 0f),
				Vertex1 = new Vector3F(0f, (0f - Height) / 2f, 0f) + vector3F2,
				Vertex2 = new Vector3F(0f, (0f - Height) / 2f, 0f) + vector3F
			}, weldVerticesBruteForce: false);
			triangleMesh.Add(new Triangle
			{
				Vertex0 = new Vector3F(0f, Height / 2f, 0f),
				Vertex1 = new Vector3F(0f, Height / 2f, 0f) + vector3F,
				Vertex2 = new Vector3F(0f, Height / 2f, 0f) + vector3F2
			}, weldVerticesBruteForce: false);
			triangleMesh.Add(new Triangle
			{
				Vertex0 = new Vector3F(0f, (0f - Height) / 2f, 0f) + vector3F,
				Vertex1 = new Vector3F(0f, (0f - Height) / 2f, 0f) + vector3F2,
				Vertex2 = new Vector3F(0f, Height / 2f, 0f) + vector3F
			}, weldVerticesBruteForce: false);
			triangleMesh.Add(new Triangle
			{
				Vertex0 = new Vector3F(0f, (0f - Height) / 2f, 0f) + vector3F2,
				Vertex1 = new Vector3F(0f, Height / 2f, 0f) + vector3F2,
				Vertex2 = new Vector3F(0f, Height / 2f, 0f) + vector3F
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
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12443), _radius, _height);
	}
}
