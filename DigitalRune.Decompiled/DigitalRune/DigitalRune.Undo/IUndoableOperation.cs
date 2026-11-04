namespace DigitalRune.Undo;

/// <summary>
///       Represents an operation that supports Undo/Redo.
///       </summary>
public interface IUndoableOperation
{
	/// <summary>
	///       Gets the description of the operation.
	///       </summary>
	object Description { get; }

	/// <summary>
	///       Undoes operation.
	///       </summary>
	void Undo();

	/// <summary>
	///       Performs/Redoes operation.
	///       </summary>
	void Do();
}
