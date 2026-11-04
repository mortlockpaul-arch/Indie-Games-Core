using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class PerspectiveViewVolumeReader : ContentTypeReader<PerspectiveViewVolume>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override PerspectiveViewVolume Read(ContentReader input, PerspectiveViewVolume existingInstance)
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
				__ldtoken(PerspectiveViewVolumeReader.Read(ContentReader, PerspectiveViewVolume));
			}
			existingInstance = new PerspectiveViewVolume();
		}
		float left = input.ReadSingle();
		float right = input.ReadSingle();
		float bottom = input.ReadSingle();
		float top = input.ReadSingle();
		float near = input.ReadSingle();
		float far = input.ReadSingle();
		existingInstance.Set(left, right, bottom, top, near, far);
		return existingInstance;
	}
}
