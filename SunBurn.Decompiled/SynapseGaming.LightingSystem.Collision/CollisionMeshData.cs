using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Collision;

/// <summary>
/// Provides access to raw geometry data used for collision.
/// </summary>
public class CollisionMeshData
{
	/// <summary>
	/// List of the geometry's vertices.
	/// </summary>
	public List<Vector3> Vertices = new List<Vector3>(256);

	/// <summary>
	/// List of the geometry's indices.
	/// </summary>
	public List<int> Indices = new List<int>(256);
}
