using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class OrthographicViewVolumeReader : ContentTypeReader<OrthographicViewVolume>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override OrthographicViewVolume Read(ContentReader input, OrthographicViewVolume existingInstance)
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
				__ldtoken(OrthographicViewVolumeReader.Read(ContentReader, OrthographicViewVolume));
			}
			existingInstance = new OrthographicViewVolume();
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
