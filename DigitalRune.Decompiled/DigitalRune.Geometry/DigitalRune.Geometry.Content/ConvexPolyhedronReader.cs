using DigitalRune.Geometry.Meshes;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class ConvexPolyhedronReader : ContentTypeReader<ConvexPolyhedron>
{
	public override bool CanDeserializeIntoExistingObject => true;

	protected override ConvexPolyhedron Read(ContentReader input, ConvexPolyhedron existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new ConvexPolyhedron();
		}
		int num = input.ReadInt32();
		Vector3F[] array = new Vector3F[num];
		for (int i = 0; i < num; i++)
		{
			ref Vector3F reference = ref array[i];
			reference = input.ReadRawObject<Vector3F>();
		}
		Aabb cdc3439410d15f28489c964ee87b88fe = input.ReadRawObject<Aabb>();
		Vector3F ce46e13dcb6111ee9566eee6448069e = input.ReadRawObject<Vector3F>();
		DirectionalLookupTableF<ushort> c313e74485477de82f358a740efbf = input.ReadObject<DirectionalLookupTableF<ushort>>();
		VertexAdjacency c292db26ac9a5ad542c10e2de2d6c859f = input.ReadObject<VertexAdjacency>();
		existingInstance.c0df7e969ca2a02100b0e8f542e95b0a0(array, cdc3439410d15f28489c964ee87b88fe, ce46e13dcb6111ee9566eee6448069e, c313e74485477de82f358a740efbf, c292db26ac9a5ad542c10e2de2d6c859f);
		return existingInstance;
	}
}
