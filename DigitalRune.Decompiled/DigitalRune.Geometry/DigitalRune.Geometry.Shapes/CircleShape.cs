using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a circle in the xy plane centered at the origin.
///       </summary>
[Serializable]
public class CircleShape : ConvexShape
{
	private float _radius;

	/// <summary>
	///       Gets an inner point (center of the circle).
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
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2713));
			}
			if (_radius == value)
			{
				return;
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(CircleShape.set_Radius(float));
				}
				_radius = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.CircleShape" /> class.
	///       </summary>
	public CircleShape()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.CircleShape" /> class with the given radius.
	///       </summary>
	/// <param name="radius">The radius.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="radius" /> is negative.
	///       </exception>
	public CircleShape(float radius)
	{
		if (radius < 0f)
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
						__ldtoken(CircleShape._002Ector(float));
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
		return new CircleShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		CircleShape circleShape = (CircleShape)sourceShape;
		_radius = circleShape.Radius;
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
			Vector3F vector3F = Vector3F.Absolute(new Vector3F(Vector3F.Dot(GetSupportPointNormalized(row), row), Vector3F.Dot(GetSupportPointNormalized(row2), row2), Vector3F.Dot(GetSupportPointNormalized(row3), row3)));
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
		direction.Z = 0f;
		if (!direction.TryNormalize())
		{
			direction = Vector3F.UnitX;
		}
		return direction * _radius;
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
		directionNormalized.Z = 0f;
		if (!directionNormalized.TryNormalize())
		{
			directionNormalized = Vector3F.UnitX;
		}
		return directionNormalized * _radius;
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
		float num = (float)Math.Acos((_radius - absoluteDistanceThreshold) / _radius) * 2f;
		int num2 = (int)Math.Ceiling((float)Math.PI * 2f / num);
		num = (float)Math.PI * 2f / (float)num2;
		Vector3F vector3F = new Vector3F(_radius, 0f, 0f);
		QuaternionF quaternionF = QuaternionF.CreateRotationZ(num);
		TriangleMesh triangleMesh = new TriangleMesh();
		for (int i = 1; i <= num2; i++)
		{
			Vector3F vector3F2 = quaternionF.Rotate(vector3F);
			triangleMesh.Add(new Triangle
			{
				Vertex0 = Vector3F.Zero,
				Vertex1 = vector3F,
				Vertex2 = vector3F2
			}, weldVerticesBruteForce: true);
			vector3F = vector3F2;
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(CircleShape.OnGetMesh(float, int));
			}
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
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2806), _radius);
	}
}
