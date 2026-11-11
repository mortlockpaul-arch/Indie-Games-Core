using System;

namespace _u0014_;

internal class _id_5
{
	private const int l8a = 64;

	private const int l8_u0013_ = 20;

	private uint[] l8N;

	private ulong l8l;

	private byte[] l8L;

	private int l8w;

	private uint[] l8J;

	public _id_5()
	{
		l8N = new uint[5];
		l8L = new byte[64];
		l8J = new uint[80];
		Initialize();
	}

	public void HashCore(byte[] rgb, int start, int size)
	{
		if (l8w != 0)
		{
			if (size < 64 - l8w)
			{
				Buffer.BlockCopy(rgb, start, l8L, l8w, size);
				l8w += size;
				return;
			}
			int num = 64 - l8w;
			Buffer.BlockCopy(rgb, start, l8L, l8w, num);
			L_u0002_(l8L, 0);
			l8w = 0;
			start += num;
			size -= num;
		}
		for (int num = 0; num < size - size % 64; num += 64)
		{
			L_u0002_(rgb, start + num);
		}
		if (size % 64 != 0)
		{
			Buffer.BlockCopy(rgb, size - size % 64 + start, l8L, 0, size % 64);
			l8w = size % 64;
		}
	}

	public byte[] HashFinal()
	{
		byte[] array = new byte[20];
		Lv(l8L, 0, l8w);
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				array[i * 4 + j] = (byte)(l8N[i] >> 8 * (3 - j));
			}
		}
		return array;
	}

	public void Initialize()
	{
		l8l = 0uL;
		l8w = 0;
		l8N[0] = 1732584193u;
		l8N[1] = 4023233417u;
		l8N[2] = 2562383102u;
		l8N[3] = 271733878u;
		l8N[4] = 3285377520u;
	}

	private void L_u0002_(byte[] param_0, int param_1)
	{
		l8l += 64uL;
		for (int i = 0; i < 16; i++)
		{
			l8J[i] = (uint)((param_0[param_1 + 4 * i] << 24) | (param_0[param_1 + 4 * i + 1] << 16) | (param_0[param_1 + 4 * i + 2] << 8) | param_0[param_1 + 4 * i + 3]);
		}
		for (int i = 16; i < 80; i++)
		{
			l8J[i] = ((l8J[i - 3] ^ l8J[i - 8] ^ l8J[i - 14] ^ l8J[i - 16]) << 1) | ((l8J[i - 3] ^ l8J[i - 8] ^ l8J[i - 14] ^ l8J[i - 16]) >> 31);
		}
		uint num = l8N[0];
		uint num2 = l8N[1];
		uint num3 = l8N[2];
		uint num4 = l8N[3];
		uint num5 = l8N[4];
		num5 += ((num << 5) | (num >> 27)) + (((num3 ^ num4) & num2) ^ num4) + 1518500249 + l8J[0];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (((num2 ^ num3) & num) ^ num3) + 1518500249 + l8J[1];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (((num ^ num2) & num5) ^ num2) + 1518500249 + l8J[2];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (((num5 ^ num) & num4) ^ num) + 1518500249 + l8J[3];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (((num4 ^ num5) & num3) ^ num5) + 1518500249 + l8J[4];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (((num3 ^ num4) & num2) ^ num4) + 1518500249 + l8J[5];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (((num2 ^ num3) & num) ^ num3) + 1518500249 + l8J[6];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (((num ^ num2) & num5) ^ num2) + 1518500249 + l8J[7];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (((num5 ^ num) & num4) ^ num) + 1518500249 + l8J[8];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (((num4 ^ num5) & num3) ^ num5) + 1518500249 + l8J[9];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (((num3 ^ num4) & num2) ^ num4) + 1518500249 + l8J[10];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (((num2 ^ num3) & num) ^ num3) + 1518500249 + l8J[11];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (((num ^ num2) & num5) ^ num2) + 1518500249 + l8J[12];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (((num5 ^ num) & num4) ^ num) + 1518500249 + l8J[13];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (((num4 ^ num5) & num3) ^ num5) + 1518500249 + l8J[14];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (((num3 ^ num4) & num2) ^ num4) + 1518500249 + l8J[15];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (((num2 ^ num3) & num) ^ num3) + 1518500249 + l8J[16];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (((num ^ num2) & num5) ^ num2) + 1518500249 + l8J[17];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (((num5 ^ num) & num4) ^ num) + 1518500249 + l8J[18];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (((num4 ^ num5) & num3) ^ num5) + 1518500249 + l8J[19];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4) + 1859775393 + l8J[20];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3) + 1859775393 + l8J[21];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2) + 1859775393 + l8J[22];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num) + 1859775393 + l8J[23];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5) + 1859775393 + l8J[24];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4) + 1859775393 + l8J[25];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3) + 1859775393 + l8J[26];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2) + 1859775393 + l8J[27];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num) + 1859775393 + l8J[28];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5) + 1859775393 + l8J[29];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4) + 1859775393 + l8J[30];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3) + 1859775393 + l8J[31];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2) + 1859775393 + l8J[32];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num) + 1859775393 + l8J[33];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5) + 1859775393 + l8J[34];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += ((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4) + 1859775393 + l8J[35];
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += ((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3) + 1859775393 + l8J[36];
		num = (num << 30) | (num >> 2);
		num3 += ((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2) + 1859775393 + l8J[37];
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += ((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num) + 1859775393 + l8J[38];
		num4 = (num4 << 30) | (num4 >> 2);
		num += ((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5) + 1859775393 + l8J[39];
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + ((num2 & num3) | (num2 & num4) | (num3 & num4))) + -1894007588 + (int)l8J[40]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + ((num & num2) | (num & num3) | (num2 & num3))) + -1894007588 + (int)l8J[41]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + ((num5 & num) | (num5 & num2) | (num & num2))) + -1894007588 + (int)l8J[42]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + ((num4 & num5) | (num4 & num) | (num5 & num))) + -1894007588 + (int)l8J[43]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + ((num3 & num4) | (num3 & num5) | (num4 & num5))) + -1894007588 + (int)l8J[44]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + ((num2 & num3) | (num2 & num4) | (num3 & num4))) + -1894007588 + (int)l8J[45]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + ((num & num2) | (num & num3) | (num2 & num3))) + -1894007588 + (int)l8J[46]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + ((num5 & num) | (num5 & num2) | (num & num2))) + -1894007588 + (int)l8J[47]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + ((num4 & num5) | (num4 & num) | (num5 & num))) + -1894007588 + (int)l8J[48]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + ((num3 & num4) | (num3 & num5) | (num4 & num5))) + -1894007588 + (int)l8J[49]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + ((num2 & num3) | (num2 & num4) | (num3 & num4))) + -1894007588 + (int)l8J[50]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + ((num & num2) | (num & num3) | (num2 & num3))) + -1894007588 + (int)l8J[51]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + ((num5 & num) | (num5 & num2) | (num & num2))) + -1894007588 + (int)l8J[52]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + ((num4 & num5) | (num4 & num) | (num5 & num))) + -1894007588 + (int)l8J[53]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + ((num3 & num4) | (num3 & num5) | (num4 & num5))) + -1894007588 + (int)l8J[54]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + ((num2 & num3) | (num2 & num4) | (num3 & num4))) + -1894007588 + (int)l8J[55]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + ((num & num2) | (num & num3) | (num2 & num3))) + -1894007588 + (int)l8J[56]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + ((num5 & num) | (num5 & num2) | (num & num2))) + -1894007588 + (int)l8J[57]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + ((num4 & num5) | (num4 & num) | (num5 & num))) + -1894007588 + (int)l8J[58]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + ((num3 & num4) | (num3 & num5) | (num4 & num5))) + -1894007588 + (int)l8J[59]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4)) + -899497514 + (int)l8J[60]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3)) + -899497514 + (int)l8J[61]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2)) + -899497514 + (int)l8J[62]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num)) + -899497514 + (int)l8J[63]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5)) + -899497514 + (int)l8J[64]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4)) + -899497514 + (int)l8J[65]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3)) + -899497514 + (int)l8J[66]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2)) + -899497514 + (int)l8J[67]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num)) + -899497514 + (int)l8J[68]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5)) + -899497514 + (int)l8J[69]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4)) + -899497514 + (int)l8J[70]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3)) + -899497514 + (int)l8J[71]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2)) + -899497514 + (int)l8J[72]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num)) + -899497514 + (int)l8J[73]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5)) + -899497514 + (int)l8J[74]);
		num3 = (num3 << 30) | (num3 >> 2);
		num5 += (uint)((int)(((num << 5) | (num >> 27)) + (num2 ^ num3 ^ num4)) + -899497514 + (int)l8J[75]);
		num2 = (num2 << 30) | (num2 >> 2);
		num4 += (uint)((int)(((num5 << 5) | (num5 >> 27)) + (num ^ num2 ^ num3)) + -899497514 + (int)l8J[76]);
		num = (num << 30) | (num >> 2);
		num3 += (uint)((int)(((num4 << 5) | (num4 >> 27)) + (num5 ^ num ^ num2)) + -899497514 + (int)l8J[77]);
		num5 = (num5 << 30) | (num5 >> 2);
		num2 += (uint)((int)(((num3 << 5) | (num3 >> 27)) + (num4 ^ num5 ^ num)) + -899497514 + (int)l8J[78]);
		num4 = (num4 << 30) | (num4 >> 2);
		num += (uint)((int)(((num2 << 5) | (num2 >> 27)) + (num3 ^ num4 ^ num5)) + -899497514 + (int)l8J[79]);
		num3 = (num3 << 30) | (num3 >> 2);
		l8N[0] += num;
		l8N[1] += num2;
		l8N[2] += num3;
		l8N[3] += num4;
		l8N[4] += num5;
	}

	private void Lv(byte[] param_0, int param_1, int param_2)
	{
		ulong num = l8l + (ulong)param_2;
		int num2 = 56 - (int)(num % 64);
		if (num2 < 1)
		{
			num2 += 64;
		}
		int num3 = param_2 + num2 + 8;
		byte[] array = ((num3 == 64) ? l8L : new byte[num3]);
		for (int i = 0; i < param_2; i++)
		{
			array[i] = param_0[i + param_1];
		}
		array[param_2] = 128;
		for (int j = param_2 + 1; j < param_2 + num2; j++)
		{
			array[j] = 0;
		}
		ulong param_3 = num << 3;
		LY(param_3, array, param_2 + num2);
		L_u0002_(array, 0);
		if (num3 == 128)
		{
			L_u0002_(array, 64);
		}
	}

	internal void LY(ulong param_0, byte[] param_1, int param_2)
	{
		param_1[param_2++] = (byte)(param_0 >> 56);
		param_1[param_2++] = (byte)(param_0 >> 48);
		param_1[param_2++] = (byte)(param_0 >> 40);
		param_1[param_2++] = (byte)(param_0 >> 32);
		param_1[param_2++] = (byte)(param_0 >> 24);
		param_1[param_2++] = (byte)(param_0 >> 16);
		param_1[param_2++] = (byte)(param_0 >> 8);
		param_1[param_2] = (byte)param_0;
	}
}
