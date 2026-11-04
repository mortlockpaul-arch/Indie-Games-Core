using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class TriangleMeshReader : ContentTypeReader<TriangleMesh>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override TriangleMesh Read(ContentReader input, TriangleMesh existingInstance)
	{
		if (existingInstance == null)
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
			if (1 == 0)
			{
				__ldtoken(TriangleMeshReader.Read(ContentReader, TriangleMesh));
			}
			existingInstance = new TriangleMesh();
		}
		else
		{
			existingInstance.Vertices.Clear();
			existingInstance.Indices.Clear();
		}
		int num = input.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			existingInstance.Vertices.Add(input.ReadRawObject<Vector3F>());
		}
		int num2 = input.ReadInt32();
		for (int j = 0; j < num2; j++)
		{
			existingInstance.Indices.Add(input.ReadInt32());
		}
		return existingInstance;
	}
}
