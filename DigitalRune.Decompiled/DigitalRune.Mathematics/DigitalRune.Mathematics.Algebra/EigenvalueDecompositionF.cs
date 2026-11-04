using System;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Computes the eigenvalues and eigenvectors of a real square matrix A (single-precision).
///       </summary>
public class EigenvalueDecompositionF
{
	private readonly MatrixF ccbdf70e0565d415a0cffd34ca03ac5e3;

	private readonly VectorF ce8aac9f53c7f93f91b33957b93c750e8;

	private MatrixF cdca57945f0c1c77be3bfc1df8a1df4c3;

	private readonly VectorF ccce78d1e25a7861e22a5dfbdd3b9533d;

	private readonly int c8704416eb2ee8a512db38b95b947dcc8;

	private readonly bool ce8913a3c974007c03eb12f43a3ba8dfb;

	/// <summary>
	///       Gets the vector of the imaginary parts of the eigenvalues. 
	///       </summary>
	public VectorF ImaginaryEigenvalues => ccce78d1e25a7861e22a5dfbdd3b9533d;

	/// <summary>
	///       Gets the block diagonal eigenvalue matrix D. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixF D
	{
		get
		{
			if (cdca57945f0c1c77be3bfc1df8a1df4c3 != null)
			{
				return cdca57945f0c1c77be3bfc1df8a1df4c3;
			}
			cdca57945f0c1c77be3bfc1df8a1df4c3 = new MatrixF(c8704416eb2ee8a512db38b95b947dcc8, c8704416eb2ee8a512db38b95b947dcc8);
			for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
			{
				cdca57945f0c1c77be3bfc1df8a1df4c3[i, i] = ce8aac9f53c7f93f91b33957b93c750e8[i];
				if (ccce78d1e25a7861e22a5dfbdd3b9533d[i] > 0f)
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
					if (1 == 0)
					{
						__ldtoken(EigenvalueDecompositionF.get_D());
					}
					cdca57945f0c1c77be3bfc1df8a1df4c3[i, i + 1] = ccce78d1e25a7861e22a5dfbdd3b9533d[i];
				}
				else
				{
					if (!(ccce78d1e25a7861e22a5dfbdd3b9533d[i] < 0f))
					{
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
					cdca57945f0c1c77be3bfc1df8a1df4c3[i, i - 1] = ccce78d1e25a7861e22a5dfbdd3b9533d[i];
				}
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				return cdca57945f0c1c77be3bfc1df8a1df4c3;
			}
		}
	}

	/// <summary>
	///       Gets the eigenvector matrix V. (This property returns the internal matrix, 
	///       not a copy.)
	///       </summary>
	public MatrixF V => ccbdf70e0565d415a0cffd34ca03ac5e3;

	/// <summary>
	///       Gets the vector of the real parts of the eigenvalues (the diagonal of D). 
	///       </summary>
	public VectorF RealEigenvalues => ce8aac9f53c7f93f91b33957b93c750e8;

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
	public EigenvalueDecompositionF(MatrixF matrixA)
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
						__ldtoken(EigenvalueDecompositionF._002Ector(MatrixF));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
				}
			}
		}
		if (!matrixA.IsSquare)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4658), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
				}
			}
		}
		c8704416eb2ee8a512db38b95b947dcc8 = matrixA.NumberOfColumns;
		ce8aac9f53c7f93f91b33957b93c750e8 = new VectorF(c8704416eb2ee8a512db38b95b947dcc8);
		ccce78d1e25a7861e22a5dfbdd3b9533d = new VectorF(c8704416eb2ee8a512db38b95b947dcc8);
		ce8913a3c974007c03eb12f43a3ba8dfb = matrixA.IsSymmetric;
		if (ce8913a3c974007c03eb12f43a3ba8dfb)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					ccbdf70e0565d415a0cffd34ca03ac5e3 = matrixA.Clone();
					c30f61ef2201a97c418b4698ca87d10e0();
					c994d424a781058095e3d76f0436d959b();
					return;
				}
			}
		}
		ccbdf70e0565d415a0cffd34ca03ac5e3 = new MatrixF(c8704416eb2ee8a512db38b95b947dcc8, c8704416eb2ee8a512db38b95b947dcc8);
		for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
		{
			for (int j = 0; j < c8704416eb2ee8a512db38b95b947dcc8; j++)
			{
				if (!Numeric.IsNaN(matrixA[i, j]))
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
					ccce78d1e25a7861e22a5dfbdd3b9533d.Set(float.NaN);
					ccbdf70e0565d415a0cffd34ca03ac5e3.Set(float.NaN);
					ce8aac9f53c7f93f91b33957b93c750e8.Set(float.NaN);
					return;
				}
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					goto end_IL_013d;
				}
				continue;
				end_IL_013d:
				break;
			}
		}
		MatrixF c4218a936b59a793594ce8a8da87b52c = matrixA.Clone();
		float[] c8dcba10f15071a699619713aa7f64afb = new float[c8704416eb2ee8a512db38b95b947dcc8];
		c529c977bc9d1176a4031bdd1ad5ce7f3(c4218a936b59a793594ce8a8da87b52c, c8dcba10f15071a699619713aa7f64afb);
		c929277d6335217b2ed08823e6d898829(c4218a936b59a793594ce8a8da87b52c);
	}

	private void c30f61ef2201a97c418b4698ca87d10e0()
	{
		for (int i = 0; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
		{
			ce8aac9f53c7f93f91b33957b93c750e8[i] = ccbdf70e0565d415a0cffd34ca03ac5e3[c8704416eb2ee8a512db38b95b947dcc8 - 1, i];
		}
		for (int num = c8704416eb2ee8a512db38b95b947dcc8 - 1; num > 0; num--)
		{
			float num2 = 0f;
			float num3 = 0f;
			for (int j = 0; j < num; j++)
			{
				num2 += Math.Abs(ce8aac9f53c7f93f91b33957b93c750e8[j]);
			}
			if (num2 == 0f)
			{
				ccce78d1e25a7861e22a5dfbdd3b9533d[num] = ce8aac9f53c7f93f91b33957b93c750e8[num - 1];
				for (int k = 0; k < num; k++)
				{
					ce8aac9f53c7f93f91b33957b93c750e8[k] = ccbdf70e0565d415a0cffd34ca03ac5e3[num - 1, k];
					ccbdf70e0565d415a0cffd34ca03ac5e3[num, k] = 0f;
					ccbdf70e0565d415a0cffd34ca03ac5e3[k, num] = 0f;
				}
			}
			else
			{
				for (int l = 0; l < num; l++)
				{
					ce8aac9f53c7f93f91b33957b93c750e8[l] /= num2;
					num3 += ce8aac9f53c7f93f91b33957b93c750e8[l] * ce8aac9f53c7f93f91b33957b93c750e8[l];
				}
				float num4 = ce8aac9f53c7f93f91b33957b93c750e8[num - 1];
				float num5 = (float)Math.Sqrt(num3);
				if (num4 > 0f)
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
					if (1 == 0)
					{
						__ldtoken(EigenvalueDecompositionF.c30f61ef2201a97c418b4698ca87d10e0());
					}
					num5 = 0f - num5;
				}
				ccce78d1e25a7861e22a5dfbdd3b9533d[num] = num2 * num5;
				num3 -= num4 * num5;
				ce8aac9f53c7f93f91b33957b93c750e8[num - 1] = num4 - num5;
				for (int m = 0; m < num; m++)
				{
					ccce78d1e25a7861e22a5dfbdd3b9533d[m] = 0f;
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
					while (true)
					{
						switch (4)
						{
						case 0:
							break;
						default:
							goto end_IL_027f;
						}
						continue;
						end_IL_027f:
						break;
					}
					ccce78d1e25a7861e22a5dfbdd3b9533d[n] = num5;
				}
				num4 = 0f;
				for (int num7 = 0; num7 < num; num7++)
				{
					ccce78d1e25a7861e22a5dfbdd3b9533d[num7] /= num3;
					num4 += ccce78d1e25a7861e22a5dfbdd3b9533d[num7] * ce8aac9f53c7f93f91b33957b93c750e8[num7];
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
				float num8 = num4 / (num3 + num3);
				for (int num9 = 0; num9 < num; num9++)
				{
					ccce78d1e25a7861e22a5dfbdd3b9533d[num9] -= num8 * ce8aac9f53c7f93f91b33957b93c750e8[num9];
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
				for (int num10 = 0; num10 < num; num10++)
				{
					num4 = ce8aac9f53c7f93f91b33957b93c750e8[num10];
					num5 = ccce78d1e25a7861e22a5dfbdd3b9533d[num10];
					for (int num11 = num10; num11 <= num - 1; num11++)
					{
						ccbdf70e0565d415a0cffd34ca03ac5e3[num11, num10] -= num4 * ccce78d1e25a7861e22a5dfbdd3b9533d[num11] + num5 * ce8aac9f53c7f93f91b33957b93c750e8[num11];
					}
					while (true)
					{
						switch (1)
						{
						case 0:
							break;
						default:
							goto end_IL_03de;
						}
						continue;
						end_IL_03de:
						break;
					}
					ce8aac9f53c7f93f91b33957b93c750e8[num10] = ccbdf70e0565d415a0cffd34ca03ac5e3[num - 1, num10];
					ccbdf70e0565d415a0cffd34ca03ac5e3[num, num10] = 0f;
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
			ce8aac9f53c7f93f91b33957b93c750e8[num] = num3;
		}
		for (int num12 = 0; num12 < c8704416eb2ee8a512db38b95b947dcc8 - 1; num12++)
		{
			ccbdf70e0565d415a0cffd34ca03ac5e3[c8704416eb2ee8a512db38b95b947dcc8 - 1, num12] = ccbdf70e0565d415a0cffd34ca03ac5e3[num12, num12];
			ccbdf70e0565d415a0cffd34ca03ac5e3[num12, num12] = 1f;
			float num13 = ce8aac9f53c7f93f91b33957b93c750e8[num12 + 1];
			if ((double)num13 != 0.0)
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
				for (int num14 = 0; num14 <= num12; num14++)
				{
					ce8aac9f53c7f93f91b33957b93c750e8[num14] = ccbdf70e0565d415a0cffd34ca03ac5e3[num14, num12 + 1] / num13;
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
				int num15 = 0;
				while (num15 <= num12)
				{
					float num16 = 0f;
					for (int num17 = 0; num17 <= num12; num17++)
					{
						num16 += ccbdf70e0565d415a0cffd34ca03ac5e3[num17, num12 + 1] * ccbdf70e0565d415a0cffd34ca03ac5e3[num17, num15];
					}
					while (true)
					{
						switch (4)
						{
						case 0:
							continue;
						}
						for (int num18 = 0; num18 <= num12; num18++)
						{
							ccbdf70e0565d415a0cffd34ca03ac5e3[num18, num15] -= num16 * ce8aac9f53c7f93f91b33957b93c750e8[num18];
						}
						num15++;
						break;
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
			for (int num19 = 0; num19 <= num12; num19++)
			{
				ccbdf70e0565d415a0cffd34ca03ac5e3[num19, num12 + 1] = 0f;
			}
		}
		for (int num20 = 0; num20 < c8704416eb2ee8a512db38b95b947dcc8; num20++)
		{
			ce8aac9f53c7f93f91b33957b93c750e8[num20] = ccbdf70e0565d415a0cffd34ca03ac5e3[c8704416eb2ee8a512db38b95b947dcc8 - 1, num20];
			ccbdf70e0565d415a0cffd34ca03ac5e3[c8704416eb2ee8a512db38b95b947dcc8 - 1, num20] = 0f;
		}
		ccbdf70e0565d415a0cffd34ca03ac5e3[c8704416eb2ee8a512db38b95b947dcc8 - 1, c8704416eb2ee8a512db38b95b947dcc8 - 1] = 1f;
		ccce78d1e25a7861e22a5dfbdd3b9533d[0] = 0f;
	}

	private void c994d424a781058095e3d76f0436d959b()
	{
		for (int i = 1; i < c8704416eb2ee8a512db38b95b947dcc8; i++)
		{
			ccce78d1e25a7861e22a5dfbdd3b9533d[i - 1] = ccce78d1e25a7861e22a5dfbdd3b9533d[i];
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
				__ldtoken(EigenvalueDecompositionF.c994d424a781058095e3d76f0436d959b());
			}
			ccce78d1e25a7861e22a5dfbdd3b9533d[c8704416eb2ee8a512db38b95b947dcc8 - 1] = 0f;
			float num = 0f;
			float num2 = 0f;
			float num3 = (float)Math.Pow(2.0, -23.0);
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
						num4++;
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
					break;
				}
				if (num4 > j)
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
					int num5 = 0;
					while (true)
					{
						num5++;
						float num6 = ce8aac9f53c7f93f91b33957b93c750e8[j];
						float num7 = (ce8aac9f53c7f93f91b33957b93c750e8[j + 1] - num6) / (2f * ccce78d1e25a7861e22a5dfbdd3b9533d[j]);
						float num8 = MathHelper.Hypotenuse(num7, 1f);
						if (num7 < 0f)
						{
							num8 = 0f - num8;
						}
						ce8aac9f53c7f93f91b33957b93c750e8[j] = ccce78d1e25a7861e22a5dfbdd3b9533d[j] / (num7 + num8);
						ce8aac9f53c7f93f91b33957b93c750e8[j + 1] = ccce78d1e25a7861e22a5dfbdd3b9533d[j] * (num7 + num8);
						float num9 = ce8aac9f53c7f93f91b33957b93c750e8[j + 1];
						float num10 = num6 - ce8aac9f53c7f93f91b33957b93c750e8[j];
						for (int k = j + 2; k < c8704416eb2ee8a512db38b95b947dcc8; k++)
						{
							ce8aac9f53c7f93f91b33957b93c750e8[k] -= num10;
						}
						while (true)
						{
							switch (7)
							{
							case 0:
								continue;
							}
							num += num10;
							num7 = ce8aac9f53c7f93f91b33957b93c750e8[num4];
							float num11 = 1f;
							float num12 = num11;
							float num13 = num11;
							float num14 = ccce78d1e25a7861e22a5dfbdd3b9533d[j + 1];
							float num15 = 0f;
							float num16 = 0f;
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
								switch (7)
								{
								case 0:
									break;
								default:
									goto end_IL_0394;
								}
								continue;
								end_IL_0394:
								break;
							}
							num7 = (0f - num15) * num16 * num13 * num14 * ccce78d1e25a7861e22a5dfbdd3b9533d[j] / num9;
							ccce78d1e25a7861e22a5dfbdd3b9533d[j] = num15 * num7;
							ce8aac9f53c7f93f91b33957b93c750e8[j] = num11 * num7;
							if (Math.Abs(ccce78d1e25a7861e22a5dfbdd3b9533d[j]) > num3 * num2)
							{
								break;
							}
							goto end_IL_0100;
						}
						continue;
						end_IL_0100:
						break;
					}
				}
				ce8aac9f53c7f93f91b33957b93c750e8[j] += num;
				ccce78d1e25a7861e22a5dfbdd3b9533d[j] = 0f;
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				for (int m = 0; m < c8704416eb2ee8a512db38b95b947dcc8 - 1; m++)
				{
					int num18 = m;
					float num19 = ce8aac9f53c7f93f91b33957b93c750e8[m];
					for (int n = m + 1; n < c8704416eb2ee8a512db38b95b947dcc8; n++)
					{
						if (!(ce8aac9f53c7f93f91b33957b93c750e8[n] < num19))
						{
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
						num18 = n;
						num19 = ce8aac9f53c7f93f91b33957b93c750e8[n];
					}
					if (num18 == m)
					{
						continue;
					}
					ce8aac9f53c7f93f91b33957b93c750e8[num18] = ce8aac9f53c7f93f91b33957b93c750e8[m];
					ce8aac9f53c7f93f91b33957b93c750e8[m] = num19;
					for (int num20 = 0; num20 < c8704416eb2ee8a512db38b95b947dcc8; num20++)
					{
						num19 = ccbdf70e0565d415a0cffd34ca03ac5e3[num20, m];
						ccbdf70e0565d415a0cffd34ca03ac5e3[num20, m] = ccbdf70e0565d415a0cffd34ca03ac5e3[num20, num18];
						ccbdf70e0565d415a0cffd34ca03ac5e3[num20, num18] = num19;
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
				while (true)
				{
					switch (2)
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

	private void c529c977bc9d1176a4031bdd1ad5ce7f3(MatrixF c4218a936b59a793594ce8a8da87b52c4, float[] c8dcba10f15071a699619713aa7f64afb)
	{
		int num = 0;
		int num2 = c8704416eb2ee8a512db38b95b947dcc8 - 1;
		for (int i = num + 1; i <= num2 - 1; i++)
		{
			float num3 = 0f;
			for (int j = i; j <= num2; j++)
			{
				num3 += Math.Abs(c4218a936b59a793594ce8a8da87b52c4[j, i - 1]);
			}
			if (num3 == 0f)
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
				break;
			}
			if (1 == 0)
			{
				__ldtoken(EigenvalueDecompositionF.c529c977bc9d1176a4031bdd1ad5ce7f3(MatrixF, float[]));
			}
			float num4 = 0f;
			for (int num5 = num2; num5 >= i; num5--)
			{
				c8dcba10f15071a699619713aa7f64afb[num5] = c4218a936b59a793594ce8a8da87b52c4[num5, i - 1] / num3;
				num4 += c8dcba10f15071a699619713aa7f64afb[num5] * c8dcba10f15071a699619713aa7f64afb[num5];
			}
			float num6 = (float)Math.Sqrt(num4);
			if (c8dcba10f15071a699619713aa7f64afb[i] > 0f)
			{
				num6 = 0f - num6;
			}
			num4 -= c8dcba10f15071a699619713aa7f64afb[i] * num6;
			c8dcba10f15071a699619713aa7f64afb[i] -= num6;
			for (int k = i; k < c8704416eb2ee8a512db38b95b947dcc8; k++)
			{
				float num7 = 0f;
				for (int num8 = num2; num8 >= i; num8--)
				{
					num7 += c8dcba10f15071a699619713aa7f64afb[num8] * c4218a936b59a793594ce8a8da87b52c4[num8, k];
				}
				num7 /= num4;
				for (int l = i; l <= num2; l++)
				{
					c4218a936b59a793594ce8a8da87b52c4[l, k] -= num7 * c8dcba10f15071a699619713aa7f64afb[l];
				}
				while (true)
				{
					switch (6)
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
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			for (int m = 0; m <= num2; m++)
			{
				float num9 = 0f;
				for (int num10 = num2; num10 >= i; num10--)
				{
					num9 += c8dcba10f15071a699619713aa7f64afb[num10] * c4218a936b59a793594ce8a8da87b52c4[m, num10];
				}
				num9 /= num4;
				for (int n = i; n <= num2; n++)
				{
					c4218a936b59a793594ce8a8da87b52c4[m, n] -= num9 * c8dcba10f15071a699619713aa7f64afb[n];
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						goto end_IL_01d0;
					}
					continue;
					end_IL_01d0:
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
				break;
			}
			c8dcba10f15071a699619713aa7f64afb[i] = num3 * c8dcba10f15071a699619713aa7f64afb[i];
			c4218a936b59a793594ce8a8da87b52c4[i, i - 1] = num3 * num6;
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			for (int num11 = 0; num11 < c8704416eb2ee8a512db38b95b947dcc8; num11++)
			{
				for (int num12 = 0; num12 < c8704416eb2ee8a512db38b95b947dcc8; num12++)
				{
					MatrixF matrixF = ccbdf70e0565d415a0cffd34ca03ac5e3;
					int row = num11;
					int column = num12;
					int num13;
					if (num11 != num12)
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
						num13 = 0;
					}
					else
					{
						num13 = 1;
					}
					matrixF[row, column] = num13;
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
						goto end_IL_025d;
					}
					continue;
					end_IL_025d:
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
				for (int num14 = num2 - 1; num14 >= num + 1; num14--)
				{
					if ((double)c4218a936b59a793594ce8a8da87b52c4[num14, num14 - 1] != 0.0)
					{
						for (int num15 = num14 + 1; num15 <= num2; num15++)
						{
							c8dcba10f15071a699619713aa7f64afb[num15] = c4218a936b59a793594ce8a8da87b52c4[num15, num14 - 1];
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
						int num16 = num14;
						while (num16 <= num2)
						{
							float num17 = 0f;
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
									switch (5)
									{
									case 0:
										break;
									default:
										goto end_IL_036f;
									}
									continue;
									end_IL_036f:
									break;
								}
								num16++;
								break;
							}
						}
					}
				}
				return;
			}
		}
	}

	private void c929277d6335217b2ed08823e6d898829(MatrixF c4218a936b59a793594ce8a8da87b52c4)
	{
		int num = c8704416eb2ee8a512db38b95b947dcc8;
		int num2 = num - 1;
		int num3 = 0;
		int num4 = num - 1;
		float num5 = (float)Math.Pow(2.0, -23.0);
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		float num11 = 0f;
		float num12 = 0f;
		for (int i = 0; i < num; i++)
		{
			if ((i < num3) | (i > num4))
			{
				ce8aac9f53c7f93f91b33957b93c750e8[i] = c4218a936b59a793594ce8a8da87b52c4[i, i];
				ccce78d1e25a7861e22a5dfbdd3b9533d[i] = 0f;
			}
			for (int j = Math.Max(i - 1, 0); j < num; j++)
			{
				num12 += Math.Abs(c4218a936b59a793594ce8a8da87b52c4[i, j]);
			}
		}
		int num13 = 0;
		while (num2 >= num3)
		{
			int num14;
			for (num14 = num2; num14 > num3; num14--)
			{
				num10 = Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num14 - 1, num14 - 1]) + Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num14, num14]);
				if (num10 == 0f)
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
					if (1 == 0)
					{
						__ldtoken(EigenvalueDecompositionF.c929277d6335217b2ed08823e6d898829(MatrixF));
					}
					num10 = num12;
				}
				if (Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num14, num14 - 1]) < num5 * num10)
				{
					break;
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						goto end_IL_0147;
					}
					continue;
					end_IL_0147:
					break;
				}
			}
			if (num14 == num2)
			{
				c4218a936b59a793594ce8a8da87b52c4[num2, num2] += num6;
				ce8aac9f53c7f93f91b33957b93c750e8[num2] = c4218a936b59a793594ce8a8da87b52c4[num2, num2];
				ccce78d1e25a7861e22a5dfbdd3b9533d[num2] = 0f;
				num2--;
				num13 = 0;
				continue;
			}
			float num16;
			float num15;
			if (num14 == num2 - 1)
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
				num15 = c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1] * c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2];
				num7 = (c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1] - c4218a936b59a793594ce8a8da87b52c4[num2, num2]) / 2f;
				num8 = num7 * num7 + num15;
				num11 = (float)Math.Sqrt(Math.Abs(num8));
				c4218a936b59a793594ce8a8da87b52c4[num2, num2] += num6;
				c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1] += num6;
				num16 = c4218a936b59a793594ce8a8da87b52c4[num2, num2];
				if (num8 >= 0f)
				{
					num11 = ((!(num7 >= 0f)) ? (num7 - num11) : (num7 + num11));
					ce8aac9f53c7f93f91b33957b93c750e8[num2 - 1] = num16 + num11;
					ce8aac9f53c7f93f91b33957b93c750e8[num2] = ce8aac9f53c7f93f91b33957b93c750e8[num2 - 1];
					if ((double)num11 != 0.0)
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
						ce8aac9f53c7f93f91b33957b93c750e8[num2] = num16 - num15 / num11;
					}
					ccce78d1e25a7861e22a5dfbdd3b9533d[num2 - 1] = 0f;
					ccce78d1e25a7861e22a5dfbdd3b9533d[num2] = 0f;
					num16 = c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1];
					num10 = Math.Abs(num16) + Math.Abs(num11);
					num7 = num16 / num10;
					num8 = num11 / num10;
					num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
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
						switch (1)
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
					ccce78d1e25a7861e22a5dfbdd3b9533d[num2] = 0f - num11;
				}
				num2 -= 2;
				num13 = 0;
				continue;
			}
			num16 = c4218a936b59a793594ce8a8da87b52c4[num2, num2];
			float num17 = 0f;
			num15 = 0f;
			if (num14 < num2)
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
				num17 = c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1];
				num15 = c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1] * c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2];
			}
			if (num13 == 10)
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
				num6 += num16;
				for (int n = num3; n <= num2; n++)
				{
					c4218a936b59a793594ce8a8da87b52c4[n, n] -= num16;
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
				num10 = Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1]) + Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 2]);
				num16 = (num17 = 0.75f * num10);
				num15 = -0.4375f * num10 * num10;
			}
			if (num13 == 30)
			{
				num10 = (num17 - num16) / 2f;
				num10 = num10 * num10 + num15;
				if (num10 > 0f)
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
					num10 = (float)Math.Sqrt(num10);
					if (num17 < num16)
					{
						num10 = 0f - num10;
					}
					num10 = num16 - num15 / ((num17 - num16) / 2f + num10);
					for (int num18 = num3; num18 <= num2; num18++)
					{
						c4218a936b59a793594ce8a8da87b52c4[num18, num18] -= num10;
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
					num6 += num10;
					num16 = (num17 = (num15 = 0.964f));
				}
			}
			num13++;
			int num19 = num2 - 2;
			while (true)
			{
				if (num19 >= num14)
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
						switch (7)
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
					while (true)
					{
						switch (4)
						{
						case 0:
							break;
						default:
							goto end_IL_0787;
						}
						continue;
						end_IL_0787:
						break;
					}
					num19--;
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
			for (int num20 = num19 + 2; num20 <= num2; num20++)
			{
				c4218a936b59a793594ce8a8da87b52c4[num20, num20 - 2] = 0f;
				if (num20 > num19 + 2)
				{
					c4218a936b59a793594ce8a8da87b52c4[num20, num20 - 3] = 0f;
				}
			}
			int num21 = num19;
			while (true)
			{
				if (num21 <= num2 - 1)
				{
					bool flag = num21 != num2 - 1;
					if (num21 != num19)
					{
						num7 = c4218a936b59a793594ce8a8da87b52c4[num21, num21 - 1];
						num8 = c4218a936b59a793594ce8a8da87b52c4[num21 + 1, num21 - 1];
						float num22;
						if (!flag)
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
							num22 = 0f;
						}
						else
						{
							num22 = c4218a936b59a793594ce8a8da87b52c4[num21 + 2, num21 - 1];
						}
						num9 = num22;
						num16 = Math.Abs(num7) + Math.Abs(num8) + Math.Abs(num9);
						if ((double)num16 != 0.0)
						{
							num7 /= num16;
							num8 /= num16;
							num9 /= num16;
						}
					}
					if ((double)num16 == 0.0)
					{
						break;
					}
					num10 = (float)Math.Sqrt(num7 * num7 + num8 * num8 + num9 * num9);
					if (num7 < 0f)
					{
						num10 = 0f - num10;
					}
					if (num10 != 0f)
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
						if (num21 != num19)
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
							c4218a936b59a793594ce8a8da87b52c4[num21, num21 - 1] = (0f - num10) * num16;
						}
						else if (num14 != num19)
						{
							c4218a936b59a793594ce8a8da87b52c4[num21, num21 - 1] = 0f - c4218a936b59a793594ce8a8da87b52c4[num21, num21 - 1];
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
									switch (3)
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
						while (true)
						{
							switch (5)
							{
							case 0:
								continue;
							}
							break;
						}
						for (int num24 = 0; num24 <= Math.Min(num2, num21 + 3); num24++)
						{
							num7 = num16 * c4218a936b59a793594ce8a8da87b52c4[num24, num21] + num17 * c4218a936b59a793594ce8a8da87b52c4[num24, num21 + 1];
							if (flag)
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
								num7 += num11 * c4218a936b59a793594ce8a8da87b52c4[num24, num21 + 2];
								c4218a936b59a793594ce8a8da87b52c4[num24, num21 + 2] -= num7 * num9;
							}
							c4218a936b59a793594ce8a8da87b52c4[num24, num21] -= num7;
							c4218a936b59a793594ce8a8da87b52c4[num24, num21 + 1] -= num7 * num8;
						}
						for (int num25 = num3; num25 <= num4; num25++)
						{
							num7 = num16 * ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21] + num17 * ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 1];
							if (flag)
							{
								num7 += num11 * ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 2];
								ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 2] = ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 2] - num7 * num9;
							}
							ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21] -= num7;
							ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 1] = ccbdf70e0565d415a0cffd34ca03ac5e3[num25, num21 + 1] - num7 * num8;
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
					num21++;
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
		}
		if ((double)num12 == 0.0)
		{
			return;
		}
		for (num2 = num - 1; num2 >= 0; num2--)
		{
			num7 = ce8aac9f53c7f93f91b33957b93c750e8[num2];
			num8 = ccce78d1e25a7861e22a5dfbdd3b9533d[num2];
			if (num8 == 0f)
			{
				int num26 = num2;
				c4218a936b59a793594ce8a8da87b52c4[num2, num2] = 1f;
				int num27 = num2 - 1;
				while (num27 >= 0)
				{
					float num15 = c4218a936b59a793594ce8a8da87b52c4[num27, num27] - num7;
					num9 = 0f;
					for (int num28 = num26; num28 <= num2; num28++)
					{
						num9 += c4218a936b59a793594ce8a8da87b52c4[num27, num28] * c4218a936b59a793594ce8a8da87b52c4[num28, num2];
					}
					while (true)
					{
						switch (7)
						{
						case 0:
							continue;
						}
						if ((double)ccce78d1e25a7861e22a5dfbdd3b9533d[num27] < 0.0)
						{
							num11 = num15;
							num10 = num9;
						}
						else
						{
							num26 = num27;
							float num29;
							if (ccce78d1e25a7861e22a5dfbdd3b9533d[num27] == 0f)
							{
								if (num15 != 0f)
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
									c4218a936b59a793594ce8a8da87b52c4[num27, num2] = (0f - num9) / num15;
								}
								else
								{
									c4218a936b59a793594ce8a8da87b52c4[num27, num2] = (0f - num9) / (num5 * num12);
								}
							}
							else
							{
								float num16 = c4218a936b59a793594ce8a8da87b52c4[num27, num27 + 1];
								float num17 = c4218a936b59a793594ce8a8da87b52c4[num27 + 1, num27];
								num8 = (ce8aac9f53c7f93f91b33957b93c750e8[num27] - num7) * (ce8aac9f53c7f93f91b33957b93c750e8[num27] - num7) + ccce78d1e25a7861e22a5dfbdd3b9533d[num27] * ccce78d1e25a7861e22a5dfbdd3b9533d[num27];
								num29 = (c4218a936b59a793594ce8a8da87b52c4[num27, num2] = (num16 * num10 - num11 * num9) / num8);
								if (Math.Abs(num16) > Math.Abs(num11))
								{
									c4218a936b59a793594ce8a8da87b52c4[num27 + 1, num2] = (0f - num9 - num15 * num29) / num16;
								}
								else
								{
									c4218a936b59a793594ce8a8da87b52c4[num27 + 1, num2] = (0f - num10 - num17 * num29) / num11;
								}
							}
							num29 = Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num27, num2]);
							if (num5 * num29 * num29 > 1f)
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
								for (int num31 = num27; num31 <= num2; num31++)
								{
									c4218a936b59a793594ce8a8da87b52c4[num31, num2] /= num29;
								}
							}
						}
						num27--;
						break;
					}
				}
			}
			else if (num8 < 0f)
			{
				int num32 = num2 - 1;
				if (Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1]) > Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2]))
				{
					c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1] = num8 / c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1];
					c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2] = (0f - (c4218a936b59a793594ce8a8da87b52c4[num2, num2] - num7)) / c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1];
				}
				else
				{
					cbb8852c28ceab96cef3608d27898f274(0f, 0f - c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2], c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1] - num7, num8, out var c6cb61f5a9cb051969b780a15ed33cd, out var cdec30e5aa80c0d5064f26d64b3af);
					c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2 - 1] = c6cb61f5a9cb051969b780a15ed33cd;
					c4218a936b59a793594ce8a8da87b52c4[num2 - 1, num2] = cdec30e5aa80c0d5064f26d64b3af;
				}
				c4218a936b59a793594ce8a8da87b52c4[num2, num2 - 1] = 0f;
				c4218a936b59a793594ce8a8da87b52c4[num2, num2] = 1f;
				for (int num33 = num2 - 2; num33 >= 0; num33--)
				{
					float num34 = 0f;
					float num35 = 0f;
					for (int num36 = num32; num36 <= num2; num36++)
					{
						num34 += c4218a936b59a793594ce8a8da87b52c4[num33, num36] * c4218a936b59a793594ce8a8da87b52c4[num36, num2 - 1];
						num35 += c4218a936b59a793594ce8a8da87b52c4[num33, num36] * c4218a936b59a793594ce8a8da87b52c4[num36, num2];
					}
					float num15 = c4218a936b59a793594ce8a8da87b52c4[num33, num33] - num7;
					if (ccce78d1e25a7861e22a5dfbdd3b9533d[num33] < 0f)
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
						num11 = num15;
						num9 = num34;
						num10 = num35;
					}
					else
					{
						num32 = num33;
						if (ccce78d1e25a7861e22a5dfbdd3b9533d[num33] == 0f)
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
							cbb8852c28ceab96cef3608d27898f274(0f - num34, 0f - num35, num15, num8, out var c6cb61f5a9cb051969b780a15ed33cd2, out var cdec30e5aa80c0d5064f26d64b3af2);
							c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1] = c6cb61f5a9cb051969b780a15ed33cd2;
							c4218a936b59a793594ce8a8da87b52c4[num33, num2] = cdec30e5aa80c0d5064f26d64b3af2;
						}
						else
						{
							float num16 = c4218a936b59a793594ce8a8da87b52c4[num33, num33 + 1];
							float num17 = c4218a936b59a793594ce8a8da87b52c4[num33 + 1, num33];
							float num37 = (ce8aac9f53c7f93f91b33957b93c750e8[num33] - num7) * (ce8aac9f53c7f93f91b33957b93c750e8[num33] - num7) + ccce78d1e25a7861e22a5dfbdd3b9533d[num33] * ccce78d1e25a7861e22a5dfbdd3b9533d[num33] - num8 * num8;
							float num38 = (ce8aac9f53c7f93f91b33957b93c750e8[num33] - num7) * 2f * num8;
							if ((num37 == 0f) & (num38 == 0f))
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
								num37 = num5 * num12 * (Math.Abs(num15) + Math.Abs(num8) + Math.Abs(num16) + Math.Abs(num17) + Math.Abs(num11));
							}
							cbb8852c28ceab96cef3608d27898f274(num16 * num9 - num11 * num34 + num8 * num35, num16 * num10 - num11 * num35 - num8 * num34, num37, num38, out var c6cb61f5a9cb051969b780a15ed33cd3, out var cdec30e5aa80c0d5064f26d64b3af3);
							c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1] = c6cb61f5a9cb051969b780a15ed33cd3;
							c4218a936b59a793594ce8a8da87b52c4[num33, num2] = cdec30e5aa80c0d5064f26d64b3af3;
							if (Math.Abs(num16) > Math.Abs(num11) + Math.Abs(num8))
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
								c4218a936b59a793594ce8a8da87b52c4[num33 + 1, num2 - 1] = (0f - num34 - num15 * c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1] + num8 * c4218a936b59a793594ce8a8da87b52c4[num33, num2]) / num16;
								c4218a936b59a793594ce8a8da87b52c4[num33 + 1, num2] = (0f - num35 - num15 * c4218a936b59a793594ce8a8da87b52c4[num33, num2] - num8 * c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1]) / num16;
							}
							else
							{
								cbb8852c28ceab96cef3608d27898f274(0f - num9 - num17 * c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1], 0f - num10 - num17 * c4218a936b59a793594ce8a8da87b52c4[num33, num2], num11, num8, out c6cb61f5a9cb051969b780a15ed33cd3, out cdec30e5aa80c0d5064f26d64b3af3);
								c4218a936b59a793594ce8a8da87b52c4[num33 + 1, num2 - 1] = c6cb61f5a9cb051969b780a15ed33cd3;
								c4218a936b59a793594ce8a8da87b52c4[num33 + 1, num2] = cdec30e5aa80c0d5064f26d64b3af3;
							}
						}
						float num39 = Math.Max(Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num33, num2 - 1]), Math.Abs(c4218a936b59a793594ce8a8da87b52c4[num33, num2]));
						if (num5 * num39 * num39 > 1f)
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
					switch (2)
					{
					case 0:
						continue;
					}
					break;
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
			for (int num41 = 0; num41 < num; num41++)
			{
				if (!((num41 < num3) | (num41 > num4)))
				{
					continue;
				}
				for (int num42 = num41; num42 < num; num42++)
				{
					ccbdf70e0565d415a0cffd34ca03ac5e3[num41, num42] = c4218a936b59a793594ce8a8da87b52c4[num41, num42];
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
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				for (int num43 = num - 1; num43 >= num3; num43--)
				{
					for (int num44 = num3; num44 <= num4; num44++)
					{
						num11 = 0f;
						for (int num45 = num3; num45 <= Math.Min(num43, num4); num45++)
						{
							num11 += ccbdf70e0565d415a0cffd34ca03ac5e3[num44, num45] * c4218a936b59a793594ce8a8da87b52c4[num45, num43];
						}
						while (true)
						{
							switch (4)
							{
							case 0:
								break;
							default:
								goto end_IL_1329;
							}
							continue;
							end_IL_1329:
							break;
						}
						ccbdf70e0565d415a0cffd34ca03ac5e3[num44, num43] = num11;
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							goto end_IL_134f;
						}
						continue;
						end_IL_134f:
						break;
					}
				}
				while (true)
				{
					switch (2)
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

	private static void cbb8852c28ceab96cef3608d27898f274(float cb9e93570e4aa83467d0b708a170944f7, float c125eeb3fef00e99f8e81684cfeaa9bd2, float cab3eb2a93f68436c211043a904a94be9, float c509f5274e4fe7380e3e2c04e68b174f5, out float c6cb61f5a9cb051969b780a15ed33cd63, out float cdec30e5aa80c0d5064f26d64b3af2518)
	{
		if (Math.Abs(cab3eb2a93f68436c211043a904a94be9) > Math.Abs(c509f5274e4fe7380e3e2c04e68b174f5))
		{
			float num = c509f5274e4fe7380e3e2c04e68b174f5 / cab3eb2a93f68436c211043a904a94be9;
			float num2 = cab3eb2a93f68436c211043a904a94be9 + num * c509f5274e4fe7380e3e2c04e68b174f5;
			c6cb61f5a9cb051969b780a15ed33cd63 = (cb9e93570e4aa83467d0b708a170944f7 + num * c125eeb3fef00e99f8e81684cfeaa9bd2) / num2;
			cdec30e5aa80c0d5064f26d64b3af2518 = (c125eeb3fef00e99f8e81684cfeaa9bd2 - num * cb9e93570e4aa83467d0b708a170944f7) / num2;
		}
		else
		{
			float num = cab3eb2a93f68436c211043a904a94be9 / c509f5274e4fe7380e3e2c04e68b174f5;
			float num2 = c509f5274e4fe7380e3e2c04e68b174f5 + num * cab3eb2a93f68436c211043a904a94be9;
			c6cb61f5a9cb051969b780a15ed33cd63 = (num * cb9e93570e4aa83467d0b708a170944f7 + c125eeb3fef00e99f8e81684cfeaa9bd2) / num2;
			cdec30e5aa80c0d5064f26d64b3af2518 = (num * c125eeb3fef00e99f8e81684cfeaa9bd2 - cb9e93570e4aa83467d0b708a170944f7) / num2;
		}
	}
}
