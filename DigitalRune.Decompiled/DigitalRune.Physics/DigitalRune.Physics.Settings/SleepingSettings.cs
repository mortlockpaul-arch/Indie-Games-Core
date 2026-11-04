using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Physics.Settings;

/// <summary>
///       Defines sleeping-related simulation settings.
///       </summary>
public class SleepingSettings
{
	private float c3567faf36b69150fdfc57ad290e5b70a;

	private float c319a9bfaaab85da1fb3812b3f3c0d520;

	private float c12d7359316e29a54ec3137a028716bb9;

	[CompilerGenerated]
	private float c372ff88ee29b0b458c875e7b664323d1;

	[CompilerGenerated]
	private float c143d548430b479bc555a083c3d97291a;

	/// <summary>
	///       Gets or sets the linear sleeping velocity threshold.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float LinearVelocityThreshold
	{
		get
		{
			return c3567faf36b69150fdfc57ad290e5b70a;
		}
		set
		{
			if (value < 0f)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(SleepingSettings.set_LinearVelocityThreshold(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5124));
					}
				}
			}
			c3567faf36b69150fdfc57ad290e5b70a = value;
			LinearVelocityThresholdSquared = value * value;
		}
	}

	internal float LinearVelocityThresholdSquared
	{
		[CompilerGenerated]
		get
		{
			return c372ff88ee29b0b458c875e7b664323d1;
		}
		[CompilerGenerated]
		private set
		{
			c372ff88ee29b0b458c875e7b664323d1 = value;
		}
	}

	/// <summary>
	///       Gets or sets the angular sleeping velocity threshold.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float AngularVelocityThreshold
	{
		get
		{
			return c319a9bfaaab85da1fb3812b3f3c0d520;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5243));
			}
			c319a9bfaaab85da1fb3812b3f3c0d520 = value;
			AngularVelocityThresholdSquared = value * value;
		}
	}

	internal float AngularVelocityThresholdSquared
	{
		[CompilerGenerated]
		get
		{
			return c143d548430b479bc555a083c3d97291a;
		}
		[CompilerGenerated]
		private set
		{
			c143d548430b479bc555a083c3d97291a = value;
		}
	}

	/// <summary>
	///       Gets or sets the sleeping time threshold.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float TimeThreshold
	{
		get
		{
			return c12d7359316e29a54ec3137a028716bb9;
		}
		set
		{
			if (value < 0f)
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
							__ldtoken(SleepingSettings.set_TimeThreshold(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5364));
					}
				}
			}
			c12d7359316e29a54ec3137a028716bb9 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Settings.SleepingSettings" /> class.
	///       </summary>
	public SleepingSettings()
	{
		AngularVelocityThreshold = 0.5f;
		LinearVelocityThreshold = 0.4f;
		TimeThreshold = 1f;
	}
}
