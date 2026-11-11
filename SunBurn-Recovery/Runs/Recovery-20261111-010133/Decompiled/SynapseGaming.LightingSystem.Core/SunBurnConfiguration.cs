using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using w;

namespace SynapseGaming.LightingSystem.Core;

public class SunBurnConfiguration
{
	public class SunBurnPluginConfigurationElement
	{
		[CompilerGenerated]
		private bool l8a;

		[CompilerGenerated]
		private string l8_u0013_;

		[CompilerGenerated]
		private List<string> l8N;

		public bool AddedByPluginManager
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

		public string Name
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

		public List<string> Assemblies
		{
			[CompilerGenerated]
			get
			{
				return l8N;
			}
			[CompilerGenerated]
			private set
			{
				l8N = value;
			}
		}

		public SunBurnPluginConfigurationElement()
		{
			Name = string.Empty;
			Assemblies = new List<string>();
		}

		internal static SunBurnPluginConfigurationElement _u0013__u0014_(w.L param_0)
		{
			SunBurnPluginConfigurationElement sunBurnPluginConfigurationElement = new SunBurnPluginConfigurationElement();
			sunBurnPluginConfigurationElement.Name = _u0013_0(param_0, "name");
			sunBurnPluginConfigurationElement.AddedByPluginManager = _u0013_0(param_0, "addedby").Equals("PluginManager", StringComparison.InvariantCultureIgnoreCase);
			w._u0013_ u0013_ = param_0.SelectNodes("Assemblies/Assembly");
			foreach (w.a item in u0013_)
			{
				sunBurnPluginConfigurationElement.Assemblies.Add(item.InnerText);
			}
			return sunBurnPluginConfigurationElement;
		}
	}

	private static SunBurnConfiguration l8a;

	[CompilerGenerated]
	private string l8_u0013_;

	[CompilerGenerated]
	private List<SunBurnPluginConfigurationElement> l8N;

	public static SunBurnConfiguration Current => l8a;

	public string ServicesPath
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

	public List<SunBurnPluginConfigurationElement> Plugins
	{
		[CompilerGenerated]
		get
		{
			return l8N;
		}
		[CompilerGenerated]
		private set
		{
			l8N = value;
		}
	}

	static SunBurnConfiguration()
	{
		string empty = string.Empty;
		string filename = Path.Combine(empty, "SunBurn.config");
		l8a = Load(filename);
	}

	public SunBurnConfiguration()
	{
		ServicesPath = string.Empty;
		Plugins = new List<SunBurnPluginConfigurationElement>();
	}

	public static SunBurnConfiguration Load(string filename)
	{
		SunBurnConfiguration sunBurnConfiguration = new SunBurnConfiguration();
		Path.GetDirectoryName(filename);
		global::w.w w2 = new global::w.w();
		try
		{
			using Stream stream = TitleContainer.OpenStream(filename);
			w2.Load(stream);
		}
		catch
		{
			return sunBurnConfiguration;
		}
		w._u0013_ u0013_ = w2.DocumentElement.SelectNodes("SunBurn/Plugins/Plugin");
		foreach (w.a item in u0013_)
		{
			if (item is w.L param_)
			{
				sunBurnConfiguration.Plugins.Add(SunBurnPluginConfigurationElement._u0013__u0014_(param_));
			}
		}
		return sunBurnConfiguration;
	}

	private static string _u0013_0(w.L param_0, string param_1)
	{
		w.N attributeNode = param_0.GetAttributeNode(param_1);
		if (attributeNode == null)
		{
			return string.Empty;
		}
		return attributeNode.InnerText;
	}
}
