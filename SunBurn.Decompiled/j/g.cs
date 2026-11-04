using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace j;

internal interface g
{
	TextureCube ShadowFaceMap { get; set; }

	TextureCube ShadowCoordMap { get; set; }

	Texture2D ShadowMap { get; }

	BoundingSphere ShadowArea { set; }

	Vector4 ShadowViewDistance { get; set; }

	Vector4[] ShadowMapLocationAndSpan { set; }

	Matrix[] ShadowViewProjection { set; }

	DetailPreference EffectDetail { get; set; }

	void SetShadowMapAndType(Texture2D shadowmap, L type);
}
