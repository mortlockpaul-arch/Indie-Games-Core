using System;
using System.Collections.Generic;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a convex hull of a set of points.
///       </summary>
public class ConvexHullOfPoints : ConvexShape
{
	private Aabb c0f54f177ec665f147b8e791c62bb1c91 = new Aabb(new Vector3F(float.NaN), new Vector3F(float.NaN));

	private Vector3F c351e7aa2aae6b81636b906506445829b = new Vector3F(float.NaN);

	private IList<Vector3F> c8b30afe1cceb3ff0094abf6661ba053a;

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint
	{
		get
		{
			if (float.IsNaN(c351e7aa2aae6b81636b906506445829b.X))
			{
				int count = c8b30afe1cceb3ff0094abf6661ba053a.Count;
				Vector3F vector3F = default;
				if (count > 0)
				{
					for (int i = 0; i < count; i++)
					{
						Vector3F vector3F2 = c8b30afe1cceb3ff0094abf6661ba053a[i];
						vector3F += vector3F2;
					}
					while (true)
					{
						switch (4)
						{
						case 0:
							continue;
						}
						break;
					}
					if (1 == 0)
					{
						__ldtoken(ConvexHullOfPoints.get_InnerPoint());
					}
					vector3F /= (float)count;
				}
				c351e7aa2aae6b81636b906506445829b = vector3F;
			}
			return c351e7aa2aae6b81636b906506445829b;
		}
	}

	/// <summary>
	///       Gets or sets the list with the points contained in the convex hull.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public IList<Vector3F> Points
	{
		get
		{
			return c8b30afe1cceb3ff0094abf6661ba053a;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (c8b30afe1cceb3ff0094abf6661ba053a == value)
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
					__ldtoken(ConvexHullOfPoints.set_Points(IList<Vector3F>));
				}
				c8b30afe1cceb3ff0094abf6661ba053a = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.ConvexHullOfPoints" /> class.
	///       </summary>
	public ConvexHullOfPoints()
	{
		c8b30afe1cceb3ff0094abf6661ba053a = new List<Vector3F>();
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.ConvexHullOfPoints" /> class from a sequence
	///       of points.
	///       </summary>
	/// <param name="points">
	///       A collection of points which are copied into the <see cref="P:DigitalRune.Geometry.Shapes.ConvexHullOfPoints.Points" /> list.
	///       </param>
	public ConvexHullOfPoints(IEnumerable<Vector3F> points)
	{
		c8b30afe1cceb3ff0094abf6661ba053a = new List<Vector3F>();
		if (points == null)
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
				__ldtoken(ConvexHullOfPoints._002Ector(IEnumerable<Vector3F>));
			}
			IEnumerator<Vector3F> enumerator = points.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Vector3F current = enumerator.Current;
					c8b30afe1cceb3ff0094abf6661ba053a.Add(current);
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						return;
					}
				}
			}
			finally
			{
				if (enumerator != null)
				{
					while (true)
					{
						switch (7)
						{
						case 0:
							continue;
						}
						enumerator.Dispose();
						break;
					}
				}
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.ConvexHullOfPoints" /> class from a list of 
	///       points.
	///       </summary>
	/// <param name="points">
	///       The point list. A reference to this list is stored in <see cref="P:DigitalRune.Geometry.Shapes.ConvexHullOfPoints.Points" />. 
	///       The list is not copied.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	public ConvexHullOfPoints(IList<Vector3F> points)
	{
		if (points == null)
		{
			throw new ArgumentNullException();
		}
		c8b30afe1cceb3ff0094abf6661ba053a = points;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new ConvexHullOfPoints();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		ConvexHullOfPoints convexHullOfPoints = (ConvexHullOfPoints)sourceShape;
		int count = convexHullOfPoints.Points.Count;
		for (int i = 0; i < count; i++)
		{
			c8b30afe1cceb3ff0094abf6661ba053a.Add(convexHullOfPoints.c8b30afe1cceb3ff0094abf6661ba053a[i]);
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
				__ldtoken(ConvexHullOfPoints.CloneCore(Shape));
			}
			return;
		}
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
		if (float.IsNaN(c0f54f177ec665f147b8e791c62bb1c91.Minimum.X))
		{
			c0f54f177ec665f147b8e791c62bb1c91 = base.GetAabb(Vector3F.One, Pose.Identity);
		}
		return c0f54f177ec665f147b8e791c62bb1c91.GetAabb(scale, pose);
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
		return c905cf9f7e74e359a03b97af77119fee0(ref direction);
	}

	/// <summary>
	///       Gets a support point for a given normalized direction vector.
	///       </summary>
	/// <param name="directionNormalized">
	///       The normalized direction vector for which to get the support point. 
	///       </param>
	/// <returns>A support point regarding the given direction.</returns>
	public override Vector3F GetSupportPointNormalized(Vector3F directionNormalized)
	{
		return c905cf9f7e74e359a03b97af77119fee0(ref directionNormalized);
	}

	private Vector3F c905cf9f7e74e359a03b97af77119fee0(ref Vector3F c75c28b357dedcad9a44abd406f2361f3)
	{
		Vector3F result = default;
		float num = float.NegativeInfinity;
		int count = c8b30afe1cceb3ff0094abf6661ba053a.Count;
		for (int i = 0; i < count; i++)
		{
			Vector3F vector3F = c8b30afe1cceb3ff0094abf6661ba053a[i];
			float num2 = Vector3F.Dot(vector3F, c75c28b357dedcad9a44abd406f2361f3);
			if (num2 > num)
			{
				result = vector3F;
				num = num2;
			}
		}
		return result;
	}

	/// <summary>
	///       Invalidates this instance.
	///       </summary>
	public void Invalidate()
	{
		OnChanged(ShapeChangedEventArgs.Empty);
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Geometry.Shapes.Shape.Changed" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///   <see cref="T:DigitalRune.Geometry.Shapes.ShapeChangedEventArgs" /> object that provides the arguments for the event.
	///       </param>
	protected override void OnChanged(ShapeChangedEventArgs eventArgs)
	{
		c0f54f177ec665f147b8e791c62bb1c91 = new Aabb(new Vector3F(float.NaN), new Vector3F(float.NaN));
		c351e7aa2aae6b81636b906506445829b = new Vector3F(float.NaN);
		base.OnChanged(eventArgs);
	}

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected override TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit)
	{
		DcelMesh dcelMesh = GeometryHelper.CreateConvexHull(Points);
		return dcelMesh.ToTriangleMesh();
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8744), c8b30afe1cceb3ff0094abf6661ba053a.Count);
	}
}
