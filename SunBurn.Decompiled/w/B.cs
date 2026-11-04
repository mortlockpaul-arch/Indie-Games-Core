using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using _0011;
using _0019;
using SynapseGaming.LightingSystem.Serialization;

namespace w
{
	internal class B
	{
		public enum nNB
		{
			Negative = -1,
			Zero,
			Positive
		}

		internal sealed class nNa
		{
			private B fNB;

			private B fNa;

			public nNa(B modulus)
			{
				fNB = modulus;
				uint num = fNB.fNg << 1;
				fNa = new B(nNB.Positive, num + 1);
				fNa.fNf[num] = 1u;
				fNa /= fNB;
			}

			public void BarrettReduction(B x)
			{
				B b = fNB;
				uint fNg = b.fNg;
				uint num = fNg + 1;
				uint num2 = fNg - 1;
				if (x.fNg >= fNg)
				{
					if (x.fNf.Length < x.fNg)
					{
						throw new IndexOutOfRangeException("x out of range");
					}
					B b2 = new B(nNB.Positive, x.fNg - num2 + fNa.fNg);
					nNf.Multiply(x.fNf, num2, x.fNg - num2, fNa.fNf, 0u, fNa.fNg, b2.fNf, 0u);
					uint fNg2 = ((x.fNg > num) ? num : x.fNg);
					x.fNg = fNg2;
					x._0003a();
					B b3 = new B(nNB.Positive, num);
					nNf.MultiplyMod2p32pmod(b2.fNf, num, b2.fNg - num, b.fNf, 0u, b.fNg, b3.fNf, 0u, num);
					b3._0003a();
					if (b3 <= x)
					{
						nNf.MinusEq(x, b3);
					}
					else
					{
						B b4 = new B(nNB.Positive, num + 1);
						b4.fNf[num] = 1u;
						nNf.MinusEq(b4, b3);
						nNf.PlusEq(x, b4);
					}
					while (x >= b)
					{
						nNf.MinusEq(x, b);
					}
				}
			}

			public B Multiply(B a, B b)
			{
				if (a == 0u || b == 0u)
				{
					return 0;
				}
				if (a > fNB)
				{
					a %= fNB;
				}
				if (b > fNB)
				{
					b %= fNB;
				}
				B b2 = new B(a * b);
				BarrettReduction(b2);
				return b2;
			}

			public B Difference(B a, B b)
			{
				nNB nNB2 = nNf.Compare(a, b);
				B b2;
				switch (nNB2)
				{
				case nNB.Zero:
					return 0;
				case nNB.Positive:
					b2 = a - b;
					break;
				case nNB.Negative:
					b2 = b - a;
					break;
				default:
					throw new Exception();
				}
				if (b2 >= fNB)
				{
					if (b2.fNg >= fNB.fNg << 1)
					{
						b2 %= fNB;
					}
					else
					{
						BarrettReduction(b2);
					}
				}
				if (nNB2 == nNB.Negative)
				{
					b2 = fNB - b2;
				}
				return b2;
			}

			public B Pow(B a, B k)
			{
				B b = new B(1u);
				if (k == 0u)
				{
					return b;
				}
				B b2 = a;
				if (k.TestBit(0))
				{
					b = a;
				}
				for (int l = 1; l < k.BitCount(); l++)
				{
					b2 = Multiply(b2, b2);
					if (k.TestBit(l))
					{
						b = Multiply(b2, b);
					}
				}
				return b;
			}

			public B Pow(uint b, B exp)
			{
				return Pow(new B(b), exp);
			}
		}

		internal sealed class nNg
		{
			private nNg()
			{
			}

			public static uint Inverse(uint n)
			{
				uint num = n;
				uint num2;
				while ((num2 = n * num) != 1)
				{
					num *= 2 - num2;
				}
				return (uint)(0uL - (ulong)num);
			}

			public static B ToMont(B n, B m)
			{
				n._0003a();
				m._0003a();
				n <<= (int)(m.fNg * 32);
				n %= m;
				return n;
			}
		}

		private sealed class nNf
		{
			public static B AddSameSign(B bi1, B bi2)
			{
				uint num = 0u;
				uint[] fNf;
				uint fNg;
				uint[] fNf2;
				uint fNg2;
				if (bi1.fNg < bi2.fNg)
				{
					fNf = bi2.fNf;
					fNg = bi2.fNg;
					fNf2 = bi1.fNf;
					fNg2 = bi1.fNg;
				}
				else
				{
					fNf = bi1.fNf;
					fNg = bi1.fNg;
					fNf2 = bi2.fNf;
					fNg2 = bi2.fNg;
				}
				B b = new B(nNB.Positive, fNg + 1);
				uint[] fNf3 = b.fNf;
				ulong num2 = 0uL;
				do
				{
					num2 = (ulong)((long)fNf[num] + (long)fNf2[num]) + num2;
					fNf3[num] = (uint)num2;
					num2 >>= 32;
				}
				while (++num < fNg2);
				bool flag = num2 != 0;
				if (flag)
				{
					if (num < fNg)
					{
						do
						{
							flag = (fNf3[num] = fNf[num] + 1) == 0;
						}
						while (++num < fNg && flag);
					}
					if (flag)
					{
						fNf3[num] = 1u;
						num = (b.fNg = num + 1);
						return b;
					}
				}
				if (num < fNg)
				{
					do
					{
						fNf3[num] = fNf[num];
					}
					while (++num < fNg);
				}
				b._0003a();
				return b;
			}

			public static B Subtract(B big, B small)
			{
				B b = new B(nNB.Positive, big.fNg);
				uint[] fNf = b.fNf;
				uint[] fNf2 = big.fNf;
				uint[] fNf3 = small.fNf;
				uint num = 0u;
				uint num2 = 0u;
				do
				{
					uint num3 = fNf3[num];
					num2 = ((((num3 += num2) < num2) | ((fNf[num] = fNf2[num] - num3) > ~num3)) ? 1u : 0u);
				}
				while (++num < small.fNg);
				if (num != big.fNg)
				{
					if (num2 == 1)
					{
						do
						{
							fNf[num] = fNf2[num] - 1;
						}
						while (fNf2[num++] == 0 && num < big.fNg);
						if (num == big.fNg)
						{
							goto IL_00c0;
						}
					}
					do
					{
						fNf[num] = fNf2[num];
					}
					while (++num < big.fNg);
				}
				goto IL_00c0;
				IL_00c0:
				b._0003a();
				return b;
			}

			public static void MinusEq(B big, B small)
			{
				uint[] fNf = big.fNf;
				uint[] fNf2 = small.fNf;
				uint num = 0u;
				uint num2 = 0u;
				do
				{
					uint num3 = fNf2[num];
					num2 = ((((num3 += num2) < num2) | ((fNf[num] -= num3) > ~num3)) ? 1u : 0u);
				}
				while (++num < small.fNg);
				if (num != big.fNg && num2 == 1)
				{
					do
					{
						fNf[num]--;
					}
					while (fNf[num++] == 0 && num < big.fNg);
				}
				while (big.fNg != 0 && big.fNf[big.fNg - 1] == 0)
				{
					big.fNg--;
				}
				if (big.fNg == 0)
				{
					big.fNg++;
				}
			}

			public static void PlusEq(B bi1, B bi2)
			{
				uint num = 0u;
				bool flag = false;
				uint[] fNf;
				uint fNg;
				uint[] fNf2;
				uint fNg2;
				if (bi1.fNg < bi2.fNg)
				{
					flag = true;
					fNf = bi2.fNf;
					fNg = bi2.fNg;
					fNf2 = bi1.fNf;
					fNg2 = bi1.fNg;
				}
				else
				{
					fNf = bi1.fNf;
					fNg = bi1.fNg;
					fNf2 = bi2.fNf;
					fNg2 = bi2.fNg;
				}
				uint[] fNf3 = bi1.fNf;
				ulong num2 = 0uL;
				do
				{
					num2 += (ulong)((long)fNf[num] + (long)fNf2[num]);
					fNf3[num] = (uint)num2;
					num2 >>= 32;
				}
				while (++num < fNg2);
				bool flag2 = num2 != 0;
				if (flag2)
				{
					if (num < fNg)
					{
						do
						{
							flag2 = (fNf3[num] = fNf[num] + 1) == 0;
						}
						while (++num < fNg && flag2);
					}
					if (flag2)
					{
						fNf3[num] = 1u;
						num = (bi1.fNg = num + 1);
						return;
					}
				}
				if (flag && num < fNg - 1)
				{
					do
					{
						fNf3[num] = fNf[num];
					}
					while (++num < fNg);
				}
				bi1.fNg = fNg + 1;
				bi1._0003a();
			}

			public static nNB Compare(B bi1, B bi2)
			{
				uint num = bi1.fNg;
				uint num2 = bi2.fNg;
				while (num != 0 && bi1.fNf[num - 1] == 0)
				{
					num--;
				}
				while (num2 != 0 && bi2.fNf[num2 - 1] == 0)
				{
					num2--;
				}
				if (num == 0 && num2 == 0)
				{
					return nNB.Zero;
				}
				if (num < num2)
				{
					return nNB.Negative;
				}
				if (num > num2)
				{
					return nNB.Positive;
				}
				uint num3 = num - 1;
				while (num3 != 0 && bi1.fNf[num3] == bi2.fNf[num3])
				{
					num3--;
				}
				if (bi1.fNf[num3] < bi2.fNf[num3])
				{
					return nNB.Negative;
				}
				if (bi1.fNf[num3] > bi2.fNf[num3])
				{
					return nNB.Positive;
				}
				return nNB.Zero;
			}

			public static uint SingleByteDivideInPlace(B n, uint d)
			{
				ulong num = 0uL;
				uint fNg = n.fNg;
				while (fNg-- != 0)
				{
					num <<= 32;
					num |= n.fNf[fNg];
					n.fNf[fNg] = (uint)(num / d);
					num %= d;
				}
				n._0003a();
				return (uint)num;
			}

			public static uint DwordMod(B n, uint d)
			{
				ulong num = 0uL;
				uint fNg = n.fNg;
				while (fNg-- != 0)
				{
					num <<= 32;
					num |= n.fNf[fNg];
					num %= d;
				}
				return (uint)num;
			}

			public static B DwordDiv(B n, uint d)
			{
				B b = new B(nNB.Positive, n.fNg);
				ulong num = 0uL;
				uint fNg = n.fNg;
				while (fNg-- != 0)
				{
					num <<= 32;
					num |= n.fNf[fNg];
					b.fNf[fNg] = (uint)(num / d);
					num %= d;
				}
				b._0003a();
				return b;
			}

			public static B[] DwordDivMod(B n, uint d)
			{
				B b = new B(nNB.Positive, n.fNg);
				ulong num = 0uL;
				uint fNg = n.fNg;
				while (fNg-- != 0)
				{
					num <<= 32;
					num |= n.fNf[fNg];
					b.fNf[fNg] = (uint)(num / d);
					num %= d;
				}
				b._0003a();
				B b2 = (uint)num;
				return new B[2] { b, b2 };
			}

			public static B[] multiByteDivide(B bi1, B bi2)
			{
				if (Compare(bi1, bi2) == nNB.Negative)
				{
					return new B[2]
					{
						0,
						new B(bi1)
					};
				}
				bi1._0003a();
				bi2._0003a();
				if (bi2.fNg == 1)
				{
					return DwordDivMod(bi1, bi2.fNf[0]);
				}
				uint num = bi1.fNg + 1;
				int num2 = (int)(bi2.fNg + 1);
				uint num3 = 2147483648u;
				uint num4 = bi2.fNf[bi2.fNg - 1];
				int num5 = 0;
				int num6 = (int)(bi1.fNg - bi2.fNg);
				while (num3 != 0 && (num4 & num3) == 0)
				{
					num5++;
					num3 >>= 1;
				}
				B b = new B(nNB.Positive, bi1.fNg - bi2.fNg + 1);
				B b2 = bi1 << num5;
				uint[] fNf = b2.fNf;
				bi2 <<= num5;
				int num7 = (int)(num - bi2.fNg);
				int num8 = (int)(num - 1);
				uint num9 = bi2.fNf[bi2.fNg - 1];
				ulong num10 = bi2.fNf[bi2.fNg - 2];
				while (num7 > 0)
				{
					ulong num11 = ((ulong)fNf[num8] << 32) + fNf[num8 - 1];
					ulong num12 = num11 / num9;
					ulong num13 = num11 % num9;
					while (num12 == 4294967296L || num12 * num10 > (num13 << 32) + fNf[num8 - 2])
					{
						num12--;
						num13 += num9;
						if (num13 >= 4294967296L)
						{
							break;
						}
					}
					uint num14 = 0u;
					int num15 = num8 - num2 + 1;
					ulong num16 = 0uL;
					uint num17 = (uint)num12;
					do
					{
						num16 += (ulong)((long)bi2.fNf[num14] * (long)num17);
						uint num18 = fNf[num15];
						fNf[num15] -= (uint)(int)num16;
						num16 >>= 32;
						if (fNf[num15] > num18)
						{
							num16++;
						}
						num14++;
						num15++;
					}
					while (num14 < num2);
					num15 = num8 - num2 + 1;
					num14 = 0u;
					if (num16 != 0)
					{
						num17--;
						ulong num19 = 0uL;
						do
						{
							num19 = (ulong)((long)fNf[num15] + (long)bi2.fNf[num14]) + num19;
							fNf[num15] = (uint)num19;
							num19 >>= 32;
							num14++;
							num15++;
						}
						while (num14 < num2);
					}
					b.fNf[num6--] = num17;
					num8--;
					num7--;
				}
				b._0003a();
				b2._0003a();
				B[] array = new B[2] { b, b2 };
				if (num5 != 0)
				{
					B[] array2;
					(array2 = array)[1] = array2[1] >> num5;
				}
				return array;
			}

			public static B LeftShift(B bi, int n)
			{
				if (n == 0)
				{
					return new B(bi, bi.fNg + 1);
				}
				int num = n >> 5;
				n &= 0x1F;
				B b = new B(nNB.Positive, bi.fNg + 1 + (uint)num);
				uint num2 = 0u;
				uint fNg = bi.fNg;
				if (n != 0)
				{
					uint num3 = 0u;
					for (; num2 < fNg; num2++)
					{
						uint num4 = bi.fNf[num2];
						b.fNf[num2 + num] = (num4 << n) | num3;
						num3 = num4 >> 32 - n;
					}
					b.fNf[num2 + num] = num3;
				}
				else
				{
					for (; num2 < fNg; num2++)
					{
						b.fNf[num2 + num] = bi.fNf[num2];
					}
				}
				b._0003a();
				return b;
			}

			public static B RightShift(B bi, int n)
			{
				if (n == 0)
				{
					return new B(bi);
				}
				int num = n >> 5;
				int num2 = n & 0x1F;
				B b = new B(nNB.Positive, (uint)((int)bi.fNg - num + 1));
				uint num3 = (uint)(b.fNf.Length - 1);
				if (num2 != 0)
				{
					uint num4 = 0u;
					while (num3-- != 0)
					{
						uint num5 = bi.fNf[num3 + num];
						b.fNf[num3] = (num5 >> n) | num4;
						num4 = num5 << 32 - n;
					}
				}
				else
				{
					while (num3-- != 0)
					{
						b.fNf[num3] = bi.fNf[num3 + num];
					}
				}
				b._0003a();
				return b;
			}

			public static B MultiplyByDword(B n, uint f)
			{
				B b = new B(nNB.Positive, n.fNg + 1);
				uint num = 0u;
				ulong num2 = 0uL;
				do
				{
					num2 += (ulong)((long)n.fNf[num] * (long)f);
					b.fNf[num] = (uint)num2;
					num2 >>= 32;
				}
				while (++num < n.fNg);
				b.fNf[num] = (uint)num2;
				b._0003a();
				return b;
			}

			public static void Multiply(uint[] x, uint xOffset, uint xLen, uint[] y, uint yOffset, uint yLen, uint[] d, uint dOffset)
			{
				uint num = xOffset + xLen;
				uint num2 = yOffset + yLen;
				uint num3 = dOffset;
				uint num4 = xOffset;
				while (num4 < num)
				{
					ulong num5 = x[num4];
					if (num5 != 0)
					{
						ulong num6 = 0uL;
						uint num7 = num3;
						uint num8 = yOffset;
						while (num8 < num2)
						{
							num6 += num5 * y[num8] + d[num7];
							d[num7] = (uint)num6;
							num6 >>= 32;
							num8++;
							num7++;
						}
						if (num6 != 0)
						{
							d[num7] = (uint)num6;
						}
					}
					num4++;
					num3++;
				}
			}

			public static void MultiplyMod2p32pmod(uint[] x, uint xOffset, uint xLen, uint[] y, uint yOffset, uint yLen, uint[] d, uint dOffset, uint mod)
			{
				uint num = xOffset + xLen;
				uint num2 = yOffset + yLen;
				uint num3 = dOffset;
				uint num4 = num3 + mod;
				uint num5 = xOffset;
				while (num5 < num)
				{
					ulong num6 = x[num5];
					if (num6 != 0)
					{
						ulong num7 = 0uL;
						uint num8 = num3;
						uint num9 = yOffset;
						while (num9 < num2 && num8 < num4)
						{
							num7 += num6 * y[num9] + d[num8];
							d[num8] = (uint)num7;
							num7 >>= 32;
							num9++;
							num8++;
						}
						if (num7 != 0 && num8 < num4)
						{
							d[num8] = (uint)num7;
						}
					}
					num5++;
					num3++;
				}
			}

			public static B gcd(B a, B b)
			{
				B b2 = a;
				B b3 = b;
				B b4 = b3;
				while (b2.fNg > 1)
				{
					b4 = b2;
					b2 = b3 % b2;
					b3 = b4;
				}
				if (b2 == 0u)
				{
					return b4;
				}
				uint num = b2.fNf[0];
				uint num2 = b3 % num;
				int num3 = 0;
				while (((num2 | num) & 1) == 0)
				{
					num2 >>= 1;
					num >>= 1;
					num3++;
				}
				while (num2 != 0)
				{
					while ((num2 & 1) == 0)
					{
						num2 >>= 1;
					}
					while ((num & 1) == 0)
					{
						num >>= 1;
					}
					if (num2 >= num)
					{
						num2 = num2 - num >> 1;
					}
					else
					{
						num = num - num2 >> 1;
					}
				}
				return num << num3;
			}

			public static uint modInverse(B bi, uint modulus)
			{
				uint num = modulus;
				uint num2 = bi % modulus;
				uint num3 = 0u;
				uint num4 = 1u;
				while (true)
				{
					switch (num2)
					{
					case 1u:
						return num4;
					default:
						num3 += num / num2 * num4;
						num %= num2;
						switch (num)
						{
						case 1u:
							return modulus - num3;
						default:
							goto IL_002d;
						case 0u:
							break;
						}
						break;
					case 0u:
						break;
					}
					break;
					IL_002d:
					num4 += num2 / num * num3;
					num2 %= num;
				}
				return 0u;
			}

			public static B modInverse(B bi, B modulus)
			{
				if (modulus.fNg == 1)
				{
					return modInverse(bi, modulus.fNf[0]);
				}
				B[] array = new B[2] { 0, 1 };
				B[] array2 = new B[2];
				B[] array3 = new B[2] { 0, 0 };
				int num = 0;
				B bi2 = modulus;
				B b = bi;
				nNa nNa2 = new nNa(modulus);
				while (b != 0u)
				{
					if (num > 1)
					{
						B b2 = nNa2.Difference(array[0], array[1] * array2[0]);
						array[0] = array[1];
						array[1] = b2;
					}
					B[] array4 = multiByteDivide(bi2, b);
					array2[0] = array2[1];
					array2[1] = array4[0];
					array3[0] = array3[1];
					array3[1] = array4[1];
					bi2 = b;
					b = array4[1];
					num++;
				}
				if (array3[0] != 1u)
				{
					throw new ArithmeticException("No inverse!");
				}
				return nNa2.Difference(array[0], array[1] * array2[0]);
			}
		}

		private const uint fNB = 20u;

		private const string fNa = "Operation would return a negative value";

		private uint fNg = 1u;

		private uint[] fNf;

		internal static readonly uint[] fN_0003 = new uint[783]
		{
			2u, 3u, 5u, 7u, 11u, 13u, 17u, 19u, 23u, 29u,
			31u, 37u, 41u, 43u, 47u, 53u, 59u, 61u, 67u, 71u,
			73u, 79u, 83u, 89u, 97u, 101u, 103u, 107u, 109u, 113u,
			127u, 131u, 137u, 139u, 149u, 151u, 157u, 163u, 167u, 173u,
			179u, 181u, 191u, 193u, 197u, 199u, 211u, 223u, 227u, 229u,
			233u, 239u, 241u, 251u, 257u, 263u, 269u, 271u, 277u, 281u,
			283u, 293u, 307u, 311u, 313u, 317u, 331u, 337u, 347u, 349u,
			353u, 359u, 367u, 373u, 379u, 383u, 389u, 397u, 401u, 409u,
			419u, 421u, 431u, 433u, 439u, 443u, 449u, 457u, 461u, 463u,
			467u, 479u, 487u, 491u, 499u, 503u, 509u, 521u, 523u, 541u,
			547u, 557u, 563u, 569u, 571u, 577u, 587u, 593u, 599u, 601u,
			607u, 613u, 617u, 619u, 631u, 641u, 643u, 647u, 653u, 659u,
			661u, 673u, 677u, 683u, 691u, 701u, 709u, 719u, 727u, 733u,
			739u, 743u, 751u, 757u, 761u, 769u, 773u, 787u, 797u, 809u,
			811u, 821u, 823u, 827u, 829u, 839u, 853u, 857u, 859u, 863u,
			877u, 881u, 883u, 887u, 907u, 911u, 919u, 929u, 937u, 941u,
			947u, 953u, 967u, 971u, 977u, 983u, 991u, 997u, 1009u, 1013u,
			1019u, 1021u, 1031u, 1033u, 1039u, 1049u, 1051u, 1061u, 1063u, 1069u,
			1087u, 1091u, 1093u, 1097u, 1103u, 1109u, 1117u, 1123u, 1129u, 1151u,
			1153u, 1163u, 1171u, 1181u, 1187u, 1193u, 1201u, 1213u, 1217u, 1223u,
			1229u, 1231u, 1237u, 1249u, 1259u, 1277u, 1279u, 1283u, 1289u, 1291u,
			1297u, 1301u, 1303u, 1307u, 1319u, 1321u, 1327u, 1361u, 1367u, 1373u,
			1381u, 1399u, 1409u, 1423u, 1427u, 1429u, 1433u, 1439u, 1447u, 1451u,
			1453u, 1459u, 1471u, 1481u, 1483u, 1487u, 1489u, 1493u, 1499u, 1511u,
			1523u, 1531u, 1543u, 1549u, 1553u, 1559u, 1567u, 1571u, 1579u, 1583u,
			1597u, 1601u, 1607u, 1609u, 1613u, 1619u, 1621u, 1627u, 1637u, 1657u,
			1663u, 1667u, 1669u, 1693u, 1697u, 1699u, 1709u, 1721u, 1723u, 1733u,
			1741u, 1747u, 1753u, 1759u, 1777u, 1783u, 1787u, 1789u, 1801u, 1811u,
			1823u, 1831u, 1847u, 1861u, 1867u, 1871u, 1873u, 1877u, 1879u, 1889u,
			1901u, 1907u, 1913u, 1931u, 1933u, 1949u, 1951u, 1973u, 1979u, 1987u,
			1993u, 1997u, 1999u, 2003u, 2011u, 2017u, 2027u, 2029u, 2039u, 2053u,
			2063u, 2069u, 2081u, 2083u, 2087u, 2089u, 2099u, 2111u, 2113u, 2129u,
			2131u, 2137u, 2141u, 2143u, 2153u, 2161u, 2179u, 2203u, 2207u, 2213u,
			2221u, 2237u, 2239u, 2243u, 2251u, 2267u, 2269u, 2273u, 2281u, 2287u,
			2293u, 2297u, 2309u, 2311u, 2333u, 2339u, 2341u, 2347u, 2351u, 2357u,
			2371u, 2377u, 2381u, 2383u, 2389u, 2393u, 2399u, 2411u, 2417u, 2423u,
			2437u, 2441u, 2447u, 2459u, 2467u, 2473u, 2477u, 2503u, 2521u, 2531u,
			2539u, 2543u, 2549u, 2551u, 2557u, 2579u, 2591u, 2593u, 2609u, 2617u,
			2621u, 2633u, 2647u, 2657u, 2659u, 2663u, 2671u, 2677u, 2683u, 2687u,
			2689u, 2693u, 2699u, 2707u, 2711u, 2713u, 2719u, 2729u, 2731u, 2741u,
			2749u, 2753u, 2767u, 2777u, 2789u, 2791u, 2797u, 2801u, 2803u, 2819u,
			2833u, 2837u, 2843u, 2851u, 2857u, 2861u, 2879u, 2887u, 2897u, 2903u,
			2909u, 2917u, 2927u, 2939u, 2953u, 2957u, 2963u, 2969u, 2971u, 2999u,
			3001u, 3011u, 3019u, 3023u, 3037u, 3041u, 3049u, 3061u, 3067u, 3079u,
			3083u, 3089u, 3109u, 3119u, 3121u, 3137u, 3163u, 3167u, 3169u, 3181u,
			3187u, 3191u, 3203u, 3209u, 3217u, 3221u, 3229u, 3251u, 3253u, 3257u,
			3259u, 3271u, 3299u, 3301u, 3307u, 3313u, 3319u, 3323u, 3329u, 3331u,
			3343u, 3347u, 3359u, 3361u, 3371u, 3373u, 3389u, 3391u, 3407u, 3413u,
			3433u, 3449u, 3457u, 3461u, 3463u, 3467u, 3469u, 3491u, 3499u, 3511u,
			3517u, 3527u, 3529u, 3533u, 3539u, 3541u, 3547u, 3557u, 3559u, 3571u,
			3581u, 3583u, 3593u, 3607u, 3613u, 3617u, 3623u, 3631u, 3637u, 3643u,
			3659u, 3671u, 3673u, 3677u, 3691u, 3697u, 3701u, 3709u, 3719u, 3727u,
			3733u, 3739u, 3761u, 3767u, 3769u, 3779u, 3793u, 3797u, 3803u, 3821u,
			3823u, 3833u, 3847u, 3851u, 3853u, 3863u, 3877u, 3881u, 3889u, 3907u,
			3911u, 3917u, 3919u, 3923u, 3929u, 3931u, 3943u, 3947u, 3967u, 3989u,
			4001u, 4003u, 4007u, 4013u, 4019u, 4021u, 4027u, 4049u, 4051u, 4057u,
			4073u, 4079u, 4091u, 4093u, 4099u, 4111u, 4127u, 4129u, 4133u, 4139u,
			4153u, 4157u, 4159u, 4177u, 4201u, 4211u, 4217u, 4219u, 4229u, 4231u,
			4241u, 4243u, 4253u, 4259u, 4261u, 4271u, 4273u, 4283u, 4289u, 4297u,
			4327u, 4337u, 4339u, 4349u, 4357u, 4363u, 4373u, 4391u, 4397u, 4409u,
			4421u, 4423u, 4441u, 4447u, 4451u, 4457u, 4463u, 4481u, 4483u, 4493u,
			4507u, 4513u, 4517u, 4519u, 4523u, 4547u, 4549u, 4561u, 4567u, 4583u,
			4591u, 4597u, 4603u, 4621u, 4637u, 4639u, 4643u, 4649u, 4651u, 4657u,
			4663u, 4673u, 4679u, 4691u, 4703u, 4721u, 4723u, 4729u, 4733u, 4751u,
			4759u, 4783u, 4787u, 4789u, 4793u, 4799u, 4801u, 4813u, 4817u, 4831u,
			4861u, 4871u, 4877u, 4889u, 4903u, 4909u, 4919u, 4931u, 4933u, 4937u,
			4943u, 4951u, 4957u, 4967u, 4969u, 4973u, 4987u, 4993u, 4999u, 5003u,
			5009u, 5011u, 5021u, 5023u, 5039u, 5051u, 5059u, 5077u, 5081u, 5087u,
			5099u, 5101u, 5107u, 5113u, 5119u, 5147u, 5153u, 5167u, 5171u, 5179u,
			5189u, 5197u, 5209u, 5227u, 5231u, 5233u, 5237u, 5261u, 5273u, 5279u,
			5281u, 5297u, 5303u, 5309u, 5323u, 5333u, 5347u, 5351u, 5381u, 5387u,
			5393u, 5399u, 5407u, 5413u, 5417u, 5419u, 5431u, 5437u, 5441u, 5443u,
			5449u, 5471u, 5477u, 5479u, 5483u, 5501u, 5503u, 5507u, 5519u, 5521u,
			5527u, 5531u, 5557u, 5563u, 5569u, 5573u, 5581u, 5591u, 5623u, 5639u,
			5641u, 5647u, 5651u, 5653u, 5657u, 5659u, 5669u, 5683u, 5689u, 5693u,
			5701u, 5711u, 5717u, 5737u, 5741u, 5743u, 5749u, 5779u, 5783u, 5791u,
			5801u, 5807u, 5813u, 5821u, 5827u, 5839u, 5843u, 5849u, 5851u, 5857u,
			5861u, 5867u, 5869u, 5879u, 5881u, 5897u, 5903u, 5923u, 5927u, 5939u,
			5953u, 5981u, 5987u
		};

		private static _0011._0012 fNn;

		private static _0011._0012 Rng
		{
			get
			{
				if (fNn == null)
				{
					fNn = _0011._0012.Create();
				}
				return fNn;
			}
		}

		public B()
		{
			fNf = new uint[20];
			fNg = 20u;
		}

		public B(nNB sign, uint len)
		{
			fNf = new uint[len];
			fNg = len;
		}

		public B(B bi)
		{
			fNf = (uint[])bi.fNf.Clone();
			fNg = bi.fNg;
		}

		public B(B bi, uint len)
		{
			fNf = new uint[len];
			for (uint num = 0u; num < bi.fNg; num++)
			{
				fNf[num] = bi.fNf[num];
			}
			fNg = bi.fNg;
		}

		public B(byte[] inData)
		{
			fNg = (uint)inData.Length >> 2;
			int num = inData.Length & 3;
			if (num != 0)
			{
				fNg++;
			}
			fNf = new uint[fNg];
			int num2 = inData.Length - 1;
			int num3 = 0;
			while (num2 >= 3)
			{
				fNf[num3] = (uint)((inData[num2 - 3] << 24) | (inData[num2 - 2] << 16) | (inData[num2 - 1] << 8) | inData[num2]);
				num2 -= 4;
				num3++;
			}
			switch (num)
			{
			case 1:
				fNf[fNg - 1] = inData[0];
				break;
			case 2:
				fNf[fNg - 1] = (uint)((inData[0] << 8) | inData[1]);
				break;
			case 3:
				fNf[fNg - 1] = (uint)((inData[0] << 16) | (inData[1] << 8) | inData[2]);
				break;
			}
			_0003a();
		}

		public B(uint[] inData)
		{
			fNg = (uint)inData.Length;
			fNf = new uint[fNg];
			int num = (int)(fNg - 1);
			int num2 = 0;
			while (num >= 0)
			{
				fNf[num2] = inData[num];
				num--;
				num2++;
			}
			_0003a();
		}

		public B(uint ui)
		{
			fNf = new uint[1] { ui };
		}

		public B(ulong ul)
		{
			fNf = new uint[2]
			{
				(uint)ul,
				(uint)(ul >> 32)
			};
			fNg = 2u;
			_0003a();
		}

		public static implicit operator B(uint value)
		{
			return new B(value);
		}

		public static implicit operator B(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			return new B((uint)value);
		}

		public static implicit operator B(ulong value)
		{
			return new B(value);
		}

		public static B Parse(string number)
		{
			if (number == null)
			{
				throw new ArgumentNullException("number");
			}
			int k = 0;
			int length = number.Length;
			bool flag = false;
			B b = new B(0u);
			if (number[k] == '+')
			{
				k++;
			}
			else if (number[k] == '-')
			{
				throw new FormatException("Operation would return a negative value");
			}
			for (; k < length; k++)
			{
				char c2 = number[k];
				switch (c2)
				{
				case '\0':
					k = length;
					continue;
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					b = b * 10 + (c2 - 48);
					flag = true;
					continue;
				}
				if (char.IsWhiteSpace(c2))
				{
					for (k++; k < length; k++)
					{
						if (!char.IsWhiteSpace(number[k]))
						{
							throw new FormatException();
						}
					}
					break;
				}
				throw new FormatException();
			}
			if (!flag)
			{
				throw new FormatException();
			}
			return b;
		}

		public static B operator +(B bi1, B bi2)
		{
			if (bi1 == 0u)
			{
				return new B(bi2);
			}
			if (bi2 == 0u)
			{
				return new B(bi1);
			}
			return nNf.AddSameSign(bi1, bi2);
		}

		public static B operator -(B bi1, B bi2)
		{
			if (bi2 == 0u)
			{
				return new B(bi1);
			}
			if (bi1 == 0u)
			{
				throw new ArithmeticException("Operation would return a negative value");
			}
			return nNf.Compare(bi1, bi2) switch
			{
				nNB.Zero => 0, 
				nNB.Positive => nNf.Subtract(bi1, bi2), 
				nNB.Negative => throw new ArithmeticException("Operation would return a negative value"), 
				_ => throw new Exception(), 
			};
		}

		public static int operator %(B bi, int i)
		{
			if (i > 0)
			{
				return (int)nNf.DwordMod(bi, (uint)i);
			}
			return (int)(0 - nNf.DwordMod(bi, (uint)(-i)));
		}

		public static uint operator %(B bi, uint ui)
		{
			return nNf.DwordMod(bi, ui);
		}

		public static B operator %(B bi1, B bi2)
		{
			return nNf.multiByteDivide(bi1, bi2)[1];
		}

		public static B operator /(B bi, int i)
		{
			if (i > 0)
			{
				return nNf.DwordDiv(bi, (uint)i);
			}
			throw new ArithmeticException("Operation would return a negative value");
		}

		public static B operator /(B bi1, B bi2)
		{
			return nNf.multiByteDivide(bi1, bi2)[0];
		}

		public static B operator *(B bi1, B bi2)
		{
			if (bi1 == 0u || bi2 == 0u)
			{
				return 0;
			}
			if (bi1.fNf.Length < bi1.fNg)
			{
				throw new IndexOutOfRangeException("bi1 out of range");
			}
			if (bi2.fNf.Length < bi2.fNg)
			{
				throw new IndexOutOfRangeException("bi2 out of range");
			}
			B b = new B(nNB.Positive, bi1.fNg + bi2.fNg);
			nNf.Multiply(bi1.fNf, 0u, bi1.fNg, bi2.fNf, 0u, bi2.fNg, b.fNf, 0u);
			b._0003a();
			return b;
		}

		public static B operator *(B bi, int i)
		{
			if (i < 0)
			{
				throw new ArithmeticException("Operation would return a negative value");
			}
			return i switch
			{
				0 => 0, 
				1 => new B(bi), 
				_ => nNf.MultiplyByDword(bi, (uint)i), 
			};
		}

		public static B operator <<(B bi1, int shiftVal)
		{
			return nNf.LeftShift(bi1, shiftVal);
		}

		public static B operator >>(B bi1, int shiftVal)
		{
			return nNf.RightShift(bi1, shiftVal);
		}

		public static B Add(B bi1, B bi2)
		{
			return bi1 + bi2;
		}

		public static B Subtract(B bi1, B bi2)
		{
			return bi1 - bi2;
		}

		public static int Modulus(B bi, int i)
		{
			return bi % i;
		}

		public static uint Modulus(B bi, uint ui)
		{
			return bi % ui;
		}

		public static B Modulus(B bi1, B bi2)
		{
			return bi1 % bi2;
		}

		public static B Divid(B bi, int i)
		{
			return bi / i;
		}

		public static B Divid(B bi1, B bi2)
		{
			return bi1 / bi2;
		}

		public static B Multiply(B bi1, B bi2)
		{
			return bi1 * bi2;
		}

		public static B Multiply(B bi, int i)
		{
			return bi * i;
		}

		public static B GenerateRandom(int bits, _0011._0012 rng)
		{
			int num = bits >> 5;
			int num2 = bits & 0x1F;
			if (num2 != 0)
			{
				num++;
			}
			B b = new B(nNB.Positive, (uint)(num + 1));
			byte[] array = new byte[num << 2];
			rng.GetBytes(array);
			Buffer.BlockCopy(array, 0, b.fNf, 0, num << 2);
			if (num2 != 0)
			{
				uint num3 = (uint)(1 << num2 - 1);
				b.fNf[num - 1] |= num3;
				num3 = uint.MaxValue >> 32 - num2;
				b.fNf[num - 1] &= num3;
			}
			else
			{
				b.fNf[num - 1] |= 2147483648u;
			}
			b._0003a();
			return b;
		}

		public static B GenerateRandom(int bits)
		{
			return GenerateRandom(bits, Rng);
		}

		public int BitCount()
		{
			_0003a();
			uint num = fNf[fNg - 1];
			uint num2 = 2147483648u;
			uint num3 = 32u;
			while (num3 != 0 && (num & num2) == 0)
			{
				num3--;
				num2 >>= 1;
			}
			return (int)(num3 + (fNg - 1 << 5));
		}

		public bool TestBit(uint bitNum)
		{
			uint num = bitNum >> 5;
			byte b = (byte)(bitNum & 0x1F);
			uint num2 = (uint)(1 << (int)b);
			return (fNf[num] & num2) != 0;
		}

		public bool TestBit(int bitNum)
		{
			if (bitNum < 0)
			{
				throw new IndexOutOfRangeException("bitNum out of range");
			}
			uint num = (uint)bitNum >> 5;
			byte b = (byte)(bitNum & 0x1F);
			uint num2 = (uint)(1 << (int)b);
			return (fNf[num] | num2) == fNf[num];
		}

		public void SetBit(uint bitNum)
		{
			SetBit(bitNum, value: true);
		}

		public void ClearBit(uint bitNum)
		{
			SetBit(bitNum, value: false);
		}

		public void SetBit(uint bitNum, bool value)
		{
			uint num = bitNum >> 5;
			if (num < fNg)
			{
				uint num2 = (uint)(1 << (int)(bitNum & 0x1F));
				if (value)
				{
					fNf[num] |= num2;
				}
				else
				{
					fNf[num] &= ~num2;
				}
			}
		}

		public int LowestSetBit()
		{
			if (this == 0u)
			{
				return -1;
			}
			int k;
			for (k = 0; !TestBit(k); k++)
			{
			}
			return k;
		}

		public byte[] GetBytes()
		{
			if (this == 0u)
			{
				return new byte[1];
			}
			int num = BitCount();
			int num2 = num >> 3;
			if ((num & 7) != 0)
			{
				num2++;
			}
			byte[] array = new byte[num2];
			int num3 = num2 & 3;
			if (num3 == 0)
			{
				num3 = 4;
			}
			int num4 = 0;
			for (int num5 = (int)(fNg - 1); num5 >= 0; num5--)
			{
				uint num6 = fNf[num5];
				for (int num7 = num3 - 1; num7 >= 0; num7--)
				{
					array[num4 + num7] = (byte)(num6 & 0xFF);
					num6 >>= 8;
				}
				num4 += num3;
				num3 = 4;
			}
			return array;
		}

		public static bool operator ==(B bi1, uint ui)
		{
			if (bi1.fNg != 1)
			{
				bi1._0003a();
			}
			if (bi1.fNg == 1)
			{
				return bi1.fNf[0] == ui;
			}
			return false;
		}

		public static bool operator !=(B bi1, uint ui)
		{
			if (bi1.fNg != 1)
			{
				bi1._0003a();
			}
			if (bi1.fNg == 1)
			{
				return bi1.fNf[0] != ui;
			}
			return true;
		}

		public static bool operator ==(B bi1, B bi2)
		{
			if ((object)bi1 == bi2)
			{
				return true;
			}
			if (null == bi1 || null == bi2)
			{
				return false;
			}
			return nNf.Compare(bi1, bi2) == nNB.Zero;
		}

		public static bool operator !=(B bi1, B bi2)
		{
			if ((object)bi1 == bi2)
			{
				return false;
			}
			if (null == bi1 || null == bi2)
			{
				return true;
			}
			return nNf.Compare(bi1, bi2) != nNB.Zero;
		}

		public static bool operator >(B bi1, B bi2)
		{
			return nNf.Compare(bi1, bi2) > nNB.Zero;
		}

		public static bool operator <(B bi1, B bi2)
		{
			return nNf.Compare(bi1, bi2) < nNB.Zero;
		}

		public static bool operator >=(B bi1, B bi2)
		{
			return nNf.Compare(bi1, bi2) >= nNB.Zero;
		}

		public static bool operator <=(B bi1, B bi2)
		{
			return nNf.Compare(bi1, bi2) <= nNB.Zero;
		}

		public nNB Compare(B bi)
		{
			return nNf.Compare(this, bi);
		}

		public string ToString(uint radix)
		{
			return ToString(radix, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
		}

		public string ToString(uint radix, string characterSet)
		{
			if (characterSet.Length < radix)
			{
				throw new ArgumentException("charSet length less than radix", "characterSet");
			}
			if (radix == 1)
			{
				throw new ArgumentException("There is no such thing as radix one notation", "radix");
			}
			if (this == 0u)
			{
				return "0";
			}
			if (this == 1u)
			{
				return "1";
			}
			string text = "";
			B b = new B(this);
			while (b != 0u)
			{
				uint index = nNf.SingleByteDivideInPlace(b, radix);
				text = characterSet[(int)index] + text;
			}
			return text;
		}

		private void _0003a()
		{
			while (fNg != 0 && fNf[fNg - 1] == 0)
			{
				fNg--;
			}
			if (fNg == 0)
			{
				fNg++;
			}
		}

		public void Clear()
		{
			for (int k = 0; k < fNg; k++)
			{
				fNf[k] = 0u;
			}
		}

		public override int GetHashCode()
		{
			uint num = 0u;
			for (uint num2 = 0u; num2 < fNg; num2++)
			{
				num ^= fNf[num2];
			}
			return (int)num;
		}

		public override string ToString()
		{
			return ToString(10u);
		}

		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			if (o is int)
			{
				if ((int)o >= 0)
				{
					return this == (uint)o;
				}
				return false;
			}
			B b = o as B;
			if (b == null)
			{
				return false;
			}
			return nNf.Compare(this, b) == nNB.Zero;
		}

		public B GCD(B bi)
		{
			return nNf.gcd(this, bi);
		}

		public B ModInverse(B modulus)
		{
			return nNf.modInverse(this, modulus);
		}

		public B ModPow(B exp, B n)
		{
			nNa nNa2 = new nNa(n);
			return nNa2.Pow(this, exp);
		}
	}
}
namespace W
{
	internal class B
	{
		private StreamingContext fNB = default;

		private FormatterConverter fNa = new FormatterConverter();

		private Dictionary<string, int> fNg = new Dictionary<string, int>(16);

		private f fNf = new f();

		internal object _0003N(_0019.n P_0)
		{
			fNf._0013();
			_0019._0003 documentElement = P_0.DocumentElement;
			if (documentElement.Name == "root")
			{
				_0019.a childNodes = documentElement.ChildNodes;
				int count = childNodes.Count;
				for (int k = 0; k < count; k++)
				{
					_0019.B b = childNodes[k];
					if (b.Name != "classes")
					{
						continue;
					}
					_0019.a childNodes2 = b.ChildNodes;
					int count2 = childNodes2.Count;
					for (int l = 0; l < count2; l++)
					{
						if (childNodes2[l] is _0019._0003 obj)
						{
							Type type = nB(obj);
							fNf.k(obj.Name, type);
						}
					}
				}
				fNf.nn();
				for (int m = 0; m < count; m++)
				{
					_0019.B b2 = childNodes[m];
					if (!(b2.Name == "classes") && b2 is _0019._0003)
					{
						return _0003N(b2 as _0019._0003);
					}
				}
			}
			return null;
		}

		private Type nB(_0019._0003 P_0)
		{
			string name = P_0.Name;
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			_0019.g g2 = (_0019.g)P_0.Attributes.GetNamedItem("fullname");
			_0019.g g3 = (_0019.g)P_0.Attributes.GetNamedItem("assembly");
			_0019.g g4 = (_0019.g)P_0.Attributes.GetNamedItem("fullyqualifiedname");
			if (g2 == null || g3 == null)
			{
				return null;
			}
			string innerText = g2.InnerText;
			string innerText2 = g3.InnerText;
			string text = "";
			if (g4 != null)
			{
				text = g4.InnerText;
			}
			Type type = fNf.n_0019(innerText, innerText2, text);
			_0019.a childNodes = P_0.ChildNodes;
			int count = childNodes.Count;
			if ((object)type == null || count < 1)
			{
				return type;
			}
			Type[] array = new Type[count];
			for (int k = 0; k < array.Length; k++)
			{
				if (childNodes[k] is _0019._0003 obj)
				{
					array[k] = nB(obj);
				}
			}
			return type.MakeGenericType(array);
		}

		private object _0003N(_0019._0003 P_0)
		{
			if (P_0 == null || string.IsNullOrEmpty(P_0.Name))
			{
				return null;
			}
			fNf.nL(P_0.Name, out var type, out var array);
			object obj = null;
			ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			ConstructorInfo[] array2 = constructors;
			foreach (ConstructorInfo constructorInfo in array2)
			{
				ParameterInfo[] parameters = constructorInfo.GetParameters();
				if (parameters == null || parameters.Length <= 0)
				{
					obj = constructorInfo.Invoke(null);
					break;
				}
			}
			if (obj == null)
			{
				obj = Activator.CreateInstance(type);
			}
			if (obj == null)
			{
				throw new Exception($"Cannot create an instance of type '{type.FullName}'.");
			}
			SerializationInfo serializationInfo = new SerializationInfo(type, fNa);
			foreach (_0019.B childNode in P_0.ChildNodes)
			{
				if (!(childNode is _0019._0003) || string.IsNullOrEmpty(childNode.Name))
				{
					continue;
				}
				bool flag = false;
				foreach (_0019.B childNode2 in childNode.ChildNodes)
				{
					if (childNode2 is _0019._0003)
					{
						serializationInfo.AddValue(childNode.Name, _0003N(childNode2 as _0019._0003));
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					string name = XmlConvert.DecodeName(childNode.Name);
					serializationInfo.AddValue(name, childNode.InnerText);
				}
			}
			if (obj is ICollection)
			{
				IList list = obj as IList;
				IDictionary dictionary = obj as IDictionary;
				foreach (SerializationEntry item in serializationInfo)
				{
					if (list != null)
					{
						list.Add(item.Value);
					}
					else if (dictionary != null)
					{
						object value = item.Value;
						Type type2 = value.GetType();
						PropertyInfo property = type2.GetProperty("Key");
						PropertyInfo property2 = type2.GetProperty("Value");
						if ((object)property == null || (object)property2 == null)
						{
							throw new Exception("Object inserted into Dictionary member is not a KeyValuePair type.");
						}
						object value2 = property.GetValue(value, null);
						object value3 = property2.GetValue(value, null);
						dictionary.Add(value2, value3);
					}
				}
			}
			else if (obj is IFullSerializable)
			{
				(obj as IFullSerializable).SetObjectData(serializationInfo, fNB);
			}
			else if (array.Length > 0)
			{
				fNg.Clear();
				for (int l = 0; l < array.Length; l++)
				{
					if (!fNg.ContainsKey(array[l].Name))
					{
						fNg.Add(array[l].Name, l);
					}
				}
				foreach (SerializationEntry item2 in serializationInfo)
				{
					if (fNg.ContainsKey(item2.Name))
					{
						int num = fNg[item2.Name];
						MemberInfo memberInfo = array[num];
						if (memberInfo is PropertyInfo)
						{
							PropertyInfo propertyInfo = memberInfo as PropertyInfo;
							object value4 = serializationInfo.GetValue(item2.Name, propertyInfo.PropertyType);
							propertyInfo.SetValue(obj, value4, null);
						}
						else if (memberInfo is FieldInfo)
						{
							FieldInfo fieldInfo = memberInfo as FieldInfo;
							object value5 = serializationInfo.GetValue(item2.Name, fieldInfo.FieldType);
							fieldInfo.SetValue(obj, value5);
						}
					}
				}
			}
			return obj;
		}
	}
}
