using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;

namespace _9;

internal class B : BaseShadowManager
{
	public B(IGraphicsDeviceService graphicsdevicemanager)
		: base(graphicsdevicemanager)
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
