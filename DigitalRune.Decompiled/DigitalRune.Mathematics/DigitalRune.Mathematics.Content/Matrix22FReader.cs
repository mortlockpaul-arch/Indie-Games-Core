using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Matrix22FReader : ContentTypeReader<Matrix22F>
{
	protected override Matrix22F Read(ContentReader input, Matrix22F existingInstance)
	{
		float m = input.ReadSingle();
		float m2 = input.ReadSingle();
		float m3 = input.ReadSingle();
		float m4 = input.ReadSingle();
		return new Matrix22F(m, m2, m3, m4);
	}
}
