using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune;

/// <summary>
///       Auxiliary methods for enumerations.
///       </summary>
public static class EnumHelper
{
	[CompilerGenerated]
	private sealed class c5669289e9153587a85743ae9690ceb08
	{
		public Type c22c9994a45ba694bd1eb2b61f9fd8c7b;

		public object cfbc51a74b1020ece6382cc340ecf56bc(FieldInfo c50f6834b19c732dc00e400bf82a4068a)
		{
			return c50f6834b19c732dc00e400bf82a4068a.GetValue(c22c9994a45ba694bd1eb2b61f9fd8c7b);
		}
	}

	[CompilerGenerated]
	private static Func<FieldInfo, bool> c2cd722d7de416d829aaf20a315c6553b;

	/// <summary>
	///       Retrieves an array of the values of the constants in a specified enumeration.
	///       </summary>
	/// <param name="enumType">An enumeration type.</param>
	/// <returns>
	///       An array of the enumeration values in <paramref name="enumType" />. 
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="enumType" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.
	///       </exception>
	public static object[] GetValues(Type enumType)
	{
		if ((object)enumType == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2228));
		}
		if (!enumType.IsEnum)
		{
			throw new ArgumentException(string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2245), enumType.Name));
		}
		return (from c50f6834b19c732dc00e400bf82a4068a in enumType.GetFields()
			where c50f6834b19c732dc00e400bf82a4068a.IsLiteral
			select c50f6834b19c732dc00e400bf82a4068a.GetValue(enumType)).ToArray();
	}

	/// <summary>
	///       Converts the string representation of the name or numeric value of one or more enumerated 
	///       constants to an equivalent enumerated object.
	///       </summary>
	/// <param name="s">The string representation of the name or numeric value.</param>
	/// <param name="ignoreCase">
	///       If set to <see langword="true" /> ignore case; otherwise, regard case.
	///       </param>
	/// <param name="value">The converted enumeration value.</param>
	/// <typeparam name="T">The type of enumeration.</typeparam>
	/// <returns>
	///   <see langword="true" /> if the string was converted successfully; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool TryParse<T>(string s, bool ignoreCase, out T value)
	{
		try
		{
			value = (T)Enum.Parse(typeof(T), s, ignoreCase);
			return true;
		}
		catch (Exception)
		{
			value = default;
			return false;
		}
	}

	[CompilerGenerated]
	private static bool c1a65b7660a026ffd5cd64bdebc10a92e(FieldInfo c50f6834b19c732dc00e400bf82a4068a)
	{
		return c50f6834b19c732dc00e400bf82a4068a.IsLiteral;
	}
}
