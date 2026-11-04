using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Collision;

/// <summary>
/// Contains the complete collision mesh for an object.
/// Generally the mesh is in world-space.
/// </summary>
public class CollisionMesh
{
	/// <summary>
	/// Creates a new CollisionSurface instance.
	/// </summary>
	public class CollisionSurface : IWorldBoundingBoxObject
	{
		/// <summary />
		public Plane Surface;

		/// <summary />
		public Plane Edge0;

		/// <summary />
		public Plane Edge1;

		/// <summary />
		public Plane Edge2;

		/// <summary />
		public int VertexIndex0;

		/// <summary />
		public int VertexIndex1;

		/// <summary />
		public int VertexIndex2;

		/// <summary />
		public ICollisionMaterial Material;

		/// <summary />
		public BoundingBox WorldBoundingBox => default;

		internal bool x(CollisionMesh P_0, ref BoundingBox P_1)
		{
			fN_0003.AccumulationValue++;
			P_1.Intersects(ref Edge0, out var result);
			if (result == PlaneIntersectionType.Back)
			{
				return false;
			}
			P_1.Intersects(ref Edge1, out var result2);
			if (result2 == PlaneIntersectionType.Back)
			{
				return false;
			}
			P_1.Intersects(ref Edge2, out var result3);
			if (result3 == PlaneIntersectionType.Back)
			{
				return false;
			}
			bool flag = result == PlaneIntersectionType.Intersecting;
			bool flag2 = result2 == PlaneIntersectionType.Intersecting;
			bool flag3 = result3 == PlaneIntersectionType.Intersecting;
			if (flag || flag2 || flag3)
			{
				fNn.AccumulationValue++;
			}
			return true;
		}

		internal bool x(CollisionMesh P_0, ref Vector3 P_1, ref BoundingSphere P_2, float P_3)
		{
			fN_0003.AccumulationValue++;
			float num = P_1.X * Edge0.Normal.X + P_1.Y * Edge0.Normal.Y + P_1.Z * Edge0.Normal.Z + Edge0.D;
			if (num < P_3)
			{
				return false;
			}
			float num2 = P_1.X * Edge1.Normal.X + P_1.Y * Edge1.Normal.Y + P_1.Z * Edge1.Normal.Z + Edge1.D;
			if (num2 < P_3)
			{
				return false;
			}
			float num3 = P_1.X * Edge2.Normal.X + P_1.Y * Edge2.Normal.Y + P_1.Z * Edge2.Normal.Z + Edge2.D;
			if (num3 < P_3)
			{
				return false;
			}
			bool flag = num < 0f;
			bool flag2 = num2 < 0f;
			bool flag3 = num3 < 0f;
			if (flag || flag2 || flag3)
			{
				fNn.AccumulationValue++;
				Vector3 vector = P_0.fNa[VertexIndex0];
				Vector3 vector2 = P_0.fNa[VertexIndex1];
				if (flag && !F(ref vector, ref vector2, ref P_2))
				{
					return false;
				}
				Vector3 vector3 = P_0.fNa[VertexIndex2];
				if (flag2 && !F(ref vector2, ref vector3, ref P_2))
				{
					return false;
				}
				if (flag3 && !F(ref vector3, ref vector, ref P_2))
				{
					return false;
				}
			}
			return true;
		}

		private bool F(ref Vector3 P_0, ref Vector3 P_1, ref BoundingSphere P_2)
		{
			Vector3 vector = P_1 - P_0;
			Vector3 vector2 = P_2.Center - P_0;
			float value = Vector3.Dot(vector, vector2) / Vector3.Dot(vector, vector);
			value = MathHelper.Clamp(value, 0f, 1f);
			Vector3 value2 = vector * value + P_0;
			float num = Vector3.DistanceSquared(P_2.Center, value2);
			return num <= P_2.Radius * P_2.Radius;
		}
	}

	private int[] fNB;

	private Vector3[] fNa;

	private List<CollisionSurface> fNg = new List<CollisionSurface>();

	internal L.a<CollisionSurface> fNf = new L.a<CollisionSurface>();

	private static SystemStatistic fN_0003 = SystemConsole.GetStatistic("Collision_PolyEdgeTestCount", SystemStatisticCategory.Collision);

	private static SystemStatistic fNn = SystemConsole.GetStatistic("Collision_PolyEdgeRayCastTestCount", SystemStatisticCategory.Collision);

	/// <summary>
	/// List of the geometry's indices.
	/// </summary>
	public int[] Indices
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
		}
	}

	/// <summary>
	/// List of the geometry's vertices.
	/// </summary>
	public Vector3[] Vertices
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
		}
	}

	/// <summary>
	/// List of the geometry's surfaces.
	/// </summary>
	public List<CollisionSurface> Surfaces
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

	internal void _0016()
	{
		if (fNa == null || fNa.Length <= 0)
		{
			return;
		}
		int num = Math.Max(Surfaces.Count / 100, 1);
		BoundingBox boundingBox = new BoundingBox(Vertices[0], Vertices[0]);
		Vector3[] array = fNa;
		foreach (Vector3 value in array)
		{
			boundingBox.Max = Vector3.Max(boundingBox.Max, value);
			boundingBox.Min = Vector3.Min(boundingBox.Min, value);
		}
		fNf._0013();
		fNf.B(ref boundingBox, num);
		foreach (CollisionSurface item in fNg)
		{
			Vector3 value2 = fNa[item.VertexIndex0];
			Vector3 value3 = fNa[item.VertexIndex1];
			Vector3 value4 = fNa[item.VertexIndex2];
			boundingBox.Max = Vector3.Max(Vector3.Max(value2, value3), value4);
			boundingBox.Min = Vector3.Min(Vector3.Min(value2, value3), value4);
			fNf.l(boundingBox, item);
		}
	}
}
