using System;

namespace DigitalRune.Mathematics.Statistics;

/// <summary>
///       Base class of probability distributions.
///       </summary>
/// <typeparam name="T">The type of the random value.</typeparam>
public abstract class Distribution<T>
{
	private Random cb8b98fd1a1a5ab13062c3456215cdaeb;

	/// <summary>
	///       Gets or sets the random number generator.
	///       </summary>
	public Random Random
	{
		get
		{
			return cb8b98fd1a1a5ab13062c3456215cdaeb ?? RandomHelper.Random;
		}
		set
		{
			cb8b98fd1a1a5ab13062c3456215cdaeb = value;
		}
	}

	/// <summary>
	///       Gets a new random value for the underlying probability distribution.
	///       </summary>
	public abstract T NextValue { get; }

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Statistics.Distribution`1" /> class</summary>
	protected Distribution()
	{
	}
}
