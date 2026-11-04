using System;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Computes the eigenvalues and eigenvectors of a real square matrix A (double-precision).
///       </summary>
public class EigenvalueDecompositionD
{
	private readonly MatrixD ccbdf70e0565d415a0cffd34ca03ac5e3;

	private readonly VectorD ce8aac9f53c7f93f91b33957b93c750e8;

	private MatrixD cdca57945f0c1c77be3bfc1df8a1df4c3;

	private readonly VectorD ccce78d1e25a7861e22a5dfbdd3b9533d;

	private readonly int c8704416eb2ee8a512db38b95b947dcc8;

	private readonly bool ce8913a3c974007c03eb12f43a3ba8dfb;

	/// <summary>
	///       Gets the vector of the imaginary parts of the eigenvalues. 
	///       </summary>
	public VectorD ImaginaryEigenvalues => ccce78d1e25a7861e22a5dfbdd3b9533d;

	/// <summary>
	///       Gets the block diagonal eigenvalue matrix D. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixD D
	{
		get
		{
			if (cdca57945f0c1c77be3bfc1df8a1df4c3 != null)
			{
				return cdca57945f0c1c77be3bfc1df8a1df4c3;
			}
			cdca57945f0c1c77be3bfc1df8a1df4c3 = new MatrixD(c8704416eb2ee8a512db38b95b947dcc8, c8704416eb2ee8a512db38b95b947dcc8);
			for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
			{
				cdca57945f0c1c77be3bfc1df8a1df4c3[i, i] = ce8aac9f53c7f93f91b33957b93c750e8[i];
				if (ccce78d1e25a7861e22a5dfbdd3b9533d[i] > 0.0)
				{
					cdca57945f0c1c77be3bfc1df8a1df4c3[i, i + 1] = ccce78d1e25a7861e22a5dfbdd3b9533d[i];
				}
				else if (ccce78d1e25a7861e22a5dfbdd3b9533d[i] < 0.0)
				{
					cdca57945f0c1c77be3bfc1df8a1df4c3[i, i - 1] = ccce78d1e25a7861e22a5dfbdd3b9533d[i];
				}
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
					__ldtoken(EigenvalueDecompositionD.get_D());
				}
				return cdca57945f0c1c77be3bfc1df8a1df4c3;
			}
		}
	}

	/// <summary>
	///       Gets the eigenvector matrix V. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixD V => ccbdf70e0565d415a0cffd34ca03ac5e3;

	/// <summary>
	///       Gets the vector of the real parts of the eigenvalues (the diagonal of D). 
	///       </summary>
	public VectorD RealEigenvalues => ce8aac9f53c7f93f91b33957b93c750e8;

	/// <summary>
	///       Creates the eigenvalue decomposition of the given matrix.
	///       </summary>
	/// <param name="matrixA">The square matrix A.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrixA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="matrixA" /> is non-square (rectangular).
	///       </exception>
	public EigenvalueDecompositionD(MatrixD matrixA)
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
						__ldtoken(EigenvalueDecompositionD._002Ector(MatrixD));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
				}
			}
		}
		if (!matrixA.IsSquare)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4658), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
		}
		c8704416eb2ee8a512db38b95b947dcc8 = matrixA.NumberOfColumns;
		ce8aac9f53c7f93f91b33957b93c750e8 = new VectorD(c8704416eb2ee8a512db38b95b947dcc8);
		ccce78d1e25a7861e22a5dfbdd3b9533d = new VectorD(c8704416eb2ee8a512db38b95b947dcc8);
		ce8913a3c974007c03eb12f43a3ba8dfb = matrixA.IsSymmetric;
		if (ce8913a3c974007c03eb12f43a3ba8dfb)
		{
			ccbdf70e0565d415a0cffd34ca03ac5e3 = matrixA.Clone();
			c30f61ef2201a97c418b4698ca87d10e0();
			c994d424a781058095e3d76f0436d959b();
			return;
		}
		ccbdf70e0565d415a0cffd34ca03ac5e3 = new MatrixD(c8704416eb2ee8a512db38b95b947dcc8, c8704416eb2ee8a512db38b95b947dcc8);
		for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
		{
			for (int j = 0; j < c8704416eb2ee8a512db38b95b947dcc8; j++)
			{
				if (Numeric.IsNaN(matrixA[i, j]))
				{
					ccce78d1e25a7861e22a5dfbdd3b9533d.Set(double.NaN);
					ccbdf70e0565d415a0cffd34ca03ac5e3.Set(double.NaN);
					ce8aac9f53c7f93f91b33957b93c750e8.Set(double.NaN);
					return;
				}
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					goto end_IL_0135;
				}
				continue;
				end_IL_0135:
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
			MatrixD c4218a936b59a793594ce8a8da87b52c = matrixA.Clone();
			double[] c8dcba10f15071a699619713aa7f64afb = new double[c8704416eb2ee8a512db38b95b947dcc8];
			c529c977bc9d1176a4031bdd1ad5ce7f3(c4218a936b59a793594ce8a8da87b52c, c8dcba10f15071a699619713aa7f64afb);
			c929277d6335217b2ed08823e6d898829(c4218a936b59a793594ce8a8da87b52c);
			return;
		}
	}

	private void c30f61ef2201a97c418b4698ca87d10e0()
	{
		for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
		{
			ce8aac9f53c7f93f91b33957b93c750e8[i] = ccbdf70e0565d415a0cffd34ca03ac5e3[c8704416eb2ee8a512db38b95b947dcc8 - 1, i];
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
				__ldtoken(EigenvalueDecompositionD.c30f61ef2201a97c418b4698ca87d10e0());
			}
			int num = c8704416eb2ee8a512db38b95b947dcc8 - 1;
			while (num > 0)
			{
				double num2 = 0.0;
				double num3 = 0.0;
				for (int j = 0; j < num; j++)
				{
					num2 += Math.Abs(ce8aac9f53c7f93f91b33957b93c750e8[j]);
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					if (num2 == 0.0)
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
						ccce78d1e25a7861e22a5dfbdd3b9533d[num] = ce8aac9f53c7f93f91b33957b93c750e8[num - 1];
						for (int k = 0; k < num; k++)
						{
							ce8aac9f53c7f93f91b33957b93c750e8[k] = ccbdf70e0565d415a0cffd34ca03ac5e3[num - 1, k];
							ccbdf70e0565d415a0cffd34ca03ac5e3[num, k] = 0.0;
							ccbdf70e0565d415a0cffd34ca03ac5e3[k, num] = 0.0;
						}
					}
					else
					{
						for (int l = 0; l < num; l++)
						{
							ce8aac9f53c7f93f91b33957b93c750e8[l] /= num2;
							num3 += ce8aac9f53c7f93f91b33957b93c750e8[l] * ce8aac9f53c7f93f91b33957b93c750e8[l];
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
						double num4 = ce8aac9f53c7f93f91b33957b93c750e8[num - 1];
						double num5 = Math.Sqrt(num3);
						if (num4 > 0.0)
						{
							num5 = 0.0 - num5;
						}
						ccce78d1e25a7861e22a5dfbdd3b9533d[num] = num2 * num5;
						num3 -= num4 * num5;
						ce8aac9f53c7f93f91b33957b93c750e8[num - 1] = num4 - num5;
						for (int m = 0; m < num; m++)
						{
							ccce78d1e25a7861e22a5dfbdd3b9533d[m] = 0.0;
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
						for (int n = 0; n < num; n++)
						{
							num4 = ce8aac9f53c7f93f91b33957b93c750e8[n];
							ccbdf70e0565d415a0cffd34ca03ac5e3[n, num] = num4;
							num5 = ccce78d1e25a7861e22a5dfbdd3b9533d[n] + ccbdf70e0565d415a0cffd34ca03ac5e3[n, n] * num4;
							for (int num6 = n + 1; num6 <= num - 1; num6++)
							{
								num5 += ccbdf70e0565d415a0cffd34ca03ac5e3[num6, n] * ce8aac9f53c7f93f91b33957b93c750e8[num6];
								ccce78d1e25a7861e22a5dfbdd3b9533d[num6] += ccbdf70e0565d415a0cffd34ca03ac5e3[num6, n] * num4;
							}
							ccce78d1e25a7861e22a5dfbdd3b9533d[n] = num5;
						}
						num4 = 0.0;
						for (int num7 = 0; num7 < num; num7++)
						{
							ccce78d1e25a7861e22a5dfbdd3b9533d[num7] /= num3;
							num4 += ccce78d1e25a7861e22a5dfbdd3b9533d[num7] * ce8aac9f53c7f93f91b33957b93c750e8[num7];
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
						double num8 = num4 / (num3 + num3);
						for (int num9 = 0; num9 < num; num9++)
						{
							ccce78d1e25a7861e22a5dfbdd3b9533d[num9] -= num8 * ce8aac9f53c7f93f91b33957b93c750e8[num9];
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
						for (int num10 = 0; num10 < num; num10++)
						{
							num4 = ce8aac9f53c7f93f91b33957b93c750e8[num10];
							num5 = ccce78d1e25a7861e22a5dfbdd3b9533d[num10];
							for (int num11 = num10; num11 <= num - 1; num11++)
							{
								ccbdf70e0565d415a0cffd34ca03ac5e3[num11, num10] -= num4 * ccce78d1e25a7861e22a5dfbdd3b9533d[num11] + num5 * ce8aac9f53c7f93f91b33957b93c750e8[num11];
							}
							ce8aac9f53c7f93f91b33957b93c750e8[num10] = ccbdf70e0565d415a0cffd34ca03ac5e3[num - 1, num10];
							ccbdf70e0565d415a0cffd34ca03ac5e3[num, num10] = 0.0;
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
					ce8aac9f53c7f93f91b33957b93c750e8[num] = num3;
					num--;
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
				for (int num12 = 0; num12 < c8704416eb2ee8a512db38b95b947dcc8 - 1; num12++)
				{
					ccbdf70e0565d415a0cffd34ca03ac5e3[c8704416eb2ee8a512db38b95b947dcc8 - 1, num12] = ccbdf70e0565d415a0cffd34ca03ac5e3[num12, num12];
					ccbdf70e0565d415a0cffd34ca03ac5e3[num12, num12] = 1.0;
					double num13 = ce8aac9f53c7f93f91b33957b93c750e8[num12 + 1];
					if (num13 != 0.0)
					{
						for (int num14 = 0; num14 <= num12; num14++)
						{
							ce8aac9f53c7f93f91b33957b93c750e8[num14] = ccbdf70e0565d415a0cffd34ca03ac5e3[num14, num12 + 1] / num13;
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
						int num15 = 0;
						while (num15 <= num12)
						{
							double num16 = 0.0;
							for (int num17 = 0; num17 <= num12; num17++)
							{
								num16 += ccbdf70e0565d415a0cffd34ca03ac5e3[num17, num12 + 1] * ccbdf70e0565d415a0cffd34ca03ac5e3[num17, num15];
							}
							while (true)
							{
								switch (5)
								{
								case 0:
									continue;
								}
								for (int num18 = 0; num18 <= num12; num18++)
								{
									ccbdf70e0565d415a0cffd34ca03ac5e3[num18, num15] -= num16 * ce8aac9f53c7f93f91b33957b93c750e8[num18];
								}
								while (true)
								{
									switch (4)
									{
									case 0:
										break;
									default:
										goto end_IL_05d1;
									}
									continue;
									end_IL_05d1:
									break;
								}
								num15++;
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
					for (int num19 = 0; num19 <= num12; num19++)
					{
						ccbdf70e0565d415a0cffd34ca03ac5e3[num19, num12 + 1] = 0.0;
					}
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					for (int num20 = 0; num20 < c8704416eb2ee8a512db38b95b947dcc8; num20++)
					{
						ce8aac9f53c7f93f91b33957b93c750e8[num20] = ccbdf70e0565d415a0cffd34ca03ac5e3[c8704416eb2ee8a512db38b95b947dcc8 - 1, num20];
						ccbdf70e0565d415a0cffd34ca03ac5e3[c8704416eb2ee8a512db38b95b947dcc8 - 1, num20] = 0.0;
					}
					ccbdf70e0565d415a0cffd34ca03ac5e3[c8704416eb2ee8a512db38b95b947dcc8 - 1, c8704416eb2ee8a512db38b95b947dcc8 - 1] = 1.0;
					ccce78d1e25a7861e22a5dfbdd3b9533d[0] = 0.0;
					return;
				}
			}
		}
	}

	private void c994d424a781058095e3d76f0436d959b()
	{
		for (int i = 1; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
		{
			ccce78d1e25a7861e22a5dfbdd3b9533d[i - 1] = ccce78d1e25a7861e22a5dfbdd3b9533d[i];
		}
		ccce78d1e25a7861e22a5dfbdd3b9533d[c8704416eb2ee8a512db38b95b947dcc8 - 1] = 0.0;
		double num = 0.0;
		double num2 = 0.0;
		double num3 = Math.Pow(2.0, -52.0);
		for (int j = 0; j < c8704416eb2ee8a512db38b95b947dcc8; j++)
		{
			num2 = Math.Max(num2, Math.Abs(ce8aac9f53c7f93f91b33957b93c750e8[j]) + Math.Abs(ccce78d1e25a7861e22a5dfbdd3b9533d[j]));
			int num4 = j;
			while (true)
			{
				if (num4 < c8704416eb2ee8a512db38b95b947dcc8)
				{
					if (Math.Abs(ccce78d1e25a7861e22a5dfbdd3b9533d[num4]) <= num3 * num2)
					{
						break;
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
							__ldtoken(EigenvalueDecompositionD.c994d424a781058095e3d76f0436d959b());
						}
						num4++;
						break;
					}
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
				break;
			}
			if (num4 > j)
			{
				int num5 = 0;
				while (true)
				{
					num5++;
					double num6 = ce8aac9f53c7f93f91b33957b93c750e8[j];
					double num7 = (ce8aac9f53c7f93f91b33957b93c750e8[j + 1] - num6) / (2.0 * ccce78d1e25a7861e22a5dfbdd3b9533d[j]);
					double num8 = MathHelper.Hypotenuse(num7, 1.0);
					if (num7 < 0.0)
					{
						num8 = 0.0 - num8;
					}
					ce8aac9f53c7f93f91b33957b93c750e8[j] = ccce78d1e25a7861e22a5dfbdd3b9533d[j] / (num7 + num8);
					ce8aac9f53c7f93f91b33957b93c750e8[j + 1] = ccce78d1e25a7861e22a5dfbdd3b9533d[j] * (num7 + num8);
					double num9 = ce8aac9f53c7f93f91b33957b93c750e8[j + 1];
					double num10 = num6 - ce8aac9f53c7f93f91b33957b93c750e8[j];
					for (int k = j + 2; k < c8704416eb2ee8a512db38b95b947dcc8; k++)
					{
						ce8aac9f53c7f93f91b33957b93c750e8[k] -= num10;
					}
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						num += num10;
						num7 = ce8aac9f53c7f93f91b33957b93c750e8[num4];
						double num11 = 1.0;
						double num12 = num11;
						double num13 = num11;
						double num14 = ccce78d1e25a7861e22a5dfbdd3b9533d[j + 1];
						double num15 = 0.0;
						double num16 = 0.0;
						for (int num17 = num4 - 1; num17 >= j; num17--)
						{
							num13 = num12;
							num12 = num11;
							num16 = num15;
							num6 = num11 * ccce78d1e25a7861e22a5dfbdd3b9533d[num17];
							num10 = num11 * num7;
							num8 = MathHelper.Hypotenuse(num7, ccce78d1e25a7861e22a5dfbdd3b9533d[num17]);
							ccce78d1e25a7861e22a5dfbdd3b9533d[num17 + 1] = num15 * num8;
							num15 = ccce78d1e25a7861e22a5dfbdd3b9533d[num17] / num8;
							num11 = num7 / num8;
							num7 = num11 * ce8aac9f53c7f93f91b33957b93c750e8[num17] - num15 * num6;
							ce8aac9f53c7f93f91b33957b93c750e8[num17 + 1] = num10 + num15 * (num11 * num6 + num15 * ce8aac9f53c7f93f91b33957b93c750e8[num17]);
							for (int l = 0; l < c8704416eb2ee8a512db38b95b947dcc8; l++)
							{
								num10 = ccbdf70e0565d415a0cffd34ca03ac5e3[l, num17 + 1];
								ccbdf70e0565d415a0cffd34ca03ac5e3[l, num17 + 1] = num15 * ccbdf70e0565d415a0cffd34ca03ac5e3[l, num17] + num11 * num10;
								ccbdf70e0565d415a0cffd34ca03ac5e3[l, num17] = num11 * ccbdf70e0565d415a0cffd34ca03ac5e3[l, num17] - num15 * num10;
							}
						}
						while (true)
						{
							switch (3)
							{
							case 0:
								break;
							default:
								goto end_IL_03b3;
							}
							continue;
							end_IL_03b3:
							break;
						}
						num7 = (0.0 - num15) * num16 * num13 * num14 * ccce78d1e25a7861e22a5dfbdd3b9533d[j] / num9;
						ccce78d1e25a7861e22a5dfbdd3b9533d[j] = num15 * num7;
						ce8aac9f53c7f93f91b33957b93c750e8[j] = num11 * num7;
						if (Math.Abs(ccce78d1e25a7861e22a5dfbdd3b9533d[j]) > num3 * num2)
						{
							break;
						}
						goto end_IL_0103;
					}
					continue;
					end_IL_0103:
					break;
				}
			}
			ce8aac9f53c7f93f91b33957b93c750e8[j] += num;
			ccce78d1e25a7861e22a5dfbdd3b9533d[j] = 0.0;
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			int num18 = 0;
			while (num18 < c8704416eb2ee8a512db38b95b947dcc8 - 1)
			{
				int num19 = num18;
				double num20 = ce8aac9f53c7f93f91b33957b93c750e8[num18];
				for (int m = num18 + 1; m < c8704416eb2ee8a512db38b95b947dcc8; m++)
				{
					if (ce8aac9f53c7f93f91b33957b93c750e8[m] < num20)
					{
						num19 = m;
						num20 = ce8aac9f53c7f93f91b33957b93c750e8[m];
					}
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					if (num19 != num18)
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
						ce8aac9f53c7f93f91b33957b93c750e8[num19] = ce8aac9f53c7f93f91b33957b93c750e8[num18];
						ce8aac9f53c7f93f91b33957b93c750e8[num18] = num20;
						for (int n = 0; n < c8704416eb2ee8a512db38b95b947dcc8; n++)
						{
							num20 = ccbdf70e0565d415a0cffd34ca03ac5e3[n, num18];
							ccbdf70e0565d415a0cffd34ca03ac5e3[n, num18] = ccbdf70e0565d415a0cffd34ca03ac5e3[n, num19];
							ccbdf70e0565d415a0cffd34ca03ac5e3[n, num19] = num20;
						}
					}
					num18++;
					break;
				}
			}
			return;
		}
	}

	private void c529c977bc9d1176a4031bdd1ad5ce7f3(MatrixD c4218a936b59a793594ce8a8da87b52c4, double[] c8dcba10f15071a699619713aa7f64afb)
	{
		int num = 0;
		int num2 = c8704416eb2ee8a512db38b95b947dcc8 - 1;
		for (int i = num + 1; i <= num2 - 1; i++)
		{
			double num3 = 0.0;
			for (int j = i; j <= num2; j++)
			{
				num3 += Math.Abs(c4218a936b59a793594ce8a8da87b52c4[j, i - 1]);
			}
			if (num3 == 0.0)
			{
				continue;
			}
			double num4 = 0.0;
			for (int num5 = num2; num5 >= i; num5--)
			{
				c8dcba10f15071a699619713aa7f64afb[num5] = c4218a936b59a793594ce8a8da87b52c4[num5, i - 1] / num3;
				num4 += c8dcba10f15071a699619713aa7f64afb[num5] * c8dcba10f15071a699619713aa7f64afb[num5];
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
			if (1 == 0)
			{
				__ldtoken(EigenvalueDecompositionD.c529c977bc9d1176a4031bdd1ad5ce7f3(MatrixD, double[]));
			}
			double num6 = Math.Sqrt(num4);
			if (c8dcba10f15071a699619713aa7f64afb[i] > 0.0)
			{
				num6 = 0.0 - num6;
			}
			num4 -= c8dcba10f15071a699619713aa7f64afb[i] * num6;
			c8dcba10f15071a699619713aa7f64afb[i] -= num6;
			for (int k = i; k < c8704416eb2ee8a512db38b95b947dcc8; k++)
			{
				double num7 = 0.0;
				for (int num8 = num2; num8 >= i; num8--)
				{
					num7 += c8dcba10f15071a699619713aa7f64afb[num8] * c4218a936b59a793594ce8a8da87b52c4[num8, k];
				}
				num7 /= num4;
				for (int l = i; l <= num2; l++)
				{
					c4218a936b59a793594ce8a8da87b52c4[l, k] -= num7 * c8dcba10f15071a699619713aa7f64afb[l];
				}
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
			int num9 = 0;
			while (num9 <= num2)
			{
				double num10 = 0.0;
				for (int num11 = num2; num11 >= i; num11--)
				{
					num10 += c8dcba10f15071a699619713aa7f64afb[num11] * c4218a936b59a793594ce8a8da87b52c4[num9, num11];
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					num10 /= num4;
					for (int m = i; m <= num2; m++)
					{
						c4218a936b59a793594ce8a8da87b52c4[num9, m] -= num10 * c8dcba10f15071a699619713aa7f64afb[m];
					}
					while (true)
					{
						switch (2)
						{
						case 0:
							break;
						default:
							goto end_IL_01da;
						}
						continue;
						end_IL_01da:
						break;
					}
					num9++;
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
			c8dcba10f15071a699619713aa7f64afb[i] = num3 * c8dcba10f15071a699619713aa7f64afb[i];
			c4218a936b59a793594ce8a8da87b52c4[i, i - 1] = num3 * num6;
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			for (int n = 0; n < c8704416eb2ee8a512db38b95b947dcc8; n++)
			{
				for (int num12 = 0; num12 < c8704416eb2ee8a512db38b95b947dcc8; num12++)
				{
					MatrixD matrixD = ccbdf70e0565d415a0cffd34ca03ac5e3;
					int row = n;
					int column = num12;
					int num13;
					if (n != num12)
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
						num13 = 0;
					}
					else
					{
						num13 = 1;
					}
					matrixD[row, column] = num13;
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						goto end_IL_0267;
					}
					continue;
					end_IL_0267:
					break;
				}
			}
			for (int num14 = num2 - 1; num14 >= num + 1; num14--)
			{
				if (c4218a936b59a793594ce8a8da87b52c4[num14, num14 - 1] != 0.0)
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
					for (int num15 = num14 + 1; num15 <= num2; num15++)
					{
						c8dcba10f15071a699619713aa7f64afb[num15] = c4218a936b59a793594ce8a8da87b52c4[num15, num14 - 1];
					}
					int num16 = num14;
					while (num16 <= num2)
					{
						double num17 = 0.0;
						for (int num18 = num14; num18 <= num2; num18++)
						{
							num17 += c8dcba10f15071a699619713aa7f64afb[num18] * ccbdf70e0565d415a0cffd34ca03ac5e3[num18, num16];
						}
						while (true)
						{
							switch (3)
							{
							case 0:
								continue;
							}
							num17 = num17 / c8dcba10f15071a699619713aa7f64afb[num14] / c4218a936b59a793594ce8a8da87b52c4[num14, num14 - 1];
							for (int num19 = num14; num19 <= num2; num19++)
							{
								ccbdf70e0565d415a0cffd34ca03ac5e3[num19, num16] += num17 * c8dcba10f15071a699619713aa7f64afb[num19];
							}
							while (true)
							{
								switch (1)
								{
								case 0:
									break;
								default:
									goto end_IL_0372;
								}
								continue;
								end_IL_0372:
								break;
							}
							num16++;
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
						break;
					}
				}
			}
			return;
		}
	}

	private void c929277d6335217b2ed08823e6d898829(MatrixD c4218a936b59a793594ce8a8da87b52c4)
	{
		int num = c8704416eb2ee8a512db38b95b947dcc8;
		int num2 = num - 1;
		int num3 = 0;
		int num4 = num - 1;
		double num5 = Math.Pow(2.0, -52.0);
		double num6 = 0.0;
		double num7 = 0.0;
		double num8 = 0.0;
		double num9 = 0.0;
		double num10 = 0.0;
		double num11 = 0.0;
		double num12 = 0.0;
		for (int i = 0; i < num; i++)
		{
			if ((i < num3) | (i > num4))
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
					__ldtoken(EigenvalueDecompositionD.c929277d6335217b2ed08823e6d898829(MatrixD));
				}
				ce8aac9f53c7f93f91b33957b93c750e8[i] = c4218a936b59a793594ce8a8da87b52c4[i, i];
				ccce78d1e25a7861e22a5dfbdd3b9533d[i] = 0.0;
			}
			for (int j = Math.Max(i - 1, 0); j < num; j++)
			{
				num12 += Math.Abs(c4218a936b59a793594ce8a8da87b52c4[i, j]);
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					goto end_IL_00fb;
				}
				continue;
				end_IL_00fb:
				break;
			}
		}
		int num13 = 0;
		while (num2 >= num3)
		{
			int num14 = num2;
			while (true)
			{
				if (num14 > num3)
				{
					num10 = Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num14 - 1, num14 - 1]) + Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num14, num14]);
					if (num10 == 0.0)
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
						num10 = num12;
					}
					if (Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num14, num14 - 1]) < num5 * num10)
					{
						break;
					}
					while (true)
					{
						switch (4)
						{
						case 0:
							break;
						default:
							goto end_IL_017a;
						}
						continue;
						end_IL_017a:
						break;
					}
					num14--;
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
				break;
			}
			if (num14 == num2)
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
				c4218a936b59a793594ce8a8da87b52c4[num2, num2] += num6;
				ce8aac9f53c7f93f91b33957b93c750e8[num2] = c4218a936b59a793594ce8a8da87b52c4[num2, num2];
				ccce78d1e25a7861e22a5dfbdd3b9533d[num2] = 0.0;
				num2--;
				num13 = 0;
				continue;
			}
			double num16;
			double num15;
			if (num14 == num2 - 1)
			{
				num15 = c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1] * c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2];
				num7 = (c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1] - c4218a936b59a793594ce8a8da87b52c4[num2, num2]) / 2.0;
				num8 = num7 * num7 + num15;
				num11 = Math.Sqrt(Math.Abs(num8));
				c4218a936b59a793594ce8a8da87b52c4[num2, num2] += num6;
				c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1] += num6;
				num16 = c4218a936b59a793594ce8a8da87b52c4[num2, num2];
				if (num8 >= 0.0)
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
					num11 = ((!(num7 >= 0.0)) ? (num7 - num11) : (num7 + num11));
					ce8aac9f53c7f93f91b33957b93c750e8[num2 - 1] = num16 + num11;
					ce8aac9f53c7f93f91b33957b93c750e8[num2] = ce8aac9f53c7f93f91b33957b93c750e8[num2 - 1];
					if (num11 != 0.0)
					{
						ce8aac9f53c7f93f91b33957b93c750e8[num2] = num16 - num15 / num11;
					}
					ccce78d1e25a7861e22a5dfbdd3b9533d[num2 - 1] = 0.0;
					ccce78d1e25a7861e22a5dfbdd3b9533d[num2] = 0.0;
					num16 = c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1];
					num10 = Math.Abs(num16) + Math.Abs(num11);
					num7 = num16 / num10;
					num8 = num11 / num10;
					num9 = Math.Sqrt(num7 * num7 + num8 * num8);
					num7 /= num9;
					num8 /= num9;
					for (int k = num2 - 1; k < num; k++)
					{
						num11 = c4218a936b59a793594ce8a8da87b52c4[num2 - 1, k];
						c4218a936b59a793594ce8a8da87b52c4[num2 - 1, k] = num8 * num11 + num7 * c4218a936b59a793594ce8a8da87b52c4[num2, k];
						c4218a936b59a793594ce8a8da87b52c4[num2, k] = num8 * c4218a936b59a793594ce8a8da87b52c4[num2, k] - num7 * num11;
					}
					for (int l = 0; l <= num2; l++)
					{
						num11 = c4218a936b59a793594ce8a8da87b52c4[l, num2 - 1];
						c4218a936b59a793594ce8a8da87b52c4[l, num2 - 1] = num8 * num11 + num7 * c4218a936b59a793594ce8a8da87b52c4[l, num2];
						c4218a936b59a793594ce8a8da87b52c4[l, num2] = num8 * c4218a936b59a793594ce8a8da87b52c4[l, num2] - num7 * num11;
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
					for (int m = num3; m <= num4; m++)
					{
						num11 = ccbdf70e0565d415a0cffd34ca03ac5e3[m, num2 - 1];
						ccbdf70e0565d415a0cffd34ca03ac5e3[m, num2 - 1] = num8 * num11 + num7 * ccbdf70e0565d415a0cffd34ca03ac5e3[m, num2];
						ccbdf70e0565d415a0cffd34ca03ac5e3[m, num2] = num8 * ccbdf70e0565d415a0cffd34ca03ac5e3[m, num2] - num7 * num11;
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
					ce8aac9f53c7f93f91b33957b93c750e8[num2 - 1] = num16 + num7;
					ce8aac9f53c7f93f91b33957b93c750e8[num2] = num16 + num7;
					ccce78d1e25a7861e22a5dfbdd3b9533d[num2 - 1] = num11;
					ccce78d1e25a7861e22a5dfbdd3b9533d[num2] = 0.0 - num11;
				}
				num2 -= 2;
				num13 = 0;
				continue;
			}
			num16 = c4218a936b59a793594ce8a8da87b52c4[num2, num2];
			double num17 = 0.0;
			num15 = 0.0;
			if (num14 < num2)
			{
				num17 = c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1];
				num15 = c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1] * c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2];
			}
			if (num13 == 10)
			{
				num6 += num16;
				for (int n = num3; n <= num2; n++)
				{
					c4218a936b59a793594ce8a8da87b52c4[n, n] -= num16;
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
				num10 = Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1]) + Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 2]);
				num16 = (num17 = 0.75 * num10);
				num15 = -0.4375 * num10 * num10;
			}
			if (num13 == 30)
			{
				num10 = (num17 - num16) / 2.0;
				num10 = num10 * num10 + num15;
				if (num10 > 0.0)
				{
					num10 = Math.Sqrt(num10);
					if (num17 < num16)
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
						num10 = 0.0 - num10;
					}
					num10 = num16 - num15 / ((num17 - num16) / 2.0 + num10);
					for (int num18 = num3; num18 <= num2; num18++)
					{
						c4218a936b59a793594ce8a8da87b52c4[num18, num18] -= num10;
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
					num6 += num10;
					num16 = (num17 = (num15 = 0.964));
				}
			}
			num13++;
			int num19;
			for (num19 = num2 - 2; num19 >= num14; num19--)
			{
				num11 = c4218a936b59a793594ce8a8da87b52c4[num19, num19];
				num9 = num16 - num11;
				num10 = num17 - num11;
				num7 = (num9 * num10 - num15) / c4218a936b59a793594ce8a8da87b52c4[num19 + 1, num19] + c4218a936b59a793594ce8a8da87b52c4[num19, num19 + 1];
				num8 = c4218a936b59a793594ce8a8da87b52c4[num19 + 1, num19 + 1] - num11 - num9 - num10;
				num9 = c4218a936b59a793594ce8a8da87b52c4[num19 + 2, num19 + 1];
				num10 = Math.Abs(num7) + Math.Abs(num8) + Math.Abs(num9);
				num7 /= num10;
				num8 /= num10;
				num9 /= num10;
				if (num19 == num14)
				{
					break;
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
				if (Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num19, num19 - 1]) * (Math.Abs(num8) + Math.Abs(num9)) < num5 * (Math.Abs(num7) * (Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num19 - 1, num19 - 1]) + Math.Abs(num11) + Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num19 + 1, num19 + 1]))))
				{
					break;
				}
			}
			for (int num20 = num19 + 2; num20 <= num2; num20++)
			{
				c4218a936b59a793594ce8a8da87b52c4[num20, num20 - 2] = 0.0;
				if (num20 <= num19 + 2)
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
				c4218a936b59a793594ce8a8da87b52c4[num20, num20 - 3] = 0.0;
			}
			int num21 = num19;
			while (num21 <= num2 - 1)
			{
				bool flag = num21 != num2 - 1;
				if (num21 != num19)
				{
					num7 = c4218a936b59a793594ce8a8da87b52c4[num21, num21 - 1];
					num8 = c4218a936b59a793594ce8a8da87b52c4[num21 + 1, num21 - 1];
					double num22;
					if (!flag)
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
						num22 = 0.0;
					}
					else
					{
						num22 = c4218a936b59a793594ce8a8da87b52c4[num21 + 2, num21 - 1];
					}
					num9 = num22;
					num16 = Math.Abs(num7) + Math.Abs(num8) + Math.Abs(num9);
					if (num16 != 0.0)
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
						num7 /= num16;
						num8 /= num16;
						num9 /= num16;
					}
				}
				if (num16 == 0.0)
				{
					break;
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					num10 = Math.Sqrt(num7 * num7 + num8 * num8 + num9 * num9);
					if (num7 < 0.0)
					{
						num10 = 0.0 - num10;
					}
					if (num10 != 0.0)
					{
						if (num21 != num19)
						{
							c4218a936b59a793594ce8a8da87b52c4[num21, num21 - 1] = (0.0 - num10) * num16;
						}
						else if (num14 != num19)
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
							c4218a936b59a793594ce8a8da87b52c4[num21, num21 - 1] = 0.0 - c4218a936b59a793594ce8a8da87b52c4[num21, num21 - 1];
						}
						num7 += num10;
						num16 = num7 / num10;
						num17 = num8 / num10;
						num11 = num9 / num10;
						num8 /= num7;
						num9 /= num7;
						for (int num23 = num21; num23 < num; num23++)
						{
							num7 = c4218a936b59a793594ce8a8da87b52c4[num21, num23] + num8 * c4218a936b59a793594ce8a8da87b52c4[num21 + 1, num23];
							if (flag)
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
								num7 += num9 * c4218a936b59a793594ce8a8da87b52c4[num21 + 2, num23];
								c4218a936b59a793594ce8a8da87b52c4[num21 + 2, num23] -= num7 * num11;
							}
							c4218a936b59a793594ce8a8da87b52c4[num21, num23] -= num7 * num16;
							c4218a936b59a793594ce8a8da87b52c4[num21 + 1, num23] -= num7 * num17;
						}
						for (int num24 = 0; num24 <= Math.Min(num2, num21 + 3); num24++)
						{
							num7 = num16 * c4218a936b59a793594ce8a8da87b52c4[num24, num21] + num17 * c4218a936b59a793594ce8a8da87b52c4[num24, num21 + 1];
							if (flag)
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
								num7 += num11 * c4218a936b59a793594ce8a8da87b52c4[num24, num21 + 2];
								c4218a936b59a793594ce8a8da87b52c4[num24, num21 + 2] -= num7 * num9;
							}
							c4218a936b59a793594ce8a8da87b52c4[num24, num21] -= num7;
							c4218a936b59a793594ce8a8da87b52c4[num24, num21 + 1] -= num7 * num8;
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
						for (int num25 = num3; num25 <= num4; num25++)
						{
							num7 = num16 * ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21] + num17 * ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 1];
							if (flag)
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
								num7 += num11 * ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 2];
								ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 2] = ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 2] - num7 * num9;
							}
							ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21] -= num7;
							ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 1] = ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 1] - num7 * num8;
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
					num21++;
					break;
				}
			}
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			if (num12 == 0.0)
			{
				return;
			}
			for (num2 = num - 1; num2 >= 0; num2--)
			{
				num7 = ce8aac9f53c7f93f91b33957b93c750e8[num2];
				num8 = ccce78d1e25a7861e22a5dfbdd3b9533d[num2];
				if (num8 == 0.0)
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
					int num26 = num2;
					c4218a936b59a793594ce8a8da87b52c4[num2, num2] = 1.0;
					for (int num27 = num2 - 1; num27 >= 0; num27--)
					{
						double num15 = c4218a936b59a793594ce8a8da87b52c4[num27, num27] - num7;
						num9 = 0.0;
						for (int num28 = num26; num28 <= num2; num28++)
						{
							num9 += c4218a936b59a793594ce8a8da87b52c4[num27, num28] * c4218a936b59a793594ce8a8da87b52c4[num28, num2];
						}
						if (ccce78d1e25a7861e22a5dfbdd3b9533d[num27] < 0.0)
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
							num11 = num15;
							num10 = num9;
						}
						else
						{
							num26 = num27;
							double num29;
							if (ccce78d1e25a7861e22a5dfbdd3b9533d[num27] == 0.0)
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
								if (num15 != 0.0)
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
									c4218a936b59a793594ce8a8da87b52c4[num27, num2] = (0.0 - num9) / num15;
								}
								else
								{
									c4218a936b59a793594ce8a8da87b52c4[num27, num2] = (0.0 - num9) / (num5 * num12);
								}
							}
							else
							{
								double num16 = c4218a936b59a793594ce8a8da87b52c4[num27, num27 + 1];
								double num17 = c4218a936b59a793594ce8a8da87b52c4[num27 + 1, num27];
								num8 = (ce8aac9f53c7f93f91b33957b93c750e8[num27] - num7) * (ce8aac9f53c7f93f91b33957b93c750e8[num27] - num7) + ccce78d1e25a7861e22a5dfbdd3b9533d[num27] * ccce78d1e25a7861e22a5dfbdd3b9533d[num27];
								num29 = (c4218a936b59a793594ce8a8da87b52c4[num27, num2] = (num16 * num10 - num11 * num9) / num8);
								if (Math.Abs(num16) > Math.Abs(num11))
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
									c4218a936b59a793594ce8a8da87b52c4[num27 + 1, num2] = (0.0 - num9 - num15 * num29) / num16;
								}
								else
								{
									c4218a936b59a793594ce8a8da87b52c4[num27 + 1, num2] = (0.0 - num10 - num17 * num29) / num11;
								}
							}
							num29 = Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num27, num2]);
							if (num5 * num29 * num29 > 1.0)
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
								for (int num31 = num27; num31 <= num2; num31++)
								{
									c4218a936b59a793594ce8a8da87b52c4[num31, num2] /= num29;
								}
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
						break;
					}
				}
				else if (num8 < 0.0)
				{
					int num32 = num2 - 1;
					if (Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1]) > Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2]))
					{
						c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1] = num8 / c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1];
						c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2] = (0.0 - (c4218a936b59a793594ce8a8da87b52c4[num2, num2] - num7)) / c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1];
					}
					else
					{
						cbb8852c28ceab96cef3608d27898f274(0.0, 0.0 - c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2], c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1] - num7, num8, out var c6cb61f5a9cb051969b780a15ed33cd, out var cdec30e5aa80c0d5064f26d64b3af);
						c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1] = c6cb61f5a9cb051969b780a15ed33cd;
						c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2] = cdec30e5aa80c0d5064f26d64b3af;
					}
					c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1] = 0.0;
					c4218a936b59a793594ce8a8da87b52c4[num2, num2] = 1.0;
					for (int num33 = num2 - 2; num33 >= 0; num33--)
					{
						double num34 = 0.0;
						double num35 = 0.0;
						for (int num36 = num32; num36 <= num2; num36++)
						{
							num34 += c4218a936b59a793594ce8a8da87b52c4[num33, num36] * c4218a936b59a793594ce8a8da87b52c4[num36, num2 - 1];
							num35 += c4218a936b59a793594ce8a8da87b52c4[num33, num36] * c4218a936b59a793594ce8a8da87b52c4[num36, num2];
						}
						double num15 = c4218a936b59a793594ce8a8da87b52c4[num33, num33] - num7;
						if (ccce78d1e25a7861e22a5dfbdd3b9533d[num33] < 0.0)
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
							num11 = num15;
							num9 = num34;
							num10 = num35;
						}
						else
						{
							num32 = num33;
							if (ccce78d1e25a7861e22a5dfbdd3b9533d[num33] == 0.0)
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
								cbb8852c28ceab96cef3608d27898f274(0.0 - num34, 0.0 - num35, num15, num8, out var c6cb61f5a9cb051969b780a15ed33cd2, out var cdec30e5aa80c0d5064f26d64b3af2);
								c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1] = c6cb61f5a9cb051969b780a15ed33cd2;
								c4218a936b59a793594ce8a8da87b52c4[num33, num2] = cdec30e5aa80c0d5064f26d64b3af2;
							}
							else
							{
								double num16 = c4218a936b59a793594ce8a8da87b52c4[num33, num33 + 1];
								double num17 = c4218a936b59a793594ce8a8da87b52c4[num33 + 1, num33];
								double num37 = (ce8aac9f53c7f93f91b33957b93c750e8[num33] - num7) * (ce8aac9f53c7f93f91b33957b93c750e8[num33] - num7) + ccce78d1e25a7861e22a5dfbdd3b9533d[num33] * ccce78d1e25a7861e22a5dfbdd3b9533d[num33] - num8 * num8;
								double num38 = (ce8aac9f53c7f93f91b33957b93c750e8[num33] - num7) * 2.0 * num8;
								if ((num37 == 0.0) & (num38 == 0.0))
								{
									num37 = num5 * num12 * (Math.Abs(num15) + Math.Abs(num8) + Math.Abs(num16) + Math.Abs(num17) + Math.Abs(num11));
								}
								cbb8852c28ceab96cef3608d27898f274(num16 * num9 - num11 * num34 + num8 * num35, num16 * num10 - num11 * num35 - num8 * num34, num37, num38, out var c6cb61f5a9cb051969b780a15ed33cd3, out var cdec30e5aa80c0d5064f26d64b3af3);
								c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1] = c6cb61f5a9cb051969b780a15ed33cd3;
								c4218a936b59a793594ce8a8da87b52c4[num33, num2] = cdec30e5aa80c0d5064f26d64b3af3;
								if (Math.Abs(num16) > Math.Abs(num11) + Math.Abs(num8))
								{
									c4218a936b59a793594ce8a8da87b52c4[num33 + 1, num2 - 1] = (0.0 - num34 - num15 * c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1] + num8 * c4218a936b59a793594ce8a8da87b52c4[num33, num2]) / num16;
									c4218a936b59a793594ce8a8da87b52c4[num33 + 1, num2] = (0.0 - num35 - num15 * c4218a936b59a793594ce8a8da87b52c4[num33, num2] - num8 * c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1]) / num16;
								}
								else
								{
									cbb8852c28ceab96cef3608d27898f274(0.0 - num9 - num17 * c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1], 0.0 - num10 - num17 * c4218a936b59a793594ce8a8da87b52c4[num33, num2], num11, num8, out c6cb61f5a9cb051969b780a15ed33cd3, out cdec30e5aa80c0d5064f26d64b3af3);
									c4218a936b59a793594ce8a8da87b52c4[num33 + 1, num2 - 1] = c6cb61f5a9cb051969b780a15ed33cd3;
									c4218a936b59a793594ce8a8da87b52c4[num33 + 1, num2] = cdec30e5aa80c0d5064f26d64b3af3;
								}
							}
							double num39 = Math.Max(Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1]), Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num33, num2]));
							if (num5 * num39 * num39 > 1.0)
							{
								for (int num40 = num33; num40 <= num2; num40++)
								{
									c4218a936b59a793594ce8a8da87b52c4[num40, num2 - 1] /= num39;
									c4218a936b59a793594ce8a8da87b52c4[num40, num2] /= num39;
								}
							}
						}
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
				}
			}
			for (int num41 = 0; num41 < num; num41++)
			{
				if (!((num41 < num3) | (num41 > num4)))
				{
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
				for (int num42 = num41; num42 < num; num42++)
				{
					ccbdf70e0565d415a0cffd34ca03ac5e3[num41, num42] = c4218a936b59a793594ce8a8da87b52c4[num41, num42];
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
				switch (2)
				{
				case 0:
					continue;
				}
				for (int num43 = num - 1; num43 >= num3; num43--)
				{
					for (int num44 = num3; num44 <= num4; num44++)
					{
						num11 = 0.0;
						for (int num45 = num3; num45 <= Math.Min(num43, num4); num45++)
						{
							num11 += ccbdf70e0565d415a0cffd34ca03ac5e3[num44, num45] * c4218a936b59a793594ce8a8da87b52c4[num45, num43];
						}
						ccbdf70e0565d415a0cffd34ca03ac5e3[num44, num43] = num11;
					}
					while (true)
					{
						switch (5)
						{
						case 0:
							break;
						default:
							goto end_IL_1415;
						}
						continue;
						end_IL_1415:
						break;
					}
				}
				return;
			}
		}
	}

	private static void cbb8852c28ceab96cef3608d27898f274(double cb9e93570e4aa83467d0b708a170944f7, double c125eeb3fef00e99f8e81684cfeaa9bd2, double cab3eb2a93f68436c211043a904a94be9, double c509f5274e4fe7380e3e2c04e68b174f5, out double c6cb61f5a9cb051969b780a15ed33cd63, out double cdec30e5aa80c0d5064f26d64b3af2518)
	{
		double num;
		double num2;
		if (Math.Abs(cab3eb2a93f68436c211043a904a94be9) > Math.Abs(c509f5274e4fe7380e3e2c04e68b174f5))
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
						__ldtoken(EigenvalueDecompositionD.cbb8852c28ceab96cef3608d27898f274(double, double, double, double, ref double, ref double));
					}
					num = c509f5274e4fe7380e3e2c04e68b174f5 / cab3eb2a93f68436c211043a904a94be9;
					num2 = cab3eb2a93f68436c211043a904a94be9 + num * c509f5274e4fe7380e3e2c04e68b174f5;
					c6cb61f5a9cb051969b780a15ed33cd63 = (cb9e93570e4aa83467d0b708a170944f7 + num * c125eeb3fef00e99f8e81684cfeaa9bd2) / num2;
					cdec30e5aa80c0d5064f26d64b3af2518 = (c125eeb3fef00e99f8e81684cfeaa9bd2 - num * cb9e93570e4aa83467d0b708a170944f7) / num2;
					return;
				}
			}
		}
		num = cab3eb2a93f68436c211043a904a94be9 / c509f5274e4fe7380e3e2c04e68b174f5;
		num2 = c509f5274e4fe7380e3e2c04e68b174f5 + num * cab3eb2a93f68436c211043a904a94be9;
		c6cb61f5a9cb051969b780a15ed33cd63 = (num * cb9e93570e4aa83467d0b708a170944f7 + c125eeb3fef00e99f8e81684cfeaa9bd2) / num2;
		cdec30e5aa80c0d5064f26d64b3af2518 = (num * c125eeb3fef00e99f8e81684cfeaa9bd2 - cb9e93570e4aa83467d0b708a170944f7) / num2;
	}
}
