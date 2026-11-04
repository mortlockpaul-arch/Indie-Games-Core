namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       Defines the type of collision object.
///       </summary>
public enum CollisionObjectType
{
	/// <summary>
	///       A normal collision object. When this type of collision object collides with another 
	///       collision object of type <see cref="F:DigitalRune.Geometry.Collisions.CollisionObjectType.Default" />, the full contact details (contact points, 
	///       normal vectors, penetration depths, etc.) are computed and stored in the 
	///       <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />s.
	///       </summary>
	Default,
	/// <summary>
	///       A collision object that is used as a trigger and does not need contact details. When this 
	///       type of collision object collides with another collision object, contact details (contact 
	///       points, normal vectors, penetration depths, etc.) will be omitted. The flag 
	///       <see cref="P:DigitalRune.Geometry.Collisions.ContactSet.HaveContact" /> will be set in the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />s for this 
	///       object. But the the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />s will not contain any <see cref="T:DigitalRune.Geometry.Collisions.Contact" />s.
	///       Use trigger collision objects instead of default collision objects to improve performance 
	///       if contact details are not required.
	///       </summary>
	Trigger
}
