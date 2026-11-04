using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Acts as a storage tree / scenegraph for objects of a particular
/// class or interface.  Supports object adding, moving, and removing
/// as well as auto-detecting movement of dynamic objects using the
/// MoveDynamicObjects method.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectGraph<T> : IQuery<T>, ISubmit<T> where T : IMovableObject
{
	private class nNB
	{
		public SystemStatistic ObjectsSubmitted = SystemConsole.GetStatistic("SceneGraph_ObjectsSubmitted", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsMoved = SystemConsole.GetStatistic("SceneGraph_ObjectsMoved", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsMovedDynamic = SystemConsole.GetStatistic("SceneGraph_ObjectsMovedDynamic", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsRemoved = SystemConsole.GetStatistic("SceneGraph_ObjectsRemoved", SystemStatisticCategory.SceneGraph);

		public SystemStatistic ObjectsRetrieved = SystemConsole.GetStatistic("SceneGraph_ObjectsRetrieved", SystemStatisticCategory.SceneGraph);

		public SystemStatistic Optimized = SystemConsole.GetStatistic("SceneGraph_Optimized", SystemStatisticCategory.SceneGraph);
	}

	private BoundingBox fNB;

	private int fNa = 20;

	private bool fNg = true;

	private int fNf;

	private Dictionary<T, int> fN_0003 = new Dictionary<T, int>(32);

	private Dictionary<string, T> fNn = new Dictionary<string, T>(32);

	private L.a<T> fN_0019 = new L.a<T>();

	private Vector3[] fNL = new Vector3[8];

	private List<T> fNv = new List<T>(32);

	private List<T> fN_0012 = new List<T>(32);

	private List<T> fN_0015 = new List<T>(32);

	private nNB fN8 = new nNB();

	/// <summary>
	/// The current containment volume for this object.
	/// </summary>
	public BoundingBox WorldBoundingBox => fNB;

	/// <summary>
	/// Enables automatic optimizations on the tree used to store contained objects. 
	/// Optimization occurs when a large number of objects fall outside of the tree bounds.
	/// </summary>
	public bool AutoOptimize
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
		}
	}

	/// <summary>
	/// Determines if the tree used to store contained objects requires optimization.
	/// </summary>
	public bool RequiresOptimization
	{
		get
		{
			if (fNf < 10)
			{
				fNf++;
				return false;
			}
			fNf = 0;
			return fN_0019.D();
		}
	}

	/// <summary>
	/// Dictionary of dynamic objects being tracked.
	/// </summary>
	protected Dictionary<T, int> DynamicObjects => fN_0003;

	/// <summary>
	/// Creates a new ObjectGraph using the default world size and tree depth.
	/// </summary>
	public ObjectGraph()
	{
		fNB = new BoundingBox(new Vector3(-1000f, -1000f, -1000f), new Vector3(1000f, 1000f, 1000f));
		Resize(fNB, fNa);
	}

	/// <summary>
	/// Creates a new ObjectGraph using the provided world size and tree depth.
	/// </summary>
	/// <param name="worldboundingbox">The smallest bounding area that completely
	/// contains the scene. Helps build an optimal scene tree.</param>
	/// <param name="worldtreemaxdepth">Maximum depth for entries in the scene tree. Small
	/// scenes with few objects see better performance with shallow trees. Large complex
	/// scenes often need deeper trees.</param>
	public ObjectGraph(BoundingBox worldboundingbox, int worldtreemaxdepth)
	{
		Resize(worldboundingbox, worldtreemaxdepth);
	}

	/// <summary>
	/// Resizes the tree used to store contained objects.
	/// </summary>
	/// <param name="worldboundingbox">The smallest bounding area that completely
	/// contains the scene. Helps the ObjectGraph build an optimal scene tree.</param>
	/// <param name="worldtreemaxdepth">Maximum depth for entries in the scene tree. Small
	/// scenes with few objects see better performance with shallow trees. Large complex
	/// scenes often need deeper trees.</param>
	public virtual void Resize(BoundingBox worldboundingbox, int worldtreemaxdepth)
	{
		fNB = worldboundingbox;
		fNa = worldtreemaxdepth;
		fN_0019.B(ref worldboundingbox, worldtreemaxdepth);
	}

	/// <summary>
	/// Optimizes the tree used to store contained objects.
	/// </summary>
	public virtual void Optimize()
	{
		Optimize(0);
	}

	/// <summary>
	/// Optimizes the tree used to store contained objects using a fixed tree depth.
	/// </summary>
	/// <param name="worldtreemaxdepth">Fixed tree depth used to optimize the tree.</param>
	public virtual void Optimize(int worldtreemaxdepth)
	{
		BoundingBox boundingBox = default;
		fNv.Clear();
		fN_0019.Z(fNv);
		bool flag = false;
		foreach (T item in fNv)
		{
			if (!item.InfiniteBounds)
			{
				boundingBox = BoundingBox.CreateMerged(boundingBox, item.WorldBoundingBox);
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
			num = Math.Max(1, fNv.Count / 20);
		}
		Resize(boundingBox, num);
		foreach (T item2 in fNv)
		{
			fN_0019.l(item2.WorldBoundingBox, item2);
		}
		fN8.Optimized.AccumulationValue++;
	}

	/// <summary>
	/// Adds an object to the container. This does not transfer ownership, disposable
	/// objects should be maintained and disposed separately.
	/// </summary>
	/// <param name="obj"></param>
	public virtual void Submit(T obj)
	{
		if (obj.UpdateType == UpdateType.Automatic)
		{
			if (fN_0003.ContainsKey(obj))
			{
				return;
			}
			fN_0003.Add(obj, obj.MoveId);
			string name = obj.Name;
			if (!string.IsNullOrEmpty(name))
			{
				if (fNn.ContainsKey(name))
				{
					fNn[name] = obj;
				}
				else
				{
					fNn.Add(name, obj);
				}
			}
		}
		fN_0019.l(obj.WorldBoundingBox, obj);
		fN8.ObjectsSubmitted.AccumulationValue++;
	}

	/// <summary>
	/// Repositions an object within the container. This method is used when a static object
	/// moves to reposition it in the storage tree / scenegraph.
	/// </summary>
	/// <param name="obj"></param>
	public virtual void Move(T obj)
	{
		fN_0019.t(obj.WorldBoundingBox, obj);
		fN8.ObjectsMoved.AccumulationValue++;
	}

	/// <summary>
	/// Auto-detects moved dynamic objects and repositions them in the storage tree / scenegraph.
	/// </summary>
	public virtual void MoveDynamicObjects()
	{
		LightingSystemPerformance.Begin("ObjectGraph.MoveDynamicObjects");
		fN_0012.Clear();
		if (fNg && RequiresOptimization)
		{
			Optimize();
			foreach (KeyValuePair<T, int> item in fN_0003)
			{
				fN_0012.Add(item.Key);
			}
			fN8.Optimized.AccumulationValue++;
		}
		else
		{
			foreach (KeyValuePair<T, int> item2 in fN_0003)
			{
				T key = item2.Key;
				if (key.MoveId != item2.Value)
				{
					fN_0019.t(key.WorldBoundingBox, key);
					fN_0012.Add(key);
					fN8.ObjectsMovedDynamic.AccumulationValue++;
				}
			}
			fN_0019._3();
		}
		foreach (T item3 in fN_0012)
		{
			fN_0003[item3] = item3.MoveId;
		}
	}

	/// <summary>
	/// Updates the object and its contained resources.
	/// </summary>
	/// <param name="gametime"></param>
	public virtual void Update(GameTime gametime)
	{
		fN_0015.Clear();
		foreach (KeyValuePair<T, int> item in fN_0003)
		{
			fN_0015.Add(item.Key);
		}
		foreach (T item2 in fN_0015)
		{
			item2.Update(gametime);
		}
	}

	/// <summary>
	/// Retrieves a dynamic object of a specific type by name.
	///
	/// Note: if multiple objects are submitted using the same name the
	/// method will return the last object submitted using that name.
	/// </summary>
	/// <typeparam name="TCastType">Type of object to find.</typeparam>
	/// <param name="name">Name of the object to find.</param>
	/// <param name="obj">Returned object.</param>
	/// <returns></returns>
	public bool Find<TCastType>(string name, out TCastType obj) where TCastType : class
	{
		fNn.TryGetValue(name, out var value);
		obj = value as TCastType;
		return obj != null;
	}

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes
	/// and overlap with or are contained in a bounding area.
	///
	/// Note: list will contain null entries when objects returned by the
	/// scenegraph are removed by the object filter.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	/// <param name="worldbounds">Bounding area used to limit query results.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	public virtual void Find(List<T> foundobjects, BoundingFrustum worldbounds, ObjectFilter objectfilter)
	{
		int count = foundobjects.Count;
		worldbounds.GetCorners(fNL);
		BoundingBox boundingBox = L._0019.a_000F(fNL);
		fN_0019.p(ref worldbounds, ref boundingBox, false, foundobjects);
		bool flag = (objectfilter & ObjectFilter.Dynamic) != 0;
		bool flag2 = (objectfilter & ObjectFilter.Static) != 0;
		int count2 = foundobjects.Count;
		if (flag && flag2)
		{
			fN8.ObjectsRetrieved.AccumulationValue += count2 - count;
			return;
		}
		for (int k = count; k < count2; k++)
		{
			UpdateType updateType = foundobjects[k].UpdateType;
			if ((!flag || updateType != UpdateType.Automatic) && (!flag2 || updateType != UpdateType.None))
			{
				foundobjects[k] = default;
			}
		}
		fN8.ObjectsRetrieved.AccumulationValue += foundobjects.Count - count;
	}

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes
	/// and overlap with or are contained in a bounding area.
	///
	/// Note: list will contain null entries when objects returned by the
	/// scenegraph are removed by the object filter.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	/// <param name="worldbounds">Bounding area used to limit query results.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	public virtual void Find(List<T> foundobjects, BoundingBox worldbounds, ObjectFilter objectfilter)
	{
		int count = foundobjects.Count;
		fN_0019.p(ref worldbounds, false, foundobjects);
		bool flag = (objectfilter & ObjectFilter.Dynamic) != 0;
		bool flag2 = (objectfilter & ObjectFilter.Static) != 0;
		int count2 = foundobjects.Count;
		if (flag && flag2)
		{
			fN8.ObjectsRetrieved.AccumulationValue += count2 - count;
			return;
		}
		for (int k = count; k < count2; k++)
		{
			UpdateType updateType = foundobjects[k].UpdateType;
			if ((!flag || updateType != UpdateType.Automatic) && (!flag2 || updateType != UpdateType.None))
			{
				foundobjects[k] = default;
			}
		}
		fN8.ObjectsRetrieved.AccumulationValue += foundobjects.Count - count;
	}

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes.
	///
	/// Note: list will contain null entries when objects returned by the
	/// scenegraph are removed by the object filter.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	public virtual void Find(List<T> foundobjects, ObjectFilter objectfilter)
	{
		bool flag = (objectfilter & ObjectFilter.Dynamic) != 0;
		bool flag2 = (objectfilter & ObjectFilter.Static) != 0;
		if (flag && !flag2)
		{
			foreach (KeyValuePair<T, int> dynamicObject in DynamicObjects)
			{
				foundobjects.Add(dynamicObject.Key);
			}
			fN8.ObjectsRetrieved.AccumulationValue += DynamicObjects.Count;
			return;
		}
		int count = foundobjects.Count;
		fN_0019.Z(foundobjects);
		int count2 = foundobjects.Count;
		if (flag && flag2)
		{
			fN8.ObjectsRetrieved.AccumulationValue += count2 - count;
			return;
		}
		for (int k = count; k < count2; k++)
		{
			UpdateType updateType = foundobjects[k].UpdateType;
			if ((!flag || updateType != UpdateType.Automatic) && (!flag2 || updateType != UpdateType.None))
			{
				foundobjects[k] = default;
			}
		}
		fN8.ObjectsRetrieved.AccumulationValue += count2 - count;
	}

	/// <summary>
	/// Quickly finds all objects near a bounding area without the overhead of
	/// filtering by object type, checking if objects are enabled, or verifying
	/// containment within the bounds.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	/// <param name="worldbounds">Bounding area used to limit query results.</param>
	public virtual void FindFast(List<T> foundobjects, BoundingBox worldbounds)
	{
		int count = foundobjects.Count;
		fN_0019.p(ref worldbounds, true, foundobjects);
		fN8.ObjectsRetrieved.AccumulationValue += foundobjects.Count - count;
	}

	/// <summary>
	/// Quickly finds all objects without the overhead of filtering by object
	/// type or checking if objects are enabled.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	public virtual void FindFast(List<T> foundobjects)
	{
		int count = foundobjects.Count;
		fN_0019.Z(foundobjects);
		fN8.ObjectsRetrieved.AccumulationValue += foundobjects.Count - count;
	}

	/// <summary>
	/// Removes an object from the container.
	/// </summary>
	/// <param name="obj"></param>
	public virtual void Remove(T obj)
	{
		fN_0003.Remove(obj);
		fNn.Remove(obj.Name);
		fN_0019._2(obj.WorldBoundingBox, obj);
		fN8.ObjectsRemoved.AccumulationValue++;
	}

	/// <summary>
	/// Removes resources managed by this object. Commonly used while clearing the scene.
	/// </summary>
	public virtual void Clear()
	{
		fN_0003.Clear();
		fNn.Clear();
		fN_0019._0013();
	}
}
