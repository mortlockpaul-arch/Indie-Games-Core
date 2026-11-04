using System.Runtime.CompilerServices;

namespace DigitalRune.Mathematics.Statistics;

/// <summary>
///       Creates random values using an approximate Gaussian distribution (single-precision).
///       </summary>
public class FastGaussianDistributionF : Distribution<float>
{
	[CompilerGenerated]
	private float c2fe009dfe7086fca7d22b5606f2a3492;

	[CompilerGenerated]
	private float c9a92d1056a4a5ff6d01ed82e1cc39b21;

	/// <summary>
	///       Gets or sets the expected value.
	///       </summary>
	public float ExpectedValue
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
	public float StandardDeviation
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
	public override float NextValue
	{
		get
		{
			float num = Random.NextFloat(-1f, 1f);
			float num2 = Random.NextFloat(-1f, 1f);
			float num3 = Random.NextFloat(-1f, 1f);
			float num4 = num + num2 + num3;
			num4 *= StandardDeviation;
			return num4 + ExpectedValue;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Statistics.FastGaussianDistributionF" /> class.
	///       </summary>
	public FastGaussianDistributionF()
	{
		StandardDeviation = 1f;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Statistics.FastGaussianDistributionF" /> class.
	///       </summary>
	/// <param name="expectedValue">The expected value.</param>
	/// <param name="standardDeviation">The standard deviation.</param>
	public FastGaussianDistributionF(float expectedValue, float standardDeviation)
	{
		ExpectedValue = expectedValue;
		StandardDeviation = standardDeviation;
	}
}
