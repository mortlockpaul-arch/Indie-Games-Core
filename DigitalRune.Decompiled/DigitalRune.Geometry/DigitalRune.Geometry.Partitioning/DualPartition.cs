using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Partitioning;

/// <summary>
///       Represents a spatial partition that internally uses two spatial partitions to manage items: 
///       one for static/sleeping items and one for dynamic items.
///       </summary>
/// <typeparam name="T">The type of item in the spatial partition.</typeparam>
public class DualPartition<T> : BasePartition<T>
{
	private readonly HashSet<T> c8a27e072722dc3a2b471b90db07a24f2;

	private readonly List<T> cff30bfc77e164f33f11282a78548cb9d;

	private readonly List<T> c3a491ea6ac3e8bd70ce4d2db57d767ed;

	private List<T> c414f97db29848d9c1019c239b4278198;

	private List<T> cac21ced15f02bd0ba56a725eae7c5b1f;

	[CompilerGenerated]
	private ISpatialPartition<T> cf0b8fc8edb02bb4fe929fad924a3c6ac;

	[CompilerGenerated]
	private ISpatialPartition<T> c72d0ae6923ffcf8759a1e238752befed;

	internal ISpatialPartition<T> StaticPartition
	{
		[CompilerGenerated]
		get
		{
			return cf0b8fc8edb02bb4fe929fad924a3c6ac;
		}
		[CompilerGenerated]
		set
		{
			cf0b8fc8edb02bb4fe929fad924a3c6ac = value;
		}
	}

	internal ISpatialPartition<T> DynamicPartition
	{
		[CompilerGenerated]
		get
		{
			return c72d0ae6923ffcf8759a1e238752befed;
		}
		[CompilerGenerated]
		set
		{
			c72d0ae6923ffcf8759a1e238752befed = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Partitioning.DualPartition`1" /> class.
	///       </summary>
	public DualPartition()
	{
		StaticPartition = new DynamicAabbTree<T>
		{
			OptimizationPerFrame = 0.01f,
			EnableMotionPrediction = false
		};
		DynamicPartition = new DynamicAabbTree<T>
		{
			OptimizationPerFrame = 0f,
			EnableMotionPrediction = true
		};
		cff30bfc77e164f33f11282a78548cb9d = new List<T>();
		c3a491ea6ac3e8bd70ce4d2db57d767ed = new List<T>();
		c8a27e072722dc3a2b471b90db07a24f2 = new HashSet<T>();
		c414f97db29848d9c1019c239b4278198 = cff30bfc77e164f33f11282a78548cb9d;
		cac21ced15f02bd0ba56a725eae7c5b1f = c3a491ea6ac3e8bd70ce4d2db57d767ed;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Partitioning.DualPartition`1" /> class using the given
	///       pair of spatial partitions.
	///       </summary>
	/// <param name="staticPartition">
	///       The spatial partition used for static/sleeping objects.
	///       </param>
	/// <param name="dynamicPartition">
	///       The spatial partition used for dynamic partition.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="staticPartition" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dynamicPartition" /> is <see langword="null" />.
	///       </exception>
	public DualPartition(ISpatialPartition<T> staticPartition, ISpatialPartition<T> dynamicPartition)
	{
		if (staticPartition == null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(DualPartition<>._002Ector(ISpatialPartition<T>, ISpatialPartition<T>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11808));
				}
			}
		}
		if (dynamicPartition == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11839));
				}
			}
		}
		StaticPartition = staticPartition;
		DynamicPartition = dynamicPartition;
		cff30bfc77e164f33f11282a78548cb9d = new List<T>();
		c3a491ea6ac3e8bd70ce4d2db57d767ed = new List<T>();
		c8a27e072722dc3a2b471b90db07a24f2 = new HashSet<T>();
		c414f97db29848d9c1019c239b4278198 = cff30bfc77e164f33f11282a78548cb9d;
		cac21ced15f02bd0ba56a725eae7c5b1f = c3a491ea6ac3e8bd70ce4d2db57d767ed;
	}

	/// <summary>
	///       Gets overlaps between all items of this spatial partition and the items of another spatial
	///       partition.
	///       </summary>
	/// <param name="otherPartition">The spatial partition to test against.</param>
	/// <returns>
	///       All pairwise overlaps between items of this spatial partition and
	///       <paramref name="otherPartition" />. In each returned <see cref="T:DigitalRune.Collections.Pair`1" /> the first item 
	///       (see <see cref="P:DigitalRune.Collections.Pair`1.First" />) is from this partition and the second item (see
	///       <see cref="P:DigitalRune.Collections.Pair`1.Second" />) is from <paramref name="otherPartition" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="otherPartition" /> is <see langword="null" />.
	///       </exception>
	public override IEnumerable<Pair<T>> GetOverlaps(ISpatialPartition<T> otherPartition)
	{
		Update(forceRebuild: false);
		IEnumerable<Pair<T>> overlaps = StaticPartition.GetOverlaps(otherPartition);
		IEnumerable<Pair<T>> overlaps2 = DynamicPartition.GetOverlaps(otherPartition);
		return overlaps.Concat(overlaps2);
	}

	/// <summary>
	///       Gets overlaps between all items of this spatial partition and the items of another spatial 
	///       partition.
	///       </summary>
	/// <param name="scale">The scale of this spatial partition.</param>
	/// <param name="pose">The pose of this spatial partition.</param>
	/// <param name="otherPartition">The other spatial partition to test against.</param>
	/// <param name="otherScale">The scale of the <paramref name="otherPartition" />.</param>
	/// <param name="otherPose">The pose of the <paramref name="otherPartition" />.</param>
	/// <returns>
	///       All pairwise overlaps between items of this spatial partition and 
	///       <paramref name="otherPartition" />. In each returned <see cref="T:DigitalRune.Collections.Pair`1" /> the first item
	///       (see <see cref="P:DigitalRune.Collections.Pair`1.First" />) is from this partition and the second item (see 
	///       <see cref="P:DigitalRune.Collections.Pair`1.Second" />) is from <paramref name="otherPartition" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="otherPartition" /> is <see langword="null" />.
	///       </exception>
	public override IEnumerable<Pair<T>> GetOverlaps(Vector3F scale, Pose pose, ISpatialPartition<T> otherPartition, Vector3F otherScale, Pose otherPose)
	{
		Update(forceRebuild: false);
		IEnumerable<Pair<T>> overlaps = StaticPartition.GetOverlaps(scale, pose, otherPartition, otherScale, otherPose);
		IEnumerable<Pair<T>> overlaps2 = DynamicPartition.GetOverlaps(scale, pose, otherPartition, otherScale, otherPose);
		return overlaps.Concat(overlaps2);
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" /> derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override BasePartition<T> CreateInstanceCore()
	{
		return new DualPartition<T>();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.
	///       </summary>
	/// <param name="sourcePartition">The object to clone.</param>
	protected override void CloneCore(BasePartition<T> sourcePartition)
	{
		base.CloneCore(sourcePartition);
		DualPartition<T> dualPartition = (DualPartition<T>)sourcePartition;
		dualPartition.StaticPartition = (ISpatialPartition<T>)dualPartition.StaticPartition.Clone();
		dualPartition.DynamicPartition = (ISpatialPartition<T>)dualPartition.DynamicPartition.Clone();
	}

	internal override void c7800892d6d855908c9d25556ad846a02()
	{
		StaticPartition.Filter = Filter;
		DynamicPartition.Filter = Filter;
	}

	internal override void c0aa2b6671f7e92b9c129410d550f7b13()
	{
		StaticPartition.EnableSelfOverlaps = EnableSelfOverlaps;
		DynamicPartition.EnableSelfOverlaps = EnableSelfOverlaps;
	}

	internal override void ccb8f4732b3b508aae0aacb2b55c391cc()
	{
		StaticPartition.GetAabbForItem = GetAabbForItem;
		DynamicPartition.GetAabbForItem = GetAabbForItem;
	}

	internal override void ced5f5b0bd8302f3e8ade582954f3650d(bool ce8ca0a645441ac556aaf6b4574dbbb1f, HashSet<T> c049085d0fafa74bddedd7455b511e4d2, HashSet<T> c2176039f062c6700652655f4b1223258, HashSet<T> c7a74c15b192971583f3f676b15968063)
	{
		using (Dictionary<T, bool>.KeyCollection.Enumerator enumerator = c2176039f062c6700652655f4b1223258.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				if (c8a27e072722dc3a2b471b90db07a24f2.Remove(current))
				{
					StaticPartition.Remove(current);
					continue;
				}
				cac21ced15f02bd0ba56a725eae7c5b1f.Remove(current);
				DynamicPartition.Remove(current);
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(DualPartition<>.ced5f5b0bd8302f3e8ade582954f3650d(bool, HashSet<T>, HashSet<T>, HashSet<T>));
				}
				break;
			}
		}
		using (Dictionary<T, bool>.KeyCollection.Enumerator enumerator2 = c049085d0fafa74bddedd7455b511e4d2.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				T current2 = enumerator2.Current;
				c414f97db29848d9c1019c239b4278198.Add(current2);
				DynamicPartition.Add(current2);
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					goto end_IL_00b8;
				}
				continue;
				end_IL_00b8:
				break;
			}
		}
		if (c7a74c15b192971583f3f676b15968063 != null)
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
			using (Dictionary<T, bool>.KeyCollection.Enumerator enumerator3 = c7a74c15b192971583f3f676b15968063.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					T current3 = enumerator3.Current;
					if (c8a27e072722dc3a2b471b90db07a24f2.Remove(current3))
					{
						StaticPartition.Remove(current3);
						c414f97db29848d9c1019c239b4278198.Add(current3);
						DynamicPartition.Add(current3);
					}
					else
					{
						cac21ced15f02bd0ba56a725eae7c5b1f.Remove(current3);
						c414f97db29848d9c1019c239b4278198.Add(current3);
						DynamicPartition.Invalidate(current3);
					}
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						goto end_IL_0161;
					}
					continue;
					end_IL_0161:
					break;
				}
			}
			if (cac21ced15f02bd0ba56a725eae7c5b1f.Count > 0)
			{
				using (List<T>.Enumerator enumerator4 = cac21ced15f02bd0ba56a725eae7c5b1f.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						T current4 = enumerator4.Current;
						DynamicPartition.Remove(current4);
						StaticPartition.Add(current4);
						c8a27e072722dc3a2b471b90db07a24f2.Add(current4);
					}
					while (true)
					{
						switch (1)
						{
						case 0:
							break;
						default:
							goto end_IL_01da;
						}
						continue;
						end_IL_01da:
						break;
					}
				}
				cac21ced15f02bd0ba56a725eae7c5b1f.Clear();
			}
		}
		else
		{
			StaticPartition.Invalidate();
			DynamicPartition.Invalidate();
			foreach (T item in cac21ced15f02bd0ba56a725eae7c5b1f)
			{
				c414f97db29848d9c1019c239b4278198.Add(item);
			}
			cac21ced15f02bd0ba56a725eae7c5b1f.Clear();
		}
		MathHelper.Swap(ref c414f97db29848d9c1019c239b4278198, ref cac21ced15f02bd0ba56a725eae7c5b1f);
		StaticPartition.Update(ce8ca0a645441ac556aaf6b4574dbbb1f);
		DynamicPartition.Update(ce8ca0a645441ac556aaf6b4574dbbb1f);
		c527912fe7c25ec4be62695147b7edc1d();
		ce759ff2781e7c0601193b90d63f12c3c();
	}

	private void c527912fe7c25ec4be62695147b7edc1d()
	{
		bool flag = StaticPartition.Count > 0;
		bool flag2 = DynamicPartition.Count > 0;
		if (flag && flag2)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(DualPartition<>.c527912fe7c25ec4be62695147b7edc1d());
					}
					Aabb = Aabb.Merge(StaticPartition.Aabb, DynamicPartition.Aabb);
					return;
				}
			}
		}
		if (flag)
		{
			Aabb = StaticPartition.Aabb;
			return;
		}
		if (flag2)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					Aabb = DynamicPartition.Aabb;
					return;
				}
			}
		}
		Aabb = default;
	}

	private void ce759ff2781e7c0601193b90d63f12c3c()
	{
		if (!EnableSelfOverlaps)
		{
			return;
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
				__ldtoken(DualPartition<>.ce759ff2781e7c0601193b90d63f12c3c());
			}
			c65379642bd949dc7e978580b9649b9bc.Clear();
			if (StaticPartition is BasePartition<T>)
			{
				using Dictionary<Pair<T>, bool>.KeyCollection.Enumerator enumerator = ((BasePartition<T>)StaticPartition).c65379642bd949dc7e978580b9649b9bc.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Pair<T> current = enumerator.Current;
					c65379642bd949dc7e978580b9649b9bc.Add(current);
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						goto end_IL_007b;
					}
					continue;
					end_IL_007b:
					break;
				}
			}
			else
			{
				using IEnumerator<Pair<T>> enumerator2 = StaticPartition.GetOverlaps().GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Pair<T> current2 = enumerator2.Current;
					c65379642bd949dc7e978580b9649b9bc.Add(current2);
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						goto end_IL_00cf;
					}
					continue;
					end_IL_00cf:
					break;
				}
			}
			if (StaticPartition is BasePartition<T>)
			{
				foreach (Pair<T> item in ((BasePartition<T>)DynamicPartition).c65379642bd949dc7e978580b9649b9bc)
				{
					c65379642bd949dc7e978580b9649b9bc.Add(item);
				}
			}
			else
			{
				using IEnumerator<Pair<T>> enumerator4 = DynamicPartition.GetOverlaps().GetEnumerator();
				while (enumerator4.MoveNext())
				{
					Pair<T> current4 = enumerator4.Current;
					c65379642bd949dc7e978580b9649b9bc.Add(current4);
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						goto end_IL_017d;
					}
					continue;
					end_IL_017d:
					break;
				}
			}
			IEnumerator<Pair<T>> enumerator5 = StaticPartition.GetOverlaps(DynamicPartition).GetEnumerator();
			try
			{
				while (enumerator5.MoveNext())
				{
					Pair<T> current5 = enumerator5.Current;
					c65379642bd949dc7e978580b9649b9bc.Add(current5);
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						return;
					}
				}
			}
			finally
			{
				if (enumerator5 != null)
				{
					while (true)
					{
						switch (4)
						{
						case 0:
							continue;
						}
						enumerator5.Dispose();
						break;
					}
				}
			}
		}
	}

	/// <summary>
	///       Gets the items that touch the given AABB.
	///       </summary>
	/// <param name="aabb">The axis-aligned bounding box.</param>
	/// <returns>All items that touch the given AABB.</returns>
	public override IEnumerable<T> GetOverlaps(Aabb aabb)
	{
		Update(forceRebuild: false);
		IEnumerable<T> overlaps = StaticPartition.GetOverlaps(aabb);
		IEnumerable<T> overlaps2 = DynamicPartition.GetOverlaps(aabb);
		return overlaps.Concat(overlaps2);
	}

	/// <summary>
	///       Gets the items that touch the given ray.
	///       </summary>
	/// <param name="ray">The ray.</param>
	/// <returns>All items that are hit by the ray.</returns>
	public override IEnumerable<T> GetOverlaps(Ray ray)
	{
		Update(forceRebuild: false);
		IEnumerable<T> overlaps = StaticPartition.GetOverlaps(ray);
		IEnumerable<T> overlaps2 = DynamicPartition.GetOverlaps(ray);
		return overlaps.Concat(overlaps2);
	}
}
