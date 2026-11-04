using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace j;

[CompilerGenerated]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
internal class v
{
	private static ResourceManager fNB;

	private static CultureInfo fNa;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (object.ReferenceEquals(fNB, null))
			{
				ResourceManager resourceManager = new ResourceManager("SynapseGaming.LightingSystem.Effects.Resources-Xbox360", typeof(v).Assembly);
				fNB = resourceManager;
			}
			return fNB;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
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

	internal static byte[] BillboardEffect
	{
		get
		{
			object obj = ResourceManager.GetObject("BillboardEffect", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] Black
	{
		get
		{
			object obj = ResourceManager.GetObject("Black", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] ConsoleFont
	{
		get
		{
			object obj = ResourceManager.GetObject("ConsoleFont", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] DefaultEffect
	{
		get
		{
			object obj = ResourceManager.GetObject("DefaultEffect", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] DeferredDepthEffect
	{
		get
		{
			object obj = ResourceManager.GetObject("DeferredDepthEffect", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] DeferredLightingEffect
	{
		get
		{
			object obj = ResourceManager.GetObject("DeferredLightingEffect", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] DeferredObjectEffect
	{
		get
		{
			object obj = ResourceManager.GetObject("DeferredObjectEffect", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] DeferredTerrainEffect
	{
		get
		{
			object obj = ResourceManager.GetObject("DeferredTerrainEffect", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] FogEffect
	{
		get
		{
			object obj = ResourceManager.GetObject("FogEffect", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] FullSphere
	{
		get
		{
			object obj = ResourceManager.GetObject("FullSphere", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] HighDynamicRange
	{
		get
		{
			object obj = ResourceManager.GetObject("HighDynamicRange", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] LightingEffect
	{
		get
		{
			object obj = ResourceManager.GetObject("LightingEffect", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] Normal
	{
		get
		{
			object obj = ResourceManager.GetObject("Normal", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] ShadowEffect
	{
		get
		{
			object obj = ResourceManager.GetObject("ShadowEffect", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] SplashScreen
	{
		get
		{
			object obj = ResourceManager.GetObject("SplashScreen", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] TerrainEffect
	{
		get
		{
			object obj = ResourceManager.GetObject("TerrainEffect", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] VolumeLightBeam
	{
		get
		{
			object obj = ResourceManager.GetObject("VolumeLightBeam", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] VolumeLightEffect
	{
		get
		{
			object obj = ResourceManager.GetObject("VolumeLightEffect", fNa);
			return (byte[])obj;
		}
	}

	internal static byte[] White
	{
		get
		{
			object obj = ResourceManager.GetObject("White", fNa);
			return (byte[])obj;
		}
	}

	internal v()
	{
	}
}
