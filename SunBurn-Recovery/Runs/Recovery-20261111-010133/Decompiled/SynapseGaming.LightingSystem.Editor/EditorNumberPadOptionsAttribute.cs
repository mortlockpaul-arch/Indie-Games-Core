using System;

namespace SynapseGaming.LightingSystem.Editor;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class EditorNumberPadOptionsAttribute : BaseControlOptionsAttribute
{
	private int l8a;

	private double l8_u0013_;

	private double l8N;

	private double l8l;

	internal int DecimalPlaces => l8a;

	internal double MinValue => l8_u0013_;

	internal double MaxValue => l8N;

	internal double Increment => l8l;

	public EditorNumberPadOptionsAttribute(int decimalplaces, double minvalue, double maxvalue, double increment)
	{
		l8a = decimalplaces;
		l8_u0013_ = minvalue;
		l8N = maxvalue;
		l8l = increment;
	}
}
