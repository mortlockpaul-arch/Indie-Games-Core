using System.Collections.Generic;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

public class SystemConsole
{
	private struct w8a(string message, float displayseconds)
	{
		public string Message = message;

		public float DisplaySeconds = displayseconds;

		public float RemainingSeconds = displayseconds;

		public Color GetFadeColor(Color initial)
		{
			return initial * MathHelper.Clamp(RemainingSeconds * 1.5f, 0f, 1f);
		}
	}

	private const int l8a = 10;

	private static bool l8_u0013_ = false;

	private static bool l8N = false;

	private static Texture2D l8l;

	private static List<w8a> l8L = new List<w8a>(32);

	private static Dictionary<string, SystemStatistic> l8w = new Dictionary<string, SystemStatistic>(32);

	private static B.v l8J = new B.v();

	private static Color l8B = new Color(0, 0, 0, 180);

	private static Vector2 l8s = default;

	private static string l8h = "________________";

	private static string l8_u0002_ = "FrameRate";

	public static Dictionary<string, SystemStatistic> Statistics => l8w;

	public static SystemStatistic GetStatistic(string name, SystemStatisticCategory category)
	{
		if (l8w.TryGetValue(name, out var value))
		{
			return value;
		}
		value = new SystemStatistic(name, category);
		l8w.Add(name, value);
		return value;
	}

	public static void AddMessage(string message, int displayseconds)
	{
		l8L.Add(new w8a(message, displayseconds));
	}

	public static void Apply()
	{
		if (!l8_u0013_)
		{
			return;
		}
		foreach (KeyValuePair<string, SystemStatistic> item in l8w)
		{
			item.Value._u0004_();
		}
		l8_u0013_ = false;
		l8N = false;
	}

	public static void Render(SystemStatisticCategory categories, bool showstats, bool showconsole, Vector2 screenposition, Vector2 scale, Color color, GameTime gametime)
	{
		if (!showstats && l8L.Count <= 0)
		{
			return;
		}
		Vector2 vector = screenposition;
		SpriteBatch spriteBatch = SunBurnCoreSystem.Instance._u0013_g();
		SpriteFont param_ = SunBurnCoreSystem.Instance._u0013_A();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
		if (l8l == null || l8l.IsDisposed)
		{
			l8l = SunBurnCoreSystem.Instance._u0013_S("White");
		}
		spriteBatch.Draw(l8l, new Rectangle((int)screenposition.X - 10, (int)screenposition.Y - 10, (int)l8s.X + 20, (int)l8s.Y + 20), l8B);
		if (showstats)
		{
			l8J._u0013__u000E_(l8_u0002_, gametime, !l8N);
			l8s = l8J.S(spriteBatch, param_, ref screenposition, scale, color);
			l8_u0013_ = true;
			l8N = true;
			foreach (KeyValuePair<string, SystemStatistic> item in l8w)
			{
				if ((item.Value.Category & categories) != 0)
				{
					l8J._u0013_U(item.Value.Name, item.Value.Value);
					Vector2 value = l8J.S(spriteBatch, param_, ref screenposition, scale, color);
					l8s = Vector2.Max(l8s, value);
				}
			}
		}
		if (showconsole && l8L.Count > 0)
		{
			float num = (float)gametime.ElapsedGameTime.TotalSeconds;
			if (showstats)
			{
				l8J._u0013_e(l8h);
				screenposition.Y += l8J.S(spriteBatch, param_, ref screenposition, scale, color).Y;
			}
			for (int i = 0; i < l8L.Count; i++)
			{
				w8a value2 = l8L[i];
				value2.RemainingSeconds -= num;
				if (value2.RemainingSeconds <= 0f)
				{
					l8L.RemoveAt(i);
					i--;
					continue;
				}
				l8L[i] = value2;
				l8J._u0013_e(value2.Message);
				Vector2 value = l8J.S(spriteBatch, param_, ref screenposition, scale, value2.GetFadeColor(color));
				l8s = Vector2.Max(l8s, value);
			}
		}
		l8s.Y = screenposition.Y - vector.Y;
		spriteBatch.End();
	}

	public static string ToString(SystemStatisticCategory categories, GameTime gametime)
	{
		string empty = string.Empty;
		l8J._u0013__u000E_(l8_u0002_, gametime, !l8N);
		empty = empty + l8J.ToString() + "\r\n";
		l8_u0013_ = true;
		l8N = true;
		foreach (KeyValuePair<string, SystemStatistic> item in l8w)
		{
			if ((item.Value.Category & categories) != 0)
			{
				l8J._u0013_U(item.Key, item.Value.Value);
				empty = empty + l8J.ToString() + "\r\n";
			}
		}
		return empty;
	}

	public new static string ToString()
	{
		l8_u0013_ = true;
		return ToString(SystemStatisticCategory.All, new GameTime());
	}
}
