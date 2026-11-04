using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Partitioning;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Threading;

namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       A collision domain that manages collision objects.
///       </summary>
public class CollisionDomain
{
	[CompilerGenerated]
	private sealed class c810f724ed208fbe32b0376955a617ac7 : IEnumerable<CollisionObject>, IEnumerator<CollisionObject>, IEnumerable, IEnumerator, IDisposable
	{
		private CollisionObject cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public CollisionDomain c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public CollisionObject c9bdbd655e343282211edce8c6b867eab;

		public CollisionObject c9932429c8066f0d42d6974e6af4e7c30;

		public ContactSet c88932e1efcae73c62cf59e238c17f5b9;

		public CollisionObject cb4b661f2233b257b79292e30fe705577;

		public IEnumerator<ContactSet> c908c20a7dc77624948085be2fc57df8d;

		CollisionObject IEnumerator<CollisionObject>.Current
		{
			[DebuggerHidden]
			get
			{
				return cd179d7f18da070d164af6afcb0af45b5;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return cd179d7f18da070d164af6afcb0af45b5;
			}
		}

		[DebuggerHidden]
		public c810f724ed208fbe32b0376955a617ac7(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<CollisionObject> c23b920021bef9b5f74a33461d2be0f33()
		{
			c810f724ed208fbe32b0376955a617ac7 c810f724ed208fbe32b0376955a617ac8;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
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
				if (1 == 0)
				{
					__ldtoken(c810f724ed208fbe32b0376955a617ac7.c23b920021bef9b5f74a33461d2be0f33());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				c810f724ed208fbe32b0376955a617ac8 = this;
			}
			else
			{
				c810f724ed208fbe32b0376955a617ac8 = new c810f724ed208fbe32b0376955a617ac7(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			c810f724ed208fbe32b0376955a617ac8.c9bdbd655e343282211edce8c6b867eab = c9932429c8066f0d42d6974e6af4e7c30;
			return c810f724ed208fbe32b0376955a617ac8;
		}

		IEnumerator<CollisionObject> IEnumerable<CollisionObject>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c23b920021bef9b5f74a33461d2be0f33
			return this.c23b920021bef9b5f74a33461d2be0f33();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c23b920021bef9b5f74a33461d2be0f33();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
			return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
		}

		private bool MoveNext()
		{
			try
			{
				switch (c94f69126adc708dfc5f5fd5b63c52835)
				{
				case 0:
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					c908c20a7dc77624948085be2fc57df8d = c1b0bbfd4af1b6a4c0c71a5dd3c509812.GetContacts(c9bdbd655e343282211edce8c6b867eab).GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
					goto IL_00c4;
				case 2:
					{
						c94f69126adc708dfc5f5fd5b63c52835 = 1;
						goto IL_00c4;
					}
					IL_00c4:
					if (c908c20a7dc77624948085be2fc57df8d.MoveNext())
					{
						c88932e1efcae73c62cf59e238c17f5b9 = c908c20a7dc77624948085be2fc57df8d.Current;
						CollisionObject collisionObject;
						if (c88932e1efcae73c62cf59e238c17f5b9.ObjectA != c9bdbd655e343282211edce8c6b867eab)
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
								__ldtoken(c810f724ed208fbe32b0376955a617ac7.MoveNext());
							}
							collisionObject = c88932e1efcae73c62cf59e238c17f5b9.ObjectA;
						}
						else
						{
							collisionObject = c88932e1efcae73c62cf59e238c17f5b9.ObjectB;
						}
						cb4b661f2233b257b79292e30fe705577 = collisionObject;
						cd179d7f18da070d164af6afcb0af45b5 = cb4b661f2233b257b79292e30fe705577;
						c94f69126adc708dfc5f5fd5b63c52835 = 2;
						return true;
					}
					c6b2b7bb0138c7a754f602e1325ab5a3a();
					break;
				}
				return false;
			}
			catch
			{
				//try-fault
				c087ddb9300ad4d53672b7a60e6d76672();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		private void c9106c2889b15ecfa28d3af82494ec7bc()
		{
			throw new NotSupportedException();
		}

		void IEnumerator.Reset()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c9106c2889b15ecfa28d3af82494ec7bc
			this.c9106c2889b15ecfa28d3af82494ec7bc();
		}

		private void c087ddb9300ad4d53672b7a60e6d76672()
		{
			switch (c94f69126adc708dfc5f5fd5b63c52835)
			{
			case 1:
			case 2:
				try
				{
					break;
				}
				finally
				{
					c6b2b7bb0138c7a754f602e1325ab5a3a();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void c6b2b7bb0138c7a754f602e1325ab5a3a()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			if (c908c20a7dc77624948085be2fc57df8d == null)
			{
				return;
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
					__ldtoken(c810f724ed208fbe32b0376955a617ac7.c6b2b7bb0138c7a754f602e1325ab5a3a());
				}
				c908c20a7dc77624948085be2fc57df8d.Dispose();
				return;
			}
		}
	}

	private readonly Dictionary<CollisionObject, ContactSet> c324608b185f3ba8d0cd53192cf8b5629 = new Dictionary<CollisionObject, ContactSet>();

	private KeyValuePair<CollisionObject, ContactSet>[] c88c6c77a07639babd08613eb38cf5957 = new KeyValuePair<CollisionObject, ContactSet>[4];

	private readonly Action<int> cf5629aae9c89f6d0f57968ecf6cafbaf;

	private float cba181d738bf534c2390ad3b4ddf23601;

	private readonly List<ContactSet> cbd94166bbc2a2acf2ac050d1c7a14d05 = new List<ContactSet>();

	[CompilerGenerated]
	private CollisionDetection ce318f76508d8601dc346daf3ba322cfa;

	[CompilerGenerated]
	private CollisionObjectCollection cdd7a7d35ff457bbccfb0e50ea393925c;

	[CompilerGenerated]
	private bool cdebab1aab2704b0f85ba208c682c1a31;

	[CompilerGenerated]
	private ContactSetCollection c960ca913fa2c3c2a450a2f83cb8fbf3b;

	[CompilerGenerated]
	private cb4bbc05e0b5e83b10a260b5b1e1c24ab c32d2afd0acd68a0d79aff278d9ce1a17;

	/// <summary>
	///       Gets the collision detection service.
	///       </summary>
	public CollisionDetection CollisionDetection
	{
		[CompilerGenerated]
		get
		{
			return ce318f76508d8601dc346daf3ba322cfa;
		}
		[CompilerGenerated]
		private set
		{
			ce318f76508d8601dc346daf3ba322cfa = value;
		}
	}

	/// <summary>
	///       Gets a collection of collision objects that are managed in this collision domain.
	///       </summary>
	public CollisionObjectCollection CollisionObjects
	{
		[CompilerGenerated]
		get
		{
			return cdd7a7d35ff457bbccfb0e50ea393925c;
		}
		[CompilerGenerated]
		private set
		{
			cdd7a7d35ff457bbccfb0e50ea393925c = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether multithreading is enabled.
	///       </summary>
	public bool EnableMultithreading
	{
		[CompilerGenerated]
		get
		{
			return cdebab1aab2704b0f85ba208c682c1a31;
		}
		[CompilerGenerated]
		set
		{
			cdebab1aab2704b0f85ba208c682c1a31 = value;
		}
	}

	/// <summary>
	///       Gets or sets the <see cref="T:DigitalRune.Geometry.Partitioning.ISpatialPartition`1" /> that is used for the broad phase of
	///       the collision detection.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public ISpatialPartition<CollisionObject> BroadPhase
	{
		get
		{
			return InternalBroadPhase.c3ef1d9cc86c190e81836cf0c7f544d6f;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			InternalBroadPhase.c3ef1d9cc86c190e81836cf0c7f544d6f = value;
		}
	}

	/// <summary>
	///       Gets a collection with all contacts found in the collision domain.
	///       </summary>
	public ContactSetCollection ContactSets
	{
		[CompilerGenerated]
		get
		{
			return c960ca913fa2c3c2a450a2f83cb8fbf3b;
		}
		[CompilerGenerated]
		private set
		{
			c960ca913fa2c3c2a450a2f83cb8fbf3b = value;
		}
	}

	internal cb4bbc05e0b5e83b10a260b5b1e1c24ab InternalBroadPhase
	{
		[CompilerGenerated]
		get
		{
			return c32d2afd0acd68a0d79aff278d9ce1a17;
		}
		[CompilerGenerated]
		private set
		{
			c32d2afd0acd68a0d79aff278d9ce1a17 = value;
		}
	}

	static CollisionDomain()
	{
		TypeInitializer.Initialize(ccb48cf63175e6724337dcebf74566228.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(12, ccb48cf63175e6724337dcebf74566228.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.CollisionDomain" /> class.
	///       </summary>
	public CollisionDomain()
		: this(new CollisionDetection())
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.CollisionDomain" /> class.
	///       </summary>
	/// <param name="collisionDetection">
	///       The collision detection instance that defines the settings (tolerance values,
	///       collision algorithm matrix, etc.) that this collision domain should use.
	///       </param>
	public CollisionDomain(CollisionDetection collisionDetection)
	{
		if (collisionDetection == null)
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
			if (1 == 0)
			{
				__ldtoken(CollisionDomain._002Ector(CollisionDetection));
			}
			collisionDetection = new CollisionDetection();
		}
		CollisionDetection = collisionDetection;
		CollisionDetection.cfb5e1c5b2b7d7015e55d4c648b31473c += c58a5fa51280a5beb38f93c24fd9567fd;
		CollisionObjects = new CollisionObjectCollection();
		CollisionObjects.CollectionChanged += c2bb1a51f74973ecd5a8191366e051fc2;
		ContactSets = cdb80ee76c96fc0751dea8d15ee69fa47.cd119f100f88d00f8b167fa83dbda39cf.Obtain();
		InternalBroadPhase = new cb4bbc05e0b5e83b10a260b5b1e1c24ab(this);
		EnableMultithreading = Environment.ProcessorCount > 1;
		cf5629aae9c89f6d0f57968ecf6cafbaf = c323401958c0832e019684ecfd47ba37d;
	}

	private static bool c773a59ff17bc2e1daf8a4ebbee35ecb5(ContactSet c3cdd6711e3e96d722a86b2bfd486c923)
	{
		if (!c3cdd6711e3e96d722a86b2bfd486c923.ObjectA.Changed)
		{
			return c3cdd6711e3e96d722a86b2bfd486c923.ObjectB.Changed;
		}
		return true;
	}

	private void c58a5fa51280a5beb38f93c24fd9567fd(object c807f682152464d11dde83a885bf87124, EventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		using Dictionary<Pair<CollisionObject>, ContactSet>.ValueCollection.Enumerator enumerator = InternalBroadPhase.CandidatePairs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ContactSet current = enumerator.Current;
			current.c55dbd29b4a90a73bb93fd94db8e41505 = -1;
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(CollisionDomain.c58a5fa51280a5beb38f93c24fd9567fd(object, EventArgs));
			}
			return;
		}
	}

	private void c2bb1a51f74973ecd5a8191366e051fc2(object c807f682152464d11dde83a885bf87124, CollectionChangedEventArgs<CollisionObject> c7fefeb91d4632901bd52a1f245b70db1)
	{
		if (c7fefeb91d4632901bd52a1f245b70db1.Action == CollectionChangedAction.Move)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CollisionDomain.c2bb1a51f74973ecd5a8191366e051fc2(object, CollectionChangedEventArgs<CollisionObject>));
					}
					return;
				}
			}
		}
		List<CollisionObject> oldItems = c7fefeb91d4632901bd52a1f245b70db1.OldItems;
		int count = oldItems.Count;
		for (int i = 0; i < count; i++)
		{
			CollisionObject collisionObject = oldItems[i];
			collisionObject.Domain = null;
			ContactSets.Remove(collisionObject);
			c324608b185f3ba8d0cd53192cf8b5629.Remove(collisionObject);
		}
		List<CollisionObject> newItems = c7fefeb91d4632901bd52a1f245b70db1.NewItems;
		int count2 = newItems.Count;
		for (int j = 0; j < count2; j++)
		{
			CollisionObject collisionObject2 = newItems[j];
			collisionObject2.Domain = this;
			if (collisionObject2.GeometricObject == null)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8319));
					}
				}
			}
			if (collisionObject2.c0dbe012fb6e35eff096504ebd112177b)
			{
				c324608b185f3ba8d0cd53192cf8b5629.Add(collisionObject2, null);
			}
		}
	}

	private bool c55dbd29b4a90a73bb93fd94db8e41505(CollisionObject c6e4150c82a8b60982e648f0411087959, CollisionObject cb9065353c960dfabb2c6812b8400ab32)
	{
		if (c6e4150c82a8b60982e648f0411087959.Enabled && cb9065353c960dfabb2c6812b8400ab32.Enabled)
		{
			if (CollisionDetection.CollisionFilter != null)
			{
				return CollisionDetection.CollisionFilter.Filter(new Pair<CollisionObject>(c6e4150c82a8b60982e648f0411087959, cb9065353c960dfabb2c6812b8400ab32));
			}
			return true;
		}
		return false;
	}

	internal bool c55dbd29b4a90a73bb93fd94db8e41505(ContactSet cfc73391dcf53c37de15d54e2434d0199)
	{
		CollisionObject objectA = cfc73391dcf53c37de15d54e2434d0199.ObjectA;
		if (!objectA.Enabled)
		{
			return false;
		}
		CollisionObject objectB = cfc73391dcf53c37de15d54e2434d0199.ObjectB;
		if (!objectB.Enabled)
		{
			return false;
		}
		switch (cfc73391dcf53c37de15d54e2434d0199.c55dbd29b4a90a73bb93fd94db8e41505)
		{
		case 1:
			return true;
		case 0:
			return false;
		default:
		{
			bool flag = CollisionDetection.CollisionFilter == null || CollisionDetection.CollisionFilter.Filter(new Pair<CollisionObject>(objectA, objectB));
			cfc73391dcf53c37de15d54e2434d0199.c55dbd29b4a90a73bb93fd94db8e41505 = (flag ? 1 : 0);
			return flag;
		}
		}
	}

	/// <summary>
	///       Gets all contacts of the given <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />.
	///       </summary>
	/// <param name="collisionObject">The collision object.</param>
	/// <returns>
	///       All <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />s where <paramref name="collisionObject" /> is involved.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="collisionObject" /> is <see langword="null" />.
	///       </exception>
	public IEnumerable<ContactSet> GetContacts(CollisionObject collisionObject)
	{
		if (collisionObject == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CollisionDomain.GetContacts(CollisionObject));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8529));
				}
			}
		}
		if (collisionObject.Domain == this)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					return ContactSets.GetContacts(collisionObject);
				}
			}
		}
		if (!collisionObject.Enabled)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					return Enumerable.Empty<ContactSet>();
				}
			}
		}
		ContactSetCollection contactSetCollection = cdb80ee76c96fc0751dea8d15ee69fa47.cd119f100f88d00f8b167fa83dbda39cf.Obtain();
		IEnumerable<CollisionObject> overlaps;
		if (collisionObject.cedb7bba503d90238311e100ff7d9069a)
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
			RayShape rayShape = (RayShape)collisionObject.GeometricObject.Shape;
			Vector3F ceabb47d7d959545d89c4300eabbdd7c = collisionObject.GeometricObject.Scale;
			Pose cbc1b7e0b011ad6dcdb8beb392ec7a = collisionObject.GeometricObject.Pose;
			Ray ray = new Ray(rayShape);
			ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
			ray.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
			overlaps = BroadPhase.GetOverlaps(ray);
		}
		else
		{
			overlaps = BroadPhase.GetOverlaps(collisionObject);
		}
		IEnumerator<CollisionObject> enumerator = overlaps.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				CollisionObject current = enumerator.Current;
				if (!c55dbd29b4a90a73bb93fd94db8e41505(collisionObject, current))
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
				ContactSet contacts = CollisionDetection.AlgorithmMatrix[collisionObject, current].GetContacts(collisionObject, current);
				if (!contacts.HaveContact)
				{
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
				CollisionObject collisionObject2 = null;
				if (contacts.ObjectA.c0dbe012fb6e35eff096504ebd112177b)
				{
					collisionObject2 = contacts.ObjectA;
				}
				else if (contacts.ObjectB.c0dbe012fb6e35eff096504ebd112177b)
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
					collisionObject2 = contacts.ObjectB;
				}
				if (collisionObject2 != null)
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
					c324608b185f3ba8d0cd53192cf8b5629.TryGetValue(collisionObject2, out var value);
					if (value != null)
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
						if (value.Count != 0)
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
							if (contacts.Count <= 0 || !(contacts[0].PenetrationDepth < value[0].PenetrationDepth))
							{
								continue;
							}
							while (true)
							{
								switch (7)
								{
								case 0:
									continue;
								}
								break;
							}
						}
					}
					if (collisionObject2 == collisionObject)
					{
						c324608b185f3ba8d0cd53192cf8b5629[collisionObject2] = contacts;
					}
					contactSetCollection.Remove(value);
					contactSetCollection.Add(contacts);
				}
				else
				{
					contactSetCollection.Add(contacts);
				}
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					goto end_IL_0255;
				}
				continue;
				end_IL_0255:
				break;
			}
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					enumerator.Dispose();
					break;
				}
			}
		}
		if (collisionObject.c0dbe012fb6e35eff096504ebd112177b)
		{
			c324608b185f3ba8d0cd53192cf8b5629.Remove(collisionObject);
		}
		ContactSet[] result = contactSetCollection.ToArray();
		cdb80ee76c96fc0751dea8d15ee69fa47.cd119f100f88d00f8b167fa83dbda39cf.Recycle(contactSetCollection);
		return result;
	}

	/// <summary>
	///       Gets the contacts for the given <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" /> pair.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <returns>
	///       A <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> describing the contact information if <paramref name="objectA" />
	///       and <paramref name="objectB" /> are intersecting; otherwise, <see langword="null" /> if the
	///       objects are separated.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	public ContactSet GetContacts(CollisionObject objectA, CollisionObject objectB)
	{
		if (objectA == null)
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
						__ldtoken(CollisionDomain.GetContacts(CollisionObject, CollisionObject));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
				}
			}
		}
		if (objectB == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
		}
		if (objectA.Domain == this)
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
			if (objectB.Domain == this)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						return ContactSets.GetContacts(objectA, objectB);
					}
				}
			}
		}
		ContactSet contactSet = null;
		if (c55dbd29b4a90a73bb93fd94db8e41505(objectA, objectB))
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
			if (cd0cfe897426aab73c90b8217df390607(objectA, objectB) && ce9b11ba3e3773f213dd90a03042dcbe8(objectA, objectB))
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
				contactSet = CollisionDetection.AlgorithmMatrix[objectA, objectB].GetContacts(objectA, objectB);
			}
		}
		if (contactSet == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					return null;
				}
			}
		}
		if (!contactSet.HaveContact)
		{
			contactSet.Recycle();
			return null;
		}
		CollisionObject collisionObject = null;
		if (objectA.c0dbe012fb6e35eff096504ebd112177b)
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
			collisionObject = objectA;
		}
		else if (objectB.c0dbe012fb6e35eff096504ebd112177b)
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
			collisionObject = objectB;
		}
		if (collisionObject != null)
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
			foreach (ContactSet contact in GetContacts(collisionObject))
			{
				if (contact.Count <= 0)
				{
					continue;
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				if (contactSet.Count != 0)
				{
					if (!(contact[0].PenetrationDepth < contactSet[0].PenetrationDepth))
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
				}
				contactSet.Recycle();
				return null;
			}
		}
		return contactSet;
	}

	/// <summary>
	///       Gets all <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s that have contact with the given object.
	///       </summary>
	/// <param name="collisionObject">The collision object.</param>
	/// <returns>
	///       All <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s that have contact with 
	///       <paramref name="collisionObject" />.
	///       </returns>
	public IEnumerable<CollisionObject> GetContactObjects(CollisionObject collisionObject)
	{
		IEnumerator<ContactSet> enumerator = GetContacts(collisionObject).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				ContactSet current = enumerator.Current;
				CollisionObject collisionObject2;
				if (current.ObjectA != collisionObject)
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
						__ldtoken(c810f724ed208fbe32b0376955a617ac7.MoveNext());
					}
					collisionObject2 = current.ObjectA;
				}
				else
				{
					collisionObject2 = current.ObjectB;
				}
				yield return collisionObject2;
			}
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(c810f724ed208fbe32b0376955a617ac7.c6b2b7bb0138c7a754f602e1325ab5a3a());
					}
					enumerator.Dispose();
					break;
				}
			}
		}
	}

	private static bool cd0cfe897426aab73c90b8217df390607(CollisionObject c6e4150c82a8b60982e648f0411087959, CollisionObject cb9065353c960dfabb2c6812b8400ab32)
	{
		return GeometryHelper.HaveContact(c6e4150c82a8b60982e648f0411087959.GeometricObject.Aabb, cb9065353c960dfabb2c6812b8400ab32.GeometricObject.Aabb);
	}

	internal static bool ce9b11ba3e3773f213dd90a03042dcbe8(CollisionObject c6e4150c82a8b60982e648f0411087959, CollisionObject cb9065353c960dfabb2c6812b8400ab32)
	{
		IGeometricObject geometricObject;
		IGeometricObject geometricObject2;
		if (c6e4150c82a8b60982e648f0411087959.cedb7bba503d90238311e100ff7d9069a)
		{
			if (cb9065353c960dfabb2c6812b8400ab32.cedb7bba503d90238311e100ff7d9069a)
			{
				return false;
			}
			geometricObject = c6e4150c82a8b60982e648f0411087959.GeometricObject;
			geometricObject2 = cb9065353c960dfabb2c6812b8400ab32.GeometricObject;
		}
		else
		{
			if (!cb9065353c960dfabb2c6812b8400ab32.cedb7bba503d90238311e100ff7d9069a)
			{
				return true;
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
			if (1 == 0)
			{
				__ldtoken(CollisionDomain.ce9b11ba3e3773f213dd90a03042dcbe8(CollisionObject, CollisionObject));
			}
			geometricObject = cb9065353c960dfabb2c6812b8400ab32.GeometricObject;
			geometricObject2 = c6e4150c82a8b60982e648f0411087959.GeometricObject;
		}
		RayShape rayShape = (RayShape)geometricObject.Shape;
		Vector3F ceabb47d7d959545d89c4300eabbdd7c = geometricObject.Scale;
		Pose cbc1b7e0b011ad6dcdb8beb392ec7a = geometricObject.Pose;
		Ray ray = new Ray(rayShape);
		ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
		ray.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
		return GeometryHelper.c3e45e70e51dbd3591dfa23857d27cfb5(c3ec982f192efbba86e42a1cea5c18565: new Vector3F(1f / ray.Direction.X, 1f / ray.Direction.Y, 1f / ray.Direction.Z), cdc3439410d15f28489c964ee87b88fe9: geometricObject2.Aabb, c355544f734bef7558e69db7ef3944f57: ray.Origin, c579aedaeb1b603c5780938faafdebe94: ray.Length);
	}

	/// <summary>
	///       Determines whether the specified collision object has contact with any other object in the 
	///       domain.
	///       </summary>
	/// <param name="collisionObject">The collision object.</param>
	/// <returns>
	///   <see langword="true" /> if the specified collision object touches or penetrates another 
	///       object in the collision domain; otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="collisionObject" /> is <see langword="null" />.
	///       </exception>
	public bool HasContact(CollisionObject collisionObject)
	{
		if (collisionObject == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CollisionDomain.HasContact(CollisionObject));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8529));
				}
			}
		}
		if (collisionObject.Domain == this)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					return ContactSets.GetContacts(collisionObject).Any();
				}
			}
		}
		if (!collisionObject.Enabled)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					return false;
				}
			}
		}
		bool flag = false;
		IEnumerable<CollisionObject> overlaps;
		if (collisionObject.cedb7bba503d90238311e100ff7d9069a)
		{
			RayShape rayShape = (RayShape)collisionObject.GeometricObject.Shape;
			Vector3F ceabb47d7d959545d89c4300eabbdd7c = collisionObject.GeometricObject.Scale;
			Pose cbc1b7e0b011ad6dcdb8beb392ec7a = collisionObject.GeometricObject.Pose;
			Ray ray = new Ray(rayShape);
			ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
			ray.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
			overlaps = BroadPhase.GetOverlaps(ray);
		}
		else
		{
			overlaps = BroadPhase.GetOverlaps(collisionObject);
		}
		IEnumerator<CollisionObject> enumerator = overlaps.GetEnumerator();
		try
		{
			do
			{
				IL_020e:
				ContactSet contacts;
				if (enumerator.MoveNext())
				{
					CollisionObject current = enumerator.Current;
					if (!c55dbd29b4a90a73bb93fd94db8e41505(collisionObject, current))
					{
						goto IL_020e;
					}
					flag = CollisionDetection.AlgorithmMatrix[collisionObject, current].HaveContact(collisionObject, current);
					if (!flag)
					{
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
					c324608b185f3ba8d0cd53192cf8b5629.TryGetValue(current, out var value);
					if (value == null || value.Count <= 0)
					{
						continue;
					}
					contacts = CollisionDetection.AlgorithmMatrix[collisionObject, current].GetContacts(collisionObject, current);
					if (contacts.Count != 0)
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
						if (!(value[0].PenetrationDepth < contacts[0].PenetrationDepth))
						{
							goto IL_01c2;
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
					}
					flag = false;
					goto IL_01c2;
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						goto end_IL_021c;
					}
					continue;
					end_IL_021c:
					break;
				}
				break;
				IL_01c2:
				using (List<Contact>.Enumerator enumerator2 = contacts.GetEnumerator())
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
							break;
						default:
							goto end_IL_01ea;
						}
						continue;
						end_IL_01ea:
						break;
					}
				}
				contacts.Recycle();
			}
			while (!flag);
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					enumerator.Dispose();
					break;
				}
			}
		}
		return flag;
	}

	/// <summary>
	///       Determines whether two <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s have contact.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <returns>
	///   <see langword="true" /> if the objects are touching or penetrating; otherwise 
	///       <see langword="false" /> if the objects are separated.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	public bool HaveContact(CollisionObject objectA, CollisionObject objectB)
	{
		if (objectA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
		}
		if (objectB == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
		}
		if (objectA.Domain == this)
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
				__ldtoken(CollisionDomain.HaveContact(CollisionObject, CollisionObject));
			}
			if (objectB.Domain == this)
			{
				ContactSet contacts = ContactSets.GetContacts(objectA, objectB);
				return contacts != null;
			}
		}
		if (!objectA.c0dbe012fb6e35eff096504ebd112177b)
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
			if (!objectB.c0dbe012fb6e35eff096504ebd112177b)
			{
				if (c55dbd29b4a90a73bb93fd94db8e41505(objectA, objectB))
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
					if (cd0cfe897426aab73c90b8217df390607(objectA, objectB) && ce9b11ba3e3773f213dd90a03042dcbe8(objectA, objectB))
					{
						return CollisionDetection.AlgorithmMatrix[objectA, objectB].HaveContact(objectA, objectB);
					}
				}
				goto IL_00dd;
			}
		}
		ContactSet contacts2 = GetContacts(objectA, objectB);
		if (contacts2 != null)
		{
			contacts2.Recycle();
			return true;
		}
		goto IL_00dd;
		IL_00dd:
		return false;
	}

	/// <summary>
	///       Updates the collision domain and computes the new contact information.
	///       </summary>
	/// <param name="deltaTime">
	///       The simulation time that has elapsed since the last update in seconds. (The size of the time
	///       step.)
	///       </param>
	public void Update(float deltaTime)
	{
		InternalBroadPhase.c70a983c78ddcdb771f728f30964ecc15();
		InternalBroadPhase.c322ef1ce205c6dbcf3a93ad899943435();
		int count = c324608b185f3ba8d0cd53192cf8b5629.Count;
		if (count > 0)
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
				__ldtoken(CollisionDomain.Update(float));
			}
			if (c88c6c77a07639babd08613eb38cf5957.Length < count)
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
				c88c6c77a07639babd08613eb38cf5957 = new KeyValuePair<CollisionObject, ContactSet>[count];
			}
			((ICollection<KeyValuePair<CollisionObject, ContactSet>>)c324608b185f3ba8d0cd53192cf8b5629).CopyTo(c88c6c77a07639babd08613eb38cf5957, 0);
			for (int i = 0; i < count; i++)
			{
				KeyValuePair<CollisionObject, ContactSet> keyValuePair = c88c6c77a07639babd08613eb38cf5957[i];
				ContactSet value = keyValuePair.Value;
				if (value == null)
				{
					continue;
				}
				if (!c773a59ff17bc2e1daf8a4ebbee35ecb5(value))
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
					if (c55dbd29b4a90a73bb93fd94db8e41505(value))
					{
						continue;
					}
				}
				c324608b185f3ba8d0cd53192cf8b5629[keyValuePair.Key] = null;
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
			Array.Clear(c88c6c77a07639babd08613eb38cf5957, 0, count);
		}
		ContactSets.Clear();
		cba181d738bf534c2390ad3b4ddf23601 = deltaTime;
		using (Dictionary<Pair<CollisionObject>, ContactSet>.ValueCollection.Enumerator enumerator = InternalBroadPhase.CandidatePairs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ContactSet current = enumerator.Current;
				cbd94166bbc2a2acf2ac050d1c7a14d05.Add(current);
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					goto end_IL_0132;
				}
				continue;
				end_IL_0132:
				break;
			}
		}
		c323401958c0832e019684ecfd47ba37d();
		cba181d738bf534c2390ad3b4ddf23601 = 0f;
		cbd94166bbc2a2acf2ac050d1c7a14d05.Clear();
		int count2 = CollisionObjects.Count;
		for (int j = 0; j < count2; j++)
		{
			CollisionObject collisionObject = CollisionObjects[j];
			collisionObject.Changed = false;
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				break;
			default:
				return;
			}
		}
	}

	/// <summary>
	///       Updates the collision domain and computes the new contact information for a given collision
	///       object.
	///       </summary>
	/// <param name="collisionObject">The collision object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="collisionObject" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="collisionObject" /> is not in this <see cref="T:DigitalRune.Geometry.Collisions.CollisionDomain" />.
	///       </exception>
	public void Update(CollisionObject collisionObject)
	{
		if (collisionObject == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8529));
		}
		if (!CollisionObjects.Contains(collisionObject))
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
						__ldtoken(CollisionDomain.Update(CollisionObject));
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8560), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8529));
				}
			}
		}
		ContactSets.Remove(collisionObject);
		InternalBroadPhase.c322ef1ce205c6dbcf3a93ad899943435(collisionObject);
		int count = c324608b185f3ba8d0cd53192cf8b5629.Count;
		if (count > 0)
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
			if (c88c6c77a07639babd08613eb38cf5957.Length < count)
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
				c88c6c77a07639babd08613eb38cf5957 = new KeyValuePair<CollisionObject, ContactSet>[count];
			}
			((ICollection<KeyValuePair<CollisionObject, ContactSet>>)c324608b185f3ba8d0cd53192cf8b5629).CopyTo(c88c6c77a07639babd08613eb38cf5957, 0);
			for (int i = 0; i < count; i++)
			{
				KeyValuePair<CollisionObject, ContactSet> keyValuePair = c88c6c77a07639babd08613eb38cf5957[i];
				ContactSet value = keyValuePair.Value;
				if (value == null)
				{
					continue;
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				if (value.ObjectA != collisionObject)
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
					if (value.ObjectB != collisionObject)
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
				}
				if (!c773a59ff17bc2e1daf8a4ebbee35ecb5(value))
				{
					if (c55dbd29b4a90a73bb93fd94db8e41505(value))
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
				}
				c324608b185f3ba8d0cd53192cf8b5629[keyValuePair.Key] = null;
			}
			Array.Clear(c88c6c77a07639babd08613eb38cf5957, 0, count);
		}
		foreach (ContactSet candidatePair in InternalBroadPhase.CandidatePairs)
		{
			if (candidatePair.ObjectA != collisionObject)
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
				if (candidatePair.ObjectB != collisionObject)
				{
					continue;
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
			}
			cbd94166bbc2a2acf2ac050d1c7a14d05.Add(candidatePair);
		}
		c323401958c0832e019684ecfd47ba37d();
		cbd94166bbc2a2acf2ac050d1c7a14d05.Clear();
		collisionObject.Changed = false;
	}

	private void c323401958c0832e019684ecfd47ba37d()
	{
		int count = cbd94166bbc2a2acf2ac050d1c7a14d05.Count;
		if (EnableMultithreading)
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
				__ldtoken(CollisionDomain.c323401958c0832e019684ecfd47ba37d());
			}
			if (count > 1)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
					{
						Parallel.For(0, count, cf5629aae9c89f6d0f57968ecf6cafbaf);
						for (int i = 0; i < count; i++)
						{
							cd98f35edeab91527eed48279ad38ccf5(i);
						}
						while (true)
						{
							switch (1)
							{
							case 0:
								break;
							default:
								return;
							}
						}
					}
					}
				}
			}
		}
		for (int j = 0; j < count; j++)
		{
			c323401958c0832e019684ecfd47ba37d(j);
			cd98f35edeab91527eed48279ad38ccf5(j);
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				break;
			default:
				return;
			}
		}
	}

	private void c323401958c0832e019684ecfd47ba37d(int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		ContactSet contactSet = cbd94166bbc2a2acf2ac050d1c7a14d05[cdeb496a0c3d046b6bc838defe2d220d0];
		if (!ce9b11ba3e3773f213dd90a03042dcbe8(contactSet.ObjectA, contactSet.ObjectB))
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
				{
					if (1 == 0)
					{
						__ldtoken(CollisionDomain.c323401958c0832e019684ecfd47ba37d(int));
					}
					contactSet.HaveContact = false;
					using (List<Contact>.Enumerator enumerator = contactSet.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Contact current = enumerator.Current;
							current.Recycle();
						}
						while (true)
						{
							switch (7)
							{
							case 0:
								break;
							default:
								goto end_IL_0062;
							}
							continue;
							end_IL_0062:
							break;
						}
					}
					contactSet.Clear();
					return;
				}
				}
			}
		}
		if (c55dbd29b4a90a73bb93fd94db8e41505(contactSet))
		{
			if (!c773a59ff17bc2e1daf8a4ebbee35ecb5(contactSet))
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
				if (contactSet.IsValid)
				{
					if (cba181d738bf534c2390ad3b4ddf23601 > 0f)
					{
						c9ae4fc785e8cd5184185f6ee6d5a3853.c4428c2d517a1c32aa278a32dae4fe2d3(contactSet, cba181d738bf534c2390ad3b4ddf23601, CollisionDetection.ContactPositionTolerance);
					}
					return;
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
			}
			if (contactSet.cc5b0109eff96f91c3e8ea71cac49ebb7 == null)
			{
				contactSet.cc5b0109eff96f91c3e8ea71cac49ebb7 = CollisionDetection.AlgorithmMatrix[contactSet];
			}
			contactSet.cc5b0109eff96f91c3e8ea71cac49ebb7.UpdateContacts(contactSet, cba181d738bf534c2390ad3b4ddf23601);
		}
		else
		{
			if (!c773a59ff17bc2e1daf8a4ebbee35ecb5(contactSet))
			{
				return;
			}
			foreach (Contact item in contactSet)
			{
				item.Recycle();
			}
			contactSet.Clear();
			contactSet.IsValid = false;
		}
	}

	private void cd98f35edeab91527eed48279ad38ccf5(int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		ContactSet contactSet = cbd94166bbc2a2acf2ac050d1c7a14d05[cdeb496a0c3d046b6bc838defe2d220d0];
		if (!contactSet.HaveContact)
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
				__ldtoken(CollisionDomain.cd98f35edeab91527eed48279ad38ccf5(int));
			}
			if (!c55dbd29b4a90a73bb93fd94db8e41505(contactSet))
			{
				return;
			}
			CollisionObject collisionObject = null;
			if (contactSet.ObjectA.c0dbe012fb6e35eff096504ebd112177b)
			{
				collisionObject = contactSet.ObjectA;
			}
			else if (contactSet.ObjectB.c0dbe012fb6e35eff096504ebd112177b)
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
				collisionObject = contactSet.ObjectB;
			}
			if (collisionObject != null)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
					{
						c324608b185f3ba8d0cd53192cf8b5629.TryGetValue(collisionObject, out var value);
						if (value != null)
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
							if (value != contactSet)
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
								if (value.Count != 0)
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
									if (contactSet.Count <= 0 || !(contactSet[0].PenetrationDepth < value[0].PenetrationDepth))
									{
										return;
									}
									while (true)
									{
										switch (6)
										{
										case 0:
											continue;
										}
										break;
									}
								}
							}
						}
						c324608b185f3ba8d0cd53192cf8b5629[collisionObject] = contactSet;
						ContactSets.Remove(value);
						ContactSets.Add(contactSet);
						return;
					}
					}
				}
			}
			ContactSets.Add(contactSet);
			return;
		}
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8687), CollisionObjects.Count);
	}
}
