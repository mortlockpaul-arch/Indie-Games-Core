using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class LineSegmentShapeReader : ContentTypeReader<LineSegmentShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override LineSegmentShape Read(ContentReader input, LineSegmentShape existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new LineSegmentShape();
		}
		existingInstance.Start = input.ReadRawObject<Vector3F>();
		existingInstance.End = input.ReadRawObject<Vector3F>();
		return existingInstance;
	}
}
