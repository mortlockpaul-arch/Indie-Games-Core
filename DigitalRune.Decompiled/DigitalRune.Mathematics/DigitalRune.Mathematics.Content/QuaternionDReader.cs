using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class QuaternionDReader : ContentTypeReader<QuaternionD>
{
	protected override QuaternionD Read(ContentReader input, QuaternionD existingInstance)
	{
		double w = input.ReadDouble();
		double x = input.ReadDouble();
		double y = input.ReadDouble();
		double z = input.ReadDouble();
		return new QuaternionD(w, x, y, z);
	}
}
