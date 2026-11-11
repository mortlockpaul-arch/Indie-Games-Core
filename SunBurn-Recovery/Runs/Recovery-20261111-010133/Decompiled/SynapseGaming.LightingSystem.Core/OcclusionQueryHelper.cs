using System;
using System.Collections.Generic;
using System.Threading;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using y;

namespace SynapseGaming.LightingSystem.Core;

public class OcclusionQueryHelper<TKey> : IDisposable
{
	private class w8a
	{
		public OcclusionQuery OcclusionQuery;

		public bool ContainsCamera;

		public BoundingBox BoundingBox;

		public w8a(GraphicsDevice device, BoundingBox bounds)
		{
			OcclusionQuery = new OcclusionQuery(device);
			ContainsCamera = false;
			BoundingBox = bounds;
		}
	}

	private bool l8a;

	private GraphicsDevice l8_u0013_;

	private B.N l8N;

	private List<w8a> l8l = new List<w8a>(32);

	private Dictionary<TKey, w8a> l8L = new Dictionary<TKey, w8a>(32);

	private BlendState l8w = new BlendState
	{
		ColorDestinationBlend = Blend.One,
		ColorSourceBlend = Blend.Zero,
		AlphaDestinationBlend = Blend.One,
		AlphaSourceBlend = Blend.Zero
	};

	private BlendState l8J = new BlendState();

	public OcclusionQueryHelper(GraphicsDevice device, bool writingtofloattarget)
	{
		l8_u0013_ = device;
		l8a = writingtofloattarget;
		l8N = new B.N(l8_u0013_);
	}

	public void SubmitObject(TKey key, BoundingBox worldbounds)
	{
		if (l8l.Count < 1)
		{
			l8L.Add(key, new w8a(l8_u0013_, worldbounds));
			return;
		}
		w8a w8a2 = l8l[0];
		w8a2.ContainsCamera = false;
		w8a2.BoundingBox = worldbounds;
		l8L.Add(key, w8a2);
		l8l.RemoveAt(0);
	}

	public void RunOcclusionQuery(ISceneState scenestate, float detectionpadding)
	{
		if (l8L.Count < 1)
		{
			return;
		}
		LightingSystemPerformance.Begin("OcclusionQueryHelper.RunOcclusionQuery");
		BlendState blendState = l8_u0013_.BlendState;
		RasterizerState rasterizerState = l8_u0013_.RasterizerState;
		DepthStencilState depthStencilState = l8_u0013_.DepthStencilState;
		l8_u0013_.RasterizerState = RasterizerState.CullNone;
		l8_u0013_.DepthStencilState = DepthStencilState.DepthRead;
		if (l8a)
		{
			l8_u0013_.BlendState = l8J;
		}
		else
		{
			l8_u0013_.BlendState = l8w;
		}
		BasicEffect basicEffect = l8N.DefaultEffect;
		basicEffect.World = Matrix.Identity;
		basicEffect.View = scenestate.View;
		basicEffect.Projection = scenestate.Projection;
		EffectPass effectPass = basicEffect.CurrentTechnique.Passes[0];
		Plane near = scenestate.ViewFrustum.Near;
		foreach (KeyValuePair<TKey, w8a> item in l8L)
		{
			BoundingBox boundingBox = item.Value.BoundingBox;
			BoundingBox boundingBox2 = CoreHelper.PaddedBoundingBox(boundingBox, detectionpadding);
			item.Value.ContainsCamera = boundingBox2.Intersects(near) == PlaneIntersectionType.Intersecting;
			if (!item.Value.ContainsCamera)
			{
				OcclusionQuery occlusionQuery = item.Value.OcclusionQuery;
				basicEffect.World = l8N._u0018_(boundingBox);
				effectPass.Apply();
				occlusionQuery.Begin();
				l8N.S();
				occlusionQuery.End();
			}
		}
		l8_u0013_.BlendState = blendState;
		l8_u0013_.RasterizerState = rasterizerState;
		l8_u0013_.DepthStencilState = depthStencilState;
	}

	public bool IsObjectVisible(TKey key)
	{
		if (!l8L.TryGetValue(key, out var value))
		{
			return true;
		}
		if (value.ContainsCamera)
		{
			return true;
		}
		LightingSystemPerformance.Begin("OcclusionQueryHelper.IsObjectVisible (query result)");
		while (!value.OcclusionQuery.IsComplete)
		{
			Thread.Sleep(0);
		}
		return value.OcclusionQuery.PixelCount > 0;
	}

	public void Clear()
	{
		foreach (KeyValuePair<TKey, w8a> item in l8L)
		{
			l8l.Add(item.Value);
		}
		l8L.Clear();
	}

	public void Dispose()
	{
		Clear();
		foreach (w8a item in l8l)
		{
			item.OcclusionQuery.Dispose();
		}
		l8l.Clear();
		y.a.w_u0005_(ref l8N);
	}
}
