using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class SphereShapeReader : ContentTypeReader<SphereShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override SphereShape Read(ContentReader input, SphereShape existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new SphereShape();
		}
		existingInstance.Radius = input.ReadSingle();
		return existingInstance;
	}
}
