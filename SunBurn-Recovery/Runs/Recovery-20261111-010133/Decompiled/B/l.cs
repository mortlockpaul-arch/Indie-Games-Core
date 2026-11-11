using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Effects;

namespace B
{
	internal class l
	{
		internal const string l8a = "Locale";

		internal const string l8_u0013_ = "EffectFile";

		internal const string l8N = "Technique";

		internal const string l8l = "DepthTechnique";

		internal const string l8L = "GBufferTechnique";

		internal const string l8w = "FinalTechnique";

		internal const string l8J = "ShadowGenerationTechnique";

		internal const string l8B = "Elasticity";

		internal const string l8s = "Friction";

		internal const string l8h = "DoubleSided";

		internal const string l8_u0002_ = "TransparencyMode";

		internal const string l8v = "Transparency";

		internal const string l8Y = "TransparencyThreshold";

		internal const string l8_u0005_ = "TransparencyAmount";

		internal const string l8b = "TransparencyMapParameterName";

		internal const string l8I = "SOFTWARE\\Microsoft\\.NETFramework\\v2.0.50727\\AssemblyFoldersEx\\Synapse Gaming - SunBurn {0} {1}";

		internal const string l85 = "\\Development\\Windows";

		internal const string l8W = "\\ShaderLibrary";

		internal static object f(Type param_0, string param_1, CultureInfo param_2)
		{
			if ((object)param_0 == typeof(string))
			{
				return param_1;
			}
			if ((object)param_0 == typeof(bool))
			{
				return _u0013_l(param_1);
			}
			if ((object)param_0 == typeof(float))
			{
				return _u0013_L(param_1, param_2);
			}
			if ((object)param_0 == typeof(int))
			{
				return (int)_u0013_L(param_1, param_2);
			}
			if ((object)param_0 == typeof(Vector4))
			{
				return _u0013_w(param_1, param_2, Vector4.Zero);
			}
			return null;
		}

		internal static T _u0013_N<T>(string param_0)
		{
			try
			{
				return (T)Enum.Parse(typeof(T), param_0, ignoreCase: true);
			}
			catch
			{
				throw new Exception("Invalid property value '" + param_0 + "'.");
			}
		}

		internal static bool _u0013_l(string param_0)
		{
			return bool.Parse(param_0);
		}

		internal static float _u0013_L(string param_0, CultureInfo param_1)
		{
			return float.Parse(param_0, param_1.NumberFormat);
		}

		internal static Vector4 _u0013_w(string param_0, CultureInfo param_1, Vector4 param_2)
		{
			string[] array = Regex.Split(param_0, " ");
			if (array.Length < 3 || array.Length > 4)
			{
				throw new Exception("Invalid vector data.");
			}
			param_2.X = _u0013_L(array[0], param_1);
			param_2.Y = _u0013_L(array[1], param_1);
			param_2.Z = _u0013_L(array[2], param_1);
			if (array.Length > 3)
			{
				param_2.W = _u0013_L(array[3], param_1);
			}
			return param_2;
		}
	}
	internal class L
	{
		private static Dictionary<Type, Dictionary<string, PropertyInfo>> l8a = new Dictionary<Type, Dictionary<string, PropertyInfo>>(128);

		private static Dictionary<string, PropertyInfo> _u0013_J(Type param_0)
		{
			if (l8a.ContainsKey(param_0))
			{
				return l8a[param_0];
			}
			PropertyInfo[] properties = param_0.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			Dictionary<string, PropertyInfo> dictionary = new Dictionary<string, PropertyInfo>(properties.Length);
			l8a.Add(param_0, dictionary);
			PropertyInfo[] array = properties;
			foreach (PropertyInfo propertyInfo in array)
			{
				if (!(propertyInfo.Name == "UniqueId") && !(propertyInfo.Name == "CollisionMove") && ((object)propertyInfo.DeclaringType != typeof(Effect) || !(propertyInfo.Name == "CurrentTechnique")))
				{
					dictionary.Add(propertyInfo.Name, propertyInfo);
				}
			}
			return dictionary;
		}

		internal static void _u0013_B(object param_0, object param_1)
		{
			Type type = param_0.GetType();
			Type type2 = param_1.GetType();
			Dictionary<string, PropertyInfo> dictionary = _u0013_J(type);
			Dictionary<string, PropertyInfo> dictionary2 = _u0013_J(type2);
			foreach (string key in dictionary.Keys)
			{
				if (!dictionary.ContainsKey(key) || !dictionary2.ContainsKey(key))
				{
					continue;
				}
				PropertyInfo propertyInfo = dictionary[key];
				PropertyInfo propertyInfo2 = dictionary2[key];
				if ((object)propertyInfo.PropertyType == propertyInfo2.PropertyType && propertyInfo2.CanWrite && propertyInfo.CanRead)
				{
					MethodInfo getMethod = propertyInfo.GetGetMethod(nonPublic: true);
					MethodInfo setMethod = propertyInfo2.GetSetMethod(nonPublic: true);
					if (!getMethod.IsPrivate && !getMethod.IsFamily && !setMethod.IsPrivate && !setMethod.IsFamily)
					{
						propertyInfo2.SetValue(param_1, propertyInfo.GetValue(param_0, null), null);
					}
				}
			}
		}
	}
}
namespace b
{
	internal class l
	{
		internal enum w8a
		{
			DeferredDepth,
			DeferredGBuffer,
			DeferredFinal,
			DeferredFinalFog,
			Lighting,
			Ambient,
			Shadow,
			ShadowGen,
			Fog,
			Billboard
		}

		internal enum w8_u0013_
		{
			None,
			Diffuse,
			DiffuseBump,
			DiffuseBumpSpecular,
			DiffuseBumpSpecularColor,
			DiffuseBumpFresnel,
			DiffuseBumpFresnelColor,
			DiffuseParallax,
			DiffuseParallaxSpecular,
			DiffuseParallaxSpecularColor,
			DiffuseParallaxFresnel,
			DiffuseParallaxFresnelColor,
			DiffuseAmbient,
			DiffuseBumpAmbient,
			DiffuseParallaxAmbient,
			DiffuseAmbientEmissive,
			DiffuseBumpAmbientEmissive,
			DiffuseParallaxAmbientEmissive,
			DiffuseParallaxSpecularColorEmissive,
			DiffuseParallaxEmissive,
			DiffuseBumpSpecularColorEmissive,
			DiffuseBumpEmissive,
			Tangent,
			Linear,
			Point,
			Directional,
			Point3,
			Directional3,
			Point4,
			Directional4,
			Count
		}

		private static Dictionary<int, string> l8a = new Dictionary<int, string>(32);

		private static int lY(w8a param_0, w8_u0013_ param_1, int param_2, bool param_3, bool param_4, bool param_5, bool param_6)
		{
			int num = (int)(param_0 + ((int)param_1 << 8));
			num += param_2 << 16;
			if (param_5)
			{
				num += 16777216;
			}
			if (param_3)
			{
				num += 33554432;
			}
			if (param_4)
			{
				num += 67108864;
			}
			if (param_6)
			{
				num += 134217728;
			}
			return num;
		}

		internal static void l_u0005_()
		{
			Dictionary<int, char> dictionary = new Dictionary<int, char>(16);
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 30; j++)
				{
					for (int k = 0; k < 3; k++)
					{
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: false, param_4: false, param_5: false, param_6: false), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: true, param_4: false, param_5: false, param_6: false), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: false, param_4: true, param_5: false, param_6: false), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: true, param_4: true, param_5: false, param_6: false), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: false, param_4: false, param_5: true, param_6: false), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: true, param_4: false, param_5: true, param_6: false), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: false, param_4: true, param_5: true, param_6: false), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: true, param_4: true, param_5: true, param_6: false), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: false, param_4: false, param_5: false, param_6: true), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: true, param_4: false, param_5: false, param_6: true), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: false, param_4: true, param_5: false, param_6: true), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: true, param_4: true, param_5: false, param_6: true), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: false, param_4: false, param_5: true, param_6: true), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: true, param_4: false, param_5: true, param_6: true), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: false, param_4: true, param_5: true, param_6: true), '0');
						dictionary.Add(lY((w8a)i, (w8_u0013_)j, k, param_3: true, param_4: true, param_5: true, param_6: true), '0');
					}
				}
			}
		}

		internal static string lb(w8a param_0, w8_u0013_ param_1, int param_2, bool param_3, bool param_4, bool param_5, bool param_6)
		{
			int key = lY(param_0, param_1, param_2, param_3, param_4, param_5, param_6);
			if (l8a.ContainsKey(key))
			{
				return l8a[key];
			}
			string text = "";
			switch (param_0)
			{
			case w8a.DeferredDepth:
				text = "DeferredDepth_";
				break;
			case w8a.DeferredGBuffer:
				text = "DeferredGBuffer_";
				break;
			case w8a.DeferredFinal:
				text = "DeferredFinal_";
				break;
			case w8a.DeferredFinalFog:
				text = "DeferredFinalFog_";
				break;
			case w8a.Lighting:
				text = "Lighting_";
				break;
			case w8a.Ambient:
				text = "Ambient_";
				break;
			case w8a.Shadow:
				text = "Shadow_";
				break;
			case w8a.ShadowGen:
				text = "ShadowGen_";
				break;
			case w8a.Fog:
				text = "Fog_";
				break;
			case w8a.Billboard:
				text = "Billboard_";
				break;
			}
			switch (param_1)
			{
			case w8_u0013_.Diffuse:
				text += "D_";
				break;
			case w8_u0013_.DiffuseBump:
				text += "DB_";
				break;
			case w8_u0013_.DiffuseBumpSpecular:
				text += "DBS_";
				break;
			case w8_u0013_.DiffuseBumpSpecularColor:
				text += "DBSC_";
				break;
			case w8_u0013_.DiffuseBumpFresnel:
				text += "DBF_";
				break;
			case w8_u0013_.DiffuseBumpFresnelColor:
				text += "DBFC_";
				break;
			case w8_u0013_.DiffuseParallax:
				text += "DP_";
				break;
			case w8_u0013_.DiffuseParallaxSpecular:
				text += "DPS_";
				break;
			case w8_u0013_.DiffuseParallaxSpecularColor:
				text += "DPSC_";
				break;
			case w8_u0013_.DiffuseParallaxFresnel:
				text += "DPF_";
				break;
			case w8_u0013_.DiffuseParallaxFresnelColor:
				text += "DPFC_";
				break;
			case w8_u0013_.DiffuseAmbient:
				text += "DA_";
				break;
			case w8_u0013_.DiffuseBumpAmbient:
				text += "DBA_";
				break;
			case w8_u0013_.DiffuseParallaxAmbient:
				text += "DPA_";
				break;
			case w8_u0013_.DiffuseAmbientEmissive:
				text += "DAG_";
				break;
			case w8_u0013_.DiffuseBumpAmbientEmissive:
				text += "DBAG_";
				break;
			case w8_u0013_.DiffuseParallaxAmbientEmissive:
				text += "DPAG_";
				break;
			case w8_u0013_.DiffuseParallaxSpecularColorEmissive:
				text += "DPSCE_";
				break;
			case w8_u0013_.DiffuseParallaxEmissive:
				text += "DPE_";
				break;
			case w8_u0013_.DiffuseBumpSpecularColorEmissive:
				text += "DBSCE_";
				break;
			case w8_u0013_.DiffuseBumpEmissive:
				text += "DBE_";
				break;
			case w8_u0013_.Tangent:
				text += "Tangent_";
				break;
			case w8_u0013_.Linear:
				text += "Linear_";
				break;
			case w8_u0013_.Point:
				text += "Point_";
				break;
			case w8_u0013_.Point3:
				text += "Point3_";
				break;
			case w8_u0013_.Point4:
				text += "Point4_";
				break;
			case w8_u0013_.Directional:
				text += "Directional_";
				break;
			case w8_u0013_.Directional3:
				text += "Directional3_";
				break;
			case w8_u0013_.Directional4:
				text += "Directional4_";
				break;
			}
			if (param_0 == w8a.Lighting)
			{
				text = text + "L" + param_2 + "_";
			}
			if (param_3)
			{
				text += "Double_";
			}
			if (param_4)
			{
				text += "Transparent_";
			}
			if (param_5)
			{
				text += "Skinned_";
			}
			if (param_6)
			{
				text += "Terrain_";
			}
			text += "Technique";
			l8a.Add(key, text);
			return text;
		}
	}
	internal class L : BaseRenderableEffect
	{
		public new enum w8a
		{
			ConvertData,
			Normal,
			BlendIntensity
		}

		private bool l8a = true;

		private w8a l8_u0013_;

		private Vector2 l8N;

		private Texture2D l8l;

		private Vector4 l8L;

		private Vector4 l8w;

		private Texture2D l8J;

		private float l8B;

		private EffectParameter l8s;

		private EffectParameter l8h;

		private EffectParameter l8_u0002_;

		private EffectParameter l8v;

		private EffectParameter l8Y;

		private EffectParameter l8_u0005_;

		[CompilerGenerated]
		private bool l8b;

		public w8a Mode
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
					SetTechnique();
				}
			}
		}

		public virtual Texture2D SourceTexture
		{
			get
			{
				return l8l;
			}
			set
			{
				EffectHelper.lh(value, ref l8l, ref l8v);
				if (value != null)
				{
					EffectHelper.ls(new Vector2(1f / (float)value.Width, 1f / (float)value.Height), ref l8N, ref l8_u0002_);
				}
			}
		}

		public Texture2D IntensityTexture
		{
			get
			{
				return l8J;
			}
			set
			{
				EffectHelper.lh(value, ref l8J, ref l8h);
			}
		}

		public float IntensityBlend
		{
			get
			{
				return l8B;
			}
			set
			{
				if (l8s != null && value != l8B)
				{
					l8B = value;
					l8s.SetValue(MathHelper.Clamp(l8B, 0f, 1f));
				}
			}
		}

		public float BloomThreshold
		{
			get
			{
				return l8L.Z;
			}
			set
			{
				SetBloomData(l8L.X, l8L.Y, value, l8L.W);
			}
		}

		public float BloomAmount
		{
			get
			{
				return l8L.X;
			}
			set
			{
				SetBloomData(value, l8L.Y, l8L.Z, l8L.W);
			}
		}

		public float ExposureAmount
		{
			get
			{
				return l8w.X;
			}
			set
			{
				SetExposureTransitionData(value, l8w.Y, l8w.Z);
			}
		}

		public float TransitionMaxScale
		{
			get
			{
				return l8w.Y;
			}
			set
			{
				SetExposureTransitionData(l8w.X, value, l8w.Z);
			}
		}

		public float TransitionMinScale
		{
			get
			{
				return l8w.Z;
			}
			set
			{
				SetExposureTransitionData(l8w.X, l8w.Y, value);
			}
		}

		public bool DynamicRangeEnabled
		{
			get
			{
				return l8a;
			}
			set
			{
				l8a = value;
				SetTechnique();
			}
		}

		public override bool DoubleSided
		{
			[CompilerGenerated]
			get
			{
				return l8b;
			}
			[CompilerGenerated]
			set
			{
				l8b = value;
			}
		}

		protected override void SetTechnique()
		{
			if (l8_u0013_ == w8a.ConvertData)
			{
				if (l8a)
				{
					CurrentTechnique = Techniques["HDRDownSampleAndConvertTechnique"];
				}
				else
				{
					CurrentTechnique = Techniques["HDRDownSampleAndConvertBloomOnlyTechnique"];
				}
			}
			else if (l8_u0013_ == w8a.BlendIntensity)
			{
				CurrentTechnique = Techniques["HDRDownSampleAndBlendIntensityTechnique"];
			}
			else
			{
				CurrentTechnique = Techniques["HDRDownSampleTechnique"];
			}
		}

		public L(GraphicsDevice graphicsdevice)
			: base(graphicsdevice, "HighDynamicRange")
		{
			CurrentTechnique = Techniques["HDRDownSampleTechnique"];
			l8_u0002_ = Parameters["_SourceTextureStride"];
			l8v = Parameters["_SourceTexture"];
			l8h = Parameters["_IntensityTexture"];
			l8s = Parameters["_IntensityBlend"];
			l8Y = Parameters["_BloomAmount_None_Threshold_Burn"];
			l8_u0005_ = Parameters["_ExposureAmount_TransitionMax_TransitionMin"];
		}

		protected void SetBloomData(float sharp, float soft, float threshold, float colorburn)
		{
			EffectHelper.lL(new Vector4(sharp, soft, threshold, colorburn), ref l8L, ref l8Y);
		}

		protected void SetExposureTransitionData(float exposure, float maxscale, float minscale)
		{
			EffectHelper.lL(new Vector4(exposure, maxscale, minscale, 0f), ref l8w, ref l8_u0005_);
		}

		protected override Effect Create()
		{
			return new L(GraphicsDevice);
		}
	}
}
