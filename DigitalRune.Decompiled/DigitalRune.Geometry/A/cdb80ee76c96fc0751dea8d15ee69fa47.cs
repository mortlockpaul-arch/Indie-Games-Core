using System;
using System.Runtime.CompilerServices;
using DigitalRune;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Geometry.Shapes;

namespace A;

internal static class cdb80ee76c96fc0751dea8d15ee69fa47
{
	public static readonly ResourcePool<ContactSetCollection> cd119f100f88d00f8b167fa83dbda39cf;

	public static readonly ResourcePool<BoxShape> c98c3bafe0aef6f270c50db484f6cf2eb;

	public static readonly ResourcePool<LineSegmentShape> c15ee94e8f68a7da40b4b9010459260d1;

	public static readonly ResourcePool<TriangleShape> c2fabcdff37c34655c8dee4068556b530;

	private static readonly c107cf2db36674a478eaf84e50d31337e c1b97f8ade7576cb1dbaf95642e216415;

	public static readonly ResourcePool<CollisionObject> c29a5464f174eae366728cbb2edc64776;

	[CompilerGenerated]
	private static Func<ContactSetCollection> c56450011eff7620c267636fce2acb8c4;

	[CompilerGenerated]
	private static Action<ContactSetCollection> c73f073436b4c66eae523762159132e8d;

	[CompilerGenerated]
	private static Func<BoxShape> c52f93b6e16dc37ce8233ad11ec6b3be7;

	[CompilerGenerated]
	private static Func<LineSegmentShape> c1c362a1a59d83efd7c796b26af77bbcc;

	[CompilerGenerated]
	private static Func<TriangleShape> c8dfeca5d3887b369bda6427cfb2884e2;

	[CompilerGenerated]
	private static Func<CollisionObject> cd338e6ceee0d962a13e4d9184081f6a1;

	[CompilerGenerated]
	private static Action<CollisionObject> cdc3780d1cc0eb85788d3986338a19245;

	static cdb80ee76c96fc0751dea8d15ee69fa47()
	{
		Func<ContactSetCollection> create = () => new ContactSetCollection();
		if (c73f073436b4c66eae523762159132e8d == null)
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
			if (1 == 0)
			{
				__ldtoken(cdb80ee76c96fc0751dea8d15ee69fa47._002Ecctor());
			}
			c73f073436b4c66eae523762159132e8d = (ContactSetCollection cbc664476e10ebecf958eb9fa7e209e05) =>
			{
				cbc664476e10ebecf958eb9fa7e209e05.Clear();
			};
		}
		cd119f100f88d00f8b167fa83dbda39cf = new ResourcePool<ContactSetCollection>(create, null, c73f073436b4c66eae523762159132e8d);
		c98c3bafe0aef6f270c50db484f6cf2eb = new ResourcePool<BoxShape>(() => new BoxShape(), null, null);
		c15ee94e8f68a7da40b4b9010459260d1 = new ResourcePool<LineSegmentShape>(() => new LineSegmentShape(), null, null);
		c2fabcdff37c34655c8dee4068556b530 = new ResourcePool<TriangleShape>(() => new TriangleShape(), null, null);
		c1b97f8ade7576cb1dbaf95642e216415 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		if (cd338e6ceee0d962a13e4d9184081f6a1 == null)
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
			cd338e6ceee0d962a13e4d9184081f6a1 = () => new CollisionObject(c1b97f8ade7576cb1dbaf95642e216415);
		}
		c29a5464f174eae366728cbb2edc64776 = new ResourcePool<CollisionObject>(cd338e6ceee0d962a13e4d9184081f6a1, null, (CollisionObject c46f6489140c503ea677cc26782e6a11b) =>
		{
			c46f6489140c503ea677cc26782e6a11b.c3d10bc97b4c9daa63aaa5e3e3fd3b284();
		});
	}

	[CompilerGenerated]
	private static ContactSetCollection c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new ContactSetCollection();
	}

	[CompilerGenerated]
	private static void c4c5cc4eb6785fd907a1974db5b42eeb5(ContactSetCollection cbc664476e10ebecf958eb9fa7e209e05)
	{
		cbc664476e10ebecf958eb9fa7e209e05.Clear();
	}

	[CompilerGenerated]
	private static BoxShape cae916c2d2e636f87174a99fc7a154c7f()
	{
		return new BoxShape();
	}

	[CompilerGenerated]
	private static LineSegmentShape c2eacb42c577fd179d20d5a4024070280()
	{
		return new LineSegmentShape();
	}

	[CompilerGenerated]
	private static TriangleShape cabc7d9463a3aca5199cf3ee0115097e1()
	{
		return new TriangleShape();
	}

	[CompilerGenerated]
	private static CollisionObject c7e37b87d8de7f0a82661edb092462f99()
	{
		return new CollisionObject(c1b97f8ade7576cb1dbaf95642e216415);
	}

	[CompilerGenerated]
	private static void c8a4e56c8333d2ebdef66dc66117c4d95(CollisionObject c46f6489140c503ea677cc26782e6a11b)
	{
		c46f6489140c503ea677cc26782e6a11b.c3d10bc97b4c9daa63aaa5e3e3fd3b284();
	}
}
