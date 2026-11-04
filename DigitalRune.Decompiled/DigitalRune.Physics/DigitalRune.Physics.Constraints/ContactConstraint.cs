using System;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Physics.Materials;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a constraint at a rigid body contact that models non-penetration, dry friction
///       and bounciness.
///       </summary>
public class ContactConstraint : IConstraint
{
	private static readonly ResourcePool<ContactConstraint> c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<ContactConstraint>(() => new ContactConstraint(), null, null);

	private Vector3F c963dfc78572f5d40fe388983b1993dcc;

	private float c7307432f075faddecbd4e53e09ed948a;

	private Vector3F c30e121ffd941742fbb472180bd6f42b8;

	private float c963d2a9b7488a67926dcfc340ebca4d3;

	private Vector3F c875514674dce71fb1a6814fe2c63c57f;

	private Vector3F ca1b39820bdcd223de9cec843cb9a69c1;

	private Vector3F cbbadb32b8d5af8e23b9b4d3d440028a5;

	private float c3029b3eb4d165025b85c5363274411cc;

	private float c75e8d5254c8b573a529df00477042b97;

	private float cd3d90de535d3e61ac1f4aa8982379a33;

	private readonly ce427b5a6ee07208e331688e3ede5e795 cb11ae9e27a8b0e95f5fcd44c7219b9d8 = new ce427b5a6ee07208e331688e3ede5e795();

	private readonly ce427b5a6ee07208e331688e3ede5e795 ce8484c1eb41bd27c7d341c3339a7930b = new ce427b5a6ee07208e331688e3ede5e795();

	private readonly ce427b5a6ee07208e331688e3ede5e795 ccf2e328c6889968d331efe647fc9c047 = new ce427b5a6ee07208e331688e3ede5e795();

	private float c07d6b9586897ae4e5d738b4006894217;

	private float cb283829888a8db0c571899e633aa28b8;

	private bool cf480035e0690734609e96f1b31388c51;

	private bool c254f70f6c865eadf4e84b71e1f23e08a;

	private float c261f1d5d6196838b200ba8f48c2b9aec;

	private RigidBody c93efd5f1d1e0a28c4b524b761aff691c;

	private RigidBody c3fa55a45bff4b015fb13d00b2b744ed8;

	private Simulation c729503850b1dde68cce1c94f7a27d104;

	[CompilerGenerated]
	private Contact cfc3d2ec644d43f0132f479fcddc68d77;

	[CompilerGenerated]
	private float ca3e7cc693eaabc0c75b0144a021d9e1a;

	[CompilerGenerated]
	private float c0e6f6f2eb839c8026cb309e997a50c28;

	[CompilerGenerated]
	private bool c70be2905cb33a3029e620f5525ae0606;

	[CompilerGenerated]
	private static Func<ContactConstraint> c49253b4520c1b7e183dfcc9c426c0283;

	/// <summary>
	///       Gets the first body.
	///       </summary>
	public RigidBody BodyA => c93efd5f1d1e0a28c4b524b761aff691c;

	/// <summary>
	///       Gets the second body.
	///       </summary>
	public RigidBody BodyB => c3fa55a45bff4b015fb13d00b2b744ed8;

	private bool cd635082a93f02af557407725c88b2e78 => true;

	private bool ca0cf37740dd2154a1c932d4d0662c19e => true;

	/// <summary>
	///       Gets the simulation to which this constraint belongs.
	///       </summary>
	public Simulation Simulation => c729503850b1dde68cce1c94f7a27d104;

	/// <summary>
	///       Gets the contact.
	///       </summary>
	public Contact Contact
	{
		[CompilerGenerated]
		get
		{
			return cfc3d2ec644d43f0132f479fcddc68d77;
		}
		[CompilerGenerated]
		private set
		{
			cfc3d2ec644d43f0132f479fcddc68d77 = value;
		}
	}

	/// <summary>
	///       Gets or sets the error reduction parameter.
	///       </summary>
	public float ErrorReduction
	{
		[CompilerGenerated]
		get
		{
			return ca3e7cc693eaabc0c75b0144a021d9e1a;
		}
		[CompilerGenerated]
		private set
		{
			ca3e7cc693eaabc0c75b0144a021d9e1a = value;
		}
	}

	internal Vector3F c9cf56ea482cbfc6991a9170d8c34c0b5
	{
		get
		{
			Vector3F vector3F = BodyA.cdb4f28852f8fdd848be747eebc3db719 + Vector3F.Cross(BodyA.ce4d650b898b2c22b831fd9383017176f, c963dfc78572f5d40fe388983b1993dcc);
			Vector3F vector3F2 = BodyB.cdb4f28852f8fdd848be747eebc3db719 + Vector3F.Cross(BodyB.ce4d650b898b2c22b831fd9383017176f, c30e121ffd941742fbb472180bd6f42b8);
			Vector3F result = vector3F2 - vector3F;
			if (cf480035e0690734609e96f1b31388c51 || c254f70f6c865eadf4e84b71e1f23e08a)
			{
				if (cf480035e0690734609e96f1b31388c51)
				{
					MaterialProperties properties = BodyA.Material.GetProperties(BodyA, Contact.PositionALocal, Contact.FeatureA);
					Vector3F vector3F3 = BodyA.Pose.ToWorldDirection(properties.SurfaceMotion);
					vector3F3 -= Vector3F.Dot(vector3F3, Contact.Normal) * Contact.Normal;
					result -= vector3F3;
				}
				if (c254f70f6c865eadf4e84b71e1f23e08a)
				{
					MaterialProperties properties2 = BodyB.Material.GetProperties(BodyB, Contact.PositionBLocal, Contact.FeatureB);
					Vector3F vector3F4 = BodyB.Pose.ToWorldDirection(properties2.SurfaceMotion);
					vector3F4 -= Vector3F.Dot(vector3F4, Contact.Normal) * Contact.Normal;
					result += vector3F4;
				}
			}
			return result;
		}
	}

	internal float c71cc6e0ecf538e2f8708d76140391b8d => Vector3F.Dot(c9cf56ea482cbfc6991a9170d8c34c0b5, Contact.Normal);

	/// <summary>
	///       Gets or sets the linear constraint impulse that was applied. 
	///       </summary>
	public Vector3F LinearConstraintImpulse => cb11ae9e27a8b0e95f5fcd44c7219b9d8.cc01f881484f44c63c9c69258954b12e0 * cb11ae9e27a8b0e95f5fcd44c7219b9d8.cd113dd0023606dbb59275f5f206680fc + ce8484c1eb41bd27c7d341c3339a7930b.cc01f881484f44c63c9c69258954b12e0 * ce8484c1eb41bd27c7d341c3339a7930b.cd113dd0023606dbb59275f5f206680fc + ccf2e328c6889968d331efe647fc9c047.cc01f881484f44c63c9c69258954b12e0 * ccf2e328c6889968d331efe647fc9c047.cd113dd0023606dbb59275f5f206680fc;

	private Vector3F c9bc6aa6bb985717847e61153cf697482 => Vector3F.Zero;

	internal float StackingTolerance
	{
		[CompilerGenerated]
		get
		{
			return c0e6f6f2eb839c8026cb309e997a50c28;
		}
		[CompilerGenerated]
		set
		{
			c0e6f6f2eb839c8026cb309e997a50c28 = value;
		}
	}

	internal bool Used
	{
		[CompilerGenerated]
		get
		{
			return c70be2905cb33a3029e620f5525ae0606;
		}
		[CompilerGenerated]
		set
		{
			c70be2905cb33a3029e620f5525ae0606 = value;
		}
	}

	private ContactConstraint()
	{
	}

	internal static ContactConstraint c2d8ce8b1ccf2b905e3b18f8a5145fe8a(RigidBody cb07cec0ca0aa8ec568184df1c048b0c6, RigidBody c830da87a8ab344ada9c88687eb99712f, Contact c4858ff102f4ef50097c5a533c1478c21)
	{
		ContactConstraint contactConstraint = c8aca4a0e3c0073ffb188fde832680587.Obtain();
		contactConstraint.cfcf5000392556fa5f33e81db7674f9ed(cb07cec0ca0aa8ec568184df1c048b0c6, c830da87a8ab344ada9c88687eb99712f, c4858ff102f4ef50097c5a533c1478c21);
		return contactConstraint;
	}

	internal void ca18b9b5faa9851d6355f617787bb5859()
	{
		cc12faf558382b18980623c1ddc8d0332();
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	private void cfcf5000392556fa5f33e81db7674f9ed(RigidBody cb07cec0ca0aa8ec568184df1c048b0c6, RigidBody c830da87a8ab344ada9c88687eb99712f, Contact c4858ff102f4ef50097c5a533c1478c21)
	{
		c93efd5f1d1e0a28c4b524b761aff691c = cb07cec0ca0aa8ec568184df1c048b0c6;
		c3fa55a45bff4b015fb13d00b2b744ed8 = c830da87a8ab344ada9c88687eb99712f;
		Contact = c4858ff102f4ef50097c5a533c1478c21;
		c729503850b1dde68cce1c94f7a27d104 = c93efd5f1d1e0a28c4b524b761aff691c.Simulation ?? c3fa55a45bff4b015fb13d00b2b744ed8.Simulation;
		ErrorReduction = c729503850b1dde68cce1c94f7a27d104.Settings.Constraints.ContactErrorReduction;
		c261f1d5d6196838b200ba8f48c2b9aec = c729503850b1dde68cce1c94f7a27d104.Settings.Constraints.MinConstraintImpulse;
		Vector3F normal = Contact.Normal;
		c963dfc78572f5d40fe388983b1993dcc = Contact.PositionAWorld - BodyA.PoseCenterOfMass.Position;
		c30e121ffd941742fbb472180bd6f42b8 = Contact.PositionBWorld - BodyB.PoseCenterOfMass.Position;
		c7307432f075faddecbd4e53e09ed948a = -1f;
		c963d2a9b7488a67926dcfc340ebca4d3 = -1f;
		Vector3F vector3F = c9cf56ea482cbfc6991a9170d8c34c0b5;
		float num = Vector3F.Dot(vector3F, normal);
		MaterialProperties properties = BodyA.Material.GetProperties(BodyA, Contact.PositionALocal, Contact.FeatureA);
		MaterialProperties properties2 = BodyB.Material.GetProperties(BodyB, Contact.PositionBLocal, Contact.FeatureB);
		IMaterialPropertyCombiner materialPropertyCombiner = c729503850b1dde68cce1c94f7a27d104.Settings.MaterialPropertyCombiner;
		if (Math.Abs(num) > c729503850b1dde68cce1c94f7a27d104.Settings.Constraints.RestingVelocityLimit)
		{
			c3029b3eb4d165025b85c5363274411cc = materialPropertyCombiner.CombineRestitution(properties.Restitution, properties2.Restitution);
			if (c3029b3eb4d165025b85c5363274411cc < c729503850b1dde68cce1c94f7a27d104.Settings.Constraints.RestitutionThreshold)
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
					__ldtoken(ContactConstraint.cfcf5000392556fa5f33e81db7674f9ed(RigidBody, RigidBody, Contact));
				}
				c3029b3eb4d165025b85c5363274411cc = 0f;
			}
		}
		else
		{
			c3029b3eb4d165025b85c5363274411cc = 0f;
		}
		c75e8d5254c8b573a529df00477042b97 = materialPropertyCombiner.CombineFriction(properties.StaticFriction, properties2.StaticFriction);
		cd3d90de535d3e61ac1f4aa8982379a33 = materialPropertyCombiner.CombineFriction(properties.DynamicFriction, properties2.DynamicFriction);
		cf480035e0690734609e96f1b31388c51 = properties.SupportsSurfaceMotion;
		c254f70f6c865eadf4e84b71e1f23e08a = properties2.SupportsSurfaceMotion;
		ca1b39820bdcd223de9cec843cb9a69c1 = vector3F - num * normal;
		if (!ca1b39820bdcd223de9cec843cb9a69c1.TryNormalize())
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
			ca1b39820bdcd223de9cec843cb9a69c1 = normal.Orthonormal1;
		}
		cbbadb32b8d5af8e23b9b4d3d440028a5 = Vector3F.Cross(ca1b39820bdcd223de9cec843cb9a69c1, normal);
		cb11ae9e27a8b0e95f5fcd44c7219b9d8.cc01f881484f44c63c9c69258954b12e0 = 0f;
		ce8484c1eb41bd27c7d341c3339a7930b.cc01f881484f44c63c9c69258954b12e0 = 0f;
		ccf2e328c6889968d331efe647fc9c047.cc01f881484f44c63c9c69258954b12e0 = 0f;
	}

	private void cc12faf558382b18980623c1ddc8d0332()
	{
		c729503850b1dde68cce1c94f7a27d104 = null;
		c93efd5f1d1e0a28c4b524b761aff691c = null;
		c3fa55a45bff4b015fb13d00b2b744ed8 = null;
		Contact = null;
		Used = false;
	}

	/// <summary>
	///       Called by the simulation to prepare this constraint for constraint solving for a new
	///       time step.
	///       </summary>
	public void Setup()
	{
		c875514674dce71fb1a6814fe2c63c57f = Contact.Normal;
		float stackingTolerance = StackingTolerance;
		if (c7307432f075faddecbd4e53e09ed948a == -1f)
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
				__ldtoken(ContactConstraint.Setup());
			}
			cb11ae9e27a8b0e95f5fcd44c7219b9d8.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -c875514674dce71fb1a6814fe2c63c57f, -Vector3F.Cross(c963dfc78572f5d40fe388983b1993dcc, c875514674dce71fb1a6814fe2c63c57f), c875514674dce71fb1a6814fe2c63c57f, Vector3F.Cross(c30e121ffd941742fbb472180bd6f42b8, c875514674dce71fb1a6814fe2c63c57f));
			ce8484c1eb41bd27c7d341c3339a7930b.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -ca1b39820bdcd223de9cec843cb9a69c1, -Vector3F.Cross(c963dfc78572f5d40fe388983b1993dcc, ca1b39820bdcd223de9cec843cb9a69c1), ca1b39820bdcd223de9cec843cb9a69c1, Vector3F.Cross(c30e121ffd941742fbb472180bd6f42b8, ca1b39820bdcd223de9cec843cb9a69c1));
			ccf2e328c6889968d331efe647fc9c047.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -cbbadb32b8d5af8e23b9b4d3d440028a5, -Vector3F.Cross(c963dfc78572f5d40fe388983b1993dcc, cbbadb32b8d5af8e23b9b4d3d440028a5), cbbadb32b8d5af8e23b9b4d3d440028a5, Vector3F.Cross(c30e121ffd941742fbb472180bd6f42b8, cbbadb32b8d5af8e23b9b4d3d440028a5));
			c7307432f075faddecbd4e53e09ed948a = c963dfc78572f5d40fe388983b1993dcc.LengthSquared;
			c963d2a9b7488a67926dcfc340ebca4d3 = c30e121ffd941742fbb472180bd6f42b8.LengthSquared;
		}
		else if (stackingTolerance == 0f)
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
			c963dfc78572f5d40fe388983b1993dcc = Contact.PositionAWorld - BodyA.PoseCenterOfMass.Position;
			c30e121ffd941742fbb472180bd6f42b8 = Contact.PositionBWorld - BodyB.PoseCenterOfMass.Position;
			cb11ae9e27a8b0e95f5fcd44c7219b9d8.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -c875514674dce71fb1a6814fe2c63c57f, -Vector3F.Cross(c963dfc78572f5d40fe388983b1993dcc, c875514674dce71fb1a6814fe2c63c57f), c875514674dce71fb1a6814fe2c63c57f, Vector3F.Cross(c30e121ffd941742fbb472180bd6f42b8, c875514674dce71fb1a6814fe2c63c57f));
			ce8484c1eb41bd27c7d341c3339a7930b.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -ca1b39820bdcd223de9cec843cb9a69c1, -Vector3F.Cross(c963dfc78572f5d40fe388983b1993dcc, ca1b39820bdcd223de9cec843cb9a69c1), ca1b39820bdcd223de9cec843cb9a69c1, Vector3F.Cross(c30e121ffd941742fbb472180bd6f42b8, ca1b39820bdcd223de9cec843cb9a69c1));
			ccf2e328c6889968d331efe647fc9c047.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -cbbadb32b8d5af8e23b9b4d3d440028a5, -Vector3F.Cross(c963dfc78572f5d40fe388983b1993dcc, cbbadb32b8d5af8e23b9b4d3d440028a5), cbbadb32b8d5af8e23b9b4d3d440028a5, Vector3F.Cross(c30e121ffd941742fbb472180bd6f42b8, cbbadb32b8d5af8e23b9b4d3d440028a5));
		}
		else if (stackingTolerance < 1f)
		{
			stackingTolerance = 1f - stackingTolerance;
			Vector3F vector3F = Contact.PositionAWorld - BodyA.PoseCenterOfMass.Position;
			Vector3F vector3F2 = Contact.PositionBWorld - BodyB.PoseCenterOfMass.Position;
			if (!(vector3F.X * c963dfc78572f5d40fe388983b1993dcc.X + vector3F.Y * c963dfc78572f5d40fe388983b1993dcc.Y + vector3F.Z * c963dfc78572f5d40fe388983b1993dcc.Z < stackingTolerance * c7307432f075faddecbd4e53e09ed948a))
			{
				if (!(vector3F2.X * c30e121ffd941742fbb472180bd6f42b8.X + vector3F2.Y * c30e121ffd941742fbb472180bd6f42b8.Y + vector3F2.Z * c30e121ffd941742fbb472180bd6f42b8.Z < stackingTolerance * c963d2a9b7488a67926dcfc340ebca4d3))
				{
					goto IL_04d1;
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
			c963dfc78572f5d40fe388983b1993dcc = vector3F;
			c30e121ffd941742fbb472180bd6f42b8 = vector3F2;
			cb11ae9e27a8b0e95f5fcd44c7219b9d8.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -c875514674dce71fb1a6814fe2c63c57f, -Vector3F.Cross(c963dfc78572f5d40fe388983b1993dcc, c875514674dce71fb1a6814fe2c63c57f), c875514674dce71fb1a6814fe2c63c57f, Vector3F.Cross(c30e121ffd941742fbb472180bd6f42b8, c875514674dce71fb1a6814fe2c63c57f));
			ce8484c1eb41bd27c7d341c3339a7930b.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -ca1b39820bdcd223de9cec843cb9a69c1, -Vector3F.Cross(c963dfc78572f5d40fe388983b1993dcc, ca1b39820bdcd223de9cec843cb9a69c1), ca1b39820bdcd223de9cec843cb9a69c1, Vector3F.Cross(c30e121ffd941742fbb472180bd6f42b8, ca1b39820bdcd223de9cec843cb9a69c1));
			ccf2e328c6889968d331efe647fc9c047.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -cbbadb32b8d5af8e23b9b4d3d440028a5, -Vector3F.Cross(c963dfc78572f5d40fe388983b1993dcc, cbbadb32b8d5af8e23b9b4d3d440028a5), cbbadb32b8d5af8e23b9b4d3d440028a5, Vector3F.Cross(c30e121ffd941742fbb472180bd6f42b8, cbbadb32b8d5af8e23b9b4d3d440028a5));
		}
		goto IL_04d1;
		IL_04d1:
		Vector3F vector3F3;
		if (!cf480035e0690734609e96f1b31388c51)
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
			if (!c254f70f6c865eadf4e84b71e1f23e08a)
			{
				Vector3F cdb4f28852f8fdd848be747eebc3db = BodyA.cdb4f28852f8fdd848be747eebc3db719;
				Vector3F cdb4f28852f8fdd848be747eebc3db2 = BodyB.cdb4f28852f8fdd848be747eebc3db719;
				Vector3F ce4d650b898b2c22b831fd9383017176f = BodyA.ce4d650b898b2c22b831fd9383017176f;
				Vector3F ce4d650b898b2c22b831fd9383017176f2 = BodyB.ce4d650b898b2c22b831fd9383017176f;
				float num = cdb4f28852f8fdd848be747eebc3db.X + ce4d650b898b2c22b831fd9383017176f.Y * c963dfc78572f5d40fe388983b1993dcc.Z - ce4d650b898b2c22b831fd9383017176f.Z * c963dfc78572f5d40fe388983b1993dcc.Y;
				float num2 = cdb4f28852f8fdd848be747eebc3db.Y - ce4d650b898b2c22b831fd9383017176f.X * c963dfc78572f5d40fe388983b1993dcc.Z + ce4d650b898b2c22b831fd9383017176f.Z * c963dfc78572f5d40fe388983b1993dcc.X;
				float num3 = cdb4f28852f8fdd848be747eebc3db.Z + ce4d650b898b2c22b831fd9383017176f.X * c963dfc78572f5d40fe388983b1993dcc.Y - ce4d650b898b2c22b831fd9383017176f.Y * c963dfc78572f5d40fe388983b1993dcc.X;
				float num4 = cdb4f28852f8fdd848be747eebc3db2.X + ce4d650b898b2c22b831fd9383017176f2.Y * c30e121ffd941742fbb472180bd6f42b8.Z - ce4d650b898b2c22b831fd9383017176f2.Z * c30e121ffd941742fbb472180bd6f42b8.Y;
				float num5 = cdb4f28852f8fdd848be747eebc3db2.Y - ce4d650b898b2c22b831fd9383017176f2.X * c30e121ffd941742fbb472180bd6f42b8.Z + ce4d650b898b2c22b831fd9383017176f2.Z * c30e121ffd941742fbb472180bd6f42b8.X;
				float num6 = cdb4f28852f8fdd848be747eebc3db2.Z + ce4d650b898b2c22b831fd9383017176f2.X * c30e121ffd941742fbb472180bd6f42b8.Y - ce4d650b898b2c22b831fd9383017176f2.Y * c30e121ffd941742fbb472180bd6f42b8.X;
				vector3F3 = new Vector3F(num4 - num, num5 - num2, num6 - num3);
				goto IL_0677;
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
		vector3F3 = c9cf56ea482cbfc6991a9170d8c34c0b5;
		goto IL_0677;
		IL_0677:
		if (c3029b3eb4d165025b85c5363274411cc > 0f)
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
			float num7 = vector3F3.X * c875514674dce71fb1a6814fe2c63c57f.X + vector3F3.Y * c875514674dce71fb1a6814fe2c63c57f.Y + vector3F3.Z * c875514674dce71fb1a6814fe2c63c57f.Z;
			if (num7 < 0f)
			{
				cb11ae9e27a8b0e95f5fcd44c7219b9d8.ceef50b9f5f0910e0d3cdffa0ab9e67bf = (0f - c3029b3eb4d165025b85c5363274411cc) * num7;
			}
			else
			{
				cb11ae9e27a8b0e95f5fcd44c7219b9d8.ceef50b9f5f0910e0d3cdffa0ab9e67bf = 0f;
			}
		}
		else
		{
			cb11ae9e27a8b0e95f5fcd44c7219b9d8.ceef50b9f5f0910e0d3cdffa0ab9e67bf = 0f;
		}
		cb283829888a8db0c571899e633aa28b8 = 0f;
		c07d6b9586897ae4e5d738b4006894217 = 0f;
		if (Contact.PenetrationDepth > c729503850b1dde68cce1c94f7a27d104.Settings.Constraints.AllowedPenetration)
		{
			float num8 = Contact.PenetrationDepth - c729503850b1dde68cce1c94f7a27d104.Settings.Constraints.AllowedPenetration;
			float fixedTimeStep = c729503850b1dde68cce1c94f7a27d104.Settings.Timing.FixedTimeStep;
			float val = num8 / fixedTimeStep * ErrorReduction;
			val = Math.Min(val, c729503850b1dde68cce1c94f7a27d104.Settings.Constraints.MaxPenetrationCorrectionVelocity);
			c07d6b9586897ae4e5d738b4006894217 = (1f - c729503850b1dde68cce1c94f7a27d104.Settings.Constraints.BaumgarteRatio) * val;
			cb11ae9e27a8b0e95f5fcd44c7219b9d8.ceef50b9f5f0910e0d3cdffa0ab9e67bf = Math.Max(c729503850b1dde68cce1c94f7a27d104.Settings.Constraints.BaumgarteRatio * val, cb11ae9e27a8b0e95f5fcd44c7219b9d8.ceef50b9f5f0910e0d3cdffa0ab9e67bf);
		}
		cb11ae9e27a8b0e95f5fcd44c7219b9d8.c1b70fe54ea8e2058c44156c348d51664(BodyA, BodyB);
		ce8484c1eb41bd27c7d341c3339a7930b.cc01f881484f44c63c9c69258954b12e0 = 0f;
		ccf2e328c6889968d331efe647fc9c047.cc01f881484f44c63c9c69258954b12e0 = 0f;
		c3029b3eb4d165025b85c5363274411cc = 0f;
	}

	/// <summary>
	///       Called by the simulation to apply an impulse that satisfies the constraint.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if a constraint larger than <see cref="P:DigitalRune.Physics.Settings.ConstraintSettings.MinConstraintImpulse" />
	///       was applied.
	///       </returns>
	public bool ApplyImpulse()
	{
		Vector3F cdb4f28852f8fdd848be747eebc3db = BodyA.cdb4f28852f8fdd848be747eebc3db719;
		Vector3F ce4d650b898b2c22b831fd9383017176f = BodyA.ce4d650b898b2c22b831fd9383017176f;
		Vector3F cdb4f28852f8fdd848be747eebc3db2 = BodyB.cdb4f28852f8fdd848be747eebc3db719;
		Vector3F ce4d650b898b2c22b831fd9383017176f2 = BodyB.ce4d650b898b2c22b831fd9383017176f;
		Vector3F vector3F = default;
		if (cf480035e0690734609e96f1b31388c51 || c254f70f6c865eadf4e84b71e1f23e08a)
		{
			vector3F = c9cf56ea482cbfc6991a9170d8c34c0b5;
		}
		else
		{
			float num = cdb4f28852f8fdd848be747eebc3db.X + ce4d650b898b2c22b831fd9383017176f.Y * c963dfc78572f5d40fe388983b1993dcc.Z - ce4d650b898b2c22b831fd9383017176f.Z * c963dfc78572f5d40fe388983b1993dcc.Y;
			float num2 = cdb4f28852f8fdd848be747eebc3db.Y - ce4d650b898b2c22b831fd9383017176f.X * c963dfc78572f5d40fe388983b1993dcc.Z + ce4d650b898b2c22b831fd9383017176f.Z * c963dfc78572f5d40fe388983b1993dcc.X;
			float num3 = cdb4f28852f8fdd848be747eebc3db.Z + ce4d650b898b2c22b831fd9383017176f.X * c963dfc78572f5d40fe388983b1993dcc.Y - ce4d650b898b2c22b831fd9383017176f.Y * c963dfc78572f5d40fe388983b1993dcc.X;
			float num4 = cdb4f28852f8fdd848be747eebc3db2.X + ce4d650b898b2c22b831fd9383017176f2.Y * c30e121ffd941742fbb472180bd6f42b8.Z - ce4d650b898b2c22b831fd9383017176f2.Z * c30e121ffd941742fbb472180bd6f42b8.Y;
			float num5 = cdb4f28852f8fdd848be747eebc3db2.Y - ce4d650b898b2c22b831fd9383017176f2.X * c30e121ffd941742fbb472180bd6f42b8.Z + ce4d650b898b2c22b831fd9383017176f2.Z * c30e121ffd941742fbb472180bd6f42b8.X;
			float num6 = cdb4f28852f8fdd848be747eebc3db2.Z + ce4d650b898b2c22b831fd9383017176f2.X * c30e121ffd941742fbb472180bd6f42b8.Y - ce4d650b898b2c22b831fd9383017176f2.Y * c30e121ffd941742fbb472180bd6f42b8.X;
			vector3F.X = num4 - num;
			vector3F.Y = num5 - num2;
			vector3F.Z = num6 - num3;
		}
		float c3e548b3010de6dec5144f5f94f3c = vector3F.X * c875514674dce71fb1a6814fe2c63c57f.X + vector3F.Y * c875514674dce71fb1a6814fe2c63c57f.Y + vector3F.Z * c875514674dce71fb1a6814fe2c63c57f.Z;
		float num7 = cb11ae9e27a8b0e95f5fcd44c7219b9d8.c71a3eb12dd7debd5b7fb325bc5538195(BodyA, BodyB, c3e548b3010de6dec5144f5f94f3c);
		float cc01f881484f44c63c9c69258954b12e = cb11ae9e27a8b0e95f5fcd44c7219b9d8.cc01f881484f44c63c9c69258954b12e0;
		float c457b97b5f00c9ef9ae3c73d443489ea = c75e8d5254c8b573a529df00477042b97 * cc01f881484f44c63c9c69258954b12e;
		float c3e548b3010de6dec5144f5f94f3c2 = vector3F.X * ca1b39820bdcd223de9cec843cb9a69c1.X + vector3F.Y * ca1b39820bdcd223de9cec843cb9a69c1.Y + vector3F.Z * ca1b39820bdcd223de9cec843cb9a69c1.Z;
		float num8 = ce8484c1eb41bd27c7d341c3339a7930b.c96ddddae0ca90ef6d9688f0837082e1e(BodyA, BodyB, c3e548b3010de6dec5144f5f94f3c2, c457b97b5f00c9ef9ae3c73d443489ea, cc01f881484f44c63c9c69258954b12e, cd3d90de535d3e61ac1f4aa8982379a33);
		float c3e548b3010de6dec5144f5f94f3c3 = vector3F.X * cbbadb32b8d5af8e23b9b4d3d440028a5.X + vector3F.Y * cbbadb32b8d5af8e23b9b4d3d440028a5.Y + vector3F.Z * cbbadb32b8d5af8e23b9b4d3d440028a5.Z;
		float num9 = ccf2e328c6889968d331efe647fc9c047.c96ddddae0ca90ef6d9688f0837082e1e(BodyA, BodyB, c3e548b3010de6dec5144f5f94f3c3, c457b97b5f00c9ef9ae3c73d443489ea, cc01f881484f44c63c9c69258954b12e, cd3d90de535d3e61ac1f4aa8982379a33);
		c93efd5f1d1e0a28c4b524b761aff691c.cdb4f28852f8fdd848be747eebc3db719.X = cdb4f28852f8fdd848be747eebc3db.X + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c23d2f4d3dcf66e6f5d9511383a8e6c57.X * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c23d2f4d3dcf66e6f5d9511383a8e6c57.X * num8 + ccf2e328c6889968d331efe647fc9c047.c23d2f4d3dcf66e6f5d9511383a8e6c57.X * num9;
		c93efd5f1d1e0a28c4b524b761aff691c.cdb4f28852f8fdd848be747eebc3db719.Y = cdb4f28852f8fdd848be747eebc3db.Y + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c23d2f4d3dcf66e6f5d9511383a8e6c57.Y * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c23d2f4d3dcf66e6f5d9511383a8e6c57.Y * num8 + ccf2e328c6889968d331efe647fc9c047.c23d2f4d3dcf66e6f5d9511383a8e6c57.Y * num9;
		c93efd5f1d1e0a28c4b524b761aff691c.cdb4f28852f8fdd848be747eebc3db719.Z = cdb4f28852f8fdd848be747eebc3db.Z + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c23d2f4d3dcf66e6f5d9511383a8e6c57.Z * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c23d2f4d3dcf66e6f5d9511383a8e6c57.Z * num8 + ccf2e328c6889968d331efe647fc9c047.c23d2f4d3dcf66e6f5d9511383a8e6c57.Z * num9;
		c93efd5f1d1e0a28c4b524b761aff691c.ce4d650b898b2c22b831fd9383017176f.X = ce4d650b898b2c22b831fd9383017176f.X + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c552ff4ae5226505f81c0f72aed1f14ae.X * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c552ff4ae5226505f81c0f72aed1f14ae.X * num8 + ccf2e328c6889968d331efe647fc9c047.c552ff4ae5226505f81c0f72aed1f14ae.X * num9;
		c93efd5f1d1e0a28c4b524b761aff691c.ce4d650b898b2c22b831fd9383017176f.Y = ce4d650b898b2c22b831fd9383017176f.Y + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c552ff4ae5226505f81c0f72aed1f14ae.Y * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c552ff4ae5226505f81c0f72aed1f14ae.Y * num8 + ccf2e328c6889968d331efe647fc9c047.c552ff4ae5226505f81c0f72aed1f14ae.Y * num9;
		c93efd5f1d1e0a28c4b524b761aff691c.ce4d650b898b2c22b831fd9383017176f.Z = ce4d650b898b2c22b831fd9383017176f.Z + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c552ff4ae5226505f81c0f72aed1f14ae.Z * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c552ff4ae5226505f81c0f72aed1f14ae.Z * num8 + ccf2e328c6889968d331efe647fc9c047.c552ff4ae5226505f81c0f72aed1f14ae.Z * num9;
		c3fa55a45bff4b015fb13d00b2b744ed8.cdb4f28852f8fdd848be747eebc3db719.X = cdb4f28852f8fdd848be747eebc3db2.X + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c6f446c6f4c1942292d6992f38657defc.X * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c6f446c6f4c1942292d6992f38657defc.X * num8 + ccf2e328c6889968d331efe647fc9c047.c6f446c6f4c1942292d6992f38657defc.X * num9;
		c3fa55a45bff4b015fb13d00b2b744ed8.cdb4f28852f8fdd848be747eebc3db719.Y = cdb4f28852f8fdd848be747eebc3db2.Y + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c6f446c6f4c1942292d6992f38657defc.Y * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c6f446c6f4c1942292d6992f38657defc.Y * num8 + ccf2e328c6889968d331efe647fc9c047.c6f446c6f4c1942292d6992f38657defc.Y * num9;
		c3fa55a45bff4b015fb13d00b2b744ed8.cdb4f28852f8fdd848be747eebc3db719.Z = cdb4f28852f8fdd848be747eebc3db2.Z + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c6f446c6f4c1942292d6992f38657defc.Z * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c6f446c6f4c1942292d6992f38657defc.Z * num8 + ccf2e328c6889968d331efe647fc9c047.c6f446c6f4c1942292d6992f38657defc.Z * num9;
		c3fa55a45bff4b015fb13d00b2b744ed8.ce4d650b898b2c22b831fd9383017176f.X = ce4d650b898b2c22b831fd9383017176f2.X + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c4ca951ae9ce9b41d0ca851737577e34f.X * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c4ca951ae9ce9b41d0ca851737577e34f.X * num8 + ccf2e328c6889968d331efe647fc9c047.c4ca951ae9ce9b41d0ca851737577e34f.X * num9;
		c3fa55a45bff4b015fb13d00b2b744ed8.ce4d650b898b2c22b831fd9383017176f.Y = ce4d650b898b2c22b831fd9383017176f2.Y + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c4ca951ae9ce9b41d0ca851737577e34f.Y * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c4ca951ae9ce9b41d0ca851737577e34f.Y * num8 + ccf2e328c6889968d331efe647fc9c047.c4ca951ae9ce9b41d0ca851737577e34f.Y * num9;
		c3fa55a45bff4b015fb13d00b2b744ed8.ce4d650b898b2c22b831fd9383017176f.Z = ce4d650b898b2c22b831fd9383017176f2.Z + cb11ae9e27a8b0e95f5fcd44c7219b9d8.c4ca951ae9ce9b41d0ca851737577e34f.Z * num7 + ce8484c1eb41bd27c7d341c3339a7930b.c4ca951ae9ce9b41d0ca851737577e34f.Z * num8 + ccf2e328c6889968d331efe647fc9c047.c4ca951ae9ce9b41d0ca851737577e34f.Z * num9;
		if (c07d6b9586897ae4e5d738b4006894217 != 0f)
		{
			cb11ae9e27a8b0e95f5fcd44c7219b9d8.cdb50e8a5c312f4a81f567fc67c52442b(BodyA, BodyB, c07d6b9586897ae4e5d738b4006894217, ref cb283829888a8db0c571899e633aa28b8);
		}
		return Math.Abs(num7) > c261f1d5d6196838b200ba8f48c2b9aec;
	}

	[CompilerGenerated]
	private static ContactConstraint c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new ContactConstraint();
	}
}
