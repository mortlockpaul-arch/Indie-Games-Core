using System;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Base object for singleton Begin/End statements.  Forces a Begin called on an object
/// to be followed by an End on the same object before Begin can be called on any other
/// object derived from this type.
/// </summary>
public class SafeSingletonBeginableObject
{
	private static bool fNB = false;

	private static object fNa;

	/// <summary>
	/// Verifies no other Begin is in process.
	/// </summary>
	public virtual void Begin()
	{
		if (fNB)
		{
			throw new Exception("Cannot call begin within previous begin statement.  Try calling end on the previously begun object.");
		}
		fNB = true;
		fNa = this;
	}

	/// <summary>
	/// Verifies a Begin is in process on this object.
	/// </summary>
	public virtual void End()
	{
		if (!fNB)
		{
			throw new Exception("Cannot call end without first calling begin.");
		}
		if (fNa != this)
		{
			throw new Exception("Cannot call end on this object.  Begin was last called on another object.");
		}
		fNB = false;
		fNa = null;
	}
}
