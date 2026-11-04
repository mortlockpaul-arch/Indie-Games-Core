using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Finds roots using the Newton-Raphson method (single-precision).
///       </summary>
public class NewtonRaphsonMethodF : RootFinderF
{
	[CompilerGenerated]
	private Func<float, float> c23eb224373e15d1af8d5bfe34c6e3b06;

	/// <summary>
	///       Gets a function that computes the derivative <i>f'(x)</i>.
	///       </summary>
	public Func<float, float> Derivative
	{
		[CompilerGenerated]
		get
		{
			return c23eb224373e15d1af8d5bfe34c6e3b06;
		}
		[CompilerGenerated]
		private set
		{
			c23eb224373e15d1af8d5bfe34c6e3b06 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.NewtonRaphsonMethodF" /> class.
	///       </summary>
	/// <param name="function">The function f(x), which root we want to find.</param>
	/// <param name="derivative">The function f'(x), which computes the derivative.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="derivative" /> is <see langword="null" />.
	///       </exception>
	public NewtonRaphsonMethodF(Func<float, float> function, Func<float, float> derivative)
		: base(function)
	{
		if (derivative == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3252));
		}
		Derivative = derivative;
	}

	/// <summary>
	///       Finds the root of the given function.
	///       </summary>
	/// <param name="function">The function f.</param>
	/// <param name="x0">
	///       An x value such that the root lies between <paramref name="x0" /> and <paramref name="x1" />.
	///       </param>
	/// <param name="x1">
	///       An x value such that the root lies between <paramref name="x0" /> and <paramref name="x1" />.
	///       </param>
	/// <returns>
	///       The x value such that <i>f(x) = 0</i>; or <i>NaN</i> if no root is found.
	///       </returns>
	protected override float FindRoot(Func<float, float> function, float x0, float x1)
	{
		NumberOfIterations = 0;
		float value = function(x0);
		float value2 = function(x1);
		if (Numeric.IsZero(value, Epsilon))
		{
			return x0;
		}
		if (Numeric.IsZero(value2, Epsilon))
		{
			return x1;
		}
		float num = (x0 + x1) / 2f;
		int num2 = 0;
		while (num2 < MaxNumberOfIterations)
		{
			NumberOfIterations++;
			float num3 = function(num);
			float num4 = Derivative(num);
			float num5 = num3 / num4;
			if (!Numeric.IsZero(num3, Epsilon))
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
					__ldtoken(NewtonRaphsonMethodF.FindRoot(Func<float, float>, float, float));
				}
				if (!Numeric.IsZero(num5, Epsilon))
				{
					num -= num5;
					if ((x0 - num) * (num - x1) < 0f)
					{
						while (true)
						{
							switch (1)
							{
							case 0:
								break;
							default:
								return float.NaN;
							}
						}
					}
					num2++;
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
			}
			return num;
		}
		return float.NaN;
	}
}
