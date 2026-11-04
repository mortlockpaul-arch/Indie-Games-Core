using System;
using System.Collections.Generic;
using A;
using DigitalRune.Collections;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a convex hull of several shapes.
///       </summary>
[Serializable]
public class ConvexHullOfShapes : ConvexShape
{
	/// <summary>
	///       Gets the child objects.
	///       </summary>
	public NotifyingCollection<IGeometricObject> Children { get; private set; }

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint
	{
		get
		{
			int count = Children.Count;
			Vector3F zero = Vector3F.Zero;
			if (count == 0)
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
							__ldtoken(ConvexHullOfShapes.get_InnerPoint());
						}
						return zero;
					}
				}
			}
			for (int i = 0; i < count; i++)
			{
				IGeometricObject geometricObject = Children[i];
				zero += geometricObject.Pose.ToWorldPosition(geometricObject.Shape.InnerPoint);
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				return zero / count;
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.CompositeShape" /> class.
	///       </summary>
	public ConvexHullOfShapes()
	{
		Children = new NotifyingCollection<IGeometricObject>(allowNull: false, allowDuplicates: false);
		Children.CollectionChanged += cc3ef9dac86e0d10822e53f51a09cfb64;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new ConvexHullOfShapes();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		ConvexHullOfShapes convexHullOfShapes = (ConvexHullOfShapes)sourceShape;
		using List<IGeometricObject>.Enumerator enumerator = convexHullOfShapes.Children.GetEnumerator();
		while (enumerator.MoveNext())
		{
			IGeometricObject current = enumerator.Current;
			Children.Add((IGeometricObject)current.Clone());
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(ConvexHullOfShapes.CloneCore(Shape));
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
		float num = float.NegativeInfinity;
		Vector3F result = default;
		int count = Children.Count;
		for (int i = 0; i < count; i++)
		{
			Pose pose = Children[i].Pose;
			Vector3F directionNormalized2 = pose.ToLocalDirection(directionNormalized);
			Vector3F supportPointNormalized = ((ConvexShape)Children[i].Shape).GetSupportPointNormalized(directionNormalized2);
			Vector3F vector3F = pose.ToWorldPosition(supportPointNormalized);
			float num2 = Vector3F.Dot(vector3F, directionNormalized);
			if (num2 > num)
			{
				result = vector3F;
				num = num2;
			}
		}
		return result;
	}

	private void cc3ef9dac86e0d10822e53f51a09cfb64(object c807f682152464d11dde83a885bf87124, CollectionChangedEventArgs<IGeometricObject> c7fefeb91d4632901bd52a1f245b70db1)
	{
		if (c7fefeb91d4632901bd52a1f245b70db1.Action == CollectionChangedAction.Move)
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
						__ldtoken(ConvexHullOfShapes.cc3ef9dac86e0d10822e53f51a09cfb64(object, CollectionChangedEventArgs<IGeometricObject>));
					}
					return;
				}
			}
		}
		List<IGeometricObject> oldItems = c7fefeb91d4632901bd52a1f245b70db1.OldItems;
		int count = oldItems.Count;
		for (int i = 0; i < count; i++)
		{
			IGeometricObject geometricObject = oldItems[i];
			geometricObject.PoseChanged -= c8b5ea39fcabf2c8c09bb85856a670ee1;
			geometricObject.ShapeChanged -= ce495e81216e74908d006ffa40b560c26;
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			List<IGeometricObject> newItems = c7fefeb91d4632901bd52a1f245b70db1.NewItems;
			int count2 = newItems.Count;
			for (int j = 0; j < count2; j++)
			{
				IGeometricObject geometricObject2 = newItems[j];
				if (!(geometricObject2.Shape is ConvexShape))
				{
					throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17642));
				}
				geometricObject2.PoseChanged += c8b5ea39fcabf2c8c09bb85856a670ee1;
				geometricObject2.ShapeChanged += ce495e81216e74908d006ffa40b560c26;
			}
			OnChanged(ShapeChangedEventArgs.Empty);
			return;
		}
	}

	private void c8b5ea39fcabf2c8c09bb85856a670ee1(object c807f682152464d11dde83a885bf87124, EventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		OnChanged(ShapeChangedEventArgs.Empty);
	}

	private void ce495e81216e74908d006ffa40b560c26(object c807f682152464d11dde83a885bf87124, ShapeChangedEventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		IGeometricObject geometricObject = (IGeometricObject)c807f682152464d11dde83a885bf87124;
		if (!(geometricObject.Shape is ConvexShape))
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
						__ldtoken(ConvexHullOfShapes.ce495e81216e74908d006ffa40b560c26(object, ShapeChangedEventArgs));
					}
					throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17642));
				}
			}
		}
		OnChanged(ShapeChangedEventArgs.Empty);
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17774), Children.Count);
	}
}
