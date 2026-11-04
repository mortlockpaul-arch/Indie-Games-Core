using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Vector2FReader : ContentTypeReader<Vector2F>
{
	protected override Vector2F Read(ContentReader input, Vector2F existingInstance)
	{
		float x = input.ReadSingle();
		float y = input.ReadSingle();
		return new Vector2F(x, y);
	}
}
