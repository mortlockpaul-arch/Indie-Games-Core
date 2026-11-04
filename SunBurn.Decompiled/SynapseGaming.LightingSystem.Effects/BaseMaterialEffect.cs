using System.Runtime.CompilerServices;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using j;
using v;

namespace SynapseGaming.LightingSystem.Effects;

/// <summary>
/// Base class that provides data for SunBurn materials (bump, specular, parallax, ...).  Used by the
/// forward rendering LightingEffect and deferred rendering DeferredObjectEffect classes.
/// </summary>
[EditorObject(true)]
public abstract class BaseMaterialEffect : BaseSkinnedEffect, IAddressableEffect, IStaticLightingEffect, ITransparentEffect, j.B, L.v, IEditorObject, INamedObject, ICollisionMaterial
{
	private bool fNB;

	/// <summary />
	protected BaseLight _CurrentLight;

	private TransparencyMode fNa;

	private float fNg = 0.5f;

	private float fNf;

	private EffectParameter fN_0003;

	private float fNn = 0.25f;

	private float fN_0019 = 0.25f;

	/// <summary />
	protected Texture2D _NormalMapTexture;

	/// <summary />
	protected Texture2D _DiffuseMapTexture;

	private int fNL;

	private CompositeLighting fNv = default;

	private Texture2D fN_0012;

	private Texture2D fN_0015;

	/// <summary />
	protected Texture2D _EmissiveMapTexture;

	/// <summary />
	protected Texture2D _SpecularColorMapTexture;

	/// <summary />
	protected Texture2D _ParallaxMapTexture;

	/// <summary />
	protected Texture2D _DefaultDiffuseMapTexture;

	/// <summary />
	protected Texture2D _DefaultNormalMapTexture;

	private Texture2D fN8;

	private Texture2D fNi;

	private string fN_0005 = "";

	private EffectParameter fNj;

	private EffectParameter fNo;

	private EffectParameter fNc;

	private EffectParameter fNd;

	private EffectParameter fNw;

	private EffectParameter fNs;

	private EffectParameter fN_0011;

	private EffectParameter fNQ;

	private EffectParameter fNG;

	/// <summary />
	protected EffectParameter _DiffuseColorIndirectParam;

	/// <summary />
	protected EffectParameter _DiffuseMapTextureIndirectParam;

	/// <summary />
	protected EffectParameter _NormalMapTextureIndirectParam;

	/// <summary />
	protected Vector4 _DiffuseColorOriginal;

	/// <summary />
	protected Vector4 _DiffuseColorCached;

	/// <summary />
	protected Vector4 _EmissiveColor;

	private EffectParameter fN_0017;

	private new float fNm;

	private float fNh;

	private EffectParameter fNR;

	private float fNW;

	private float fN9;

	private float fN_0013;

	private Vector4 fNq;

	private EffectParameter fN_000F;

	private EffectParameter fN_0014;

	[CompilerGenerated]
	private bool fNr;

	[CompilerGenerated]
	private string fNO;

	[CompilerGenerated]
	private string fNu;

	[CompilerGenerated]
	private string fN1;

	[CompilerGenerated]
	private string fN_0016;

	[CompilerGenerated]
	private string fNx;

	[CompilerGenerated]
	private string fNF;

	[CompilerGenerated]
	private string fNV;

	[CompilerGenerated]
	private TextureAddressMode fNA;

	[CompilerGenerated]
	private TextureAddressMode fN0;

	[CompilerGenerated]
	private TextureAddressMode fNK;

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
			return fNr;
		}
		[CompilerGenerated]
		set
		{
			fNr = value;
		}
	}

	internal string MaterialFile
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

	string j.B.MaterialFile => fN_0005;

	string L.v.ProjectFile => ProjectFile;

	internal string MaterialName
	{
		[CompilerGenerated]
		get
		{
			return fNO;
		}
		[CompilerGenerated]
		set
		{
			fNO = value;
		}
	}

	internal string ProjectFile
	{
		[CompilerGenerated]
		get
		{
			return fNu;
		}
		[CompilerGenerated]
		set
		{
			fNu = value;
		}
	}

	internal string NormalMapFile
	{
		[CompilerGenerated]
		get
		{
			return fN1;
		}
		[CompilerGenerated]
		set
		{
			fN1 = value;
		}
	}

	internal string DiffuseMapFile
	{
		[CompilerGenerated]
		get
		{
			return fN_0016;
		}
		[CompilerGenerated]
		set
		{
			fN_0016 = value;
		}
	}

	internal string EmissiveMapFile
	{
		[CompilerGenerated]
		get
		{
			return fNx;
		}
		[CompilerGenerated]
		set
		{
			fNx = value;
		}
	}

	internal string SpecularColorMapFile
	{
		[CompilerGenerated]
		get
		{
			return fNF;
		}
		[CompilerGenerated]
		set
		{
			fNF = value;
		}
	}

	internal string ParallaxMapFile
	{
		[CompilerGenerated]
		get
		{
			return fNV;
		}
		[CompilerGenerated]
		set
		{
			fNV = value;
		}
	}

	/// <summary>
	/// Texture that represents the fresnel micro-facet distribution method.
	/// </summary>
	public Texture2D MicrofacetTexture
	{
		get
		{
			return fN_0015;
		}
		set
		{
			if (fN_0015 != value && fNs != null)
			{
				fN_0015 = value;
				fNs.SetValue(value);
			}
		}
	}

	/// <summary>
	/// Texture normal-map used to apply bump mapping to materials. Setting the
	/// texture to null disables this feature.
	/// </summary>
	[EditorProperty(true, Description = "Normal Map", HorizontalAlignment = false, MajorGrouping = 1, MinorGrouping = 3, ToolTipText = "")]
	[v.B("NormalMapFile", false)]
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

	/// <summary>
	/// Texture used as the primary color map for materials. Generally this texture
	/// includes shading and lighting information when bump mapping is not used. Setting
	/// the texture to null disables this feature.
	/// </summary>
	[v.B("DiffuseMapFile", false)]
	[EditorProperty(true, Description = "Diffuse Map", HorizontalAlignment = false, MajorGrouping = 1, MinorGrouping = 1, ToolTipText = "")]
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

	/// <summary>
	/// Texture used to apply emissive lighting and self-illumination to materials. Setting the
	/// texture to null disables this feature.
	/// </summary>
	[EditorProperty(true, Description = "Emissive Map", HorizontalAlignment = false, MajorGrouping = 1, MinorGrouping = 4, ToolTipText = "")]
	[v.B("EmissiveMapFile", false)]
	public Texture2D EmissiveMapTexture
	{
		get
		{
			return _EmissiveMapTexture;
		}
		set
		{
			EffectHelper.fO(value, ref _EmissiveMapTexture, ref fN_0011);
			SetTechnique();
		}
	}

	/// <summary>
	/// Texture used to apply tint to the specular reflection. Commonly used for
	/// materials with complex reflection properties like metal, oil, and skin.
	/// Setting the texture to null disables this feature.
	/// </summary>
	[EditorProperty(true, Description = "Color Spec", HorizontalAlignment = false, MajorGrouping = 3, MinorGrouping = 1, ToolTipText = "")]
	[v.B("SpecularColorMapFile", false)]
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
				texture2D = fN8;
			}
			EffectHelper.fO(texture2D, ref _SpecularColorMapTexture, ref fNQ);
			SetTechnique();
		}
	}

	/// <summary>
	/// Gray scale height-map texture used to apply visual depth to materials
	/// without adding geometry. Setting the texture to null disables this feature.
	/// </summary>
	[v.B("ParallaxMapFile", false)]
	[EditorProperty(true, Description = "Parallax", HorizontalAlignment = false, MajorGrouping = 5, MinorGrouping = 1, ToolTipText = "")]
	public Texture2D ParallaxMapTexture
	{
		get
		{
			return _ParallaxMapTexture;
		}
		set
		{
			EffectHelper.fO(value, ref _ParallaxMapTexture, ref fNG);
			SetTechnique();
		}
	}

	/// <summary>
	/// Base color applied to materials when no DiffuseMapTexture is specified.
	/// </summary>
	[EditorProperty(true, Description = "Diffuse Color", HorizontalAlignment = false, MajorGrouping = 2, MinorGrouping = 1, ControlType = ControlType.ColorSelection, ToolTipText = "")]
	public Vector3 DiffuseColor
	{
		get
		{
			return new Vector3(_DiffuseColorOriginal.X, _DiffuseColorOriginal.Y, _DiffuseColorOriginal.Z);
		}
		set
		{
			SyncDiffuseAndNormalData(new Vector4(value, 1f), _DiffuseMapTexture, _NormalMapTexture);
		}
	}

	/// <summary>
	/// Color used to apply emissive lighting and self-illumination to materials.
	/// </summary>
	[EditorProperty(true, Description = "Emissive Color", HorizontalAlignment = false, MajorGrouping = 2, MinorGrouping = 2, ControlType = ControlType.ColorSelection, ToolTipText = "")]
	public Vector3 EmissiveColor
	{
		get
		{
			return new Vector3(_EmissiveColor.X, _EmissiveColor.Y, _EmissiveColor.Z);
		}
		set
		{
			EffectHelper.f_0013(new Vector4(value.X, value.Y, value.Z, 1f), ref _EmissiveColor, ref fN_0017);
		}
	}

	/// <summary>
	/// Power applied to material specular reflections. Affects how shiny a material appears.
	/// </summary>
	[EditorNumberPadOptions(2, 0.0, 256.0, 0.5)]
	[EditorProperty(true, Description = "Specular Power", HorizontalAlignment = false, MajorGrouping = 3, MinorGrouping = 2, ToolTipText = "")]
	public float SpecularPower
	{
		get
		{
			return fNm;
		}
		set
		{
			gT(value, fNh);
			SyncDiffuseAndNormalData(_DiffuseColorOriginal, _DiffuseMapTexture, _NormalMapTexture);
			SetTechnique();
		}
	}

	/// <summary>
	/// Intensity applied to material specular reflections. Affects how intense the specular appears.
	/// </summary>
	[EditorNumberPadOptions(2, 0.0, 32.0, 0.05)]
	[EditorProperty(true, Description = "Specular Amount", HorizontalAlignment = false, MajorGrouping = 3, MinorGrouping = 3, ToolTipText = "")]
	public float SpecularAmount
	{
		get
		{
			return fNh;
		}
		set
		{
			gT(fNm, value);
			SyncDiffuseAndNormalData(_DiffuseColorOriginal, _DiffuseMapTexture, _NormalMapTexture);
			SetTechnique();
		}
	}

	/// <summary>
	/// Fine tunes fresnel sub-surface scattering on reflection angles facing away from the camera.
	/// </summary>
	[EditorNumberPadOptions(3, 0.0, 4.0, 0.01)]
	[EditorProperty(true, Description = "Fresnel Bias", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 1, ToolTipText = "")]
	public float FresnelReflectBias
	{
		get
		{
			return fNW;
		}
		set
		{
			gz(value, fN9, fN_0013);
			SetTechnique();
		}
	}

	/// <summary>
	/// Fine tunes fresnel sub-surface scattering on reflection angles facing towards the camera.
	/// </summary>
	[EditorNumberPadOptions(3, 0.0, 4.0, 0.01)]
	[EditorProperty(true, Description = "Fresnel Offset", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 2, ToolTipText = "")]
	public float FresnelReflectOffset
	{
		get
		{
			return fN9;
		}
		set
		{
			gz(fNW, value, fN_0013);
			SetTechnique();
		}
	}

	/// <summary>
	/// Determines the material roughness used in fresnel sub-surface scattering.
	/// </summary>
	[EditorNumberPadOptions(3, 0.0, 1.0, 0.01)]
	[EditorProperty(true, Description = "Distribution", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 3, ToolTipText = "")]
	public float FresnelMicrofacetDistribution
	{
		get
		{
			return fN_0013;
		}
		set
		{
			gz(fNW, fN9, value);
			SetTechnique();
		}
	}

	/// <summary>
	/// Determines the depth applied to parallax mapping.
	/// </summary>
	[EditorNumberPadOptions(3, 0.0, 100.0, 0.005)]
	[EditorProperty(true, Description = "Parallax Scale", HorizontalAlignment = false, MajorGrouping = 5, MinorGrouping = 2, ToolTipText = "")]
	public float ParallaxScale
	{
		get
		{
			return fNq.X;
		}
		set
		{
			EffectHelper.f_0013(new Vector4(value, fNq.Y, fNq.Z, fNq.W), ref fNq, ref fN_0014);
		}
	}

	/// <summary>
	/// Fine tunes the parallax map offset to avoid watery artifacts.
	/// </summary>
	[EditorNumberPadOptions(3, -100.0, 100.0, 0.005)]
	[EditorProperty(true, Description = "Parallax Offset", HorizontalAlignment = false, MajorGrouping = 5, MinorGrouping = 3, ToolTipText = "")]
	public float ParallaxOffset
	{
		get
		{
			return fNq.Y;
		}
		set
		{
			EffectHelper.f_0013(new Vector4(fNq.X, value, fNq.Z, fNq.W), ref fNq, ref fN_0014);
		}
	}

	/// <summary>
	/// Determines the effect's texture address mode in the U texture-space direction.
	/// </summary>
	[EditorProperty(true, Description = "Addressing U", HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 1, ToolTipText = "")]
	public TextureAddressMode AddressModeU
	{
		[CompilerGenerated]
		get
		{
			return fNA;
		}
		[CompilerGenerated]
		set
		{
			fNA = value;
		}
	}

	/// <summary>
	/// Determines the effect's texture address mode in the V texture-space direction.
	/// </summary>
	[EditorProperty(true, Description = "Addressing V", HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 2, ToolTipText = "")]
	public TextureAddressMode AddressModeV
	{
		[CompilerGenerated]
		get
		{
			return fN0;
		}
		[CompilerGenerated]
		set
		{
			fN0 = value;
		}
	}

	/// <summary>
	/// Determines the effect's texture address mode in the W texture-space direction.
	/// </summary>
	[EditorProperty(true, Description = "Addressing W", HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 3, ToolTipText = "")]
	public TextureAddressMode AddressModeW
	{
		[CompilerGenerated]
		get
		{
			return fNK;
		}
		[CompilerGenerated]
		set
		{
			fNK = value;
		}
	}

	/// <summary>
	/// The transparency style used when rendering the effect.
	/// </summary>
	[EditorProperty(true, Description = "Transparent", HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 11, ControlType = ControlType.CheckBox, ToolTipText = "")]
	[EditorCheckboxOptions(true)]
	public virtual TransparencyMode TransparencyMode
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
				SyncTransparency(changedmode: true);
			}
		}
	}

	/// <summary>
	/// Used with TransparencyMode to determine the effect transparency.
	///   -For Clipped mode this value is a comparison value, where all TransparencyMap
	///    alpha values below this value are *not* rendered.
	/// </summary>
	[EditorNumberPadOptions(3, 0.0, 1.0, 0.005)]
	[EditorProperty(true, Description = "Threshold", HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 12, ToolTipText = "")]
	public virtual float Transparency
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
			SyncTransparency(changedmode: false);
		}
	}

	/// <summary>
	/// The texture map used for transparency (values are pulled from the alpha channel).
	/// </summary>
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

	/// <summary>
	/// Amount material absorbs impact force.
	/// </summary>
	[EditorProperty(true, Description = "Elasticity", HorizontalAlignment = true, MajorGrouping = 8, MinorGrouping = 1, ToolTipText = "")]
	[EditorNumberPadOptions(3, 0.0, 1.0, 0.05)]
	public float Elasticity
	{
		get
		{
			return fNn;
		}
		set
		{
			fNn = MathHelper.Clamp(value, 0f, 1f);
		}
	}

	/// <summary>
	/// Amount material resists objects moving across its surface.
	/// </summary>
	[EditorNumberPadOptions(3, 0.0, 1.0, 0.05)]
	[EditorProperty(true, Description = "Friction", HorizontalAlignment = true, MajorGrouping = 8, MinorGrouping = 2, ToolTipText = "")]
	public float Friction
	{
		get
		{
			return fN_0019;
		}
		set
		{
			fN_0019 = MathHelper.Clamp(value, 0f, 1f);
		}
	}

	/// <summary>
	/// Sets the static lighting applied to the effect during rendering.
	/// </summary>
	/// <param name="lightmap">Light map texture containing static
	/// lighting. If texture is not null the texture will override
	/// the provided composite lighting.</param>
	/// <param name="compositelighting">Composite lighting containing
	/// static lighting.</param>
	public void SetStaticLighting(Texture2D lightmap, ref CompositeLighting compositelighting)
	{
		if (lightmap == null)
		{
			lightmap = fNi;
		}
		if (lightmap != fN_0012)
		{
			_UpdatedByBatch = true;
		}
		int num = ((lightmap != fNi) ? 1 : 0);
		EffectHelper.fO(lightmap, ref fN_0012, ref fNo);
		EffectHelper.f_000F(num, ref fNL, ref fNj);
		if (fNc != null && fNd != null && fNw != null && !fNv.Equals(compositelighting))
		{
			fNc.SetValue(compositelighting.AmbientColor);
			fNd.SetValue(compositelighting.DiffuseColor);
			fNw.SetValue(compositelighting.Direction);
			fNv = compositelighting;
			_UpdatedByBatch = true;
		}
		bool clearambient = fN_0012 != fNi || !fNv.Equals(CompositeLighting.EmptyLighting);
		SyncAmbientLighting(clearambient);
	}

	/// <summary>
	/// Sets all transparency information at once.  Used to improve performance
	/// by avoiding multiple effect technique changes.
	/// </summary>
	/// <param name="mode">The transparency style used when rendering the effect.</param>
	/// <param name="transparency">Used with TransparencyMode to determine the effect transparency.
	///   -For Clipped mode this value is a comparison value, where all TransparencyMap
	///    alpha values below this value are *not* rendered.</param>
	/// <param name="map">The texture map used for transparency (values are pulled from the alpha channel).</param>
	public void SetTransparencyModeAndMap(TransparencyMode mode, float transparency, Texture map)
	{
		bool changedmode = fNa != mode;
		fNa = mode;
		fNg = transparency;
		DiffuseMapTexture = map as Texture2D;
		SyncTransparency(changedmode);
	}

	/// <summary>
	/// Applies the object's transparency information to its effect parameters.
	/// </summary>
	protected virtual void SyncTransparency(bool changedmode)
	{
		float num = fNf;
		num = ((TransparencyMode != TransparencyMode.Clip) ? 0f : Transparency);
		EffectHelper.f_0014(num, ref fNf, ref fN_0003);
	}

	/// <summary>
	/// Clears the effect ambient lighting based on the provided value.
	/// </summary>
	/// <param name="clearambient"></param>
	protected virtual void SyncAmbientLighting(bool clearambient)
	{
	}

	/// <summary>
	/// Applies the provided diffuse information to the object and its effect parameters.
	/// </summary>
	/// <param name="diffusecolor"></param>
	/// <param name="diffusemap"></param>
	/// <param name="normalmap"></param>
	protected virtual void SyncDiffuseAndNormalData(Vector4 diffusecolor, Texture2D diffusemap, Texture2D normalmap)
	{
		_DiffuseColorOriginal = diffusecolor;
		if (diffusemap == null || diffusemap == _DefaultDiffuseMapTexture)
		{
			EffectHelper.fO(_DefaultDiffuseMapTexture, ref _DiffuseMapTexture, ref _DiffuseMapTextureIndirectParam);
			EffectHelper.f_0013(diffusecolor, ref _DiffuseColorCached, ref _DiffuseColorIndirectParam);
		}
		else
		{
			EffectHelper.fO(diffusemap, ref _DiffuseMapTexture, ref _DiffuseMapTextureIndirectParam);
			EffectHelper.f_0013(Vector4.One, ref _DiffuseColorCached, ref _DiffuseColorIndirectParam);
		}
		if (normalmap == null || normalmap == _DefaultNormalMapTexture)
		{
			if (fNh > 0f && fNm > 0f)
			{
				EffectHelper.fO(_DefaultNormalMapTexture, ref _NormalMapTexture, ref _NormalMapTextureIndirectParam);
			}
			else
			{
				EffectHelper.fO(null, ref _NormalMapTexture, ref _NormalMapTextureIndirectParam);
			}
		}
		else
		{
			EffectHelper.fO(normalmap, ref _NormalMapTexture, ref _NormalMapTextureIndirectParam);
		}
	}

	private void gT(float P_0, float P_1)
	{
		if ((fNm != P_0 || fNh != P_1) && fNR != null)
		{
			fNm = P_0;
			fNh = P_1;
			if (fNm <= 0f || fNh <= 0f)
			{
				fNR.SetValue(new Vector4(10000f, 0f, 0f, 0f));
			}
			else
			{
				fNR.SetValue(new Vector4(fNm, fNh, 0f, 0f));
			}
		}
	}

	private void gz(float P_0, float P_1, float P_2)
	{
		if ((fNW != P_0 || fN9 != P_1 || fN_0013 != P_2) && fN_000F != null)
		{
			fNW = P_0;
			fN9 = P_1;
			fN_0013 = P_2;
			fN_000F.SetValue(new Vector4(fNW, fN9, fN_0013, 0f));
		}
	}

	/// <summary>
	/// Sets the effect technique based on its current property values.
	/// </summary>
	protected override void SetTechnique()
	{
		base.fNm.fNB.AccumulationValue++;
		bool flag = DoubleSided && fNB;
		if (_CurrentLight != null)
		{
			bool flag2 = _CurrentLight is AmbientLight;
			bool fillLight = _CurrentLight.FillLight;
			bool flag3 = EffectDetail <= DetailPreference.High && !fillLight;
			bool flag4 = EffectDetail <= DetailPreference.Medium && fNa == TransparencyMode.None;
			bool flag5 = EffectDetail <= DetailPreference.Low && !fillLight;
			if (flag2)
			{
				if (flag4 && _ParallaxMapTexture != null && _NormalMapTexture != null)
				{
					if (_EmissiveMapTexture != null)
					{
						CurrentTechnique = Techniques[j.f.fF(j.f.nNB.Lighting, j.f.nNa.DiffuseParallaxAmbientEmissive, 1, false, false, Skinned, false)];
					}
					else
					{
						CurrentTechnique = Techniques[j.f.fF(j.f.nNB.Lighting, j.f.nNa.DiffuseParallaxAmbient, 1, false, false, Skinned, false)];
					}
				}
				else if (_NormalMapTexture != null)
				{
					if (_EmissiveMapTexture != null)
					{
						CurrentTechnique = Techniques[j.f.fF(j.f.nNB.Lighting, j.f.nNa.DiffuseBumpAmbientEmissive, 1, false, false, Skinned, false)];
					}
					else
					{
						CurrentTechnique = Techniques[j.f.fF(j.f.nNB.Lighting, j.f.nNa.DiffuseBumpAmbient, 1, false, false, Skinned, false)];
					}
				}
				else if (_EmissiveMapTexture != null)
				{
					CurrentTechnique = Techniques[j.f.fF(j.f.nNB.Lighting, j.f.nNa.DiffuseAmbientEmissive, 1, false, false, Skinned, false)];
				}
				else
				{
					CurrentTechnique = Techniques[j.f.fF(j.f.nNB.Lighting, j.f.nNa.DiffuseAmbient, 1, false, false, Skinned, false)];
				}
			}
			else
			{
				j.f.nNa nNa = j.f.nNa.Diffuse;
				bool flag6 = _SpecularColorMapTexture != null && _SpecularColorMapTexture != fN8;
				if (_NormalMapTexture != null)
				{
					if (flag4 && _ParallaxMapTexture != null)
					{
						if (!flag5 || !(fNm > 0f) || !(fNh > 0f))
						{
							nNa = j.f.nNa.DiffuseParallax;
						}
						else if (flag3 && fNW > 0f && fN9 > 0f)
						{
							nNa = ((!flag6) ? j.f.nNa.DiffuseParallaxFresnel : j.f.nNa.DiffuseParallaxFresnelColor);
						}
						else
						{
							nNa = ((!flag6) ? j.f.nNa.DiffuseParallaxSpecular : j.f.nNa.DiffuseParallaxSpecularColor);
						}
					}
					else if (!flag5 || !(fNm > 0f) || !(fNh > 0f))
					{
						nNa = j.f.nNa.DiffuseBump;
					}
					else if (flag3 && fNW > 0f && fN9 > 0f)
					{
						nNa = ((!flag6) ? j.f.nNa.DiffuseBumpFresnel : j.f.nNa.DiffuseBumpFresnelColor);
					}
					else
					{
						nNa = ((!flag6) ? j.f.nNa.DiffuseBumpSpecular : j.f.nNa.DiffuseBumpSpecularColor);
					}
				}
				CurrentTechnique = Techniques[j.f.fF(j.f.nNB.Lighting, nNa, 1, flag, false, Skinned, false)];
			}
		}
		else
		{
			CurrentTechnique = Techniques[j.f.fF(j.f.nNB.Lighting, j.f.nNa.DiffuseAmbientEmissive, 1, false, false, Skinned, false)];
		}
	}

	/// <summary>
	/// Creates a new BaseMaterialEffect instance.
	/// </summary>
	/// <param name="graphicsdevice"></param>
	/// <param name="effectname"></param>
	public BaseMaterialEffect(GraphicsDevice graphicsdevice, string effectname)
		: base(graphicsdevice, effectname)
	{
		B(graphicsdevice, true);
	}

	/// <summary>
	/// Creates a new BaseMaterialEffect instance.
	/// </summary>
	/// <param name="graphicsdevice"></param>
	/// <param name="effectname"></param>
	/// <param name="trackeffect"></param>
	internal BaseMaterialEffect(GraphicsDevice P_0, string P_1, bool P_2)
		: base(P_0, P_1)
	{
		B(P_0, P_2);
	}

	private void B(GraphicsDevice P_0, bool P_1)
	{
		fNB = GraphicsDevice.GraphicsProfile == GraphicsProfile.HiDef;
		fNR = Parameters["_SpecularPower_And_Amount"];
		fN_0003 = Parameters["_TransClipRef"];
		fN_000F = Parameters["_FresnelReflectBias_Offset_Microfacet"];
		fN_0014 = Parameters["_ParallaxScale_And_Offset"];
		_DiffuseColorIndirectParam = Parameters["_DiffuseColor"];
		_DiffuseMapTextureIndirectParam = Parameters["_DiffuseMapTexture"];
		_NormalMapTextureIndirectParam = Parameters["_NormalMapTexture"];
		fN_0017 = Parameters["_EmissiveColor"];
		fNj = Parameters["_LightMapped"];
		fNo = Parameters["_LightMapTexture"];
		fNc = Parameters["_CompositeLightingAmbient"];
		fNd = Parameters["_CompositeLightingDiffuse"];
		fNw = Parameters["_CompositeLightingDirection"];
		fNs = Parameters["_MicrofacetTexture"];
		fN_0011 = Parameters["_EmissiveMapTexture"];
		fNQ = Parameters["_SpecularColorMapTexture"];
		fNG = Parameters["_ParallaxMapTexture"];
		MicrofacetTexture = LightingSystemManager.Instance.a4(P_0);
		_DefaultDiffuseMapTexture = LightingSystemManager.Instance.aP("White");
		_DefaultNormalMapTexture = LightingSystemManager.Instance.aP("Normal");
		fN8 = LightingSystemManager.Instance.aP("White");
		fNi = LightingSystemManager.Instance.aP("Black");
		DiffuseColor = Vector3.One;
		SpecularPower = 4f;
		SpecularAmount = 0.25f;
		FresnelReflectBias = 0f;
		FresnelReflectOffset = 1f;
		FresnelMicrofacetDistribution = 0.4f;
		SpecularColorMapTexture = fN8;
		SetStaticLighting(fNi, ref fNv);
		SetTechnique();
		MaterialName = string.Empty;
		ProjectFile = string.Empty;
		NormalMapFile = string.Empty;
		DiffuseMapFile = string.Empty;
		EmissiveMapFile = string.Empty;
		SpecularColorMapFile = string.Empty;
		ParallaxMapFile = string.Empty;
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
		LightingSystemEditor.OnDisposeResource(this);
	}
}
