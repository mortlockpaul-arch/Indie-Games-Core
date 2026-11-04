using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry;

/// <summary>
///       Defines an object that has a <see cref="P:DigitalRune.Geometry.GeometricObject.Shape" /> and a <see cref="P:DigitalRune.Geometry.GeometricObject.Pose" /> (position and 
///       orientation). (Default implementation of <see cref="T:DigitalRune.Geometry.IGeometricObject" />.)
///       </summary>
[Serializable]
public class GeometricObject : IGeometricObject, ICloneable
{
	private Aabb _aabb;

	private bool _aabbIsValid;

	private Pose _pose;

	private Shape _shape;

	private Vector3F _scale;

	/// <summary>
	///       Gets the axis-aligned bounding box (AABB).
	///       </summary>
	public Aabb Aabb
	{
		get
		{
			if (!_aabbIsValid)
			{
				_aabb = Shape.GetAabb(Scale, Pose);
				_aabbIsValid = true;
			}
			return _aabb;
		}
	}

	/// <summary>
	///       Gets or sets the pose (position and orientation).
	///       </summary>
	public Pose Pose
	{
		get
		{
			return _pose;
		}
		set
		{
			if (_pose != value)
			{
				_pose = value;
				OnPoseChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets the shape.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public Shape Shape
	{
		get
		{
			return _shape;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040));
			}
			if (_shape != null)
			{
				_shape.Changed -= c8d854b6330cdc452eac9c19732fabc3c;
			}
			_shape = value;
			_shape.Changed += c8d854b6330cdc452eac9c19732fabc3c;
			OnShapeChanged(ShapeChangedEventArgs.Empty);
		}
	}

	/// <summary>
	///       Gets or sets the scale.
	///       </summary>
	public Vector3F Scale
	{
		get
		{
			return _scale;
		}
		set
		{
			if (_scale != value)
			{
				_scale = value;
				OnShapeChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Occurs when the pose was changed.
	///       </summary>
	public event EventHandler<EventArgs> PoseChanged;

	/// <summary>
	///       Occurs when the <see cref="P:DigitalRune.Geometry.GeometricObject.Shape" /> or <see cref="P:DigitalRune.Geometry.GeometricObject.Scale" /> was changed.
	///       </summary>
	public event EventHandler<ShapeChangedEventArgs> ShapeChanged;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.GeometricObject" /> class.
	///       </summary>
	public GeometricObject()
	{
		_shape = new EmptyShape();
		_shape.Changed += c8d854b6330cdc452eac9c19732fabc3c;
		_scale = Vector3F.One;
		_pose = Pose.Identity;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.GeometricObject" /> class with a shape.
	///       </summary>
	/// <param name="shape">
	///       The shape (must not be <see langword="null" />). See property <see cref="P:DigitalRune.Geometry.GeometricObject.Shape" /> for more 
	///       details.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="shape" /> is <see langword="null" />.
	///       </exception>
	public GeometricObject(Shape shape)
	{
		if (shape == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1542));
		}
		_shape = shape;
		_shape.Changed += c8d854b6330cdc452eac9c19732fabc3c;
		_scale = Vector3F.One;
		_pose = Pose.Identity;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.GeometricObject" /> class with shape and scale.
	///       </summary>
	/// <param name="shape">
	///       The shape (must not be <see langword="null" />). See property <see cref="P:DigitalRune.Geometry.GeometricObject.Shape" /> for more
	///       details.
	///       </param>
	/// <param name="scale">The scale.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="shape" /> is <see langword="null" />.
	///       </exception>
	public GeometricObject(Shape shape, Vector3F scale)
	{
		if (shape == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1542));
		}
		_shape = shape;
		_shape.Changed += c8d854b6330cdc452eac9c19732fabc3c;
		_scale = scale;
		_pose = Pose.Identity;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.GeometricObject" /> class with shape and pose
	///       (position and orientation).
	///       </summary>
	/// <param name="shape">
	///       The shape (must not be <see langword="null" />). See property <see cref="P:DigitalRune.Geometry.GeometricObject.Shape" /> for more 
	///       details.
	///       </param>
	/// <param name="pose">The pose (position and orientation).</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="shape" /> is <see langword="null" />.
	///       </exception>
	public GeometricObject(Shape shape, Pose pose)
	{
		if (shape == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1542));
		}
		_shape = shape;
		_shape.Changed += c8d854b6330cdc452eac9c19732fabc3c;
		_scale = Vector3F.One;
		_pose = pose;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.GeometricObject" /> class with shape, scale and 
	///       pose (position and orientation).
	///       </summary>
	/// <param name="shape">
	///       The shape (must not be <see langword="null" />). See property <see cref="P:DigitalRune.Geometry.GeometricObject.Shape" /> for more
	///       details.
	///       </param>
	/// <param name="scale">The scale.</param>
	/// <param name="pose">The pose (position and orientation).</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="shape" /> is <see langword="null" />.
	///       </exception>
	public GeometricObject(Shape shape, Vector3F scale, Pose pose)
	{
		if (shape == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1542));
		}
		_shape = shape;
		_shape.Changed += c8d854b6330cdc452eac9c19732fabc3c;
		_scale = scale;
		_pose = pose;
	}

	private object c48423685eed44c8f6f4708513cf05f2b()
	{
		return Clone();
	}

	object ICloneable.Clone()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c48423685eed44c8f6f4708513cf05f2b
		return this.c48423685eed44c8f6f4708513cf05f2b();
	}

	/// <summary>
	///       Creates a new <see cref="T:DigitalRune.Geometry.GeometricObject" /> that is a clone (deep copy) of the current 
	///       instance.
	///       </summary>
	/// <returns>
	///       A new <see cref="T:DigitalRune.Geometry.GeometricObject" /> that is a clone (deep copy) of the current instance.
	///       </returns>
	public GeometricObject Clone()
	{
		GeometricObject geometricObject = cec9b74d9244fb114116db0f3a6fbc279();
		geometricObject.CloneCore(this);
		return geometricObject;
	}

	private GeometricObject cec9b74d9244fb114116db0f3a6fbc279()
	{
		GeometricObject geometricObject = CreateInstanceCore();
		if ((object)GetType() != geometricObject.GetType())
		{
			string message = string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11872), GetType());
			throw new InvalidOperationException(message);
		}
		return geometricObject;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the 
	///       <see cref="T:DigitalRune.Geometry.GeometricObject" /> derived class. 
	///       </summary>
	/// <returns>The new instance.</returns>
	protected virtual GeometricObject CreateInstanceCore()
	{
		return new GeometricObject();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.GeometricObject" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected virtual void CloneCore(GeometricObject sourceShape)
	{
		Pose = sourceShape.Pose;
		Shape = sourceShape.Shape.Clone();
		Scale = sourceShape.Scale;
	}

	private void c8d854b6330cdc452eac9c19732fabc3c(object c807f682152464d11dde83a885bf87124, ShapeChangedEventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		OnShapeChanged(c7fefeb91d4632901bd52a1f245b70db1);
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Geometry.GeometricObject.PoseChanged" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///   <see cref="T:System.EventArgs" /> object that provides the arguments for the event.
	///       </param>
	protected virtual void OnPoseChanged(EventArgs eventArgs)
	{
		_aabbIsValid = false;
		PoseChanged?.Invoke(this, eventArgs);
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Geometry.GeometricObject.ShapeChanged" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///   <see cref="T:DigitalRune.Geometry.Shapes.ShapeChangedEventArgs" /> object that provides the arguments for the event.
	///       </param>
	protected virtual void OnShapeChanged(ShapeChangedEventArgs eventArgs)
	{
		_aabbIsValid = false;
		ShapeChanged?.Invoke(this, eventArgs);
	}
}
