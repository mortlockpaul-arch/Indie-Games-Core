using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Effects;

public interface ITerrainEffect
{
	Texture2D HeightMapTexture { get; set; }

	float HeightScale { get; set; }

	float Tiling { get; set; }

	int MeshSegments { get; set; }

	int TileRepeatCount { get; set; }
}
