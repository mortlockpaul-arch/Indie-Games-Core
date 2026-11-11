using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using b;

namespace _id_5;

internal class a : BaseSkinnedEffect, ITransparentEffect, IAddressableEffect, b.N, IShadowGenerateEffect, ITerrainEffect
{
	private b.B l8a;

	private TransparencyMode l8_u0013_;

	private float l8N = -1f;

	private Texture2D l8l;

	private TextureCube l8L;

	private TextureCube l8w;

	private Texture2D l8J;

	private float l8B;

	private float l8s;

	private float l8h;

	private Vector4 l8_u0002_;

	private Vector4 l8v;

	private TextureAddressMode l8Y;

	private TextureAddressMode l8_u0005_;

	private TextureAddressMode l8b;

	private Matrix l8I;

	private int l85;

	private int l8W;

	private float l8Z;

	private float l8T;

	private Texture2D l8_u0014_;

	private EffectParameter l8E;

	private EffectParameter l8f;

	private new EffectParameter l8_u0016_;

	private EffectParameter l86;

	private EffectParameter l8x;

	private EffectParameter l8_u0004_;

	private EffectParameter l8X;

	private EffectParameter l8_u0019_;

	private EffectParameter l8y;

	private EffectParameter l83;

	private EffectParameter l8j;

	private EffectParameter l8p;

	private EffectParameter l8z;

	private EffectParameter l8o;

	private EffectParameter l8_u0017_;

	private EffectParameter l82;

	private EffectParameter l87;

	private EffectParameter l8Q;

	private EffectParameter l8_u0012_;

	private EffectParameter l8O;

	private static Matrix[] l8_u000F_ = new Matrix[3];

	private static Vector4[] l8F = new Vector4[3];

	[CompilerGenerated]
	private bool l8q;

	public TextureCube ShadowFaceMap
	{
		get
		{
			return l8L;
		}
		set
		{
			if (l8x != null && value != l8L)
			{
				l8x.SetValue(value);
				l8L = value;
			}
		}
	}

	public TextureCube ShadowCoordMap
	{
		get
		{
			return l8w;
		}
		set
		{
			if (l8_u0004_ != null && value != l8w)
			{
				l8_u0004_.SetValue(value);
				l8w = value;
			}
		}
	}

	public Texture2D ShadowMap => l8l;

	public float ShadowPrimaryBias
	{
		get
		{
			return l8h;
		}
		set
		{
			if (l8y != null && value != l8h)
			{
				l8y.SetValue(value);
				l8h = value;
			}
		}
	}

	public float ShadowSecondaryBias
	{
		get
		{
			return l8s;
		}
		set
		{
			if (l8_u0019_ != null && value != l8s)
			{
				l8_u0019_.SetValue(value);
				l8s = value;
			}
		}
	}

	public Vector4 ShadowViewDistance
	{
		get
		{
			return l8v;
		}
		set
		{
			value.W = Math.Min(value.Z * 0.99f, value.W);
			EffectHelper.lL(value, ref l8v, ref l8j);
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
			if (l8p != null)
			{
				int num = Math.Min(value.Length, l8F.Length);
				for (int i = 0; i < num; i++)
				{
					ref Vector4 reference = ref l8F[i];
					reference = value[i];
				}
				l8p.SetValue(l8F);
			}
			if (l8z != null && l8o != null)
			{
				l8z.SetValue(value2);
				l8o.SetValue(value3);
			}
		}
	}

	public BoundingSphere ShadowArea
	{
		set
		{
			Vector4 param_ = new Vector4(value.Center, value.Radius);
			EffectHelper.lL(param_, ref l8_u0002_, ref l8f);
		}
	}

	public Matrix[] ShadowViewProjection
	{
		set
		{
			if (l83 != null)
			{
				int num = Math.Min(value.Length, l8_u000F_.Length);
				for (int i = 0; i < num; i++)
				{
					ref Matrix reference = ref l8_u000F_[i];
					reference = value[i];
				}
				l83.SetValue(l8_u000F_);
			}
		}
	}

	public b.B ShadowSourceType => l8a;

	public bool SupportsShadowGeneration => true;

	public TransparencyMode TransparencyMode => l8_u0013_;

	public float TransparencyThreshold => l8N;

	public Texture TransparencyMap => l8J;

	public TextureAddressMode AddressModeU
	{
		get
		{
			return l8Y;
		}
		set
		{
			l8Y = value;
		}
	}

	public TextureAddressMode AddressModeV
	{
		get
		{
			return l8_u0005_;
		}
		set
		{
			l8_u0005_ = value;
		}
	}

	public TextureAddressMode AddressModeW
	{
		get
		{
			return l8b;
		}
		set
		{
			l8b = value;
		}
	}

	public Texture2D HeightMapTexture
	{
		get
		{
			return l8_u0014_;
		}
		set
		{
			if (value != l8_u0014_)
			{
				EffectHelper.lh(value, ref l8_u0014_, ref l8O);
				SetTechnique();
			}
		}
	}

	public float HeightScale
	{
		get
		{
			return l8Z;
		}
		set
		{
			EffectHelper.lB(value, ref l8Z, ref l8Q);
		}
	}

	public float Tiling
	{
		get
		{
			return l8T;
		}
		set
		{
			EffectHelper.lB(value, ref l8T, ref l8_u0012_);
		}
	}

	public int TileRepeatCount
	{
		get
		{
			return l8W;
		}
		set
		{
			EffectHelper.lJ(value, ref l8W, ref l87);
		}
	}

	public int MeshSegments
	{
		get
		{
			return l85;
		}
		set
		{
			EffectHelper.lJ(value, ref l85, ref l82);
		}
	}

	public override bool DoubleSided
	{
		[CompilerGenerated]
		get
		{
			return l8q;
		}
		[CompilerGenerated]
		set
		{
			l8q = value;
		}
	}

	public void SetCameraView(Matrix view, Matrix viewtoworld)
	{
		EffectHelper.l_u0013_(viewtoworld, ref l8I, ref l8_u0017_);
	}

	public void SetTransparencyModeAndMap(TransparencyMode mode, float threshold, Texture map)
	{
		bool flag = false;
		if (mode != l8_u0013_)
		{
			l8_u0013_ = mode;
			flag = true;
		}
		if (l8E != null && threshold != l8N)
		{
			l8N = threshold;
			l8E.SetValue(l8N);
			flag = true;
		}
		Texture2D texture2D = map as Texture2D;
		if (l8X != null && texture2D != l8J)
		{
			l8J = texture2D;
			l8X.SetValue(l8J);
			flag = true;
		}
		if (flag)
		{
			SetTechnique();
		}
	}

	protected override void SetTechnique()
	{
		base.l8_u0016_.l8a.AccumulationValue++;
		bool flag = false;
		bool flag2 = l8_u0014_ != null;
		b.l.w8_u0013_ w8_u0013_ = b.l.w8_u0013_.Point;
		b.l.w8a w8a = b.l.w8a.ShadowGen;
		if (l8l == null)
		{
			flag = l8J != null && l8_u0013_ != TransparencyMode.None;
			w8a = b.l.w8a.ShadowGen;
			w8_u0013_ = ((l8a != b.B.Directional) ? b.l.w8_u0013_.Point : b.l.w8_u0013_.Directional);
		}
		else if (EffectDetail == DetailPreference.High)
		{
			w8a = b.l.w8a.Shadow;
			w8_u0013_ = ((l8a != b.B.Directional) ? b.l.w8_u0013_.Point4 : b.l.w8_u0013_.Directional4);
		}
		else if (EffectDetail == DetailPreference.Medium)
		{
			w8a = b.l.w8a.Shadow;
			w8_u0013_ = ((l8a != b.B.Directional) ? b.l.w8_u0013_.Point3 : b.l.w8_u0013_.Directional3);
		}
		else
		{
			w8a = b.l.w8a.Shadow;
			w8_u0013_ = ((l8a != b.B.Directional) ? b.l.w8_u0013_.Point : b.l.w8_u0013_.Directional);
		}
		CurrentTechnique = Techniques[b.l.lb(w8a, w8_u0013_, 0, param_3: false, flag && !flag2, Skinned && !flag2, flag2)];
	}

	public void SetShadowMapAndType(Texture2D shadowmap, b.B type)
	{
		bool flag = false;
		if (type != l8a)
		{
			l8a = type;
			flag = true;
		}
		if (l8_u0016_ != null && shadowmap != null && (float)shadowmap.Width != l8B)
		{
			l8B = shadowmap.Width;
			l8_u0016_.SetValue(l8B);
			flag = true;
		}
		if (l86 != null && shadowmap != l8l)
		{
			l8l = shadowmap;
			l86.SetValue(l8l);
			flag = true;
		}
		if (flag)
		{
			SetTechnique();
		}
	}

	public a(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "ShadowEffect")
	{
		l8E = Parameters["_TransparencyClipReference"];
		l8f = Parameters["_Direction_Or_Position_And_Radius"];
		l8_u0016_ = Parameters["_ShadowBufferPageSize"];
		l86 = Parameters["_ShadowMap"];
		l8x = Parameters["_FaceMap"];
		l8_u0004_ = Parameters["_CoordMap"];
		l8X = Parameters["_TransparencyMap"];
		l8_u0019_ = Parameters["_DepthBias"];
		l8y = Parameters["_OffsetBias"];
		l83 = Parameters["_ShadowViewProjection"];
		l8j = Parameters["_ShadowViewDistance"];
		l8p = Parameters["_RenderTargetLocation_And_Span"];
		l8z = Parameters["_RenderTargetLocation_Offset"];
		l8o = Parameters["_RenderTargetLocation_Difference"];
		l8_u0017_ = Parameters["_CameraViewToWorld"];
		l8O = Parameters["HeightMapTexture"];
		l82 = Parameters["MeshSegments"];
		l87 = Parameters["MeshRepeatCount"];
		l8Q = Parameters["HeightScale"];
		l8_u0012_ = Parameters["Tiling"];
		ShadowFaceMap = SunBurnCoreSystem.Instance._u0013_G();
		ShadowCoordMap = SunBurnCoreSystem.Instance._u0013_u();
		ShadowPrimaryBias = 1f;
		ShadowSecondaryBias = 0.2f;
		TileRepeatCount = 1;
		SetTechnique();
	}

	protected override Effect Create()
	{
		return new a(GraphicsDevice);
	}
}
