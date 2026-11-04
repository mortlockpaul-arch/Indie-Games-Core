namespace System.ComponentModel;

[AttributeUsage(AttributeTargets.All)]
public sealed class BrowsableAttribute : Attribute
{
	private readonly bool _browsable = true;

	public static readonly BrowsableAttribute Yes = new BrowsableAttribute(browsable: true);

	public static readonly BrowsableAttribute No = new BrowsableAttribute(browsable: false);

	public static readonly BrowsableAttribute Default = Yes;

	public bool Browsable => _browsable;

	public BrowsableAttribute(bool browsable)
	{
		_browsable = browsable;
	}

	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj is BrowsableAttribute browsableAttribute)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(BrowsableAttribute.Equals(object));
				}
				return browsableAttribute.Browsable == _browsable;
			}
		}
		return false;
	}

	public override int GetHashCode()
	{
		return _browsable.GetHashCode();
	}
}
