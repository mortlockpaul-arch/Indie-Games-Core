using System;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a height field which can be used for simple terrains.
///       </summary>
public class HeightField : Shape
{
	private float c526d59d167cc46a092b30f68879e176e = float.NaN;

	private float c5cf26e930f4a2c66e4109032b6d7d1b8 = float.NaN;

	private float[,] c37adb342465be20dcf2c430c7d6aeb68;

	private float c92625694f9148dcf7fad2d25b6eac568;

	private float c356f65f7067c642b76c62fa0a6cd5811 = 1000f;

	private float c8ca3257fba3e46f4d65cae54a3bf7715 = 1000f;

	[CompilerGenerated]
	private bool ccb8ecdcf0b964e280753c1f55ea16a64;

	/// <summary>
	///       Gets or sets the height field.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> contains less than 2 x 2 elements.
	///       </exception>
	public float[,] Array
	{
		get
		{
			return c37adb342465be20dcf2c430c7d6aeb68;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (value.GetLength(0) >= 2)
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
					__ldtoken(HeightField.set_Array(float[,]));
				}
				if (value.GetLength(1) >= 2)
				{
					if (c37adb342465be20dcf2c430c7d6aeb68 != value)
					{
						c37adb342465be20dcf2c430c7d6aeb68 = value;
						OnChanged(ShapeChangedEventArgs.Empty);
					}
					return;
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					break;
				}
			}
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17349));
		}
	}

	/// <summary>
	///       Gets or sets the depth of the height field.
	///       </summary>
	public float Depth
	{
		get
		{
			return c92625694f9148dcf7fad2d25b6eac568;
		}
		set
		{
			if (c92625694f9148dcf7fad2d25b6eac568 == value)
			{
				return;
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
					__ldtoken(HeightField.set_Depth(float));
				}
				c92625694f9148dcf7fad2d25b6eac568 = value;
				OnChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint => new Vector3F(c356f65f7067c642b76c62fa0a6cd5811 / 2f, GetHeight(c356f65f7067c642b76c62fa0a6cd5811 / 2f, c8ca3257fba3e46f4d65cae54a3bf7715 / 2f) - c356f65f7067c642b76c62fa0a6cd5811, c8ca3257fba3e46f4d65cae54a3bf7715 / 2f);

	/// <summary>
	///       Gets or sets a value indicating whether the collision detection should use a fast, less
	///       accurate method. (Experimental)
	///       </summary>
	public bool UseFastCollisionApproximation
	{
		[CompilerGenerated]
		get
		{
			return ccb8ecdcf0b964e280753c1f55ea16a64;
		}
		[CompilerGenerated]
		set
		{
			ccb8ecdcf0b964e280753c1f55ea16a64 = value;
		}
	}

	/// <summary>
	///       Gets or sets the width along the local x-axis.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public float WidthX
	{
		get
		{
			return c356f65f7067c642b76c62fa0a6cd5811;
		}
		set
		{
			if (value <= 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17468));
			}
			if (c356f65f7067c642b76c62fa0a6cd5811 != value)
			{
				c356f65f7067c642b76c62fa0a6cd5811 = value;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Gets or sets the width along the local z-axis.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public float WidthZ
	{
		get
		{
			return c8ca3257fba3e46f4d65cae54a3bf7715;
		}
		set
		{
			if (value <= 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17468));
			}
			if (c8ca3257fba3e46f4d65cae54a3bf7715 != value)
			{
				c8ca3257fba3e46f4d65cae54a3bf7715 = value;
				OnChanged(ShapeChangedEventArgs.Empty);
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.HeightField" /> class.
	///       </summary>
	public HeightField()
	{
		float[,] array = new float[2, 2];
		c37adb342465be20dcf2c430c7d6aeb68 = array;
		c92625694f9148dcf7fad2d25b6eac568 = 100f;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.HeightField" /> class from the given array.
	///       </summary>
	/// <param name="widthX">The width along the x-axis.</param>
	/// <param name="widthZ">The width along the z-axis.</param>
	/// <param name="array">The array.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> contains less than 2 x 2 elements.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="widthX" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="widthZ" /> is negative or 0.
	///       </exception>
	public HeightField(float widthX, float widthZ, float[,] array)
	{
		if (widthX <= 0f)
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
						__ldtoken(HeightField._002Ector(float, float, float[,]));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7369), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17468));
				}
			}
		}
		if (widthZ <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7395), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17468));
		}
		if (array == null)
		{
			throw new ArgumentNullException();
		}
		if (array.GetLength(0) >= 2)
		{
			if (array.GetLength(1) >= 2)
			{
				c356f65f7067c642b76c62fa0a6cd5811 = widthX;
				c8ca3257fba3e46f4d65cae54a3bf7715 = widthZ;
				c37adb342465be20dcf2c430c7d6aeb68 = array;
				c92625694f9148dcf7fad2d25b6eac568 = 100f;
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
		}
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17349));
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new HeightField();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		HeightField heightField = (HeightField)sourceShape;
		c356f65f7067c642b76c62fa0a6cd5811 = heightField.WidthX;
		c8ca3257fba3e46f4d65cae54a3bf7715 = heightField.WidthZ;
		c92625694f9148dcf7fad2d25b6eac568 = heightField.Depth;
		c37adb342465be20dcf2c430c7d6aeb68 = (float[,])c37adb342465be20dcf2c430c7d6aeb68.Clone();
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
		if (float.IsNaN(c526d59d167cc46a092b30f68879e176e))
		{
			c526d59d167cc46a092b30f68879e176e = float.PositiveInfinity;
			c5cf26e930f4a2c66e4109032b6d7d1b8 = float.NegativeInfinity;
			float[,] array = c37adb342465be20dcf2c430c7d6aeb68;
			foreach (float val in array)
			{
				c526d59d167cc46a092b30f68879e176e = Math.Min(val, c526d59d167cc46a092b30f68879e176e);
				c5cf26e930f4a2c66e4109032b6d7d1b8 = Math.Max(val, c5cf26e930f4a2c66e4109032b6d7d1b8);
			}
		}
		Vector3F minimum = new Vector3F(0f, c526d59d167cc46a092b30f68879e176e, 0f);
		Vector3F maximum = new Vector3F(c356f65f7067c642b76c62fa0a6cd5811, c5cf26e930f4a2c66e4109032b6d7d1b8, c8ca3257fba3e46f4d65cae54a3bf7715);
		Aabb aabb = new Aabb(minimum, maximum);
		aabb.Scale(scale);
		aabb.Minimum.Y = Math.Min(aabb.Minimum.Y - c92625694f9148dcf7fad2d25b6eac568, 0f - c92625694f9148dcf7fad2d25b6eac568);
		return aabb.GetAabb(pose);
	}

	/// <summary>
	///       Gets the height for a height field coordinate.
	///       </summary>
	/// <param name="x">
	///       The x coordinate that lies in the interval [0, <see cref="P:DigitalRune.Geometry.Shapes.HeightField.WidthX" />].
	///       </param>
	/// <param name="z">
	///       The z coordinate that lies in the interval [0, <see cref="P:DigitalRune.Geometry.Shapes.HeightField.WidthZ" />].
	///       </param>
	/// <returns>The interpolated height for the given coordinates.</returns>
	public float GetHeight(float x, float z)
	{
		if (x < 0f || x > c356f65f7067c642b76c62fa0a6cd5811 || z < 0f || z > c8ca3257fba3e46f4d65cae54a3bf7715)
		{
			return float.NaN;
		}
		int length = c37adb342465be20dcf2c430c7d6aeb68.GetLength(0);
		int length2 = c37adb342465be20dcf2c430c7d6aeb68.GetLength(1);
		float num = c356f65f7067c642b76c62fa0a6cd5811 / (float)(length - 1);
		float num2 = c8ca3257fba3e46f4d65cae54a3bf7715 / (float)(length2 - 1);
		int num3 = Math.Min((int)(x / num), length - 2);
		int num4 = Math.Min((int)(z / num2), length2 - 2);
		float num5 = x / num - (float)num3;
		float num6 = z / num2 - (float)num4;
		bool second = num5 + num6 > 1f;
		Triangle triangle = GetTriangle(num3, num4, second);
		float y = triangle.Vertex0.Y;
		float y2 = triangle.Vertex1.Y;
		float y3 = triangle.Vertex2.Y;
		triangle.Vertex0.Y = 0f;
		triangle.Vertex1.Y = 0f;
		triangle.Vertex2.Y = 0f;
		GeometryHelper.GetBarycentricFromPoint(triangle, new Vector3F(x, 0f, z), out var u, out var v, out var w);
		return u * y + v * y2 + w * y3;
	}

	/// <summary>
	///       Gets a triangle representing a part of a height field cell.
	///       </summary>
	/// <param name="indexX">The cell index along <see cref="P:DigitalRune.Geometry.Shapes.HeightField.WidthX" />.</param>
	/// <param name="indexZ">The cell index along <see cref="P:DigitalRune.Geometry.Shapes.HeightField.WidthZ" />.</param>
	/// <param name="second">
	///       If set to <see langword="true" /> the triangle for the second cell half is returned; 
	///       otherwise the triangle for the first cell half is returned.
	///       </param>
	/// <returns>A triangle.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="indexX" /> is out of range.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="indexZ" /> is out of range.
	///       </exception>
	public Triangle GetTriangle(int indexX, int indexZ, bool second)
	{
		int length = c37adb342465be20dcf2c430c7d6aeb68.GetLength(0);
		int length2 = c37adb342465be20dcf2c430c7d6aeb68.GetLength(1);
		if (indexX < 0 || indexX + 1 >= length)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17541));
		}
		if (indexZ < 0 || indexZ + 1 >= length2)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17554));
		}
		Triangle result = default;
		float num = c356f65f7067c642b76c62fa0a6cd5811 / (float)(length - 1);
		float num2 = c8ca3257fba3e46f4d65cae54a3bf7715 / (float)(length2 - 1);
		float num3 = num * (float)indexX;
		float num4 = num2 * (float)indexZ;
		if (!second)
		{
			float y = c37adb342465be20dcf2c430c7d6aeb68[indexX, indexZ];
			float y2 = c37adb342465be20dcf2c430c7d6aeb68[indexX + 1, indexZ];
			float y3 = c37adb342465be20dcf2c430c7d6aeb68[indexX, indexZ + 1];
			result.Vertex0 = new Vector3F(num3, y, num4);
			result.Vertex2 = new Vector3F(num3 + num, y2, num4);
			result.Vertex1 = new Vector3F(num3, y3, num4 + num2);
		}
		else
		{
			float y4 = c37adb342465be20dcf2c430c7d6aeb68[indexX, indexZ + 1];
			float y5 = c37adb342465be20dcf2c430c7d6aeb68[indexX + 1, indexZ];
			float y6 = c37adb342465be20dcf2c430c7d6aeb68[indexX + 1, indexZ + 1];
			result.Vertex0 = new Vector3F(num3, y4, num4 + num2);
			result.Vertex2 = new Vector3F(num3 + num, y5, num4);
			result.Vertex1 = new Vector3F(num3 + num, y6, num4 + num2);
		}
		return result;
	}

	/// <summary>
	///       Gets the volume of this shape.
	///       </summary>
	/// <param name="relativeError">Not used.</param>
	/// <param name="iterationLimit">Not used</param>
	/// <returns>Positive infinity (<see cref="F:System.Single.PositiveInfinity" />)</returns>
	public override float GetVolume(float relativeError, int iterationLimit)
	{
		return float.PositiveInfinity;
	}

	/// <summary>
	///       Invalidates this height field.
	///       </summary>
	public void Invalidate()
	{
		OnChanged(ShapeChangedEventArgs.Empty);
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
		int length = Array.GetLength(0);
		int length2 = Array.GetLength(1);
		int num = length - 1;
		int num2 = length2 - 1;
		int num3 = 0;
		while (num3 < length)
		{
			for (int i = 0; i < length2; i++)
			{
				triangleMesh.Vertices.Add(new Vector3F((float)num3 / (float)num * WidthX, Array[num3, i], (float)i / (float)num2 * WidthZ));
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
					__ldtoken(HeightField.OnGetMesh(float, int));
				}
				num3++;
				break;
			}
		}
		for (int j = 0; j < num; j++)
		{
			for (int k = 0; k < num2; k++)
			{
				triangleMesh.Indices.Add(j * length + k);
				triangleMesh.Indices.Add(j * length + k + 1);
				triangleMesh.Indices.Add((j + 1) * length + k);
				triangleMesh.Indices.Add(j * length + k + 1);
				triangleMesh.Indices.Add((j + 1) * length + k + 1);
				triangleMesh.Indices.Add((j + 1) * length + k);
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					goto end_IL_0139;
				}
				continue;
				end_IL_0139:
				break;
			}
		}
		return triangleMesh;
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Geometry.Shapes.Shape.Changed" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///   <see cref="T:DigitalRune.Geometry.Shapes.ShapeChangedEventArgs" /> object that provides the arguments for the event.
	///       </param>
	protected override void OnChanged(ShapeChangedEventArgs eventArgs)
	{
		c526d59d167cc46a092b30f68879e176e = float.NaN;
		c5cf26e930f4a2c66e4109032b6d7d1b8 = float.NaN;
		base.OnChanged(eventArgs);
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17567), c356f65f7067c642b76c62fa0a6cd5811, c8ca3257fba3e46f4d65cae54a3bf7715);
	}
}
