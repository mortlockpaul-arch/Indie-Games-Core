using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;

namespace SynapseGaming.LightingSystem.Rendering;

public class RenderableMesh
{
	public class ComparisonIndex
	{
		public class BufferComparisonIndex
		{
			internal IndexBuffer l8a;

			internal VertexBuffer l8_u0013_;

			public BufferComparisonIndex(IndexBuffer indexbuffer, VertexBuffer vertexbuffer)
			{
				l8a = indexbuffer;
				l8_u0013_ = vertexbuffer;
			}

			public override int GetHashCode()
			{
				if (l8a == null)
				{
					return l8_u0013_.GetHashCode() ^ 1;
				}
				return (l8a.GetHashCode() ^ 1) + (l8_u0013_.GetHashCode() ^ 2);
			}

			public override bool Equals(object obj)
			{
				if (!(obj is BufferComparisonIndex bufferComparisonIndex))
				{
					return false;
				}
				if (l8a == bufferComparisonIndex.l8a)
				{
					return l8_u0013_ == bufferComparisonIndex.l8_u0013_;
				}
				return false;
			}
		}

		internal int l8a;

		internal int l8_u0013_;

		internal int l8N;

		internal int l8l;

		public BufferComparisonIndex BufferIndex = new BufferComparisonIndex(null, null);

		public override int GetHashCode()
		{
			if (BufferIndex.l8a == null)
			{
				return (l8a.GetHashCode() ^ 1) + (l8_u0013_.GetHashCode() ^ 2) + (l8N.GetHashCode() ^ 3) + (l8l.GetHashCode() ^ 4) + (BufferIndex.l8_u0013_.GetHashCode() ^ 5);
			}
			return (l8a.GetHashCode() ^ 1) + (l8_u0013_.GetHashCode() ^ 2) + (l8N.GetHashCode() ^ 3) + (l8l.GetHashCode() ^ 4) + (BufferIndex.l8a.GetHashCode() ^ 5) + (BufferIndex.l8_u0013_.GetHashCode() ^ 6);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ComparisonIndex comparisonIndex))
			{
				return false;
			}
			if (l8a == comparisonIndex.l8a && l8_u0013_ == comparisonIndex.l8_u0013_ && l8N == comparisonIndex.l8N && l8l == comparisonIndex.l8l && BufferIndex.l8a == comparisonIndex.BufferIndex.l8a)
			{
				return BufferIndex.l8_u0013_ == comparisonIndex.BufferIndex.l8_u0013_;
			}
			return false;
		}
	}

	private string l8a = string.Empty;

	internal ISceneObject l8_u0013_;

	internal int l8N;

	internal bool l8l;

	internal Matrix l8L;

	internal Matrix l8w;

	internal BoundingSphere l8J;

	internal BoundingBox l8B;

	internal Matrix l8s;

	internal Matrix l8h;

	internal Effect l8_u0002_;

	internal Matrix l8v;

	internal Matrix l8Y;

	internal int l8_u0005_;

	internal PrimitiveType l8b;

	internal CullMode l8I = CullMode.CullCounterClockwiseFace;

	public ComparisonIndex Index = new ComparisonIndex();

	internal bool l85 = true;

	internal int l8W;

	internal int l8Z;

	internal int l8T;

	internal bool l8_u0014_;

	internal bool l8E;

	internal bool l8f;

	internal bool l8_u0016_;

	internal bool l86;

	internal bool l8x;

	internal TransparencyMode l8_u0004_;

	public string Name
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
		}
	}

	public ISceneObject SceneObject
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
		}
	}

	public int UniqueId
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

	public bool CanLightMap => l8l;

	public Effect Effect
	{
		get
		{
			return l8_u0002_;
		}
		set
		{
			l8_u0002_ = value;
			RemapEffect();
			CalculateMaterialInfo();
		}
	}

	public Matrix World => l8v;

	public Matrix WorldToMesh => l8Y;

	public Matrix MeshToObject
	{
		get
		{
			return l8L;
		}
		set
		{
			l8L = value;
			Matrix.Invert(ref l8L, out l8w);
			Lz();
		}
	}

	public IndexBuffer IndexBuffer => Index.BufferIndex.l8a;

	public VertexBuffer VertexBuffer => Index.BufferIndex.l8_u0013_;

	public int VertexStreamOffset => Index.l8l;

	public int VertexBase
	{
		get
		{
			return Index.l8N;
		}
		set
		{
			Index.l8N = value;
		}
	}

	public int VertexCount
	{
		get
		{
			return l8_u0005_;
		}
		set
		{
			l8_u0005_ = value;
		}
	}

	public int ElementStart
	{
		get
		{
			return Index.l8_u0013_;
		}
		set
		{
			Index.l8_u0013_ = value;
		}
	}

	public PrimitiveType PrimitiveType
	{
		get
		{
			return l8b;
		}
		set
		{
			l8b = value;
		}
	}

	public int PrimitiveCount
	{
		get
		{
			return Index.l8a;
		}
		set
		{
			Index.l8a = value;
		}
	}

	public CullMode CullMode => l8I;

	public BoundingSphere MeshBoundingSphere
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = value;
		}
	}

	public BoundingBox MeshBoundingBox
	{
		get
		{
			return l8B;
		}
		set
		{
			l8B = value;
		}
	}

	public void Build(ISceneObject sceneobject, ModelMesh mesh, ModelMeshPart part)
	{
		Build(sceneobject, mesh, part, part.Effect);
	}

	public void Build(ISceneObject sceneobject, ModelMesh mesh, ModelMeshPart part, Effect overrideeffect)
	{
		l8a = mesh.Name;
		Matrix identity = Matrix.Identity;
		for (ModelBone modelBone = mesh.ParentBone; modelBone != null; modelBone = modelBone.Parent)
		{
			identity *= modelBone.Transform;
		}
		BoundingSphere sphere;
		BoundingBox result;
		if (mesh.Tag is IBoundingVolume)
		{
			IBoundingVolume boundingVolume = mesh.Tag as IBoundingVolume;
			sphere = boundingVolume.BoundingSphere;
			result = boundingVolume.BoundingBox;
		}
		else
		{
			sphere = mesh.BoundingSphere;
			BoundingBox.CreateFromSphere(ref sphere, out result);
		}
		Build(sceneobject, overrideeffect, identity, sphere, result, part.IndexBuffer, part.VertexBuffer, part.StartIndex, PrimitiveType.TriangleList, part.PrimitiveCount, part.VertexOffset, part.NumVertices, 0, detectskinningandlightmapping: true);
	}

	public void Build(ISceneObject sceneobject, Effect effect, Matrix objectspace, BoundingSphere meshboundingsphere, BoundingBox meshboundingbox, IndexBuffer indexbuffer, VertexBuffer vertexbuffer, int elementstart, PrimitiveType primitivetype, int primitivecount, int vertexbase, int vertexcount, int vertexstreamoffset, bool detectskinningandlightmapping)
	{
		bool flag = false;
		bool flag2 = false;
		l8l = false;
		if (detectskinningandlightmapping)
		{
			VertexElement[] vertexElements = vertexbuffer.VertexDeclaration.GetVertexElements();
			for (int i = 0; i < vertexElements.Length; i++)
			{
				VertexElement vertexElement = vertexElements[i];
				switch (vertexElement.VertexElementUsage)
				{
				case VertexElementUsage.BlendWeight:
					flag = true;
					break;
				case VertexElementUsage.BlendIndices:
					flag2 = true;
					break;
				case VertexElementUsage.TextureCoordinate:
					if (vertexElement.UsageIndex == 1)
					{
						l8l = true;
					}
					break;
				}
			}
		}
		l8_u0013_ = sceneobject;
		l8_u0002_ = effect;
		l8L = objectspace;
		Matrix.Invert(ref l8L, out l8w);
		l8J = meshboundingsphere;
		l8B = meshboundingbox;
		Index.BufferIndex.l8a = indexbuffer;
		Index.l8_u0013_ = elementstart;
		l8b = primitivetype;
		Index.l8a = primitivecount;
		Index.l8N = vertexbase;
		Index.BufferIndex.l8_u0013_ = vertexbuffer;
		l8_u0005_ = vertexcount;
		Index.l8l = vertexstreamoffset;
		if (!(l8_u0002_ is IRenderableEffect) && !(l8_u0002_ is IEffectMatrices))
		{
			throw new ArgumentException("Only effects derived from IRenderableEffect and IEffectMatrices are supported by built-in renderers.");
		}
		if (l8_u0002_ is ISkinnedEffect { Skinned: not false } && (!flag || !flag2))
		{
			throw new ArgumentException("Effects that implement skinning require object vertex buffers to supply both blending weight and indices in the vertex stream.");
		}
		if (Index.BufferIndex.l8a != null)
		{
			l8Z = CoreHelper.GetHashCode(Index.BufferIndex.l8a.GetHashCode(), Index.BufferIndex.l8_u0013_.GetHashCode(), Index.l8l);
		}
		else
		{
			l8Z = CoreHelper.GetHashCode(Index.BufferIndex.l8_u0013_.GetHashCode(), Index.l8l);
		}
		Matrix world = Matrix.Identity;
		SetWorldAndWorldToObject(ref world, ref world);
		CalculateMaterialInfo();
	}

	public void CalculateMaterialInfo()
	{
		if (l8_u0002_ != null)
		{
			l8W = l8_u0002_.GetHashCode();
		}
		else
		{
			l8W = 0;
		}
		EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(l8_u0002_);
		IRenderableEffect renderableEffect = effectTypeCaster.RenderableEffect;
		ISkinnedEffect skinnedEffect = effectTypeCaster.SkinnedEffect;
		IShadowGenerateEffect shadowGenerateEffect = effectTypeCaster.ShadowGenerateEffect;
		ITransparentEffect transparentEffect = effectTypeCaster.TransparentEffect;
		ITerrainEffect terrainEffect = effectTypeCaster.TerrainEffect;
		if (transparentEffect != null)
		{
			l8_u0004_ = transparentEffect.TransparencyMode;
		}
		else
		{
			l8_u0004_ = TransparencyMode.None;
		}
		l8E = skinnedEffect?.Skinned ?? false;
		l8f = l8_u0004_ != TransparencyMode.None;
		l8_u0016_ = renderableEffect?.DoubleSided ?? false;
		l86 = shadowGenerateEffect?.SupportsShadowGeneration ?? false;
		l8x = terrainEffect != null;
		if (terrainEffect != null)
		{
			Lp(terrainEffect);
		}
	}

	private void Lp(ITerrainEffect param_0)
	{
		if (!(param_0 is BaseTerrainEffect baseTerrainEffect))
		{
			return;
		}
		baseTerrainEffect.N_u0003_(CalculateMaterialInfo);
		baseTerrainEffect.N_u0011_(CalculateMaterialInfo);
		float tileWidth = baseTerrainEffect.GetTileWidth();
		float num = tileWidth * 0.5f;
		float num2 = (float)Math.Ceiling((float)baseTerrainEffect.TileRepeatCount * 0.5f);
		float num3 = num2 * (0f - tileWidth) + num;
		float num4 = (float)baseTerrainEffect.TileRepeatCount * tileWidth + num3;
		float heightScale = baseTerrainEffect.HeightScale;
		l8B = new BoundingBox(new Vector3(num3, num3, 0f), new Vector3(num4, num4, heightScale));
		BoundingSphere.CreateFromBoundingBox(ref l8B, out l8J);
		if (l8_u0013_ is SceneObject sceneObject)
		{
			sceneObject.CalculateBounds();
			if (sceneObject.ContainingManagers.GetItem(SceneInterface.ObjectManagerType) is IObjectManager objectManager)
			{
				objectManager.Move(sceneObject);
			}
		}
	}

	public void RemapEffect()
	{
	}

	public void SetWorldAndWorldToObject(Matrix world, Matrix worldtoobject)
	{
		SetWorldAndWorldToObject(ref world, ref worldtoobject);
	}

	public void SetWorldAndWorldToObject(ref Matrix world, ref Matrix worldtoobject)
	{
		l8s = world;
		l8h = worldtoobject;
		Lz();
	}

	private void Lz()
	{
		Matrix.Multiply(ref l8L, ref l8s, out l8v);
		Matrix.Multiply(ref l8h, ref l8w, out l8Y);
		if ((double)l8v.Determinant() >= 0.0)
		{
			l8I = CullMode.CullCounterClockwiseFace;
		}
		else
		{
			l8I = CullMode.CullClockwiseFace;
		}
	}

	public virtual RenderableMesh Clone()
	{
		RenderableMesh renderableMesh = new RenderableMesh();
		renderableMesh.Build(l8_u0013_, l8_u0002_, l8L, l8J, l8B, Index.BufferIndex.l8a, Index.BufferIndex.l8_u0013_, Index.l8_u0013_, l8b, Index.l8a, Index.l8N, l8_u0005_, Index.l8l, detectskinningandlightmapping: true);
		return renderableMesh;
	}
}
