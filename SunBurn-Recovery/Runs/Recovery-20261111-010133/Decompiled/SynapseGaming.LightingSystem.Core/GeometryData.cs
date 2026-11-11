using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

public class GeometryData
{
	public List<Vector3> Vertices = new List<Vector3>(256);

	public List<int> Indices = new List<int>(256);
}
