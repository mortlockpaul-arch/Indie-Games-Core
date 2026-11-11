using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Collision;

public class CollisionPoint
{
	public float ContactTime;

	public Vector3 ContactPoint;

	public Vector3 SurfaceNormal;

	public ICollisionObject ContactObject;

	public ICollisionMaterial Material;

	public List<ICollisionObject> Triggers = new List<ICollisionObject>(16);

	public void Clear()
	{
		ContactTime = 1f;
		Triggers.Clear();
	}
}
