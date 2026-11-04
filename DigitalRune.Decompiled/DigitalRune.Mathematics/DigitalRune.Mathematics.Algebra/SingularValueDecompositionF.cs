using System;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Computes the Singular Value Decomposition (SVD) of a matrix (single-precision).
///       </summary>
public class SingularValueDecompositionF
{
	private readonly MatrixF c57880ecc022a6861cd4d2d6e60986133;

	private readonly MatrixF ccbdf70e0565d415a0cffd34ca03ac5e3;

	private readonly VectorF c7e5b75e3e3ecaee327bd4f034b73fe79;

	private MatrixF c3fdd944710aa3ea645b83e278d648282;

	private readonly int c6ae83731ca5ed1d50b72b5b5953792b6;

	private readonly int c8704416eb2ee8a512db38b95b947dcc8;

	private int c485333536cea24abf4481a6b5913a28e = -1;

	/// <summary>
	///       Gets the condition number of A.
	///       </summary>
	public float ConditionNumber => c7e5b75e3e3ecaee327bd4f034b73fe79[0] / c7e5b75e3e3ecaee327bd4f034b73fe79[Math.Min(c6ae83731ca5ed1d50b72b5b5953792b6, c8704416eb2ee8a512db38b95b947dcc8) - 1];

	/// <summary>
	///       Gets the diagonal matrix S with the singular values. (This property returns the internal 
	///       matrix, not a copy.)
	///       </summary>
	public MatrixF S
	{
		get
		{
			if (c3fdd944710aa3ea645b83e278d648282 != null)
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
							__ldtoken(SingularValueDecompositionF.get_S());
						}
						return c3fdd944710aa3ea645b83e278d648282;
					}
				}
			}
			c3fdd944710aa3ea645b83e278d648282 = new MatrixF(c8704416eb2ee8a512db38b95b947dcc8, c8704416eb2ee8a512db38b95b947dcc8);
			for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
			{
				c3fdd944710aa3ea645b83e278d648282[i, i] = c7e5b75e3e3ecaee327bd4f034b73fe79[i];
			}
			return c3fdd944710aa3ea645b83e278d648282;
		}
	}

	/// <summary>
	///       Gets the matrix U with the left singular vectors. (This property returns the internal 
	///       matrix, not a copy.)
	///       </summary>
	public MatrixF U => c57880ecc022a6861cd4d2d6e60986133;

	/// <summary>
	///       Gets the matrix V with the right singular vectors. (This property returns the internal 
	///       matrix, not a copy.)
	///       </summary>
	public MatrixF V => ccbdf70e0565d415a0cffd34ca03ac5e3;

	/// <summary>
	///       Gets the two norm of A.
	///       </summary>
	public float Norm2 => c7e5b75e3e3ecaee327bd4f034b73fe79[0];

	/// <summary>
	///       Gets the effective numerical rank of A.
	///       </summary>
	public int NumericalRank
	{
		get
		{
			if (c485333536cea24abf4481a6b5913a28e == -1)
			{
				float num = (float)Math.Pow(2.0, -23.0);
				float num2 = (float)Math.Max(c6ae83731ca5ed1d50b72b5b5953792b6, c8704416eb2ee8a512db38b95b947dcc8) * c7e5b75e3e3ecaee327bd4f034b73fe79[0] * num;
				c485333536cea24abf4481a6b5913a28e = 0;
				for (int i = 0; i < c7e5b75e3e3ecaee327bd4f034b73fe79.NumberOfElements; i++)
				{
					if (!(c7e5b75e3e3ecaee327bd4f034b73fe79[i] > num2))
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
					if (1 == 0)
					{
						__ldtoken(SingularValueDecompositionF.get_NumericalRank());
					}
					c485333536cea24abf4481a6b5913a28e++;
				}
			}
			return c485333536cea24abf4481a6b5913a28e;
		}
	}

	/// <summary>
	///       Gets the vector of singular values (the diagonal of S). 
	///       </summary>
	public VectorF SingularValues => c7e5b75e3e3ecaee327bd4f034b73fe79;

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
	public SingularValueDecompositionF(MatrixF matrixA)
	{
		if (matrixA == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(SingularValueDecompositionF._002Ector(MatrixF));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
				}
			}
		}
		c6ae83731ca5ed1d50b72b5b5953792b6 = matrixA.NumberOfRows;
		c8704416eb2ee8a512db38b95b947dcc8 = matrixA.NumberOfColumns;
		MatrixF matrixF = matrixA.Clone();
		if (c6ae83731ca5ed1d50b72b5b5953792b6 < c8704416eb2ee8a512db38b95b947dcc8)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2306), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
		}
		int num = Math.Min(c6ae83731ca5ed1d50b72b5b5953792b6, c8704416eb2ee8a512db38b95b947dcc8);
		c7e5b75e3e3ecaee327bd4f034b73fe79 = new VectorF(Math.Min(c6ae83731ca5ed1d50b72b5b5953792b6 + 1, c8704416eb2ee8a512db38b95b947dcc8));
		c57880ecc022a6861cd4d2d6e60986133 = new MatrixF(c6ae83731ca5ed1d50b72b5b5953792b6, num);
		ccbdf70e0565d415a0cffd34ca03ac5e3 = new MatrixF(c8704416eb2ee8a512db38b95b947dcc8, c8704416eb2ee8a512db38b95b947dcc8);
		float[] array = new float[c8704416eb2ee8a512db38b95b947dcc8];
		float[] array2 = new float[c6ae83731ca5ed1d50b72b5b5953792b6];
		for (int i = 0; i < c6ae83731ca5ed1d50b72b5b5953792b6; i++)
		{
			for (int j = 0; j < c8704416eb2ee8a512db38b95b947dcc8; j++)
			{
				if (Numeric.IsNaN(matrixA[i, j]))
				{
					c57880ecc022a6861cd4d2d6e60986133.Set(float.NaN);
					ccbdf70e0565d415a0cffd34ca03ac5e3.Set(float.NaN);
					c7e5b75e3e3ecaee327bd4f034b73fe79.Set(float.NaN);
					return;
				}
			}
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			int num2 = Math.Min(c6ae83731ca5ed1d50b72b5b5953792b6 - 1, c8704416eb2ee8a512db38b95b947dcc8);
			int num3 = Math.Max(0, Math.Min(c8704416eb2ee8a512db38b95b947dcc8 - 2, c6ae83731ca5ed1d50b72b5b5953792b6));
			int num4 = 0;
			while (num4 < Math.Max(num2, num3))
			{
				if (num4 < num2)
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
					c7e5b75e3e3ecaee327bd4f034b73fe79[num4] = 0f;
					for (int k = num4; k < c6ae83731ca5ed1d50b72b5b5953792b6; k++)
					{
						c7e5b75e3e3ecaee327bd4f034b73fe79[num4] = MathHelper.Hypotenuse(c7e5b75e3e3ecaee327bd4f034b73fe79[num4], matrixF[k, num4]);
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
					if (c7e5b75e3e3ecaee327bd4f034b73fe79[num4] != 0f)
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
						if (matrixF[num4, num4] < 0f)
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
							c7e5b75e3e3ecaee327bd4f034b73fe79[num4] = 0f - c7e5b75e3e3ecaee327bd4f034b73fe79[num4];
						}
						for (int l = num4; l < c6ae83731ca5ed1d50b72b5b5953792b6; l++)
						{
							matrixF[l, num4] /= c7e5b75e3e3ecaee327bd4f034b73fe79[num4];
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
						matrixF[num4, num4]++;
					}
					c7e5b75e3e3ecaee327bd4f034b73fe79[num4] = 0f - c7e5b75e3e3ecaee327bd4f034b73fe79[num4];
				}
				for (int m = num4 + 1; m < c8704416eb2ee8a512db38b95b947dcc8; m++)
				{
					if (num4 < num2)
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
						if (c7e5b75e3e3ecaee327bd4f034b73fe79[num4] != 0f)
						{
							float num5 = 0f;
							for (int n = num4; n < c6ae83731ca5ed1d50b72b5b5953792b6; n++)
							{
								num5 += matrixF[n, num4] * matrixF[n, m];
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
							num5 = (0f - num5) / matrixF[num4, num4];
							for (int num6 = num4; num6 < c6ae83731ca5ed1d50b72b5b5953792b6; num6++)
							{
								matrixF[num6, m] += num5 * matrixF[num6, num4];
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
					}
					array[m] = matrixF[num4, m];
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					if (num4 < num2)
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
						for (int num7 = num4; num7 < c6ae83731ca5ed1d50b72b5b5953792b6; num7++)
						{
							c57880ecc022a6861cd4d2d6e60986133[num7, num4] = matrixF[num7, num4];
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
					if (num4 < num3)
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
						array[num4] = 0f;
						for (int num8 = num4 + 1; num8 < c8704416eb2ee8a512db38b95b947dcc8; num8++)
						{
							array[num4] = MathHelper.Hypotenuse(array[num4], array[num8]);
						}
						if (array[num4] != 0f)
						{
							if (array[num4 + 1] < 0f)
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
								array[num4] = 0f - array[num4];
							}
							for (int num9 = num4 + 1; num9 < c8704416eb2ee8a512db38b95b947dcc8; num9++)
							{
								array[num9] /= array[num4];
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
							array[num4 + 1]++;
						}
						array[num4] = 0f - array[num4];
						if (num4 + 1 < c6ae83731ca5ed1d50b72b5b5953792b6)
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
							if (array[num4] != 0f)
							{
								for (int num10 = num4 + 1; num10 < c6ae83731ca5ed1d50b72b5b5953792b6; num10++)
								{
									array2[num10] = 0f;
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
								for (int num11 = num4 + 1; num11 < c8704416eb2ee8a512db38b95b947dcc8; num11++)
								{
									for (int num12 = num4 + 1; num12 < c6ae83731ca5ed1d50b72b5b5953792b6; num12++)
									{
										array2[num12] += array[num11] * matrixF[num12, num11];
									}
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
								for (int num13 = num4 + 1; num13 < c8704416eb2ee8a512db38b95b947dcc8; num13++)
								{
									float num14 = (0f - array[num13]) / array[num4 + 1];
									for (int num15 = num4 + 1; num15 < c6ae83731ca5ed1d50b72b5b5953792b6; num15++)
									{
										matrixF[num15, num13] += num14 * array2[num15];
									}
									while (true)
									{
										switch (6)
										{
										case 0:
											break;
										default:
											goto end_IL_0624;
										}
										continue;
										end_IL_0624:
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
									break;
								}
							}
						}
						for (int num16 = num4 + 1; num16 < c8704416eb2ee8a512db38b95b947dcc8; num16++)
						{
							ccbdf70e0565d415a0cffd34ca03ac5e3[num16, num4] = array[num16];
						}
					}
					num4++;
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
				int num17 = Math.Min(c8704416eb2ee8a512db38b95b947dcc8, c6ae83731ca5ed1d50b72b5b5953792b6 + 1);
				if (num2 < c8704416eb2ee8a512db38b95b947dcc8)
				{
					c7e5b75e3e3ecaee327bd4f034b73fe79[num2] = matrixF[num2, num2];
				}
				if (c6ae83731ca5ed1d50b72b5b5953792b6 < num17)
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
					c7e5b75e3e3ecaee327bd4f034b73fe79[num17 - 1] = 0f;
				}
				if (num3 + 1 < num17)
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
					array[num3] = matrixF[num3, num17 - 1];
				}
				array[num17 - 1] = 0f;
				for (int num18 = num2; num18 < num; num18++)
				{
					for (int num19 = 0; num19 < c6ae83731ca5ed1d50b72b5b5953792b6; num19++)
					{
						c57880ecc022a6861cd4d2d6e60986133[num19, num18] = 0f;
					}
					while (true)
					{
						switch (6)
						{
						case 0:
							break;
						default:
							goto end_IL_074f;
						}
						continue;
						end_IL_074f:
						break;
					}
					c57880ecc022a6861cd4d2d6e60986133[num18, num18] = 1f;
				}
				for (int num20 = num2 - 1; num20 >= 0; num20--)
				{
					if (c7e5b75e3e3ecaee327bd4f034b73fe79[num20] != 0f)
					{
						for (int num21 = num20 + 1; num21 < num; num21++)
						{
							float num22 = 0f;
							for (int num23 = num20; num23 < c6ae83731ca5ed1d50b72b5b5953792b6; num23++)
							{
								num22 += c57880ecc022a6861cd4d2d6e60986133[num23, num20] * c57880ecc022a6861cd4d2d6e60986133[num23, num21];
							}
							num22 = (0f - num22) / c57880ecc022a6861cd4d2d6e60986133[num20, num20];
							for (int num24 = num20; num24 < c6ae83731ca5ed1d50b72b5b5953792b6; num24++)
							{
								c57880ecc022a6861cd4d2d6e60986133[num24, num21] += num22 * c57880ecc022a6861cd4d2d6e60986133[num24, num20];
							}
							while (true)
							{
								switch (7)
								{
								case 0:
									break;
								default:
									goto end_IL_0851;
								}
								continue;
								end_IL_0851:
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
						for (int num25 = num20; num25 < c6ae83731ca5ed1d50b72b5b5953792b6; num25++)
						{
							c57880ecc022a6861cd4d2d6e60986133[num25, num20] = 0f - c57880ecc022a6861cd4d2d6e60986133[num25, num20];
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
						c57880ecc022a6861cd4d2d6e60986133[num20, num20] = 1f + c57880ecc022a6861cd4d2d6e60986133[num20, num20];
						for (int num26 = 0; num26 < num20 - 1; num26++)
						{
							c57880ecc022a6861cd4d2d6e60986133[num26, num20] = 0f;
						}
					}
					else
					{
						for (int num27 = 0; num27 < c6ae83731ca5ed1d50b72b5b5953792b6; num27++)
						{
							c57880ecc022a6861cd4d2d6e60986133[num27, num20] = 0f;
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
						c57880ecc022a6861cd4d2d6e60986133[num20, num20] = 1f;
					}
				}
				for (int num28 = c8704416eb2ee8a512db38b95b947dcc8 - 1; num28 >= 0; num28--)
				{
					if ((num28 < num3) & ((double)array[num28] != 0.0))
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
						int num29 = num28 + 1;
						while (num29 < num)
						{
							float num30 = 0f;
							for (int num31 = num28 + 1; num31 < c8704416eb2ee8a512db38b95b947dcc8; num31++)
							{
								num30 += ccbdf70e0565d415a0cffd34ca03ac5e3[num31, num28] * ccbdf70e0565d415a0cffd34ca03ac5e3[num31, num29];
							}
							while (true)
							{
								switch (7)
								{
								case 0:
									continue;
								}
								num30 = (0f - num30) / ccbdf70e0565d415a0cffd34ca03ac5e3[num28 + 1, num28];
								for (int num32 = num28 + 1; num32 < c8704416eb2ee8a512db38b95b947dcc8; num32++)
								{
									ccbdf70e0565d415a0cffd34ca03ac5e3[num32, num29] += num30 * ccbdf70e0565d415a0cffd34ca03ac5e3[num32, num28];
								}
								num29++;
								break;
							}
						}
					}
					for (int num33 = 0; num33 < c8704416eb2ee8a512db38b95b947dcc8; num33++)
					{
						ccbdf70e0565d415a0cffd34ca03ac5e3[num33, num28] = 0f;
					}
					while (true)
					{
						switch (1)
						{
						case 0:
							break;
						default:
							goto end_IL_0a88;
						}
						continue;
						end_IL_0a88:
						break;
					}
					ccbdf70e0565d415a0cffd34ca03ac5e3[num28, num28] = 1f;
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					int num34 = num17 - 1;
					int num35 = 0;
					double num36 = (float)Math.Pow(2.0, -23.0);
					double num37 = (float)Math.Pow(2.0, -100.0);
					while (num17 > 0)
					{
						int num38 = num17 - 2;
						while (true)
						{
							if (num38 >= -1)
							{
								if (num38 == -1)
								{
									break;
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
								if ((double)Math.Abs(array[num38]) <= num37 + num36 * (double)(Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num38]) + Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num38 + 1])))
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
									array[num38] = 0f;
									break;
								}
								num38--;
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
							break;
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
									float num41 = ((num40 != num17) ? Math.Abs(array[num40]) : 0f) + ((num40 != num38 + 1) ? Math.Abs(array[num40 - 1]) : 0f);
									if ((double)Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num40]) <= num37 + num36 * (double)num41)
									{
										c7e5b75e3e3ecaee327bd4f034b73fe79[num40] = 0f;
										break;
									}
									num40--;
									continue;
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
								break;
							}
							if (num40 == num38)
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
							float num45 = array[num17 - 2];
							array[num17 - 2] = 0f;
							for (int num46 = num17 - 2; num46 >= num38; num46--)
							{
								float num47 = MathHelper.Hypotenuse(c7e5b75e3e3ecaee327bd4f034b73fe79[num46], num45);
								float num48 = c7e5b75e3e3ecaee327bd4f034b73fe79[num46] / num47;
								float num49 = num45 / num47;
								c7e5b75e3e3ecaee327bd4f034b73fe79[num46] = num47;
								if (num46 != num38)
								{
									num45 = (0f - num49) * array[num46 - 1];
									array[num46 - 1] = num48 * array[num46 - 1];
								}
								for (int num50 = 0; num50 < c8704416eb2ee8a512db38b95b947dcc8; num50++)
								{
									num47 = num48 * ccbdf70e0565d415a0cffd34ca03ac5e3[num50, num46] + num49 * ccbdf70e0565d415a0cffd34ca03ac5e3[num50, num17 - 1];
									ccbdf70e0565d415a0cffd34ca03ac5e3[num50, num17 - 1] = (0f - num49) * ccbdf70e0565d415a0cffd34ca03ac5e3[num50, num46] + num48 * ccbdf70e0565d415a0cffd34ca03ac5e3[num50, num17 - 1];
									ccbdf70e0565d415a0cffd34ca03ac5e3[num50, num46] = num47;
								}
								while (true)
								{
									switch (4)
									{
									case 0:
										break;
									default:
										goto end_IL_0d8c;
									}
									continue;
									end_IL_0d8c:
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
							break;
						}
						case 2:
						{
							float num51 = array[num38 - 1];
							array[num38 - 1] = 0f;
							for (int num52 = num38; num52 < num17; num52++)
							{
								float num53 = MathHelper.Hypotenuse(c7e5b75e3e3ecaee327bd4f034b73fe79[num52], num51);
								float num54 = c7e5b75e3e3ecaee327bd4f034b73fe79[num52] / num53;
								float num55 = num51 / num53;
								c7e5b75e3e3ecaee327bd4f034b73fe79[num52] = num53;
								num51 = (0f - num55) * array[num52];
								array[num52] = num54 * array[num52];
								for (int num56 = 0; num56 < c6ae83731ca5ed1d50b72b5b5953792b6; num56++)
								{
									num53 = num54 * c57880ecc022a6861cd4d2d6e60986133[num56, num52] + num55 * c57880ecc022a6861cd4d2d6e60986133[num56, num38 - 1];
									c57880ecc022a6861cd4d2d6e60986133[num56, num38 - 1] = (0f - num55) * c57880ecc022a6861cd4d2d6e60986133[num56, num52] + num54 * c57880ecc022a6861cd4d2d6e60986133[num56, num38 - 1];
									c57880ecc022a6861cd4d2d6e60986133[num56, num52] = num53;
								}
								while (true)
								{
									switch (1)
									{
									case 0:
										break;
									default:
										goto end_IL_0eb6;
									}
									continue;
									end_IL_0eb6:
									break;
								}
							}
							break;
						}
						case 3:
						{
							float num57 = Math.Max(Math.Max(Math.Max(Math.Max(Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num17 - 1]), Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num17 - 2])), Math.Abs(array[num17 - 2])), Math.Abs(c7e5b75e3e3ecaee327bd4f034b73fe79[num38])), Math.Abs(array[num38]));
							float num58 = c7e5b75e3e3ecaee327bd4f034b73fe79[num17 - 1] / num57;
							float num59 = c7e5b75e3e3ecaee327bd4f034b73fe79[num17 - 2] / num57;
							float num60 = array[num17 - 2] / num57;
							float num61 = c7e5b75e3e3ecaee327bd4f034b73fe79[num38] / num57;
							float num62 = array[num38] / num57;
							float num63 = ((num59 + num58) * (num59 - num58) + num60 * num60) / 2f;
							float num64 = num58 * num60 * (num58 * num60);
							float num65 = 0f;
							if (((double)num63 != 0.0) | ((double)num64 != 0.0))
							{
								num65 = (float)Math.Sqrt(num63 * num63 + num64);
								if ((double)num63 < 0.0)
								{
									num65 = 0f - num65;
								}
								num65 = num64 / (num63 + num65);
							}
							float num66 = (num61 + num58) * (num61 - num58) + num65;
							float num67 = num61 * num62;
							for (int num68 = num38; num68 < num17 - 1; num68++)
							{
								float num69 = MathHelper.Hypotenuse(num66, num67);
								float num70 = num66 / num69;
								float num71 = num67 / num69;
								if (num68 != num38)
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
									array[num68 - 1] = num69;
								}
								num66 = num70 * c7e5b75e3e3ecaee327bd4f034b73fe79[num68] + num71 * array[num68];
								array[num68] = num70 * array[num68] - num71 * c7e5b75e3e3ecaee327bd4f034b73fe79[num68];
								num67 = num71 * c7e5b75e3e3ecaee327bd4f034b73fe79[num68 + 1];
								c7e5b75e3e3ecaee327bd4f034b73fe79[num68 + 1] = num70 * c7e5b75e3e3ecaee327bd4f034b73fe79[num68 + 1];
								for (int num72 = 0; num72 < c8704416eb2ee8a512db38b95b947dcc8; num72++)
								{
									num69 = num70 * ccbdf70e0565d415a0cffd34ca03ac5e3[num72, num68] + num71 * ccbdf70e0565d415a0cffd34ca03ac5e3[num72, num68 + 1];
									ccbdf70e0565d415a0cffd34ca03ac5e3[num72, num68 + 1] = (0f - num71) * ccbdf70e0565d415a0cffd34ca03ac5e3[num72, num68] + num70 * ccbdf70e0565d415a0cffd34ca03ac5e3[num72, num68 + 1];
									ccbdf70e0565d415a0cffd34ca03ac5e3[num72, num68] = num69;
								}
								num69 = MathHelper.Hypotenuse(num66, num67);
								num70 = num66 / num69;
								num71 = num67 / num69;
								c7e5b75e3e3ecaee327bd4f034b73fe79[num68] = num69;
								num66 = num70 * array[num68] + num71 * c7e5b75e3e3ecaee327bd4f034b73fe79[num68 + 1];
								c7e5b75e3e3ecaee327bd4f034b73fe79[num68 + 1] = (0f - num71) * array[num68] + num70 * c7e5b75e3e3ecaee327bd4f034b73fe79[num68 + 1];
								num67 = num71 * array[num68 + 1];
								array[num68 + 1] = num70 * array[num68 + 1];
								if (num68 >= c6ae83731ca5ed1d50b72b5b5953792b6 - 1)
								{
									continue;
								}
								for (int num73 = 0; num73 < c6ae83731ca5ed1d50b72b5b5953792b6; num73++)
								{
									num69 = num70 * c57880ecc022a6861cd4d2d6e60986133[num73, num68] + num71 * c57880ecc022a6861cd4d2d6e60986133[num73, num68 + 1];
									c57880ecc022a6861cd4d2d6e60986133[num73, num68 + 1] = (0f - num71) * c57880ecc022a6861cd4d2d6e60986133[num73, num68] + num70 * c57880ecc022a6861cd4d2d6e60986133[num73, num68 + 1];
									c57880ecc022a6861cd4d2d6e60986133[num73, num68] = num69;
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
							array[num17 - 2] = num66;
							num35++;
							break;
						}
						case 4:
							if ((double)c7e5b75e3e3ecaee327bd4f034b73fe79[num38] <= 0.0)
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
								VectorF vectorF = c7e5b75e3e3ecaee327bd4f034b73fe79;
								int index = num38;
								float value;
								if (!((double)c7e5b75e3e3ecaee327bd4f034b73fe79[num38] < 0.0))
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
									value = 0f;
								}
								else
								{
									value = 0f - c7e5b75e3e3ecaee327bd4f034b73fe79[num38];
								}
								vectorF[index] = value;
								for (int num42 = 0; num42 <= num34; num42++)
								{
									ccbdf70e0565d415a0cffd34ca03ac5e3[num42, num38] = 0f - ccbdf70e0565d415a0cffd34ca03ac5e3[num42, num38];
								}
							}
							for (; num38 < num34 && !(c7e5b75e3e3ecaee327bd4f034b73fe79[num38] >= c7e5b75e3e3ecaee327bd4f034b73fe79[num38 + 1]); num38++)
							{
								float value2 = c7e5b75e3e3ecaee327bd4f034b73fe79[num38];
								c7e5b75e3e3ecaee327bd4f034b73fe79[num38] = c7e5b75e3e3ecaee327bd4f034b73fe79[num38 + 1];
								c7e5b75e3e3ecaee327bd4f034b73fe79[num38 + 1] = value2;
								if (num38 < c8704416eb2ee8a512db38b95b947dcc8 - 1)
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
									for (int num43 = 0; num43 < c8704416eb2ee8a512db38b95b947dcc8; num43++)
									{
										value2 = ccbdf70e0565d415a0cffd34ca03ac5e3[num43, num38 + 1];
										ccbdf70e0565d415a0cffd34ca03ac5e3[num43, num38 + 1] = ccbdf70e0565d415a0cffd34ca03ac5e3[num43, num38];
										ccbdf70e0565d415a0cffd34ca03ac5e3[num43, num38] = value2;
									}
								}
								if (num38 >= c6ae83731ca5ed1d50b72b5b5953792b6 - 1)
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
									break;
								}
								for (int num44 = 0; num44 < c6ae83731ca5ed1d50b72b5b5953792b6; num44++)
								{
									value2 = c57880ecc022a6861cd4d2d6e60986133[num44, num38 + 1];
									c57880ecc022a6861cd4d2d6e60986133[num44, num38 + 1] = c57880ecc022a6861cd4d2d6e60986133[num44, num38];
									c57880ecc022a6861cd4d2d6e60986133[num44, num38] = value2;
								}
							}
							num35 = 0;
							num17--;
							break;
						}
					}
					return;
				}
			}
		}
	}
}
