using System;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry;

/// <summary>
///       Defines an object that has a <see cref="P:DigitalRune.Geometry.IGeometricObject.Shape" /> and a <see cref="P:DigitalRune.Geometry.IGeometricObject.Pose" /> (position and 
///       orientation).
///       </summary>
public interface IGeometricObject : ICloneable
{
	/// <summary>
	///       Gets the axis-aligned bounding box (AABB).
	///       </summary>
	Aabb Aabb { get; }

	/// <summary>
	///       Gets the pose (position and orientation).
	///       </summary>
	Pose Pose { get; }

	/// <summary>
	///       Gets the shape.
	///       </summary>
	Shape Shape { get; }

	/// <summary>
	///       Gets the scale.
	///       </summary>
	Vector3F Scale { get; }

	/// <summary>
	///       Occurs when the <see cref="P:DigitalRune.Geometry.IGeometricObject.Pose" /> was changed.
	///       </summary>
	event EventHandler<EventArgs> PoseChanged;

	/// <summary>
	///       Occurs when the <see cref="P:DigitalRune.Geometry.IGeometricObject.Shape" /> or <see cref="P:DigitalRune.Geometry.IGeometricObject.Scale" /> was changed.
	///       </summary>
	event EventHandler<ShapeChangedEventArgs> ShapeChanged;
}
