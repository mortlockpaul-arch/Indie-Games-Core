using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class Vector3DReader : ContentTypeReader<Vector3D>
{
	protected override Vector3D Read(ContentReader input, Vector3D existingInstance)
	{
		double x = input.ReadDouble();
		double y = input.ReadDouble();
		double z = input.ReadDouble();
		return new Vector3D(x, y, z);
	}
}
