using System;
using System.Collections;
using System.Collections.Generic;

namespace A;

internal class c70a9f1f6302abae8209fa3bd3ad7722e<c228f9361f0da69cfe47819ff67ccfac0, cf44350890eed4b1efc197f803e97db3d> : IEnumerable<KeyValuePair<c228f9361f0da69cfe47819ff67ccfac0, cf44350890eed4b1efc197f803e97db3d>>, IEnumerable
{
	private enum cb322f623ccc1eb32480f1ef2b9a823f7
	{
		c5e575b1e405b2dad96e3ae6b5455f3c9,
		cff390782086d4775a28c5af1a8ec7667,
		ccadcbc070ed232d8b8b87ab961ea7f15
	}

	private struct c421c20aed5f171eaf1d853159f1198bc
	{
		public c228f9361f0da69cfe47819ff67ccfac0 c72e2ac26518e130b84ec17acbbadf82c;

		public cf44350890eed4b1efc197f803e97db3d c65c4219852d5a388a39ac50b453062c2;

		public cb322f623ccc1eb32480f1ef2b9a823f7 c068b47ba1187b1dccad6a03226fb4abf;
	}

	public struct c27bac404a3ce140128437624b882afb4(c70a9f1f6302abae8209fa3bd3ad7722e<c228f9361f0da69cfe47819ff67ccfac0, cf44350890eed4b1efc197f803e97db3d> cd8f3312bb82377290424ac00fb35f2aa) : IEnumerator<KeyValuePair<c228f9361f0da69cfe47819ff67ccfac0, cf44350890eed4b1efc197f803e97db3d>>, IEnumerator, IDisposable
	{
		private readonly c70a9f1f6302abae8209fa3bd3ad7722e<c228f9361f0da69cfe47819ff67ccfac0, cf44350890eed4b1efc197f803e97db3d> c56fbe704569b1d233c9ac83419117b1a = cd8f3312bb82377290424ac00fb35f2aa;

		private int c1bcc6ebd2b67d4552f56570928d09c76 = -1;

		private KeyValuePair<c228f9361f0da69cfe47819ff67ccfac0, cf44350890eed4b1efc197f803e97db3d> c209b142037e732ef8ff084df7ac98927 = default;

		private object c1bbc3372d25e0136d793f1306899a54b => c209b142037e732ef8ff084df7ac98927;

		public KeyValuePair<c228f9361f0da69cfe47819ff67ccfac0, cf44350890eed4b1efc197f803e97db3d> Current => c209b142037e732ef8ff084df7ac98927;

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			c421c20aed5f171eaf1d853159f1198bc c421c20aed5f171eaf1d853159f1198bc2;
			do
			{
				c1bcc6ebd2b67d4552f56570928d09c76++;
				if (c56fbe704569b1d233c9ac83419117b1a.c37adb342465be20dcf2c430c7d6aeb68.Length <= c1bcc6ebd2b67d4552f56570928d09c76)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							break;
						default:
							if (1 == 0)
							{
								__ldtoken(c70a9f1f6302abae8209fa3bd3ad7722e<, >.c27bac404a3ce140128437624b882afb4.MoveNext());
							}
							return false;
						}
					}
				}
				c421c20aed5f171eaf1d853159f1198bc2 = c56fbe704569b1d233c9ac83419117b1a.c37adb342465be20dcf2c430c7d6aeb68[c1bcc6ebd2b67d4552f56570928d09c76];
			}
			while (c421c20aed5f171eaf1d853159f1198bc2.c068b47ba1187b1dccad6a03226fb4abf != cb322f623ccc1eb32480f1ef2b9a823f7.cff390782086d4775a28c5af1a8ec7667);
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				c209b142037e732ef8ff084df7ac98927 = new KeyValuePair<c228f9361f0da69cfe47819ff67ccfac0, cf44350890eed4b1efc197f803e97db3d>(c421c20aed5f171eaf1d853159f1198bc2.c72e2ac26518e130b84ec17acbbadf82c, c421c20aed5f171eaf1d853159f1198bc2.c65c4219852d5a388a39ac50b453062c2);
				return true;
			}
		}

		public void Reset()
		{
			c1bcc6ebd2b67d4552f56570928d09c76 = -1;
		}
	}

	private static readonly EqualityComparer<c228f9361f0da69cfe47819ff67ccfac0> c509560a58d69b736db5bf8d9407388b7 = EqualityComparer<c228f9361f0da69cfe47819ff67ccfac0>.Default;

	private static readonly c421c20aed5f171eaf1d853159f1198bc c0bd812ad217d9815a582b43d35554749 = new c421c20aed5f171eaf1d853159f1198bc
	{
		c72e2ac26518e130b84ec17acbbadf82c = default,
		c65c4219852d5a388a39ac50b453062c2 = default,
		c068b47ba1187b1dccad6a03226fb4abf = cb322f623ccc1eb32480f1ef2b9a823f7.ccadcbc070ed232d8b8b87ab961ea7f15
	};

	private volatile c421c20aed5f171eaf1d853159f1198bc[] c37adb342465be20dcf2c430c7d6aeb68;

	private c1895de254ebbe9cb91fe0418007c7c26 c5428727f04c7bfdeb67e7ab91f81a9b1;

	public c70a9f1f6302abae8209fa3bd3ad7722e(int c1b0b028008ee11ca8e177f1a77582d9d)
	{
		if (c1b0b028008ee11ca8e177f1a77582d9d < 1)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5572), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5603));
		}
		c37adb342465be20dcf2c430c7d6aeb68 = new c421c20aed5f171eaf1d853159f1198bc[c1b0b028008ee11ca8e177f1a77582d9d];
	}

	public void c03d72a32723924cb5b81c585d5df7d9d(c228f9361f0da69cfe47819ff67ccfac0 c8f1b13f3e698dc6357eae9af1da14536, cf44350890eed4b1efc197f803e97db3d c64f632f47ec42f89161439dfec6e213c)
	{
		try
		{
			c5428727f04c7bfdeb67e7ab91f81a9b1.ca45e58c22f5934bf30e51f80b31fc58b();
			if (!ce879568be87f78a2d090bdfb9ec54e44(c37adb342465be20dcf2c430c7d6aeb68, c8f1b13f3e698dc6357eae9af1da14536, c64f632f47ec42f89161439dfec6e213c))
			{
				c7f557ecaf4cce935648f40fa38f71088();
				ce879568be87f78a2d090bdfb9ec54e44(c37adb342465be20dcf2c430c7d6aeb68, c8f1b13f3e698dc6357eae9af1da14536, c64f632f47ec42f89161439dfec6e213c);
			}
		}
		finally
		{
			c5428727f04c7bfdeb67e7ab91f81a9b1.c2e949f29d1f93161447a38cd6f450556();
		}
	}

	private void c7f557ecaf4cce935648f40fa38f71088()
	{
		c421c20aed5f171eaf1d853159f1198bc[] cd8f3312bb82377290424ac00fb35f2aa = new c421c20aed5f171eaf1d853159f1198bc[c37adb342465be20dcf2c430c7d6aeb68.Length * 2];
		for (int i = 0; i < c37adb342465be20dcf2c430c7d6aeb68.Length; i++)
		{
			c421c20aed5f171eaf1d853159f1198bc c421c20aed5f171eaf1d853159f1198bc2 = c37adb342465be20dcf2c430c7d6aeb68[i];
			if (c421c20aed5f171eaf1d853159f1198bc2.c068b47ba1187b1dccad6a03226fb4abf == cb322f623ccc1eb32480f1ef2b9a823f7.cff390782086d4775a28c5af1a8ec7667)
			{
				ce879568be87f78a2d090bdfb9ec54e44(cd8f3312bb82377290424ac00fb35f2aa, c421c20aed5f171eaf1d853159f1198bc2.c72e2ac26518e130b84ec17acbbadf82c, c421c20aed5f171eaf1d853159f1198bc2.c65c4219852d5a388a39ac50b453062c2);
			}
		}
		c37adb342465be20dcf2c430c7d6aeb68 = cd8f3312bb82377290424ac00fb35f2aa;
	}

	private static bool ce879568be87f78a2d090bdfb9ec54e44(c421c20aed5f171eaf1d853159f1198bc[] cd8f3312bb82377290424ac00fb35f2aa, c228f9361f0da69cfe47819ff67ccfac0 c8f1b13f3e698dc6357eae9af1da14536, cf44350890eed4b1efc197f803e97db3d c64f632f47ec42f89161439dfec6e213c)
	{
		int num = Math.Abs(c8f1b13f3e698dc6357eae9af1da14536.GetHashCode()) % cd8f3312bb82377290424ac00fb35f2aa.Length;
		int num2 = num;
		bool result = false;
		do
		{
			c421c20aed5f171eaf1d853159f1198bc c421c20aed5f171eaf1d853159f1198bc2 = cd8f3312bb82377290424ac00fb35f2aa[num2];
			if (c421c20aed5f171eaf1d853159f1198bc2.c068b47ba1187b1dccad6a03226fb4abf == cb322f623ccc1eb32480f1ef2b9a823f7.c5e575b1e405b2dad96e3ae6b5455f3c9 || c421c20aed5f171eaf1d853159f1198bc2.c068b47ba1187b1dccad6a03226fb4abf == cb322f623ccc1eb32480f1ef2b9a823f7.ccadcbc070ed232d8b8b87ab961ea7f15 || c509560a58d69b736db5bf8d9407388b7.Equals(c8f1b13f3e698dc6357eae9af1da14536, c421c20aed5f171eaf1d853159f1198bc2.c72e2ac26518e130b84ec17acbbadf82c))
			{
				ref c421c20aed5f171eaf1d853159f1198bc reference = ref cd8f3312bb82377290424ac00fb35f2aa[num2];
				reference = new c421c20aed5f171eaf1d853159f1198bc
				{
					c72e2ac26518e130b84ec17acbbadf82c = c8f1b13f3e698dc6357eae9af1da14536,
					c65c4219852d5a388a39ac50b453062c2 = c64f632f47ec42f89161439dfec6e213c,
					c068b47ba1187b1dccad6a03226fb4abf = cb322f623ccc1eb32480f1ef2b9a823f7.cff390782086d4775a28c5af1a8ec7667
				};
				result = true;
				break;
			}
			num2 = (num2 + 1) % cd8f3312bb82377290424ac00fb35f2aa.Length;
		}
		while (num2 != num);
		return result;
	}

	public void c2741d364921e6fbf232efb72e0eb1546(c228f9361f0da69cfe47819ff67ccfac0 c8f1b13f3e698dc6357eae9af1da14536, cf44350890eed4b1efc197f803e97db3d c8eea6fe5a82bc3c0867f6cf9ae7c0eea)
	{
		bool flag = false;
		c421c20aed5f171eaf1d853159f1198bc[] array;
		do
		{
			array = c37adb342465be20dcf2c430c7d6aeb68;
			int num = Math.Abs(c8f1b13f3e698dc6357eae9af1da14536.GetHashCode()) % array.Length;
			int num2 = num;
			do
			{
				c421c20aed5f171eaf1d853159f1198bc c421c20aed5f171eaf1d853159f1198bc2 = array[num2];
				if (c509560a58d69b736db5bf8d9407388b7.Equals(c8f1b13f3e698dc6357eae9af1da14536, c421c20aed5f171eaf1d853159f1198bc2.c72e2ac26518e130b84ec17acbbadf82c))
				{
					ref c421c20aed5f171eaf1d853159f1198bc reference = ref array[num2];
					reference = new c421c20aed5f171eaf1d853159f1198bc
					{
						c72e2ac26518e130b84ec17acbbadf82c = c8f1b13f3e698dc6357eae9af1da14536,
						c65c4219852d5a388a39ac50b453062c2 = c8eea6fe5a82bc3c0867f6cf9ae7c0eea,
						c068b47ba1187b1dccad6a03226fb4abf = cb322f623ccc1eb32480f1ef2b9a823f7.cff390782086d4775a28c5af1a8ec7667
					};
					flag = true;
					break;
				}
				num2 = (num2 + 1) % array.Length;
			}
			while (num2 != num);
		}
		while (array != c37adb342465be20dcf2c430c7d6aeb68);
		if (!flag)
		{
			c03d72a32723924cb5b81c585d5df7d9d(c8f1b13f3e698dc6357eae9af1da14536, c8eea6fe5a82bc3c0867f6cf9ae7c0eea);
		}
	}

	private bool ce3452180322d3dcb6d6aeb55186df614(c228f9361f0da69cfe47819ff67ccfac0 c8f1b13f3e698dc6357eae9af1da14536, out c421c20aed5f171eaf1d853159f1198bc c7f587bd57d47897172f47483e6bcca68)
	{
		c7f587bd57d47897172f47483e6bcca68 = default;
		c421c20aed5f171eaf1d853159f1198bc[] array = c37adb342465be20dcf2c430c7d6aeb68;
		int num = Math.Abs(c8f1b13f3e698dc6357eae9af1da14536.GetHashCode()) % array.Length;
		int num2 = num;
		do
		{
			c421c20aed5f171eaf1d853159f1198bc c421c20aed5f171eaf1d853159f1198bc2 = array[num2];
			if (c421c20aed5f171eaf1d853159f1198bc2.c068b47ba1187b1dccad6a03226fb4abf == cb322f623ccc1eb32480f1ef2b9a823f7.c5e575b1e405b2dad96e3ae6b5455f3c9)
			{
				return false;
			}
			if (c421c20aed5f171eaf1d853159f1198bc2.c068b47ba1187b1dccad6a03226fb4abf == cb322f623ccc1eb32480f1ef2b9a823f7.ccadcbc070ed232d8b8b87ab961ea7f15 || !c509560a58d69b736db5bf8d9407388b7.Equals(c8f1b13f3e698dc6357eae9af1da14536, c421c20aed5f171eaf1d853159f1198bc2.c72e2ac26518e130b84ec17acbbadf82c))
			{
				num2 = (num2 + 1) % array.Length;
				continue;
			}
			c7f587bd57d47897172f47483e6bcca68 = c421c20aed5f171eaf1d853159f1198bc2;
			return true;
		}
		while (num2 != num);
		return false;
	}

	public bool cfa34cf3f48196497030d8ee48d8e4741(c228f9361f0da69cfe47819ff67ccfac0 c8f1b13f3e698dc6357eae9af1da14536, out cf44350890eed4b1efc197f803e97db3d c64f632f47ec42f89161439dfec6e213c)
	{
		if (ce3452180322d3dcb6d6aeb55186df614(c8f1b13f3e698dc6357eae9af1da14536, out var c7f587bd57d47897172f47483e6bcca))
		{
			c64f632f47ec42f89161439dfec6e213c = c7f587bd57d47897172f47483e6bcca.c65c4219852d5a388a39ac50b453062c2;
			return true;
		}
		c64f632f47ec42f89161439dfec6e213c = default;
		return false;
	}

	public void c0293645f58f4f4f88a5baaf07d85095f(c228f9361f0da69cfe47819ff67ccfac0 c8f1b13f3e698dc6357eae9af1da14536)
	{
		try
		{
			c5428727f04c7bfdeb67e7ab91f81a9b1.ca45e58c22f5934bf30e51f80b31fc58b();
			c421c20aed5f171eaf1d853159f1198bc[] array = c37adb342465be20dcf2c430c7d6aeb68;
			int num = Math.Abs(c8f1b13f3e698dc6357eae9af1da14536.GetHashCode()) % array.Length;
			int num2 = num;
			do
			{
				c421c20aed5f171eaf1d853159f1198bc c421c20aed5f171eaf1d853159f1198bc2 = array[num2];
				if (c421c20aed5f171eaf1d853159f1198bc2.c068b47ba1187b1dccad6a03226fb4abf == cb322f623ccc1eb32480f1ef2b9a823f7.c5e575b1e405b2dad96e3ae6b5455f3c9)
				{
					break;
				}
				if (c421c20aed5f171eaf1d853159f1198bc2.c068b47ba1187b1dccad6a03226fb4abf == cb322f623ccc1eb32480f1ef2b9a823f7.ccadcbc070ed232d8b8b87ab961ea7f15 || !c509560a58d69b736db5bf8d9407388b7.Equals(c8f1b13f3e698dc6357eae9af1da14536, c421c20aed5f171eaf1d853159f1198bc2.c72e2ac26518e130b84ec17acbbadf82c))
				{
					num2 = (num2 + 1) % array.Length;
					continue;
				}
				ref c421c20aed5f171eaf1d853159f1198bc reference = ref array[num2];
				reference = c0bd812ad217d9815a582b43d35554749;
			}
			while (num2 != num);
		}
		finally
		{
			c5428727f04c7bfdeb67e7ab91f81a9b1.c2e949f29d1f93161447a38cd6f450556();
		}
	}

	private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
	{
		return new c27bac404a3ce140128437624b882afb4(this);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
		return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
	}

	private IEnumerator<KeyValuePair<c228f9361f0da69cfe47819ff67ccfac0, cf44350890eed4b1efc197f803e97db3d>> c9214c251f696aee64e5becfb09d2aff1()
	{
		return new c27bac404a3ce140128437624b882afb4(this);
	}

	IEnumerator<KeyValuePair<c228f9361f0da69cfe47819ff67ccfac0, cf44350890eed4b1efc197f803e97db3d>> IEnumerable<KeyValuePair<c228f9361f0da69cfe47819ff67ccfac0, cf44350890eed4b1efc197f803e97db3d>>.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c9214c251f696aee64e5becfb09d2aff1
		return this.c9214c251f696aee64e5becfb09d2aff1();
	}

	public c27bac404a3ce140128437624b882afb4 c7b92ede539d48fe07a43280283af7eb4()
	{
		return new c27bac404a3ce140128437624b882afb4(this);
	}
}
