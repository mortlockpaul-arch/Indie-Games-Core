using A;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class MatrixFReader : ContentTypeReader<MatrixF>
{
	protected override MatrixF Read(ContentReader input, MatrixF existingInstance)
	{
		int num = input.ReadInt32();
		int num2 = input.ReadInt32();
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
				__ldtoken(MatrixFReader.Read(ContentReader, MatrixF));
			}
			existingInstance = new MatrixF(num, num2);
			goto IL_00bd;
		}
		if (existingInstance.NumberOfRows == num)
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
			if (existingInstance.NumberOfColumns == num2)
			{
				goto IL_00bd;
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		string message = string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4541), num, num2, existingInstance.NumberOfRows, existingInstance.NumberOfColumns);
		throw new ContentLoadException(message);
		IL_00bd:
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				existingInstance[i, j] = input.ReadSingle();
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					goto end_IL_00e0;
				}
				continue;
				end_IL_00e0:
				break;
			}
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
