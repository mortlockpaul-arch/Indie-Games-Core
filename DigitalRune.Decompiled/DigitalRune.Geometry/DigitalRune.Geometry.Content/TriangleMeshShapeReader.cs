using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Geometry.Partitioning;
using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class TriangleMeshShapeReader : ContentTypeReader<TriangleMeshShape>
{
	[CompilerGenerated]
	private sealed class c46cb311a163625b4f272b6ab7a9bf446
	{
		public ISpatialPartition<int> c828d0c1f4103275cf37fe54b9af3f709;

		public TriangleMeshShape c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(ITriangleMesh cea4e848206cd8826d44040bad21efb96)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.cf291ec93e82da38f9e1814873cf030de(cea4e848206cd8826d44040bad21efb96);
			c1864f4cd67afc8f8420dcfd541bcf756.c56d88ced32e38076203610b9aa5a84d4(c828d0c1f4103275cf37fe54b9af3f709);
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override TriangleMeshShape Read(ContentReader input, TriangleMeshShape existingInstance)
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
				__ldtoken(TriangleMeshShapeReader.Read(ContentReader, TriangleMeshShape));
			}
			existingInstance = new TriangleMeshShape();
		}
		existingInstance._aabbLocal = input.ReadRawObject<Aabb>();
		ISpatialPartition<int> c828d0c1f4103275cf37fe54b9af3f709 = input.ReadObject<ISpatialPartition<int>>();
		input.ReadSharedResource((ITriangleMesh cea4e848206cd8826d44040bad21efb96) =>
		{
			existingInstance.cf291ec93e82da38f9e1814873cf030de(cea4e848206cd8826d44040bad21efb96);
			existingInstance.c56d88ced32e38076203610b9aa5a84d4(c828d0c1f4103275cf37fe54b9af3f709);
		});
		if (input.ReadBoolean())
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			int num = input.ReadInt32();
			existingInstance.TriangleNeighbors = new List<int>(num);
			for (int num2 = 0; num2 < num; num2++)
			{
				existingInstance.TriangleNeighbors.Add(input.ReadInt32());
			}
		}
		return existingInstance;
	}
}
