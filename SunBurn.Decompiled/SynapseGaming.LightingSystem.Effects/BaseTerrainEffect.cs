using System;
using System.Runtime.CompilerServices;
using _0013;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using j;
using v;

namespace SynapseGaming.LightingSystem.Effects;

/// <summary>
/// Base class that provides data for rendering SunBurn's terrain.
/// </summary>
[EditorObject(true)]
public abstract class BaseTerrainEffect : BaseRenderableEffect, j.B, ITerrainEffect, L.v, IEditorObject, INamedObject
{
	private string fNB = "";

	private int fNa;

	private int fNg;

	private int fNf;

	private float fN_0003;

	private float fNn;

	private float fN_0019;

	private float fNL;

	private float fNv;

	private float fN_0012;

	private Vector3 fN_0015;

	private Texture2D fN8;

	private Texture2D fNi;

	private Texture2D fN_0005;

	private Texture2D fNj;

	private Texture2D fNo;

	private Texture2D fNc;

	private Texture2D fNd;

	private Texture2D fNw;

	private Texture2D fNs;

	private Texture2D fN_0011;

	private Texture2D fNQ;

	private Texture2D fNG;

	private Texture2D fN_0017;

	private new Texture2D fNm;

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

	private EffectParameter fNF;

	private EffectParameter fNV;

	private EffectParameter fNA;

	private EffectParameter fN0;

	private EffectParameter fNK;

	private EffectParameter fNM;

	private EffectParameter fN_0006;

	private static byte[] fNJ;

	private static HalfSingle[] fN_0004;

	[CompilerGenerated]
	private bool fN3;

	[CompilerGenerated]
	private string fNl;

	[CompilerGenerated]
	private string fNt;

	[CompilerGenerated]
	private string fN2;

	[CompilerGenerated]
	private string fN7;

	[CompilerGenerated]
	private string fNp;

	[CompilerGenerated]
	private string fNZ;

	[CompilerGenerated]
	private string fND;

	[CompilerGenerated]
	private string fNU;

	[CompilerGenerated]
	private string fNP;

	[CompilerGenerated]
	private string fNe;

	[CompilerGenerated]
	private string fN5;

	[CompilerGenerated]
	private string fNE;

	/// <summary>
	/// Notifies the editor that this object is partially controlled via code. The editor
	/// will display information to the user indicating some property values are
	/// overridden in code and changes may not take effect.
	/// </summary>
	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return fN3;
		}
		[CompilerGenerated]
		set
		{
			fN3 = value;
		}
	}

	internal string MaterialFile
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
		}
	}

	string j.B.MaterialFile => fNB;

	internal string MaterialName
	{
		[CompilerGenerated]
		get
		{
			return fNl;
		}
		[CompilerGenerated]
		set
		{
			fNl = value;
		}
	}

	internal string ProjectFile
	{
		[CompilerGenerated]
		get
		{
			return fNt;
		}
		[CompilerGenerated]
		set
		{
			fNt = value;
		}
	}

	string L.v.ProjectFile => ProjectFile;

	internal string DiffuseMapLayer1File
	{
		[CompilerGenerated]
		get
		{
			return fN2;
		}
		[CompilerGenerated]
		set
		{
			fN2 = value;
		}
	}

	internal string DiffuseMapLayer2File
	{
		[CompilerGenerated]
		get
		{
			return fN7;
		}
		[CompilerGenerated]
		set
		{
			fN7 = value;
		}
	}

	internal string DiffuseMapLayer3File
	{
		[CompilerGenerated]
		get
		{
			return fNp;
		}
		[CompilerGenerated]
		set
		{
			fNp = value;
		}
	}

	internal string DiffuseMapLayer4File
	{
		[CompilerGenerated]
		get
		{
			return fNZ;
		}
		[CompilerGenerated]
		set
		{
			fNZ = value;
		}
	}

	internal string NormalMapLayer1File
	{
		[CompilerGenerated]
		get
		{
			return fND;
		}
		[CompilerGenerated]
		set
		{
			fND = value;
		}
	}

	internal string NormalMapLayer2File
	{
		[CompilerGenerated]
		get
		{
			return fNU;
		}
		[CompilerGenerated]
		set
		{
			fNU = value;
		}
	}

	internal string NormalMapLayer3File
	{
		[CompilerGenerated]
		get
		{
			return fNP;
		}
		[CompilerGenerated]
		set
		{
			fNP = value;
		}
	}

	internal string NormalMapLayer4File
	{
		[CompilerGenerated]
		get
		{
			return fNe;
		}
		[CompilerGenerated]
		set
		{
			fNe = value;
		}
	}

	internal string HeightMapFile
	{
		[CompilerGenerated]
		get
		{
			return fN5;
		}
		[CompilerGenerated]
		set
		{
			fN5 = value;
		}
	}

	internal string BlendMapFile
	{
		[CompilerGenerated]
		get
		{
			return fNE;
		}
		[CompilerGenerated]
		set
		{
			fNE = value;
		}
	}

	/// <summary>
	/// Diffuse texture used in blend mapping (associated with the Red
	/// blend map texture channel).
	///
	/// For optimal performance always use the lowest layers first (for instance:
	/// if using two layers use layer 1 and layer 2).
	/// </summary>
	[EditorProperty(true, Description = "Diffuse 1", MajorGrouping = 2, MinorGrouping = 1, ToolTipText = "")]
	[v.B("DiffuseMapLayer1File", false)]
	public Texture2D DiffuseMapLayer1Texture
	{
		get
		{
			return fNj;
		}
		set
		{
			EffectHelper.fO(value, fNi, ref fNj, ref fNR);
		}
	}

	/// <summary>
	/// Diffuse texture used in blend mapping (associated with the Green
	/// blend map texture channel).
	///
	/// For optimal performance always use the lowest layers first (for instance:
	/// if using two layers use layer 1 and layer 2).
	/// </summary>
	[EditorProperty(true, Description = "Diffuse 2", MajorGrouping = 3, MinorGrouping = 1, ToolTipText = "")]
	[v.B("DiffuseMapLayer2File", false)]
	public Texture2D DiffuseMapLayer2Texture
	{
		get
		{
			return fNo;
		}
		set
		{
			EffectHelper.fO(value, fNi, ref fNo, ref fNW);
			f_0011();
		}
	}

	/// <summary>
	/// Diffuse texture used in blend mapping (associated with the Blue
	/// blend map texture channel).
	///
	/// For optimal performance always use the lowest layers first (for instance:
	/// if using two layers use layer 1 and layer 2).
	/// </summary>
	[EditorProperty(true, Description = "Diffuse 3", MajorGrouping = 4, MinorGrouping = 1, ToolTipText = "")]
	[v.B("DiffuseMapLayer3File", false)]
	public Texture2D DiffuseMapLayer3Texture
	{
		get
		{
			return fNc;
		}
		set
		{
			EffectHelper.fO(value, fNi, ref fNc, ref fN9);
			f_0011();
		}
	}

	/// <summary>
	/// Diffuse texture used in blend mapping (associated with the Alpha
	/// blend map texture channel).
	///
	/// For optimal performance always use the lowest layers first (for instance:
	/// if using two layers use layer 1 and layer 2).
	/// </summary>
	[v.B("DiffuseMapLayer4File", false)]
	[EditorProperty(true, Description = "Diffuse 4", MajorGrouping = 5, MinorGrouping = 1, ToolTipText = "")]
	public Texture2D DiffuseMapLayer4Texture
	{
		get
		{
			return fNd;
		}
		set
		{
			EffectHelper.fO(value, fNi, ref fNd, ref fN_0013);
			f_0011();
		}
	}

	/// <summary>
	/// Normal map texture used in blend mapping (associated with the Red
	/// blend map texture channel).
	///
	/// For optimal performance always use the lowest layers first (for instance:
	/// if using two layers use layer 1 and layer 2).
	/// </summary>
	[v.B("NormalMapLayer1File", false)]
	[EditorProperty(true, Description = "Normal 1", MajorGrouping = 2, MinorGrouping = 2, ToolTipText = "")]
	public Texture2D NormalMapLayer1Texture
	{
		get
		{
			return fNw;
		}
		set
		{
			EffectHelper.fO(value, fN_0005, ref fNw, ref fNq);
		}
	}

	/// <summary>
	/// Normal map texture used in blend mapping (associated with the Green
	/// blend map texture channel).
	///
	/// For optimal performance always use the lowest layers first (for instance:
	/// if using two layers use layer 1 and layer 2).
	/// </summary>
	[EditorProperty(true, Description = "Normal 2", MajorGrouping = 3, MinorGrouping = 2, ToolTipText = "")]
	[v.B("NormalMapLayer2File", false)]
	public Texture2D NormalMapLayer2Texture
	{
		get
		{
			return fNs;
		}
		set
		{
			EffectHelper.fO(value, fN_0005, ref fNs, ref fN_000F);
			f_0011();
		}
	}

	/// <summary>
	/// Normal map texture used in blend mapping (associated with the Blue
	/// blend map texture channel).
	///
	/// For optimal performance always use the lowest layers first (for instance:
	/// if using two layers use layer 1 and layer 2).
	/// </summary>
	[EditorProperty(true, Description = "Normal 3", MajorGrouping = 4, MinorGrouping = 2, ToolTipText = "")]
	[v.B("NormalMapLayer3File", false)]
	public Texture2D NormalMapLayer3Texture
	{
		get
		{
			return fN_0011;
		}
		set
		{
			EffectHelper.fO(value, fN_0005, ref fN_0011, ref fN_0014);
			f_0011();
		}
	}

	/// <summary>
	/// Normal map texture used in blend mapping (associated with the Alpha
	/// blend map texture channel).
	///
	/// For optimal performance always use the lowest layers first (for instance:
	/// if using two layers use layer 1 and layer 2).
	/// </summary>
	[v.B("NormalMapLayer4File", false)]
	[EditorProperty(true, Description = "Normal 4", MajorGrouping = 5, MinorGrouping = 2, ToolTipText = "")]
	public Texture2D NormalMapLayer4Texture
	{
		get
		{
			return fNQ;
		}
		set
		{
			EffectHelper.fO(value, fN_0005, ref fNQ, ref fNr);
			f_0011();
		}
	}

	/// <summary>
	/// Texture containing height values used to displace a terrain mesh. Also used
	/// for low frequency lighting.
	///
	/// Requires a HalfSingle format texture.
	/// </summary>
	[v.B("HeightMapFile", true)]
	[EditorProperty(true, Description = "Height Map", MajorGrouping = 1, MinorGrouping = 1, ToolTipText = "")]
	public Texture2D HeightMapTexture
	{
		get
		{
			return fNG;
		}
		set
		{
			if (value == fNG)
			{
				return;
			}
			EffectHelper.fO(value, fN8, ref fNG, ref fNO);
			if (fNG == null)
			{
				fNu.SetValue(fNG);
				return;
			}
			if (fNG.Format != SurfaceFormat.HalfSingle)
			{
				throw new Exception("Terrain height map requires a HalfSingle format texture.");
			}
			int num = fNG.Width;
			int num2 = fNG.Height;
			int levelCount = fNG.LevelCount;
			int num3 = num * num2;
			GraphicsDevice graphicsDevice = GraphicsDevice;
			for (int k = 0; k < 16; k++)
			{
				graphicsDevice.Textures[k] = null;
			}
			for (int l = 0; l < 4; l++)
			{
				graphicsDevice.VertexTextures[l] = null;
			}
			if (fN_0017 == null || num != fN_0017.Width || num2 != fN_0017.Height)
			{
				_0013.B.nu(ref fN_0017);
				fN_0017 = new Texture2D(graphicsDevice, num, num2, levelCount > 1, SurfaceFormat.Alpha8);
			}
			if (fNJ == null || num3 > fNJ.Length)
			{
				fNJ = new byte[num3];
				fN_0004 = new HalfSingle[num3];
			}
			for (int m = 0; m < levelCount; m++)
			{
				int num4 = num * num2;
				fNG.GetData(m, null, fN_0004, 0, num4);
				for (int num5 = 0; num5 < num4; num5++)
				{
					float num6 = fN_0004[num5].ToSingle();
					fNJ[num5] = (byte)(num6 * 255f);
				}
				fN_0017.SetData(m, null, fNJ, 0, num4);
				num = Math.Max(1, num >> 1);
				num2 = Math.Max(1, num2 >> 1);
			}
			fNu.SetValue(fN_0017);
			int num7 = fNG.Width / 3;
			EffectHelper.f_000F(num7, ref fNf, ref fNx);
		}
	}

	/// <summary>
	/// Texture containing intensity values used to blend diffuse and normal map textures
	/// into the final material. Each texture channel (Red, Green, Blue, Alpha) controls
	/// a terrain texture layer (layer 1, 2, 3, 4).
	/// </summary>
	[v.B("BlendMapFile", false)]
	[EditorProperty(true, Description = "Blend Map", MajorGrouping = 1, MinorGrouping = 2, ToolTipText = "")]
	public Texture2D BlendMapTexture
	{
		get
		{
			return fNm;
		}
		set
		{
			EffectHelper.fO(value, fNi, ref fNm, ref fN1);
		}
	}

	/// <summary>
	/// Controls the depth or detail level of low frequency lighting on a terrain.
	/// </summary>
	[EditorProperty(true, Description = "Normal Strength", MajorGrouping = 6, MinorGrouping = 4, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 32.0, 0.1)]
	public float NormalMapStrength
	{
		get
		{
			return fN_0003;
		}
		set
		{
			EffectHelper.f_0014(value, ref fN_0003, ref fNF);
		}
	}

	/// <summary>
	/// Adjusts the number of times the blend mapped materials tile across a terrain's
	/// mesh. Similar to uv scale when texture mapping.
	/// </summary>
	[EditorNumberPadOptions(2, 0.0, 512.0, 0.2)]
	[EditorProperty(true, Description = "Material Scale", MajorGrouping = 6, MinorGrouping = 3, ToolTipText = "")]
	public float DiffuseScale
	{
		get
		{
			return fNn;
		}
		set
		{
			EffectHelper.f_0014(value, ref fNn, ref fNV);
		}
	}

	/// <summary>
	/// Adjusts the terrain displacement magnitude.
	/// </summary>
	[EditorNumberPadOptions(3, 0.0, 100.0, 0.01)]
	[EditorProperty(true, Description = "Height Scale", MajorGrouping = 6, MinorGrouping = 1, ToolTipText = "")]
	public float HeightScale
	{
		get
		{
			return fN_0019;
		}
		set
		{
			EffectHelper.f_0014(value, ref fN_0019, ref fNA);
		}
	}

	/// <summary>
	/// Adjusts the number of times the height map tiles across a terrain's
	/// mesh. Similar to uv scale when texture mapping.
	/// </summary>
	[EditorProperty(true, Description = "Tiling Amount", MajorGrouping = 6, MinorGrouping = 2, ToolTipText = "")]
	[EditorNumberPadOptions(3, 0.0, 100.0, 0.01)]
	public float Tiling
	{
		get
		{
			return fNL;
		}
		set
		{
			EffectHelper.f_0014(value, ref fNL, ref fN0);
		}
	}

	/// <summary>
	/// Power applied to material specular reflections. Affects how shiny a material appears.
	/// </summary>
	[EditorProperty(true, Description = "Specular Power", MajorGrouping = 7, MinorGrouping = 1, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 256.0, 0.5)]
	public float SpecularPower
	{
		get
		{
			return fNv;
		}
		set
		{
			EffectHelper.f_0014(value, ref fNv, ref fNK);
		}
	}

	/// <summary>
	/// Intensity applied to material specular reflections. Affects how intense the specular appears.
	/// </summary>
	[EditorProperty(true, Description = "Specular Amount", MajorGrouping = 7, MinorGrouping = 2, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 32.0, 0.5)]
	public float SpecularAmount
	{
		get
		{
			return fN_0012;
		}
		set
		{
			EffectHelper.f_0014(value, ref fN_0012, ref fNM);
		}
	}

	/// <summary>
	/// Color applied to material specular reflections.
	/// </summary>
	[EditorProperty(true, Description = "Specular Color", MajorGrouping = 7, MinorGrouping = 11, ControlType = ControlType.ColorSelection, ToolTipText = "")]
	public Vector3 SpecularColor
	{
		get
		{
			return fN_0015;
		}
		set
		{
			EffectHelper.fq(value, ref fN_0015, ref fN_0006);
		}
	}

	/// <summary>
	/// Density or tessellation of the terrain mesh.
	/// </summary>
	public int MeshSegments
	{
		get
		{
			return fNg;
		}
		set
		{
			EffectHelper.f_000F(value, ref fNg, ref fN_0016);
		}
	}

	private bool fs(Texture2D P_0, Texture2D P_1)
	{
		if (P_0 != P_1)
		{
			return P_0 == null;
		}
		return true;
	}

	private void f_0011()
	{
		if (fNh != null)
		{
			int num = 1;
			if (!fs(fNd, fNi) || !fs(fNQ, fN_0005))
			{
				num = 4;
			}
			else if (!fs(fNc, fNi) || !fs(fN_0011, fN_0005))
			{
				num = 3;
			}
			else if (!fs(fNo, fNi) || !fs(fNs, fN_0005))
			{
				num = 2;
			}
			EffectHelper.f_000F(num, ref fNa, ref fNh);
		}
	}

	/// <summary>
	/// Creates a new BaseTerrainEffect instance.
	/// </summary>
	/// <param name="graphicsdevice"></param>
	/// <param name="effectname"></param>
	public BaseTerrainEffect(GraphicsDevice graphicsdevice, string effectname)
		: base(graphicsdevice, effectname)
	{
		B(graphicsdevice, true);
	}

	/// <summary>
	/// Creates a new BaseTerrainEffect instance.
	/// </summary>
	/// <param name="graphicsdevice"></param>
	/// <param name="effectname"></param>
	/// <param name="trackeffect"></param>
	internal BaseTerrainEffect(GraphicsDevice P_0, string P_1, bool P_2)
		: base(P_0, P_1)
	{
		B(P_0, P_2);
	}

	private void B(GraphicsDevice P_0, bool P_1)
	{
		fNh = Parameters["LayerCount"];
		fNR = Parameters["DiffuseLayer1Texture"];
		fNW = Parameters["DiffuseLayer2Texture"];
		fN9 = Parameters["DiffuseLayer3Texture"];
		fN_0013 = Parameters["DiffuseLayer4Texture"];
		fNq = Parameters["NormalLayer1Texture"];
		fN_000F = Parameters["NormalLayer2Texture"];
		fN_0014 = Parameters["NormalLayer3Texture"];
		fNr = Parameters["NormalLayer4Texture"];
		fNO = Parameters["HeightMapTexture"];
		fNu = Parameters["HeightMapPSTexture"];
		fN1 = Parameters["BlendMapTexture"];
		fN_0016 = Parameters["MeshSegments"];
		fNx = Parameters["NormalMapSize"];
		fNF = Parameters["NormalMapStrength"];
		fNV = Parameters["DiffuseScale"];
		fNA = Parameters["HeightScale"];
		fN0 = Parameters["Tiling"];
		fNK = Parameters["SpecularPower"];
		fNM = Parameters["SpecularAmount"];
		fN_0006 = Parameters["SpecularColor"];
		fNi = LightingSystemManager.Instance.aP("White");
		fN8 = LightingSystemManager.Instance.aY(P_0);
		fN_0005 = LightingSystemManager.Instance.aP("Normal");
		DiffuseMapLayer1Texture = fNi;
		DiffuseMapLayer2Texture = fNi;
		DiffuseMapLayer3Texture = fNi;
		DiffuseMapLayer4Texture = fNi;
		NormalMapLayer1Texture = fN_0005;
		NormalMapLayer2Texture = fN_0005;
		NormalMapLayer3Texture = fN_0005;
		NormalMapLayer4Texture = fN_0005;
		BlendMapTexture = fNi;
		HeightMapTexture = fN8;
		HeightScale = 1f;
		Tiling = 1f;
		MeshSegments = 128;
		DiffuseScale = 16f;
		NormalMapStrength = 1f;
		SpecularAmount = 1f;
		SpecularColor = Vector3.One;
		SpecularPower = 0f;
		SetTechnique();
		f_0011();
		if (P_1)
		{
			LightingSystemEditor.OnCreateResource(this);
		}
	}

	/// <summary>
	/// Releases the unmanaged resources used by the Effect and optionally releases the managed resources.
	/// </summary>
	/// <param name="releasemanaged"></param>
	protected override void Dispose(bool releasemanaged)
	{
		base.Dispose(releasemanaged);
		_0013.B.nu(ref fN_0017);
		LightingSystemEditor.OnDisposeResource(this);
	}
}
