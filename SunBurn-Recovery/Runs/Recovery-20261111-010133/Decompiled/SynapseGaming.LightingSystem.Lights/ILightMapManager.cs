using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Lights;

public interface ILightMapManager : IManagerService, IManager, IUnloadable
{
	LightMap GetLightMap(RenderableMesh mesh);

	LightOcclusionBuffer GetLightOcclusionBuffer(ILight light);
}
