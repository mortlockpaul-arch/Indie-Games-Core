using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Meshes;

/// <summary>
///       Face data structure for a Doubly-Connected Edge List (DCEL).
///       </summary>
[DebuggerDisplay("Tag {Tag}")]
public class DcelFace
{
	[CompilerGenerated]
	private DcelEdge c4ba4dfe46b8e87ae8f972d8922dd016e;

	[CompilerGenerated]
	private List<DcelEdge> c573d7a47e2dabcefe76625ea7017fc22;

	[CompilerGenerated]
	private int cc7e93c3723c6ec20026f3db5b43e2cca;

	[CompilerGenerated]
	private int c7f3c238236986c797c2cbe1131f82bf1;

	[CompilerGenerated]
	private object c0df89c0ee57aa7bc1eed1e7eeab72eb6;

	/// <summary>
	///       Gets or sets an edge of the outer boundary.
	///       </summary>
	public DcelEdge Boundary
	{
		[CompilerGenerated]
		get
		{
			return c4ba4dfe46b8e87ae8f972d8922dd016e;
		}
		[CompilerGenerated]
		set
		{
			c4ba4dfe46b8e87ae8f972d8922dd016e = value;
		}
	}

	/// <summary>
	///       Gets or sets a list with one edge of the boundary for each hole; <see langword="null" /> if
	///       there are no holes.
	///       </summary>
	public List<DcelEdge> Holes
	{
		[CompilerGenerated]
		get
		{
			return c573d7a47e2dabcefe76625ea7017fc22;
		}
		[CompilerGenerated]
		set
		{
			c573d7a47e2dabcefe76625ea7017fc22 = value;
		}
	}

	internal Vector3F c92b2ed2c76c9d5b2e6f8466991b83c86
	{
		get
		{
			DcelEdge boundary = Boundary;
			Vector3F position = boundary.Origin.Position;
			boundary = boundary.Next;
			Vector3F position2 = boundary.Origin.Position;
			boundary = boundary.Next;
			Vector3F position3 = boundary.Origin.Position;
			return Vector3F.Cross(position2 - position, position3 - position);
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
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Meshes.DcelFace" /> class.
	///       </summary>
	public DcelFace()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Meshes.DcelFace" /> class from a given edge.
	///       </summary>
	/// <param name="boundary">An edge of the outer boundary.</param>
	public DcelFace(DcelEdge boundary)
	{
		Boundary = boundary;
	}
}
