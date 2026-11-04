using System;
using System.Runtime.CompilerServices;
using DigitalRune;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;

namespace A;

internal class c107cf2db36674a478eaf84e50d31337e : IGeometricObject, ICloneable
{
	private static readonly ResourcePool<c107cf2db36674a478eaf84e50d31337e> c8aca4a0e3c0073ffb188fde832680587;

	private Aabb c3a708fca2e00c0f5027ce857e9da5624;

	private bool c98eff13aac2c9760773f96dcf2f23831;

	private Pose cd337f7a206e7977cce3e1030296b1b57;

	private Shape cc3d25c57005ac063144d857d0f17c454;

	private Vector3F c8a035e79b582cd5fd7cd2b1a6f489725;

	[CompilerGenerated]
	private static Func<c107cf2db36674a478eaf84e50d31337e> c49253b4520c1b7e183dfcc9c426c0283;

	public Aabb Aabb
	{
		get
		{
			if (!c98eff13aac2c9760773f96dcf2f23831)
			{
				c3a708fca2e00c0f5027ce857e9da5624 = Shape.GetAabb(Scale, Pose);
				c98eff13aac2c9760773f96dcf2f23831 = true;
			}
			return c3a708fca2e00c0f5027ce857e9da5624;
		}
	}

	public Pose Pose
	{
		get
		{
			return cd337f7a206e7977cce3e1030296b1b57;
		}
		set
		{
			cd337f7a206e7977cce3e1030296b1b57 = value;
			c98eff13aac2c9760773f96dcf2f23831 = false;
		}
	}

	public Shape Shape
	{
		get
		{
			return cc3d25c57005ac063144d857d0f17c454;
		}
		set
		{
			cc3d25c57005ac063144d857d0f17c454 = value;
			c98eff13aac2c9760773f96dcf2f23831 = false;
		}
	}

	public Vector3F Scale
	{
		get
		{
			return c8a035e79b582cd5fd7cd2b1a6f489725;
		}
		set
		{
			c8a035e79b582cd5fd7cd2b1a6f489725 = value;
			c98eff13aac2c9760773f96dcf2f23831 = false;
		}
	}

	public event EventHandler<EventArgs> PoseChanged
	{
		add
		{
		}
		remove
		{
		}
	}

	public event EventHandler<ShapeChangedEventArgs> ShapeChanged
	{
		add
		{
		}
		remove
		{
		}
	}

	private c107cf2db36674a478eaf84e50d31337e()
	{
	}

	static c107cf2db36674a478eaf84e50d31337e()
	{
		if (c49253b4520c1b7e183dfcc9c426c0283 == null)
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
			if (1 == 0)
			{
				__ldtoken(c107cf2db36674a478eaf84e50d31337e._002Ecctor());
			}
			c49253b4520c1b7e183dfcc9c426c0283 = () => new c107cf2db36674a478eaf84e50d31337e();
		}
		c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<c107cf2db36674a478eaf84e50d31337e>(c49253b4520c1b7e183dfcc9c426c0283, null, null);
	}

	public static c107cf2db36674a478eaf84e50d31337e c2d8ce8b1ccf2b905e3b18f8a5145fe8a()
	{
		return c8aca4a0e3c0073ffb188fde832680587.Obtain();
	}

	public void ca18b9b5faa9851d6355f617787bb5859()
	{
		Shape = null;
		Scale = Vector3F.One;
		Pose = Pose.Identity;
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	private object c48423685eed44c8f6f4708513cf05f2b()
	{
		throw new NotImplementedException();
	}

	object ICloneable.Clone()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c48423685eed44c8f6f4708513cf05f2b
		return this.c48423685eed44c8f6f4708513cf05f2b();
	}

	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4519), Shape);
	}

	[CompilerGenerated]
	private static c107cf2db36674a478eaf84e50d31337e c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new c107cf2db36674a478eaf84e50d31337e();
	}
}
