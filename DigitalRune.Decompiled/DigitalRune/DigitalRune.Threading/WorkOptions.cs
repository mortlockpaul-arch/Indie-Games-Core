using System.Runtime.CompilerServices;

namespace DigitalRune.Threading;

/// <summary>
///       Defines how an <see cref="T:DigitalRune.Threading.IWork" /> instance can be executed.
///       </summary>
public struct WorkOptions
{
	/// <summary>
	///       Defines the default options.
	///       </summary>
	public static readonly WorkOptions Default = new WorkOptions
	{
		DetachFromParent = false,
		MaximumThreads = 1,
		QueueFIFO = false
	};

	[CompilerGenerated]
	private bool ccf6f0745bcbb411578db09696e51b4ac;

	[CompilerGenerated]
	private int cfb64d8bb81b6acaa998fd2fab8c387c1;

	[CompilerGenerated]
	private bool ccc1acb1d42724c91625dd2647c1101f8;

	/// <summary>
	///       Gets or sets a value indicating whether the work will be created detached from its 
	///       parent. 
	///       </summary>
	public bool DetachFromParent
	{
		[CompilerGenerated]
		get
		{
			return ccf6f0745bcbb411578db09696e51b4ac;
		}
		[CompilerGenerated]
		set
		{
			ccf6f0745bcbb411578db09696e51b4ac = value;
		}
	}

	/// <summary>
	///       Gets or sets the maximum number of threads which can concurrently execute this work.
	///       </summary>
	public int MaximumThreads
	{
		[CompilerGenerated]
		get
		{
			return cfb64d8bb81b6acaa998fd2fab8c387c1;
		}
		[CompilerGenerated]
		set
		{
			cfb64d8bb81b6acaa998fd2fab8c387c1 = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether this work should be queued in a first-in-first-out 
	///       order.
	///       </summary>
	public bool QueueFIFO
	{
		[CompilerGenerated]
		get
		{
			return ccc1acb1d42724c91625dd2647c1101f8;
		}
		[CompilerGenerated]
		set
		{
			ccc1acb1d42724c91625dd2647c1101f8 = value;
		}
	}
}
