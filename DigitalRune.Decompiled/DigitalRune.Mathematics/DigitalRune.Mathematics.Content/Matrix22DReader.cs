using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Matrix22DReader : ContentTypeReader<Matrix22D>
{
	protected override Matrix22D Read(ContentReader input, Matrix22D existingInstance)
	{
		double m = input.ReadDouble();
		double m2 = input.ReadDouble();
		double m3 = input.ReadDouble();
		double m4 = input.ReadDouble();
		return new Matrix22D(m, m2, m3, m4);
	}
}
