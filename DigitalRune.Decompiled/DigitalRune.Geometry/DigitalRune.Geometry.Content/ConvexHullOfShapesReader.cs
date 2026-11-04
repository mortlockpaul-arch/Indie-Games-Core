using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class ConvexHullOfShapesReader : ContentTypeReader<ConvexHullOfShapes>
{
	[CompilerGenerated]
	private sealed class c494ab1b189ac7949b6e5ab01013a2d66
	{
		public ConvexHullOfShapes c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(GeometricObject c812969d803f43545dee254dc6fdae4c9)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.Children.Add(c812969d803f43545dee254dc6fdae4c9);
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override ConvexHullOfShapes Read(ContentReader input, ConvexHullOfShapes existingInstance)
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
				__ldtoken(ConvexHullOfShapesReader.Read(ContentReader, ConvexHullOfShapes));
			}
			existingInstance = new ConvexHullOfShapes();
		}
		else
		{
			existingInstance.Children.Clear();
		}
		int num = input.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			input.ReadSharedResource((GeometricObject c812969d803f43545dee254dc6fdae4c9) =>
			{
				existingInstance.Children.Add(c812969d803f43545dee254dc6fdae4c9);
			});
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			return existingInstance;
		}
	}
}
