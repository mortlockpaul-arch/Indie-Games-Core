using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class RayShapeReader : ContentTypeReader<RayShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override RayShape Read(ContentReader input, RayShape existingInstance)
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
				__ldtoken(RayShapeReader.Read(ContentReader, RayShape));
			}
			existingInstance = new RayShape();
		}
		existingInstance.Origin = input.ReadRawObject<Vector3F>();
		existingInstance.Direction = input.ReadRawObject<Vector3F>();
		existingInstance.Length = input.ReadSingle();
		existingInstance.StopsAtFirstHit = input.ReadBoolean();
		return existingInstance;
	}
}
