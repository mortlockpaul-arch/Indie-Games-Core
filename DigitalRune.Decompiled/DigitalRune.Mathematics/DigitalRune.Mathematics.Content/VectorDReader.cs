using A;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class VectorDReader : ContentTypeReader<VectorD>
{
	protected override VectorD Read(ContentReader input, VectorD existingInstance)
	{
		int num = input.ReadInt32();
		if (existingInstance == null)
		{
			existingInstance = new VectorD(num);
		}
		else if (existingInstance.NumberOfElements != num)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
				{
					if (1 == 0)
					{
						__ldtoken(VectorDReader.Read(ContentReader, VectorD));
					}
					string message = string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4383), num, existingInstance.NumberOfElements);
					throw new ContentLoadException(message);
				}
				}
			}
		}
		for (int i = 0; i < num; i++)
		{
			existingInstance[i] = input.ReadDouble();
		}
		return existingInstance;
	}
}
