using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Partitioning;
using DigitalRune.Geometry.Shapes;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class CompositeShapeReader : ContentTypeReader<CompositeShape>
{
	[CompilerGenerated]
	private sealed class c66948d5c4e5022f9f6023c69277db555
	{
		public int c8fec44e5f8ce93dcbda28f3026bff8d4;

		public ISpatialPartition<int> c828d0c1f4103275cf37fe54b9af3f709;

		public GeometricObject[] cdc1c1940f0fe8866acb8f20d73e4fcfa;

		public int c943673bf4a019321f5f0f6d2dfda2f94;

		public CompositeShape c1864f4cd67afc8f8420dcfd541bcf756;
	}

	[CompilerGenerated]
	private sealed class cedb4ae325c760f6686bfbcf4a3f25085
	{
		public c66948d5c4e5022f9f6023c69277db555 c0ef5d618be24b4747e55c021a4e20479;

		public int c88a85a6fc8e0a66e19d046d4d51cd3f1;

		public void ccc20b61db7076118d78cb0c67e8931cc(GeometricObject c812969d803f43545dee254dc6fdae4c9)
		{
			c0ef5d618be24b4747e55c021a4e20479.cdc1c1940f0fe8866acb8f20d73e4fcfa[c88a85a6fc8e0a66e19d046d4d51cd3f1] = c812969d803f43545dee254dc6fdae4c9;
			c0ef5d618be24b4747e55c021a4e20479.c943673bf4a019321f5f0f6d2dfda2f94++;
			if (c0ef5d618be24b4747e55c021a4e20479.c943673bf4a019321f5f0f6d2dfda2f94 != c0ef5d618be24b4747e55c021a4e20479.c8fec44e5f8ce93dcbda28f3026bff8d4)
			{
				return;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(cedb4ae325c760f6686bfbcf4a3f25085.ccc20b61db7076118d78cb0c67e8931cc(GeometricObject));
				}
				c0ef5d618be24b4747e55c021a4e20479.c1864f4cd67afc8f8420dcfd541bcf756.Children.AddRange(c0ef5d618be24b4747e55c021a4e20479.cdc1c1940f0fe8866acb8f20d73e4fcfa);
				c0ef5d618be24b4747e55c021a4e20479.c1864f4cd67afc8f8420dcfd541bcf756.c56d88ced32e38076203610b9aa5a84d4(c0ef5d618be24b4747e55c021a4e20479.c828d0c1f4103275cf37fe54b9af3f709);
				return;
			}
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override CompositeShape Read(ContentReader input, CompositeShape existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new CompositeShape();
		}
		else
		{
			existingInstance.Children.Clear();
		}
		int c8fec44e5f8ce93dcbda28f3026bff8d4 = input.ReadInt32();
		ISpatialPartition<int> c828d0c1f4103275cf37fe54b9af3f709 = input.ReadObject<ISpatialPartition<int>>();
		GeometricObject[] cdc1c1940f0fe8866acb8f20d73e4fcfa = new GeometricObject[c8fec44e5f8ce93dcbda28f3026bff8d4];
		int c943673bf4a019321f5f0f6d2dfda2f94 = 0;
		for (int i = 0; i < c8fec44e5f8ce93dcbda28f3026bff8d4; i++)
		{
			int c88a85a6fc8e0a66e19d046d4d51cd3f1 = i;
			input.ReadSharedResource((GeometricObject c812969d803f43545dee254dc6fdae4c9) =>
			{
				cdc1c1940f0fe8866acb8f20d73e4fcfa[c88a85a6fc8e0a66e19d046d4d51cd3f1] = c812969d803f43545dee254dc6fdae4c9;
				c943673bf4a019321f5f0f6d2dfda2f94++;
				if (c943673bf4a019321f5f0f6d2dfda2f94 == c8fec44e5f8ce93dcbda28f3026bff8d4)
				{
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							if (1 == 0)
							{
								__ldtoken(cedb4ae325c760f6686bfbcf4a3f25085.ccc20b61db7076118d78cb0c67e8931cc(GeometricObject));
							}
							existingInstance.Children.AddRange(cdc1c1940f0fe8866acb8f20d73e4fcfa);
							existingInstance.c56d88ced32e38076203610b9aa5a84d4(c828d0c1f4103275cf37fe54b9af3f709);
							return;
						}
					}
				}
			});
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(CompositeShapeReader.Read(ContentReader, CompositeShape));
			}
			return existingInstance;
		}
	}
}
