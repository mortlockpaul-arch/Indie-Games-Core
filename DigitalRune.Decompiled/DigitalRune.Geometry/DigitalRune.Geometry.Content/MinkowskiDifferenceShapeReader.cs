using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class MinkowskiDifferenceShapeReader : ContentTypeReader<MinkowskiDifferenceShape>
{
	[CompilerGenerated]
	private sealed class c7e42f679d9226faec4970ddb9d86284d
	{
		public MinkowskiDifferenceShape c1864f4cd67afc8f8420dcfd541bcf756;

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

	protected override MinkowskiDifferenceShape Read(ContentReader input, MinkowskiDifferenceShape existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new MinkowskiDifferenceShape();
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
