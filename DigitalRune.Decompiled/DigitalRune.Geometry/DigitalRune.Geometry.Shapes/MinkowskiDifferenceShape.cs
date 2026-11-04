using System;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents the <i>Minkowski Difference</i> of two geometric objects.
///       </summary>
[Serializable]
public class MinkowskiDifferenceShape : ConvexShape
{
	private IGeometricObject _objectA;

	private IGeometricObject _objectB;

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint
	{
		get
		{
			Vector3F vector3F = _objectA.Pose.ToWorldPosition(_objectA.Shape.InnerPoint);
			Vector3F vector3F2 = _objectB.Pose.ToWorldPosition(_objectB.Shape.InnerPoint);
			return vector3F - vector3F2;
		}
	}

	/// <summary>
	///       Gets or sets the first <see cref="T:DigitalRune.Geometry.IGeometricObject" />.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///   <paramref name="value" /> is not convex.
	///       </exception>
	public IGeometricObject ObjectA
	{
		get
		{
			return _objectA;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (_objectA == value)
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
					__ldtoken(MinkowskiDifferenceShape.set_ObjectA(IGeometricObject));
				}
				if (_objectA != null)
				{
					_objectA.PoseChanged -= c8b5ea39fcabf2c8c09bb85856a670ee1;
					_objectA.ShapeChanged -= c8b5ea39fcabf2c8c09bb85856a670ee1;
				}
				_objectA = value;
				_objectA.PoseChanged += c8b5ea39fcabf2c8c09bb85856a670ee1;
				_objectA.ShapeChanged += c8b5ea39fcabf2c8c09bb85856a670ee1;
				cca2bcde369ccf37146d188412fd7de42();
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the second <see cref="T:DigitalRune.Geometry.IGeometricObject" />.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///   <paramref name="value" /> is not convex.
	///       </exception>
	public IGeometricObject ObjectB
	{
		get
		{
			return _objectB;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (_objectB == value)
			{
				return;
			}
			if (_objectB != null)
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
				if (1 == 0)
				{
					__ldtoken(MinkowskiDifferenceShape.set_ObjectB(IGeometricObject));
				}
				_objectB.PoseChanged -= c8b5ea39fcabf2c8c09bb85856a670ee1;
				_objectB.ShapeChanged -= ce495e81216e74908d006ffa40b560c26;
			}
			_objectB = value;
			_objectB.PoseChanged += c8b5ea39fcabf2c8c09bb85856a670ee1;
			_objectB.ShapeChanged += ce495e81216e74908d006ffa40b560c26;
			cca2bcde369ccf37146d188412fd7de42();
			OnChanged(ShapeChangedEventArgs.Empty);
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.MinkowskiDifferenceShape" /> class.
	///       </summary>
	public MinkowskiDifferenceShape()
	{
		_objectA = new GeometricObject(new PointShape());
		_objectA.PoseChanged += c8b5ea39fcabf2c8c09bb85856a670ee1;
		_objectA.ShapeChanged += ce495e81216e74908d006ffa40b560c26;
		_objectB = new GeometricObject(new PointShape());
		_objectB.PoseChanged += c8b5ea39fcabf2c8c09bb85856a670ee1;
		_objectB.ShapeChanged += ce495e81216e74908d006ffa40b560c26;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.MinkowskiDifferenceShape" /> class from two
	///       geometric objects.
	///       </summary>
	/// <param name="objectA">The geometric object A.</param>
	/// <param name="objectB">The geometric object B.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///   <paramref name="objectA" /> is not convex.
	///       </exception>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///   <paramref name="objectB" /> is not convex.
	///       </exception>
	public MinkowskiDifferenceShape(IGeometricObject objectA, IGeometricObject objectB)
	{
		if (objectA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
		}
		if (objectB == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
		}
		_objectA = objectA;
		_objectA.PoseChanged += c8b5ea39fcabf2c8c09bb85856a670ee1;
		_objectA.ShapeChanged += ce495e81216e74908d006ffa40b560c26;
		_objectB = objectB;
		_objectB.PoseChanged += c8b5ea39fcabf2c8c09bb85856a670ee1;
		_objectB.ShapeChanged += ce495e81216e74908d006ffa40b560c26;
		cca2bcde369ccf37146d188412fd7de42();
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new MinkowskiDifferenceShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		MinkowskiDifferenceShape minkowskiDifferenceShape = (MinkowskiDifferenceShape)sourceShape;
		ObjectA = (IGeometricObject)minkowskiDifferenceShape.ObjectA.Clone();
		ObjectB = (IGeometricObject)minkowskiDifferenceShape.ObjectB.Clone();
	}

	private void cca2bcde369ccf37146d188412fd7de42()
	{
		if (_objectA != null && !(_objectA.Shape is ConvexShape))
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
						__ldtoken(MinkowskiDifferenceShape.cca2bcde369ccf37146d188412fd7de42());
					}
					throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17065));
				}
			}
		}
		if (_objectB == null)
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
			if (!(_objectB.Shape is ConvexShape))
			{
				throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17207));
			}
			return;
		}
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
		Vector3F directionNormalized2 = _objectA.Pose.ToLocalDirection(directionNormalized);
		Vector3F directionNormalized3 = _objectB.Pose.ToLocalDirection(-directionNormalized);
		Vector3F supportPointNormalized = ((ConvexShape)_objectA.Shape).GetSupportPointNormalized(directionNormalized2);
		Vector3F supportPointNormalized2 = ((ConvexShape)_objectB.Shape).GetSupportPointNormalized(directionNormalized3);
		Vector3F vector3F = _objectA.Pose.ToWorldPosition(supportPointNormalized);
		Vector3F vector3F2 = _objectB.Pose.ToWorldPosition(supportPointNormalized2);
		return vector3F - vector3F2;
	}

	private void c8b5ea39fcabf2c8c09bb85856a670ee1(object c807f682152464d11dde83a885bf87124, EventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		OnChanged(ShapeChangedEventArgs.Empty);
	}

	private void ce495e81216e74908d006ffa40b560c26(object c807f682152464d11dde83a885bf87124, ShapeChangedEventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		cca2bcde369ccf37146d188412fd7de42();
		OnChanged(c7fefeb91d4632901bd52a1f245b70db1);
	}
}
