using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class TransformedShapeReader : ContentTypeReader<TransformedShape>
{
	[CompilerGenerated]
	private sealed class c150af76815b139dc2d48e3c02de11b08
	{
		public TransformedShape c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(GeometricObject c812969d803f43545dee254dc6fdae4c9)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.Child = c812969d803f43545dee254dc6fdae4c9;
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override TransformedShape Read(ContentReader input, TransformedShape existingInstance)
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
				__ldtoken(TransformedShapeReader.Read(ContentReader, TransformedShape));
			}
			existingInstance = new TransformedShape();
		}
		input.ReadSharedResource((GeometricObject c812969d803f43545dee254dc6fdae4c9) =>
		{
			existingInstance.Child = c812969d803f43545dee254dc6fdae4c9;
		});
		return existingInstance;
	}
}
