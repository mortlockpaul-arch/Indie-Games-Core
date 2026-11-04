using System;
using System.Diagnostics;
using System.Resources;
using _0013;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using SynapseGaming.LightingSystem.Effects;
using j;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Provides and manages lighting system specific resources
/// such as lighting textures, effects, and helper models.
/// At least one instance must be created before interacting
/// with the lighting system.
/// </summary>
public class LightingSystemManager
{
	private class nNB
	{
		private IServiceProvider fNB;

		private ResourceContentManager fNa;

		public ResourceContentManager EmbeddedResourceManager
		{
			get
			{
				if (fNa == null)
				{
					fNa = new ResourceContentManager(fNB, ResourceManager);
				}
				return fNa;
			}
		}

		public nNB(IServiceProvider serviceprovider)
		{
			fNB = serviceprovider;
		}

		public void Unload()
		{
			if (fNa != null)
			{
				fNa.Dispose();
				fNa = null;
			}
		}
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static LightingSystemManager fNB;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Texture2D fNa;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Texture2D fNg;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private bool fNf = true;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private TextureCube fN_0003;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private TextureCube fNn;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Texture2D fN_0019;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private SpriteFont fNL;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private SpriteBatch fNv;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private GraphicsDeviceSupport fN_0012;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private IServiceProvider fN_0015;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private nNB fN8;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private ResourceManager fNi;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	internal static LightingSystemManager Instance
	{
		[DebuggerHidden]
		get
		{
			if (fNB == null)
			{
				throw new ArgumentException("LightingSystemManager unavailable, please create an instance of the manager before using this object.");
			}
			global::B.B.g();
			return fNB;
		}
	}

	/// <summary>
	/// Returns the edition of the loaded SunBurn assembly.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
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
	[DebuggerHidden]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
	public static string Version => "2.0.16.5";

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	private static System.Resources.ResourceManager ResourceManager
	{
		[DebuggerHidden]
		get
		{
			return j.v.ResourceManager;
		}
	}

	/// <summary>
	/// Determines if SunBurn should throw an exception when the frame buffers exceed the
	/// viewport size. This helps detect performance issues due to mismatched buffer sizes.
	/// </summary>
	public bool DetectOverSizedFrameBuffers
	{
		[DebuggerHidden]
		get
		{
			return fNf;
		}
		[DebuggerHidden]
		set
		{
			fNf = value;
		}
	}

	/// <summary>
	/// Provides a default resource manager for use without access to a scene interface.
	/// </summary>
	[DebuggerHidden]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ResourceManager DefaultResourceManager => fNi;

	/// <summary>
	/// Creates a new LightingSystemManager instance.
	/// </summary>
	/// <param name="service"></param>
	/// <param name="managerwithactivationfile">Content manager that contains the SunBurn activation file.</param>
	[DebuggerHidden]
	public LightingSystemManager(IServiceProvider service, ContentManager managerwithactivationfile)
	{
		B.g.ActivationPath = managerwithactivationfile.RootDirectory;
		fNB = this;
		fN_0015 = service;
		fN8 = new nNB(service);
		fNi = new ResourceManager();
		global::B.B.g();
	}

	/// <summary>
	/// Cleans up a deleted LightingSystemManager instance.
	/// </summary>
	[DebuggerHidden]
	~LightingSystemManager()
	{
		Unload();
		if (fNB == this)
		{
			fNB = null;
		}
	}

	internal void aZ(GraphicsDevice P_0, FrameBuffers P_1)
	{
		if (fNf)
		{
			Viewport viewport = P_0.Viewport;
			if (viewport.Width < P_1.Width || viewport.Height < P_1.Height)
			{
				throw new Exception("Supplied frame buffers are too large for final target viewport, this will cause performance issues. Supply properly sized buffers or disable the LightingSystemManager's DetectOverSizedFrameBuffers property to ignore.");
			}
		}
	}

	/// <summary>
	/// Gets the system's prefered render target usage for the current platform.
	/// </summary>
	/// <returns></returns>
	public RenderTargetUsage GetBestRenderTargetUsage()
	{
		return RenderTargetUsage.PlatformContents;
	}

	internal EffectData aD(string P_0)
	{
		return fN8.EmbeddedResourceManager.Load<EffectData>(P_0);
	}

	internal Model aU(string P_0)
	{
		return fN8.EmbeddedResourceManager.Load<Model>(P_0);
	}

	internal Texture2D aP(string P_0)
	{
		return fN8.EmbeddedResourceManager.Load<Texture2D>(P_0);
	}

	internal Texture2D ae(GraphicsDevice P_0)
	{
		if (fNg == null)
		{
			fNg = aP("SplashScreen");
		}
		return fNg;
	}

	internal TextureCube a5(GraphicsDevice P_0)
	{
		if (fN_0003 == null)
		{
			fN_0003 = new TextureCube(P_0, 1, mipMap: false, SurfaceFormat.Color);
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
				fN_0003.SetData((CubeMapFace)i, array);
			}
		}
		return fN_0003;
	}

	internal TextureCube aE(GraphicsDevice P_0)
	{
		if (fNn == null)
		{
			fNn = new TextureCube(P_0, 256, mipMap: false, SurfaceFormat.Color);
			Color[] array = new Color[fNn.Size * fNn.Size];
			int size = fNn.Size;
			float num = 1f / (float)(fNn.Size - 1);
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
				fNn.SetData((CubeMapFace)i, array);
			}
		}
		return fNn;
	}

	internal Texture2D aY(GraphicsDevice P_0)
	{
		if (fN_0019 == null)
		{
			fN_0019 = new Texture2D(P_0, 16, 16, mipMap: false, SurfaceFormat.HalfSingle);
			HalfSingle[] data = new HalfSingle[fN_0019.Width * fN_0019.Height];
			fN_0019.SetData(data);
		}
		return fN_0019;
	}

	internal Texture2D a4(GraphicsDevice P_0)
	{
		if (fNa == null)
		{
			fNa = new Texture2D(P_0, 64, 256, mipMap: true, SurfaceFormat.Color);
			Color[] array = new Color[fNa.Width * fNa.Height];
			float num = 1f / (float)(fNa.Width - 1);
			float num2 = 1f / (float)(fNa.Height - 1);
			int num3 = 0;
			for (int i = 0; i < fNa.Height; i++)
			{
				for (int j = 0; j < fNa.Width; j++)
				{
					float num4 = (float)j * num;
					float num5 = (float)i * num2;
					float num6 = num4 * num4;
					float num7 = num5 * num5;
					float num8 = (float)Math.PI * 4f * num6 * num7 * num7;
					if (num8 == 0f)
					{
						num8 = 1E-07f;
					}
					num8 = 1f / num8;
					float num9 = num6 * num7;
					if (num9 == 0f)
					{
						num9 = 1E-07f;
					}
					num9 = (num7 - 1f) / num9;
					byte b = (byte)(MathHelper.Clamp(num8 * (float)Math.Exp(num9), 0f, 1f) * 255f);
					ref Color reference = ref array[num3++];
					reference = new Color(b, b, b);
				}
			}
			fNa.SetData(array);
		}
		return fNa;
	}

	internal SpriteFont aT()
	{
		if (fNL != null)
		{
			return fNL;
		}
		fNL = fN8.EmbeddedResourceManager.Load<SpriteFont>("ConsoleFont");
		fNL.DefaultCharacter = '_';
		return fNL;
	}

	internal SpriteBatch az(GraphicsDevice P_0)
	{
		if (fNv != null)
		{
			return fNv;
		}
		fNv = new SpriteBatch(P_0);
		return fNv;
	}

	/// <summary>
	/// Returns information on the currently configured and supported graphic device features.
	/// </summary>
	/// <param name="device"></param>
	/// <returns></returns>
	public GraphicsDeviceSupport GetGraphicsDeviceSupport(GraphicsDevice device)
	{
		if (fN_0012 == null)
		{
			fN_0012 = new GraphicsDeviceSupport(device);
		}
		return fN_0012;
	}

	/// <summary>
	/// Unloads all lighting system and device specific data.  Must be called
	/// when the device is reset (during Game.UnloadGraphicsContent()).
	/// </summary>
	public void Unload()
	{
		fN8.Unload();
		fNi.Unload();
		fNL = null;
		fN_0012 = null;
		_0013.B.nu(ref fNv);
		_0013.B.nu(ref fNa);
		_0013.B.nu(ref fNg);
		_0013.B.nu(ref fN_0003);
		_0013.B.nu(ref fNn);
		_0013.B.nu(ref fN_0019);
	}
}
