using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Matrix33FReader : ContentTypeReader<Matrix33F>
{
	protected override Matrix33F Read(ContentReader input, Matrix33F existingInstance)
	{
		float m = input.ReadSingle();
		float m2 = input.ReadSingle();
		float m3 = input.ReadSingle();
		float m4 = input.ReadSingle();
		float m5 = input.ReadSingle();
		float m6 = input.ReadSingle();
		float m7 = input.ReadSingle();
		float m8 = input.ReadSingle();
		float m9 = input.ReadSingle();
		return new Matrix33F(m, m2, m3, m4, m5, m6, m7, m8, m9);
	}
}
