using Microsoft.Xna.Framework.Graphics;

namespace _u0004_;

internal class a
{
	internal static BlendState l8a;

	internal static BlendState l8_u0013_;

	internal static BlendState l8N;

	internal static BlendState l8l;

	internal static BlendState l8L;

	internal static DepthStencilState l8w;

	internal static DepthStencilState l8J;

	internal static DepthStencilState l8B;

	internal static DepthStencilState l8s;

	internal static DepthStencilState l8h;

	internal static BlendState l8_u0002_;

	internal static DepthStencilState l8v;

	internal static DepthStencilState l8Y;

	internal static DepthStencilState l8_u0005_;

	internal static DepthStencilState l8b;

	internal static BlendState l8I;

	internal static RasterizerState[,] l85;

	internal static RasterizerState[,] l8W;

	static a()
	{
		l8a = new BlendState
		{
			ColorSourceBlend = Blend.One,
			AlphaSourceBlend = Blend.One,
			ColorDestinationBlend = Blend.One,
			AlphaDestinationBlend = Blend.One
		};
		l8_u0013_ = new BlendState();
		l8N = new BlendState
		{
			ColorSourceBlend = Blend.One,
			ColorDestinationBlend = Blend.Zero,
			ColorWriteChannels = ColorWriteChannels.Alpha
		};
		l8l = new BlendState
		{
			ColorSourceBlend = Blend.DestinationAlpha,
			AlphaSourceBlend = Blend.DestinationAlpha,
			ColorDestinationBlend = Blend.One,
			AlphaDestinationBlend = Blend.One,
			ColorWriteChannels = (ColorWriteChannels.Red | ColorWriteChannels.Green | ColorWriteChannels.Blue)
		};
		l8L = new BlendState
		{
			ColorSourceBlend = Blend.DestinationAlpha,
			ColorDestinationBlend = Blend.One,
			AlphaSourceBlend = Blend.One,
			AlphaDestinationBlend = Blend.Zero
		};
		l8w = new DepthStencilState
		{
			DepthBufferEnable = true,
			DepthBufferWriteEnable = false,
			DepthBufferFunction = CompareFunction.Equal
		};
		l8J = new DepthStencilState
		{
			DepthBufferEnable = true,
			DepthBufferWriteEnable = false,
			DepthBufferFunction = CompareFunction.Greater
		};
		l8B = new DepthStencilState
		{
			DepthBufferEnable = true,
			DepthBufferWriteEnable = false,
			DepthBufferFunction = CompareFunction.Less
		};
		l8s = new DepthStencilState
		{
			DepthBufferEnable = true,
			DepthBufferWriteEnable = false,
			DepthBufferFunction = CompareFunction.LessEqual
		};
		l8h = new DepthStencilState
		{
			DepthBufferEnable = true,
			DepthBufferWriteEnable = false,
			DepthBufferFunction = CompareFunction.Equal,
			StencilEnable = true,
			StencilFunction = CompareFunction.NotEqual,
			StencilPass = StencilOperation.Replace,
			StencilDepthBufferFail = StencilOperation.Keep,
			StencilFail = StencilOperation.Keep,
			StencilMask = int.MaxValue,
			StencilWriteMask = int.MaxValue,
			TwoSidedStencilMode = false
		};
		l8_u0002_ = new BlendState
		{
			ColorWriteChannels = ColorWriteChannels.None
		};
		l8v = new DepthStencilState
		{
			DepthBufferWriteEnable = false,
			StencilEnable = true,
			StencilFunction = CompareFunction.Always,
			StencilPass = StencilOperation.Replace,
			StencilFail = StencilOperation.Keep,
			StencilDepthBufferFail = StencilOperation.Keep,
			StencilMask = int.MaxValue,
			StencilWriteMask = int.MaxValue
		};
		l8Y = new DepthStencilState
		{
			DepthBufferWriteEnable = true,
			StencilEnable = true,
			StencilFunction = CompareFunction.Equal,
			StencilPass = StencilOperation.Keep,
			StencilFail = StencilOperation.Keep,
			StencilDepthBufferFail = StencilOperation.Keep,
			StencilMask = int.MaxValue,
			StencilWriteMask = int.MaxValue
		};
		l8_u0005_ = new DepthStencilState
		{
			StencilEnable = true,
			StencilFunction = CompareFunction.Always,
			StencilPass = StencilOperation.Replace,
			StencilFail = StencilOperation.Keep,
			StencilDepthBufferFail = StencilOperation.Keep,
			StencilMask = int.MaxValue,
			StencilWriteMask = int.MaxValue
		};
		l8b = new DepthStencilState
		{
			DepthBufferEnable = false,
			StencilEnable = true,
			StencilFunction = CompareFunction.Equal,
			StencilPass = StencilOperation.Keep,
			StencilFail = StencilOperation.Keep,
			StencilDepthBufferFail = StencilOperation.Keep,
			StencilMask = int.MaxValue,
			StencilWriteMask = int.MaxValue
		};
		l8I = new BlendState
		{
			ColorSourceBlend = Blend.SourceAlpha,
			ColorDestinationBlend = Blend.InverseSourceAlpha
		};
		l85 = new RasterizerState[3, 2];
		l8W = new RasterizerState[3, 2];
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				RasterizerState rasterizerState = new RasterizerState
				{
					CullMode = (CullMode)i,
					FillMode = (FillMode)j
				};
				RasterizerState rasterizerState2 = new RasterizerState
				{
					CullMode = (CullMode)i,
					FillMode = (FillMode)j,
					ScissorTestEnable = true
				};
				l85[i, j] = rasterizerState;
				l8W[i, j] = rasterizerState2;
			}
		}
	}

	internal static void Lx(GraphicsDevice param_0, FillMode param_1, CullMode param_2, bool param_3, bool param_4, bool param_5)
	{
		RasterizerState[,] array = ((!param_5) ? l85 : l8W);
		if (param_4)
		{
			param_0.RasterizerState = array[0, (int)param_1];
		}
		else if (param_2 == CullMode.None || !param_3)
		{
			param_0.RasterizerState = array[(int)param_2, (int)param_1];
		}
		else if (param_2 == CullMode.CullCounterClockwiseFace)
		{
			param_0.RasterizerState = array[1, (int)param_1];
		}
		else
		{
			param_0.RasterizerState = array[2, (int)param_1];
		}
	}
}
