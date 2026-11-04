using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Vector4DReader : ContentTypeReader<Vector4D>
{
	protected override Vector4D Read(ContentReader input, Vector4D existingInstance)
	{
		double x = input.ReadDouble();
		double y = input.ReadDouble();
		double z = input.ReadDouble();
		double w = input.ReadDouble();
		return new Vector4D(x, y, z, w);
	}
}
