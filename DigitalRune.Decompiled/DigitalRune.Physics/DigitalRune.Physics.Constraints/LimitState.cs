using System;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Describe the state of a constraint limit.
///       </summary>
[Flags]
public enum LimitState
{
	/// <summary>
	///       The limit is inactive. The bodies are in an allowed position.
	///       </summary>
	Inactive = 0,
	/// <summary>
	///       The minimal limit is reached.
	///       </summary>
	Min = 1,
	/// <summary>
	///       The maximal limit is reached.
	///       </summary>
	Max = 2,
	/// <summary>
	///       The constraint is locked and does not allow relative movement on this constraint axis.
	///       </summary>
	Locked = Min | Max
}
