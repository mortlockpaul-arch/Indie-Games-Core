using System;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Computes the LU Decomposition of a matrix (single-precision).
///       </summary>
public class LUDecompositionF
{
	private readonly MatrixF ce3d227b832e5aa9c210dacd899947dc8;

	private MatrixF c89cedc2eec116029c22b4afe8aec50f0;

	private MatrixF c46bb73fe903f09ca6248448445516738;

	private readonly int c6ae83731ca5ed1d50b72b5b5953792b6;

	private readonly int c8704416eb2ee8a512db38b95b947dcc8;

	private readonly int c05efacd6ada73cfc790306827f0b2c62;

	private readonly int[] c06d19e10bc7150f145203237a793d723;

	private float? cf404479145d699daee3b1893547f0b99;

	/// <summary>
	///       Gets the determinant of matrix A.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">Matrix A is not square.</exception>
	public float Determinant
	{
		get
		{
			if (c6ae83731ca5ed1d50b72b5b5953792b6 != c8704416eb2ee8a512db38b95b947dcc8)
			{
				throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2067));
			}
			if (!cf404479145d699daee3b1893547f0b99.HasValue)
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
					__ldtoken(LUDecompositionF.get_Determinant());
				}
				float num = c05efacd6ada73cfc790306827f0b2c62;
				for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
				{
					num *= ce3d227b832e5aa9c210dacd899947dc8[i, i];
				}
				cf404479145d699daee3b1893547f0b99 = num;
			}
			return cf404479145d699daee3b1893547f0b99.Value;
		}
	}

	/// <summary>
	///       Gets a value indicating whether the matrix U is numerically singular.
	///       </summary>
	public bool IsNumericallySingular
	{
		get
		{
			int num = Math.Min(c8704416eb2ee8a512db38b95b947dcc8, c6ae83731ca5ed1d50b72b5b5953792b6);
			for (int i = 0; i < num; i++)
			{
				if (Numeric.IsZero(ce3d227b832e5aa9c210dacd899947dc8[i, i]))
				{
					return true;
				}
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(LUDecompositionF.get_IsNumericallySingular());
				}
				return false;
			}
		}
	}

	/// <summary>
	///       Gets the lower triangular matrix L. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixF L
	{
		get
		{
			if (c89cedc2eec116029c22b4afe8aec50f0 != null)
			{
				return c89cedc2eec116029c22b4afe8aec50f0;
			}
			c89cedc2eec116029c22b4afe8aec50f0 = new MatrixF(c6ae83731ca5ed1d50b72b5b5953792b6, c8704416eb2ee8a512db38b95b947dcc8);
			int num = 0;
			while (num < c6ae83731ca5ed1d50b72b5b5953792b6)
			{
				for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
				{
					if (num > i)
					{
						c89cedc2eec116029c22b4afe8aec50f0[num, i] = ce3d227b832e5aa9c210dacd899947dc8[num, i];
					}
					else if (num == i)
					{
						c89cedc2eec116029c22b4afe8aec50f0[num, i] = 1f;
					}
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
						__ldtoken(LUDecompositionF.get_L());
					}
					num++;
					break;
				}
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				return c89cedc2eec116029c22b4afe8aec50f0;
			}
		}
	}

	/// <summary>
	///       Gets the upper triangular matrix U. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixF U
	{
		get
		{
			if (c46bb73fe903f09ca6248448445516738 != null)
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
							__ldtoken(LUDecompositionF.get_U());
						}
						return c46bb73fe903f09ca6248448445516738;
					}
				}
			}
			c46bb73fe903f09ca6248448445516738 = new MatrixF(c8704416eb2ee8a512db38b95b947dcc8, c8704416eb2ee8a512db38b95b947dcc8);
			for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
			{
				for (int j = 0; j < c8704416eb2ee8a512db38b95b947dcc8; j++)
				{
					if (i <= j)
					{
						c46bb73fe903f09ca6248448445516738[i, j] = ce3d227b832e5aa9c210dacd899947dc8[i, j];
					}
					else
					{
						c46bb73fe903f09ca6248448445516738[i, j] = 0f;
					}
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						goto end_IL_008a;
					}
					continue;
					end_IL_008a:
					break;
				}
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				return c46bb73fe903f09ca6248448445516738;
			}
		}
	}

	/// <summary>
	///       Gets the pivot permutation vector. (This property returns the internal array, 
	///       not a copy.)
	///       </summary>
	public int[] PivotPermutationVector => c06d19e10bc7150f145203237a793d723;

	/// <summary>
	///       Creates the LU decomposition of the given matrix.
	///       </summary>
	/// <param name="matrixA">
	///       The matrix A. (Can be rectangular. Number of rows ≥ number of columns.)
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrixA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The number of rows must be greater than or equal to the number of columns.
	///       </exception>
	public LUDecompositionF(MatrixF matrixA)
	{
		if (matrixA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
		}
		if (matrixA.NumberOfColumns > matrixA.NumberOfRows)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2306), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
		}
		ce3d227b832e5aa9c210dacd899947dc8 = matrixA.Clone();
		c6ae83731ca5ed1d50b72b5b5953792b6 = matrixA.NumberOfRows;
		c8704416eb2ee8a512db38b95b947dcc8 = matrixA.NumberOfColumns;
		c06d19e10bc7150f145203237a793d723 = new int[c6ae83731ca5ed1d50b72b5b5953792b6];
		for (int i = 0; i < c6ae83731ca5ed1d50b72b5b5953792b6; i++)
		{
			c06d19e10bc7150f145203237a793d723[i] = i;
		}
		c05efacd6ada73cfc790306827f0b2c62 = 1;
		int num = 0;
		while (num < c8704416eb2ee8a512db38b95b947dcc8)
		{
			float[] array = new float[c6ae83731ca5ed1d50b72b5b5953792b6];
			for (int j = 0; j < c6ae83731ca5ed1d50b72b5b5953792b6; j++)
			{
				array[j] = ce3d227b832e5aa9c210dacd899947dc8[j, num];
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(LUDecompositionF._002Ector(MatrixF));
				}
				for (int k = 0; k < c6ae83731ca5ed1d50b72b5b5953792b6; k++)
				{
					int num2 = Math.Min(k, num);
					float num3 = 0f;
					for (int l = 0; l < num2; l++)
					{
						num3 += ce3d227b832e5aa9c210dacd899947dc8[k, l] * array[l];
					}
					array[k] -= num3;
					ce3d227b832e5aa9c210dacd899947dc8[k, num] = array[k];
				}
				int num4 = num;
				for (int m = num + 1; m < c6ae83731ca5ed1d50b72b5b5953792b6; m++)
				{
					if (!(Math.Abs(array[m]) > Math.Abs(array[num4])))
					{
						continue;
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
					num4 = m;
				}
				if (num4 != num)
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
					for (int n = 0; n < c8704416eb2ee8a512db38b95b947dcc8; n++)
					{
						float value = ce3d227b832e5aa9c210dacd899947dc8[num4, n];
						ce3d227b832e5aa9c210dacd899947dc8[num4, n] = ce3d227b832e5aa9c210dacd899947dc8[num, n];
						ce3d227b832e5aa9c210dacd899947dc8[num, n] = value;
					}
					MathHelper.Swap(ref c06d19e10bc7150f145203237a793d723[num4], ref c06d19e10bc7150f145203237a793d723[num]);
					c05efacd6ada73cfc790306827f0b2c62 = -c05efacd6ada73cfc790306827f0b2c62;
				}
				if (num < c6ae83731ca5ed1d50b72b5b5953792b6 && ce3d227b832e5aa9c210dacd899947dc8[num, num] != 0f)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
					for (int num5 = num + 1; num5 < c6ae83731ca5ed1d50b72b5b5953792b6; num5++)
					{
						ce3d227b832e5aa9c210dacd899947dc8[num5, num] /= ce3d227b832e5aa9c210dacd899947dc8[num, num];
					}
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
				}
				num++;
				break;
			}
		}
	}

	/// <summary>
	///       Solves the equation <c>A * X = B</c>.
	///       </summary>
	/// <param name="matrixB">The matrix B with as many rows as A and any number of columns.</param>
	/// <returns>X, so that <c>A * X = B</c></returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrixB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">The number of rows does not match.</exception>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">The matrix A is numerically singular.</exception>
	public MatrixF SolveLinearEquations(MatrixF matrixB)
	{
		if (matrixB == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2114));
		}
		if (matrixB.NumberOfRows != c6ae83731ca5ed1d50b72b5b5953792b6)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2129), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2114));
		}
		if (IsNumericallySingular)
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
						__ldtoken(LUDecompositionF.SolveLinearEquations(MatrixF));
					}
					throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2198));
				}
			}
		}
		MatrixF submatrix = matrixB.GetSubmatrix(c06d19e10bc7150f145203237a793d723, 0, matrixB.NumberOfColumns - 1);
		for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
		{
			for (int j = i + 1; j < c8704416eb2ee8a512db38b95b947dcc8; j++)
			{
				for (int k = 0; k < matrixB.NumberOfColumns; k++)
				{
					submatrix[j, k] -= submatrix[i, k] * ce3d227b832e5aa9c210dacd899947dc8[j, i];
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						goto end_IL_00dc;
					}
					continue;
					end_IL_00dc:
					break;
				}
			}
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			for (int num = c8704416eb2ee8a512db38b95b947dcc8 - 1; num >= 0; num--)
			{
				for (int l = 0; l < matrixB.NumberOfColumns; l++)
				{
					submatrix[num, l] /= ce3d227b832e5aa9c210dacd899947dc8[num, num];
				}
				for (int m = 0; m < num; m++)
				{
					for (int n = 0; n < matrixB.NumberOfColumns; n++)
					{
						submatrix[m, n] -= submatrix[num, n] * ce3d227b832e5aa9c210dacd899947dc8[m, num];
					}
					while (true)
					{
						switch (7)
						{
						case 0:
							break;
						default:
							goto end_IL_01bb;
						}
						continue;
						end_IL_01bb:
						break;
					}
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						goto end_IL_01d1;
					}
					continue;
					end_IL_01d1:
					break;
				}
			}
			return submatrix;
		}
	}
}
