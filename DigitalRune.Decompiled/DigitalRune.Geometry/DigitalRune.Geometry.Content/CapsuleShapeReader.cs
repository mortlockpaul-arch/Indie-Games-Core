using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class CapsuleShapeReader : ContentTypeReader<CapsuleShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override CapsuleShape Read(ContentReader input, CapsuleShape existingInstance)
	{
		if (existingInstance == null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(CapsuleShapeReader.Read(ContentReader, CapsuleShape));
			}
			existingInstance = new CapsuleShape();
		}
		existingInstance.Height = float.MaxValue;
		existingInstance.Radius = input.ReadSingle();
		existingInstance.Height = input.ReadSingle();
		return existingInstance;
	}
}
