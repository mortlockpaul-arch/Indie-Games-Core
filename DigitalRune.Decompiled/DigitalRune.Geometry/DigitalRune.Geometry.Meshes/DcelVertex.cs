using System.Diagnostics;
using System.Runtime.CompilerServices;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Meshes;

/// <summary>
///       Vertex data structure for a Doubly-Connected Edge List (DCEL).
///       </summary>
[DebuggerDisplay("{Position}, Tag {Tag}")]
public class DcelVertex
{
	[CompilerGenerated]
	private Vector3F cfad2b109ba2c6f891737b1ad92b7eb51;

	[CompilerGenerated]
	private DcelEdge cbdd76a56438c807e0988f280255b8ca0;

	[CompilerGenerated]
	private int cc7e93c3723c6ec20026f3db5b43e2cca;

	[CompilerGenerated]
	private int c7f3c238236986c797c2cbe1131f82bf1;

	[CompilerGenerated]
	private object c0df89c0ee57aa7bc1eed1e7eeab72eb6;

	/// <summary>
	///       Gets or sets the coordinates of the vertex.
	///       </summary>
	public Vector3F Position
	{
		[CompilerGenerated]
		get
		{
			return cfad2b109ba2c6f891737b1ad92b7eb51;
		}
		[CompilerGenerated]
		set
		{
			cfad2b109ba2c6f891737b1ad92b7eb51 = value;
		}
	}

	/// <summary>
	///       Gets or sets an outgoing edge.
	///       </summary>
	public DcelEdge Edge
	{
		[CompilerGenerated]
		get
		{
			return cbdd76a56438c807e0988f280255b8ca0;
		}
		[CompilerGenerated]
		set
		{
			cbdd76a56438c807e0988f280255b8ca0 = value;
		}
	}

	/// <summary>
	///       Gets or sets an integer flag that can be used by algorithms that operate on the 
	///       <see cref="T:DigitalRune.Geometry.Meshes.DcelMesh" />.
	///       </summary>
	public int Tag
	{
		[CompilerGenerated]
		get
		{
			return cc7e93c3723c6ec20026f3db5b43e2cca;
		}
		[CompilerGenerated]
		set
		{
			cc7e93c3723c6ec20026f3db5b43e2cca = value;
		}
	}

	internal int InternalTag
	{
		[CompilerGenerated]
		get
		{
			return c7f3c238236986c797c2cbe1131f82bf1;
		}
		[CompilerGenerated]
		set
		{
			c7f3c238236986c797c2cbe1131f82bf1 = value;
		}
	}

	/// <summary>
	///       Gets or sets user-defined data.
	///       </summary>
	public object UserData
	{
		[CompilerGenerated]
		get
		{
			return c0df89c0ee57aa7bc1eed1e7eeab72eb6;
		}
		[CompilerGenerated]
		set
		{
			c0df89c0ee57aa7bc1eed1e7eeab72eb6 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Meshes.DcelVertex" /> class.
	///       </summary>
	public DcelVertex()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Meshes.DcelVertex" /> class with a given position and
	///       edge.
	///       </summary>
	/// <param name="position">The position.</param>
	/// <param name="edge">The edge.</param>
	public DcelVertex(Vector3F position, DcelEdge edge)
	{
		Position = position;
		Edge = edge;
	}
}
