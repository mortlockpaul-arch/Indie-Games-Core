using System;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Computes the Singular Value Decomposition (SVD) of a matrix (double-precision).
///       </summary>
public class SingularValueDecompositionD
{
	private readonly MatrixD c57880ecc022a6861cd4d2d6e60986133;

	private readonly MatrixD ccbdf70e0565d415a0cffd34ca03ac5e3;

	private readonly VectorD c7e5b75e3e3ecaee327bd4f034b73fe79;

	private MatrixD c3fdd944710aa3ea645b83e278d648282;

	private readonly int c6ae83731ca5ed1d50b72b5b5953792b6;

	private readonly int c8704416eb2ee8a512db38b95b947dcc8;

	private int c485333536cea24abf4481a6b5913a28e = -1;

	/// <summary>
	///       Gets the condition number of A.
	///       </summary>
	public double ConditionNumber => c7e5b75e3e3ecaee327bd4f034b73fe79[0] / c7e5b75e3e3ecaee327bd4f034b73fe79[Math.Min(c6ae83731ca5ed1d50b72b5b5953792b6, c8704416eb2ee8a512db38b95b947dcc8) - 1];

	/// <summary>
	///       Gets the diagonal matrix S with the singular values. (This property returns the internal 
	///       matrix, not a copy.)
	///       </summary>
	public MatrixD S
	{
		get
		{
			if (c3fdd944710aa3ea645b83e278d648282 != null)
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
							__ldtoken(SingularValueDecompositionD.get_S());
						}
						return c3fdd944710aa3ea645b83e278d648282;
					}
				}
			}
			c3fdd944710aa3ea645b83e278d648282 = new MatrixD(c8704416eb2ee8a512db38b95b947dcc8, c8704416eb2ee8a512db38b95b947dcc8);
			for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
			{
				c3fdd944710aa3ea645b83e278d648282[i, i] = c7e5b75e3e3ecaee327bd4f034b73fe79[i];
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				return c3fdd944710aa3ea645b83e278d648282;
			}
		}
	}

	/// <summary>
	///       Gets the matrix U with the left singular vectors. (This property returns the internal 
	///       matrix, not a copy.)
	///       </summary>
	public MatrixD U => c57880ecc022a6861cd4d2d6e60986133;

	/// <summary>
	///       Gets the matrix V with the right singular vectors. (This property returns the internal 
	///       matrix, not a copy.)
	///       </summary>
	public MatrixD V => ccbdf70e0565d415a0cffd34ca03ac5e3;

	/// <summary>
	///       Gets the two norm of A.
	///       </summary>
	public double Norm2 => c7e5b75e3e3ecaee327bd4f034b73fe79[0];

	/// <summary>
	///       Gets the effective numerical rank of A.
	///       </summary>
	public int NumericalRank
	{
		get
		{
			if (c485333536cea24abf4481a6b5913a28e == -1)
			{
				double num = Math.Pow(2.0, -52.0);
				double num2 = (double)Math.Max(c6ae83731ca5ed1d50b72b5b5953792b6, c8704416eb2ee8a512db38b95b947dcc8) * c7e5b75e3e3ecaee327bd4f034b73fe79[0] * num;
				c485333536cea24abf4481a6b5913a28e = 0;
				for (int i = 0; i < c7e5b75e3e3ecaee327bd4f034b73fe79.NumberOfElements; i++)
				{
					if (!(c7e5b75e3e3ecaee327bd4f034b73fe79[i] > num2))
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
						__ldtoken(SingularValueDecompositionD.get_NumericalRank());
					}
					c485333536cea24abf4481a6b5913a28e++;
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
			return c485333536cea24abf4481a6b5913a28e;
		}
	}

	/// <summary>
	///       Gets the vector of singular values (the diagonal of S). 
	///       </summary>
	public VectorD SingularValues => c7e5b75e3e3ecaee327bd4f034b73fe79;

	/// <summary>
	///       Creates the singular value decomposition of the given matrix.
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
	public SingularValueDecompositionD(MatrixD matrixA)
	{
		if (matrixA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
		}
		c6ae83731ca5ed1d50b72b5b5953792b6 = matrixA.NumberOfRows;
		c8704416eb2ee8a512db38b95b947dcc8 = matrixA.NumberOfColumns;
		MatrixD matrixD = matrixA.Clone();
		if (c6ae83731ca5ed1d50b72b5b5953792b6 < c8704416eb2ee8a512db38b95b947dcc8)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2306), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
		}
		int num = Math.Min(c6ae83731ca5ed1d50b72b5b5953792b6, c8704416eb2ee8a512db38b95b947dcc8);
		c7e5b75e3e3ecaee327bd4f034b73fe79 = new VectorD(Math.Min(c6ae83731ca5ed1d50b72b5b5953792b6 + 1, c8704416eb2ee8a512db38b95b947dcc8));
		c57880ecc022a6861cd4d2d6e60986133 = new MatrixD(c6ae83731ca5ed1d50b72b5b5953792b6, num);
		ccbdf70e0565d415a0cffd34ca03ac5e3 = new MatrixD(c8704416eb2ee8a512db38b95b947dcc8, c8704416eb2ee8a512db38b95b947dcc8);
		double[] array = new double[c8704416eb2ee8a512db38b95b947dcc8];
		double[] array2 = new double[c6ae83731ca5ed1d50b72b5b5953792b6];
		for (int i = 0; i < c6ae83731ca5ed1d50b72b5b5953792b6; i++)
		{
			for (int j = 0; j < c8704416eb2ee8a512db38b95b947dcc8; j++)
			{
				if (!Numeric.IsNaN(matrixA[i, j]))
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
						__ldtoken(SingularValueDecompositionD._002Ector(MatrixD));
					}
					c57880ecc022a6861cd4d2d6e60986133.Set(double.NaN);
					ccbdf70e0565d415a0cffd34ca03ac5e3.Set(double.NaN);
					c7e5b75e3e3ecaee327bd4f034b73fe79.Set(double.NaN);
					return;
				}
			}
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			int num2 = Math.Min(c6ae83731ca5ed1d50b72b5b5953792b6 - 1, c8704416eb2ee8a512db38b95b947dcc8);
			int num3 = Math.Max(0, Math.Min(c8704416eb2ee8a512db38b95b947dcc8 - 2, c6ae83731ca5ed1d50b72b5b5953792b6));
			for (int k = 0; k < Math.Max(num2, num3); k++)
			{
				if (k < num2)
				{
					c7e5b75e3e3ecaee327bd4f034b73fe79[k] = 0.0;
					for (int l = k; l < c6ae83731ca5ed1d50b72b5b5953792b6; l++)
					{
						c7e5b75e3e3ecaee327bd4f034b73fe79[k] = MathHelper.Hypotenuse(c7e5b75e3e3ecaee327bd4f034b73fe79[k], matrixD[l, k]);
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							continue;
						}
						break;
					}
					if (c7e5b75e3e3ecaee327bd4f034b73fe79[k] != 0.0)
					{
						while (true)
						{
							switch (4)
							{
							case 0:
								continue;
							}
							break;
						}
						if (matrixD[k, k] < 0.0)
						{
							c7e5b75e3e3ecaee327bd4f034b73fe79[k] = 0.0 - c7e5b75e3e3ecaee327bd4f034b73fe79[k];
						}
						for (int m = k; m < c6ae83731ca5ed1d50b72b5b5953792b6; m++)
						{
							matrixD[m, k] /= c7e5b75e3e3ecaee327bd4f034b73fe79[k];
						}
						matrixD[k, k]++;
					}
					c7e5b75e3e3ecaee327bd4f034b73fe79[k] = 0.0 - c7e5b75e3e3ecaee327bd4f034b73fe79[k];
				}
				for (int n = k + 1; n < c8704416eb2ee8a512db38b95b947dcc8; n++)
				{
					if (k < num2)
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
						if (c7e5b75e3e3ecaee327bd4f034b73fe79[k] != 0.0)
						{
							double num4 = 0.0;
							for (int num5 = k; num5 < c6ae83731ca5ed1d50b72b5b5953792b6; num5++)
							{
								num4 += matrixD[num5, k] * matrixD[num5, n];
							}
							num4 = (0.0 - num4) / matrixD[k, k];
							for (int num6 = k; num6 < c6ae83731ca5ed1d50b72b5b5953792b6; num6++)
							{
								matrixD[num6, n] += num4 * matrixD[num6, k];
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
					}
					array[n] = matrixD[k, n];
				}
				if (k < num2)
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
					for (int num7 = k; num7 < c6ae83731ca5ed1d50b72b5b5953792b6; num7++)
					{
						c57880ecc022a6861cd4d2d6e60986133[num7, k] = matrixD[num7, k];
					}
				}
				if (k >= num3)
				{
					continue;
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
				array[k] = 0.0;
				for (int num8 = k + 1; num8 < c8704416eb2ee8a512db38b95b947dcc8; num8++)
				{
					array[k] = MathHelper.Hypotenuse(array[k], array[num8]);
				}
				if (array[k] != 0.0)
				{
					if (array[k + 1] < 0.0)
					{
						while (true)
						{
							switch (4)
							{
							case 0:
								continue;
							}
							break;
						}
						array[k] = 0.0 - array[k];
					}
					for (int num9 = k + 1; num9 < c8704416eb2ee8a512db38b95b947dcc8; num9++)
					{
						array[num9] /= array[k];
					}
					array[k + 1]++;
				}
				array[k] = 0.0 - array[k];
				if (k + 1 < c6ae83731ca5ed1d50b72b5b5953792b6)
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
					if (array[k] != 0.0)
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
						for (int num10 = k + 1; num10 < c6ae83731ca5ed1d50b72b5b5953792b6; num10++)
						{
							array2[num10] = 0.0;
						}
						for (int num11 = k + 1; num11 < c8704416eb2ee8a512db38b95b947dcc8; num11++)
						{
							for (int num12 = k + 1; num12 < c6ae83731ca5ed1d50b72b5b5953792b6; num12++)
							{
								array2[num12] += array[num11] * matrixD[num12, num11];
							}
						}
						for (int num13 = k + 1; num13 < c8704416eb2ee8a512db38b95b947dcc8; num13++)
						{
							double num14 = (0.0 - array[num13]) / array[k + 1];
							for (int num15 = k + 1; num15 < c6ae83731ca5ed1d50b72b5b5953792b6; num15++)
							{
								matrixD[num15, num13] += num14 * array2[num15];
							}
							while (true)
							{
								switch (2)
								{
								case 0:
									break;
								default:
									goto end_IL_061c;
								}
								continue;
								end_IL_061c:
								break;
							}
						}
					}
				}
				for (int num16 = k + 1; num16 < c8704416eb2ee8a512db38b95b947dcc8; num16++)
				{
					ccbdf70e0565d415a0cffd34ca03ac5e3[num16, k] = array[num16];
				}
			}
			int num17 = Math.Min(c8704416eb2ee8a512db38b95b947dcc8, c6ae83731ca5ed1d50b72b5b5953792b6 + 1);
			if (num2 < c8704416eb2ee8a512db38b95b947dcc8)
			{
				c7e5b75e3e3ecaee327bd4f034b73fe79[num2] = matrixD[num2, num2];
			}
			if (c6ae83731ca5ed1d50b72b5b5953792b6 < num17)
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
				c7e5b75e3e3ecaee327bd4f034b73fe79[num17 - 1] = 0.0;
			}
			if (num3 + 1 < num17)
			{
				array[num3] = matrixD[num3, num17 - 1];
			}
			array[num17 - 1] = 0.0;
			for (int num18 = num2; num18 < num; num18++)
			{
				for (int num19 = 0; num19 < c6ae83731ca5ed1d50b72b5b5953792b6; num19++)
				{
					c57880ecc022a6861cd4d2d6e60986133[num19, num18] = 0.0;
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						goto end_IL_0737;
					}
					continue;
					end_IL_0737:
					break;
				}
				c57880ecc022a6861cd4d2d6e60986133[num18, num18] = 1.0;
			}
			for (int num20 = num2 - 1; num20 >= 0; num20--)
			{
				if (c7e5b75e3e3ecaee327bd4f034b73fe79[num20] != 0.0)
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
					for (int num21 = num20 + 1; num21 < num; num21++)
					{
						double num22 = 0.0;
						for (int num23 = num20; num23 < c6ae83731ca5ed1d50b72b5b5953792b6; num23++)
						{
							num22 += c57880ecc022a6861cd4d2d6e60986133[num23, num20] * c57880ecc022a6861cd4d2d6e60986133[num23, num21];
						}
						num22 = (0.0 - num22) / c57880ecc022a6861cd4d2d6e60986133[num20, num20];
						for (int num24 = num20; num24 < c6ae83731ca5ed1d50b72b5b5953792b6; num24++)
						{
							c57880ecc022a6861cd4d2d6e60986133[num24, num21] += num22 * c57880ecc022a6861cd4d2d6e60986133[num24, num20];
						}
						while (true)
						{
							switch (6)
							{
							case 0:
								break;
							default:
								goto end_IL_0849;
							}
							continue;
							end_IL_0849:
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
					for (int num25 = num20; num25 < c6ae83731ca5ed1d50b72b5b5953792b6; num25++)
					{
						c57880ecc022a6861cd4d2d6e60986133[num25, num20] = 0.0 - c57880ecc022a6861cd4d2d6e60986133[num25, num20];
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
					c57880ecc022a6861cd4d2d6e60986133[num20, num20] = 1.0 + c57880ecc022a6861cd4d2d6e60986133[num20, num20];
					for (int num26 = 0; num26 < num20 - 1; num26++)
					{
						c57880ecc022a6861cd4d2d6e60986133[num26, num20] = 0.0;
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
				else
				{
					for (int num27 = 0; num27 < c6ae83731ca5ed1d50b72b5b5953792b6; num27++)
					{
						c57880ecc022a6861cd4d2d6e60986133[num27, num20] = 0.0;
					}
					c57880ecc022a6861cd4d2d6e60986133[num20, num20] = 1.0;
				}
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				for (int num28 = c8704416eb2ee8a512db38b95b947dcc8 - 1; num28 >= 0; num28--)
				{
					if ((num28 < num3) & (array[num28] != 0.0))
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
						for (int num29 = num28 + 1; num29 < num; num29++)
						{
							double num30 = 0.0;
							for (int num31 = num28 + 1; num31 < c8704416eb2ee8a512db38b95b947dcc8; num31++)
							{
								num30 += ccbdf70e0565d415a0cffd34ca03ac5e3[num31, num28] * ccbdf70e0565d415a0cffd34ca03ac5e3[num31, num29];
							}
							num30 = (0.0 - num30) / ccbdf70e0565d415a0cffd34ca03ac5e3[num28 + 1, num28];
							for (int num32 = num28 + 1; num32 < c8704416eb2ee8a512db38b95b947dcc8; num32++)
							{
								ccbdf70e0565d415a0cffd34ca03ac5e3[num32, num29] += num30 * ccbdf70e0565d415a0cffd34ca03ac5e3[num32, num28];
							}
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
					for (int num33 = 0; num33 < c8704416eb2ee8a512db38b95b947dcc8; num33++)
					{
						ccbdf70e0565d415a0cffd34ca03ac5e3[num33, num28] = 0.0;
					}
					while (true)
					{
						switch (4)
						{
						case 0:
							break;
						default:
							goto end_IL_0a9f;
						}
						continue;
						end_IL_0a9f:
						break;
					}
					ccbdf70e0565d415a0cffd34ca03ac5e3[num28, num28] = 1.0;
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					int num34 = num17 - 1;
					int num35 = 0;
					double num36 = Math.Pow(2.0, -52.0);
					double num37 = Math.Pow(2.0, -966.0);
					while (num17 > 0)
					{
						int num38 = num17 - 2;
						while (num38 >= -1 && num38 != -1)
						{
							if (Math.Abs(array[num38]) <= num37 + num36 * (Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num38]) + Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num38 + 1])))
							{
								array[num38] = 0.0;
								break;
							}
							num38--;
						}
						int num39;
						if (num38 == num17 - 2)
						{
							num39 = 4;
						}
						else
						{
							int num40 = num17 - 1;
							while (true)
							{
								if (num40 >= num38)
								{
									if (num40 == num38)
									{
										break;
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
									double num41;
									if (num40 == num17)
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
										num41 = 0.0;
									}
									else
									{
										num41 = Math.Abs(array[num40]);
									}
									double num42;
									if (num40 == num38 + 1)
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
										num42 = 0.0;
									}
									else
									{
										num42 = Math.Abs(array[num40 - 1]);
									}
									double num43 = num41 + num42;
									if (Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num40]) <= num37 + num36 * num43)
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
										c7e5b75e3e3ecaee327bd4f034b73fe79[num40] = 0.0;
										break;
									}
									num40--;
									continue;
								}
								while (true)
								{
									switch (3)
									{
									case 0:
										continue;
									}
									break;
								}
								break;
							}
							if (num40 == num38)
							{
								num39 = 3;
							}
							else if (num40 == num17 - 1)
							{
								num39 = 1;
							}
							else
							{
								num39 = 2;
								num38 = num40;
							}
						}
						num38++;
						switch (num39)
						{
						case 1:
						{
							double num70 = array[num17 - 2];
							array[num17 - 2] = 0.0;
							for (int num71 = num17 - 2; num71 >= num38; num71--)
							{
								double num72 = MathHelper.Hypotenuse(c7e5b75e3e3ecaee327bd4f034b73fe79[num71], num70);
								double num73 = c7e5b75e3e3ecaee327bd4f034b73fe79[num71] / num72;
								double num74 = num70 / num72;
								c7e5b75e3e3ecaee327bd4f034b73fe79[num71] = num72;
								if (num71 != num38)
								{
									num70 = (0.0 - num74) * array[num71 - 1];
									array[num71 - 1] = num73 * array[num71 - 1];
								}
								for (int num75 = 0; num75 < c8704416eb2ee8a512db38b95b947dcc8; num75++)
								{
									num72 = num73 * ccbdf70e0565d415a0cffd34ca03ac5e3[num75, num71] + num74 * ccbdf70e0565d415a0cffd34ca03ac5e3[num75, num17 - 1];
									ccbdf70e0565d415a0cffd34ca03ac5e3[num75, num17 - 1] = (0.0 - num74) * ccbdf70e0565d415a0cffd34ca03ac5e3[num75, num71] + num73 * ccbdf70e0565d415a0cffd34ca03ac5e3[num75, num17 - 1];
									ccbdf70e0565d415a0cffd34ca03ac5e3[num75, num71] = num72;
								}
								while (true)
								{
									switch (3)
									{
									case 0:
										break;
									default:
										goto end_IL_0db5;
									}
									continue;
									end_IL_0db5:
									break;
								}
							}
							break;
						}
						case 2:
						{
							double num47 = array[num38 - 1];
							array[num38 - 1] = 0.0;
							for (int num48 = num38; num48 < num17; num48++)
							{
								double num49 = MathHelper.Hypotenuse(c7e5b75e3e3ecaee327bd4f034b73fe79[num48], num47);
								double num50 = c7e5b75e3e3ecaee327bd4f034b73fe79[num48] / num49;
								double num51 = num47 / num49;
								c7e5b75e3e3ecaee327bd4f034b73fe79[num48] = num49;
								num47 = (0.0 - num51) * array[num48];
								array[num48] = num50 * array[num48];
								for (int num52 = 0; num52 < c6ae83731ca5ed1d50b72b5b5953792b6; num52++)
								{
									num49 = num50 * c57880ecc022a6861cd4d2d6e60986133[num52, num48] + num51 * c57880ecc022a6861cd4d2d6e60986133[num52, num38 - 1];
									c57880ecc022a6861cd4d2d6e60986133[num52, num38 - 1] = (0.0 - num51) * c57880ecc022a6861cd4d2d6e60986133[num52, num48] + num50 * c57880ecc022a6861cd4d2d6e60986133[num52, num38 - 1];
									c57880ecc022a6861cd4d2d6e60986133[num52, num48] = num49;
								}
								while (true)
								{
									switch (4)
									{
									case 0:
										break;
									default:
										goto end_IL_0edb;
									}
									continue;
									end_IL_0edb:
									break;
								}
							}
							break;
						}
						case 3:
						{
							double num53 = Math.Max(Math.Max(Math.Max(Math.Max(Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num17 - 1]), Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num17 - 2])), Math.Abs(array[num17 - 2])), Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num38])), Math.Abs(array[num38]));
							double num54 = c7e5b75e3e3ecaee327bd4f034b73fe79[num17 - 1] / num53;
							double num55 = c7e5b75e3e3ecaee327bd4f034b73fe79[num17 - 2] / num53;
							double num56 = array[num17 - 2] / num53;
							double num57 = c7e5b75e3e3ecaee327bd4f034b73fe79[num38] / num53;
							double num58 = array[num38] / num53;
							double num59 = ((num55 + num54) * (num55 - num54) + num56 * num56) / 2.0;
							double num60 = num54 * num56 * (num54 * num56);
							double num61 = 0.0;
							if ((num59 != 0.0) | (num60 != 0.0))
							{
								num61 = Math.Sqrt(num59 * num59 + num60);
								if (num59 < 0.0)
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
									num61 = 0.0 - num61;
								}
								num61 = num60 / (num59 + num61);
							}
							double num62 = (num57 + num54) * (num57 - num54) + num61;
							double num63 = num57 * num58;
							int num64 = num38;
							while (num64 < num17 - 1)
							{
								double num65 = MathHelper.Hypotenuse(num62, num63);
								double num66 = num62 / num65;
								double num67 = num63 / num65;
								if (num64 != num38)
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
									array[num64 - 1] = num65;
								}
								num62 = num66 * c7e5b75e3e3ecaee327bd4f034b73fe79[num64] + num67 * array[num64];
								array[num64] = num66 * array[num64] - num67 * c7e5b75e3e3ecaee327bd4f034b73fe79[num64];
								num63 = num67 * c7e5b75e3e3ecaee327bd4f034b73fe79[num64 + 1];
								c7e5b75e3e3ecaee327bd4f034b73fe79[num64 + 1] = num66 * c7e5b75e3e3ecaee327bd4f034b73fe79[num64 + 1];
								for (int num68 = 0; num68 < c8704416eb2ee8a512db38b95b947dcc8; num68++)
								{
									num65 = num66 * ccbdf70e0565d415a0cffd34ca03ac5e3[num68, num64] + num67 * ccbdf70e0565d415a0cffd34ca03ac5e3[num68, num64 + 1];
									ccbdf70e0565d415a0cffd34ca03ac5e3[num68, num64 + 1] = (0.0 - num67) * ccbdf70e0565d415a0cffd34ca03ac5e3[num68, num64] + num66 * ccbdf70e0565d415a0cffd34ca03ac5e3[num68, num64 + 1];
									ccbdf70e0565d415a0cffd34ca03ac5e3[num68, num64] = num65;
								}
								while (true)
								{
									switch (3)
									{
									case 0:
										continue;
									}
									num65 = MathHelper.Hypotenuse(num62, num63);
									num66 = num62 / num65;
									num67 = num63 / num65;
									c7e5b75e3e3ecaee327bd4f034b73fe79[num64] = num65;
									num62 = num66 * array[num64] + num67 * c7e5b75e3e3ecaee327bd4f034b73fe79[num64 + 1];
									c7e5b75e3e3ecaee327bd4f034b73fe79[num64 + 1] = (0.0 - num67) * array[num64] + num66 * c7e5b75e3e3ecaee327bd4f034b73fe79[num64 + 1];
									num63 = num67 * array[num64 + 1];
									array[num64 + 1] = num66 * array[num64 + 1];
									if (num64 < c6ae83731ca5ed1d50b72b5b5953792b6 - 1)
									{
										for (int num69 = 0; num69 < c6ae83731ca5ed1d50b72b5b5953792b6; num69++)
										{
											num65 = num66 * c57880ecc022a6861cd4d2d6e60986133[num69, num64] + num67 * c57880ecc022a6861cd4d2d6e60986133[num69, num64 + 1];
											c57880ecc022a6861cd4d2d6e60986133[num69, num64 + 1] = (0.0 - num67) * c57880ecc022a6861cd4d2d6e60986133[num69, num64] + num66 * c57880ecc022a6861cd4d2d6e60986133[num69, num64 + 1];
											c57880ecc022a6861cd4d2d6e60986133[num69, num64] = num65;
										}
									}
									num64++;
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
								break;
							}
							array[num17 - 2] = num62;
							num35++;
							break;
						}
						case 4:
							if (c7e5b75e3e3ecaee327bd4f034b73fe79[num38] <= 0.0)
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
								c7e5b75e3e3ecaee327bd4f034b73fe79[num38] = ((c7e5b75e3e3ecaee327bd4f034b73fe79[num38] < 0.0) ? (0.0 - c7e5b75e3e3ecaee327bd4f034b73fe79[num38]) : 0.0);
								for (int num44 = 0; num44 <= num34; num44++)
								{
									ccbdf70e0565d415a0cffd34ca03ac5e3[num44, num38] = 0.0 - ccbdf70e0565d415a0cffd34ca03ac5e3[num44, num38];
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
							while (true)
							{
								if (num38 < num34)
								{
									if (c7e5b75e3e3ecaee327bd4f034b73fe79[num38] >= c7e5b75e3e3ecaee327bd4f034b73fe79[num38 + 1])
									{
										break;
									}
									while (true)
									{
										switch (1)
										{
										case 0:
											continue;
										}
										double value = c7e5b75e3e3ecaee327bd4f034b73fe79[num38];
										c7e5b75e3e3ecaee327bd4f034b73fe79[num38] = c7e5b75e3e3ecaee327bd4f034b73fe79[num38 + 1];
										c7e5b75e3e3ecaee327bd4f034b73fe79[num38 + 1] = value;
										if (num38 < c8704416eb2ee8a512db38b95b947dcc8 - 1)
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
											for (int num45 = 0; num45 < c8704416eb2ee8a512db38b95b947dcc8; num45++)
											{
												value = ccbdf70e0565d415a0cffd34ca03ac5e3[num45, num38 + 1];
												ccbdf70e0565d415a0cffd34ca03ac5e3[num45, num38 + 1] = ccbdf70e0565d415a0cffd34ca03ac5e3[num45, num38];
												ccbdf70e0565d415a0cffd34ca03ac5e3[num45, num38] = value;
											}
										}
										if (num38 < c6ae83731ca5ed1d50b72b5b5953792b6 - 1)
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
											for (int num46 = 0; num46 < c6ae83731ca5ed1d50b72b5b5953792b6; num46++)
											{
												value = c57880ecc022a6861cd4d2d6e60986133[num46, num38 + 1];
												c57880ecc022a6861cd4d2d6e60986133[num46, num38 + 1] = c57880ecc022a6861cd4d2d6e60986133[num46, num38];
												c57880ecc022a6861cd4d2d6e60986133[num46, num38] = value;
											}
										}
										num38++;
										break;
									}
									continue;
								}
								while (true)
								{
									switch (3)
									{
									case 0:
										continue;
									}
									break;
								}
								break;
							}
							num35 = 0;
							num17--;
							break;
						}
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
			}
		}
	}
}
