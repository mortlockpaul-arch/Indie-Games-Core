using System;
using System.Globalization;
using A;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Mathematics.Interpolation;
using Microsoft.Xna.Framework;

namespace DigitalRune.Geometry;

/// <summary>
///       A pose defines the position and orientation of a shape in world space (or the parent 
///       coordinate space).
///       </summary>
[Serializable]
public struct Pose : IEquatable<Pose>
{
	/// <summary>
	///       A pose with no translation and no rotation.
	///       </summary>
	public static readonly Pose Identity = new Pose(Vector3F.Zero, Matrix33F.Identity);

	/// <summary>
	///       Gets or sets the position.
	///       </summary>
	public Vector3F Position;

	/// <summary>
	///       Gets or sets the orientation.
	///       </summary>
	public Matrix33F Orientation;

	/// <summary>
	///       Gets the inverse of this pose.
	///       </summary>
	public Pose Inverse
	{
		get
		{
			Pose result = this;
			result.Invert();
			return result;
		}
	}

	/// <summary>
	///       Gets a value indicating whether the position is not 0.
	///       </summary>
	public bool HasTranslation => !Position.IsNumericallyZero;

	/// <summary>
	///       Gets a value indicating whether the orientation is not the default rotation.
	///       </summary>
	public bool HasRotation
	{
		get
		{
			if (!Numeric.AreEqual(1f, Orientation.M00))
			{
				return true;
			}
			if (!Numeric.AreEqual(1f, Orientation.M11))
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Pose" /> class from position and orientation.
	///       </summary>
	/// <param name="position">The position.</param>
	/// <param name="orientation">The orientation.</param>
	public Pose(Vector3F position, Matrix33F orientation)
	{
		Position = position;
		Orientation = orientation;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Pose" /> class from position.
	///       </summary>
	/// <param name="position">The position.</param>
	public Pose(Vector3F position)
	{
		Position = position;
		Orientation = Matrix33F.Identity;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Pose" /> class from orientation.
	///       </summary>
	/// <param name="orientation">The orientation.</param>
	public Pose(Matrix33F orientation)
	{
		Position = Vector3F.Zero;
		Orientation = orientation;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Pose" /> class from position and orientation.
	///       </summary>
	/// <param name="position">The position.</param>
	/// <param name="orientation">The orientation.</param>
	public Pose(Vector3F position, QuaternionF orientation)
	{
		Position = position;
		Orientation = orientation.ToRotationMatrix33();
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Pose" /> class from orientation.
	///       </summary>
	/// <param name="orientation">The orientation.</param>
	public Pose(QuaternionF orientation)
	{
		Position = Vector3F.Zero;
		Orientation = orientation.ToRotationMatrix33();
	}

	/// <summary>
	///       Indicates whether the current object is equal to another object of the same type.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(Pose other)
	{
		return this == other;
	}

	/// <summary>
	///       Inverts the pose.
	///       </summary>
	public void Invert()
	{
		Orientation.Transpose();
		Position = Orientation * -Position;
	}

	/// <summary>
	///       Converts a direction vector from local space to world space (or the parent space for nested 
	///       coordinate spaces).
	///       </summary>
	/// <param name="localDirection">The local direction.</param>
	/// <returns>
	///       The direction in world space (or the parent space for nested coordinate spaces).
	///       </returns>
	public Vector3F ToWorldDirection(Vector3F localDirection)
	{
		Vector3F result = default;
		result.X = Orientation.M00 * localDirection.X + Orientation.M01 * localDirection.Y + Orientation.M02 * localDirection.Z;
		result.Y = Orientation.M10 * localDirection.X + Orientation.M11 * localDirection.Y + Orientation.M12 * localDirection.Z;
		result.Z = Orientation.M20 * localDirection.X + Orientation.M21 * localDirection.Y + Orientation.M22 * localDirection.Z;
		return result;
	}

	/// <summary>
	///       Converts a direction vector from world space (or the parent space for nested coordinate 
	///       spaces) to local space.
	///       </summary>
	/// <param name="worldDirection">
	///       The direction vector in world space (or the parent space for nested coordinate spaces).
	///       </param>
	/// <returns>The direction in local space.</returns>
	public Vector3F ToLocalDirection(Vector3F worldDirection)
	{
		Vector3F result = default;
		result.X = Orientation.M00 * worldDirection.X + Orientation.M10 * worldDirection.Y + Orientation.M20 * worldDirection.Z;
		result.Y = Orientation.M01 * worldDirection.X + Orientation.M11 * worldDirection.Y + Orientation.M21 * worldDirection.Z;
		result.Z = Orientation.M02 * worldDirection.X + Orientation.M12 * worldDirection.Y + Orientation.M22 * worldDirection.Z;
		return result;
	}

	/// <summary>
	///       Converts a position vector from local space to world space (or the parent space for nested 
	///       coordinate spaces).
	///       </summary>
	/// <param name="localPosition">The local position.</param>
	/// <returns>
	///       The position in world space (or the parent space for nested coordinate spaces).
	///       </returns>
	public Vector3F ToWorldPosition(Vector3F localPosition)
	{
		Vector3F result = default;
		result.X = Orientation.M00 * localPosition.X + Orientation.M01 * localPosition.Y + Orientation.M02 * localPosition.Z + Position.X;
		result.Y = Orientation.M10 * localPosition.X + Orientation.M11 * localPosition.Y + Orientation.M12 * localPosition.Z + Position.Y;
		result.Z = Orientation.M20 * localPosition.X + Orientation.M21 * localPosition.Y + Orientation.M22 * localPosition.Z + Position.Z;
		return result;
	}

	/// <summary>
	///       Converts a direction vector from world space (or the parent space for nested coordinate 
	///       spaces) to local space.
	///       </summary>
	/// <param name="worldPosition">
	///       The position vector in world space (or the parent space for nested coordinate spaces).
	///       </param>
	/// <returns>The position in local space.</returns>
	public Vector3F ToLocalPosition(Vector3F worldPosition)
	{
		Vector3F vector3F = default;
		vector3F.X = worldPosition.X - Position.X;
		vector3F.Y = worldPosition.Y - Position.Y;
		vector3F.Z = worldPosition.Z - Position.Z;
		Vector3F result = default;
		result.X = Orientation.M00 * vector3F.X + Orientation.M10 * vector3F.Y + Orientation.M20 * vector3F.Z;
		result.Y = Orientation.M01 * vector3F.X + Orientation.M11 * vector3F.Y + Orientation.M21 * vector3F.Z;
		result.Z = Orientation.M02 * vector3F.X + Orientation.M12 * vector3F.Y + Orientation.M22 * vector3F.Z;
		return result;
	}

	/// <summary>
	///       Creates a <see cref="T:DigitalRune.Geometry.Pose" /> from a matrix that contains a translation and a rotation.
	///       </summary>
	/// <param name="poseMatrix">The pose matrix.</param>
	/// <returns>A pose that represents the same transformation as the 4x4-matrix.</returns>
	public static Pose FromMatrix(Matrix44F poseMatrix)
	{
		Vector4F column = poseMatrix.GetColumn(3);
		return new Pose(new Vector3F(column.X, column.Y, column.Z), poseMatrix.Minor);
	}

	/// <summary>
	///       Converts this pose to a 4x4 transformation matrix.
	///       </summary>
	/// <returns>
	///       A 4x4-matrix that represents the same transformation as the pose.
	///       </returns>
	public Matrix44F ToMatrix44F()
	{
		return this;
	}

	/// <summary>
	///       Creates a <see cref="T:DigitalRune.Geometry.Pose" /> from a <see cref="T:Microsoft.Xna.Framework.Matrix" /> (XNA Framework) that contains a 
	///       translation and a rotation.
	///       </summary>
	/// <param name="poseMatrix">The pose matrix.</param>
	/// <returns>A pose that represents the same transformation as the 4x4-matrix.</returns>
	public static Pose FromMatrix(Matrix poseMatrix)
	{
		Vector3 translation = poseMatrix.Translation;
		return new Pose(new Vector3F(translation.X, translation.Y, translation.Z), new Matrix33F(poseMatrix.M11, poseMatrix.M21, poseMatrix.M31, poseMatrix.M12, poseMatrix.M22, poseMatrix.M32, poseMatrix.M13, poseMatrix.M23, poseMatrix.M33));
	}

	/// <summary>
	///       Converts a pose to a 4x4 transformation matrix (XNA Framework).
	///       </summary>
	/// <returns>
	///       A 4x4-matrix that represents the same transformation as the pose.
	///       </returns>
	public Matrix ToXna()
	{
		Matrix33F orientation = Orientation;
		return new Matrix(orientation.M00, orientation.M10, orientation.M20, 0f, orientation.M01, orientation.M11, orientation.M21, 0f, orientation.M02, orientation.M12, orientation.M22, 0f, Position.X, Position.Y, Position.Z, 1f);
	}

	/// <summary>
	///       Interpolates two poses.
	///       </summary>
	/// <param name="startPose">The start pose.</param>
	/// <param name="endPose">The end pose.</param>
	/// <param name="parameter">
	///       The interpolation parameter. If the value is 0, the <paramref name="startPose" /> is
	///       returned. If the value is 1, the <paramref name="endPose" /> is returned. For values between
	///       0 and 1 an interpolated pose is returned.
	///       </param>
	/// <returns>
	///       An interpolated pose.
	///       </returns>
	public static Pose Interpolate(Pose startPose, Pose endPose, float parameter)
	{
		Vector3F position = startPose.Position * (1f - parameter) + endPose.Position * parameter;
		QuaternionF orientation = InterpolationHelper.Slerp(QuaternionF.CreateRotation(startPose.Orientation), QuaternionF.CreateRotation(endPose.Orientation), parameter);
		orientation.Normalize();
		return new Pose(position, orientation);
	}

	/// <summary>
	///       Determines whether the specified matrix is a valid pose matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>
	///   <see langword="true" /> if the specified matrix is a valid pose matrix; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool IsValid(Matrix44F matrix)
	{
		Vector4F vector = matrix * Vector4F.UnitX;
		Vector4F vector4F = matrix * Vector4F.UnitY;
		Vector4F vector2 = matrix * Vector4F.UnitZ;
		if (Numeric.AreEqual(vector.LengthSquared, 1f))
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
				__ldtoken(Pose.IsValid(Matrix44F));
			}
			if (Numeric.AreEqual(vector4F.LengthSquared, 1f) && Numeric.AreEqual(vector2.LengthSquared, 1f) && Numeric.IsZero(Vector4F.Dot(vector, vector4F)))
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
				if (Numeric.IsZero(Vector4F.Dot(vector4F, vector2)))
				{
					return Numeric.IsZero(Vector4F.Dot(vector, vector2));
				}
			}
		}
		return false;
	}

	/// <summary>
	///       Returns the hash code for this instance.
	///       </summary>
	/// <returns>
	///       A 32-bit signed integer that is the hash code for this instance.
	///       </returns>
	public override int GetHashCode()
	{
		int hashCode = Position.GetHashCode();
		return (hashCode * 397) ^ Orientation.GetHashCode();
	}

	/// <summary>
	///       Indicates whether this instance and a specified object are equal.
	///       </summary>
	/// <param name="obj">Another object to compare to.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and
	///       represent the same value; otherwise, <see langword="false" />.
	///       </returns>
	public override bool Equals(object obj)
	{
		if (obj is Pose)
		{
			return this == (Pose)obj;
		}
		return false;
	}

	/// <summary>
	///       Returns the string representation of this pose.
	///       </summary>
	/// <returns>
	///       The string representation of this pose.
	///       </returns>
	public override string ToString()
	{
		return ToString(CultureInfo.CurrentCulture);
	}

	/// <summary>
	///       Returns the string representation of this pose using the specified culture-specific format
	///       information.
	///       </summary>
	/// <param name="provider">
	///       An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information
	///       </param>
	/// <returns>
	///       The string representation of this pose.
	///       </returns>
	public string ToString(IFormatProvider provider)
	{
		return string.Format(provider, c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4444), new object[2] { Position, Orientation });
	}

	/// <summary>
	///       Multiplies two poses.
	///       </summary>
	/// <param name="p1">The first pose p1.</param>
	/// <param name="p2">The second pose p2.</param>
	/// <returns>The product of p1 and p2: p1 * p2.</returns>
	public static Pose operator *(Pose p1, Pose p2)
	{
		return new Pose(p1.Orientation * p2.Position + p1.Position, p1.Orientation * p2.Orientation);
	}

	/// <summary>
	///       Multiplies two poses.
	///       </summary>
	/// <param name="p1">The first pose p1.</param>
	/// <param name="p2">The second pose p2.</param>
	/// <returns>The multiplication of p1 and p2: p1 * p2.</returns>
	public static Pose Multiply(Pose p1, Pose p2)
	{
		return new Pose(p1.Orientation * p2.Position + p1.Position, p1.Orientation * p2.Orientation);
	}

	/// <summary>
	///       Multiplies the pose with a vector.
	///       </summary>
	/// <param name="pose">The pose.</param>
	/// <param name="vector">The vector.</param>
	/// <returns>The transformed vector.</returns>
	public static Vector4F operator *(Pose pose, Vector4F vector)
	{
		return pose.ToMatrix44F() * vector;
	}

	/// <summary>
	///       Multiplies the pose with a vector.
	///       </summary>
	/// <param name="pose">The pose.</param>
	/// <param name="vector">The vector.</param>
	/// <returns>The transformed vector.</returns>
	public static Vector4F Multiply(Pose pose, Vector4F vector)
	{
		return pose.ToMatrix44F() * vector;
	}

	/// <summary>
	///       Compares two <see cref="T:DigitalRune.Geometry.Pose" />s to determine whether they are the same.
	///       </summary>
	/// <param name="pose1">The first pose.</param>
	/// <param name="pose2">The second pose.</param>
	/// <returns>
	///   <see langword="true" /> if the poses are equal; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator ==(Pose pose1, Pose pose2)
	{
		if (pose1.Position == pose2.Position)
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
						__ldtoken(Pose.op_Equality(Pose, Pose));
					}
					return pose1.Orientation == pose2.Orientation;
				}
			}
		}
		return false;
	}

	/// <summary>
	///       Compares two <see cref="T:DigitalRune.Geometry.Pose" />s to determine whether they are different.
	///       </summary>
	/// <param name="pose1">The first pose.</param>
	/// <param name="pose2">The second pose.</param>
	/// <returns>
	///   <see langword="true" /> if the poses are different; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator !=(Pose pose1, Pose pose2)
	{
		return !(pose1 == pose2);
	}

	/// <summary>
	///       Converts a pose to a 4x4 transformation matrix.
	///       </summary>
	/// <param name="pose">The pose.</param>
	/// <returns>
	///       A 4x4-matrix that represents the same transformation as the pose.
	///       </returns>
	public static implicit operator Matrix44F(Pose pose)
	{
		Vector3F position = pose.Position;
		Matrix33F orientation = pose.Orientation;
		return new Matrix44F(orientation.M00, orientation.M01, orientation.M02, position.X, orientation.M10, orientation.M11, orientation.M12, position.Y, orientation.M20, orientation.M21, orientation.M22, position.Z, 0f, 0f, 0f, 1f);
	}

	/// <summary>
	///       Converts a pose to a 4x4 transformation matrix (XNA Framework).
	///       </summary>
	/// <param name="pose">The pose.</param>
	/// <returns>
	///       A 4x4-matrix that represents the same transformation as the pose.
	///       </returns>
	public static implicit operator Matrix(Pose pose)
	{
		Vector3F position = pose.Position;
		Matrix33F orientation = pose.Orientation;
		return new Matrix(orientation.M00, orientation.M10, orientation.M20, 0f, orientation.M01, orientation.M11, orientation.M21, 0f, orientation.M02, orientation.M12, orientation.M22, 0f, position.X, position.Y, position.Z, 1f);
	}
}
