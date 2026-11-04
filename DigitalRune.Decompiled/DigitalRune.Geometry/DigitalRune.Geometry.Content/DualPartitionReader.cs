using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Partitioning;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class DualPartitionReader<T> : ContentTypeReader<DualPartition<T>>
{
	[CompilerGenerated]
	private sealed class c944a2d795156b9072f00b4d463a75517
	{
		public DualPartition<T> c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(IPairFilter<T> c2ecd87fc581169eaea3415c2bbaed8f5)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.Filter = c2ecd87fc581169eaea3415c2bbaed8f5;
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override DualPartition<T> Read(ContentReader input, DualPartition<T> existingInstance)
	{
		if (existingInstance == null)
		{
			existingInstance = new DualPartition<T>();
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
		existingInstance.StaticPartition = input.ReadObject<ISpatialPartition<T>>();
		existingInstance.DynamicPartition = input.ReadObject<ISpatialPartition<T>>();
		return existingInstance;
	}
}
