using A;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Mathematics.Content;

public class MatrixDReader : ContentTypeReader<MatrixD>
{
	protected override MatrixD Read(ContentReader input, MatrixD existingInstance)
	{
		int num = input.ReadInt32();
		int num2 = input.ReadInt32();
		if (existingInstance == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(MatrixDReader.Read(ContentReader, MatrixD));
			}
			existingInstance = new MatrixD(num, num2);
			goto IL_00aa;
		}
		if (existingInstance.NumberOfRows == num)
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
			if (existingInstance.NumberOfColumns == num2)
			{
				goto IL_00aa;
			}
		}
		string message = string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10037), num, num2, existingInstance.NumberOfRows, existingInstance.NumberOfColumns);
		throw new ContentLoadException(message);
		IL_00aa:
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				existingInstance[i, j] = input.ReadDouble();
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					goto end_IL_00cd;
				}
				continue;
				end_IL_00cd:
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
