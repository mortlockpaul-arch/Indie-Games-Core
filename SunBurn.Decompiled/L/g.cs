using System;
using _0013;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Effects;

namespace L;

internal class g : IDisposable
{
	private GraphicsDevice fNB;

	private BasicEffect fNa;

	private VertexBuffer fNg;

	private IndexBuffer fNf;

	private static Vector3[] fN_0003 = new Vector3[8]
	{
		new Vector3(1f, 1f, 1f),
		new Vector3(0f, 1f, 1f),
		new Vector3(1f, 0f, 1f),
		new Vector3(0f, 0f, 1f),
		new Vector3(1f, 1f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, 0f, 0f)
	};

	private static int[] fNn = new int[24]
	{
		1, 0, 3, 2, 4, 5, 6, 7, 0, 4,
		2, 6, 5, 1, 7, 3, 0, 1, 4, 5,
		3, 2, 7, 6
	};

	internal BasicEffect DefaultEffect => fNa;

	internal g(GraphicsDevice P_0)
	{
		fNB = P_0;
		fNa = new SunBurnBasicEffect(fNB);
		fNa.TextureEnabled = false;
		fNa.VertexColorEnabled = false;
		fNa.PreferPerPixelLighting = false;
		fNa.LightingEnabled = false;
		fNa.FogEnabled = false;
		fNa.SpecularColor = Vector3.Zero;
		VertexPositionTexture[] array = new VertexPositionTexture[24];
		short[] array2 = new short[36];
		int num = 0;
		int num2 = 0;
		for (int k = 0; k < 6; k++)
		{
			Vector3 position = fN_0003[fNn[num]];
			Vector3 position2 = fN_0003[fNn[num + 1]];
			Vector3 position3 = fN_0003[fNn[num + 2]];
			Vector3 position4 = fN_0003[fNn[num + 3]];
			array[num].Position = position;
			array[num + 1].Position = position2;
			array[num + 2].Position = position3;
			array[num + 3].Position = position4;
			array2[num2++] = (byte)num;
			array2[num2++] = (byte)(num + 1);
			array2[num2++] = (byte)(num + 2);
			array2[num2++] = (byte)(num + 3);
			array2[num2++] = (byte)(num + 2);
			array2[num2++] = (byte)(num + 1);
			num += 4;
		}
		fNg = new VertexBuffer(fNB, typeof(VertexPositionTexture), array.Length, BufferUsage.WriteOnly);
		fNg.SetData(array);
		fNf = new IndexBuffer(fNB, typeof(short), array2.Length, BufferUsage.WriteOnly);
		fNf.SetData(array2);
	}

	internal Matrix e(BoundingBox P_0)
	{
		Matrix result = Matrix.CreateScale(P_0.Max - P_0.Min);
		result.Translation = P_0.Min;
		return result;
	}

	internal void _5()
	{
		fNB.SetVertexBuffer(fNg);
		fNB.Indices = fNf;
		fNB.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 24, 0, 12);
	}

	public void Dispose()
	{
		_0013.B.nu(ref fNa);
		_0013.B.nu(ref fNg);
		_0013.B.nu(ref fNf);
	}
}
