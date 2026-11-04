using System;
using System.Runtime.CompilerServices;

namespace DigitalRune.Geometry.Shapes;

/// <summary>
///       Provides arguments for an <see cref="E:DigitalRune.Geometry.Shapes.Shape.Changed" /> event of a <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
///       </summary>
[Serializable]
public class ShapeChangedEventArgs : EventArgs
{
	private static readonly ResourcePool<ShapeChangedEventArgs> Pool;

	/// <summary>
	///       Represents an event with no event data.
	///       </summary>
	public new static readonly ShapeChangedEventArgs Empty;

	[CompilerGenerated]
	private static Func<ShapeChangedEventArgs> CS_0024_003C_003E9__CachedAnonymousMethodDelegate1;

	/// <summary>
	///       Gets the index of the shape feature that has changed.
	///       </summary>
	public int Feature { get; internal set; }

	private ShapeChangedEventArgs()
	{
	}

	static ShapeChangedEventArgs()
	{
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
			if (1 == 0)
			{
				__ldtoken(ShapeChangedEventArgs._002Ecctor());
			}
			CS_0024_003C_003E9__CachedAnonymousMethodDelegate1 = () => new ShapeChangedEventArgs();
		}
		Pool = new ResourcePool<ShapeChangedEventArgs>(CS_0024_003C_003E9__CachedAnonymousMethodDelegate1, null, null);
		Empty = new ShapeChangedEventArgs();
	}

	/// <summary>
	///       Creates an instance of the <see cref="T:DigitalRune.Geometry.Shapes.ShapeChangedEventArgs" /> class. (This method reuses a 
	///       previously recycled instance or allocates a new instance if necessary.)
	///       </summary>
	/// <param name="feature">The index of the shape feature that has changed.</param>
	/// <returns>
	///       A new or reusable instance of the <see cref="T:DigitalRune.Geometry.Shapes.ShapeChangedEventArgs" /> class.
	///       </returns>
	public static ShapeChangedEventArgs Create()
	{
		ShapeChangedEventArgs e = Pool.Obtain();
		e.Feature = -1;
		return e;
	}

	/// <summary>
	///       Creates an instance of the <see cref="T:DigitalRune.Geometry.Shapes.ShapeChangedEventArgs" /> class with a given feature. 
	///       (This method reuses a previously recycled instance or allocates a new instance if 
	///       necessary.)
	///       </summary>
	/// <param name="feature">The index of the shape feature that has changed.</param>
	/// <returns>
	///       A new or reusable instance of the <see cref="T:DigitalRune.Geometry.Shapes.ShapeChangedEventArgs" /> class.
	///       </returns>
	public static ShapeChangedEventArgs Create(int feature)
	{
		ShapeChangedEventArgs e = Pool.Obtain();
		e.Feature = feature;
		return e;
	}

	public void Recycle()
	{
		Pool.Recycle(this);
	}

	[Obsolete("The static method Recycle() is obsolete. Use the instance method instead.")]
	public static void Recycle(ShapeChangedEventArgs eventArgs)
	{
		Pool.Recycle(eventArgs);
	}

	[CompilerGenerated]
	private static ShapeChangedEventArgs c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new ShapeChangedEventArgs();
	}
}
