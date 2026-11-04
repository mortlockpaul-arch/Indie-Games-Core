using System;
using A;
using DigitalRune.Mathematics;

namespace DigitalRune.Physics.Settings;

/// <summary>
///       Defines timing-related simulation settings.
///       </summary>
public class TimingSettings
{
	private float c3abbe5d8d6d59363f57a23363fafdc17;

	private int c5dcfbc2f67f8a7699ef46391493f2414;

	/// <summary>
	///       Gets or sets the time step size for fixed time steps.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public float FixedTimeStep
	{
		get
		{
			return c3abbe5d8d6d59363f57a23363fafdc17;
		}
		set
		{
			if (Numeric.IsLessOrEqual(value, 0f))
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4396));
			}
			c3abbe5d8d6d59363f57a23363fafdc17 = value;
		}
	}

	/// <summary>
	///       Gets or sets the maximal number of sub-steps performed during one time step.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public int MaxNumberOfSteps
	{
		get
		{
			return c5dcfbc2f67f8a7699ef46391493f2414;
		}
		set
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4471));
			}
			c5dcfbc2f67f8a7699ef46391493f2414 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Settings.TimingSettings" /> class.
	///       </summary>
	public TimingSettings()
	{
		FixedTimeStep = 1f / 60f;
		MaxNumberOfSteps = 8;
	}
}
