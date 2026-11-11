using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

public class GraphicsDeviceSupport
{
	private int l8a;

	private int l8_u0013_;

	private Dictionary<SurfaceFormat, bool> l8N = new Dictionary<SurfaceFormat, bool>(16);

	public int MaxTextureSize => l8a;

	public int MaxAnisotropy => l8_u0013_;

	public Dictionary<SurfaceFormat, bool> SurfaceFormat => l8N;

	internal static bool _u0013__u0002_(bool param_0, bool param_1)
	{
		if (!param_0)
		{
			return false;
		}
		return param_1;
	}

	internal GraphicsDeviceSupport(GraphicsDevice param_0)
	{
		if (param_0.GraphicsProfile == GraphicsProfile.Reach)
		{
			l8a = 2048;
			l8_u0013_ = 4;
		}
		else
		{
			l8a = 4096;
			l8_u0013_ = 4;
		}
		DepthFormat depthStencilFormat = param_0.PresentationParameters.DepthStencilFormat;
		int multiSampleCount = param_0.PresentationParameters.MultiSampleCount;
		for (int i = 0; i < 19; i++)
		{
			SurfaceFormat surfaceFormat = (SurfaceFormat)i;
			bool flag = GraphicsAdapter.DefaultAdapter.QueryRenderTargetFormat(param_0.GraphicsProfile, surfaceFormat, depthStencilFormat, multiSampleCount, out var selectedFormat, out var _, out var _);
			l8N.Add(surfaceFormat, flag && surfaceFormat == selectedFormat);
		}
	}

	public SurfaceFormat FindSupportedFormat(SurfaceFormat[] requestedformats)
	{
		foreach (SurfaceFormat surfaceFormat in requestedformats)
		{
			if (SurfaceFormat[surfaceFormat])
			{
				return surfaceFormat;
			}
		}
		return Microsoft.Xna.Framework.Graphics.SurfaceFormat.Color;
	}
}
