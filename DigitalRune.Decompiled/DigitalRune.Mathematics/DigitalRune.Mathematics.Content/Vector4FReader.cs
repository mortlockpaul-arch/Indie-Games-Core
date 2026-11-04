using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Vector4FReader : ContentTypeReader<Vector4F>
{
	protected override Vector4F Read(ContentReader input, Vector4F existingInstance)
	{
		float x = input.ReadSingle();
		float y = input.ReadSingle();
		float z = input.ReadSingle();
		float w = input.ReadSingle();
		return new Vector4F(x, y, z, w);
	}
}
