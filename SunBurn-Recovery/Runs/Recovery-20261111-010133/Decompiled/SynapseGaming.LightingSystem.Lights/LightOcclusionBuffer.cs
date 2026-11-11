using System.IO;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Lights;

public class LightOcclusionBuffer
{
	private float l8a;

	private Vector3 l8_u0013_;

	private Matrix l8N;

	private ushort[,] l8l;

	internal LightOcclusionBuffer(Vector3 param_0, float param_1, Matrix param_2, ushort[,] param_3)
	{
		l8a = param_1;
		l8_u0013_ = param_0;
		l8N = param_2;
		l8l = param_3;
	}

	internal LightOcclusionBuffer()
	{
	}

	public float GetOcclusionAmount(Vector3 worldpos)
	{
		Vector3.Distance(ref worldpos, ref l8_u0013_, out var result);
		Vector4.Transform(ref worldpos, ref l8N, out var result2);
		ushort num = (ushort)(65535f * MathHelper.Clamp(result / l8a, 0f, 1f));
		int length = l8l.GetLength(0);
		int length2 = l8l.GetLength(1);
		float num2 = result2.X / result2.W;
		float num3 = result2.Y / result2.W;
		num2 = num2 * 0.5f + 0.5f;
		num3 = 1f - (num3 * 0.5f + 0.5f);
		num2 = MathHelper.Clamp(num2, 0f, 1f);
		num3 = MathHelper.Clamp(num3, 0f, 1f);
		num2 *= (float)(length - 1);
		num3 *= (float)(length2 - 1);
		int num4 = (int)num2;
		int num5 = (int)num3;
		int num6 = (num4 + 1) % length;
		int num7 = (num5 + 1) % length2;
		float amount = num2 - (float)num4;
		float amount2 = num3 - (float)num5;
		float value = 0f;
		float value2 = 0f;
		float value3 = 0f;
		float value4 = 0f;
		if (l8l[num4, num5] > num)
		{
			value = 1f;
		}
		if (l8l[num6, num5] > num)
		{
			value3 = 1f;
		}
		if (l8l[num4, num7] > num)
		{
			value2 = 1f;
		}
		if (l8l[num6, num7] > num)
		{
			value4 = 1f;
		}
		float value5 = MathHelper.Lerp(value, value3, amount);
		float value6 = MathHelper.Lerp(value2, value4, amount);
		return MathHelper.Lerp(value5, value6, amount2);
	}

	internal void li(Stream param_0)
	{
		try
		{
			using BinaryWriter binaryWriter = new BinaryWriter(param_0);
			binaryWriter.Write(l8a);
			binaryWriter.Write(l8_u0013_.X);
			binaryWriter.Write(l8_u0013_.Y);
			binaryWriter.Write(l8_u0013_.Z);
			binaryWriter.Write(l8N.M11);
			binaryWriter.Write(l8N.M12);
			binaryWriter.Write(l8N.M13);
			binaryWriter.Write(l8N.M14);
			binaryWriter.Write(l8N.M21);
			binaryWriter.Write(l8N.M22);
			binaryWriter.Write(l8N.M23);
			binaryWriter.Write(l8N.M24);
			binaryWriter.Write(l8N.M31);
			binaryWriter.Write(l8N.M32);
			binaryWriter.Write(l8N.M33);
			binaryWriter.Write(l8N.M34);
			binaryWriter.Write(l8N.M41);
			binaryWriter.Write(l8N.M42);
			binaryWriter.Write(l8N.M43);
			binaryWriter.Write(l8N.M44);
			int length = l8l.GetLength(0);
			int length2 = l8l.GetLength(1);
			binaryWriter.Write(length);
			binaryWriter.Write(length2);
			for (int i = 0; i < length2; i++)
			{
				for (int j = 0; j < length; j++)
				{
					binaryWriter.Write(l8l[j, i]);
				}
			}
		}
		catch
		{
		}
	}

	internal void _u0013__u0014_(BinaryReader param_0)
	{
		try
		{
			l8a = param_0.ReadSingle();
			l8_u0013_.X = param_0.ReadSingle();
			l8_u0013_.Y = param_0.ReadSingle();
			l8_u0013_.Z = param_0.ReadSingle();
			l8N.M11 = param_0.ReadSingle();
			l8N.M12 = param_0.ReadSingle();
			l8N.M13 = param_0.ReadSingle();
			l8N.M14 = param_0.ReadSingle();
			l8N.M21 = param_0.ReadSingle();
			l8N.M22 = param_0.ReadSingle();
			l8N.M23 = param_0.ReadSingle();
			l8N.M24 = param_0.ReadSingle();
			l8N.M31 = param_0.ReadSingle();
			l8N.M32 = param_0.ReadSingle();
			l8N.M33 = param_0.ReadSingle();
			l8N.M34 = param_0.ReadSingle();
			l8N.M41 = param_0.ReadSingle();
			l8N.M42 = param_0.ReadSingle();
			l8N.M43 = param_0.ReadSingle();
			l8N.M44 = param_0.ReadSingle();
			int num = param_0.ReadInt32();
			int num2 = param_0.ReadInt32();
			l8l = new ushort[num, num2];
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					l8l[j, i] = param_0.ReadUInt16();
				}
			}
		}
		catch
		{
		}
	}
}
