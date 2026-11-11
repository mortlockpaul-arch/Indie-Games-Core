using System.Collections.Generic;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;

namespace _u0019_;

internal class a : BaseShadowManager
{
	public a(IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
	}

	public new void BuildShadowGroups(List<ShadowGroup> shadowgroups, List<BaseLight> lights, bool usedefaultgrouping)
	{
		base.BuildShadowGroups(shadowgroups, lights, true);
	}

	public override void Clear()
	{
	}

	public override void Unload()
	{
	}
}
