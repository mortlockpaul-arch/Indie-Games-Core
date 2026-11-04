using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class EmptyShapeReader : ContentTypeReader<EmptyShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override EmptyShape Read(ContentReader input, EmptyShape existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new EmptyShape();
		}
		return existingInstance;
	}
}
