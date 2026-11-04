using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DigitalRune.Geometry.Meshes;

/// <summary>
///       Edge data structure for a Doubly-Connected Edge List (DCEL).
///       </summary>
[DebuggerDisplay("Origin {Origin.Position}, Tag {Tag}")]
public class DcelEdge
{
	[CompilerGenerated]
	private DcelVertex cf7a8e83920f65c74ee0fa25eea4d27af;

	[CompilerGenerated]
	private DcelEdge c2f7dd172607abc5d8423a6301f509b4a;

	[CompilerGenerated]
	private DcelFace c8653b27547a0270ed0d9373bc05ec5bd;

	[CompilerGenerated]
	private DcelEdge c859d5b2635af9e17d263fb1472dd91db;

	[CompilerGenerated]
	private DcelEdge c2d3a805e65fe34b5c47691ed3ec214d6;

	[CompilerGenerated]
	private int cc7e93c3723c6ec20026f3db5b43e2cca;

	[CompilerGenerated]
	private int c7f3c238236986c797c2cbe1131f82bf1;

	[CompilerGenerated]
	private object c0df89c0ee57aa7bc1eed1e7eeab72eb6;

	/// <summary>
	///       Gets or sets the vertex where the edge starts.
	///       </summary>
	public DcelVertex Origin
	{
		[CompilerGenerated]
		get
		{
			return cf7a8e83920f65c74ee0fa25eea4d27af;
		}
		[CompilerGenerated]
		set
		{
			cf7a8e83920f65c74ee0fa25eea4d27af = value;
		}
	}

	/// <summary>
	///       Gets or sets the twin edge.
	///       </summary>
	public DcelEdge Twin
	{
		[CompilerGenerated]
		get
		{
			return c2f7dd172607abc5d8423a6301f509b4a;
		}
		[CompilerGenerated]
		set
		{
			c2f7dd172607abc5d8423a6301f509b4a = value;
		}
	}

	/// <summary>
	///       Gets or sets the face for which this edge is part of the boundary.
	///       </summary>
	public DcelFace Face
	{
		[CompilerGenerated]
		get
		{
			return c8653b27547a0270ed0d9373bc05ec5bd;
		}
		[CompilerGenerated]
		set
		{
			c8653b27547a0270ed0d9373bc05ec5bd = value;
		}
	}

	/// <summary>
	///       Gets or sets the next edge (for the same face).
	///       </summary>
	public DcelEdge Next
	{
		[CompilerGenerated]
		get
		{
			return c859d5b2635af9e17d263fb1472dd91db;
		}
		[CompilerGenerated]
		set
		{
			c859d5b2635af9e17d263fb1472dd91db = value;
		}
	}

	/// <summary>
	///       Gets or sets the previous edge (for the same face).
	///       </summary>
	public DcelEdge Previous
	{
		[CompilerGenerated]
		get
		{
			return c2d3a805e65fe34b5c47691ed3ec214d6;
		}
		[CompilerGenerated]
		set
		{
			c2d3a805e65fe34b5c47691ed3ec214d6 = value;
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

	internal float c8c9313fee1ca2682f0170d92769928f1 => (Origin.Position - Twin.Origin.Position).Length;

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Meshes.DcelEdge" /> class</summary>
	public DcelEdge()
	{
	}
}
