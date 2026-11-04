using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using A;
using Microsoft.Xna.Framework;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Defines a 4 x 4 matrix (single-precision).
///       </summary>
[Serializable]
[TypeConverter(typeof(ExpandableObjectConverter))]
public struct Matrix44F : IEquatable<Matrix44F>
{
	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> with all of its components set to zero.
	///       </summary>
	public static readonly Matrix44F Zero;

	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> with all of its components set to one.
	///       </summary>
	public static readonly Matrix44F One;

	/// <summary>
	///       Returns the 4 x 4 identity matrix.
	///       </summary>
	public static readonly Matrix44F Identity;

	/// <summary>
	///       The element in first row, first column.
	///       </summary>
	public float M00;

	/// <summary>
	///       The element in first row, second column.
	///       </summary>
	public float M01;

	/// <summary>
	///       The element in first row, third column.
	///       </summary>
	public float M02;

	/// <summary>
	///       The element in first row, fourth column.
	///       </summary>
	public float M03;

	/// <summary>
	///       The element in second row, first column.
	///       </summary>
	public float M10;

	/// <summary>
	///       The element in second row, second column.
	///       </summary>
	public float M11;

	/// <summary>
	///       The element in second row, third column.
	///       </summary>
	public float M12;

	/// <summary>
	///       The element in second row, fourth column.
	///       </summary>
	public float M13;

	/// <summary>
	///       The element in third row, first column.
	///       </summary>
	public float M20;

	/// <summary>
	///       The element in third row, second column.
	///       </summary>
	public float M21;

	/// <summary>
	///       The element in third row, third column.
	///       </summary>
	public float M22;

	/// <summary>
	///       The element in third row, fourth column.
	///       </summary>
	public float M23;

	/// <summary>
	///       The element in fourth row, first column.
	///       </summary>
	public float M30;

	/// <summary>
	///       The element in fourth row, second column.
	///       </summary>
	public float M31;

	/// <summary>
	///       The element in fourth row, third column.
	///       </summary>
	public float M32;

	/// <summary>
	///       The element in fourth row, fourth column.
	///       </summary>
	public float M33;

	/// <summary>
	///       Gets or sets the element at the specified index.
	///       </summary>
	/// <param name="index">The index.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public float this[int index]
	{
		get
		{
			return index switch
			{
				0 => M00, 
				1 => M01, 
				2 => M02, 
				3 => M03, 
				4 => M10, 
				5 => M11, 
				6 => M12, 
				7 => M13, 
				8 => M20, 
				9 => M21, 
				10 => M22, 
				11 => M23, 
				12 => M30, 
				13 => M31, 
				14 => M32, 
				15 => M33, 
				_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4715)), 
			};
		}
		set
		{
			switch (index)
			{
			case 0:
				M00 = value;
				break;
			case 1:
				M01 = value;
				break;
			case 2:
				M02 = value;
				break;
			case 3:
				M03 = value;
				break;
			case 4:
				M10 = value;
				break;
			case 5:
				M11 = value;
				break;
			case 6:
				M12 = value;
				break;
			case 7:
				M13 = value;
				break;
			case 8:
				M20 = value;
				break;
			case 9:
				M21 = value;
				break;
			case 10:
				M22 = value;
				break;
			case 11:
				M23 = value;
				break;
			case 12:
				M30 = value;
				break;
			case 13:
				M31 = value;
				break;
			case 14:
				M32 = value;
				break;
			case 15:
				M33 = value;
				break;
			default:
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4715));
			}
		}
	}

	/// <summary>
	///       Gets or sets the element at the specified index.
	///       </summary>
	/// <param name="row">The row index.</param>
	/// <param name="column">The column index.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The index [<paramref name="row" />, <paramref name="column" />] is out of range.
	///       </exception>
	public float this[int row, int column]
	{
		get
		{
			return row switch
			{
				0 => column switch
				{
					0 => M00, 
					1 => M01, 
					2 => M02, 
					3 => M03, 
					_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4837)), 
				}, 
				1 => column switch
				{
					0 => M10, 
					1 => M11, 
					2 => M12, 
					3 => M13, 
					_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4837)), 
				}, 
				2 => column switch
				{
					0 => M20, 
					1 => M21, 
					2 => M22, 
					3 => M23, 
					_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4837)), 
				}, 
				3 => column switch
				{
					0 => M30, 
					1 => M31, 
					2 => M32, 
					3 => M33, 
					_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4837)), 
				}, 
				_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4958), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4965)), 
			};
		}
		set
		{
			switch (row)
			{
			case 0:
				switch (column)
				{
				case 0:
					M00 = value;
					break;
				case 1:
					M01 = value;
					break;
				case 2:
					M02 = value;
					break;
				case 3:
					M03 = value;
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4837));
				}
				break;
			case 1:
				switch (column)
				{
				case 0:
					M10 = value;
					break;
				case 1:
					M11 = value;
					break;
				case 2:
					M12 = value;
					break;
				case 3:
					M13 = value;
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4837));
				}
				break;
			case 2:
				switch (column)
				{
				case 0:
					M20 = value;
					break;
				case 1:
					M21 = value;
					break;
				case 2:
					M22 = value;
					break;
				case 3:
					M23 = value;
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4837));
				}
				break;
			case 3:
				switch (column)
				{
				case 0:
					M30 = value;
					break;
				case 1:
					M31 = value;
					break;
				case 2:
					M32 = value;
					break;
				case 3:
					M33 = value;
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4837));
				}
				break;
			default:
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4958), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4965));
			}
		}
	}

	/// <summary>
	///       Gets a value indicating whether an element of the matrix is <see cref="F:System.Single.NaN" />.
	///       </summary>
	public bool IsNaN
	{
		get
		{
			if (!Numeric.IsNaN(M00) && !Numeric.IsNaN(M01) && !Numeric.IsNaN(M02) && !Numeric.IsNaN(M03) && !Numeric.IsNaN(M10) && !Numeric.IsNaN(M11) && !Numeric.IsNaN(M12) && !Numeric.IsNaN(M13) && !Numeric.IsNaN(M20) && !Numeric.IsNaN(M21) && !Numeric.IsNaN(M22) && !Numeric.IsNaN(M23) && !Numeric.IsNaN(M30) && !Numeric.IsNaN(M31) && !Numeric.IsNaN(M32))
			{
				return Numeric.IsNaN(M33);
			}
			return true;
		}
	}

	/// <summary>
	///       Gets a value indicating whether this matrix is symmetric.
	///       </summary>
	public bool IsSymmetric
	{
		get
		{
			if (M01 == M10 && M02 == M20 && M03 == M30 && M12 == M21 && M13 == M31)
			{
				return M23 == M32;
			}
			return false;
		}
	}

	/// <summary>
	///       Gets or sets the upper left 3x3 sub-matrix.
	///       </summary>
	[XmlIgnore]
	public Matrix33F Minor
	{
		get
		{
			return new Matrix33F(M00, M01, M02, M10, M11, M12, M20, M21, M22);
		}
		set
		{
			M00 = value.M00;
			M01 = value.M01;
			M02 = value.M02;
			M10 = value.M10;
			M11 = value.M11;
			M12 = value.M12;
			M20 = value.M20;
			M21 = value.M21;
			M22 = value.M22;
		}
	}

	/// <summary>
	///       Gets the matrix trace (the sum of the diagonal elements).
	///       </summary>
	public float Trace => M00 + M11 + M22 + M33;

	/// <summary>
	///       Gets or sets the translation vector (<see cref="F:DigitalRune.Mathematics.Algebra.Matrix44F.M03" />, <see cref="F:DigitalRune.Mathematics.Algebra.Matrix44F.M13" />, 
	///       <see cref="F:DigitalRune.Mathematics.Algebra.Matrix44F.M23" />) 
	///       of the matrix.
	///       </summary>
	[XmlIgnore]
	public Vector3F Translation
	{
		get
		{
			return new Vector3F(M03, M13, M23);
		}
		set
		{
			M03 = value.X;
			M13 = value.Y;
			M23 = value.Z;
		}
	}

	/// <summary>
	///       Returns the determinant of this matrix.
	///       </summary>
	public float Determinant
	{
		get
		{
			float num = M22 * M33 - M23 * M32;
			float num2 = M21 * M33 - M23 * M31;
			float num3 = M21 * M32 - M22 * M31;
			float num4 = M20 * M33 - M23 * M30;
			float num5 = M20 * M32 - M22 * M30;
			float num6 = M20 * M31 - M21 * M30;
			return M00 * (M11 * num - M12 * num2 + M13 * num3) - M01 * (M10 * num - M12 * num4 + M13 * num5) + M02 * (M10 * num2 - M11 * num4 + M13 * num6) - M03 * (M10 * num3 - M11 * num5 + M12 * num6);
		}
	}

	/// <summary>
	///       Returns the transposed of this matrix.
	///       </summary>
	/// <returns>The transposed of this matrix.</returns>
	public Matrix44F Transposed
	{
		get
		{
			Matrix44F result = this;
			result.Transpose();
			return result;
		}
	}

	/// <summary>
	///       Returns the inverse of this matrix.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The matrix is singular (i.e. it is not invertible).
	///       </exception>
	public Matrix44F Inverse
	{
		get
		{
			Matrix44F result = this;
			result.Invert();
			return result;
		}
	}

	static Matrix44F()
	{
		Zero = new Matrix44F(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
		One = new Matrix44F(1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
		Identity = new Matrix44F(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> struct.
	///       </summary>
	/// <param name="elementValue">The initial value for the matrix elements.</param>
	public Matrix44F(float elementValue)
		: this(elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> class.
	///       </summary>
	/// <param name="m00">The element in the first row, first column.</param>
	/// <param name="m01">The element in the first row, second column.</param>
	/// <param name="m02">The element in the first row, third column.</param>
	/// <param name="m03">The element in the first row, fourth column.</param>
	/// <param name="m10">The element in the second row, first column.</param>
	/// <param name="m11">The element in the second row, second column.</param>
	/// <param name="m12">The element in the second row, third column.</param>
	/// <param name="m13">The element in the second row, fourth column.</param>
	/// <param name="m20">The element in the third row, first column.</param>
	/// <param name="m21">The element in the third row, second column.</param>
	/// <param name="m22">The element in the third row, third column.</param>
	/// <param name="m23">The element in the third row, fourth column.</param>
	/// <param name="m30">The element in the fourth row, first column</param>
	/// <param name="m31">The element in the fourth row, second column</param>
	/// <param name="m32">The element in the fourth row, third column</param>
	/// <param name="m33">The element in the fourth row, fourth column</param>
	public Matrix44F(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
	{
		M00 = m00;
		M01 = m01;
		M02 = m02;
		M03 = m03;
		M10 = m10;
		M11 = m11;
		M12 = m12;
		M13 = m13;
		M20 = m20;
		M21 = m21;
		M22 = m22;
		M23 = m23;
		M30 = m30;
		M31 = m31;
		M32 = m32;
		M33 = m33;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> struct.
	///       </summary>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <param name="order">The order of the matrix elements in <paramref name="elements" />.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> has less than 16 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public Matrix44F(float[] elements, MatrixOrder order)
	{
		if (order == MatrixOrder.RowMajor)
		{
			M00 = elements[0];
			M01 = elements[1];
			M02 = elements[2];
			M03 = elements[3];
			M10 = elements[4];
			M11 = elements[5];
			M12 = elements[6];
			M13 = elements[7];
			M20 = elements[8];
			M21 = elements[9];
			M22 = elements[10];
			M23 = elements[11];
			M30 = elements[12];
			M31 = elements[13];
			M32 = elements[14];
			M33 = elements[15];
		}
		else
		{
			M00 = elements[0];
			M10 = elements[1];
			M20 = elements[2];
			M30 = elements[3];
			M01 = elements[4];
			M11 = elements[5];
			M21 = elements[6];
			M31 = elements[7];
			M02 = elements[8];
			M12 = elements[9];
			M22 = elements[10];
			M32 = elements[11];
			M03 = elements[12];
			M13 = elements[13];
			M23 = elements[14];
			M33 = elements[15];
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> struct.
	///       </summary>
	/// <param name="elements">The list with the initial values for the matrix elements.</param>
	/// <param name="order">The order of the matrix elements in <paramref name="elements" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="elements" /> has less than 16 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public Matrix44F(IList<float> elements, MatrixOrder order)
	{
		if (order == MatrixOrder.RowMajor)
		{
			M00 = elements[0];
			M01 = elements[1];
			M02 = elements[2];
			M03 = elements[3];
			M10 = elements[4];
			M11 = elements[5];
			M12 = elements[6];
			M13 = elements[7];
			M20 = elements[8];
			M21 = elements[9];
			M22 = elements[10];
			M23 = elements[11];
			M30 = elements[12];
			M31 = elements[13];
			M32 = elements[14];
			M33 = elements[15];
		}
		else
		{
			M00 = elements[0];
			M10 = elements[1];
			M20 = elements[2];
			M30 = elements[3];
			M01 = elements[4];
			M11 = elements[5];
			M21 = elements[6];
			M31 = elements[7];
			M02 = elements[8];
			M12 = elements[9];
			M22 = elements[10];
			M32 = elements[11];
			M03 = elements[12];
			M13 = elements[13];
			M23 = elements[14];
			M33 = elements[15];
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> struct.
	///       </summary>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> has less than 4x4 elements.</exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.</exception>
	[CLSCompliant(false)]
	public Matrix44F(float[,] elements)
	{
		M00 = elements[0, 0];
		M01 = elements[0, 1];
		M02 = elements[0, 2];
		M03 = elements[0, 3];
		M10 = elements[1, 0];
		M11 = elements[1, 1];
		M12 = elements[1, 2];
		M13 = elements[1, 3];
		M20 = elements[2, 0];
		M21 = elements[2, 1];
		M22 = elements[2, 2];
		M23 = elements[2, 3];
		M30 = elements[3, 0];
		M31 = elements[3, 1];
		M32 = elements[3, 2];
		M33 = elements[3, 3];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> struct.
	///       </summary>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> has less than 4x4 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> or the arrays in elements[0] must not be <see langword="null" />.
	///       </exception>
	public Matrix44F(float[][] elements)
	{
		M00 = elements[0][0];
		M01 = elements[0][1];
		M02 = elements[0][2];
		M03 = elements[0][3];
		M10 = elements[1][0];
		M11 = elements[1][1];
		M12 = elements[1][2];
		M13 = elements[1][3];
		M20 = elements[2][0];
		M21 = elements[2][1];
		M22 = elements[2][2];
		M23 = elements[2][3];
		M30 = elements[3][0];
		M31 = elements[3][1];
		M32 = elements[3][2];
		M33 = elements[3][3];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> struct.
	///       </summary>
	/// <param name="minor">The upper left 3x3 submatrix.</param>
	/// <param name="translation">The translation vector.</param>
	public Matrix44F(Matrix33F minor, Vector3F translation)
	{
		M00 = minor.M00;
		M01 = minor.M01;
		M02 = minor.M02;
		M03 = translation.X;
		M10 = minor.M10;
		M11 = minor.M11;
		M12 = minor.M12;
		M13 = translation.Y;
		M20 = minor.M20;
		M21 = minor.M21;
		M22 = minor.M22;
		M23 = translation.Z;
		M30 = 0f;
		M31 = 0f;
		M32 = 0f;
		M33 = 1f;
	}

	/// <summary>
	///       Returns the hash code for this instance.
	///       </summary>
	/// <returns>
	///       A 32-bit signed integer that is the hash code for this instance.
	///       </returns>
	public override int GetHashCode()
	{
		int hashCode = M00.GetHashCode();
		hashCode = (hashCode * 397) ^ M01.GetHashCode();
		hashCode = (hashCode * 397) ^ M02.GetHashCode();
		hashCode = (hashCode * 397) ^ M03.GetHashCode();
		hashCode = (hashCode * 397) ^ M10.GetHashCode();
		hashCode = (hashCode * 397) ^ M11.GetHashCode();
		hashCode = (hashCode * 397) ^ M12.GetHashCode();
		hashCode = (hashCode * 397) ^ M13.GetHashCode();
		hashCode = (hashCode * 397) ^ M20.GetHashCode();
		hashCode = (hashCode * 397) ^ M21.GetHashCode();
		hashCode = (hashCode * 397) ^ M22.GetHashCode();
		hashCode = (hashCode * 397) ^ M23.GetHashCode();
		hashCode = (hashCode * 397) ^ M30.GetHashCode();
		hashCode = (hashCode * 397) ^ M31.GetHashCode();
		hashCode = (hashCode * 397) ^ M32.GetHashCode();
		return (hashCode * 397) ^ M33.GetHashCode();
	}

	/// <summary>
	///       Indicates whether this instance and a specified object are equal.
	///       </summary>
	/// <param name="obj">Another object to compare to.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> and this instance are the same 
	///       type and represent the same value; otherwise, <see langword="false" />.
	///       </returns>
	public override bool Equals(object obj)
	{
		if (obj is Matrix44F)
		{
			return this == (Matrix44F)obj;
		}
		return false;
	}

	/// <summary>
	///       Indicates whether the current object is equal to another object of the same type.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(Matrix44F other)
	{
		return this == other;
	}

	/// <summary>
	///       Returns the string representation of this matrix.
	///       </summary>
	/// <returns>The string representation of this matrix</returns>
	public override string ToString()
	{
		return ToString(CultureInfo.CurrentCulture);
	}

	/// <summary>
	///       Returns the string representation of this matrix using the specified
	///       culture-specific format information.
	///       </summary>
	/// <param name="provider">
	///       An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information.
	///       </param>
	/// <returns>The string representation of this matrix</returns>
	public string ToString(IFormatProvider provider)
	{
		return string.Format(provider, c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5080), M00, M01, M02, M03, M10, M11, M12, M13, M20, M21, M22, M23, M30, M31, M32, M33);
	}

	/// <summary>
	///       Negates a matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The negated matrix.</returns>
	public static Matrix44F operator -(Matrix44F matrix)
	{
		matrix.M00 = 0f - matrix.M00;
		matrix.M01 = 0f - matrix.M01;
		matrix.M02 = 0f - matrix.M02;
		matrix.M03 = 0f - matrix.M03;
		matrix.M10 = 0f - matrix.M10;
		matrix.M11 = 0f - matrix.M11;
		matrix.M12 = 0f - matrix.M12;
		matrix.M13 = 0f - matrix.M13;
		matrix.M20 = 0f - matrix.M20;
		matrix.M21 = 0f - matrix.M21;
		matrix.M22 = 0f - matrix.M22;
		matrix.M23 = 0f - matrix.M23;
		matrix.M30 = 0f - matrix.M30;
		matrix.M31 = 0f - matrix.M31;
		matrix.M32 = 0f - matrix.M32;
		matrix.M33 = 0f - matrix.M33;
		return matrix;
	}

	/// <summary>
	///       Negates a matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The negated matrix.</returns>
	public static Matrix44F Negate(Matrix44F matrix)
	{
		matrix.M00 = 0f - matrix.M00;
		matrix.M01 = 0f - matrix.M01;
		matrix.M02 = 0f - matrix.M02;
		matrix.M03 = 0f - matrix.M03;
		matrix.M10 = 0f - matrix.M10;
		matrix.M11 = 0f - matrix.M11;
		matrix.M12 = 0f - matrix.M12;
		matrix.M13 = 0f - matrix.M13;
		matrix.M20 = 0f - matrix.M20;
		matrix.M21 = 0f - matrix.M21;
		matrix.M22 = 0f - matrix.M22;
		matrix.M23 = 0f - matrix.M23;
		matrix.M30 = 0f - matrix.M30;
		matrix.M31 = 0f - matrix.M31;
		matrix.M32 = 0f - matrix.M32;
		matrix.M33 = 0f - matrix.M33;
		return matrix;
	}

	/// <summary>
	///       Adds two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second Matrix.</param>
	/// <returns>The sum of the two matrices.</returns>
	public static Matrix44F operator +(Matrix44F matrix1, Matrix44F matrix2)
	{
		matrix1.M00 += matrix2.M00;
		matrix1.M01 += matrix2.M01;
		matrix1.M02 += matrix2.M02;
		matrix1.M03 += matrix2.M03;
		matrix1.M10 += matrix2.M10;
		matrix1.M11 += matrix2.M11;
		matrix1.M12 += matrix2.M12;
		matrix1.M13 += matrix2.M13;
		matrix1.M20 += matrix2.M20;
		matrix1.M21 += matrix2.M21;
		matrix1.M22 += matrix2.M22;
		matrix1.M23 += matrix2.M23;
		matrix1.M30 += matrix2.M30;
		matrix1.M31 += matrix2.M31;
		matrix1.M32 += matrix2.M32;
		matrix1.M33 += matrix2.M33;
		return matrix1;
	}

	/// <summary>
	///       Adds two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second Matrix.</param>
	/// <returns>The sum of the two matrices.</returns>
	public static Matrix44F Add(Matrix44F matrix1, Matrix44F matrix2)
	{
		matrix1.M00 += matrix2.M00;
		matrix1.M01 += matrix2.M01;
		matrix1.M02 += matrix2.M02;
		matrix1.M03 += matrix2.M03;
		matrix1.M10 += matrix2.M10;
		matrix1.M11 += matrix2.M11;
		matrix1.M12 += matrix2.M12;
		matrix1.M13 += matrix2.M13;
		matrix1.M20 += matrix2.M20;
		matrix1.M21 += matrix2.M21;
		matrix1.M22 += matrix2.M22;
		matrix1.M23 += matrix2.M23;
		matrix1.M30 += matrix2.M30;
		matrix1.M31 += matrix2.M31;
		matrix1.M32 += matrix2.M32;
		matrix1.M33 += matrix2.M33;
		return matrix1;
	}

	/// <summary>
	///       Subtracts two matrices.
	///       </summary>
	/// <param name="minuend">The first matrix (minuend).</param>
	/// <param name="subtrahend">The second matrix (subtrahend).</param>
	/// <returns>The difference of the two matrices.</returns>
	public static Matrix44F operator -(Matrix44F minuend, Matrix44F subtrahend)
	{
		minuend.M00 -= subtrahend.M00;
		minuend.M01 -= subtrahend.M01;
		minuend.M02 -= subtrahend.M02;
		minuend.M03 -= subtrahend.M03;
		minuend.M10 -= subtrahend.M10;
		minuend.M11 -= subtrahend.M11;
		minuend.M12 -= subtrahend.M12;
		minuend.M13 -= subtrahend.M13;
		minuend.M20 -= subtrahend.M20;
		minuend.M21 -= subtrahend.M21;
		minuend.M22 -= subtrahend.M22;
		minuend.M23 -= subtrahend.M23;
		minuend.M30 -= subtrahend.M30;
		minuend.M31 -= subtrahend.M31;
		minuend.M32 -= subtrahend.M32;
		minuend.M33 -= subtrahend.M33;
		return minuend;
	}

	/// <summary>
	///       Subtracts two matrices.
	///       </summary>
	/// <param name="minuend">The first matrix (minuend).</param>
	/// <param name="subtrahend">The second matrix (subtrahend).</param>
	/// <returns>The difference of the two matrices.</returns>
	public static Matrix44F Subtract(Matrix44F minuend, Matrix44F subtrahend)
	{
		minuend.M00 -= subtrahend.M00;
		minuend.M01 -= subtrahend.M01;
		minuend.M02 -= subtrahend.M02;
		minuend.M03 -= subtrahend.M03;
		minuend.M10 -= subtrahend.M10;
		minuend.M11 -= subtrahend.M11;
		minuend.M12 -= subtrahend.M12;
		minuend.M13 -= subtrahend.M13;
		minuend.M20 -= subtrahend.M20;
		minuend.M21 -= subtrahend.M21;
		minuend.M22 -= subtrahend.M22;
		minuend.M23 -= subtrahend.M23;
		minuend.M30 -= subtrahend.M30;
		minuend.M31 -= subtrahend.M31;
		minuend.M32 -= subtrahend.M32;
		minuend.M33 -= subtrahend.M33;
		return minuend;
	}

	/// <summary>
	///       Multiplies a matrix and a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The matrix with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	public static Matrix44F operator *(Matrix44F matrix, float scalar)
	{
		matrix.M00 *= scalar;
		matrix.M01 *= scalar;
		matrix.M02 *= scalar;
		matrix.M03 *= scalar;
		matrix.M10 *= scalar;
		matrix.M11 *= scalar;
		matrix.M12 *= scalar;
		matrix.M13 *= scalar;
		matrix.M20 *= scalar;
		matrix.M21 *= scalar;
		matrix.M22 *= scalar;
		matrix.M23 *= scalar;
		matrix.M30 *= scalar;
		matrix.M31 *= scalar;
		matrix.M32 *= scalar;
		matrix.M33 *= scalar;
		return matrix;
	}

	/// <summary>
	///       Multiplies a matrix by a scalar.
	///       </summary>
	/// <param name="scalar">The scalar.</param>
	/// <param name="matrix">The matrix.</param>
	/// <returns>
	///       The matrix with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	public static Matrix44F operator *(float scalar, Matrix44F matrix)
	{
		matrix.M00 *= scalar;
		matrix.M01 *= scalar;
		matrix.M02 *= scalar;
		matrix.M03 *= scalar;
		matrix.M10 *= scalar;
		matrix.M11 *= scalar;
		matrix.M12 *= scalar;
		matrix.M13 *= scalar;
		matrix.M20 *= scalar;
		matrix.M21 *= scalar;
		matrix.M22 *= scalar;
		matrix.M23 *= scalar;
		matrix.M30 *= scalar;
		matrix.M31 *= scalar;
		matrix.M32 *= scalar;
		matrix.M33 *= scalar;
		return matrix;
	}

	/// <summary>
	///       Multiplies a matrix by a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The matrix with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	public static Matrix44F Multiply(float scalar, Matrix44F matrix)
	{
		matrix.M00 *= scalar;
		matrix.M01 *= scalar;
		matrix.M02 *= scalar;
		matrix.M03 *= scalar;
		matrix.M10 *= scalar;
		matrix.M11 *= scalar;
		matrix.M12 *= scalar;
		matrix.M13 *= scalar;
		matrix.M20 *= scalar;
		matrix.M21 *= scalar;
		matrix.M22 *= scalar;
		matrix.M23 *= scalar;
		matrix.M30 *= scalar;
		matrix.M31 *= scalar;
		matrix.M32 *= scalar;
		matrix.M33 *= scalar;
		return matrix;
	}

	/// <summary>
	///       Multiplies two matrices.
	///       </summary>
	/// <param name="m1">The first matrix.</param>
	/// <param name="m2">The second matrix.</param>
	/// <returns>The matrix with the product the two matrices.</returns>
	public static Matrix44F operator *(Matrix44F m1, Matrix44F m2)
	{
		Matrix44F result = default;
		result.M00 = m1.M00 * m2.M00 + m1.M01 * m2.M10 + m1.M02 * m2.M20 + m1.M03 * m2.M30;
		result.M01 = m1.M00 * m2.M01 + m1.M01 * m2.M11 + m1.M02 * m2.M21 + m1.M03 * m2.M31;
		result.M02 = m1.M00 * m2.M02 + m1.M01 * m2.M12 + m1.M02 * m2.M22 + m1.M03 * m2.M32;
		result.M03 = m1.M00 * m2.M03 + m1.M01 * m2.M13 + m1.M02 * m2.M23 + m1.M03 * m2.M33;
		result.M10 = m1.M10 * m2.M00 + m1.M11 * m2.M10 + m1.M12 * m2.M20 + m1.M13 * m2.M30;
		result.M11 = m1.M10 * m2.M01 + m1.M11 * m2.M11 + m1.M12 * m2.M21 + m1.M13 * m2.M31;
		result.M12 = m1.M10 * m2.M02 + m1.M11 * m2.M12 + m1.M12 * m2.M22 + m1.M13 * m2.M32;
		result.M13 = m1.M10 * m2.M03 + m1.M11 * m2.M13 + m1.M12 * m2.M23 + m1.M13 * m2.M33;
		result.M20 = m1.M20 * m2.M00 + m1.M21 * m2.M10 + m1.M22 * m2.M20 + m1.M23 * m2.M30;
		result.M21 = m1.M20 * m2.M01 + m1.M21 * m2.M11 + m1.M22 * m2.M21 + m1.M23 * m2.M31;
		result.M22 = m1.M20 * m2.M02 + m1.M21 * m2.M12 + m1.M22 * m2.M22 + m1.M23 * m2.M32;
		result.M23 = m1.M20 * m2.M03 + m1.M21 * m2.M13 + m1.M22 * m2.M23 + m1.M23 * m2.M33;
		result.M30 = m1.M30 * m2.M00 + m1.M31 * m2.M10 + m1.M32 * m2.M20 + m1.M33 * m2.M30;
		result.M31 = m1.M30 * m2.M01 + m1.M31 * m2.M11 + m1.M32 * m2.M21 + m1.M33 * m2.M31;
		result.M32 = m1.M30 * m2.M02 + m1.M31 * m2.M12 + m1.M32 * m2.M22 + m1.M33 * m2.M32;
		result.M33 = m1.M30 * m2.M03 + m1.M31 * m2.M13 + m1.M32 * m2.M23 + m1.M33 * m2.M33;
		return result;
	}

	/// <summary>
	///       Multiplies two matrices.
	///       </summary>
	/// <param name="m1">The first matrix.</param>
	/// <param name="m2">The second matrix.</param>
	/// <returns>The matrix with the product the two matrices.</returns>
	public static Matrix44F Multiply(Matrix44F m1, Matrix44F m2)
	{
		Matrix44F result = default;
		result.M00 = m1.M00 * m2.M00 + m1.M01 * m2.M10 + m1.M02 * m2.M20 + m1.M03 * m2.M30;
		result.M01 = m1.M00 * m2.M01 + m1.M01 * m2.M11 + m1.M02 * m2.M21 + m1.M03 * m2.M31;
		result.M02 = m1.M00 * m2.M02 + m1.M01 * m2.M12 + m1.M02 * m2.M22 + m1.M03 * m2.M32;
		result.M03 = m1.M00 * m2.M03 + m1.M01 * m2.M13 + m1.M02 * m2.M23 + m1.M03 * m2.M33;
		result.M10 = m1.M10 * m2.M00 + m1.M11 * m2.M10 + m1.M12 * m2.M20 + m1.M13 * m2.M30;
		result.M11 = m1.M10 * m2.M01 + m1.M11 * m2.M11 + m1.M12 * m2.M21 + m1.M13 * m2.M31;
		result.M12 = m1.M10 * m2.M02 + m1.M11 * m2.M12 + m1.M12 * m2.M22 + m1.M13 * m2.M32;
		result.M13 = m1.M10 * m2.M03 + m1.M11 * m2.M13 + m1.M12 * m2.M23 + m1.M13 * m2.M33;
		result.M20 = m1.M20 * m2.M00 + m1.M21 * m2.M10 + m1.M22 * m2.M20 + m1.M23 * m2.M30;
		result.M21 = m1.M20 * m2.M01 + m1.M21 * m2.M11 + m1.M22 * m2.M21 + m1.M23 * m2.M31;
		result.M22 = m1.M20 * m2.M02 + m1.M21 * m2.M12 + m1.M22 * m2.M22 + m1.M23 * m2.M32;
		result.M23 = m1.M20 * m2.M03 + m1.M21 * m2.M13 + m1.M22 * m2.M23 + m1.M23 * m2.M33;
		result.M30 = m1.M30 * m2.M00 + m1.M31 * m2.M10 + m1.M32 * m2.M20 + m1.M33 * m2.M30;
		result.M31 = m1.M30 * m2.M01 + m1.M31 * m2.M11 + m1.M32 * m2.M21 + m1.M33 * m2.M31;
		result.M32 = m1.M30 * m2.M02 + m1.M31 * m2.M12 + m1.M32 * m2.M22 + m1.M33 * m2.M32;
		result.M33 = m1.M30 * m2.M03 + m1.M31 * m2.M13 + m1.M32 * m2.M23 + m1.M33 * m2.M33;
		return result;
	}

	/// <summary>
	///       Multiplies the matrix with a column vector.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="vector">The vector.</param>
	/// <returns>The resulting column vector.</returns>
	public static Vector4F operator *(Matrix44F matrix, Vector4F vector)
	{
		Vector4F result = default;
		result.X = matrix.M00 * vector.X + matrix.M01 * vector.Y + matrix.M02 * vector.Z + matrix.M03 * vector.W;
		result.Y = matrix.M10 * vector.X + matrix.M11 * vector.Y + matrix.M12 * vector.Z + matrix.M13 * vector.W;
		result.Z = matrix.M20 * vector.X + matrix.M21 * vector.Y + matrix.M22 * vector.Z + matrix.M23 * vector.W;
		result.W = matrix.M30 * vector.X + matrix.M31 * vector.Y + matrix.M32 * vector.Z + matrix.M33 * vector.W;
		return result;
	}

	/// <summary>
	///       Multiplies a matrix with a column vector.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="vector">The column vector.</param>
	/// <returns>The resulting column vector.</returns>
	public static Vector4F Multiply(Matrix44F matrix, Vector4F vector)
	{
		Vector4F result = default;
		result.X = matrix.M00 * vector.X + matrix.M01 * vector.Y + matrix.M02 * vector.Z + matrix.M03 * vector.W;
		result.Y = matrix.M10 * vector.X + matrix.M11 * vector.Y + matrix.M12 * vector.Z + matrix.M13 * vector.W;
		result.Z = matrix.M20 * vector.X + matrix.M21 * vector.Y + matrix.M22 * vector.Z + matrix.M23 * vector.W;
		result.W = matrix.M30 * vector.X + matrix.M31 * vector.Y + matrix.M32 * vector.Z + matrix.M33 * vector.W;
		return result;
	}

	/// <summary>
	///       Divides a matrix by a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The matrix with each element divided by scalar.</returns>
	public static Matrix44F operator /(Matrix44F matrix, float scalar)
	{
		float num = 1f / scalar;
		matrix.M00 *= num;
		matrix.M01 *= num;
		matrix.M02 *= num;
		matrix.M03 *= num;
		matrix.M10 *= num;
		matrix.M11 *= num;
		matrix.M12 *= num;
		matrix.M13 *= num;
		matrix.M20 *= num;
		matrix.M21 *= num;
		matrix.M22 *= num;
		matrix.M23 *= num;
		matrix.M30 *= num;
		matrix.M31 *= num;
		matrix.M32 *= num;
		matrix.M33 *= num;
		return matrix;
	}

	/// <summary>
	///       Divides a matrix by a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The matrix with each element divided by scalar.</returns>
	public static Matrix44F Divide(Matrix44F matrix, float scalar)
	{
		float num = 1f / scalar;
		matrix.M00 *= num;
		matrix.M01 *= num;
		matrix.M02 *= num;
		matrix.M03 *= num;
		matrix.M10 *= num;
		matrix.M11 *= num;
		matrix.M12 *= num;
		matrix.M13 *= num;
		matrix.M20 *= num;
		matrix.M21 *= num;
		matrix.M22 *= num;
		matrix.M23 *= num;
		matrix.M30 *= num;
		matrix.M31 *= num;
		matrix.M32 *= num;
		matrix.M33 *= num;
		return matrix;
	}

	/// <summary>
	///       Tests if two matrices are equal.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>
	///   <see langword="true" /> if the matrices are equal; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator ==(Matrix44F matrix1, Matrix44F matrix2)
	{
		if (matrix1.M00 == matrix2.M00 && matrix1.M01 == matrix2.M01 && matrix1.M02 == matrix2.M02 && matrix1.M03 == matrix2.M03 && matrix1.M10 == matrix2.M10 && matrix1.M11 == matrix2.M11 && matrix1.M12 == matrix2.M12 && matrix1.M13 == matrix2.M13 && matrix1.M20 == matrix2.M20 && matrix1.M21 == matrix2.M21 && matrix1.M22 == matrix2.M22 && matrix1.M23 == matrix2.M23 && matrix1.M30 == matrix2.M30 && matrix1.M31 == matrix2.M31 && matrix1.M32 == matrix2.M32)
		{
			return matrix1.M33 == matrix2.M33;
		}
		return false;
	}

	/// <summary>
	///       Tests if two matrices are not equal.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>
	///   <see langword="true" /> if the matrices are different; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator !=(Matrix44F matrix1, Matrix44F matrix2)
	{
		if (matrix1.M00 == matrix2.M00 && matrix1.M01 == matrix2.M01 && matrix1.M02 == matrix2.M02 && matrix1.M03 == matrix2.M03 && matrix1.M10 == matrix2.M10 && matrix1.M11 == matrix2.M11 && matrix1.M12 == matrix2.M12 && matrix1.M13 == matrix2.M13 && matrix1.M20 == matrix2.M20 && matrix1.M21 == matrix2.M21 && matrix1.M22 == matrix2.M22 && matrix1.M23 == matrix2.M23 && matrix1.M30 == matrix2.M30 && matrix1.M31 == matrix2.M31 && matrix1.M32 == matrix2.M32)
		{
			return matrix1.M33 != matrix2.M33;
		}
		return true;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> 
	///       to a 2-dimensional float array.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator float[,](Matrix44F matrix)
	{
		return new float[4, 4]
		{
			{ matrix.M00, matrix.M01, matrix.M02, matrix.M03 },
			{ matrix.M10, matrix.M11, matrix.M12, matrix.M13 },
			{ matrix.M20, matrix.M21, matrix.M22, matrix.M23 },
			{ matrix.M30, matrix.M31, matrix.M32, matrix.M33 }
		};
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> to a 2-dimensional float array.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public float[,] ToArray2D()
	{
		return (float[,])this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> 
	///       to a jagged float array.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator float[][](Matrix44F matrix)
	{
		float[][] array = new float[4][]
		{
			new float[4],
			new float[4],
			new float[4],
			new float[4]
		};
		array[0][0] = matrix.M00;
		array[0][1] = matrix.M01;
		array[0][2] = matrix.M02;
		array[0][3] = matrix.M03;
		array[1][0] = matrix.M10;
		array[1][1] = matrix.M11;
		array[1][2] = matrix.M12;
		array[1][3] = matrix.M13;
		array[2][0] = matrix.M20;
		array[2][1] = matrix.M21;
		array[2][2] = matrix.M22;
		array[2][3] = matrix.M23;
		array[3][0] = matrix.M30;
		array[3][1] = matrix.M31;
		array[3][2] = matrix.M32;
		array[3][3] = matrix.M33;
		return array;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> to a jagged float array.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public float[][] ToArrayJagged()
	{
		return (float[][])this;
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixF" />.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator MatrixF(Matrix44F matrix)
	{
		MatrixF matrixF = new MatrixF(4, 4);
		matrixF[0, 0] = matrix.M00;
		matrixF[0, 1] = matrix.M01;
		matrixF[0, 2] = matrix.M02;
		matrixF[0, 3] = matrix.M03;
		matrixF[1, 0] = matrix.M10;
		matrixF[1, 1] = matrix.M11;
		matrixF[1, 2] = matrix.M12;
		matrixF[1, 3] = matrix.M13;
		matrixF[2, 0] = matrix.M20;
		matrixF[2, 1] = matrix.M21;
		matrixF[2, 2] = matrix.M22;
		matrixF[2, 3] = matrix.M23;
		matrixF[3, 0] = matrix.M30;
		matrixF[3, 1] = matrix.M31;
		matrixF[3, 2] = matrix.M32;
		matrixF[3, 3] = matrix.M33;
		return matrixF;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixF" /></summary>
	/// <returns>The result of the conversion.</returns>
	public MatrixF ToMatrixF()
	{
		return this;
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44D" />.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator Matrix44D(Matrix44F matrix)
	{
		return new Matrix44D(matrix.M00, matrix.M01, matrix.M02, matrix.M03, matrix.M10, matrix.M11, matrix.M12, matrix.M13, matrix.M20, matrix.M21, matrix.M22, matrix.M23, matrix.M30, matrix.M31, matrix.M32, matrix.M33);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44D" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public Matrix44D ToMatrix44D()
	{
		return this;
	}

	/// <summary>
	///       Performs an conversion from <see cref="T:Microsoft.Xna.Framework.Matrix" /> (XNA Framework) to 
	///       <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> (DigitalRune Mathematics).
	///       </summary>
	/// <param name="matrix">The <see cref="T:Microsoft.Xna.Framework.Matrix" /> (XNA Framework).</param>
	/// <returns>The <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> (DigitalRune Mathematics).</returns>
	public static explicit operator Matrix44F(Matrix matrix)
	{
		return new Matrix44F(matrix.M11, matrix.M21, matrix.M31, matrix.M41, matrix.M12, matrix.M22, matrix.M32, matrix.M42, matrix.M13, matrix.M23, matrix.M33, matrix.M43, matrix.M14, matrix.M24, matrix.M34, matrix.M44);
	}

	/// <summary>
	///       Converts a <see cref="T:Microsoft.Xna.Framework.Matrix" /> (XNA Framework) to a <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> (DigitalRune 
	///       Mathematics).
	///       </summary>
	/// <param name="matrix">The <see cref="T:Microsoft.Xna.Framework.Matrix" /> (XNA Framework).</param>
	/// <returns>The <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> (DigitalRune Mathematics).</returns>
	public static Matrix44F FromXna(Matrix matrix)
	{
		return new Matrix44F(matrix.M11, matrix.M21, matrix.M31, matrix.M41, matrix.M12, matrix.M22, matrix.M32, matrix.M42, matrix.M13, matrix.M23, matrix.M33, matrix.M43, matrix.M14, matrix.M24, matrix.M34, matrix.M44);
	}

	/// <summary>
	///       Performs an conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> 
	///       (DigitalRune Mathematics) to <see cref="T:Microsoft.Xna.Framework.Matrix" /> (XNA Framework).
	///       </summary>
	/// <param name="matrix">The <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> (DigitalRune Mathematics).</param>
	/// <returns>The <see cref="T:Microsoft.Xna.Framework.Matrix" /> (XNA Framework).</returns>
	public static explicit operator Matrix(Matrix44F matrix)
	{
		return new Matrix(matrix.M00, matrix.M10, matrix.M20, matrix.M30, matrix.M01, matrix.M11, matrix.M21, matrix.M31, matrix.M02, matrix.M12, matrix.M22, matrix.M32, matrix.M03, matrix.M13, matrix.M23, matrix.M33);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" /> (DigitalRune Mathematics) to <see cref="T:Microsoft.Xna.Framework.Matrix" /> 
	///       (XNA Framework).
	///       </summary>
	/// <returns>The <see cref="T:Microsoft.Xna.Framework.Matrix" /> (XNA Framework).</returns>
	public Matrix ToXna()
	{
		return new Matrix(M00, M10, M20, M30, M01, M11, M21, M31, M02, M12, M22, M32, M03, M13, M23, M33);
	}

	/// <summary>
	///       Sets each matrix element to its absolute value.
	///       </summary>
	public void Absolute()
	{
		M00 = Math.Abs(M00);
		M01 = Math.Abs(M01);
		M02 = Math.Abs(M02);
		M03 = Math.Abs(M03);
		M10 = Math.Abs(M10);
		M11 = Math.Abs(M11);
		M12 = Math.Abs(M12);
		M13 = Math.Abs(M13);
		M20 = Math.Abs(M20);
		M21 = Math.Abs(M21);
		M22 = Math.Abs(M22);
		M23 = Math.Abs(M23);
		M30 = Math.Abs(M30);
		M31 = Math.Abs(M31);
		M32 = Math.Abs(M32);
		M33 = Math.Abs(M33);
	}

	/// <summary>
	///       Clamps near-zero matrix elements to zero.
	///       </summary>
	public void ClampToZero()
	{
		M00 = Numeric.ClampToZero(M00);
		M01 = Numeric.ClampToZero(M01);
		M02 = Numeric.ClampToZero(M02);
		M03 = Numeric.ClampToZero(M03);
		M10 = Numeric.ClampToZero(M10);
		M11 = Numeric.ClampToZero(M11);
		M12 = Numeric.ClampToZero(M12);
		M13 = Numeric.ClampToZero(M13);
		M20 = Numeric.ClampToZero(M20);
		M21 = Numeric.ClampToZero(M21);
		M22 = Numeric.ClampToZero(M22);
		M23 = Numeric.ClampToZero(M23);
		M30 = Numeric.ClampToZero(M30);
		M31 = Numeric.ClampToZero(M31);
		M32 = Numeric.ClampToZero(M32);
		M33 = Numeric.ClampToZero(M33);
	}

	/// <summary>
	///       Clamps near-zero matrix elements to zero.
	///       </summary>
	/// <param name="epsilon">The tolerance value.</param>
	public void ClampToZero(float epsilon)
	{
		M00 = Numeric.ClampToZero(M00, epsilon);
		M01 = Numeric.ClampToZero(M01, epsilon);
		M02 = Numeric.ClampToZero(M02, epsilon);
		M03 = Numeric.ClampToZero(M03, epsilon);
		M10 = Numeric.ClampToZero(M10, epsilon);
		M11 = Numeric.ClampToZero(M11, epsilon);
		M12 = Numeric.ClampToZero(M12, epsilon);
		M13 = Numeric.ClampToZero(M13, epsilon);
		M20 = Numeric.ClampToZero(M20, epsilon);
		M21 = Numeric.ClampToZero(M21, epsilon);
		M22 = Numeric.ClampToZero(M22, epsilon);
		M23 = Numeric.ClampToZero(M23, epsilon);
		M30 = Numeric.ClampToZero(M30, epsilon);
		M31 = Numeric.ClampToZero(M31, epsilon);
		M32 = Numeric.ClampToZero(M32, epsilon);
		M33 = Numeric.ClampToZero(M33, epsilon);
	}

	/// <summary>
	///       Decomposes matrix into the scale, translation, and rotation components.
	///       </summary>
	/// <param name="scale">The scale component of the matrix.</param>
	/// <param name="rotation">The rotation component of the matrix.</param>
	/// <param name="translation">The translation component of the matrix.</param>
	/// <returns>
	///   <see langword="true" /> if the matrix was successfully decomposed; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Decompose(out Vector3F scale, out QuaternionF rotation, out Vector3F translation)
	{
		translation.X = M03;
		translation.Y = M13;
		translation.Z = M23;
		Vector3F vector3F = new Vector3F(M00, M10, M20);
		Vector3F vector3F2 = new Vector3F(M01, M11, M21);
		Vector3F vector3F3 = new Vector3F(M02, M12, M22);
		scale.X = vector3F.Length;
		scale.Y = vector3F2.Length;
		scale.Z = vector3F3.Length;
		if (Numeric.IsZero(scale.X) || Numeric.IsZero(scale.Y) || Numeric.IsZero(scale.Z))
		{
			rotation = QuaternionF.Identity;
			return false;
		}
		vector3F /= scale.X;
		vector3F2 /= scale.Y;
		vector3F3 /= scale.Z;
		Matrix33F rotationMatrix = new Matrix33F(vector3F.X, vector3F2.X, vector3F3.X, vector3F.Y, vector3F2.Y, vector3F3.Y, vector3F.Z, vector3F2.Z, vector3F3.Z);
		rotation = QuaternionF.CreateRotation(rotationMatrix);
		if (Numeric.IsZero(M30) && Numeric.IsZero(M31) && Numeric.IsZero(M32))
		{
			return Numeric.AreEqual(M33, 1f);
		}
		return false;
	}

	/// <summary>
	///       Gets a column as <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="index">The index of the column (0, 1, 2, or 3).</param>
	/// <returns>The column vector</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public Vector4F GetColumn(int index)
	{
		Vector4F result = default;
		switch (index)
		{
		case 0:
			result.X = M00;
			result.Y = M10;
			result.Z = M20;
			result.W = M30;
			break;
		case 1:
			result.X = M01;
			result.Y = M11;
			result.Z = M21;
			result.W = M31;
			break;
		case 2:
			result.X = M02;
			result.Y = M12;
			result.Z = M22;
			result.W = M32;
			break;
		case 3:
			result.X = M03;
			result.Y = M13;
			result.Z = M23;
			result.W = M33;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4837));
		}
		return result;
	}

	/// <summary>
	///       Sets a column from a <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="index">The index of the column (0, 1, 2, or 3).</param>
	/// <param name="columnVector">The column vector.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public void SetColumn(int index, Vector4F columnVector)
	{
		switch (index)
		{
		case 0:
			M00 = columnVector.X;
			M10 = columnVector.Y;
			M20 = columnVector.Z;
			M30 = columnVector.W;
			break;
		case 1:
			M01 = columnVector.X;
			M11 = columnVector.Y;
			M21 = columnVector.Z;
			M31 = columnVector.W;
			break;
		case 2:
			M02 = columnVector.X;
			M12 = columnVector.Y;
			M22 = columnVector.Z;
			M32 = columnVector.W;
			break;
		case 3:
			M03 = columnVector.X;
			M13 = columnVector.Y;
			M23 = columnVector.Z;
			M33 = columnVector.W;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4837));
		}
	}

	/// <summary>
	///       Gets a row as <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="index">The index of the row (0, 1, 2, or 3).</param>
	/// <returns>The row vector</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public Vector4F GetRow(int index)
	{
		Vector4F result = default;
		switch (index)
		{
		case 0:
			result.X = M00;
			result.Y = M01;
			result.Z = M02;
			result.W = M03;
			break;
		case 1:
			result.X = M10;
			result.Y = M11;
			result.Z = M12;
			result.W = M13;
			break;
		case 2:
			result.X = M20;
			result.Y = M21;
			result.Z = M22;
			result.W = M23;
			break;
		case 3:
			result.X = M30;
			result.Y = M31;
			result.Z = M32;
			result.W = M33;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4965));
		}
		return result;
	}

	/// <summary>
	///       Sets a row from a <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="index">The index of the row (0, 1, 2, or 3).</param>
	/// <param name="rowVector">The row vector.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public void SetRow(int index, Vector4F rowVector)
	{
		switch (index)
		{
		case 0:
			M00 = rowVector.X;
			M01 = rowVector.Y;
			M02 = rowVector.Z;
			M03 = rowVector.W;
			break;
		case 1:
			M10 = rowVector.X;
			M11 = rowVector.Y;
			M12 = rowVector.Z;
			M13 = rowVector.W;
			break;
		case 2:
			M20 = rowVector.X;
			M21 = rowVector.Y;
			M22 = rowVector.Z;
			M23 = rowVector.W;
			break;
		case 3:
			M30 = rowVector.X;
			M31 = rowVector.Y;
			M32 = rowVector.Z;
			M33 = rowVector.W;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4965));
		}
	}

	/// <summary>
	///       Inverts the matrix.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The matrix is singular (i.e. it is not invertible).
	///       </exception>
	public void Invert()
	{
		if (!TryInvert())
		{
			throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5262));
		}
	}

	/// <summary>
	///       Converts this matrix to an array of float values.
	///       </summary>
	/// <param name="order">The order of the matrix elements in the array.</param>
	/// <returns>The result of the conversion.</returns>
	public float[] ToArray1D(MatrixOrder order)
	{
		float[] array = new float[16];
		if (order == MatrixOrder.ColumnMajor)
		{
			array[0] = M00;
			array[1] = M10;
			array[2] = M20;
			array[3] = M30;
			array[4] = M01;
			array[5] = M11;
			array[6] = M21;
			array[7] = M31;
			array[8] = M02;
			array[9] = M12;
			array[10] = M22;
			array[11] = M32;
			array[12] = M03;
			array[13] = M13;
			array[14] = M23;
			array[15] = M33;
		}
		else
		{
			array[0] = M00;
			array[1] = M01;
			array[2] = M02;
			array[3] = M03;
			array[4] = M10;
			array[5] = M11;
			array[6] = M12;
			array[7] = M13;
			array[8] = M20;
			array[9] = M21;
			array[10] = M22;
			array[11] = M23;
			array[12] = M30;
			array[13] = M31;
			array[14] = M32;
			array[15] = M33;
		}
		return array;
	}

	/// <summary>
	///       Converts this matrix to a list of float values.
	///       </summary>
	/// <param name="order">The order of the matrix elements in the list.</param>
	/// <returns>The result of the conversion.</returns>
	public IList<float> ToList(MatrixOrder order)
	{
		List<float> list = new List<float>(16);
		if (order == MatrixOrder.ColumnMajor)
		{
			list.Add(M00);
			list.Add(M10);
			list.Add(M20);
			list.Add(M30);
			list.Add(M01);
			list.Add(M11);
			list.Add(M21);
			list.Add(M31);
			list.Add(M02);
			list.Add(M12);
			list.Add(M22);
			list.Add(M32);
			list.Add(M03);
			list.Add(M13);
			list.Add(M23);
			list.Add(M33);
		}
		else
		{
			list.Add(M00);
			list.Add(M01);
			list.Add(M02);
			list.Add(M03);
			list.Add(M10);
			list.Add(M11);
			list.Add(M12);
			list.Add(M13);
			list.Add(M20);
			list.Add(M21);
			list.Add(M22);
			list.Add(M23);
			list.Add(M30);
			list.Add(M31);
			list.Add(M32);
			list.Add(M33);
		}
		return list;
	}

	/// <summary>
	///       Inverts the matrix if it is invertible.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if the matrix is invertible; otherwise <see langword="false" />.
	///       </returns>
	public bool TryInvert()
	{
		float num = M22 * M33 - M23 * M32;
		float num2 = M21 * M33 - M23 * M31;
		float num3 = M21 * M32 - M22 * M31;
		float num4 = M20 * M33 - M23 * M30;
		float num5 = M20 * M32 - M22 * M30;
		float num6 = M20 * M31 - M21 * M30;
		float num7 = M11 * num - M12 * num2 + M13 * num3;
		float num8 = M10 * num - M12 * num4 + M13 * num5;
		float num9 = M10 * num2 - M11 * num4 + M13 * num6;
		float num10 = M10 * num3 - M11 * num5 + M12 * num6;
		float num11 = M00 * num7 - M01 * num8 + M02 * num9 - M03 * num10;
		if (Numeric.IsZero(num11, Numeric.EpsilonFSquared * Numeric.EpsilonFSquared))
		{
			return false;
		}
		float num12 = M01 * num - M02 * num2 + M03 * num3;
		float num13 = M00 * num - M02 * num4 + M03 * num5;
		float num14 = M00 * num2 - M01 * num4 + M03 * num6;
		float num15 = M00 * num3 - M01 * num5 + M02 * num6;
		float num16 = M02 * M13 - M03 * M12;
		float num17 = M01 * M13 - M03 * M11;
		float num18 = M01 * M12 - M02 * M11;
		float num19 = M00 * M13 - M03 * M10;
		float num20 = M00 * M12 - M02 * M10;
		float num21 = M00 * M11 - M01 * M10;
		float num22 = M31 * num16 - M32 * num17 + M33 * num18;
		float num23 = M30 * num16 - M32 * num19 + M33 * num20;
		float num24 = M30 * num17 - M31 * num19 + M33 * num21;
		float num25 = M30 * num18 - M31 * num20 + M32 * num21;
		float num26 = M21 * num16 - M22 * num17 + M23 * num18;
		float num27 = M20 * num16 - M22 * num19 + M23 * num20;
		float num28 = M20 * num17 - M21 * num19 + M23 * num21;
		float num29 = M20 * num18 - M21 * num20 + M22 * num21;
		float num30 = 1f / num11;
		M00 = num7 * num30;
		M01 = (0f - num12) * num30;
		M02 = num22 * num30;
		M03 = (0f - num26) * num30;
		M10 = (0f - num8) * num30;
		M11 = num13 * num30;
		M12 = (0f - num23) * num30;
		M13 = num27 * num30;
		M20 = num9 * num30;
		M21 = (0f - num14) * num30;
		M22 = num24 * num30;
		M23 = (0f - num28) * num30;
		M30 = (0f - num10) * num30;
		M31 = num15 * num30;
		M32 = (0f - num25) * num30;
		M33 = num29 * num30;
		return true;
	}

	/// <summary>
	///       Transforms a position.
	///       </summary>
	/// <param name="position">The position.</param>
	/// <returns>The transformed position.</returns>
	public Vector3F TransformPosition(Vector3F position)
	{
		float num = M00 * position.X + M01 * position.Y + M02 * position.Z + M03;
		float num2 = M10 * position.X + M11 * position.Y + M12 * position.Z + M13;
		float num3 = M20 * position.X + M21 * position.Y + M22 * position.Z + M23;
		float num4 = M30 * position.X + M31 * position.Y + M32 * position.Z + M33;
		if (!Numeric.AreEqual(num4, 1f))
		{
			float num5 = 1f / num4;
			num *= num5;
			num2 *= num5;
			num3 *= num5;
		}
		position.X = num;
		position.Y = num2;
		position.Z = num3;
		return position;
	}

	/// <summary>
	///       Transforms a direction vector (or tangent vector).
	///       </summary>
	/// <param name="direction">The direction vector.</param>
	/// <param name="position">The position.</param>
	/// <returns>The transformed direction vector.</returns>
	public Vector3F TransformDirection(Vector3F direction)
	{
		float x = M00 * direction.X + M01 * direction.Y + M02 * direction.Z;
		float y = M10 * direction.X + M11 * direction.Y + M12 * direction.Z;
		float z = M20 * direction.X + M21 * direction.Y + M22 * direction.Z;
		direction.X = x;
		direction.Y = y;
		direction.Z = z;
		return direction;
	}

	/// <summary>
	///       Transforms a normal vector.
	///       </summary>
	/// <param name="normal">The normal vector.</param>
	/// <param name="position">The position.</param>
	/// <returns>
	///       The transformed normal. (Note: The resulting vector might need to be normalized!)
	///       </returns>
	public Vector3F TransformNormal(Vector3F normal)
	{
		Matrix33F inverse = Minor.Inverse;
		float x = inverse.M00 * normal.X + inverse.M10 * normal.Y + inverse.M20 * normal.Z;
		float y = inverse.M01 * normal.X + inverse.M11 * normal.Y + inverse.M21 * normal.Z;
		float z = inverse.M02 * normal.X + inverse.M12 * normal.Y + inverse.M22 * normal.Z;
		normal.X = x;
		normal.Y = y;
		normal.Z = z;
		return normal;
	}

	/// <summary>
	///       Transposes this matrix.
	///       </summary>
	public void Transpose()
	{
		MathHelper.Swap(ref M01, ref M10);
		MathHelper.Swap(ref M02, ref M20);
		MathHelper.Swap(ref M03, ref M30);
		MathHelper.Swap(ref M12, ref M21);
		MathHelper.Swap(ref M13, ref M31);
		MathHelper.Swap(ref M23, ref M32);
	}

	/// <summary>
	///       Returns a matrix with the absolute values of the elements of the given matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>A matrix with the absolute values of the elements of the given matrix.</returns>
	public static Matrix44F Absolute(Matrix44F matrix)
	{
		return new Matrix44F(Math.Abs(matrix.M00), Math.Abs(matrix.M01), Math.Abs(matrix.M02), Math.Abs(matrix.M03), Math.Abs(matrix.M10), Math.Abs(matrix.M11), Math.Abs(matrix.M12), Math.Abs(matrix.M13), Math.Abs(matrix.M20), Math.Abs(matrix.M21), Math.Abs(matrix.M22), Math.Abs(matrix.M23), Math.Abs(matrix.M30), Math.Abs(matrix.M31), Math.Abs(matrix.M32), Math.Abs(matrix.M33));
	}

	/// <summary>
	///       Determines whether two matrices are equal (regarding the 
	///       tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>
	///   <see langword="true" /> if the matrices are equal (within the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />); otherwise, <see langword="false" />.
	///       </returns>
	public static bool AreNumericallyEqual(Matrix44F matrix1, Matrix44F matrix2)
	{
		if (Numeric.AreEqual(matrix1.M00, matrix2.M00) && Numeric.AreEqual(matrix1.M01, matrix2.M01) && Numeric.AreEqual(matrix1.M02, matrix2.M02) && Numeric.AreEqual(matrix1.M03, matrix2.M03) && Numeric.AreEqual(matrix1.M10, matrix2.M10) && Numeric.AreEqual(matrix1.M11, matrix2.M11) && Numeric.AreEqual(matrix1.M12, matrix2.M12) && Numeric.AreEqual(matrix1.M13, matrix2.M13) && Numeric.AreEqual(matrix1.M20, matrix2.M20) && Numeric.AreEqual(matrix1.M21, matrix2.M21) && Numeric.AreEqual(matrix1.M22, matrix2.M22) && Numeric.AreEqual(matrix1.M23, matrix2.M23) && Numeric.AreEqual(matrix1.M30, matrix2.M30) && Numeric.AreEqual(matrix1.M31, matrix2.M31) && Numeric.AreEqual(matrix1.M32, matrix2.M32))
		{
			return Numeric.AreEqual(matrix1.M33, matrix2.M33);
		}
		return false;
	}

	/// <summary>
	///       Determines whether two matrices are equal (regarding a specific 
	///       tolerance).
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if the matrices are equal (within the tolerance 
	///       <paramref name="epsilon" />); otherwise, <see langword="false" />.
	///       </returns>
	public static bool AreNumericallyEqual(Matrix44F matrix1, Matrix44F matrix2, float epsilon)
	{
		if (Numeric.AreEqual(matrix1.M00, matrix2.M00, epsilon) && Numeric.AreEqual(matrix1.M01, matrix2.M01, epsilon) && Numeric.AreEqual(matrix1.M02, matrix2.M02, epsilon) && Numeric.AreEqual(matrix1.M03, matrix2.M03, epsilon) && Numeric.AreEqual(matrix1.M10, matrix2.M10, epsilon) && Numeric.AreEqual(matrix1.M11, matrix2.M11, epsilon) && Numeric.AreEqual(matrix1.M12, matrix2.M12, epsilon) && Numeric.AreEqual(matrix1.M13, matrix2.M13, epsilon) && Numeric.AreEqual(matrix1.M20, matrix2.M20, epsilon) && Numeric.AreEqual(matrix1.M21, matrix2.M21, epsilon) && Numeric.AreEqual(matrix1.M22, matrix2.M22, epsilon) && Numeric.AreEqual(matrix1.M23, matrix2.M23, epsilon) && Numeric.AreEqual(matrix1.M30, matrix2.M30, epsilon) && Numeric.AreEqual(matrix1.M31, matrix2.M31, epsilon) && Numeric.AreEqual(matrix1.M32, matrix2.M32, epsilon))
		{
			return Numeric.AreEqual(matrix1.M33, matrix2.M33, epsilon);
		}
		return false;
	}

	/// <summary>
	///       Returns a matrix with the matrix elements clamped to the range [min, max].
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>
	///       The matrix with small elements clamped to zero.
	///       </returns>
	public static Matrix44F ClampToZero(Matrix44F matrix)
	{
		matrix.M00 = Numeric.ClampToZero(matrix.M00);
		matrix.M01 = Numeric.ClampToZero(matrix.M01);
		matrix.M02 = Numeric.ClampToZero(matrix.M02);
		matrix.M03 = Numeric.ClampToZero(matrix.M03);
		matrix.M10 = Numeric.ClampToZero(matrix.M10);
		matrix.M11 = Numeric.ClampToZero(matrix.M11);
		matrix.M12 = Numeric.ClampToZero(matrix.M12);
		matrix.M13 = Numeric.ClampToZero(matrix.M13);
		matrix.M20 = Numeric.ClampToZero(matrix.M20);
		matrix.M21 = Numeric.ClampToZero(matrix.M21);
		matrix.M22 = Numeric.ClampToZero(matrix.M22);
		matrix.M23 = Numeric.ClampToZero(matrix.M23);
		matrix.M30 = Numeric.ClampToZero(matrix.M30);
		matrix.M31 = Numeric.ClampToZero(matrix.M31);
		matrix.M32 = Numeric.ClampToZero(matrix.M32);
		matrix.M33 = Numeric.ClampToZero(matrix.M33);
		return matrix;
	}

	/// <summary>
	///       Returns a matrix with the matrix elements clamped to the range [min, max].
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///       The matrix with small elements clamped to zero.
	///       </returns>
	public static Matrix44F ClampToZero(Matrix44F matrix, float epsilon)
	{
		matrix.M00 = Numeric.ClampToZero(matrix.M00, epsilon);
		matrix.M01 = Numeric.ClampToZero(matrix.M01, epsilon);
		matrix.M02 = Numeric.ClampToZero(matrix.M02, epsilon);
		matrix.M03 = Numeric.ClampToZero(matrix.M03, epsilon);
		matrix.M10 = Numeric.ClampToZero(matrix.M10, epsilon);
		matrix.M11 = Numeric.ClampToZero(matrix.M11, epsilon);
		matrix.M12 = Numeric.ClampToZero(matrix.M12, epsilon);
		matrix.M13 = Numeric.ClampToZero(matrix.M13, epsilon);
		matrix.M20 = Numeric.ClampToZero(matrix.M20, epsilon);
		matrix.M21 = Numeric.ClampToZero(matrix.M21, epsilon);
		matrix.M22 = Numeric.ClampToZero(matrix.M22, epsilon);
		matrix.M23 = Numeric.ClampToZero(matrix.M23, epsilon);
		matrix.M30 = Numeric.ClampToZero(matrix.M30, epsilon);
		matrix.M31 = Numeric.ClampToZero(matrix.M31, epsilon);
		matrix.M32 = Numeric.ClampToZero(matrix.M32, epsilon);
		matrix.M33 = Numeric.ClampToZero(matrix.M33, epsilon);
		return matrix;
	}

	/// <summary>
	///       Creates a scaling matrix.
	///       </summary>
	/// <param name="scale">
	///       The uniform scale factor that is applied to the x-, y-, and z-axis.
	///       </param>
	/// <returns>The created scaling matrix.</returns>
	public static Matrix44F CreateScale(float scale)
	{
		return new Matrix44F
		{
			M00 = scale,
			M11 = scale,
			M22 = scale,
			M33 = 1f
		};
	}

	/// <summary>
	///       Creates a scaling matrix.
	///       </summary>
	/// <param name="scaleX">The value to scale by on the x-axis.</param>
	/// <param name="scaleY">The value to scale by on the y-axis.</param>
	/// <param name="scaleZ">The value to scale by on the z-axis.</param>
	/// <returns>The created scaling matrix.</returns>
	public static Matrix44F CreateScale(float scaleX, float scaleY, float scaleZ)
	{
		return new Matrix44F
		{
			M00 = scaleX,
			M11 = scaleY,
			M22 = scaleZ,
			M33 = 1f
		};
	}

	/// <summary>
	///       Creates a scaling matrix.
	///       </summary>
	/// <param name="scale">Amounts to scale by the x, y, and z-axis.</param>
	/// <returns>The created scaling matrix.</returns>
	public static Matrix44F CreateScale(Vector3F scale)
	{
		return new Matrix44F
		{
			M00 = scale.X,
			M11 = scale.Y,
			M22 = scale.Z,
			M33 = 1f
		};
	}

	/// <summary>
	///       Creates a rotation matrix from axis and angle.
	///       </summary>
	/// <param name="axis">The rotation axis. (Does not need to be normalized.)</param>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>The created rotation matrix.</returns>
	/// <exception cref="T:System.ArgumentException">The axis vector has 0 length.</exception>
	public static Matrix44F CreateRotation(Vector3F axis, float angle)
	{
		try
		{
			axis.Normalize();
		}
		catch (DivideByZeroException)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5357));
		}
		float x = axis.X;
		float y = axis.Y;
		float z = axis.Z;
		float num = x * x;
		float num2 = y * y;
		float num3 = z * z;
		float num4 = x * y;
		float num5 = x * z;
		float num6 = y * z;
		float num7 = (float)Math.Cos(angle);
		float num8 = (float)Math.Sin(angle);
		float num9 = x * num8;
		float num10 = y * num8;
		float num11 = z * num8;
		float num12 = 1f - num7;
		Matrix44F result = default;
		result.M00 = num + num7 * (1f - num);
		result.M01 = num4 * num12 - num11;
		result.M02 = num5 * num12 + num10;
		result.M03 = 0f;
		result.M10 = num4 * num12 + num11;
		result.M11 = num2 + num7 * (1f - num2);
		result.M12 = num6 * num12 - num9;
		result.M13 = 0f;
		result.M20 = num5 * num12 - num10;
		result.M21 = num6 * num12 + num9;
		result.M22 = num3 + num7 * (1f - num3);
		result.M23 = 0f;
		result.M30 = 0f;
		result.M31 = 0f;
		result.M32 = 0f;
		result.M33 = 1f;
		return result;
	}

	/// <summary>
	///       Creates a rotation matrix from a unit quaternion.
	///       </summary>
	/// <param name="rotation">The rotation described by a unit quaternion.</param>
	/// <returns>The created rotation matrix.</returns>
	public static Matrix44F CreateRotation(QuaternionF rotation)
	{
		return rotation.ToRotationMatrix44();
	}

	/// <summary>
	///       Creates a translation matrix from the given values.
	///       </summary>
	/// <param name="x">The translation along the x-axis.</param>
	/// <param name="y">The translation along the y-axis.</param>
	/// <param name="z">The translation along the z-axis.</param>
	/// <returns>
	///       A transformation matrix that translates vectors.
	///       </returns>
	public static Matrix44F CreateTranslation(float x, float y, float z)
	{
		Matrix44F identity = Identity;
		identity.M03 = x;
		identity.M13 = y;
		identity.M23 = z;
		identity.M33 = 1f;
		return identity;
	}

	/// <summary>
	///       Creates a translation matrix from a vector.
	///       </summary>
	/// <param name="translation">The translation.</param>
	/// <returns>A transformation matrix that translates vectors.</returns>
	public static Matrix44F CreateTranslation(Vector3F translation)
	{
		Matrix44F identity = Identity;
		identity.M03 = translation.X;
		identity.M13 = translation.Y;
		identity.M23 = translation.Z;
		identity.M33 = 1f;
		return identity;
	}

	/// <summary>
	///       Creates a matrix that specifies a rotation around the x-axis.
	///       </summary>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>The created rotation matrix.</returns>
	public static Matrix44F CreateRotationX(float angle)
	{
		float num = (float)Math.Cos(angle);
		float num2 = (float)Math.Sin(angle);
		return new Matrix44F(1f, 0f, 0f, 0f, 0f, num, 0f - num2, 0f, 0f, num2, num, 0f, 0f, 0f, 0f, 1f);
	}

	/// <summary>
	///       Creates a matrix that specifies a rotation around the y-axis.
	///       </summary>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>The created rotation matrix.</returns>
	public static Matrix44F CreateRotationY(float angle)
	{
		float num = (float)Math.Cos(angle);
		float num2 = (float)Math.Sin(angle);
		return new Matrix44F(num, 0f, num2, 0f, 0f, 1f, 0f, 0f, 0f - num2, 0f, num, 0f, 0f, 0f, 0f, 1f);
	}

	/// <summary>
	///       Creates a matrix that specifies a rotation around the z-axis.
	///       </summary>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>The created rotation matrix.</returns>
	public static Matrix44F CreateRotationZ(float angle)
	{
		float num = (float)Math.Cos(angle);
		float num2 = (float)Math.Sin(angle);
		return new Matrix44F(num, 0f - num2, 0f, 0f, num2, num, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
	}

	/// <summary>
	///       Creates a right-handed look-at matrix (view matrix). (Only available in the XNA-compatible 
	///       build.)
	///       </summary>
	/// <param name="position">The position of the viewer.</param>
	/// <param name="target">The target at which the viewer is looking.</param>
	/// <param name="upVector">
	///       The direction that is "up" from the viewer's point of view. (Does not need to be 
	///       normalized.)
	///       </param>
	/// <returns>The right-handed look-at matrix (view matrix).</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="position" /> is the same as <paramref name="target" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="upVector" /> is (0, 0, 0).
	///       </exception>
	/// <exception cref="T:System.DivideByZeroException">
	///       The camera direction (<paramref name="target" /> - <paramref name="position" />) 
	///       is probably pointing in the same or opposite direction as <paramref name="upVector" />. 
	///       (The two vectors must not be parallel.)
	///       </exception>
	public static Matrix44F CreateLookAt(Vector3F position, Vector3F target, Vector3F upVector)
	{
		if (Vector3F.AreNumericallyEqual(position, target))
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5416), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5541));
		}
		if (upVector.IsNumericallyZero)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5554), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5639));
		}
		Vector3F normalized = (position - target).Normalized;
		Vector3F normalized2 = Vector3F.Cross(upVector, normalized).Normalized;
		Vector3F vector = Vector3F.Cross(normalized, normalized2);
		return new Matrix44F(normalized2.X, normalized2.Y, normalized2.Z, 0f - Vector3F.Dot(normalized2, position), vector.X, vector.Y, vector.Z, 0f - Vector3F.Dot(vector, position), normalized.X, normalized.Y, normalized.Z, 0f - Vector3F.Dot(normalized, position), 0f, 0f, 0f, 1f);
	}

	/// <summary>
	///       Creates a right-handed, orthographic projection matrix. (Only available in the XNA 
	///       compatible build.)
	///       </summary>
	/// <param name="width">The width of the view volume.</param>
	/// <param name="height">The height of the view volume.</param>
	/// <param name="zNear">
	///       The minimum z-value of the view volume. (Distance of the near view plane.)
	///       </param>
	/// <param name="zFar">
	///       The maximum z-value of the view volume. (Distance of the far view plane.)
	///       </param>
	/// <returns>The right-handed orthographic projection matrix.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="width" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="height" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="zNear" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="zNear" /> is greater than or equal to <paramref name="zFar" /></exception>
	public static Matrix44F CreateOrthographic(float width, float height, float zNear, float zFar)
	{
		if (width <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5656), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5667));
		}
		if (height <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5728), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5741));
		}
		if (zNear < 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5815));
		}
		if (zNear >= zFar)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5930));
		}
		return new Matrix44F(2f / width, 0f, 0f, 0f, 0f, 2f / height, 0f, 0f, 0f, 0f, 1f / (zNear - zFar), zNear / (zNear - zFar), 0f, 0f, 0f, 1f);
	}

	/// <summary>
	///       Creates a customized (off-center), right-handed, orthographic projection matrix. (Only 
	///       available in the XNA-compatible build.)
	///       </summary>
	/// <param name="left">The minimum x-value of the view volume.</param>
	/// <param name="right">The maximum x-value of the view volume.</param>
	/// <param name="bottom">The minimum y-value of the view volume.</param>
	/// <param name="top">The maximum y-value of the view volume.</param>
	/// <param name="zNear">
	///       The minimum z-value of the view volume. (Distance of the near view plane.)
	///       </param>
	/// <param name="zFar">
	///       The maximum z-value of the view volume. (Distance of the far view plane.)
	///       </param>
	/// <returns>The customized (off-center), right-handed orthographic projection matrix.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="left" /> is greater than or equal to <paramref name="right" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="bottom" /> is greater than or equal to <paramref name="top" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="zNear" /> is negative.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="zNear" /> is greater than or equal to <paramref name="zFar" /></exception>
	public static Matrix44F CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNear, float zFar)
	{
		if (left >= right)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6140), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6149));
		}
		if (bottom >= top)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6297), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6310));
		}
		if (zNear < 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5815));
		}
		if (zNear >= zFar)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5930));
		}
		return new Matrix44F(2f / (right - left), 0f, 0f, (left + right) / (left - right), 0f, 2f / (top - bottom), 0f, (top + bottom) / (bottom - top), 0f, 0f, 1f / (zNear - zFar), zNear / (zNear - zFar), 0f, 0f, 0f, 1f);
	}

	/// <summary>
	///       Creates a right-handed, perspective projection matrix. (Only available in the XNA-compatible 
	///       build.)
	///       </summary>
	/// <param name="width">The width of the view volume at the near view-plane.</param>
	/// <param name="height">The height of the view volume at the near view-plane.</param>
	/// <param name="zNear">
	///       The minimum z-value of the view volume. (Distance of the near view plane.)
	///       </param>
	/// <param name="zFar">
	///       The maximum z-value of the view volume. (Distance of the far view plane.)
	///       </param>
	/// <returns>The right-handed, perspective projection matrix.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="width" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="height" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="zNear" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="zNear" /> is greater than or equal to <paramref name="zFar" /></exception>
	public static Matrix44F CreatePerspective(float width, float height, float zNear, float zFar)
	{
		if (width <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5656), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5667));
		}
		if (height <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5728), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5741));
		}
		if (zNear <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6458));
		}
		if (zNear >= zFar)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5930));
		}
		return new Matrix44F(2f * zNear / width, 0f, 0f, 0f, 0f, 2f * zNear / height, 0f, 0f, 0f, 0f, zFar / (zNear - zFar), zNear * zFar / (zNear - zFar), 0f, 0f, -1f, 0f);
	}

	/// <summary>
	///       Creates a right-handed, perspective projection matrix based on a field of view. (Only 
	///       available in the XNA-compatible build.)
	///       </summary>
	/// <param name="fieldOfViewY">The vertical field of view.</param>
	/// <param name="aspectRatio">The aspect ratio (width / height).</param>
	/// <param name="zNear">
	///       The minimum z-value of the view volume. (Distance of the near view plane.)
	///       </param>
	/// <param name="zFar">
	///       The maximum z-value of the view volume. (Distance of the far view plane.)
	///       </param>
	/// <returns>
	///       The right-handed, perspective projection matrix.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="fieldOfViewY" /> is not between 0 and <c>π</c> radians (0° and 180°).
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="aspectRatio" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="zNear" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="zNear" /> is greater than or equal to <paramref name="zFar" /></exception>
	public static Matrix44F CreatePerspectiveFieldOfView(float fieldOfViewY, float aspectRatio, float zNear, float zFar)
	{
		if (fieldOfViewY <= 0f || fieldOfViewY >= (float)Math.PI)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6583), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6608));
		}
		if (aspectRatio <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6725), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6748));
		}
		if (zNear <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6458));
		}
		if (zNear >= zFar)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5930));
		}
		float num = 1f / (float)Math.Tan(0.5f * fieldOfViewY);
		float m = num / aspectRatio;
		return new Matrix44F(m, 0f, 0f, 0f, 0f, num, 0f, 0f, 0f, 0f, zFar / (zNear - zFar), zNear * zFar / (zNear - zFar), 0f, 0f, -1f, 0f);
	}

	/// <summary>
	///       Creates a customized, right-handed, perspective projection matrix. (Only available in the 
	///       XNA-compatible build.)
	///       </summary>
	/// <param name="left">The minimum x-value of the view volume at the near view plane.</param>
	/// <param name="right">The maximum x-value of the view volume at the near view plane.</param>
	/// <param name="bottom">The minimum y-value of the view volume at the near view plane.</param>
	/// <param name="top">The maximum y-value of the view volume at the near view plane.</param>
	/// <param name="zNear">
	///       The minimum z-value of the view volume. (Distance of the near view plane.)
	///       </param>
	/// <param name="zFar">
	///       The maximum z-value of the view volume. (Distance of the far view plane.)
	///       </param>
	/// <returns>
	///       The customized, right-handed, perspective projection matrix.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="left" /> is greater than or equal to <paramref name="right" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="bottom" /> is greater than or equal to <paramref name="top" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="zNear" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="zNear" /> is greater than or equal to <paramref name="zFar" /></exception>
	public static Matrix44F CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float zNear, float zFar)
	{
		if (left >= right)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6140), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6149));
		}
		if (bottom >= top)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6297), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6310));
		}
		if (zNear <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6458));
		}
		if (zNear >= zFar)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5930));
		}
		return new Matrix44F(2f * zNear / (right - left), 0f, (left + right) / (right - left), 0f, 0f, 2f * zNear / (top - bottom), (top + bottom) / (top - bottom), 0f, 0f, 0f, zFar / (zNear - zFar), zNear * zFar / (zNear - zFar), 0f, 0f, -1f, 0f);
	}
}
