using System;
using System.Runtime.CompilerServices;
using DigitalRune.Geometry;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Physics;
using DigitalRune.Physics.ForceEffects;
using DigitalRune.Physics.Specialized;

namespace A;

internal class c474bd0c7094acd3af328887ce9866162 : ForceEffect
{
	[CompilerGenerated]
	private Vehicle c9bf29c5f604a72feb31ef7e915d3e4af;

	public Vehicle Vehicle
	{
		[CompilerGenerated]
		get
		{
			return c9bf29c5f604a72feb31ef7e915d3e4af;
		}
		[CompilerGenerated]
		private set
		{
			c9bf29c5f604a72feb31ef7e915d3e4af = value;
		}
	}

	internal c474bd0c7094acd3af328887ce9866162(Vehicle c8c2c420ce1a795e21409222e581745bb)
	{
		Vehicle = c8c2c420ce1a795e21409222e581745bb;
	}

	protected override void OnApply()
	{
		RigidBody chassis = Vehicle.Chassis;
		Pose pose = chassis.Pose;
		float mass = chassis.MassFrame.Mass;
		Vector3F vector3F = pose.ToWorldDirection(Vector3F.UnitY);
		float fixedTimeStep = Simulation.Settings.Timing.FixedTimeStep;
		int count = Vehicle.Wheels.Count;
		for (int i = 0; i < count; i++)
		{
			Wheel wheel = Vehicle.Wheels[i];
			wheel.PreviousSuspensionLength = wheel.SuspensionLength;
			wheel.c12c24226dfa6f48adef0c6b19cc54b9c();
			float num = Vector3F.Dot(wheel.GroundNormal, vector3F);
			if (wheel.HasGroundContact)
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
					__ldtoken(c474bd0c7094acd3af328887ce9866162.OnApply());
				}
				if (!Numeric.IsLessOrEqual(num, 0f))
				{
					float num2 = wheel.SuspensionStiffness * mass * (wheel.SuspensionRestLength - wheel.SuspensionLength) * num;
					float num3 = (wheel.PreviousSuspensionLength - wheel.SuspensionLength) / fixedTimeStep;
					float num4 = ((num3 > 0f) ? wheel.SuspensionCompressionDamping : wheel.SuspensionRelaxationDamping);
					float num5 = num4 * mass * num3;
					float num6 = MathHelper.Clamp(num2 + num5, 0f, wheel.MaxSuspensionForce);
					ForceEffect.AddForce(chassis, num6 * vector3F, wheel.GroundPosition);
					ForceEffect.AddForce(wheel.TouchedBody, (0f - num6) * vector3F, wheel.GroundPosition);
					Vector3F vector = pose.ToWorldDirection(Matrix33F.CreateRotationY(wheel.SteeringAngle) * Vector3F.UnitX);
					Vector3F normalized = Vector3F.Cross(wheel.GroundNormal, vector).Normalized;
					Vector3F normalized2 = Vector3F.Cross(normalized, wheel.GroundNormal).Normalized;
					float num7 = cadb9c02cfbc0b7fa4a673776b41da6ff(wheel, normalized2) / fixedTimeStep;
					num7 /= (float)count;
					float num8;
					if (Math.Abs(wheel.MotorForce) > wheel.BrakeForce)
					{
						num8 = wheel.MotorForce - wheel.BrakeForce;
					}
					else
					{
						num8 = cadb9c02cfbc0b7fa4a673776b41da6ff(wheel, normalized) / fixedTimeStep;
						float val = wheel.BrakeForce - Math.Abs(wheel.MotorForce);
						float num9 = Math.Max(wheel.RollingFrictionForce, val);
						num8 = MathHelper.Clamp(num8, 0f - num9, num9);
					}
					float num10 = wheel.Friction * num6;
					float length = (num8 * normalized + num7 * normalized2).Length;
					if (length > num10)
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
						float num11 = length - num10;
						float num12 = num11 * fixedTimeStep / mass;
						wheel.SkidEnergy = num10 * num12 * fixedTimeStep;
						float num13 = num10 / length;
						num8 *= num13;
						num7 *= num13;
					}
					else
					{
						wheel.SkidEnergy = 0f;
					}
					ForceEffect.AddForce(chassis, num8 * normalized, wheel.GroundPosition);
					ForceEffect.AddForce(wheel.TouchedBody, (0f - num8) * normalized, wheel.GroundPosition);
					Vector3F positionWorld = wheel.GroundPosition + wheel.RollReduction * (wheel.SuspensionLength + wheel.Radius) * vector3F;
					ForceEffect.AddForce(chassis, num7 * normalized2, positionWorld);
					ForceEffect.AddForce(wheel.TouchedBody, (0f - num7) * normalized2, positionWorld);
					Vector3F vector2 = chassis.GetVelocityOfWorldPoint(wheel.GroundPosition) - wheel.TouchedBody.GetVelocityOfWorldPoint(wheel.GroundPosition);
					float num14 = Vector3F.Dot(vector2, normalized);
					wheel.AngularVelocity = num14 / wheel.Radius;
					wheel.RotationAngle += wheel.AngularVelocity * fixedTimeStep;
					continue;
				}
			}
			wheel.AngularVelocity *= 0.99f;
			wheel.RotationAngle += wheel.AngularVelocity * fixedTimeStep;
		}
	}

	private float cadb9c02cfbc0b7fa4a673776b41da6ff(Wheel c84c199250fb7872e2c245fca2a5e1faa, Vector3F c75c28b357dedcad9a44abd406f2361f3)
	{
		RigidBody chassis = Vehicle.Chassis;
		RigidBody touchedBody = c84c199250fb7872e2c245fca2a5e1faa.TouchedBody;
		Vector3F vector = c84c199250fb7872e2c245fca2a5e1faa.GroundPosition - chassis.PoseCenterOfMass.Position;
		Vector3F vector2 = c84c199250fb7872e2c245fca2a5e1faa.GroundPosition - touchedBody.PoseCenterOfMass.Position;
		Vector3F vector3F = -c75c28b357dedcad9a44abd406f2361f3;
		Vector3F vector3F2 = -Vector3F.Cross(vector, c75c28b357dedcad9a44abd406f2361f3);
		Vector3F vector3F3 = Vector3F.Cross(vector2, c75c28b357dedcad9a44abd406f2361f3);
		Vector3F vector3 = chassis.MassInverse * vector3F;
		Vector3F vector4 = chassis.InertiaInverseWorld * vector3F2;
		Vector3F vector5 = touchedBody.MassInverse * c75c28b357dedcad9a44abd406f2361f3;
		Vector3F vector6 = touchedBody.InertiaInverseWorld * vector3F3;
		float num = Vector3F.Dot(vector3F, vector3) + Vector3F.Dot(vector3F2, vector4) + Vector3F.Dot(c75c28b357dedcad9a44abd406f2361f3, vector5) + Vector3F.Dot(vector3F3, vector6);
		float num2 = 1f / num;
		float num3 = Vector3F.Dot(vector3F, chassis.LinearVelocity) + Vector3F.Dot(vector3F2, chassis.AngularVelocity) + Vector3F.Dot(c75c28b357dedcad9a44abd406f2361f3, touchedBody.LinearVelocity) + Vector3F.Dot(vector3F3, touchedBody.AngularVelocity);
		return num2 * num3;
	}
}
