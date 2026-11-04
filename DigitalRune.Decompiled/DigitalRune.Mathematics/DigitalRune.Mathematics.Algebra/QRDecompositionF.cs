using System;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Computes the QR Decomposition of a matrix (single-precision).
///       </summary>
public class QRDecompositionF
{
	private readonly MatrixF c80fb3fde8a4d298a9d38a932edd2f039;

	private MatrixF c6d458b5addfcc5bf18baf0275eb3cd23;

	private MatrixF c1936aacd02c1df6653a5203e412f3235;

	private MatrixF cb16f9640bb813638c5a9009ebeff63a1;

	private readonly int c6ae83731ca5ed1d50b72b5b5953792b6;

	private readonly int c8704416eb2ee8a512db38b95b947dcc8;

	private readonly float[] c00fdda5a9d8efae627a1461abec098e6;

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
					switch (5)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(QRDecompositionF.get_HasNumericallyFullRank());
					}
					return false;
				}
			}
			while (true)
			{
				switch (5)
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
	public MatrixF H
	{
		get
		{
			if (cb16f9640bb813638c5a9009ebeff63a1 != null)
			{
				return cb16f9640bb813638c5a9009ebeff63a1;
			}
			cb16f9640bb813638c5a9009ebeff63a1 = new MatrixF(c6ae83731ca5ed1d50b72b5b5953792b6, c8704416eb2ee8a512db38b95b947dcc8);
			for (int i = 0; i < c6ae83731ca5ed1d50b72b5b5953792b6; i++)
			{
				for (int j = 0; j < c8704416eb2ee8a512db38b95b947dcc8; j++)
				{
					if (i < j)
					{
						continue;
					}
					while (true)
					{
						switch (7)
						{
						case 0:
							continue;
						}
						break;
					}
					if (1 == 0)
					{
						__ldtoken(QRDecompositionF.get_H());
					}
					cb16f9640bb813638c5a9009ebeff63a1[i, j] = c80fb3fde8a4d298a9d38a932edd2f039[i, j];
				}
			}
			return cb16f9640bb813638c5a9009ebeff63a1;
		}
	}

	/// <summary>
	///       Gets the orthogonal matrix Q. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixF Q
	{
		get
		{
			if (c6d458b5addfcc5bf18baf0275eb3cd23 != null)
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
							__ldtoken(QRDecompositionF.get_Q());
						}
						return c6d458b5addfcc5bf18baf0275eb3cd23;
					}
				}
			}
			c6d458b5addfcc5bf18baf0275eb3cd23 = new MatrixF(c6ae83731ca5ed1d50b72b5b5953792b6, c8704416eb2ee8a512db38b95b947dcc8);
			for (int num = c8704416eb2ee8a512db38b95b947dcc8 - 1; num >= 0; num--)
			{
				if (num < c6ae83731ca5ed1d50b72b5b5953792b6)
				{
					c6d458b5addfcc5bf18baf0275eb3cd23[num, num] = 1f;
				}
				for (int i = num; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
				{
					if (c80fb3fde8a4d298a9d38a932edd2f039[num, num] == 0f)
					{
						continue;
					}
					float num2 = 0f;
					for (int j = num; j < c6ae83731ca5ed1d50b72b5b5953792b6; j++)
					{
						num2 += c80fb3fde8a4d298a9d38a932edd2f039[j, num] * c6d458b5addfcc5bf18baf0275eb3cd23[j, i];
					}
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						break;
					}
					num2 = (0f - num2) / c80fb3fde8a4d298a9d38a932edd2f039[num, num];
					for (int k = num; k < c6ae83731ca5ed1d50b72b5b5953792b6; k++)
					{
						c6d458b5addfcc5bf18baf0275eb3cd23[k, i] += num2 * c80fb3fde8a4d298a9d38a932edd2f039[k, num];
					}
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						break;
					}
				}
			}
			return c6d458b5addfcc5bf18baf0275eb3cd23;
		}
	}

	/// <summary>
	///       Gets the upper triangular matrix R. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixF R
	{
		get
		{
			if (c1936aacd02c1df6653a5203e412f3235 != null)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(QRDecompositionF.get_R());
						}
						return c1936aacd02c1df6653a5203e412f3235;
					}
				}
			}
			c1936aacd02c1df6653a5203e412f3235 = new MatrixF(c8704416eb2ee8a512db38b95b947dcc8, c8704416eb2ee8a512db38b95b947dcc8);
			for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
			{
				for (int j = 0; j < c8704416eb2ee8a512db38b95b947dcc8; j++)
				{
					if (i < j)
					{
						c1936aacd02c1df6653a5203e412f3235[i, j] = c80fb3fde8a4d298a9d38a932edd2f039[i, j];
					}
					else if (i == j)
					{
						c1936aacd02c1df6653a5203e412f3235[i, j] = c00fdda5a9d8efae627a1461abec098e6[i];
					}
					else
					{
						c1936aacd02c1df6653a5203e412f3235[i, j] = 0f;
					}
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						goto end_IL_00a8;
					}
					continue;
					end_IL_00a8:
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
	public QRDecompositionF(MatrixF matrixA)
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
						__ldtoken(QRDecompositionF._002Ector(MatrixF));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
				}
			}
		}
		if (matrixA.NumberOfRows < matrixA.NumberOfColumns)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2306), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
				}
			}
		}
		c80fb3fde8a4d298a9d38a932edd2f039 = matrixA.Clone();
		c6ae83731ca5ed1d50b72b5b5953792b6 = matrixA.NumberOfRows;
		c8704416eb2ee8a512db38b95b947dcc8 = matrixA.NumberOfColumns;
		c00fdda5a9d8efae627a1461abec098e6 = new float[c8704416eb2ee8a512db38b95b947dcc8];
		int num = 0;
		while (num < c8704416eb2ee8a512db38b95b947dcc8)
		{
			float num2 = 0f;
			for (int i = num; i < c6ae83731ca5ed1d50b72b5b5953792b6; i++)
			{
				num2 = MathHelper.Hypotenuse(num2, c80fb3fde8a4d298a9d38a932edd2f039[i, num]);
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				if (num2 != 0f)
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
					if (c80fb3fde8a4d298a9d38a932edd2f039[num, num] < 0f)
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
						num2 = 0f - num2;
					}
					for (int j = num; j < c6ae83731ca5ed1d50b72b5b5953792b6; j++)
					{
						c80fb3fde8a4d298a9d38a932edd2f039[j, num] /= num2;
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
					c80fb3fde8a4d298a9d38a932edd2f039[num, num]++;
					for (int k = num + 1; k < c8704416eb2ee8a512db38b95b947dcc8; k++)
					{
						float num3 = 0f;
						for (int l = num; l < c6ae83731ca5ed1d50b72b5b5953792b6; l++)
						{
							num3 += c80fb3fde8a4d298a9d38a932edd2f039[l, num] * c80fb3fde8a4d298a9d38a932edd2f039[l, k];
						}
						num3 = (0f - num3) / c80fb3fde8a4d298a9d38a932edd2f039[num, num];
						for (int m = num; m < c6ae83731ca5ed1d50b72b5b5953792b6; m++)
						{
							c80fb3fde8a4d298a9d38a932edd2f039[m, k] += num3 * c80fb3fde8a4d298a9d38a932edd2f039[m, num];
						}
						while (true)
						{
							switch (2)
							{
							case 0:
								break;
							default:
								goto end_IL_022a;
							}
							continue;
							end_IL_022a:
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
						break;
					}
				}
				c00fdda5a9d8efae627a1461abec098e6[num] = 0f - num2;
				num++;
				break;
			}
		}
		while (true)
		{
			switch (4)
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
		if (!HasNumericallyFullRank)
		{
			throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3273));
		}
		MatrixF matrixF = matrixB.Clone();
		for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
		{
			int num = 0;
			while (num < matrixB.NumberOfColumns)
			{
				float num2 = 0f;
				for (int j = i; j < c6ae83731ca5ed1d50b72b5b5953792b6; j++)
				{
					num2 += c80fb3fde8a4d298a9d38a932edd2f039[j, i] * matrixF[j, num];
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
						__ldtoken(QRDecompositionF.SolveLinearEquations(MatrixF));
					}
					num2 = (0f - num2) / c80fb3fde8a4d298a9d38a932edd2f039[i, i];
					for (int k = i; k < c6ae83731ca5ed1d50b72b5b5953792b6; k++)
					{
						matrixF[k, num] += num2 * c80fb3fde8a4d298a9d38a932edd2f039[k, i];
					}
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
					goto end_IL_0127;
				}
				continue;
				end_IL_0127:
				break;
			}
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			int num3 = c8704416eb2ee8a512db38b95b947dcc8 - 1;
			while (num3 >= 0)
			{
				for (int l = 0; l < matrixB.NumberOfColumns; l++)
				{
					matrixF[num3, l] /= c00fdda5a9d8efae627a1461abec098e6[num3];
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					for (int m = 0; m < num3; m++)
					{
						for (int n = 0; n < matrixB.NumberOfColumns; n++)
						{
							matrixF[m, n] -= matrixF[num3, n] * c80fb3fde8a4d298a9d38a932edd2f039[m, num3];
						}
					}
					num3--;
					break;
				}
			}
			return matrixF.GetSubmatrix(0, c8704416eb2ee8a512db38b95b947dcc8 - 1, 0, matrixB.NumberOfColumns - 1);
		}
	}
}
