namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       Defines the mode of the continuous collision detection.
///       </summary>
public enum ContinuousCollisionDetectionMode
{
	/// <summary>
	///       The continuous collision detection considers the linear and the rotational movement of the 
	///       objects involved. (Slower, but more accurate.)
	///       </summary>
	Full,
	/// <summary>
	///       The continuous collision detection considers only the linear movement of the objects 
	///       involved. The rotational movement is ignored. (Faster, but less accurate.)
	///       </summary>
	Linear
}
