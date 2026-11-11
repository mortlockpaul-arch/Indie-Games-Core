using System;

namespace SynapseGaming.LightingSystem.Core;

public class SafeSingletonBeginableObject
{
	private static bool l8a = false;

	private static object l8_u0013_;

	public virtual void Begin()
	{
		if (l8a)
		{
			throw new Exception("Cannot call begin within previous begin statement.  Try calling end on the previously begun object.");
		}
		l8a = true;
		l8_u0013_ = this;
	}

	public virtual void End()
	{
		if (!l8a)
		{
			throw new Exception("Cannot call end without first calling begin.");
		}
		if (l8_u0013_ != this)
		{
			throw new Exception("Cannot call end on this object.  Begin was last called on another object.");
		}
		l8a = false;
		l8_u0013_ = null;
	}
}
