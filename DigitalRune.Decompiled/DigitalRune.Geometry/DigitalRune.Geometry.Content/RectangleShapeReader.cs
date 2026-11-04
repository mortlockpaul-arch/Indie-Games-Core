using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class RectangleShapeReader : ContentTypeReader<RectangleShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override RectangleShape Read(ContentReader input, RectangleShape existingInstance)
	{
		if (existingInstance == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(RectangleShapeReader.Read(ContentReader, RectangleShape));
			}
			existingInstance = new RectangleShape();
		}
		existingInstance.WidthX = input.ReadSingle();
		existingInstance.WidthY = input.ReadSingle();
		return existingInstance;
	}
}
