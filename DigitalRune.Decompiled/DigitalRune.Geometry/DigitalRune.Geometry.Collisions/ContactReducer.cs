using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       Removes contacts if a <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> contains more than 4 <see cref="T:DigitalRune.Geometry.Collisions.Contact" />s.
///       </summary>
public class ContactReducer : IContactFilter
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.ContactReducer" /> class</summary>
	public ContactReducer()
	{
	}

	/// <summary>
	///       Filters the specified contact set.
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	public void Filter(ContactSet contactSet)
	{
		if (contactSet == null)
		{
			return;
		}
		int count = contactSet.Count;
		if (count <= 4)
		{
			return;
		}
		int num = 0;
		float penetrationDepth = contactSet[0].PenetrationDepth;
		for (int i = 1; i < count; i++)
		{
			Contact contact = contactSet[i];
			if (contact.PenetrationDepth > penetrationDepth)
			{
				penetrationDepth = contact.PenetrationDepth;
				num = i;
			}
		}
		if (num != 0)
		{
			Contact value = contactSet[num];
			contactSet[num] = contactSet[0];
			contactSet[0] = value;
		}
		while (contactSet.Count > 4)
		{
			cc22df018b4f405eb78d4ecf049a7bd4d(contactSet);
		}
	}

	private static void cc22df018b4f405eb78d4ecf049a7bd4d(ContactSet cfc73391dcf53c37de15d54e2434d0199)
	{
		Vector3F vector = cfc73391dcf53c37de15d54e2434d0199[2].Position - cfc73391dcf53c37de15d54e2434d0199[1].Position;
		Vector3F vector3F = cfc73391dcf53c37de15d54e2434d0199[3].Position - cfc73391dcf53c37de15d54e2434d0199[1].Position;
		Vector3F vector2 = cfc73391dcf53c37de15d54e2434d0199[4].Position - cfc73391dcf53c37de15d54e2434d0199[1].Position;
		Vector3F vector3 = cfc73391dcf53c37de15d54e2434d0199[3].Position - cfc73391dcf53c37de15d54e2434d0199[2].Position;
		Vector3F vector4 = cfc73391dcf53c37de15d54e2434d0199[4].Position - cfc73391dcf53c37de15d54e2434d0199[2].Position;
		float lengthSquared = Vector3F.Cross(vector, vector3F).LengthSquared;
		float num = lengthSquared;
		int num2 = 4;
		lengthSquared = Vector3F.Cross(vector, vector2).LengthSquared;
		if (lengthSquared > num)
		{
			num = lengthSquared;
			num2 = 3;
		}
		lengthSquared = Vector3F.Cross(vector3F, vector2).LengthSquared;
		if (lengthSquared > num)
		{
			num = lengthSquared;
			num2 = 2;
		}
		lengthSquared = Vector3F.Cross(vector3, vector4).LengthSquared;
		if (lengthSquared > num)
		{
			num2 = 1;
		}
		cfc73391dcf53c37de15d54e2434d0199[num2].Recycle();
		int num3 = cfc73391dcf53c37de15d54e2434d0199.Count - 1;
		if (num2 != num3)
		{
			cfc73391dcf53c37de15d54e2434d0199[num2] = cfc73391dcf53c37de15d54e2434d0199[num3];
		}
		cfc73391dcf53c37de15d54e2434d0199.RemoveAt(num3);
	}
}
