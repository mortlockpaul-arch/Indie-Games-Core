using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public abstract class BaseTransparencyRenderNode
{
	[CompilerGenerated]
	private int l8a;

	public int SortIndex
	{
		[CompilerGenerated]
		get
		{
			return l8a;
		}
		[CompilerGenerated]
		protected set
		{
			l8a = value;
		}
	}

	public abstract Effect Effect { get; }

	protected void Build(ISceneState scenestate, Vector3 objectcenter)
	{
		float visibleDistance = scenestate.Environment.VisibleDistance;
		if (!(visibleDistance <= 0f))
		{
			Vector3 value = scenestate.ViewToWorld.Translation;
			Vector3.DistanceSquared(ref value, ref objectcenter, out var result);
			SortIndex = (int)(result / (visibleDistance * visibleDistance) * 65535f);
		}
	}

	public abstract void RenderBatch(ISceneState scenestate, List<BaseTransparencyRenderNode> nodes, bool resetrenderstates);
}
