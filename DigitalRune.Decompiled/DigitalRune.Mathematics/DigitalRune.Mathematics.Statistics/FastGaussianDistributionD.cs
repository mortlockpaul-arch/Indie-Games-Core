using System.Runtime.CompilerServices;

namespace DigitalRune.Mathematics.Statistics;

/// <summary>
///       Creates random values using an approximate Gaussian distribution (double-precision).
///       </summary>
public class FastGaussianDistributionD : Distribution<double>
{
	[CompilerGenerated]
	private double c2fe009dfe7086fca7d22b5606f2a3492;

	[CompilerGenerated]
	private double c9a92d1056a4a5ff6d01ed82e1cc39b21;

	/// <summary>
	///       Gets or sets the expected value.
	///       </summary>
	public double ExpectedValue
	{
		[CompilerGenerated]
		get
		{
			return c2fe009dfe7086fca7d22b5606f2a3492;
		}
		[CompilerGenerated]
		set
		{
			c2fe009dfe7086fca7d22b5606f2a3492 = value;
		}
	}

	/// <summary>
	///       Gets or sets the standard deviation.
	///       </summary>
	public double StandardDeviation
	{
		[CompilerGenerated]
		get
		{
			return c9a92d1056a4a5ff6d01ed82e1cc39b21;
		}
		[CompilerGenerated]
		set
		{
			c9a92d1056a4a5ff6d01ed82e1cc39b21 = value;
		}
	}

	/// <summary>
	///       Gets a new random value for the underlying probability distribution.
	///       </summary>
	public override double NextValue
	{
		get
		{
			double num = Random.NextDouble(-1.0, 1.0);
			double num2 = Random.NextDouble(-1.0, 1.0);
			double num3 = Random.NextDouble(-1.0, 1.0);
			double num4 = num + num2 + num3;
			num4 *= StandardDeviation;
			return num4 + ExpectedValue;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Statistics.FastGaussianDistributionD" /> class.
	///       </summary>
	public FastGaussianDistributionD()
	{
		StandardDeviation = 1.0;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Statistics.FastGaussianDistributionF" /> class.
	///       </summary>
	/// <param name="expectedValue">The expected value.</param>
	/// <param name="standardDeviation">The standard deviation.</param>
	public FastGaussianDistributionD(double expectedValue, double standardDeviation)
	{
		ExpectedValue = expectedValue;
		StandardDeviation = standardDeviation;
	}
}
