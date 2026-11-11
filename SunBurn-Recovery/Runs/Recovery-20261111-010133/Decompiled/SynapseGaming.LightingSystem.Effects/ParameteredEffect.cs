using System;
using System.Collections.Generic;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Effects;

public abstract class ParameteredEffect : Effect, ITransparentEffect, ICollisionMaterial
{
	internal class w8a
	{
		internal SystemStatistic l8a = SystemConsole.GetStatistic("Effect_TechniqueChanges", SystemStatisticCategory.Rendering);

		internal SystemStatistic l8_u0013_ = SystemConsole.GetStatistic("Effect_LightSourceChanges", SystemStatisticCategory.Rendering);
	}

	private Texture l8a;

	private int l8_u0013_;

	private float l8N;

	private float l8l;

	private bool l8L;

	private TransparencyMode l8w;

	private float l8J = 1f;

	private Dictionary<string, object> l8B = new Dictionary<string, object>();

	private Dictionary<string, Texture> l8s = new Dictionary<string, Texture>();

	internal w8a l8h = new w8a();

	public float Elasticity
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
			l8_u0013_++;
		}
	}

	public float Friction
	{
		get
		{
			return l8l;
		}
		set
		{
			l8l = value;
			l8_u0013_++;
		}
	}

	public int CollisionId => l8_u0013_;

	public bool DoubleSided
	{
		get
		{
			return l8L;
		}
		set
		{
			l8L = value;
		}
	}

	public TransparencyMode TransparencyMode
	{
		get
		{
			return l8w;
		}
		set
		{
			l8w = value;
			SyncTransparency();
		}
	}

	public float TransparencyThreshold
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = value;
			SyncTransparency();
		}
	}

	public Texture TransparencyMap
	{
		get
		{
			return l8a;
		}
		set
		{
		}
	}

	internal Dictionary<string, object> Properties => l8B;

	internal Dictionary<string, Texture> Textures => l8s;

	public void SetTransparencyModeAndMap(TransparencyMode mode, float threshold, Texture map)
	{
		l8w = mode;
		l8J = threshold;
		l8a = map;
		SyncTransparency();
	}

	protected virtual void SyncTransparency()
	{
	}

	public void SetTechnique(string techniquename)
	{
		l8h.l8a.AccumulationValue++;
		EffectTechnique effectTechnique = Techniques[techniquename];
		if (effectTechnique != null)
		{
			CurrentTechnique = effectTechnique;
		}
	}

	public void SetTexture(string name, Texture texture)
	{
		EffectParameter effectParameter = Parameters[name];
		if (effectParameter != null)
		{
			if (l8s.ContainsKey(name))
			{
				l8s[name] = texture;
			}
			else
			{
				l8s.Add(name, texture);
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

	internal void N_u0012_()
	{
		foreach (KeyValuePair<string, object> item in l8B)
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
				l8L = (bool)item.Value;
				continue;
			}
			if (item.Key == "TransparencyMode")
			{
				l8w = (TransparencyMode)item.Value;
				continue;
			}
			if (item.Key == "Transparency" || item.Key == "TransparencyThreshold")
			{
				l8J = (float)item.Value;
				continue;
			}
			if (item.Key == "TransparencyMapParameterName")
			{
				EffectParameter effectParameter = Parameters[(string)item.Value];
				if (effectParameter != null)
				{
					if (effectParameter.ParameterType == EffectParameterType.Texture2D)
					{
						l8a = effectParameter.GetValueTexture2D();
					}
					else if (effectParameter.ParameterType == EffectParameterType.Texture3D)
					{
						l8a = effectParameter.GetValueTexture3D();
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
					effectParameter2.SetValue(Ni(item.Value));
				}
				else if (effectParameter2.ColumnCount == 3)
				{
					effectParameter2.SetValue(NR(item.Value));
				}
				else if (effectParameter2.ColumnCount == 4)
				{
					effectParameter2.SetValue(N0(item.Value));
				}
			}
			else if (effectParameter2.ParameterType == EffectParameterType.Int32)
			{
				effectParameter2.SetValue((int)Ni(item.Value));
			}
		}
		SyncTransparency();
	}

	internal void NO(Dictionary<string, Texture> param_0)
	{
		foreach (KeyValuePair<string, Texture> item in param_0)
		{
			SetTexture(item.Key, item.Value);
		}
	}

	internal void N_u000F_(Dictionary<string, object> param_0)
	{
		foreach (KeyValuePair<string, object> item in param_0)
		{
			Type type = item.Value.GetType();
			if ((item.Key == "EffectFile" || item.Key == "Technique" || item.Key == "DepthTechnique" || item.Key == "GBufferTechnique" || item.Key == "FinalTechnique" || item.Key == "ShadowGenerationTechnique" || item.Key == "DoubleSided" || item.Key == "TransparencyMode" || item.Key == "TransparencyMapParameterName") && (object)type == typeof(string))
			{
				object obj = item.Value;
				if (item.Key == "DoubleSided")
				{
					obj = Nq(obj);
				}
				else if (item.Key == "TransparencyMode")
				{
					obj = B.l._u0013_N<TransparencyMode>((string)obj);
				}
				if (l8B.ContainsKey(item.Key))
				{
					l8B[item.Key] = obj;
				}
				else
				{
					l8B.Add(item.Key, obj);
				}
			}
			else if ((object)type == typeof(float) && (item.Key == "Transparency" || item.Key == "TransparencyThreshold" || item.Key == "Elasticity" || item.Key == "Friction"))
			{
				if (l8B.ContainsKey(item.Key))
				{
					l8B[item.Key] = (float)item.Value;
				}
				else
				{
					l8B.Add(item.Key, (float)item.Value);
				}
			}
			else if (l8B.ContainsKey(item.Key))
			{
				object obj2 = l8B[item.Key];
				Type type2 = obj2.GetType();
				if ((object)type2 == type)
				{
					obj2 = item.Value;
				}
				else if ((object)type2 == typeof(float))
				{
					obj2 = Ni(item.Value);
				}
				else if ((object)type2 == typeof(Vector3))
				{
					obj2 = NR(item.Value);
				}
				else if ((object)type2 == typeof(Vector4))
				{
					obj2 = N0(item.Value);
				}
				l8B[item.Key] = obj2;
			}
		}
		if (!l8B.ContainsKey("Technique"))
		{
			l8B.Add("Technique", CurrentTechnique.Name);
		}
		if (!l8B.ContainsKey("DepthTechnique"))
		{
			l8B.Add("DepthTechnique", NF("DepthTechnique"));
		}
		if (!l8B.ContainsKey("GBufferTechnique"))
		{
			l8B.Add("GBufferTechnique", NF("GBufferTechnique"));
		}
		if (!l8B.ContainsKey("FinalTechnique"))
		{
			l8B.Add("FinalTechnique", NF("FinalTechnique"));
		}
		if (!l8B.ContainsKey("ShadowGenerationTechnique"))
		{
			l8B.Add("ShadowGenerationTechnique", NF("ShadowGenerationTechnique"));
		}
		if (!l8B.ContainsKey("Elasticity"))
		{
			l8B.Add("Elasticity", 0.25f);
		}
		if (!l8B.ContainsKey("Friction"))
		{
			l8B.Add("Friction", 0.25f);
		}
		if (!l8B.ContainsKey("DoubleSided"))
		{
			l8B.Add("DoubleSided", false);
		}
		if (!l8B.ContainsKey("TransparencyMode"))
		{
			l8B.Add("TransparencyMode", TransparencyMode.None);
		}
		if (!l8B.ContainsKey("Transparency") && !l8B.ContainsKey("TransparencyThreshold"))
		{
			l8B.Add("TransparencyThreshold", 0.5f);
		}
		if (l8B.ContainsKey("TransparencyMapParameterName"))
		{
			return;
		}
		string value = "";
		for (int i = 0; i < Parameters.Count; i++)
		{
			EffectParameter effectParameter = Parameters[i];
			if ((effectParameter.ParameterType == EffectParameterType.Texture || effectParameter.ParameterType == EffectParameterType.Texture2D || effectParameter.ParameterType == EffectParameterType.Texture3D) && !string.IsNullOrEmpty(effectParameter.Name))
			{
				value = effectParameter.Name;
				break;
			}
		}
		l8B.Add("TransparencyMapParameterName", value);
	}

	private string NF(string param_0)
	{
		if (Techniques[param_0] == null)
		{
			return "";
		}
		return param_0;
	}

	private bool Nq(object param_0)
	{
		if (param_0 is bool)
		{
			return (bool)param_0;
		}
		if (param_0 is string)
		{
			try
			{
				return bool.Parse((string)param_0);
			}
			catch
			{
			}
		}
		return false;
	}

	private Vector4 N0(object param_0)
	{
		if (param_0 is Vector4)
		{
			return (Vector4)param_0;
		}
		if (param_0 is Vector3)
		{
			return new Vector4((Vector3)param_0, 1f);
		}
		if (param_0 is float)
		{
			return new Vector4((float)param_0);
		}
		return default;
	}

	private Vector3 NR(object param_0)
	{
		if (param_0 is Vector4 vector)
		{
			return new Vector3(vector.X, vector.Y, vector.Z);
		}
		if (param_0 is Vector3)
		{
			return (Vector3)param_0;
		}
		if (param_0 is float)
		{
			return new Vector3((float)param_0);
		}
		return default;
	}

	private float Ni(object param_0)
	{
		if (param_0 is Vector4)
		{
			return ((Vector4)param_0).X;
		}
		if (param_0 is Vector3)
		{
			return ((Vector3)param_0).X;
		}
		if (param_0 is float)
		{
			return (float)param_0;
		}
		return 0f;
	}

	internal ParameteredEffect(GraphicsDevice param_0, byte[] param_1)
		: base(param_0, param_1)
	{
		for (int i = 0; i < Parameters.Count; i++)
		{
			EffectParameter effectParameter = Parameters[i];
			if (l8B.ContainsKey(effectParameter.Name) || effectParameter.RowCount > 1 || effectParameter.Elements.Count > 0 || effectParameter.Annotations["SasBindAddress"] != null || !string.IsNullOrEmpty(effectParameter.Semantic))
			{
				continue;
			}
			if (effectParameter.ParameterType == EffectParameterType.Single)
			{
				if (effectParameter.ColumnCount == 0)
				{
					l8B.Add(effectParameter.Name, effectParameter.GetValueSingle());
				}
				else if (effectParameter.ColumnCount == 1 && effectParameter.ParameterClass == EffectParameterClass.Scalar)
				{
					l8B.Add(effectParameter.Name, effectParameter.GetValueSingle());
				}
				else if (effectParameter.ColumnCount == 3)
				{
					l8B.Add(effectParameter.Name, effectParameter.GetValueVector3());
				}
				else if (effectParameter.ColumnCount == 4)
				{
					l8B.Add(effectParameter.Name, effectParameter.GetValueVector4());
				}
			}
			else if (effectParameter.ParameterType == EffectParameterType.Int32 && effectParameter.ColumnCount == 0)
			{
				l8B.Add(effectParameter.Name, effectParameter.GetValueInt32());
			}
			else if (effectParameter.ParameterType == EffectParameterType.Texture2D && effectParameter.ColumnCount == 0)
			{
				l8B.Add(effectParameter.Name, "");
			}
			else if (effectParameter.ParameterType == EffectParameterType.Texture3D && effectParameter.ColumnCount == 0)
			{
				l8B.Add(effectParameter.Name, "");
			}
			else if (effectParameter.ParameterType == EffectParameterType.TextureCube && effectParameter.ColumnCount == 0)
			{
				l8B.Add(effectParameter.Name, "");
			}
			else if (effectParameter.ParameterType == EffectParameterType.Texture && effectParameter.ColumnCount == 0)
			{
				l8B.Add(effectParameter.Name, "");
			}
		}
	}

	public override Effect Clone()
	{
		Effect effect = Create();
		B.L._u0013_B(this, effect);
		if (effect is ParameteredEffect parameteredEffect)
		{
			parameteredEffect.NO(l8s);
			parameteredEffect.N_u000F_(l8B);
			parameteredEffect.N_u0012_();
		}
		return effect;
	}

	protected abstract Effect Create();
}
