using System;
using System.Runtime.CompilerServices;
using DigitalRune.Physics.Materials;

namespace DigitalRune.Physics.Settings;

/// <summary>
///       Defines simulation settings that control the physics simulation.
///       </summary>
public class SimulationSettings
{
	private ConstraintSettings c719741ad5138547824c9ab491e7ec0f5;

	private MotionSettings ce502dbf7190730f0d36296fd67cafec4;

	private SleepingSettings cc8623bca706d85896b8364ac7ea99ca3;

	private TimingSettings c34e279156cb1bf3eff39aa874590d5ff;

	private IMaterialPropertyCombiner ce275dee01bbcc4828171878077005097;

	[CompilerGenerated]
	private bool cdebab1aab2704b0f85ba208c682c1a31;

	[CompilerGenerated]
	private bool ce49667347ed791b35f1f687cdde14f9a;

	/// <summary>
	///       Gets or sets the constraint-related simulation settings.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public ConstraintSettings Constraints
	{
		get
		{
			return c719741ad5138547824c9ab491e7ec0f5;
		}
		set
		{
			if (value == null)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(SimulationSettings.set_Constraints(ConstraintSettings));
						}
						throw new ArgumentNullException();
					}
				}
			}
			c719741ad5138547824c9ab491e7ec0f5 = value;
		}
	}

	/// <summary>
	///       Gets or sets the motion-related simulation settings.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public MotionSettings Motion
	{
		get
		{
			return ce502dbf7190730f0d36296fd67cafec4;
		}
		set
		{
			if (value == null)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(SimulationSettings.set_Motion(MotionSettings));
						}
						throw new ArgumentNullException();
					}
				}
			}
			ce502dbf7190730f0d36296fd67cafec4 = value;
		}
	}

	/// <summary>
	///       Gets or sets the sleeping-related simulation settings.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public SleepingSettings Sleeping
	{
		get
		{
			return cc8623bca706d85896b8364ac7ea99ca3;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			cc8623bca706d85896b8364ac7ea99ca3 = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether multithreading is enabled.
	///       </summary>
	public bool EnableMultithreading
	{
		[CompilerGenerated]
		get
		{
			return cdebab1aab2704b0f85ba208c682c1a31;
		}
		[CompilerGenerated]
		set
		{
			cdebab1aab2704b0f85ba208c682c1a31 = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether the collision domain is kept up-to-date.
	///       </summary>
	public bool SynchronizeCollisionDomain
	{
		[CompilerGenerated]
		get
		{
			return ce49667347ed791b35f1f687cdde14f9a;
		}
		[CompilerGenerated]
		set
		{
			ce49667347ed791b35f1f687cdde14f9a = value;
		}
	}

	/// <summary>
	///       Gets or sets the timing-related simulation settings.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public TimingSettings Timing
	{
		get
		{
			return c34e279156cb1bf3eff39aa874590d5ff;
		}
		set
		{
			if (value == null)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(SimulationSettings.set_Timing(TimingSettings));
						}
						throw new ArgumentNullException();
					}
				}
			}
			c34e279156cb1bf3eff39aa874590d5ff = value;
		}
	}

	/// <summary>
	///       Gets or sets the material property combiner.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public IMaterialPropertyCombiner MaterialPropertyCombiner
	{
		get
		{
			return ce275dee01bbcc4828171878077005097;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			ce275dee01bbcc4828171878077005097 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Settings.SimulationSettings" /> class.
	///       </summary>
	public SimulationSettings()
	{
		Constraints = new ConstraintSettings();
		Motion = new MotionSettings();
		Sleeping = new SleepingSettings();
		Timing = new TimingSettings();
		MaterialPropertyCombiner = new MaterialPropertyCombiner();
		EnableMultithreading = Environment.ProcessorCount > 1;
	}
}
