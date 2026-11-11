using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Rendering;

public class VolumeLightTransparencyRenderNode : BaseTransparencyRenderNode
{
	private static List<BaseLight> l8a = new List<BaseLight>();

	[CompilerGenerated]
	private BaseLight l8_u0013_;

	[CompilerGenerated]
	private ILightManager l8N;

	public BaseLight VolumeLight
	{
		[CompilerGenerated]
		get
		{
			return l8_u0013_;
		}
		[CompilerGenerated]
		protected set
		{
			l8_u0013_ = value;
		}
	}

	public ILightManager LightManager
	{
		[CompilerGenerated]
		get
		{
			return l8N;
		}
		[CompilerGenerated]
		protected set
		{
			l8N = value;
		}
	}

	public override Effect Effect => null;

	public void Build(ILightManager lightmanager, ISceneState scenestate, BaseLight volumelight)
	{
		LightManager = lightmanager;
		VolumeLight = volumelight;
		BoundingBox worldBoundingBox = volumelight.WorldBoundingBox;
		Vector3 objectcenter = (worldBoundingBox.Max + worldBoundingBox.Min) * 0.5f;
		Build(scenestate, objectcenter);
	}

	public override void RenderBatch(ISceneState scenestate, List<BaseTransparencyRenderNode> nodes, bool resetrenderstates)
	{
		l8a.Clear();
		foreach (BaseTransparencyRenderNode node in nodes)
		{
			if (node is VolumeLightTransparencyRenderNode volumeLightTransparencyRenderNode)
			{
				l8a.Add(volumeLightTransparencyRenderNode.VolumeLight);
			}
		}
		LightManager.RenderVolumeLights(l8a);
	}
}
