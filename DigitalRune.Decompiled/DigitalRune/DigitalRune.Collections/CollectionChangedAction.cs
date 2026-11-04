namespace DigitalRune.Collections;

/// <summary>
///       Describes the action that caused a <see cref="E:DigitalRune.Collections.NotifyingCollection`1.CollectionChanged" /> 
///       event.
///       </summary>
public enum CollectionChangedAction
{
	/// <summary>
	///       New items were added to the collection.
	///       </summary>
	Add,
	/// <summary>
	///       Items were removed from the collection.
	///       </summary>
	Remove,
	/// <summary>
	///       One item was replaced in the collection.
	///       </summary>
	Replace,
	/// <summary>
	///       All items were removed from the collection.
	///       </summary>
	Clear,
	/// <summary>
	///       An item was moved within the collection.
	///       </summary>
	Move
}
