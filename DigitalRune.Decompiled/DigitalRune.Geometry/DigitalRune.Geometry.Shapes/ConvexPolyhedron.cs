using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Represents a convex polyhedron.
///       </summary>
[Serializable]
public class ConvexPolyhedron : ConvexShape
{
	private const int VertexThreshold = 16;

	private const float RadiusFactor = 2f;

	private const int LookupTableWidth = 4;

	private Aabb _aabbLocal = new Aabb(new Vector3F(float.NaN), new Vector3F(float.NaN));

	private Vector3F _innerPoint = new Vector3F(float.NaN);

	private Vector3F[] _vertices;

	private ReadOnlyCollection<Vector3F> _verticesReadOnly;

	private DirectionalLookupTableF<ushort> _directionLookupTable;

	private VertexAdjacency _vertexAdjacency;

	[CompilerGenerated]
	private static Func<DcelVertex, Vector3F> CS_0024_003C_003E9__CachedAnonymousMethodDelegate1;

	/// <summary>
	///       Gets an inner point.
	///       </summary>
	public override Vector3F InnerPoint => _innerPoint;

	/// <summary>
	///       Gets the a read-only list with the vertices of the convex polyhedron.
	///       </summary>
	[XmlIgnore]
	public ReadOnlyCollection<Vector3F> Vertices
	{
		get
		{
			if (_verticesReadOnly == null)
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
					__ldtoken(ConvexPolyhedron.get_Vertices());
				}
				_verticesReadOnly = new ReadOnlyCollection<Vector3F>(_vertices);
			}
			return _verticesReadOnly;
		}
	}

	internal DirectionalLookupTableF<ushort> DirectionalLookupTable => _directionLookupTable;

	internal VertexAdjacency VertexAdjacency => _vertexAdjacency;

	internal ConvexPolyhedron()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.ConvexHullOfPoints" /> class.
	///       </summary>
	/// <param name="points">
	///       A collection of points which from which a convex polyhedron is built.
	///       </param>
	/// <exception cref="T:System.NotSupportedException">
	///       Too many vertices in convex hull. Max. 65534 vertices in a convex polyhedron are supported.
	///       </exception>
	public ConvexPolyhedron(IEnumerable<Vector3F> points)
	{
		c855e8921726cd29311778472ba8223ae(points);
	}

	internal void c0df7e969ca2a02100b0e8f542e95b0a0(Vector3F[] ce6696c2e906808a833ddbf2dc996e635, Aabb cdc3439410d15f28489c964ee87b88fe9, Vector3F ce46e13dcb6111ee9566eee6448069e79, DirectionalLookupTableF<ushort> c313e74485477de82f358a740efbf0947, VertexAdjacency c292db26ac9a5ad542c10e2de2d6c859f)
	{
		_vertices = ce6696c2e906808a833ddbf2dc996e635;
		_aabbLocal = cdc3439410d15f28489c964ee87b88fe9;
		_innerPoint = ce46e13dcb6111ee9566eee6448069e79;
		_directionLookupTable = c313e74485477de82f358a740efbf0947;
		_vertexAdjacency = c292db26ac9a5ad542c10e2de2d6c859f;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the <see cref="T:DigitalRune.Geometry.Shapes.Shape" />
	///       derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override Shape CreateInstanceCore()
	{
		return new ConvexPolyhedron();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
	///       </summary>
	/// <param name="sourceShape">The object to clone.</param>
	protected override void CloneCore(Shape sourceShape)
	{
		ConvexPolyhedron convexPolyhedron = (ConvexPolyhedron)sourceShape;
		_vertices = convexPolyhedron._vertices;
		_aabbLocal = convexPolyhedron._aabbLocal;
		_innerPoint = convexPolyhedron._innerPoint;
		_directionLookupTable = convexPolyhedron._directionLookupTable;
		_vertexAdjacency = convexPolyhedron._vertexAdjacency;
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
		return _aabbLocal.GetAabb(scale, pose);
	}

	/// <summary>
	///       Gets a support point for a given direction.
	///       </summary>
	/// <param name="direction">
	///       The direction for which to get the support point. The vector does not need to be normalized.
	///       The result is undefined if the vector is a zero vector.
	///       </param>
	/// <returns>
	///       A support point regarding the given direction.
	///       </returns>
	public override Vector3F GetSupportPoint(Vector3F direction)
	{
		return c905cf9f7e74e359a03b97af77119fee0(ref direction);
	}

	/// <summary>
	///       Gets a support point for a given normalized direction vector.
	///       </summary>
	/// <param name="directionNormalized">
	///       The normalized direction vector for which to get the support point. 
	///       </param>
	/// <returns>A support point regarding the given direction.</returns>
	public override Vector3F GetSupportPointNormalized(Vector3F directionNormalized)
	{
		return c905cf9f7e74e359a03b97af77119fee0(ref directionNormalized);
	}

	private Vector3F c905cf9f7e74e359a03b97af77119fee0(ref Vector3F c75c28b357dedcad9a44abd406f2361f3)
	{
		Vector3F c467595ef8f19a612d1393b5a46682eaf = default;
		if (_directionLookupTable != null)
		{
			c2c1b8fa18d2f330e86938c6ea37036c3(ref c75c28b357dedcad9a44abd406f2361f3, out c467595ef8f19a612d1393b5a46682eaf);
		}
		else
		{
			float num = float.NegativeInfinity;
			for (int i = 0; i < _vertices.Length; i++)
			{
				Vector3F vector3F = _vertices[i];
				float num2 = Vector3F.Dot(vector3F, c75c28b357dedcad9a44abd406f2361f3);
				if (num2 > num)
				{
					c467595ef8f19a612d1393b5a46682eaf = vector3F;
					num = num2;
				}
			}
		}
		return c467595ef8f19a612d1393b5a46682eaf;
	}

	/// <summary>
	///       Called when a mesh should be generated for the shape.
	///       </summary>
	/// <param name="absoluteDistanceThreshold">The absolute distance threshold.</param>
	/// <param name="iterationLimit">The iteration limit.</param>
	/// <returns>The triangle mesh for this shape.</returns>
	protected override TriangleMesh OnGetMesh(float absoluteDistanceThreshold, int iterationLimit)
	{
		DcelMesh dcelMesh = GeometryHelper.CreateConvexHull(_vertices);
		return dcelMesh.ToTriangleMesh();
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13983), _vertices.Length);
	}

	private void c855e8921726cd29311778472ba8223ae(IEnumerable<Vector3F> c2ebf3a3e77e8b4507a45357157f26fae)
	{
		DcelMesh dcelMesh = GeometryHelper.CreateConvexHull(c2ebf3a3e77e8b4507a45357157f26fae);
		int num;
		if (dcelMesh == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(ConvexPolyhedron.c855e8921726cd29311778472ba8223ae(IEnumerable<Vector3F>));
			}
			num = 0;
		}
		else
		{
			num = dcelMesh.Vertices.Count;
		}
		int num2 = num;
		if (num2 >= 65535)
		{
			throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7872));
		}
		Vector3F[] vertices;
		if (dcelMesh == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			vertices = new Vector3F[0];
		}
		else
		{
			ReadOnlyCollection<DcelVertex> vertices2 = dcelMesh.Vertices;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1 == null)
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
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1 = (DcelVertex caa9d7ab80719ca5895d6816e6deee0d6) => caa9d7ab80719ca5895d6816e6deee0d6.Position;
			}
			vertices = vertices2.Select(CS_0024_003C_003E9__CachedAnonymousMethodDelegate1).ToArray();
		}
		_vertices = vertices;
		ccd257491ffbff3ce6954ff7c7b9e5d55();
		c92c79ec1e58a7e9fa78969e88642a451();
		if (num2 <= 16)
		{
			return;
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			c8e54abf156a5db24466c2f8a4b7da754();
			c17095f63d94603e52362c40da333d012(dcelMesh);
			return;
		}
	}

	private void ccd257491ffbff3ce6954ff7c7b9e5d55()
	{
		_aabbLocal = base.GetAabb(Vector3F.One, Pose.Identity);
	}

	private void c92c79ec1e58a7e9fa78969e88642a451()
	{
		int num = _vertices.Length;
		Vector3F innerPoint = default;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				Vector3F vector3F = _vertices[i];
				innerPoint += vector3F;
			}
			innerPoint /= (float)num;
		}
		_innerPoint = innerPoint;
	}

	private void c8e54abf156a5db24466c2f8a4b7da754()
	{
		GeometryHelper.ComputeBoundingSphere(_vertices, out var radius, out var _);
		float num = radius * 2f;
		_directionLookupTable = new DirectionalLookupTableF<ushort>(4);
		using IEnumerator<Vector3F> enumerator = _directionLookupTable.GetSampleDirections().GetEnumerator();
		while (enumerator.MoveNext())
		{
			Vector3F current = enumerator.Current;
			current.Normalize();
			Vector3F c2821accfb3598a94e94a1d2928e93b = current * num;
			_directionLookupTable[current] = c2dfcc05ff5a2cdc48f071b5f9fc6b39a(c2821accfb3598a94e94a1d2928e93b);
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(ConvexPolyhedron.c8e54abf156a5db24466c2f8a4b7da754());
			}
			return;
		}
	}

	private ushort c2dfcc05ff5a2cdc48f071b5f9fc6b39a(Vector3F c2821accfb3598a94e94a1d2928e93b85)
	{
		int num = -1;
		float num2 = float.MaxValue;
		for (ushort num3 = 0; num3 < _vertices.Length; num3++)
		{
			float lengthSquared = (c2821accfb3598a94e94a1d2928e93b85 - _vertices[num3]).LengthSquared;
			if (lengthSquared < num2)
			{
				num2 = lengthSquared;
				num = num3;
			}
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
				__ldtoken(ConvexPolyhedron.c2dfcc05ff5a2cdc48f071b5f9fc6b39a(Vector3F));
			}
			return (ushort)num;
		}
	}

	private void c17095f63d94603e52362c40da333d012(DcelMesh c2f54efaf76bdafcae91473da27cd0562)
	{
		_vertexAdjacency = new VertexAdjacency(c2f54efaf76bdafcae91473da27cd0562);
	}

	private void c2c1b8fa18d2f330e86938c6ea37036c3(ref Vector3F c75c28b357dedcad9a44abd406f2361f3, out Vector3F c467595ef8f19a612d1393b5a46682eaf)
	{
		ushort num = _directionLookupTable[c75c28b357dedcad9a44abd406f2361f3];
		Vector3F vector3F = _vertices[num];
		float num2 = Vector3F.Dot(vector3F, c75c28b357dedcad9a44abd406f2361f3);
		ushort num3 = num;
		ushort num4 = ushort.MaxValue;
		do
		{
			num = num3;
			ushort num5 = _vertexAdjacency.ListIndices[num];
			ushort num6 = _vertexAdjacency.ListIndices[num + 1];
			for (ushort num7 = num5; num7 < num6; num7++)
			{
				ushort num8 = _vertexAdjacency.Lists[num7];
				if (num8 != num4)
				{
					Vector3F vector3F2 = _vertices[num8];
					float num9 = vector3F2.X * c75c28b357dedcad9a44abd406f2361f3.X + vector3F2.Y * c75c28b357dedcad9a44abd406f2361f3.Y + vector3F2.Z * c75c28b357dedcad9a44abd406f2361f3.Z;
					if (num9 > num2)
					{
						vector3F = vector3F2;
						num2 = num9;
						num3 = num8;
					}
				}
			}
			num4 = num;
		}
		while (num != num3);
		c467595ef8f19a612d1393b5a46682eaf = vector3F;
	}

	[CompilerGenerated]
	private static Vector3F c806266532cb512eb414327bcce549b48(DcelVertex caa9d7ab80719ca5895d6816e6deee0d6)
	{
		return caa9d7ab80719ca5895d6816e6deee0d6.Position;
	}
}
