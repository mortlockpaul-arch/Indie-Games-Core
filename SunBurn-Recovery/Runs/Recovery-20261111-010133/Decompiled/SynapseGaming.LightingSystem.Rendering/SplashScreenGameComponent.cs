using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Rendering;

public sealed class SplashScreenGameComponent : DrawableGameComponent
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private SplashScreen l8a;

	[DebuggerHidden]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static bool DisplayComplete
	{
		[DebuggerHidden]
		get
		{
			return SplashScreen.DisplayComplete;
		}
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[DebuggerHidden]
	public static bool UserCancelled
	{
		[DebuggerHidden]
		get
		{
			return SplashScreen.UserCancelled;
		}
	}

	[DebuggerHidden]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool ShowDuringDevelopment
	{
		[DebuggerHidden]
		get
		{
			return l8a.ShowDuringDevelopment;
		}
		[DebuggerHidden]
		set
		{
			l8a.ShowDuringDevelopment = value;
		}
	}

	[DebuggerHidden]
	public SplashScreenGameComponent(Game game)
		: base(game)
	{
		DrawOrder = int.MaxValue;
		l8a = new SplashScreen();
	}

	[DebuggerHidden]
	protected sealed override void OnDrawOrderChanged(object sender, EventArgs args)
	{
		if (DrawOrder != int.MaxValue)
		{
			DrawOrder = int.MaxValue;
		}
		base.OnDrawOrderChanged(sender, args);
	}

	[DebuggerHidden]
	protected sealed override void UnloadContent()
	{
		l8a.Unload();
		base.UnloadContent();
	}

	[DebuggerHidden]
	public sealed override void Update(GameTime gameTime)
	{
		l8a.Update(gameTime);
		base.Update(gameTime);
	}

	[DebuggerHidden]
	public sealed override void Draw(GameTime gameTime)
	{
		l8a.Render(gameTime);
		base.Draw(gameTime);
	}
}
