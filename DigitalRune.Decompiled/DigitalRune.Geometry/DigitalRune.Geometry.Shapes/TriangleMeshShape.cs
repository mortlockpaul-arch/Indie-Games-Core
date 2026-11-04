using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Geometry.Partitioning;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a shape defined by an <see cref="T:DigitalRune.Geometry.Meshes.ITriangleMesh" />.
///       </summary>
[Serializable]
public class TriangleMeshShape : Shape
{
	private struct c6d68cf9e6b62039f1d723919c1b51821
	{
		public int c454a53f7742ebf6150769a948c7d7107;

		public int c35869433cd0b866e767a4503e251e7f5;
	}

	internal Aabb _aabbLocal = new Aabb(new Vector3F(float.NaN), new Vector3F(float.NaN));

	private ITriangleMesh _mesh;

	private ISpatialPartition<int> _partition;

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint
	{
		get
		{
			if (_mesh.NumberOfTriangles == 0)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(TriangleMeshShape.get_InnerPoint());
						}
						return default;
					}
				}
			}
			return _mesh.GetTriangle(_mesh.NumberOfTriangles / 2).Vertex1;
		}
	}

	/// <summary>
	///       Gets or sets the triangle mesh.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public ITriangleMesh Mesh
	{
		get
		{
			return _mesh;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (_mesh == value)
			{
				return;
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(TriangleMeshShape.set_Mesh(ITriangleMesh));
				}
				_mesh = value;
				Invalidate();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or set the spatial partition used to improve the performance of geometric queries.
	///       </summary>
	public ISpatialPartition<int> Partition
	{
		get
		{
			return _partition;
		}
		set
		{
			Func<int, Aabb> func = null;
			if (_partition == value)
			{
				return;
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(TriangleMeshShape.set_Partition(ISpatialPartition<int>));
				}
				if (_partition != null)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
					_partition.GetAabbForItem = null;
					_partition.Clear();
				}
				_partition = value;
				if (_partition == null)
				{
					return;
				}
				ISpatialPartition<int> partition = _partition;
				if (func == null)
				{
					while (true)
					{
						switch (3)
						{
						case 0:
							continue;
						}
						break;
					}
					func = (int cdeb496a0c3d046b6bc838defe2d220d0) => Mesh.GetTriangle(cdeb496a0c3d046b6bc838defe2d220d0).Aabb;
				}
				partition.GetAabbForItem = func;
				_partition.Clear();
				int numberOfTriangles = Mesh.NumberOfTriangles;
				for (int num = 0; num < numberOfTriangles; num++)
				{
					_partition.Add(num);
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						return;
					}
				}
			}
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether contact welding is enabled.
	///       </summary>
	public bool EnableContactWelding
	{
		get
		{
			return TriangleNeighbors != null;
		}
		set
		{
			if (value == EnableContactWelding)
			{
				return;
			}
			if (!value)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(TriangleMeshShape.set_EnableContactWelding(bool));
						}
						TriangleNeighbors = null;
						return;
					}
				}
			}
			TriangleNeighbors = new List<int>(Mesh.NumberOfTriangles * 3);
			c29d76136b62ebab16eb1079663858187();
		}
	}

	internal List<int> TriangleNeighbors { get; set; }

	/// <summary>
	///       Gets or sets a value indicating whether the mesh is two-sided.
	///       </summary>
	public bool IsTwoSided { get; set; }

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.TriangleMeshShape" /> class.
	///       </summary>
	public TriangleMeshShape()
		: this(new TriangleMesh(), enableContactWelding: false, null)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.TriangleMeshShape" /> class from the given 
	///       triangle mesh.
	///       </summary>
	/// <param name="mesh">The mesh.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="mesh" /> is <see langword="null" />.
	///       </exception>
	public TriangleMeshShape(ITriangleMesh mesh)
		: this(mesh, enableContactWelding: false, null)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.TriangleMeshShape" /> class from the given
	///       triangle mesh.
	///       </summary>
	/// <param name="mesh">The mesh.</param>
	/// <param name="enableContactWelding"> 
	///       If set to <see langword="true" /> contact welding is enabled; otherwise, the shape will not
	///       use contact welding. See <see cref="P:DigitalRune.Geometry.Shapes.TriangleMeshShape.EnableContactWelding" /> for more information.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="mesh" /> is <see langword="null" />.
	///       </exception>
	public TriangleMeshShape(ITriangleMesh mesh, bool enableContactWelding)
	{
		if (mesh == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(TriangleMeshShape._002Ector(ITriangleMesh, bool));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3787));
				}
			}
		}
		_mesh = mesh;
		EnableContactWelding = enableContactWelding;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.TriangleMeshShape" /> class from the given
	///       triangle mesh.
	///       </summary>
	/// <param name="mesh">The mesh.</param>
	/// <param name="enableContactWelding"> 
	///       If set to <see langword="true" /> contact welding is enabled; otherwise, the shape will not
	///       use contact welding. See <see cref="P:DigitalRune.Geometry.Shapes.TriangleMeshShape.EnableContactWelding" /> for more information.
	///       </param>
	/// <param name="partition">
	///       The spatial partition (see <see cref="P:DigitalRune.Geometry.Shapes.TriangleMeshShape.Partition" />). Can be <see langword="null" /> if no 
	///       partition should be used.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="mesh" /> is <see langword="null" />.
	///       </exception>
	public TriangleMeshShape(ITriangleMesh mesh, bool enableContactWelding, ISpatialPartition<int> partition)
	{
		if (mesh == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(TriangleMeshShape._002Ector(ITriangleMesh, bool, ISpatialPartition<int>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3787));
				}
			}
		}
		_mesh = mesh;
		Partition = partition;
		EnableContactWelding = enableContactWelding;
	}

	internal void cf291ec93e82da38f9e1814873cf030de(ITriangleMesh cf1674ba51e57b7e621e83565f0591384)
	{
		_mesh = cf1674ba51e57b7e621e83565f0591384;
	}

	internal void c56d88ced32e38076203610b9aa5a84d4(ISpatialPartition<int> c828d0c1f4103275cf37fe54b9af3f709)
	{
		if (c828d0c1f4103275cf37fe54b9af3f709 == null)
		{
			return;
		}
		_partition = c828d0c1f4103275cf37fe54b9af3f709;
		_partition.GetAabbForItem = (int cdeb496a0c3d046b6bc838defe2d220d0) => Mesh.GetTriangle(cdeb496a0c3d046b6bc838defe2d220d0).Aabb;
		int numberOfTriangles = _mesh.NumberOfTriangles;
		if (_partition.Count == numberOfTriangles)
		{
			return;
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(TriangleMeshShape.c56d88ced32e38076203610b9aa5a84d4(ISpatialPartition<int>));
			}
			_partition.Clear();
			for (int num = 0; num < numberOfTriangles; num++)
			{
				_partition.Add(num);
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				_partition.Update(forceRebuild: false);
				return;
			}
		}
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new TriangleMeshShape();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		TriangleMeshShape triangleMeshShape = (TriangleMeshShape)sourceShape;
		_mesh = cf22904f08230fb947bb753964c9d5021(triangleMeshShape.Mesh);
		if (triangleMeshShape.Partition != null)
		{
			Partition = (ISpatialPartition<int>)triangleMeshShape.Partition.Clone();
		}
		if (triangleMeshShape.TriangleNeighbors != null)
		{
			TriangleNeighbors = new List<int>(triangleMeshShape.TriangleNeighbors);
		}
	}

	private static ITriangleMesh cf22904f08230fb947bb753964c9d5021(ITriangleMesh cf1674ba51e57b7e621e83565f0591384)
	{
		if (cf1674ba51e57b7e621e83565f0591384 is ICloneable)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(TriangleMeshShape.cf22904f08230fb947bb753964c9d5021(ITriangleMesh));
					}
					return (ITriangleMesh)((ICloneable)cf1674ba51e57b7e621e83565f0591384).Clone();
				}
			}
		}
		return cf1674ba51e57b7e621e83565f0591384;
	}

	/// <summary>
	///       Computes the axis-aligned bounding box (AABB) for this shape positioned in world space using
	///       the given scale and <see cref="T:DigitalRune.Geometry.Pose" />.
	///       </summary>
	/// <param name="scale">
	///       The scale factor by which the shape should be scaled. The scaling is applied in the
	///       shape's local space before the pose is applied.
	///       </param>
	/// <param name="pose">
	///       The <see cref="T:DigitalRune.Geometry.Pose" /> of the shape. This pose defines how the shape should be positioned
	///       in world space.
	///       </param>
	/// <returns>
	///       The AABB of the shape positioned in world space.
	///       </returns>
	public override Aabb GetAabb(Vector3F scale, Pose pose)
	{
		if (float.IsNaN(_aabbLocal.Minimum.X))
		{
			if (Partition == null)
			{
				_aabbLocal = default;
				if (_mesh != null && _mesh.NumberOfTriangles > 0)
				{
					bool flag = true;
					for (int i = 0; i < _mesh.NumberOfTriangles; i++)
					{
						Triangle triangle = _mesh.GetTriangle(i);
						for (int j = 0; j < 3; j++)
						{
							Vector3F vector3F = triangle[j];
							if (flag)
							{
								flag = false;
								_aabbLocal = new Aabb(vector3F, vector3F);
							}
							else
							{
								_aabbLocal.Grow(vector3F);
							}
						}
					}
				}
			}
			else
			{
				_aabbLocal = Partition.Aabb;
			}
		}
		return _aabbLocal.GetAabb(scale, pose);
	}

	/// <summary>
	///       Invalidates the triangle mesh.
	///       </summary>
	public void Invalidate()
	{
		Invalidate(-1, invalidateTopology: true);
	}

	/// <summary>
	///       Invalidates the the triangle mesh.
	///       </summary>
	/// <param name="invalidateTopology">
	///       if set to <see langword="true" /> the mesh topology is invalidated.
	///       </param>
	public void Invalidate(bool invalidateTopology)
	{
		Invalidate(-1, invalidateTopology);
	}

	/// <summary>
	///       Invalidates the whole triangle mesh or a single triangle.
	///       </summary>
	/// <param name="triangleIndex">
	///       Index of the triangle. Can be -1 to invalidate the whole mesh.
	///       </param>
	/// <param name="invalidateTopology">
	///       If set to <see langword="true" /> the mesh topology is invalidated.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="triangleIndex" /> is out of range.
	///       </exception>
	public void Invalidate(int triangleIndex, bool invalidateTopology)
	{
		int numberOfTriangles = Mesh.NumberOfTriangles;
		if (triangleIndex >= numberOfTriangles)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12779));
		}
		_aabbLocal = new Aabb(new Vector3F(float.NaN), new Vector3F(float.NaN));
		if (_partition != null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(TriangleMeshShape.Invalidate(int, bool));
			}
			if (numberOfTriangles != _partition.Count)
			{
				_partition.Clear();
				for (int i = 0; i < numberOfTriangles; i++)
				{
					_partition.Add(i);
				}
			}
			else if (triangleIndex >= 0)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				_partition.Invalidate(triangleIndex);
			}
			else
			{
				_partition.Invalidate();
			}
		}
		if (invalidateTopology)
		{
			c29d76136b62ebab16eb1079663858187();
		}
		if (triangleIndex < 0)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					OnChanged(ShapeChangedEventArgs.Empty);
					return;
				}
			}
		}
		ShapeChangedEventArgs e = ShapeChangedEventArgs.Create(triangleIndex);
		OnChanged(e);
		e.Recycle();
	}

	/// <summary>
	///       Computes the enclosed volume of the mesh.
	///       </summary>
	/// <returns>The enclosed volume of the mesh.</returns>
	public float GetVolume()
	{
		return Mesh.GetVolume();
	}

	/// <summary>
	///       Computes the enclosed volume of the mesh.
	///       </summary>
	/// <param name="relativeError">Not used.</param>
	/// <param name="iterationLimit">Not used.</param>
	/// <returns>The enclosed volume of the mesh.</returns>
	public override float GetVolume(float relativeError, int iterationLimit)
	{
		return Mesh.GetVolume();
	}

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected override TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit)
	{
		if (Mesh is TriangleMesh triangleMesh)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(TriangleMeshShape.OnGetMesh(float, int));
				}
				return triangleMesh.Clone();
			}
		}
		TriangleMesh triangleMesh2 = new TriangleMesh();
		int numberOfTriangles = _mesh.NumberOfTriangles;
		for (int i = 0; i < numberOfTriangles; i++)
		{
			Triangle triangle = _mesh.GetTriangle(i);
			triangleMesh2.Add(triangle, weldVerticesBruteForce: false);
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			triangleMesh2.WeldVertices();
			return triangleMesh2;
		}
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12806), _mesh.NumberOfTriangles);
	}

	private void c29d76136b62ebab16eb1079663858187()
	{
		if (TriangleNeighbors == null)
		{
			return;
		}
		int numberOfTriangles = Mesh.NumberOfTriangles;
		TriangleNeighbors.Clear();
		TriangleNeighbors.Capacity = numberOfTriangles * 3;
		for (int i = 0; i < numberOfTriangles * 3; i++)
		{
			TriangleNeighbors.Add(-1);
		}
		if (Mesh is TriangleMesh triangleMesh)
		{
			Dictionary<Pair<int, int>, c6d68cf9e6b62039f1d723919c1b51821> dictionary = new Dictionary<Pair<int, int>, c6d68cf9e6b62039f1d723919c1b51821>(3 * numberOfTriangles);
			for (int j = 0; j < numberOfTriangles; j++)
			{
				int k = 0;
				int num = 2;
				for (; k < 3; k++)
				{
					int num2 = (k + 1) % 3;
					int obj = triangleMesh.Indices[j * 3 + k];
					int obj2 = triangleMesh.Indices[j * 3 + num];
					if (obj > obj2)
					{
						while (true)
						{
							switch (6)
							{
							case 0:
								continue;
							}
							break;
						}
						if (1 == 0)
						{
							__ldtoken(TriangleMeshShape.c29d76136b62ebab16eb1079663858187());
						}
						MathHelper.Swap(ref obj, ref obj2);
					}
					if (dictionary.TryGetValue(new Pair<int, int>(obj, obj2), out var value))
					{
						TriangleNeighbors[j * 3 + num2] = value.c454a53f7742ebf6150769a948c7d7107;
						TriangleNeighbors[value.c454a53f7742ebf6150769a948c7d7107 * 3 + value.c35869433cd0b866e767a4503e251e7f5] = j;
					}
					else
					{
						value = new c6d68cf9e6b62039f1d723919c1b51821
						{
							c454a53f7742ebf6150769a948c7d7107 = j,
							c35869433cd0b866e767a4503e251e7f5 = num2
						};
						dictionary.Add(new Pair<int, int>(obj, obj2), value);
					}
					num = k;
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						goto end_IL_015d;
					}
					continue;
					end_IL_015d:
					break;
				}
			}
			return;
		}
		Dictionary<Pair<Vector3F>, c6d68cf9e6b62039f1d723919c1b51821> dictionary2 = new Dictionary<Pair<Vector3F>, c6d68cf9e6b62039f1d723919c1b51821>(3 * numberOfTriangles);
		for (int l = 0; l < numberOfTriangles; l++)
		{
			Triangle triangle = Mesh.GetTriangle(l);
			int m = 0;
			int index = 2;
			for (; m < 3; m++)
			{
				int num3 = (m + 1) % 3;
				Vector3F first = triangle[m];
				Vector3F second = triangle[index];
				if (dictionary2.TryGetValue(new Pair<Vector3F>(first, second), out var value2))
				{
					while (true)
					{
						switch (1)
						{
						case 0:
							continue;
						}
						break;
					}
					TriangleNeighbors[l * 3 + num3] = value2.c454a53f7742ebf6150769a948c7d7107;
					TriangleNeighbors[value2.c454a53f7742ebf6150769a948c7d7107 * 3 + value2.c35869433cd0b866e767a4503e251e7f5] = l;
				}
				else
				{
					value2 = new c6d68cf9e6b62039f1d723919c1b51821
					{
						c454a53f7742ebf6150769a948c7d7107 = l,
						c35869433cd0b866e767a4503e251e7f5 = num3
					};
					dictionary2.Add(new Pair<Vector3F>(first, second), value2);
				}
				index = m;
			}
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				break;
			default:
				return;
			}
		}
	}

	[CompilerGenerated]
	private Aabb cc8115fa4d90a38024a9d3283f6d5d47c(int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		return Mesh.GetTriangle(cdeb496a0c3d046b6bc838defe2d220d0).Aabb;
	}

	[CompilerGenerated]
	private Aabb c864907fd83dd9abccc91af0a00fc4160(int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		return Mesh.GetTriangle(cdeb496a0c3d046b6bc838defe2d220d0).Aabb;
	}
}
