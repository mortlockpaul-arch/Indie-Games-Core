using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using y;

namespace SynapseGaming.LightingSystem.Rendering;

public class MeshData : IDisposable
{
	private VertexBuffer l8a;

	private IndexBuffer l8_u0013_;

	private Effect l8N;

	[CompilerGenerated]
	private Matrix l8l;

	[CompilerGenerated]
	private bool l8L;

	[CompilerGenerated]
	private int l8w;

	[CompilerGenerated]
	private int l8J;

	[CompilerGenerated]
	private int l8B;

	[CompilerGenerated]
	private BoundingSphere l8s;

	[CompilerGenerated]
	private BoundingBox l8h;

	public Matrix MeshToObject
	{
		[CompilerGenerated]
		get
		{
			return l8l;
		}
		[CompilerGenerated]
		set
		{
			l8l = value;
		}
	}

	public bool InfiniteBounds
	{
		[CompilerGenerated]
		get
		{
			return l8L;
		}
		[CompilerGenerated]
		set
		{
			l8L = value;
		}
	}

	public int PrimitiveCount
	{
		[CompilerGenerated]
		get
		{
			return l8w;
		}
		[CompilerGenerated]
		set
		{
			l8w = value;
		}
	}

	public int VertexCount
	{
		[CompilerGenerated]
		get
		{
			return l8J;
		}
		[CompilerGenerated]
		set
		{
			l8J = value;
		}
	}

	public int VertexStride
	{
		[CompilerGenerated]
		get
		{
			return l8B;
		}
		[CompilerGenerated]
		set
		{
			l8B = value;
		}
	}

	public BoundingSphere ObjectSpaceBoundingSphere
	{
		[CompilerGenerated]
		get
		{
			return l8s;
		}
		[CompilerGenerated]
		set
		{
			l8s = value;
		}
	}

	public BoundingBox ObjectSpaceBoundingBox
	{
		[CompilerGenerated]
		get
		{
			return l8h;
		}
		[CompilerGenerated]
		set
		{
			l8h = value;
		}
	}

	public VertexBuffer VertexBuffer
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

	public IndexBuffer IndexBuffer
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

	public Effect Effect
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

	public void Dispose()
	{
		y.a.w_u0005_(ref l8a);
		y.a.w_u0005_(ref l8_u0013_);
		y.a.w_u0005_(ref l8N);
	}
}
