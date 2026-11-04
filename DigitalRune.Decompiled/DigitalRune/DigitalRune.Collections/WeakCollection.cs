using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Collections;

/// <summary>
///       Represents a collection of objects of type <typeparamref name="T" /> using weak references. 
///       Weak references are purged by iteration/count operations, not by add/remove operations.
///       </summary>
/// <typeparam name="T">The type of object to hold weak references to.</typeparam>
public sealed class WeakCollection<T> : ICollection<T>, IEnumerable<T>, IWeakCollection<T>, IEnumerable where T : class
{
	private readonly List<WeakReference> cd5f0fd0d2b324c4577c350f94adfec00;

	[CompilerGenerated]
	private static Func<WeakReference, T> c49253b4520c1b7e183dfcc9c426c0283;

	[CompilerGenerated]
	private static Func<T, bool> c19ae5197e8f1d77c5b9a5a5a3cba33d3;

	[CompilerGenerated]
	private static Func<T, bool> c422f799ed4ec89e25dd968008fe98786;

	[CompilerGenerated]
	private static Func<T, bool> c56450011eff7620c267636fce2acb8c4;

	/// <summary>
	///       Gets a sequence of live objects from the collection, causing a purge.
	///       </summary>
	public IEnumerable<T> LiveList
	{
		get
		{
			List<T> list = new List<T>(cd5f0fd0d2b324c4577c350f94adfec00.Count);
			list.AddRange(cd0145c1e94719d3efdfda58f15baef54);
			return list;
		}
	}

	/// <summary>
	///       Gets a complete sequence of objects from the collection. Does not cause a purge. Null 
	///       entries represent dead objects.
	///       </summary>
	public IEnumerable<T> CompleteList => cd5f0fd0d2b324c4577c350f94adfec00.Select((WeakReference c5078b1ca2bd86333c65cb625b996b618) => c5078b1ca2bd86333c65cb625b996b618.Target as T);

	/// <summary>
	///       Gets a sequence of live objects from the collection without causing a purge.
	///       </summary>
	public IEnumerable<T> LiveListWithoutPurge => CompleteList.Where((T c5078b1ca2bd86333c65cb625b996b618) => c5078b1ca2bd86333c65cb625b996b618 != null);

	/// <summary>
	///       Gets the number of live and dead entries in the collection. Does not cause a purge. O(1).
	///       </summary>
	public int CompleteCount => cd5f0fd0d2b324c4577c350f94adfec00.Count;

	/// <summary>
	///       Gets the number of dead entries in the collection. Does not cause a purge. O(n).
	///       </summary>
	public int DeadCount => CompleteList.Count((T c5078b1ca2bd86333c65cb625b996b618) => c5078b1ca2bd86333c65cb625b996b618 == null);

	/// <summary>
	///       Gets the number of live entries in the collection, causing a purge. O(n).
	///       </summary>
	public int LiveCount => cd0145c1e94719d3efdfda58f15baef54.Count();

	/// <summary>
	///       Gets the number of live entries in the collection without causing a purge. O(n).
	///       </summary>
	public int LiveCountWithoutPurge => CompleteList.Count((T c5078b1ca2bd86333c65cb625b996b618) => c5078b1ca2bd86333c65cb625b996b618 != null);

	private int cf796e8f8194a290fb92968fa480dfa46 => LiveCount;

	private bool c02aea6f65acc1ac2c5e3545134255a92 => false;

	private IEnumerable<T> cd0145c1e94719d3efdfda58f15baef54
	{
		get
		{
			int num = 0;
			for (int i = 0; i != cd5f0fd0d2b324c4577c350f94adfec00.Count; i++)
			{
				WeakReference weakReference = cd5f0fd0d2b324c4577c350f94adfec00[i];
				if (weakReference.Target is T val)
				{
					yield return val;
					if (i != num)
					{
						cd5f0fd0d2b324c4577c350f94adfec00[num] = cd5f0fd0d2b324c4577c350f94adfec00[i];
					}
					num++;
				}
			}
			cd5f0fd0d2b324c4577c350f94adfec00.RemoveRange(num, cd5f0fd0d2b324c4577c350f94adfec00.Count - num);
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.WeakCollection`1" /> class that is empty.
	///       </summary>
	public WeakCollection()
	{
		cd5f0fd0d2b324c4577c350f94adfec00 = new List<WeakReference>();
	}

	/// <summary>
	///       Adds a weak reference to an object to the collection. Does not cause a purge.
	///       </summary>
	/// <param name="item">The object to add a weak reference to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="item" /> is <see langword="null" />. The <see cref="T:System.WeakReference" /> does not 
	///       support null entries.
	///       </exception>
	public void Add(T item)
	{
		if (item == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2312));
		}
		cd5f0fd0d2b324c4577c350f94adfec00.Add(new WeakReference(item));
	}

	/// <summary>
	///       Removes a weak reference to an object from the collection. Does not cause a purge.
	///       </summary>
	/// <param name="item">The object to remove a weak reference to.</param>
	/// <returns>
	///   <see langword="true" /> if the object was found and removed; <see langword="false" /> if the 
	///       object was not found.
	///       </returns>
	public bool Remove(T item)
	{
		if (item == null)
		{
			return false;
		}
		for (int i = 0; i != cd5f0fd0d2b324c4577c350f94adfec00.Count; i++)
		{
			WeakReference weakReference = cd5f0fd0d2b324c4577c350f94adfec00[i];
			T val = weakReference.Target as T;
			if (val == item)
			{
				cd5f0fd0d2b324c4577c350f94adfec00.RemoveAt(i);
				return true;
			}
		}
		return false;
	}

	/// <summary>
	///       Removes all dead objects from the collection.
	///       </summary>
	public void Purge()
	{
		IEnumerator<T> enumerator = cd0145c1e94719d3efdfda58f15baef54.GetEnumerator();
		while (enumerator.MoveNext())
		{
		}
	}

	/// <summary>
	///       Empties the collection.
	///       </summary>
	public void Clear()
	{
		cd5f0fd0d2b324c4577c350f94adfec00.Clear();
	}

	/// <summary>
	///       Determines whether the collection contains a specific value.
	///       </summary>
	/// <param name="item">The object to locate.</param>
	/// <returns>
	///   <see langword="true" /> if the collection contains a specific value; <see langword="false" /> 
	///       if it does not.
	///       </returns>
	public bool Contains(T item)
	{
		return LiveListWithoutPurge.Contains(item);
	}

	private void cdf00ecccd4fc648afabc72fe2374fc19(T[] c6278a2c6debbe0cf2d35fcb5ff5fd0d5, int c400871890da984ad2947a014526712c4)
	{
		List<T> list = new List<T>(cd5f0fd0d2b324c4577c350f94adfec00.Count);
		list.AddRange(cd0145c1e94719d3efdfda58f15baef54);
		list.CopyTo(c6278a2c6debbe0cf2d35fcb5ff5fd0d5, c400871890da984ad2947a014526712c4);
	}

	void ICollection<T>.CopyTo(T[] c6278a2c6debbe0cf2d35fcb5ff5fd0d5, int c400871890da984ad2947a014526712c4)
	{
		//ILSpy generated this explicit interface implementation from .override directive in cdf00ecccd4fc648afabc72fe2374fc19
		this.cdf00ecccd4fc648afabc72fe2374fc19(c6278a2c6debbe0cf2d35fcb5ff5fd0d5, c400871890da984ad2947a014526712c4);
	}

	private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
	{
		return LiveList.GetEnumerator();
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c0843ff4e059c4450bad00d5ba0b6ef48
		return this.c0843ff4e059c4450bad00d5ba0b6ef48();
	}

	private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
	{
		return ((IEnumerable<T>)this).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
		return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
	}

	[CompilerGenerated]
	private static T c7edfaca51dd4840eb98f38fe99450e9d(WeakReference c5078b1ca2bd86333c65cb625b996b618)
	{
		return c5078b1ca2bd86333c65cb625b996b618.Target as T;
	}

	[CompilerGenerated]
	private static bool c701ab5443aeba43046cd3a74fc69b100(T c5078b1ca2bd86333c65cb625b996b618)
	{
		return c5078b1ca2bd86333c65cb625b996b618 != null;
	}

	[CompilerGenerated]
	private static bool cb4dd44467326e23b9857176a5b3925dc(T c5078b1ca2bd86333c65cb625b996b618)
	{
		return c5078b1ca2bd86333c65cb625b996b618 == null;
	}

	[CompilerGenerated]
	private static bool c80c1fb016b2b2e084479dd7a8d355a83(T c5078b1ca2bd86333c65cb625b996b618)
	{
		return c5078b1ca2bd86333c65cb625b996b618 != null;
	}
}
