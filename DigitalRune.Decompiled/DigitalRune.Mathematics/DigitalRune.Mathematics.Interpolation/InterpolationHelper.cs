using System;
using System.Collections.Generic;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Provides methods for interpolation and approximation of values.
///       </summary>
public static class InterpolationHelper
{
	/// <summary>
	///       Performs a linear interpolation between two values (single-precision).
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The linear interpolation of two values.</returns>
	public static float Lerp(float source, float target, float parameter)
	{
		return source + (target - source) * parameter;
	}

	/// <summary>
	///       Performs a linear interpolation between two values (double-precision).
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The linear interpolation of two values.</returns>
	public static double Lerp(double source, double target, double parameter)
	{
		return source + (target - source) * parameter;
	}

	/// <summary>
	///       Performs a linear interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <param name="source">The source vector.</param>
	/// <param name="target">The target vector.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The linear interpolation of two vectors.</returns>
	public static Vector2F Lerp(Vector2F source, Vector2F target, float parameter)
	{
		source.X += (target.X - source.X) * parameter;
		source.Y += (target.Y - source.Y) * parameter;
		return source;
	}

	/// <summary>
	///       Performs a linear interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector2D" />.
	///       </summary>
	/// <param name="source">The source vector.</param>
	/// <param name="target">The target vector.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The linear interpolation of two vectors.</returns>
	public static Vector2D Lerp(Vector2D source, Vector2D target, double parameter)
	{
		source.X += (target.X - source.X) * parameter;
		source.Y += (target.Y - source.Y) * parameter;
		return source;
	}

	/// <summary>
	///       Performs a linear interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />.
	///       </summary>
	/// <param name="source">The source vector.</param>
	/// <param name="target">The target vector.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The linear interpolation of two vectors.</returns>
	public static Vector3F Lerp(Vector3F source, Vector3F target, float parameter)
	{
		source.X += (target.X - source.X) * parameter;
		source.Y += (target.Y - source.Y) * parameter;
		source.Z += (target.Z - source.Z) * parameter;
		return source;
	}

	/// <summary>
	///       Performs a linear interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector3D" />.
	///       </summary>
	/// <param name="source">The source vector.</param>
	/// <param name="target">The target vector.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The linear interpolation of two vectors.</returns>
	public static Vector3D Lerp(Vector3D source, Vector3D target, double parameter)
	{
		source.X += (target.X - source.X) * parameter;
		source.Y += (target.Y - source.Y) * parameter;
		source.Z += (target.Z - source.Z) * parameter;
		return source;
	}

	/// <summary>
	///       Performs a linear interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="source">The source vector.</param>
	/// <param name="target">The target vector.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The linear interpolation of two vectors.</returns>
	public static Vector4F Lerp(Vector4F source, Vector4F target, float parameter)
	{
		source.X += (target.X - source.X) * parameter;
		source.Y += (target.Y - source.Y) * parameter;
		source.Z += (target.Z - source.Z) * parameter;
		source.W += (target.W - source.W) * parameter;
		return source;
	}

	/// <summary>
	///       Performs a linear interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector4D" />.
	///       </summary>
	/// <param name="source">The source vector.</param>
	/// <param name="target">The target vector.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The linear interpolation of two vectors.</returns>
	public static Vector4D Lerp(Vector4D source, Vector4D target, double parameter)
	{
		source.X += (target.X - source.X) * parameter;
		source.Y += (target.Y - source.Y) * parameter;
		source.Z += (target.Z - source.Z) * parameter;
		source.W += (target.W - source.W) * parameter;
		return source;
	}

	/// <summary>
	///       Performs a linear interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" />.
	///       </summary>
	/// <param name="source">The source vector.</param>
	/// <param name="target">The target vector.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The linear interpolation of two vectors.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="source" /> or <paramref name="target" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static VectorF Lerp(VectorF source, VectorF target, float parameter)
	{
		if (source == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10722));
		}
		if (target == null)
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
						__ldtoken(InterpolationHelper.Lerp(VectorF, VectorF, float));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5541));
				}
			}
		}
		if (source.NumberOfElements != target.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10735));
		}
		return source * (1f - parameter) + target * parameter;
	}

	/// <summary>
	///       Performs a linear interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" />.
	///       </summary>
	/// <param name="source">The source vector.</param>
	/// <param name="target">The target vector.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The linear interpolation of two vectors.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="source" /> or <paramref name="target" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static VectorD Lerp(VectorD source, VectorD target, double parameter)
	{
		if (source == null)
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
						__ldtoken(InterpolationHelper.Lerp(VectorD, VectorD, double));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10722));
				}
			}
		}
		if (target == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5541));
		}
		if (source.NumberOfElements != target.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10735));
		}
		return source * (1.0 - parameter) + target * parameter;
	}

	/// <summary>
	///       Performs a linear interpolation of two <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" />.
	///       </summary>
	/// <param name="source">The start quaternion.</param>
	/// <param name="target">The end quaternion.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.</param>
	/// <returns>The linear interpolation of the two quaternions</returns>
	public static QuaternionF Lerp(QuaternionF source, QuaternionF target, float parameter)
	{
		float num = QuaternionF.Dot(source, target);
		if (num < 0f)
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
				__ldtoken(InterpolationHelper.Lerp(QuaternionF, QuaternionF, float));
			}
			target.W = 0f - target.W;
			target.X = 0f - target.X;
			target.Y = 0f - target.Y;
			target.Z = 0f - target.Z;
		}
		QuaternionF result = default;
		result.W = source.W + (target.W - source.W) * parameter;
		result.X = source.X + (target.X - source.X) * parameter;
		result.Y = source.Y + (target.Y - source.Y) * parameter;
		result.Z = source.Z + (target.Z - source.Z) * parameter;
		result.Normalize();
		return result;
	}

	/// <summary>
	///       Performs a linear interpolation of two <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" />.
	///       </summary>
	/// <param name="source">The start quaternion.</param>
	/// <param name="target">The end quaternion.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.</param>
	/// <returns>The linear interpolation of the two quaternions</returns>
	public static QuaternionD Lerp(QuaternionD source, QuaternionD target, double parameter)
	{
		double num = QuaternionD.Dot(source, target);
		if (num < 0.0)
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
				__ldtoken(InterpolationHelper.Lerp(QuaternionD, QuaternionD, double));
			}
			target.W = 0.0 - target.W;
			target.X = 0.0 - target.X;
			target.Y = 0.0 - target.Y;
			target.Z = 0.0 - target.Z;
		}
		QuaternionD result = default;
		result.W = source.W + (target.W - source.W) * parameter;
		result.X = source.X + (target.X - source.X) * parameter;
		result.Y = source.Y + (target.Y - source.Y) * parameter;
		result.Z = source.Z + (target.Z - source.Z) * parameter;
		result.Normalize();
		return result;
	}

	/// <summary>
	///       Performs a step interpolation between two values.
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <param name="interpolation">The step interpolation type.</param>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <returns>The step interpolation of two values.</returns>
	public static T Step<T>(T source, T target, float parameter, StepInterpolation interpolation)
	{
		if (interpolation == StepInterpolation.Centered)
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
						__ldtoken(InterpolationHelper.Step<T>(T, T, float, StepInterpolation));
					}
					if (!(parameter < 0.5f))
					{
						while (true)
						{
							switch (4)
							{
							case 0:
								break;
							default:
								return target;
							}
						}
					}
					return source;
				}
			}
		}
		if (interpolation == StepInterpolation.Left)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (!(parameter <= 0f))
					{
						return target;
					}
					return source;
				}
			}
		}
		if (!(parameter < 1f))
		{
			return target;
		}
		return source;
	}

	/// <summary>
	///       Performs a step interpolation between two values.
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <param name="interpolation">The step interpolation type.</param>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <returns>The step interpolation of two values.</returns>
	public static T Step<T>(T source, T target, double parameter, StepInterpolation interpolation)
	{
		switch (interpolation)
		{
		case StepInterpolation.Centered:
			if (!(parameter < 0.5))
			{
				return target;
			}
			return source;
		case StepInterpolation.Left:
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(InterpolationHelper.Step<T>(T, T, double, StepInterpolation));
				}
				if (!(parameter <= 0.0))
				{
					while (true)
					{
						switch (4)
						{
						case 0:
							break;
						default:
							return target;
						}
					}
				}
				return source;
			}
		default:
			if (!(parameter < 1.0))
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						return target;
					}
				}
			}
			return source;
		}
	}

	/// <summary>
	///       Performs a cosine interpolation between two values (single-precision).
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The cosine interpolation of two values.</returns>
	public static float CosineInterpolation(float source, float target, float parameter)
	{
		float num = (1f - (float)Math.Cos((double)parameter * Math.PI)) / 2f;
		return source * (1f - num) + target * num;
	}

	/// <summary>
	///       Performs a cosine interpolation between two values (double-precision).
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The cosine interpolation of two values.</returns>
	public static double CosineInterpolation(double source, double target, double parameter)
	{
		double num = (1.0 - Math.Cos(parameter * Math.PI)) / 2.0;
		return source * (1.0 - num) + target * num;
	}

	/// <summary>
	///       Performs a cosine interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The cosine interpolation of two values.</returns>
	public static Vector2F CosineInterpolation(Vector2F source, Vector2F target, float parameter)
	{
		float num = (1f - (float)Math.Cos((double)parameter * Math.PI)) / 2f;
		return source * (1f - num) + target * num;
	}

	/// <summary>
	///       Performs a cosine interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector2D" />.
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The cosine interpolation of two values.</returns>
	public static Vector2D CosineInterpolation(Vector2D source, Vector2D target, double parameter)
	{
		double num = (1.0 - Math.Cos(parameter * Math.PI)) / 2.0;
		return source * (1.0 - num) + target * num;
	}

	/// <summary>
	///       Performs a cosine interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />.
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The cosine interpolation of two values.</returns>
	public static Vector3F CosineInterpolation(Vector3F source, Vector3F target, float parameter)
	{
		float num = (1f - (float)Math.Cos((double)parameter * Math.PI)) / 2f;
		return source * (1f - num) + target * num;
	}

	/// <summary>
	///       Performs a cosine interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector3D" />.
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The cosine interpolation of two values.</returns>
	public static Vector3D CosineInterpolation(Vector3D source, Vector3D target, double parameter)
	{
		double num = (1.0 - Math.Cos(parameter * Math.PI)) / 2.0;
		return source * (1.0 - num) + target * num;
	}

	/// <summary>
	///       Performs a cosine interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The cosine interpolation of two values.</returns>
	public static Vector4F CosineInterpolation(Vector4F source, Vector4F target, float parameter)
	{
		float num = (1f - (float)Math.Cos((double)parameter * Math.PI)) / 2f;
		return source * (1f - num) + target * num;
	}

	/// <summary>
	///       Performs a cosine interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.Vector4D" />.
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The cosine interpolation of two values.</returns>
	public static Vector4D CosineInterpolation(Vector4D source, Vector4D target, double parameter)
	{
		double num = (1.0 - Math.Cos(parameter * Math.PI)) / 2.0;
		return source * (1.0 - num) + target * num;
	}

	/// <summary>
	///       Performs a cosine interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" />.
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The cosine interpolation of two values.</returns>
	public static VectorF CosineInterpolation(VectorF source, VectorF target, float parameter)
	{
		float num = (1f - (float)Math.Cos((double)parameter * Math.PI)) / 2f;
		return source * (1f - num) + target * num;
	}

	/// <summary>
	///       Performs a cosine interpolation between two <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" />.
	///       </summary>
	/// <param name="source">The source value.</param>
	/// <param name="target">The target value.</param>
	/// <param name="parameter">
	///       The interpolation parameter that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The cosine interpolation of two values.</returns>
	public static VectorD CosineInterpolation(VectorD source, VectorD target, double parameter)
	{
		double num = (1.0 - Math.Cos(parameter * Math.PI)) / 2.0;
		return source * (1.0 - num) + target * num;
	}

	/// <summary>
	///       Computes the function value for <paramref name="x" /> using polynomial interpolation (single-precision).
	///       </summary>
	/// <param name="points">The points.</param>
	/// <param name="x">The <i>x</i> value.</param>
	/// <returns>The interpolated value <i>y = P(x)</i>.</returns>
	public static float PolynomialInterpolation(IList<Vector2F> points, float x)
	{
		float error;
		return PolynomialInterpolation(points, x, out error);
	}

	/// <summary>
	///       Computes the function value for <paramref name="x" /> using polynomial interpolation (double-precision).
	///       </summary>
	/// <param name="points">The points.</param>
	/// <param name="x">The <i>x</i> value.</param>
	/// <returns>The interpolated value <i>y = P(x)</i>.</returns>
	public static double PolynomialInterpolation(IList<Vector2D> points, double x)
	{
		double error;
		return PolynomialInterpolation(points, x, out error);
	}

	/// <summary>
	///       Computes the function value for <paramref name="x" /> using polynomial interpolation (single-precision).
	///       </summary>
	/// <param name="points">The points.</param>
	/// <param name="x">The <i>x</i> value.</param>
	/// <param name="error">The error estimate.</param>
	/// <returns>The interpolated value <i>y = P(x)</i>.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The number of points should be greater than 1.
	///       </exception>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       2 points have identical x-values (within roundoff tolerance).
	///       </exception>
	public static float PolynomialInterpolation(IList<Vector2F> points, float x, out float error)
	{
		if (points == null)
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
						__ldtoken(InterpolationHelper.PolynomialInterpolation(IList<Vector2F>, float, ref float));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3057));
				}
			}
		}
		if (points.Count == 0)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10848));
		}
		int count = points.Count;
		float[] array = new float[count];
		float[] array2 = new float[count];
		float num = Math.Abs(x - points[0].X);
		int num2 = 0;
		for (int i = 0; i < count; i++)
		{
			float num3 = Math.Abs(x - points[i].X);
			if (num3 < num)
			{
				num2 = i;
				num = num3;
			}
			array[i] = points[i].Y;
			array2[i] = points[i].Y;
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			float num4 = points[num2].Y;
			error = 0f;
			num2--;
			int num5 = 1;
			while (num5 < count)
			{
				for (int j = 0; j < count - num5; j++)
				{
					float num6 = points[j].X - x;
					float num7 = points[j + num5].X - x;
					float num8 = array[j + 1] - array2[j];
					float num9 = num6 - num7;
					if ((double)num9 == 0.0)
					{
						while (true)
						{
							switch (7)
							{
							case 0:
								break;
							default:
								throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10941));
							}
						}
					}
					num9 = num8 / num9;
					array2[j] = num7 * num9;
					array[j] = num6 * num9;
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					float num10;
					if (2 * (num2 + 1) >= count - num5)
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
						num10 = array2[num2--];
					}
					else
					{
						num10 = array[num2 + 1];
					}
					num4 += (error = num10);
					num5++;
					break;
				}
			}
			return num4;
		}
	}

	/// <summary>
	///       Computes the function value for <paramref name="x" /> using polynomial interpolation (double-precision).
	///       </summary>
	/// <param name="points">The points.</param>
	/// <param name="x">The <i>x</i> value.</param>
	/// <param name="error">The error estimate.</param>
	/// <returns>The interpolated value <i>y = P(x)</i>.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The number of points should be greater than 1.
	///       </exception>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       2 points have identical x-values (within roundoff tolerance).
	///       </exception>
	public static double PolynomialInterpolation(IList<Vector2D> points, double x, out double error)
	{
		if (points == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3057));
		}
		if (points.Count == 0)
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
						__ldtoken(InterpolationHelper.PolynomialInterpolation(IList<Vector2D>, double, ref double));
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10848));
				}
			}
		}
		int count = points.Count;
		double[] array = new double[count];
		double[] array2 = new double[count];
		double num = Math.Abs(x - points[0].X);
		int num2 = 0;
		for (int i = 0; i < count; i++)
		{
			double num3 = Math.Abs(x - points[i].X);
			if (num3 < num)
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
				num2 = i;
				num = num3;
			}
			array[i] = points[i].Y;
			array2[i] = points[i].Y;
		}
		double num4 = points[num2].Y;
		error = 0.0;
		num2--;
		for (int j = 1; j < count; j++)
		{
			for (int k = 0; k < count - j; k++)
			{
				double num5 = points[k].X - x;
				double num6 = points[k + j].X - x;
				double num7 = array[k + 1] - array2[k];
				double num8 = num5 - num6;
				if (num8 == 0.0)
				{
					while (true)
					{
						switch (4)
						{
						case 0:
							break;
						default:
							throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10941));
						}
					}
				}
				num8 = num7 / num8;
				array2[k] = num6 * num8;
				array[k] = num5 * num8;
			}
			double num9;
			if (2 * (num2 + 1) >= count - j)
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
				num9 = array2[num2--];
			}
			else
			{
				num9 = array[num2 + 1];
			}
			num4 += (error = num9);
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			return num4;
		}
	}

	/// <summary>
	///       Performs a spherical linear interpolation of the two <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" />.
	///       </summary>
	/// <param name="source">The start quaternion.</param>
	/// <param name="target">The end quaternion.</param>
	/// <param name="parameter">
	///       The interpolation factor that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The spherical linear interpolation of the two quaternions.</returns>
	public static QuaternionF Slerp(QuaternionF source, QuaternionF target, float parameter)
	{
		float num = QuaternionF.Dot(source, target);
		if (num < 0f)
		{
			target.W = 0f - target.W;
			target.X = 0f - target.X;
			target.Y = 0f - target.Y;
			target.Z = 0f - target.Z;
			num = 0f - num;
		}
		float num6;
		float num7;
		if (num <= 0.9998f)
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
				__ldtoken(InterpolationHelper.Slerp(QuaternionF, QuaternionF, float));
			}
			float num2 = 1f - num * num;
			float num3 = (float)Math.Sqrt(num2);
			float num4 = (float)Math.Atan2(num3, num);
			float num5 = 1f / num3;
			num6 = (float)Math.Sin((1f - parameter) * num4) * num5;
			num7 = (float)Math.Sin(parameter * num4) * num5;
		}
		else
		{
			num6 = 1f - parameter;
			num7 = parameter;
		}
		QuaternionF result = default;
		result.W = source.W * num6 + target.W * num7;
		result.X = source.X * num6 + target.X * num7;
		result.Y = source.Y * num6 + target.Y * num7;
		result.Z = source.Z * num6 + target.Z * num7;
		return result;
	}

	/// <summary>
	///       Performs a spherical linear interpolation of the two <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" />.
	///       </summary>
	/// <param name="source">The start quaternion.</param>
	/// <param name="target">The end quaternion.</param>
	/// <param name="parameter">
	///       The interpolation factor that lies in the interval [0,1]; also known as <i>interpolation
	///       factor</i> or <i>weight of the target value</i>.
	///       </param>
	/// <returns>The spherical linear interpolation of the two quaternions.</returns>
	public static QuaternionD Slerp(QuaternionD source, QuaternionD target, double parameter)
	{
		double num = QuaternionD.Dot(source, target);
		if (num < 0.0)
		{
			target.W = 0.0 - target.W;
			target.X = 0.0 - target.X;
			target.Y = 0.0 - target.Y;
			target.Z = 0.0 - target.Z;
			num = 0.0 - num;
		}
		double num5;
		double num6;
		if (num <= 0.9998)
		{
			double d = 1.0 - num * num;
			double num2 = Math.Sqrt(d);
			double num3 = Math.Atan2(num2, num);
			double num4 = 1.0 / num2;
			num5 = Math.Sin((1.0 - parameter) * num3) * num4;
			num6 = Math.Sin(parameter * num3) * num4;
		}
		else
		{
			num5 = 1.0 - parameter;
			num6 = parameter;
		}
		QuaternionD result = default;
		result.W = source.W * num5 + target.W * num6;
		result.X = source.X * num5 + target.X * num6;
		result.Y = source.Y * num5 + target.Y * num6;
		result.Z = source.Z * num5 + target.Z * num6;
		return result;
	}

	/// <summary>
	///       Interpolates between quaternions using spherical quadrangle interpolation (single-precision).
	///       </summary>
	/// <param name="q">The source quaternion (<i>q<sub>n</sub></i>).</param>
	/// <param name="a">The first intermediate quaternion (<i>a<sub>n</sub></i>).</param>
	/// <param name="b">The second intermediate quaternion (<i>a<sub>n+1</sub></i>).</param>
	/// <param name="p">The target quaternion (<i>q<sub>n+1</sub></i>).</param>
	/// <param name="t">The interpolation parameter t.</param>
	/// <returns>The interpolated quaternion.</returns>
	public static QuaternionF Squad(QuaternionF q, QuaternionF a, QuaternionF b, QuaternionF p, float t)
	{
		return Slerp(Slerp(q, p, t), Slerp(a, b, t), 2f * t * (1f - t));
	}

	/// <summary>
	///       Interpolates between quaternions using spherical quadrangle interpolation (double-precision).
	///       </summary>
	/// <param name="q">The source quaternion (<i>q<sub>n</sub></i>).</param>
	/// <param name="a">The first intermediate quaternion (<i>a<sub>n</sub></i>).</param>
	/// <param name="b">The second intermediate quaternion (<i>a<sub>n+1</sub></i>).</param>
	/// <param name="p">The target quaternion (<i>q<sub>n+1</sub></i>).</param>
	/// <param name="t">The interpolation parameter t.</param>
	/// <returns>The interpolated quaternion.</returns>
	public static QuaternionD Squad(QuaternionD q, QuaternionD a, QuaternionD b, QuaternionD p, double t)
	{
		return Slerp(Slerp(q, p, t), Slerp(a, b, t), 2.0 * t * (1.0 - t));
	}

	/// <summary>
	///       Calculates the parameters for a spline-based quaternion interpolation (single-precision).
	///       </summary>
	/// <param name="q0">The previous quaternion (<i>q<sub>n-1</sub></i>).</param>
	/// <param name="q1">The source quaternion (<i>q<sub>n</sub></i>).</param>
	/// <param name="q2">The target quaternion (<i>q<sub>n+1</sub></i>).</param>
	/// <param name="q3">The subsequent quaternion (<i>q<sub>n+2</sub></i>).</param>
	/// <param name="q">The source quaternion (<i>q<sub>n</sub></i>).</param>
	/// <param name="a">The first intermediate quaternion (<i>a<sub>n</sub></i>).</param>
	/// <param name="b">The second intermediate quaternion (<i>a<sub>n+1</sub></i>).</param>
	/// <param name="p">The target quaternion (<i>q<sub>n+1</sub></i>).</param>
	public static void SquadSetup(QuaternionF q0, QuaternionF q1, QuaternionF q2, QuaternionF q3, out QuaternionF q, out QuaternionF a, out QuaternionF b, out QuaternionF p)
	{
		q = q1;
		p = q2;
		QuaternionF inverse = q1.Inverse;
		QuaternionF inverse2 = q2.Inverse;
		a = q1 * QuaternionF.Exp(-0.25f * (QuaternionF.Ln(inverse * q0) + QuaternionF.Ln(inverse * q2)));
		b = q2 * QuaternionF.Exp(-0.25f * (QuaternionF.Ln(inverse2 * q1) + QuaternionF.Ln(inverse2 * q3)));
	}

	/// <summary>
	///       Calculates the parameters for a spline-based quaternion interpolation (double-precision).
	///       </summary>
	/// <param name="q0">The previous quaternion (<i>q<sub>n-1</sub></i>).</param>
	/// <param name="q1">The source quaternion (<i>q<sub>n</sub></i>).</param>
	/// <param name="q2">The target quaternion (<i>q<sub>n+1</sub></i>).</param>
	/// <param name="q3">The subsequent quaternion (<i>q<sub>n+2</sub></i>).</param>
	/// <param name="q">The source quaternion (<i>q<sub>n</sub></i>).</param>
	/// <param name="a">The first intermediate quaternion (<i>a<sub>n</sub></i>).</param>
	/// <param name="b">The second intermediate quaternion (<i>a<sub>n+1</sub></i>).</param>
	/// <param name="p">The target quaternion (<i>q<sub>n+1</sub></i>).</param>
	public static void SquadSetup(QuaternionD q0, QuaternionD q1, QuaternionD q2, QuaternionD q3, out QuaternionD q, out QuaternionD a, out QuaternionD b, out QuaternionD p)
	{
		q = q1;
		p = q2;
		QuaternionD inverse = q1.Inverse;
		QuaternionD inverse2 = q2.Inverse;
		a = q1 * QuaternionD.Exp(-0.25 * (QuaternionD.Ln(inverse * q0) + QuaternionD.Ln(inverse * q2)));
		b = q2 * QuaternionD.Exp(-0.25 * (QuaternionD.Ln(inverse2 * q1) + QuaternionD.Ln(inverse2 * q3)));
	}

	/// <summary>
	///       Updates position and velocity according to a critically damped spring (single-precision).
	///       </summary>
	/// <param name="position">The position.</param>
	/// <param name="velocity">The velocity.</param>
	/// <param name="targetPosition">The target position.</param>
	/// <param name="deltaTime">The size of the timestep.</param>
	/// <param name="smoothTime">
	///   <paramref name="smoothTime" /> is parameter that determines how fast the 
	///       <paramref name="targetPosition" /> is reached. <paramref name="smoothTime" /> is equal to the 
	///       time that is required to reach the target when at maximum speed.
	///       </param>
	public static void CriticallyDampedSmoothing(ref float position, ref float velocity, float targetPosition, float deltaTime, float smoothTime)
	{
		float num = 2f / smoothTime;
		float num2 = num * deltaTime;
		float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
		float num4 = position - targetPosition;
		float num5 = (velocity + num * num4) * deltaTime;
		velocity = (velocity - num * num5) * num3;
		position = targetPosition + (num4 + num5) * num3;
	}

	/// <summary>
	///       Updates position and velocity according to a critically damped spring (double-precision).
	///       </summary>
	/// <param name="position">The position.</param>
	/// <param name="velocity">The velocity.</param>
	/// <param name="targetPosition">The target position.</param>
	/// <param name="deltaTime">The size of the timestep.</param>
	/// <param name="smoothTime">
	///   <paramref name="smoothTime" /> is parameter that determines how fast the 
	///       <paramref name="targetPosition" /> is reached. <paramref name="smoothTime" /> is equal to the 
	///       time that is required to reach the target when at maximum speed.
	///       </param>
	public static void CriticallyDampedSmoothing(ref double position, ref double velocity, double targetPosition, double deltaTime, double smoothTime)
	{
		double num = 2.0 / smoothTime;
		double num2 = num * deltaTime;
		double num3 = 1.0 / (1.0 + num2 + 0.48 * num2 * num2 + 0.235 * num2 * num2 * num2);
		double num4 = position - targetPosition;
		double num5 = (velocity + num * num4) * deltaTime;
		velocity = (velocity - num * num5) * num3;
		position = targetPosition + (num4 + num5) * num3;
	}

	/// <summary>
	///       Computes the Hermite smooth step function (single-precision).
	///       </summary>
	/// <param name="x">The argument x.</param>
	/// <returns>The function value y.</returns>
	public static float HermitSmoothStep(float x)
	{
		x = MathHelper.Clamp(x, 0f, 1f);
		return x * x * (3f - 2f * x);
	}

	/// <summary>
	///       Computes the Hermite smooth step function (double-precision).
	///       </summary>
	/// <param name="x">The argument x.</param>
	/// <returns>The function value y.</returns>
	public static double HermitSmoothStep(double x)
	{
		x = MathHelper.Clamp(x, 0.0, 1.0);
		return x * x * (3.0 - 2.0 * x);
	}

	/// <summary>
	///       Computes a constant acceleration ease-in/ease-out smooth step function (single-precision).
	///       </summary>
	/// <param name="x">The argument x.</param>
	/// <returns>The function value y.</returns>
	public static float EaseInOutSmoothStep(float x)
	{
		if (x <= 0f)
		{
			return 0f;
		}
		if (x >= 1f)
		{
			return 1f;
		}
		if ((double)x < 0.5)
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
						__ldtoken(InterpolationHelper.EaseInOutSmoothStep(float));
					}
					return 2f * x * x;
				}
			}
		}
		return 1f - 2f * (1f - x) * (1f - x);
	}

	/// <summary>
	///       Computes a constant acceleration ease-in/ease-out smooth step function (double-precision).
	///       </summary>
	/// <param name="x">The argument x.</param>
	/// <returns>The function value y.</returns>
	public static double EaseInOutSmoothStep(double x)
	{
		if (x <= 0.0)
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
						__ldtoken(InterpolationHelper.EaseInOutSmoothStep(double));
					}
					return 0.0;
				}
			}
		}
		if (x >= 1.0)
		{
			return 1.0;
		}
		if (x < 0.5)
		{
			return 2.0 * x * x;
		}
		return 1.0 - 2.0 * (1.0 - x) * (1.0 - x);
	}
}
