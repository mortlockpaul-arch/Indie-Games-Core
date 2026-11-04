using System;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Scattered Interpolation using Shepard's method (single-precision).
///       </summary>
public class ShepardInterpolationF : ScatteredInterpolationF
{
	private float c66096959a252b9bca92da5049f39a2c9 = 2f;

	private VectorF ca6072a768e3ec26bfbe5aa7917058914;

	/// <summary>
	///       Gets or sets the exponent for the power of the distance.
	///       </summary>
	public float Power
	{
		get
		{
			return c66096959a252b9bca92da5049f39a2c9;
		}
		set
		{
			c66096959a252b9bca92da5049f39a2c9 = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.ShepardInterpolationF" /> class</summary>
	public ShepardInterpolationF()
	{
	}

	/// <summary>
	///       Called when <see cref="M:DigitalRune.Mathematics.Interpolation.ScatteredInterpolationF.Setup" /> is called.
	///       </summary>
	protected override void OnSetup()
	{
		ca6072a768e3ec26bfbe5aa7917058914 = new VectorF(Count);
	}

	/// <summary>
	///       Called when <see cref="M:DigitalRune.Mathematics.Interpolation.ScatteredInterpolationF.Compute(DigitalRune.Mathematics.Algebra.VectorF)" /> is called.
	///       </summary>
	/// <param name="x">The x value.</param>
	/// <returns>The y value.</returns>
	protected override VectorF OnCompute(VectorF x)
	{
		float num = 0f;
		int count = Count;
		for (int i = 0; i < count; i++)
		{
			ca6072a768e3ec26bfbe5aa7917058914[i] = (float)Math.Pow((x - c53720e3d9bbf70b21bdaa6ec34cfd6b0(i)).Length + Numeric.EpsilonF, 0f - Power);
			num += ca6072a768e3ec26bfbe5aa7917058914[i];
		}
		VectorF vectorF = new VectorF(cee44eaa2fcfe0f22bd650b561025cbd7(0).NumberOfElements);
		for (int j = 0; j < count; j++)
		{
			vectorF += ca6072a768e3ec26bfbe5aa7917058914[j] * cee44eaa2fcfe0f22bd650b561025cbd7(j);
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(ShepardInterpolationF.OnCompute(VectorF));
			}
			return vectorF / num;
		}
	}
}
