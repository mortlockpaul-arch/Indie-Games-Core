using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace B
{
	internal interface J
	{
		string ProjectFile { get; }
	}
}
namespace b
{
	internal class J : L
	{
		public new interface w8a
		{
			void GenerateSampleData(int texturewidth, int textureheight, int samplecount, ref Vector2[] generateduvs, ref float[] generatedweights);
		}

		public class w8_u0013_ : w8a
		{
			private const float l8a = 8.5f;

			public virtual void GenerateSampleData(int texturewidth, int textureheight, int samplecount, ref Vector2[] generateduvs, ref float[] generatedweights)
			{
				float texelstride = 1f / (float)texturewidth;
				float gaussbase = GenerateGaussianBase();
				float gaussexp = GenerateGaussianExp();
				for (int i = 0; i < samplecount; i++)
				{
					ref Vector2 reference = ref generateduvs[i];
					reference = new Vector2(GenerateUVOffset(i, samplecount, texelstride), 0f);
					generatedweights[i] = GenerateWeight(i, samplecount, gaussbase, gaussexp);
				}
				NormalizeKernel(ref generatedweights);
			}

			protected void NormalizeKernel(ref float[] generatedweights)
			{
				float num = 0f;
				float[] array = generatedweights;
				foreach (float num2 in array)
				{
					num += num2;
				}
				if (num != 0f)
				{
					float num3 = 1f / num;
					for (int j = 0; j < generatedweights.Length; j++)
					{
						generatedweights[j] *= num3;
					}
				}
			}

			protected float GenerateGaussianBase()
			{
				return 1f / (float)Math.Sqrt(Math.PI * 17.0);
			}

			protected float GenerateGaussianExp()
			{
				return -0.0069204154f;
			}

			protected float GenerateUVOffset(int index, int count, float texelstride)
			{
				float num = (float)count * 0.5f - 0.5f;
				num = (float)index - num;
				return num * texelstride;
			}

			protected float GenerateWeight(int index, int count, float gaussbase, float gaussexp)
			{
				float num = (float)count * 0.5f - 0.5f;
				num = (float)index - num;
				return gaussbase * (float)Math.Exp(num * num * gaussexp);
			}
		}

		public class w8N : w8_u0013_
		{
			public override void GenerateSampleData(int texturewidth, int textureheight, int samplecount, ref Vector2[] generateduvs, ref float[] generatedweights)
			{
				float texelstride = 1f / (float)textureheight;
				float gaussbase = GenerateGaussianBase();
				float gaussexp = GenerateGaussianExp();
				for (int i = 0; i < samplecount; i++)
				{
					ref Vector2 reference = ref generateduvs[i];
					reference = new Vector2(0f, GenerateUVOffset(i, samplecount, texelstride));
					generatedweights[i] = GenerateWeight(i, samplecount, gaussbase, gaussexp);
				}
				NormalizeKernel(ref generatedweights);
			}
		}

		private const int l8a = 32;

		public static w8a HorizontalBlurKernel = new w8_u0013_();

		public static w8a VerticalKernelKernel = new w8N();

		private w8a l8_u0013_;

		private DetailPreference l8N;

		private EffectParameter l8l;

		private EffectParameter l8L;

		private static Vector2[] l8w = new Vector2[32];

		private static float[] l8J = new float[32];

		private static Vector4[] l8B = new Vector4[16];

		private static Vector2[] l8s = new Vector2[16];

		public w8a BlurKernel
		{
			get
			{
				return l8_u0013_;
			}
			set
			{
				l8_u0013_ = value;
				lW();
			}
		}

		public DetailPreference BloomDetail
		{
			get
			{
				return l8N;
			}
			set
			{
				l8N = value;
				lW();
				SetTechnique();
			}
		}

		public override Texture2D SourceTexture
		{
			get
			{
				return base.SourceTexture;
			}
			set
			{
				base.SourceTexture = value;
				lW();
			}
		}

		private void lW()
		{
			if (l8_u0013_ != null && SourceTexture != null)
			{
				int samplecount;
				if (l8N != DetailPreference.High)
				{
					samplecount = ((l8N != DetailPreference.Medium) ? 8 : 16);
				}
				else
				{
					samplecount = 32;
				}
				l8_u0013_.GenerateSampleData(SourceTexture.Width, SourceTexture.Height, samplecount, ref l8w, ref l8J);
				int num = 0;
				for (int i = 0; i < l8B.Length; i++)
				{
					Vector2 value = l8w[num];
					float x = l8J[num++];
					Vector2 vector = l8w[num];
					float y = l8J[num++];
					ref Vector4 reference = ref l8B[i];
					reference = new Vector4(value, vector.X, vector.Y);
					ref Vector2 reference2 = ref l8s[i];
					reference2 = new Vector2(x, y);
				}
				if (l8l != null)
				{
					l8l.SetValue(l8B);
				}
				if (l8L != null)
				{
					l8L.SetValue(l8s);
				}
			}
		}

		protected override void SetTechnique()
		{
			l8_u0016_.l8a.AccumulationValue++;
			if (l8N == DetailPreference.High)
			{
				CurrentTechnique = Techniques["HDRBloomBlurHighTechnique"];
			}
			else if (l8N == DetailPreference.Medium)
			{
				CurrentTechnique = Techniques["HDRBloomBlurMediumTechnique"];
			}
			else
			{
				CurrentTechnique = Techniques["HDRBloomBlurLowTechnique"];
			}
		}

		public J(GraphicsDevice graphicsdevice)
			: base(graphicsdevice)
		{
			CurrentTechnique = Techniques["HDRBloomBlurHighTechnique"];
			l8l = Parameters["_GaussianCoords"];
			l8L = Parameters["_GaussianWeights"];
			BloomDetail = DetailPreference.High;
		}
	}
}
