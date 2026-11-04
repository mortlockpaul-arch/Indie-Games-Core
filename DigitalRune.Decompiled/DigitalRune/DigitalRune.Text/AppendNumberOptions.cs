using System;

namespace DigitalRune.Text;

/// <summary>
///       Options for <see cref="T:System.Text.StringBuilder" /> extension methods.
///       </summary>
[Flags]
public enum AppendNumberOptions
{
	/// <summary>
	///        Normal format.
	///       </summary>
	None = 0,
	/// <summary>
	///        Added "+" sign for positive value.
	///       </summary>
	PositiveSign = 1,
	/// <summary>
	///        Insert number group separation characters. In use, added "," for every 3 digits.
	///       </summary>
	NumberGroup = 2
}
