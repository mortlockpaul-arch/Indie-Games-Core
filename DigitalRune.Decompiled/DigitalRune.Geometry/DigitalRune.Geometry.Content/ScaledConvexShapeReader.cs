using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class ScaledConvexShapeReader : ContentTypeReader<ScaledConvexShape>
{
	[CompilerGenerated]
	private sealed class c65afcd8424f5d6e27be191f0f71b3ed0
	{
		public ScaledConvexShape c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(ConvexShape cc5fdd9518d3b72c985e2608f434fa386)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.Shape = cc5fdd9518d3b72c985e2608f434fa386;
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override ScaledConvexShape Read(ContentReader input, ScaledConvexShape existingInstance)
	{
		if (existingInstance == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(ScaledConvexShapeReader.Read(ContentReader, ScaledConvexShape));
			}
			existingInstance = new ScaledConvexShape();
		}
		existingInstance.Scale = input.ReadRawObject<Vector3F>();
		input.ReadSharedResource((ConvexShape cc5fdd9518d3b72c985e2608f434fa386) =>
		{
			existingInstance.Shape = cc5fdd9518d3b72c985e2608f434fa386;
		});
		return existingInstance;
	}
}
