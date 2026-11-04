namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       A filter which processes contacts in a contact set.
///       </summary>
public interface IContactFilter
{
	/// <summary>
	///       Filters the specified contact set.
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	void Filter(ContactSet contactSet);
}
