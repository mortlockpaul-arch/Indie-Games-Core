using System;
using DigitalRune.Collections;

namespace DigitalRune.Geometry.Partitioning;

/// <summary>
///       Filters a pair of items.
///       </summary>
/// <typeparam name="T">The type of the items.</typeparam>
public interface IPairFilter<T>
{
	/// <summary>
	///       Occurs when the filter rules were changed.
	///       </summary>
	event EventHandler<EventArgs> Changed;

	/// <summary>
	///       Filters the specified item pair.
	///       </summary>
	/// <param name="pair">The pair.</param>
	/// <returns>
	///   <see langword="true" /> if the pair should be processed (pair is accepted); otherwise,
	///       <see langword="false" /> if the pair should not be processed (pair is rejected).
	///       </returns>
	bool Filter(Pair<T> pair);
}
