using System;
using System.Xml.Serialization;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a box centered at the origin.
///       </summary>
[Serializable]
public class BoxShape : ConvexShape
{
	private float _widthX;

	private float _widthY;

	private float _widthZ;

	/// <summary>
	///       Gets or sets the extent vector.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       A component of <paramref name="value" /> is negative.
	///       </exception>
	[XmlIgnore]
	public Vector3F Extent
	{
		get
		{
			return new Vector3F(_widthX, _widthY, _widthZ);
		}
		set
		{
			if (!(value.X < 0f) && !(value.Y < 0f))
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(BoxShape.set_Extent(Vector3F));
				}
				if (!(value.Z < 0f))
				{
					if (_widthX == value.X)
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
						if (_widthY == value.Y)
						{
							if (_widthZ == value.Z)
							{
								return;
							}
							while (true)
							{
								switch (6)
								{
								case 0:
									continue;
								}
								break;
							}
						}
					}
					_widthX = value.X;
					_widthY = value.Y;
					_widthZ = value.Z;
					OnChanged(ShapeChangedEventArgs.Empty);
					return;
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
			}
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7074));
		}
	}

	/// <summary>
	///       Gets an inner point (center of box).
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
					switch (3)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(BoxShape.set_WidthX(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7185));
					}
				}
			}
			if (_widthX == value)
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
				_widthX = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
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
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(BoxShape.set_WidthY(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7185));
					}
				}
			}
			if (_widthY != value)
			{
				_widthY = value;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the width along the z-axis.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float WidthZ
	{
		get
		{
			return _widthZ;
		}
		set
		{
			if (value < 0f)
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
							__ldtoken(BoxShape.set_WidthZ(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7185));
					}
				}
			}
			if (_widthZ == value)
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
				_widthZ = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.BoxShape" /> class.
	///       </summary>
	public BoxShape()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.BoxShape" /> class from the given extent vector.
	///       </summary>
	/// <param name="extent">The extent of the box.</param>
	public BoxShape(Vector3F extent)
		: this(extent.X, extent.Y, extent.Z)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.BoxShape" /> class with the given size.
	///       </summary>
	/// <param name="widthX">The width along the x-axis.</param>
	/// <param name="widthY">The width along the y-axis.</param>
	/// <param name="widthZ">The width along the z-axis.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="widthX" />, <paramref name="widthY" />, or <paramref name="widthZ" /> is 
	///       negative.
	///       </exception>
	public BoxShape(float widthX, float widthY, float widthZ)
	{
		if (widthX < 0f)
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
						__ldtoken(BoxShape._002Ector(float, float, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7369), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7185));
				}
			}
		}
		if (widthY < 0f)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7382), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7185));
				}
			}
		}
		if (widthZ < 0f)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7395), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7185));
				}
			}
		}
		_widthX = widthX;
		_widthY = widthY;
		_widthZ = widthZ;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new BoxShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		BoxShape boxShape = (BoxShape)sourceShape;
		_widthX = boxShape.WidthX;
		_widthY = boxShape.WidthY;
		_widthZ = boxShape.WidthZ;
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
		Vector3F vector3F = new Vector3F(_widthX / 2f, _widthY / 2f, _widthZ / 2f) * Vector3F.Absolute(scale);
		if (pose == Pose.Identity)
		{
			return new Aabb(-vector3F, vector3F);
		}
		Matrix33F orientation = pose.Orientation;
		Vector3F row = orientation.GetRow(0);
		Vector3F row2 = orientation.GetRow(1);
		Vector3F row3 = orientation.GetRow(2);
		row = Vector3F.Absolute(row);
		row2 = Vector3F.Absolute(row2);
		row3 = Vector3F.Absolute(row3);
		Vector3F vector3F2 = new Vector3F(Vector3F.Dot(vector3F, row), Vector3F.Dot(vector3F, row2), Vector3F.Dot(vector3F, row3));
		return new Aabb(pose.Position - vector3F2, pose.Position + vector3F2);
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
		return new Vector3F
		{
			X = ((direction.X >= 0f) ? (_widthX / 2f) : ((0f - _widthX) / 2f)),
			Y = ((direction.Y >= 0f) ? (_widthY / 2f) : ((0f - _widthY) / 2f)),
			Z = ((direction.Z >= 0f) ? (_widthZ / 2f) : ((0f - _widthZ) / 2f))
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
			Z = ((directionNormalized.Z >= 0f) ? (_widthZ / 2f) : ((0f - _widthZ) / 2f))
		};
	}

	internal LineSegment c9fec5a4b39e0cab537d06f7eb7d3cb90(int c7d96080eb2f6c52630b4332d78a61022, Vector3F cac61a7506efc9a0a2646433d050913a7, Vector3F ceabb47d7d959545d89c4300eabbdd7c2)
	{
		ceabb47d7d959545d89c4300eabbdd7c2 = Vector3F.Absolute(ceabb47d7d959545d89c4300eabbdd7c2);
		int num = ((!(cac61a7506efc9a0a2646433d050913a7.X < 0f)) ? 1 : (-1));
		int num2 = ((!(cac61a7506efc9a0a2646433d050913a7.Y < 0f)) ? 1 : (-1));
		int num3 = ((!(cac61a7506efc9a0a2646433d050913a7.Z < 0f)) ? 1 : (-1));
		Vector3F vector3F = new Vector3F((float)num * WidthX / 2f * ceabb47d7d959545d89c4300eabbdd7c2.X, (float)num2 * WidthY / 2f * ceabb47d7d959545d89c4300eabbdd7c2.Y, (float)num3 * WidthZ / 2f * ceabb47d7d959545d89c4300eabbdd7c2.Z);
		Vector3F end = vector3F;
		switch (c7d96080eb2f6c52630b4332d78a61022)
		{
		case 0:
			end.X = 0f - end.X;
			break;
		case 1:
			end.Y = 0f - end.Y;
			break;
		default:
			end.Z = 0f - end.Z;
			break;
		}
		return new LineSegment(vector3F, end);
	}

	/// <summary>
	///       Gets the volume of this box.
	///       </summary>
	/// <returns>The volume of this box.</returns>
	public float GetVolume()
	{
		return _widthX * _widthY * _widthZ;
	}

	/// <summary>
	///       Gets the volume of this box.
	///       </summary>
	/// <param name="relativeError">Not used.</param>
	/// <param name="iterationLimit">Not used.</param>
	/// <returns>The volume of this box.</returns>
	public override float GetVolume(float relativeError, int iterationLimit)
	{
		return GetVolume();
	}

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected override TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit)
	{
		float num = _widthX / 2f;
		float num2 = _widthY / 2f;
		float num3 = _widthZ / 2f;
		TriangleMesh triangleMesh = new TriangleMesh();
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(0f - num, 0f - num2, num3),
			Vertex1 = new Vector3F(0f - num, 0f - num2, 0f - num3),
			Vertex2 = new Vector3F(num, 0f - num2, 0f - num3)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num, 0f - num2, 0f - num3),
			Vertex1 = new Vector3F(num, 0f - num2, num3),
			Vertex2 = new Vector3F(0f - num, 0f - num2, num3)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num, num2, num3),
			Vertex1 = new Vector3F(num, 0f - num2, num3),
			Vertex2 = new Vector3F(num, 0f - num2, 0f - num3)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num, 0f - num2, 0f - num3),
			Vertex1 = new Vector3F(num, num2, 0f - num3),
			Vertex2 = new Vector3F(num, num2, num3)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num, num2, 0f - num3),
			Vertex1 = new Vector3F(num, 0f - num2, 0f - num3),
			Vertex2 = new Vector3F(0f - num, 0f - num2, 0f - num3)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(0f - num, 0f - num2, 0f - num3),
			Vertex1 = new Vector3F(0f - num, num2, 0f - num3),
			Vertex2 = new Vector3F(num, num2, 0f - num3)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(0f - num, num2, 0f - num3),
			Vertex1 = new Vector3F(0f - num, 0f - num2, 0f - num3),
			Vertex2 = new Vector3F(0f - num, 0f - num2, num3)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(0f - num, 0f - num2, num3),
			Vertex1 = new Vector3F(0f - num, num2, num3),
			Vertex2 = new Vector3F(0f - num, num2, 0f - num3)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(0f - num, num2, num3),
			Vertex1 = new Vector3F(0f - num, 0f - num2, num3),
			Vertex2 = new Vector3F(num, 0f - num2, num3)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num, 0f - num2, num3),
			Vertex1 = new Vector3F(num, num2, num3),
			Vertex2 = new Vector3F(0f - num, num2, num3)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(0f - num, num2, 0f - num3),
			Vertex1 = new Vector3F(0f - num, num2, num3),
			Vertex2 = new Vector3F(num, num2, num3)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num, num2, num3),
			Vertex1 = new Vector3F(num, num2, 0f - num3),
			Vertex2 = new Vector3F(0f - num, num2, 0f - num3)
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
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7276), _widthX, _widthY, _widthZ);
	}
}
