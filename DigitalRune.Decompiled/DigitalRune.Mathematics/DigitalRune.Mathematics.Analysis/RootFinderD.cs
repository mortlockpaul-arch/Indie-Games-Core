using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       A base class for methods which solve a single equation (double-precision). 
///       </summary>
public abstract class RootFinderD
{
	[CompilerGenerated]
	private sealed class cfb770f43fb92a4cd77605421696d8432
	{
		public RootFinderD c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public double c0c03f4e8a4fc683a5246023a7ee7fb32;

		public double c32c313e0acca75bb9a66aafc35741e64(double c5078b1ca2bd86333c65cb625b996b618)
		{
			return c1b0bbfd4af1b6a4c0c71a5dd3c509812.Function(c5078b1ca2bd86333c65cb625b996b618) - c0c03f4e8a4fc683a5246023a7ee7fb32;
		}
	}

	[CompilerGenerated]
	private sealed class ca720a50f0acce7d5dd6a78f038e2273d
	{
		public RootFinderD c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public double c0c03f4e8a4fc683a5246023a7ee7fb32;

		public double c8b34a5027fefd0e28040b0c4b9fd58a8(double c5078b1ca2bd86333c65cb625b996b618)
		{
			return c1b0bbfd4af1b6a4c0c71a5dd3c509812.Function(c5078b1ca2bd86333c65cb625b996b618) - c0c03f4e8a4fc683a5246023a7ee7fb32;
		}
	}

	private int ce55e022e0018785bcb4289746320a408 = 100;

	private double c8b99649fe8ac20b87a444ad033b2d940 = Numeric.EpsilonD;

	[CompilerGenerated]
	private Func<double, double> c80ff9ef51b94af980a78c423a644d183;

	[CompilerGenerated]
	private int c067481a3e0ca7109815a79d198ffdfe2;

	/// <summary>
	///       Gets the function f(x), which root we want to find.
	///       </summary>
	public Func<double, double> Function
	{
		[CompilerGenerated]
		get
		{
			return c80ff9ef51b94af980a78c423a644d183;
		}
		[CompilerGenerated]
		private set
		{
			c80ff9ef51b94af980a78c423a644d183 = value;
		}
	}

	/// <summary>
	///       Gets or sets the number of iterations of the last 
	///       <see cref="M:DigitalRune.Mathematics.Analysis.RootFinderD.FindRoot(System.Double,System.Double)" /> method call.
	///       </summary>
	public int NumberOfIterations
	{
		[CompilerGenerated]
		get
		{
			return c067481a3e0ca7109815a79d198ffdfe2;
		}
		[CompilerGenerated]
		protected set
		{
			c067481a3e0ca7109815a79d198ffdfe2 = value;
		}
	}

	/// <summary>
	///       Gets or sets the maximum number of iterations.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public int MaxNumberOfIterations
	{
		get
		{
			return ce55e022e0018785bcb4289746320a408;
		}
		set
		{
			if (value < 0)
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
							__ldtoken(RootFinderD.set_MaxNumberOfIterations(int));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(159), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(170));
					}
				}
			}
			ce55e022e0018785bcb4289746320a408 = value;
		}
	}

	/// <summary>
	///       Gets or sets the tolerance value. 
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public double Epsilon
	{
		get
		{
			return c8b99649fe8ac20b87a444ad033b2d940;
		}
		set
		{
			if (value <= 0.0)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(159), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(275));
			}
			c8b99649fe8ac20b87a444ad033b2d940 = value;
		}
	}

	static RootFinderD()
	{
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.RootFinderD" /> class.
	///       </summary>
	/// <param name="function">The function f(x), which root we want to find.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	protected RootFinderD(Func<double, double> function)
	{
		if (function == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(366));
		}
		Function = function;
	}

	/// <summary>
	///       Takes the initial guessed range [<paramref name="x0" />, <paramref name="x1" />] and expands
	///       this interval such that the root <i>x<sub>root</sub></i> where 
	///       <i>f(x<sub>root</sub>) = 0</i> is in the interval.
	///       </summary>
	/// <param name="x0">The left bound of the interval.</param>
	/// <param name="x1">The right bound of the interval.</param>
	/// <returns>
	///   <see langword="true" /> if a valid bracket was found; otherwise <see langword="false" />.
	///       </returns>
	public bool ExpandBracket(ref double x0, ref double x1)
	{
		return c0824ecf2e45417105df379175cc711c5(Function, ref x0, ref x1);
	}

	/// <summary>
	///       Takes the initial guessed range [<paramref name="x0" />, <paramref name="x1" />] and expands
	///       this interval such that <i>x</i> with <i>f(x) = y</i>
	///       is in the interval.
	///       </summary>
	/// <param name="x0">The left bound of the interval.</param>
	/// <param name="x1">The right bound of the interval.</param>
	/// <param name="y">The y for which an x is searched for such that <i>f(x) = y</i>.</param>
	/// <returns>
	///   <see langword="true" /> if a valid bracket was found; otherwise <see langword="false" />.
	///       </returns>
	public bool ExpandBracket(ref double x0, ref double x1, double y)
	{
		return c0824ecf2e45417105df379175cc711c5((double c5078b1ca2bd86333c65cb625b996b618) => Function(c5078b1ca2bd86333c65cb625b996b618) - y, ref x0, ref x1);
	}

	private bool c0824ecf2e45417105df379175cc711c5(Func<double, double> c82d1df3580e4faabc557f40ec1c0a401, ref double ce24152afb0bbefbe03e385c829277515, ref double cd92535445e7c9b4ca45abbd8144b7bcc)
	{
		if (Numeric.AreEqual(ce24152afb0bbefbe03e385c829277515, cd92535445e7c9b4ca45abbd8144b7bcc))
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
				__ldtoken(RootFinderD.c0824ecf2e45417105df379175cc711c5(Func<double, double>, ref double, ref double));
			}
			cd92535445e7c9b4ca45abbd8144b7bcc++;
		}
		double num = c82d1df3580e4faabc557f40ec1c0a401(ce24152afb0bbefbe03e385c829277515);
		double num2 = c82d1df3580e4faabc557f40ec1c0a401(cd92535445e7c9b4ca45abbd8144b7bcc);
		for (int i = 0; i < MaxNumberOfIterations; i++)
		{
			if (num * num2 < 0.0)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						return true;
					}
				}
			}
			if (Math.Abs(num) < Math.Abs(num2))
			{
				ce24152afb0bbefbe03e385c829277515 += 1.6 * (ce24152afb0bbefbe03e385c829277515 - cd92535445e7c9b4ca45abbd8144b7bcc);
				num = c82d1df3580e4faabc557f40ec1c0a401(ce24152afb0bbefbe03e385c829277515);
			}
			else
			{
				cd92535445e7c9b4ca45abbd8144b7bcc += 1.6 * (cd92535445e7c9b4ca45abbd8144b7bcc - ce24152afb0bbefbe03e385c829277515);
				num2 = c82d1df3580e4faabc557f40ec1c0a401(cd92535445e7c9b4ca45abbd8144b7bcc);
			}
		}
		return false;
	}

	/// <summary>
	///       Finds the root of the given function.
	///       </summary>
	/// <param name="x0">
	///       An x value such that the root lies between <paramref name="x0" /> and <paramref name="x1" />.
	///       </param>
	/// <param name="x1">
	///       An x value such that the root lies between <paramref name="x0" /> and <paramref name="x1" />.
	///       </param>
	/// <returns>The x value such that <i>f(x) = 0</i>; or <i>NaN</i> if no root is found.</returns>
	public double FindRoot(double x0, double x1)
	{
		return FindRoot(Function, x0, x1);
	}

	/// <summary>
	///       Finds the x for the given function, such that <i>f(x) = y</i>.
	///       </summary>
	/// <param name="x0">
	///       An x value such that the root lies between <paramref name="x0" /> and <paramref name="x1" />.
	///       </param>
	/// <param name="x1">
	///       An x value such that the root lies between <paramref name="x0" /> and <paramref name="x1" />.
	///       </param>
	/// <param name="y">
	///       The y for which an x is searched for such that <i>f(x) = y</i>.
	///       </param>
	/// <returns>
	///       The x value such that <i>f(x) = y</i>; or <i>NaN</i> if no suitable x is found.
	///       </returns>
	public double FindRoot(double x0, double x1, double y)
	{
		return FindRoot((double c5078b1ca2bd86333c65cb625b996b618) => Function(c5078b1ca2bd86333c65cb625b996b618) - y, x0, x1);
	}

	/// <summary>
	///       Finds the root of the given function.
	///       </summary>
	/// <param name="function">The function f(x).</param>
	/// <param name="x0">
	///       An x value such that the root lies between <paramref name="x0" /> and <paramref name="x1" />.
	///       </param>
	/// <param name="x1">
	///       An x value such that the root lies between <paramref name="x0" /> and <paramref name="x1" />.
	///       </param>
	/// <returns>The x value such that <i>f(x) = 0</i>; or <i>NaN</i> if no root is found.</returns>
	protected abstract double FindRoot(Func<double, double> function, double x0, double x1);
}
