using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Shadows;

public class ShadowRenderTargetGroup : SafeSingletonBeginableObject, IDisposable
{
	private RenderTarget2D l8a;

	private Viewport l8_u0013_;

	private List<ShadowGroup> l8N = new List<ShadowGroup>(16);

	private Viewport l8l;

	private RenderTargetBinding[] l8L;

	public RenderTarget2D RenderTarget => l8a;

	public Viewport Viewport => l8_u0013_;

	public List<ShadowGroup> ShadowGroups => l8N;

	public bool ContentsAreValid
	{
		get
		{
			foreach (ShadowGroup item in l8N)
			{
				if (!item.Shadow.ContentsAreValid)
				{
					return false;
				}
			}
			return true;
		}
	}

	public bool HasShadows()
	{
		return l8a != null;
	}

	public void Build(RenderTarget2D shadowmaprendertarget)
	{
		l8a = shadowmaprendertarget;
		if (shadowmaprendertarget != null)
		{
			l8_u0013_.X = 0;
			l8_u0013_.Y = 0;
			l8_u0013_.Width = shadowmaprendertarget.Width;
			l8_u0013_.Height = shadowmaprendertarget.Height;
			l8_u0013_.MinDepth = 0f;
			l8_u0013_.MaxDepth = 1f;
		}
		else
		{
			l8_u0013_ = default;
		}
	}

	public void Dispose()
	{
		l8a = null;
		l8N.Clear();
		l8L = null;
	}

	public override void Begin()
	{
		base.Begin();
		if (l8a == null)
		{
			throw new Exception("Render target is null. This group dosn't contain shadows, begin cannot be called.");
		}
		if (l8a == null)
		{
			throw new Exception("Unsupported render target type. Must be RenderTarget2D.");
		}
		GraphicsDevice graphicsDevice = l8a.GraphicsDevice;
		l8l = graphicsDevice.Viewport;
		l8L = graphicsDevice.GetRenderTargets();
		graphicsDevice.SetRenderTarget(l8a);
		graphicsDevice.Viewport = l8_u0013_;
		graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer | ClearOptions.Stencil, Color.White, 1f, 0);
	}

	public override void End()
	{
		base.End();
		if (l8a != null)
		{
			GraphicsDevice graphicsDevice = l8a.GraphicsDevice;
			graphicsDevice.SetRenderTargets(l8L);
			graphicsDevice.Viewport = l8l;
		}
	}
}
