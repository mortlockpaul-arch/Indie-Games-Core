using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public class TransparencyRenderNodeSorter
{
	private class w8a : IComparer<BaseTransparencyRenderNode>
	{
		public int Compare(BaseTransparencyRenderNode a, BaseTransparencyRenderNode b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			return b.SortIndex - a.SortIndex;
		}
	}

	private List<BaseTransparencyRenderNode> l8a = new List<BaseTransparencyRenderNode>();

	private List<BaseTransparencyRenderNode> l8_u0013_ = new List<BaseTransparencyRenderNode>();

	private static w8a l8N = new w8a();

	public void Add(BaseTransparencyRenderNode node)
	{
		l8a.Add(node);
	}

	public void Clear()
	{
		l8a.Clear();
	}

	public void RenderBatches(ISceneState scenestate, bool allowbatching)
	{
		l8a.Sort(l8N);
		int count = l8a.Count;
		Type type = null;
		for (int i = 0; i < count; i++)
		{
			BaseTransparencyRenderNode baseTransparencyRenderNode = l8a[i];
			Type type2 = baseTransparencyRenderNode.GetType();
			bool resetrenderstates = (object)type2 != type;
			l8_u0013_.Clear();
			l8_u0013_.Add(baseTransparencyRenderNode);
			if (allowbatching)
			{
				Effect effect = baseTransparencyRenderNode.Effect;
				for (int j = i + 1; j < count; j++)
				{
					BaseTransparencyRenderNode baseTransparencyRenderNode2 = l8a[j];
					if ((object)type2 != baseTransparencyRenderNode2.GetType() || effect != baseTransparencyRenderNode2.Effect)
					{
						break;
					}
					l8_u0013_.Add(baseTransparencyRenderNode2);
					i++;
				}
			}
			baseTransparencyRenderNode.RenderBatch(scenestate, l8_u0013_, resetrenderstates);
			type = type2;
		}
	}
}
