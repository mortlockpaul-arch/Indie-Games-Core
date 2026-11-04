using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       A base class for numerical integration of a function over an interval (single-precision).
///       </summary>
public abstract class IntegratorF
{
	private int ce55e022e0018785bcb4289746320a408 = 20;

	private int c618f87d2598740c881f8ce6368ec7d7b = 5;

	private float c8b99649fe8ac20b87a444ad033b2d940 = Numeric.EpsilonF;

	[CompilerGenerated]
	private int c067481a3e0ca7109815a79d198ffdfe2;

	/// <summary>
	///       Gets (or sets) the number of iterations of the last <see cref="M:DigitalRune.Mathematics.Analysis.IntegratorF.Integrate(System.Func{System.Single,System.Single},System.Single,System.Single)" /> method call.
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
	///       Gets or sets the minimum number number of iterations.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public int MinNumberOfIterations
	{
		get
		{
			return c618f87d2598740c881f8ce6368ec7d7b;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(159), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(383));
			}
			c618f87d2598740c881f8ce6368ec7d7b = value;
		}
	}

	/// <summary>
	///       Gets or sets the maximum number number of iterations.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
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
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(159), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(488));
			}
			c8b99649fe8ac20b87a444ad033b2d940 = value;
		}
	}

	static IntegratorF()
	{
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.IntegratorF" /> class</summary>
	protected IntegratorF()
	{
	}

	/// <summary>
	///       Integrates the specified function within the given interval.
	///       </summary>
	/// <param name="function">The function.</param>
	/// <param name="lowerBound">The lower bound.</param>
	/// <param name="upperBound">The upper bound.</param>
	/// <returns>
	///       The integral of the given function over the interval 
	///       [<paramref name="lowerBound" />, <paramref name="upperBound" />].
	///       </returns>
	public abstract float Integrate(Func<float, float> function, float lowerBound, float upperBound);
}
