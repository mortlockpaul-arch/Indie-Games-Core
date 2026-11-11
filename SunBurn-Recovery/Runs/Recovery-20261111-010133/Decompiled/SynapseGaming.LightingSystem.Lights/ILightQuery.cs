using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Lights;

public interface ILightQuery : IQuery<BaseLight>
{
	CompositeLighting GetCompositeLighting(BoundingBox worldbounds, float ambientblend, LightingType lightingtype);

	void GetCompositeLighting(ref BoundingBox worldbounds, float ambientblend, LightingType lightingtype, out CompositeLighting compositelighting);

	CompositeLighting GetCompositeLighting(List<BaseLight> sourcelights, BoundingBox worldbounds, float ambientblend);

	void GetCompositeLighting(List<BaseLight> sourcelights, ref BoundingBox worldbounds, float ambientblend, out CompositeLighting compositelighting);
}
