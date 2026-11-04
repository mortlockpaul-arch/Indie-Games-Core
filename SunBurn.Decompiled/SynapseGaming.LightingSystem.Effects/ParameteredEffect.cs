using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Effects;

/// <summary>
/// Base class for SAS, XSI, and other effects with shader driven properties.
/// </summary>
public abstract class ParameteredEffect : Effect, ITransparentEffect, ICollisionMaterial
{
	internal class nNB
	{
		internal SystemStatistic fNB = SystemConsole.GetStatistic("Effect_TechniqueChanges", SystemStatisticCategory.Rendering);

		internal SystemStatistic fNa = SystemConsole.GetStatistic("Effect_LightSourceChanges", SystemStatisticCategory.Rendering);
	}

	private Texture fNB;

	private bool fNa;

	private TransparencyMode fNg;

	private float fNf = 1f;

	private Dictionary<string, object> fN_0003 = new Dictionary<string, object>();

	private Dictionary<string, Texture> fNn = new Dictionary<string, Texture>();

	internal nNB fN_0019 = new nNB();

	[CompilerGenerated]
	private float fNL;

	[CompilerGenerated]
	private float fNv;

	/// <summary>
	/// Amount material absorbs impact force.
	/// </summary>
	public float Elasticity
	{
		[CompilerGenerated]
		get
		{
			return fNL;
		}
		[CompilerGenerated]
		set
		{
			fNL = value;
		}
	}

	/// <summary>
	/// Amount material resists objects moving across its surface.
	/// </summary>
	public float Friction
	{
		[CompilerGenerated]
		get
		{
			return fNv;
		}
		[CompilerGenerated]
		set
		{
			fNv = value;
		}
	}

	/// <summary>
	/// Surfaces rendered with the effect should be visible from both sides.
	/// </summary>
	public bool DoubleSided
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
		}
	}

	/// <summary>
	/// The transparency style used when rendering the effect.
	/// </summary>
	public TransparencyMode TransparencyMode
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
			SyncTransparency();
		}
	}

	/// <summary>
	/// Used with TransparencyMode to determine the effect transparency.
	///   -For Clipped mode this value is a comparison value, where all TransparencyMap
	///    alpha values below this value are *not* rendered.
	/// </summary>
	public float Transparency
	{
		get
		{
			return fNf;
		}
		set
		{
			fNf = value;
			SyncTransparency();
		}
	}

	/// <summary>
	/// The texture map used for transparency (values are pulled from the alpha channel).
	/// </summary>
	public Texture TransparencyMap
	{
		get
		{
			return fNB;
		}
		set
		{
		}
	}

	internal Dictionary<string, object> Properties => fN_0003;

	internal Dictionary<string, Texture> Textures => fNn;

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
		fNg = mode;
		fNf = transparency;
		fNB = map;
		SyncTransparency();
	}

	/// <summary>
	/// Applies the object's transparency information to its effect parameters.
	/// </summary>
	protected virtual void SyncTransparency()
	{
	}

	/// <summary>
	/// Sets the effect technique by name.
	/// </summary>
	public void SetTechnique(string techniquename)
	{
		fN_0019.fNB.AccumulationValue++;
		EffectTechnique effectTechnique = Techniques[techniquename];
		if (effectTechnique != null)
		{
			CurrentTechnique = effectTechnique;
		}
	}

	/// <summary>
	/// Sets the effect texture by name.
	/// </summary>
	public void SetTexture(string name, Texture texture)
	{
		EffectParameter effectParameter = Parameters[name];
		if (effectParameter != null)
		{
			if (fNn.ContainsKey(name))
			{
				fNn[name] = texture;
			}
			else
			{
				fNn.Add(name, texture);
			}
			if (effectParameter.ParameterType == EffectParameterType.Texture2D && texture is Texture2D)
			{
				effectParameter.SetValue((Texture2D)texture);
			}
			else if (effectParameter.ParameterType == EffectParameterType.Texture3D && texture is Texture3D)
			{
				effectParameter.SetValue((Texture3D)texture);
			}
			else if (effectParameter.ParameterType == EffectParameterType.TextureCube && texture is TextureCube)
			{
				effectParameter.SetValue((TextureCube)texture);
			}
			else if (effectParameter.ParameterType == EffectParameterType.Texture)
			{
				effectParameter.SetValue(texture);
			}
		}
	}

	internal void gS()
	{
		foreach (KeyValuePair<string, object> item in fN_0003)
		{
			if (item.Key == "EffectFile")
			{
				continue;
			}
			if (item.Key == "Technique")
			{
				SetTechnique((string)item.Value);
				continue;
			}
			if (item.Key == "Elasticity")
			{
				Elasticity = (float)item.Value;
				continue;
			}
			if (item.Key == "Friction")
			{
				Friction = (float)item.Value;
				continue;
			}
			if (item.Key == "DoubleSided")
			{
				fNa = (bool)item.Value;
				continue;
			}
			if (item.Key == "TransparencyMode")
			{
				fNg = (TransparencyMode)item.Value;
				continue;
			}
			if (item.Key == "Transparency")
			{
				fNf = (float)item.Value;
				continue;
			}
			if (item.Key == "TransparencyMapParameterName")
			{
				EffectParameter effectParameter = Parameters[(string)item.Value];
				if (effectParameter != null)
				{
					if (effectParameter.ParameterType == EffectParameterType.Texture2D)
					{
						fNB = effectParameter.GetValueTexture2D();
					}
					else if (effectParameter.ParameterType == EffectParameterType.Texture3D)
					{
						fNB = effectParameter.GetValueTexture3D();
					}
				}
				continue;
			}
			EffectParameter effectParameter2 = Parameters[item.Key];
			if (effectParameter2 == null || (effectParameter2.ParameterType != EffectParameterType.Single && effectParameter2.ParameterType != EffectParameterType.Int32))
			{
				continue;
			}
			if (effectParameter2.ParameterType == EffectParameterType.Single)
			{
				if (effectParameter2.ColumnCount == 1)
				{
					effectParameter2.SetValue(gH(item.Value));
				}
				else if (effectParameter2.ColumnCount == 3)
				{
					effectParameter2.SetValue(gy(item.Value));
				}
				else if (effectParameter2.ColumnCount == 4)
				{
					effectParameter2.SetValue(gC(item.Value));
				}
			}
			else if (effectParameter2.ParameterType == EffectParameterType.Int32)
			{
				effectParameter2.SetValue((int)gH(item.Value));
			}
		}
		SyncTransparency();
	}

	internal void gI(Dictionary<string, Texture> P_0)
	{
		foreach (KeyValuePair<string, Texture> item in P_0)
		{
			SetTexture(item.Key, item.Value);
		}
	}

	internal void g_000E(Dictionary<string, object> P_0)
	{
		foreach (KeyValuePair<string, object> item in P_0)
		{
			Type type = item.Value.GetType();
			if ((item.Key == "EffectFile" || item.Key == "Technique" || item.Key == "DepthTechnique" || item.Key == "GBufferTechnique" || item.Key == "FinalTechnique" || item.Key == "ShadowGenerationTechnique" || item.Key == "DoubleSided" || item.Key == "TransparencyMode" || item.Key == "TransparencyMapParameterName") && (object)type == typeof(string))
			{
				object obj = item.Value;
				if (item.Key == "DoubleSided")
				{
					obj = gk(obj);
				}
				else if (item.Key == "TransparencyMode")
				{
					obj = L._0003.a_0015<TransparencyMode>((string)obj);
				}
				if (fN_0003.ContainsKey(item.Key))
				{
					fN_0003[item.Key] = obj;
				}
				else
				{
					fN_0003.Add(item.Key, obj);
				}
			}
			else if ((object)type == typeof(float) && (item.Key == "Transparency" || item.Key == "Elasticity" || item.Key == "Friction"))
			{
				if (fN_0003.ContainsKey(item.Key))
				{
					fN_0003[item.Key] = (float)item.Value;
				}
				else
				{
					fN_0003.Add(item.Key, (float)item.Value);
				}
			}
			else if (fN_0003.ContainsKey(item.Key))
			{
				object obj2 = fN_0003[item.Key];
				Type type2 = obj2.GetType();
				if ((object)type2 == type)
				{
					obj2 = item.Value;
				}
				else if ((object)type2 == typeof(float))
				{
					obj2 = gH(item.Value);
				}
				else if ((object)type2 == typeof(Vector3))
				{
					obj2 = gy(item.Value);
				}
				else if ((object)type2 == typeof(Vector4))
				{
					obj2 = gC(item.Value);
				}
				fN_0003[item.Key] = obj2;
			}
		}
		if (!fN_0003.ContainsKey("Technique"))
		{
			fN_0003.Add("Technique", CurrentTechnique.Name);
		}
		if (!fN_0003.ContainsKey("DepthTechnique"))
		{
			fN_0003.Add("DepthTechnique", gX("DepthTechnique"));
		}
		if (!fN_0003.ContainsKey("GBufferTechnique"))
		{
			fN_0003.Add("GBufferTechnique", gX("GBufferTechnique"));
		}
		if (!fN_0003.ContainsKey("FinalTechnique"))
		{
			fN_0003.Add("FinalTechnique", gX("FinalTechnique"));
		}
		if (!fN_0003.ContainsKey("ShadowGenerationTechnique"))
		{
			fN_0003.Add("ShadowGenerationTechnique", gX("ShadowGenerationTechnique"));
		}
		if (!fN_0003.ContainsKey("Elasticity"))
		{
			fN_0003.Add("Elasticity", 0.25f);
		}
		if (!fN_0003.ContainsKey("Friction"))
		{
			fN_0003.Add("Friction", 0.25f);
		}
		if (!fN_0003.ContainsKey("DoubleSided"))
		{
			fN_0003.Add("DoubleSided", false);
		}
		if (!fN_0003.ContainsKey("TransparencyMode"))
		{
			fN_0003.Add("TransparencyMode", TransparencyMode.None);
		}
		if (!fN_0003.ContainsKey("Transparency"))
		{
			fN_0003.Add("Transparency", 0.5f);
		}
		if (fN_0003.ContainsKey("TransparencyMapParameterName"))
		{
			return;
		}
		string value = "";
		for (int k = 0; k < Parameters.Count; k++)
		{
			EffectParameter effectParameter = Parameters[k];
			if ((effectParameter.ParameterType == EffectParameterType.Texture || effectParameter.ParameterType == EffectParameterType.Texture2D || effectParameter.ParameterType == EffectParameterType.Texture3D) && !string.IsNullOrEmpty(effectParameter.Name))
			{
				value = effectParameter.Name;
				break;
			}
		}
		fN_0003.Add("TransparencyMapParameterName", value);
	}

	private string gX(string P_0)
	{
		if (Techniques[P_0] == null)
		{
			return "";
		}
		return P_0;
	}

	private bool gk(object P_0)
	{
		if (P_0 is bool)
		{
			return (bool)P_0;
		}
		if (P_0 is string)
		{
			try
			{
				return bool.Parse((string)P_0);
			}
			catch
			{
			}
		}
		return false;
	}

	private Vector4 gC(object P_0)
	{
		if (P_0 is Vector4)
		{
			return (Vector4)P_0;
		}
		if (P_0 is Vector3)
		{
			return new Vector4((Vector3)P_0, 1f);
		}
		if (P_0 is float)
		{
			return new Vector4((float)P_0);
		}
		return default;
	}

	private Vector3 gy(object P_0)
	{
		if (P_0 is Vector4 vector)
		{
			return new Vector3(vector.X, vector.Y, vector.Z);
		}
		if (P_0 is Vector3)
		{
			return (Vector3)P_0;
		}
		if (P_0 is float)
		{
			return new Vector3((float)P_0);
		}
		return default;
	}

	private float gH(object P_0)
	{
		if (P_0 is Vector4)
		{
			return ((Vector4)P_0).X;
		}
		if (P_0 is Vector3)
		{
			return ((Vector3)P_0).X;
		}
		if (P_0 is float)
		{
			return (float)P_0;
		}
		return 0f;
	}

	internal ParameteredEffect(GraphicsDevice P_0, byte[] P_1)
		: base(P_0, P_1)
	{
		for (int k = 0; k < Parameters.Count; k++)
		{
			EffectParameter effectParameter = Parameters[k];
			if (fN_0003.ContainsKey(effectParameter.Name) || effectParameter.RowCount > 1 || effectParameter.Elements.Count > 0 || effectParameter.Annotations["SasBindAddress"] != null || !string.IsNullOrEmpty(effectParameter.Semantic))
			{
				continue;
			}
			if (effectParameter.ParameterType == EffectParameterType.Single)
			{
				if (effectParameter.ColumnCount == 0)
				{
					fN_0003.Add(effectParameter.Name, effectParameter.GetValueSingle());
				}
				else if (effectParameter.ColumnCount == 1 && effectParameter.ParameterClass == EffectParameterClass.Scalar)
				{
					fN_0003.Add(effectParameter.Name, effectParameter.GetValueSingle());
				}
				else if (effectParameter.ColumnCount == 3)
				{
					fN_0003.Add(effectParameter.Name, effectParameter.GetValueVector3());
				}
				else if (effectParameter.ColumnCount == 4)
				{
					fN_0003.Add(effectParameter.Name, effectParameter.GetValueVector4());
				}
			}
			else if (effectParameter.ParameterType == EffectParameterType.Int32 && effectParameter.ColumnCount == 0)
			{
				fN_0003.Add(effectParameter.Name, effectParameter.GetValueInt32());
			}
			else if (effectParameter.ParameterType == EffectParameterType.Texture2D && effectParameter.ColumnCount == 0)
			{
				fN_0003.Add(effectParameter.Name, "");
			}
			else if (effectParameter.ParameterType == EffectParameterType.Texture3D && effectParameter.ColumnCount == 0)
			{
				fN_0003.Add(effectParameter.Name, "");
			}
			else if (effectParameter.ParameterType == EffectParameterType.TextureCube && effectParameter.ColumnCount == 0)
			{
				fN_0003.Add(effectParameter.Name, "");
			}
			else if (effectParameter.ParameterType == EffectParameterType.Texture && effectParameter.ColumnCount == 0)
			{
				fN_0003.Add(effectParameter.Name, "");
			}
		}
	}

	/// <summary>
	/// Creates a new effect of the same class type, with the same property values, and using the same effect file as this object.
	/// </summary>
	/// <returns></returns>
	public override Effect Clone()
	{
		Effect effect = Create();
		L.n.ao(this, effect);
		if (effect is ParameteredEffect parameteredEffect)
		{
			parameteredEffect.gI(fNn);
			parameteredEffect.g_000E(fN_0003);
			parameteredEffect.gS();
		}
		return effect;
	}

	/// <summary>
	/// Creates a new empty effect of the same class type and using the same effect file as this object.
	/// </summary>
	/// <returns></returns>
	protected abstract Effect Create();
}
