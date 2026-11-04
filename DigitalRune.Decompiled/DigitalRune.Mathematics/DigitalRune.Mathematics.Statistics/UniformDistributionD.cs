using System.Runtime.CompilerServices;

namespace DigitalRune.Mathematics.Statistics;

/// <summary>
///       Represents a uniform distribution that returns random double values for a given interval 
///       [<see cref="P:DigitalRune.Mathematics.Statistics.UniformDistributionD.MinValue" />, <see cref="P:DigitalRune.Mathematics.Statistics.UniformDistributionD.MaxValue" />] (double-precision).
///       </summary>
public class UniformDistributionD : Distribution<double>
{
	[CompilerGenerated]
	private double c62f561ab2d62d86962c826e0554147f6;

	[CompilerGenerated]
	private double c133610356f88778206929b76fb9881e0;

	/// <summary>
	///       Gets or sets the maximum value.
	///       </summary>
	public double MaxValue
	{
		[CompilerGenerated]
		get
		{
			return c62f561ab2d62d86962c826e0554147f6;
		}
		[CompilerGenerated]
		set
		{
			c62f561ab2d62d86962c826e0554147f6 = value;
		}
	}

	/// <summary>
	///       Gets or sets the minimum value.
	///       </summary>
	public double MinValue
	{
		[CompilerGenerated]
		get
		{
			return c133610356f88778206929b76fb9881e0;
		}
		[CompilerGenerated]
		set
		{
			c133610356f88778206929b76fb9881e0 = value;
		}
	}

	/// <summary>
	///       Gets a new random value for the underlying probability distribution.
	///       </summary>
	public override double NextValue => MinValue + (MaxValue - MinValue) * Random.NextDouble();

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Statistics.UniformDistributionD" /> class.
	///       </summary>
	public UniformDistributionD()
	{
		MinValue = -1.0;
		MaxValue = 1.0;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Statistics.UniformDistributionD" /> class.
	///       </summary>
	/// <param name="minValue">The minimum value.</param>
	/// <param name="maxValue">The maximum value.</param>
	public UniformDistributionD(double minValue, double maxValue)
	{
		MinValue = minValue;
		MaxValue = maxValue;
	}
}
