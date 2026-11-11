using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public class RenderableMeshTransparencyRenderNode : BaseTransparencyRenderNode
{
	private static List<RenderableMesh> l8a = new List<RenderableMesh>();

	[CompilerGenerated]
	private RenderableMesh l8_u0013_;

	[CompilerGenerated]
	private BaseRenderManager l8N;

	public RenderableMesh RenderableMesh
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

	public BaseRenderManager RenderManager
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

	public override Effect Effect => RenderableMesh.l8_u0002_;

	public void Build(BaseRenderManager rendermanager, ISceneState scenestate, RenderableMesh mesh)
	{
		SceneObjectTypeCaster sceneObjectTypeCaster = OptimizationSystem.SceneObjectTypeCasters.Get(mesh.l8_u0013_);
		RenderManager = rendermanager;
		RenderableMesh = mesh;
		Vector3 result;
		if (sceneObjectTypeCaster.SpriteContainer != null)
		{
			result = new Vector3(0f, 0f, sceneObjectTypeCaster.SpriteContainer.WorldBoundingBox.Max.Z);
		}
		else
		{
			BoundingBox meshBoundingBox = mesh.MeshBoundingBox;
			Vector3 position = (meshBoundingBox.Max + meshBoundingBox.Min) * 0.5f;
			Vector3.Transform(ref position, ref mesh.l8v, out result);
		}
		Build(scenestate, result);
	}

	public override void RenderBatch(ISceneState scenestate, List<BaseTransparencyRenderNode> nodes, bool resetrenderstates)
	{
		l8a.Clear();
		foreach (BaseTransparencyRenderNode node in nodes)
		{
			if (node is RenderableMeshTransparencyRenderNode renderableMeshTransparencyRenderNode)
			{
				l8a.Add(renderableMeshTransparencyRenderNode.RenderableMesh);
			}
		}
		RenderManager.r4_u0015__u0011_1(l8a, resetrenderstates);
	}
}
