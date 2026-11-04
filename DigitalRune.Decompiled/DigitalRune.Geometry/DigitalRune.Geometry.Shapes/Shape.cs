using System;
using System.Threading;
using System.Xml.Serialization;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Defines the volume of an <see cref="T:DigitalRune.Geometry.IGeometricObject" />.
///       </summary>
[Serializable]
[XmlInclude(typeof(CircleShape))]
[XmlInclude(typeof(CapsuleShape))]
[XmlInclude(typeof(CylinderShape))]
[XmlInclude(typeof(ConvexPolyhedron))]
[XmlInclude(typeof(TriangleShape))]
[XmlInclude(typeof(EmptyShape))]
[XmlInclude(typeof(ScaledConvexShape))]
[XmlInclude(typeof(RectangleShape))]
[XmlInclude(typeof(BoxShape))]
[XmlInclude(typeof(OrthographicViewVolume))]
[XmlInclude(typeof(PlaneShape))]
[XmlInclude(typeof(PerspectiveViewVolume))]
[XmlInclude(typeof(ConeShape))]
[XmlInclude(typeof(PointShape))]
[XmlInclude(typeof(LineShape))]
[XmlInclude(typeof(RayShape))]
[XmlInclude(typeof(SphereShape))]
[XmlInclude(typeof(LineSegmentShape))]
public abstract class Shape : ICloneable
{
	private EventHandler<ShapeChangedEventArgs> m_Changed;

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public abstract Vector3F InnerPoint { get; }

	/// <summary>
	///       Occurs when the shape was changed.
	///       </summary>
	public event EventHandler<ShapeChangedEventArgs> Changed
	{
		add
		{
			EventHandler<ShapeChangedEventArgs> eventHandler = this.m_Changed;
			EventHandler<ShapeChangedEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<ShapeChangedEventArgs> value2 = (EventHandler<ShapeChangedEventArgs>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref this.m_Changed, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		remove
		{
			EventHandler<ShapeChangedEventArgs> eventHandler = this.m_Changed;
			EventHandler<ShapeChangedEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<ShapeChangedEventArgs> value2 = (EventHandler<ShapeChangedEventArgs>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref this.m_Changed, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Shape.remove_Changed(EventHandler<ShapeChangedEventArgs>));
				}
				return;
			}
		}
	}

	static Shape()
	{
		TypeInitializer.Initialize(ccb48cf63175e6724337dcebf74566228.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(12, ccb48cf63175e6724337dcebf74566228.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" /> class</summary>
	protected Shape()
	{
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
	///       Creates a new <see cref="T:DigitalRune.Geometry.Shapes.Shape" /> that is a clone (deep copy) of the current instance.
	///       </summary>
	/// <returns>
	///       A new <see cref="T:DigitalRune.Geometry.Shapes.Shape" /> that is a clone (deep copy) of the current instance.
	///       </returns>
	public Shape Clone()
	{
		Shape shape = cec9b74d9244fb114116db0f3a6fbc279();
		shape.CloneCore(this);
		return shape;
	}

	private Shape cec9b74d9244fb114116db0f3a6fbc279()
	{
		Shape shape = CreateInstanceCore();
		if ((object)GetType() != shape.GetType())
		{
			string message = string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), GetType());
			throw new InvalidOperationException(message);
		}
		return shape;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" /> 
	///       derived class. 
	///       </summary>
	/// <returns>The new instance.</returns>
	protected abstract Shape CreateInstanceCore();

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected abstract void CloneCore(Shape sourceShape);

	/// <summary>
	///       Computes the axis-aligned bounding box (AABB) for this shape in local space.
	///       </summary>
	/// <returns>
	///       The AABB of the shape positioned in local space.
	///       </returns>
	public Aabb GetAabb()
	{
		return GetAabb(Vector3F.One, Pose.Identity);
	}

	/// <summary>
	///       Computes the axis-aligned bounding box (AABB) for this shape positioned in world space using
	///       the given <see cref="T:DigitalRune.Geometry.Pose" />.
	///       </summary>
	/// <param name="pose">
	///       The <see cref="T:DigitalRune.Geometry.Pose" /> of the shape. This pose defines how the shape should be positioned
	///       in world space.
	///       </param>
	/// <returns>
	///       The AABB of the shape positioned in world space.
	///       </returns>
	public Aabb GetAabb(Pose pose)
	{
		return GetAabb(Vector3F.One, pose);
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
	public abstract Aabb GetAabb(Vector3F scale, Pose pose);

	/// <summary>
	///       Gets a mesh that represents this shape.
	///       </summary>
	/// <param name="relativeDistanceThreshold">The relative distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="relativeDistanceThreshold" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="iterationLimit" /> is negative or 0.
	///       </exception>
	public TriangleMesh GetMesh(float relativeDistanceThreshold, int iterationLimit)
	{
		if (Numeric.IsLess(relativeDistanceThreshold, 0f))
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(167), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(218));
		}
		if (iterationLimit <= 0)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Shape.GetMesh(float, int));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(325), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(354));
				}
			}
		}
		float largestComponent = GetAabb().Extent.LargestComponent;
		float absoluteDistanceThreshold = largestComponent * relativeDistanceThreshold;
		return OnGetMesh(absoluteDistanceThreshold, iterationLimit);
	}

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected abstract TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit);

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
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="iterationLimit" /> is negative or 0.
	///       </exception>
	public virtual float GetVolume(float relativeError, int iterationLimit)
	{
		if (Numeric.IsLess(relativeError, 0f))
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Shape.GetVolume(float, int));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(441), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(468));
				}
			}
		}
		if (iterationLimit < 0)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(325), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(354));
				}
			}
		}
		if (iterationLimit == 0)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					return GetAabb().Volume;
				}
			}
		}
		TriangleMesh mesh = GetMesh(relativeError, iterationLimit);
		return mesh.GetVolume();
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Geometry.Shapes.Shape.Changed" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///   <see cref="T:DigitalRune.Geometry.Shapes.ShapeChangedEventArgs" /> object that provides the arguments for the event.
	///       </param>
	protected virtual void OnChanged(ShapeChangedEventArgs eventArgs)
	{
		this.m_Changed?.Invoke(this, eventArgs);
	}
}
