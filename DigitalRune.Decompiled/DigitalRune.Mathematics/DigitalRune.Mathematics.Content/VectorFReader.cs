using A;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class VectorFReader : ContentTypeReader<VectorF>
{
	protected override VectorF Read(ContentReader input, VectorF existingInstance)
	{
		int num = input.ReadInt32();
		if (existingInstance == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(VectorFReader.Read(ContentReader, VectorF));
			}
			existingInstance = new VectorF(num);
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
					string message = string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), num, existingInstance.NumberOfElements);
					throw new ContentLoadException(message);
				}
				}
			}
		}
		for (int i = 0; i < num; i++)
		{
			existingInstance[i] = input.ReadSingle();
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			return existingInstance;
		}
	}
}
