using System;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       Describes a contact (or the closest points) of two objects.
///       </summary>
public class Contact
{
	private static readonly ResourcePool<Contact> c8aca4a0e3c0073ffb188fde832680587;

	private int ca22cd4f398ac59c5ae099b9e0b551c6a = -1;

	private int c0d88ea1a918c3a9cb376d4e8bf96c518 = -1;

	private Vector3F c875514674dce71fb1a6814fe2c63c57f = Vector3F.UnitY;

	[CompilerGenerated]
	private Vector3F cfad2b109ba2c6f891737b1ad92b7eb51;

	[CompilerGenerated]
	private Vector3F cd8caf23d1f2cfffa64c0bb381f2f21cf;

	[CompilerGenerated]
	private Vector3F c762f2f73a72de16bcfcb5b2964f0794d;

	[CompilerGenerated]
	private float c6708ba3209d21405ac542ed8783ff2d0;

	[CompilerGenerated]
	private double ce3f48980e866b3d34eea6ab74daa1226;

	[CompilerGenerated]
	private object c0df89c0ee57aa7bc1eed1e7eeab72eb6;

	[CompilerGenerated]
	private bool ca0e87417a1612e4ad2df75eea0e27793;

	[CompilerGenerated]
	private static Func<Contact> c49253b4520c1b7e183dfcc9c426c0283;

	/// <summary>
	///       Gets or sets the index of the shape feature of object A that was hit.
	///       </summary>
	public int FeatureA
	{
		get
		{
			return ca22cd4f398ac59c5ae099b9e0b551c6a;
		}
		set
		{
			ca22cd4f398ac59c5ae099b9e0b551c6a = value;
		}
	}

	/// <summary>
	///       Gets or sets the index of the shape feature of object B that was hit.
	///       </summary>
	public int FeatureB
	{
		get
		{
			return c0d88ea1a918c3a9cb376d4e8bf96c518;
		}
		set
		{
			c0d88ea1a918c3a9cb376d4e8bf96c518 = value;
		}
	}

	/// <summary>
	///       Gets or sets the contact position (in world space).
	///       </summary>
	public Vector3F Position
	{
		[CompilerGenerated]
		get
		{
			return cfad2b109ba2c6f891737b1ad92b7eb51;
		}
		[CompilerGenerated]
		set
		{
			cfad2b109ba2c6f891737b1ad92b7eb51 = value;
		}
	}

	/// <summary>
	///       Gets or sets the contact position on object A in the local space of object A.
	///       </summary>
	public Vector3F PositionALocal
	{
		[CompilerGenerated]
		get
		{
			return cd8caf23d1f2cfffa64c0bb381f2f21cf;
		}
		[CompilerGenerated]
		set
		{
			cd8caf23d1f2cfffa64c0bb381f2f21cf = value;
		}
	}

	/// <summary>
	///       Gets or sets the contact position on object B in the local space of object B.
	///       </summary>
	public Vector3F PositionBLocal
	{
		[CompilerGenerated]
		get
		{
			return c762f2f73a72de16bcfcb5b2964f0794d;
		}
		[CompilerGenerated]
		set
		{
			c762f2f73a72de16bcfcb5b2964f0794d = value;
		}
	}

	/// <summary>
	///       Gets the contact position on object A (in world space).
	///       </summary>
	public Vector3F PositionAWorld
	{
		get
		{
			if (IsRayHit)
			{
				return Position;
			}
			return Position + Normal * (PenetrationDepth / 2f);
		}
	}

	/// <summary>
	///       Gets the contact position on object B (in world space).
	///       </summary>
	public Vector3F PositionBWorld
	{
		get
		{
			if (IsRayHit)
			{
				return Position;
			}
			return Position - Normal * (PenetrationDepth / 2f);
		}
	}

	/// <summary>
	///       Gets or sets the normalized contact normal (pointing from object A to object B; in world space).
	///       </summary>
	public Vector3F Normal
	{
		get
		{
			return c875514674dce71fb1a6814fe2c63c57f;
		}
		set
		{
			c875514674dce71fb1a6814fe2c63c57f = value;
		}
	}

	/// <summary>
	///       Gets or sets the penetration depth.
	///       </summary>
	public float PenetrationDepth
	{
		[CompilerGenerated]
		get
		{
			return c6708ba3209d21405ac542ed8783ff2d0;
		}
		[CompilerGenerated]
		set
		{
			c6708ba3209d21405ac542ed8783ff2d0 = value;
		}
	}

	/// <summary>
	///       Gets or sets the lifetime of this contact (in seconds).
	///       </summary>
	public double Lifetime
	{
		[CompilerGenerated]
		get
		{
			return ce3f48980e866b3d34eea6ab74daa1226;
		}
		[CompilerGenerated]
		set
		{
			ce3f48980e866b3d34eea6ab74daa1226 = value;
		}
	}

	/// <summary>
	///       Gets or sets the user data.
	///       </summary>
	public object UserData
	{
		[CompilerGenerated]
		get
		{
			return c0df89c0ee57aa7bc1eed1e7eeab72eb6;
		}
		[CompilerGenerated]
		set
		{
			c0df89c0ee57aa7bc1eed1e7eeab72eb6 = value;
		}
	}

	/// <summary>
	///       Gets a value indicating whether this contact is a hit by a ray.
	///       </summary>
	public bool IsRayHit
	{
		[CompilerGenerated]
		get
		{
			return ca0e87417a1612e4ad2df75eea0e27793;
		}
		[CompilerGenerated]
		set
		{
			ca0e87417a1612e4ad2df75eea0e27793 = value;
		}
	}

	/// <summary>
	///       Gets a copy of the contact where the collision objects are swapped.
	///       </summary>
	public Contact Swapped
	{
		get
		{
			Contact contact = Create();
			contact.PositionALocal = PositionBLocal;
			contact.PositionBLocal = PositionALocal;
			contact.Normal = -Normal;
			contact.Position = Position;
			contact.PenetrationDepth = PenetrationDepth;
			contact.Lifetime = Lifetime;
			contact.UserData = UserData;
			contact.FeatureA = FeatureB;
			contact.FeatureB = FeatureA;
			contact.IsRayHit = IsRayHit;
			return contact;
		}
	}

	static Contact()
	{
		c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<Contact>(() => new Contact(), null, null);
		TypeInitializer.Initialize(ccb48cf63175e6724337dcebf74566228.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(12, ccb48cf63175e6724337dcebf74566228.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	private Contact()
	{
	}

	/// <summary>
	///       Creates an instance of the <see cref="T:DigitalRune.Geometry.Collisions.Contact" /> class. (This method 
	///       reuses a previously recycled instance or allocates a new instance if necessary.)
	///       </summary>
	/// <returns>
	///       A new or reusable instance of the <see cref="T:DigitalRune.Geometry.Collisions.Contact" /> class.
	///       </returns>
	public static Contact Create()
	{
		return c8aca4a0e3c0073ffb188fde832680587.Obtain();
	}

	public void Recycle()
	{
		cf06348a19a922c6295282d49671cbe77();
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	[Obsolete("The static method Recycle() is obsolete. Use the instance method instead.")]
	public static void Recycle(Contact contact)
	{
		contact.cf06348a19a922c6295282d49671cbe77();
		c8aca4a0e3c0073ffb188fde832680587.Recycle(contact);
	}

	internal void cf06348a19a922c6295282d49671cbe77()
	{
		FeatureA = -1;
		FeatureB = -1;
		Lifetime = 0.0;
		UserData = null;
		IsRayHit = false;
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(16187), Position, Normal, PenetrationDepth, Lifetime);
	}

	[CompilerGenerated]
	private static Contact c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new Contact();
	}
}
