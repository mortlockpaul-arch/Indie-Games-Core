using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DigitalRune.Mathematics;

namespace A;

internal class c3813009c1171253be56c785c7a4646a7 : IComparer<c972bb04a7023e8f3ae8a26639588d00f>
{
	[CompilerGenerated]
	private c972bb04a7023e8f3ae8a26639588d00f[] c9c0f3d0a34f10be9296c6750e52a5e01;

	[CompilerGenerated]
	private int cf87c61fe64f20acbb43be20e21f7afe5;

	public c972bb04a7023e8f3ae8a26639588d00f[] Elements
	{
		[CompilerGenerated]
		get
		{
			return c9c0f3d0a34f10be9296c6750e52a5e01;
		}
		[CompilerGenerated]
		private set
		{
			c9c0f3d0a34f10be9296c6750e52a5e01 = value;
		}
	}

	public int NumberOfUnions
	{
		[CompilerGenerated]
		get
		{
			return cf87c61fe64f20acbb43be20e21f7afe5;
		}
		[CompilerGenerated]
		private set
		{
			cf87c61fe64f20acbb43be20e21f7afe5 = value;
		}
	}

	public c3813009c1171253be56c785c7a4646a7()
	{
		Elements = new c972bb04a7023e8f3ae8a26639588d00f[256];
	}

	public void cf06348a19a922c6295282d49671cbe77(int c0520b795a9d959ae18c1bdbf183de8ee)
	{
		if (c0520b795a9d959ae18c1bdbf183de8ee > 256)
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
				__ldtoken(c3813009c1171253be56c785c7a4646a7.cf06348a19a922c6295282d49671cbe77(int));
			}
			if (c0520b795a9d959ae18c1bdbf183de8ee <= Elements.Length)
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
				if (c0520b795a9d959ae18c1bdbf183de8ee * 4 >= Elements.Length)
				{
					goto IL_0065;
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
			uint num = MathHelper.Bitmask((uint)c0520b795a9d959ae18c1bdbf183de8ee) + 1;
			Elements = new c972bb04a7023e8f3ae8a26639588d00f[2 * num];
		}
		goto IL_0065;
		IL_0065:
		for (int i = 0; i < c0520b795a9d959ae18c1bdbf183de8ee; i++)
		{
			ref c972bb04a7023e8f3ae8a26639588d00f reference = ref Elements[i];
			reference = new c972bb04a7023e8f3ae8a26639588d00f(i, 1);
		}
		NumberOfUnions = c0520b795a9d959ae18c1bdbf183de8ee;
	}

	public bool c85821338908d46f63fe87290a04cff3e(int c09fd01352a8161308ebdc5dbb04ebc15, int ce960b2607d7897e33dadb7dabd76272d)
	{
		return cce0f90a395df190a07fc3d56126999ae(c09fd01352a8161308ebdc5dbb04ebc15) == cce0f90a395df190a07fc3d56126999ae(ce960b2607d7897e33dadb7dabd76272d);
	}

	public int Compare(c972bb04a7023e8f3ae8a26639588d00f elementA, c972bb04a7023e8f3ae8a26639588d00f elementB)
	{
		return elementA.cb4ba6e6e9c6a11b6723dbb2c70241460 - elementB.cb4ba6e6e9c6a11b6723dbb2c70241460;
	}

	public int cce0f90a395df190a07fc3d56126999ae(int c09fd01352a8161308ebdc5dbb04ebc15)
	{
		int cb4ba6e6e9c6a11b6723dbb2c = Elements[c09fd01352a8161308ebdc5dbb04ebc15].cb4ba6e6e9c6a11b6723dbb2c70241460;
		while (c09fd01352a8161308ebdc5dbb04ebc15 != cb4ba6e6e9c6a11b6723dbb2c)
		{
			ref c972bb04a7023e8f3ae8a26639588d00f reference = ref Elements[c09fd01352a8161308ebdc5dbb04ebc15];
			reference = Elements[cb4ba6e6e9c6a11b6723dbb2c];
			c09fd01352a8161308ebdc5dbb04ebc15 = cb4ba6e6e9c6a11b6723dbb2c;
			cb4ba6e6e9c6a11b6723dbb2c = Elements[c09fd01352a8161308ebdc5dbb04ebc15].cb4ba6e6e9c6a11b6723dbb2c70241460;
		}
		return c09fd01352a8161308ebdc5dbb04ebc15;
	}

	public int ce03e879f51e03e85f1205d3b0409fb78(int c09fd01352a8161308ebdc5dbb04ebc15)
	{
		return Elements[cce0f90a395df190a07fc3d56126999ae(c09fd01352a8161308ebdc5dbb04ebc15)].c4b2dab96343831e7df85372853e51f0e;
	}

	public void c9d6febe2f42deb115290757a0772d244(int c09fd01352a8161308ebdc5dbb04ebc15, int ce960b2607d7897e33dadb7dabd76272d)
	{
		int num = cce0f90a395df190a07fc3d56126999ae(c09fd01352a8161308ebdc5dbb04ebc15);
		int num2 = cce0f90a395df190a07fc3d56126999ae(ce960b2607d7897e33dadb7dabd76272d);
		if (num != num2)
		{
			int c4b2dab96343831e7df85372853e51f0e = Elements[num].c4b2dab96343831e7df85372853e51f0e;
			int c4b2dab96343831e7df85372853e51f0e2 = Elements[num2].c4b2dab96343831e7df85372853e51f0e;
			c972bb04a7023e8f3ae8a26639588d00f c972bb04a7023e8f3ae8a26639588d00f2 = ((c4b2dab96343831e7df85372853e51f0e >= c4b2dab96343831e7df85372853e51f0e2) ? new c972bb04a7023e8f3ae8a26639588d00f(num, c4b2dab96343831e7df85372853e51f0e + c4b2dab96343831e7df85372853e51f0e2) : new c972bb04a7023e8f3ae8a26639588d00f(num2, c4b2dab96343831e7df85372853e51f0e + c4b2dab96343831e7df85372853e51f0e2));
			ref c972bb04a7023e8f3ae8a26639588d00f reference = ref Elements[num];
			reference = (Elements[num2] = c972bb04a7023e8f3ae8a26639588d00f2);
			NumberOfUnions--;
		}
	}
}
