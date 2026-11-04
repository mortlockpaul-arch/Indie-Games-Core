using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Physics.Settings;

/// <summary>
///       Defines constraint-related simulation settings.
///       </summary>
public class ConstraintSettings
{
	private float c54c5a6c6e67d167b90020392a8372a13;

	private float ccac92bed40aebbc507105e8c881d36d6;

	private float ca0e64154687030c89a8fbe3b94a4b924;

	private int c935fb61d61df8a356a55a23321252c37;

	private float c261f1d5d6196838b200ba8f48c2b9aec;

	private float c2a0754abd7ea2e7e860387d24e444f50;

	private float cf71d3cfc20f9c54cd06db9c9927bf978;

	private float c9a879452eca0b9c5373de521db8c91ac;

	private float c7eb98f23735ffba0f1dc3ac8c58367a1;

	private float c83288a85cd51107b6009a8039d714798;

	private float ce7789823174300446968b79ff7983f24;

	private float cf41dd4187eb415b980de700b251bd5c7;

	private float cbe577b78713b6d396933a137c9034665;

	[CompilerGenerated]
	private float c2c00761ffbe82c52f2e85205f9c1e9f8;

	[CompilerGenerated]
	private bool c04e20aa2e47bcfc04af2519de04aef87;

	[CompilerGenerated]
	private float cc333c2cbb9ae60205d24dd89005dec84;

	/// <summary>
	///       Gets or sets the allowed penetration of <see cref="T:DigitalRune.Physics.Constraints.ContactConstraint" />s.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float AllowedPenetration
	{
		get
		{
			return c54c5a6c6e67d167b90020392a8372a13;
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
							__ldtoken(ConstraintSettings.set_AllowedPenetration(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2948));
					}
				}
			}
			c54c5a6c6e67d167b90020392a8372a13 = value;
		}
	}

	/// <summary>
	///       Gets or sets the allowed linear error of constraints.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float AllowedLinearDeviation
	{
		get
		{
			return ccac92bed40aebbc507105e8c881d36d6;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3057));
			}
			ccac92bed40aebbc507105e8c881d36d6 = value;
		}
	}

	/// <summary>
	///       Gets or sets the allowed angular error of constraints.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float AllowedAngularDeviation
	{
		get
		{
			return ca0e64154687030c89a8fbe3b94a4b924;
		}
		set
		{
			if (value < 0f)
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
							__ldtoken(ConstraintSettings.set_AllowedAngularDeviation(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3174));
					}
				}
			}
			ca0e64154687030c89a8fbe3b94a4b924 = value;
		}
	}

	/// <summary>
	///       Gets or sets the number of constraint iterations per time step.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public int NumberOfConstraintIterations
	{
		get
		{
			return c935fb61d61df8a356a55a23321252c37;
		}
		set
		{
			if (value <= 0)
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
							__ldtoken(ConstraintSettings.set_NumberOfConstraintIterations(int));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3293));
					}
				}
			}
			c935fb61d61df8a356a55a23321252c37 = value;
		}
	}

	/// <summary>
	///       Gets or sets the minimal constraint impulse.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float MinConstraintImpulse
	{
		get
		{
			return c261f1d5d6196838b200ba8f48c2b9aec;
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
							__ldtoken(ConstraintSettings.set_MinConstraintImpulse(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3398));
					}
				}
			}
			c261f1d5d6196838b200ba8f48c2b9aec = value;
			MinConstraintImpulseSquared = value * value;
		}
	}

	internal float MinConstraintImpulseSquared
	{
		[CompilerGenerated]
		get
		{
			return c2c00761ffbe82c52f2e85205f9c1e9f8;
		}
		[CompilerGenerated]
		private set
		{
			c2c00761ffbe82c52f2e85205f9c1e9f8 = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether constraints are randomly reordered during
	///       constraint solving.
	///       </summary>
	public bool RandomizeConstraints
	{
		[CompilerGenerated]
		get
		{
			return c04e20aa2e47bcfc04af2519de04aef87;
		}
		[CompilerGenerated]
		set
		{
			c04e20aa2e47bcfc04af2519de04aef87 = value;
		}
	}

	/// <summary>
	///       Gets or sets the stacking optimization factor. (Experimental)
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float StackingFactor
	{
		get
		{
			return c2a0754abd7ea2e7e860387d24e444f50;
		}
		set
		{
			if (value < 0f)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(ConstraintSettings.set_StackingFactor(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3511));
					}
				}
			}
			c2a0754abd7ea2e7e860387d24e444f50 = value;
		}
	}

	/// <summary>
	///       Gets or sets the stacking optimization tolerance. (Experimental)
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is less than 0 or greater than 1.
	///       </exception>
	public float StackingTolerance
	{
		get
		{
			return cf71d3cfc20f9c54cd06db9c9927bf978;
		}
		set
		{
			if (value < 0f || value > 1f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3612));
			}
			cf71d3cfc20f9c54cd06db9c9927bf978 = value;
		}
	}

	/// <summary>
	///       Gets or sets the contact error reduction parameter.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float ContactErrorReduction
	{
		get
		{
			return c9a879452eca0b9c5373de521db8c91ac;
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
							__ldtoken(ConstraintSettings.set_ContactErrorReduction(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3705));
					}
				}
			}
			c9a879452eca0b9c5373de521db8c91ac = value;
		}
	}

	/// <summary>
	///       Gets or sets the restitution threshold.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float RestitutionThreshold
	{
		get
		{
			return c7eb98f23735ffba0f1dc3ac8c58367a1;
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
							__ldtoken(ConstraintSettings.set_RestitutionThreshold(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3820));
					}
				}
			}
			c7eb98f23735ffba0f1dc3ac8c58367a1 = value;
		}
	}

	/// <summary>
	///       Gets or sets the resting velocity limit.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float RestingVelocityLimit
	{
		get
		{
			return c83288a85cd51107b6009a8039d714798;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3933));
			}
			c83288a85cd51107b6009a8039d714798 = value;
		}
	}

	/// <summary>
	///       Gets or sets the Baumgarte error correction ratio. (Experimental)
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or greater than 1.
	///       </exception>
	public float BaumgarteRatio
	{
		get
		{
			return ce7789823174300446968b79ff7983f24;
		}
		set
		{
			if (!(value < 0f))
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(ConstraintSettings.set_BaumgarteRatio(float));
				}
				if (!(value > 1f))
				{
					ce7789823174300446968b79ff7983f24 = value;
					return;
				}
			}
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4046));
		}
	}

	/// <summary>
	///       Gets or sets the maximal error correction velocity for general constraint errors.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float MaxErrorCorrectionVelocity
	{
		get
		{
			return cf41dd4187eb415b980de700b251bd5c7;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4133));
			}
			cf41dd4187eb415b980de700b251bd5c7 = value;
			MaxErrorCorrectionVelocitySquared = value * value;
		}
	}

	internal float MaxErrorCorrectionVelocitySquared
	{
		[CompilerGenerated]
		get
		{
			return cc333c2cbb9ae60205d24dd89005dec84;
		}
		[CompilerGenerated]
		private set
		{
			cc333c2cbb9ae60205d24dd89005dec84 = value;
		}
	}

	/// <summary>
	///       Gets or sets the maximal error correction velocity for correcting rigid body 
	///       interpenetrations.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float MaxPenetrationCorrectionVelocity
	{
		get
		{
			return cbe577b78713b6d396933a137c9034665;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4258));
			}
			cbe577b78713b6d396933a137c9034665 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Settings.ConstraintSettings" /> class.
	///       </summary>
	public ConstraintSettings()
	{
		AllowedPenetration = 0.01f;
		AllowedLinearDeviation = 0.01f;
		AllowedAngularDeviation = 0.04f;
		BaumgarteRatio = 1f;
		ContactErrorReduction = 0.2f;
		MaxErrorCorrectionVelocity = 100f;
		MaxPenetrationCorrectionVelocity = 2f;
		MinConstraintImpulse = 1E-06f;
		NumberOfConstraintIterations = 10;
		RandomizeConstraints = true;
		RestingVelocityLimit = 1f;
		RestitutionThreshold = 0.05f;
		StackingFactor = 0f;
		StackingTolerance = 0.1f;
	}
}
