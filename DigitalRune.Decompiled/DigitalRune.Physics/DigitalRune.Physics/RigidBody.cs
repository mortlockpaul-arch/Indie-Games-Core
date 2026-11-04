using System;
using System.Runtime.CompilerServices;
using System.Threading;
using A;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Physics.Constraints;
using DigitalRune.Physics.Materials;

namespace DigitalRune.Physics;

/// <summary>
///       Represents a rigid body.
///       </summary>
public class RigidBody : IGeometricObject, INamedObject, ICloneable
{
	private float cde649bc90ccfb7b0acac0b5aed096655;

	private bool ca120c24eb0da93f2ffee7fd28150d65b;

	internal float c075558120e08634a46735a58fcaf20a7;

	internal Vector3F cdb4f28852f8fdd848be747eebc3db719;

	internal Vector3F ce4d650b898b2c22b831fd9383017176f;

	internal Vector3F cde1e376686dbbfe635cf5179a3745894;

	internal Vector3F c987e687b006e2ed060a162fb236a4292;

	private Aabb c3a708fca2e00c0f5027ce857e9da5624;

	private bool c98eff13aac2c9760773f96dcf2f23831;

	private Pose cd337f7a206e7977cce3e1030296b1b57;

	private Pose c0464c558b3d17fa6a173be04fb7119a1;

	private Shape cc3d25c57005ac063144d857d0f17c454;

	private Vector3F c8a035e79b582cd5fd7cd2b1a6f489725;

	private EventHandler<EventArgs> c2ef3bc618d5a70efa57fdaea000c762f;

	private EventHandler<ShapeChangedEventArgs> cc96b25121a2c4b3535aafb716db5a490;

	private IMaterial c27637ecabe8a564fcf502a363f40d1bd;

	private MotionType c3b0c25df1ace0528add58c51678b0bc6;

	private Simulation c729503850b1dde68cce1c94f7a27d104;

	private MassFrame ca5d81da64995ab664e9fdc0e800476ad;

	private float c17844fdaef32a19069e786a1f6dd657c;

	private Matrix33F ccea184e9e6f091d83376b22c729dd562;

	private bool ccc15fd1926ce89a5ff63b9d1fdec8354;

	private bool cacffc4aa1212fc450a8b5dc46eef96ce;

	private bool ce241fac5309e985f2b14d8946d88a172;

	internal Vector3F c9db2f3d2ea57a42c0954897116e7e018;

	internal Vector3F c8cc59972c02a657a040f0f4333ff4c3a;

	internal Vector3F cb310332bf0a7b2aabfc439d94b2ed22f;

	internal Vector3F c375c3334a1cc1adb185ce6b0ebc20ce7;

	[CompilerGenerated]
	private bool c9aa8ae568cdc2db52790068879dfe3c3;

	[CompilerGenerated]
	private bool c0c225a3048c3668525aa602a80e258ab;

	[CompilerGenerated]
	private bool c438affc5acefa3fd9a561a27a63df697;

	[CompilerGenerated]
	private bool cebea294255189f2179d0f16df75e2fc4;

	[CompilerGenerated]
	private Pose ca2eb55eeb5c861205d6a29a7c397c93c;

	[CompilerGenerated]
	private CollisionObject c9f70e8b23bf60595b29e28c6f9beb85d;

	[CompilerGenerated]
	private bool cc24a7684ea0c7439bbd6cae9d279506a;

	[CompilerGenerated]
	private string c4562595c3012a609fed551ff9cc45ada;

	[CompilerGenerated]
	private object c0df89c0ee57aa7bc1eed1e7eeab72eb6;

	[CompilerGenerated]
	private int c3568679eff6b7fa5130c6d450eada207;

	[CompilerGenerated]
	private c2963fb90db42c0068cb273726df5881d ceae2d1316f36122e6dd101f7bfaaaa80;

	[CompilerGenerated]
	private bool cfa6cb09792013b5d813894d94c2396c8;

	/// <summary>
	///       Gets or sets a value indicating whether this body can sleep.
	///       </summary>
	public bool CanSleep
	{
		get
		{
			return ca120c24eb0da93f2ffee7fd28150d65b;
		}
		set
		{
			ca120c24eb0da93f2ffee7fd28150d65b = value;
			if (ca120c24eb0da93f2ffee7fd28150d65b)
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
				if (1 == 0)
				{
					__ldtoken(RigidBody.set_CanSleep(bool));
				}
				IsSleeping = false;
				return;
			}
		}
	}

	internal bool c0d4caf414e813f4e1505938c05c05689 => cde649bc90ccfb7b0acac0b5aed096655 >= Simulation.Settings.Sleeping.TimeThreshold;

	/// <summary>
	///       Gets a value indicating whether this rigid body is sleeping.
	///       </summary>
	public bool IsSleeping
	{
		[CompilerGenerated]
		get
		{
			return c9aa8ae568cdc2db52790068879dfe3c3;
		}
		[CompilerGenerated]
		private set
		{
			c9aa8ae568cdc2db52790068879dfe3c3 = value;
		}
	}

	internal bool IslandTouchesImmovable
	{
		[CompilerGenerated]
		get
		{
			return c0c225a3048c3668525aa602a80e258ab;
		}
		[CompilerGenerated]
		set
		{
			c0c225a3048c3668525aa602a80e258ab = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether Continuous Collision Detection (CCD) is enabled.
	///       </summary>
	public bool CcdEnabled
	{
		[CompilerGenerated]
		get
		{
			return c438affc5acefa3fd9a561a27a63df697;
		}
		[CompilerGenerated]
		set
		{
			c438affc5acefa3fd9a561a27a63df697 = value;
		}
	}

	internal bool IsCcdActive
	{
		[CompilerGenerated]
		get
		{
			return cebea294255189f2179d0f16df75e2fc4;
		}
		[CompilerGenerated]
		set
		{
			cebea294255189f2179d0f16df75e2fc4 = value;
		}
	}

	/// <summary>
	///       Gets or sets the linear velocity of this body in world space.
	///       </summary>
	public Vector3F LinearVelocity
	{
		get
		{
			return cdb4f28852f8fdd848be747eebc3db719;
		}
		set
		{
			if (!value.IsNaN)
			{
				cdb4f28852f8fdd848be747eebc3db719 = value;
				if (!IsSleeping)
				{
					return;
				}
				if (Simulation != null)
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
						__ldtoken(RigidBody.set_LinearVelocity(Vector3F));
					}
					if (!(cdb4f28852f8fdd848be747eebc3db719.LengthSquared > Simulation.Settings.Sleeping.LinearVelocityThresholdSquared))
					{
						return;
					}
				}
				WakeUp();
			}
			else
			{
				cdb4f28852f8fdd848be747eebc3db719 = Vector3F.Zero;
			}
		}
	}

	/// <summary>
	///       Gets or sets the angular velocity about the center of mass in world space.
	///       </summary>
	public Vector3F AngularVelocity
	{
		get
		{
			return ce4d650b898b2c22b831fd9383017176f;
		}
		set
		{
			if (!value.IsNaN)
			{
				ce4d650b898b2c22b831fd9383017176f = value;
				if (!IsSleeping)
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
						__ldtoken(RigidBody.set_AngularVelocity(Vector3F));
					}
					if (Simulation != null)
					{
						if (!(ce4d650b898b2c22b831fd9383017176f.LengthSquared > Simulation.Settings.Sleeping.AngularVelocityThresholdSquared))
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
							break;
						}
					}
					WakeUp();
					return;
				}
			}
			ce4d650b898b2c22b831fd9383017176f = Vector3F.Zero;
		}
	}

	/// <summary>
	///       Gets the rotational kinetic energy.
	///       </summary>
	public float RotationalEnergy
	{
		get
		{
			Vector3F angularVelocity = AngularVelocity;
			Matrix33F matrix33F = c1abeda2f0cf341df50f3b6f3862cdd63;
			Vector3F vector = matrix33F * angularVelocity;
			return 0.5f * Vector3F.Dot(angularVelocity, vector);
		}
	}

	/// <summary>
	///       Gets the translational kinetic energy.
	///       </summary>
	public float TranslationalEnergy
	{
		get
		{
			float length = LinearVelocity.Length;
			return 0.5f * MassFrame.Mass * length * length;
		}
	}

	/// <summary>
	///       Gets the kinetic energy.
	///       </summary>
	public float KineticEnergy => TranslationalEnergy + RotationalEnergy;

	/// <summary>
	///       Gets the axis-aligned bounding box (AABB).
	///       </summary>
	public Aabb Aabb
	{
		get
		{
			if (!c98eff13aac2c9760773f96dcf2f23831)
			{
				c3a708fca2e00c0f5027ce857e9da5624 = Shape.GetAabb(Scale, Pose);
				c98eff13aac2c9760773f96dcf2f23831 = true;
				if (IsCcdActive)
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
						__ldtoken(RigidBody.get_Aabb());
					}
					Aabb aabb = Shape.GetAabb(Scale, TargetPose);
					c3a708fca2e00c0f5027ce857e9da5624.Grow(aabb);
				}
			}
			return c3a708fca2e00c0f5027ce857e9da5624;
		}
	}

	/// <summary>
	///       Gets or sets the pose (position and orientation).
	///       </summary>
	public Pose Pose
	{
		get
		{
			return cd337f7a206e7977cce3e1030296b1b57;
		}
		set
		{
			if (cd337f7a206e7977cce3e1030296b1b57 != value)
			{
				cd337f7a206e7977cce3e1030296b1b57 = value;
				c3c12ccb0e0feb2f78a298059c5a1223b();
			}
		}
	}

	/// <summary>
	///       Gets or sets the pose (position and orientation) of the center of mass.
	///       </summary>
	public Pose PoseCenterOfMass
	{
		get
		{
			return c0464c558b3d17fa6a173be04fb7119a1;
		}
		set
		{
			if (!(c0464c558b3d17fa6a173be04fb7119a1 != value))
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
					__ldtoken(RigidBody.set_PoseCenterOfMass(Pose));
				}
				c0464c558b3d17fa6a173be04fb7119a1 = value;
				c497a60acf433a006cd4a48049498aa67();
				return;
			}
		}
	}

	internal Pose TargetPose
	{
		[CompilerGenerated]
		get
		{
			return ca2eb55eeb5c861205d6a29a7c397c93c;
		}
		[CompilerGenerated]
		set
		{
			ca2eb55eeb5c861205d6a29a7c397c93c = value;
		}
	}

	/// <summary>
	///       Gets the shape.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public Shape Shape
	{
		get
		{
			return cc3d25c57005ac063144d857d0f17c454;
		}
		set
		{
			if (cc3d25c57005ac063144d857d0f17c454 == value)
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
							__ldtoken(RigidBody.set_Shape(Shape));
						}
						return;
					}
				}
			}
			if (value == null)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1));
					}
				}
			}
			if (cc3d25c57005ac063144d857d0f17c454 != null)
			{
				cc3d25c57005ac063144d857d0f17c454.Changed -= c8d854b6330cdc452eac9c19732fabc3c;
			}
			cc3d25c57005ac063144d857d0f17c454 = value;
			cc3d25c57005ac063144d857d0f17c454.Changed += c8d854b6330cdc452eac9c19732fabc3c;
			OnShapeChanged(ShapeChangedEventArgs.Empty);
		}
	}

	/// <summary>
	///       Gets or sets the scale.
	///       </summary>
	public Vector3F Scale
	{
		get
		{
			return c8a035e79b582cd5fd7cd2b1a6f489725;
		}
		set
		{
			if (!(c8a035e79b582cd5fd7cd2b1a6f489725 != value))
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
					__ldtoken(RigidBody.set_Scale(Vector3F));
				}
				c8a035e79b582cd5fd7cd2b1a6f489725 = value;
				OnShapeChanged(ShapeChangedEventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Gets the collision object.
	///       </summary>
	public CollisionObject CollisionObject
	{
		[CompilerGenerated]
		get
		{
			return c9f70e8b23bf60595b29e28c6f9beb85d;
		}
		[CompilerGenerated]
		private set
		{
			c9f70e8b23bf60595b29e28c6f9beb85d = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether collision response is enabled.
	///       </summary>
	public bool CollisionResponseEnabled
	{
		[CompilerGenerated]
		get
		{
			return cc24a7684ea0c7439bbd6cae9d279506a;
		}
		[CompilerGenerated]
		set
		{
			cc24a7684ea0c7439bbd6cae9d279506a = value;
		}
	}

	/// <summary>
	///       Gets or sets the material.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public IMaterial Material
	{
		get
		{
			return c27637ecabe8a564fcf502a363f40d1bd;
		}
		set
		{
			if (value == null)
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
							__ldtoken(RigidBody.set_Material(IMaterial));
						}
						throw new ArgumentNullException();
					}
				}
			}
			c27637ecabe8a564fcf502a363f40d1bd = value;
		}
	}

	/// <summary>
	///       Gets or sets the motion type.
	///       </summary>
	public MotionType MotionType
	{
		get
		{
			return c3b0c25df1ace0528add58c51678b0bc6;
		}
		set
		{
			if (c3b0c25df1ace0528add58c51678b0bc6 == value)
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
					__ldtoken(RigidBody.set_MotionType(MotionType));
				}
				c3b0c25df1ace0528add58c51678b0bc6 = value;
				c11a95d585ff3a525b28e31dc1898a636();
				if (c3b0c25df1ace0528add58c51678b0bc6 != MotionType.Static)
				{
					while (true)
					{
						switch (6)
						{
						case 0:
							break;
						default:
							WakeUp();
							return;
						}
					}
				}
				Sleep();
				return;
			}
		}
	}

	/// <summary>
	///       Gets the name.
	///       </summary>
	public string Name
	{
		[CompilerGenerated]
		get
		{
			return c4562595c3012a609fed551ff9cc45ada;
		}
		[CompilerGenerated]
		set
		{
			c4562595c3012a609fed551ff9cc45ada = value;
		}
	}

	/// <summary>
	///       Gets the simulation.
	///       </summary>
	public Simulation Simulation
	{
		get
		{
			return c729503850b1dde68cce1c94f7a27d104;
		}
		internal set
		{
			if (c729503850b1dde68cce1c94f7a27d104 == value)
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
				if (1 == 0)
				{
					__ldtoken(RigidBody.set_Simulation(Simulation));
				}
				if (c729503850b1dde68cce1c94f7a27d104 != null)
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
					OnRemoveFromSimulation();
				}
				c729503850b1dde68cce1c94f7a27d104 = value;
				if (c729503850b1dde68cce1c94f7a27d104 == null)
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
					OnAddToSimulation();
					return;
				}
			}
		}
	}

	/// <summary>
	///       Gets or sets the user data.
	///       </summary>
	public object UserData
	{
		[CompilerGenerated]
		get
		{
			return c0df89c0ee57aa7bc1eed1e7eeab72eb6;
		}
		[CompilerGenerated]
		set
		{
			c0df89c0ee57aa7bc1eed1e7eeab72eb6 = value;
		}
	}

	internal int IslandId
	{
		[CompilerGenerated]
		get
		{
			return c3568679eff6b7fa5130c6d450eada207;
		}
		[CompilerGenerated]
		set
		{
			c3568679eff6b7fa5130c6d450eada207 = value;
		}
	}

	internal c2963fb90db42c0068cb273726df5881d BuoyancyData
	{
		[CompilerGenerated]
		get
		{
			return ceae2d1316f36122e6dd101f7bfaaaa80;
		}
		[CompilerGenerated]
		set
		{
			ceae2d1316f36122e6dd101f7bfaaaa80 = value;
		}
	}

	/// <summary>
	///       Gets or sets the mass frame which defines the mass properties of this body.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public MassFrame MassFrame
	{
		get
		{
			return ca5d81da64995ab664e9fdc0e800476ad;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (ca5d81da64995ab664e9fdc0e800476ad != value)
			{
				ca5d81da64995ab664e9fdc0e800476ad = value;
				WakeUp();
				ca8a47c08ad997e3d543389e91b7a3943();
				c11a95d585ff3a525b28e31dc1898a636();
			}
		}
	}

	/// <summary>
	///       Gets the effective inverse mass.
	///       </summary>
	public float MassInverse
	{
		get
		{
			return c17844fdaef32a19069e786a1f6dd657c;
		}
		private set
		{
			c17844fdaef32a19069e786a1f6dd657c = value;
		}
	}

	internal Matrix33F c1abeda2f0cf341df50f3b6f3862cdd63
	{
		get
		{
			Matrix33F orientation = PoseCenterOfMass.Orientation;
			return orientation * Matrix33F.CreateScale(MassFrame.Inertia) * orientation.Transposed;
		}
	}

	/// <summary>
	///       Gets the effective inverse inertia.
	///       </summary>
	public Matrix33F InertiaInverseWorld
	{
		get
		{
			return ccea184e9e6f091d83376b22c729dd562;
		}
		private set
		{
			ccea184e9e6f091d83376b22c729dd562 = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether the <see cref="P:DigitalRune.Physics.RigidBody.MassFrame" /> is automatically updated
	///       if the <see cref="P:DigitalRune.Physics.RigidBody.Shape" /> or <see cref="P:DigitalRune.Physics.RigidBody.Scale" /> of this body is changed.
	///       </summary>
	public bool AutoUpdateMass
	{
		[CompilerGenerated]
		get
		{
			return cfa6cb09792013b5d813894d94c2396c8;
		}
		[CompilerGenerated]
		set
		{
			cfa6cb09792013b5d813894d94c2396c8 = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether the body can rotate around its local mass frame 
	///       x-axis.
	///       </summary>
	public bool LockRotationX
	{
		get
		{
			return ccc15fd1926ce89a5ff63b9d1fdec8354;
		}
		set
		{
			ccc15fd1926ce89a5ff63b9d1fdec8354 = value;
			c11a95d585ff3a525b28e31dc1898a636();
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether the body can rotate around its local mass frame 
	///       y-axis.
	///       </summary>
	public bool LockRotationY
	{
		get
		{
			return cacffc4aa1212fc450a8b5dc46eef96ce;
		}
		set
		{
			cacffc4aa1212fc450a8b5dc46eef96ce = value;
			c11a95d585ff3a525b28e31dc1898a636();
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether the body can rotate around its local mass frame 
	///       z-axis.
	///       </summary>
	public bool LockRotationZ
	{
		get
		{
			return ce241fac5309e985f2b14d8946d88a172;
		}
		set
		{
			ce241fac5309e985f2b14d8946d88a172 = value;
			c11a95d585ff3a525b28e31dc1898a636();
		}
	}

	/// <summary>
	///       Occurs when the pose was changed.
	///       </summary>
	public event EventHandler<EventArgs> PoseChanged
	{
		add
		{
			EventHandler<EventArgs> eventHandler = c2ef3bc618d5a70efa57fdaea000c762f;
			EventHandler<EventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<EventArgs> value2 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref c2ef3bc618d5a70efa57fdaea000c762f, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(RigidBody.add_PoseChanged(EventHandler<EventArgs>));
				}
				return;
			}
		}
		remove
		{
			EventHandler<EventArgs> eventHandler = c2ef3bc618d5a70efa57fdaea000c762f;
			EventHandler<EventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<EventArgs> value2 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref c2ef3bc618d5a70efa57fdaea000c762f, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(RigidBody.remove_PoseChanged(EventHandler<EventArgs>));
				}
				return;
			}
		}
	}

	/// <summary>
	///       Occurs when the <see cref="P:DigitalRune.Physics.RigidBody.Shape" /> or <see cref="P:DigitalRune.Physics.RigidBody.Scale" /> was changed.
	///       </summary>
	public event EventHandler<ShapeChangedEventArgs> ShapeChanged
	{
		add
		{
			EventHandler<ShapeChangedEventArgs> eventHandler = cc96b25121a2c4b3535aafb716db5a490;
			EventHandler<ShapeChangedEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<ShapeChangedEventArgs> value2 = (EventHandler<ShapeChangedEventArgs>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref cc96b25121a2c4b3535aafb716db5a490, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(RigidBody.add_ShapeChanged(EventHandler<ShapeChangedEventArgs>));
				}
				return;
			}
		}
		remove
		{
			EventHandler<ShapeChangedEventArgs> eventHandler = cc96b25121a2c4b3535aafb716db5a490;
			EventHandler<ShapeChangedEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<ShapeChangedEventArgs> value2 = (EventHandler<ShapeChangedEventArgs>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref cc96b25121a2c4b3535aafb716db5a490, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(RigidBody.remove_ShapeChanged(EventHandler<ShapeChangedEventArgs>));
				}
				return;
			}
		}
	}

	static RigidBody()
	{
		TypeInitializer.Initialize(c31a405ff5d24b3c4be569f1e85a6f73f.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(13, c31a405ff5d24b3c4be569f1e85a6f73f.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.RigidBody" /> class.
	///       </summary>
	public RigidBody()
		: this(null, null, null)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.RigidBody" /> class.
	///       </summary>
	/// <param name="shape">
	///       The shape. Can be <see langword="null" /> to use the default <see cref="P:DigitalRune.Physics.RigidBody.Shape" />.
	///       </param>
	public RigidBody(Shape shape)
		: this(shape, null, null)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.RigidBody" /> class.
	///       </summary>
	/// <param name="shape">
	///       The shape. Can be <see langword="null" /> to use the default <see cref="P:DigitalRune.Physics.RigidBody.Shape" />.
	///       </param>
	/// <param name="massFrame">
	///       The mass frame. Can be <see langword="null" /> in which case the mass properties for
	///       a density of 1000 are used.
	///       </param>
	/// <param name="material">
	///       The material. Can be <see langword="null" /> to use the default <see cref="P:DigitalRune.Physics.RigidBody.Material" />.
	///       </param>
	public RigidBody(Shape shape, MassFrame massFrame, IMaterial material)
	{
		AutoUpdateMass = true;
		IslandId = -1;
		Name = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1714);
		cd337f7a206e7977cce3e1030296b1b57 = Pose.Identity;
		cc3d25c57005ac063144d857d0f17c454 = shape ?? new BoxShape(1f, 1f, 1f);
		cc3d25c57005ac063144d857d0f17c454.Changed += c8d854b6330cdc452eac9c19732fabc3c;
		c8a035e79b582cd5fd7cd2b1a6f489725 = Vector3F.One;
		object obj = material;
		if (obj == null)
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
				__ldtoken(RigidBody._002Ector(Shape, MassFrame, IMaterial));
			}
			obj = new UniformMaterial();
		}
		Material = (IMaterial)obj;
		if (massFrame != null)
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
			MassFrame = massFrame;
		}
		else
		{
			c3390a6aeb78372f2b910662e1bae61cd();
		}
		CollisionResponseEnabled = true;
		MotionType = MotionType.Dynamic;
		CollisionObject = new CollisionObject(this);
		CanSleep = true;
		IsSleeping = false;
		c075558120e08634a46735a58fcaf20a7 = 1f;
	}

	internal void c4ada8b8e9f664f77c14206a9c1cb6a17(float ca596459ee67fc0bf53c7ee7ef778aa7e)
	{
		if (MotionType == MotionType.Static)
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
				__ldtoken(RigidBody.c4ada8b8e9f664f77c14206a9c1cb6a17(float));
			}
			IsSleeping = false;
			cde649bc90ccfb7b0acac0b5aed096655 = Math.Min(cde649bc90ccfb7b0acac0b5aed096655, Simulation.Settings.Sleeping.TimeThreshold - ca596459ee67fc0bf53c7ee7ef778aa7e);
			return;
		}
	}

	/// <summary>
	///       Deactivates this rigid body. 
	///       </summary>
	public void Sleep()
	{
		if (CanSleep)
		{
			IsSleeping = true;
			cdb4f28852f8fdd848be747eebc3db719 = Vector3F.Zero;
			ce4d650b898b2c22b831fd9383017176f = Vector3F.Zero;
			cde649bc90ccfb7b0acac0b5aed096655 = float.PositiveInfinity;
		}
	}

	/// <summary>
	///       Wakes the rigid body up from sleeping.
	///       </summary>
	public void WakeUp()
	{
		if (MotionType != MotionType.Static)
		{
			IsSleeping = false;
			cde649bc90ccfb7b0acac0b5aed096655 = 0f;
		}
	}

	private void c6d80a4ef3e6917ee0098728b998d75c6(float ce8c71427a0828475a969c745c8335cde)
	{
		if (IsSleeping)
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
				__ldtoken(RigidBody.c6d80a4ef3e6917ee0098728b998d75c6(float));
			}
			if (!CanSleep)
			{
				return;
			}
			if (LinearVelocity.LengthSquared < Simulation.Settings.Sleeping.LinearVelocityThresholdSquared && AngularVelocity.LengthSquared < Simulation.Settings.Sleeping.AngularVelocityThresholdSquared)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						cde649bc90ccfb7b0acac0b5aed096655 += ce8c71427a0828475a969c745c8335cde;
						if (cde649bc90ccfb7b0acac0b5aed096655 > Simulation.Settings.Sleeping.TimeThreshold)
						{
							IsSleeping = true;
						}
						return;
					}
				}
			}
			cde649bc90ccfb7b0acac0b5aed096655 = 0f;
			return;
		}
	}

	/// <summary>
	///       Applies an impulse at a given position.
	///       </summary>
	/// <param name="impulseWorld">The impulse in world space.</param>
	/// <param name="positionWorld">
	///       The position where the impulse is applied in world space.
	///       </param>
	public void ApplyImpulse(Vector3F impulseWorld, Vector3F positionWorld)
	{
		if (MotionType == MotionType.Dynamic)
		{
			if (IsSleeping)
			{
				WakeUp();
			}
			Vector3F vector = positionWorld - PoseCenterOfMass.Position;
			LinearVelocity += MassInverse * impulseWorld;
			AngularVelocity += InertiaInverseWorld * Vector3F.Cross(vector, impulseWorld);
		}
	}

	/// <summary>
	///       Applies a linear impulse at the center of mass
	///       </summary>
	/// <param name="impulseWorld">The impulse in world space.</param>
	public void ApplyLinearImpulse(Vector3F impulseWorld)
	{
		if (MotionType == MotionType.Dynamic)
		{
			if (IsSleeping)
			{
				WakeUp();
			}
			LinearVelocity += MassInverse * impulseWorld;
		}
	}

	/// <summary>
	///       Applies an angular impulse at the center of mass
	///       </summary>
	/// <param name="impulseWorld">The impulse in world space.</param>
	public void ApplyAngularImpulse(Vector3F impulseWorld)
	{
		if (MotionType == MotionType.Dynamic)
		{
			if (IsSleeping)
			{
				WakeUp();
			}
			AngularVelocity += InertiaInverseWorld * impulseWorld;
		}
	}

	internal void c47dadd8a7c3a05eb9bfcf9d2af08e003(Vector3F cf85380076fc01bcec720339ab5ff0374, Vector3F cb99e1b59abf001edcb91dcd4a599ef8e)
	{
		if (MotionType == MotionType.Dynamic)
		{
			Vector3F vector = cb99e1b59abf001edcb91dcd4a599ef8e - PoseCenterOfMass.Position;
			cde1e376686dbbfe635cf5179a3745894 += MassInverse * cf85380076fc01bcec720339ab5ff0374;
			c987e687b006e2ed060a162fb236a4292 += InertiaInverseWorld * Vector3F.Cross(vector, cf85380076fc01bcec720339ab5ff0374);
		}
	}

	/// <summary>
	///       Gets the velocity of a point on the rigid body.
	///       </summary>
	/// <param name="positionWorld">The position of the point in world space.</param>
	/// <returns>
	///       The velocity of the point on the rigid body in world space.
	///       </returns>
	public Vector3F GetVelocityOfWorldPoint(Vector3F positionWorld)
	{
		return LinearVelocity + Vector3F.Cross(AngularVelocity, positionWorld - PoseCenterOfMass.Position);
	}

	/// <summary>
	///       Gets the velocity of a point on the rigid body.
	///       </summary>
	/// <param name="positionLocal">
	///       The position of the point in the local space of the rigid body.
	///       </param>
	/// <returns>
	///       The velocity of the point on the rigid body in world space.
	///       </returns>
	public virtual Vector3F GetVelocityOfLocalPoint(Vector3F positionLocal)
	{
		return GetVelocityOfWorldPoint(Pose.ToWorldPosition(positionLocal));
	}

	internal void c36bda22bcb218ef5a106e2eb3ad0058c(float ce8c71427a0828475a969c745c8335cde)
	{
		c6d80a4ef3e6917ee0098728b998d75c6(ce8c71427a0828475a969c745c8335cde);
		if (MotionType == MotionType.Dynamic && !IsSleeping)
		{
			Vector3F linearVelocity = default;
			linearVelocity.X = cdb4f28852f8fdd848be747eebc3db719.X + ce8c71427a0828475a969c745c8335cde * (c9db2f3d2ea57a42c0954897116e7e018.X * c17844fdaef32a19069e786a1f6dd657c);
			linearVelocity.Y = cdb4f28852f8fdd848be747eebc3db719.Y + ce8c71427a0828475a969c745c8335cde * (c9db2f3d2ea57a42c0954897116e7e018.Y * c17844fdaef32a19069e786a1f6dd657c);
			linearVelocity.Z = cdb4f28852f8fdd848be747eebc3db719.Z + ce8c71427a0828475a969c745c8335cde * (c9db2f3d2ea57a42c0954897116e7e018.Z * c17844fdaef32a19069e786a1f6dd657c);
			LinearVelocity = linearVelocity;
			Vector3F angularVelocity = default;
			angularVelocity.X = ce4d650b898b2c22b831fd9383017176f.X + ce8c71427a0828475a969c745c8335cde * (ccea184e9e6f091d83376b22c729dd562.M00 * c8cc59972c02a657a040f0f4333ff4c3a.X + ccea184e9e6f091d83376b22c729dd562.M01 * c8cc59972c02a657a040f0f4333ff4c3a.Y + ccea184e9e6f091d83376b22c729dd562.M02 * c8cc59972c02a657a040f0f4333ff4c3a.Z);
			angularVelocity.Y = ce4d650b898b2c22b831fd9383017176f.Y + ce8c71427a0828475a969c745c8335cde * (ccea184e9e6f091d83376b22c729dd562.M10 * c8cc59972c02a657a040f0f4333ff4c3a.X + ccea184e9e6f091d83376b22c729dd562.M11 * c8cc59972c02a657a040f0f4333ff4c3a.Y + ccea184e9e6f091d83376b22c729dd562.M12 * c8cc59972c02a657a040f0f4333ff4c3a.Z);
			angularVelocity.Z = ce4d650b898b2c22b831fd9383017176f.Z + ce8c71427a0828475a969c745c8335cde * (ccea184e9e6f091d83376b22c729dd562.M20 * c8cc59972c02a657a040f0f4333ff4c3a.X + ccea184e9e6f091d83376b22c729dd562.M21 * c8cc59972c02a657a040f0f4333ff4c3a.Y + ccea184e9e6f091d83376b22c729dd562.M22 * c8cc59972c02a657a040f0f4333ff4c3a.Z);
			AngularVelocity = angularVelocity;
		}
	}

	internal void c33164d470800b44e1b9c7f00db54e817(float ce8c71427a0828475a969c745c8335cde)
	{
		if (MotionType == MotionType.Static)
		{
			return;
		}
		if (IsSleeping)
		{
			cdb4f28852f8fdd848be747eebc3db719 = Vector3F.Zero;
			ce4d650b898b2c22b831fd9383017176f = Vector3F.Zero;
			return;
		}
		if (cdb4f28852f8fdd848be747eebc3db719.IsNaN)
		{
			cdb4f28852f8fdd848be747eebc3db719 = Vector3F.Zero;
		}
		else if (cdb4f28852f8fdd848be747eebc3db719.LengthSquared > Simulation.Settings.Motion.MaxLinearVelocitySquared)
		{
			cdb4f28852f8fdd848be747eebc3db719.Length = Simulation.Settings.Motion.MaxLinearVelocity;
		}
		if (ce4d650b898b2c22b831fd9383017176f.IsNaN)
		{
			ce4d650b898b2c22b831fd9383017176f = Vector3F.Zero;
		}
		else if (ce4d650b898b2c22b831fd9383017176f.LengthSquared > Simulation.Settings.Motion.MaxAngularVelocitySquared)
		{
			ce4d650b898b2c22b831fd9383017176f.Length = Simulation.Settings.Motion.MaxAngularVelocity;
		}
		Vector3F position = PoseCenterOfMass.Position;
		QuaternionF quaternionF = QuaternionF.CreateRotation(PoseCenterOfMass.Orientation);
		Vector3F vector3F = LinearVelocity + cde1e376686dbbfe635cf5179a3745894;
		QuaternionF quaternionF2 = 0.5f * new QuaternionF(0f, AngularVelocity + c987e687b006e2ed060a162fb236a4292) * quaternionF;
		Pose pose = new Pose(position + ce8c71427a0828475a969c745c8335cde * vector3F, (quaternionF + ce8c71427a0828475a969c745c8335cde * quaternionF2).Normalized);
		if (CcdEnabled && Simulation.Settings.Motion.CcdEnabled && LinearVelocity.LengthSquared > Simulation.Settings.Motion.CcdVelocityThresholdSquared)
		{
			IsCcdActive = true;
			c075558120e08634a46735a58fcaf20a7 = 1f;
			Simulation.CcdRequested = true;
			TargetPose = pose * MassFrame.Pose.Inverse;
			c98eff13aac2c9760773f96dcf2f23831 = false;
			OnPoseChanged(EventArgs.Empty);
		}
		else
		{
			IsCcdActive = false;
			PoseCenterOfMass = pose;
		}
		cde1e376686dbbfe635cf5179a3745894 = Vector3F.Zero;
		c987e687b006e2ed060a162fb236a4292 = Vector3F.Zero;
	}

	private void c3c12ccb0e0feb2f78a298059c5a1223b()
	{
		c98eff13aac2c9760773f96dcf2f23831 = false;
		if (IsSleeping)
		{
			WakeUp();
		}
		ca8a47c08ad997e3d543389e91b7a3943();
		c11a95d585ff3a525b28e31dc1898a636();
		OnPoseChanged(EventArgs.Empty);
	}

	private void c497a60acf433a006cd4a48049498aa67()
	{
		c98eff13aac2c9760773f96dcf2f23831 = false;
		if (IsSleeping)
		{
			WakeUp();
		}
		c33164d470800b44e1b9c7f00db54e817();
		c11a95d585ff3a525b28e31dc1898a636();
		OnPoseChanged(EventArgs.Empty);
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Physics.RigidBody.PoseChanged" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///   <see cref="T:System.EventArgs" /> object that provides the arguments for the event.
	///       </param>
	protected virtual void OnPoseChanged(EventArgs eventArgs)
	{
		c2ef3bc618d5a70efa57fdaea000c762f?.Invoke(this, eventArgs);
	}

	private void c8d854b6330cdc452eac9c19732fabc3c(object c807f682152464d11dde83a885bf87124, ShapeChangedEventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		OnShapeChanged(c7fefeb91d4632901bd52a1f245b70db1);
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Physics.RigidBody.ShapeChanged" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///   <see cref="T:DigitalRune.Geometry.Shapes.ShapeChangedEventArgs" /> object that provides the arguments for the event.
	///       </param>
	protected virtual void OnShapeChanged(ShapeChangedEventArgs eventArgs)
	{
		c98eff13aac2c9760773f96dcf2f23831 = false;
		BuoyancyData = null;
		WakeUp();
		c3390a6aeb78372f2b910662e1bae61cd();
		cc96b25121a2c4b3535aafb716db5a490?.Invoke(this, eventArgs);
	}

	private void ca8a47c08ad997e3d543389e91b7a3943()
	{
		c0464c558b3d17fa6a173be04fb7119a1 = cd337f7a206e7977cce3e1030296b1b57 * MassFrame.Pose;
	}

	private void c33164d470800b44e1b9c7f00db54e817()
	{
		cd337f7a206e7977cce3e1030296b1b57 = c0464c558b3d17fa6a173be04fb7119a1 * MassFrame.Pose.Inverse;
	}

	/// <summary>
	///       Called when this rigid body is added to a simulation.
	///       </summary>
	protected virtual void OnAddToSimulation()
	{
		Simulation.CollisionDomain.CollisionObjects.Add(CollisionObject);
	}

	/// <summary>
	///       Called when this rigid body is removed from a simulation.
	///       </summary>
	protected virtual void OnRemoveFromSimulation()
	{
		Simulation.CollisionDomain.CollisionObjects.Remove(CollisionObject);
		for (int num = Simulation.c32f28f83e5bad489c86f1585da1c621f.Count - 1; num >= 0; num--)
		{
			ContactConstraint contactConstraint = Simulation.c32f28f83e5bad489c86f1585da1c621f[num];
			if (contactConstraint.BodyA == this || contactConstraint.BodyB == this)
			{
				contactConstraint.BodyA.WakeUp();
				contactConstraint.BodyB.WakeUp();
				Simulation.c32f28f83e5bad489c86f1585da1c621f.RemoveAt(num);
			}
		}
		for (int num2 = Simulation.Constraints.Count - 1; num2 >= 0; num2--)
		{
			Constraint constraint = Simulation.Constraints[num2];
			if (constraint.BodyA != this)
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
					__ldtoken(RigidBody.OnRemoveFromSimulation());
				}
				if (constraint.BodyB != this)
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
			}
			Simulation.Constraints.RemoveAt(num2);
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			IslandId = -1;
			return;
		}
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents this instance.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents this instance.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1491), Name);
	}

	private object c48423685eed44c8f6f4708513cf05f2b()
	{
		return Clone();
	}

	object ICloneable.Clone()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c48423685eed44c8f6f4708513cf05f2b
		return this.c48423685eed44c8f6f4708513cf05f2b();
	}

	/// <summary>
	///       Creates a new <see cref="T:DigitalRune.Physics.RigidBody" /> that is a clone (deep copy) of the current  instance.
	///       </summary>
	/// <returns>
	///       A new <see cref="T:DigitalRune.Physics.RigidBody" /> that is a clone (deep copy) of the current instance.
	///       </returns>
	public RigidBody Clone()
	{
		RigidBody rigidBody = cec9b74d9244fb114116db0f3a6fbc279();
		rigidBody.CloneCore(this);
		return rigidBody;
	}

	private RigidBody cec9b74d9244fb114116db0f3a6fbc279()
	{
		RigidBody rigidBody = CreateInstanceCore();
		if ((object)GetType() != rigidBody.GetType())
		{
			string message = string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1538), GetType());
			throw new InvalidOperationException(message);
		}
		return rigidBody;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the 
	///       <see cref="T:DigitalRune.Physics.RigidBody" /> derived class. 
	///       </summary>
	/// <returns>The new instance.</returns>
	protected virtual RigidBody CreateInstanceCore()
	{
		return new RigidBody();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Physics.RigidBody" />.
	///       </summary>
	/// <param name="source">The object to clone.</param>
	protected virtual void CloneCore(RigidBody source)
	{
		Material = source.Material;
		MassFrame = source.MassFrame.Clone();
		AutoUpdateMass = source.AutoUpdateMass;
		LockRotationX = source.LockRotationX;
		LockRotationY = source.LockRotationY;
		LockRotationZ = source.LockRotationZ;
		Pose = source.Pose;
		Shape = source.Shape.Clone();
		Scale = source.Scale;
		CollisionObject.CollisionGroup = source.CollisionObject.CollisionGroup;
		CollisionObject.Type = source.CollisionObject.Type;
		CollisionObject.Enabled = source.CollisionObject.Enabled;
		CollisionResponseEnabled = source.CollisionResponseEnabled;
		MotionType = source.MotionType;
		Name = source.Name;
		UserData = ((source.UserData is ICloneable) ? ((ICloneable)source.UserData).Clone() : source.UserData);
		BuoyancyData = source.BuoyancyData;
		CcdEnabled = source.CcdEnabled;
		LinearVelocity = source.LinearVelocity;
		AngularVelocity = source.AngularVelocity;
		CanSleep = source.CanSleep;
	}

	private void c3390a6aeb78372f2b910662e1bae61cd()
	{
		if (MassFrame == null)
		{
			MassFrame = MassFrame.FromShapeAndDensity(Shape, Scale, 1000f, 0.01f, 3);
		}
		else if (AutoUpdateMass)
		{
			if (MassFrame.Density > 0f)
			{
				MassFrame = MassFrame.FromShapeAndDensity(Shape, Scale, MassFrame.Density, 0.01f, 3);
			}
			else
			{
				MassFrame = MassFrame.FromShapeAndMass(Shape, Scale, MassFrame.Mass, 0.01f, 3);
			}
		}
	}

	private void c11a95d585ff3a525b28e31dc1898a636()
	{
		MassInverse = ((MotionType == MotionType.Dynamic) ? MassFrame.c8a351a04f7dae1ec8ab06c1ff3fa4e2f : 0f);
		Vector3F vector3F;
		if (MotionType != MotionType.Dynamic)
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
				__ldtoken(RigidBody.c11a95d585ff3a525b28e31dc1898a636());
			}
			vector3F = Vector3F.Zero;
		}
		else
		{
			vector3F = MassFrame.c9366fd43f24869c3f8a2696505a00f00;
		}
		Vector3F scale = vector3F;
		if (LockRotationX)
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
			scale.X = 0f;
		}
		if (LockRotationY)
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
			scale.Y = 0f;
		}
		if (LockRotationZ)
		{
			scale.Z = 0f;
		}
		Matrix33F orientation = c0464c558b3d17fa6a173be04fb7119a1.Orientation;
		InertiaInverseWorld = orientation * Matrix33F.CreateScale(scale) * orientation.Transposed;
	}

	/// <summary>
	///       Clears all forces that were set with <see cref="M:DigitalRune.Physics.RigidBody.AddForce(DigitalRune.Mathematics.Algebra.Vector3F,DigitalRune.Mathematics.Algebra.Vector3F)" />,
	///       <see cref="M:DigitalRune.Physics.RigidBody.AddForce(DigitalRune.Mathematics.Algebra.Vector3F)" /> or <see cref="M:DigitalRune.Physics.RigidBody.AddTorque(DigitalRune.Mathematics.Algebra.Vector3F)" />.
	///       </summary>
	public void ClearForces()
	{
		cb310332bf0a7b2aabfc439d94b2ed22f = default;
		c375c3334a1cc1adb185ce6b0ebc20ce7 = default;
	}

	/// <summary>
	///       Applies a force to the rigid body.
	///       </summary>
	/// <param name="forceWorld">The force in world space.</param>
	/// <param name="positionWorld">
	///       The position where the force is applied in world space.
	///       </param>
	public void AddForce(Vector3F forceWorld, Vector3F positionWorld)
	{
		Vector3F vector = positionWorld - PoseCenterOfMass.Position;
		cb310332bf0a7b2aabfc439d94b2ed22f += forceWorld;
		c375c3334a1cc1adb185ce6b0ebc20ce7 += Vector3F.Cross(vector, forceWorld);
	}

	/// <summary>
	///       Applies a force at the center of mass of the rigid body.
	///       </summary>
	/// <param name="forceWorld">The force in world space.</param>
	public void AddForce(Vector3F forceWorld)
	{
		cb310332bf0a7b2aabfc439d94b2ed22f += forceWorld;
	}

	/// <summary>
	///       Applies a torque at the center of mass of the rigid body.
	///       </summary>
	/// <param name="torqueWorld">The torque in world space.</param>
	public void AddTorque(Vector3F torqueWorld)
	{
		c375c3334a1cc1adb185ce6b0ebc20ce7 += torqueWorld;
	}
}
