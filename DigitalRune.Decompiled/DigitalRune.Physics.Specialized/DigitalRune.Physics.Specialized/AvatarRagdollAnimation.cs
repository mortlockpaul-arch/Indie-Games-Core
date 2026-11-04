using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Physics.Constraints;
using DigitalRune.Physics.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace DigitalRune.Physics.Specialized;

/// <summary>
///       Animates an avatar using ragdoll physics. (Only available on Xbox 360.)
///       </summary>
public class AvatarRagdollAnimation : IAvatarAnimation
{
	private readonly Simulation c729503850b1dde68cce1c94f7a27d104;

	private readonly AvatarRenderer c228112107f106a5ec474a36e940e90b5;

	private bool c955f5545a3cf1c9df10247b21a325310;

	private bool c792db5efbaf1a307f0518fc5478235ef;

	private bool c156d4efdea6687e58ea3af5ac2653429;

	private bool ca05864f40d68d0a518c8cf96ecefacd9;

	private bool c02afc629ff9c33cc78da89c7f98f46a6;

	private float c8a035e79b582cd5fd7cd2b1a6f489725;

	private List<Pose> ccbe287c53bf4266bdffce5ba1332d614;

	private List<RigidBody> ca97b0d2056b6bcdf7091a0c33a7b2d74;

	private List<Pose> cd6c1c4383c8c767867b7010a2d803405;

	private List<BallJoint> c14e53428041de292581f200d8aaf4c1e;

	private List<Constraint> c4db155f25335b2ae95eeb3353c9dc7c8;

	private List<QuaternionMotor> c476f9d2a53540e1d4de17504927f6901;

	private float c6e73e2b321632ffda3c817af4598a6af = 10000000f;

	private float c789a67a7250bbf53bdfef25b80f0ed2b = 1000000f;

	private Pose cd337f7a206e7977cce3e1030296b1b57 = Pose.Identity;

	private readonly List<Matrix> cac7c4f631e0929b81ab7031d896a2f26;

	private readonly List<Matrix> c06a1fb656358c46c1f82e6f55636ea4e;

	private List<Pose> cb653bf43e3eec1018e0c10e91c0cd591;

	private readonly ReadOnlyCollection<Matrix> c7b8d9e2c93e11d5844c0ec8eeddf6616;

	private ReadOnlyCollection<RigidBody> c0844a56cf0491e8cf82a124f913a0564;

	private List<RigidBody> c4919f248e728afb8b4e1265ed264e932;

	private bool c5330841072d34369931c3c693aa5d5bc = true;

	private bool c3fa087251bf85c20a38db245aa73001c;

	[CompilerGenerated]
	private AvatarExpression c601a1e1ab42f59aa0238c3c43b348d86;

	/// <summary>
	///       Gets the current bone transformation matrices.
	///       </summary>
	public ReadOnlyCollection<Matrix> BoneTransforms => c7b8d9e2c93e11d5844c0ec8eeddf6616;

	private TimeSpan c2dbd8dd537dd9e2b3ff8df7040332132
	{
		get
		{
			return TimeSpan.Zero;
		}
		set
		{
		}
	}

	/// <summary>
	///       Gets or sets the avatar expression.
	///       </summary>
	public AvatarExpression Expression
	{
		[CompilerGenerated]
		get
		{
			return c601a1e1ab42f59aa0238c3c43b348d86;
		}
		[CompilerGenerated]
		set
		{
			c601a1e1ab42f59aa0238c3c43b348d86 = value;
		}
	}

	private TimeSpan cd5dc9498657787521e84d876185e6800 => TimeSpan.MaxValue;

	/// <summary>
	///       Gets the rigid bodies of the avatar ragdoll.
	///       </summary>
	public ReadOnlyCollection<RigidBody> Bodies => c0844a56cf0491e8cf82a124f913a0564;

	/// <summary>
	///       Gets or sets a value indicating whether joint limits have been enabled.
	///       </summary>
	public bool LimitsEnabled
	{
		get
		{
			return c5330841072d34369931c3c693aa5d5bc;
		}
		set
		{
			if (c5330841072d34369931c3c693aa5d5bc == value)
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
							__ldtoken(AvatarRagdollAnimation.set_LimitsEnabled(bool));
						}
						return;
					}
				}
			}
			c5330841072d34369931c3c693aa5d5bc = value;
			if (!c792db5efbaf1a307f0518fc5478235ef)
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
				if (c5330841072d34369931c3c693aa5d5bc)
				{
					while (true)
					{
						switch (4)
						{
						case 0:
							break;
						default:
						{
							foreach (Constraint item in c4db155f25335b2ae95eeb3353c9dc7c8)
							{
								c729503850b1dde68cce1c94f7a27d104.Constraints.Add(item);
							}
							return;
						}
						}
					}
				}
				if (c5330841072d34369931c3c693aa5d5bc)
				{
					return;
				}
				using List<Constraint>.Enumerator enumerator2 = c4db155f25335b2ae95eeb3353c9dc7c8.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Constraint current2 = enumerator2.Current;
					c729503850b1dde68cce1c94f7a27d104.Constraints.Remove(current2);
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
	}

	/// <summary>
	///       Gets or sets a value indicating whether joint motors have been enabled.
	///       </summary>
	public bool MotorsEnabled
	{
		get
		{
			return c3fa087251bf85c20a38db245aa73001c;
		}
		set
		{
			if (c3fa087251bf85c20a38db245aa73001c == value)
			{
				return;
			}
			c3fa087251bf85c20a38db245aa73001c = value;
			if (!c792db5efbaf1a307f0518fc5478235ef)
			{
				return;
			}
			if (c3fa087251bf85c20a38db245aa73001c)
			{
				using (List<QuaternionMotor>.Enumerator enumerator = c476f9d2a53540e1d4de17504927f6901.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QuaternionMotor current = enumerator.Current;
						if (current != null)
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
								__ldtoken(AvatarRagdollAnimation.set_MotorsEnabled(bool));
							}
							c729503850b1dde68cce1c94f7a27d104.Constraints.Add(current);
						}
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
			}
			if (c3fa087251bf85c20a38db245aa73001c)
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
				foreach (QuaternionMotor item in c476f9d2a53540e1d4de17504927f6901)
				{
					if (item == null)
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
					c729503850b1dde68cce1c94f7a27d104.Constraints.Remove(item);
				}
				return;
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Specialized.AvatarRagdollAnimation" /> class.
	///       </summary>
	/// <param name="renderer">The renderer.</param>
	/// <param name="simulation">The simulation.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="renderer" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="simulation" /> is <see langword="null" />.
	///       </exception>
	public AvatarRagdollAnimation(AvatarRenderer renderer, Simulation simulation)
	{
		if (renderer == null)
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
						__ldtoken(AvatarRagdollAnimation._002Ector(AvatarRenderer, Simulation));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(570));
				}
			}
		}
		if (simulation == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(421));
				}
			}
		}
		c228112107f106a5ec474a36e940e90b5 = renderer;
		c729503850b1dde68cce1c94f7a27d104 = simulation;
		cac7c4f631e0929b81ab7031d896a2f26 = new List<Matrix>(71);
		c7b8d9e2c93e11d5844c0ec8eeddf6616 = new ReadOnlyCollection<Matrix>(cac7c4f631e0929b81ab7031d896a2f26);
		c06a1fb656358c46c1f82e6f55636ea4e = new List<Matrix>(71);
		for (int i = 0; i < 71; i++)
		{
			cac7c4f631e0929b81ab7031d896a2f26.Add(Matrix.Identity);
			c06a1fb656358c46c1f82e6f55636ea4e.Add(Matrix.Identity);
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
	///       Starts the ragdoll animation.
	///       </summary>
	public void Start()
	{
		c955f5545a3cf1c9df10247b21a325310 = true;
	}

	/// <summary>
	///       Stops the ragdoll animation.
	///       </summary>
	public void Stop()
	{
		c955f5545a3cf1c9df10247b21a325310 = false;
		c84c8eb6894fdb45d3105d63d8a7c28fd();
	}

	/// <summary>
	///       Sets the bone transforms.
	///       </summary>
	/// <param name="pose">The position and orientation of the whole avatar.</param>
	/// <param name="boneTransforms">The bone transforms.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="boneTransforms" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="boneTransforms" /> list must have one entry per bone.
	///       </exception>
	public void SetBoneTransforms(Pose pose, IList<Matrix> boneTransforms)
	{
		if (boneTransforms == null)
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
						__ldtoken(AvatarRagdollAnimation.SetBoneTransforms(Pose, IList<Matrix>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(442));
				}
			}
		}
		if (boneTransforms.Count < 71)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(471), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(442));
		}
		c156d4efdea6687e58ea3af5ac2653429 = true;
		cd337f7a206e7977cce3e1030296b1b57 = pose;
		for (int i = 0; i < 71; i++)
		{
			cac7c4f631e0929b81ab7031d896a2f26[i] = boneTransforms[i];
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
	///       Sets the bone transforms that define the joint motor targets.
	///       </summary>
	/// <param name="boneTransforms">The bone transforms.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="boneTransforms" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="boneTransforms" /> list must have one entry per bone.
	///       </exception>
	public void SetMotorTargets(IList<Matrix> boneTransforms)
	{
		if (boneTransforms == null)
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
						__ldtoken(AvatarRagdollAnimation.SetMotorTargets(IList<Matrix>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(442));
				}
			}
		}
		if (boneTransforms.Count < 71)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(471), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(442));
		}
		ca05864f40d68d0a518c8cf96ecefacd9 = true;
		for (int i = 0; i < 71; i++)
		{
			c06a1fb656358c46c1f82e6f55636ea4e[i] = boneTransforms[i];
		}
	}

	/// <summary>
	///       Sets the joint motor properties.
	///       </summary>
	/// <param name="springConstant">The spring constant.</param>
	/// <param name="dampingConstant">The damping constant.</param>
	public void SetMotorProperties(float springConstant, float dampingConstant)
	{
		c6e73e2b321632ffda3c817af4598a6af = springConstant;
		c789a67a7250bbf53bdfef25b80f0ed2b = dampingConstant;
		c02afc629ff9c33cc78da89c7f98f46a6 = true;
	}

	/// <summary>
	///       Updates the avatar animation (must be called in each frame).
	///       </summary>
	/// <param name="notUsed0">Not used.</param>
	/// <param name="notUsed1">Not used.</param>
	public void Update(TimeSpan notUsed0, bool notUsed1)
	{
		if (!c955f5545a3cf1c9df10247b21a325310)
		{
			return;
		}
		if (c228112107f106a5ec474a36e940e90b5.State != AvatarRendererState.Ready)
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
						__ldtoken(AvatarRagdollAnimation.Update(TimeSpan, bool));
					}
					return;
				}
			}
		}
		if (ca97b0d2056b6bcdf7091a0c33a7b2d74 == null)
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
			c5f12f281f85fe1aa9ff2a63186c84393();
		}
		if (!c792db5efbaf1a307f0518fc5478235ef)
		{
			c06c72b133bed662e2f9a87ae052639e3();
		}
		if (c156d4efdea6687e58ea3af5ac2653429)
		{
			c35721cd3744fb3e9141424b32ebf0813();
			c156d4efdea6687e58ea3af5ac2653429 = false;
			return;
		}
		if (ca05864f40d68d0a518c8cf96ecefacd9)
		{
			c4f8aca5e059a764df76024e3ebee50d8();
			ca05864f40d68d0a518c8cf96ecefacd9 = false;
		}
		if (c02afc629ff9c33cc78da89c7f98f46a6)
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
			cb8ef915b00858e6d77fc8008b14849e6();
			c02afc629ff9c33cc78da89c7f98f46a6 = false;
		}
		c58ec0a33854201ce1ab39ec2d7506517();
	}

	private void c06c72b133bed662e2f9a87ae052639e3()
	{
		if (c792db5efbaf1a307f0518fc5478235ef)
		{
			return;
		}
		c792db5efbaf1a307f0518fc5478235ef = true;
		using (List<RigidBody>.Enumerator enumerator = ca97b0d2056b6bcdf7091a0c33a7b2d74.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				RigidBody current = enumerator.Current;
				if (current != null)
				{
					c729503850b1dde68cce1c94f7a27d104.RigidBodies.Add(current);
				}
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
					__ldtoken(AvatarRagdollAnimation.c06c72b133bed662e2f9a87ae052639e3());
				}
				break;
			}
		}
		using (List<BallJoint>.Enumerator enumerator2 = c14e53428041de292581f200d8aaf4c1e.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				BallJoint current2 = enumerator2.Current;
				c729503850b1dde68cce1c94f7a27d104.Constraints.Add(current2);
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					goto end_IL_00a4;
				}
				continue;
				end_IL_00a4:
				break;
			}
		}
		if (c5330841072d34369931c3c693aa5d5bc)
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
			using List<Constraint>.Enumerator enumerator3 = c4db155f25335b2ae95eeb3353c9dc7c8.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				Constraint current3 = enumerator3.Current;
				c729503850b1dde68cce1c94f7a27d104.Constraints.Add(current3);
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					goto end_IL_0103;
				}
				continue;
				end_IL_0103:
				break;
			}
		}
		if (!c3fa087251bf85c20a38db245aa73001c)
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
			using List<QuaternionMotor>.Enumerator enumerator4 = c476f9d2a53540e1d4de17504927f6901.GetEnumerator();
			while (enumerator4.MoveNext())
			{
				QuaternionMotor current4 = enumerator4.Current;
				if (current4 != null)
				{
					c729503850b1dde68cce1c94f7a27d104.Constraints.Add(current4);
				}
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

	private void c84c8eb6894fdb45d3105d63d8a7c28fd()
	{
		if (!c792db5efbaf1a307f0518fc5478235ef)
		{
			return;
		}
		c792db5efbaf1a307f0518fc5478235ef = false;
		if (c3fa087251bf85c20a38db245aa73001c)
		{
			foreach (QuaternionMotor item in c476f9d2a53540e1d4de17504927f6901)
			{
				if (item != null)
				{
					c729503850b1dde68cce1c94f7a27d104.Constraints.Remove(item);
				}
			}
		}
		if (c5330841072d34369931c3c693aa5d5bc)
		{
			using List<Constraint>.Enumerator enumerator2 = c4db155f25335b2ae95eeb3353c9dc7c8.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Constraint current2 = enumerator2.Current;
				c729503850b1dde68cce1c94f7a27d104.Constraints.Remove(current2);
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
					__ldtoken(AvatarRagdollAnimation.c84c8eb6894fdb45d3105d63d8a7c28fd());
				}
				break;
			}
		}
		foreach (BallJoint item2 in c14e53428041de292581f200d8aaf4c1e)
		{
			c729503850b1dde68cce1c94f7a27d104.Constraints.Remove(item2);
		}
		foreach (RigidBody item3 in ca97b0d2056b6bcdf7091a0c33a7b2d74)
		{
			if (item3 != null)
			{
				c729503850b1dde68cce1c94f7a27d104.RigidBodies.Remove(item3);
			}
		}
	}

	private void c35721cd3744fb3e9141424b32ebf0813()
	{
		for (int i = 0; i < 71; i++)
		{
			if (ca97b0d2056b6bcdf7091a0c33a7b2d74[i] == null)
			{
				continue;
			}
			int num = c228112107f106a5ec474a36e940e90b5.ParentBones[i];
			Pose pose = ((num >= 0) ? (ca97b0d2056b6bcdf7091a0c33a7b2d74[num].Pose * cd6c1c4383c8c767867b7010a2d803405[num].Inverse) : cd337f7a206e7977cce3e1030296b1b57);
			Pose pose2 = Pose.FromMatrix(cac7c4f631e0929b81ab7031d896a2f26[i]);
			if (i == 0)
			{
				pose2.Position *= c8a035e79b582cd5fd7cd2b1a6f489725;
			}
			Pose pose3 = ccbe287c53bf4266bdffce5ba1332d614[i] * pose2;
			if (i != 0)
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
					__ldtoken(AvatarRagdollAnimation.c35721cd3744fb3e9141424b32ebf0813());
				}
				pose3.Position *= c8a035e79b582cd5fd7cd2b1a6f489725;
			}
			ca97b0d2056b6bcdf7091a0c33a7b2d74[i].Pose = pose * pose3 * cd6c1c4383c8c767867b7010a2d803405[i];
			ca97b0d2056b6bcdf7091a0c33a7b2d74[i].LinearVelocity = Vector3F.Zero;
			ca97b0d2056b6bcdf7091a0c33a7b2d74[i].AngularVelocity = Vector3F.Zero;
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

	private void c58ec0a33854201ce1ab39ec2d7506517()
	{
		for (int i = 0; i < 71; i++)
		{
			if (ca97b0d2056b6bcdf7091a0c33a7b2d74[i] == null)
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
				__ldtoken(AvatarRagdollAnimation.c58ec0a33854201ce1ab39ec2d7506517());
			}
			int num = c228112107f106a5ec474a36e940e90b5.ParentBones[i];
			Pose pose;
			if (num < 0)
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
				pose = cd337f7a206e7977cce3e1030296b1b57;
			}
			else
			{
				pose = ca97b0d2056b6bcdf7091a0c33a7b2d74[num].Pose * cd6c1c4383c8c767867b7010a2d803405[num].Inverse;
			}
			Pose pose2 = pose;
			Pose pose3 = pose2.Inverse * ca97b0d2056b6bcdf7091a0c33a7b2d74[i].Pose * cd6c1c4383c8c767867b7010a2d803405[i].Inverse;
			if (i != 0)
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
				pose3.Position /= c8a035e79b582cd5fd7cd2b1a6f489725;
			}
			Pose pose4 = ccbe287c53bf4266bdffce5ba1332d614[i].Inverse * pose3;
			if (i == 0)
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
				pose4.Position /= c8a035e79b582cd5fd7cd2b1a6f489725;
			}
			cac7c4f631e0929b81ab7031d896a2f26[i] = pose4;
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

	private void c4f8aca5e059a764df76024e3ebee50d8()
	{
		if (cb653bf43e3eec1018e0c10e91c0cd591 == null)
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
				__ldtoken(AvatarRagdollAnimation.c4f8aca5e059a764df76024e3ebee50d8());
			}
			cb653bf43e3eec1018e0c10e91c0cd591 = new List<Pose>(71);
			for (int i = 0; i < 71; i++)
			{
				cb653bf43e3eec1018e0c10e91c0cd591.Add(Pose.Identity);
			}
		}
		cb653bf43e3eec1018e0c10e91c0cd591.Add(ca97b0d2056b6bcdf7091a0c33a7b2d74[0].Pose);
		for (int j = 1; j < 71; j++)
		{
			if (ca97b0d2056b6bcdf7091a0c33a7b2d74[j] == null)
			{
				continue;
			}
			int index = c228112107f106a5ec474a36e940e90b5.ParentBones[j];
			Pose pose = cb653bf43e3eec1018e0c10e91c0cd591[index] * cd6c1c4383c8c767867b7010a2d803405[index].Inverse;
			Pose pose2 = Pose.FromMatrix(c06a1fb656358c46c1f82e6f55636ea4e[j]);
			if (j == 0)
			{
				pose2.Position *= c8a035e79b582cd5fd7cd2b1a6f489725;
			}
			Pose pose3 = ccbe287c53bf4266bdffce5ba1332d614[j] * pose2;
			if (j != 0)
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
				pose3.Position *= c8a035e79b582cd5fd7cd2b1a6f489725;
			}
			cb653bf43e3eec1018e0c10e91c0cd591[j] = pose * pose3 * cd6c1c4383c8c767867b7010a2d803405[j];
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.Root, AvatarBone.BackLower);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.BackLower, AvatarBone.BackUpper);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.BackUpper, AvatarBone.Neck);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.Neck, AvatarBone.Head);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.BackUpper, AvatarBone.CollarLeft);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.BackUpper, AvatarBone.CollarRight);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.CollarLeft, AvatarBone.ShoulderLeft);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.CollarRight, AvatarBone.ShoulderRight);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.ShoulderLeft, AvatarBone.ElbowLeft);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.ShoulderRight, AvatarBone.ElbowRight);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.ElbowLeft, AvatarBone.WristLeft);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.ElbowRight, AvatarBone.WristRight);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.Root, AvatarBone.HipLeft);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.Root, AvatarBone.HipRight);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.HipLeft, AvatarBone.KneeLeft);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.HipRight, AvatarBone.KneeRight);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.KneeLeft, AvatarBone.AnkleLeft);
			cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone.KneeRight, AvatarBone.AnkleRight);
			return;
		}
	}

	private void cd935a1d1adfff74cb6adb9aac610f6b4(AvatarBone c8fcf57ff39c079b4b8e66f10e8859394, AvatarBone c0d27bb0e263c237fc97bad8a8dacc3fe)
	{
		Matrix33F rotationMatrix = cb653bf43e3eec1018e0c10e91c0cd591[(int)c8fcf57ff39c079b4b8e66f10e8859394].Orientation.Transposed * cb653bf43e3eec1018e0c10e91c0cd591[(int)c0d27bb0e263c237fc97bad8a8dacc3fe].Orientation;
		c476f9d2a53540e1d4de17504927f6901[(int)c0d27bb0e263c237fc97bad8a8dacc3fe].TargetOrientation = QuaternionF.CreateRotation(rotationMatrix);
	}

	private void cb8ef915b00858e6d77fc8008b14849e6()
	{
		using List<QuaternionMotor>.Enumerator enumerator = c476f9d2a53540e1d4de17504927f6901.GetEnumerator();
		while (enumerator.MoveNext())
		{
			QuaternionMotor current = enumerator.Current;
			if (current != null)
			{
				current.SpringConstant = c6e73e2b321632ffda3c817af4598a6af;
				current.DampingConstant = c789a67a7250bbf53bdfef25b80f0ed2b;
			}
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
				__ldtoken(AvatarRagdollAnimation.cb8ef915b00858e6d77fc8008b14849e6());
			}
			return;
		}
	}

	private void c5f12f281f85fe1aa9ff2a63186c84393()
	{
		if (c228112107f106a5ec474a36e940e90b5.State != AvatarRendererState.Ready)
		{
			return;
		}
		ccbe287c53bf4266bdffce5ba1332d614 = new List<Pose>();
		for (int i = 0; i < 71; i++)
		{
			((Matrix44F)c228112107f106a5ec474a36e940e90b5.BindPose[i]).Decompose(out var scale, out var rotation, out var translation);
			ccbe287c53bf4266bdffce5ba1332d614.Add(new Pose(translation, rotation));
			if (i == 0)
			{
				c8a035e79b582cd5fd7cd2b1a6f489725 = scale.Y;
			}
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
				__ldtoken(AvatarRagdollAnimation.c5f12f281f85fe1aa9ff2a63186c84393());
			}
			ca97b0d2056b6bcdf7091a0c33a7b2d74 = new List<RigidBody>(71);
			cd6c1c4383c8c767867b7010a2d803405 = new List<Pose>(71);
			c14e53428041de292581f200d8aaf4c1e = new List<BallJoint>();
			c4db155f25335b2ae95eeb3353c9dc7c8 = new List<Constraint>();
			c476f9d2a53540e1d4de17504927f6901 = new List<QuaternionMotor>();
			for (int j = 0; j < 71; j++)
			{
				ca97b0d2056b6bcdf7091a0c33a7b2d74.Add(null);
				cd6c1c4383c8c767867b7010a2d803405.Add(Pose.Identity);
				c476f9d2a53540e1d4de17504927f6901.Add(null);
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				MassFrame massFrame = MassFrame.FromShapeAndMass(new SphereShape(0.2f), new Vector3F(c8a035e79b582cd5fd7cd2b1a6f489725), 4f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.1f, 1);
				UniformMaterial material = new UniformMaterial();
				ca97b0d2056b6bcdf7091a0c33a7b2d74[0] = new RigidBody(new BoxShape(0.22f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.16f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.16f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[0] = new Pose(new Vector3F(0f, -0.08f, -0.01f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationX(-0f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[1] = new RigidBody(new BoxShape(0.22f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.16f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.16f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[1] = new Pose(new Vector3F(0f, 0.08f, -0.01f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationX(-0f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[5] = new RigidBody(new BoxShape(0.22f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.16f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.16f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[5] = new Pose(new Vector3F(0f, 0.08f, -0.01f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationX(-0.1f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[14] = new RigidBody(new CapsuleShape(0.04f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.09f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				ca97b0d2056b6bcdf7091a0c33a7b2d74[19] = new RigidBody(new SphereShape(0.15f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[19] = new Pose(new Vector3F(0f, 0.1f, 0f) * c8a035e79b582cd5fd7cd2b1a6f489725);
				ca97b0d2056b6bcdf7091a0c33a7b2d74[12] = new RigidBody(new EmptyShape(), massFrame, material);
				ca97b0d2056b6bcdf7091a0c33a7b2d74[16] = new RigidBody(new EmptyShape(), massFrame, material);
				ca97b0d2056b6bcdf7091a0c33a7b2d74[20] = new RigidBody(new CapsuleShape(0.04f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.25f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[20] = new Pose(new Vector3F(0.08f, 0f, -0.02f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationZ((float)Math.PI / 2f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[22] = new RigidBody(new CapsuleShape(0.04f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.25f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[22] = new Pose(new Vector3F(-0.08f, 0f, -0.02f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationZ((float)Math.PI / 2f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[25] = new RigidBody(new CapsuleShape(0.04f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.21f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[25] = new Pose(new Vector3F(0.06f, 0f, -0.02f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationZ((float)Math.PI / 2f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[28] = new RigidBody(new CapsuleShape(0.04f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.21f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[28] = new Pose(new Vector3F(-0.06f, 0f, -0.02f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationZ((float)Math.PI / 2f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[33] = new RigidBody(new BoxShape(0.1f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.04f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.1f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[33] = new Pose(new Vector3F(0.06f, -0.02f, -0.01f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationZ(0f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[36] = new RigidBody(new BoxShape(0.1f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.04f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.1f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[36] = new Pose(new Vector3F(-0.06f, -0.02f, -0.01f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationZ(0f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[2] = new RigidBody(new CapsuleShape(0.06f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.34f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[2] = new Pose(new Vector3F(0f, -0.14f, -0.02f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationX(0.1f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[3] = new RigidBody(new CapsuleShape(0.06f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.34f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[3] = new Pose(new Vector3F(0f, -0.14f, -0.02f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationX(0.1f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[6] = new RigidBody(new CapsuleShape(0.06f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.36f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[6] = new Pose(new Vector3F(0f, -0.18f, -0.04f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationX(0.1f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[8] = new RigidBody(new CapsuleShape(0.06f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.36f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[8] = new Pose(new Vector3F(0f, -0.18f, -0.04f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationX(0.1f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[11] = new RigidBody(new BoxShape(0.1f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.06f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.22f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[11] = new Pose(new Vector3F(0f, -0.07f, 0.05f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationZ(0f));
				ca97b0d2056b6bcdf7091a0c33a7b2d74[15] = new RigidBody(new BoxShape(0.1f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.06f * c8a035e79b582cd5fd7cd2b1a6f489725, 0.22f * c8a035e79b582cd5fd7cd2b1a6f489725), massFrame, material);
				cd6c1c4383c8c767867b7010a2d803405[15] = new Pose(new Vector3F(0f, -0.07f, 0.05f) * c8a035e79b582cd5fd7cd2b1a6f489725, QuaternionF.CreateRotationZ(0f));
				c4919f248e728afb8b4e1265ed264e932 = new List<RigidBody>();
				for (int k = 0; k < 71; k++)
				{
					if (ca97b0d2056b6bcdf7091a0c33a7b2d74[k] != null)
					{
						c4919f248e728afb8b4e1265ed264e932.Add(ca97b0d2056b6bcdf7091a0c33a7b2d74[k]);
					}
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					c0844a56cf0491e8cf82a124f913a0564 = new ReadOnlyCollection<RigidBody>(c4919f248e728afb8b4e1265ed264e932);
					List<Pose> list = new List<Pose>();
					for (int l = 0; l < 71; l++)
					{
						list.Add(Pose.Identity);
						if (ca97b0d2056b6bcdf7091a0c33a7b2d74[l] == null)
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
						int num = c228112107f106a5ec474a36e940e90b5.ParentBones[l];
						Pose pose = ((num >= 0) ? list[num] : Pose.Identity);
						Pose pose2 = ccbe287c53bf4266bdffce5ba1332d614[l];
						if (l != 0)
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
							pose2.Position *= c8a035e79b582cd5fd7cd2b1a6f489725;
						}
						list[l] = pose * pose2;
					}
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.Root, AvatarBone.BackLower, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.BackLower, AvatarBone.BackUpper, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.BackUpper, AvatarBone.Neck, list, 0.6f, 1E-06f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.Neck, AvatarBone.Head, list, 0.6f, 1E-06f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.BackUpper, AvatarBone.CollarLeft, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.BackUpper, AvatarBone.CollarRight, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.CollarLeft, AvatarBone.ShoulderLeft, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.CollarRight, AvatarBone.ShoulderRight, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.ShoulderLeft, AvatarBone.ElbowLeft, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.ShoulderRight, AvatarBone.ElbowRight, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.ElbowLeft, AvatarBone.WristLeft, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.ElbowRight, AvatarBone.WristRight, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.Root, AvatarBone.HipLeft, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.Root, AvatarBone.HipRight, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.HipLeft, AvatarBone.KneeLeft, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.HipRight, AvatarBone.KneeRight, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.KneeLeft, AvatarBone.AnkleLeft, list, 0.2f, 0.0001f);
					cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone.KneeRight, AvatarBone.AnkleRight, list, 0.2f, 0.0001f);
					c7ee1ca2d08837d9f53342888a58de276(AvatarBone.Root, AvatarBone.BackLower, list[1].Orientation, list[1].Orientation, new Vector3F(-0.3f, 0f, 0f), new Vector3F(0.3f, 0f, 0f), list);
					c7ee1ca2d08837d9f53342888a58de276(AvatarBone.BackLower, AvatarBone.BackUpper, list[5].Orientation, list[5].Orientation, new Vector3F(-0.3f, 0f, 0f), new Vector3F(0.4f, 0f, 0f), list);
					Matrix33F matrix33F = Matrix33F.CreateRotationZ((float)Math.PI / 2f);
					c97bac2591978a10350ba576198692c35(AvatarBone.BackUpper, AvatarBone.Neck, matrix33F, matrix33F, new Vector3F(-0.1f, -0.3f, -0.3f), new Vector3F(0.1f, 0.3f, 0.3f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.Neck, AvatarBone.Head, matrix33F, matrix33F, new Vector3F(-0.1f, -0.6f, -0.6f), new Vector3F(0.1f, 0.6f, 0.6f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.BackUpper, AvatarBone.CollarLeft, list[12].Orientation, list[12].Orientation, new Vector3F(0f), new Vector3F(0f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.BackUpper, AvatarBone.CollarRight, list[16].Orientation, list[16].Orientation, new Vector3F(0f), new Vector3F(0f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.CollarLeft, AvatarBone.ShoulderLeft, Matrix33F.Identity, Matrix33F.CreateRotationY(0.7f), new Vector3F(-0.7f, -1.2f, -1.2f), new Vector3F(0.7f, 1.2f, 1.2f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.CollarRight, AvatarBone.ShoulderRight, Matrix33F.Identity, Matrix33F.CreateRotationY(-0.7f), new Vector3F(-0.7f, -1.2f, -1.2f), new Vector3F(0.7f, 1.2f, 1.2f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.ShoulderLeft, AvatarBone.ElbowLeft, Matrix33F.Identity, Matrix33F.CreateRotationY(1.2f), new Vector3F(-0.7f, -1.2f, -1.2f), new Vector3F(0.7f, 1.2f, 1.2f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.ShoulderRight, AvatarBone.ElbowRight, Matrix33F.Identity, Matrix33F.CreateRotationY(-1.2f), new Vector3F(-0.7f, -1.2f, -1.2f), new Vector3F(0.7f, 1.2f, 1.2f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.ElbowLeft, AvatarBone.WristLeft, Matrix33F.Identity, Matrix33F.Identity, new Vector3F(-0.7f, -0.7f, -0.7f), new Vector3F(0.7f, 0.7f, 0.7f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.ElbowRight, AvatarBone.WristRight, Matrix33F.Identity, Matrix33F.Identity, new Vector3F(-0.7f, -0.7f, -0.7f), new Vector3F(0.7f, 0.7f, 0.7f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.Root, AvatarBone.HipLeft, matrix33F, Matrix33F.CreateRotationX(-1.2f) * Matrix33F.CreateRotationZ(1.7707964f), new Vector3F(-0.1f, -1.5f, -0.7f), new Vector3F(0.1f, 1.5f, 0.7f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.Root, AvatarBone.HipRight, matrix33F, Matrix33F.CreateRotationX(-1.2f) * Matrix33F.CreateRotationZ(1.3707963f), new Vector3F(-0.1f, -1.5f, -0.7f), new Vector3F(0.1f, 1.5f, 0.7f), list);
					c7ee1ca2d08837d9f53342888a58de276(AvatarBone.HipLeft, AvatarBone.KneeLeft, list[6].Orientation, list[6].Orientation, new Vector3F(0f, 0f, 0f), new Vector3F(2.2f, 0f, 0f), list);
					c7ee1ca2d08837d9f53342888a58de276(AvatarBone.HipRight, AvatarBone.KneeRight, list[8].Orientation, list[8].Orientation, new Vector3F(0f, 0f, 0f), new Vector3F(2.2f, 0f, 0f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.KneeLeft, AvatarBone.AnkleLeft, matrix33F, matrix33F, new Vector3F(-0.1f, -0.7f, -0.3f), new Vector3F(0.1f, 0.7f, 0.3f), list);
					c97bac2591978a10350ba576198692c35(AvatarBone.KneeRight, AvatarBone.AnkleRight, matrix33F, matrix33F, new Vector3F(-0.1f, -0.7f, -0.3f), new Vector3F(0.1f, 0.7f, 0.3f), list);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.Root, AvatarBone.BackLower);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.BackLower, AvatarBone.BackUpper);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.BackUpper, AvatarBone.Neck);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.Neck, AvatarBone.Head);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.BackUpper, AvatarBone.CollarLeft);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.BackUpper, AvatarBone.CollarRight);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.CollarLeft, AvatarBone.ShoulderLeft);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.CollarRight, AvatarBone.ShoulderRight);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.ShoulderLeft, AvatarBone.ElbowLeft);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.ShoulderRight, AvatarBone.ElbowRight);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.ElbowLeft, AvatarBone.WristLeft);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.ElbowRight, AvatarBone.WristRight);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.Root, AvatarBone.HipLeft);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.Root, AvatarBone.HipRight);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.HipLeft, AvatarBone.KneeLeft);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.HipRight, AvatarBone.KneeRight);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.KneeLeft, AvatarBone.AnkleLeft);
					c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone.KneeRight, AvatarBone.AnkleRight);
					ca97b0d2056b6bcdf7091a0c33a7b2d74[14].CollisionObject.Enabled = false;
					ca97b0d2056b6bcdf7091a0c33a7b2d74[12].CollisionObject.Enabled = false;
					ca97b0d2056b6bcdf7091a0c33a7b2d74[16].CollisionObject.Enabled = false;
					CollisionFilter collisionFilter = (CollisionFilter)c729503850b1dde68cce1c94f7a27d104.CollisionDomain.CollisionDetection.CollisionFilter;
					collisionFilter.Set(ca97b0d2056b6bcdf7091a0c33a7b2d74[5].CollisionObject, ca97b0d2056b6bcdf7091a0c33a7b2d74[20].CollisionObject, collisionsEnabled: false);
					collisionFilter.Set(ca97b0d2056b6bcdf7091a0c33a7b2d74[5].CollisionObject, ca97b0d2056b6bcdf7091a0c33a7b2d74[22].CollisionObject, collisionsEnabled: false);
					return;
				}
			}
		}
	}

	private void cdb496ac5dfaf7976dbc6b5c9159496df(AvatarBone c8fcf57ff39c079b4b8e66f10e8859394, AvatarBone c0d27bb0e263c237fc97bad8a8dacc3fe, IList<Pose> cb0a6a4260b25f988b763db42c78a70e9, float c75186be1308be18e3492ddaaeb084262, float cad6d615cea81a5446de3403a944a2d81)
	{
		BallJoint ballJoint = new BallJoint();
		ballJoint.BodyA = ca97b0d2056b6bcdf7091a0c33a7b2d74[(int)c8fcf57ff39c079b4b8e66f10e8859394];
		ballJoint.BodyB = ca97b0d2056b6bcdf7091a0c33a7b2d74[(int)c0d27bb0e263c237fc97bad8a8dacc3fe];
		ballJoint.CollisionEnabled = false;
		ballJoint.AnchorPositionALocal = (cd6c1c4383c8c767867b7010a2d803405[(int)c8fcf57ff39c079b4b8e66f10e8859394].Inverse * cb0a6a4260b25f988b763db42c78a70e9[(int)c8fcf57ff39c079b4b8e66f10e8859394].Inverse * cb0a6a4260b25f988b763db42c78a70e9[(int)c0d27bb0e263c237fc97bad8a8dacc3fe]).Position;
		ballJoint.AnchorPositionBLocal = cd6c1c4383c8c767867b7010a2d803405[(int)c0d27bb0e263c237fc97bad8a8dacc3fe].Inverse.Position;
		ballJoint.ErrorReduction = c75186be1308be18e3492ddaaeb084262;
		ballJoint.Softness = cad6d615cea81a5446de3403a944a2d81;
		BallJoint item = ballJoint;
		c14e53428041de292581f200d8aaf4c1e.Add(item);
	}

	private void c97bac2591978a10350ba576198692c35(AvatarBone c8fcf57ff39c079b4b8e66f10e8859394, AvatarBone c0d27bb0e263c237fc97bad8a8dacc3fe, Matrix33F cd69379845bca34cd747feb41bf56cb07, Matrix33F cb3071e7cd5dc004f23b4d0dbd00e1a04, Vector3F caaf95081cf14ad7794bfd88499640354, Vector3F ce19571f79859f441a405d0d1ee01f53e, IList<Pose> cb0a6a4260b25f988b763db42c78a70e9)
	{
		TwistSwingLimit twistSwingLimit = new TwistSwingLimit();
		twistSwingLimit.BodyA = ca97b0d2056b6bcdf7091a0c33a7b2d74[(int)c8fcf57ff39c079b4b8e66f10e8859394];
		twistSwingLimit.BodyB = ca97b0d2056b6bcdf7091a0c33a7b2d74[(int)c0d27bb0e263c237fc97bad8a8dacc3fe];
		twistSwingLimit.AnchorOrientationALocal = cd6c1c4383c8c767867b7010a2d803405[(int)c8fcf57ff39c079b4b8e66f10e8859394].Orientation.Transposed * cb0a6a4260b25f988b763db42c78a70e9[(int)c8fcf57ff39c079b4b8e66f10e8859394].Orientation.Transposed * cd69379845bca34cd747feb41bf56cb07;
		twistSwingLimit.AnchorOrientationBLocal = cd6c1c4383c8c767867b7010a2d803405[(int)c0d27bb0e263c237fc97bad8a8dacc3fe].Orientation.Transposed * cb0a6a4260b25f988b763db42c78a70e9[(int)c0d27bb0e263c237fc97bad8a8dacc3fe].Orientation.Transposed * cb3071e7cd5dc004f23b4d0dbd00e1a04;
		twistSwingLimit.Minimum = caaf95081cf14ad7794bfd88499640354;
		twistSwingLimit.Maximum = ce19571f79859f441a405d0d1ee01f53e;
		twistSwingLimit.ErrorReduction = 0.2f;
		twistSwingLimit.Softness = 0.001f;
		TwistSwingLimit item = twistSwingLimit;
		c4db155f25335b2ae95eeb3353c9dc7c8.Add(item);
	}

	private void c7ee1ca2d08837d9f53342888a58de276(AvatarBone c8fcf57ff39c079b4b8e66f10e8859394, AvatarBone c0d27bb0e263c237fc97bad8a8dacc3fe, Matrix33F cd69379845bca34cd747feb41bf56cb07, Matrix33F cb3071e7cd5dc004f23b4d0dbd00e1a04, Vector3F caaf95081cf14ad7794bfd88499640354, Vector3F ce19571f79859f441a405d0d1ee01f53e, IList<Pose> cb0a6a4260b25f988b763db42c78a70e9)
	{
		AngularLimit angularLimit = new AngularLimit();
		angularLimit.BodyA = ca97b0d2056b6bcdf7091a0c33a7b2d74[(int)c8fcf57ff39c079b4b8e66f10e8859394];
		angularLimit.BodyB = ca97b0d2056b6bcdf7091a0c33a7b2d74[(int)c0d27bb0e263c237fc97bad8a8dacc3fe];
		angularLimit.AnchorOrientationALocal = cd6c1c4383c8c767867b7010a2d803405[(int)c8fcf57ff39c079b4b8e66f10e8859394].Orientation.Transposed * cb0a6a4260b25f988b763db42c78a70e9[(int)c8fcf57ff39c079b4b8e66f10e8859394].Orientation.Transposed * cd69379845bca34cd747feb41bf56cb07;
		angularLimit.AnchorOrientationBLocal = cd6c1c4383c8c767867b7010a2d803405[(int)c0d27bb0e263c237fc97bad8a8dacc3fe].Orientation.Transposed * cb0a6a4260b25f988b763db42c78a70e9[(int)c0d27bb0e263c237fc97bad8a8dacc3fe].Orientation.Transposed * cb3071e7cd5dc004f23b4d0dbd00e1a04;
		angularLimit.Minimum = caaf95081cf14ad7794bfd88499640354;
		angularLimit.Maximum = ce19571f79859f441a405d0d1ee01f53e;
		angularLimit.ErrorReduction = new Vector3F(0.2f);
		angularLimit.Softness = new Vector3F(0.001f);
		AngularLimit item = angularLimit;
		c4db155f25335b2ae95eeb3353c9dc7c8.Add(item);
	}

	private void c3f217219d0d36c779ee6af11fba2c8a5(AvatarBone c8fcf57ff39c079b4b8e66f10e8859394, AvatarBone c0d27bb0e263c237fc97bad8a8dacc3fe)
	{
		QuaternionMotor quaternionMotor = new QuaternionMotor();
		quaternionMotor.BodyA = ca97b0d2056b6bcdf7091a0c33a7b2d74[(int)c8fcf57ff39c079b4b8e66f10e8859394];
		quaternionMotor.BodyB = ca97b0d2056b6bcdf7091a0c33a7b2d74[(int)c0d27bb0e263c237fc97bad8a8dacc3fe];
		quaternionMotor.DampingConstant = 1000000f;
		quaternionMotor.SpringConstant = 10000000f;
		quaternionMotor.MaxForce = float.PositiveInfinity;
		quaternionMotor.AnchorOrientationALocal = Matrix33F.Identity;
		quaternionMotor.AnchorOrientationBLocal = Matrix33F.Identity;
		quaternionMotor.UseSingleAxisMode = false;
		QuaternionMotor value = quaternionMotor;
		c476f9d2a53540e1d4de17504927f6901[(int)c0d27bb0e263c237fc97bad8a8dacc3fe] = value;
	}

	[Conditional("DEBUG")]
	private void c61cb1ff1a6bad364a019ec8089d42893()
	{
		for (int i = 0; i < 71; i++)
		{
			_ = c228112107f106a5ec474a36e940e90b5.ParentBones[i];
			((Matrix44F)c228112107f106a5ec474a36e940e90b5.BindPose[i]).Decompose(out var scale, out var _, out var _);
			if (Vector3F.AreNumericallyEqual(scale, Vector3F.One))
			{
				continue;
			}
			foreach (AvatarBone item in EnumHelper.GetValues(typeof(AvatarBone)).OfType<AvatarBone>())
			{
				if (item != (AvatarBone)i)
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
				if (1 == 0)
				{
					__ldtoken(AvatarRagdollAnimation.c61cb1ff1a6bad364a019ec8089d42893());
				}
				if (!Numeric.AreEqual(scale.X, scale.Y))
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
				Numeric.AreEqual(scale.X, scale.Z);
			}
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			new List<Matrix>(cac7c4f631e0929b81ab7031d896a2f26);
			c35721cd3744fb3e9141424b32ebf0813();
			c58ec0a33854201ce1ab39ec2d7506517();
			for (int j = 0; j < 71; j++)
			{
			}
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
	}
}
