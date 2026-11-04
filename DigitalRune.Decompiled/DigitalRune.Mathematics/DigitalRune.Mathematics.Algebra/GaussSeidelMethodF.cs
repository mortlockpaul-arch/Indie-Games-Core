using System;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       An iterative solver using the Gauss-Seidel method (single-precision).
///       </summary>
public class GaussSeidelMethodF : IterativeLinearSystemSolverF
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.GaussSeidelMethodF" /> class</summary>
	public GaussSeidelMethodF()
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
	public override VectorF Solve(MatrixF matrixA, VectorF initialX, VectorF vectorB)
	{
		NumberOfIterations = 0;
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
						__ldtoken(GaussSeidelMethodF.Solve(MatrixF, VectorF, VectorF));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
				}
			}
		}
		if (vectorB == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2456));
				}
			}
		}
		if (!matrixA.IsSquare)
		{
			while (true)
			{
				switch (4)
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
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2538));
		}
		if (initialX != null && initialX.NumberOfElements != vectorB.NumberOfElements)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2625));
				}
			}
		}
		VectorF vectorF = initialX;
		if ((object)vectorF == null)
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
			vectorF = new VectorF(vectorB.NumberOfElements);
		}
		VectorF vectorF2 = vectorF;
		VectorF vectorF3 = new VectorF(vectorB.NumberOfElements);
		bool flag = false;
		for (int i = 0; i < MaxNumberOfIterations; i++)
		{
			if (flag)
			{
				break;
			}
			int num = 0;
			while (num < vectorB.NumberOfElements)
			{
				float num2 = 0f;
				for (int j = 0; j < num; j++)
				{
					num2 += matrixA[num, j] * vectorF3[j];
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					for (int k = num + 1; k < vectorB.NumberOfElements; k++)
					{
						num2 += matrixA[num, k] * vectorF2[k];
					}
					while (true)
					{
						switch (4)
						{
						case 0:
							break;
						default:
							goto end_IL_018b;
						}
						continue;
						end_IL_018b:
						break;
					}
					vectorF3[num] = (vectorB[num] - num2) / matrixA[num, num];
					num++;
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
					goto end_IL_01ca;
				}
				continue;
				end_IL_01ca:
				break;
			}
			flag = VectorF.AreNumericallyEqual(vectorF2, vectorF3, Epsilon);
			vectorF2 = vectorF3.Clone();
			NumberOfIterations = i + 1;
		}
		return vectorF3;
	}
}
