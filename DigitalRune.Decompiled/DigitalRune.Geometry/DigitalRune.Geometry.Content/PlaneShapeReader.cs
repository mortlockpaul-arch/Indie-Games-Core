using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class PlaneShapeReader : ContentTypeReader<PlaneShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override PlaneShape Read(ContentReader input, PlaneShape existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new PlaneShape();
		}
		existingInstance.Normal = input.ReadRawObject<Vector3F>();
		existingInstance.DistanceFromOrigin = input.ReadSingle();
		return existingInstance;
	}
}
