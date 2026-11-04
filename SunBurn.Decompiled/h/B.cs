using System;
using System.Collections.Generic;
using SynapseGaming.LightingSystem.Core;

namespace h;

internal class B<TManager, TObject> where TManager : ISubmit<TObject>
{
	private TManager fNB;

	private List<TObject> fNa = new List<TObject>(16);

	public TManager ContainingManager => fNB;

	public void SetManager(TManager manager)
	{
		if (fNB != null)
		{
			if (manager != null)
			{
				throw new Exception("Unable to assign scene to more than one " + typeof(TManager).Name + ". Remove from the previous manager first.");
			}
			RemoveSubmittedObjects();
		}
		fNB = manager;
	}

	public void Submit(TObject obj)
	{
		if (fNB != null)
		{
			fNB.Submit(obj);
			fNa.Add(obj);
		}
	}

	public void RemoveSubmittedObjects()
	{
		if (fNB == null)
		{
			return;
		}
		foreach (TObject item in fNa)
		{
			fNB.Remove(item);
		}
		fNa.Clear();
	}

	public void Optimize()
	{
		if (fNB is IWorldRenderableManager)
		{
			(fNB as IWorldRenderableManager).Optimize();
		}
	}
}
