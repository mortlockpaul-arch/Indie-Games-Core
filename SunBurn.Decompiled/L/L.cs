using System;
using Microsoft.Xna.Framework.Graphics;

namespace L;

internal class L
{
	private bool fNB;

	private IGraphicsDeviceService fNa;

	private int fNg;

	private int fNf;

	private int fN_0003;

	internal bool Changed
	{
		get
		{
			PresentationParameters presentationParameters = fNa.GraphicsDevice.PresentationParameters;
			if (fNB || presentationParameters.BackBufferWidth != fNg || presentationParameters.BackBufferHeight != fNf || presentationParameters.MultiSampleCount != fN_0003)
			{
				fNg = presentationParameters.BackBufferWidth;
				fNf = presentationParameters.BackBufferHeight;
				fN_0003 = presentationParameters.MultiSampleCount;
				fNB = false;
				return true;
			}
			return false;
		}
	}

	internal L(IGraphicsDeviceService P_0)
	{
		fNa = P_0;
		fNa.DeviceCreated += at;
		fNa.DeviceReset += at;
		fNa.DeviceDisposing += at;
	}

	private void at(object P_0, EventArgs P_1)
	{
		fNB = true;
	}
}
