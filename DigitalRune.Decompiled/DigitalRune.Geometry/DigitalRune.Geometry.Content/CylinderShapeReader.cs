using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class CylinderShapeReader : ContentTypeReader<CylinderShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override CylinderShape Read(ContentReader input, CylinderShape existingInstance)
	{
		if (existingInstance == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(CylinderShapeReader.Read(ContentReader, CylinderShape));
			}
			existingInstance = new CylinderShape();
		}
		existingInstance.Radius = input.ReadSingle();
		existingInstance.Height = input.ReadSingle();
		return existingInstance;
	}
}
