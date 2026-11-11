using System;
using System.Diagnostics;
using System.Resources;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using SynapseGaming.LightingSystem.Effects;
using a;
using b;
using y;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Provides and manages engine specific resources
/// such as lighting textures, effects, and helper models.
/// At least one instance must be created before interacting
/// with SunBurn.
/// </summary>
public class SunBurnCoreSystem
{
	private class w8a
	{
		private IServiceProvider l8a;

		private ResourceContentManager l8\u0013;

		public ResourceContentManager EmbeddedResourceManager
		{
			get
			{
				if (l8\u0013 == null)
				{
					l8\u0013 = new ResourceContentManager(l8a, ResourceManager);
				}
				return l8\u0013;
			}
		}

		public w8a(IServiceProvider serviceprovider)
		{
			l8a = serviceprovider;
		}

		public void Unload()
		{
			if (l8\u0013 != null)
			{
				l8\u0013.Dispose();
				l8\u0013 = null;
			}
		}
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static int l8a = 10000;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static SunBurnCoreSystem l8\u0013;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Texture2D l8N;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Texture2D l8l;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private bool l8L = true;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private TextureCube l8w;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private TextureCube l8J;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Texture2D l8B;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private SpriteFont l8s;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private SpriteBatch l8h;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private GraphicsDeviceSupport l8\u0002;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private IServiceProvider l8v;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private IGraphicsDeviceService l8Y;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private w8a l8\u0005;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private ResourceManager l8b;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private B.s l8I;

	/// <summary>
	/// Provides global access to the game's SunBurnCoreSystem. An instance of this class
	/// must be created before calling the property.
	/// </summary>
	[DebuggerHidden]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static SunBurnCoreSystem Instance
	{
		[DebuggerHidden]
		get
		{
			if (l8\u0013 == null)
			{
				throw new ArgumentException("SunBurnCoreSystem unavailable, please create an instance of the manager before using this object.");
			}
			global::a.a.N();
			return l8\u0013;
		}
	}

	/// <summary>
	/// Returns the edition of the loaded SunBurn assembly.
	/// </summary>
	[DebuggerHidden]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static string Edition
	{
		[DebuggerHidden]
		get
		{
			return "Pro";
		}
	}

	/// <summary>
	/// Returns the public key token of the loaded SunBurn assembly.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	public static string PublicKeyToken
	{
		[DebuggerHidden]
		get
		{
			return "c23c60523565dbfd";
		}
	}

	/// <summary>
	/// Returns the version of the loaded SunBurn assembly.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	public static string Version => "2.0.18.7";

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	private static System.Resources.ResourceManager ResourceManager
	{
		[DebuggerHidden]
		get
		{
			return b.s.ResourceManager;
		}
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	internal static int MaxLightsPerGroup
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = num;
		}
	}

	/// <summary>
	/// Determines if SunBurn should throw an exception when the frame buffers exceed the
	/// viewport size. This helps detect performance issues due to mismatched buffer sizes.
	/// </summary>
	[DebuggerHidden]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool DetectOverSizedFrameBuffers
	{
		[DebuggerHidden]
		get
		{
			return l8L;
		}
		[DebuggerHidden]
		set
		{
			l8L = value;
		}
	}

	/// <summary>
	/// Provides a default resource manager for use without access to a scene interface.
	/// </summary>
	[DebuggerHidden]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ResourceManager DefaultResourceManager => l8b;

	/// <summary>
	/// Provides access to the game's XNA services.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	public IServiceProvider Services => l8v;

	/// <summary>
	/// The current GraphicsDeviceManager used by the game.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	public IGraphicsDeviceService GraphicsDeviceManager => l8Y;

	/// <summary>
	/// Creates a new SunBurnCoreSystem instance.
	/// </summary>
	/// <param name="service"></param>
	/// <param name="managerwithactivationfile">Content manager that contains the SunBurn activation file.</param>
	[DebuggerHidden]
	public SunBurnCoreSystem(IServiceProvider service, ContentManager managerwithactivationfile)
	{
		a.N.ActivationPath = managerwithactivationfile.RootDirectory;
		l8\u0013 = this;
		l8v = service;
		l8Y = service.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
		l8\u0005 = new w8a(service);
		l8b = new ResourceManager(null);
		l8I = new B.s();
		l8I.\u0013T();
		global::a.a.N();
	}

	internal void \u0013M(FrameBuffers P_0)
	{
		if (l8L)
		{
			GraphicsDevice graphicsDevice = l8Y.GraphicsDevice;
			Viewport viewport = graphicsDevice.Viewport;
			if (viewport.Width < P_0.Width || viewport.Height < P_0.Height)
			{
				throw new Exception("Supplied frame buffers are too large for final target viewport, this will cause performance issues. Supply properly sized buffers or disable the SunBurnCoreSystem's DetectOverSizedFrameBuffers property to ignore.");
			}
		}
	}

	/// <summary>
	/// Manually loads a SunBurn plugin class and ties the plugin
	/// into SunBurn's scene interface for initialization and unload
	/// scheme for resource cleanup.
	/// </summary>
	/// <typeparam name="T">Plugin class that implements the IPlugin interface.</typeparam>
	public void ManuallyLoadPlugin<T>() where T : IPlugin
	{
		l8I.\u0013\u0014<T>(false);
	}

	public void InitializeSceneInterfaceWithLoadedPlugins(IManagerServiceProvider sceneinterface, bool includeautoloaded)
	{
		l8I.\u0013E(sceneinterface, includeautoloaded);
	}

	/// <summary>
	/// Gets the system's prefered render target usage for the current platform.
	/// </summary>
	/// <returns></returns>
	public RenderTargetUsage GetBestRenderTargetUsage()
	{
		return RenderTargetUsage.PlatformContents;
	}

	internal EffectData \u0013\u0001(string P_0)
	{
		return l8\u0005.EmbeddedResourceManager.Load<EffectData>(P_0);
	}

	internal Model \u0013\u0018(string P_0)
	{
		return l8\u0005.EmbeddedResourceManager.Load<Model>(P_0);
	}

	internal Texture2D \u0013S(string P_0)
	{
		return l8\u0005.EmbeddedResourceManager.Load<Texture2D>(P_0);
	}

	internal Texture2D \u00139()
	{
		if (l8l == null)
		{
			_ = l8Y.GraphicsDevice;
			l8l = \u0013S("SplashScreen");
		}
		return l8l;
	}

	internal TextureCube \u0013G()
	{
		if (l8w == null)
		{
			GraphicsDevice graphicsDevice = l8Y.GraphicsDevice;
			l8w = new TextureCube(graphicsDevice, 1, mipMap: false, SurfaceFormat.Color);
			Color[] array = new Color[1];
			for (int i = 0; i < 6; i++)
			{
				switch (i)
				{
				case 0:
				{
					ref Color reference6 = ref array[0];
					reference6 = new Color(255, 0, 0, 255);
					break;
				}
				case 1:
				{
					ref Color reference5 = ref array[0];
					reference5 = new Color(255, 0, 0, 0);
					break;
				}
				case 2:
				{
					ref Color reference4 = ref array[0];
					reference4 = new Color(0, 255, 0, 255);
					break;
				}
				case 3:
				{
					ref Color reference3 = ref array[0];
					reference3 = new Color(0, 255, 0, 0);
					break;
				}
				case 4:
				{
					ref Color reference2 = ref array[0];
					reference2 = new Color(0, 0, 255, 255);
					break;
				}
				case 5:
				{
					ref Color reference = ref array[0];
					reference = new Color(0, 0, 255, 0);
					break;
				}
				}
				l8w.SetData((CubeMapFace)i, array);
			}
		}
		return l8w;
	}

	internal TextureCube \u0013u()
	{
		if (l8J == null)
		{
			GraphicsDevice graphicsDevice = l8Y.GraphicsDevice;
			l8J = new TextureCube(graphicsDevice, 256, mipMap: false, SurfaceFormat.Color);
			Color[] array = new Color[l8J.Size * l8J.Size];
			int size = l8J.Size;
			float num = 1f / (float)(l8J.Size - 1);
			Vector3[] array2 = new Vector3[6]
			{
				new Vector3(1f, 0f, 0f),
				new Vector3(-1f, 0f, 0f),
				new Vector3(0f, 1f, 0f),
				new Vector3(0f, -1f, 0f),
				new Vector3(0f, 0f, 1f),
				new Vector3(0f, 0f, -1f)
			};
			Vector3[] array3 = new Vector3[6]
			{
				new Vector3(0f, 0f, -1f),
				new Vector3(0f, 0f, 1f),
				new Vector3(1f, 0f, 0f),
				new Vector3(1f, 0f, 0f),
				new Vector3(1f, 0f, 0f),
				new Vector3(-1f, 0f, 0f)
			};
			Vector3[] array4 = new Vector3[6]
			{
				new Vector3(0f, -1f, 0f),
				new Vector3(0f, -1f, 0f),
				new Vector3(0f, 0f, 1f),
				new Vector3(0f, 0f, -1f),
				new Vector3(0f, -1f, 0f),
				new Vector3(0f, -1f, 0f)
			};
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < size; j++)
				{
					for (int k = 0; k < size; k++)
					{
						Vector3 vector = array2[i] + ((float)k * num * 2f - 1f) * array3[i] + ((float)j * num * 2f - 1f) * array4[i];
						vector.Normalize();
						float num2;
						float num3;
						float num4;
						switch (i)
						{
						case 0:
							num2 = 0f - vector.X;
							num3 = vector.Z;
							num4 = vector.Y;
							break;
						case 1:
							num2 = vector.X;
							num3 = 0f - vector.Z;
							num4 = vector.Y;
							break;
						case 2:
							num2 = 0f - vector.Y;
							num3 = vector.X;
							num4 = vector.Z;
							break;
						case 3:
							num2 = vector.Y;
							num3 = 0f - vector.X;
							num4 = vector.Z;
							break;
						case 4:
							num2 = vector.Z;
							num3 = vector.X;
							num4 = 0f - vector.Y;
							break;
						default:
							num2 = vector.Z;
							num3 = vector.X;
							num4 = vector.Y;
							break;
						}
						vector.X = MathHelper.Clamp(num3 / num2 * 0.5f + 0.5f, 0f, 1f);
						vector.Y = MathHelper.Clamp(num4 / num2 * 0.5f + 0.5f, 0f, 1f);
						ref Color reference = ref array[j * size + k];
						reference = new Color((byte)(vector.X * 255f + 0.5f), (byte)(vector.Y * 255f + 0.5f), (byte)(vector.Z * 255f + 0.5f), 0);
					}
				}
				l8J.SetData((CubeMapFace)i, array);
			}
		}
		return l8J;
	}

	internal Texture2D \u0013t()
	{
		if (l8B == null)
		{
			GraphicsDevice graphicsDevice = l8Y.GraphicsDevice;
			l8B = new Texture2D(graphicsDevice, 16, 16, mipMap: false, SurfaceFormat.HalfSingle);
			HalfSingle[] data = new HalfSingle[l8B.Width * l8B.Height];
			l8B.SetData(data);
		}
		return l8B;
	}

	internal Texture2D \u00134()
	{
		if (l8N == null)
		{
			GraphicsDevice graphicsDevice = l8Y.GraphicsDevice;
			l8N = new Texture2D(graphicsDevice, 64, 256, mipMap: true, SurfaceFormat.Color);
			int num = l8N.Width;
			int num2 = l8N.Height;
			for (int i = 0; i < l8N.LevelCount; i++)
			{
				Color[] array = new Color[num * num2];
				float num3 = 1f / (float)(num - 1);
				float num4 = 1f / (float)(num2 - 1);
				int num5 = 0;
				for (int j = 0; j < num2; j++)
				{
					for (int k = 0; k < num; k++)
					{
						float num6 = (float)k * num3;
						float num7 = (float)j * num4;
						float num8 = num6 * num6;
						float num9 = num7 * num7;
						float num10 = (float)Math.PI * 4f * num8 * num9 * num9;
						if (num10 == 0f)
						{
							num10 = 1E-07f;
						}
						num10 = 1f / num10;
						float num11 = num8 * num9;
						if (num11 == 0f)
						{
							num11 = 1E-07f;
						}
						num11 = (num9 - 1f) / num11;
						byte b = (byte)(MathHelper.Clamp(num10 * (float)Math.Exp(num11), 0f, 1f) * 255f);
						ref Color reference = ref array[num5++];
						reference = new Color(b, b, b);
					}
				}
				l8N.SetData(i, null, array, 0, array.Length);
				num = Math.Max(num / 2, 1);
				num2 = Math.Max(num2 / 2, 1);
			}
		}
		return l8N;
	}

	internal SpriteFont \u0013A()
	{
		if (l8s != null)
		{
			return l8s;
		}
		l8s = l8\u0005.EmbeddedResourceManager.Load<SpriteFont>("ConsoleFont");
		l8s.DefaultCharacter = '_';
		return l8s;
	}

	internal SpriteBatch \u0013g()
	{
		if (l8h != null)
		{
			return l8h;
		}
		GraphicsDevice graphicsDevice = l8Y.GraphicsDevice;
		l8h = new SpriteBatch(graphicsDevice);
		return l8h;
	}

	/// <summary>
	/// Returns information on the currently configured and supported graphic device features.
	/// </summary>
	/// <returns></returns>
	public GraphicsDeviceSupport GetGraphicsDeviceSupport()
	{
		if (l8\u0002 == null)
		{
			GraphicsDevice graphicsDevice = l8Y.GraphicsDevice;
			l8\u0002 = new GraphicsDeviceSupport(graphicsDevice);
		}
		return l8\u0002;
	}

	/// <summary>
	/// Unloads all lighting system and device specific data.  Must be called
	/// when the device is reset (during Game.UnloadGraphicsContent()).
	/// </summary>
	public void Unload()
	{
		l8\u0005.Unload();
		l8b.Unload();
		l8I.6();
		l8s = null;
		l8\u0002 = null;
		y.a.w\u0005(ref l8h);
		y.a.w\u0005(ref l8N);
		y.a.w\u0005(ref l8l);
		y.a.w\u0005(ref l8w);
		y.a.w\u0005(ref l8J);
		y.a.w\u0005(ref l8B);
	}
}
