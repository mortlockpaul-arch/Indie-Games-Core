using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;

namespace B
{
	internal class w
	{
		private bool l8a;

		private IGraphicsDeviceService l8_u0013_;

		private int l8N;

		private int l8l;

		private int l8L;

		internal bool Changed
		{
			get
			{
				PresentationParameters presentationParameters = l8_u0013_.GraphicsDevice.PresentationParameters;
				if (l8a || presentationParameters.BackBufferWidth != l8N || presentationParameters.BackBufferHeight != l8l || presentationParameters.MultiSampleCount != l8L)
				{
					l8N = presentationParameters.BackBufferWidth;
					l8l = presentationParameters.BackBufferHeight;
					l8L = presentationParameters.MultiSampleCount;
					l8a = false;
					return true;
				}
				return false;
			}
		}

		internal w()
		{
			IGraphicsDeviceService graphicsDeviceManager = SunBurnCoreSystem.Instance.GraphicsDeviceManager;
			l8_u0013_ = graphicsDeviceManager;
			l8_u0013_.DeviceCreated += _u0013_h;
			l8_u0013_.DeviceReset += _u0013_h;
			l8_u0013_.DeviceDisposing += _u0013_h;
		}

		private void _u0013_h(object param_0, EventArgs param_1)
		{
			l8a = true;
		}
	}
}
namespace b
{
	internal class w : L
	{
		private Vector3 l8a;

		private Texture2D l8_u0013_;

		private EffectParameter l8N;

		private EffectParameter l8l;

		public Texture2D BloomTexture
		{
			get
			{
				return l8_u0013_;
			}
			set
			{
				EffectHelper.lh(value, ref l8_u0013_, ref l8l);
			}
		}

		public void SetSaturationDarkenAndCinematic(float saturation, float darken, float cinematic)
		{
			EffectHelper.lw(new Vector3(saturation, darken, cinematic), ref l8a, ref l8N);
		}

		public w(GraphicsDevice graphicsdevice)
			: base(graphicsdevice)
		{
			CurrentTechnique = Techniques["HDRFinalBlendPointFilteringTechnique"];
			l8l = Parameters["_BloomTexture"];
			l8N = Parameters["_Saturation_Darken_Cinematic"];
		}

		protected override void SetTechnique()
		{
			Texture2D sourceTexture = SourceTexture;
			bool flag = sourceTexture != null && sourceTexture.Format < SurfaceFormat.Single;
			if (DynamicRangeEnabled)
			{
				if (flag)
				{
					CurrentTechnique = Techniques["HDRFinalBlendLinearFilteringTechnique"];
				}
				else
				{
					CurrentTechnique = Techniques["HDRFinalBlendPointFilteringTechnique"];
				}
			}
			else if (flag)
			{
				CurrentTechnique = Techniques["HDRFinalBlendLinearFilteringBloomOnlyTechnique"];
			}
			else
			{
				CurrentTechnique = Techniques["HDRFinalBlendPointFilteringBloomOnlyTechnique"];
			}
		}
	}
}
