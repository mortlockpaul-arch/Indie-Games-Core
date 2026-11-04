using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class AabbReader : ContentTypeReader<Aabb>
{
	protected override Aabb Read(ContentReader input, Aabb existingInstance)
	{
		Vector3F minimum = input.ReadRawObject<Vector3F>();
		Vector3F maximum = input.ReadRawObject<Vector3F>();
		return new Aabb(minimum, maximum);
	}
}
