using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Lights;
using j;

namespace o;

internal class a : BaseRenderableEffect, ILightingEffect, j.g, j.a
{
	private const int fNB = 10;

	private Texture2D fNa;

	private Texture2D fNg;

	private Texture2D fNf;

	private float fN_0003;

	private Vector4[] fNn = new Vector4[10];

	private Vector4[] fN_0019 = new Vector4[10];

	private Vector4[] fNL = new Vector4[10];

	private Vector4[] fNv = new Vector4[10];

	private Vector2 fN_0012;

	private Vector4 fN_0015;

	private EffectParameter fN8;

	private EffectParameter fNi;

	private EffectParameter fN_0005;

	private EffectParameter fNj;

	private EffectParameter fNo;

	private EffectParameter fNc;

	private EffectParameter fNd;

	private EffectParameter fNw;

	private EffectParameter fNs;

	private EffectParameter fN_0011;

	private EffectParameter fNQ;

	private j.L fNG;

	private DetailPreference fN_0017;

	private new Texture2D fNm;

	private TextureCube fNh;

	private TextureCube fNR;

	private float fNW;

	private Vector4 fN9;

	private EffectParameter fN_0013;

	private EffectParameter fNq;

	private EffectParameter fN_000F;

	private EffectParameter fN_0014;

	private EffectParameter fNr;

	private EffectParameter fNO;

	private Matrix[] fNu = new Matrix[3];

	private Vector4 fN1;

	private EffectParameter fN_0016;

	private EffectParameter fNx;

	private EffectParameter fNF;

	private EffectParameter fNV;

	private EffectParameter fNA;

	private EffectParameter fN0;

	private static Matrix[] fNK = new Matrix[3];

	private static Vector4[] fNM = new Vector4[3];

	public Texture2D SceneDepthMap
	{
		get
		{
			return fNa;
		}
		set
		{
			EffectHelper.fO(value, ref fNa, ref fN_0005);
			if (fNi != null && fNa != null)
			{
				EffectHelper.fr(new Vector2(fNa.Width, fNa.Height), ref fN_0012, ref fNi);
			}
		}
	}

	public Texture2D SceneNormalSpecularMap
	{
		get
		{
			return fNg;
		}
		set
		{
			EffectHelper.fO(value, ref fNg, ref fNj);
		}
	}

	public int MaxLightSources => 10;

	public List<BaseLight> LightSources
	{
		set
		{
			fm(value);
			base.fNm.fNg.AccumulationValue++;
		}
	}

	public float LightGroupDebugAmount
	{
		get
		{
			return fN_0003;
		}
		set
		{
			EffectHelper.f_0014(value, ref fN_0003, ref fNc);
		}
	}

	public Texture2D MicrofacetTexture
	{
		get
		{
			return fNf;
		}
		set
		{
			EffectHelper.fO(value, ref fNf, ref fNo);
		}
	}

	public TextureCube ShadowFaceMap
	{
		get
		{
			return fNh;
		}
		set
		{
			if (fN_0014 != null && value != fNh)
			{
				fN_0014.SetValue(value);
				fNh = value;
			}
		}
	}

	public TextureCube ShadowCoordMap
	{
		get
		{
			return fNR;
		}
		set
		{
			if (fNr != null && value != fNR)
			{
				fNr.SetValue(value);
				fNR = value;
			}
		}
	}

	public Texture2D ShadowMap
	{
		get
		{
			return fNm;
		}
		set
		{
			SetShadowMapAndType(value, fNG);
		}
	}

	public Vector4 ShadowViewDistance
	{
		get
		{
			return fN9;
		}
		set
		{
			value.W = Math.Min(value.Z * 0.99f, value.W);
			EffectHelper.f_0013(value, ref fN9, ref fNO);
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
			if (fNV != null)
			{
				int num = Math.Min(value.Length, fNM.Length);
				for (int k = 0; k < num; k++)
				{
					ref Vector4 reference = ref fNM[k];
					reference = value[k];
				}
				fNV.SetValue(fNM);
			}
			if (fNA != null && fN0 != null)
			{
				fNA.SetValue(value2);
				fN0.SetValue(value3);
			}
		}
	}

	public BoundingSphere ShadowArea
	{
		set
		{
			Vector4 vector = new Vector4(value.Center, value.Radius);
			fh(vector, fNu);
		}
	}

	public Matrix[] ShadowViewProjection
	{
		set
		{
			fh(fN1, value);
		}
	}

	public j.L ShadowSourceType
	{
		get
		{
			return fNG;
		}
		set
		{
			SetShadowMapAndType(fNm, value);
		}
	}

	public DetailPreference ShadowDetail
	{
		get
		{
			return fN_0017;
		}
		set
		{
			fN_0017 = value;
			f_0017();
		}
	}

	public BoundingSphere WorldClippingSphere
	{
		set
		{
			Vector4 vector = new Vector4(value.Center, value.Radius);
			EffectHelper.f_0013(vector, ref fN_0015, ref fN8);
		}
	}

	internal static int MaxLightSourcesInternal => 10;

	public void SetShadowMapAndType(Texture2D shadowmap, j.L type)
	{
		if (fNq != null && shadowmap != null && (float)shadowmap.Width != fNW)
		{
			fNq.SetValue(shadowmap.Width);
			fNW = shadowmap.Width;
		}
		EffectHelper.fO(shadowmap, ref fNm, ref fN_000F);
		fNG = type;
		f_0017();
	}

	private void f_0017()
	{
		if (fN_0013 == null)
		{
			return;
		}
		if (fNG == j.L.Directional)
		{
			if (fNm == null)
			{
				fN_0013.SetValue(1);
			}
			else if (fN_0017 == DetailPreference.High)
			{
				fN_0013.SetValue(7);
			}
			else if (fN_0017 == DetailPreference.Medium)
			{
				fN_0013.SetValue(5);
			}
			else
			{
				fN_0013.SetValue(3);
			}
		}
		else if (fNG == j.L.Point)
		{
			if (fNm == null)
			{
				fN_0013.SetValue(0);
			}
			else if (fN_0017 == DetailPreference.High)
			{
				fN_0013.SetValue(6);
			}
			else if (fN_0017 == DetailPreference.Medium)
			{
				fN_0013.SetValue(4);
			}
			else
			{
				fN_0013.SetValue(2);
			}
		}
	}

	private void fm(List<BaseLight> P_0)
	{
		if (fNd == null || fNw == null || fNs == null || fN_0011 == null || fNQ == null)
		{
			return;
		}
		if (P_0.Count > 10)
		{
			throw new Exception("Too many light sources provided for effect.");
		}
		int num = 0;
		foreach (BaseLight item in P_0)
		{
			L.o o2 = OptimizationSystem.fNB.g_0011(item);
			ref Vector4 reference = ref fN_0019[num];
			reference = new Vector4(item.CompositeColorAndIntensity, 0f);
			if (item.FillLight)
			{
				ref Vector4 reference2 = ref fNv[num];
				reference2 = new Vector4(0f, 0f, 0f, 0f);
			}
			else
			{
				ref Vector4 reference3 = ref fNv[num];
				reference3 = new Vector4(0f, 1f, 0f, 0f);
			}
			if (o2.fNa != null)
			{
				ISpotSource spotSource = o2.fNa;
				float value = spotSource.Angle * 0.5f;
				float num2 = (float)Math.Cos(MathHelper.ToRadians(MathHelper.Clamp(value, 0.01f, 89.99f)));
				float num3 = 1f / (1f - num2);
				fNv[num].X = item.FalloffStrength;
				fNv[num].Z = num2;
				fNv[num].W = num3;
				ref Vector4 reference4 = ref fNL[num];
				reference4 = new Vector4(Vector3.TransformNormal(spotSource.Direction, View), 0f);
				ref Vector4 reference5 = ref fNn[num];
				reference5 = new Vector4(Vector3.Transform(spotSource.Position, View), spotSource.Radius);
			}
			else if (o2.fNB != null)
			{
				IPointSource pointSource = o2.fNB;
				fNv[num].X = item.FalloffStrength;
				ref Vector4 reference6 = ref fNn[num];
				reference6 = new Vector4(Vector3.Transform(pointSource.Position, View), pointSource.Radius);
			}
			else if (o2.fN_0003 != null)
			{
				ref Vector4 reference7 = ref fNn[num];
				reference7 = new Vector4(Vector3.Transform(o2.fN_0003.ShadowPosition, View), 1E+09f);
			}
			num++;
		}
		fNd.SetValue(num);
		if (num >= 1)
		{
			fNw.SetValue(fNn);
			fNs.SetValue(fN_0019);
			fN_0011.SetValue(fNL);
			fNQ.SetValue(fNv);
		}
	}

	private void fh(Vector4 P_0, Matrix[] P_1)
	{
		fN1 = P_0;
		int num = Math.Min(P_1.Length, fNu.Length);
		for (int k = 0; k < num; k++)
		{
			ref Matrix reference = ref fNu[k];
			reference = P_1[k];
		}
		Matrix matrix = Matrix.CreateTranslation(ViewToWorld.Translation);
		Matrix viewToWorld = ViewToWorld;
		viewToWorld.Translation = Vector3.Zero;
		if (fN_0016 != null)
		{
			fN_0016.SetValue(viewToWorld);
		}
		if (fNx != null)
		{
			Vector3 value = Vector3.Transform(new Vector3(P_0.X, P_0.Y, P_0.Z), Matrix.Invert(matrix));
			fNx.SetValue(new Vector4(value, P_0.W));
		}
		if (fNF != null)
		{
			for (int l = 0; l < num; l++)
			{
				ref Matrix reference2 = ref fNK[l];
				reference2 = matrix * P_1[l];
			}
			fNF.SetValue(fNK);
		}
	}

	protected override void SetTechnique()
	{
		if (EffectDetail == DetailPreference.High)
		{
			CurrentTechnique = Techniques["Lighting_High_Technique"];
		}
		else if (EffectDetail == DetailPreference.Medium)
		{
			CurrentTechnique = Techniques["Lighting_Medium_Technique"];
		}
		else if (EffectDetail == DetailPreference.Low)
		{
			CurrentTechnique = Techniques["Lighting_Low_Technique"];
		}
		else
		{
			CurrentTechnique = Techniques["Lighting_Off_Technique"];
		}
	}

	public a(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "DeferredLightingEffect")
	{
		fN_0005 = Parameters["_SceneDepthMap"];
		fNj = Parameters["_SceneNormalSpecularMap"];
		fNo = Parameters["_MicrofacetMap"];
		fNc = Parameters["_DebugAmount"];
		fNd = Parameters["_LightCount"];
		fNw = Parameters["_LightPos_Radius"];
		fNs = Parameters["_LightColor"];
		fN_0011 = Parameters["_LightSpotDir"];
		fNQ = Parameters["_LightModel_Fill_SpotAng_InvSpotAng"];
		fNi = Parameters["_TargetWidthHeight"];
		fN8 = Parameters["_WorldSphere"];
		fN_0013 = Parameters["_ShadowType"];
		fNx = Parameters["_Shadow_Direction_Or_Position_And_Radius"];
		fNq = Parameters["_ShadowBufferPageSize"];
		fN_000F = Parameters["_ShadowMap"];
		fN_0014 = Parameters["_FaceMap"];
		fNr = Parameters["_CoordMap"];
		fN_0016 = Parameters["_ViewToCameraWorld"];
		fNF = Parameters["_CameraWorldToShadowViewProjection"];
		fNO = Parameters["_ShadowViewDistance"];
		fNV = Parameters["_RenderTargetLocation_And_Span"];
		fNA = Parameters["_RenderTargetLocation_Offset"];
		fN0 = Parameters["_RenderTargetLocation_Difference"];
		LightGroupDebugAmount = 0f;
		MicrofacetTexture = LightingSystemManager.Instance.a4(graphicsdevice);
		ShadowFaceMap = LightingSystemManager.Instance.a5(graphicsdevice);
		ShadowCoordMap = LightingSystemManager.Instance.aE(graphicsdevice);
		SetTechnique();
	}

	protected override Effect Create()
	{
		return new a(GraphicsDevice);
	}
}
