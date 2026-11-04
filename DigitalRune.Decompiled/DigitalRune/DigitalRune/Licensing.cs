using System;
using System.Collections.Generic;
using A;

namespace DigitalRune;

/// <summary>
///       Manages copy protection and licensing.
///       </summary>
public static class Licensing
{
	internal static List<string> cdc5dc301a2b0c93a09e61c8f609a5298 = new List<string>();

	/// <summary>
	///       Adds the specified serial number.
	///       </summary>
	/// <param name="serialNumber">A serial number (license key) for a DigitalRune product.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="serialNumber" /> is <see langword="null" />.
	///       </exception>
	public static void AddSerialNumber(string serialNumber)
	{
		if (serialNumber == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6373));
		}
		if (cdc5dc301a2b0c93a09e61c8f609a5298.Count > 20)
		{
			throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6398));
		}
		cdc5dc301a2b0c93a09e61c8f609a5298.Add(serialNumber);
	}
}
