using System;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a scaled convex shape.
///       </summary>
[Serializable]
public class ScaledConvexShape : ConvexShape
{
	private Vector3F _scale;

	private ConvexShape _shape;

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint => _shape.InnerPoint * _scale;

	/// <summary>
	///       Gets or sets the scale factor.
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
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the convex shape that is scaled.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public ConvexShape Shape
	{
		get
		{
			return _shape;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (_shape == value)
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
				if (1 == 0)
				{
					__ldtoken(ScaledConvexShape.set_Shape(ConvexShape));
				}
				if (_shape != null)
				{
					_shape.Changed -= ce495e81216e74908d006ffa40b560c26;
				}
				_shape = value;
				_shape.Changed += ce495e81216e74908d006ffa40b560c26;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.ScaledConvexShape" /> class.
	///       </summary>
	public ScaledConvexShape()
	{
		Shape = new PointShape();
		Scale = Vector3F.One;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.ScaledConvexShape" /> class from two geometric
	///       objects.
	///       </summary>
	/// <param name="shape">The convex shape that should be scaled.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="shape" /> is <see langword="null" />.
	///       </exception>
	public ScaledConvexShape(ConvexShape shape)
	{
		if (shape == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1542));
		}
		Shape = shape;
		Scale = Vector3F.One;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.ScaledConvexShape" /> class from two geometric
	///       objects.
	///       </summary>
	/// <param name="shape">The convex shape that should be scaled.</param>
	/// <param name="scale">The scale of the convex shape.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="shape" /> is <see langword="null" />.
	///       </exception>
	public ScaledConvexShape(ConvexShape shape, Vector3F scale)
	{
		if (shape == null)
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
						__ldtoken(ScaledConvexShape._002Ector(ConvexShape, Vector3F));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1542));
				}
			}
		}
		Shape = shape;
		Scale = scale;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="P:DigitalRune.Geometry.Shapes.ScaledConvexShape.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new ScaledConvexShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="P:DigitalRune.Geometry.Shapes.ScaledConvexShape.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		ScaledConvexShape scaledConvexShape = (ScaledConvexShape)sourceShape;
		Scale = scaledConvexShape.Scale;
		Shape = (ConvexShape)((ICloneable)scaledConvexShape.Shape).Clone();
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
		return _shape.GetSupportPoint(direction, _scale);
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
		if (_scale.X == _scale.Y && _scale.Y == _scale.Z)
		{
			return _shape.GetSupportPointNormalized(directionNormalized) * _scale;
		}
		return _shape.GetSupportPoint(directionNormalized, _scale);
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
		Vector3F vector3F = Vector3F.Absolute(Scale);
		return Shape.GetVolume(relativeError, iterationLimit) * vector3F.X * vector3F.Y * vector3F.Z;
	}

	private void ce495e81216e74908d006ffa40b560c26(object c807f682152464d11dde83a885bf87124, ShapeChangedEventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		OnChanged(c7fefeb91d4632901bd52a1f245b70db1);
	}
}
