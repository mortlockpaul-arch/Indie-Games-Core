using System;
using System.Runtime.CompilerServices;
using System.Threading;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using b;
using s;
using y;

namespace SynapseGaming.LightingSystem.Effects;

[EditorObject(true)]
public abstract class BaseTerrainEffect : BaseRenderableEffect, b.a, ITerrainEffect, B.J, IEditorObject, INamedObject, ICollisionMaterial
{
	internal new delegate void w8a();

	private w8a l8a;

	private string l8_u0013_ = "";

	private int l8N;

	private int l8l;

	private float l8L = 0.75f;

	private float l8w = 0.25f;

	private int l8J;

	private int l8B;

	private int l8s;

	private float l8h;

	private float l8_u0002_;

	private float l8v;

	private float l8Y;

	private float l8_u0005_;

	private float l8b;

	private Vector3 l8I;

	private Texture2D l85;

	private Texture2D l8W;

	private Texture2D l8Z;

	private Texture2D l8T;

	private Texture2D l8_u0014_;

	private Texture2D l8E;

	private Texture2D l8f;

	private new Texture2D l8_u0016_;

	private Texture2D l86;

	private Texture2D l8x;

	private Texture2D l8_u0004_;

	private Texture2D l8X;

	private Texture2D l8_u0019_;

	private Texture2D l8y;

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

	private EffectParameter l8_u000F_;

	private EffectParameter l8F;

	private EffectParameter l8q;

	private EffectParameter l80;

	private EffectParameter l8R;

	private EffectParameter l8i;

	private EffectParameter l8d;

	private EffectParameter l8_u0015_;

	private EffectParameter l8r;

	private EffectParameter l8K;

	private EffectParameter l8M;

	private static byte[] l8_u0001_;

	private static HalfSingle[] l8_u0018_;

	[CompilerGenerated]
	private bool l8S;

	[CompilerGenerated]
	private string l89;

	[CompilerGenerated]
	private string l8G;

	[CompilerGenerated]
	private string l8u;

	[CompilerGenerated]
	private string l8t;

	[CompilerGenerated]
	private string l84;

	[CompilerGenerated]
	private string l8A;

	[CompilerGenerated]
	private string l8g;

	[CompilerGenerated]
	private string l8_u0006_;

	[CompilerGenerated]
	private string l8e;

	[CompilerGenerated]
	private string l8U;

	[CompilerGenerated]
	private string l8_u000E_;

	[CompilerGenerated]
	private string l8C;

	[CompilerGenerated]
	private bool l81;

	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l8S;
		}
		[CompilerGenerated]
		set
		{
			l8S = value;
		}
	}

	internal string MaterialFile
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
		}
	}

	string b.a.MaterialFile => l8_u0013_;

	internal string MaterialName
	{
		[CompilerGenerated]
		get
		{
			return l89;
		}
		[CompilerGenerated]
		set
		{
			l89 = value;
		}
	}

	internal string ProjectFile
	{
		[CompilerGenerated]
		get
		{
			return l8G;
		}
		[CompilerGenerated]
		set
		{
			l8G = value;
		}
	}

	string B.J.ProjectFile => ProjectFile;

	internal string DiffuseMapLayer1File
	{
		[CompilerGenerated]
		get
		{
			return l8u;
		}
		[CompilerGenerated]
		set
		{
			l8u = value;
		}
	}

	internal string DiffuseMapLayer2File
	{
		[CompilerGenerated]
		get
		{
			return l8t;
		}
		[CompilerGenerated]
		set
		{
			l8t = value;
		}
	}

	internal string DiffuseMapLayer3File
	{
		[CompilerGenerated]
		get
		{
			return l84;
		}
		[CompilerGenerated]
		set
		{
			l84 = value;
		}
	}

	internal string DiffuseMapLayer4File
	{
		[CompilerGenerated]
		get
		{
			return l8A;
		}
		[CompilerGenerated]
		set
		{
			l8A = value;
		}
	}

	internal string NormalMapLayer1File
	{
		[CompilerGenerated]
		get
		{
			return l8g;
		}
		[CompilerGenerated]
		set
		{
			l8g = value;
		}
	}

	internal string NormalMapLayer2File
	{
		[CompilerGenerated]
		get
		{
			return l8_u0006_;
		}
		[CompilerGenerated]
		set
		{
			l8_u0006_ = value;
		}
	}

	internal string NormalMapLayer3File
	{
		[CompilerGenerated]
		get
		{
			return l8e;
		}
		[CompilerGenerated]
		set
		{
			l8e = value;
		}
	}

	internal string NormalMapLayer4File
	{
		[CompilerGenerated]
		get
		{
			return l8U;
		}
		[CompilerGenerated]
		set
		{
			l8U = value;
		}
	}

	internal string HeightMapFile
	{
		[CompilerGenerated]
		get
		{
			return l8_u000E_;
		}
		[CompilerGenerated]
		set
		{
			l8_u000E_ = value;
		}
	}

	internal string BlendMapFile
	{
		[CompilerGenerated]
		get
		{
			return l8C;
		}
		[CompilerGenerated]
		set
		{
			l8C = value;
		}
	}

	[EditorProperty(true, Description = "Diffuse 1", MajorGrouping = 2, MinorGrouping = 1, ToolTipText = "")]
	[s.a("DiffuseMapLayer1File", false)]
	public Texture2D DiffuseMapLayer1Texture
	{
		get
		{
			return l8T;
		}
		set
		{
			EffectHelper.lh(value, l8W, ref l8T, ref l8j);
		}
	}

	[s.a("DiffuseMapLayer2File", false)]
	[EditorProperty(true, Description = "Diffuse 2", MajorGrouping = 3, MinorGrouping = 1, ToolTipText = "")]
	public Texture2D DiffuseMapLayer2Texture
	{
		get
		{
			return l8_u0014_;
		}
		set
		{
			EffectHelper.lh(value, l8W, ref l8_u0014_, ref l8p);
			Nk();
		}
	}

	[s.a("DiffuseMapLayer3File", false)]
	[EditorProperty(true, Description = "Diffuse 3", MajorGrouping = 4, MinorGrouping = 1, ToolTipText = "")]
	public Texture2D DiffuseMapLayer3Texture
	{
		get
		{
			return l8E;
		}
		set
		{
			EffectHelper.lh(value, l8W, ref l8E, ref l8z);
			Nk();
		}
	}

	[s.a("DiffuseMapLayer4File", false)]
	[EditorProperty(true, Description = "Diffuse 4", MajorGrouping = 5, MinorGrouping = 1, ToolTipText = "")]
	public Texture2D DiffuseMapLayer4Texture
	{
		get
		{
			return l8f;
		}
		set
		{
			EffectHelper.lh(value, l8W, ref l8f, ref l8o);
			Nk();
		}
	}

	[EditorProperty(true, Description = "Normal 1", MajorGrouping = 2, MinorGrouping = 2, ToolTipText = "")]
	[s.a("NormalMapLayer1File", false)]
	public Texture2D NormalMapLayer1Texture
	{
		get
		{
			return l8_u0016_;
		}
		set
		{
			EffectHelper.lh(value, l8Z, ref l8_u0016_, ref l8_u0017_);
		}
	}

	[s.a("NormalMapLayer2File", false)]
	[EditorProperty(true, Description = "Normal 2", MajorGrouping = 3, MinorGrouping = 2, ToolTipText = "")]
	public Texture2D NormalMapLayer2Texture
	{
		get
		{
			return l86;
		}
		set
		{
			EffectHelper.lh(value, l8Z, ref l86, ref l82);
			Nk();
		}
	}

	[EditorProperty(true, Description = "Normal 3", MajorGrouping = 4, MinorGrouping = 2, ToolTipText = "")]
	[s.a("NormalMapLayer3File", false)]
	public Texture2D NormalMapLayer3Texture
	{
		get
		{
			return l8x;
		}
		set
		{
			EffectHelper.lh(value, l8Z, ref l8x, ref l87);
			Nk();
		}
	}

	[EditorProperty(true, Description = "Normal 4", MajorGrouping = 5, MinorGrouping = 2, ToolTipText = "")]
	[s.a("NormalMapLayer4File", false)]
	public Texture2D NormalMapLayer4Texture
	{
		get
		{
			return l8_u0004_;
		}
		set
		{
			EffectHelper.lh(value, l8Z, ref l8_u0004_, ref l8Q);
			Nk();
		}
	}

	[EditorProperty(true, Description = "Height Map", MajorGrouping = 1, MinorGrouping = 1, ToolTipText = "")]
	[s.a("HeightMapFile", true)]
	public Texture2D HeightMapTexture
	{
		get
		{
			return l8X;
		}
		set
		{
			if (value == l8X)
			{
				return;
			}
			EffectHelper.lh(value, l85, ref l8X, ref l8_u0012_);
			Nm();
			if (l8X == null)
			{
				l8O.SetValue(l8X);
				return;
			}
			if (l8X.Format != SurfaceFormat.HalfSingle)
			{
				throw new Exception("Terrain height map requires a HalfSingle format texture.");
			}
			int num = l8X.Width;
			int num2 = l8X.Height;
			int levelCount = l8X.LevelCount;
			int num3 = num * num2;
			GraphicsDevice graphicsDevice = GraphicsDevice;
			for (int i = 0; i < 16; i++)
			{
				graphicsDevice.Textures[i] = null;
			}
			for (int j = 0; j < 4; j++)
			{
				graphicsDevice.VertexTextures[j] = null;
			}
			if (l8_u0019_ == null || num != l8_u0019_.Width || num2 != l8_u0019_.Height)
			{
				y.a.w_u0005_(ref l8_u0019_);
				l8_u0019_ = new Texture2D(graphicsDevice, num, num2, levelCount > 1, SurfaceFormat.Alpha8);
			}
			if (l8_u0001_ == null || num3 > l8_u0001_.Length)
			{
				l8_u0001_ = new byte[num3];
				l8_u0018_ = new HalfSingle[num3];
			}
			for (int k = 0; k < levelCount; k++)
			{
				int num4 = num * num2;
				l8X.GetData(k, null, l8_u0018_, 0, num4);
				for (int m = 0; m < num4; m++)
				{
					float num5 = l8_u0018_[m].ToSingle();
					l8_u0001_[m] = (byte)(num5 * 255f);
				}
				l8_u0019_.SetData(k, null, l8_u0001_, 0, num4);
				num = Math.Max(1, num >> 1);
				num2 = Math.Max(1, num2 >> 1);
			}
			l8O.SetValue(l8_u0019_);
			int param_ = l8X.Width / 3;
			EffectHelper.lJ(param_, ref l8s, ref l80);
		}
	}

	[s.a("BlendMapFile", false)]
	[EditorProperty(true, Description = "Blend Map", MajorGrouping = 1, MinorGrouping = 2, ToolTipText = "")]
	public Texture2D BlendMapTexture
	{
		get
		{
			return l8y;
		}
		set
		{
			EffectHelper.lh(value, l8W, ref l8y, ref l8_u000F_);
		}
	}

	[EditorProperty(true, Description = "Normal Strength", MajorGrouping = 6, MinorGrouping = 4, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 32.0, 0.1)]
	public float NormalMapStrength
	{
		get
		{
			return l8h;
		}
		set
		{
			EffectHelper.lB(value, ref l8h, ref l8R);
		}
	}

	[EditorProperty(true, Description = "Material Scale", MajorGrouping = 6, MinorGrouping = 3, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 512.0, 0.2)]
	public float DiffuseScale
	{
		get
		{
			return l8_u0002_;
		}
		set
		{
			EffectHelper.lB(value, ref l8_u0002_, ref l8i);
		}
	}

	[EditorProperty(true, Description = "Height Scale", MajorGrouping = 6, MinorGrouping = 1, ToolTipText = "")]
	[EditorNumberPadOptions(3, 0.0, 100.0, 0.01)]
	public float HeightScale
	{
		get
		{
			return l8v;
		}
		set
		{
			float param_ = Math.Max(1E-06f, value);
			EffectHelper.lB(param_, ref l8v, ref l8d);
			Nm();
		}
	}

	[EditorProperty(true, Description = "Tiling Amount", MajorGrouping = 6, MinorGrouping = 2, ToolTipText = "")]
	[EditorNumberPadOptions(3, 0.0, 100.0, 0.01)]
	public float Tiling
	{
		get
		{
			return l8Y;
		}
		set
		{
			float param_ = Math.Max(1E-06f, value);
			EffectHelper.lB(param_, ref l8Y, ref l8_u0015_);
			Nm();
		}
	}

	[EditorProperty(true, Description = "Specular Power", MajorGrouping = 7, MinorGrouping = 1, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 256.0, 0.5)]
	public float SpecularPower
	{
		get
		{
			return l8_u0005_;
		}
		set
		{
			EffectHelper.lB(value, ref l8_u0005_, ref l8r);
		}
	}

	[EditorProperty(true, Description = "Specular Amount", MajorGrouping = 7, MinorGrouping = 2, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 32.0, 0.5)]
	public float SpecularAmount
	{
		get
		{
			return l8b;
		}
		set
		{
			EffectHelper.lB(value, ref l8b, ref l8K);
		}
	}

	[EditorProperty(true, Description = "Specular Color", MajorGrouping = 7, MinorGrouping = 11, ControlType = ControlType.ColorSelection, ToolTipText = "")]
	public Vector3 SpecularColor
	{
		get
		{
			return l8I;
		}
		set
		{
			EffectHelper.lw(value, ref l8I, ref l8M);
		}
	}

	[EditorProperty(true, Description = "Repeat Count", MajorGrouping = 6, MinorGrouping = 3, ToolTipText = "")]
	[EditorNumberPadOptions(3, 1.0, 100.0, 1.0)]
	public int TileRepeatCount
	{
		get
		{
			return l8B;
		}
		set
		{
			int param_ = Math.Max(value, 1);
			EffectHelper.lJ(param_, ref l8B, ref l8q);
			Nm();
		}
	}

	public int MeshSegments
	{
		get
		{
			return l8J;
		}
		set
		{
			EffectHelper.lJ(value, ref l8J, ref l8F);
		}
	}

	[EditorProperty(true, Description = "Double Sided", HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 1, ToolTipText = "")]
	public override bool DoubleSided
	{
		[CompilerGenerated]
		get
		{
			return l81;
		}
		[CompilerGenerated]
		set
		{
			l81 = value;
		}
	}

	[EditorNumberPadOptions(3, 0.0, 1.0, 0.05)]
	[EditorProperty(true, Description = "Elasticity", HorizontalAlignment = true, MajorGrouping = 8, MinorGrouping = 1, ToolTipText = "")]
	public float Elasticity
	{
		get
		{
			return l8L;
		}
		set
		{
			l8L = MathHelper.Clamp(value, 0f, 1f);
			l8l++;
		}
	}

	[EditorProperty(true, Description = "Friction", HorizontalAlignment = true, MajorGrouping = 8, MinorGrouping = 2, ToolTipText = "")]
	[EditorNumberPadOptions(3, 0.0, 1.0, 0.05)]
	public float Friction
	{
		get
		{
			return l8w;
		}
		set
		{
			l8w = MathHelper.Clamp(value, 0f, 1f);
			l8l++;
		}
	}

	public int CollisionId => l8l;

	[SpecialName]
	internal void N_u0011_(w8a param_0)
	{
		w8a w8a2 = l8a;
		w8a w8a3;
		do
		{
			w8a3 = w8a2;
			w8a value = (w8a)Delegate.Combine(w8a3, param_0);
			w8a2 = Interlocked.CompareExchange(ref l8a, value, w8a3);
		}
		while ((object)w8a2 != w8a3);
	}

	[SpecialName]
	internal void N_u0003_(w8a param_0)
	{
		w8a w8a2 = l8a;
		w8a w8a3;
		do
		{
			w8a3 = w8a2;
			w8a value = (w8a)Delegate.Remove(w8a3, param_0);
			w8a2 = Interlocked.CompareExchange(ref l8a, value, w8a3);
		}
		while ((object)w8a2 != w8a3);
	}

	internal void Nm()
	{
		l8l++;
		if (l8a != null)
		{
			l8a();
		}
	}

	public float GetTileWidth()
	{
		return 1f / l8Y;
	}

	private bool Nn(Texture2D param_0, Texture2D param_1)
	{
		if (param_0 != param_1)
		{
			return param_0 == null;
		}
		return true;
	}

	private void Nk()
	{
		if (l83 != null)
		{
			int param_ = 1;
			if (!Nn(l8f, l8W) || !Nn(l8_u0004_, l8Z))
			{
				param_ = 4;
			}
			else if (!Nn(l8E, l8W) || !Nn(l8x, l8Z))
			{
				param_ = 3;
			}
			else if (!Nn(l8_u0014_, l8W) || !Nn(l86, l8Z))
			{
				param_ = 2;
			}
			EffectHelper.lJ(param_, ref l8N, ref l83);
		}
	}

	public BaseTerrainEffect(GraphicsDevice graphicsdevice, string effectname)
		: base(graphicsdevice, effectname)
	{
		a(graphicsdevice, param_1: true);
	}

	internal BaseTerrainEffect(GraphicsDevice param_0, string param_1, bool param_2)
		: base(param_0, param_1)
	{
		a(param_0, param_2);
	}

	private void a(GraphicsDevice param_0, bool param_1)
	{
		l83 = Parameters["LayerCount"];
		l8j = Parameters["DiffuseLayer1Texture"];
		l8p = Parameters["DiffuseLayer2Texture"];
		l8z = Parameters["DiffuseLayer3Texture"];
		l8o = Parameters["DiffuseLayer4Texture"];
		l8_u0017_ = Parameters["NormalLayer1Texture"];
		l82 = Parameters["NormalLayer2Texture"];
		l87 = Parameters["NormalLayer3Texture"];
		l8Q = Parameters["NormalLayer4Texture"];
		l8_u0012_ = Parameters["HeightMapTexture"];
		l8O = Parameters["HeightMapPSTexture"];
		l8_u000F_ = Parameters["BlendMapTexture"];
		l8F = Parameters["MeshSegments"];
		l8q = Parameters["MeshRepeatCount"];
		l80 = Parameters["NormalMapSize"];
		l8R = Parameters["NormalMapStrength"];
		l8i = Parameters["DiffuseScale"];
		l8d = Parameters["HeightScale"];
		l8_u0015_ = Parameters["Tiling"];
		l8r = Parameters["SpecularPower"];
		l8K = Parameters["SpecularAmount"];
		l8M = Parameters["SpecularColor"];
		l8W = SunBurnCoreSystem.Instance._u0013_S("White");
		l85 = SunBurnCoreSystem.Instance._u0013_t();
		l8Z = SunBurnCoreSystem.Instance._u0013_S("Normal");
		DiffuseMapLayer1Texture = l8W;
		DiffuseMapLayer2Texture = l8W;
		DiffuseMapLayer3Texture = l8W;
		DiffuseMapLayer4Texture = l8W;
		NormalMapLayer1Texture = l8Z;
		NormalMapLayer2Texture = l8Z;
		NormalMapLayer3Texture = l8Z;
		NormalMapLayer4Texture = l8Z;
		BlendMapTexture = l8W;
		HeightMapTexture = l85;
		HeightScale = 1f;
		Tiling = 1f;
		MeshSegments = 128;
		TileRepeatCount = 1;
		DiffuseScale = 16f;
		NormalMapStrength = 1f;
		SpecularAmount = 1f;
		SpecularColor = Vector3.One;
		SpecularPower = 0f;
		SetTechnique();
		Nk();
		if (param_1)
		{
			SunBurnEditor.OnCreateResource(this);
		}
	}

	protected override void Dispose(bool releasemanaged)
	{
		base.Dispose(releasemanaged);
		y.a.w_u0005_(ref l8_u0019_);
		SunBurnEditor.OnDisposeResource(this);
	}
}
