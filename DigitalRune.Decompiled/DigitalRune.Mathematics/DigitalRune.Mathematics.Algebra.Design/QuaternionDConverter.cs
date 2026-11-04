using System;
using System.ComponentModel;
using System.Globalization;

namespace DigitalRune.Mathematics.Algebra.Design;

/// <summary>
///       Converts a <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> to and from string representation.
///       </summary>
public class QuaternionDConverter : ExpandableObjectConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Design.QuaternionDConverter" /> class</summary>
	public QuaternionDConverter()
	{
	}

	/// <summary>
	///       Returns whether this converter can convert an object of the given type 
	///       to the type of this converter, using the specified context.     
	///       </summary>
	/// <param name="context">
	///       An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. 
	///       </param>
	/// <param name="sourceType">
	///       A <see cref="T:System.Type" /> that represents the type you want to convert from. 
	///       </param>
	/// <returns>
	///   <see langword="true" /> if this converter can perform the conversion; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if ((object)sourceType == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	/// <summary>
	///       Returns whether this converter can convert the object to the specified 
	///       type, using the specified context. 
	///       </summary>
	/// <param name="context">
	///       An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. 
	///       </param>
	/// <param name="destinationType">
	///       A <see cref="T:System.Type" /> that represents the type you want to convert to. 
	///       </param>
	/// <returns>
	///   <see langword="true" /> if this converter can perform the conversion; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if ((object)destinationType == typeof(string))
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
						__ldtoken(QuaternionDConverter.CanConvertTo(ITypeDescriptorContext, Type));
					}
					return true;
				}
			}
		}
		return base.CanConvertTo(context, destinationType);
	}

	/// <summary>
	///       Converts the given object to the type of this converter, using the 
	///       specified context and culture information. 
	///       </summary>
	/// <param name="context">
	///       An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. 
	///       </param>
	/// <param name="culture">
	///       The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.
	///       </param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value is string)
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
						__ldtoken(QuaternionDConverter.ConvertFrom(ITypeDescriptorContext, CultureInfo, object));
					}
					return QuaternionD.Parse((string)value, culture);
				}
			}
		}
		return base.ConvertFrom(context, culture, value);
	}

	/// <summary>
	///       Converts the given value object to the specified type, using the 
	///       specified context and culture information. 
	///       </summary>
	/// <param name="context">
	///       An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. 
	///       </param>
	/// <param name="culture">
	///       The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.
	///       </param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	/// <param name="destinationType">
	///       A <see cref="T:System.Type" /> that represents the type you want to convert to. 
	///       </param>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if ((object)destinationType == typeof(string))
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(QuaternionDConverter.ConvertTo(ITypeDescriptorContext, CultureInfo, object, Type));
			}
			if (value is QuaternionD)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						return ((QuaternionD)value).ToString(culture);
					}
				}
			}
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
