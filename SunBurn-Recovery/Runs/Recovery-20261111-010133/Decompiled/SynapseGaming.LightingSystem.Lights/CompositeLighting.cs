using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Lights;

public struct CompositeLighting
{
	public static readonly CompositeLighting EmptyLighting = default;

	public Vector3 Direction;

	public Vector3 DiffuseColor;

	public Vector3 AmbientColor;

	public bool Equals(CompositeLighting other)
	{
		if (Direction.Equals(other.Direction) && DiffuseColor.Equals(other.DiffuseColor))
		{
			return AmbientColor.Equals(other.AmbientColor);
		}
		return false;
	}
}
