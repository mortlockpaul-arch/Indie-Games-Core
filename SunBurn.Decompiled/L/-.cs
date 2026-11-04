using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace L;

internal class _0003
{
	internal const string fNB = "Locale";

	internal const string fNa = "EffectFile";

	internal const string fNg = "Technique";

	internal const string fNf = "DepthTechnique";

	internal const string fN_0003 = "GBufferTechnique";

	internal const string fNn = "FinalTechnique";

	internal const string fN_0019 = "ShadowGenerationTechnique";

	internal const string fNL = "Elasticity";

	internal const string fNv = "Friction";

	internal const string fN_0012 = "DoubleSided";

	internal const string fN_0015 = "TransparencyMode";

	internal const string fN8 = "Transparency";

	internal const string fNi = "TransparencyMapParameterName";

	internal const string fN_0005 = "SOFTWARE\\Microsoft\\.NETFramework\\v2.0.50727\\AssemblyFoldersEx\\Synapse Gaming - SunBurn {0} {1}";

	internal const string fNj = "\\Development\\Windows";

	internal const string fNo = "\\ShaderLibrary";

	internal static object G(Type P_0, string P_1, CultureInfo P_2)
	{
		if ((object)P_0 == typeof(string))
		{
			return P_1;
		}
		if ((object)P_0 == typeof(bool))
		{
			return a8(P_1);
		}
		if ((object)P_0 == typeof(float))
		{
			return ai(P_1, P_2);
		}
		if ((object)P_0 == typeof(Vector4))
		{
			return a_0005(P_1, P_2, Vector4.Zero);
		}
		return null;
	}

	internal static T a_0015<T>(string P_0)
	{
		try
		{
			return (T)Enum.Parse(typeof(T), P_0, ignoreCase: true);
		}
		catch
		{
			throw new Exception("Invalid property value '" + P_0 + "'.");
		}
	}

	internal static bool a8(string P_0)
	{
		return bool.Parse(P_0);
	}

	internal static float ai(string P_0, CultureInfo P_1)
	{
		return float.Parse(P_0, P_1.NumberFormat);
	}

	internal static Vector4 a_0005(string P_0, CultureInfo P_1, Vector4 P_2)
	{
		string[] array = Regex.Split(P_0, " ");
		if (array.Length < 3 || array.Length > 4)
		{
			throw new Exception("Invalid vector data.");
		}
		P_2.X = ai(array[0], P_1);
		P_2.Y = ai(array[1], P_1);
		P_2.Z = ai(array[2], P_1);
		if (array.Length > 3)
		{
			P_2.W = ai(array[3], P_1);
		}
		return P_2;
	}
}
internal class _0019
{
	private static float[] fNB = new float[4];

	private static BoundingFrustum fNa = new BoundingFrustum(Matrix.Identity);

	private static Vector3[] fNg = new Vector3[8];

	private static float fNf = 1f / (float)Math.Log(2.0);

	private static Random fN_0003 = new Random((int)DateTime.Now.Ticks);

	internal static bool ac(int P_0)
	{
		return (P_0 & (P_0 - 1)) == 0;
	}

	internal static bool ad(ref Vector3 P_0, ref Vector3 P_1, ref Vector3 P_2)
	{
		float num = P_1.X - P_0.X;
		float num2 = P_1.Y - P_0.Y;
		float num3 = P_1.Z - P_0.Z;
		float num4 = P_2.X - P_0.X;
		float num5 = P_2.Y - P_0.Y;
		float num6 = P_2.Z - P_0.Z;
		float num7 = num2 * num6 - num3 * num5;
		float num8 = num3 * num4 - num * num6;
		float num9 = num * num5 - num2 * num4;
		float num10 = num7 * num7 + num8 * num8 + num9 * num9;
		return num10 <= 0f;
	}

	internal static float aw(double P_0, double P_1)
	{
		if (P_1 <= 0.0)
		{
			return 0f;
		}
		double num = 1.0 / P_1;
		double num2 = 0.5 * num * (num - 1.0);
		if (num2 <= 0.0)
		{
			return 0f;
		}
		double num3 = P_0 / num2;
		return (float)num3;
	}

	internal static int @as(object P_0)
	{
		return au(P_0.GetHashCode(), fN_0003.Next());
	}

	internal static float a_0011(float P_0)
	{
		return (float)Math.Log(P_0) * fNf;
	}

	internal static void aQ(Vector3 P_0, float P_1, float P_2, out Vector3 P_3, out Vector3 P_4)
	{
		float num = 1f + P_1;
		float num2 = 1f - P_1;
		P_3 = P_0 * 2f * new Vector3(num, 1f, num2) * P_2;
		P_4 = P_0 * 2f * new Vector3(num2, 1f, num) * (1f - P_2);
	}

	internal static string aG(INamedObject P_0)
	{
		Type type = P_0.GetType();
		string text = P_0.Name;
		if (text == null)
		{
			text = string.Empty;
		}
		return $"\"{text}\" - {type.Name}";
	}

	internal static Vector3 a_0017(Vector3 P_0, int P_1, float P_2)
	{
		fNB[0] = P_0.X;
		fNB[1] = P_0.Y;
		fNB[2] = P_0.Z;
		fNB[P_1] = P_2;
		return new Vector3(fNB[0], fNB[1], fNB[2]);
	}

	internal static Vector3 am(Vector3 P_0, int P_1, int P_2, int P_3)
	{
		fNB[0] = P_0.X;
		fNB[1] = P_0.Y;
		fNB[2] = P_0.Z;
		return new Vector3(fNB[P_1], fNB[P_2], fNB[P_3]);
	}

	internal static BoundingBox ah(BoundingBox P_0, Matrix P_1)
	{
		P_0.GetCorners(fNg);
		for (int k = 0; k < fNg.Length; k++)
		{
			ref Vector3 reference = ref fNg[k];
			reference = Vector3.Transform(fNg[k], P_1);
		}
		return a_000F(fNg);
	}

	internal static BoundingSphere aR(BoundingSphere P_0, Matrix P_1)
	{
		aW(ref P_0, ref P_1, out var result);
		return result;
	}

	internal static void aW(ref BoundingSphere P_0, ref Matrix P_1, out BoundingSphere P_2)
	{
		Vector3 vector = default;
		vector.X = new Vector3(P_1.M11, P_1.M12, P_1.M13).Length();
		vector.Y = new Vector3(P_1.M21, P_1.M22, P_1.M23).Length();
		vector.Z = new Vector3(P_1.M31, P_1.M32, P_1.M33).Length();
		float num = Math.Max(Math.Abs(vector.X), Math.Max(Math.Abs(vector.Y), Math.Abs(vector.Z)));
		Vector3.Transform(ref P_0.Center, ref P_1, out P_2.Center);
		P_2.Radius = P_0.Radius * num;
	}

	internal static BoundingBox a9(BoundingBox P_0, float P_1)
	{
		Vector3 vector = (P_0.Max - P_0.Min) * 0.5f;
		Vector3 vector2 = P_0.Max - vector;
		Vector3 vector3 = vector * P_1;
		return new BoundingBox(vector2 - vector3, vector2 + vector3);
	}

	internal static BoundingSphere a_0013(BoundingSphere P_0, float P_1)
	{
		BoundingSphere result = P_0;
		result.Radius *= P_1;
		return result;
	}

	internal static Vector3 aq(Vector3 P_0, Plane P_1)
	{
		float num = P_1.DotCoordinate(P_0);
		return P_0 - P_1.Normal * num;
	}

	internal static bool F(Vector3 P_0, Vector3 P_1, Plane P_2, ref Vector3 P_3)
	{
		Vector3 vector = P_1 - P_0;
		float num = Vector3.Dot(P_2.Normal, vector);
		if (num == 0f)
		{
			return false;
		}
		float num2 = (0f - (Vector3.Dot(P_2.Normal, P_0) + P_2.D)) / num;
		if (num2 >= 0f && num2 <= 1f)
		{
			P_3 = P_0 + num2 * vector;
			return true;
		}
		return false;
	}

	internal static BoundingBox a_000F(Vector3[] P_0)
	{
		if (P_0.Length < 1)
		{
			return default;
		}
		BoundingBox result = new BoundingBox(P_0[0], P_0[0]);
		for (int k = 1; k < P_0.Length; k++)
		{
			result.Max = Vector3.Max(result.Max, P_0[k]);
			result.Min = Vector3.Min(result.Min, P_0[k]);
		}
		return result;
	}

	internal static Plane a_0014(Vector3 P_0, Vector3 P_1)
	{
		Plane result = default;
		result.D = 0f - Vector3.Dot(P_1, result.Normal = Vector3.Normalize(P_0));
		return result;
	}

	internal static Matrix ar(Vector3 P_0, Vector3 P_1, Vector3 P_2, Vector3 P_3)
	{
		return new Matrix(P_0.X, P_0.Y, P_0.Z, 0f, P_1.X, P_1.Y, P_1.Z, 0f, P_2.X, P_2.Y, P_2.Z, 0f, P_3.X, P_3.Y, P_3.Z, 1f);
	}

	internal static Matrix aO(Vector3 P_0, Vector3 P_1)
	{
		float num = Vector3.Dot(P_0, P_1);
		float num2 = 1f + num;
		Quaternion quaternion;
		if ((double)num2 < 0.0001)
		{
			quaternion = new Quaternion(P_0.Z, P_0.X, P_0.Y, 0f);
		}
		else
		{
			Vector3 vector = Vector3.Cross(P_0, P_1);
			quaternion = new Quaternion(vector.X, vector.Y, vector.Z, num2);
		}
		quaternion.Normalize();
		return Matrix.CreateFromQuaternion(quaternion);
	}

	internal static Matrix aO(Vector3 P_0, Vector3 P_1, Vector3 P_2)
	{
		float num = MathHelper.Clamp(Vector3.Dot(P_0, P_1), 0f, 1f);
		Quaternion quaternion = Quaternion.CreateFromAxisAngle(P_2, (float)Math.Acos(num));
		return Matrix.CreateFromQuaternion(quaternion);
	}

	internal static int au(int P_0, int P_1)
	{
		return (P_0 ^ 1) + (P_1 ^ 2);
	}

	internal static int au(int P_0, int P_1, int P_2)
	{
		return (P_0 ^ 1) + (P_1 ^ 2) + (P_2 ^ 3);
	}

	internal static int au(int P_0, int P_1, int P_2, int P_3)
	{
		return (P_0 ^ 1) + (P_1 ^ 2) + (P_2 ^ 3) + (P_3 ^ 4);
	}

	internal static int a1(int P_0, int P_1, int P_2)
	{
		int num = 0;
		for (int k = 0; k < P_2; k++)
		{
			num += P_0 * P_1;
			P_0 >>= 1;
			P_1 >>= 1;
			P_0 = Math.Max(P_0, 1);
			P_0 = Math.Max(P_1, 1);
		}
		return num;
	}

	internal static Vector3 a_0016(Quaternion P_0)
	{
		float num = P_0.W;
		float y = P_0.Y;
		float x = P_0.X;
		float z = P_0.Z;
		return new Vector3
		{
			X = (float)Math.Atan2(2f * (num * y + x * z), 1.0 - 2.0 * (Math.Pow(y, 2.0) + Math.Pow(x, 2.0))),
			Y = (float)Math.Asin(2f * (num * x - z * y)),
			Z = (float)Math.Atan2(2f * (num * z + y * x), 1.0 - 2.0 * (Math.Pow(x, 2.0) + Math.Pow(z, 2.0)))
		};
	}

	internal static float ax(ref BoundingBox P_0, ref Vector3 P_1, out Vector3 P_2)
	{
		Vector3.Clamp(ref P_1, ref P_0.Min, ref P_0.Max, out P_2);
		Vector3.DistanceSquared(ref P_1, ref P_2, out var result);
		return result;
	}

	internal static float aF(ref BoundingBox P_0, ref Vector3 P_1, out Vector3 P_2, out Vector3 P_3)
	{
		P_2 = P_1;
		P_3 = Vector3.UnitY;
		if (P_2.X > P_0.Max.X)
		{
			P_2.X = P_0.Max.X;
			P_3 = Vector3.UnitX;
		}
		else if (P_2.X < P_0.Min.X)
		{
			P_2.X = P_0.Min.X;
			P_3 = -Vector3.UnitX;
		}
		if (P_2.Z > P_0.Max.Z)
		{
			P_2.Z = P_0.Max.Z;
			P_3 = Vector3.UnitZ;
		}
		else if (P_2.Z < P_0.Min.Z)
		{
			P_2.Z = P_0.Min.Z;
			P_3 = -Vector3.UnitZ;
		}
		if (P_2.Y > P_0.Max.Y)
		{
			P_2.Y = P_0.Max.Y;
			P_3 = Vector3.UnitY;
		}
		else if (P_2.Y < P_0.Min.Y)
		{
			P_2.Y = P_0.Min.Y;
			P_3 = -Vector3.UnitY;
		}
		Vector3.DistanceSquared(ref P_1, ref P_2, out var result);
		return result;
	}

	internal static void aV(ref Vector3 P_0)
	{
		if (P_0 == Vector3.Zero)
		{
			P_0 = Vector3.UnitY;
			return;
		}
		float num = Math.Abs(P_0.X);
		float num2 = Math.Abs(P_0.Y);
		float num3 = Math.Abs(P_0.Z);
		if (num > num2)
		{
			if (num > num3)
			{
				P_0.Y = 0f;
				P_0.Z = 0f;
			}
			else
			{
				P_0.X = 0f;
				P_0.Y = 0f;
			}
		}
		else if (num2 > num3)
		{
			P_0.X = 0f;
			P_0.Z = 0f;
		}
		else
		{
			P_0.X = 0f;
			P_0.Y = 0f;
		}
		P_0.Normalize();
	}

	internal static Vector3 aA(Vector3 P_0, Vector3 P_1, Vector3 P_2)
	{
		float num = Vector3.DistanceSquared(P_1, P_2);
		if (num <= 0f)
		{
			return P_1;
		}
		Vector3 vector = P_0 - P_1;
		Vector3 vector2 = P_2 - P_1;
		float value = Vector3.Dot(vector, vector2) / num;
		value = MathHelper.Clamp(value, 0f, 1f);
		return P_1 + value * (P_2 - P_1);
	}

	internal static bool a0(Plane P_0, Plane P_1, Plane P_2, out Vector3 P_3)
	{
		Vector3 vector = Vector3.Cross(P_1.Normal, P_2.Normal);
		float num = Vector3.Dot(P_0.Normal, vector);
		if (num == 0f)
		{
			P_3 = default;
			return false;
		}
		P_3 = P_0.D * vector + P_1.D * Vector3.Cross(P_2.Normal, P_0.Normal) + P_2.D * Vector3.Cross(P_0.Normal, P_1.Normal);
		P_3 /= 0f - num;
		return true;
	}

	internal static float aK(float P_0, float P_1, Matrix P_2)
	{
		Vector4 vector = new Vector4(P_0, P_0, 0f - P_1, 1f);
		Vector4 vector2 = Vector4.Transform(vector, P_2);
		if (vector2.W <= 0f)
		{
			return 0f;
		}
		vector2 /= vector2.W;
		float num = Math.Max(Math.Abs(vector2.X), Math.Abs(vector2.Y));
		if (num <= 0f)
		{
			return 0f;
		}
		return num;
	}

	internal static float aK(float P_0, Matrix P_1, Matrix P_2, Matrix P_3)
	{
		Vector4 vector = Vector4.Transform(new Vector4(P_1.Translation, 1f), P_2);
		vector.X = P_0;
		vector.Y = P_0;
		Vector4 vector2 = Vector4.Transform(vector, P_3);
		if (vector2.W <= 0f)
		{
			return 0f;
		}
		vector2 /= vector2.W;
		float num = Math.Max(Math.Abs(vector2.X), Math.Abs(vector2.Y));
		if (num <= 0f)
		{
			return 0f;
		}
		return num;
	}

	internal static float aM(Matrix P_0, Matrix P_1, Matrix P_2)
	{
		float num = aK(200f, P_0, P_1, P_2);
		if (num > 0f)
		{
			return 1f / num;
		}
		return 0f;
	}

	internal static bool a_0006(Vector3 P_0, Viewport P_1, Matrix P_2, Matrix P_3, Matrix P_4, Matrix P_5, ref Vector3 P_6)
	{
		if (P_0.X < (float)P_1.X || P_0.X - (float)P_1.X > (float)P_1.Width || P_0.Y < (float)P_1.Y || P_0.Y - (float)P_1.Y > (float)P_1.Height)
		{
			return false;
		}
		P_6 = P_1.Unproject(P_0, P_5, P_3, P_2);
		fNa.Matrix = P_3 * P_5;
		if (fNa.Near.DotCoordinate(P_6) > 0f)
		{
			P_6 = P_4.Translation - (P_6 - P_4.Translation);
		}
		return true;
	}

	internal static bool aJ(Vector3 P_0, Viewport P_1, Matrix P_2, Matrix P_3, Matrix P_4, Matrix P_5, ref Vector3 P_6)
	{
		if (a_0014(P_4.Forward, P_4.Translation).DotCoordinate(P_0) <= 0f)
		{
			return false;
		}
		P_6 = P_1.Project(P_0, P_5, P_3, P_2);
		return true;
	}

	internal static Rectangle a_0004(BoundingBox P_0, Viewport P_1, Matrix P_2, Matrix P_3)
	{
		if (P_0.Contains(P_3.Translation) == ContainmentType.Contains)
		{
			return new Rectangle(P_1.X, P_1.Y, P_1.Width, P_1.Height);
		}
		P_0.GetCorners(fNg);
		for (int k = 0; k < 8; k++)
		{
			Vector4 vector = Vector4.Transform(fNg[k], P_2);
			if (vector.W == 0f)
			{
				return new Rectangle(P_1.X, P_1.Y, P_1.Width, P_1.Height);
			}
			if (vector.W < 0f)
			{
				vector /= vector.W * -0.5f;
			}
			else
			{
				vector /= vector.W;
			}
			vector.Y *= -1f;
			vector = vector * 0.5f + Vector4.One * 0.5f;
			ref Vector3 reference = ref fNg[k];
			reference = Vector3.Clamp(new Vector3(vector.X, vector.Y, vector.Z), Vector3.Zero, Vector3.One) * new Vector3(P_1.Width, P_1.Height, 0f);
		}
		BoundingBox boundingBox = a_000F(fNg);
		return new Rectangle(P_1.X + (int)boundingBox.Min.X, P_1.Y + (int)boundingBox.Min.Y, (int)boundingBox.Max.X - (int)boundingBox.Min.X, (int)boundingBox.Max.Y - (int)boundingBox.Min.Y);
	}

	internal static Texture2D a3(GraphicsDevice P_0, Texture2D P_1)
	{
		return P_1;
	}
}
internal class _0012
{
	private static int fNB;

	private static float fNa;

	private static double fNg;

	private static StringBuilder fNf = new StringBuilder();

	private void a_0010(StringBuilder P_0, int P_1, int P_2)
	{
		if (P_1 <= 0)
		{
			P_0.Append("0");
			return;
		}
		int length = P_0.Length;
		int num = 0;
		while (P_1 > 0)
		{
			int num2 = P_1 / 10;
			switch (P_1 - num2 * 10)
			{
			case 0:
				P_0.Insert(length, "0");
				break;
			case 1:
				P_0.Insert(length, "1");
				break;
			case 2:
				P_0.Insert(length, "2");
				break;
			case 3:
				P_0.Insert(length, "3");
				break;
			case 4:
				P_0.Insert(length, "4");
				break;
			case 5:
				P_0.Insert(length, "5");
				break;
			case 6:
				P_0.Insert(length, "6");
				break;
			case 7:
				P_0.Insert(length, "7");
				break;
			case 8:
				P_0.Insert(length, "8");
				break;
			case 9:
				P_0.Insert(length, "9");
				break;
			}
			num++;
			if (num == P_2 && num != 0 && P_1 > 0)
			{
				P_0.Insert(length, ".");
			}
			P_1 = num2;
		}
		if (num < P_2)
		{
			for (int k = num; k < P_2; k++)
			{
				P_0.Insert(length, "0");
			}
			P_0.Insert(length, ".");
		}
	}

	internal Vector2 _5(SpriteBatch P_0, SpriteFont P_1, ref Vector2 P_2, Vector2 P_3, Color P_4)
	{
		P_0.DrawString(P_1, fNf, P_2, P_4, 0f, Vector2.Zero, P_3, SpriteEffects.None, 0f);
		P_2.Y += 14f * P_3.Y;
		return new Vector2((float)fNf.Length * 8f * P_3.X, 14f * P_3.Y);
	}

	internal void a6(string P_0)
	{
		fNf.Length = 0;
		fNf.Append(P_0);
	}

	internal void aS(string P_0, int P_1)
	{
		fNf.Length = 0;
		fNf.Append(P_0);
		fNf.Append(": ");
		a_0010(fNf, P_1, 0);
	}

	private void aS(string P_0, float P_1)
	{
		fNf.Length = 0;
		fNf.Append(P_0);
		fNf.Append(": ");
		a_0010(fNf, (int)(P_1 * 100f), 2);
	}

	internal void aI(string P_0, GameTime P_1, bool P_2)
	{
		double totalSeconds = P_1.TotalGameTime.TotalSeconds;
		if (totalSeconds - fNg >= 0.5)
		{
			fNa = (float)fNB / (float)(totalSeconds - fNg);
			fNB = 0;
			fNg = totalSeconds;
		}
		aS(P_0, fNa);
		if (P_2)
		{
			fNB++;
		}
	}

	public override string ToString()
	{
		return fNf.ToString();
	}
}
internal class _0015
{
	private struct nNB
	{
		internal string fNB;

		internal float fNa;

		internal nNB(string P_0, float P_1)
		{
			fNB = P_0;
			fNa = P_1;
		}
	}

	private static List<nNB> fNB = new List<nNB>(32);

	internal static void ay(string P_0)
	{
		fNB.Add(new nNB(P_0, -1f));
	}

	internal static void aH(string P_0, float P_1)
	{
		fNB.Add(new nNB(P_0, P_1));
	}

	internal static void ab(string P_0)
	{
	}
}
internal class _0005<T> : global::L.i<T> where T : IDisposable, new()
{
	public override void Clear()
	{
		FreeAllTracked();
		foreach (T item in _UnusedObjectPool)
		{
			item.Dispose();
		}
		_UnusedObjectPool.Clear();
		if (_LostObjectCount > 0)
		{
			throw new Exception("Some tracked pool objects were not disposed.");
		}
	}

	public void Unload()
	{
		Clear();
	}
}
internal class _0011
{
	internal const string fNB = "2.0.16.5";

	internal const string fNa = "Pro";

	internal const string fNg = "Synapse Gaming - SunBurn Pro";
}
