using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Partitioning;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class SweepAndPruneSpaceReader<T> : ContentTypeReader<SweepAndPruneSpace<T>>
{
	[CompilerGenerated]
	private sealed class c41f05620d8ac385b5abbc12e829e35a6
	{
		public SweepAndPruneSpace<T> c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(IPairFilter<T> c2ecd87fc581169eaea3415c2bbaed8f5)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.Filter = c2ecd87fc581169eaea3415c2bbaed8f5;
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override SweepAndPruneSpace<T> Read(ContentReader input, SweepAndPruneSpace<T> existingInstance)
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
				__ldtoken(SweepAndPruneSpaceReader<>.Read(ContentReader, SweepAndPruneSpace<T>));
			}
			existingInstance = new SweepAndPruneSpace<T>();
		}
		else
		{
			existingInstance.Clear();
		}
		existingInstance.EnableSelfOverlaps = input.ReadBoolean();
		input.ReadSharedResource((IPairFilter<T> c2ecd87fc581169eaea3415c2bbaed8f5) =>
		{
			existingInstance.Filter = c2ecd87fc581169eaea3415c2bbaed8f5;
		});
		return existingInstance;
	}
}
