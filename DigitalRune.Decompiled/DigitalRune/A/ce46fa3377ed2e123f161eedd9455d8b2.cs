using System;
using System.Text;

namespace A;

internal class ce46fa3377ed2e123f161eedd9455d8b2
{
	internal static byte[] ce5f0615654bd80e731ced18842900ade(string c178caa51c848fc2a47f1589dec7f06ca, uint cde87a2bf744009f180fc1b58bf049cfc)
	{
		if (cf392f57816a6dc1e4f972afdbd253659.c352aa377b2bb1402ecb10043063d52f1(c178caa51c848fc2a47f1589dec7f06ca))
		{
			return null;
		}
		byte[] bytes = Encoding.UTF8.GetBytes(c178caa51c848fc2a47f1589dec7f06ca);
		return ce5f0615654bd80e731ced18842900ade(bytes, cde87a2bf744009f180fc1b58bf049cfc);
	}

	internal static byte[] ce5f0615654bd80e731ced18842900ade(byte[] c64f632f47ec42f89161439dfec6e213c, uint cde87a2bf744009f180fc1b58bf049cfc)
	{
		byte[][] c64f632f47ec42f89161439dfec6e213c2 = new byte[1][] { c64f632f47ec42f89161439dfec6e213c };
		return ce5f0615654bd80e731ced18842900ade(c64f632f47ec42f89161439dfec6e213c2, cde87a2bf744009f180fc1b58bf049cfc);
	}

	internal static byte[] ce5f0615654bd80e731ced18842900ade(byte[][] c64f632f47ec42f89161439dfec6e213c, uint cde87a2bf744009f180fc1b58bf049cfc)
	{
		byte[] array = null;
		if (cde87a2bf744009f180fc1b58bf049cfc == 32772)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(ce46fa3377ed2e123f161eedd9455d8b2.ce5f0615654bd80e731ced18842900ade(byte[][], uint));
					}
					throw new Exception(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10649));
				}
			}
		}
		cace3468c03ee39d8d9c9826b32a61418 cace3468c03ee39d8d9c9826b32a61419 = new cace3468c03ee39d8d9c9826b32a61418();
		array = new byte[16];
		foreach (byte[] array2 in c64f632f47ec42f89161439dfec6e213c)
		{
			cace3468c03ee39d8d9c9826b32a61419.c0066714d07a2c4da4148092f4587ccd0(array2, 0, array2.Length);
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			cace3468c03ee39d8d9c9826b32a61419.ce0be770e1b233eb3954a473664bf9e64(array, 0);
			return array;
		}
	}

	internal static bool c06cdb620cdf9fdb25f8efd9a8667479a(c185887a1d2592f0f90e73b127b91e33b c27dd4d0e357e091d6e71153f51e7a196, byte[] cd9c4cc5183a0fffb12775fbe1db9e3f6)
	{
		int num = ca52bddd40aba449ad005abf9228c9b87(cd9c4cc5183a0fffb12775fbe1db9e3f6);
		byte[] array = ((num == 64) ? cf392f57816a6dc1e4f972afdbd253659.c0bbae15145e91cbda560736d56f7f842(c27dd4d0e357e091d6e71153f51e7a196.c0f61a1e5bfeb1897e6ea1009c0cfaee8) : Convert.FromBase64String(c27dd4d0e357e091d6e71153f51e7a196.c0f61a1e5bfeb1897e6ea1009c0cfaee8));
		int num2 = array.Length - num / 8;
		byte[] array2 = new byte[num2];
		Buffer.BlockCopy(array, 0, array2, 0, num2);
		byte[] array3 = new byte[array.Length - num2];
		Buffer.BlockCopy(array, num2, array3, 0, array3.Length);
		bool flag = c06cdb620cdf9fdb25f8efd9a8667479a(array2, array3, cd9c4cc5183a0fffb12775fbe1db9e3f6);
		if (flag)
		{
			c27dd4d0e357e091d6e71153f51e7a196.c7eb4bc05febcd26c74593de318373369(array2);
		}
		return flag;
	}

	internal static bool c06cdb620cdf9fdb25f8efd9a8667479a(byte[] c64f632f47ec42f89161439dfec6e213c, byte[] c540be2ef0f390002d76423dd8364d6fd, byte[] cd9c4cc5183a0fffb12775fbe1db9e3f6)
	{
		byte[] c402104de3808c009827870e3f63b2f4f = ca2ce2c9b45e7d0b4b6d1c1f6ff13f6c1.c61e2272b24a8d3dab89375f37341cab6(c540be2ef0f390002d76423dd8364d6fd, cd9c4cc5183a0fffb12775fbe1db9e3f6);
		byte[] array = null;
		array = ((ca52bddd40aba449ad005abf9228c9b87(cd9c4cc5183a0fffb12775fbe1db9e3f6) > 128) ? ce5f0615654bd80e731ced18842900ade(c64f632f47ec42f89161439dfec6e213c, 32771u) : cb153c1fc363382e25e55dedda0eb37c7(c64f632f47ec42f89161439dfec6e213c));
		int num = 0;
		while (true)
		{
			if (num < array.Length)
			{
				if (array[num] != 0)
				{
					break;
				}
				array[num] = (byte)(num + 1);
				num++;
				continue;
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
			if (1 == 0)
			{
				__ldtoken(ce46fa3377ed2e123f161eedd9455d8b2.c06cdb620cdf9fdb25f8efd9a8667479a(byte[], byte[], byte[]));
			}
			break;
		}
		return cf392f57816a6dc1e4f972afdbd253659.ce0f1db791659db34570eda261419cc2d(c402104de3808c009827870e3f63b2f4f, 0, array, 0, array.Length);
	}

	internal static byte[] c18e6fe7b4301d04ff044c5cc145cf28d(int cc9318c57b86104b1057fdbd227eb8df4)
	{
		byte[] array = new byte[cc9318c57b86104b1057fdbd227eb8df4];
		cd73565acbed5ec6aedfbfb6565f5f32c cd73565acbed5ec6aedfbfb6565f5f32c2 = new cd73565acbed5ec6aedfbfb6565f5f32c();
		cd73565acbed5ec6aedfbfb6565f5f32c2.c4a0be3d68e42efc58fec42010e95c248(array);
		return array;
	}

	internal static int ca52bddd40aba449ad005abf9228c9b87(byte[] cd9c4cc5183a0fffb12775fbe1db9e3f6)
	{
		return c68ca6617c26c8d07e1d3552c202b8862.ccf0b10fedac007c4f73ef13a38a96900(cd9c4cc5183a0fffb12775fbe1db9e3f6, 1) * 2;
	}

	internal static string c740bdf071c8fb80158e907e3f5b0d79a(byte[] c64f632f47ec42f89161439dfec6e213c, byte[] c540be2ef0f390002d76423dd8364d6fd)
	{
		byte[] array = new byte[c64f632f47ec42f89161439dfec6e213c.Length + c540be2ef0f390002d76423dd8364d6fd.Length];
		c64f632f47ec42f89161439dfec6e213c.CopyTo(array, 0);
		c540be2ef0f390002d76423dd8364d6fd.CopyTo(array, c64f632f47ec42f89161439dfec6e213c.Length);
		return Convert.ToBase64String(array);
	}

	internal static string cee62e00569920cb7af73e67441496943(byte[] c64f632f47ec42f89161439dfec6e213c, byte[] c540be2ef0f390002d76423dd8364d6fd)
	{
		byte[] array = new byte[c64f632f47ec42f89161439dfec6e213c.Length + c540be2ef0f390002d76423dd8364d6fd.Length];
		c64f632f47ec42f89161439dfec6e213c.CopyTo(array, 0);
		c540be2ef0f390002d76423dd8364d6fd.CopyTo(array, c64f632f47ec42f89161439dfec6e213c.Length);
		string c178caa51c848fc2a47f1589dec7f06ca = cf392f57816a6dc1e4f972afdbd253659.cbb20da9675067bc12f19be38016fe1f3(array);
		return cf392f57816a6dc1e4f972afdbd253659.ceb9d502fa8f46b3c7d576612d6cd871c(c178caa51c848fc2a47f1589dec7f06ca, c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7712), 4);
	}

	private static byte[] cb153c1fc363382e25e55dedda0eb37c7(byte[] c64f632f47ec42f89161439dfec6e213c)
	{
		byte[] array = ce5f0615654bd80e731ced18842900ade(c64f632f47ec42f89161439dfec6e213c, 32771u);
		byte[] array2 = new byte[7];
		int num = 0;
		for (int i = 0; i < array.Length - 2; i += 2)
		{
			array2[num] = array[i];
			num++;
		}
		return array2;
	}

	internal static byte[] ce7a8aa4a10cda60d884a9d6ed2b39cb7(byte[] cd9c4cc5183a0fffb12775fbe1db9e3f6, byte[] c64f632f47ec42f89161439dfec6e213c)
	{
		byte[] array = null;
		array = ((ca52bddd40aba449ad005abf9228c9b87(cd9c4cc5183a0fffb12775fbe1db9e3f6) > 128) ? ce5f0615654bd80e731ced18842900ade(c64f632f47ec42f89161439dfec6e213c, 32771u) : cb153c1fc363382e25e55dedda0eb37c7(c64f632f47ec42f89161439dfec6e213c));
		int num = 0;
		while (true)
		{
			if (num < array.Length)
			{
				if (array[num] != 0)
				{
					break;
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
						__ldtoken(ce46fa3377ed2e123f161eedd9455d8b2.ce7a8aa4a10cda60d884a9d6ed2b39cb7(byte[], byte[]));
					}
					array[num] = (byte)(num + 1);
					num++;
					break;
				}
				continue;
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
			break;
		}
		return ca2ce2c9b45e7d0b4b6d1c1f6ff13f6c1.caf8924ca089641fea586031eec23ccb9(array, cd9c4cc5183a0fffb12775fbe1db9e3f6);
	}

	internal static byte[] cb8d54ed5acdab8aae3563c99c1c20ab7(byte[] cd9c4cc5183a0fffb12775fbe1db9e3f6)
	{
		cf23b4d8f8deffc8d2fb0df3730127576 cf23b4d8f8deffc8d2fb0df37301275762 = cf23b4d8f8deffc8d2fb0df3730127576.c7fead2fa029058e5207ca992ffd0e474(cd9c4cc5183a0fffb12775fbe1db9e3f6);
		return cf23b4d8f8deffc8d2fb0df37301275762.ce736b8b9e8f368561f651891fb197f44();
	}
}
