using System;
using Microsoft.Xna.Framework.Graphics;
using y;

namespace SynapseGaming.LightingSystem.Lights;

public class LightMap : IDisposable
{
	private Texture2D l8a;

	private Texture2D l8_u0013_;

	private static byte[] l8N;

	public Texture2D LightMapColorTexture => l8a;

	public Texture2D LightMapDirectionalTexture
	{
		get
		{
			return l8_u0013_;
		}
		internal set
		{
			l8_u0013_ = value;
		}
	}

	internal static byte[] lq(int param_0)
	{
		if (l8N == null || l8N.Length < param_0)
		{
			l8N = new byte[param_0];
		}
		return l8N;
	}

	public LightMap(Texture2D colortexture, Texture2D directionaltexture)
	{
		l8a = colortexture;
		l8_u0013_ = directionaltexture;
	}

	public LightMap(GraphicsDevice device, int colorwidth, int colorheight, int dirwidth, int dirheight, bool mipmap, SurfaceFormat format)
	{
		l8a = new Texture2D(device, colorwidth, colorheight, mipmap, format);
		l8_u0013_ = new Texture2D(device, dirwidth, dirheight, mipmap, format);
	}

	public void Dispose()
	{
		y.a.w_u0005_(ref l8a);
		y.a.w_u0005_(ref l8_u0013_);
	}
}
