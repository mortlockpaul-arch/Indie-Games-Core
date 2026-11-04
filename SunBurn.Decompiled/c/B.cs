using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using j;

namespace c;

internal class B : BaseSkinnedEffect, ITransparentEffect, IAddressableEffect, j.g, IShadowGenerateEffect, ITerrainEffect
{
	private j.L fNB;

	private TransparencyMode fNa;

	private float fNg = -1f;

	private Texture2D fNf;

	private TextureCube fN_0003;

	private TextureCube fNn;

	private Texture2D fN_0019;

	private float fNL;

	private float fNv;

	private float fN_0012;

	private Vector4 fN_0015;

	private Vector4 fN8;

	private TextureAddressMode fNi;

	private TextureAddressMode fN_0005;

	private TextureAddressMode fNj;

	private Matrix fNo;

	private int fNc;

	private float fNd;

	private float fNw;

	private Texture2D fNs;

	private EffectParameter fN_0011;

	private EffectParameter fNQ;

	private EffectParameter fNG;

	private EffectParameter fN_0017;

	private new EffectParameter fNm;

	private EffectParameter fNh;

	private EffectParameter fNR;

	private EffectParameter fNW;

	private EffectParameter fN9;

	private EffectParameter fN_0013;

	private EffectParameter fNq;

	private EffectParameter fN_000F;

	private EffectParameter fN_0014;

	private EffectParameter fNr;

	private EffectParameter fNO;

	private EffectParameter fNu;

	private EffectParameter fN1;

	private EffectParameter fN_0016;

	private EffectParameter fNx;

	private static Matrix[] fNF = new Matrix[3];

	private static Vector4[] fNV = new Vector4[3];

	public TextureCube ShadowFaceMap
	{
		get
		{
			return fN_0003;
		}
		set
		{
			if (fNm != null && value != fN_0003)
			{
				fNm.SetValue(value);
				fN_0003 = value;
			}
		}
	}

	public TextureCube ShadowCoordMap
	{
		get
		{
			return fNn;
		}
		set
		{
			if (fNh != null && value != fNn)
			{
				fNh.SetValue(value);
				fNn = value;
			}
		}
	}

	public Texture2D ShadowMap => fNf;

	public float ShadowPrimaryBias
	{
		get
		{
			return fN_0012;
		}
		set
		{
			if (fN9 != null && value != fN_0012)
			{
				fN9.SetValue(value);
				fN_0012 = value;
			}
		}
	}

	public float ShadowSecondaryBias
	{
		get
		{
			return fNv;
		}
		set
		{
			if (fNW != null && value != fNv)
			{
				fNW.SetValue(value);
				fNv = value;
			}
		}
	}

	public Vector4 ShadowViewDistance
	{
		get
		{
			return fN8;
		}
		set
		{
			value.W = Math.Min(value.Z * 0.99f, value.W);
			EffectHelper.f_0013(value, ref fN8, ref fNq);
		}
	}

	public Vector4[] ShadowMapLocationAndSpan
	{
		set
		{
			Matrix value2 = new Matrix
			{
				M11 = (value[0].X + value[1].X) * 0.5f,
				M12 = (value[0].Y + value[1].Y) * 0.5f,
				M13 = (value[0].Z + value[1].Z) * 0.5f,
				M14 = (value[0].W + value[1].W) * 0.5f,
				M21 = (value[2].X + value[3].X) * 0.5f,
				M22 = (value[2].Y + value[3].Y) * 0.5f,
				M23 = (value[2].Z + value[3].Z) * 0.5f,
				M24 = (value[2].W + value[3].W) * 0.5f,
				M31 = (value[4].X + value[5].X) * 0.5f,
				M32 = (value[4].Y + value[5].Y) * 0.5f,
				M33 = (value[4].Z + value[5].Z) * 0.5f,
				M34 = (value[4].W + value[5].W) * 0.5f
			};
			Matrix value3 = new Matrix
			{
				M11 = (value[0].X - value[1].X) * 0.5f,
				M12 = (value[0].Y - value[1].Y) * 0.5f,
				M13 = (value[0].Z - value[1].Z) * 0.5f,
				M14 = (value[0].W - value[1].W) * 0.5f,
				M21 = (value[2].X - value[3].X) * 0.5f,
				M22 = (value[2].Y - value[3].Y) * 0.5f,
				M23 = (value[2].Z - value[3].Z) * 0.5f,
				M24 = (value[2].W - value[3].W) * 0.5f,
				M31 = (value[4].X - value[5].X) * 0.5f,
				M32 = (value[4].Y - value[5].Y) * 0.5f,
				M33 = (value[4].Z - value[5].Z) * 0.5f,
				M34 = (value[4].W - value[5].W) * 0.5f
			};
			if (fN_000F != null)
			{
				int num = Math.Min(value.Length, fNV.Length);
				for (int i = 0; i < num; i++)
				{
					ref Vector4 reference = ref fNV[i];
					reference = value[i];
				}
				fN_000F.SetValue(fNV);
			}
			if (fN_0014 != null && fNr != null)
			{
				fN_0014.SetValue(value2);
				fNr.SetValue(value3);
			}
		}
	}

	public BoundingSphere ShadowArea
	{
		set
		{
			Vector4 vector = new Vector4(value.Center, value.Radius);
			EffectHelper.f_0013(vector, ref fN_0015, ref fNQ);
		}
	}

	public Matrix[] ShadowViewProjection
	{
		set
		{
			if (fN_0013 != null)
			{
				int num = Math.Min(value.Length, fNF.Length);
				for (int i = 0; i < num; i++)
				{
					ref Matrix reference = ref fNF[i];
					reference = value[i];
				}
				fN_0013.SetValue(fNF);
			}
		}
	}

	public j.L ShadowSourceType => fNB;

	public bool SupportsShadowGeneration => true;

	public TransparencyMode TransparencyMode => fNa;

	public float Transparency => fNg;

	public Texture TransparencyMap => fN_0019;

	public TextureAddressMode AddressModeU
	{
		get
		{
			return fNi;
		}
		set
		{
			fNi = value;
		}
	}

	public TextureAddressMode AddressModeV
	{
		get
		{
			return fN_0005;
		}
		set
		{
			fN_0005 = value;
		}
	}

	public TextureAddressMode AddressModeW
	{
		get
		{
			return fNj;
		}
		set
		{
			fNj = value;
		}
	}

	public Texture2D HeightMapTexture
	{
		get
		{
			return fNs;
		}
		set
		{
			if (value != fNs)
			{
				EffectHelper.fO(value, ref fNs, ref fNx);
				SetTechnique();
			}
		}
	}

	public float HeightScale
	{
		get
		{
			return fNd;
		}
		set
		{
			EffectHelper.f_0014(value, ref fNd, ref fN1);
		}
	}

	public float Tiling
	{
		get
		{
			return fNw;
		}
		set
		{
			EffectHelper.f_0014(value, ref fNw, ref fN_0016);
		}
	}

	public int MeshSegments
	{
		get
		{
			return fNc;
		}
		set
		{
			EffectHelper.f_000F(value, ref fNc, ref fNu);
		}
	}

	public void SetCameraView(Matrix view, Matrix viewtoworld)
	{
		EffectHelper.fR(viewtoworld, ref fNo, ref fNO);
	}

	public void SetTransparencyModeAndMap(TransparencyMode mode, float transparency, Texture map)
	{
		bool flag = false;
		if (mode != fNa)
		{
			fNa = mode;
			flag = true;
		}
		if (fN_0011 != null && transparency != fNg)
		{
			fNg = transparency;
			fN_0011.SetValue(fNg);
			flag = true;
		}
		Texture2D texture2D = map as Texture2D;
		if (fNR != null && texture2D != fN_0019)
		{
			fN_0019 = texture2D;
			fNR.SetValue(fN_0019);
			flag = true;
		}
		if (flag)
		{
			SetTechnique();
		}
	}

	protected override void SetTechnique()
	{
		base.fNm.fNB.AccumulationValue++;
		bool flag = false;
		bool flag2 = fNs != null;
		j.f.nNa nNa = j.f.nNa.Point;
		j.f.nNB nNB = j.f.nNB.ShadowGen;
		if (fNf == null)
		{
			flag = fN_0019 != null && fNa != TransparencyMode.None;
			nNB = j.f.nNB.ShadowGen;
			nNa = ((fNB != j.L.Directional) ? j.f.nNa.Point : j.f.nNa.Directional);
		}
		else if (EffectDetail == DetailPreference.High)
		{
			nNB = j.f.nNB.Shadow;
			nNa = ((fNB != j.L.Directional) ? j.f.nNa.Point4 : j.f.nNa.Directional4);
		}
		else if (EffectDetail == DetailPreference.Medium)
		{
			nNB = j.f.nNB.Shadow;
			nNa = ((fNB != j.L.Directional) ? j.f.nNa.Point3 : j.f.nNa.Directional3);
		}
		else
		{
			nNB = j.f.nNB.Shadow;
			nNa = ((fNB != j.L.Directional) ? j.f.nNa.Point : j.f.nNa.Directional);
		}
		CurrentTechnique = Techniques[j.f.fF(nNB, nNa, 0, false, flag && !flag2, Skinned && !flag2, flag2)];
	}

	public void SetShadowMapAndType(Texture2D shadowmap, j.L type)
	{
		bool flag = false;
		if (type != fNB)
		{
			fNB = type;
			flag = true;
		}
		if (fNG != null && shadowmap != null && (float)shadowmap.Width != fNL)
		{
			fNL = shadowmap.Width;
			fNG.SetValue(fNL);
			flag = true;
		}
		if (fN_0017 != null && shadowmap != fNf)
		{
			fNf = shadowmap;
			fN_0017.SetValue(fNf);
			flag = true;
		}
		if (flag)
		{
			SetTechnique();
		}
	}

	public B(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "ShadowEffect")
	{
		fN_0011 = Parameters["_TransparencyClipReference"];
		fNQ = Parameters["_Direction_Or_Position_And_Radius"];
		fNG = Parameters["_ShadowBufferPageSize"];
		fN_0017 = Parameters["_ShadowMap"];
		fNm = Parameters["_FaceMap"];
		fNh = Parameters["_CoordMap"];
		fNR = Parameters["_TransparencyMap"];
		fNW = Parameters["_DepthBias"];
		fN9 = Parameters["_OffsetBias"];
		fN_0013 = Parameters["_ShadowViewProjection"];
		fNq = Parameters["_ShadowViewDistance"];
		fN_000F = Parameters["_RenderTargetLocation_And_Span"];
		fN_0014 = Parameters["_RenderTargetLocation_Offset"];
		fNr = Parameters["_RenderTargetLocation_Difference"];
		fNO = Parameters["_CameraViewToWorld"];
		fNx = Parameters["HeightMapTexture"];
		fNu = Parameters["MeshSegments"];
		fN1 = Parameters["HeightScale"];
		fN_0016 = Parameters["Tiling"];
		ShadowFaceMap = LightingSystemManager.Instance.a5(graphicsdevice);
		ShadowCoordMap = LightingSystemManager.Instance.aE(graphicsdevice);
		ShadowPrimaryBias = 1f;
		ShadowSecondaryBias = 0.2f;
		SetTechnique();
	}

	protected override Effect Create()
	{
		return new B(GraphicsDevice);
	}
}
