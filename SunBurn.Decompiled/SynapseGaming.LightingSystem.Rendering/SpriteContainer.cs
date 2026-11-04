using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using h;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Specialized 2D scene object used to store and render sprites using
/// SunBurn's forward and deferred rendering systems and effects.
///
/// Create an instance using SpriteManager.CreateSpriteContainer().
/// </summary>
public class SpriteContainer : SceneObject
{
	private Vector2 fNB = Vector2.One;

	private Vector2 fNa = Vector2.Zero;

	private bool fNg;

	private GraphicsDevice fNf;

	private L.i<RenderableMesh> fN_0003;

	private L._0005<h.i> fNn;

	private int fN_0019 = -1;

	private h._0005 fNL;

	private Dictionary<int, h._0005> fNv = new Dictionary<int, h._0005>(16);

	internal SpriteContainer(GraphicsDevice P_0, L.i<RenderableMesh> P_1, L._0005<h.i> P_2)
	{
		fNf = P_0;
		fN_0003 = P_1;
		fNn = P_2;
		StaticLightingType = StaticLightingType.Composite;
	}

	/// <summary>
	/// Prepares the container for new sprites, also clears all existing sprites from the container.
	/// </summary>
	public void Begin()
	{
		if (fNg)
		{
			throw new Exception("Begin already called on this object, make sure all Begin calls have an accompanying End call.");
		}
		fNg = true;
		fNf.Indices = null;
		foreach (KeyValuePair<int, h._0005> item in fNv)
		{
			item.Value._0013();
		}
		while (RenderableMeshes.Count > 0)
		{
			RenderableMesh renderableMesh = RenderableMeshes[0];
			fN_0003.Free(renderableMesh);
			Remove(renderableMesh);
		}
	}

	/// <summary>
	/// Adds a sprite to this container. Can only be used between calls to Begin() and End().
	/// </summary>
	/// <param name="effect">Effect applied to the sprite during rendering.</param>
	/// <param name="size">Size of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="position">Position of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="layerdepth">Controls both the z-sorting and the height between
	/// sprites, which is critical for proper shadowing. If shadows are too
	/// disconnected form the caster try reducing the depth between the shadow
	/// caster and receiver.</param>
	public void Add(Effect effect, Vector2 size, Vector2 position, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, 0f, ref fNa, ref fNB, ref fNa, layerdepth);
	}

	/// <summary>
	/// Adds a sprite to this container. Can only be used between calls to Begin() and End().
	/// </summary>
	/// <param name="effect">Effect applied to the sprite during rendering.</param>
	/// <param name="size">Size of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="position">Position of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="rotation">Rotation of the sprite in radians.</param>
	/// <param name="layerdepth">Controls both the z-sorting and the height between
	/// sprites, which is critical for proper shadowing. If shadows are too
	/// disconnected form the caster try reducing the depth between the shadow
	/// caster and receiver.</param>
	public void Add(Effect effect, Vector2 size, Vector2 position, float rotation, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, rotation, ref fNa, ref fNB, ref fNa, layerdepth);
	}

	/// <summary>
	/// Adds a sprite to this container. Can only be used between calls to Begin() and End().
	/// </summary>
	/// <param name="effect">Effect applied to the sprite during rendering.</param>
	/// <param name="size">Size of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="position">Position of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="rotation">Rotation of the sprite in radians.</param>
	/// <param name="origin">Indicates the sprite origin or pivot point (offset from
	/// the sprite center).</param>
	/// <param name="layerdepth">Controls both the z-sorting and the height between
	/// sprites, which is critical for proper shadowing. If shadows are too
	/// disconnected form the caster try reducing the depth between the shadow
	/// caster and receiver.</param>
	public void Add(Effect effect, Vector2 size, Vector2 position, float rotation, Vector2 origin, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, rotation, ref origin, ref fNB, ref fNa, layerdepth);
	}

	/// <summary>
	/// Adds a sprite to this container. Can only be used between calls to Begin() and End().
	/// </summary>
	/// <param name="effect">Effect applied to the sprite during rendering.</param>
	/// <param name="size">Size of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="position">Position of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="uvsize">Indicates the number of times a material will tile
	/// across the sprite.</param>
	/// <param name="uvposition">Indicates the uv offset applied to a material
	/// on the sprite (in uv coordinates, where a single material tile ranges from 0.0f - 1.0f).</param>
	/// <param name="layerdepth">Controls both the z-sorting and the height between
	/// sprites, which is critical for proper shadowing. If shadows are too
	/// disconnected form the caster try reducing the depth between the shadow
	/// caster and receiver.</param>
	public void Add(Effect effect, Vector2 size, Vector2 position, Vector2 uvsize, Vector2 uvposition, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, 0f, ref fNa, ref uvsize, ref uvposition, layerdepth);
	}

	/// <summary>
	/// Adds a sprite to this container. Can only be used between calls to Begin() and End().
	/// </summary>
	/// <param name="effect">Effect applied to the sprite during rendering.</param>
	/// <param name="size">Size of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="position">Position of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="rotation">Rotation of the sprite in radians.</param>
	/// <param name="uvsize">Indicates the number of times a material will tile
	/// across the sprite.</param>
	/// <param name="uvposition">Indicates the uv offset applied to a material
	/// on the sprite (in uv coordinates, where a single material tile ranges from 0.0f - 1.0f).</param>
	/// <param name="layerdepth">Controls both the z-sorting and the height between
	/// sprites, which is critical for proper shadowing. If shadows are too
	/// disconnected form the caster try reducing the depth between the shadow
	/// caster and receiver.</param>
	public void Add(Effect effect, Vector2 size, Vector2 position, float rotation, Vector2 uvsize, Vector2 uvposition, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, rotation, ref fNa, ref uvsize, ref uvposition, layerdepth);
	}

	/// <summary>
	/// Adds a sprite to this container. Can only be used between calls to Begin() and End().
	/// </summary>
	/// <param name="effect">Effect applied to the sprite during rendering.</param>
	/// <param name="size">Size of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="position">Position of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="rotation">Rotation of the sprite in radians.</param>
	/// <param name="origin">Indicates the sprite origin or pivot point (offset from
	/// the sprite center).</param>
	/// <param name="uvsize">Indicates the number of times a material will tile
	/// across the sprite.</param>
	/// <param name="uvposition">Indicates the uv offset applied to a material
	/// on the sprite (in uv coordinates, where a single material tile ranges from 0.0f - 1.0f).</param>
	/// <param name="layerdepth">Controls both the z-sorting and the height between
	/// sprites, which is critical for proper shadowing. If shadows are too
	/// disconnected form the caster try reducing the depth between the shadow
	/// caster and receiver.</param>
	public void Add(Effect effect, Vector2 size, Vector2 position, float rotation, Vector2 origin, Vector2 uvsize, Vector2 uvposition, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, rotation, ref origin, ref uvsize, ref uvposition, layerdepth);
	}

	/// <summary>
	/// Adds a sprite to this container. Can only be used between calls to Begin() and End().
	/// </summary>
	/// <param name="effect">Effect applied to the sprite during rendering.</param>
	/// <param name="effecthashcode">Unique hashcode of the effect.</param>
	/// <param name="size">Size of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="position">Position of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="rotation">Rotation of the sprite in radians.</param>
	/// <param name="origin">Indicates the sprite origin or pivot point (offset from
	/// the sprite center).</param>
	/// <param name="uvsize">Indicates the number of times a material will tile
	/// across the sprite.</param>
	/// <param name="uvposition">Indicates the uv offset applied to a material
	/// on the sprite (in uv coordinates, where a single material tile ranges from 0.0f - 1.0f).</param>
	/// <param name="layerdepth">Controls both the z-sorting and the height between
	/// sprites, which is critical for proper shadowing. If shadows are too
	/// disconnected form the caster try reducing the depth between the shadow
	/// caster and receiver.</param>
	public void Add(Effect effect, int effecthashcode, Vector2 size, Vector2 position, float rotation, Vector2 origin, Vector2 uvsize, Vector2 uvposition, float layerdepth)
	{
		Add(effect, effecthashcode, ref size, ref position, rotation, ref origin, ref uvsize, ref uvposition, layerdepth);
	}

	/// <summary>
	/// Adds a sprite to this container. Can only be used between calls to Begin() and End().
	/// </summary>
	/// <param name="effect">Effect applied to the sprite during rendering.</param>
	/// <param name="size">Size of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="position">Position of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="rotation">Rotation of the sprite in radians.</param>
	/// <param name="origin">Indicates the sprite origin or pivot point (offset from
	/// the sprite center).</param>
	/// <param name="uvsize">Indicates the number of times a material will tile
	/// across the sprite.</param>
	/// <param name="uvposition">Indicates the uv offset applied to a material
	/// on the sprite (in uv coordinates, where a single material tile ranges from 0.0f - 1.0f).</param>
	/// <param name="layerdepth">Controls both the z-sorting and the height between
	/// sprites, which is critical for proper shadowing. If shadows are too
	/// disconnected form the caster try reducing the depth between the shadow
	/// caster and receiver.</param>
	public void Add(Effect effect, ref Vector2 size, ref Vector2 position, float rotation, ref Vector2 origin, ref Vector2 uvsize, ref Vector2 uvposition, float layerdepth)
	{
		Add(effect, effect.GetHashCode(), ref size, ref position, rotation, ref origin, ref uvsize, ref uvposition, layerdepth);
	}

	/// <summary>
	/// Adds a sprite to this container. Can only be used between calls to Begin() and End().
	/// </summary>
	/// <param name="effect">Effect applied to the sprite during rendering.</param>
	/// <param name="effecthashcode">Unique hashcode of the effect.</param>
	/// <param name="size">Size of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="position">Position of the sprite (in world-space if the container
	/// uses an identity world transform, otherwise in object-space)</param>
	/// <param name="rotation">Rotation of the sprite in radians.</param>
	/// <param name="origin">Indicates the sprite origin or pivot point (offset from
	/// the sprite center).</param>
	/// <param name="uvsize">Indicates the number of times a material will tile
	/// across the sprite.</param>
	/// <param name="uvposition">Indicates the uv offset applied to a material
	/// on the sprite (in uv coordinates, where a single material tile ranges from 0.0f - 1.0f).</param>
	/// <param name="layerdepth">Controls both the z-sorting and the height between
	/// sprites, which is critical for proper shadowing. If shadows are too
	/// disconnected form the caster try reducing the depth between the shadow
	/// caster and receiver.</param>
	public void Add(Effect effect, int effecthashcode, ref Vector2 size, ref Vector2 position, float rotation, ref Vector2 origin, ref Vector2 uvsize, ref Vector2 uvposition, float layerdepth)
	{
		if (!fNg)
		{
			throw new Exception("Begin must be called before adding sprites to the container.");
		}
		h._0005 value;
		if (fN_0019 == effecthashcode && fNL != null)
		{
			value = fNL;
		}
		else
		{
			if (!fNv.TryGetValue(effecthashcode, out value))
			{
				value = new h._0005(fNf, fNn, effect);
				fNv.Add(effecthashcode, value);
			}
			fN_0019 = effecthashcode;
			fNL = value;
		}
		value.k(ref size, ref position, rotation, ref origin, ref uvsize, ref uvposition, layerdepth);
	}

	/// <summary>
	/// Finishes all sprite operations until the next call to Begin().
	/// </summary>
	public void End()
	{
		if (!fNg)
		{
			throw new Exception("Begin must be called before calling End.");
		}
		fNg = false;
		foreach (KeyValuePair<int, h._0005> item in fNv)
		{
			h._0005 value = item.Value;
			Effect effect = value.Effect;
			value._0003b();
			foreach (h.i item2 in value.Buffers)
			{
				RenderableMesh renderableMesh = fN_0003.New();
				renderableMesh.Build(this, effect, Matrix.Identity, BoundingSphere.CreateFromBoundingBox(item2.ObjectBoundingBox), item2.ObjectBoundingBox, item2.IndexBuffer, item2.VertexBuffer, 0, PrimitiveType.TriangleList, item2.VertexCount / 4 * 2, 0, item2.VertexCount, 0, detectskinningandlightmapping: false);
				Add(renderableMesh);
			}
		}
	}
}
