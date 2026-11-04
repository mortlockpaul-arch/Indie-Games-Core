using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Tracks per-frame lighting and rendering statistics.
/// </summary>
public class SystemConsole
{
	private struct nNB(string message, float displayseconds)
	{
		public string Message = message;

		public float DisplaySeconds = displayseconds;

		public float RemainingSeconds = displayseconds;

		public Color GetFadeColor(Color initial)
		{
			return initial * MathHelper.Clamp(RemainingSeconds * 1.5f, 0f, 1f);
		}
	}

	private const int fNB = 10;

	private static bool fNa = false;

	private static bool fNg = false;

	private static Texture2D fNf;

	private static List<nNB> fN_0003 = new List<nNB>(32);

	private static Dictionary<string, SystemStatistic> fNn = new Dictionary<string, SystemStatistic>(32);

	private static L._0012 fN_0019 = new L._0012();

	private static Color fNL = new Color(0, 0, 0, 180);

	private static Vector2 fNv = default;

	private static string fN_0012 = "________________";

	private static string fN_0015 = "FrameRate";

	/// <summary>
	/// Dictionary of all statistics.
	/// </summary>
	public static Dictionary<string, SystemStatistic> Statistics => fNn;

	/// <summary>
	/// Gets a statistic by name, creating it if necessary.
	/// </summary>
	/// <param name="name"></param>
	/// <param name="category">Category assign to the statistic if a new statistic object is created.</param>
	/// <returns></returns>
	public static SystemStatistic GetStatistic(string name, SystemStatisticCategory category)
	{
		if (fNn.TryGetValue(name, out var value))
		{
			return value;
		}
		value = new SystemStatistic(name, category);
		fNn.Add(name, value);
		return value;
	}

	/// <summary>
	/// Adds a message to the system console.
	/// </summary>
	/// <param name="message">Message to display.</param>
	/// <param name="displayseconds">Time in seconds the message is displayed.</param>
	public static void AddMessage(string message, int displayseconds)
	{
		fN_0003.Add(new nNB(message, displayseconds));
	}

	/// <summary>
	/// Ends statistic gathering for this frame and resets the AccumulationValue for all statistics.
	/// </summary>
	public static void Apply()
	{
		if (!fNa)
		{
			return;
		}
		foreach (KeyValuePair<string, SystemStatistic> item in fNn)
		{
			item.Value.h();
		}
		fNa = false;
		fNg = false;
	}

	/// <summary>
	/// Renders stats to the screen. This can be slow on some hardware, rendering several
	/// categories when trying to capture the frame rate is not recommended.
	/// </summary>
	/// <param name="device"></param>
	/// <param name="categories">The statistic categories to render.</param>
	/// <param name="showstats">Determines if system stats are rendered.</param>
	/// <param name="showconsole">Determines if system console messages
	/// are rendered. The console is not displayed if no messages exist.</param>
	/// <param name="screenposition">Upper left corner to begin rendering.</param>
	/// <param name="scale">Text scale.</param>
	/// <param name="color">Text color.</param>
	/// <param name="gametime"></param>
	public static void Render(GraphicsDevice device, SystemStatisticCategory categories, bool showstats, bool showconsole, Vector2 screenposition, Vector2 scale, Color color, GameTime gametime)
	{
		if (!showstats && fN_0003.Count <= 0)
		{
			return;
		}
		Vector2 vector = screenposition;
		SpriteBatch spriteBatch = LightingSystemManager.Instance.az(device);
		SpriteFont spriteFont = LightingSystemManager.Instance.aT();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
		if (fNf == null || fNf.IsDisposed)
		{
			fNf = LightingSystemManager.Instance.aP("White");
		}
		spriteBatch.Draw(fNf, new Rectangle((int)screenposition.X - 10, (int)screenposition.Y - 10, (int)fNv.X + 20, (int)fNv.Y + 20), fNL);
		if (showstats)
		{
			fN_0019.aI(fN_0015, gametime, !fNg);
			fNv = fN_0019._5(spriteBatch, spriteFont, ref screenposition, scale, color);
			fNa = true;
			fNg = true;
			foreach (KeyValuePair<string, SystemStatistic> item in fNn)
			{
				if ((item.Value.Category & categories) != 0)
				{
					fN_0019.aS(item.Value.Name, item.Value.Value);
					Vector2 value = fN_0019._5(spriteBatch, spriteFont, ref screenposition, scale, color);
					fNv = Vector2.Max(fNv, value);
				}
			}
		}
		if (showconsole && fN_0003.Count > 0)
		{
			float num = (float)gametime.ElapsedGameTime.TotalSeconds;
			if (showstats)
			{
				fN_0019.a6(fN_0012);
				screenposition.Y += fN_0019._5(spriteBatch, spriteFont, ref screenposition, scale, color).Y;
			}
			for (int k = 0; k < fN_0003.Count; k++)
			{
				nNB value2 = fN_0003[k];
				value2.RemainingSeconds -= num;
				if (value2.RemainingSeconds <= 0f)
				{
					fN_0003.RemoveAt(k);
					k--;
					continue;
				}
				fN_0003[k] = value2;
				fN_0019.a6(value2.Message);
				Vector2 value = fN_0019._5(spriteBatch, spriteFont, ref screenposition, scale, value2.GetFadeColor(color));
				fNv = Vector2.Max(fNv, value);
			}
		}
		fNv.Y = screenposition.Y - vector.Y;
		spriteBatch.End();
	}

	/// <summary>
	/// Returns a string containing the names and values of all requested statistics.
	/// </summary>
	/// <param name="categories">Statistic categories to include.</param>
	/// <param name="gametime">Current game time used in frame rate calculation.</param>
	public static string ToString(SystemStatisticCategory categories, GameTime gametime)
	{
		string empty = string.Empty;
		fN_0019.aI(fN_0015, gametime, !fNg);
		empty = empty + fN_0019.ToString() + "\r\n";
		fNa = true;
		fNg = true;
		foreach (KeyValuePair<string, SystemStatistic> item in fNn)
		{
			if ((item.Value.Category & categories) != 0)
			{
				fN_0019.aS(item.Key, item.Value.Value);
				empty = empty + fN_0019.ToString() + "\r\n";
			}
		}
		return empty;
	}

	/// <summary>
	/// Returns a string containing the names and values of all statistics. Because this method
	/// does not take the current game time the frame rate is likely to be inaccurate.
	/// </summary>
	/// <returns></returns>
	public new static string ToString()
	{
		fNa = true;
		return ToString(SystemStatisticCategory.All, new GameTime());
	}
}
