using DigitalRune.Geometry.Meshes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content.Pipeline;

public class VertexAdjacencyReader : ContentTypeReader<VertexAdjacency>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override VertexAdjacency Read(ContentReader input, VertexAdjacency existingInstance)
	{
		if (existingInstance == null)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(VertexAdjacencyReader.Read(ContentReader, VertexAdjacency));
			}
			existingInstance = new VertexAdjacency();
		}
		existingInstance.ListIndices = input.ReadRawObject<ushort[]>();
		existingInstance.Lists = input.ReadRawObject<ushort[]>();
		return existingInstance;
	}
}
