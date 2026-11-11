using System.Collections.Generic;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Shadows;

public interface IShadowMapManager : IRenderableManager, IManagerService, IManager, IUnloadable
{
	void BuildShadows(List<ShadowRenderTargetGroup> rendertargetgroups, List<BaseLight> lights, bool usedefaultgrouping);
}
