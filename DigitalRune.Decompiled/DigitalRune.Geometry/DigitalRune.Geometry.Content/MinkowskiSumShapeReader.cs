using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class MinkowskiSumShapeReader : ContentTypeReader<MinkowskiSumShape>
{
	[CompilerGenerated]
	private sealed class cf71bf93b1c77f1a162358bd1743e1d68
	{
		public MinkowskiSumShape c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(GeometricObject c812969d803f43545dee254dc6fdae4c9)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.ObjectA = c812969d803f43545dee254dc6fdae4c9;
		}

		public void c862e05195c4b76d1738c2bd89afc732c(GeometricObject c812969d803f43545dee254dc6fdae4c9)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.ObjectB = c812969d803f43545dee254dc6fdae4c9;
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override MinkowskiSumShape Read(ContentReader input, MinkowskiSumShape existingInstance)
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
				__ldtoken(MinkowskiSumShapeReader.Read(ContentReader, MinkowskiSumShape));
			}
			existingInstance = new MinkowskiSumShape();
		}
		input.ReadSharedResource((GeometricObject c812969d803f43545dee254dc6fdae4c9) =>
		{
			existingInstance.ObjectA = c812969d803f43545dee254dc6fdae4c9;
		});
		input.ReadSharedResource((GeometricObject c812969d803f43545dee254dc6fdae4c9) =>
		{
			existingInstance.ObjectB = c812969d803f43545dee254dc6fdae4c9;
		});
		return existingInstance;
	}
}
