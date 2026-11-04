using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using L;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Collision;

namespace _0003
{
	internal class a
	{
		internal static void _0(ICollisionObject P_0, ICollisionObject P_1, CollisionPoint P_2)
		{
			ICollisionMove collisionMove = P_0.CollisionMove;
			BoundingBox worldBoundingBox = collisionMove.WorldBoundingBox;
			BoundingSphere worldBoundingSphere = P_1.WorldBoundingSphere;
			float num = L._0019.ax(ref worldBoundingBox, ref worldBoundingSphere.Center, out var _);
			if (num > worldBoundingSphere.Radius * worldBoundingSphere.Radius)
			{
				return;
			}
			Ray ray = new Ray(worldBoundingSphere.Center, -collisionMove.Normal);
			worldBoundingBox = P_0.WorldBoundingBox;
			worldBoundingBox.Max.X += worldBoundingSphere.Radius;
			worldBoundingBox.Max.Y += worldBoundingSphere.Radius;
			worldBoundingBox.Max.Z += worldBoundingSphere.Radius;
			worldBoundingBox.Min.X -= worldBoundingSphere.Radius;
			worldBoundingBox.Min.Y -= worldBoundingSphere.Radius;
			worldBoundingBox.Min.Z -= worldBoundingSphere.Radius;
			ray.Intersects(ref worldBoundingBox, out var result);
			if (!result.HasValue)
			{
				return;
			}
			float value = result.Value;
			if (!(value < 0f) && !B.A(P_0, P_1, P_2))
			{
				float num2 = value / collisionMove.Distance;
				if (!(num2 >= P_2.ContactTime))
				{
					BoundingBox worldBoundingBox2 = P_0.WorldBoundingBox;
					Vector3 vector2 = (worldBoundingBox2.Max + worldBoundingBox2.Min) * 0.5f;
					Vector3 vector3 = vector2 + collisionMove.Normal * (collisionMove.Distance - value);
					Vector3 surfaceNormal = Vector3.Normalize(vector3 - worldBoundingSphere.Center);
					P_2.ContactTime = MathHelper.Clamp(num2, 0f, 1f);
					P_2.ContactPoint = vector3;
					P_2.SurfaceNormal = surfaceNormal;
					P_2.ContactObject = P_1;
					P_2.Material = P_1.DefaultCollisionMaterial;
				}
			}
		}

		internal static void K(ICollisionObject P_0, ICollisionObject P_1, CollisionPoint P_2)
		{
			ICollisionMove collisionMove = P_0.CollisionMove;
			BoundingBox worldBoundingBox = collisionMove.WorldBoundingBox;
			BoundingBox box = P_1.WorldBoundingBox;
			worldBoundingBox.Contains(ref box, out var result);
			if (result == ContainmentType.Disjoint)
			{
				return;
			}
			BoundingBox worldBoundingBox2 = P_0.WorldBoundingBox;
			Vector3 vector = (worldBoundingBox2.Max - worldBoundingBox2.Min) * 0.5f;
			box.Max.X += vector.X;
			box.Max.Y += vector.Y;
			box.Max.Z += vector.Z;
			box.Min.X -= vector.X;
			box.Min.Y -= vector.Y;
			box.Min.Z -= vector.Z;
			Vector3 position = (worldBoundingBox2.Max + worldBoundingBox2.Min) * 0.5f;
			new Ray(position, collisionMove.Normal).Intersects(ref box, out var result2);
			if (!result2.HasValue)
			{
				return;
			}
			float value = result2.Value;
			if (!(value < 0f) && !B.A(P_0, P_1, P_2))
			{
				float num = value / collisionMove.Distance;
				if (!(num >= P_2.ContactTime))
				{
					L._0019.aF(ref box, ref position, out var contactPoint, out var surfaceNormal);
					P_2.ContactTime = MathHelper.Clamp(num, 0f, 1f);
					P_2.ContactPoint = contactPoint;
					P_2.SurfaceNormal = surfaceNormal;
					P_2.ContactObject = P_1;
					P_2.Material = P_1.DefaultCollisionMaterial;
				}
			}
		}

		internal static void M(ICollisionObject P_0, ICollisionObject P_1, CollisionPoint P_2)
		{
			B.fNB.AccumulationValue++;
			CollisionMove collisionMove = P_0.CollisionMove as CollisionMove;
			CollisionMove collisionMove2 = P_1.CollisionMove as CollisionMove;
			if (collisionMove == null || collisionMove2 == null)
			{
				return;
			}
			BoundingBox worldSweepBoundingBox = collisionMove.WorldSweepBoundingBox;
			CollisionMesh worldCollisionMesh = collisionMove2.WorldCollisionMesh;
			B.fNL.Clear();
			worldCollisionMesh.fNf.p(ref worldSweepBoundingBox, true, B.fNL);
			if (B.fNL.Count <= 0)
			{
				return;
			}
			BoundingBox worldBoundingBox = collisionMove.WorldBoundingBox;
			BoundingBox worldBoundingBox2 = P_0.WorldBoundingBox;
			BoundingSphere worldBoundingSphere = collisionMove.WorldBoundingSphere;
			float distance = collisionMove.Distance;
			float radius = worldBoundingSphere.Radius;
			float num = 0f - (radius + distance);
			Vector3 vector = (worldBoundingBox2.Max + worldBoundingBox2.Min) * 0.5f;
			Vector3 center = worldBoundingSphere.Center;
			Ray ray = new Ray
			{
				Direction = collisionMove.Normal
			};
			foreach (CollisionMesh.CollisionSurface item in B.fNL)
			{
				B.fNa.AccumulationValue++;
				float num2 = center.X * item.Surface.Normal.X + center.Y * item.Surface.Normal.Y + center.Z * item.Surface.Normal.Z + item.Surface.D;
				if (num2 > radius || num2 < num)
				{
					continue;
				}
				worldBoundingBox.Intersects(ref item.Surface, out var result);
				if (result == PlaneIntersectionType.Front)
				{
					continue;
				}
				Vector3.Dot(ref ray.Direction, ref item.Surface.Normal, out var result2);
				result2 *= -1f;
				if (result2 <= 0f || !item.x(worldCollisionMesh, ref worldBoundingBox))
				{
					continue;
				}
				Vector3 vector2 = vector - item.Surface.Normal * 100000f;
				L._0019.ax(ref worldBoundingBox2, ref vector2, out ray.Position);
				float? result3 = null;
				ray.Intersects(ref item.Surface, out result3);
				if (result3.HasValue)
				{
					if (B.A(P_0, P_1, P_2))
					{
						break;
					}
					float value = result3.Value;
					float num3 = value / distance;
					if (!(num3 >= P_2.ContactTime))
					{
						P_2.ContactTime = MathHelper.Clamp(num3, 0f, 1f);
						P_2.ContactPoint = ray.Position + ray.Direction * value;
						P_2.SurfaceNormal = item.Surface.Normal;
						P_2.ContactObject = P_1;
						P_2.Material = item.Material;
					}
				}
			}
		}
	}
}
namespace _0019
{
	internal class a : List<B>
	{
	}
}
namespace _0015
{
	internal class a
	{
		public const float LightIconSize = 0.01f;

		private float fNB = 10f;

		private float fNa = 10f;

		private bool fNg;

		private float fNf = 1f;

		public float IconScale
		{
			get
			{
				return fNf;
			}
			set
			{
				fNf = value;
			}
		}

		public float MoveScale
		{
			get
			{
				return fNB;
			}
			set
			{
				fNB = value;
			}
		}

		public float RotationScale
		{
			get
			{
				return fNa;
			}
			set
			{
				fNa = value;
			}
		}

		public bool UserHandledView
		{
			get
			{
				return fNg;
			}
			set
			{
				fNg = value;
			}
		}
	}
}
namespace _0011
{
	internal abstract class a : B
	{
		public new static a Create()
		{
			return new _8();
		}

		public a()
		{
		}

		public abstract byte[] EncryptValue(byte[] rgb);

		public abstract byte[] DecryptValue(byte[] rgb);

		public abstract _0005 ExportParameters(bool include);

		public abstract void ImportParameters(_0005 parameters);

		internal void _0003_0015(_0005 P_0)
		{
			if (P_0.P != null)
			{
				Array.Clear(P_0.P, 0, P_0.P.Length);
			}
			if (P_0.Q != null)
			{
				Array.Clear(P_0.Q, 0, P_0.Q.Length);
			}
			if (P_0.DP != null)
			{
				Array.Clear(P_0.DP, 0, P_0.DP.Length);
			}
			if (P_0.DQ != null)
			{
				Array.Clear(P_0.DQ, 0, P_0.DQ.Length);
			}
			if (P_0.InverseQ != null)
			{
				Array.Clear(P_0.InverseQ, 0, P_0.InverseQ.Length);
			}
			if (P_0.D != null)
			{
				Array.Clear(P_0.D, 0, P_0.D.Length);
			}
		}
	}
}
namespace _0017
{
	[Serializable]
	internal sealed class a : IComparer
	{
		public static readonly a Default = new a();

		internal static readonly a fNB = new a(CultureInfo.InvariantCulture);

		private CompareInfo fNa;

		private a()
		{
		}

		internal a(CultureInfo P_0)
		{
			if (P_0 == null)
			{
				throw new ArgumentNullException("culture");
			}
			fNa = P_0.CompareInfo;
		}

		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (fNa != null)
			{
				string text = a as string;
				string text2 = b as string;
				if (text != null && text2 != null)
				{
					return fNa.Compare(text, text2);
				}
			}
			if (a is IComparable)
			{
				return (a as IComparable).CompareTo(b);
			}
			if (b is IComparable)
			{
				return -(b as IComparable).CompareTo(a);
			}
			throw new ArgumentException("Neither 'a' nor 'b' implements IComparable.");
		}
	}
}
namespace _0013
{
	internal class a
	{
		private int fNB;

		internal a()
		{
			fNB = Thread.CurrentThread.ManagedThreadId;
		}

		internal void n1()
		{
			if (fNB != Thread.CurrentThread.ManagedThreadId)
			{
				throw new Exception("Calls to object cannot be made from threads other than the one that created it.");
			}
		}
	}
}
