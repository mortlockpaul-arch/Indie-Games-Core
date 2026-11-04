using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Mathematics.Interpolation;
using DigitalRune.Physics.Materials;

namespace DigitalRune.Physics.Specialized;

/// <summary>
///       Controls the movement of a game character.
///       </summary>
public class KinematicCharacterController
{
	private struct c3041dfbb3cbd6699ef28ee61cec2b134
	{
		public Vector3F cf2e6e952fbc0f199d7bf1b90ba27a6c4;

		public Vector3F c92b2ed2c76c9d5b2e6f8466991b83c86;

		public float c23a1aa2e7d59028e538aacc27f1605ef;
	}

	private int cec5cdd76f94f5bb099186cb0e7a6aef2 = 4;

	private int c90de9a5dfb6ee39beb51b2bbbe0bc796 = 4;

	private static readonly float ca501053ecf509007f5df0ca8073ae7d7 = (float)Math.Cos(MathHelper.ToRadians(120f));

	private readonly List<ContactSet> c11920511034e84795e1ea305d0a19726 = new List<ContactSet>();

	private readonly List<c3041dfbb3cbd6699ef28ee61cec2b134> c9d16faceeb0058b07b0e5f0c16bb5d30 = new List<c3041dfbb3cbd6699ef28ee61cec2b134>();

	private readonly List<Plane> cc3ce9130a3423576fa83a39414b44722 = new List<Plane>();

	private bool? caca9684c1d35ec909f8a977cf16d06c2;

	private readonly List<Vector3F> cef9e6bdbfa3db21fb0691e37ea1cfe5a = new List<Vector3F>();

	private readonly List<c3041dfbb3cbd6699ef28ee61cec2b134> c638e590c47a58719f99786cab6f4b646 = new List<c3041dfbb3cbd6699ef28ee61cec2b134>();

	private bool? cd40ce33af1439483defa304c90b9f352;

	private readonly c6224484343dd5e4b503f24e8511a105a cdce9fc77873c3d41a418cf6852418351;

	private Vector3F ceed236e559deae7838ad345f8943bc70;

	private Vector3F cbaeb4e660c0fe16cd631be6e28fff0d4;

	private Vector3F c51057f5392c6248708f69f88665e9784;

	private Vector3F ccfe7ea1f3b3b9581790352c6b3e1663f;

	private bool cecf3d096b3e11a68c5fc94d070aa96f6;

	private bool cff254c44a108860eed374fc874e0e1e9;

	private bool c00ff7d5a5edc1c349e4745b7a5f3f005;

	internal Vector3F c0147c1ef6134168e9e91f8597953e3f3;

	private Vector3F cbcf0196a1372f758a7d565584664cf87;

	private float c4715d9a188bd9456099225c1f4ba9553 = 9.81f;

	private float c8e23b9e9bcef99c776b900fc3710e9a6 = 20f;

	private float cacdded03d72533fe6983abecc8161c65 = 10000f;

	private float c0b863e3cc499f0ed78d50bf700c73849 = (float)Math.PI / 4f;

	private float c761883ef938eed076b0f3f25560b2e73 = (float)Math.Cos(Math.PI / 4.0);

	private float c45d8daf3efdb56f9a67f294277aaf7f5 = 0.4f;

	private float cc5e2bed59c123ac81c5c0a7e67489d1f = 0.05f;

	[CompilerGenerated]
	private Simulation cbebb653f415fd36f75adc579c8bca56d;

	[CompilerGenerated]
	private bool c68860899517a94a312380103b1cf7142;

	[CompilerGenerated]
	private RigidBody c06605e21c26a6252efbeb1204abb6588;

	[CompilerGenerated]
	private Vector3F c054f975a2e61207f4b856b29e86132ed;

	/// <summary>
	///       Gets or sets the number of slide iterations.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public int NumberOfSlideIterations
	{
		get
		{
			return cec5cdd76f94f5bb099186cb0e7a6aef2;
		}
		set
		{
			if (value < 1)
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
							__ldtoken(KinematicCharacterController.set_NumberOfSlideIterations(int));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12));
					}
				}
			}
			cec5cdd76f94f5bb099186cb0e7a6aef2 = value;
		}
	}

	/// <summary>
	///       Gets or sets the number of solver iterations.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public int NumberOfSolverIterations
	{
		get
		{
			return c90de9a5dfb6ee39beb51b2bbbe0bc796;
		}
		set
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(121));
			}
			c90de9a5dfb6ee39beb51b2bbbe0bc796 = value;
		}
	}

	private float c2a646653dba15bfd9a3effefbb59f8e1 => Simulation.Settings.Constraints.AllowedPenetration;

	private CollisionDetection c301cfbfc773b3808c41a931567ba2b08 => Simulation.CollisionDomain.CollisionDetection;

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
					switch (7)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(KinematicCharacterController.get_HasGroundContact());
						}
						return caca9684c1d35ec909f8a977cf16d06c2.Value;
					}
				}
			}
			float num = (0f - Height) / 2f;
			float num2 = Width / 2f;
			float num3 = num + num2;
			float num4 = num2 - num2 * c761883ef938eed076b0f3f25560b2e73;
			float num5 = num + num4;
			cef9e6bdbfa3db21fb0691e37ea1cfe5a.Clear();
			int count = c9d16faceeb0058b07b0e5f0c16bb5d30.Count;
			for (int i = 0; i < count; i++)
			{
				c3041dfbb3cbd6699ef28ee61cec2b134 c3041dfbb3cbd6699ef28ee61cec2b135 = c9d16faceeb0058b07b0e5f0c16bb5d30[i];
				if (c3041dfbb3cbd6699ef28ee61cec2b135.cf2e6e952fbc0f199d7bf1b90ba27a6c4.Y <= num5)
				{
					caca9684c1d35ec909f8a977cf16d06c2 = true;
					return true;
				}
				if (c3041dfbb3cbd6699ef28ee61cec2b135.cf2e6e952fbc0f199d7bf1b90ba27a6c4.Y <= num3)
				{
					cef9e6bdbfa3db21fb0691e37ea1cfe5a.Add(c3041dfbb3cbd6699ef28ee61cec2b135.cf2e6e952fbc0f199d7bf1b90ba27a6c4);
				}
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				int count2 = cef9e6bdbfa3db21fb0691e37ea1cfe5a.Count;
				if (count2 < 2)
				{
					caca9684c1d35ec909f8a977cf16d06c2 = false;
					return false;
				}
				Vector3F vector3F = new Vector3F(0f, num3, 0f);
				for (int j = 0; j < count2; j++)
				{
					cef9e6bdbfa3db21fb0691e37ea1cfe5a[j] = (cef9e6bdbfa3db21fb0691e37ea1cfe5a[j] - vector3F).Normalized;
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					for (int k = 0; k < count2; k++)
					{
						Vector3F vector = cef9e6bdbfa3db21fb0691e37ea1cfe5a[k];
						for (int l = k + 1; l < count2; l++)
						{
							Vector3F vector2 = cef9e6bdbfa3db21fb0691e37ea1cfe5a[l];
							if (!(Vector3F.Dot(vector, vector2) <= ca501053ecf509007f5df0ca8073ae7d7))
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
								goto end_IL_01be;
							}
							continue;
							end_IL_01be:
							break;
						}
					}
					while (true)
					{
						switch (6)
						{
						case 0:
							continue;
						}
						caca9684c1d35ec909f8a977cf16d06c2 = false;
						return false;
					}
				}
			}
		}
	}

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
				return;
			}
			if (value)
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
							__ldtoken(KinematicCharacterController.set_Enabled(bool));
						}
						Simulation.RigidBodies.Add(Body);
						Simulation.ForceEffects.Add(cdce9fc77873c3d41a418cf6852418351);
						return;
					}
				}
			}
			Simulation.RigidBodies.Remove(Body);
			Simulation.ForceEffects.Remove(cdce9fc77873c3d41a418cf6852418351);
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
	///       Gets or sets the maximum velocity of the character.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float MaxVelocity
	{
		get
		{
			return c8e23b9e9bcef99c776b900fc3710e9a6;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(232));
			}
			c8e23b9e9bcef99c776b900fc3710e9a6 = value;
		}
	}

	/// <summary>
	///       Gets or sets the maximal push force with which the character pushes other objects.
	///       </summary>
	public float PushForce
	{
		get
		{
			return cacdded03d72533fe6983abecc8161c65;
		}
		set
		{
			cacdded03d72533fe6983abecc8161c65 = value;
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
	///       Gets or sets the height of the step.
	///       </summary>
	public float StepHeight
	{
		get
		{
			return c45d8daf3efdb56f9a67f294277aaf7f5;
		}
		set
		{
			c45d8daf3efdb56f9a67f294277aaf7f5 = value;
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
	///       Gets or sets the jump maneuverability.
	///       </summary>
	public float JumpManeuverability
	{
		get
		{
			return cc5e2bed59c123ac81c5c0a7e67489d1f;
		}
		set
		{
			cc5e2bed59c123ac81c5c0a7e67489d1f = value;
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
		}
	}

	/// <summary>
	///       Gets the vector that points into the "up" direction.
	///       </summary>
	public Vector3F UpVector
	{
		[CompilerGenerated]
		get
		{
			return c054f975a2e61207f4b856b29e86132ed;
		}
		[CompilerGenerated]
		private set
		{
			c054f975a2e61207f4b856b29e86132ed = value;
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
			return Body.Pose.Position - Height / 2f * UpVector;
		}
		set
		{
			Pose pose = Body.Pose;
			pose.Position = value + Height / 2f * UpVector;
			Body.Pose = pose;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Specialized.KinematicCharacterController" /> class.
	///       </summary>
	/// <param name="simulation">The simulation.</param>
	public KinematicCharacterController(Simulation simulation)
		: this(simulation, Vector3F.UnitY)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Specialized.KinematicCharacterController" /> class.
	///       </summary>
	/// <param name="simulation">The simulation.</param>
	/// <param name="upVector">The normalized up vector.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="simulation" /> is <see langword="null" />.
	///       </exception>
	public KinematicCharacterController(Simulation simulation, Vector3F upVector)
	{
		if (simulation == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(421));
		}
		Simulation = simulation;
		c5154614a3a67b403b9d195e92843c7ff(upVector);
		cdce9fc77873c3d41a418cf6852418351 = new c6224484343dd5e4b503f24e8511a105a(this);
		Enabled = true;
	}

	/// <summary>
	///       Tries to move the character to the nearest position where it does not penetrate other
	///       objects.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if the character is in or moved to a non-penetrating position;
	///       otherwise, <see langword="false" /> if the penetrations could not be resolved.
	///       </returns>
	public bool ResolvePenetrations()
	{
		if (!Enabled)
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
						__ldtoken(KinematicCharacterController.ResolvePenetrations());
					}
					return false;
				}
			}
		}
		Vector3F position = Position;
		c704f8765964babdc710e877639298dae(2f * Width);
		c4428c2d517a1c32aa278a32dae4fe2d3();
		bool flag = c40f89438c6f1094a25930d92b3bbdd2c(Vector3F.Zero);
		if (!flag)
		{
			return true;
		}
		cc3ce9130a3423576fa83a39414b44722.Clear();
		int num = 0;
		while (true)
		{
			num++;
			c3926c52129a5bdb2848c62fb44add613(Position);
			int num2 = 0;
			do
			{
				num2++;
				bool flag2 = false;
				int count = cc3ce9130a3423576fa83a39414b44722.Count;
				for (int i = 0; i < count; i++)
				{
					Plane plane = cc3ce9130a3423576fa83a39414b44722[i];
					float num3 = GeometryHelper.GetDistance(plane, Position) + c2a646653dba15bfd9a3effefbb59f8e1;
					if (Numeric.IsLess(num3, 0f))
					{
						Position += plane.Normal * (0f - num3);
						flag2 = true;
					}
				}
				if (!flag2)
				{
					break;
				}
				while (true)
				{
					switch (5)
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
			while (num2 < NumberOfSolverIterations);
			c4428c2d517a1c32aa278a32dae4fe2d3();
			if (num2 >= NumberOfSolverIterations)
			{
				break;
			}
			flag = c40f89438c6f1094a25930d92b3bbdd2c(Vector3F.Zero);
			if (!flag)
			{
				break;
			}
			if (num < NumberOfSlideIterations)
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
			break;
		}
		if (!Position.IsNaN)
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
			if (!flag)
			{
				return true;
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
		Position = position;
		c4428c2d517a1c32aa278a32dae4fe2d3();
		return false;
	}

	private void cab21e6fd78ac3a2c86cce6c66c65dbde()
	{
		Vector3F vector3F = ccfe7ea1f3b3b9581790352c6b3e1663f - c51057f5392c6248708f69f88665e9784;
		if (vector3F.IsNumericallyZero)
		{
			return;
		}
		cc3ce9130a3423576fa83a39414b44722.Clear();
		Vector3F position = Position;
		cb6d2effaf3b978bdeb5f8f29b54f728f();
		bool flag = true;
		int num = 0;
		while (true)
		{
			num++;
			c3926c52129a5bdb2848c62fb44add613(Position);
			Vector3F vector3F2 = vector3F;
			int num2 = 0;
			while (true)
			{
				num2++;
				bool flag2 = true;
				int count = cc3ce9130a3423576fa83a39414b44722.Count;
				for (int i = 0; i < count; i++)
				{
					Plane plane = cc3ce9130a3423576fa83a39414b44722[i];
					if (!Numeric.IsGreaterOrEqual(Vector3F.Dot(plane.Normal, vector3F2), 0f))
					{
						float num3 = GeometryHelper.GetDistance(plane, position + vector3F2) + c2a646653dba15bfd9a3effefbb59f8e1;
						if (Numeric.IsLess(num3, 0f))
						{
							Vector3F vector3F3 = plane.Normal * (0f - num3);
							vector3F2 += vector3F3;
							flag2 = false;
						}
					}
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
						__ldtoken(KinematicCharacterController.cab21e6fd78ac3a2c86cce6c66c65dbde());
					}
					if (!flag2)
					{
						if (num2 < NumberOfSolverIterations)
						{
							break;
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
					if (num2 < NumberOfSolverIterations)
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
						if (!Numeric.IsLessOrEqual(Vector3F.Dot(vector3F2, vector3F), 0f))
						{
							Position = position + vector3F2;
							c4428c2d517a1c32aa278a32dae4fe2d3();
							flag = c40f89438c6f1094a25930d92b3bbdd2c(vector3F2);
							if (flag)
							{
								while (true)
								{
									switch (5)
									{
									case 0:
										break;
									default:
										goto end_IL_017a;
									}
									continue;
									end_IL_017a:
									break;
								}
								if (num < NumberOfSlideIterations)
								{
									goto end_IL_0053;
								}
							}
						}
					}
					if (!Position.IsNaN)
					{
						if (!flag)
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
					Position = position;
					cffaf7c48f4eeb827a7c541d7ad77fe4c();
					return;
				}
				continue;
				end_IL_0053:
				break;
			}
		}
	}

	private bool c5d424cf8c6d1a3d37a851d5e45a96c60(bool c88c3b5f528224719eb7b303b2d454647)
	{
		Vector3F vector3F = ccfe7ea1f3b3b9581790352c6b3e1663f - Position;
		if (vector3F.IsNumericallyZero)
		{
			return true;
		}
		Vector3F normalized = vector3F.Normalized;
		Vector3F vector3F2 = normalized - Vector3F.Dot(normalized, UpVector) * UpVector;
		cc3ce9130a3423576fa83a39414b44722.Clear();
		Vector3F position = Position;
		cb6d2effaf3b978bdeb5f8f29b54f728f();
		bool flag = false;
		bool flag2 = false;
		bool hasGroundContact = HasGroundContact;
		int num = 0;
		bool flag3 = false;
		bool flag4 = false;
		while (true)
		{
			num++;
			c3926c52129a5bdb2848c62fb44add613(Position);
			Vector3F vector3F3 = vector3F;
			int num2 = 0;
			while (true)
			{
				num2++;
				bool flag5 = true;
				int count = cc3ce9130a3423576fa83a39414b44722.Count;
				for (int i = 0; i < count; i++)
				{
					Plane plane = cc3ce9130a3423576fa83a39414b44722[i];
					if (Numeric.IsGreaterOrEqual(Vector3F.Dot(plane.Normal, vector3F3), 0f))
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
					if (1 == 0)
					{
						__ldtoken(KinematicCharacterController.c5d424cf8c6d1a3d37a851d5e45a96c60(bool));
					}
					float num3 = GeometryHelper.GetDistance(plane, position + vector3F3) + c2a646653dba15bfd9a3effefbb59f8e1;
					if (!Numeric.IsLess(num3, 0f))
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
					Vector3F vector3F4 = plane.Normal * (0f - num3);
					if (!cef056a544cf5e5c672c1b1a6e94eb1c8(plane.Normal))
					{
						if (c88c3b5f528224719eb7b303b2d454647)
						{
							vector3F4 = (0f - num3) / Vector3F.Dot(normalized, plane.Normal) * normalized;
							flag = true;
						}
						else if (flag3)
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
							vector3F4 = (0f - num3) / Vector3F.Dot(vector3F2, plane.Normal) * vector3F2;
						}
						else
						{
							if (!flag4)
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
								if (!(Vector3F.Dot(vector3F4, UpVector) > 0f))
								{
									goto IL_0217;
								}
							}
							Vector3F vector3F5 = plane.Normal - Vector3F.Dot(plane.Normal, UpVector) * UpVector;
							if (vector3F5.TryNormalize())
							{
								vector3F4 = (0f - num3) / Vector3F.Dot(vector3F5, plane.Normal) * vector3F5;
							}
						}
					}
					goto IL_0217;
					IL_0217:
					vector3F3 += vector3F4;
					if (vector3F3.Length > vector3F.Length)
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
						vector3F3.Length = vector3F.Length;
					}
					flag5 = false;
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					if (!flag5 && num2 < NumberOfSolverIterations)
					{
						break;
					}
					if (num2 >= NumberOfSolverIterations)
					{
						if (flag4 || vector3F2.IsNumericallyZero)
						{
							if (!flag3)
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
								if (!vector3F2.IsNumericallyZero)
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
									flag3 = true;
									goto IL_030b;
								}
							}
							if (Numeric.IsLess(Vector3F.Dot(normalized, UpVector), 0f))
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
								if (vector3F2.IsNumericallyZero)
								{
									flag2 = true;
									caca9684c1d35ec909f8a977cf16d06c2 = true;
								}
							}
							goto IL_03c6;
						}
						flag4 = true;
					}
					goto IL_030b;
					IL_03c6:
					if (!Position.IsNaN)
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
						if (flag2)
						{
							goto IL_03fe;
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
					Position = position;
					cffaf7c48f4eeb827a7c541d7ad77fe4c();
					goto IL_03fe;
					IL_03fe:
					return !flag;
					IL_030b:
					Position = position + vector3F3;
					c4428c2d517a1c32aa278a32dae4fe2d3();
					if (hasGroundContact)
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
						if (Vector3F.Dot(vector3F3, vector3F2) < 0f - c2a646653dba15bfd9a3effefbb59f8e1)
						{
							flag = true;
							goto IL_03c6;
						}
					}
					flag2 = !c40f89438c6f1094a25930d92b3bbdd2c(vector3F3);
					if (flag2)
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
						if (hasGroundContact && Numeric.IsZero(Vector3F.Dot(vector3F.Orthonormal1, UpVector)) && Vector3F.Dot(vector3F3, UpVector) <= 0f)
						{
							flag2 = false;
							goto IL_03c6;
						}
					}
					if (!flag2)
					{
						while (true)
						{
							switch (7)
							{
							case 0:
								break;
							default:
								goto end_IL_03a5;
							}
							continue;
							end_IL_03a5:
							break;
						}
						if (num < NumberOfSlideIterations)
						{
							goto end_IL_0091;
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
					goto IL_03c6;
				}
				continue;
				end_IL_0091:
				break;
			}
		}
	}

	private bool c804c2a089309e5e73a68cdc0febc7c92()
	{
		Vector3F position = Position;
		Vector3F vector3F = ccfe7ea1f3b3b9581790352c6b3e1663f - position;
		Vector3F vector3F2 = vector3F - Vector3F.ProjectTo(vector3F, UpVector);
		if (!vector3F2.TryNormalize())
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
						__ldtoken(KinematicCharacterController.c804c2a089309e5e73a68cdc0febc7c92());
					}
					return false;
				}
			}
		}
		cb6d2effaf3b978bdeb5f8f29b54f728f();
		Position = Position + UpVector * StepHeight + vector3F2 * (Width / 2f - c2a646653dba15bfd9a3effefbb59f8e1);
		c4428c2d517a1c32aa278a32dae4fe2d3();
		if (!c40f89438c6f1094a25930d92b3bbdd2c(Vector3F.Zero) && cf9cc4a0aa878319271315fc87fc89ae0(c5fea40527337561565334510cdf5fcb6: true))
		{
			return true;
		}
		Position = position;
		cffaf7c48f4eeb827a7c541d7ad77fe4c();
		return false;
	}

	private bool cf9cc4a0aa878319271315fc87fc89ae0(bool c5fea40527337561565334510cdf5fcb6)
	{
		if (HasGroundContact)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(KinematicCharacterController.cf9cc4a0aa878319271315fc87fc89ae0(bool));
				}
				return true;
			}
		}
		if (Numeric.IsZero(StepHeight))
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				return true;
			}
		}
		Vector3F vector3F = -UpVector * StepHeight;
		bool flag = false;
		Vector3F position = Position;
		Vector3F vector3F2 = default;
		Vector3F vector3F3 = vector3F;
		cc3ce9130a3423576fa83a39414b44722.Clear();
		cb6d2effaf3b978bdeb5f8f29b54f728f();
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		int num = 0;
		do
		{
			num++;
			c3926c52129a5bdb2848c62fb44add613(Position);
			int num2 = 0;
			flag3 = false;
			bool flag5;
			do
			{
				num2++;
				flag5 = true;
				int count = cc3ce9130a3423576fa83a39414b44722.Count;
				for (int i = 0; i < count; i++)
				{
					Plane plane = cc3ce9130a3423576fa83a39414b44722[i];
					if (Numeric.IsGreaterOrEqual(Vector3F.Dot(plane.Normal, -UpVector), 0f, 0.001f))
					{
						continue;
					}
					float num3 = GeometryHelper.GetDistance(plane, position + vector3F3) + c2a646653dba15bfd9a3effefbb59f8e1;
					if (num3 < c2a646653dba15bfd9a3effefbb59f8e1)
					{
						flag3 = true;
						flag4 = flag4 || cef056a544cf5e5c672c1b1a6e94eb1c8(plane.Normal);
					}
					if (!Numeric.IsLess(num3, 0f))
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
					Vector3F vector3F4 = (0f - num3) / Vector3F.Dot(UpVector, plane.Normal) * UpVector;
					vector3F3 += vector3F4;
					flag5 = false;
				}
				if (num2 >= NumberOfSolverIterations)
				{
					break;
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
						goto end_IL_01ae;
					}
					continue;
					end_IL_01ae:
					break;
				}
			}
			while (!flag5);
			bool flag6 = Numeric.IsGreater(Vector3F.Dot(vector3F3, -UpVector), Vector3F.Dot(vector3F2, -UpVector));
			if (num == 1)
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
				if (!flag5 || !flag6)
				{
					break;
				}
			}
			bool flag7 = Vector3F.AreNumericallyEqual(position + vector3F3, Position);
			if (num > 1 && flag7)
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
				if (!flag)
				{
					break;
				}
				cc3ce9130a3423576fa83a39414b44722.Clear();
				vector3F3 = (vector3F2 + vector3F) / 2f;
			}
			if (flag5)
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
				if (flag6)
				{
					goto IL_0295;
				}
			}
			cc3ce9130a3423576fa83a39414b44722.Clear();
			flag3 = false;
			vector3F3 = (vector3F2 + vector3F) / 2f;
			goto IL_0295;
			IL_0295:
			Position = position + vector3F3;
			c4428c2d517a1c32aa278a32dae4fe2d3();
			flag2 = c40f89438c6f1094a25930d92b3bbdd2c(Vector3F.Zero);
			flag = flag || flag2;
			if (flag2)
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
				vector3F = vector3F3;
			}
			else
			{
				vector3F2 = vector3F3;
			}
			if (flag2)
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
			if (!flag3)
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
			if (!flag)
			{
				break;
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
			if (c9d16faceeb0058b07b0e5f0c16bb5d30.Count != 0)
			{
				break;
			}
		}
		while (num < NumberOfSlideIterations);
		if (!Position.IsNaN)
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
			if (!flag2)
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
				if (flag3)
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
					if (c5fea40527337561565334510cdf5fcb6)
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
						if (!flag4)
						{
							goto IL_036a;
						}
					}
					return true;
				}
			}
		}
		goto IL_036a;
		IL_036a:
		Position = position;
		cffaf7c48f4eeb827a7c541d7ad77fe4c();
		return false;
	}

	private void cb6d2effaf3b978bdeb5f8f29b54f728f()
	{
		c638e590c47a58719f99786cab6f4b646.Clear();
		using (List<c3041dfbb3cbd6699ef28ee61cec2b134>.Enumerator enumerator = c9d16faceeb0058b07b0e5f0c16bb5d30.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				c3041dfbb3cbd6699ef28ee61cec2b134 current = enumerator.Current;
				c638e590c47a58719f99786cab6f4b646.Add(current);
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
					__ldtoken(KinematicCharacterController.cb6d2effaf3b978bdeb5f8f29b54f728f());
				}
				break;
			}
		}
		cd40ce33af1439483defa304c90b9f352 = caca9684c1d35ec909f8a977cf16d06c2;
	}

	private void cffaf7c48f4eeb827a7c541d7ad77fe4c()
	{
		c9d16faceeb0058b07b0e5f0c16bb5d30.Clear();
		foreach (c3041dfbb3cbd6699ef28ee61cec2b134 item in c638e590c47a58719f99786cab6f4b646)
		{
			c9d16faceeb0058b07b0e5f0c16bb5d30.Add(item);
		}
		caca9684c1d35ec909f8a977cf16d06c2 = cd40ce33af1439483defa304c90b9f352;
	}

	private void c704f8765964babdc710e877639298dae(float c62a8ac8a1ea6107858bdc026334c8108)
	{
		foreach (ContactSet item2 in c11920511034e84795e1ea305d0a19726)
		{
			foreach (Contact item3 in item2)
			{
				item3.Recycle();
			}
			item2.Recycle();
		}
		c11920511034e84795e1ea305d0a19726.Clear();
		if (!Body.CollisionObject.Enabled)
		{
			return;
		}
		Aabb aabb = Body.Aabb;
		aabb.Minimum -= new Vector3F(c62a8ac8a1ea6107858bdc026334c8108);
		aabb.Maximum += new Vector3F(c62a8ac8a1ea6107858bdc026334c8108);
		IEnumerable<CollisionObject> overlaps = Simulation.CollisionDomain.BroadPhase.GetOverlaps(aabb);
		IEnumerator<CollisionObject> enumerator3 = overlaps.GetEnumerator();
		try
		{
			while (enumerator3.MoveNext())
			{
				CollisionObject current3 = enumerator3.Current;
				if (current3 == Body.CollisionObject)
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
				if (1 == 0)
				{
					__ldtoken(KinematicCharacterController.c704f8765964babdc710e877639298dae(float));
				}
				ContactSet item = ContactSet.Create(Body.CollisionObject, current3);
				c11920511034e84795e1ea305d0a19726.Add(item);
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
		finally
		{
			if (enumerator3 != null)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					enumerator3.Dispose();
					break;
				}
			}
		}
	}

	private void c4428c2d517a1c32aa278a32dae4fe2d3()
	{
		c9d16faceeb0058b07b0e5f0c16bb5d30.Clear();
		caca9684c1d35ec909f8a977cf16d06c2 = null;
		int count = c11920511034e84795e1ea305d0a19726.Count;
		for (int i = 0; i < count; i++)
		{
			ContactSet contactSet = c11920511034e84795e1ea305d0a19726[i];
			c301cfbfc773b3808c41a931567ba2b08.UpdateContacts(contactSet, 0f);
			int count2 = contactSet.Count;
			for (int j = 0; j < count2; j++)
			{
				Contact contact = contactSet[j];
				c9d16faceeb0058b07b0e5f0c16bb5d30.Add(new c3041dfbb3cbd6699ef28ee61cec2b134
				{
					cf2e6e952fbc0f199d7bf1b90ba27a6c4 = contact.PositionALocal,
					c92b2ed2c76c9d5b2e6f8466991b83c86 = -contact.Normal,
					c23a1aa2e7d59028e538aacc27f1605ef = contact.PenetrationDepth
				});
			}
		}
	}

	private int c3926c52129a5bdb2848c62fb44add613(Vector3F c2fed316f5bcf4dd4ea7883930f3822f9)
	{
		int count = cc3ce9130a3423576fa83a39414b44722.Count;
		int count2 = c9d16faceeb0058b07b0e5f0c16bb5d30.Count;
		int num = 0;
		while (num < count2)
		{
			c3041dfbb3cbd6699ef28ee61cec2b134 c3041dfbb3cbd6699ef28ee61cec2b135 = c9d16faceeb0058b07b0e5f0c16bb5d30[num];
			Vector3F c92b2ed2c76c9d5b2e6f8466991b83c = c3041dfbb3cbd6699ef28ee61cec2b135.c92b2ed2c76c9d5b2e6f8466991b83c86;
			float c23a1aa2e7d59028e538aacc27f1605ef = c3041dfbb3cbd6699ef28ee61cec2b135.c23a1aa2e7d59028e538aacc27f1605ef;
			Plane item = new Plane(c92b2ed2c76c9d5b2e6f8466991b83c, c2fed316f5bcf4dd4ea7883930f3822f9 + c92b2ed2c76c9d5b2e6f8466991b83c * c23a1aa2e7d59028e538aacc27f1605ef);
			bool flag = true;
			int count3 = cc3ce9130a3423576fa83a39414b44722.Count;
			for (int i = 0; i < count3; i++)
			{
				Plane plane = cc3ce9130a3423576fa83a39414b44722[i];
				if (!Vector3F.AreNumericallyEqual(plane.Normal, item.Normal, c301cfbfc773b3808c41a931567ba2b08.Epsilon))
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
					__ldtoken(KinematicCharacterController.c3926c52129a5bdb2848c62fb44add613(Vector3F));
				}
				if (!Numeric.AreEqual(plane.DistanceFromOrigin, item.DistanceFromOrigin, c301cfbfc773b3808c41a931567ba2b08.Epsilon))
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
				flag = false;
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				if (flag)
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
					if (cef056a544cf5e5c672c1b1a6e94eb1c8(item.Normal))
					{
						cc3ce9130a3423576fa83a39414b44722.Add(item);
					}
					else
					{
						cc3ce9130a3423576fa83a39414b44722.Insert(0, item);
					}
				}
				num++;
				break;
			}
		}
		return cc3ce9130a3423576fa83a39414b44722.Count - count;
	}

	private bool c40f89438c6f1094a25930d92b3bbdd2c(Vector3F c11d9d8c7deef96000c2262b051bdf165)
	{
		bool flag = c11d9d8c7deef96000c2262b051bdf165 == Vector3F.Zero;
		float num = c2a646653dba15bfd9a3effefbb59f8e1 + c301cfbfc773b3808c41a931567ba2b08.Epsilon;
		int count = c9d16faceeb0058b07b0e5f0c16bb5d30.Count;
		for (int i = 0; i < count; i++)
		{
			c3041dfbb3cbd6699ef28ee61cec2b134 c3041dfbb3cbd6699ef28ee61cec2b135 = c9d16faceeb0058b07b0e5f0c16bb5d30[i];
			if ((flag || Numeric.IsLess(Vector3F.Dot(c3041dfbb3cbd6699ef28ee61cec2b135.c92b2ed2c76c9d5b2e6f8466991b83c86, c11d9d8c7deef96000c2262b051bdf165), 0f)) && c3041dfbb3cbd6699ef28ee61cec2b135.c23a1aa2e7d59028e538aacc27f1605ef > num)
			{
				return true;
			}
		}
		return false;
	}

	internal bool cef056a544cf5e5c672c1b1a6e94eb1c8(Vector3F c79a3b94c816fc4e87d6348c3713a1943)
	{
		return Vector3F.Dot(c79a3b94c816fc4e87d6348c3713a1943, UpVector) >= c761883ef938eed076b0f3f25560b2e73;
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
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(KinematicCharacterController.Move(Vector3F, float, float));
				}
				return;
			}
		}
		Body.LinearVelocity = Vector3F.Zero;
		c51057f5392c6248708f69f88665e9784 = Position;
		Vector3F vector3F = moveVelocity * deltaTime;
		if (Gravity == 0f)
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
			ceed236e559deae7838ad345f8943bc70 = Vector3F.Zero;
			cbaeb4e660c0fe16cd631be6e28fff0d4 = Vector3F.Zero;
			vector3F += jumpVelocity * UpVector * deltaTime;
			float num = vector3F.Length;
			float num2 = MaxVelocity * deltaTime;
			if (num > num2)
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
				vector3F.Length = num2;
				num = num2;
			}
			c704f8765964babdc710e877639298dae(num);
			ccfe7ea1f3b3b9581790352c6b3e1663f = c51057f5392c6248708f69f88665e9784 + vector3F;
			cab21e6fd78ac3a2c86cce6c66c65dbde();
			goto IL_0449;
		}
		if (!cecf3d096b3e11a68c5fc94d070aa96f6)
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
			if (IsClimbing)
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
				if ((cbaeb4e660c0fe16cd631be6e28fff0d4 + ceed236e559deae7838ad345f8943bc70).Y <= 0f)
				{
					goto IL_012b;
				}
			}
			if (!cff254c44a108860eed374fc874e0e1e9)
			{
				Vector3F vector3F2 = cbcf0196a1372f758a7d565584664cf87 * deltaTime;
				vector3F2 -= Vector3F.ProjectTo(vector3F2, UpVector);
				vector3F -= Vector3F.ProjectTo(vector3F, UpVector);
				vector3F = InterpolationHelper.Lerp(vector3F2, vector3F, JumpManeuverability);
				if (jumpVelocity > 0f)
				{
					ceed236e559deae7838ad345f8943bc70 = Vector3F.Zero;
				}
				else
				{
					ceed236e559deae7838ad345f8943bc70 += -UpVector * Gravity * deltaTime;
					if (ceed236e559deae7838ad345f8943bc70.LengthSquared > MaxVelocity * MaxVelocity)
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
						ceed236e559deae7838ad345f8943bc70.Length = MaxVelocity;
					}
				}
				goto IL_0212;
			}
		}
		goto IL_012b;
		IL_037b:
		float num3;
		float num4;
		if (!c00ff7d5a5edc1c349e4745b7a5f3f005)
		{
			num3 = num4;
		}
		Vector3F vector3F3 = Position - c51057f5392c6248708f69f88665e9784;
		float length = vector3F3.Length;
		if (length > num3 && !Numeric.IsZero(length))
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
			Position = c51057f5392c6248708f69f88665e9784 + vector3F3 / length * num3;
			vector3F3 = Position - c51057f5392c6248708f69f88665e9784;
			c4428c2d517a1c32aa278a32dae4fe2d3();
		}
		cecf3d096b3e11a68c5fc94d070aa96f6 = HasGroundContact;
		if (cbaeb4e660c0fe16cd631be6e28fff0d4 == Vector3F.Zero && Vector3F.Dot(vector3F3, UpVector) > 0f)
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
			ceed236e559deae7838ad345f8943bc70 = Vector3F.Zero;
		}
		goto IL_0449;
		IL_0374:
		c00ff7d5a5edc1c349e4745b7a5f3f005 = false;
		goto IL_037b;
		IL_0212:
		cbaeb4e660c0fe16cd631be6e28fff0d4 = Vector3F.Max(jumpVelocity * UpVector, cbaeb4e660c0fe16cd631be6e28fff0d4);
		vector3F += cbaeb4e660c0fe16cd631be6e28fff0d4 * deltaTime;
		vector3F += ceed236e559deae7838ad345f8943bc70 * deltaTime;
		num4 = vector3F.Length;
		num3 = MaxVelocity * deltaTime;
		if (num4 > num3)
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
			vector3F.Length = num3;
			num4 = num3;
		}
		c704f8765964babdc710e877639298dae(Math.Max(num4, StepHeight));
		c4428c2d517a1c32aa278a32dae4fe2d3();
		ccfe7ea1f3b3b9581790352c6b3e1663f = c51057f5392c6248708f69f88665e9784 + vector3F;
		bool c88c3b5f528224719eb7b303b2d = cecf3d096b3e11a68c5fc94d070aa96f6 || cff254c44a108860eed374fc874e0e1e9;
		cff254c44a108860eed374fc874e0e1e9 = false;
		if (!c5d424cf8c6d1a3d37a851d5e45a96c60(c88c3b5f528224719eb7b303b2d))
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
			cff254c44a108860eed374fc874e0e1e9 = c804c2a089309e5e73a68cdc0febc7c92();
			if (!cff254c44a108860eed374fc874e0e1e9)
			{
				c5d424cf8c6d1a3d37a851d5e45a96c60(c88c3b5f528224719eb7b303b2d454647: false);
			}
		}
		if (!cff254c44a108860eed374fc874e0e1e9)
		{
			if (!cecf3d096b3e11a68c5fc94d070aa96f6)
			{
				if (!c00ff7d5a5edc1c349e4745b7a5f3f005)
				{
					goto IL_0374;
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
			if (cbaeb4e660c0fe16cd631be6e28fff0d4 == Vector3F.Zero)
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
				if (!IsClimbing)
				{
					c00ff7d5a5edc1c349e4745b7a5f3f005 = cf9cc4a0aa878319271315fc87fc89ae0(!c00ff7d5a5edc1c349e4745b7a5f3f005);
					goto IL_037b;
				}
			}
		}
		goto IL_0374;
		IL_012b:
		cbaeb4e660c0fe16cd631be6e28fff0d4 = Vector3F.Zero;
		ceed236e559deae7838ad345f8943bc70 = Vector3F.Zero;
		goto IL_0212;
		IL_0449:
		c0147c1ef6134168e9e91f8597953e3f3 = vector3F / deltaTime;
		cbcf0196a1372f758a7d565584664cf87 = (Position - c51057f5392c6248708f69f88665e9784) / deltaTime;
		Body.LinearVelocity = Vector3F.Zero;
	}

	private void c5154614a3a67b403b9d195e92843c7ff(Vector3F c24715abd743177129d29b2151c9453fc)
	{
		if (!c24715abd743177129d29b2151c9453fc.TryNormalize())
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(301));
		}
		UpVector = c24715abd743177129d29b2151c9453fc;
		CapsuleShape capsuleShape = new CapsuleShape(0.4f, 1.8f);
		MassFrame massFrame = new MassFrame();
		massFrame.Mass = 100f;
		MassFrame massFrame2 = massFrame;
		UniformMaterial uniformMaterial = new UniformMaterial();
		uniformMaterial.StaticFriction = 0f;
		uniformMaterial.DynamicFriction = 0f;
		uniformMaterial.Restitution = 0f;
		UniformMaterial material = uniformMaterial;
		Body = new RigidBody(capsuleShape, massFrame2, material)
		{
			AutoUpdateMass = false,
			CanSleep = false,
			CcdEnabled = true,
			LockRotationX = true,
			LockRotationY = true,
			LockRotationZ = true,
			Name = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(382),
			Pose = new Pose(capsuleShape.Height / 2f * c24715abd743177129d29b2151c9453fc, QuaternionF.CreateRotation(Vector3F.UnitY, c24715abd743177129d29b2151c9453fc))
		};
		Body.ShapeChanged += (object cf3e112fd3d3aa49f1b42cc826ed0e916, ShapeChangedEventArgs c4496d1e28885071f753d5d8c6ed007b6) =>
		{
			c4428c2d517a1c32aa278a32dae4fe2d3();
		};
	}

	[CompilerGenerated]
	private void c1b9988f445483f6a109e312f39add812(object cf3e112fd3d3aa49f1b42cc826ed0e916, ShapeChangedEventArgs c4496d1e28885071f753d5d8c6ed007b6)
	{
		c4428c2d517a1c32aa278a32dae4fe2d3();
	}
}
