using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class PoseReader : ContentTypeReader<Pose>
{
	protected override Pose Read(ContentReader input, Pose existingInstance)
	{
		Vector3F position = input.ReadRawObject<Vector3F>();
		Matrix33F orientation = input.ReadRawObject<Matrix33F>();
		return new Pose(position, orientation);
	}
}
