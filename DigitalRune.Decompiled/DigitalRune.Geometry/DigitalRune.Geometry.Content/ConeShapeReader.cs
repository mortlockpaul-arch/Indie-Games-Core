using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class ConeShapeReader : ContentTypeReader<ConeShape>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override ConeShape Read(ContentReader input, ConeShape existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new ConeShape();
		}
		existingInstance.Radius = input.ReadSingle();
		existingInstance.Height = input.ReadSingle();
		return existingInstance;
	}
}
