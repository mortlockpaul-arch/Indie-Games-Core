using System;

namespace _0011;

internal class c
{
	private const int fNB = 64;

	private const int fNa = 20;

	private uint[] fNg;

	private ulong fNf;

	private byte[] fN_0003;

	private int fNn;

	private uint[] fN_0019;

	public c()
	{
		fNg = new uint[5];
		fN_0003 = new byte[64];
		fN_0019 = new uint[80];
		Initialize();
	}

	public void HashCore(byte[] rgb, int start, int size)
	{
		if (fNn != 0)
		{
			if (size < 64 - fNn)
			{
				Buffer.BlockCopy(rgb, start, fN_0003, fNn, size);
				fNn += size;
				return;
			}
			int num = 64 - fNn;
			Buffer.BlockCopy(rgb, start, fN_0003, fNn, num);
			_00031(fN_0003, 0);
			fNn = 0;
			start += num;
			size -= num;
		}
		for (int num = 0; num < size - size % 64; num += 64)
		{
			_00031(rgb, start + num);
		}
		if (size % 64 != 0)
		{
			Buffer.BlockCopy(rgb, size - size % 64 + start, fN_0003, 0, size % 64);
			fNn = size % 64;
		}
	}

	public byte[] HashFinal()
	{
		byte[] array = new byte[20];
		_0003_0016(fN_0003, 0, fNn);
		for (int k = 0; k < 5; k++)
		{
			for (int l = 0; l < 4; l++)
			{
				array[k * 4 + l] = (byte)(fNg[k] >> 8 * (3 - l));
			}
		}
		return array;
	}

	public void Initialize()
	{
		fNf = 0uL;
		fNn = 0;
		fNg[0] = 1732584193u;
		fNg[1] = 4023233417u;
		fNg[2] = 2562383102u;
		fNg[3] = 271733878u;
		fNg[4] = 3285377520u;
	}

	private void _00031(byte[] P_0, int P_1)
	{
		fNf += 64uL;
		for (int k = 0; k < 16; k++)
		{
			fN_0019[k] = (uint)((P_0[P_1 + 4 * k] << 24) | (P_0[P_1 + 4 * k + 1] << 16) | (P_0[P_1 + 4 * k + 2] << 8) | P_0[P_1 + 4 * k + 3]);
		}
		for (int k = 16; k < 80; k++)
		{
			fN_0019[k] = ((fN_0019[k - 3] ^ fN_0019[k - 8] ^ fN_0019[k - 14] ^ fN_0019[k - 16]) << 1) | ((fN_0019[k - 3] ^ fN_0019[k - 8] ^ fN_0019[k - 14] ^ fN_0019[k - 16]) >> 31);
		}
		uint num = fNg[0];
		uint num2 = fNg[1];
		uint num3 = fNg[2];
		uint num4 = fNg[3];
		uint num5 = fNg[4];
		num5 += ((num << 5) | (num >> 27)) + (((num3 ^ num4) & num2) ^ num4) + 1518500249 + fN_0019[0];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (((num2 ^ num3) & num) ^ num3) + 1518500249 + fN_0019[1];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (((num ^ num2) & num5) ^ num2) + 1518500249 + fN_0019[2];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (((num5 ^ num) & num4) ^ num) + 1518500249 + fN_0019[3];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (((num4 ^ num5) & num3) ^ num5) + 1518500249 + fN_0019[4];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (((num3 ^ num4) & num2) ^ num4) + 1518500249 + fN_0019[5];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (((num2 ^ num3) & num) ^ num3) + 1518500249 + fN_0019[6];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (((num ^ num2) & num5) ^ num2) + 1518500249 + fN_0019[7];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (((num5 ^ num) & num4) ^ num) + 1518500249 + fN_0019[8];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (((num4 ^ num5) & num3) ^ num5) + 1518500249 + fN_0019[9];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (((num3 ^ num4) & num2) ^ num4) + 1518500249 + fN_0019[10];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (((num2 ^ num3) & num) ^ num3) + 1518500249 + fN_0019[11];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (((num ^ num2) & num5) ^ num2) + 1518500249 + fN_0019[12];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (((num5 ^ num) & num4) ^ num) + 1518500249 + fN_0019[13];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (((num4 ^ num5) & num3) ^ num5) + 1518500249 + fN_0019[14];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (((num3 ^ num4) & num2) ^ num4) + 1518500249 + fN_0019[15];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (((num2 ^ num3) & num) ^ num3) + 1518500249 + fN_0019[16];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (((num ^ num2) & num5) ^ num2) + 1518500249 + fN_0019[17];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (((num5 ^ num) & num4) ^ num) + 1518500249 + fN_0019[18];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (((num4 ^ num5) & num3) ^ num5) + 1518500249 + fN_0019[19];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4) + 1859775393 + fN_0019[20];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3) + 1859775393 + fN_0019[21];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2) + 1859775393 + fN_0019[22];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num) + 1859775393 + fN_0019[23];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5) + 1859775393 + fN_0019[24];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4) + 1859775393 + fN_0019[25];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3) + 1859775393 + fN_0019[26];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2) + 1859775393 + fN_0019[27];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num) + 1859775393 + fN_0019[28];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5) + 1859775393 + fN_0019[29];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4) + 1859775393 + fN_0019[30];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3) + 1859775393 + fN_0019[31];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2) + 1859775393 + fN_0019[32];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num) + 1859775393 + fN_0019[33];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5) + 1859775393 + fN_0019[34];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4) + 1859775393 + fN_0019[35];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3) + 1859775393 + fN_0019[36];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2) + 1859775393 + fN_0019[37];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num) + 1859775393 + fN_0019[38];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5) + 1859775393 + fN_0019[39];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + ((num2 & num3) | (num2 & num4) | (num3 & num4))) + -1894007588 + (int)fN_0019[40]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + ((num & num2) | (num & num3) | (num2 & num3))) + -1894007588 + (int)fN_0019[41]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + ((num5 & num) | (num5 & num2) | (num & num2))) + -1894007588 + (int)fN_0019[42]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + ((num4 & num5) | (num4 & num) | (num5 & num))) + -1894007588 + (int)fN_0019[43]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + ((num3 & num4) | (num3 & num5) | (num4 & num5))) + -1894007588 + (int)fN_0019[44]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + ((num2 & num3) | (num2 & num4) | (num3 & num4))) + -1894007588 + (int)fN_0019[45]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + ((num & num2) | (num & num3) | (num2 & num3))) + -1894007588 + (int)fN_0019[46]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + ((num5 & num) | (num5 & num2) | (num & num2))) + -1894007588 + (int)fN_0019[47]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + ((num4 & num5) | (num4 & num) | (num5 & num))) + -1894007588 + (int)fN_0019[48]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + ((num3 & num4) | (num3 & num5) | (num4 & num5))) + -1894007588 + (int)fN_0019[49]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + ((num2 & num3) | (num2 & num4) | (num3 & num4))) + -1894007588 + (int)fN_0019[50]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + ((num & num2) | (num & num3) | (num2 & num3))) + -1894007588 + (int)fN_0019[51]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + ((num5 & num) | (num5 & num2) | (num & num2))) + -1894007588 + (int)fN_0019[52]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + ((num4 & num5) | (num4 & num) | (num5 & num))) + -1894007588 + (int)fN_0019[53]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + ((num3 & num4) | (num3 & num5) | (num4 & num5))) + -1894007588 + (int)fN_0019[54]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + ((num2 & num3) | (num2 & num4) | (num3 & num4))) + -1894007588 + (int)fN_0019[55]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + ((num & num2) | (num & num3) | (num2 & num3))) + -1894007588 + (int)fN_0019[56]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + ((num5 & num) | (num5 & num2) | (num & num2))) + -1894007588 + (int)fN_0019[57]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + ((num4 & num5) | (num4 & num) | (num5 & num))) + -1894007588 + (int)fN_0019[58]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + ((num3 & num4) | (num3 & num5) | (num4 & num5))) + -1894007588 + (int)fN_0019[59]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4)) + -899497514 + (int)fN_0019[60]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3)) + -899497514 + (int)fN_0019[61]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2)) + -899497514 + (int)fN_0019[62]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num)) + -899497514 + (int)fN_0019[63]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5)) + -899497514 + (int)fN_0019[64]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4)) + -899497514 + (int)fN_0019[65]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3)) + -899497514 + (int)fN_0019[66]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2)) + -899497514 + (int)fN_0019[67]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num)) + -899497514 + (int)fN_0019[68]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5)) + -899497514 + (int)fN_0019[69]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4)) + -899497514 + (int)fN_0019[70]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3)) + -899497514 + (int)fN_0019[71]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2)) + -899497514 + (int)fN_0019[72]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num)) + -899497514 + (int)fN_0019[73]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5)) + -899497514 + (int)fN_0019[74]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4)) + -899497514 + (int)fN_0019[75]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3)) + -899497514 + (int)fN_0019[76]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2)) + -899497514 + (int)fN_0019[77]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num)) + -899497514 + (int)fN_0019[78]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5)) + -899497514 + (int)fN_0019[79]);
		num3 = (num3 << 30) | (num3 >> 2);
		fNg[0] += num;
		fNg[1] += num2;
		fNg[2] += num3;
		fNg[3] += num4;
		fNg[4] += num5;
	}

	private void _0003_0016(byte[] P_0, int P_1, int P_2)
	{
		ulong num = fNf + (ulong)P_2;
		int num2 = 56 - (int)(num % 64);
		if (num2 < 1)
		{
			num2 += 64;
		}
		int num3 = P_2 + num2 + 8;
		byte[] array = ((num3 == 64) ? fN_0003 : new byte[num3]);
		for (int k = 0; k < P_2; k++)
		{
			array[k] = P_0[k + P_1];
		}
		array[P_2] = 128;
		for (int l = P_2 + 1; l < P_2 + num2; l++)
		{
			array[l] = 0;
		}
		ulong num4 = num << 3;
		_0003x(num4, array, P_2 + num2);
		_00031(array, 0);
		if (num3 == 128)
		{
			_00031(array, 64);
		}
	}

	internal void _0003x(ulong P_0, byte[] P_1, int P_2)
	{
		P_1[P_2++] = (byte)(P_0 >> 56);
		P_1[P_2++] = (byte)(P_0 >> 48);
		P_1[P_2++] = (byte)(P_0 >> 40);
		P_1[P_2++] = (byte)(P_0 >> 32);
		P_1[P_2++] = (byte)(P_0 >> 24);
		P_1[P_2++] = (byte)(P_0 >> 16);
		P_1[P_2++] = (byte)(P_0 >> 8);
		P_1[P_2] = (byte)P_0;
	}
}
