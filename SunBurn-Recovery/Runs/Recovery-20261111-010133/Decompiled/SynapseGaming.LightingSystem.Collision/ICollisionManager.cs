using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Collision;

public interface ICollisionManager : IManagerService, IRenderableManager, IUpdatableManager, IManager, IUnloadable
{
	bool RayCast(Vector3 startposition, Vector3 endposition, out RayCollisionPoint firsthit);

	void RayCast(List<RayCollisionPoint> hits, Vector3 startposition, Vector3 endposition);

	bool RayCast(Ray ray, float castdistance, out RayCollisionPoint firsthit);

	void RayCast(List<RayCollisionPoint> hits, Ray ray, float castdistance);
}
