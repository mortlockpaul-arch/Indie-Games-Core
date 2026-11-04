using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Partitioning;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class AdaptiveAabbTreeReader<T> : ContentTypeReader<AdaptiveAabbTree<T>>
{
	[CompilerGenerated]
	private sealed class c8f3e46eb29a6c6d42d58ab00d6736294
	{
		public AdaptiveAabbTree<T> c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(IPairFilter<T> c2ecd87fc581169eaea3415c2bbaed8f5)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.Filter = c2ecd87fc581169eaea3415c2bbaed8f5;
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override AdaptiveAabbTree<T> Read(ContentReader input, AdaptiveAabbTree<T> existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new AdaptiveAabbTree<T>();
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
