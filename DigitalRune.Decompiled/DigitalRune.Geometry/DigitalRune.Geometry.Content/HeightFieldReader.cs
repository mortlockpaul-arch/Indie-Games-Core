using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class HeightFieldReader : ContentTypeReader<HeightField>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override HeightField Read(ContentReader input, HeightField existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new HeightField();
		}
		existingInstance.WidthX = input.ReadSingle();
		existingInstance.WidthZ = input.ReadSingle();
		existingInstance.Depth = input.ReadSingle();
		int num = input.ReadInt32();
		int num2 = input.ReadInt32();
		float[,] array = new float[num, num2];
		int num3 = 0;
		while (num3 < num)
		{
			for (int i = 0; i < num2; i++)
			{
				array[num3, i] = input.ReadSingle();
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(HeightFieldReader.Read(ContentReader, HeightField));
				}
				num3++;
				break;
			}
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			existingInstance.Array = array;
			return existingInstance;
		}
	}
}
