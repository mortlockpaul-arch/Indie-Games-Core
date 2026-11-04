using System;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       An iterative solver using the Gauss-Seidel method (double-precision).
///       </summary>
public class GaussSeidelMethodD : IterativeLinearSystemSolverD
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.GaussSeidelMethodD" /> class</summary>
	public GaussSeidelMethodD()
	{
	}

	/// <summary>
	///       Solves the specified linear system of equations <i>Ax=b</i>.
	///       </summary>
	/// <param name="matrixA">The matrix A.</param>
	/// <param name="initialX">
	///       The initial guess for x. If this value is <see langword="null" />, a zero vector will be used
	///       as initial guess.
	///       </param>
	/// <param name="vectorB">The vector b.</param>
	/// <returns>The solution vector x.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrixA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vectorB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="matrixA" />is not a square matrix.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The number of elements of <paramref name="initialX" /> does not match.
	///       </exception>
	public override VectorD Solve(MatrixD matrixA, VectorD initialX, VectorD vectorB)
	{
		NumberOfIterations = 0;
		if (matrixA == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(GaussSeidelMethodD.Solve(MatrixD, VectorD, VectorD));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
				}
			}
		}
		if (vectorB == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2456));
		}
		if (!matrixA.IsSquare)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2471), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
				}
			}
		}
		if (matrixA.NumberOfRows != vectorB.NumberOfElements)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2538));
				}
			}
		}
		if (initialX != null && initialX.NumberOfElements != vectorB.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2625));
		}
		VectorD vectorD = initialX;
		if ((object)vectorD == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			vectorD = new VectorD(vectorB.NumberOfElements);
		}
		VectorD vectorD2 = vectorD;
		VectorD vectorD3 = new VectorD(vectorB.NumberOfElements);
		bool flag = false;
		for (int i = 0; i < MaxNumberOfIterations; i++)
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
			if (!flag)
			{
				for (int j = 0; j < vectorB.NumberOfElements; j++)
				{
					double num = 0.0;
					for (int k = 0; k < j; k++)
					{
						num += matrixA[j, k] * vectorD3[k];
					}
					for (int l = j + 1; l < vectorB.NumberOfElements; l++)
					{
						num += matrixA[j, l] * vectorD2[l];
					}
					while (true)
					{
						switch (4)
						{
						case 0:
							break;
						default:
							goto end_IL_0173;
						}
						continue;
						end_IL_0173:
						break;
					}
					vectorD3[j] = (vectorB[j] - num) / matrixA[j, j];
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						goto end_IL_01b4;
					}
					continue;
					end_IL_01b4:
					break;
				}
				flag = VectorD.AreNumericallyEqual(vectorD2, vectorD3, Epsilon);
				vectorD2 = vectorD3.Clone();
				NumberOfIterations = i + 1;
				continue;
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
			break;
		}
		return vectorD3;
	}
}
