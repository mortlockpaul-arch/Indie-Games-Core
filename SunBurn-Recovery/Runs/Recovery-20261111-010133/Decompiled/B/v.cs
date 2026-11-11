using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace B;

internal class v
{
	private static int l8a;

	private static float l8_u0013_;

	private static double l8N;

	private static StringBuilder l8l = new StringBuilder();

	private void _u0013__u0006_(StringBuilder param_0, int param_1, int param_2)
	{
		if (param_1 <= 0)
		{
			param_0.Append("0");
			return;
		}
		int length = param_0.Length;
		int num = 0;
		while (param_1 > 0)
		{
			int num2 = param_1 / 10;
			switch (param_1 - num2 * 10)
			{
			case 0:
				param_0.Insert(length, "0");
				break;
			case 1:
				param_0.Insert(length, "1");
				break;
			case 2:
				param_0.Insert(length, "2");
				break;
			case 3:
				param_0.Insert(length, "3");
				break;
			case 4:
				param_0.Insert(length, "4");
				break;
			case 5:
				param_0.Insert(length, "5");
				break;
			case 6:
				param_0.Insert(length, "6");
				break;
			case 7:
				param_0.Insert(length, "7");
				break;
			case 8:
				param_0.Insert(length, "8");
				break;
			case 9:
				param_0.Insert(length, "9");
				break;
			}
			num++;
			if (num == param_2 && num != 0 && param_1 > 0)
			{
				param_0.Insert(length, ".");
			}
			param_1 = num2;
		}
		if (num < param_2)
		{
			for (int i = num; i < param_2; i++)
			{
				param_0.Insert(length, "0");
			}
			param_0.Insert(length, ".");
		}
	}

	internal Vector2 S(SpriteBatch param_0, SpriteFont param_1, ref Vector2 param_2, Vector2 param_3, Color param_4)
	{
		param_0.DrawString(param_1, l8l, param_2, param_4, 0f, Vector2.Zero, param_3, SpriteEffects.None, 0f);
		param_2.Y += 14f * param_3.Y;
		return new Vector2((float)l8l.Length * 8f * param_3.X, 14f * param_3.Y);
	}

	internal void _u0013_e(string param_0)
	{
		l8l.Length = 0;
		l8l.Append(param_0);
	}

	internal void _u0013_U(string param_0, int param_1)
	{
		l8l.Length = 0;
		l8l.Append(param_0);
		l8l.Append(": ");
		_u0013__u0006_(l8l, param_1, 0);
	}

	private void _u0013_U(string param_0, float param_1)
	{
		l8l.Length = 0;
		l8l.Append(param_0);
		l8l.Append(": ");
		_u0013__u0006_(l8l, (int)(param_1 * 100f), 2);
	}

	internal void _u0013__u000E_(string param_0, GameTime param_1, bool param_2)
	{
		double totalSeconds = param_1.TotalGameTime.TotalSeconds;
		if (totalSeconds - l8N >= 0.5)
		{
			l8_u0013_ = (float)l8a / (float)(totalSeconds - l8N);
			l8a = 0;
			l8N = totalSeconds;
		}
		_u0013_U(param_0, l8_u0013_);
		if (param_2)
		{
			l8a++;
		}
	}

	public override string ToString()
	{
		return l8l.ToString();
	}
}
