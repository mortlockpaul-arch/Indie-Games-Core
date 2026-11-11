using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SynapseGaming.LightingSystem.Core;
using a;
using y;

namespace SynapseGaming.LightingSystem.Rendering;

public sealed class SplashScreen
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private const double l8a = 5.0;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private const double l8_u0013_ = 1.0;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private const int l8N = 100;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static bool l8l;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static bool l8L;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private bool l8w;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private double l8J;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private int l8B;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private BasicEffect l8s;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private FullFrameQuad l8h;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Vector2 l8_u0002_ = default;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Vector2 l8v = default;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private float l8Y = 1f;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	public static bool DisplayComplete
	{
		[DebuggerHidden]
		get
		{
			return l8l;
		}
	}

	[DebuggerHidden]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static bool UserCancelled
	{
		[DebuggerHidden]
		get
		{
			return l8L;
		}
	}

	[DebuggerHidden]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool ShowDuringDevelopment
	{
		[DebuggerHidden]
		get
		{
			return l8w;
		}
		[DebuggerHidden]
		set
		{
			l8w = value;
		}
	}

	[DebuggerHidden]
	internal static void L_u0015_()
	{
		global::a.a.N();
		if (l8l)
		{
			return;
		}
		throw new Exception("SunBurn splash screen required for rendering, please display splash screen before calling this method.");
	}

	[DebuggerHidden]
	public SplashScreen()
	{
	}

	[DebuggerHidden]
	private void a()
	{
		if (l8s == null)
		{
			GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
			l8s = new BasicEffect(graphicsDevice);
			l8s.DiffuseColor = Vector3.Zero;
			l8s.AmbientLightColor = Vector3.Zero;
			l8s.EmissiveColor = Vector3.Zero;
			l8s.LightingEnabled = false;
			l8s.FogEnabled = false;
			l8s.VertexColorEnabled = false;
			Texture2D texture2D = SunBurnCoreSystem.Instance._u0013_9();
			l8s.TextureEnabled = true;
			l8s.Texture = texture2D;
			l8s.World = Matrix.Identity;
			l8s.View = Matrix.Identity;
			l8s.Projection = Matrix.Identity;
			Vector2 screenmin = -Vector2.One;
			Vector2 one = Vector2.One;
			float num = (float)texture2D.Width / (float)texture2D.Height;
			if (graphicsDevice.Viewport.AspectRatio > num)
			{
				float num2 = (float)graphicsDevice.Viewport.Height * num;
				float num3 = num2 / (float)graphicsDevice.Viewport.Width;
				screenmin.X = 0f - num3;
				one.X = num3;
				l8Y = num2 / 1280f;
			}
			else
			{
				float num4 = (float)graphicsDevice.Viewport.Width / num;
				float num5 = num4 / (float)graphicsDevice.Viewport.Height;
				screenmin.Y = 0f - num5;
				one.Y = num5;
				l8Y = num4 / 720f;
			}
			l8h = new FullFrameQuad(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, screenmin, one);
			l8_u0002_ = one * new Vector2(0.2f, 0.75f);
			Vector2 vector = new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height) * 0.5f;
			l8_u0002_ *= vector;
			l8_u0002_.Y += vector.Y;
			l8v = one * new Vector2(0.2f, 0.707f);
			l8v *= vector;
			l8v.Y += vector.Y;
		}
	}

	[DebuggerHidden]
	public void Unload()
	{
		y.a.w_u0005_(ref l8s);
		y.a.w_u0005_(ref l8h);
	}

	[DebuggerHidden]
	public void Update(GameTime gameTime)
	{
		if (l8l || !(l8J > 0.0) || l8B <= 100)
		{
			return;
		}
		double num = gameTime.TotalGameTime.TotalSeconds - l8J;
		if (num > 5.0)
		{
			l8l = true;
		}
		else if (num > 1.0)
		{
			GamePadState state = GamePad.GetState(PlayerIndex.One);
			KeyboardState state2 = Keyboard.GetState();
			if (state.IsConnected && (state.IsButtonDown(Buttons.A) || state.IsButtonDown(Buttons.B)))
			{
				l8l = true;
			}
			else if (state2.IsKeyDown(Keys.Space) || state2.IsKeyDown(Keys.Enter) || state2.IsKeyDown(Keys.Escape))
			{
				l8l = true;
			}
			if (l8l)
			{
				l8L = true;
			}
		}
	}

	[DebuggerHidden]
	public void Render(GameTime gameTime)
	{
		if (l8s == null)
		{
			a();
		}
		if (l8l)
		{
			return;
		}
		if (!l8w && Debugger.IsAttached)
		{
			l8l = true;
			return;
		}
		if (l8J <= 0.0)
		{
			l8J = gameTime.TotalGameTime.TotalSeconds;
		}
		l8B++;
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		graphicsDevice.RasterizerState = RasterizerState.CullNone;
		graphicsDevice.DepthStencilState = DepthStencilState.None;
		graphicsDevice.BlendState = BlendState.Opaque;
		double num = gameTime.TotalGameTime.TotalSeconds - l8J;
		double num2 = 0.25;
		float num3 = 4f;
		float num4 = MathHelper.Clamp((float)(num - num2) * num3, 0f, 1f);
		float num5 = MathHelper.Clamp((float)(5.0 - (num + num2)) * num3, 0f, 1f);
		float num6 = num4 * num5;
		Vector3 vector = Vector3.One * num6;
		graphicsDevice.Clear(new Color(vector));
		l8s.DiffuseColor = vector;
		l8h.Render(l8s);
		GamePadState state = GamePad.GetState(PlayerIndex.One);
		if (state.Buttons.X == ButtonState.Pressed && state.Buttons.Y == ButtonState.Pressed)
		{
			SpriteFont spriteFont = SunBurnCoreSystem.Instance._u0013_A();
			SpriteBatch spriteBatch = SunBurnCoreSystem.Instance._u0013_g();
			Color color = new Color(Vector3.Zero);
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			spriteBatch.DrawString(spriteFont, "SunBurn " + SunBurnCoreSystem.Edition + " " + SunBurnCoreSystem.Version, l8v, color, 0f, Vector2.Zero, l8Y, SpriteEffects.None, 0f);
			spriteBatch.DrawString(spriteFont, "License Id:" + global::a.a.l8l, l8_u0002_, color, 0f, Vector2.Zero, l8Y, SpriteEffects.None, 0f);
			spriteBatch.End();
		}
	}
}
