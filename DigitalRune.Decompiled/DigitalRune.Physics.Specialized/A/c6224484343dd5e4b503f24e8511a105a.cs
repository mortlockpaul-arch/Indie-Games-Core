using System;
using System.Collections.Generic;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Physics;
using DigitalRune.Physics.Constraints;
using DigitalRune.Physics.ForceEffects;
using DigitalRune.Physics.Materials;
using DigitalRune.Physics.Specialized;

namespace A;

internal class c6224484343dd5e4b503f24e8511a105a : ForceEffect
{
	private readonly KinematicCharacterController c23df679dffc07caa0587f3362beb5610;

	public c6224484343dd5e4b503f24e8511a105a(KinematicCharacterController c5b0da91872ad40f8b7f3b1ec0a644bf5)
	{
		if (c5b0da91872ad40f8b7f3b1ec0a644bf5 == null)
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
						__ldtoken(c6224484343dd5e4b503f24e8511a105a._002Ector(KinematicCharacterController));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(838));
				}
			}
		}
		c23df679dffc07caa0587f3362beb5610 = c5b0da91872ad40f8b7f3b1ec0a644bf5;
	}

	protected override void OnApply()
	{
		Vector3F upVector = c23df679dffc07caa0587f3362beb5610.UpVector;
		float height = c23df679dffc07caa0587f3362beb5610.Height;
		float num = c23df679dffc07caa0587f3362beb5610.Width / 2f;
		float num2 = (0f - height) / 2f + num;
		float fixedTimeStep = Simulation.Settings.Timing.FixedTimeStep;
		Vector3F impulseWorld = -upVector * (c23df679dffc07caa0587f3362beb5610.Gravity * c23df679dffc07caa0587f3362beb5610.Body.MassFrame.Mass * fixedTimeStep);
		int num3 = 0;
		Vector3F zero = Vector3F.Zero;
		using (IEnumerator<ContactConstraint> enumerator = Simulation.ContactConstraints.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ContactConstraint current = enumerator.Current;
				if (current.BodyA != c23df679dffc07caa0587f3362beb5610.Body && current.BodyB != c23df679dffc07caa0587f3362beb5610.Body)
				{
					continue;
				}
				Contact contact = current.Contact;
				bool flag = current.BodyB == c23df679dffc07caa0587f3362beb5610.Body;
				RigidBody rigidBody;
				if (!flag)
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
						__ldtoken(c6224484343dd5e4b503f24e8511a105a.OnApply());
					}
					rigidBody = current.BodyB;
				}
				else
				{
					rigidBody = current.BodyA;
				}
				RigidBody rigidBody2 = rigidBody;
				Vector3F vector3F;
				if (!flag)
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
					vector3F = contact.PositionALocal;
				}
				else
				{
					vector3F = contact.PositionBLocal;
				}
				Vector3F vector = vector3F;
				Vector3F vector3F2;
				if (!flag)
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
					vector3F2 = contact.Normal;
				}
				else
				{
					vector3F2 = -contact.Normal;
				}
				Vector3F vector3F3 = vector3F2;
				if (Vector3F.Dot(vector, upVector) < num2)
				{
					if (rigidBody2.MotionType == MotionType.Dynamic)
					{
						rigidBody2.ApplyImpulse(impulseWorld, contact.Position);
					}
					if (!c23df679dffc07caa0587f3362beb5610.cef056a544cf5e5c672c1b1a6e94eb1c8(-vector3F3))
					{
						continue;
					}
					num3++;
					Vector3F velocityOfWorldPoint = rigidBody2.GetVelocityOfWorldPoint(contact.Position);
					zero += velocityOfWorldPoint;
					IMaterial material = rigidBody2.Material;
					Vector3F positionLocal;
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
						positionLocal = contact.PositionBLocal;
					}
					else
					{
						positionLocal = contact.PositionALocal;
					}
					int featureIndex;
					if (!flag)
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
						featureIndex = contact.FeatureB;
					}
					else
					{
						featureIndex = contact.FeatureA;
					}
					MaterialProperties properties = material.GetProperties(rigidBody2, positionLocal, featureIndex);
					if (!properties.SupportsSurfaceMotion)
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
					Vector3F vector3F4 = rigidBody2.Pose.ToWorldDirection(properties.SurfaceMotion);
					zero += vector3F4;
				}
				else
				{
					if (rigidBody2.MotionType != MotionType.Dynamic)
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
					Vector3F velocityOfWorldPoint2 = rigidBody2.GetVelocityOfWorldPoint(contact.Position);
					Vector3F vector3F5 = c23df679dffc07caa0587f3362beb5610.c0147c1ef6134168e9e91f8597953e3f3 - velocityOfWorldPoint2;
					float num4 = Vector3F.Dot(vector3F5, vector3F3);
					if (!(num4 > 0f))
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
					Vector3F impulseWorld2 = rigidBody2.ComputeKMatrix(contact.Position).Inverse * vector3F5;
					float length = impulseWorld2.Length;
					float num5 = c23df679dffc07caa0587f3362beb5610.PushForce * fixedTimeStep;
					if (length > num5)
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
						if (length > 0f)
						{
							impulseWorld2.Length = num5;
						}
					}
					rigidBody2.ApplyImpulse(impulseWorld2, contact.Position);
				}
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					goto end_IL_0369;
				}
				continue;
				end_IL_0369:
				break;
			}
		}
		zero /= (float)num3;
		c23df679dffc07caa0587f3362beb5610.Body.LinearVelocity = zero;
	}
}
