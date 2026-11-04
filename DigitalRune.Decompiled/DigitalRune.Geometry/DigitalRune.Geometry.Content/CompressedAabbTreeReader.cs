using System;
using System.Runtime.CompilerServices;
using DigitalRune.Geometry.Partitioning;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework.Content;

namespace DigitalRune.Geometry.Content;

public class CompressedAabbTreeReader : ContentTypeReader<CompressedAabbTree>
{
	[CompilerGenerated]
	private sealed class c7d5c47136df4e94ad06bcc05f65414ab
	{
		public CompressedAabbTree c1864f4cd67afc8f8420dcfd541bcf756;

		public void ccc20b61db7076118d78cb0c67e8931cc(IPairFilter<int> c2ecd87fc581169eaea3415c2bbaed8f5)
		{
			c1864f4cd67afc8f8420dcfd541bcf756.Filter = c2ecd87fc581169eaea3415c2bbaed8f5;
		}

		public void c862e05195c4b76d1738c2bd89afc732c(IPairFilter<int> c2ecd87fc581169eaea3415c2bbaed8f5)
		{
			if (c1864f4cd67afc8f8420dcfd541bcf756.c7215839bcbb23f1ded1a6a9876755f7f != null)
			{
				c1864f4cd67afc8f8420dcfd541bcf756.c7215839bcbb23f1ded1a6a9876755f7f.Changed -= c1864f4cd67afc8f8420dcfd541bcf756.c7800892d6d855908c9d25556ad846a02;
			}
			c1864f4cd67afc8f8420dcfd541bcf756.c7215839bcbb23f1ded1a6a9876755f7f = c2ecd87fc581169eaea3415c2bbaed8f5;
			if (c1864f4cd67afc8f8420dcfd541bcf756.c7215839bcbb23f1ded1a6a9876755f7f != null)
			{
				c1864f4cd67afc8f8420dcfd541bcf756.c7215839bcbb23f1ded1a6a9876755f7f.Changed += c1864f4cd67afc8f8420dcfd541bcf756.c7800892d6d855908c9d25556ad846a02;
			}
		}
	}

	public override bool CanDeserializeIntoExistingObject => true;

	protected override CompressedAabbTree Read(ContentReader input, CompressedAabbTree existingInstance)
	{
		Action<IPairFilter<int>> action = null;
		if (existingInstance == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(CompressedAabbTreeReader.Read(ContentReader, CompressedAabbTree));
			}
			existingInstance = new CompressedAabbTree();
		}
		else
		{
			existingInstance.Clear();
		}
		if (input.ReadBoolean())
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			existingInstance.EnableSelfOverlaps = input.ReadBoolean();
			existingInstance.BottomUpBuildThreshold = input.ReadInt32();
			if (action == null)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				action = (IPairFilter<int> c2ecd87fc581169eaea3415c2bbaed8f5) =>
				{
					existingInstance.Filter = c2ecd87fc581169eaea3415c2bbaed8f5;
				};
			}
			input.ReadSharedResource(action);
		}
		else
		{
			existingInstance.c8c9b32159bf45aa88a32433dee5df2c1 = (CompressedAabbTree.cf8d321a387e439baf196dca5797d9576)input.ReadInt32();
			existingInstance.ccc439410c5678c542a116b1c7b4551b8 = input.ReadInt32();
			if (existingInstance.c650f95538a011c39773e3b9e0b147480 != null)
			{
				ResourcePools<int>.Lists.Recycle(existingInstance.c650f95538a011c39773e3b9e0b147480);
				existingInstance.c650f95538a011c39773e3b9e0b147480 = null;
			}
			int num = input.ReadInt32();
			existingInstance.cd26c0d77f8d7f857646d6821d2cb60a7 = new CompressedAabbTree.c3be4e279c85d04da07f3693866750bf4[num];
			for (int num2 = 0; num2 < num; num2++)
			{
				CompressedAabbTree.c3be4e279c85d04da07f3693866750bf4 c3be4e279c85d04da07f3693866750bf = new CompressedAabbTree.c3be4e279c85d04da07f3693866750bf4
				{
					c0766ee351495379577e4bd426a93fd76 = input.ReadUInt16(),
					ca908b2ef855c3e51cf79eb5717ba52da = input.ReadUInt16(),
					c2d1089e3916028efbe65eebdee0edea4 = input.ReadUInt16(),
					ca496a3c2e4033c7383700937a074c8be = input.ReadUInt16(),
					c7d2a56de6e39281750b1f1c7a61a8868 = input.ReadUInt16(),
					c95b253ba97348b0005f6ccc969f9c2e3 = input.ReadUInt16(),
					c69b0e87934525495e9ec68864d16eee1 = input.ReadInt32()
				};
				existingInstance.cd26c0d77f8d7f857646d6821d2cb60a7[num2] = c3be4e279c85d04da07f3693866750bf;
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			existingInstance.c3a708fca2e00c0f5027ce857e9da5624 = input.ReadRawObject<Aabb>();
			existingInstance.c559f787cd29aab31167fc46547165074 = input.ReadRawObject<Vector3F>();
			existingInstance.c955716b7e41c5abb5a33946fe6b86eda = input.ReadRawObject<Vector3F>();
			existingInstance.EnableSelfOverlaps = input.ReadBoolean();
			existingInstance.BottomUpBuildThreshold = input.ReadInt32();
			input.ReadSharedResource((IPairFilter<int> c2ecd87fc581169eaea3415c2bbaed8f5) =>
			{
				if (existingInstance.c7215839bcbb23f1ded1a6a9876755f7f != null)
				{
					existingInstance.c7215839bcbb23f1ded1a6a9876755f7f.Changed -= existingInstance.c7800892d6d855908c9d25556ad846a02;
				}
				existingInstance.c7215839bcbb23f1ded1a6a9876755f7f = c2ecd87fc581169eaea3415c2bbaed8f5;
				if (existingInstance.c7215839bcbb23f1ded1a6a9876755f7f != null)
				{
					existingInstance.c7215839bcbb23f1ded1a6a9876755f7f.Changed += existingInstance.c7800892d6d855908c9d25556ad846a02;
				}
			});
		}
		return existingInstance;
	}
}
