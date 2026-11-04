using System;
using System.Collections.Generic;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a convex shape. 
///       </summary>
[Serializable]
public abstract class ConvexShape : Shape
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.ConvexShape" /> class</summary>
	protected ConvexShape()
	{
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
	/// <returns>The AABB of the shape positioned in world space.</returns>
	public override Aabb GetAabb(Vector3F scale, Pose pose)
	{
		if (scale.X == scale.Y && scale.Y == scale.Z)
		{
			Matrix33F orientation = pose.Orientation;
			Vector3F row = orientation.GetRow(0);
			Vector3F row2 = orientation.GetRow(1);
			Vector3F row3 = orientation.GetRow(2);
			Vector3F vector3F = new Vector3F(Vector3F.Dot(GetSupportPointNormalized(-row), row), Vector3F.Dot(GetSupportPointNormalized(-row2), row2), Vector3F.Dot(GetSupportPointNormalized(-row3), row3));
			vector3F = vector3F * scale.X + pose.Position;
			Vector3F vector3F2 = new Vector3F(Vector3F.Dot(GetSupportPointNormalized(row), row), Vector3F.Dot(GetSupportPointNormalized(row2), row2), Vector3F.Dot(GetSupportPointNormalized(row3), row3));
			vector3F2 = vector3F2 * scale.X + pose.Position;
			if (vector3F <= vector3F2)
			{
				return new Aabb(vector3F, vector3F2);
			}
			return new Aabb(vector3F2, vector3F);
		}
		Matrix33F orientation2 = pose.Orientation;
		Vector3F row4 = orientation2.GetRow(0);
		Vector3F row5 = orientation2.GetRow(1);
		Vector3F row6 = orientation2.GetRow(2);
		Vector3F minimum = new Vector3F(Vector3F.Dot(GetSupportPoint(-row4, scale), row4), Vector3F.Dot(GetSupportPoint(-row5, scale), row5), Vector3F.Dot(GetSupportPoint(-row6, scale), row6));
		Vector3F maximum = new Vector3F(Vector3F.Dot(GetSupportPoint(row4, scale), row4), Vector3F.Dot(GetSupportPoint(row5, scale), row5), Vector3F.Dot(GetSupportPoint(row6, scale), row6));
		minimum += pose.Position;
		maximum += pose.Position;
		if (minimum.X > maximum.X)
		{
			MathHelper.Swap(ref minimum.X, ref maximum.X);
		}
		if (minimum.Y > maximum.Y)
		{
			MathHelper.Swap(ref minimum.Y, ref maximum.Y);
		}
		if (minimum.Z > maximum.Z)
		{
			MathHelper.Swap(ref minimum.Z, ref maximum.Z);
		}
		return new Aabb(minimum, maximum);
	}

	/// <summary>
	///       Gets a support point for a given direction.
	///       </summary>
	/// <param name="direction">
	///       The direction for which to get the support point. The vector does not need to be normalized.
	///       The result is undefined if the vector is a zero vector.
	///       </param>
	/// <returns>A support point regarding the given direction.</returns>
	public virtual Vector3F GetSupportPoint(Vector3F direction)
	{
		if (!direction.TryNormalize())
		{
			direction = Vector3F.UnitX;
		}
		return GetSupportPointNormalized(direction);
	}

	/// <summary>
	///       Gets a support point for a given direction and a given non-uniform scaling.
	///       </summary>
	/// <param name="direction">
	///       The direction for which to get the support point on the scaled shape. The vector does not 
	///       need to be normalized. The result is undefined if the vector is a zero vector.
	///       </param>
	/// <param name="scale">
	///       The scale that is applied to the shape. This can be a non-uniform 3D scaling.
	///       </param>
	/// <returns>
	///       A support point regarding the given direction on the scaled shape.
	///       </returns>
	public Vector3F GetSupportPoint(Vector3F direction, Vector3F scale)
	{
		return GetSupportPoint(scale * direction) * scale;
	}

	/// <summary>
	///       Gets a support point for a given normalized direction vector.
	///       </summary>
	/// <param name="directionNormalized">
	///       The normalized direction vector for which to get the support point. 
	///       </param>
	/// <returns>A support point regarding the given direction.</returns>
	public abstract Vector3F GetSupportPointNormalized(Vector3F directionNormalized);

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected override TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit)
	{
		IList<Vector3F> points = GeometryHelper.cb62be1cc9adc6e749e53fcadaf4a7908(this, absoluteDistanceThreshold, iterationLimit);
		DcelMesh dcelMesh = GeometryHelper.CreateConvexHull(points);
		return dcelMesh.ToTriangleMesh();
	}
}
