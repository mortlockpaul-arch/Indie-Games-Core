using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Shapes;

namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       Represents an object which can collide with other objects.
///       </summary>
public class CollisionObject
{
	private enum c1c3087dbe97c0e65d1b19a976416b744
	{
		cd7a78bd43074e324847007105e636193,
		c1e75be3e596c1795b2b13abdc5440efb,
		ce38a2d521d8f8e1030e9a39815c9d087
	}

	private c1c3087dbe97c0e65d1b19a976416b744 c3626f9fd3ee4068556f67fec98f8cf9d;

	private CollisionDomain c5d1ff84a7eafda5b7365e8fe4c32b7d9;

	private IGeometricObject c478123814d6e8ad075dde45fc0f9a623;

	private CollisionObjectType c7c9af702452497cba0063de94eb6e293;

	[CompilerGenerated]
	private bool c5d4fd01386fb89cdf48dd8128fbd0243;

	[CompilerGenerated]
	private int cc38f69c3dda5612cf05b8c32daf49853;

	[CompilerGenerated]
	private bool ca756fb27da26facec9281ebca64c1de1;

	/// <summary>
	///       Gets or sets the <see cref="P:DigitalRune.Geometry.Collisions.CollisionObject.Domain" />.
	///       </summary>
	public CollisionDomain Domain
	{
		get
		{
			return c5d1ff84a7eafda5b7365e8fe4c32b7d9;
		}
		internal set
		{
			c5d1ff84a7eafda5b7365e8fe4c32b7d9 = value;
			Changed = true;
		}
	}

	/// <summary>
	///       Gets or sets the geometric object.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.</exception>
	public IGeometricObject GeometricObject
	{
		get
		{
			return c478123814d6e8ad075dde45fc0f9a623;
		}
		set
		{
			if (c5d1ff84a7eafda5b7365e8fe4c32b7d9 != null)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(CollisionObject.set_GeometricObject(IGeometricObject));
				}
				if (value == null)
				{
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8040));
				}
			}
			if (c478123814d6e8ad075dde45fc0f9a623 == value)
			{
				return;
			}
			if (c478123814d6e8ad075dde45fc0f9a623 != null)
			{
				c478123814d6e8ad075dde45fc0f9a623.ShapeChanged -= c8d854b6330cdc452eac9c19732fabc3c;
				c478123814d6e8ad075dde45fc0f9a623.PoseChanged -= c3c12ccb0e0feb2f78a298059c5a1223b;
			}
			c478123814d6e8ad075dde45fc0f9a623 = value;
			if (c478123814d6e8ad075dde45fc0f9a623 != null)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				c478123814d6e8ad075dde45fc0f9a623.ShapeChanged += c8d854b6330cdc452eac9c19732fabc3c;
				c478123814d6e8ad075dde45fc0f9a623.PoseChanged += c3c12ccb0e0feb2f78a298059c5a1223b;
			}
			c8d854b6330cdc452eac9c19732fabc3c(null, ShapeChangedEventArgs.Empty);
			Changed = true;
		}
	}

	internal bool Changed
	{
		[CompilerGenerated]
		get
		{
			return c5d4fd01386fb89cdf48dd8128fbd0243;
		}
		[CompilerGenerated]
		set
		{
			c5d4fd01386fb89cdf48dd8128fbd0243 = value;
		}
	}

	/// <summary>
	///       Gets or sets the collision object type.
	///       </summary>
	public CollisionObjectType Type
	{
		get
		{
			return c7c9af702452497cba0063de94eb6e293;
		}
		set
		{
			if (value != c7c9af702452497cba0063de94eb6e293)
			{
				c7c9af702452497cba0063de94eb6e293 = value;
				Changed = true;
			}
		}
	}

	/// <summary>
	///       Gets or sets the collision group ID.
	///       </summary>
	public int CollisionGroup
	{
		[CompilerGenerated]
		get
		{
			return cc38f69c3dda5612cf05b8c32daf49853;
		}
		[CompilerGenerated]
		set
		{
			cc38f69c3dda5612cf05b8c32daf49853 = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether this <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" /> is enabled.
	///       </summary>
	public bool Enabled
	{
		[CompilerGenerated]
		get
		{
			return ca756fb27da26facec9281ebca64c1de1;
		}
		[CompilerGenerated]
		set
		{
			ca756fb27da26facec9281ebca64c1de1 = value;
		}
	}

	internal bool c0dbe012fb6e35eff096504ebd112177b => c3626f9fd3ee4068556f67fec98f8cf9d == c1c3087dbe97c0e65d1b19a976416b744.ce38a2d521d8f8e1030e9a39815c9d087;

	internal bool cedb7bba503d90238311e100ff7d9069a => c3626f9fd3ee4068556f67fec98f8cf9d != c1c3087dbe97c0e65d1b19a976416b744.cd7a78bd43074e324847007105e636193;

	internal CollisionObject()
		: this(new GeometricObject())
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" /> class with the given
	///       geometric object.
	///       </summary>
	/// <param name="geometricObject">
	///       The geometric object (see property <see cref="P:DigitalRune.Geometry.Collisions.CollisionObject.GeometricObject" />).
	///       </param>
	public CollisionObject(IGeometricObject geometricObject)
	{
		GeometricObject = geometricObject;
		Enabled = true;
		Changed = true;
		Type = CollisionObjectType.Default;
	}

	internal void ceabf44cab475c8ec76f26d6553011a78(CollisionObject c9bdbd655e343282211edce8c6b867eab, IGeometricObject cb68f9e7d713a301d7f5d4ed772a8666f)
	{
		Changed = c9bdbd655e343282211edce8c6b867eab.Changed;
		CollisionGroup = c9bdbd655e343282211edce8c6b867eab.CollisionGroup;
		c5d1ff84a7eafda5b7365e8fe4c32b7d9 = c9bdbd655e343282211edce8c6b867eab.c5d1ff84a7eafda5b7365e8fe4c32b7d9;
		Enabled = c9bdbd655e343282211edce8c6b867eab.Enabled;
		c478123814d6e8ad075dde45fc0f9a623 = cb68f9e7d713a301d7f5d4ed772a8666f;
		c7c9af702452497cba0063de94eb6e293 = c9bdbd655e343282211edce8c6b867eab.c7c9af702452497cba0063de94eb6e293;
		c3626f9fd3ee4068556f67fec98f8cf9d = c9bdbd655e343282211edce8c6b867eab.c3626f9fd3ee4068556f67fec98f8cf9d;
	}

	internal void c3d10bc97b4c9daa63aaa5e3e3fd3b284()
	{
		Changed = true;
		CollisionGroup = 0;
		c5d1ff84a7eafda5b7365e8fe4c32b7d9 = null;
		Enabled = true;
		c478123814d6e8ad075dde45fc0f9a623 = null;
		c7c9af702452497cba0063de94eb6e293 = CollisionObjectType.Default;
		c3626f9fd3ee4068556f67fec98f8cf9d = c1c3087dbe97c0e65d1b19a976416b744.cd7a78bd43074e324847007105e636193;
	}

	private void c3c12ccb0e0feb2f78a298059c5a1223b(object c807f682152464d11dde83a885bf87124, EventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		Changed = true;
	}

	private void c8d854b6330cdc452eac9c19732fabc3c(object c807f682152464d11dde83a885bf87124, ShapeChangedEventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		Changed = true;
		object obj;
		if (c478123814d6e8ad075dde45fc0f9a623 == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(CollisionObject.c8d854b6330cdc452eac9c19732fabc3c(object, ShapeChangedEventArgs));
			}
			obj = null;
		}
		else
		{
			obj = c478123814d6e8ad075dde45fc0f9a623.Shape as RayShape;
		}
		RayShape rayShape = (RayShape)obj;
		if (rayShape != null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			c3626f9fd3ee4068556f67fec98f8cf9d = ((!rayShape.StopsAtFirstHit) ? c1c3087dbe97c0e65d1b19a976416b744.c1e75be3e596c1795b2b13abdc5440efb : c1c3087dbe97c0e65d1b19a976416b744.ce38a2d521d8f8e1030e9a39815c9d087);
		}
		else
		{
			c3626f9fd3ee4068556f67fec98f8cf9d = c1c3087dbe97c0e65d1b19a976416b744.cd7a78bd43074e324847007105e636193;
		}
		if (Domain == null)
		{
			return;
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			int feature = c7fefeb91d4632901bd52a1f245b70db1.Feature;
			if (feature == -1)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
					{
						foreach (ContactSet contactSet in Domain.ContactSets)
						{
							if (contactSet.ObjectA != this)
							{
								if (contactSet.ObjectB != this)
								{
									continue;
								}
								while (true)
								{
									switch (7)
									{
									case 0:
										continue;
									}
									break;
								}
							}
							using (List<Contact>.Enumerator enumerator2 = contactSet.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									Contact current2 = enumerator2.Current;
									current2.Recycle();
								}
								while (true)
								{
									switch (7)
									{
									case 0:
										break;
									default:
										goto end_IL_00f3;
									}
									continue;
									end_IL_00f3:
									break;
								}
							}
							contactSet.Clear();
							contactSet.IsValid = false;
						}
						return;
					}
					}
				}
			}
			foreach (ContactSet contactSet2 in Domain.ContactSets)
			{
				if (contactSet2.ObjectA == this)
				{
					for (int num = contactSet2.Count - 1; num >= 0; num--)
					{
						Contact contact = contactSet2[num];
						if (contact.FeatureA != -1)
						{
							if (contact.FeatureA != feature)
							{
								continue;
							}
							while (true)
							{
								switch (1)
								{
								case 0:
									continue;
								}
								break;
							}
						}
						contactSet2.RemoveAt(num);
						contact.Recycle();
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							continue;
						}
						break;
					}
					contactSet2.IsValid = false;
					continue;
				}
				if (contactSet2.ObjectB != this)
				{
					continue;
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				for (int num2 = contactSet2.Count - 1; num2 >= 0; num2--)
				{
					Contact contact2 = contactSet2[num2];
					if (contact2.FeatureB != -1)
					{
						while (true)
						{
							switch (1)
							{
							case 0:
								continue;
							}
							break;
						}
						if (contact2.FeatureB != feature)
						{
							continue;
						}
						while (true)
						{
							switch (2)
							{
							case 0:
								continue;
							}
							break;
						}
					}
					contactSet2.RemoveAt(num2);
					contact2.Recycle();
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				contactSet2.IsValid = false;
			}
			return;
		}
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8274), GeometricObject);
	}
}
