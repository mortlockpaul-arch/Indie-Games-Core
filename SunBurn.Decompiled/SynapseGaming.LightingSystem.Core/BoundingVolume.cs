using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Class used to provide a bounding box
/// and sphere for an object.
/// </summary>
public class BoundingVolume : IBoundingVolume
{
	[CompilerGenerated]
	private BoundingBox fNB;

	[CompilerGenerated]
	private BoundingSphere fNa;

	/// <summary>
	/// Bounding area that completely contains the associated object.
	/// </summary>
	public BoundingBox BoundingBox
	{
		[CompilerGenerated]
		get
		{
			return fNB;
		}
		[CompilerGenerated]
		set
		{
			fNB = value;
		}
	}

	/// <summary>
	/// Bounding area that completely contains the associated object.
	/// </summary>
	public BoundingSphere BoundingSphere
	{
		[CompilerGenerated]
		get
		{
			return fNa;
		}
		[CompilerGenerated]
		set
		{
			fNa = value;
		}
	}
}
