using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using y;

namespace SynapseGaming.LightingSystem.Core;

public class FullFrameQuad : IDisposable
{
	private int l8a;

	private int l8_u0013_;

	private GraphicsDevice l8N;

	private VertexBuffer l8l;

	private VertexPositionTexture[] l8L = new VertexPositionTexture[4];

	private static VertexPositionTexture[] l8w = new VertexPositionTexture[4];

	public VertexBuffer VertexBuffer => l8l;

	public static void Render(GraphicsDevice device, int width, int height)
	{
		_u0013__u0013_(l8w, width, height, -Vector2.One, Vector2.One);
		device.DrawUserPrimitives(PrimitiveType.TriangleStrip, l8w, 0, 2);
	}

	private static void _u0013__u0013_(VertexPositionTexture[] param_0, int param_1, int param_2, Vector2 param_3, Vector2 param_4)
	{
		Vector3 vector = new Vector3(-0.5f / (float)param_1, 0.5f / (float)param_2, 0f);
		param_0[0].Position = new Vector3(param_3.X, param_4.Y, 0f) + vector;
		param_0[0].TextureCoordinate = new Vector2(0f, 0f);
		param_0[1].Position = new Vector3(param_4.X, param_4.Y, 0f) + vector;
		param_0[1].TextureCoordinate = new Vector2(1f, 0f);
		param_0[2].Position = new Vector3(param_3.X, param_3.Y, 0f) + vector;
		param_0[2].TextureCoordinate = new Vector2(0f, 1f);
		param_0[3].Position = new Vector3(param_4.X, param_3.Y, 0f) + vector;
		param_0[3].TextureCoordinate = new Vector2(1f, 1f);
	}

	public FullFrameQuad(GraphicsDevice device, int width, int height)
		: this(device, width, height, -Vector2.One, Vector2.One)
	{
	}

	public FullFrameQuad(GraphicsDevice device, int width, int height, Vector2 screenmin, Vector2 screenmax)
	{
		l8N = device;
		l8a = width;
		l8_u0013_ = height;
		_u0013__u0013_(l8L, width, height, screenmin, screenmax);
		l8l = new VertexBuffer(device, typeof(VertexPositionTexture), 4, BufferUsage.WriteOnly);
		l8l.SetData(l8L);
	}

	public void Render(Effect effect)
	{
		l8N.SetVertexBuffer(l8l);
		for (int i = 0; i < effect.CurrentTechnique.Passes.Count; i++)
		{
			effect.CurrentTechnique.Passes[i].Apply();
			l8N.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
		}
	}

	public void Render()
	{
		l8N.SetVertexBuffer(l8l);
		l8N.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
	}

	public void Dispose()
	{
		l8N = null;
		y.a.w_u0005_(ref l8l);
	}
}
