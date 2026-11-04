using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       A base class for methods which solve a single equation (single-precision). 
///       </summary>
public abstract class RootFinderF
{
	[CompilerGenerated]
	private sealed class cfac1af70369a520d2b4fffef910cd91d
	{
		public RootFinderF c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public float c0c03f4e8a4fc683a5246023a7ee7fb32;

		public float c32c313e0acca75bb9a66aafc35741e64(float c5078b1ca2bd86333c65cb625b996b618)
		{
			return c1b0bbfd4af1b6a4c0c71a5dd3c509812.Function(c5078b1ca2bd86333c65cb625b996b618) - c0c03f4e8a4fc683a5246023a7ee7fb32;
		}
	}

	[CompilerGenerated]
	private sealed class cf772a50f5392471d1964f4269bc5c55b
	{
		public RootFinderF c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public float c0c03f4e8a4fc683a5246023a7ee7fb32;

		public float c8b34a5027fefd0e28040b0c4b9fd58a8(float c5078b1ca2bd86333c65cb625b996b618)
		{
			return c1b0bbfd4af1b6a4c0c71a5dd3c509812.Function(c5078b1ca2bd86333c65cb625b996b618) - c0c03f4e8a4fc683a5246023a7ee7fb32;
		}
	}

	private int ce55e022e0018785bcb4289746320a408 = 50;

	private float c8b99649fe8ac20b87a444ad033b2d940 = Numeric.EpsilonF;

	[CompilerGenerated]
	private Func<float, float> c80ff9ef51b94af980a78c423a644d183;

	[CompilerGenerated]
	private int c067481a3e0ca7109815a79d198ffdfe2;

	/// <summary>
	///       Gets the function f(x), which root we want to find.
	///       </summary>
	public Func<float, float> Function
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
	///       <see cref="M:DigitalRune.Mathematics.Analysis.RootFinderF.FindRoot(System.Single,System.Single)" /> method call.
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
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(159), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(170));
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
	public float Epsilon
	{
		get
		{
			return c8b99649fe8ac20b87a444ad033b2d940;
		}
		set
		{
			if (value <= 0f)
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
							__ldtoken(RootFinderF.set_Epsilon(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(159), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(275));
					}
				}
			}
			c8b99649fe8ac20b87a444ad033b2d940 = value;
		}
	}

	static RootFinderF()
	{
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.RootFinderF" /> class.
	///       </summary>
	/// <param name="function">The function f(x), which root we want to find.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	protected RootFinderF(Func<float, float> function)
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
	public bool ExpandBracket(ref float x0, ref float x1)
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
	public bool ExpandBracket(ref float x0, ref float x1, float y)
	{
		return c0824ecf2e45417105df379175cc711c5((float c5078b1ca2bd86333c65cb625b996b618) => Function(c5078b1ca2bd86333c65cb625b996b618) - y, ref x0, ref x1);
	}

	private bool c0824ecf2e45417105df379175cc711c5(Func<float, float> c82d1df3580e4faabc557f40ec1c0a401, ref float ce24152afb0bbefbe03e385c829277515, ref float cd92535445e7c9b4ca45abbd8144b7bcc)
	{
		if (Numeric.AreEqual(ce24152afb0bbefbe03e385c829277515, cd92535445e7c9b4ca45abbd8144b7bcc))
		{
			cd92535445e7c9b4ca45abbd8144b7bcc++;
		}
		float num = c82d1df3580e4faabc557f40ec1c0a401(ce24152afb0bbefbe03e385c829277515);
		float num2 = c82d1df3580e4faabc557f40ec1c0a401(cd92535445e7c9b4ca45abbd8144b7bcc);
		for (int i = 0; i < MaxNumberOfIterations; i++)
		{
			if (num * num2 < 0f)
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
							__ldtoken(RootFinderF.c0824ecf2e45417105df379175cc711c5(Func<float, float>, ref float, ref float));
						}
						return true;
					}
				}
			}
			if (Math.Abs(num) < Math.Abs(num2))
			{
				ce24152afb0bbefbe03e385c829277515 += 1.6f * (ce24152afb0bbefbe03e385c829277515 - cd92535445e7c9b4ca45abbd8144b7bcc);
				num = c82d1df3580e4faabc557f40ec1c0a401(ce24152afb0bbefbe03e385c829277515);
			}
			else
			{
				cd92535445e7c9b4ca45abbd8144b7bcc += 1.6f * (cd92535445e7c9b4ca45abbd8144b7bcc - ce24152afb0bbefbe03e385c829277515);
				num2 = c82d1df3580e4faabc557f40ec1c0a401(cd92535445e7c9b4ca45abbd8144b7bcc);
			}
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			return false;
		}
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
	public float FindRoot(float x0, float x1)
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
	public float FindRoot(float x0, float x1, float y)
	{
		return FindRoot((float c5078b1ca2bd86333c65cb625b996b618) => Function(c5078b1ca2bd86333c65cb625b996b618) - y, x0, x1);
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
	protected abstract float FindRoot(Func<float, float> function, float x0, float x1);
}
