using System;
using System.Collections.Generic;
using DigitalRune;
using DigitalRune.Collections;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;

namespace A;

internal static class c5c139e98a37beb65b1cb5aea8ce81daf
{
	public static cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> ccf1870247a728556a633ff212007937e<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> cd4531a9ec3304b0c90a95f8f073929f4, Func<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c6793f36b73592292eb7947a6a57e4674)
	{
		return ce77015ff5cdaf0b454b4d5608cc3fd31(cd4531a9ec3304b0c90a95f8f073929f4, 0, cd4531a9ec3304b0c90a95f8f073929f4.Count - 1, c6793f36b73592292eb7947a6a57e4674);
	}

	public static cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> ccf1870247a728556a633ff212007937e<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> cd4531a9ec3304b0c90a95f8f073929f4, Func<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c6793f36b73592292eb7947a6a57e4674, int cd596ca51884c19972969f4b3b670880f)
	{
		return ce77015ff5cdaf0b454b4d5608cc3fd31(cd4531a9ec3304b0c90a95f8f073929f4, 0, cd4531a9ec3304b0c90a95f8f073929f4.Count - 1, c6793f36b73592292eb7947a6a57e4674, cd596ca51884c19972969f4b3b670880f);
	}

	private static cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c911b8dab6e83ac044dd7e244ba153dff<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> cd4531a9ec3304b0c90a95f8f073929f4, int cd5e7f4a7353310028f25ea0fdb5728d7, int c1d1ad668ad1b3a67eabee1170d21d397, Func<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c6793f36b73592292eb7947a6a57e4674)
	{
		List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> list = ResourcePools<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>.Lists.Obtain();
		for (int i = cd5e7f4a7353310028f25ea0fdb5728d7; i <= c1d1ad668ad1b3a67eabee1170d21d397; i++)
		{
			list.Add(cd4531a9ec3304b0c90a95f8f073929f4[i]);
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(c5c139e98a37beb65b1cb5aea8ce81daf.c911b8dab6e83ac044dd7e244ba153dff<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>, int, int, Func<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>));
			}
			while (list.Count > 1)
			{
				float num = float.PositiveInfinity;
				Pair<int> pair = new Pair<int>(-1, -1);
				for (int j = 0; j < list.Count; j++)
				{
					for (int k = j + 1; k < list.Count; k++)
					{
						Vector3F extent = Aabb.Merge(list[j].c3e263e990bc8d6c3a2d636ac7a19c6c6, list[k].c3e263e990bc8d6c3a2d636ac7a19c6c6).Extent;
						float num2 = extent.X * extent.Y * extent.Z + extent.X + extent.Y + extent.Z;
						if (!(num2 <= num))
						{
							continue;
						}
						while (true)
						{
							switch (3)
							{
							case 0:
								continue;
							}
							break;
						}
						num = num2;
						pair.First = j;
						pair.Second = k;
					}
					while (true)
					{
						switch (2)
						{
						case 0:
							break;
						default:
							goto end_IL_00fa;
						}
						continue;
						end_IL_00fa:
						break;
					}
				}
				cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cbe5649b18b8b0caa00be9ad49188b194 = list[pair.First];
				cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cbe5649b18b8b0caa00be9ad49188b195 = list[pair.Second];
				cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cbe5649b18b8b0caa00be9ad49188b196 = c6793f36b73592292eb7947a6a57e4674();
				cbe5649b18b8b0caa00be9ad49188b196.c3e263e990bc8d6c3a2d636ac7a19c6c6 = Aabb.Merge(cbe5649b18b8b0caa00be9ad49188b194.c3e263e990bc8d6c3a2d636ac7a19c6c6, cbe5649b18b8b0caa00be9ad49188b195.c3e263e990bc8d6c3a2d636ac7a19c6c6);
				cbe5649b18b8b0caa00be9ad49188b196.c6d3fb34101b9e1ce92055c2914d2d1de = cbe5649b18b8b0caa00be9ad49188b194;
				cbe5649b18b8b0caa00be9ad49188b196.c2d9d869d0c8fd5fb55f452375f580a4a = cbe5649b18b8b0caa00be9ad49188b195;
				list.RemoveAt(pair.Second);
				list.RemoveAt(pair.First);
				list.Add(cbe5649b18b8b0caa00be9ad49188b196);
			}
			cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> result = list[0];
			ResourcePools<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>.Lists.Recycle(list);
			return result;
		}
	}

	private static cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c9821fe27f07aa602a384d689403ba400<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> cd4531a9ec3304b0c90a95f8f073929f4, int cd5e7f4a7353310028f25ea0fdb5728d7, int c1d1ad668ad1b3a67eabee1170d21d397, Func<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c6793f36b73592292eb7947a6a57e4674)
	{
		int num = c1d1ad668ad1b3a67eabee1170d21d397 - cd5e7f4a7353310028f25ea0fdb5728d7 + 1;
		if (num == 1)
		{
			return cd4531a9ec3304b0c90a95f8f073929f4[cd5e7f4a7353310028f25ea0fdb5728d7];
		}
		cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cbe5649b18b8b0caa00be9ad49188b194 = c6793f36b73592292eb7947a6a57e4674();
		cbe5649b18b8b0caa00be9ad49188b194.c3e263e990bc8d6c3a2d636ac7a19c6c6 = c388a516b3306df5257f72228eec77428(cd4531a9ec3304b0c90a95f8f073929f4, cd5e7f4a7353310028f25ea0fdb5728d7, c1d1ad668ad1b3a67eabee1170d21d397);
		int indexOfLargestComponent = cbe5649b18b8b0caa00be9ad49188b194.c3e263e990bc8d6c3a2d636ac7a19c6c6.Extent.IndexOfLargestComponent;
		float cb76fbc629b698e3b1b873a267ebe714e = cbe5649b18b8b0caa00be9ad49188b194.c3e263e990bc8d6c3a2d636ac7a19c6c6.Center[indexOfLargestComponent];
		c9a76abd2a74eafc9b1ae5461a5f70d04(cd4531a9ec3304b0c90a95f8f073929f4, cd5e7f4a7353310028f25ea0fdb5728d7, c1d1ad668ad1b3a67eabee1170d21d397, indexOfLargestComponent, cb76fbc629b698e3b1b873a267ebe714e, out var c970f878d2fc540f781119125607c4ce);
		if (c970f878d2fc540f781119125607c4ce != cd5e7f4a7353310028f25ea0fdb5728d7)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(c5c139e98a37beb65b1cb5aea8ce81daf.c9821fe27f07aa602a384d689403ba400<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>, int, int, Func<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>));
			}
			if (c970f878d2fc540f781119125607c4ce <= c1d1ad668ad1b3a67eabee1170d21d397)
			{
				goto IL_00a4;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		c970f878d2fc540f781119125607c4ce = (cd5e7f4a7353310028f25ea0fdb5728d7 + c1d1ad668ad1b3a67eabee1170d21d397 + 1) / 2;
		goto IL_00a4;
		IL_00a4:
		if (c970f878d2fc540f781119125607c4ce == cd5e7f4a7353310028f25ea0fdb5728d7 + 1)
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
			cbe5649b18b8b0caa00be9ad49188b194.c6d3fb34101b9e1ce92055c2914d2d1de = cd4531a9ec3304b0c90a95f8f073929f4[cd5e7f4a7353310028f25ea0fdb5728d7];
		}
		else
		{
			cbe5649b18b8b0caa00be9ad49188b194.c6d3fb34101b9e1ce92055c2914d2d1de = c9821fe27f07aa602a384d689403ba400(cd4531a9ec3304b0c90a95f8f073929f4, cd5e7f4a7353310028f25ea0fdb5728d7, c970f878d2fc540f781119125607c4ce - 1, c6793f36b73592292eb7947a6a57e4674);
		}
		if (c970f878d2fc540f781119125607c4ce == c1d1ad668ad1b3a67eabee1170d21d397)
		{
			cbe5649b18b8b0caa00be9ad49188b194.c2d9d869d0c8fd5fb55f452375f580a4a = cd4531a9ec3304b0c90a95f8f073929f4[c970f878d2fc540f781119125607c4ce];
		}
		else
		{
			cbe5649b18b8b0caa00be9ad49188b194.c2d9d869d0c8fd5fb55f452375f580a4a = c9821fe27f07aa602a384d689403ba400(cd4531a9ec3304b0c90a95f8f073929f4, c970f878d2fc540f781119125607c4ce, c1d1ad668ad1b3a67eabee1170d21d397, c6793f36b73592292eb7947a6a57e4674);
		}
		return cbe5649b18b8b0caa00be9ad49188b194;
	}

	private static cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c9be34bcca1dfbc9fdc369e2b5c87e8f6<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> cd4531a9ec3304b0c90a95f8f073929f4, int cd5e7f4a7353310028f25ea0fdb5728d7, int c1d1ad668ad1b3a67eabee1170d21d397, Func<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c6793f36b73592292eb7947a6a57e4674)
	{
		int num = c1d1ad668ad1b3a67eabee1170d21d397 - cd5e7f4a7353310028f25ea0fdb5728d7 + 1;
		if (num == 1)
		{
			return cd4531a9ec3304b0c90a95f8f073929f4[cd5e7f4a7353310028f25ea0fdb5728d7];
		}
		Vector3F vector3F = default;
		for (int i = cd5e7f4a7353310028f25ea0fdb5728d7; i <= c1d1ad668ad1b3a67eabee1170d21d397; i++)
		{
			vector3F += cd4531a9ec3304b0c90a95f8f073929f4[i].c3e263e990bc8d6c3a2d636ac7a19c6c6.Center;
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(c5c139e98a37beb65b1cb5aea8ce81daf.c9be34bcca1dfbc9fdc369e2b5c87e8f6<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>, int, int, Func<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>));
			}
			vector3F /= (float)num;
			Vector3F vector3F2 = default;
			for (int j = cd5e7f4a7353310028f25ea0fdb5728d7; j <= c1d1ad668ad1b3a67eabee1170d21d397; j++)
			{
				Vector3F vector3F3 = cd4531a9ec3304b0c90a95f8f073929f4[j].c3e263e990bc8d6c3a2d636ac7a19c6c6.Center - vector3F;
				vector3F2 += vector3F3 * vector3F3;
			}
			int indexOfLargestComponent = (vector3F2 / num).IndexOfLargestComponent;
			float cb76fbc629b698e3b1b873a267ebe714e = vector3F[indexOfLargestComponent];
			cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cbe5649b18b8b0caa00be9ad49188b194 = c6793f36b73592292eb7947a6a57e4674();
			cbe5649b18b8b0caa00be9ad49188b194.c3e263e990bc8d6c3a2d636ac7a19c6c6 = c388a516b3306df5257f72228eec77428(cd4531a9ec3304b0c90a95f8f073929f4, cd5e7f4a7353310028f25ea0fdb5728d7, c1d1ad668ad1b3a67eabee1170d21d397);
			c9a76abd2a74eafc9b1ae5461a5f70d04(cd4531a9ec3304b0c90a95f8f073929f4, cd5e7f4a7353310028f25ea0fdb5728d7, c1d1ad668ad1b3a67eabee1170d21d397, indexOfLargestComponent, cb76fbc629b698e3b1b873a267ebe714e, out var c970f878d2fc540f781119125607c4ce);
			int num2 = num / 3;
			if (c970f878d2fc540f781119125607c4ce != cd5e7f4a7353310028f25ea0fdb5728d7)
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
				if (c970f878d2fc540f781119125607c4ce <= c1d1ad668ad1b3a67eabee1170d21d397 && cd5e7f4a7353310028f25ea0fdb5728d7 + num2 < c970f878d2fc540f781119125607c4ce)
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
					if (c970f878d2fc540f781119125607c4ce + num2 < c1d1ad668ad1b3a67eabee1170d21d397)
					{
						goto IL_0140;
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							continue;
						}
						break;
					}
				}
			}
			c970f878d2fc540f781119125607c4ce = (cd5e7f4a7353310028f25ea0fdb5728d7 + c1d1ad668ad1b3a67eabee1170d21d397 + 1) / 2;
			goto IL_0140;
			IL_0140:
			cbe5649b18b8b0caa00be9ad49188b194.c6d3fb34101b9e1ce92055c2914d2d1de = c9be34bcca1dfbc9fdc369e2b5c87e8f6(cd4531a9ec3304b0c90a95f8f073929f4, cd5e7f4a7353310028f25ea0fdb5728d7, c970f878d2fc540f781119125607c4ce - 1, c6793f36b73592292eb7947a6a57e4674);
			cbe5649b18b8b0caa00be9ad49188b194.c2d9d869d0c8fd5fb55f452375f580a4a = c9be34bcca1dfbc9fdc369e2b5c87e8f6(cd4531a9ec3304b0c90a95f8f073929f4, c970f878d2fc540f781119125607c4ce, c1d1ad668ad1b3a67eabee1170d21d397, c6793f36b73592292eb7947a6a57e4674);
			return cbe5649b18b8b0caa00be9ad49188b194;
		}
	}

	private static cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> ce77015ff5cdaf0b454b4d5608cc3fd31<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> cd4531a9ec3304b0c90a95f8f073929f4, int cd5e7f4a7353310028f25ea0fdb5728d7, int c1d1ad668ad1b3a67eabee1170d21d397, Func<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c6793f36b73592292eb7947a6a57e4674)
	{
		return ce77015ff5cdaf0b454b4d5608cc3fd31(cd4531a9ec3304b0c90a95f8f073929f4, cd5e7f4a7353310028f25ea0fdb5728d7, c1d1ad668ad1b3a67eabee1170d21d397, c6793f36b73592292eb7947a6a57e4674, 128);
	}

	private static cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> ce77015ff5cdaf0b454b4d5608cc3fd31<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> cd4531a9ec3304b0c90a95f8f073929f4, int cd5e7f4a7353310028f25ea0fdb5728d7, int c1d1ad668ad1b3a67eabee1170d21d397, Func<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c6793f36b73592292eb7947a6a57e4674, int cd596ca51884c19972969f4b3b670880f)
	{
		int num = c1d1ad668ad1b3a67eabee1170d21d397 - cd5e7f4a7353310028f25ea0fdb5728d7 + 1;
		if (num == 1)
		{
			return cd4531a9ec3304b0c90a95f8f073929f4[cd5e7f4a7353310028f25ea0fdb5728d7];
		}
		if (num <= cd596ca51884c19972969f4b3b670880f)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(c5c139e98a37beb65b1cb5aea8ce81daf.ce77015ff5cdaf0b454b4d5608cc3fd31<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>, int, int, Func<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>, int));
					}
					return c911b8dab6e83ac044dd7e244ba153dff(cd4531a9ec3304b0c90a95f8f073929f4, cd5e7f4a7353310028f25ea0fdb5728d7, c1d1ad668ad1b3a67eabee1170d21d397, c6793f36b73592292eb7947a6a57e4674);
				}
			}
		}
		cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cbe5649b18b8b0caa00be9ad49188b194 = c6793f36b73592292eb7947a6a57e4674();
		cbe5649b18b8b0caa00be9ad49188b194.c3e263e990bc8d6c3a2d636ac7a19c6c6 = c388a516b3306df5257f72228eec77428(cd4531a9ec3304b0c90a95f8f073929f4, cd5e7f4a7353310028f25ea0fdb5728d7, c1d1ad668ad1b3a67eabee1170d21d397);
		Vector3F center = cbe5649b18b8b0caa00be9ad49188b194.c3e263e990bc8d6c3a2d636ac7a19c6c6.Center;
		int[,] array = new int[3, 2];
		for (int i = cd5e7f4a7353310028f25ea0fdb5728d7; i <= c1d1ad668ad1b3a67eabee1170d21d397; i++)
		{
			Vector3F vector3F = cd4531a9ec3304b0c90a95f8f073929f4[i].c3e263e990bc8d6c3a2d636ac7a19c6c6.Center - center;
			for (int j = 0; j < 3; j++)
			{
				if (vector3F[j] <= 0f)
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
					array[j, 0]++;
				}
				else
				{
					array[j, 1]++;
				}
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					goto end_IL_00eb;
				}
				continue;
				end_IL_00eb:
				break;
			}
		}
		int num2 = num;
		int num3 = -1;
		float cb76fbc629b698e3b1b873a267ebe714e = 0f;
		for (int k = 0; k < 3; k++)
		{
			int num4 = array[k, 0];
			int num5 = array[k, 1];
			if (num4 <= 0)
			{
				continue;
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (num5 <= 0)
			{
				continue;
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			int num6 = Math.Abs(num4 - num5);
			if (num6 < num2)
			{
				num2 = num6;
				num3 = k;
				cb76fbc629b698e3b1b873a267ebe714e = center[k];
			}
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			int c970f878d2fc540f781119125607c4ce;
			if (num3 >= 0)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				c9a76abd2a74eafc9b1ae5461a5f70d04(cd4531a9ec3304b0c90a95f8f073929f4, cd5e7f4a7353310028f25ea0fdb5728d7, c1d1ad668ad1b3a67eabee1170d21d397, num3, cb76fbc629b698e3b1b873a267ebe714e, out c970f878d2fc540f781119125607c4ce);
			}
			else
			{
				c970f878d2fc540f781119125607c4ce = (cd5e7f4a7353310028f25ea0fdb5728d7 + c1d1ad668ad1b3a67eabee1170d21d397 + 1) / 2;
			}
			cbe5649b18b8b0caa00be9ad49188b194.c6d3fb34101b9e1ce92055c2914d2d1de = ce77015ff5cdaf0b454b4d5608cc3fd31(cd4531a9ec3304b0c90a95f8f073929f4, cd5e7f4a7353310028f25ea0fdb5728d7, c970f878d2fc540f781119125607c4ce - 1, c6793f36b73592292eb7947a6a57e4674, cd596ca51884c19972969f4b3b670880f);
			cbe5649b18b8b0caa00be9ad49188b194.c2d9d869d0c8fd5fb55f452375f580a4a = ce77015ff5cdaf0b454b4d5608cc3fd31(cd4531a9ec3304b0c90a95f8f073929f4, c970f878d2fc540f781119125607c4ce, c1d1ad668ad1b3a67eabee1170d21d397, c6793f36b73592292eb7947a6a57e4674, cd596ca51884c19972969f4b3b670880f);
			return cbe5649b18b8b0caa00be9ad49188b194;
		}
	}

	private static Aabb c388a516b3306df5257f72228eec77428<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> cd4531a9ec3304b0c90a95f8f073929f4, int cd5e7f4a7353310028f25ea0fdb5728d7, int c1d1ad668ad1b3a67eabee1170d21d397)
	{
		Aabb c3e263e990bc8d6c3a2d636ac7a19c6c = cd4531a9ec3304b0c90a95f8f073929f4[cd5e7f4a7353310028f25ea0fdb5728d7].c3e263e990bc8d6c3a2d636ac7a19c6c6;
		for (int i = cd5e7f4a7353310028f25ea0fdb5728d7 + 1; i <= c1d1ad668ad1b3a67eabee1170d21d397; i++)
		{
			c3e263e990bc8d6c3a2d636ac7a19c6c.Grow(cd4531a9ec3304b0c90a95f8f073929f4[i].c3e263e990bc8d6c3a2d636ac7a19c6c6);
		}
		return c3e263e990bc8d6c3a2d636ac7a19c6c;
	}

	private static void c9a76abd2a74eafc9b1ae5461a5f70d04<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> cd4531a9ec3304b0c90a95f8f073929f4, int cd5e7f4a7353310028f25ea0fdb5728d7, int c1d1ad668ad1b3a67eabee1170d21d397, int c59af201794f410b1aa702b1a6f9e7e5c, float cb76fbc629b698e3b1b873a267ebe714e, out int c970f878d2fc540f781119125607c4ce6)
	{
		int num = cd5e7f4a7353310028f25ea0fdb5728d7;
		c970f878d2fc540f781119125607c4ce6 = c1d1ad668ad1b3a67eabee1170d21d397 + 1;
		while (num < c970f878d2fc540f781119125607c4ce6)
		{
			if (cd4531a9ec3304b0c90a95f8f073929f4[num].c3e263e990bc8d6c3a2d636ac7a19c6c6.Center[c59af201794f410b1aa702b1a6f9e7e5c] <= cb76fbc629b698e3b1b873a267ebe714e)
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
					__ldtoken(c5c139e98a37beb65b1cb5aea8ce81daf.c9a76abd2a74eafc9b1ae5461a5f70d04<c96c92d0bb3e5901860b75aee31593453>(List<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>, int, int, int, float, ref int));
				}
				num++;
			}
			else
			{
				c970f878d2fc540f781119125607c4ce6--;
				cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> value = cd4531a9ec3304b0c90a95f8f073929f4[num];
				cd4531a9ec3304b0c90a95f8f073929f4[num] = cd4531a9ec3304b0c90a95f8f073929f4[c970f878d2fc540f781119125607c4ce6];
				cd4531a9ec3304b0c90a95f8f073929f4[c970f878d2fc540f781119125607c4ce6] = value;
			}
		}
	}
}
