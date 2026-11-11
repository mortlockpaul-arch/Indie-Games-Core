using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace x;

internal class h
{
	protected class w8a
	{
		internal SystemStatistic l8a = SystemConsole.GetStatistic("Renderer_BatchSamplerChanges", SystemStatisticCategory.Rendering);
	}

	protected w8a Statistics = new w8a();

	private bool l8a = true;

	private SamplerState l8_u0013_;

	private static Dictionary<int, SamplerState> l8N = new Dictionary<int, SamplerState>(16);

	internal void L_u0012_()
	{
		l8a = true;
	}

	internal static void LO(GraphicsDevice param_0)
	{
		for (int i = 0; i < 16; i++)
		{
			param_0.SamplerStates[i] = SamplerState.PointClamp;
		}
		for (int j = 0; j < 2; j++)
		{
			param_0.VertexSamplerStates[j] = SamplerState.PointWrap;
		}
	}

	internal void L_u000F_(GraphicsDevice param_0)
	{
		if (l8a || l8_u0013_ != SamplerState.PointClamp)
		{
			Statistics.l8a.AccumulationValue++;
			l8a = false;
			l8_u0013_ = SamplerState.PointClamp;
			for (int i = 0; i < 16; i++)
			{
				param_0.SamplerStates[i] = SamplerState.PointClamp;
			}
			for (int j = 0; j < 2; j++)
			{
				param_0.VertexSamplerStates[j] = SamplerState.PointWrap;
			}
		}
	}

	internal void LF(GraphicsDevice param_0, SamplerState param_1, bool param_2)
	{
		if (!l8a && l8_u0013_ == param_1)
		{
			return;
		}
		Statistics.l8a.AccumulationValue++;
		l8a = false;
		l8_u0013_ = param_1;
		for (int i = 0; i < 16; i++)
		{
			Texture texture = param_0.Textures[i];
			if (texture != null && texture.Format >= SurfaceFormat.Single)
			{
				param_0.SamplerStates[i] = SamplerState.PointClamp;
			}
			else
			{
				param_0.SamplerStates[i] = param_1;
			}
			if (param_2 && texture != null)
			{
				GC.SuppressFinalize(texture);
			}
		}
		for (int j = 0; j < 2; j++)
		{
			param_0.VertexSamplerStates[j] = SamplerState.PointWrap;
		}
	}

	internal SamplerState Lq(GraphicsDevice param_0, TextureAddressMode param_1, TextureAddressMode param_2, TextureAddressMode param_3, TextureFilter param_4, int param_5)
	{
		int key = (int)(param_1 + ((int)param_2 << 2) + ((int)param_3 << 4) + ((int)param_4 << 6) + (param_5 << 10));
		if (!l8N.TryGetValue(key, out var value))
		{
			value = new SamplerState();
			value.AddressU = param_1;
			value.AddressV = param_2;
			value.AddressW = param_3;
			value.Filter = param_4;
			value.MaxAnisotropy = param_5;
			l8N.Add(key, value);
		}
		return value;
	}
}
