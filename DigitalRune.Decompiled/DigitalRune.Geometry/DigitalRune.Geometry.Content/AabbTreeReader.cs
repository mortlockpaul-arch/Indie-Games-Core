using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Partitioning;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class AabbTreeReader<T> : ContentTypeReader<AabbTree<T>>
{
	[CompilerGenerated]
	private sealed class c56c597ba047364a74a2a9efc40f5a915
	{
		public AabbTree<T> c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(IPairFilter<T> c2ecd87fc581169eaea3415c2bbaed8f5)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.Filter = c2ecd87fc581169eaea3415c2bbaed8f5;
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override AabbTree<T> Read(ContentReader input, AabbTree<T> existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new AabbTree<T>();
		}
		else
		{
			existingInstance.Clear();
		}
		existingInstance.EnableSelfOverlaps = input.ReadBoolean();
		existingInstance.BottomUpBuildThreshold = input.ReadInt32();
		input.ReadSharedResource((IPairFilter<T> c2ecd87fc581169eaea3415c2bbaed8f5) =>
		{
			existingInstance.Filter = c2ecd87fc581169eaea3415c2bbaed8f5;
		});
		return existingInstance;
	}
}
