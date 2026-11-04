using System;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Mathematics;

namespace DigitalRune.Physics.Materials;

/// <summary>
///       Computes the material properties for two materials in contact using simple mathematical
///       operations.
///       </summary>
public class MaterialPropertyCombiner : IMaterialPropertyCombiner
{
	/// <summary>
	///       Defines how two coefficients are combined.
	///       </summary>
	public enum Mode
	{
		/// <summary>Use the sum of the two coefficients.</summary>
		Add,
		/// <summary>Use the minimum of the two coefficients.</summary>
		Min,
		/// <summary>Use the maximum of the two coefficients.</summary>
		Max,
		/// <summary>Use the arithmetic mean (average) of the two coefficients.</summary>
		ArithmeticMean,
		/// <summary>Use the geometric mean of the two coefficients.</summary>
		GeometricMean,
		/// <summary>Use the product of the two coefficients.</summary>
		Multiply
	}

	private float c246c70e95cd3ba8ef35350e28b679066;

	[CompilerGenerated]
	private Mode c46b5a189188653de33f00ad972ccfb5f;

	[CompilerGenerated]
	private float ca7993896f0784c1ff5662e0b3ab24beb;

	[CompilerGenerated]
	private float c956aa2d59bde764cde0d12edb8c135a8;

	[CompilerGenerated]
	private Mode c305dc813f4fca89de021142d9df70c8a;

	/// <summary>
	///       Gets or sets the mode that is used to combine friction coefficients.
	///       </summary>
	public Mode FrictionMode
	{
		[CompilerGenerated]
		get
		{
			return c46b5a189188653de33f00ad972ccfb5f;
		}
		[CompilerGenerated]
		set
		{
			c46b5a189188653de33f00ad972ccfb5f = value;
		}
	}

	/// <summary>
	///       Gets or sets the minimal friction value.
	///       </summary>
	public float MinFriction
	{
		[CompilerGenerated]
		get
		{
			return ca7993896f0784c1ff5662e0b3ab24beb;
		}
		[CompilerGenerated]
		set
		{
			ca7993896f0784c1ff5662e0b3ab24beb = value;
		}
	}

	/// <summary>
	///       Gets or sets the maximal friction value.
	///       </summary>
	public float MaxFriction
	{
		[CompilerGenerated]
		get
		{
			return c956aa2d59bde764cde0d12edb8c135a8;
		}
		[CompilerGenerated]
		set
		{
			c956aa2d59bde764cde0d12edb8c135a8 = value;
		}
	}

	/// <summary>
	///       Gets or sets the mode that is used to combine coefficients of restitution.
	///       </summary>
	public Mode RestitutionMode
	{
		[CompilerGenerated]
		get
		{
			return c305dc813f4fca89de021142d9df70c8a;
		}
		[CompilerGenerated]
		set
		{
			c305dc813f4fca89de021142d9df70c8a = value;
		}
	}

	/// <summary>
	///       Gets or sets the maximal restitution value.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float MaxRestitution
	{
		get
		{
			return c246c70e95cd3ba8ef35350e28b679066;
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
							__ldtoken(MaterialPropertyCombiner.set_MaxRestitution(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(571));
					}
				}
			}
			c246c70e95cd3ba8ef35350e28b679066 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Materials.MaterialPropertyCombiner" /> class.
	///       </summary>
	public MaterialPropertyCombiner()
	{
		FrictionMode = Mode.GeometricMean;
		MinFriction = 0f;
		MaxFriction = 10f;
		RestitutionMode = Mode.Multiply;
		MaxRestitution = 1f;
	}

	/// <summary>
	///       Computes the combined friction coefficient.
	///       </summary>
	/// <param name="frictionA">The first friction coefficient.</param>
	/// <param name="frictionB">The second friction coefficient.</param>
	/// <returns>
	///       The combined friction coefficient.
	///       </returns>
	public float CombineFriction(float frictionA, float frictionB)
	{
		return MathHelper.Clamp(FrictionMode switch
		{
			Mode.Add => frictionA + frictionB, 
			Mode.Min => Math.Min(frictionA, frictionB), 
			Mode.Max => Math.Max(frictionA, frictionB), 
			Mode.Multiply => frictionA * frictionB, 
			Mode.ArithmeticMean => 0.5f * (frictionA + frictionB), 
			Mode.GeometricMean => (float)Math.Sqrt(frictionA * frictionB), 
			_ => throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(672)), 
		}, MinFriction, MaxFriction);
	}

	/// <summary>
	///       Computes the combined coefficient of restitution.
	///       </summary>
	/// <param name="restitutionA">The first coefficient of restitution.</param>
	/// <param name="restitutionB">The second coefficient of restitution.</param>
	/// <returns>
	///       The combined coefficient of restitution.
	///       </returns>
	public float CombineRestitution(float restitutionA, float restitutionB)
	{
		return MathHelper.Clamp(RestitutionMode switch
		{
			Mode.Add => restitutionA + restitutionB, 
			Mode.Min => Math.Min(restitutionA, restitutionB), 
			Mode.Max => Math.Max(restitutionA, restitutionB), 
			Mode.Multiply => restitutionA * restitutionB, 
			Mode.ArithmeticMean => 0.5f * (restitutionA + restitutionB), 
			Mode.GeometricMean => (float)Math.Sqrt(restitutionA * restitutionB), 
			_ => throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(672)), 
		}, 0f, MaxRestitution);
	}
}
