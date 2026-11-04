using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Shadows;

/// <summary>
/// Defines a group of lights that share the same shadow source.
/// </summary>
public class ShadowGroup
{
	private L.c fNB;

	private IShadowSource fNa;

	private IShadow fNg;

	private BoundingSphere fNf;

	private BoundingBox fN_0003;

	private List<BaseLight> fNn = new List<BaseLight>(128);

	/// <summary>
	/// Shared shadow source used to determine shadow casting information.
	/// </summary>
	public IShadowSource ShadowSource => fNa;

	/// <summary>
	/// Shadow object used to store and render shadows.
	/// </summary>
	public IShadow Shadow
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
		}
	}

	/// <summary>
	/// Shadow bounding sphere originating at the shadow source center.
	/// </summary>
	public BoundingSphere BoundingSphereCentered
	{
		get
		{
			return fNf;
		}
		internal set
		{
			fNf = value;
		}
	}

	/// <summary>
	/// Shadow bounding box fitted to the shadow region. For some light types like
	/// spotlights this is not necessarily centered around the shadow source.  For
	/// others like directional lights this is only the shadow bounding area and does
	/// not relate to the illuminated area.
	/// </summary>
	public BoundingBox BoundingBox
	{
		get
		{
			return fN_0003;
		}
		internal set
		{
			fN_0003 = value;
		}
	}

	/// <summary>
	/// List of lights that share the shadow source.
	/// </summary>
	public List<BaseLight> Lights => fNn;

	internal L.c ShadowSourceTypes => fNB;

	/// <summary>
	/// Builds the shadow group information based on the shadow source.
	/// </summary>
	/// <param name="shadowsource"></param>
	/// <param name="scenestate">Scene state used to render the current view.</param>
	public void Build(IShadowSource shadowsource, ISceneState scenestate)
	{
		if (fNn.Count < 1)
		{
			throw new Exception("Cannot build an empty shadow group.");
		}
		fNB = OptimizationSystem.fNa.g_0011(shadowsource);
		fNa = shadowsource;
		if (fNB.fNB != null)
		{
			bool flag = true;
			foreach (BaseLight item in fNn)
			{
				if (!flag)
				{
					fN_0003 = BoundingBox.CreateMerged(fN_0003, item.WorldBoundingBox);
					continue;
				}
				fN_0003 = item.WorldBoundingBox;
				flag = false;
			}
			BoundingSphere boundingSphere = BoundingSphere.CreateFromBoundingBox(fN_0003);
			float radius = Vector3.Distance(boundingSphere.Center, shadowsource.ShadowPosition) + boundingSphere.Radius;
			fNf = new BoundingSphere(shadowsource.ShadowPosition, radius);
		}
		else
		{
			if (fNB.fNg == null)
			{
				throw new Exception("Unknown light type - only point, spot, and directional lights are supported at this time.");
			}
			float shadowCasterDistance = scenestate.Environment.ShadowCasterDistance;
			Vector3 translation = scenestate.ViewToWorld.Translation;
			Vector3 vector = new Vector3(shadowCasterDistance);
			fN_0003 = new BoundingBox(translation - vector, translation + vector);
			fNf = new BoundingSphere(translation - fNB.fNg.Direction * scenestate.Environment.ShadowCasterDistance, shadowCasterDistance * 2f);
		}
	}
}
