using System;
using System.Xml.Serialization;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a rectangle in the xy-plane centered at the origin.
///       </summary>
[Serializable]
public class RectangleShape : ConvexShape
{
	private float _widthX;

	private float _widthY;

	/// <summary>
	///       Gets or sets the extent vector.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       A component of <paramref name="value" /> is negative.
	///       </exception>
	[XmlIgnore]
	public Vector2F Extent
	{
		get
		{
			return new Vector2F(_widthX, _widthY);
		}
		set
		{
			if (!(value.X < 0f))
			{
				if (!(value.Y < 0f))
				{
					if (_widthX != value.X || _widthY != value.Y)
					{
						_widthX = value.X;
						_widthY = value.Y;
						OnChanged(ShapeChangedEventArgs.Empty);
					}
					return;
				}
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
					__ldtoken(RectangleShape.set_Extent(Vector2F));
				}
			}
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(16798));
		}
	}

	/// <summary>
	///       Gets an inner point (center of rectangle).
	///       </summary>
	public override Vector3F InnerPoint => Vector3F.Zero;

	/// <summary>
	///       Gets or sets the width along the x-axis.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float WidthX
	{
		get
		{
			return _widthX;
		}
		set
		{
			if (value < 0f)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(RectangleShape.set_WidthX(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7185));
					}
				}
			}
			if (_widthX != value)
			{
				_widthX = value;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the width along the y-axis.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float WidthY
	{
		get
		{
			return _widthY;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7185));
			}
			if (_widthY != value)
			{
				_widthY = value;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.RectangleShape" /> class.
	///       </summary>
	public RectangleShape()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.RectangleShape" /> class with the given extent.
	///       </summary>
	/// <param name="extent">The extent vector.</param>
	public RectangleShape(Vector2F extent)
		: this(extent.X, extent.Y)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.RectangleShape" /> class with the given width
	///       and height.
	///       </summary>
	/// <param name="widthX">The width along the x-axis.</param>
	/// <param name="widthY">The width along the y-axis.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="widthX" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="widthY" /> is negative.
	///       </exception>
	public RectangleShape(float widthX, float widthY)
	{
		if (widthX < 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7369), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7185));
		}
		if (widthY < 0f)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(RectangleShape._002Ector(float, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7382), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7185));
				}
			}
		}
		_widthX = widthX;
		_widthY = widthY;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new RectangleShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		RectangleShape rectangleShape = (RectangleShape)sourceShape;
		_widthX = rectangleShape.WidthX;
		_widthY = rectangleShape.WidthY;
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
		Vector3F vector = new Vector3F(_widthX / 2f, _widthY / 2f, 0f) * Vector3F.Absolute(scale);
		Matrix33F orientation = pose.Orientation;
		Vector3F row = orientation.GetRow(0);
		Vector3F row2 = orientation.GetRow(1);
		Vector3F row3 = orientation.GetRow(2);
		row = Vector3F.Absolute(row);
		row2 = Vector3F.Absolute(row2);
		row3 = Vector3F.Absolute(row3);
		Vector3F vector3F = new Vector3F(Vector3F.Dot(vector, row), Vector3F.Dot(vector, row2), Vector3F.Dot(vector, row3));
		return new Aabb(pose.Position - vector3F, pose.Position + vector3F);
	}

	/// <summary>
	///       Gets a support point for a given direction.
	///       </summary>
	/// <param name="direction">
	///       The direction for which to get the support point. The vector does not need to be normalized.
	///       The result is undefined if the vector is a zero vector.</param>
	/// <returns>
	///       A support point regarding the given direction.
	///       </returns>
	public override Vector3F GetSupportPoint(Vector3F direction)
	{
		return new Vector3F
		{
			X = ((direction.X >= 0f) ? (_widthX / 2f) : ((0f - _widthX) / 2f)),
			Y = ((direction.Y >= 0f) ? (_widthY / 2f) : ((0f - _widthY) / 2f)),
			Z = 0f
		};
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
		return new Vector3F
		{
			X = ((directionNormalized.X >= 0f) ? (_widthX / 2f) : ((0f - _widthX) / 2f)),
			Y = ((directionNormalized.Y >= 0f) ? (_widthY / 2f) : ((0f - _widthY) / 2f)),
			Z = 0f
		};
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <param name="relativeError">Not used.</param>
	/// <param name="iterationLimit">Not used</param>
	/// <returns>0</returns>
	public override float GetVolume(float relativeError, int iterationLimit)
	{
		return 0f;
	}

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected override TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit)
	{
		TriangleMesh triangleMesh = new TriangleMesh();
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(_widthX / 2f, _widthY / 2f, 0f),
			Vertex1 = new Vector3F((0f - _widthX) / 2f, _widthY / 2f, 0f),
			Vertex2 = new Vector3F((0f - _widthX) / 2f, (0f - _widthY) / 2f, 0f)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F((0f - _widthX) / 2f, (0f - _widthY) / 2f, 0f),
			Vertex1 = new Vector3F(_widthX / 2f, (0f - _widthY) / 2f, 0f),
			Vertex2 = new Vector3F(_widthX / 2f, _widthY / 2f, 0f)
		}, weldVerticesBruteForce: true);
		return triangleMesh;
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(16921), _widthX, _widthY);
	}
}
