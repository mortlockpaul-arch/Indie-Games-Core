using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class PointShapeReader : ContentTypeReader<PointShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override PointShape Read(ContentReader input, PointShape existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new PointShape();
		}
		existingInstance.Position = input.ReadRawObject<Vector3F>();
		return existingInstance;
	}
}
