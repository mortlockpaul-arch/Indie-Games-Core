using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class CircleShapeReader : ContentTypeReader<CircleShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override CircleShape Read(ContentReader input, CircleShape existingInstance)
	{
		if (existingInstance == null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(CircleShapeReader.Read(ContentReader, CircleShape));
			}
			existingInstance = new CircleShape();
		}
		existingInstance.Radius = input.ReadSingle();
		return existingInstance;
	}
}
