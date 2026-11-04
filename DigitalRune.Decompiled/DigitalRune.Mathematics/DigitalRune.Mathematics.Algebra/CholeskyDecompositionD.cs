using System;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Computes the Cholesky Decomposition of a matrix (double-precision).
///       </summary>
public class CholeskyDecompositionD
{
	private readonly MatrixD c118c74aa6d29eb06da2f4baff7ca2389;

	private readonly bool c4377e83fc9977f00eee9869ce7af4d92;

	/// <summary>
	///       Gets a value indicating whether the original matrix is symmetric and positive definite.
	///       </summary>
	public bool IsSymmetricPositiveDefinite => c4377e83fc9977f00eee9869ce7af4d92;

	/// <summary>
	///       Gets the lower triangular matrix L. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixD L => c118c74aa6d29eb06da2f4baff7ca2389;

	/// <summary>
	///       Creates the Cholesky decomposition of the given matrix.
	///       </summary>
	/// <param name="matrixA">The square matrix A.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrixA" /> is <see langword="null" />.
	///       </exception>
	public CholeskyDecompositionD(MatrixD matrixA)
	{
		if (matrixA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
		}
		int num = matrixA.NumberOfRows;
		c4377e83fc9977f00eee9869ce7af4d92 = true;
		if (matrixA.NumberOfColumns != num)
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
				__ldtoken(CholeskyDecompositionD._002Ector(MatrixD));
			}
			num = Math.Min(num, matrixA.NumberOfColumns);
			c4377e83fc9977f00eee9869ce7af4d92 = false;
		}
		c118c74aa6d29eb06da2f4baff7ca2389 = new MatrixD(num, num);
		int num2 = 0;
		while (num2 < num)
		{
			double num3 = 0.0;
			int num4 = 0;
			while (num4 < num2)
			{
				double num5 = 0.0;
				for (int i = 0; i < num4; i++)
				{
					num5 += c118c74aa6d29eb06da2f4baff7ca2389[num4, i] * c118c74aa6d29eb06da2f4baff7ca2389[num2, i];
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					num5 = (matrixA[num2, num4] - num5) / c118c74aa6d29eb06da2f4baff7ca2389[num4, num4];
					L[num2, num4] = num5;
					num3 += num5 * num5;
					int num6;
					if (c4377e83fc9977f00eee9869ce7af4d92)
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
						num6 = ((matrixA[num4, num2] == matrixA[num2, num4]) ? 1 : 0);
					}
					else
					{
						num6 = 0;
					}
					c4377e83fc9977f00eee9869ce7af4d92 = (byte)num6 != 0;
					num4++;
					break;
				}
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				num3 = matrixA[num2, num2] - num3;
				c4377e83fc9977f00eee9869ce7af4d92 = c4377e83fc9977f00eee9869ce7af4d92 && num3 > 0.0;
				c118c74aa6d29eb06da2f4baff7ca2389[num2, num2] = Math.Sqrt(Math.Max(num3, 0.0));
				num2++;
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
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The matrix A is not symmetric and positive definite.
	///       </exception>
	public MatrixD SolveLinearEquations(MatrixD matrixB)
	{
		if (matrixB == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2114));
		}
		if (matrixB.NumberOfRows != L.NumberOfRows)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2129), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2114));
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
					if (1 == 0)
					{
						__ldtoken(CholeskyDecompositionD.SolveLinearEquations(MatrixD));
					}
					throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7577));
				}
			}
		}
		MatrixD matrixD = matrixB.Clone();
		for (int i = 0; i < L.NumberOfRows; i++)
		{
			for (int j = 0; j < matrixB.NumberOfColumns; j++)
			{
				for (int k = 0; k < i; k++)
				{
					matrixD[i, j] -= matrixD[k, j] * L[i, k];
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						goto end_IL_00d1;
					}
					continue;
					end_IL_00d1:
					break;
				}
				matrixD[i, j] /= L[i, i];
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					goto end_IL_011b;
				}
				continue;
				end_IL_011b:
				break;
			}
		}
		while (true)
		{
			switch (1)
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
						matrixD[num, l] -= matrixD[m, l] * L[m, num];
					}
					matrixD[num, l] /= L[num, num];
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						goto end_IL_0205;
					}
					continue;
					end_IL_0205:
					break;
				}
			}
			return matrixD;
		}
	}
}
