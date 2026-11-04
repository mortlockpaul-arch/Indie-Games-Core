using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class BoxShapeReader : ContentTypeReader<BoxShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override BoxShape Read(ContentReader input, BoxShape existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new BoxShape();
		}
		existingInstance.WidthX = input.ReadSingle();
		existingInstance.WidthY = input.ReadSingle();
		existingInstance.WidthZ = input.ReadSingle();
		return existingInstance;
	}
}
