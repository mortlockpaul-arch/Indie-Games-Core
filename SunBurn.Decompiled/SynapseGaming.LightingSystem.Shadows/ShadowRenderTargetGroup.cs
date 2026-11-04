using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Shadows;

/// <summary>
/// Class that manages shadow groups sharing the same render target.
/// </summary>
public class ShadowRenderTargetGroup : SafeSingletonBeginableObject, IDisposable
{
	private GraphicsDevice fNB;

	private RenderTarget2D fNa;

	private Viewport fNg;

	private List<ShadowGroup> fNf = new List<ShadowGroup>(16);

	private Viewport fN_0003;

	private RenderTargetBinding[] fNn;

	/// <summary>
	/// The current RenderTarget used by this object.
	/// </summary>
	public RenderTarget2D RenderTarget => fNa;

	/// <summary>
	/// Viewport that encapsulates the entire render target.
	/// </summary>
	public Viewport Viewport => fNg;

	/// <summary>
	/// List of shadow groups managed by this object.
	/// </summary>
	public List<ShadowGroup> ShadowGroups => fNf;

	/// <summary>
	/// Used to determine if the render target contents are valid or if the contents need
	/// to be re-rendered.
	///
	/// The default SunBurn shadow mapping implementation renders shadow map contents
	/// every frame, however custom implementations can provide static shadow maps.
	///
	/// Please note: if shadow maps are static and the contents are valid DO NOT call
	/// ShadowRenderTargetGroup Begin() and End().  On the Xbox this will invalidate the
	/// render target data.
	///
	/// However skipping calls to Begin and End require calling
	/// ShadowRenderTargetGroup.UpdateRenderTargetTexture() to ensure the shadow texture
	/// is up to date.
	///
	/// When using the built-in render managers this is all handled automatically.
	/// </summary>
	public bool ContentsAreValid
	{
		get
		{
			foreach (ShadowGroup item in fNf)
			{
				if (item.Shadow is IShadowMap && !(item.Shadow as IShadowMap).ContentsAreValid)
				{
					return false;
				}
			}
			return true;
		}
	}

	/// <summary>
	/// Creates a new ShadowRenderTargetGroup instance.
	/// </summary>
	public ShadowRenderTargetGroup()
	{
	}

	/// <summary>
	/// Determines if the render target group uses shadows.
	/// </summary>
	/// <returns></returns>
	public bool HasShadows()
	{
		return fNa != null;
	}

	/// <summary>
	/// Builds the render target group information based on the
	/// provided render target and depth buffer.
	/// </summary>
	/// <param name="device"></param>
	/// <param name="shadowmaprendertarget"></param>
	public void Build(GraphicsDevice device, RenderTarget2D shadowmaprendertarget)
	{
		fNB = device;
		fNa = shadowmaprendertarget;
		if (shadowmaprendertarget != null)
		{
			fNg.X = 0;
			fNg.Y = 0;
			fNg.Width = shadowmaprendertarget.Width;
			fNg.Height = shadowmaprendertarget.Height;
			fNg.MinDepth = 0f;
			fNg.MaxDepth = 1f;
		}
		else
		{
			fNg = default;
		}
	}

	/// <summary>
	/// Releases resources allocated by this object.
	/// </summary>
	public void Dispose()
	{
		fNB = null;
		fNa = null;
		fNf.Clear();
		fNn = null;
	}

	/// <summary>
	/// Sets up the render target group for generating the shadow maps.
	/// </summary>
	public override void Begin()
	{
		base.Begin();
		if (fNa == null)
		{
			throw new Exception("Render target is null. This group dosn't contain shadows, begin cannot be called.");
		}
		if (fNa == null)
		{
			throw new Exception("Unsupported render target type. Must be RenderTarget2D.");
		}
		fN_0003 = fNB.Viewport;
		fNn = fNB.GetRenderTargets();
		fNB.SetRenderTarget(fNa);
		fNB.Viewport = fNg;
		fNB.Clear(ClearOptions.Target | ClearOptions.DepthBuffer | ClearOptions.Stencil, Color.White, 1f, 0);
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public override void End()
	{
		base.End();
		if (fNa != null)
		{
			fNB.SetRenderTargets(fNn);
			fNB.Viewport = fN_0003;
		}
	}
}
