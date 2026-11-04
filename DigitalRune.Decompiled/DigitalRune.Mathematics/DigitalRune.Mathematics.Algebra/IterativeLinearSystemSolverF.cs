using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       An iterative method for solving a linear system of equations <i>A * x = b</i>
///       (single-precision).
///       </summary>
public abstract class IterativeLinearSystemSolverF
{
	private int ce55e022e0018785bcb4289746320a408 = 1000;

	private float c8b99649fe8ac20b87a444ad033b2d940 = Numeric.EpsilonF;

	[CompilerGenerated]
	private int c067481a3e0ca7109815a79d198ffdfe2;

	/// <summary>
	///       Gets or sets the number of iterations of the last <see cref="M:DigitalRune.Mathematics.Algebra.IterativeLinearSystemSolverF.Solve(DigitalRune.Mathematics.Algebra.MatrixF,DigitalRune.Mathematics.Algebra.VectorF)" /> 
	///       method call.
	///       </summary>
	public int NumberOfIterations
	{
		[CompilerGenerated]
		get
		{
			return c067481a3e0ca7109815a79d198ffdfe2;
		}
		[CompilerGenerated]
		protected set
		{
			c067481a3e0ca7109815a79d198ffdfe2 = value;
		}
	}

	/// <summary>
	///       Gets or sets the maximum number number of iterations.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public int MaxNumberOfIterations
	{
		get
		{
			return ce55e022e0018785bcb4289746320a408;
		}
		set
		{
			if (value < 0)
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
							__ldtoken(IterativeLinearSystemSolverF.set_MaxNumberOfIterations(int));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(159), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(170));
					}
				}
			}
			ce55e022e0018785bcb4289746320a408 = value;
		}
	}

	/// <summary>
	///       Gets or sets the tolerance value. 
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public float Epsilon
	{
		get
		{
			return c8b99649fe8ac20b87a444ad033b2d940;
		}
		set
		{
			if (value <= 0f)
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
							__ldtoken(IterativeLinearSystemSolverF.set_Epsilon(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(159), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(488));
					}
				}
			}
			c8b99649fe8ac20b87a444ad033b2d940 = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.IterativeLinearSystemSolverF" /> class</summary>
	protected IterativeLinearSystemSolverF()
	{
	}

	/// <summary>
	///       Solves the specified linear system of equations <i>A * x = b</i>.
	///       </summary>
	/// <param name="matrixA">The matrix A.</param>
	/// <param name="vectorB">The vector b.</param>
	/// <returns>The solution vector x.</returns>
	public VectorF Solve(MatrixF matrixA, VectorF vectorB)
	{
		return Solve(matrixA, null, vectorB);
	}

	/// <summary>
	///       Solves the specified linear system of equations <i>A * x = b</i> using an initial guess.
	///       </summary>
	/// <param name="matrixA">The matrix A.</param>
	/// <param name="initialX">
	///       The initial guess for x. If this value is <see langword="null" />, a zero vector will be used
	///       as initial guess.
	///       </param>
	/// <param name="vectorB">The vector b.</param>
	/// <returns>The solution vector x.</returns>
	public abstract VectorF Solve(MatrixF matrixA, VectorF initialX, VectorF vectorB);
}
