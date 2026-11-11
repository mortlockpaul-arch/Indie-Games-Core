using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Lights;
using b;

namespace I;

internal class _u0013_ : BaseRenderableEffect, ILightingEffect, b.N, b._u0013_
{
	private const int l8a = 10;

	private Texture2D l8_u0013_;

	private Texture2D l8N;

	private Texture2D l8l;

	private float l8L;

	private Vector4[] l8w = new Vector4[10];

	private Vector4[] l8J = new Vector4[10];

	private Vector4[] l8B = new Vector4[10];

	private Vector4[] l8s = new Vector4[10];

	private Vector2 l8h;

	private Vector4 l8_u0002_;

	private EffectParameter l8v;

	private EffectParameter l8Y;

	private EffectParameter l8_u0005_;

	private EffectParameter l8b;

	private EffectParameter l8I;

	private EffectParameter l85;

	private EffectParameter l8W;

	private EffectParameter l8Z;

	private EffectParameter l8T;

	private EffectParameter l8_u0014_;

	private EffectParameter l8E;

	private b.B l8f;

	private new DetailPreference l8_u0016_;

	private Texture2D l86;

	private TextureCube l8x;

	private TextureCube l8_u0004_;

	private float l8X;

	private Vector4 l8_u0019_;

	private EffectParameter l8y;

	private EffectParameter l83;

	private EffectParameter l8j;

	private EffectParameter l8p;

	private EffectParameter l8z;

	private EffectParameter l8o;

	private Matrix[] l8_u0017_ = new Matrix[3];

	private Vector4 l82;

	private EffectParameter l87;

	private EffectParameter l8Q;

	private EffectParameter l8_u0012_;

	private EffectParameter l8O;

	private EffectParameter l8_u000F_;

	private EffectParameter l8F;

	private static Matrix[] l8q = new Matrix[3];

	private static Vector4[] l80 = new Vector4[3];

	[CompilerGenerated]
	private bool l8R;

	public Texture2D SceneDepthMap
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			EffectHelper.lh(value, ref l8_u0013_, ref l8_u0005_);
			if (l8Y != null && l8_u0013_ != null)
			{
				EffectHelper.ls(new Vector2(l8_u0013_.Width, l8_u0013_.Height), ref l8h, ref l8Y);
			}
		}
	}

	public Texture2D SceneNormalSpecularMap
	{
		get
		{
			return l8N;
		}
		set
		{
			EffectHelper.lh(value, ref l8N, ref l8b);
		}
	}

	public int MaxLightSources => 10;

	public List<BaseLight> LightSources
	{
		set
		{
			N8(value);
			base.l8_u0016_.l8N.AccumulationValue++;
		}
	}

	public float LightGroupDebugAmount
	{
		get
		{
			return l8L;
		}
		set
		{
			EffectHelper.lB(value, ref l8L, ref l85);
		}
	}

	public Texture2D MicrofacetTexture
	{
		get
		{
			return l8l;
		}
		set
		{
			EffectHelper.lh(value, ref l8l, ref l8I);
		}
	}

	public TextureCube ShadowFaceMap
	{
		get
		{
			return l8x;
		}
		set
		{
			if (l8p != null && value != l8x)
			{
				l8p.SetValue(value);
				l8x = value;
			}
		}
	}

	public TextureCube ShadowCoordMap
	{
		get
		{
			return l8_u0004_;
		}
		set
		{
			if (l8z != null && value != l8_u0004_)
			{
				l8z.SetValue(value);
				l8_u0004_ = value;
			}
		}
	}

	public Texture2D ShadowMap
	{
		get
		{
			return l86;
		}
		set
		{
			SetShadowMapAndType(value, l8f);
		}
	}

	public Vector4 ShadowViewDistance
	{
		get
		{
			return l8_u0019_;
		}
		set
		{
			value.W = Math.Min(value.Z * 0.99f, value.W);
			EffectHelper.lL(value, ref l8_u0019_, ref l8o);
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
			if (l8O != null)
			{
				int num = Math.Min(value.Length, l80.Length);
				for (int i = 0; i < num; i++)
				{
					ref Vector4 reference = ref l80[i];
					reference = value[i];
				}
				l8O.SetValue(l80);
			}
			if (l8_u000F_ != null && l8F != null)
			{
				l8_u000F_.SetValue(value2);
				l8F.SetValue(value3);
			}
		}
	}

	public BoundingSphere ShadowArea
	{
		set
		{
			Vector4 param_ = new Vector4(value.Center, value.Radius);
			la(param_, l8_u0017_);
		}
	}

	public Matrix[] ShadowViewProjection
	{
		set
		{
			la(l82, value);
		}
	}

	public b.B ShadowSourceType
	{
		get
		{
			return l8f;
		}
		set
		{
			SetShadowMapAndType(l86, value);
		}
	}

	public DetailPreference ShadowDetail
	{
		get
		{
			return l8_u0016_;
		}
		set
		{
			l8_u0016_ = value;
			NH();
		}
	}

	public BoundingSphere WorldClippingSphere
	{
		set
		{
			Vector4 param_ = new Vector4(value.Center, value.Radius);
			EffectHelper.lL(param_, ref l8_u0002_, ref l8v);
		}
	}

	public override bool DoubleSided
	{
		[CompilerGenerated]
		get
		{
			return l8R;
		}
		[CompilerGenerated]
		set
		{
			l8R = value;
		}
	}

	internal static int MaxLightSourcesInternal => 10;

	public void SetShadowMapAndType(Texture2D shadowmap, b.B type)
	{
		if (l83 != null && shadowmap != null && (float)shadowmap.Width != l8X)
		{
			l83.SetValue(shadowmap.Width);
			l8X = shadowmap.Width;
		}
		EffectHelper.lh(shadowmap, ref l86, ref l8j);
		l8f = type;
		NH();
	}

	private void NH()
	{
		if (l8y == null)
		{
			return;
		}
		if (l8f == b.B.Directional)
		{
			if (l86 == null)
			{
				l8y.SetValue(1);
			}
			else if (l8_u0016_ == DetailPreference.High)
			{
				l8y.SetValue(7);
			}
			else if (l8_u0016_ == DetailPreference.Medium)
			{
				l8y.SetValue(5);
			}
			else
			{
				l8y.SetValue(3);
			}
		}
		else if (l8f == b.B.Point)
		{
			if (l86 == null)
			{
				l8y.SetValue(0);
			}
			else if (l8_u0016_ == DetailPreference.High)
			{
				l8y.SetValue(6);
			}
			else if (l8_u0016_ == DetailPreference.Medium)
			{
				l8y.SetValue(4);
			}
			else
			{
				l8y.SetValue(2);
			}
		}
	}

	private void N8(List<BaseLight> param_0)
	{
		if (l8W == null || l8Z == null || l8T == null || l8_u0014_ == null || l8E == null)
		{
			return;
		}
		if (param_0.Count > 10)
		{
			throw new Exception("Too many light sources provided for effect.");
		}
		int num = 0;
		foreach (BaseLight item in param_0)
		{
			LightTypeCaster lightTypeCaster = OptimizationSystem.LightTypeCasters.Get(item);
			ref Vector4 reference = ref l8J[num];
			reference = new Vector4(item.CompositeColorAndIntensity, 0f);
			if (item.FillLight)
			{
				ref Vector4 reference2 = ref l8s[num];
				reference2 = new Vector4(0f, 0f, 0f, 0f);
			}
			else
			{
				ref Vector4 reference3 = ref l8s[num];
				reference3 = new Vector4(0f, 1f, 0f, 0f);
			}
			if (lightTypeCaster.SpotSource != null)
			{
				ISpotSource spotSource = lightTypeCaster.SpotSource;
				float value = spotSource.Angle * 0.5f;
				float num2 = (float)Math.Cos(MathHelper.ToRadians(MathHelper.Clamp(value, 0.01f, 89.99f)));
				float num3 = 1f / (1f - num2);
				l8s[num].X = item.FalloffStrength;
				l8s[num].Z = num2;
				l8s[num].W = num3;
				ref Vector4 reference4 = ref l8B[num];
				reference4 = new Vector4(Vector3.TransformNormal(spotSource.Direction, View), 0f);
				ref Vector4 reference5 = ref l8w[num];
				reference5 = new Vector4(Vector3.Transform(spotSource.Position, View), spotSource.Radius);
			}
			else if (lightTypeCaster.PointSource != null)
			{
				IPointSource pointSource = lightTypeCaster.PointSource;
				l8s[num].X = item.FalloffStrength;
				ref Vector4 reference6 = ref l8w[num];
				reference6 = new Vector4(Vector3.Transform(pointSource.Position, View), pointSource.Radius);
			}
			else if (lightTypeCaster.ShadowSource != null)
			{
				ref Vector4 reference7 = ref l8w[num];
				reference7 = new Vector4(Vector3.Transform(lightTypeCaster.ShadowSource.ShadowPosition, View), 1E+09f);
			}
			num++;
		}
		l8W.SetValue(num);
		if (num >= 1)
		{
			l8Z.SetValue(l8w);
			l8T.SetValue(l8J);
			l8_u0014_.SetValue(l8B);
			l8E.SetValue(l8s);
		}
	}

	private void la(Vector4 param_0, Matrix[] param_1)
	{
		l82 = param_0;
		int num = Math.Min(param_1.Length, l8_u0017_.Length);
		for (int i = 0; i < num; i++)
		{
			ref Matrix reference = ref l8_u0017_[i];
			reference = param_1[i];
		}
		Matrix matrix = Matrix.CreateTranslation(ViewToWorld.Translation);
		Matrix viewToWorld = ViewToWorld;
		viewToWorld.Translation = Vector3.Zero;
		if (l87 != null)
		{
			l87.SetValue(viewToWorld);
		}
		if (l8Q != null)
		{
			Vector3 value = Vector3.Transform(new Vector3(param_0.X, param_0.Y, param_0.Z), Matrix.Invert(matrix));
			l8Q.SetValue(new Vector4(value, param_0.W));
		}
		if (l8_u0012_ != null)
		{
			for (int j = 0; j < num; j++)
			{
				ref Matrix reference2 = ref l8q[j];
				reference2 = matrix * param_1[j];
			}
			l8_u0012_.SetValue(l8q);
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

	public _u0013_(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "DeferredLightingEffect")
	{
		l8_u0005_ = Parameters["_SceneDepthMap"];
		l8b = Parameters["_SceneNormalSpecularMap"];
		l8I = Parameters["_MicrofacetMap"];
		l85 = Parameters["_DebugAmount"];
		l8W = Parameters["_LightCount"];
		l8Z = Parameters["_LightPos_Radius"];
		l8T = Parameters["_LightColor"];
		l8_u0014_ = Parameters["_LightSpotDir"];
		l8E = Parameters["_LightModel_Fill_SpotAng_InvSpotAng"];
		l8Y = Parameters["_TargetWidthHeight"];
		l8v = Parameters["_WorldSphere"];
		l8y = Parameters["_ShadowType"];
		l8Q = Parameters["_Shadow_Direction_Or_Position_And_Radius"];
		l83 = Parameters["_ShadowBufferPageSize"];
		l8j = Parameters["_ShadowMap"];
		l8p = Parameters["_FaceMap"];
		l8z = Parameters["_CoordMap"];
		l87 = Parameters["_ViewToCameraWorld"];
		l8_u0012_ = Parameters["_CameraWorldToShadowViewProjection"];
		l8o = Parameters["_ShadowViewDistance"];
		l8O = Parameters["_RenderTargetLocation_And_Span"];
		l8_u000F_ = Parameters["_RenderTargetLocation_Offset"];
		l8F = Parameters["_RenderTargetLocation_Difference"];
		LightGroupDebugAmount = 0f;
		MicrofacetTexture = SunBurnCoreSystem.Instance._u0013_4();
		ShadowFaceMap = SunBurnCoreSystem.Instance._u0013_G();
		ShadowCoordMap = SunBurnCoreSystem.Instance._u0013_u();
		SetTechnique();
	}

	protected override Effect Create()
	{
		return new _u0013_(GraphicsDevice);
	}
}
