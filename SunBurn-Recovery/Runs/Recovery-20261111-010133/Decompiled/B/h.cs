using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;

namespace B
{
	internal class h<T> : PooledObjectFactory<T> where T : new()
	{
		private List<T> l8a = new List<T>();

		public override T New()
		{
			T val = base.New();
			l8a.Add(val);
			return val;
		}

		public override void Free(T obj)
		{
			l8a.Remove(obj);
			base.Free(obj);
		}

		public void FreeAllTracked()
		{
			foreach (T item in l8a)
			{
				base.Free(item);
			}
			l8a.Clear();
		}

		public override void Clear()
		{
			FreeAllTracked();
			base.Clear();
		}
	}
}
namespace b
{
	internal class h : BaseRenderableEffect
	{
		private Vector3 l8a;

		private Vector3 l8_u0013_;

		private Vector3 l8N;

		private Vector2 l8l;

		private Texture2D l8L;

		private Texture2D l8w;

		private EffectParameter l8J;

		private EffectParameter l8B;

		private EffectParameter l8s;

		private EffectParameter l8h;

		private EffectParameter l8_u0002_;

		private EffectParameter l8v;

		[CompilerGenerated]
		private bool l8Y;

		public Vector3 Color
		{
			get
			{
				return l8N;
			}
			set
			{
				EffectHelper.lw(value, ref l8N, ref l8s);
			}
		}

		public Texture2D BeamTexture
		{
			get
			{
				return l8L;
			}
			set
			{
				EffectHelper.lh(value, ref l8L, ref l8_u0002_);
			}
		}

		public Texture2D SceneDepthMap
		{
			get
			{
				return l8w;
			}
			set
			{
				EffectHelper.lh(value, ref l8w, ref l8v);
				if (l8h != null && l8w != null)
				{
					EffectHelper.ls(new Vector2(l8w.Width, l8w.Height), ref l8l, ref l8h);
				}
				SetTechnique();
			}
		}

		public override bool DoubleSided
		{
			[CompilerGenerated]
			get
			{
				return l8Y;
			}
			[CompilerGenerated]
			set
			{
				l8Y = value;
			}
		}

		public void SetAngleAndRadius(float angle, float radius)
		{
			if (l8J != null)
			{
				angle = MathHelper.ToRadians(MathHelper.Clamp(angle, 0.1f, 90f));
				float num = (float)Math.Sin(angle) * radius;
				Vector3 param_ = new Vector3(num, num, radius);
				EffectHelper.lw(param_, ref l8a, ref l8J);
			}
		}

		public void SetDepthRange(Matrix projectiontoview)
		{
			if (l8B != null)
			{
				Vector3 param_ = default;
				Vector4 vector = Vector4.Transform(new Vector4(0f, 0f, 0f, 1f), projectiontoview);
				Vector4 vector2 = Vector4.Transform(new Vector4(0f, 0f, 1f, 1f), projectiontoview);
				if (vector.W != 0f)
				{
					param_.X = Math.Abs(vector.Z / vector.W);
				}
				if (vector2.W != 0f)
				{
					param_.Y = Math.Abs(vector2.Z / vector2.W);
				}
				float num = param_.Y - param_.X;
				if (num != 0f)
				{
					param_.Z = 10000f / num;
				}
				EffectHelper.lw(param_, ref l8_u0013_, ref l8B);
			}
		}

		public h(GraphicsDevice graphicsdevice)
			: base(graphicsdevice, "VolumeLightEffect")
		{
			l8J = Parameters["_AngleAndRadiusScale"];
			l8B = Parameters["_NearClipFarClipScale"];
			l8s = Parameters["_Color"];
			l8h = Parameters["_TargetWidthHeight"];
			l8_u0002_ = Parameters["_BeamTexture"];
			l8v = Parameters["_SceneDepthTexture"];
			Color = Vector3.One;
			BeamTexture = SunBurnCoreSystem.Instance._u0013_S("VolumeLightBeam");
		}

		protected override void SetTechnique()
		{
			if (l8w != null)
			{
				CurrentTechnique = Techniques["Technique_Deferred"];
			}
			else
			{
				CurrentTechnique = Techniques["Technique_Forward"];
			}
		}

		protected override Effect Create()
		{
			return new h(GraphicsDevice);
		}
	}
}
