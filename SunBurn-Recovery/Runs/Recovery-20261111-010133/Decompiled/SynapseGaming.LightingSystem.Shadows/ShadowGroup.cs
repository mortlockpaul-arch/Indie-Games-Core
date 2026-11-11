using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Shadows;

public class ShadowGroup
{
	private ShadowSourceTypeCaster l8a;

	private IShadowSource l8_u0013_;

	private IShadowMap l8N;

	private BoundingSphere l8l;

	private BoundingBox l8L;

	private List<BaseLight> l8w = new List<BaseLight>(128);

	public IShadowSource ShadowSource => l8_u0013_;

	public IShadowMap Shadow
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
		}
	}

	public BoundingSphere BoundingSphereCentered
	{
		get
		{
			return l8l;
		}
		internal set
		{
			l8l = value;
		}
	}

	public BoundingBox BoundingBox
	{
		get
		{
			return l8L;
		}
		internal set
		{
			l8L = value;
		}
	}

	public List<BaseLight> Lights => l8w;

	internal ShadowSourceTypeCaster ShadowSourceTypes => l8a;

	public void Build(IShadowSource shadowsource, ISceneState scenestate)
	{
		if (l8w.Count < 1)
		{
			throw new Exception("Cannot build an empty shadow group.");
		}
		l8a = OptimizationSystem.ShadowSourceTypeCasters.Get(shadowsource);
		l8_u0013_ = shadowsource;
		if (l8a.PointSource != null)
		{
			bool flag = true;
			foreach (BaseLight item in l8w)
			{
				if (!flag)
				{
					l8L = BoundingBox.CreateMerged(l8L, item.WorldBoundingBox);
					continue;
				}
				l8L = item.WorldBoundingBox;
				flag = false;
			}
			BoundingSphere boundingSphere = BoundingSphere.CreateFromBoundingBox(l8L);
			float radius = Vector3.Distance(boundingSphere.Center, shadowsource.ShadowPosition) + boundingSphere.Radius;
			l8l = new BoundingSphere(shadowsource.ShadowPosition, radius);
		}
		else
		{
			if (l8a.DirectionalSource == null)
			{
				throw new Exception("Unknown light type - only point, spot, and directional lights are supported at this time.");
			}
			float shadowCasterDistance = scenestate.Environment.ShadowCasterDistance;
			Vector3 translation = scenestate.ViewToWorld.Translation;
			Vector3 vector = new Vector3(shadowCasterDistance);
			l8L = new BoundingBox(translation - vector, translation + vector);
			l8l = new BoundingSphere(translation - l8a.DirectionalSource.Direction * scenestate.Environment.ShadowCasterDistance, shadowCasterDistance * 2f);
		}
	}
}
