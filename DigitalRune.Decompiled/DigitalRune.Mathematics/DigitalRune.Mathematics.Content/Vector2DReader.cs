using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Vector2DReader : ContentTypeReader<Vector2D>
{
	protected override Vector2D Read(ContentReader input, Vector2D existingInstance)
	{
		double x = input.ReadDouble();
		double y = input.ReadDouble();
		return new Vector2D(x, y);
	}
}
