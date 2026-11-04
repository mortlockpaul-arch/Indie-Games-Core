using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class LineShapeReader : ContentTypeReader<LineShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override LineShape Read(ContentReader input, LineShape existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new LineShape();
		}
		existingInstance.PointOnLine = input.ReadRawObject<Vector3F>();
		existingInstance.Direction = input.ReadRawObject<Vector3F>();
		return existingInstance;
	}
}
