using System.IO;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Lights;

/// <summary>
/// Provides data for calculating light occlusion (shadowing) with composite lighting.
/// </summary>
public class LightOcclusionBuffer
{
	private float fNB;

	private Vector3 fNa;

	private Matrix fNg;

	private ushort[,] fNf;

	internal LightOcclusionBuffer(Vector3 P_0, float P_1, Matrix P_2, ushort[,] P_3)
	{
		fNB = P_1;
		fNa = P_0;
		fNg = P_2;
		fNf = P_3;
	}

	internal LightOcclusionBuffer()
	{
	}

	/// <summary>
	/// Calculates the occlusion amount (shadowing) for the provided world position.
	/// </summary>
	/// <param name="worldpos"></param>
	/// <returns></returns>
	public float GetOcclusionAmount(Vector3 worldpos)
	{
		Vector3.Distance(ref worldpos, ref fNa, out var result);
		Vector4.Transform(ref worldpos, ref fNg, out var result2);
		ushort num = (ushort)(65535f * MathHelper.Clamp(result / fNB, 0f, 1f));
		int length = fNf.GetLength(0);
		int length2 = fNf.GetLength(1);
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
		if (fNf[num4, num5] > num)
		{
			value = 1f;
		}
		if (fNf[num6, num5] > num)
		{
			value3 = 1f;
		}
		if (fNf[num4, num7] > num)
		{
			value2 = 1f;
		}
		if (fNf[num6, num7] > num)
		{
			value4 = 1f;
		}
		float value5 = MathHelper.Lerp(value, value3, amount);
		float value6 = MathHelper.Lerp(value2, value4, amount);
		return MathHelper.Lerp(value5, value6, amount2);
	}

	internal void fk(Stream P_0)
	{
		try
		{
			using BinaryWriter binaryWriter = new BinaryWriter(P_0);
			binaryWriter.Write(fNB);
			binaryWriter.Write(fNa.X);
			binaryWriter.Write(fNa.Y);
			binaryWriter.Write(fNa.Z);
			binaryWriter.Write(fNg.M11);
			binaryWriter.Write(fNg.M12);
			binaryWriter.Write(fNg.M13);
			binaryWriter.Write(fNg.M14);
			binaryWriter.Write(fNg.M21);
			binaryWriter.Write(fNg.M22);
			binaryWriter.Write(fNg.M23);
			binaryWriter.Write(fNg.M24);
			binaryWriter.Write(fNg.M31);
			binaryWriter.Write(fNg.M32);
			binaryWriter.Write(fNg.M33);
			binaryWriter.Write(fNg.M34);
			binaryWriter.Write(fNg.M41);
			binaryWriter.Write(fNg.M42);
			binaryWriter.Write(fNg.M43);
			binaryWriter.Write(fNg.M44);
			int length = fNf.GetLength(0);
			int length2 = fNf.GetLength(1);
			binaryWriter.Write(length);
			binaryWriter.Write(length2);
			for (int i = 0; i < length2; i++)
			{
				for (int j = 0; j < length; j++)
				{
					binaryWriter.Write(fNf[j, i]);
				}
			}
		}
		catch
		{
		}
	}

	internal void fC(BinaryReader P_0)
	{
		try
		{
			fNB = P_0.ReadSingle();
			fNa.X = P_0.ReadSingle();
			fNa.Y = P_0.ReadSingle();
			fNa.Z = P_0.ReadSingle();
			fNg.M11 = P_0.ReadSingle();
			fNg.M12 = P_0.ReadSingle();
			fNg.M13 = P_0.ReadSingle();
			fNg.M14 = P_0.ReadSingle();
			fNg.M21 = P_0.ReadSingle();
			fNg.M22 = P_0.ReadSingle();
			fNg.M23 = P_0.ReadSingle();
			fNg.M24 = P_0.ReadSingle();
			fNg.M31 = P_0.ReadSingle();
			fNg.M32 = P_0.ReadSingle();
			fNg.M33 = P_0.ReadSingle();
			fNg.M34 = P_0.ReadSingle();
			fNg.M41 = P_0.ReadSingle();
			fNg.M42 = P_0.ReadSingle();
			fNg.M43 = P_0.ReadSingle();
			fNg.M44 = P_0.ReadSingle();
			int num = P_0.ReadInt32();
			int num2 = P_0.ReadInt32();
			fNf = new ushort[num, num2];
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					fNf[j, i] = P_0.ReadUInt16();
				}
			}
		}
		catch
		{
		}
	}
}
