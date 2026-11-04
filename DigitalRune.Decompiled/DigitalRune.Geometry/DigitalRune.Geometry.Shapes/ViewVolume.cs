using System;
using A;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a view volume (base implementation).
///       </summary>
[Serializable]
public abstract class ViewVolume : ConvexShape
{
	private float _left;

	private float _right;

	private float _bottom;

	private float _top;

	private float _near;

	private float _far;

	/// <summary>
	///       Gets or sets the minimum x-value of the view volume at the near clip plane.
	///       </summary>
	public float Left
	{
		get
		{
			return _left;
		}
		set
		{
			if (_left != value)
			{
				_left = value;
				Update();
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximum x-value of the view volume at the near clip plane.
	///       </summary>
	public float Right
	{
		get
		{
			return _right;
		}
		set
		{
			if (_right != value)
			{
				_right = value;
				Update();
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the minimum y-value of the view volume at the near clip plane.
	///       </summary>
	public float Bottom
	{
		get
		{
			return _bottom;
		}
		set
		{
			if (_bottom != value)
			{
				_bottom = value;
				Update();
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximum y-value of the view volume at the near clip plane.
	///       </summary>
	public float Top
	{
		get
		{
			return _top;
		}
		set
		{
			if (_top != value)
			{
				_top = value;
				Update();
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the distance to the near clip plane. 
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The near plane distance must not be negative or 0.
	///       </exception>
	public float Near
	{
		get
		{
			return _near;
		}
		set
		{
			if (value <= 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9593));
			}
			if (_near != value)
			{
				_near = value;
				Update();
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the distance to the far clip plane. 
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The far plane distance must not be negative or 0.
	///       </exception>
	public float Far
	{
		get
		{
			return _far;
		}
		set
		{
			if (value <= 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9593));
			}
			if (_far == value)
			{
				return;
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(ViewVolume.set_Far(float));
				}
				_far = value;
				Update();
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets the width of the view volume at the near clip plane.
	///       </summary>
	public float Width => Math.Abs(Right - Left);

	/// <summary>
	///       Gets the height of the view volume at the near clip plane.
	///       </summary>
	public float Height => Math.Abs(Top - Bottom);

	/// <summary>
	///       Gets the depth of the view volume (= <see cref="P:DigitalRune.Geometry.Shapes.ViewVolume.Far" /> - <see cref="P:DigitalRune.Geometry.Shapes.ViewVolume.Near" />).
	///       </summary>
	public float Depth => Math.Abs(Far - Near);

	/// <summary>
	///       Gets the aspect ratio (width / height).
	///       </summary>
	public float AspectRatio => Width / Height;

	/// <summary>
	///       Gets the horizontal field of view.
	///       </summary>
	public abstract float FieldOfViewX { get; }

	/// <summary>
	///       Gets the vertical field of view.
	///       </summary>
	public abstract float FieldOfViewY { get; }

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.ViewVolume" /> class</summary>
	protected ViewVolume()
	{
	}

	/// <summary>
	///       Sets the width and height of the view volume to the specified size and depth.
	///       </summary>
	/// <param name="width">The width of the view volume at the near clip plane.</param>
	/// <param name="height">The height of the view volume at the near clip plane.</param>
	/// <param name="near">The distance to the near clip plane.</param>
	/// <param name="far">The distance to the far clip plane.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="width" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="height" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is greater than or equal to <paramref name="far" />.
	///       </exception>
	public void SetWidthAndHeight(float width, float height, float near, float far)
	{
		if (near <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9731), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9593));
		}
		if (near >= far)
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
						__ldtoken(ViewVolume.SetWidthAndHeight(float, float, float, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9731), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9740));
				}
			}
		}
		_near = near;
		_far = far;
		SetWidthAndHeight(width, height);
	}

	/// <summary>
	///       Sets the width and height of the view volume to the specified size.
	///       </summary>
	/// <param name="width">The width of the view volume at the near clip plane.</param>
	/// <param name="height">The height of the view volume at the near clip plane.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="width" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="height" /> is negative.
	///       </exception>
	public void SetWidthAndHeight(float width, float height)
	{
		if (width <= 0f)
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
						__ldtoken(ViewVolume.SetWidthAndHeight(float, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9914), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9925));
				}
			}
		}
		if (height <= 0f)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4813), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10030));
				}
			}
		}
		float num = width / 2f;
		float num2 = height / 2f;
		_left = 0f - num;
		_right = num;
		_bottom = 0f - num2;
		_top = num2;
		Update();
	}

	/// <summary>
	///       Sets the dimensions of the view volume (including depths).
	///       </summary>
	/// <param name="left">The minimum x-value of the view volume at the near clip plane.</param>
	/// <param name="right">The maximum x-value of the view volume at the near clip plane.</param>
	/// <param name="bottom">The minimum y-value of the view volume at the near clip plane.</param>
	/// <param name="top">The maximum y-value of the view volume at the near clip plane.</param>
	/// <param name="near">The distance to the near clip plane.</param>
	/// <param name="far">The distance to the far clip plane.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="left" /> is greater than or equal to <paramref name="right" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="bottom" /> is greater than or equal to <paramref name="top" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="near" /> is greater than or equal to <paramref name="far" />.
	///       </exception>
	public void Set(float left, float right, float bottom, float top, float near, float far)
	{
		if (near <= 0f)
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
						__ldtoken(ViewVolume.Set(float, float, float, float, float, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9731), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9593));
				}
			}
		}
		if (near >= far)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9731), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9740));
				}
			}
		}
		_near = near;
		_far = far;
		Set(left, right, bottom, top);
	}

	/// <summary>
	///       Sets the dimensions of the view volume.
	///       </summary>
	/// <param name="left">The minimum x-value of the view volume at the near clip plane.</param>
	/// <param name="right">The maximum x-value of the view volume at the near clip plane.</param>
	/// <param name="bottom">The minimum y-value of the view volume at the near clip plane.</param>
	/// <param name="top">The maximum y-value of the view volume at the near clip plane.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="left" /> is greater than or equal to <paramref name="right" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="bottom" /> is greater than or equal to <paramref name="top" />.
	///       </exception>
	public void Set(float left, float right, float bottom, float top)
	{
		if (left >= right)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10137), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10146));
		}
		if (bottom >= top)
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
						__ldtoken(ViewVolume.Set(float, float, float, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10213), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10226));
				}
			}
		}
		_left = left;
		_right = right;
		_bottom = bottom;
		_top = top;
		Update();
	}

	/// <summary>
	///       Updates the shape.
	///       </summary>
	protected abstract void Update();
}
