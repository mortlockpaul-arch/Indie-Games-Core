using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class ConvexHullOfPointsReader : ContentTypeReader<ConvexHullOfPoints>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override ConvexHullOfPoints Read(ContentReader input, ConvexHullOfPoints existingInstance)
	{
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
				__ldtoken(ConvexHullOfPointsReader.Read(ContentReader, ConvexHullOfPoints));
			}
			existingInstance = new ConvexHullOfPoints();
		}
		else
		{
			existingInstance.Points.Clear();
		}
		int num = input.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			existingInstance.Points.Add(input.ReadRawObject<Vector3F>());
		}
		return existingInstance;
	}
}
