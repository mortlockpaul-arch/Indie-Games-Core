using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Matrix44DReader : ContentTypeReader<Matrix44D>
{
	protected override Matrix44D Read(ContentReader input, Matrix44D existingInstance)
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
		double m10 = input.ReadDouble();
		double m11 = input.ReadDouble();
		double m12 = input.ReadDouble();
		double m13 = input.ReadDouble();
		double m14 = input.ReadDouble();
		double m15 = input.ReadDouble();
		double m16 = input.ReadDouble();
		return new Matrix44D(m, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16);
	}
}
