using System;
using System.Collections.Generic;
using B;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

public class ObjectGraph<T> : IQuery<T>, ISubmit<T> where T : IMovableObject
{
	private class w8a
	{
		public SystemStatistic ObjectsSubmitted = SystemConsole.GetStatistic("SceneGraph_ObjectsSubmitted", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsMoved = SystemConsole.GetStatistic("SceneGraph_ObjectsMoved", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsMovedDynamic = SystemConsole.GetStatistic("SceneGraph_ObjectsMovedDynamic", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsRemoved = SystemConsole.GetStatistic("SceneGraph_ObjectsRemoved", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsRetrieved = SystemConsole.GetStatistic("SceneGraph_ObjectsRetrieved", SystemStatisticCategory.SceneGraph);

		public SystemStatistic Optimized = SystemConsole.GetStatistic("SceneGraph_Optimized", SystemStatisticCategory.SceneGraph);
	}

	private BoundingBox l8a;

	private int l8_u0013_ = 20;

	private bool l8N = true;

	private int l8l;

	private ObjectIndex<T> l8L = new ObjectIndex<T>();

	private B._u0013_<T> l8w = new B._u0013_<T>();

	private Vector3[] l8J = new Vector3[8];

	private List<T> l8B = new List<T>(32);

	private List<T> l8s = new List<T>(32);

	private w8a l8h = new w8a();

	public BoundingBox WorldBoundingBox => l8a;

	public bool AutoOptimize
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
		}
	}

	public bool RequiresOptimization
	{
		get
		{
			if (l8l < 10)
			{
				l8l++;
				return false;
			}
			l8l = 0;
			return l8w.K();
		}
	}

	protected ObjectIndex<T> ObjectIndex => l8L;

	public ObjectGraph()
	{
		l8a = new BoundingBox(new Vector3(-1000f, -1000f, -1000f), new Vector3(1000f, 1000f, 1000f));
		Resize(l8a, l8_u0013_);
	}

	public ObjectGraph(BoundingBox worldboundingbox, int worldtreemaxdepth)
	{
		Resize(worldboundingbox, worldtreemaxdepth);
	}

	public virtual void Resize(BoundingBox worldboundingbox, int worldtreemaxdepth)
	{
		l8a = worldboundingbox;
		l8_u0013_ = worldtreemaxdepth;
		l8w.a(ref worldboundingbox, worldtreemaxdepth);
	}

	public virtual void Optimize()
	{
		Optimize(0);
	}

	public virtual void Optimize(int worldtreemaxdepth)
	{
		BoundingBox boundingBox = default;
		Dictionary<T, int> allObjects = l8L.AllObjects;
		bool flag = false;
		foreach (KeyValuePair<T, int> item in allObjects)
		{
			T key = item.Key;
			if (!key.InfiniteBounds)
			{
				boundingBox = BoundingBox.CreateMerged(boundingBox, key.WorldBoundingBox);
				flag = true;
			}
		}
		if (!flag)
		{
			boundingBox = new BoundingBox(new Vector3(float.MinValue, float.MinValue, float.MinValue), new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));
		}
		int num = worldtreemaxdepth;
		if (num <= 0)
		{
			num = Math.Max(1, allObjects.Count / 20);
		}
		Resize(boundingBox, num);
		foreach (KeyValuePair<T, int> item2 in allObjects)
		{
			T key2 = item2.Key;
			l8w._id_0(key2.WorldBoundingBox, key2);
		}
		l8h.Optimized.AccumulationValue++;
	}

	public virtual void Submit(T obj)
	{
		l8L.Add(obj);
		l8w._id_0(obj.WorldBoundingBox, obj);
		l8h.ObjectsSubmitted.AccumulationValue++;
	}

	public virtual void Move(T obj)
	{
		l8w.R(obj.WorldBoundingBox, obj);
		l8h.ObjectsMoved.AccumulationValue++;
	}

	private void _u0016_()
	{
		LightingSystemPerformance.Begin("ObjectGraph.MoveDynamicObjects");
		Dictionary<T, int> dynamicObjects = l8L.DynamicObjects;
		l8B.Clear();
		if (l8N && RequiresOptimization)
		{
			Optimize();
			foreach (KeyValuePair<T, int> item in dynamicObjects)
			{
				l8B.Add(item.Key);
			}
			l8h.Optimized.AccumulationValue++;
		}
		else
		{
			foreach (KeyValuePair<T, int> item2 in dynamicObjects)
			{
				T key = item2.Key;
				if (key.MoveId != item2.Value)
				{
					l8w.R(key.WorldBoundingBox, key);
					l8B.Add(key);
					l8h.ObjectsMovedDynamic.AccumulationValue++;
				}
			}
			l8w.q();
		}
		foreach (T item3 in l8B)
		{
			dynamicObjects[item3] = item3.MoveId;
		}
	}

	public virtual void Update(GameTime gametime)
	{
		Dictionary<T, int> dynamicObjects = l8L.DynamicObjects;
		l8s.Clear();
		foreach (KeyValuePair<T, int> item in dynamicObjects)
		{
			l8s.Add(item.Key);
		}
		foreach (T l in l8s)
		{
			l.Update(gametime);
		}
		_u0016_();
	}

	public TCastType Find<TCastType>(string name, bool onlysearchdynamicobjects) where TCastType : class
	{
		if (Find<TCastType>(name, onlysearchdynamicobjects, out var obj))
		{
			return obj;
		}
		throw new Exception($"Unable to find object named '{name}'.");
	}

	public TCastType Find<TCastType>(int uniqueid) where TCastType : class
	{
		if (Find<TCastType>(uniqueid, out var obj))
		{
			return obj;
		}
		throw new Exception($"Unable to find object with id '{uniqueid}'.");
	}

	public bool Find<TCastType>(string name, bool onlysearchdynamicobjects, out TCastType obj) where TCastType : class
	{
		T value;
		if (onlysearchdynamicobjects)
		{
			l8L.DynamicObjectsByName.TryGetValue(name, out value);
		}
		else
		{
			l8L.AllObjectsByName.TryGetValue(name, out value);
		}
		obj = value as TCastType;
		return obj != null;
	}

	public bool Find<TCastType>(int uniqueid, out TCastType obj) where TCastType : class
	{
		l8L.AllObjectsByUniqueId.TryGetValue(uniqueid, out var value);
		obj = value as TCastType;
		return obj != null;
	}

	public virtual void Find(List<T> foundobjects, BoundingFrustum worldbounds, ObjectFilter objectfilter)
	{
		int count = foundobjects.Count;
		worldbounds.GetCorners(l8J);
		BoundingBox param_ = CoreHelper.CreateBoundingBoxFromPoints(l8J);
		l8w._u0015_(ref worldbounds, ref param_, param_2: false, foundobjects);
		bool flag = (objectfilter & ObjectFilter.Dynamic) != 0;
		bool flag2 = (objectfilter & ObjectFilter.Static) != 0;
		int count2 = foundobjects.Count;
		if (flag && flag2)
		{
			l8h.ObjectsRetrieved.AccumulationValue += count2 - count;
			return;
		}
		for (int i = count; i < count2; i++)
		{
			UpdateType updateType = foundobjects[i].UpdateType;
			if ((!flag || updateType != UpdateType.Automatic) && (!flag2 || updateType != UpdateType.None))
			{
				foundobjects[i] = default;
			}
		}
		l8h.ObjectsRetrieved.AccumulationValue += foundobjects.Count - count;
	}

	public virtual void Find(List<T> foundobjects, BoundingBox worldbounds, ObjectFilter objectfilter)
	{
		int count = foundobjects.Count;
		l8w._u0015_(ref worldbounds, param_1: false, foundobjects);
		bool flag = (objectfilter & ObjectFilter.Dynamic) != 0;
		bool flag2 = (objectfilter & ObjectFilter.Static) != 0;
		int count2 = foundobjects.Count;
		if (flag && flag2)
		{
			l8h.ObjectsRetrieved.AccumulationValue += count2 - count;
			return;
		}
		for (int i = count; i < count2; i++)
		{
			UpdateType updateType = foundobjects[i].UpdateType;
			if ((!flag || updateType != UpdateType.Automatic) && (!flag2 || updateType != UpdateType.None))
			{
				foundobjects[i] = default;
			}
		}
		l8h.ObjectsRetrieved.AccumulationValue += foundobjects.Count - count;
	}

	public virtual void Find(List<T> foundobjects, ObjectFilter objectfilter)
	{
		bool flag = (objectfilter & ObjectFilter.Dynamic) != 0;
		bool flag2 = (objectfilter & ObjectFilter.Static) != 0;
		if (flag && !flag2)
		{
			Dictionary<T, int> dynamicObjects = l8L.DynamicObjects;
			foreach (KeyValuePair<T, int> item in dynamicObjects)
			{
				foundobjects.Add(item.Key);
			}
			l8h.ObjectsRetrieved.AccumulationValue += dynamicObjects.Count;
			return;
		}
		int count = foundobjects.Count;
		Dictionary<T, int> allObjects = l8L.AllObjects;
		foreach (KeyValuePair<T, int> item2 in allObjects)
		{
			foundobjects.Add(item2.Key);
		}
		int count2 = foundobjects.Count;
		if (flag && flag2)
		{
			l8h.ObjectsRetrieved.AccumulationValue += count2 - count;
			return;
		}
		for (int i = count; i < count2; i++)
		{
			UpdateType updateType = foundobjects[i].UpdateType;
			if ((!flag || updateType != UpdateType.Automatic) && (!flag2 || updateType != UpdateType.None))
			{
				foundobjects[i] = default;
			}
		}
		l8h.ObjectsRetrieved.AccumulationValue += count2 - count;
	}

	public virtual void FindFast(List<T> foundobjects, BoundingBox worldbounds)
	{
		int count = foundobjects.Count;
		l8w._u0015_(ref worldbounds, param_1: true, foundobjects);
		l8h.ObjectsRetrieved.AccumulationValue += foundobjects.Count - count;
	}

	public virtual void FindFast(List<T> foundobjects)
	{
		int count = foundobjects.Count;
		Dictionary<T, int> allObjects = l8L.AllObjects;
		foreach (KeyValuePair<T, int> item in allObjects)
		{
			foundobjects.Add(item.Key);
		}
		l8h.ObjectsRetrieved.AccumulationValue += foundobjects.Count - count;
	}

	public virtual void Remove(T obj)
	{
		l8L.Remove(obj);
		l8w.i(obj.WorldBoundingBox, obj);
		l8h.ObjectsRemoved.AccumulationValue++;
	}

	public virtual void Clear()
	{
		l8L.Clear();
		l8w._id_3();
	}
}
