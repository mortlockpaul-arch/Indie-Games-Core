using System;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Computes the QR Decomposition of a matrix (double-precision).
///       </summary>
public class QRDecompositionD
{
	private readonly MatrixD c80fb3fde8a4d298a9d38a932edd2f039;

	private MatrixD c6d458b5addfcc5bf18baf0275eb3cd23;

	private MatrixD c1936aacd02c1df6653a5203e412f3235;

	private MatrixD cb16f9640bb813638c5a9009ebeff63a1;

	private readonly int c6ae83731ca5ed1d50b72b5b5953792b6;

	private readonly int c8704416eb2ee8a512db38b95b947dcc8;

	private readonly double[] c00fdda5a9d8efae627a1461abec098e6;

	/// <summary>
	///       Gets a value indicating whether the matrix R has full rank (numerically).
	///       </summary>
	public bool HasNumericallyFullRank
	{
		get
		{
			for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
			{
				if (!Numeric.IsZero(c00fdda5a9d8efae627a1461abec098e6[i]))
				{
					continue;
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(QRDecompositionD.get_HasNumericallyFullRank());
					}
					return false;
				}
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				return true;
			}
		}
	}

	/// <summary>
	///       Gets the matrix H with the Householder vectors. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixD H
	{
		get
		{
			if (cb16f9640bb813638c5a9009ebeff63a1 != null)
			{
				return cb16f9640bb813638c5a9009ebeff63a1;
			}
			cb16f9640bb813638c5a9009ebeff63a1 = new MatrixD(c6ae83731ca5ed1d50b72b5b5953792b6, c8704416eb2ee8a512db38b95b947dcc8);
			for (int i = 0; i < c6ae83731ca5ed1d50b72b5b5953792b6; i++)
			{
				for (int j = 0; j < c8704416eb2ee8a512db38b95b947dcc8; j++)
				{
					if (i >= j)
					{
						cb16f9640bb813638c5a9009ebeff63a1[i, j] = c80fb3fde8a4d298a9d38a932edd2f039[i, j];
					}
				}
			}
			return cb16f9640bb813638c5a9009ebeff63a1;
		}
	}

	/// <summary>
	///       Gets the orthogonal matrix Q. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixD Q
	{
		get
		{
			if (c6d458b5addfcc5bf18baf0275eb3cd23 != null)
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
							__ldtoken(QRDecompositionD.get_Q());
						}
						return c6d458b5addfcc5bf18baf0275eb3cd23;
					}
				}
			}
			c6d458b5addfcc5bf18baf0275eb3cd23 = new MatrixD(c6ae83731ca5ed1d50b72b5b5953792b6, c8704416eb2ee8a512db38b95b947dcc8);
			for (int num = c8704416eb2ee8a512db38b95b947dcc8 - 1; num >= 0; num--)
			{
				if (num < c6ae83731ca5ed1d50b72b5b5953792b6)
				{
					c6d458b5addfcc5bf18baf0275eb3cd23[num, num] = 1.0;
				}
				for (int i = num; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
				{
					if (c80fb3fde8a4d298a9d38a932edd2f039[num, num] == 0.0)
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
					double num2 = 0.0;
					for (int j = num; j < c6ae83731ca5ed1d50b72b5b5953792b6; j++)
					{
						num2 += c80fb3fde8a4d298a9d38a932edd2f039[j, num] * c6d458b5addfcc5bf18baf0275eb3cd23[j, i];
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
					num2 = (0.0 - num2) / c80fb3fde8a4d298a9d38a932edd2f039[num, num];
					for (int k = num; k < c6ae83731ca5ed1d50b72b5b5953792b6; k++)
					{
						c6d458b5addfcc5bf18baf0275eb3cd23[k, i] += num2 * c80fb3fde8a4d298a9d38a932edd2f039[k, num];
					}
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						goto end_IL_014a;
					}
					continue;
					end_IL_014a:
					break;
				}
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				return c6d458b5addfcc5bf18baf0275eb3cd23;
			}
		}
	}

	/// <summary>
	///       Gets the upper triangular matrix R. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixD R
	{
		get
		{
			if (c1936aacd02c1df6653a5203e412f3235 != null)
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
							__ldtoken(QRDecompositionD.get_R());
						}
						return c1936aacd02c1df6653a5203e412f3235;
					}
				}
			}
			c1936aacd02c1df6653a5203e412f3235 = new MatrixD(c8704416eb2ee8a512db38b95b947dcc8, c8704416eb2ee8a512db38b95b947dcc8);
			for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
			{
				for (int j = 0; j < c8704416eb2ee8a512db38b95b947dcc8; j++)
				{
					if (i < j)
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
						c1936aacd02c1df6653a5203e412f3235[i, j] = c80fb3fde8a4d298a9d38a932edd2f039[i, j];
					}
					else if (i == j)
					{
						c1936aacd02c1df6653a5203e412f3235[i, j] = c00fdda5a9d8efae627a1461abec098e6[i];
					}
					else
					{
						c1936aacd02c1df6653a5203e412f3235[i, j] = 0.0;
					}
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						goto end_IL_00b4;
					}
					continue;
					end_IL_00b4:
					break;
				}
			}
			return c1936aacd02c1df6653a5203e412f3235;
		}
	}

	/// <summary>
	///       Creates the QR decomposition of the given matrix.
	///       </summary>
	/// <param name="matrixA">
	///       The matrix A. (Can be rectangular. NumberOfRows must be ≥ NumberOfColumns.)
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrixA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The number of rows must be greater than or equal to the number of columns.
	///       </exception>
	public QRDecompositionD(MatrixD matrixA)
	{
		if (matrixA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
		}
		if (matrixA.NumberOfRows < matrixA.NumberOfColumns)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2306), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
		}
		c80fb3fde8a4d298a9d38a932edd2f039 = matrixA.Clone();
		c6ae83731ca5ed1d50b72b5b5953792b6 = matrixA.NumberOfRows;
		c8704416eb2ee8a512db38b95b947dcc8 = matrixA.NumberOfColumns;
		c00fdda5a9d8efae627a1461abec098e6 = new double[c8704416eb2ee8a512db38b95b947dcc8];
		int num = 0;
		while (num < c8704416eb2ee8a512db38b95b947dcc8)
		{
			double num2 = 0.0;
			for (int i = num; i < c6ae83731ca5ed1d50b72b5b5953792b6; i++)
			{
				num2 = MathHelper.Hypotenuse(num2, c80fb3fde8a4d298a9d38a932edd2f039[i, num]);
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
					__ldtoken(QRDecompositionD._002Ector(MatrixD));
				}
				if (num2 != 0.0)
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
					if (c80fb3fde8a4d298a9d38a932edd2f039[num, num] < 0.0)
					{
						num2 = 0.0 - num2;
					}
					for (int j = num; j < c6ae83731ca5ed1d50b72b5b5953792b6; j++)
					{
						c80fb3fde8a4d298a9d38a932edd2f039[j, num] /= num2;
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
					c80fb3fde8a4d298a9d38a932edd2f039[num, num]++;
					int num3 = num + 1;
					while (num3 < c8704416eb2ee8a512db38b95b947dcc8)
					{
						double num4 = 0.0;
						for (int k = num; k < c6ae83731ca5ed1d50b72b5b5953792b6; k++)
						{
							num4 += c80fb3fde8a4d298a9d38a932edd2f039[k, num] * c80fb3fde8a4d298a9d38a932edd2f039[k, num3];
						}
						while (true)
						{
							switch (5)
							{
							case 0:
								continue;
							}
							num4 = (0.0 - num4) / c80fb3fde8a4d298a9d38a932edd2f039[num, num];
							for (int l = num; l < c6ae83731ca5ed1d50b72b5b5953792b6; l++)
							{
								c80fb3fde8a4d298a9d38a932edd2f039[l, num3] += num4 * c80fb3fde8a4d298a9d38a932edd2f039[l, num];
							}
							num3++;
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
						break;
					}
				}
				c00fdda5a9d8efae627a1461abec098e6[num] = 0.0 - num2;
				num++;
				break;
			}
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				break;
			default:
				return;
			}
		}
	}

	/// <summary>
	///       Returns the least squares solution for the equation <c>A * X = B</c>.
	///       </summary>
	/// <param name="matrixB">The matrix B with as many rows as A and any number of columns.</param>
	/// <returns>X with the least squares solution.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrixB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">The number of rows does not match.</exception>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">The matrix A does not have full rank.</exception>
	public MatrixD SolveLinearEquations(MatrixD matrixB)
	{
		if (matrixB == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2114));
		}
		if (matrixB.NumberOfRows != c6ae83731ca5ed1d50b72b5b5953792b6)
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
						__ldtoken(QRDecompositionD.SolveLinearEquations(MatrixD));
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2129), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2114));
				}
			}
		}
		if (!HasNumericallyFullRank)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3273));
				}
			}
		}
		MatrixD matrixD = matrixB.Clone();
		for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
		{
			for (int j = 0; j < matrixB.NumberOfColumns; j++)
			{
				double num = 0.0;
				for (int k = i; k < c6ae83731ca5ed1d50b72b5b5953792b6; k++)
				{
					num += c80fb3fde8a4d298a9d38a932edd2f039[k, i] * matrixD[k, j];
				}
				num = (0.0 - num) / c80fb3fde8a4d298a9d38a932edd2f039[i, i];
				for (int l = i; l < c6ae83731ca5ed1d50b72b5b5953792b6; l++)
				{
					matrixD[l, j] += num * c80fb3fde8a4d298a9d38a932edd2f039[l, i];
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						goto end_IL_0125;
					}
					continue;
					end_IL_0125:
					break;
				}
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					goto end_IL_013f;
				}
				continue;
				end_IL_013f:
				break;
			}
		}
		int num2 = c8704416eb2ee8a512db38b95b947dcc8 - 1;
		while (num2 >= 0)
		{
			for (int m = 0; m < matrixB.NumberOfColumns; m++)
			{
				matrixD[num2, m] /= c00fdda5a9d8efae627a1461abec098e6[num2];
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				for (int n = 0; n < num2; n++)
				{
					for (int num3 = 0; num3 < matrixB.NumberOfColumns; num3++)
					{
						matrixD[n, num3] -= matrixD[num2, num3] * c80fb3fde8a4d298a9d38a932edd2f039[n, num2];
					}
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						goto end_IL_0216;
					}
					continue;
					end_IL_0216:
					break;
				}
				num2--;
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
			return matrixD.GetSubmatrix(0, c8704416eb2ee8a512db38b95b947dcc8 - 1, 0, matrixB.NumberOfColumns - 1);
		}
	}
}
