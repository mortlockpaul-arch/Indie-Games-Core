using System;

namespace SynapseGaming.LightingSystem.Editor;

/// <summary>
/// Attribute for numeric properties to define numberpad specific control options.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class EditorNumberPadOptionsAttribute : BaseControlOptionsAttribute
{
	private int fNB;

	private double fNa;

	private double fNg;

	private double fNf;

	internal int DecimalPlaces => fNB;

	internal double MinValue => fNa;

	internal double MaxValue => fNg;

	internal double Increment => fNf;

	/// <summary>
	/// Creates a new EditorNumberPadOptionsAttribute instance.
	/// </summary>
	/// <param name="decimalplaces">The number of decimal places to show in the numberpad control.</param>
	/// <param name="minvalue">The minimum allowed value for the property.</param>
	/// <param name="maxvalue">The maximum allowed value for the property.</param>
	/// <param name="increment">The amount to increase/decrease by when the user cycles through the numberpad.</param>
	public EditorNumberPadOptionsAttribute(int decimalplaces, double minvalue, double maxvalue, double increment)
	{
		fNB = decimalplaces;
		fNa = minvalue;
		fNg = maxvalue;
		fNf = increment;
	}
}
