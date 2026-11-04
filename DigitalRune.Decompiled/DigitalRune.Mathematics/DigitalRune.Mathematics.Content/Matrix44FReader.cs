using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Matrix44FReader : ContentTypeReader<Matrix44F>
{
	protected override Matrix44F Read(ContentReader input, Matrix44F existingInstance)
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
		float m10 = input.ReadSingle();
		float m11 = input.ReadSingle();
		float m12 = input.ReadSingle();
		float m13 = input.ReadSingle();
		float m14 = input.ReadSingle();
		float m15 = input.ReadSingle();
		float m16 = input.ReadSingle();
		return new Matrix44F(m, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16);
	}
}
