using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Defines a 2 x 2 matrix (single-precision).
///       </summary>
[Serializable]
[TypeConverter(typeof(ExpandableObjectConverter))]
public struct Matrix22F : IEquatable<Matrix22F>
{
	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> with all of its components set to zero.
	///       </summary>
	public static readonly Matrix22F Zero;

	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> with all of its components set to one.
	///       </summary>
	public static readonly Matrix22F One;

	/// <summary>
	///       Returns the 2 x 2 identity matrix.
	///       </summary>
	public static readonly Matrix22F Identity;

	/// <summary>
	///       The element in first row, first column.
	///       </summary>
	public float M00;

	/// <summary>
	///       The element in first row, second column.
	///       </summary>
	public float M01;

	/// <summary>
	///       The element in second row, first column.
	///       </summary>
	public float M10;

	/// <summary>
	///       The element in second row, second column.
	///       </summary>
	public float M11;

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
				2 => M10, 
				3 => M11, 
				_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1175)), 
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
				M10 = value;
				break;
			case 3:
				M11 = value;
				break;
			default:
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1175));
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
					_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7292)), 
				}, 
				1 => column switch
				{
					0 => M10, 
					1 => M11, 
					_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7292)), 
				}, 
				_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4958), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7415)), 
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
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7292));
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
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7292));
				}
				break;
			default:
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4958), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7072));
			}
		}
	}

	/// <summary>
	///       Returns the determinant of this matrix.
	///       </summary>
	public float Determinant => M00 * M11 - M01 * M10;

	/// <summary>
	///       Gets a value indicating whether an element of the matrix is <see cref="F:System.Single.NaN" />.
	///       </summary>
	public bool IsNaN
	{
		get
		{
			if (!Numeric.IsNaN(M00) && !Numeric.IsNaN(M01) && !Numeric.IsNaN(M10))
			{
				return Numeric.IsNaN(M11);
			}
			return true;
		}
	}

	/// <summary>
	///       Gets a value indicating whether this matrix is symmetric.
	///       </summary>
	public bool IsSymmetric => M01 == M10;

	/// <summary>
	///       Gets the matrix trace (the sum of the diagonal elements).
	///       </summary>
	public float Trace => M00 + M11;

	/// <summary>
	///       Returns the transposed of this matrix.
	///       </summary>
	/// <returns>The transposed of this matrix.</returns>
	public Matrix22F Transposed
	{
		get
		{
			Matrix22F result = this;
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
	public Matrix22F Inverse
	{
		get
		{
			Matrix22F result = this;
			result.Invert();
			return result;
		}
	}

	static Matrix22F()
	{
		Zero = new Matrix22F(0f, 0f, 0f, 0f);
		One = new Matrix22F(1f, 1f, 1f, 1f);
		Identity = new Matrix22F(1f, 0f, 0f, 1f);
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> struct.
	///       </summary>
	/// <param name="elementValue">The initial value for the matrix elements.</param>
	public Matrix22F(float elementValue)
		: this(elementValue, elementValue, elementValue, elementValue)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> class.
	///       </summary>
	/// <param name="m00">The element in the first row, first column.</param>
	/// <param name="m01">The element in the first row, second column.</param>
	/// <param name="m10">The element in the second row, first column.</param>
	/// <param name="m11">The element in the second row, second column.</param>
	public Matrix22F(float m00, float m01, float m10, float m11)
	{
		M00 = m00;
		M01 = m01;
		M10 = m10;
		M11 = m11;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> struct.
	///       </summary>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <param name="order">The order of the matrix elements in <paramref name="elements" />.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> has less than 4 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public Matrix22F(float[] elements, MatrixOrder order)
	{
		if (order == MatrixOrder.RowMajor)
		{
			M00 = elements[0];
			M01 = elements[1];
			M10 = elements[2];
			M11 = elements[3];
		}
		else
		{
			M00 = elements[0];
			M10 = elements[1];
			M01 = elements[2];
			M11 = elements[3];
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> struct.
	///       </summary>
	/// <param name="elements">The list with the initial values for the matrix elements.</param>
	/// <param name="order">The order of the matrix elements in <paramref name="elements" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="elements" /> has less than 4 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public Matrix22F(IList<float> elements, MatrixOrder order)
	{
		if (order == MatrixOrder.RowMajor)
		{
			M00 = elements[0];
			M01 = elements[1];
			M10 = elements[2];
			M11 = elements[3];
		}
		else
		{
			M00 = elements[0];
			M10 = elements[1];
			M01 = elements[2];
			M11 = elements[3];
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> struct.
	///       </summary>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> has less than 2x2 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	[CLSCompliant(false)]
	public Matrix22F(float[,] elements)
	{
		M00 = elements[0, 0];
		M01 = elements[0, 1];
		M10 = elements[1, 0];
		M11 = elements[1, 1];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> struct.
	///       </summary>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> has less than 2x2 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> or the arrays in elements[0] must not be <see langword="null" />.
	///       </exception>
	public Matrix22F(float[][] elements)
	{
		M00 = elements[0][0];
		M01 = elements[0][1];
		M10 = elements[1][0];
		M11 = elements[1][1];
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
		hashCode = (hashCode * 397) ^ M10.GetHashCode();
		return (hashCode * 397) ^ M11.GetHashCode();
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
		if (obj is Matrix22F)
		{
			return this == (Matrix22F)obj;
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
	public bool Equals(Matrix22F other)
	{
		return this == other;
	}

	/// <summary>
	///       Returns the string representation of this matrix.
	///       </summary>
	/// <returns>The string representation of this matrix.</returns>
	public override string ToString()
	{
		return ToString(CultureInfo.CurrentCulture);
	}

	/// <summary>
	///       Returns the string representation of this matrix using the specified 
	///       culture-specific format information.
	///       </summary>
	/// <param name="provider">
	///       An <see cref="T:System.IFormatProvider" /> that supplies culture-specific 
	///       formatting information
	///       </param>
	/// <returns>The string representation of this matrix.</returns>
	public string ToString(IFormatProvider provider)
	{
		return string.Format(provider, c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7532), M00, M01, M10, M11);
	}

	/// <summary>
	///       Negates a matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The negated matrix.</returns>
	public static Matrix22F operator -(Matrix22F matrix)
	{
		matrix.M00 = 0f - matrix.M00;
		matrix.M01 = 0f - matrix.M01;
		matrix.M10 = 0f - matrix.M10;
		matrix.M11 = 0f - matrix.M11;
		return matrix;
	}

	/// <summary>
	///       Negates a matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The negated matrix.</returns>
	public static Matrix22F Negate(Matrix22F matrix)
	{
		matrix.M00 = 0f - matrix.M00;
		matrix.M01 = 0f - matrix.M01;
		matrix.M10 = 0f - matrix.M10;
		matrix.M11 = 0f - matrix.M11;
		return matrix;
	}

	/// <summary>
	///       Adds two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second Matrix.</param>
	/// <returns>The sum of the two matrices.</returns>
	public static Matrix22F operator +(Matrix22F matrix1, Matrix22F matrix2)
	{
		matrix1.M00 += matrix2.M00;
		matrix1.M01 += matrix2.M01;
		matrix1.M10 += matrix2.M10;
		matrix1.M11 += matrix2.M11;
		return matrix1;
	}

	/// <summary>
	///       Adds two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second Matrix.</param>
	/// <returns>The sum of the two matrices.</returns>
	public static Matrix22F Add(Matrix22F matrix1, Matrix22F matrix2)
	{
		matrix1.M00 += matrix2.M00;
		matrix1.M01 += matrix2.M01;
		matrix1.M10 += matrix2.M10;
		matrix1.M11 += matrix2.M11;
		return matrix1;
	}

	/// <summary>
	///       Subtracts two matrices.
	///       </summary>
	/// <param name="minuend">The first matrix (minuend).</param>
	/// <param name="subtrahend">The second matrix (subtrahend).</param>
	/// <returns>The difference of the two matrices.</returns>
	public static Matrix22F operator -(Matrix22F minuend, Matrix22F subtrahend)
	{
		minuend.M00 -= subtrahend.M00;
		minuend.M01 -= subtrahend.M01;
		minuend.M10 -= subtrahend.M10;
		minuend.M11 -= subtrahend.M11;
		return minuend;
	}

	/// <summary>
	///       Subtracts two matrices.
	///       </summary>
	/// <param name="minuend">The first matrix (minuend).</param>
	/// <param name="subtrahend">The second matrix (subtrahend).</param>
	/// <returns>The difference of the two matrices.</returns>
	public static Matrix22F Subtract(Matrix22F minuend, Matrix22F subtrahend)
	{
		minuend.M00 -= subtrahend.M00;
		minuend.M01 -= subtrahend.M01;
		minuend.M10 -= subtrahend.M10;
		minuend.M11 -= subtrahend.M11;
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
	public static Matrix22F operator *(Matrix22F matrix, float scalar)
	{
		matrix.M00 *= scalar;
		matrix.M01 *= scalar;
		matrix.M10 *= scalar;
		matrix.M11 *= scalar;
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
	public static Matrix22F operator *(float scalar, Matrix22F matrix)
	{
		matrix.M00 *= scalar;
		matrix.M01 *= scalar;
		matrix.M10 *= scalar;
		matrix.M11 *= scalar;
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
	public static Matrix22F Multiply(float scalar, Matrix22F matrix)
	{
		matrix.M00 *= scalar;
		matrix.M01 *= scalar;
		matrix.M10 *= scalar;
		matrix.M11 *= scalar;
		return matrix;
	}

	/// <summary>
	///       Multiplies two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>The matrix with the product the two matrices.</returns>
	public static Matrix22F operator *(Matrix22F matrix1, Matrix22F matrix2)
	{
		Matrix22F result = default;
		result.M00 = matrix1.M00 * matrix2.M00 + matrix1.M01 * matrix2.M10;
		result.M01 = matrix1.M00 * matrix2.M01 + matrix1.M01 * matrix2.M11;
		result.M10 = matrix1.M10 * matrix2.M00 + matrix1.M11 * matrix2.M10;
		result.M11 = matrix1.M10 * matrix2.M01 + matrix1.M11 * matrix2.M11;
		return result;
	}

	/// <summary>
	///       Multiplies two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>The matrix with the product the two matrices.</returns>
	public static Matrix22F Multiply(Matrix22F matrix1, Matrix22F matrix2)
	{
		Matrix22F result = default;
		result.M00 = matrix1.M00 * matrix2.M00 + matrix1.M01 * matrix2.M10;
		result.M01 = matrix1.M00 * matrix2.M01 + matrix1.M01 * matrix2.M11;
		result.M10 = matrix1.M10 * matrix2.M00 + matrix1.M11 * matrix2.M10;
		result.M11 = matrix1.M10 * matrix2.M01 + matrix1.M11 * matrix2.M11;
		return result;
	}

	/// <summary>
	///       Multiplies a matrix with a column vector.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="vector">The column vector.</param>
	/// <returns>The resulting column vector.</returns>
	public static Vector2F operator *(Matrix22F matrix, Vector2F vector)
	{
		Vector2F result = default;
		result.X = matrix.M00 * vector.X + matrix.M01 * vector.Y;
		result.Y = matrix.M10 * vector.X + matrix.M11 * vector.Y;
		return result;
	}

	/// <summary>
	///       Multiplies a matrix with a column vector.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="vector">The column vector.</param>
	/// <returns>The resulting column vector.</returns>
	public static Vector2F Multiply(Matrix22F matrix, Vector2F vector)
	{
		Vector2F result = default;
		result.X = matrix.M00 * vector.X + matrix.M01 * vector.Y;
		result.Y = matrix.M10 * vector.X + matrix.M11 * vector.Y;
		return result;
	}

	/// <summary>
	///       Divides a matrix by a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The matrix with each element divided by scalar.</returns>
	public static Matrix22F operator /(Matrix22F matrix, float scalar)
	{
		float num = 1f / scalar;
		matrix.M00 *= num;
		matrix.M01 *= num;
		matrix.M10 *= num;
		matrix.M11 *= num;
		return matrix;
	}

	/// <summary>
	///       Divides a matrix by a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The matrix with each element divided by scalar.</returns>
	public static Matrix22F Divide(Matrix22F matrix, float scalar)
	{
		float num = 1f / scalar;
		matrix.M00 *= num;
		matrix.M01 *= num;
		matrix.M10 *= num;
		matrix.M11 *= num;
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
	public static bool operator ==(Matrix22F matrix1, Matrix22F matrix2)
	{
		if (matrix1.M00 == matrix2.M00 && matrix1.M01 == matrix2.M01 && matrix1.M10 == matrix2.M10)
		{
			return matrix1.M11 == matrix2.M11;
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
	public static bool operator !=(Matrix22F matrix1, Matrix22F matrix2)
	{
		if (matrix1.M00 == matrix2.M00 && matrix1.M01 == matrix2.M01 && matrix1.M10 == matrix2.M10)
		{
			return matrix1.M11 != matrix2.M11;
		}
		return true;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> 
	///       to a 2-dimensional float array.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator float[,](Matrix22F matrix)
	{
		return new float[2, 2]
		{
			{ matrix.M00, matrix.M01 },
			{ matrix.M10, matrix.M11 }
		};
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> to a 2-dimensional float array.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public float[,] ToArray2D()
	{
		return (float[,])this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> 
	///       to a jagged float array.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator float[][](Matrix22F matrix)
	{
		float[][] array = new float[2][]
		{
			new float[2],
			new float[2]
		};
		array[0][0] = matrix.M00;
		array[0][1] = matrix.M01;
		array[1][0] = matrix.M10;
		array[1][1] = matrix.M11;
		return array;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> to a jagged float array.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public float[][] ToArrayJagged()
	{
		return (float[][])this;
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixF" />.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator MatrixF(Matrix22F matrix)
	{
		MatrixF matrixF = new MatrixF(2, 2);
		matrixF[0, 0] = matrix.M00;
		matrixF[0, 1] = matrix.M01;
		matrixF[1, 0] = matrix.M10;
		matrixF[1, 1] = matrix.M11;
		return matrixF;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixF" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public MatrixF ToMatrixF()
	{
		return this;
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22D" />.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator Matrix22D(Matrix22F matrix)
	{
		return new Matrix22D(matrix.M00, matrix.M01, matrix.M10, matrix.M11);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22D" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public Matrix22D ToMatrix22D()
	{
		return this;
	}

	/// <summary>
	///       Sets each matrix element to its absolute value.
	///       </summary>
	public void Absolute()
	{
		M00 = Math.Abs(M00);
		M01 = Math.Abs(M01);
		M10 = Math.Abs(M10);
		M11 = Math.Abs(M11);
	}

	/// <summary>
	///       Clamps near-zero matrix elements to zero.
	///       </summary>
	public void ClampToZero()
	{
		M00 = Numeric.ClampToZero(M00);
		M01 = Numeric.ClampToZero(M01);
		M10 = Numeric.ClampToZero(M10);
		M11 = Numeric.ClampToZero(M11);
	}

	/// <summary>
	///       Clamps near-zero matrix elements to zero.
	///       </summary>
	/// <param name="epsilon">The tolerance value.</param>
	public void ClampToZero(float epsilon)
	{
		M00 = Numeric.ClampToZero(M00, epsilon);
		M01 = Numeric.ClampToZero(M01, epsilon);
		M10 = Numeric.ClampToZero(M10, epsilon);
		M11 = Numeric.ClampToZero(M11, epsilon);
	}

	/// <summary>
	///       Gets a column as <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <param name="index">The index of the column.</param>
	/// <returns>The column vector</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public Vector2F GetColumn(int index)
	{
		Vector2F result = default;
		switch (index)
		{
		case 0:
			result.X = M00;
			result.Y = M10;
			break;
		case 1:
			result.X = M01;
			result.Y = M11;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7292));
		}
		return result;
	}

	/// <summary>
	///       Sets a column from a <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <param name="index">The index of the column.</param>
	/// <param name="columnVector">The column vector.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public void SetColumn(int index, Vector2F columnVector)
	{
		switch (index)
		{
		case 0:
			M00 = columnVector.X;
			M10 = columnVector.Y;
			break;
		case 1:
			M01 = columnVector.X;
			M11 = columnVector.Y;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7292));
		}
	}

	/// <summary>
	///       Gets a row as <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <param name="index">The index of the row.</param>
	/// <returns>The row vector</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public Vector2F GetRow(int index)
	{
		Vector2F result = default;
		switch (index)
		{
		case 0:
			result.X = M00;
			result.Y = M01;
			break;
		case 1:
			result.X = M10;
			result.Y = M11;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7415));
		}
		return result;
	}

	/// <summary>
	///       Sets a row from a <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <param name="index">The index of the row (0, 1, or 2).</param>
	/// <param name="rowVector">The row vector.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public void SetRow(int index, Vector2F rowVector)
	{
		switch (index)
		{
		case 0:
			M00 = rowVector.X;
			M01 = rowVector.Y;
			break;
		case 1:
			M10 = rowVector.X;
			M11 = rowVector.Y;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7415));
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
		float[] array = new float[4];
		if (order == MatrixOrder.ColumnMajor)
		{
			array[0] = M00;
			array[1] = M10;
			array[2] = M01;
			array[3] = M11;
		}
		else
		{
			array[0] = M00;
			array[1] = M01;
			array[2] = M10;
			array[3] = M11;
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
		List<float> list = new List<float>(4);
		if (order == MatrixOrder.ColumnMajor)
		{
			list.Add(M00);
			list.Add(M10);
			list.Add(M01);
			list.Add(M11);
		}
		else
		{
			list.Add(M00);
			list.Add(M01);
			list.Add(M10);
			list.Add(M11);
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
		float num = M00 * M11 - M01 * M10;
		if (Numeric.IsZero(num, Numeric.EpsilonFSquared))
		{
			return false;
		}
		float num2 = 1f / num;
		float m = M00;
		M00 = M11 * num2;
		M01 = (0f - M01) * num2;
		M10 = (0f - M10) * num2;
		M11 = m * num2;
		return true;
	}

	/// <summary>
	///       Transposes this matrix.
	///       </summary>
	public void Transpose()
	{
		MathHelper.Swap(ref M01, ref M10);
	}

	/// <summary>
	///       Returns a matrix with the absolute values of the elements of the given matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>A matrix with the absolute values of the elements of the given matrix.</returns>
	public static Matrix22F Absolute(Matrix22F matrix)
	{
		return new Matrix22F(Math.Abs(matrix.M00), Math.Abs(matrix.M01), Math.Abs(matrix.M10), Math.Abs(matrix.M11));
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
	public static bool AreNumericallyEqual(Matrix22F matrix1, Matrix22F matrix2)
	{
		if (Numeric.AreEqual(matrix1.M00, matrix2.M00) && Numeric.AreEqual(matrix1.M01, matrix2.M01) && Numeric.AreEqual(matrix1.M10, matrix2.M10))
		{
			return Numeric.AreEqual(matrix1.M11, matrix2.M11);
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
	public static bool AreNumericallyEqual(Matrix22F matrix1, Matrix22F matrix2, float epsilon)
	{
		if (Numeric.AreEqual(matrix1.M00, matrix2.M00, epsilon) && Numeric.AreEqual(matrix1.M01, matrix2.M01, epsilon) && Numeric.AreEqual(matrix1.M10, matrix2.M10, epsilon))
		{
			return Numeric.AreEqual(matrix1.M11, matrix2.M11, epsilon);
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
	public static Matrix22F ClampToZero(Matrix22F matrix)
	{
		matrix.M00 = Numeric.ClampToZero(matrix.M00);
		matrix.M01 = Numeric.ClampToZero(matrix.M01);
		matrix.M10 = Numeric.ClampToZero(matrix.M10);
		matrix.M11 = Numeric.ClampToZero(matrix.M11);
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
	public static Matrix22F ClampToZero(Matrix22F matrix, float epsilon)
	{
		matrix.M00 = Numeric.ClampToZero(matrix.M00, epsilon);
		matrix.M01 = Numeric.ClampToZero(matrix.M01, epsilon);
		matrix.M10 = Numeric.ClampToZero(matrix.M10, epsilon);
		matrix.M11 = Numeric.ClampToZero(matrix.M11, epsilon);
		return matrix;
	}

	/// <summary>
	///       Creates a uniform scaling matrix.
	///       </summary>
	/// <param name="scale">
	///       The uniform scale factor that is applied to the x- and y-axis.
	///       </param>
	/// <returns>The created scaling matrix.</returns>
	public static Matrix22F CreateScale(float scale)
	{
		return new Matrix22F
		{
			M00 = scale,
			M11 = scale
		};
	}

	/// <summary>
	///       Creates a scaling matrix.
	///       </summary>
	/// <param name="scaleX">The value to scale by on the x-axis.</param>
	/// <param name="scaleY">The value to scale by on the y-axis.</param>
	/// <returns>The created scaling matrix.</returns>
	public static Matrix22F CreateScale(float scaleX, float scaleY)
	{
		return new Matrix22F
		{
			M00 = scaleX,
			M11 = scaleY
		};
	}

	/// <summary>
	///       Creates a scaling matrix.
	///       </summary>
	/// <param name="scale">Amounts to scale by the x, and y-axis.</param>
	/// <returns>The created scaling matrix.</returns>
	public static Matrix22F CreateScale(Vector2F scale)
	{
		return new Matrix22F
		{
			M00 = scale.X,
			M11 = scale.Y
		};
	}

	/// <summary>
	///       Creates a rotation matrix.
	///       </summary>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>The created rotation matrix.</returns>
	public static Matrix22F CreateRotation(float angle)
	{
		float num = (float)Math.Cos(angle);
		float num2 = (float)Math.Sin(angle);
		Matrix22F result = default;
		result.M00 = num;
		result.M01 = 0f - num2;
		result.M10 = num2;
		result.M11 = num;
		return result;
	}
}
