using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Vector3FReader : ContentTypeReader<Vector3F>
{
	protected override Vector3F Read(ContentReader input, Vector3F existingInstance)
	{
		float x = input.ReadSingle();
		float y = input.ReadSingle();
		float z = input.ReadSingle();
		return new Vector3F(x, y, z);
	}
}
