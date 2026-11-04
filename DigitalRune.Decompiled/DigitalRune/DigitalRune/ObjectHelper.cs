using System;
using System.ComponentModel;
using System.Globalization;
using A;

namespace DigitalRune;

/// <summary>
///       Provides extension methods applicable to all objects.
///       </summary>
public static class ObjectHelper
{
	/// <summary>
	///       Parses the specified value.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <typeparam name="T">The target type.</typeparam>
	/// <returns>The parsed value.</returns>
	/// <exception cref="T:System.NotSupportedException">
	///       Cannot convert string to target type.
	///       </exception>
	public static T Parse<T>(string value)
	{
		return (T)Parse(typeof(T), value);
	}

	/// <summary>
	///       Parses a string and converts it to the specified type.
	///       </summary>
	/// <param name="type">The target type.</param>
	/// <param name="value">The value.</param>
	/// <returns>An instance of the given type that represents the string value.</returns>
	/// <exception cref="T:System.NotSupportedException">
	///       Cannot convert string to target type.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.
	///       </exception>
	public static object Parse(Type type, string value)
	{
		if ((object)type == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1802));
		}
		if ((object)type == typeof(string))
		{
			return value;
		}
		if (type.IsEnum)
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
						__ldtoken(ObjectHelper.Parse(Type, string));
					}
					return Enum.Parse(type, value, ignoreCase: true);
				}
			}
		}
		if (typeof(IConvertible).IsAssignableFrom(type))
		{
			return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}
		TypeConverter typeConverter = GetTypeConverter(type);
		if (typeConverter == null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6618));
				}
			}
		}
		return typeConverter.ConvertFromString(value);
	}

	/// <summary>
	///       Gets the type converter for the given type.
	///       </summary>
	/// <param name="type">The type.</param>
	/// <returns>
	///       A new type converter instance for the given type; or <see langword="null" /> if no type
	///       converter was found.
	///       </returns>
	public static TypeConverter GetTypeConverter(Type type)
	{
		TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)Attribute.GetCustomAttribute(type, typeof(TypeConverterAttribute), inherit: false);
		if (typeConverterAttribute != null)
		{
			try
			{
				Type type2 = Type.GetType(typeConverterAttribute.ConverterTypeName, throwOnError: false);
				if ((object)type2 != null)
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
								__ldtoken(ObjectHelper.GetTypeConverter(Type));
							}
							return Activator.CreateInstance(type2) as TypeConverter;
						}
					}
				}
			}
			catch
			{
			}
		}
		return null;
	}
}
