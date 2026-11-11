using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

public class BoundingVolume : IBoundingVolume
{
	[CompilerGenerated]
	private BoundingBox l8a;

	[CompilerGenerated]
	private BoundingSphere l8_u0013_;

	public BoundingBox BoundingBox
	{
		[CompilerGenerated]
		get
		{
			return l8a;
		}
		[CompilerGenerated]
		set
		{
			l8a = value;
		}
	}

	public BoundingSphere BoundingSphere
	{
		[CompilerGenerated]
		get
		{
			return l8_u0013_;
		}
		[CompilerGenerated]
		set
		{
			l8_u0013_ = value;
		}
	}
}
