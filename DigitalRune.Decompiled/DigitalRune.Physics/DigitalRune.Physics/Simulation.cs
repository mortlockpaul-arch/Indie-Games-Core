using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Geometry.Partitioning;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Physics.Constraints;
using DigitalRune.Physics.ForceEffects;
using DigitalRune.Physics.Settings;
using DigitalRune.Threading;

namespace DigitalRune.Physics;

/// <summary>
///       Manages a physics simulation.
///       </summary>
public class Simulation
{
	/// <summary>
	///       Represents the simulation "world".
	///       </summary>
	public readonly RigidBody World;

	private readonly List<Vector3F> c2b21a6c36da9673481a74f8de99d0666;

	private readonly List<ForceEffect> cd7be8bb417a6fd3f68e0383ef0d8d26b;

	private float c3abbe5d8d6d59363f57a23363fafdc17;

	private readonly Action<int> ccb81f453d974faffc33e7a8c2d5f3b6a;

	private readonly Action<int> cae0c023e36fc7ee0dba015f99b56d82e;

	private readonly Action<int> c7ad6871f933db85fc737eb3c3cfc40ef;

	private readonly Action<ContactSet> c7982fd622f012c1345c5692d40bd07fe;

	private readonly Action<int> ccbf9f59d3487904c6e65f36c7e58ac43;

	private ReadOnlyCollection<ContactConstraint> c3ecaf6cc88e7c4be42a38eb49c03e95c;

	internal List<ContactConstraint> c32f28f83e5bad489c86f1585da1c621f;

	private float cde3177bf9dee776efacc417aefcd78be;

	private SimulationSettings c02b9671201c62b88aed57d129e6bb034;

	private EventHandler c521f0caca5a747ecffe180c1dff6e608;

	[CompilerGenerated]
	private CollisionDomain cace13a7b286bedc98f2968acf27c5da6;

	[CompilerGenerated]
	private ConstraintCollection cc15be04a86305ed0fce153860d6972e2;

	[CompilerGenerated]
	private c2943f95551cd5c00e1ad7d7f91414782 c87820ace9775bed17875bf6d5ac3960b;

	[CompilerGenerated]
	private ForceEffectCollection c79f65f613a16a5746ad1f9916bb06da8;

	[CompilerGenerated]
	private RigidBodyCollection c9aba495476df432177775bd6429a7541;

	[CompilerGenerated]
	private IPairFilter<RigidBody> c3bc35221bd1dc21f6bc6e4c291f7da1e;

	[CompilerGenerated]
	private TimeSpan cd3ec56eea12add1978ed574106e92da4;

	[CompilerGenerated]
	private TimeSpan c1eb66a9452bfe77cc8e73ac0a3bd4e6e;

	[CompilerGenerated]
	private bool c0139457fd7569c773c840a039fcb6ba8;

	[CompilerGenerated]
	private SimulationIslandManager c2d95a19fb2e19b82d1b1d8e5c9a4a0b1;

	/// <summary>
	///       Gets the collision domain.
	///       </summary>
	public CollisionDomain CollisionDomain
	{
		[CompilerGenerated]
		get
		{
			return cace13a7b286bedc98f2968acf27c5da6;
		}
		[CompilerGenerated]
		private set
		{
			cace13a7b286bedc98f2968acf27c5da6 = value;
		}
	}

	/// <summary>
	///       Gets the contact constraints.
	///       </summary>
	public ReadOnlyCollection<ContactConstraint> ContactConstraints
	{
		get
		{
			if (c3ecaf6cc88e7c4be42a38eb49c03e95c == null)
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
					__ldtoken(Simulation.get_ContactConstraints());
				}
				c3ecaf6cc88e7c4be42a38eb49c03e95c = c32f28f83e5bad489c86f1585da1c621f.AsReadOnly();
			}
			return c3ecaf6cc88e7c4be42a38eb49c03e95c;
		}
	}

	/// <summary>
	///       Gets the constraints.
	///       </summary>
	public ConstraintCollection Constraints
	{
		[CompilerGenerated]
		get
		{
			return cc15be04a86305ed0fce153860d6972e2;
		}
		[CompilerGenerated]
		private set
		{
			cc15be04a86305ed0fce153860d6972e2 = value;
		}
	}

	private c2943f95551cd5c00e1ad7d7f91414782 ConstraintSolver
	{
		[CompilerGenerated]
		get
		{
			return c87820ace9775bed17875bf6d5ac3960b;
		}
		[CompilerGenerated]
		set
		{
			c87820ace9775bed17875bf6d5ac3960b = value;
		}
	}

	/// <summary>
	///       Gets the force effects that act on the rigid bodies.
	///       </summary>
	public ForceEffectCollection ForceEffects
	{
		[CompilerGenerated]
		get
		{
			return c79f65f613a16a5746ad1f9916bb06da8;
		}
		[CompilerGenerated]
		private set
		{
			c79f65f613a16a5746ad1f9916bb06da8 = value;
		}
	}

	/// <summary>
	///       Gets the rigid bodies.
	///       </summary>
	public RigidBodyCollection RigidBodies
	{
		[CompilerGenerated]
		get
		{
			return c9aba495476df432177775bd6429a7541;
		}
		[CompilerGenerated]
		private set
		{
			c9aba495476df432177775bd6429a7541 = value;
		}
	}

	/// <summary>
	///       Gets or sets the collision response filter.
	///       </summary>
	public IPairFilter<RigidBody> ResponseFilter
	{
		[CompilerGenerated]
		get
		{
			return c3bc35221bd1dc21f6bc6e4c291f7da1e;
		}
		[CompilerGenerated]
		set
		{
			c3bc35221bd1dc21f6bc6e4c291f7da1e = value;
		}
	}

	/// <summary>
	///       Gets the current simulation time.
	///       </summary>
	public TimeSpan Time
	{
		[CompilerGenerated]
		get
		{
			return cd3ec56eea12add1978ed574106e92da4;
		}
		[CompilerGenerated]
		private set
		{
			cd3ec56eea12add1978ed574106e92da4 = value;
		}
	}

	private TimeSpan TargetTime
	{
		[CompilerGenerated]
		get
		{
			return c1eb66a9452bfe77cc8e73ac0a3bd4e6e;
		}
		[CompilerGenerated]
		set
		{
			c1eb66a9452bfe77cc8e73ac0a3bd4e6e = value;
		}
	}

	/// <summary>
	///       Gets or sets the time scaling.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float TimeScaling
	{
		get
		{
			return cde3177bf9dee776efacc417aefcd78be;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12));
			}
			cde3177bf9dee776efacc417aefcd78be = value;
		}
	}

	/// <summary>
	///       Gets or sets the simulation settings.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public SimulationSettings Settings
	{
		get
		{
			return c02b9671201c62b88aed57d129e6bb034;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			c02b9671201c62b88aed57d129e6bb034 = value;
		}
	}

	internal bool CcdRequested
	{
		[CompilerGenerated]
		get
		{
			return c0139457fd7569c773c840a039fcb6ba8;
		}
		[CompilerGenerated]
		set
		{
			c0139457fd7569c773c840a039fcb6ba8 = value;
		}
	}

	/// <summary>
	///       Gets the <see cref="T:DigitalRune.Physics.SimulationIslandManager" />.
	///       </summary>
	public SimulationIslandManager IslandManager
	{
		[CompilerGenerated]
		get
		{
			return c2d95a19fb2e19b82d1b1d8e5c9a4a0b1;
		}
		[CompilerGenerated]
		private set
		{
			c2d95a19fb2e19b82d1b1d8e5c9a4a0b1 = value;
		}
	}

	/// <summary>
	///       Occurs when an internal time step has finished.
	///       </summary>
	public event EventHandler SubTimeStepFinished
	{
		add
		{
			EventHandler eventHandler = c521f0caca5a747ecffe180c1dff6e608;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref c521f0caca5a747ecffe180c1dff6e608, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Simulation.add_SubTimeStepFinished(EventHandler));
				}
				return;
			}
		}
		remove
		{
			EventHandler eventHandler = c521f0caca5a747ecffe180c1dff6e608;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref c521f0caca5a747ecffe180c1dff6e608, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	static Simulation()
	{
		TypeInitializer.Initialize(c31a405ff5d24b3c4be569f1e85a6f73f.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(13, c31a405ff5d24b3c4be569f1e85a6f73f.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Simulation" /> class.
	///       </summary>
	public Simulation()
	{
		Action<int> action = null;
		c2b21a6c36da9673481a74f8de99d0666 = new List<Vector3F>();
		cd7be8bb417a6fd3f68e0383ef0d8d26b = new List<ForceEffect>();
		cde3177bf9dee776efacc417aefcd78be = 1f;
		base._002Ector();
		Settings = new SimulationSettings();
		CollisionDetection collisionDetection = new CollisionDetection
		{
			CollisionFilter = new c097cad3a52ef7e36c2bf28861167a5f8(),
			FullContactSetPerFrame = true
		};
		CollisionDomain = new CollisionDomain(collisionDetection);
		ConstraintSolver = new c02168253bcb64d2a565d402ed87663bb(this);
		Constraints = new ConstraintCollection();
		Constraints.CollectionChanged += c8047b80408eb1a16d08cf45f026d931d;
		ForceEffects = new ForceEffectCollection();
		ForceEffects.CollectionChanged += c022653720478ad2b59f4cd7f31452fcf;
		RigidBodies = new RigidBodyCollection();
		RigidBodies.CollectionChanged += cfd7e73f58765deb4e3cdda705ff40935;
		c32f28f83e5bad489c86f1585da1c621f = new List<ContactConstraint>();
		IslandManager = new SimulationIslandManager(this);
		RigidBody rigidBody = new RigidBody(new BoxShape(20000f, 20000f, 20000f));
		rigidBody.CollisionResponseEnabled = false;
		rigidBody.CollisionObject.Type = CollisionObjectType.Trigger;
		rigidBody.MotionType = MotionType.Static;
		rigidBody.Name = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(107);
		rigidBody.Simulation = this;
		World = rigidBody;
		CollisionDomain.CollisionObjects.Remove(World.CollisionObject);
		if (action == null)
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
				__ldtoken(Simulation._002Ector());
			}
			action = (int cdeb496a0c3d046b6bc838defe2d220d0) =>
			{
				RigidBody rigidBody2 = RigidBodies[cdeb496a0c3d046b6bc838defe2d220d0];
				rigidBody2.c36bda22bcb218ef5a106e2eb3ad0058c(c3abbe5d8d6d59363f57a23363fafdc17);
			};
		}
		ccb81f453d974faffc33e7a8c2d5f3b6a = action;
		cae0c023e36fc7ee0dba015f99b56d82e = cf224e75070973e040bf1a2eaab7619d8;
		c7ad6871f933db85fc737eb3c3cfc40ef = (int cdeb496a0c3d046b6bc838defe2d220d0) =>
		{
			RigidBody rigidBody2 = RigidBodies[cdeb496a0c3d046b6bc838defe2d220d0];
			rigidBody2.c33164d470800b44e1b9c7f00db54e817(c3abbe5d8d6d59363f57a23363fafdc17);
		};
		c7982fd622f012c1345c5692d40bd07fe = ca68049edaffd4dda9c3563614603e1d8;
		ccbf9f59d3487904c6e65f36c7e58ac43 = c0b1f87392c81473fdeeee5af0b3429a5;
	}

	private void c022653720478ad2b59f4cd7f31452fcf(object c807f682152464d11dde83a885bf87124, CollectionChangedEventArgs<ForceEffect> c7fefeb91d4632901bd52a1f245b70db1)
	{
		if (c7fefeb91d4632901bd52a1f245b70db1.Action == CollectionChangedAction.Move)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Simulation.c022653720478ad2b59f4cd7f31452fcf(object, CollectionChangedEventArgs<ForceEffect>));
					}
					return;
				}
			}
		}
		int count = c7fefeb91d4632901bd52a1f245b70db1.OldItems.Count;
		for (int i = 0; i < count; i++)
		{
			ForceEffect forceEffect = c7fefeb91d4632901bd52a1f245b70db1.OldItems[i];
			forceEffect.Simulation = null;
		}
		int count2 = c7fefeb91d4632901bd52a1f245b70db1.NewItems.Count;
		for (int j = 0; j < count2; j++)
		{
			ForceEffect forceEffect2 = c7fefeb91d4632901bd52a1f245b70db1.NewItems[j];
			forceEffect2.Simulation = this;
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

	private void cfd7e73f58765deb4e3cdda705ff40935(object c807f682152464d11dde83a885bf87124, CollectionChangedEventArgs<RigidBody> c7fefeb91d4632901bd52a1f245b70db1)
	{
		if (c7fefeb91d4632901bd52a1f245b70db1.Action == CollectionChangedAction.Move)
		{
			return;
		}
		int count = c7fefeb91d4632901bd52a1f245b70db1.OldItems.Count;
		for (int i = 0; i < count; i++)
		{
			RigidBody rigidBody = c7fefeb91d4632901bd52a1f245b70db1.OldItems[i];
			rigidBody.Simulation = null;
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
				__ldtoken(Simulation.cfd7e73f58765deb4e3cdda705ff40935(object, CollectionChangedEventArgs<RigidBody>));
			}
			int count2 = c7fefeb91d4632901bd52a1f245b70db1.NewItems.Count;
			for (int j = 0; j < count2; j++)
			{
				RigidBody rigidBody2 = c7fefeb91d4632901bd52a1f245b70db1.NewItems[j];
				if (rigidBody2 != World)
				{
					rigidBody2.Simulation = this;
				}
			}
			return;
		}
	}

	private void c8047b80408eb1a16d08cf45f026d931d(object c807f682152464d11dde83a885bf87124, CollectionChangedEventArgs<Constraint> c7fefeb91d4632901bd52a1f245b70db1)
	{
		if (c7fefeb91d4632901bd52a1f245b70db1.Action == CollectionChangedAction.Move)
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
						__ldtoken(Simulation.c8047b80408eb1a16d08cf45f026d931d(object, CollectionChangedEventArgs<Constraint>));
					}
					return;
				}
			}
		}
		int count = c7fefeb91d4632901bd52a1f245b70db1.OldItems.Count;
		for (int i = 0; i < count; i++)
		{
			Constraint constraint = c7fefeb91d4632901bd52a1f245b70db1.OldItems[i];
			constraint.Simulation = null;
		}
		int count2 = c7fefeb91d4632901bd52a1f245b70db1.NewItems.Count;
		for (int j = 0; j < count2; j++)
		{
			Constraint constraint2 = c7fefeb91d4632901bd52a1f245b70db1.NewItems[j];
			constraint2.Simulation = this;
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

	/// <summary>
	///       Advances the simulation by the given time.
	///       </summary>
	/// <param name="deltaTime">The size of the time step in seconds.</param>
	public void Update(float deltaTime)
	{
		if (Numeric.IsZero(deltaTime))
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
						__ldtoken(Simulation.Update(float));
					}
					return;
				}
			}
		}
		deltaTime *= TimeScaling;
		deltaTime = MathHelper.Clamp(deltaTime, 0f, Settings.Timing.FixedTimeStep * (float)Settings.Timing.MaxNumberOfSteps);
		TargetTime += TimeSpan.FromSeconds(deltaTime);
		c3abbe5d8d6d59363f57a23363fafdc17 = Settings.Timing.FixedTimeStep;
		while (Numeric.IsGreaterOrEqual((TargetTime - Time).TotalSeconds, c3abbe5d8d6d59363f57a23363fafdc17))
		{
			if (Settings.EnableMultithreading)
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
				cec30c6fe61398312619cdcd10a0a691d(c3abbe5d8d6d59363f57a23363fafdc17);
			}
			else
			{
				c9595873bf2e3b501aed01fda64e717d7(c3abbe5d8d6d59363f57a23363fafdc17);
			}
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			if (Settings.SynchronizeCollisionDomain)
			{
				CollisionDomain.Update(0f);
			}
			int count = RigidBodies.Count;
			for (int i = 0; i < count; i++)
			{
				RigidBody rigidBody = RigidBodies[i];
				rigidBody.ClearForces();
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

	private void c9595873bf2e3b501aed01fda64e717d7(float c8b459619a7ae018a2933149ea0bc4815)
	{
		c4428c2d517a1c32aa278a32dae4fe2d3();
		c6f24625b13a57bb6e31e3b4397cfa400();
		int count = RigidBodies.Count;
		for (int i = 0; i < count; i++)
		{
			RigidBody rigidBody = RigidBodies[i];
			rigidBody.c36bda22bcb218ef5a106e2eb3ad0058c(c8b459619a7ae018a2933149ea0bc4815);
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
				__ldtoken(Simulation.c9595873bf2e3b501aed01fda64e717d7(float));
			}
			IslandManager.c322ef1ce205c6dbcf3a93ad899943435();
			int count2 = IslandManager.IslandsInternal.Count;
			for (int j = 0; j < count2; j++)
			{
				cf224e75070973e040bf1a2eaab7619d8(j);
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				for (int k = 0; k < count; k++)
				{
					RigidBody rigidBody2 = RigidBodies[k];
					rigidBody2.c33164d470800b44e1b9c7f00db54e817(c8b459619a7ae018a2933149ea0bc4815);
				}
				c68ef0999797db74199211cac500632c5();
				c88ea60f00d9c959e57656f20ef49450e();
				Time += TimeSpan.FromSeconds(c8b459619a7ae018a2933149ea0bc4815);
				OnSubTimeStepFinished(EventArgs.Empty);
				return;
			}
		}
	}

	private void cec30c6fe61398312619cdcd10a0a691d(float c8b459619a7ae018a2933149ea0bc4815)
	{
		c4428c2d517a1c32aa278a32dae4fe2d3();
		c6f24625b13a57bb6e31e3b4397cfa400();
		int count = RigidBodies.Count;
		Parallel.For(0, count, ccb81f453d974faffc33e7a8c2d5f3b6a);
		IslandManager.c322ef1ce205c6dbcf3a93ad899943435();
		Parallel.For(0, IslandManager.IslandsInternal.Count, cae0c023e36fc7ee0dba015f99b56d82e);
		Parallel.For(0, count, c7ad6871f933db85fc737eb3c3cfc40ef);
		ca90952064133bfe0914e7611d97f7aee();
		c88ea60f00d9c959e57656f20ef49450e();
		Time += TimeSpan.FromSeconds(c8b459619a7ae018a2933149ea0bc4815);
		OnSubTimeStepFinished(EventArgs.Empty);
	}

	private void c4428c2d517a1c32aa278a32dae4fe2d3()
	{
		CollisionDomain.Update(Settings.Timing.FixedTimeStep);
		IslandManager.ContactSetLinks.Clear();
		foreach (ContactSet contactSet in CollisionDomain.ContactSets)
		{
			RigidBody rigidBody = contactSet.ObjectA.GeometricObject as RigidBody;
			RigidBody rigidBody2 = contactSet.ObjectB.GeometricObject as RigidBody;
			if (rigidBody == null)
			{
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
				__ldtoken(Simulation.c4428c2d517a1c32aa278a32dae4fe2d3());
			}
			if (rigidBody2 == null)
			{
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
			int num;
			if (rigidBody.CollisionResponseEnabled)
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
				if (rigidBody2.CollisionResponseEnabled)
				{
					if (ResponseFilter != null)
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
						num = (ResponseFilter.Filter(new Pair<RigidBody>(rigidBody, rigidBody2)) ? 1 : 0);
					}
					else
					{
						num = 1;
					}
					goto IL_00f1;
				}
			}
			num = 0;
			goto IL_00f1;
			IL_00f1:
			bool flag = (byte)num != 0;
			if (flag)
			{
				IslandManager.ContactSetLinks.Add(new Pair<RigidBody>(rigidBody, rigidBody2));
			}
			int count = contactSet.Count;
			for (int i = 0; i < count; i++)
			{
				Contact contact = contactSet[i];
				if (contact.UserData is ContactConstraint contactConstraint)
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
					if (flag)
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
						contactConstraint.Used = true;
					}
					else
					{
						contact.UserData = null;
					}
				}
				else
				{
					if (!flag)
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
					ContactConstraint contactConstraint2 = (ContactConstraint)(contact.UserData = ContactConstraint.c2d8ce8b1ccf2b905e3b18f8a5145fe8a(rigidBody, rigidBody2, contact));
					c32f28f83e5bad489c86f1585da1c621f.Add(contactConstraint2);
					contactConstraint2.Used = true;
				}
			}
		}
		int count2 = c32f28f83e5bad489c86f1585da1c621f.Count;
		int num2 = count2;
		for (int num3 = count2 - 1; num3 >= 0; num3--)
		{
			ContactConstraint contactConstraint4 = c32f28f83e5bad489c86f1585da1c621f[num3];
			if (contactConstraint4.Used)
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
				contactConstraint4.Used = false;
			}
			else
			{
				num2--;
				contactConstraint4.ca18b9b5faa9851d6355f617787bb5859();
				c32f28f83e5bad489c86f1585da1c621f[num3] = c32f28f83e5bad489c86f1585da1c621f[num2];
			}
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			int num4 = count2 - num2;
			if (num4 <= 0)
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
				c32f28f83e5bad489c86f1585da1c621f.RemoveRange(num2, num4);
				return;
			}
		}
	}

	private void c6f24625b13a57bb6e31e3b4397cfa400()
	{
		c2b21a6c36da9673481a74f8de99d0666.Clear();
		int count = RigidBodies.Count;
		for (int i = 0; i < count; i++)
		{
			RigidBody rigidBody = RigidBodies[i];
			c2b21a6c36da9673481a74f8de99d0666.Add(rigidBody.c9db2f3d2ea57a42c0954897116e7e018);
			rigidBody.c9db2f3d2ea57a42c0954897116e7e018 = rigidBody.cb310332bf0a7b2aabfc439d94b2ed22f;
			rigidBody.c8cc59972c02a657a040f0f4333ff4c3a = rigidBody.c375c3334a1cc1adb185ce6b0ebc20ce7;
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(Simulation.c6f24625b13a57bb6e31e3b4397cfa400());
			}
			try
			{
				foreach (ForceEffect forceEffect2 in ForceEffects)
				{
					cd7be8bb417a6fd3f68e0383ef0d8d26b.Add(forceEffect2);
				}
				int count2 = cd7be8bb417a6fd3f68e0383ef0d8d26b.Count;
				for (int j = 0; j < count2; j++)
				{
					ForceEffect forceEffect = cd7be8bb417a6fd3f68e0383ef0d8d26b[j];
					forceEffect.c1a3cd64f0234e04728b4ac42f0cab1e4();
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
						goto end_IL_00e8;
					}
					continue;
					end_IL_00e8:
					break;
				}
			}
			finally
			{
				cd7be8bb417a6fd3f68e0383ef0d8d26b.Clear();
			}
			for (int k = 0; k < count; k++)
			{
				RigidBody rigidBody2 = RigidBodies[k];
				if (!((c2b21a6c36da9673481a74f8de99d0666[k] - rigidBody2.c9db2f3d2ea57a42c0954897116e7e018).LengthSquared > Settings.Sleeping.LinearVelocityThresholdSquared * rigidBody2.MassFrame.Mass * rigidBody2.MassFrame.Mass))
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
				rigidBody2.WakeUp();
			}
			return;
		}
	}

	private void cf224e75070973e040bf1a2eaab7619d8(int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		SimulationIsland simulationIsland = IslandManager.IslandsInternal[cdeb496a0c3d046b6bc838defe2d220d0];
		if (simulationIsland.cd467b98690cc77fb7e4140a129f87542())
		{
			return;
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(Simulation.cf224e75070973e040bf1a2eaab7619d8(int));
			}
			ConstraintSolver.cb71790430f9d613551af34a891399e09(simulationIsland, c3abbe5d8d6d59363f57a23363fafdc17);
			return;
		}
	}

	private void c68ef0999797db74199211cac500632c5()
	{
		if (!CcdRequested)
		{
			return;
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
				__ldtoken(Simulation.c68ef0999797db74199211cac500632c5());
			}
			CollisionDomain.InternalBroadPhase.c322ef1ce205c6dbcf3a93ad899943435();
			using (Dictionary<Pair<CollisionObject>, ContactSet>.ValueCollection.Enumerator enumerator = CollisionDomain.InternalBroadPhase.CandidatePairs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ContactSet current = enumerator.Current;
					c70a2f44051bf4175831db0fb985022ed(current);
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						goto end_IL_006c;
					}
					continue;
					end_IL_006c:
					break;
				}
			}
			int count = RigidBodies.Count;
			for (int i = 0; i < count; i++)
			{
				c0b1f87392c81473fdeeee5af0b3429a5(i);
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				CcdRequested = false;
				return;
			}
		}
	}

	private void ca90952064133bfe0914e7611d97f7aee()
	{
		if (!CcdRequested)
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
				__ldtoken(Simulation.ca90952064133bfe0914e7611d97f7aee());
			}
			CollisionDomain.InternalBroadPhase.c322ef1ce205c6dbcf3a93ad899943435();
			Parallel.ForEach(CollisionDomain.InternalBroadPhase.CandidatePairs, c7982fd622f012c1345c5692d40bd07fe);
			Parallel.For(0, RigidBodies.Count, ccbf9f59d3487904c6e65f36c7e58ac43);
			CcdRequested = false;
			return;
		}
	}

	private void c70a2f44051bf4175831db0fb985022ed(ContactSet cfc73391dcf53c37de15d54e2434d0199)
	{
		RigidBody rigidBody = cfc73391dcf53c37de15d54e2434d0199.ObjectA.GeometricObject as RigidBody;
		RigidBody rigidBody2 = cfc73391dcf53c37de15d54e2434d0199.ObjectB.GeometricObject as RigidBody;
		if (rigidBody == null)
		{
			return;
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
				__ldtoken(Simulation.c70a2f44051bf4175831db0fb985022ed(ContactSet));
			}
			if (rigidBody2 == null)
			{
				return;
			}
			if (!rigidBody.IsCcdActive)
			{
				if (!rigidBody2.IsCcdActive)
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
					break;
				}
			}
			Func<RigidBody, RigidBody, bool> ccdFilter = Settings.Motion.CcdFilter;
			if (ccdFilter != null && !ccdFilter(rigidBody, rigidBody2))
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						return;
					}
				}
			}
			if (!CollisionDomain.c55dbd29b4a90a73bb93fd94db8e41505(cfc73391dcf53c37de15d54e2434d0199))
			{
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
			if (rigidBody.Shape is ConvexShape && rigidBody2.Shape is ConvexShape)
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
				if (CollisionDomain.HaveContact(cfc73391dcf53c37de15d54e2434d0199.ObjectA, cfc73391dcf53c37de15d54e2434d0199.ObjectB))
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
					break;
				}
			}
			CollisionDetection collisionDetection = CollisionDomain.CollisionDetection;
			CollisionObject objectA = cfc73391dcf53c37de15d54e2434d0199.ObjectA;
			Pose targetPoseA;
			if (!rigidBody.IsCcdActive)
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
				targetPoseA = rigidBody.Pose;
			}
			else
			{
				targetPoseA = rigidBody.TargetPose;
			}
			CollisionObject objectB = cfc73391dcf53c37de15d54e2434d0199.ObjectB;
			Pose targetPoseB;
			if (!rigidBody2.IsCcdActive)
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
				targetPoseB = rigidBody2.Pose;
			}
			else
			{
				targetPoseB = rigidBody2.TargetPose;
			}
			float timeOfImpact = collisionDetection.GetTimeOfImpact(objectA, targetPoseA, objectB, targetPoseB, Settings.Constraints.AllowedPenetration * 2f);
			if (timeOfImpact < rigidBody.c075558120e08634a46735a58fcaf20a7)
			{
				rigidBody.c075558120e08634a46735a58fcaf20a7 = timeOfImpact;
			}
			if (!(timeOfImpact < rigidBody2.c075558120e08634a46735a58fcaf20a7))
			{
				return;
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				rigidBody2.c075558120e08634a46735a58fcaf20a7 = timeOfImpact;
				return;
			}
		}
	}

	private void ca68049edaffd4dda9c3563614603e1d8(ContactSet cfc73391dcf53c37de15d54e2434d0199)
	{
		RigidBody rigidBody = cfc73391dcf53c37de15d54e2434d0199.ObjectA.GeometricObject as RigidBody;
		RigidBody rigidBody2 = cfc73391dcf53c37de15d54e2434d0199.ObjectB.GeometricObject as RigidBody;
		if (rigidBody == null || rigidBody2 == null)
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
				__ldtoken(Simulation.ca68049edaffd4dda9c3563614603e1d8(ContactSet));
			}
			if (!rigidBody.IsCcdActive)
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
				if (!rigidBody2.IsCcdActive)
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
			Func<RigidBody, RigidBody, bool> ccdFilter = Settings.Motion.CcdFilter;
			if (ccdFilter != null && !ccdFilter(rigidBody, rigidBody2))
			{
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
			if (!CollisionDomain.c55dbd29b4a90a73bb93fd94db8e41505(cfc73391dcf53c37de15d54e2434d0199))
			{
				return;
			}
			if (rigidBody.Shape is ConvexShape && rigidBody2.Shape is ConvexShape)
			{
				if (CollisionDomain.HaveContact(cfc73391dcf53c37de15d54e2434d0199.ObjectA, cfc73391dcf53c37de15d54e2434d0199.ObjectB))
				{
					return;
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
			float timeOfImpact = CollisionDomain.CollisionDetection.GetTimeOfImpact(cfc73391dcf53c37de15d54e2434d0199.ObjectA, rigidBody.IsCcdActive ? rigidBody.TargetPose : rigidBody.Pose, cfc73391dcf53c37de15d54e2434d0199.ObjectB, rigidBody2.IsCcdActive ? rigidBody2.TargetPose : rigidBody2.Pose, Settings.Constraints.AllowedPenetration * 2f);
			if (timeOfImpact < rigidBody.c075558120e08634a46735a58fcaf20a7)
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
				lock (rigidBody)
				{
					if (timeOfImpact < rigidBody.c075558120e08634a46735a58fcaf20a7)
					{
						rigidBody.c075558120e08634a46735a58fcaf20a7 = timeOfImpact;
					}
				}
			}
			if (!(timeOfImpact < rigidBody2.c075558120e08634a46735a58fcaf20a7))
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
				lock (rigidBody2)
				{
					if (!(timeOfImpact < rigidBody2.c075558120e08634a46735a58fcaf20a7))
					{
						return;
					}
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						rigidBody2.c075558120e08634a46735a58fcaf20a7 = timeOfImpact;
						return;
					}
				}
			}
		}
	}

	private void c0b1f87392c81473fdeeee5af0b3429a5(int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		RigidBody rigidBody = RigidBodies[cdeb496a0c3d046b6bc838defe2d220d0];
		if (!rigidBody.IsCcdActive)
		{
			return;
		}
		if (rigidBody.c075558120e08634a46735a58fcaf20a7 == 1f)
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
			if (1 == 0)
			{
				__ldtoken(Simulation.c0b1f87392c81473fdeeee5af0b3429a5(int));
			}
			rigidBody.Pose = rigidBody.TargetPose;
		}
		else
		{
			rigidBody.Pose = Pose.Interpolate(rigidBody.Pose, rigidBody.TargetPose, rigidBody.c075558120e08634a46735a58fcaf20a7);
		}
		rigidBody.IsCcdActive = false;
	}

	private void c88ea60f00d9c959e57656f20ef49450e()
	{
		if (!Settings.Motion.RemoveBodiesOutsideWorld)
		{
			return;
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
				__ldtoken(Simulation.c88ea60f00d9c959e57656f20ef49450e());
			}
			int count = RigidBodies.Count;
			for (int num = count - 1; num >= 0; num--)
			{
				RigidBody rigidBody = RigidBodies[num];
				if (rigidBody.IsSleeping)
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
				Pose pose = rigidBody.Pose;
				if (!pose.Position.IsNaN && !pose.Orientation.IsNaN)
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
					if (GeometryHelper.HaveContact(rigidBody.Aabb, World.Aabb))
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
				}
				RigidBodies.Remove(rigidBody);
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
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Physics.Simulation.SubTimeStepFinished" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///   <see cref="T:System.EventArgs" /> object that provides the arguments for the event.
	///       </param>
	protected virtual void OnSubTimeStepFinished(EventArgs eventArgs)
	{
		EventHandler eventHandler = c521f0caca5a747ecffe180c1dff6e608;
		if (eventHandler == null)
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
				__ldtoken(Simulation.OnSubTimeStepFinished(EventArgs));
			}
			eventHandler(this, eventArgs);
			return;
		}
	}

	[CompilerGenerated]
	private void cee7f26da6adf5529bef1b7b5761d34d1(int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		RigidBody rigidBody = RigidBodies[cdeb496a0c3d046b6bc838defe2d220d0];
		rigidBody.c36bda22bcb218ef5a106e2eb3ad0058c(c3abbe5d8d6d59363f57a23363fafdc17);
	}

	[CompilerGenerated]
	private void ca68f0967e7eb36f8db54b1fdf833338d(int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		RigidBody rigidBody = RigidBodies[cdeb496a0c3d046b6bc838defe2d220d0];
		rigidBody.c33164d470800b44e1b9c7f00db54e817(c3abbe5d8d6d59363f57a23363fafdc17);
	}
}
