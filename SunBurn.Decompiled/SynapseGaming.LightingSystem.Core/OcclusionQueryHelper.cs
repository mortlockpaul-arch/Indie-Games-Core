using System;
using System.Collections.Generic;
using System.Threading;
using _0013;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Helper class that provides occlusion testing of object and/or object group bounding areas.  The occlusion
/// test is asynchronous, calling RunOcclusionQuery does not block the thread, which allows other cpu related
/// work to be done in parallel.  Calling IsObjectVisible however IS blocking, so try to place as much cpu
/// related work between these two calls to avoid wasting cpu cycles.
/// </summary>
/// <typeparam name="TKey">Object type used to store and lookup query results.</typeparam>
public class OcclusionQueryHelper<TKey> : IDisposable
{
	private class nNB
	{
		public OcclusionQuery OcclusionQuery;

		public bool ContainsCamera;

		public BoundingBox BoundingBox;

		public nNB(GraphicsDevice device, BoundingBox bounds)
		{
			OcclusionQuery = new OcclusionQuery(device);
			ContainsCamera = false;
			BoundingBox = bounds;
		}
	}

	private bool fNB;

	private GraphicsDevice fNa;

	private L.g fNg;

	private List<nNB> fNf = new List<nNB>(32);

	private Dictionary<TKey, nNB> fN_0003 = new Dictionary<TKey, nNB>(32);

	private BlendState fNn = new BlendState
	{
		ColorDestinationBlend = Blend.One,
		ColorSourceBlend = Blend.Zero,
		AlphaDestinationBlend = Blend.One,
		AlphaSourceBlend = Blend.Zero
	};

	private BlendState fN_0019 = new BlendState();

	/// <summary>
	/// Creates a new OcclusionQueryHelper instance.
	/// </summary>
	/// <param name="device"></param>
	/// <param name="writingtofloattarget">Indicates the query occurs
	/// on a floating point render target.</param>
	public OcclusionQueryHelper(GraphicsDevice device, bool writingtofloattarget)
	{
		fNa = device;
		fNB = writingtofloattarget;
		fNg = new L.g(fNa);
	}

	/// <summary>
	/// Adds an object and its bounding area to the occlusion testing list.
	/// </summary>
	/// <param name="key">Object to add to the occlusion testing list.</param>
	/// <param name="worldbounds">Object's world bounding area.</param>
	public void SubmitObject(TKey key, BoundingBox worldbounds)
	{
		if (fNf.Count < 1)
		{
			fN_0003.Add(key, new nNB(fNa, worldbounds));
			return;
		}
		nNB nNB2 = fNf[0];
		nNB2.ContainsCamera = false;
		nNB2.BoundingBox = worldbounds;
		fN_0003.Add(key, nNB2);
		fNf.RemoveAt(0);
	}

	/// <summary>
	/// Asynchronously performs the occlusion test (does not block the thread), which allows other cpu related
	/// work to be done in parallel.
	/// </summary>
	/// <param name="scenestate">Scene state used to render the current view.</param>
	/// <param name="detectionpadding">Used to avoid false "invisible" results by padding
	/// the detection area. A good value is 1.4 (ie: x1.4 or 140%), however this value is
	/// affected by the projection near clipping plane.</param>
	public void RunOcclusionQuery(ISceneState scenestate, float detectionpadding)
	{
		if (fN_0003.Count < 1)
		{
			return;
		}
		LightingSystemPerformance.Begin("OcclusionQueryHelper.RunOcclusionQuery");
		BlendState blendState = fNa.BlendState;
		RasterizerState rasterizerState = fNa.RasterizerState;
		DepthStencilState depthStencilState = fNa.DepthStencilState;
		fNa.RasterizerState = RasterizerState.CullNone;
		fNa.DepthStencilState = DepthStencilState.DepthRead;
		if (fNB)
		{
			fNa.BlendState = fN_0019;
		}
		else
		{
			fNa.BlendState = fNn;
		}
		BasicEffect basicEffect = fNg.DefaultEffect;
		basicEffect.World = Matrix.Identity;
		basicEffect.View = scenestate.View;
		basicEffect.Projection = scenestate.Projection;
		EffectPass effectPass = basicEffect.CurrentTechnique.Passes[0];
		Plane near = scenestate.ViewFrustum.Near;
		foreach (KeyValuePair<TKey, nNB> item in fN_0003)
		{
			BoundingBox boundingBox = item.Value.BoundingBox;
			BoundingBox boundingBox2 = L._0019.a9(boundingBox, detectionpadding);
			item.Value.ContainsCamera = boundingBox2.Intersects(near) == PlaneIntersectionType.Intersecting;
			if (!item.Value.ContainsCamera)
			{
				OcclusionQuery occlusionQuery = item.Value.OcclusionQuery;
				basicEffect.World = fNg.e(boundingBox);
				effectPass.Apply();
				occlusionQuery.Begin();
				fNg._5();
				occlusionQuery.End();
			}
		}
		fNa.BlendState = blendState;
		fNa.RasterizerState = rasterizerState;
		fNa.DepthStencilState = depthStencilState;
	}

	/// <summary>
	/// Retrieves the occlusion test results for an object. This call DOES block the
	/// thread until the object results are available.
	///
	/// To maximize cpu/gpu parallelization:
	///   -always retrieve the test results in the same order objects were submitted.
	///   -always try to perform as much cpu related work between running the test
	///    and this call.
	/// </summary>
	/// <param name="key">Object to retrieve occlusion test result from.</param>
	/// <returns></returns>
	public bool IsObjectVisible(TKey key)
	{
		if (!fN_0003.TryGetValue(key, out var value))
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

	/// <summary>
	/// Removes all objects from the occlusion testing list.
	/// </summary>
	public void Clear()
	{
		foreach (KeyValuePair<TKey, nNB> item in fN_0003)
		{
			fNf.Add(item.Value);
		}
		fN_0003.Clear();
	}

	/// <summary>
	/// Disposes all related graphics objects.
	/// </summary>
	public void Dispose()
	{
		Clear();
		foreach (nNB item in fNf)
		{
			item.OcclusionQuery.Dispose();
		}
		fNf.Clear();
		_0013.B.nu(ref fNg);
	}
}
