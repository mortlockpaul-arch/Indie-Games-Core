using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Physics.Settings;

/// <summary>
///       Defines motion-related simulation settings.
///       </summary>
public class MotionSettings
{
	private float c39011b2c201c95ea9ebd9a0b71af1ff1;

	private float cd92b26048531d9703d896b015d5833d5;

	private float cec5c0f725fac4596af67deb766dadfcf;

	[CompilerGenerated]
	private bool c438affc5acefa3fd9a561a27a63df697;

	[CompilerGenerated]
	private Func<RigidBody, RigidBody, bool> cfb96594ec3156d5377ce41a6ee77dd45;

	[CompilerGenerated]
	private float c6dae39f755f34e41c9b96f03f6b7a22e;

	[CompilerGenerated]
	private float c89ea136e6d3f940e4ca4f19d40385e5c;

	[CompilerGenerated]
	private float c859221baae2898c7ec6b2b9a644f6522;

	[CompilerGenerated]
	private bool c71e0dbbac74b7f38dc1b98d55991268e;

	/// <summary>
	///       Gets or sets a value indicating whether Continuous Collision Detection (CCD) is enabled.
	///       </summary>
	public bool CcdEnabled
	{
		[CompilerGenerated]
		get
		{
			return c438affc5acefa3fd9a561a27a63df697;
		}
		[CompilerGenerated]
		set
		{
			c438affc5acefa3fd9a561a27a63df697 = value;
		}
	}

	/// <summary>
	///       Gets or sets a predicate method that defines whether CCD is enabled between a pair of rigid 
	///       bodies.
	///       </summary>
	public Func<RigidBody, RigidBody, bool> CcdFilter
	{
		[CompilerGenerated]
		get
		{
			return cfb96594ec3156d5377ce41a6ee77dd45;
		}
		[CompilerGenerated]
		set
		{
			cfb96594ec3156d5377ce41a6ee77dd45 = value;
		}
	}

	/// <summary>
	///       Gets or sets the velocity threshold for Continuous Collision Detection (CCD).
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float CcdVelocityThreshold
	{
		get
		{
			return c39011b2c201c95ea9ebd9a0b71af1ff1;
		}
		set
		{
			if (value < 0f)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(MotionSettings.set_CcdVelocityThreshold(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1000));
					}
				}
			}
			c39011b2c201c95ea9ebd9a0b71af1ff1 = value;
			CcdVelocityThresholdSquared = value * value;
		}
	}

	internal float CcdVelocityThresholdSquared
	{
		[CompilerGenerated]
		get
		{
			return c6dae39f755f34e41c9b96f03f6b7a22e;
		}
		[CompilerGenerated]
		private set
		{
			c6dae39f755f34e41c9b96f03f6b7a22e = value;
		}
	}

	/// <summary>
	///       Gets or sets the maximal linear velocity.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float MaxLinearVelocity
	{
		get
		{
			return cd92b26048531d9703d896b015d5833d5;
		}
		set
		{
			if (value < 0f)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(MotionSettings.set_MaxLinearVelocity(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1113));
					}
				}
			}
			cd92b26048531d9703d896b015d5833d5 = value;
			MaxLinearVelocitySquared = value * value;
		}
	}

	internal float MaxLinearVelocitySquared
	{
		[CompilerGenerated]
		get
		{
			return c89ea136e6d3f940e4ca4f19d40385e5c;
		}
		[CompilerGenerated]
		private set
		{
			c89ea136e6d3f940e4ca4f19d40385e5c = value;
		}
	}

	/// <summary>
	///       Gets or sets the maximal angular velocity.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float MaxAngularVelocity
	{
		get
		{
			return cec5c0f725fac4596af67deb766dadfcf;
		}
		set
		{
			if (value < 0f)
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
							__ldtoken(MotionSettings.set_MaxAngularVelocity(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1220));
					}
				}
			}
			cec5c0f725fac4596af67deb766dadfcf = value;
			MaxAngularVelocitySquared = value * value;
		}
	}

	internal float MaxAngularVelocitySquared
	{
		[CompilerGenerated]
		get
		{
			return c859221baae2898c7ec6b2b9a644f6522;
		}
		[CompilerGenerated]
		private set
		{
			c859221baae2898c7ec6b2b9a644f6522 = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether bodies that leave the simulation are automatically
	///       removed.
	///       </summary>
	public bool RemoveBodiesOutsideWorld
	{
		[CompilerGenerated]
		get
		{
			return c71e0dbbac74b7f38dc1b98d55991268e;
		}
		[CompilerGenerated]
		set
		{
			c71e0dbbac74b7f38dc1b98d55991268e = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Settings.MotionSettings" /> class.
	///       </summary>
	public MotionSettings()
	{
		CcdEnabled = true;
		CcdVelocityThreshold = 5f;
		MaxLinearVelocity = 100f;
		MaxAngularVelocity = 100f;
		RemoveBodiesOutsideWorld = true;
	}
}
