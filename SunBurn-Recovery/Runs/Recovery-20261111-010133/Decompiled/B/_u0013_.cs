using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace B
{
	internal class _u0013_<T> : global::B.a<T> where T : IWorldBoundingBoxObject
	{
		internal Dictionary<T, global::B.a<T>> l8a = new Dictionary<T, global::B.a<T>>(128);

		internal _u0013_(BoundingBox param_0, int param_1)
		{
			Init(ref param_0, param_1, this);
		}

		public _u0013_()
		{
		}

		internal void a(ref BoundingBox param_0, int param_1)
		{
			Init(ref param_0, param_1, this);
		}

		internal override void _id_3()
		{
			l8a.Clear();
			base._id_3();
		}
	}
}
namespace b
{
	internal interface _u0013_
	{
		Texture2D SceneDepthMap { get; set; }

		Texture2D SceneNormalSpecularMap { get; set; }
	}
}
