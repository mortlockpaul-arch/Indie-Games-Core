using System;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a perspective view volume (frustum).
///       </summary>
[Serializable]
public class PerspectiveViewVolume : ViewVolume
{
	private Vector3F _nearBottomLeftVertex;

	private Vector3F _nearBottomRightVertex;

	private Vector3F _nearTopLeftVertex;

	private Vector3F _nearTopRightVertex;

	private Vector3F _farBottomLeftVertex;

	private Vector3F _farBottomRightVertex;

	private Vector3F _farTopLeftVertex;

	private Vector3F _farTopRightVertex;

	private Vector3F _innerPoint;

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint => _innerPoint;

	/// <summary>
	///       Gets the horizontal field of view.
	///       </summary>
	public override float FieldOfViewX
	{
		get
		{
			float num;
			float num2;
			if (Left <= Right)
			{
				num = Left;
				num2 = Right;
			}
			else
			{
				num = Right;
				num2 = Left;
			}
			float num3;
			if (!(Near <= Far))
			{
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
					__ldtoken(PerspectiveViewVolume.get_FieldOfViewX());
				}
				num3 = Far;
			}
			else
			{
				num3 = Near;
			}
			float num4 = num3;
			return (float)Math.Atan((0f - num) / num4) + (float)Math.Atan(num2 / num4);
		}
	}

	/// <summary>
	///       Gets the vertical field of view.
	///       </summary>
	public override float FieldOfViewY
	{
		get
		{
			float num;
			float num2;
			if (Bottom <= Top)
			{
				num = Bottom;
				num2 = Top;
			}
			else
			{
				num = Top;
				num2 = Bottom;
			}
			float num3 = ((Near <= Far) ? Near : Far);
			return (float)Math.Atan((0f - num) / num3) + (float)Math.Atan(num2 / num3);
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.PerspectiveViewVolume" /> class using default 
	///       settings.
	///       </summary>
	public PerspectiveViewVolume()
	{
		SetWidthAndHeight(2f, 2f, 1f, 4f);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.PerspectiveViewVolume" /> class with the given
	///       field of view and depth.
	///       </summary>
	/// <param name="fieldOfViewY">The vertical field of view.</param>
	/// <param name="aspectRatio">The aspect ratio (width / height).</param>
	/// <param name="near">The distance to the near clip plane.</param>
	/// <param name="far">The distance to the far clip plane.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="fieldOfViewY" /> is not between 0 and <c>π</c> radians (0° and 180°).
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="aspectRatio" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is greater than or equal to <paramref name="far" /></exception>
	public PerspectiveViewVolume(float fieldOfViewY, float aspectRatio, float near, float far)
	{
		SetFieldOfView(fieldOfViewY, aspectRatio, near, far);
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new PerspectiveViewVolume();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		PerspectiveViewVolume perspectiveViewVolume = (PerspectiveViewVolume)sourceShape;
		Set(perspectiveViewVolume.Left, perspectiveViewVolume.Right, perspectiveViewVolume.Bottom, perspectiveViewVolume.Top, perspectiveViewVolume.Near, perspectiveViewVolume.Far);
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
		if (direction.X > 0f)
		{
			if (direction.Y > 0f)
			{
				if (Vector3F.Dot(_nearTopRightVertex, direction) > Vector3F.Dot(_farTopRightVertex, direction))
				{
					return _nearTopRightVertex;
				}
				return _farTopRightVertex;
			}
			if (Vector3F.Dot(_nearBottomRightVertex, direction) > Vector3F.Dot(_farBottomRightVertex, direction))
			{
				return _nearBottomRightVertex;
			}
			return _farBottomRightVertex;
		}
		if (direction.Y > 0f)
		{
			if (Vector3F.Dot(_nearTopLeftVertex, direction) > Vector3F.Dot(_farTopLeftVertex, direction))
			{
				return _nearTopLeftVertex;
			}
			return _farTopLeftVertex;
		}
		if (Vector3F.Dot(_nearBottomLeftVertex, direction) > Vector3F.Dot(_farBottomLeftVertex, direction))
		{
			return _nearBottomLeftVertex;
		}
		return _farBottomLeftVertex;
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
		if (directionNormalized.X > 0f)
		{
			if (directionNormalized.Y > 0f)
			{
				if (Vector3F.Dot(_nearTopRightVertex, directionNormalized) > Vector3F.Dot(_farTopRightVertex, directionNormalized))
				{
					return _nearTopRightVertex;
				}
				return _farTopRightVertex;
			}
			if (Vector3F.Dot(_nearBottomRightVertex, directionNormalized) > Vector3F.Dot(_farBottomRightVertex, directionNormalized))
			{
				return _nearBottomRightVertex;
			}
			return _farBottomRightVertex;
		}
		if (directionNormalized.Y > 0f)
		{
			if (Vector3F.Dot(_nearTopLeftVertex, directionNormalized) > Vector3F.Dot(_farTopLeftVertex, directionNormalized))
			{
				return _nearTopLeftVertex;
			}
			return _farTopLeftVertex;
		}
		if (Vector3F.Dot(_nearBottomLeftVertex, directionNormalized) > Vector3F.Dot(_farBottomLeftVertex, directionNormalized))
		{
			return _nearBottomLeftVertex;
		}
		return _farBottomLeftVertex;
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <returns>The volume of this shape.</returns>
	public float GetVolume()
	{
		float num = Width * Height;
		float num2 = Far / Near;
		float num3 = num * num2 * num2;
		return 1f / 3f * (num3 * Far - num * Near);
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <param name="relativeError">Not used.</param>
	/// <param name="iterationLimit">Not used.</param>
	/// <returns>The volume of this shape.</returns>
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
		float num = 0f - Math.Min(Near, Far);
		float num2 = 0f - Math.Max(Near, Far);
		float num3 = Math.Min(Left, Right);
		float num4 = Math.Max(Left, Right);
		float num5 = Math.Max(Top, Bottom);
		float num6 = Math.Min(Top, Bottom);
		float num7 = 1f / num * num2;
		float x = num3 * num7;
		float x2 = num4 * num7;
		float y = num5 * num7;
		float y2 = num6 * num7;
		TriangleMesh triangleMesh = new TriangleMesh();
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num3, num6, num),
			Vertex1 = new Vector3F(x, y2, num2),
			Vertex2 = new Vector3F(x2, y2, num2)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x2, y2, num2),
			Vertex1 = new Vector3F(num4, num6, num),
			Vertex2 = new Vector3F(num3, num6, num)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num4, num5, num),
			Vertex1 = new Vector3F(num4, num6, num),
			Vertex2 = new Vector3F(x2, y2, num2)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x2, y2, num2),
			Vertex1 = new Vector3F(x2, y, num2),
			Vertex2 = new Vector3F(num4, num5, num)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x2, y, num2),
			Vertex1 = new Vector3F(x2, y2, num2),
			Vertex2 = new Vector3F(x, y2, num2)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x, y2, num2),
			Vertex1 = new Vector3F(x, y, num2),
			Vertex2 = new Vector3F(x2, y, num2)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x, y, num2),
			Vertex1 = new Vector3F(x, y2, num2),
			Vertex2 = new Vector3F(num3, num6, num)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num3, num6, num),
			Vertex1 = new Vector3F(num3, num5, num),
			Vertex2 = new Vector3F(x, y, num2)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num3, num5, num),
			Vertex1 = new Vector3F(num3, num6, num),
			Vertex2 = new Vector3F(num4, num6, num)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num4, num6, num),
			Vertex1 = new Vector3F(num4, num5, num),
			Vertex2 = new Vector3F(num3, num5, num)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(x, y, num2),
			Vertex1 = new Vector3F(num3, num5, num),
			Vertex2 = new Vector3F(num4, num5, num)
		}, weldVerticesBruteForce: true);
		triangleMesh.Add(new Triangle
		{
			Vertex0 = new Vector3F(num4, num5, num),
			Vertex1 = new Vector3F(x2, y, num2),
			Vertex2 = new Vector3F(x, y, num2)
		}, weldVerticesBruteForce: true);
		return triangleMesh;
	}

	/// <summary>
	///       Updates the shape.
	///       </summary>
	protected override void Update()
	{
		float num;
		float num2;
		if (Left <= Right)
		{
			num = Left;
			num2 = Right;
		}
		else
		{
			num = Right;
			num2 = Left;
		}
		float num3;
		float num4;
		if (Bottom <= Top)
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
				__ldtoken(PerspectiveViewVolume.Update());
			}
			num3 = Bottom;
			num4 = Top;
		}
		else
		{
			num3 = Top;
			num4 = Bottom;
		}
		float num5;
		float num6;
		if (Near <= Far)
		{
			num5 = Near;
			num6 = Far;
		}
		else
		{
			num5 = Far;
			num6 = Near;
		}
		_nearBottomLeftVertex = new Vector3F(num, num3, 0f - num5);
		_nearBottomRightVertex = new Vector3F(num2, num3, 0f - num5);
		_nearTopLeftVertex = new Vector3F(num, num4, 0f - num5);
		_nearTopRightVertex = new Vector3F(num2, num4, 0f - num5);
		float num7 = num6 / num5;
		num *= num7;
		num2 *= num7;
		num3 *= num7;
		num4 *= num7;
		_farBottomLeftVertex = new Vector3F(num, num3, 0f - num6);
		_farBottomRightVertex = new Vector3F(num2, num3, 0f - num6);
		_farTopLeftVertex = new Vector3F(num, num4, 0f - num6);
		_farTopRightVertex = new Vector3F(num2, num4, 0f - num6);
		_innerPoint = new Vector3F(num + num2, num3 + num4, 0f - num5 - num6) * 0.5f;
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14451), Left, Right, Bottom, Top, Near, Far);
	}

	/// <summary>
	///       Sets the dimensions of the frustum to the specified field of view and near/far values.
	///       </summary>
	/// <param name="fieldOfViewY">The vertical field of view.</param>
	/// <param name="aspectRatio">The aspect ratio (width / height).</param>
	/// <param name="near">The distance to the near clip plane.</param>
	/// <param name="far">The distance to the far clip plane.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="fieldOfViewY" /> is not between 0 and <c>π</c> radians (0° and 180°).
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="aspectRatio" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is greater than or equal to <paramref name="far" /></exception>
	public void SetFieldOfView(float fieldOfViewY, float aspectRatio, float near, float far)
	{
		if (near <= 0f)
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
						__ldtoken(PerspectiveViewVolume.SetFieldOfView(float, float, float, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9731), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14621));
				}
			}
		}
		if (near >= far)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9731), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14751));
		}
		GetWidthAndHeight(fieldOfViewY, aspectRatio, near, out var width, out var height);
		SetWidthAndHeight(width, height, near, far);
	}

	/// <summary>
	///       Sets the dimensions of the frustum to the specified field of view.
	///       </summary>
	/// <param name="fieldOfViewY">The vertical field of view.</param>
	/// <param name="aspectRatio">The aspect ratio (width / height).</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="fieldOfViewY" /> is not between 0 and <c>π</c> radians (0° and 180°).
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="aspectRatio" /> is negative or 0.
	///       </exception>
	public void SetFieldOfView(float fieldOfViewY, float aspectRatio)
	{
		GetWidthAndHeight(fieldOfViewY, aspectRatio, Near, out var width, out var height);
		SetWidthAndHeight(width, height);
	}

	/// <summary>
	///       Converts the vertical field of view of a symmetric frustum to a horizontal field of view.
	///       </summary>
	/// <param name="fieldOfViewY">The vertical field of view in radians.</param>
	/// <param name="aspectRatio">The aspect ratio (width / height).</param>
	/// <returns>The horizontal field of view in radians.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="fieldOfViewY" /> is not between 0 and <c>π</c> radians (0° and 180°).
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="aspectRatio" /> is negative or 0.
	///       </exception>
	public static float GetFieldOfViewX(float fieldOfViewY, float aspectRatio)
	{
		if (!(fieldOfViewY <= 0f))
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
				__ldtoken(PerspectiveViewVolume.GetFieldOfViewX(float, float));
			}
			if (!(fieldOfViewY >= (float)Math.PI))
			{
				if (aspectRatio <= 0f)
				{
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15059), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15082));
						}
					}
				}
				float num = 2f * (float)Math.Tan(fieldOfViewY / 2f);
				float num2 = num * aspectRatio;
				return 2f * (float)Math.Atan(num2 / 2f);
			}
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14917), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14942));
	}

	/// <summary>
	///       Converts a horizontal field of view of a symmetric frustum to a vertical field of view.
	///       </summary>
	/// <param name="fieldOfViewX">The horizontal field of view in radians.</param>
	/// <param name="aspectRatio">The aspect ratio.</param>
	/// <returns>The vertical field of view in radians.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="fieldOfViewX" /> is not between 0 and <c>π</c> radians (0° and 180°).
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="aspectRatio" /> is negative or 0.
	///       </exception>
	public static float GetFieldOfViewY(float fieldOfViewX, float aspectRatio)
	{
		if (!(fieldOfViewX <= 0f))
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(PerspectiveViewVolume.GetFieldOfViewY(float, float));
			}
			if (!(fieldOfViewX >= (float)Math.PI))
			{
				if (aspectRatio <= 0f)
				{
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15059), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15082));
				}
				float num = 2f * (float)Math.Tan(fieldOfViewX / 2f);
				float num2 = num / aspectRatio;
				return 2f * (float)Math.Atan(num2 / 2f);
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
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15169), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14942));
	}

	/// <summary>
	///       Gets the extent of the frustum at the given distance.
	///       </summary>
	/// <param name="fieldOfView">The field of view in radians.</param>
	/// <param name="distance">The distance at which the extent is calculated.</param>
	/// <returns>The extent of the view volume at the given distance.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="fieldOfView" /> is not between 0 and <c>π</c> radians (0° and 180°).
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="distance" /> is negative.
	///       </exception>
	public static float GetExtent(float fieldOfView, float distance)
	{
		if (!(fieldOfView <= 0f))
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(PerspectiveViewVolume.GetExtent(float, float));
			}
			if (!(fieldOfView >= (float)Math.PI))
			{
				if (distance < 0f)
				{
					while (true)
					{
						switch (6)
						{
						case 0:
							break;
						default:
							throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15217), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15234));
						}
					}
				}
				return 2f * distance * (float)Math.Tan(fieldOfView / 2f);
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
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15194), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14942));
	}

	/// <summary>
	///       Converts a field of view of a symmetric frustum to width and height.
	///       </summary>
	/// <param name="fieldOfViewY">The vertical field of view in radians.</param>
	/// <param name="aspectRatio">The aspect ratio (width / height).</param>
	/// <param name="distance">
	///       The distance at which <paramref name="width" /> and <paramref name="height" /> are calculated.
	///       </param>
	/// <param name="width">
	///       The width of the view volume at the specified <paramref name="distance" />.
	///       </param>
	/// <param name="height">
	///       The height of the view volume at the specified <paramref name="distance" />.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="fieldOfViewY" /> is not between 0 and <c>π</c> radians (0° and 180°).
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="aspectRatio" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="distance" /> is negative.
	///       </exception>
	public static void GetWidthAndHeight(float fieldOfViewY, float aspectRatio, float distance, out float width, out float height)
	{
		if (!(fieldOfViewY <= 0f))
		{
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
				__ldtoken(PerspectiveViewVolume.GetWidthAndHeight(float, float, float, ref float, ref float));
			}
			if (!(fieldOfViewY >= (float)Math.PI))
			{
				if (aspectRatio <= 0f)
				{
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15059), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15082));
				}
				if (distance < 0f)
				{
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15217), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15234));
				}
				height = 2f * distance * (float)Math.Tan(fieldOfViewY / 2f);
				width = height * aspectRatio;
				return;
			}
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14917), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14942));
	}

	/// <summary>
	///       Gets the field of view from a frustum with the given extent.
	///       </summary>
	/// <param name="extent">
	///       The extent of the frustum at the specified <paramref name="distance" />.
	///       </param>
	/// <param name="distance">The distance.</param>
	/// <returns>The field of view for the given extent.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="extent" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="distance" /> is negative or 0.
	///       </exception>
	public static float GetFieldOfView(float extent, float distance)
	{
		if (extent < 0f)
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
						__ldtoken(PerspectiveViewVolume.GetFieldOfView(float, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15303), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15316));
				}
			}
		}
		if (distance <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15217), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15439));
		}
		return 2f * (float)Math.Atan(extent / (2f * distance));
	}
}
