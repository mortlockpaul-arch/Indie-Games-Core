using System;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Mesh class used by the built-in renderers that provides
/// properties common to all rendering in XNA / DirectX.
/// </summary>
public class RenderableMesh
{
	private string fNB = string.Empty;

	internal ISceneObject fNa;

	internal int fNg;

	internal bool fNf;

	internal Matrix fN_0003;

	internal Matrix fNn;

	internal BoundingSphere fN_0019;

	internal BoundingBox fNL;

	internal Matrix fNv;

	internal Matrix fN_0012;

	internal Effect fN_0015;

	internal Matrix fN8;

	internal Matrix fNi;

	internal IndexBuffer fN_0005;

	internal VertexBuffer fNj;

	internal int fNo;

	internal int fNc;

	internal int fNd;

	internal int fNw;

	internal PrimitiveType fNs;

	internal int fN_0011;

	internal CullMode fNQ = CullMode.CullCounterClockwiseFace;

	internal bool fNG = true;

	internal int fN_0017;

	internal int fNm;

	internal int fNh;

	internal bool fNR;

	internal bool fNW;

	internal bool fN9;

	internal bool fN_0013;

	internal bool fNq;

	internal bool fN_000F;

	internal TransparencyMode fN_0014;

	/// <summary>
	/// The mesh's current name.
	/// </summary>
	public string Name
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
		}
	}

	/// <summary>
	/// Parent scene object this mesh is contained in.
	/// </summary>
	public ISceneObject SceneObject
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
		}
	}

	/// <summary>
	/// Unique id used to identify the mesh across multiple scene loads / reloads.
	/// </summary>
	public int UniqueId
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
	/// Indicates the mesh is capable of using light maps.
	/// </summary>
	public bool CanLightMap => fNf;

	/// <summary>
	/// Effect applied to the mesh during rendering.
	/// </summary>
	public Effect Effect
	{
		get
		{
			return fN_0015;
		}
		set
		{
			fN_0015 = value;
			RemapEffect();
			CalculateMaterialInfo();
		}
	}

	/// <summary>
	/// Complete world space transform of the mesh (from mesh-space to
	/// world-space, ie: includes the mesh's object-space transform).
	/// </summary>
	public Matrix World => fN8;

	/// <summary>
	/// Inverse complete world space transform of the mesh (from world-space
	/// to mesh-space, ie: includes the mesh's object-space transform).
	/// </summary>
	public Matrix WorldToMesh => fNi;

	/// <summary>
	/// Object space transform of the mesh.
	/// </summary>
	public Matrix MeshToObject
	{
		get
		{
			return fN_0003;
		}
		set
		{
			fN_0003 = value;
			Matrix.Invert(ref fN_0003, out fNn);
			_0003U();
		}
	}

	/// <summary>
	/// IndexBuffer that contains the mesh geometry.
	/// </summary>
	public IndexBuffer IndexBuffer => fN_0005;

	/// <summary>
	/// VertexBuffer that contains the mesh geometry.
	/// </summary>
	public VertexBuffer VertexBuffer => fNj;

	/// <summary>
	/// Offset in bytes from the beginning of the vertex buffer to start reading data.
	/// </summary>
	public int VertexStreamOffset => fNo;

	/// <summary>
	/// Offset added to each index in the index buffer during rendering.
	/// </summary>
	public int VertexBase
	{
		get
		{
			return fNc;
		}
		set
		{
			fNc = value;
		}
	}

	/// <summary>
	/// Number of vertices in the vertex buffer range required to draw the mesh.
	/// For instance, a quad rendering vertices at indices (2, 5, 6, 9) requires
	/// a vertex buffer range of 8 vertices (vertices 2 – 9 inclusive).
	/// </summary>
	public int VertexCount
	{
		get
		{
			return fNd;
		}
		set
		{
			fNd = value;
		}
	}

	/// <summary>
	/// Index into the buffer that mesh geometry begins. For indexed meshes this
	/// is the first index in the index buffer. For non-indexed meshes this is
	/// the first vertex in the vertex buffer.
	/// </summary>
	public int ElementStart
	{
		get
		{
			return fNw;
		}
		set
		{
			fNw = value;
		}
	}

	/// <summary>
	/// Primitive format the mesh geometry is stored in.
	/// </summary>
	public PrimitiveType PrimitiveType
	{
		get
		{
			return fNs;
		}
		set
		{
			fNs = value;
		}
	}

	/// <summary>
	/// Number of primitives in the mesh geometry.
	/// </summary>
	public int PrimitiveCount
	{
		get
		{
			return fN_0011;
		}
		set
		{
			fN_0011 = value;
		}
	}

	/// <summary>
	/// Cull mode used to ensure the mesh is rendered correctly.
	/// </summary>
	public CullMode CullMode => fNQ;

	/// <summary>
	/// Object-space bounding area that completely contains the mesh.
	/// </summary>
	public BoundingSphere MeshBoundingSphere
	{
		get
		{
			return fN_0019;
		}
		set
		{
			fN_0019 = value;
		}
	}

	/// <summary>
	/// Object-space bounding area that completely contains the mesh.
	/// </summary>
	public BoundingBox MeshBoundingBox
	{
		get
		{
			return fNL;
		}
		set
		{
			fNL = value;
		}
	}

	/// <summary>
	/// Creates an empty RenderableMesh instance.
	///
	/// Warning: Build must be called to finish constructing the mesh before
	/// attempting to render it.
	/// </summary>
	public RenderableMesh()
	{
	}

	/// <summary>
	/// Updates the mesh with new effect and geometry data.
	/// </summary>
	/// <param name="sceneobject">Parent scene object.</param>
	/// <param name="mesh">XNA ModelMesh to retrieve information from.</param>
	/// <param name="part">XNA ModelMeshPart to retrieve information from.</param>
	public void Build(ISceneObject sceneobject, ModelMesh mesh, ModelMeshPart part)
	{
		Build(sceneobject, mesh, part, part.Effect);
	}

	/// <summary>
	/// Updates the mesh with new effect and geometry data.
	/// </summary>
	/// <param name="sceneobject">Parent scene object.</param>
	/// <param name="mesh">XNA ModelMesh to retrieve information from.</param>
	/// <param name="part">XNA ModelMeshPart to retrieve information from.</param>
	/// <param name="overrideeffect">Effect applied to the mesh during rendering.</param>
	public void Build(ISceneObject sceneobject, ModelMesh mesh, ModelMeshPart part, Effect overrideeffect)
	{
		fNB = mesh.Name;
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

	/// <summary>
	/// Updates the mesh with new effect and geometry data.
	/// </summary>
	/// <param name="sceneobject">Parent scene object.</param>
	/// <param name="effect">Effect applied to the mesh during rendering.</param>
	/// <param name="indexbuffer">IndexBuffer that contains the mesh geometry.</param>
	/// <param name="vertexbuffer">VertexBuffer that contains the mesh geometry.</param>
	/// <param name="elementstart">Index into the buffer that mesh geometry begins. For indexed meshes this
	/// is the first index in the index buffer. For non-indexed meshes this is
	/// the first vertex in the vertex buffer.</param>
	/// <param name="primitivetype">Primitive format the mesh geometry is stored in.</param>
	/// <param name="primitivecount">Number of primitives in the mesh geometry.</param>
	/// <param name="vertexbase">Offset added to each index in the index buffer during rendering.</param>
	/// <param name="vertexcount">Number of vertices in the vertex buffer range required to
	/// draw the mesh.  For instance, a quad rendering vertices at indices (2, 5, 6, 9) requires
	/// a vertex buffer range of 8 vertices (vertices 2 – 9 inclusive).</param>
	/// <param name="vertexstreamoffset">Offset in bytes from the beginning of the vertex
	/// buffer to start reading data.</param>
	/// <param name="objectspace">Mesh object-space matrix.</param>
	/// <param name="meshboundingsphere">Smallest mesh space bounding sphere that
	/// completely encloses the object.</param>
	/// <param name="meshboundingbox">Smallest mesh space bounding box that
	/// completely encloses the object.</param>
	/// <param name="detectskinningandlightmapping">Indicates if the mesh should test for skinning
	/// and light mapping support. Only necessary if the provided effect supports these features
	/// and the game will use them. Testing for the features allocates memory.</param>
	public void Build(ISceneObject sceneobject, Effect effect, Matrix objectspace, BoundingSphere meshboundingsphere, BoundingBox meshboundingbox, IndexBuffer indexbuffer, VertexBuffer vertexbuffer, int elementstart, PrimitiveType primitivetype, int primitivecount, int vertexbase, int vertexcount, int vertexstreamoffset, bool detectskinningandlightmapping)
	{
		bool flag = false;
		bool flag2 = false;
		fNf = false;
		if (detectskinningandlightmapping)
		{
			VertexElement[] vertexElements = vertexbuffer.VertexDeclaration.GetVertexElements();
			for (int k = 0; k < vertexElements.Length; k++)
			{
				VertexElement vertexElement = vertexElements[k];
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
						fNf = true;
					}
					break;
				}
			}
		}
		fNa = sceneobject;
		fN_0015 = effect;
		fN_0003 = objectspace;
		Matrix.Invert(ref fN_0003, out fNn);
		fN_0019 = meshboundingsphere;
		fNL = meshboundingbox;
		fN_0005 = indexbuffer;
		fNw = elementstart;
		fNs = primitivetype;
		fN_0011 = primitivecount;
		fNc = vertexbase;
		fNj = vertexbuffer;
		fNd = vertexcount;
		fNo = vertexstreamoffset;
		if (!(fN_0015 is IRenderableEffect) && !(fN_0015 is IEffectMatrices))
		{
			throw new ArgumentException("Only effects derived from IRenderableEffect and IEffectMatrices are supported by built-in renderers.");
		}
		if (fN_0015 is ISkinnedEffect { Skinned: not false } && (!flag || !flag2))
		{
			throw new ArgumentException("Effects that implement skinning require object vertex buffers to supply both blending weight and indices in the vertex stream.");
		}
		if (fN_0005 != null)
		{
			fNm = L._0019.au(fN_0005.GetHashCode(), fNj.GetHashCode(), fNo);
		}
		else
		{
			fNm = L._0019.au(fNj.GetHashCode(), fNo);
		}
		Matrix world = Matrix.Identity;
		SetWorldAndWorldToObject(ref world, ref world);
		CalculateMaterialInfo();
	}

	/// <summary>
	/// Recalculates the mesh batching information. This may become necessary
	/// if the mesh effect changes from a non-transparent mode to transparent.
	/// </summary>
	public void CalculateMaterialInfo()
	{
		if (fN_0015 != null)
		{
			fN_0017 = fN_0015.GetHashCode();
		}
		else
		{
			fN_0017 = 0;
		}
		IRenderableEffect renderableEffect = fN_0015 as IRenderableEffect;
		ISkinnedEffect skinnedEffect = fN_0015 as ISkinnedEffect;
		IShadowGenerateEffect shadowGenerateEffect = fN_0015 as IShadowGenerateEffect;
		if (fN_0015 is ITransparentEffect transparentEffect)
		{
			fN_0014 = transparentEffect.TransparencyMode;
		}
		else
		{
			fN_0014 = TransparencyMode.None;
		}
		fNW = skinnedEffect?.Skinned ?? false;
		fN9 = fN_0014 != TransparencyMode.None;
		fN_0013 = renderableEffect?.DoubleSided ?? false;
		fNq = shadowGenerateEffect?.SupportsShadowGeneration ?? false;
		fN_000F = fN_0015 is ITerrainEffect;
	}

	/// <summary>
	/// Should be called by custom renderers when receiving a ReplaceEffect event from
	/// the editor. Replaces the current effect with an editor assigned effect.
	/// </summary>
	public void RemapEffect()
	{
	}

	/// <summary>
	/// Sets both the world and inverse world matrices.  Used to improve
	/// performance when the world matrix is set, by providing a cached
	/// or precalculated inverse matrix with the world matrix.
	///
	/// Note: the matrix should only contain the objectToWorld (not the meshToWorld)
	/// transform. The mesh specific meshToObject transform is applied using the
	/// MeshToObject property.
	/// </summary>
	/// <param name="world">World space transform of the object.</param>
	/// <param name="worldtoobject">Inverse world space transform of the object.</param>
	public void SetWorldAndWorldToObject(Matrix world, Matrix worldtoobject)
	{
		SetWorldAndWorldToObject(ref world, ref worldtoobject);
	}

	/// <summary>
	/// Sets both the world and inverse world matrices.  Used to improve
	/// performance when the world matrix is set, by providing a cached
	/// or precalculated inverse matrix with the world matrix.
	///
	/// Note: the matrix should only contain the objectToWorld (not the meshToWorld)
	/// transform. The mesh specific meshToObject transform is applied using the
	/// MeshToObject property.
	/// </summary>
	/// <param name="world">World space transform of the object.</param>
	/// <param name="worldtoobject">Inverse world space transform of the object.</param>
	public void SetWorldAndWorldToObject(ref Matrix world, ref Matrix worldtoobject)
	{
		fNv = world;
		fN_0012 = worldtoobject;
		_0003U();
	}

	private void _0003U()
	{
		Matrix.Multiply(ref fN_0003, ref fNv, out fN8);
		Matrix.Multiply(ref fN_0012, ref fNn, out fNi);
		if ((double)fN8.Determinant() >= 0.0)
		{
			fNQ = CullMode.CullCounterClockwiseFace;
		}
		else
		{
			fNQ = CullMode.CullClockwiseFace;
		}
	}
}
