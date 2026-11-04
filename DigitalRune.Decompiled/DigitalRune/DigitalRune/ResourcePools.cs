using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DigitalRune;

/// <summary>
///       Provides resource pools for reusable generic collections.
///       </summary>
/// <typeparam name="T">The type of the elements in the collection.</typeparam>
public static class ResourcePools<T>
{
	/// <summary>
	///       A resource pool containing collections of type <see cref="T:System.Collections.Generic.HashSet`1" />.
	///       </summary>
	public static readonly ResourcePool<HashSet<T>> HashSets;

	/// <summary>
	///       A resource pool containing collections of type <see cref="T:System.Collections.Generic.List`1" />.
	///       </summary>
	public static readonly ResourcePool<List<T>> Lists;

	/// <summary>
	///       A resource pool containing collections of type <see cref="T:System.Collections.Generic.Stack`1" />.
	///       </summary>
	public static readonly ResourcePool<Stack<T>> Stacks;

	[CompilerGenerated]
	private static Func<HashSet<T>> c83ce51cca2c9a5b9ef62a19a0014722a;

	[CompilerGenerated]
	private static Action<HashSet<T>> c56450011eff7620c267636fce2acb8c4;

	[CompilerGenerated]
	private static Func<List<T>> c73f073436b4c66eae523762159132e8d;

	[CompilerGenerated]
	private static Action<List<T>> c52f93b6e16dc37ce8233ad11ec6b3be7;

	[CompilerGenerated]
	private static Func<Stack<T>> c1c362a1a59d83efd7c796b26af77bbcc;

	[CompilerGenerated]
	private static Action<Stack<T>> c8dfeca5d3887b369bda6427cfb2884e2;

	static ResourcePools()
	{
		Func<HashSet<T>> create = () => new HashSet<T>();
		if (c56450011eff7620c267636fce2acb8c4 == null)
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
			if (1 == 0)
			{
				__ldtoken(ResourcePools<>._002Ecctor());
			}
			c56450011eff7620c267636fce2acb8c4 = (HashSet<T> cb4d8415d192e6216e9129cc4adc2ec81) =>
			{
				cb4d8415d192e6216e9129cc4adc2ec81.Clear();
			};
		}
		HashSets = new ResourcePool<HashSet<T>>(create, null, c56450011eff7620c267636fce2acb8c4);
		if (c73f073436b4c66eae523762159132e8d == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			c73f073436b4c66eae523762159132e8d = () => new List<T>();
		}
		Func<List<T>> create2 = c73f073436b4c66eae523762159132e8d;
		if (c52f93b6e16dc37ce8233ad11ec6b3be7 == null)
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
			c52f93b6e16dc37ce8233ad11ec6b3be7 = (List<T> c9cc70b23e19e1a842fac3e03e9b3105f) =>
			{
				c9cc70b23e19e1a842fac3e03e9b3105f.Clear();
			};
		}
		Lists = new ResourcePool<List<T>>(create2, null, c52f93b6e16dc37ce8233ad11ec6b3be7);
		Func<Stack<T>> create3 = () => new Stack<T>();
		if (c8dfeca5d3887b369bda6427cfb2884e2 == null)
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
			c8dfeca5d3887b369bda6427cfb2884e2 = (Stack<T> c63511248710b2604ad15f716f6701d87) =>
			{
				c63511248710b2604ad15f716f6701d87.Clear();
			};
		}
		Stacks = new ResourcePool<Stack<T>>(create3, null, c8dfeca5d3887b369bda6427cfb2884e2);
	}

	[CompilerGenerated]
	private static HashSet<T> c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new HashSet<T>();
	}

	[CompilerGenerated]
	private static void c4c5cc4eb6785fd907a1974db5b42eeb5(HashSet<T> cb4d8415d192e6216e9129cc4adc2ec81)
	{
		cb4d8415d192e6216e9129cc4adc2ec81.Clear();
	}

	[CompilerGenerated]
	private static List<T> cae916c2d2e636f87174a99fc7a154c7f()
	{
		return new List<T>();
	}

	[CompilerGenerated]
	private static void c2eacb42c577fd179d20d5a4024070280(List<T> c9cc70b23e19e1a842fac3e03e9b3105f)
	{
		c9cc70b23e19e1a842fac3e03e9b3105f.Clear();
	}

	[CompilerGenerated]
	private static Stack<T> cabc7d9463a3aca5199cf3ee0115097e1()
	{
		return new Stack<T>();
	}

	[CompilerGenerated]
	private static void c7e37b87d8de7f0a82661edb092462f99(Stack<T> c63511248710b2604ad15f716f6701d87)
	{
		c63511248710b2604ad15f716f6701d87.Clear();
	}
}
