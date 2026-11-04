using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents an orthographic view volume.
///       </summary>
[Serializable]
public class OrthographicViewVolume : ViewVolume
{
	private Vector3F _boxCenter;

	private readonly BoxShape _box;

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint => _boxCenter + _box.InnerPoint;

	/// <summary>
	///       Gets the horizontal field of view (always <see cref="F:System.Single.NaN" />).
	///       </summary>
	public override float FieldOfViewX => float.NaN;

	/// <summary>
	///       Gets the vertical field of view (always <see cref="F:System.Single.NaN" />).
	///       </summary>
	public override float FieldOfViewY => float.NaN;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.OrthographicViewVolume" /> class using default 
	///       settings.
	///       </summary>
	public OrthographicViewVolume()
		: this(2f, 2f, 1f, 4f)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.OrthographicViewVolume" /> class as a symmetric
	///       view volume.
	///       </summary>
	/// <param name="width">The width of the view volume at the near clip plane.</param>
	/// <param name="height">The height of the view volume at the near clip plane.</param>
	/// <param name="near">The distance to the near clip plane.</param>
	/// <param name="far">The distance to the far clip plane.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="width" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="height" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is greater than or equal to <paramref name="far" />.
	///       </exception>
	public OrthographicViewVolume(float width, float height, float near, float far)
	{
		_boxCenter = Vector3F.Zero;
		_box = new BoxShape();
		SetWidthAndHeight(width, height, near, far);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.OrthographicViewVolume" /> class as an 
	///       asymmetric, off-center view volume.
	///       </summary>
	/// <param name="left">The minimum x-value of the view volume at the near clip plane.</param>
	/// <param name="right">The maximum x-value of the view volume at the near clip plane.</param>
	/// <param name="bottom">The minimum y-value of the view volume at the near clip plane.</param>
	/// <param name="top">The maximum y-value of the view volume at the near clip plane.</param>
	/// <param name="near">The distance to the near clip plane.</param>
	/// <param name="far">The distance to the far clip plane.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="left" /> is greater than or equal to <paramref name="right" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="bottom" /> is greater than or equal to <paramref name="top" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is greater than or equal to <paramref name="far" />.
	///       </exception>
	public OrthographicViewVolume(float left, float right, float bottom, float top, float near, float far)
	{
		_boxCenter = Vector3F.Zero;
		_box = new BoxShape();
		Set(left, right, bottom, top, near, far);
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new OrthographicViewVolume();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		OrthographicViewVolume orthographicViewVolume = (OrthographicViewVolume)sourceShape;
		Set(orthographicViewVolume.Left, orthographicViewVolume.Right, orthographicViewVolume.Bottom, orthographicViewVolume.Top, orthographicViewVolume.Near, orthographicViewVolume.Far);
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
		return _box.GetAabb(scale, pose * new Pose(_boxCenter * scale));
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10293), Left, Right, Bottom, Top, Near, Far);
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
		Vector3F supportPoint = _box.GetSupportPoint(direction);
		return _boxCenter + supportPoint;
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
		Vector3F supportPointNormalized = _box.GetSupportPointNormalized(directionNormalized);
		return _boxCenter + supportPointNormalized;
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <returns>The volume of this shape.</returns>
	public float GetVolume()
	{
		return Width * Height * Depth;
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
		float z = 0f - Math.Min(Near, Far);
		float z2 = 0f - Math.Max(Near, Far);
		float x = Math.Min(Left, Right);
		float x2 = Math.Max(Left, Right);
		float y = Math.Max(Top, Bottom);
		float y2 = Math.Min(Top, Bottom);
		TriangleMesh triangleMesh = new TriangleMesh();
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x, y2, z),
			Vertex1 = new Vector3F(x, y2, z2),
			Vertex2 = new Vector3F(x2, y2, z2)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x2, y2, z2),
			Vertex1 = new Vector3F(x2, y2, z),
			Vertex2 = new Vector3F(x, y2, z)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x2, y, z),
			Vertex1 = new Vector3F(x2, y2, z),
			Vertex2 = new Vector3F(x2, y2, z2)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x2, y2, z2),
			Vertex1 = new Vector3F(x2, y, z2),
			Vertex2 = new Vector3F(x2, y, z)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x2, y, z2),
			Vertex1 = new Vector3F(x2, y2, z2),
			Vertex2 = new Vector3F(x, y2, z2)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x, y2, z2),
			Vertex1 = new Vector3F(x, y, z2),
			Vertex2 = new Vector3F(x2, y, z2)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x, y, z2),
			Vertex1 = new Vector3F(x, y2, z2),
			Vertex2 = new Vector3F(x, y2, z)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x, y2, z),
			Vertex1 = new Vector3F(x, y, z),
			Vertex2 = new Vector3F(x, y, z2)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x, y, z),
			Vertex1 = new Vector3F(x, y2, z),
			Vertex2 = new Vector3F(x2, y2, z)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x2, y2, z),
			Vertex1 = new Vector3F(x2, y, z),
			Vertex2 = new Vector3F(x, y, z)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x, y, z2),
			Vertex1 = new Vector3F(x, y, z),
			Vertex2 = new Vector3F(x2, y, z)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x2, y, z),
			Vertex1 = new Vector3F(x2, y, z2),
			Vertex2 = new Vector3F(x, y, z2)
		}, weldVerticesBruteForce: true);
		return triangleMesh;
	}

	/// <summary>
	///       Updates the shape.
	///       </summary>
	protected override void Update()
	{
		float num;
		float num2;
		if (Left <= Right)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(OrthographicViewVolume.Update());
			}
			num = Left;
			num2 = Right;
		}
		else
		{
			num = Right;
			num2 = Left;
		}
		float num3;
		float num4;
		if (Bottom <= Top)
		{
			num3 = Bottom;
			num4 = Top;
		}
		else
		{
			num3 = Top;
			num4 = Bottom;
		}
		float num5;
		float num6;
		if (Near <= Far)
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
			num5 = Near;
			num6 = Far;
		}
		else
		{
			num5 = Far;
			num6 = Near;
		}
		float num7 = num2 - num;
		float num8 = num4 - num3;
		float num9 = num6 - num5;
		_box.WidthX = num7;
		_box.WidthY = num8;
		_box.WidthZ = num9;
		float x = num + num7 / 2f;
		float y = num3 + num8 / 2f;
		float z = 0f - (num5 + num9 / 2f);
		_boxCenter = new Vector3F(x, y, z);
	}
}
