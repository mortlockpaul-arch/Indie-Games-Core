using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;

namespace j;

internal class _0003 : BaseRenderableEffect
{
	public new enum nNB
	{
		ConvertData,
		Normal,
		BlendIntensity
	}

	private bool fNB = true;

	private nNB fNa;

	private Vector2 fNg;

	private Texture2D fNf;

	private Vector4 fN_0003;

	private Vector4 fNn;

	private Texture2D fN_0019;

	private float fNL;

	private EffectParameter fNv;

	private EffectParameter fN_0012;

	private EffectParameter fN_0015;

	private EffectParameter fN8;

	private EffectParameter fNi;

	private EffectParameter fN_0005;

	public nNB Mode
	{
		get
		{
			return fNa;
		}
		set
		{
			if (fNa != value)
			{
				fNa = value;
				SetTechnique();
			}
		}
	}

	public virtual Texture2D SourceTexture
	{
		get
		{
			return fNf;
		}
		set
		{
			EffectHelper.fO(value, ref fNf, ref fN8);
			if (value != null)
			{
				EffectHelper.fr(new Vector2(1f / (float)value.Width, 1f / (float)value.Height), ref fNg, ref fN_0015);
			}
		}
	}

	public Texture2D IntensityTexture
	{
		get
		{
			return fN_0019;
		}
		set
		{
			EffectHelper.fO(value, ref fN_0019, ref fN_0012);
		}
	}

	public float IntensityBlend
	{
		get
		{
			return fNL;
		}
		set
		{
			if (fNv != null && value != fNL)
			{
				fNL = value;
				fNv.SetValue(MathHelper.Clamp(fNL, 0f, 1f));
			}
		}
	}

	public float BloomThreshold
	{
		get
		{
			return fN_0003.Z;
		}
		set
		{
			SetBloomData(fN_0003.X, fN_0003.Y, value, fN_0003.W);
		}
	}

	public float BloomAmount
	{
		get
		{
			return fN_0003.X;
		}
		set
		{
			SetBloomData(value, fN_0003.Y, fN_0003.Z, fN_0003.W);
		}
	}

	public float ExposureAmount
	{
		get
		{
			return fNn.X;
		}
		set
		{
			SetExposureTransitionData(value, fNn.Y, fNn.Z);
		}
	}

	public float TransitionMaxScale
	{
		get
		{
			return fNn.Y;
		}
		set
		{
			SetExposureTransitionData(fNn.X, value, fNn.Z);
		}
	}

	public float TransitionMinScale
	{
		get
		{
			return fNn.Z;
		}
		set
		{
			SetExposureTransitionData(fNn.X, fNn.Y, value);
		}
	}

	public bool DynamicRangeEnabled
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
			SetTechnique();
		}
	}

	protected override void SetTechnique()
	{
		if (fNa == nNB.ConvertData)
		{
			if (fNB)
			{
				CurrentTechnique = Techniques["HDRDownSampleAndConvertTechnique"];
			}
			else
			{
				CurrentTechnique = Techniques["HDRDownSampleAndConvertBloomOnlyTechnique"];
			}
		}
		else if (fNa == nNB.BlendIntensity)
		{
			CurrentTechnique = Techniques["HDRDownSampleAndBlendIntensityTechnique"];
		}
		else
		{
			CurrentTechnique = Techniques["HDRDownSampleTechnique"];
		}
	}

	public _0003(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "HighDynamicRange")
	{
		CurrentTechnique = Techniques["HDRDownSampleTechnique"];
		fN_0015 = Parameters["_SourceTextureStride"];
		fN8 = Parameters["_SourceTexture"];
		fN_0012 = Parameters["_IntensityTexture"];
		fNv = Parameters["_IntensityBlend"];
		fNi = Parameters["_BloomAmount_None_Threshold_Burn"];
		fN_0005 = Parameters["_ExposureAmount_TransitionMax_TransitionMin"];
	}

	protected void SetBloomData(float sharp, float soft, float threshold, float colorburn)
	{
		EffectHelper.f_0013(new Vector4(sharp, soft, threshold, colorburn), ref fN_0003, ref fNi);
	}

	protected void SetExposureTransitionData(float exposure, float maxscale, float minscale)
	{
		EffectHelper.f_0013(new Vector4(exposure, maxscale, minscale, 0f), ref fNn, ref fN_0005);
	}

	protected override Effect Create()
	{
		return new _0003(GraphicsDevice);
	}
}
internal class _0019 : _0003
{
	public new interface nNB
	{
		void GenerateSampleData(int texturewidth, int textureheight, int samplecount, ref Vector2[] generateduvs, ref float[] generatedweights);
	}

	public class nNa : nNB
	{
		private const float fNB = 8.5f;

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

	public class nNg : nNa
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

	private const int fNB = 32;

	public static nNB HorizontalBlurKernel = new nNa();

	public static nNB VerticalKernelKernel = new nNg();

	private nNB fNa;

	private DetailPreference fNg;

	private EffectParameter fNf;

	private EffectParameter fN_0003;

	private static Vector2[] fNn = new Vector2[32];

	private static float[] fN_0019 = new float[32];

	private static Vector4[] fNL = new Vector4[16];

	private static Vector2[] fNv = new Vector2[16];

	public nNB BlurKernel
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
			f0();
		}
	}

	public DetailPreference BloomDetail
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
			f0();
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
			f0();
		}
	}

	private void f0()
	{
		if (fNa != null && SourceTexture != null)
		{
			int samplecount;
			if (fNg != DetailPreference.High)
			{
				samplecount = ((fNg != DetailPreference.Medium) ? 8 : 16);
			}
			else
			{
				samplecount = 32;
			}
			fNa.GenerateSampleData(SourceTexture.Width, SourceTexture.Height, samplecount, ref fNn, ref fN_0019);
			int num = 0;
			for (int i = 0; i < fNL.Length; i++)
			{
				Vector2 value = fNn[num];
				float x = fN_0019[num++];
				Vector2 vector = fNn[num];
				float y = fN_0019[num++];
				ref Vector4 reference = ref fNL[i];
				reference = new Vector4(value, vector.X, vector.Y);
				ref Vector2 reference2 = ref fNv[i];
				reference2 = new Vector2(x, y);
			}
			if (fNf != null)
			{
				fNf.SetValue(fNL);
			}
			if (fN_0003 != null)
			{
				fN_0003.SetValue(fNv);
			}
		}
	}

	protected override void SetTechnique()
	{
		fNm.fNB.AccumulationValue++;
		if (fNg == DetailPreference.High)
		{
			CurrentTechnique = Techniques["HDRBloomBlurHighTechnique"];
		}
		else if (fNg == DetailPreference.Medium)
		{
			CurrentTechnique = Techniques["HDRBloomBlurMediumTechnique"];
		}
		else
		{
			CurrentTechnique = Techniques["HDRBloomBlurLowTechnique"];
		}
	}

	public _0019(GraphicsDevice graphicsdevice)
		: base(graphicsdevice)
	{
		CurrentTechnique = Techniques["HDRBloomBlurHighTechnique"];
		fNf = Parameters["_GaussianCoords"];
		fN_0003 = Parameters["_GaussianWeights"];
		BloomDetail = DetailPreference.High;
	}
}
internal class _0012 : BaseRenderableEffect
{
	private Vector3 fNB;

	private Vector3 fNa;

	private Vector3 fNg;

	private Vector2 fNf;

	private Texture2D fN_0003;

	private Texture2D fNn;

	private EffectParameter fN_0019;

	private EffectParameter fNL;

	private EffectParameter fNv;

	private EffectParameter fN_0012;

	private EffectParameter fN_0015;

	private EffectParameter fN8;

	public Vector3 Color
	{
		get
		{
			return fNg;
		}
		set
		{
			EffectHelper.fq(value, ref fNg, ref fNv);
		}
	}

	public Texture2D BeamTexture
	{
		get
		{
			return fN_0003;
		}
		set
		{
			EffectHelper.fO(value, ref fN_0003, ref fN_0015);
		}
	}

	public Texture2D SceneDepthMap
	{
		get
		{
			return fNn;
		}
		set
		{
			EffectHelper.fO(value, ref fNn, ref fN8);
			if (fN_0012 != null && fNn != null)
			{
				EffectHelper.fr(new Vector2(fNn.Width, fNn.Height), ref fNf, ref fN_0012);
			}
			SetTechnique();
		}
	}

	public void SetAngleAndRadius(float angle, float radius)
	{
		if (fN_0019 != null)
		{
			angle = MathHelper.ToRadians(MathHelper.Clamp(angle, 0.1f, 90f));
			float num = (float)Math.Sin(angle) * radius;
			Vector3 vector = new Vector3(num, num, radius);
			EffectHelper.fq(vector, ref fNB, ref fN_0019);
		}
	}

	public void SetDepthRange(Matrix projectiontoview)
	{
		if (fNL != null)
		{
			Vector3 vector = default;
			Vector4 vector2 = Vector4.Transform(new Vector4(0f, 0f, 0f, 1f), projectiontoview);
			Vector4 vector3 = Vector4.Transform(new Vector4(0f, 0f, 1f, 1f), projectiontoview);
			if (vector2.W != 0f)
			{
				vector.X = Math.Abs(vector2.Z / vector2.W);
			}
			if (vector3.W != 0f)
			{
				vector.Y = Math.Abs(vector3.Z / vector3.W);
			}
			float num = vector.Y - vector.X;
			if (num != 0f)
			{
				vector.Z = 10000f / num;
			}
			EffectHelper.fq(vector, ref fNa, ref fNL);
		}
	}

	public _0012(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "VolumeLightEffect")
	{
		fN_0019 = Parameters["_AngleAndRadiusScale"];
		fNL = Parameters["_NearClipFarClipScale"];
		fNv = Parameters["_Color"];
		fN_0012 = Parameters["_TargetWidthHeight"];
		fN_0015 = Parameters["_BeamTexture"];
		fN8 = Parameters["_SceneDepthTexture"];
		Color = Vector3.One;
		BeamTexture = LightingSystemManager.Instance.aP("VolumeLightBeam");
	}

	protected override void SetTechnique()
	{
		if (fNn != null)
		{
			CurrentTechnique = Techniques["Technique_Deferred"];
		}
		else
		{
			CurrentTechnique = Techniques["Technique_Forward"];
		}
	}

	protected override Effect Create()
	{
		return new _0012(GraphicsDevice);
	}
}
