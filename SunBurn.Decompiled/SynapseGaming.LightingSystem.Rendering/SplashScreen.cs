using System;
using System.Diagnostics;
using _0013;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Displays the SunBurn splash screen. Used when the XNA Game object is not available, such as WinForm applications.
/// </summary>
public sealed class SplashScreen
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private const double fNB = 5.0;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private const double fNa = 1.0;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private const int fNg = 100;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static bool fNf;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static bool fN_0003;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private bool fNn;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private double fN_0019;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private int fNL;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private BasicEffect fNv;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private FullFrameQuad fN_0012;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Vector2 fN_0015 = default;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Vector2 fN8 = default;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private float fNi = 1f;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private IGraphicsDeviceService fN_0005;

	/// <summary>
	/// Used to determine when the SunBurn splash screen is finished displaying
	/// and it's safe to begin game rendering.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	public static bool DisplayComplete
	{
		[DebuggerHidden]
		get
		{
			return fNf;
		}
	}

	/// <summary>
	/// Determines if the splash screen was canceled early by the user.
	/// This can be used to skip later splash screens.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	public static bool UserCancelled
	{
		[DebuggerHidden]
		get
		{
			return fN_0003;
		}
	}

	/// <summary>
	/// Used to enable or disable the SunBurn splash screen during development. Enabling the splash
	/// screen helps when making sure the screen displays properly in released projects.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	public bool ShowDuringDevelopment
	{
		[DebuggerHidden]
		get
		{
			return fNn;
		}
		[DebuggerHidden]
		set
		{
			fNn = value;
		}
	}

	[DebuggerHidden]
	internal static void _0003k()
	{
		global::B.B.g();
		if (fNf)
		{
			return;
		}
		throw new Exception("SunBurn splash screen required for rendering, please display splash screen before calling this method.");
	}

	/// <summary>
	/// Creates a new SplashScreen instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	[DebuggerHidden]
	public SplashScreen(IGraphicsDeviceService graphicsdevicemanager)
	{
		fN_0005 = graphicsdevicemanager;
	}

	[DebuggerHidden]
	private void B()
	{
		if (fNv == null)
		{
			GraphicsDevice graphicsDevice = fN_0005.GraphicsDevice;
			fNv = new BasicEffect(graphicsDevice);
			fNv.DiffuseColor = Vector3.Zero;
			fNv.AmbientLightColor = Vector3.Zero;
			fNv.EmissiveColor = Vector3.Zero;
			fNv.LightingEnabled = false;
			fNv.FogEnabled = false;
			fNv.VertexColorEnabled = false;
			Texture2D texture2D = LightingSystemManager.Instance.ae(graphicsDevice);
			fNv.TextureEnabled = true;
			fNv.Texture = texture2D;
			fNv.World = Matrix.Identity;
			fNv.View = Matrix.Identity;
			fNv.Projection = Matrix.Identity;
			Vector2 screenmin = -Vector2.One;
			Vector2 one = Vector2.One;
			float num = (float)texture2D.Width / (float)texture2D.Height;
			if (graphicsDevice.Viewport.AspectRatio > num)
			{
				float num2 = (float)graphicsDevice.Viewport.Height * num;
				float num3 = num2 / (float)graphicsDevice.Viewport.Width;
				screenmin.X = 0f - num3;
				one.X = num3;
				fNi = num2 / 1280f;
			}
			else
			{
				float num4 = (float)graphicsDevice.Viewport.Width / num;
				float num5 = num4 / (float)graphicsDevice.Viewport.Height;
				screenmin.Y = 0f - num5;
				one.Y = num5;
				fNi = num4 / 720f;
			}
			fN_0012 = new FullFrameQuad(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, screenmin, one);
			fN_0015 = one * new Vector2(0.2f, 0.75f);
			Vector2 vector = new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height) * 0.5f;
			fN_0015 *= vector;
			fN_0015.Y += vector.Y;
			fN8 = one * new Vector2(0.2f, 0.707f);
			fN8 *= vector;
			fN8.Y += vector.Y;
		}
	}

	/// <summary>
	/// Called when graphics resources need to be unloaded.
	/// </summary>
	[DebuggerHidden]
	public void Unload()
	{
		_0013.B.nu(ref fNv);
		_0013.B.nu(ref fN_0012);
	}

	/// <summary>
	/// Called periodically to allow users to click out of the splash screen.
	/// </summary>
	/// <param name="gameTime"></param>
	[DebuggerHidden]
	public void Update(GameTime gameTime)
	{
		if (fNf || !(fN_0019 > 0.0) || fNL <= 100)
		{
			return;
		}
		double num = gameTime.TotalGameTime.TotalSeconds - fN_0019;
		if (num > 5.0)
		{
			fNf = true;
		}
		else if (num > 1.0)
		{
			GamePadState state = GamePad.GetState(PlayerIndex.One);
			KeyboardState state2 = Keyboard.GetState();
			if (state.IsConnected && (state.IsButtonDown(Buttons.A) || state.IsButtonDown(Buttons.B)))
			{
				fNf = true;
			}
			else if (state2.IsKeyDown(Keys.Space) || state2.IsKeyDown(Keys.Enter) || state2.IsKeyDown(Keys.Escape))
			{
				fNf = true;
			}
			if (fNf)
			{
				fN_0003 = true;
			}
		}
	}

	/// <summary>
	/// Renders the SunBurn splash screen (require by the SunBurn license).
	/// </summary>
	/// <param name="gameTime"></param>
	[DebuggerHidden]
	public void Render(GameTime gameTime)
	{
		if (fNv == null)
		{
			B();
		}
		if (fNf)
		{
			return;
		}
		if (!fNn && Debugger.IsAttached)
		{
			fNf = true;
			return;
		}
		if (fN_0019 <= 0.0)
		{
			fN_0019 = gameTime.TotalGameTime.TotalSeconds;
		}
		fNL++;
		GraphicsDevice graphicsDevice = fN_0005.GraphicsDevice;
		graphicsDevice.RasterizerState = RasterizerState.CullNone;
		graphicsDevice.DepthStencilState = DepthStencilState.None;
		graphicsDevice.BlendState = BlendState.Opaque;
		double num = gameTime.TotalGameTime.TotalSeconds - fN_0019;
		double num2 = 0.25;
		float num3 = 4f;
		float num4 = MathHelper.Clamp((float)(num - num2) * num3, 0f, 1f);
		float num5 = MathHelper.Clamp((float)(5.0 - (num + num2)) * num3, 0f, 1f);
		float num6 = num4 * num5;
		Vector3 vector = Vector3.One * num6;
		graphicsDevice.Clear(new Color(vector));
		fNv.DiffuseColor = vector;
		fN_0012.Render(fNv);
		GamePadState state = GamePad.GetState(PlayerIndex.One);
		if (state.Buttons.X == ButtonState.Pressed && state.Buttons.Y == ButtonState.Pressed)
		{
			SpriteFont spriteFont = LightingSystemManager.Instance.aT();
			SpriteBatch spriteBatch = LightingSystemManager.Instance.az(graphicsDevice);
			Color color = new Color(Vector3.Zero);
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			spriteBatch.DrawString(spriteFont, "SunBurn " + LightingSystemManager.Edition + " " + LightingSystemManager.Version, fN8, color, 0f, Vector2.Zero, fNi, SpriteEffects.None, 0f);
			spriteBatch.DrawString(spriteFont, "License Id:" + global::B.B.fN_0003, fN_0015, color, 0f, Vector2.Zero, fNi, SpriteEffects.None, 0f);
			spriteBatch.End();
		}
	}
}
