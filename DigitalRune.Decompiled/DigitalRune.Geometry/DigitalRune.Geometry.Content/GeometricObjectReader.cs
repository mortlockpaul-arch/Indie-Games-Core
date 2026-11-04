using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class GeometricObjectReader : ContentTypeReader<GeometricObject>
{
	[CompilerGenerated]
	private sealed class cf2b3b4c009c9f602e22bd444925a9d1a
	{
		public GeometricObject c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(Shape cc5fdd9518d3b72c985e2608f434fa386)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.Shape = cc5fdd9518d3b72c985e2608f434fa386;
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override GeometricObject Read(ContentReader input, GeometricObject existingInstance)
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
				__ldtoken(GeometricObjectReader.Read(ContentReader, GeometricObject));
			}
			existingInstance = new GeometricObject();
		}
		existingInstance.Pose = input.ReadRawObject<Pose>();
		input.ReadSharedResource((Shape cc5fdd9518d3b72c985e2608f434fa386) =>
		{
			existingInstance.Shape = cc5fdd9518d3b72c985e2608f434fa386;
		});
		existingInstance.Scale = input.ReadRawObject<Vector3F>();
		return existingInstance;
	}
}
