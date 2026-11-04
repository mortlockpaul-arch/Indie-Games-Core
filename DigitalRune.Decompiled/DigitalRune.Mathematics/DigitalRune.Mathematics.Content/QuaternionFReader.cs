using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class QuaternionFReader : ContentTypeReader<QuaternionF>
{
	protected override QuaternionF Read(ContentReader input, QuaternionF existingInstance)
	{
		float w = input.ReadSingle();
		float x = input.ReadSingle();
		float y = input.ReadSingle();
		float z = input.ReadSingle();
		return new QuaternionF(w, x, y, z);
	}
}
