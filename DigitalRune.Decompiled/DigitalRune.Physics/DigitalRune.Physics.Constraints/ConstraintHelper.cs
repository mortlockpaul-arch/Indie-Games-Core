using System;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Provides constraint-related helper methods.
///       </summary>
public static class ConstraintHelper
{
	/// <summary>
	///       Computes the error reduction parameter for a given spring and damping constant.
	///       </summary>
	/// <param name="deltaTime">The time step size.</param>
	/// <param name="springConstant">The spring constant.</param>
	/// <param name="dampingConstant">The damping constant.</param>
	/// <returns>
	///       The error reduction parameter that lets the constraint behave like a damped spring with
	///       the given parameters.
	///       </returns>
	public static float ComputeErrorReduction(float deltaTime, float springConstant, float dampingConstant)
	{
		float num = deltaTime * springConstant + dampingConstant;
		if (!Numeric.IsZero(num))
		{
			if (!Numeric.IsNaN(num))
			{
				return deltaTime * springConstant / num;
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
				__ldtoken(ConstraintHelper.ComputeErrorReduction(float, float, float));
			}
		}
		return 0f;
	}

	/// <summary>
	///       Computes the softness parameter for a given spring and damping constant.
	///       </summary>
	/// <param name="deltaTime">The time step size.</param>
	/// <param name="springConstant">The spring constant.</param>
	/// <param name="dampingConstant">The damping constant.</param>
	/// <returns>
	///       The softness parameter that lets the constraint behave like a damped spring with
	///       the given parameters.
	///       </returns>
	public static float ComputeSoftness(float deltaTime, float springConstant, float dampingConstant)
	{
		float num = deltaTime * springConstant + dampingConstant;
		if (!Numeric.IsZero(num))
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
				__ldtoken(ConstraintHelper.ComputeSoftness(float, float, float));
			}
			if (!Numeric.IsNaN(num))
			{
				return 1f / num;
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
		return 1f;
	}

	/// <summary>
	///       Computes the spring constant from error reduction and softness parameters.
	///       </summary>
	/// <param name="deltaTime">The time step size.</param>
	/// <param name="errorReduction">The error reduction parameter.</param>
	/// <param name="softness">The softness parameter.</param>
	/// <returns>The spring constant.</returns>
	public static float ComputeSpringConstant(float deltaTime, float errorReduction, float softness)
	{
		return errorReduction / (softness * deltaTime);
	}

	/// <summary>
	///       Computes the damping constant from error reduction and softness parameters.
	///       </summary>
	/// <param name="deltaTime">The time step size.</param>
	/// <param name="errorReduction">The error reduction parameter.</param>
	/// <param name="softness">The softness parameter.</param>
	/// <returns>The damping constant.</returns>
	public static float ComputeDampingConstant(float deltaTime, float errorReduction, float softness)
	{
		return 1f / softness - errorReduction / softness;
	}

	/// <summary>
	///       Computes the K matrix needed by sequential impulse-based methods.
	///       </summary>
	/// <param name="body">The body.</param>
	/// <param name="positionWorld">The constraint anchor position in world space.</param>
	/// <returns>
	///       The K matrix.
	///       </returns>
	public static Matrix33F ComputeKMatrix(this RigidBody body, Vector3F positionWorld)
	{
		if (body.MotionType != MotionType.Dynamic)
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
						__ldtoken(ConstraintHelper.ComputeKMatrix(RigidBody, Vector3F));
					}
					return Matrix33F.Zero;
				}
			}
		}
		Matrix33F matrix33F = (positionWorld - body.PoseCenterOfMass.Position).ToCrossProductMatrix();
		Matrix33F matrix33F2 = Matrix33F.CreateScale(body.MassInverse);
		return matrix33F2 - matrix33F * body.InertiaInverseWorld * matrix33F;
	}

	/// <summary>
	///       Applies an impulse so that the velocity of point on the body is changed.
	///       </summary>
	/// <param name="body">The body.</param>
	/// <param name="positionWorld">The position on the body in world space.</param>
	/// <param name="velocityWorld">The target velocity of the point in world space.</param>
	public static void SetVelocityOfWorldPoint(this RigidBody body, Vector3F positionWorld, Vector3F velocityWorld)
	{
		Vector3F velocityOfWorldPoint = body.GetVelocityOfWorldPoint(positionWorld);
		Vector3F impulseWorld = body.ComputeKMatrix(positionWorld).Inverse * (velocityWorld - velocityOfWorldPoint);
		body.ApplyImpulse(impulseWorld, positionWorld);
	}

	/// <summary>
	///       Gets the Euler angles for the given rotation.
	///       </summary>
	/// <param name="rotation">The rotation.</param>
	/// <returns>
	///       A vector with the three Euler angles in radians: (angle0, angle1, angle2).
	///       </returns>
	public static Vector3F GetEulerAngles(Matrix33F rotation)
	{
		Vector3F result = default;
		float m = rotation.M02;
		if (m < 1f)
		{
			if (m > -1f)
			{
				result.X = (float)Math.Atan2(0f - rotation.M12, rotation.M22);
				result.Y = (float)Math.Asin(m);
				result.Z = (float)Math.Atan2(0f - rotation.M01, rotation.M00);
			}
			else
			{
				result.X = 0f - (float)Math.Atan2(rotation.M10, rotation.M11);
				result.Y = -(float)Math.PI / 2f;
				result.Z = 0f;
			}
		}
		else
		{
			result.X = (float)Math.Atan2(rotation.M10, rotation.M11);
			result.Y = (float)Math.PI / 2f;
			result.Z = 0f;
		}
		return result;
	}
}
