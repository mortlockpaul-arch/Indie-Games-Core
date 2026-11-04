using System.Runtime.CompilerServices;

namespace DigitalRune.Mathematics.Statistics;

/// <summary>
///       Represents a uniform distribution that returns random double values for a given interval 
///       [<see cref="P:DigitalRune.Mathematics.Statistics.UniformDistributionF.MinValue" />, <see cref="P:DigitalRune.Mathematics.Statistics.UniformDistributionF.MaxValue" />] (single-precision).
///       </summary>
public class UniformDistributionF : Distribution<float>
{
	[CompilerGenerated]
	private float c62f561ab2d62d86962c826e0554147f6;

	[CompilerGenerated]
	private float c133610356f88778206929b76fb9881e0;

	/// <summary>
	///       Gets or sets the maximum value.
	///       </summary>
	public float MaxValue
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
	public float MinValue
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
	public override float NextValue => MinValue + (MaxValue - MinValue) * (float)Random.NextDouble();

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Statistics.UniformDistributionF" /> class.
	///       </summary>
	public UniformDistributionF()
	{
		MinValue = -1f;
		MaxValue = 1f;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Statistics.UniformDistributionF" /> class.
	///       </summary>
	/// <param name="minValue">The minimum value.</param>
	/// <param name="maxValue">The maximum value.</param>
	public UniformDistributionF(float minValue, float maxValue)
	{
		MinValue = minValue;
		MaxValue = maxValue;
	}
}
