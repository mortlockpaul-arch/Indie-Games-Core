using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Finds roots using the Newton-Raphson method (double-precision).
///       </summary>
public class NewtonRaphsonMethodD : RootFinderD
{
	[CompilerGenerated]
	private Func<double, double> c23eb224373e15d1af8d5bfe34c6e3b06;

	/// <summary>
	///       Gets a function that computes the derivative <i>f'(x)</i>.
	///       </summary>
	public Func<double, double> Derivative
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
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.NewtonRaphsonMethodD" /> class.
	///       </summary>
	/// <param name="function">The function f(x), which root we want to find.</param>
	/// <param name="derivative">The function f'(x), which computes the derivative.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="derivative" /> is <see langword="null" />.
	///       </exception>
	public NewtonRaphsonMethodD(Func<double, double> function, Func<double, double> derivative)
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
	protected override double FindRoot(Func<double, double> function, double x0, double x1)
	{
		NumberOfIterations = 0;
		double value = function(x0);
		double value2 = function(x1);
		if (Numeric.IsZero(value, Epsilon))
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
						__ldtoken(NewtonRaphsonMethodD.FindRoot(Func<double, double>, double, double));
					}
					return x0;
				}
			}
		}
		if (Numeric.IsZero(value2, Epsilon))
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					return x1;
				}
			}
		}
		double num = (x0 + x1) / 2.0;
		int num2 = 0;
		while (num2 < MaxNumberOfIterations)
		{
			NumberOfIterations++;
			double num3 = function(num);
			double num4 = Derivative(num);
			double num5 = num3 / num4;
			if (!Numeric.IsZero(num3, Epsilon))
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
				if (!Numeric.IsZero(num5, Epsilon))
				{
					num -= num5;
					if ((x0 - num) * (num - x1) < 0.0)
					{
						while (true)
						{
							switch (5)
							{
							case 0:
								break;
							default:
								return double.NaN;
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
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			return double.NaN;
		}
	}
}
