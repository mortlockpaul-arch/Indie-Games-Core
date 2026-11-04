using System;
using System.Runtime.CompilerServices;
using _0013;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Represents geometry data that can be shared between multiple
/// scene objects (similar to xna Model).
///
/// Generally loaded through the xna content manager.
/// </summary>
public class MeshData : IDisposable
{
	private VertexBuffer fNB;

	private IndexBuffer fNa;

	private Effect fNg;

	[CompilerGenerated]
	private Matrix fNf;

	[CompilerGenerated]
	private bool fN_0003;

	[CompilerGenerated]
	private int fNn;

	[CompilerGenerated]
	private int fN_0019;

	[CompilerGenerated]
	private int fNL;

	[CompilerGenerated]
	private BoundingSphere fNv;

	/// <summary>
	/// Object space transform of the mesh.
	/// </summary>
	public Matrix MeshToObject
	{
		[CompilerGenerated]
		get
		{
			return fNf;
		}
		[CompilerGenerated]
		set
		{
			fNf = value;
		}
	}

	/// <summary>
	/// Indicates the object bounding area spans the entire world and
	/// the object is always visible.
	/// </summary>
	public bool InfiniteBounds
	{
		[CompilerGenerated]
		get
		{
			return fN_0003;
		}
		[CompilerGenerated]
		set
		{
			fN_0003 = value;
		}
	}

	/// <summary>
	/// Number of primitives in the mesh geometry.
	/// </summary>
	public int PrimitiveCount
	{
		[CompilerGenerated]
		get
		{
			return fNn;
		}
		[CompilerGenerated]
		set
		{
			fNn = value;
		}
	}

	/// <summary>
	/// Number of vertices in the vertex buffer range required to draw the mesh.
	/// For instance, a quad rendering vertices at indices (2, 5, 6, 9) requires
	/// a vertex buffer range of 8 vertices (vertices 2 – 9 inclusive).
	/// </summary>
	public int VertexCount
	{
		[CompilerGenerated]
		get
		{
			return fN_0019;
		}
		[CompilerGenerated]
		set
		{
			fN_0019 = value;
		}
	}

	/// <summary>
	/// Size in bytes of the elements in the vertex buffer.
	/// </summary>
	public int VertexStride
	{
		[CompilerGenerated]
		get
		{
			return fNL;
		}
		[CompilerGenerated]
		set
		{
			fNL = value;
		}
	}

	/// <summary>
	/// Object-space bounding area that completely contains the mesh.
	/// </summary>
	public BoundingSphere ObjectSpaceBoundingSphere
	{
		[CompilerGenerated]
		get
		{
			return fNv;
		}
		[CompilerGenerated]
		set
		{
			fNv = value;
		}
	}

	/// <summary>
	/// VertexBuffer that contains the mesh geometry.
	/// </summary>
	public VertexBuffer VertexBuffer
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
	/// IndexBuffer that contains the mesh geometry.
	/// </summary>
	public IndexBuffer IndexBuffer
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
	/// Effect applied to the mesh during rendering.
	/// </summary>
	public Effect Effect
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
	/// Creates a new MeshData instance.
	/// </summary>
	public MeshData()
	{
	}

	/// <summary>
	/// Releases resources allocated by this object.
	/// </summary>
	public void Dispose()
	{
		_0013.B.nu(ref fNB);
		_0013.B.nu(ref fNa);
		_0013.B.nu(ref fNg);
	}
}
