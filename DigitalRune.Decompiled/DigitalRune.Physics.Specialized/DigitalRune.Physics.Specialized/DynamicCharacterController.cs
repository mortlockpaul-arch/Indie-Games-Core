using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Physics.Materials;

namespace DigitalRune.Physics.Specialized;

/// <summary>
///       Controls the movement of a game character.
///       </summary>
public class DynamicCharacterController
{
	[CompilerGenerated]
	private sealed class cea8ef8905f87f99d338a7b7831eaff91
	{
		public GeometricObject c7e079faf55903e6a725512c4571ac537;

		public DynamicCharacterController c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public void ccf6134e3b9d563d5b24933d5574bd94e(object cf3e112fd3d3aa49f1b42cc826ed0e916, EventArgs c4496d1e28885071f753d5d8c6ed007b6)
		{
			c7e079faf55903e6a725512c4571ac537.Pose = c1b0bbfd4af1b6a4c0c71a5dd3c509812.Body.Pose;
		}
	}

	private Vector3F ceed236e559deae7838ad345f8943bc70;

	private Vector3F cbaeb4e660c0fe16cd631be6e28fff0d4;

	private readonly CollisionObject cbcd1055f1a19d37083f7656d531d4b26;

	private float c4715d9a188bd9456099225c1f4ba9553 = 9.81f;

	private float c0b863e3cc499f0ed78d50bf700c73849 = (float)Math.PI / 4f;

	private float c761883ef938eed076b0f3f25560b2e73 = (float)Math.Cos(Math.PI / 4.0);

	private bool? caca9684c1d35ec909f8a977cf16d06c2;

	[CompilerGenerated]
	private Simulation cbebb653f415fd36f75adc579c8bca56d;

	[CompilerGenerated]
	private RigidBody c06605e21c26a6252efbeb1204abb6588;

	[CompilerGenerated]
	private bool c68860899517a94a312380103b1cf7142;

	/// <summary>
	///       Gets or sets the simulation.
	///       </summary>
	public Simulation Simulation
	{
		[CompilerGenerated]
		get
		{
			return cbebb653f415fd36f75adc579c8bca56d;
		}
		[CompilerGenerated]
		private set
		{
			cbebb653f415fd36f75adc579c8bca56d = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether this character controller is enabled.
	///       </summary>
	public bool Enabled
	{
		get
		{
			return Body.Simulation != null;
		}
		set
		{
			if (Enabled == value)
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
							__ldtoken(DynamicCharacterController.set_Enabled(bool));
						}
						return;
					}
				}
			}
			if (value)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						Simulation.RigidBodies.Add(Body);
						Simulation.CollisionDomain.CollisionObjects.Add(cbcd1055f1a19d37083f7656d531d4b26);
						return;
					}
				}
			}
			Simulation.RigidBodies.Remove(Body);
			Simulation.CollisionDomain.CollisionObjects.Remove(cbcd1055f1a19d37083f7656d531d4b26);
		}
	}

	/// <summary>
	///       Gets or sets the body.
	///       </summary>
	public RigidBody Body
	{
		[CompilerGenerated]
		get
		{
			return c06605e21c26a6252efbeb1204abb6588;
		}
		[CompilerGenerated]
		private set
		{
			c06605e21c26a6252efbeb1204abb6588 = value;
		}
	}

	/// <summary>
	///       Gets or sets the collision group.
	///       </summary>
	public int CollisionGroup
	{
		get
		{
			return Body.CollisionObject.CollisionGroup;
		}
		set
		{
			Body.CollisionObject.CollisionGroup = value;
			cbcd1055f1a19d37083f7656d531d4b26.CollisionGroup = value;
		}
	}

	/// <summary>
	///       Gets or sets the height.
	///       </summary>
	public float Height
	{
		get
		{
			return ((CapsuleShape)Body.Shape).Height;
		}
		set
		{
			Vector3F position = Position;
			((CapsuleShape)Body.Shape).Height = value;
			Position = position;
		}
	}

	/// <summary>
	///       Gets or sets the width.
	///       </summary>
	public float Width
	{
		get
		{
			return ((CapsuleShape)Body.Shape).Radius * 2f;
		}
		set
		{
			((CapsuleShape)Body.Shape).Radius = value / 2f;
		}
	}

	/// <summary>
	///       Gets or sets the position of the character.
	///       </summary>
	public Vector3F Position
	{
		get
		{
			return Body.Pose.Position - Height / 2f * Vector3F.UnitY;
		}
		set
		{
			Pose pose = Body.Pose;
			pose.Position = value + Height / 2f * Vector3F.UnitY;
			Body.Pose = pose;
		}
	}

	/// <summary>
	///       Gets or sets the gravity.
	///       </summary>
	public float Gravity
	{
		get
		{
			return c4715d9a188bd9456099225c1f4ba9553;
		}
		set
		{
			c4715d9a188bd9456099225c1f4ba9553 = value;
		}
	}

	/// <summary>
	///       Gets or sets the slope limit (in radians).
	///       </summary>
	public float SlopeLimit
	{
		get
		{
			return c0b863e3cc499f0ed78d50bf700c73849;
		}
		set
		{
			c0b863e3cc499f0ed78d50bf700c73849 = value;
			c761883ef938eed076b0f3f25560b2e73 = (float)Math.Cos(SlopeLimit);
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether this instance is climbing.
	///       </summary>
	public bool IsClimbing
	{
		[CompilerGenerated]
		get
		{
			return c68860899517a94a312380103b1cf7142;
		}
		[CompilerGenerated]
		set
		{
			c68860899517a94a312380103b1cf7142 = value;
		}
	}

	/// <summary>
	///       Gets a value indicating whether this character has ground contact.
	///       </summary>
	public bool HasGroundContact
	{
		get
		{
			if (caca9684c1d35ec909f8a977cf16d06c2.HasValue)
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
							__ldtoken(DynamicCharacterController.get_HasGroundContact());
						}
						return caca9684c1d35ec909f8a977cf16d06c2.Value;
					}
				}
			}
			caca9684c1d35ec909f8a977cf16d06c2 = false;
			foreach (ContactSet contact in Simulation.CollisionDomain.GetContacts(Body.CollisionObject))
			{
				bool flag = Body.CollisionObject != contact.ObjectA;
				using List<Contact>.Enumerator enumerator2 = contact.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Contact current2 = enumerator2.Current;
					Vector3F vector3F;
					if (!flag)
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
						vector3F = -current2.Normal;
					}
					else
					{
						vector3F = current2.Normal;
					}
					Vector3F vector = vector3F;
					if (!(Vector3F.Dot(vector, Vector3F.UnitY) >= c761883ef938eed076b0f3f25560b2e73))
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
						caca9684c1d35ec909f8a977cf16d06c2 = true;
						return true;
					}
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						goto end_IL_00fa;
					}
					continue;
					end_IL_00fa:
					break;
				}
			}
			return false;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Specialized.KinematicCharacterController" /> class.
	///       </summary>
	/// <param name="simulation">The simulation.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="simulation" /> is <see langword="null" />.
	///       </exception>
	public DynamicCharacterController(Simulation simulation)
	{
		if (simulation == null)
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
						__ldtoken(DynamicCharacterController._002Ector(Simulation));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(421));
				}
			}
		}
		Simulation = simulation;
		CapsuleShape capsuleShape = new CapsuleShape(0.4f, 1.8f);
		MassFrame massFrame = new MassFrame
		{
			Mass = 80f
		};
		UniformMaterial material = new UniformMaterial
		{
			StaticFriction = 0f,
			DynamicFriction = 0f,
			Restitution = 0f
		};
		Body = new RigidBody(capsuleShape, massFrame, material)
		{
			AutoUpdateMass = false,
			CanSleep = false,
			CcdEnabled = true,
			LockRotationX = true,
			LockRotationY = true,
			LockRotationZ = true,
			Name = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(382),
			Pose = new Pose(new Vector3F(0f, capsuleShape.Height / 2f, 0f))
		};
		RayShape shape = new RayShape(Vector3F.Zero, -Vector3F.UnitY, capsuleShape.Height / 2f + 0.4f)
		{
			StopsAtFirstHit = true
		};
		GeometricObject c7e079faf55903e6a725512c4571ac537 = new GeometricObject(shape, Body.Pose);
		cbcd1055f1a19d37083f7656d531d4b26 = new CollisionObject(c7e079faf55903e6a725512c4571ac537);
		Body.PoseChanged += (object cf3e112fd3d3aa49f1b42cc826ed0e916, EventArgs c4496d1e28885071f753d5d8c6ed007b6) =>
		{
			c7e079faf55903e6a725512c4571ac537.Pose = Body.Pose;
		};
		CollisionFilter collisionFilter = (CollisionFilter)Simulation.CollisionDomain.CollisionDetection.CollisionFilter;
		collisionFilter.Set(Body.CollisionObject, cbcd1055f1a19d37083f7656d531d4b26, collisionsEnabled: false);
		Enabled = true;
	}

	/// <summary>
	///       Moves the character to a new position while avoiding penetrations and sliding along 
	///       obstacles.
	///       </summary>
	/// <param name="moveVelocity">The desired linear walk or fly velocity.</param>
	/// <param name="jumpVelocity">
	///       The jump velocity. Set a velocity vector to let the character jump. The character will only
	///       jump if it starts on the ground. If gravity is turned off, the character will fly into the
	///       given direction.
	///       </param>
	/// <param name="deltaTime">The size of the time step.</param>
	public void Move(Vector3F moveVelocity, float jumpVelocity, float deltaTime)
	{
		if (!Enabled)
		{
			return;
		}
		caca9684c1d35ec909f8a977cf16d06c2 = null;
		if (Gravity == 0f)
		{
			ceed236e559deae7838ad345f8943bc70 = Vector3F.Zero;
			cbaeb4e660c0fe16cd631be6e28fff0d4 = Vector3F.Zero;
			Body.LinearVelocity = moveVelocity;
			return;
		}
		ContactSet contactSet = Simulation.CollisionDomain.GetContacts(cbcd1055f1a19d37083f7656d531d4b26).FirstOrDefault();
		bool flag = false;
		Vector3F vector3F = Vector3F.UnitY;
		if (contactSet != null)
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
				__ldtoken(DynamicCharacterController.Move(Vector3F, float, float));
			}
			if (contactSet.Count > 0)
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
				Contact contact = contactSet[0];
				vector3F = ((contactSet.ObjectB == cbcd1055f1a19d37083f7656d531d4b26) ? contact.Normal : (-contact.Normal));
				flag = Vector3F.Dot(vector3F, Vector3F.UnitY) >= c761883ef938eed076b0f3f25560b2e73;
				Vector3F vector3F2 = vector3F;
				vector3F2.Y = 0f;
				if (!flag && vector3F2.TryNormalize())
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
					float num = 0f - Vector3F.Dot(moveVelocity, vector3F2);
					if (num > 0f)
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
						moveVelocity += num * vector3F2;
					}
				}
			}
		}
		if (HasGroundContact || (IsClimbing && (cbaeb4e660c0fe16cd631be6e28fff0d4 + ceed236e559deae7838ad345f8943bc70).Y <= 0f))
		{
			cbaeb4e660c0fe16cd631be6e28fff0d4 = Vector3F.Zero;
			ceed236e559deae7838ad345f8943bc70 = Vector3F.Zero;
		}
		else if (jumpVelocity > 0f)
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
			ceed236e559deae7838ad345f8943bc70 = Vector3F.Zero;
		}
		else
		{
			ceed236e559deae7838ad345f8943bc70 += -Vector3F.UnitY * Gravity * deltaTime;
		}
		cbaeb4e660c0fe16cd631be6e28fff0d4 = Vector3F.Max(jumpVelocity * Vector3F.UnitY, cbaeb4e660c0fe16cd631be6e28fff0d4);
		Body.LinearVelocity = moveVelocity + cbaeb4e660c0fe16cd631be6e28fff0d4 + ceed236e559deae7838ad345f8943bc70;
		if (cbaeb4e660c0fe16cd631be6e28fff0d4 == Vector3F.Zero)
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
			if (!IsClimbing)
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
				if (flag && Vector3F.Dot(Body.LinearVelocity, vector3F) > 0f)
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
					Vector3F vector = Vector3F.Cross(vector3F, Body.LinearVelocity);
					Vector3F vector3F3 = -Vector3F.Cross(vector3F, vector);
					vector3F3.Normalize();
					Body.LinearVelocity = vector3F3 * Body.LinearVelocity.Length;
				}
			}
		}
		caca9684c1d35ec909f8a977cf16d06c2 = null;
	}
}
