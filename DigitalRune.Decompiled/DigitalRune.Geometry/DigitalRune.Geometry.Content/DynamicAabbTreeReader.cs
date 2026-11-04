using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Partitioning;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class DynamicAabbTreeReader<T> : ContentTypeReader<DynamicAabbTree<T>>
{
	[CompilerGenerated]
	private sealed class c708a368b2592df328741fb5a8b661325
	{
		public DynamicAabbTree<T> c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(IPairFilter<T> c2ecd87fc581169eaea3415c2bbaed8f5)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.Filter = c2ecd87fc581169eaea3415c2bbaed8f5;
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override DynamicAabbTree<T> Read(ContentReader input, DynamicAabbTree<T> existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new DynamicAabbTree<T>();
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
		existingInstance.BottomUpBuildThreshold = input.ReadInt32();
		existingInstance.EnableMotionPrediction = input.ReadBoolean();
		existingInstance.MotionPrediction = input.ReadSingle();
		existingInstance.OptimizationPerFrame = input.ReadSingle();
		existingInstance.RelativeMargin = input.ReadSingle();
		return existingInstance;
	}
}
