using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a transformed shape.
///       </summary>
[Serializable]
public class TransformedShape : Shape
{
	private IGeometricObject _child;

	/// <summary>
	///       Gets or sets the child <see cref="T:DigitalRune.Geometry.IGeometricObject" />.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public IGeometricObject Child
	{
		get
		{
			return _child;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (_child != value)
			{
				_child.PoseChanged -= c8b5ea39fcabf2c8c09bb85856a670ee1;
				_child.ShapeChanged -= ce495e81216e74908d006ffa40b560c26;
				_child = value;
				_child.PoseChanged += c8b5ea39fcabf2c8c09bb85856a670ee1;
				_child.ShapeChanged += ce495e81216e74908d006ffa40b560c26;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint => _child.Pose.ToWorldPosition(_child.Shape.InnerPoint * _child.Scale);

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.TransformedShape" /> class.
	///       </summary>
	public TransformedShape()
	{
		_child = new GeometricObject(new EmptyShape());
		_child.PoseChanged += c8b5ea39fcabf2c8c09bb85856a670ee1;
		_child.ShapeChanged += ce495e81216e74908d006ffa40b560c26;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.TransformedShape" /> class from the given 
	///       geometric object.
	///       </summary>
	/// <param name="child">The geometric object (pose + shape).</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="child" /> is <see langword="null" />.
	///       </exception>
	public TransformedShape(IGeometricObject child)
	{
		if (child == null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(TransformedShape._002Ector(IGeometricObject));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17837));
				}
			}
		}
		_child = child;
		_child.PoseChanged += c8b5ea39fcabf2c8c09bb85856a670ee1;
		_child.ShapeChanged += ce495e81216e74908d006ffa40b560c26;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new TransformedShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		TransformedShape transformedShape = (TransformedShape)sourceShape;
		Child = (IGeometricObject)transformedShape.Child.Clone();
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
		if (scale.X == scale.Y && scale.X == scale.Y)
		{
			Pose pose2 = new Pose(_child.Pose.Position * scale.X, _child.Pose.Orientation);
			return _child.Shape.GetAabb(scale.X * _child.Scale, pose * pose2);
		}
		return _child.Aabb.GetAabb(scale, pose);
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <param name="relativeError">
	///       The desired relative error for approximations (in the range [0, 1]). For example, use the 
	///       value 0.01 to get a maximal error of about 1%.
	///       </param>
	/// <param name="iterationLimit">
	///       The iteration limit. Must be greater than or equal to 0. For most cases a small value like 4 
	///       is appropriate. 
	///       </param>
	/// <returns>The volume of this shape.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="relativeError" /> is negative.
	///       </exception>
	public override float GetVolume(float relativeError, int iterationLimit)
	{
		Vector3F vector3F = Vector3F.Absolute(Child.Scale);
		return Child.Shape.GetVolume(relativeError, iterationLimit) * vector3F.X * vector3F.Y * vector3F.Z;
	}

	private void c8b5ea39fcabf2c8c09bb85856a670ee1(object c807f682152464d11dde83a885bf87124, EventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		OnChanged(ShapeChangedEventArgs.Empty);
	}

	private void ce495e81216e74908d006ffa40b560c26(object c807f682152464d11dde83a885bf87124, ShapeChangedEventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		OnChanged(c7fefeb91d4632901bd52a1f245b70db1);
	}

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected override TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit)
	{
		float largestComponent = GetAabb(Vector3F.One, Pose.Identity).Extent.LargestComponent;
		float relativeDistanceThreshold = ((!Numeric.IsZero(largestComponent)) ? (absoluteDistanceThreshold / largestComponent) : Numeric.EpsilonF);
		TriangleMesh mesh = _child.Shape.GetMesh(relativeDistanceThreshold, iterationLimit);
		mesh.Transform(_child.Pose.ToMatrix44F() * Matrix44F.CreateScale(_child.Scale));
		return mesh;
	}
}
