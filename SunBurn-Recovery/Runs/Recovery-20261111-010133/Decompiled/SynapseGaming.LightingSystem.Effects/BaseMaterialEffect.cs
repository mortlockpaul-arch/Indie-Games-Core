using System.Runtime.CompilerServices;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using b;
using s;

namespace SynapseGaming.LightingSystem.Effects;

[EditorObject(true)]
public abstract class BaseMaterialEffect : BaseSkinnedEffect, IAddressableEffect, IStaticLightingEffect, ITransparentEffect, b.a, B.J, IEditorObject, INamedObject, ICollisionMaterial
{
	private bool l8a;

	protected BaseLight _CurrentLight;

	protected StaticLightingEffectMode _CurrentStaticLightingEffectMode;

	private TransparencyMode l8_u0013_;

	private float l8N = 0.5f;

	private float l8l;

	private EffectParameter l8L;

	private int l8w;

	private float l8J = 0.75f;

	private float l8B = 0.25f;

	protected Texture2D _NormalMapTexture;

	protected Texture2D _DiffuseMapTexture;

	private int l8s;

	private CompositeLighting l8h = default;

	private Texture2D l8_u0002_;

	private Texture2D l8v;

	private Texture2D l8Y;

	protected Texture2D _EmissiveMapTexture;

	protected Texture2D _SpecularColorMapTexture;

	protected Texture2D _ParallaxMapTexture;

	protected Texture2D _DefaultDiffuseMapTexture;

	protected Texture2D _DefaultNormalMapTexture;

	private Texture2D l8_u0005_;

	protected Texture2D _DefaultEmissiveMapTexture;

	private LightMap l8b;

	private string l8I = "";

	protected Vector3 _AlphaPreblend_NoPreBlend_Additive;

	protected EffectParameter _AlphaPreblend_NoPreBlend_AdditiveParam;

	private EffectParameter l85;

	private EffectParameter l8W;

	private EffectParameter l8Z;

	private EffectParameter l8T;

	private EffectParameter l8_u0014_;

	private EffectParameter l8E;

	private EffectParameter l8f;

	private new EffectParameter l8_u0016_;

	private EffectParameter l86;

	private EffectParameter l8x;

	protected EffectParameter _DiffuseColorIndirectParam;

	protected EffectParameter _DiffuseMapTextureIndirectParam;

	protected EffectParameter _NormalMapTextureIndirectParam;

	protected Vector4 _DiffuseColorOriginal;

	protected Vector4 _DiffuseColorCached;

	protected Vector4 _EmissiveColor;

	private EffectParameter l8_u0004_;

	private float l8X;

	private float l8_u0019_;

	private EffectParameter l8y;

	private float l83;

	private float l8j;

	private float l8p;

	private Vector4 l8z;

	private EffectParameter l8o;

	private EffectParameter l8_u0017_;

	[CompilerGenerated]
	private bool l82;

	[CompilerGenerated]
	private string l87;

	[CompilerGenerated]
	private string l8Q;

	[CompilerGenerated]
	private string l8_u0012_;

	[CompilerGenerated]
	private string l8O;

	[CompilerGenerated]
	private string l8_u000F_;

	[CompilerGenerated]
	private string l8F;

	[CompilerGenerated]
	private string l8q;

	[CompilerGenerated]
	private TextureAddressMode l80;

	[CompilerGenerated]
	private TextureAddressMode l8R;

	[CompilerGenerated]
	private TextureAddressMode l8i;

	[CompilerGenerated]
	private bool l8d;

	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l82;
		}
		[CompilerGenerated]
		set
		{
			l82 = value;
		}
	}

	internal string MaterialFile
	{
		get
		{
			return l8I;
		}
		set
		{
			l8I = value;
		}
	}

	string b.a.MaterialFile => l8I;

	string B.J.ProjectFile => ProjectFile;

	internal string MaterialName
	{
		[CompilerGenerated]
		get
		{
			return l87;
		}
		[CompilerGenerated]
		set
		{
			l87 = value;
		}
	}

	internal string ProjectFile
	{
		[CompilerGenerated]
		get
		{
			return l8Q;
		}
		[CompilerGenerated]
		set
		{
			l8Q = value;
		}
	}

	internal string NormalMapFile
	{
		[CompilerGenerated]
		get
		{
			return l8_u0012_;
		}
		[CompilerGenerated]
		set
		{
			l8_u0012_ = value;
		}
	}

	internal string DiffuseMapFile
	{
		[CompilerGenerated]
		get
		{
			return l8O;
		}
		[CompilerGenerated]
		set
		{
			l8O = value;
		}
	}

	internal string EmissiveMapFile
	{
		[CompilerGenerated]
		get
		{
			return l8_u000F_;
		}
		[CompilerGenerated]
		set
		{
			l8_u000F_ = value;
		}
	}

	internal string SpecularColorMapFile
	{
		[CompilerGenerated]
		get
		{
			return l8F;
		}
		[CompilerGenerated]
		set
		{
			l8F = value;
		}
	}

	internal string ParallaxMapFile
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

	public Texture2D MicrofacetTexture
	{
		get
		{
			return l8Y;
		}
		set
		{
			if (l8Y != value && l8f != null)
			{
				l8Y = value;
				l8f.SetValue(value);
			}
		}
	}

	[EditorProperty(true, Description = "Normal Map", HorizontalAlignment = false, MajorGrouping = 1, MinorGrouping = 3, ToolTipText = "")]
	[s.a("NormalMapFile", false)]
	public Texture2D NormalMapTexture
	{
		get
		{
			return _NormalMapTexture;
		}
		set
		{
			SyncDiffuseAndNormalData(_DiffuseColorOriginal, _DiffuseMapTexture, value);
			SetTechnique();
		}
	}

	[EditorProperty(true, Description = "Diffuse Map", HorizontalAlignment = false, MajorGrouping = 1, MinorGrouping = 1, ToolTipText = "")]
	[s.a("DiffuseMapFile", false)]
	public Texture2D DiffuseMapTexture
	{
		get
		{
			return _DiffuseMapTexture;
		}
		set
		{
			SyncDiffuseAndNormalData(_DiffuseColorOriginal, value, _NormalMapTexture);
		}
	}

	[s.a("EmissiveMapFile", false)]
	[EditorProperty(true, Description = "Emissive Map", HorizontalAlignment = false, MajorGrouping = 1, MinorGrouping = 4, ToolTipText = "")]
	public Texture2D EmissiveMapTexture
	{
		get
		{
			return _EmissiveMapTexture;
		}
		set
		{
			Texture2D texture2D = value;
			if (texture2D == null)
			{
				texture2D = _DefaultEmissiveMapTexture;
			}
			EffectHelper.lh(texture2D, ref _EmissiveMapTexture, ref l8_u0016_);
			SetTechnique();
		}
	}

	[s.a("SpecularColorMapFile", false)]
	[EditorProperty(true, Description = "Color Spec", HorizontalAlignment = false, MajorGrouping = 3, MinorGrouping = 1, ToolTipText = "")]
	public Texture2D SpecularColorMapTexture
	{
		get
		{
			return _SpecularColorMapTexture;
		}
		set
		{
			Texture2D texture2D = value;
			if (texture2D == null)
			{
				texture2D = l8_u0005_;
			}
			EffectHelper.lh(texture2D, ref _SpecularColorMapTexture, ref l86);
			SetTechnique();
		}
	}

	[EditorProperty(true, Description = "Parallax", HorizontalAlignment = false, MajorGrouping = 5, MinorGrouping = 1, ToolTipText = "")]
	[s.a("ParallaxMapFile", false)]
	public Texture2D ParallaxMapTexture
	{
		get
		{
			return _ParallaxMapTexture;
		}
		set
		{
			EffectHelper.lh(value, ref _ParallaxMapTexture, ref l8x);
			SetTechnique();
		}
	}

	[EditorProperty(true, Description = "Diffuse Color", HorizontalAlignment = true, MajorGrouping = 2, MinorGrouping = 2, ControlType = ControlType.ColorSelection, ToolTipText = "")]
	public Vector3 DiffuseColor
	{
		get
		{
			return new Vector3(_DiffuseColorOriginal.X, _DiffuseColorOriginal.Y, _DiffuseColorOriginal.Z);
		}
		set
		{
			SyncDiffuseAndNormalData(new Vector4(value, _DiffuseColorOriginal.W), _DiffuseMapTexture, _NormalMapTexture);
		}
	}

	[EditorProperty(true, Description = "Amount", HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 13, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 1.0, 0.02)]
	public float TransparencyAmount
	{
		get
		{
			return _DiffuseColorOriginal.W;
		}
		set
		{
			SyncDiffuseAndNormalData(new Vector4(_DiffuseColorOriginal.X, _DiffuseColorOriginal.Y, _DiffuseColorOriginal.Z, value), _DiffuseMapTexture, _NormalMapTexture);
		}
	}

	[EditorProperty(true, Description = "Emissive Color", HorizontalAlignment = true, MajorGrouping = 2, MinorGrouping = 3, ControlType = ControlType.ColorSelection, ToolTipText = "")]
	public Vector3 EmissiveColor
	{
		get
		{
			return new Vector3(_EmissiveColor.X, _EmissiveColor.Y, _EmissiveColor.Z);
		}
		set
		{
			EffectHelper.lL(new Vector4(value.X, value.Y, value.Z, 1f), ref _EmissiveColor, ref l8_u0004_);
		}
	}

	[EditorNumberPadOptions(2, 0.0, 256.0, 0.5)]
	[EditorProperty(true, Description = "Specular Power", HorizontalAlignment = false, MajorGrouping = 3, MinorGrouping = 2, ToolTipText = "")]
	public float SpecularPower
	{
		get
		{
			return l8X;
		}
		set
		{
			N_u0017_(value, l8_u0019_);
			SyncDiffuseAndNormalData(_DiffuseColorOriginal, _DiffuseMapTexture, _NormalMapTexture);
			SetTechnique();
		}
	}

	[EditorNumberPadOptions(2, 0.0, 32.0, 0.05)]
	[EditorProperty(true, Description = "Specular Amount", HorizontalAlignment = false, MajorGrouping = 3, MinorGrouping = 3, ToolTipText = "")]
	public float SpecularAmount
	{
		get
		{
			return l8_u0019_;
		}
		set
		{
			N_u0017_(l8X, value);
			SyncDiffuseAndNormalData(_DiffuseColorOriginal, _DiffuseMapTexture, _NormalMapTexture);
			SetTechnique();
		}
	}

	[EditorProperty(true, Description = "Fresnel Bias", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 1, ToolTipText = "")]
	[EditorNumberPadOptions(3, 0.0, 4.0, 0.01)]
	public float FresnelReflectBias
	{
		get
		{
			return l83;
		}
		set
		{
			N2(value, l8j, l8p);
			SetTechnique();
		}
	}

	[EditorProperty(true, Description = "Fresnel Offset", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 2, ToolTipText = "")]
	[EditorNumberPadOptions(3, 0.0, 4.0, 0.01)]
	public float FresnelReflectOffset
	{
		get
		{
			return l8j;
		}
		set
		{
			N2(l83, value, l8p);
			SetTechnique();
		}
	}

	[EditorProperty(true, Description = "Distribution", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 3, ToolTipText = "")]
	[EditorNumberPadOptions(3, 0.0, 1.0, 0.01)]
	public float FresnelMicrofacetDistribution
	{
		get
		{
			return l8p;
		}
		set
		{
			N2(l83, l8j, value);
			SetTechnique();
		}
	}

	[EditorNumberPadOptions(3, 0.0, 100.0, 0.005)]
	[EditorProperty(true, Description = "Parallax Scale", HorizontalAlignment = false, MajorGrouping = 5, MinorGrouping = 2, ToolTipText = "")]
	public float ParallaxScale
	{
		get
		{
			return l8z.X;
		}
		set
		{
			EffectHelper.lL(new Vector4(value, l8z.Y, l8z.Z, l8z.W), ref l8z, ref l8_u0017_);
		}
	}

	[EditorProperty(true, Description = "Parallax Offset", HorizontalAlignment = false, MajorGrouping = 5, MinorGrouping = 3, ToolTipText = "")]
	[EditorNumberPadOptions(3, -100.0, 100.0, 0.005)]
	public float ParallaxOffset
	{
		get
		{
			return l8z.Y;
		}
		set
		{
			EffectHelper.lL(new Vector4(l8z.X, value, l8z.Z, l8z.W), ref l8z, ref l8_u0017_);
		}
	}

	[EditorProperty(true, Description = "Addressing U", HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 1, ToolTipText = "")]
	public TextureAddressMode AddressModeU
	{
		[CompilerGenerated]
		get
		{
			return l80;
		}
		[CompilerGenerated]
		set
		{
			l80 = value;
		}
	}

	[EditorProperty(true, Description = "Addressing V", HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 2, ToolTipText = "")]
	public TextureAddressMode AddressModeV
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

	[EditorProperty(true, Description = "Addressing W", HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 3, ToolTipText = "")]
	public TextureAddressMode AddressModeW
	{
		[CompilerGenerated]
		get
		{
			return l8i;
		}
		[CompilerGenerated]
		set
		{
			l8i = value;
		}
	}

	[EditorCheckboxOptions(true)]
	[EditorProperty(true, Description = "Transparent", HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 11, ControlType = ControlType.DropDown, ToolTipText = "")]
	public virtual TransparencyMode TransparencyMode
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			if (l8_u0013_ != value)
			{
				l8_u0013_ = value;
				SyncTransparency(changedmode: true);
			}
		}
	}

	[EditorNumberPadOptions(3, 0.0, 1.0, 0.005)]
	[EditorProperty(true, Description = "Threshold", HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 12, ToolTipText = "")]
	public virtual float TransparencyThreshold
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
			SyncTransparency(changedmode: false);
		}
	}

	public Texture TransparencyMap
	{
		get
		{
			return _DiffuseMapTexture;
		}
		set
		{
			DiffuseMapTexture = (Texture2D)value;
		}
	}

	[EditorProperty(true, Description = "Double Sided", HorizontalAlignment = true, MajorGrouping = 2, MinorGrouping = 1, ToolTipText = "")]
	public override bool DoubleSided
	{
		[CompilerGenerated]
		get
		{
			return l8d;
		}
		[CompilerGenerated]
		set
		{
			l8d = value;
		}
	}

	[EditorProperty(true, Description = "Elasticity", HorizontalAlignment = true, MajorGrouping = 8, MinorGrouping = 1, ToolTipText = "")]
	[EditorNumberPadOptions(3, 0.0, 1.0, 0.05)]
	public float Elasticity
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = MathHelper.Clamp(value, 0f, 1f);
			l8w++;
		}
	}

	[EditorNumberPadOptions(3, 0.0, 1.0, 0.05)]
	[EditorProperty(true, Description = "Friction", HorizontalAlignment = true, MajorGrouping = 8, MinorGrouping = 2, ToolTipText = "")]
	public float Friction
	{
		get
		{
			return l8B;
		}
		set
		{
			l8B = MathHelper.Clamp(value, 0f, 1f);
			l8w++;
		}
	}

	public int CollisionId => l8w;

	public void SetStaticLighting(StaticLightingEffectMode lightingmode, LightMap lightmap, ref CompositeLighting compositelighting)
	{
		bool flag = lightingmode == StaticLightingEffectMode.BakedDown || lightingmode == StaticLightingEffectMode.BakedDownAndComposite;
		bool flag2 = lightingmode == StaticLightingEffectMode.Composite || lightingmode == StaticLightingEffectMode.BakedDownAndComposite;
		if (lightmap == null)
		{
			lightmap = l8b;
		}
		int num = (flag ? 1 : 0);
		Texture2D lightMapColorTexture = lightmap.LightMapColorTexture;
		if (num != l8s || (flag && lightMapColorTexture != l8_u0002_))
		{
			if (flag)
			{
				EffectHelper.lh(lightMapColorTexture, ref l8_u0002_, ref l8W);
				EffectHelper.lh(lightmap.LightMapDirectionalTexture, ref l8v, ref l8Z);
			}
			EffectHelper.lJ(num, ref l8s, ref l85);
			_UpdatedByBatch = true;
		}
		if (flag2 && l8T != null && l8_u0014_ != null && l8E != null && !l8h.Equals(compositelighting))
		{
			l8T.SetValue(compositelighting.AmbientColor);
			l8_u0014_.SetValue(compositelighting.DiffuseColor);
			l8E.SetValue(compositelighting.Direction);
			l8h = compositelighting;
			_UpdatedByBatch = true;
		}
		_CurrentStaticLightingEffectMode = lightingmode;
		SetTechniqueShaderArrayIndices();
	}

	public void SetStaticLighting(StaticLightingEffectMode lightingmode, LightMap lightmap)
	{
		SetStaticLighting(lightingmode, lightmap, ref l8h);
	}

	public void SetTransparencyModeAndMap(TransparencyMode mode, float threshold, Texture map)
	{
		bool changedmode = l8_u0013_ != mode;
		l8_u0013_ = mode;
		l8N = threshold;
		DiffuseMapTexture = map as Texture2D;
		SyncTransparency(changedmode);
	}

	protected virtual void SyncTransparency(bool changedmode)
	{
		float num = l8l;
		Vector3 param_ = Vector3.UnitY;
		if (l8_u0013_ == TransparencyMode.Clip)
		{
			num = TransparencyThreshold;
		}
		else if (l8_u0013_ == TransparencyMode.None)
		{
			num = 0f;
		}
		else
		{
			param_ = ((l8_u0013_ != TransparencyMode.Additive) ? Vector3.UnitX : new Vector3(1f, 0f, 1f));
			num = 0.04f;
		}
		EffectHelper.lB(num, ref l8l, ref l8L);
		EffectHelper.lw(param_, ref _AlphaPreblend_NoPreBlend_Additive, ref _AlphaPreblend_NoPreBlend_AdditiveParam);
	}

	protected virtual void SyncDiffuseAndNormalData(Vector4 diffusecolor, Texture2D diffusemap, Texture2D normalmap)
	{
		_DiffuseColorOriginal = diffusecolor;
		if (diffusemap == null || diffusemap == _DefaultDiffuseMapTexture)
		{
			EffectHelper.lh(_DefaultDiffuseMapTexture, ref _DiffuseMapTexture, ref _DiffuseMapTextureIndirectParam);
			EffectHelper.lL(diffusecolor, ref _DiffuseColorCached, ref _DiffuseColorIndirectParam);
		}
		else
		{
			EffectHelper.lh(diffusemap, ref _DiffuseMapTexture, ref _DiffuseMapTextureIndirectParam);
			EffectHelper.lL(new Vector4(Vector3.One, diffusecolor.W), ref _DiffuseColorCached, ref _DiffuseColorIndirectParam);
		}
		if (normalmap == null || normalmap == _DefaultNormalMapTexture)
		{
			if (l8_u0019_ > 0f && l8X > 0f)
			{
				EffectHelper.lh(_DefaultNormalMapTexture, ref _NormalMapTexture, ref _NormalMapTextureIndirectParam);
			}
			else
			{
				EffectHelper.lh(null, ref _NormalMapTexture, ref _NormalMapTextureIndirectParam);
			}
		}
		else
		{
			EffectHelper.lh(normalmap, ref _NormalMapTexture, ref _NormalMapTextureIndirectParam);
		}
	}

	private void N_u0017_(float param_0, float param_1)
	{
		if ((l8X != param_0 || l8_u0019_ != param_1) && l8y != null)
		{
			l8X = param_0;
			l8_u0019_ = param_1;
			if (l8X <= 0f || l8_u0019_ <= 0f)
			{
				l8y.SetValue(new Vector4(10000f, 0f, 0f, 0f));
			}
			else
			{
				l8y.SetValue(new Vector4(l8X, l8_u0019_, 0f, 0f));
			}
		}
	}

	private void N2(float param_0, float param_1, float param_2)
	{
		if ((l83 != param_0 || l8j != param_1 || l8p != param_2) && l8o != null)
		{
			l83 = param_0;
			l8j = param_1;
			l8p = param_2;
			l8o.SetValue(new Vector4(l83, l8j, l8p, 0f));
		}
	}

	protected override void SetTechnique()
	{
		base.l8_u0016_.l8a.AccumulationValue++;
		bool param_ = DoubleSided && l8a;
		if (_CurrentLight != null)
		{
			bool flag = _CurrentLight is AmbientLight;
			bool fillLight = _CurrentLight.FillLight;
			bool flag2 = EffectDetail <= DetailPreference.High && !fillLight;
			bool flag3 = EffectDetail <= DetailPreference.Medium && l8_u0013_ != TransparencyMode.Clip;
			bool flag4 = EffectDetail <= DetailPreference.Low && !fillLight;
			if (flag)
			{
				if (flag3 && _ParallaxMapTexture != null && _NormalMapTexture != null)
				{
					CurrentTechnique = Techniques[b.l.lb(b.l.w8a.Ambient, b.l.w8_u0013_.Tangent, 1, param_3: false, param_4: false, Skinned, param_6: false)];
				}
				else if (_NormalMapTexture != null)
				{
					CurrentTechnique = Techniques[b.l.lb(b.l.w8a.Ambient, b.l.w8_u0013_.Tangent, 1, param_3: false, param_4: false, Skinned, param_6: false)];
				}
				else
				{
					CurrentTechnique = Techniques[b.l.lb(b.l.w8a.Ambient, b.l.w8_u0013_.None, 1, param_3: false, param_4: false, Skinned, param_6: false)];
				}
			}
			else
			{
				b.l.w8_u0013_ param_2 = b.l.w8_u0013_.Diffuse;
				bool flag5 = _SpecularColorMapTexture != null && _SpecularColorMapTexture != l8_u0005_;
				if (_NormalMapTexture != null)
				{
					if (flag3 && _ParallaxMapTexture != null)
					{
						if (!flag4 || !(l8X > 0f) || !(l8_u0019_ > 0f))
						{
							param_2 = b.l.w8_u0013_.DiffuseParallax;
						}
						else if (flag2 && l83 > 0f && l8j > 0f)
						{
							param_2 = ((!flag5) ? b.l.w8_u0013_.DiffuseParallaxFresnel : b.l.w8_u0013_.DiffuseParallaxFresnelColor);
						}
						else
						{
							param_2 = ((!flag5) ? b.l.w8_u0013_.DiffuseParallaxSpecular : b.l.w8_u0013_.DiffuseParallaxSpecularColor);
						}
					}
					else if (!flag4 || !(l8X > 0f) || !(l8_u0019_ > 0f))
					{
						param_2 = b.l.w8_u0013_.DiffuseBump;
					}
					else if (flag2 && l83 > 0f && l8j > 0f)
					{
						param_2 = ((!flag5) ? b.l.w8_u0013_.DiffuseBumpFresnel : b.l.w8_u0013_.DiffuseBumpFresnelColor);
					}
					else
					{
						param_2 = ((!flag5) ? b.l.w8_u0013_.DiffuseBumpSpecular : b.l.w8_u0013_.DiffuseBumpSpecularColor);
					}
				}
				CurrentTechnique = Techniques[b.l.lb(b.l.w8a.Lighting, param_2, 1, param_, param_4: false, Skinned, param_6: false)];
			}
		}
		else
		{
			CurrentTechnique = Techniques[b.l.lb(b.l.w8a.Ambient, b.l.w8_u0013_.None, 1, param_3: false, param_4: false, Skinned, param_6: false)];
		}
		SetTechniqueShaderArrayIndices();
	}

	protected abstract void SetTechniqueShaderArrayIndices();

	public BaseMaterialEffect(GraphicsDevice graphicsdevice, string effectname)
		: base(graphicsdevice, effectname)
	{
		a(graphicsdevice, param_1: true);
	}

	internal BaseMaterialEffect(GraphicsDevice param_0, string param_1, bool param_2)
		: base(param_0, param_1)
	{
		a(param_0, param_2);
	}

	private void a(GraphicsDevice param_0, bool param_1)
	{
		l8a = GraphicsDevice.GraphicsProfile == GraphicsProfile.HiDef;
		l8y = Parameters["_SpecularPower_And_Amount"];
		l8L = Parameters["_TransClipRef"];
		l8o = Parameters["_FresnelReflectBias_Offset_Microfacet"];
		l8_u0017_ = Parameters["_ParallaxScale_And_Offset"];
		_DiffuseColorIndirectParam = Parameters["_DiffuseColor"];
		_DiffuseMapTextureIndirectParam = Parameters["_DiffuseMapTexture"];
		_NormalMapTextureIndirectParam = Parameters["_NormalMapTexture"];
		l8_u0004_ = Parameters["_EmissiveColor"];
		_AlphaPreblend_NoPreBlend_AdditiveParam = Parameters["_AlphaPreblend_NoPreBlend_Additive"];
		l85 = Parameters["_LightMapped"];
		l8W = Parameters["_LightMapColorTexture"];
		l8Z = Parameters["_LightMapDirectionalTexture"];
		l8T = Parameters["_CompositeLightingAmbient"];
		l8_u0014_ = Parameters["_CompositeLightingDiffuse"];
		l8E = Parameters["_CompositeLightingDirection"];
		l8f = Parameters["_MicrofacetTexture"];
		l8_u0016_ = Parameters["_EmissiveMapTexture"];
		l86 = Parameters["_SpecularColorMapTexture"];
		l8x = Parameters["_ParallaxMapTexture"];
		MicrofacetTexture = SunBurnCoreSystem.Instance._u0013_4();
		Texture2D defaultDiffuseMapTexture = SunBurnCoreSystem.Instance._u0013_S("White");
		Texture2D texture2D = SunBurnCoreSystem.Instance._u0013_S("Black");
		_DefaultDiffuseMapTexture = defaultDiffuseMapTexture;
		_DefaultNormalMapTexture = SunBurnCoreSystem.Instance._u0013_S("Normal");
		l8_u0005_ = defaultDiffuseMapTexture;
		_DefaultEmissiveMapTexture = texture2D;
		l8b = new LightMap(texture2D, texture2D);
		DiffuseColor = Vector3.One;
		TransparencyAmount = 1f;
		SpecularPower = 4f;
		SpecularAmount = 0.25f;
		FresnelReflectBias = 0f;
		FresnelReflectOffset = 1f;
		FresnelMicrofacetDistribution = 0.4f;
		SpecularColorMapTexture = l8_u0005_;
		EmissiveMapTexture = _DefaultEmissiveMapTexture;
		SetStaticLighting(StaticLightingEffectMode.Ambient, l8b);
		SetTechnique();
		MaterialName = string.Empty;
		ProjectFile = string.Empty;
		NormalMapFile = string.Empty;
		DiffuseMapFile = string.Empty;
		EmissiveMapFile = string.Empty;
		SpecularColorMapFile = string.Empty;
		ParallaxMapFile = string.Empty;
		if (param_1)
		{
			SunBurnEditor.OnCreateResource(this);
		}
	}

	protected override void Dispose(bool releasemanaged)
	{
		base.Dispose(releasemanaged);
		SunBurnEditor.OnDisposeResource(this);
	}
}
