using System;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Computes the Cholesky Decomposition of a matrix (single-precision).
///       </summary>
public class CholeskyDecompositionF
{
	private readonly MatrixF c118c74aa6d29eb06da2f4baff7ca2389;

	private readonly bool c4377e83fc9977f00eee9869ce7af4d92;

	/// <summary>
	///       Gets a value indicating whether the original matrix is symmetric and positive definite.
	///       </summary>
	public bool IsSymmetricPositiveDefinite => c4377e83fc9977f00eee9869ce7af4d92;

	/// <summary>
	///       Gets the lower triangular matrix L. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixF L => c118c74aa6d29eb06da2f4baff7ca2389;

	/// <summary>
	///       Creates the Cholesky decomposition of the given matrix.
	///       </summary>
	/// <param name="matrixA">The square matrix A.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrixA" /> is <see langword="null" />.
	///       </exception>
	public CholeskyDecompositionF(MatrixF matrixA)
	{
		if (matrixA == null)
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
						__ldtoken(CholeskyDecompositionF._002Ector(MatrixF));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
				}
			}
		}
		int num = matrixA.NumberOfRows;
		c4377e83fc9977f00eee9869ce7af4d92 = true;
		if (matrixA.NumberOfColumns != num)
		{
			num = Math.Min(num, matrixA.NumberOfColumns);
			c4377e83fc9977f00eee9869ce7af4d92 = false;
		}
		c118c74aa6d29eb06da2f4baff7ca2389 = new MatrixF(num, num);
		for (int i = 0; i < num; i++)
		{
			float num2 = 0f;
			int num3 = 0;
			while (num3 < i)
			{
				float num4 = 0f;
				for (int j = 0; j < num3; j++)
				{
					num4 += c118c74aa6d29eb06da2f4baff7ca2389[num3, j] * c118c74aa6d29eb06da2f4baff7ca2389[i, j];
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					num4 = (matrixA[i, num3] - num4) / c118c74aa6d29eb06da2f4baff7ca2389[num3, num3];
					L[i, num3] = num4;
					num2 += num4 * num4;
					int num5;
					if (c4377e83fc9977f00eee9869ce7af4d92)
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
						num5 = ((matrixA[num3, i] == matrixA[i, num3]) ? 1 : 0);
					}
					else
					{
						num5 = 0;
					}
					c4377e83fc9977f00eee9869ce7af4d92 = (byte)num5 != 0;
					num3++;
					break;
				}
			}
			num2 = matrixA[i, i] - num2;
			int num6;
			if (c4377e83fc9977f00eee9869ce7af4d92)
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
				num6 = ((num2 > 0f) ? 1 : 0);
			}
			else
			{
				num6 = 0;
			}
			c4377e83fc9977f00eee9869ce7af4d92 = (byte)num6 != 0;
			c118c74aa6d29eb06da2f4baff7ca2389[i, i] = (float)Math.Sqrt(Math.Max(num2, 0f));
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				break;
			default:
				return;
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
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The matrix A is not symmetric and positive definite.
	///       </exception>
	public MatrixF SolveLinearEquations(MatrixF matrixB)
	{
		if (matrixB == null)
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
						__ldtoken(CholeskyDecompositionF.SolveLinearEquations(MatrixF));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2114));
				}
			}
		}
		if (matrixB.NumberOfRows != L.NumberOfRows)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2129), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2114));
				}
			}
		}
		if (!IsSymmetricPositiveDefinite)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7577));
				}
			}
		}
		MatrixF matrixF = matrixB.Clone();
		for (int i = 0; i < L.NumberOfRows; i++)
		{
			for (int j = 0; j < matrixB.NumberOfColumns; j++)
			{
				for (int k = 0; k < i; k++)
				{
					matrixF[i, j] -= matrixF[k, j] * L[i, k];
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						goto end_IL_00e9;
					}
					continue;
					end_IL_00e9:
					break;
				}
				matrixF[i, j] /= L[i, i];
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					goto end_IL_0131;
				}
				continue;
				end_IL_0131:
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
			for (int num = L.NumberOfRows - 1; num >= 0; num--)
			{
				for (int l = 0; l < matrixB.NumberOfColumns; l++)
				{
					for (int m = num + 1; m < L.NumberOfRows; m++)
					{
						matrixF[num, l] -= matrixF[m, l] * L[m, num];
					}
					matrixF[num, l] /= L[num, num];
				}
			}
			return matrixF;
		}
	}
}
