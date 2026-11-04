using Microsoft.Xna.Framework.Graphics;

namespace R;

internal class B
{
	internal static BlendState fNB;

	internal static BlendState fNa;

	internal static BlendState fNg;

	internal static BlendState fNf;

	internal static BlendState fN_0003;

	internal static DepthStencilState fNn;

	internal static DepthStencilState fN_0019;

	internal static DepthStencilState fNL;

	internal static DepthStencilState fNv;

	internal static DepthStencilState fN_0012;

	internal static BlendState fN_0015;

	internal static DepthStencilState fN8;

	internal static DepthStencilState fNi;

	internal static DepthStencilState fN_0005;

	internal static DepthStencilState fNj;

	internal static BlendState fNo;

	internal static RasterizerState[,] fNc;

	internal static RasterizerState[,] fNd;

	static B()
	{
		fNB = new BlendState
		{
			ColorSourceBlend = Blend.One,
			AlphaSourceBlend = Blend.One,
			ColorDestinationBlend = Blend.One,
			AlphaDestinationBlend = Blend.One
		};
		fNa = new BlendState();
		fNg = new BlendState
		{
			ColorSourceBlend = Blend.One,
			ColorDestinationBlend = Blend.Zero,
			ColorWriteChannels = ColorWriteChannels.Alpha
		};
		fNf = new BlendState
		{
			ColorSourceBlend = Blend.DestinationAlpha,
			AlphaSourceBlend = Blend.DestinationAlpha,
			ColorDestinationBlend = Blend.One,
			AlphaDestinationBlend = Blend.One,
			ColorWriteChannels = (ColorWriteChannels.Red | ColorWriteChannels.Green | ColorWriteChannels.Blue)
		};
		fN_0003 = new BlendState
		{
			ColorSourceBlend = Blend.DestinationAlpha,
			ColorDestinationBlend = Blend.One,
			AlphaSourceBlend = Blend.One,
			AlphaDestinationBlend = Blend.Zero
		};
		fNn = new DepthStencilState
		{
			DepthBufferEnable = true,
			DepthBufferWriteEnable = false,
			DepthBufferFunction = CompareFunction.Equal
		};
		fN_0019 = new DepthStencilState
		{
			DepthBufferEnable = true,
			DepthBufferWriteEnable = false,
			DepthBufferFunction = CompareFunction.Greater
		};
		fNL = new DepthStencilState
		{
			DepthBufferEnable = true,
			DepthBufferWriteEnable = false,
			DepthBufferFunction = CompareFunction.Less
		};
		fNv = new DepthStencilState
		{
			DepthBufferEnable = true,
			DepthBufferWriteEnable = false,
			DepthBufferFunction = CompareFunction.LessEqual
		};
		fN_0012 = new DepthStencilState
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
		fN_0015 = new BlendState
		{
			ColorWriteChannels = ColorWriteChannels.None
		};
		fN8 = new DepthStencilState
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
		fNi = new DepthStencilState
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
		fN_0005 = new DepthStencilState
		{
			StencilEnable = true,
			StencilFunction = CompareFunction.Always,
			StencilPass = StencilOperation.Replace,
			StencilFail = StencilOperation.Keep,
			StencilDepthBufferFail = StencilOperation.Keep,
			StencilMask = int.MaxValue,
			StencilWriteMask = int.MaxValue
		};
		fNj = new DepthStencilState
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
		fNo = new BlendState
		{
			ColorSourceBlend = Blend.SourceAlpha,
			ColorDestinationBlend = Blend.InverseSourceAlpha
		};
		fNc = new RasterizerState[3, 2];
		fNd = new RasterizerState[3, 2];
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
				fNc[i, j] = rasterizerState;
				fNd[i, j] = rasterizerState2;
			}
		}
	}

	internal static void _0003l(GraphicsDevice P_0, FillMode P_1, CullMode P_2, bool P_3, bool P_4, bool P_5)
	{
		RasterizerState[,] array = ((!P_5) ? fNc : fNd);
		if (P_4)
		{
			P_0.RasterizerState = array[0, (int)P_1];
		}
		else if (P_2 == CullMode.None || !P_3)
		{
			P_0.RasterizerState = array[(int)P_2, (int)P_1];
		}
		else if (P_2 == CullMode.CullCounterClockwiseFace)
		{
			P_0.RasterizerState = array[1, (int)P_1];
		}
		else
		{
			P_0.RasterizerState = array[2, (int)P_1];
		}
	}
}
