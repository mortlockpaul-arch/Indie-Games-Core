using System;
using System.Collections.Generic;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;
using x;

namespace SynapseGaming.LightingSystem.Rendering;

public class SpriteContainer : SceneObject
{
	private Vector2 l8a = Vector2.One;

	private Vector2 l8_u0013_ = Vector2.Zero;

	private bool l8N;

	private GraphicsDevice l8l;

	private SpriteBatch l8L;

	private Texture2D l8w;

	private B.h<RenderableMesh> l8J;

	private B._u0002_<x.Y> l8B;

	private int l8s = -1;

	private x._u0005_ l8h;

	private Dictionary<int, x._u0005_> l8_u0002_ = new Dictionary<int, x._u0005_>(16);

	internal SpriteContainer(GraphicsDevice param_0, B.h<RenderableMesh> param_1, B._u0002_<x.Y> param_2)
	{
		l8l = param_0;
		l8J = param_1;
		l8B = param_2;
		l8L = SunBurnCoreSystem.Instance._u0013_g();
		l8w = SunBurnCoreSystem.Instance._u0013_S("White");
		StaticLightingType = StaticLightingType.Composite;
	}

	public void Begin()
	{
		if (l8N)
		{
			throw new Exception("Begin already called on this object, make sure all Begin calls have an accompanying End call.");
		}
		l8N = true;
		l8l.Indices = null;
		foreach (KeyValuePair<int, x._u0005_> item in l8_u0002_)
		{
			item.Value._id_3();
		}
		while (RenderableMeshes.Count > 0)
		{
			RenderableMesh renderableMesh = RenderableMeshes[0];
			l8J.Free(renderableMesh);
			Remove(renderableMesh);
		}
	}

	public void Add(Effect effect, Vector2 size, Vector2 position, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, 0f, ref l8_u0013_, ref l8a, ref l8_u0013_, layerdepth);
	}

	public void Add(Effect effect, Vector2 size, Vector2 position, float rotation, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, rotation, ref l8_u0013_, ref l8a, ref l8_u0013_, layerdepth);
	}

	public void Add(Effect effect, Vector2 size, Vector2 position, float rotation, Vector2 origin, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, rotation, ref origin, ref l8a, ref l8_u0013_, layerdepth);
	}

	public void Add(Effect effect, Vector2 size, Vector2 position, Vector2 uvsize, Vector2 uvposition, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, 0f, ref l8_u0013_, ref uvsize, ref uvposition, layerdepth);
	}

	public void Add(Effect effect, Vector2 size, Vector2 position, float rotation, Vector2 uvsize, Vector2 uvposition, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, rotation, ref l8_u0013_, ref uvsize, ref uvposition, layerdepth);
	}

	public void Add(Effect effect, Vector2 size, Vector2 position, float rotation, Vector2 origin, Vector2 uvsize, Vector2 uvposition, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, rotation, ref origin, ref uvsize, ref uvposition, layerdepth);
	}

	public void Add(Effect effect, int effecthashcode, Vector2 size, Vector2 position, float rotation, Vector2 origin, Vector2 uvsize, Vector2 uvposition, float layerdepth)
	{
		Add(effect, effecthashcode, ref size, ref position, rotation, ref origin, ref uvsize, ref uvposition, layerdepth);
	}

	public void Add(Effect effect, ref Vector2 size, ref Vector2 position, float rotation, ref Vector2 origin, ref Vector2 uvsize, ref Vector2 uvposition, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, rotation, ref origin, ref uvsize, ref uvposition, layerdepth);
	}

	public void Add(Effect effect, int effecthashcode, ref Vector2 size, ref Vector2 position, float rotation, ref Vector2 origin, ref Vector2 uvsize, ref Vector2 uvposition, float layerdepth)
	{
		if (!l8N)
		{
			throw new Exception("Begin must be called before adding sprites to the container.");
		}
		x._u0005_ value;
		if (l8s == effecthashcode && l8h != null)
		{
			value = l8h;
		}
		else
		{
			if (!l8_u0002_.TryGetValue(effecthashcode, out value))
			{
				value = new x._u0005_(l8l, l8B, effect);
				l8_u0002_.Add(effecthashcode, value);
			}
			l8s = effecthashcode;
			l8h = value;
		}
		value._u0006_(ref size, ref position, rotation, ref origin, ref uvsize, ref uvposition, layerdepth);
	}

	public void End()
	{
		if (!l8N)
		{
			throw new Exception("Begin must be called before calling End.");
		}
		l8N = false;
		l8L.Begin();
		l8L.Draw(l8w, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 0f, SpriteEffects.None, 0f);
		l8L.End();
		foreach (KeyValuePair<int, x._u0005_> item in l8_u0002_)
		{
			x._u0005_ value = item.Value;
			Effect effect = value.Effect;
			value.L_u0001_();
			foreach (x.Y item2 in value.Buffers)
			{
				RenderableMesh renderableMesh = l8J.New();
				renderableMesh.Build(this, effect, Matrix.Identity, BoundingSphere.CreateFromBoundingBox(item2.ObjectBoundingBox), item2.ObjectBoundingBox, item2.IndexBuffer, item2.VertexBuffer, 0, PrimitiveType.TriangleList, item2.VertexCount / 4 * 2, 0, item2.VertexCount, 0, detectskinningandlightmapping: false);
				Add(renderableMesh);
			}
		}
	}
}
