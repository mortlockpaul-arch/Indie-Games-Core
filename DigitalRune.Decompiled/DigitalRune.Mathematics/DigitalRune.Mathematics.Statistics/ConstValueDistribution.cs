using System.Runtime.CompilerServices;

namespace DigitalRune.Mathematics.Statistics;

/// <summary>
///       Represents a probability distribution that contains only 1 value with 100% probability. Hence 
///       this distribution always returns a single constant value - no uncertainty.
///       </summary>
/// <typeparam name="T">The type of the constant.</typeparam>
public class ConstValueDistribution<T> : Distribution<T>
{
	[CompilerGenerated]
	private T c63ed07404d6ada7b80f4a3adc560f463;

	/// <summary>
	///       Gets or sets the single constant value.
	///       </summary>
	public T Value
	{
		[CompilerGenerated]
		get
		{
			return c63ed07404d6ada7b80f4a3adc560f463;
		}
		[CompilerGenerated]
		set
		{
			c63ed07404d6ada7b80f4a3adc560f463 = value;
		}
	}

	/// <summary>
	///       Gets a new random value for the underlying probability distribution.
	///       </summary>
	public override T NextValue => Value;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Statistics.ConstValueDistribution`1" /> class.
	///       </summary>
	public ConstValueDistribution()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Statistics.ConstValueDistribution`1" /> class.
	///       </summary>
	/// <param name="value">The single constant value.</param>
	public ConstValueDistribution(T value)
	{
		Value = value;
	}
}
