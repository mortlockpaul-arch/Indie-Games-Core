using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Matrix33DReader : ContentTypeReader<Matrix33D>
{
	protected override Matrix33D Read(ContentReader input, Matrix33D existingInstance)
	{
		double m = input.ReadDouble();
		double m2 = input.ReadDouble();
		double m3 = input.ReadDouble();
		double m4 = input.ReadDouble();
		double m5 = input.ReadDouble();
		double m6 = input.ReadDouble();
		double m7 = input.ReadDouble();
		double m8 = input.ReadDouble();
		double m9 = input.ReadDouble();
		return new Matrix33D(m, m2, m3, m4, m5, m6, m7, m8, m9);
	}
}
