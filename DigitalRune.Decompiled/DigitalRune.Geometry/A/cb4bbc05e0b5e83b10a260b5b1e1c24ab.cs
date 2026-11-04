using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DigitalRune;
using DigitalRune.Collections;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Geometry.Partitioning;
using DigitalRune.Geometry.Shapes;

namespace A;

internal class cb4bbc05e0b5e83b10a260b5b1e1c24ab
{
	private readonly CollisionDomain c245b81ceb0fc8e3cb159a268567f76bd;

	private cfd5199210e5d1b1eef9639935dc7441a<CollisionObject> c8789409a7033f4d35e037da8421aa449;

	private List<ContactSet> c6ae54e19b460425a8ce261485e9becfd = ResourcePools<ContactSet>.Lists.Obtain();

	private List<ContactSet> cfccfddc72aa41620f244a9fa9beac153 = ResourcePools<ContactSet>.Lists.Obtain();

	private ISpatialPartition<CollisionObject> c94412554568490f06675aa465d9fd626;

	[CompilerGenerated]
	private ContactSetCollection c2857fcde90793f1b91ed0032253c083a;

	[CompilerGenerated]
	private static Func<CollisionObject, Aabb> c49253b4520c1b7e183dfcc9c426c0283;

	public ContactSetCollection CandidatePairs
	{
		[CompilerGenerated]
		get
		{
			return c2857fcde90793f1b91ed0032253c083a;
		}
		[CompilerGenerated]
		protected set
		{
			c2857fcde90793f1b91ed0032253c083a = value;
		}
	}

	public ISpatialPartition<CollisionObject> c3ef1d9cc86c190e81836cf0c7f544d6f
	{
		get
		{
			return c94412554568490f06675aa465d9fd626;
		}
		set
		{
			if (value == null)
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
							__ldtoken(cb4bbc05e0b5e83b10a260b5b1e1c24ab.set_c3ef1d9cc86c190e81836cf0c7f544d6f(ISpatialPartition<CollisionObject>));
						}
						throw new ArgumentNullException();
					}
				}
			}
			if (value == c94412554568490f06675aa465d9fd626)
			{
				return;
			}
			if (c94412554568490f06675aa465d9fd626 != null)
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
				c94412554568490f06675aa465d9fd626.GetAabbForItem = null;
				c94412554568490f06675aa465d9fd626.Clear();
				if (c8789409a7033f4d35e037da8421aa449 != null)
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
					c8789409a7033f4d35e037da8421aa449.c706091e93d24d199da97520bab9f558d();
					c8789409a7033f4d35e037da8421aa449 = null;
				}
			}
			c94412554568490f06675aa465d9fd626 = value;
			ISpatialPartition<CollisionObject> spatialPartition = c94412554568490f06675aa465d9fd626;
			if (c49253b4520c1b7e183dfcc9c426c0283 == null)
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
				c49253b4520c1b7e183dfcc9c426c0283 = (CollisionObject c9bdbd655e343282211edce8c6b867eab) => c9bdbd655e343282211edce8c6b867eab.GeometricObject.Aabb;
			}
			spatialPartition.GetAabbForItem = c49253b4520c1b7e183dfcc9c426c0283;
			c94412554568490f06675aa465d9fd626.EnableSelfOverlaps = true;
			if (c94412554568490f06675aa465d9fd626 is cfd5199210e5d1b1eef9639935dc7441a<CollisionObject>)
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
				c8789409a7033f4d35e037da8421aa449 = (cfd5199210e5d1b1eef9639935dc7441a<CollisionObject>)c94412554568490f06675aa465d9fd626;
				c8789409a7033f4d35e037da8421aa449.c1bdb53c8c6209d3e6b68cb29eb6c1773(c906a14bfb0136bca6fd839cc6d64d983, c659ea2c6bb7ca570f9ee471be1ff7bf2, cde9e5961110a357cdd4b1f67a339c309);
			}
			if (CandidatePairs != null)
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
				using (Dictionary<Pair<CollisionObject>, ContactSet>.ValueCollection.Enumerator enumerator = CandidatePairs.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ContactSet current = enumerator.Current;
						foreach (Contact item in current)
						{
							item.Recycle();
						}
						current.Recycle();
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							goto end_IL_0182;
						}
						continue;
						end_IL_0182:
						break;
					}
				}
				CandidatePairs.Clear();
			}
			c94412554568490f06675aa465d9fd626.Clear();
			using List<CollisionObject>.Enumerator enumerator3 = c245b81ceb0fc8e3cb159a268567f76bd.CollisionObjects.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				CollisionObject current3 = enumerator3.Current;
				current3.Changed = true;
				c94412554568490f06675aa465d9fd626.Add(current3);
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					return;
				}
			}
		}
	}

	public cb4bbc05e0b5e83b10a260b5b1e1c24ab(CollisionDomain cb06f815b0a00f692cb944e3cae76a866)
	{
		c245b81ceb0fc8e3cb159a268567f76bd = cb06f815b0a00f692cb944e3cae76a866;
		c245b81ceb0fc8e3cb159a268567f76bd.CollisionObjects.CollectionChanged += c2bb1a51f74973ecd5a8191366e051fc2;
		c3ef1d9cc86c190e81836cf0c7f544d6f = new SweepAndPruneSpace<CollisionObject>();
		CandidatePairs = cdb80ee76c96fc0751dea8d15ee69fa47.cd119f100f88d00f8b167fa83dbda39cf.Obtain();
	}

	private void c2bb1a51f74973ecd5a8191366e051fc2(object c807f682152464d11dde83a885bf87124, CollectionChangedEventArgs<CollisionObject> c7fefeb91d4632901bd52a1f245b70db1)
	{
		if (c7fefeb91d4632901bd52a1f245b70db1.Action == CollectionChangedAction.Move)
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
						__ldtoken(cb4bbc05e0b5e83b10a260b5b1e1c24ab.c2bb1a51f74973ecd5a8191366e051fc2(object, CollectionChangedEventArgs<CollisionObject>));
					}
					return;
				}
			}
		}
		List<CollisionObject> oldItems = c7fefeb91d4632901bd52a1f245b70db1.OldItems;
		int count = oldItems.Count;
		for (int i = 0; i < count; i++)
		{
			CollisionObject item = oldItems[i];
			c3ef1d9cc86c190e81836cf0c7f544d6f.Remove(item);
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			List<CollisionObject> newItems = c7fefeb91d4632901bd52a1f245b70db1.NewItems;
			int count2 = newItems.Count;
			for (int j = 0; j < count2; j++)
			{
				CollisionObject item2 = newItems[j];
				c3ef1d9cc86c190e81836cf0c7f544d6f.Add(item2);
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					return;
				}
			}
		}
	}

	internal void c70a983c78ddcdb771f728f30964ecc15()
	{
		foreach (ContactSet item in cfccfddc72aa41620f244a9fa9beac153)
		{
			using (List<Contact>.Enumerator enumerator2 = item.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Contact current2 = enumerator2.Current;
					current2.Recycle();
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
						__ldtoken(cb4bbc05e0b5e83b10a260b5b1e1c24ab.c70a983c78ddcdb771f728f30964ecc15());
					}
					break;
				}
			}
			item.Recycle();
		}
		cfccfddc72aa41620f244a9fa9beac153.Clear();
	}

	internal void c322ef1ce205c6dbcf3a93ad899943435()
	{
		int count = c245b81ceb0fc8e3cb159a268567f76bd.CollisionObjects.Count;
		for (int i = 0; i < count; i++)
		{
			CollisionObject collisionObject = c245b81ceb0fc8e3cb159a268567f76bd.CollisionObjects[i];
			if (collisionObject.Changed)
			{
				c3ef1d9cc86c190e81836cf0c7f544d6f.Invalidate(collisionObject);
			}
		}
		c3ef1d9cc86c190e81836cf0c7f544d6f.Update(forceRebuild: false);
		if (c8789409a7033f4d35e037da8421aa449 != null)
		{
			return;
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(cb4bbc05e0b5e83b10a260b5b1e1c24ab.c322ef1ce205c6dbcf3a93ad899943435());
			}
			cc23cda8a270a47436e1d5166e798b49a();
			return;
		}
	}

	internal void c322ef1ce205c6dbcf3a93ad899943435(CollisionObject c9bdbd655e343282211edce8c6b867eab)
	{
		c3ef1d9cc86c190e81836cf0c7f544d6f.Invalidate(c9bdbd655e343282211edce8c6b867eab);
		c3ef1d9cc86c190e81836cf0c7f544d6f.Update(forceRebuild: false);
		if (c8789409a7033f4d35e037da8421aa449 != null)
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
				__ldtoken(cb4bbc05e0b5e83b10a260b5b1e1c24ab.c322ef1ce205c6dbcf3a93ad899943435(CollisionObject));
			}
			cc23cda8a270a47436e1d5166e798b49a();
			return;
		}
	}

	private void cc23cda8a270a47436e1d5166e798b49a()
	{
		IEnumerable<Pair<CollisionObject>> overlaps = c3ef1d9cc86c190e81836cf0c7f544d6f.GetOverlaps();
		if (overlaps is HashSet<Pair<CollisionObject>> hashSet)
		{
			foreach (Pair<CollisionObject> item2 in hashSet)
			{
				CandidatePairs.TryGet(item2, out var contactSet);
				if (contactSet == null)
				{
					contactSet = ContactSet.Create(item2.First, item2.Second);
					CandidatePairs.Add(contactSet);
				}
				contactSet.cff390782086d4775a28c5af1a8ec7667 = true;
			}
		}
		else
		{
			foreach (Pair<CollisionObject> item3 in overlaps)
			{
				CandidatePairs.TryGet(item3, out var contactSet2);
				if (contactSet2 == null)
				{
					contactSet2 = ContactSet.Create(item3.First, item3.Second);
					CandidatePairs.Add(contactSet2);
				}
				contactSet2.cff390782086d4775a28c5af1a8ec7667 = true;
			}
		}
		foreach (ContactSet candidatePair in CandidatePairs)
		{
			if (candidatePair.cff390782086d4775a28c5af1a8ec7667)
			{
				candidatePair.cff390782086d4775a28c5af1a8ec7667 = false;
			}
			else
			{
				c6ae54e19b460425a8ce261485e9becfd.Add(candidatePair);
			}
		}
		int count = c6ae54e19b460425a8ce261485e9becfd.Count;
		for (int i = 0; i < count; i++)
		{
			ContactSet item = c6ae54e19b460425a8ce261485e9becfd[i];
			CandidatePairs.Remove(item);
			cfccfddc72aa41620f244a9fa9beac153.Add(item);
		}
		c6ae54e19b460425a8ce261485e9becfd.Clear();
	}

	private void c906a14bfb0136bca6fd839cc6d64d983()
	{
		foreach (ContactSet candidatePair in CandidatePairs)
		{
			cfccfddc72aa41620f244a9fa9beac153.Add(candidatePair);
		}
		CandidatePairs.Clear();
	}

	private void c659ea2c6bb7ca570f9ee471be1ff7bf2(Pair<CollisionObject> c22655ba082a86d172e436abc6a4fdf11)
	{
		ContactSet contactSet = ContactSet.Create(c22655ba082a86d172e436abc6a4fdf11.First, c22655ba082a86d172e436abc6a4fdf11.Second);
		CandidatePairs.Add(contactSet);
	}

	private void cde9e5961110a357cdd4b1f67a339c309(Pair<CollisionObject> c22655ba082a86d172e436abc6a4fdf11)
	{
		if (CandidatePairs.TryGet(c22655ba082a86d172e436abc6a4fdf11, out var contactSet))
		{
			CandidatePairs.Remove(c22655ba082a86d172e436abc6a4fdf11);
			cfccfddc72aa41620f244a9fa9beac153.Add(contactSet);
		}
	}

	[CompilerGenerated]
	private static Aabb c52cc1d7ebd993feac13aeb87a489690b(CollisionObject c9bdbd655e343282211edce8c6b867eab)
	{
		return c9bdbd655e343282211edce8c6b867eab.GeometricObject.Aabb;
	}
}
