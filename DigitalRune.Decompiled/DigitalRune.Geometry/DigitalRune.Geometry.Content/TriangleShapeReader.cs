using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class TriangleShapeReader : ContentTypeReader<TriangleShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override TriangleShape Read(ContentReader input, TriangleShape existingInstance)
	{
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
				__ldtoken(TriangleShapeReader.Read(ContentReader, TriangleShape));
			}
			existingInstance = new TriangleShape();
		}
		existingInstance.Vertex0 = input.ReadRawObject<Vector3F>();
		existingInstance.Vertex1 = input.ReadRawObject<Vector3F>();
		existingInstance.Vertex2 = input.ReadRawObject<Vector3F>();
		return existingInstance;
	}
}
