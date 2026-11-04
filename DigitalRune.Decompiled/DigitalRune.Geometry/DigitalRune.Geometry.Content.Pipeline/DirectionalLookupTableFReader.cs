using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content.Pipeline;

public class DirectionalLookupTableFReader<T> : ContentTypeReader<DirectionalLookupTableF<T>>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override DirectionalLookupTableF<T> Read(ContentReader input, DirectionalLookupTableF<T> existingInstance)
	{
		int num = input.ReadInt32();
		T[,,] array = new T[6, num, num];
		for (int i = 0; i < 6; i++)
		{
			int num2 = 0;
			while (num2 < num)
			{
				for (int j = 0; j < num; j++)
				{
					array[i, num2, j] = input.ReadRawObject<T>();
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(DirectionalLookupTableFReader<>.Read(ContentReader, DirectionalLookupTableF<T>));
					}
					num2++;
					break;
				}
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					goto end_IL_0055;
				}
				continue;
				end_IL_0055:
				break;
			}
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			if (existingInstance == null)
			{
				existingInstance = new DirectionalLookupTableF<T>(array);
			}
			else
			{
				existingInstance.CubeMap = array;
			}
			return existingInstance;
		}
	}
}
