using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using SynapseGaming.LightingSystem.Core;

namespace B
{
	internal class s
	{
		private class w8a
		{
			[CompilerGenerated]
			private bool l8a;

			[CompilerGenerated]
			private IPlugin l8_u0013_;

			public bool AutoLoaded
			{
				[CompilerGenerated]
				get
				{
					return l8a;
				}
				[CompilerGenerated]
				private set
				{
					l8a = value;
				}
			}

			public IPlugin Plugin
			{
				[CompilerGenerated]
				get
				{
					return l8_u0013_;
				}
				[CompilerGenerated]
				private set
				{
					l8_u0013_ = value;
				}
			}

			public static w8a Create(IPlugin plugin, bool autoloaded)
			{
				w8a w8a2 = new w8a();
				w8a2.Plugin = plugin;
				w8a2.AutoLoaded = autoloaded;
				return w8a2;
			}
		}

		private TypeDictionary<w8a> l8a = new TypeDictionary<w8a>();

		internal void _u0013_T()
		{
			SunBurnConfiguration current = SunBurnConfiguration.Current;
			foreach (SunBurnConfiguration.SunBurnPluginConfigurationElement plugin in current.Plugins)
			{
				foreach (string assembly2 in plugin.Assemblies)
				{
					Assembly assembly = Assembly.Load(assembly2);
					object[] customAttributes = assembly.GetCustomAttributes(typeof(SunBurnPluginTypeAttribute), inherit: true);
					object[] array = customAttributes;
					for (int i = 0; i < array.Length; i++)
					{
						SunBurnPluginTypeAttribute sunBurnPluginTypeAttribute = (SunBurnPluginTypeAttribute)array[i];
						Type type = assembly.GetType(sunBurnPluginTypeAttribute.FullName);
						if ((object)type == null)
						{
							type = Type.GetType(sunBurnPluginTypeAttribute.AssemblyQualifiedName);
						}
						if ((object)type == null)
						{
							throw new Exception($"Unable to load plugin class '{sunBurnPluginTypeAttribute.AssemblyQualifiedName}'.");
						}
						_u0013__u0014_(type, param_1: true);
					}
				}
			}
		}

		internal void _u0013__u0014_<T>(bool param_0) where T : IPlugin
		{
			_u0013__u0014_(typeof(T), param_0);
		}

		internal void _u0013__u0014_(Type param_0, bool param_1)
		{
			if (l8a.Items.ContainsKey(param_0))
			{
				return;
			}
			if (!typeof(IPlugin).IsAssignableFrom(param_0))
			{
				throw new Exception($"Class '{param_0.Name}' is not a SunBurn plugin.");
			}
			try
			{
				IPlugin plugin = (IPlugin)Activator.CreateInstance(param_0);
				if (plugin == null)
				{
					throw new Exception("Plugin class instance is null.");
				}
				l8a.Add(param_0, w8a.Create(plugin, param_1));
			}
			catch (Exception innerException)
			{
				throw new Exception($"Unable to create plugin '{param_0.Name}'.", innerException);
			}
		}

		internal void _u0013_E(IManagerServiceProvider param_0, bool param_1)
		{
			foreach (KeyValuePair<Type, w8a> item in l8a.Items)
			{
				w8a value = item.Value;
				if (!value.AutoLoaded || param_1)
				{
					value.Plugin.Initialize(param_0);
				}
			}
		}

		internal void _id_6()
		{
			foreach (KeyValuePair<Type, w8a> item in l8a.Items)
			{
				item.Value.Plugin.Unload();
			}
		}
	}
}
namespace b
{
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class s
	{
		private static System.Resources.ResourceManager l8a;

		private static CultureInfo l8_u0013_;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(l8a, null))
				{
					System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("SynapseGaming.LightingSystem.Effects.Resources-Xbox360", typeof(s).Assembly);
					l8a = resourceManager;
				}
				return l8a;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
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

		internal static byte[] BillboardEffect
		{
			get
			{
				object obj = ResourceManager.GetObject("BillboardEffect", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] Black
		{
			get
			{
				object obj = ResourceManager.GetObject("Black", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] ConsoleFont
		{
			get
			{
				object obj = ResourceManager.GetObject("ConsoleFont", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] DefaultEffect
		{
			get
			{
				object obj = ResourceManager.GetObject("DefaultEffect", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] DeferredDepthEffect
		{
			get
			{
				object obj = ResourceManager.GetObject("DeferredDepthEffect", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] DeferredLightingEffect
		{
			get
			{
				object obj = ResourceManager.GetObject("DeferredLightingEffect", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] DeferredObjectEffect
		{
			get
			{
				object obj = ResourceManager.GetObject("DeferredObjectEffect", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] DeferredTerrainEffect
		{
			get
			{
				object obj = ResourceManager.GetObject("DeferredTerrainEffect", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] FogEffect
		{
			get
			{
				object obj = ResourceManager.GetObject("FogEffect", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] FullSphere
		{
			get
			{
				object obj = ResourceManager.GetObject("FullSphere", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] HighDynamicRange
		{
			get
			{
				object obj = ResourceManager.GetObject("HighDynamicRange", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] LightingEffect
		{
			get
			{
				object obj = ResourceManager.GetObject("LightingEffect", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] Normal
		{
			get
			{
				object obj = ResourceManager.GetObject("Normal", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] ShadowEffect
		{
			get
			{
				object obj = ResourceManager.GetObject("ShadowEffect", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] SplashScreen
		{
			get
			{
				object obj = ResourceManager.GetObject("SplashScreen", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] TerrainEffect
		{
			get
			{
				object obj = ResourceManager.GetObject("TerrainEffect", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] VolumeLightBeam
		{
			get
			{
				object obj = ResourceManager.GetObject("VolumeLightBeam", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] VolumeLightEffect
		{
			get
			{
				object obj = ResourceManager.GetObject("VolumeLightEffect", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal static byte[] White
		{
			get
			{
				object obj = ResourceManager.GetObject("White", l8_u0013_);
				return (byte[])obj;
			}
		}

		internal s()
		{
		}
	}
}
