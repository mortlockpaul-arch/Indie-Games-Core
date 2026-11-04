using System;
using L;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Collision;

namespace _0003
{
	internal class g
	{
		internal static void _0006(ICollisionObject P_0, ICollisionObject P_1, CollisionPoint P_2)
		{
			ICollisionMove collisionMove = P_0.CollisionMove;
			BoundingSphere worldBoundingSphere = collisionMove.WorldBoundingSphere;
			BoundingBox box = P_1.WorldBoundingBox;
			float num = L._0019.ax(ref box, ref worldBoundingSphere.Center, out var _);
			if (num > worldBoundingSphere.Radius * worldBoundingSphere.Radius)
			{
				return;
			}
			Vector3 center = P_0.WorldBoundingSphere.Center;
			Ray ray = new Ray(center, collisionMove.Normal);
			box.Max.X += worldBoundingSphere.Radius;
			box.Max.Y += worldBoundingSphere.Radius;
			box.Max.Z += worldBoundingSphere.Radius;
			box.Min.X -= worldBoundingSphere.Radius;
			box.Min.Y -= worldBoundingSphere.Radius;
			box.Min.Z -= worldBoundingSphere.Radius;
			ray.Intersects(ref box, out var result);
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
					L._0019.aF(ref box, ref center, out var contactPoint, out var surfaceNormal);
					P_2.ContactTime = MathHelper.Clamp(num2, 0f, 1f);
					P_2.ContactPoint = contactPoint;
					P_2.SurfaceNormal = surfaceNormal;
					P_2.ContactObject = P_1;
					P_2.Material = P_1.DefaultCollisionMaterial;
				}
			}
		}

		internal static void J(ICollisionObject P_0, ICollisionObject P_1, CollisionPoint P_2)
		{
			ICollisionMove collisionMove = P_0.CollisionMove;
			BoundingSphere worldBoundingSphere = collisionMove.WorldBoundingSphere;
			BoundingSphere sphere = P_1.WorldBoundingSphere;
			worldBoundingSphere.Contains(ref sphere, out var result);
			if (result == ContainmentType.Disjoint)
			{
				return;
			}
			Vector3 center = P_0.WorldBoundingSphere.Center;
			Ray ray = new Ray(center, collisionMove.Normal);
			Plane plane = default;
			plane.Normal = -ray.Direction;
			plane.D = 0f - Vector3.Dot(plane.Normal, sphere.Center);
			ray.Intersects(ref plane, out var result2);
			if (!result2.HasValue)
			{
				return;
			}
			float value = result2.Value;
			if (value < 0f || B.A(P_0, P_1, P_2))
			{
				return;
			}
			Vector3 value2 = ray.Position + ray.Direction * value;
			Vector3.DistanceSquared(ref value2, ref sphere.Center, out var result3);
			float num = worldBoundingSphere.Radius + sphere.Radius;
			num *= num;
			float num2 = num - result3;
			if (!(num2 < 0f))
			{
				num2 = (float)Math.Sqrt(num2);
				value -= num2;
				float num3 = value / collisionMove.Distance;
				if (!(num3 >= P_2.ContactTime))
				{
					Vector3 vector = ray.Position + ray.Direction * value;
					Vector3 vector2 = Vector3.Normalize(vector - sphere.Center);
					P_2.ContactTime = MathHelper.Clamp(num3, 0f, 1f);
					P_2.ContactPoint = vector - vector2 * worldBoundingSphere.Radius;
					P_2.SurfaceNormal = vector2;
					P_2.ContactObject = P_1;
					P_2.Material = P_1.DefaultCollisionMaterial;
				}
			}
		}

		internal static void _0004(ICollisionObject P_0, ICollisionObject P_1, CollisionPoint P_2)
		{
			B.fNB.AccumulationValue++;
			CollisionMove collisionMove = P_0.CollisionMove as CollisionMove;
			CollisionMove collisionMove2 = P_1.CollisionMove as CollisionMove;
			if (collisionMove == null || collisionMove2 == null)
			{
				return;
			}
			BoundingSphere worldBoundingSphere = collisionMove.WorldBoundingSphere;
			BoundingBox boundingBox = BoundingBox.CreateMerged(P_0.WorldBoundingBox, BoundingBox.CreateFromSphere(worldBoundingSphere));
			CollisionMesh worldCollisionMesh = collisionMove2.WorldCollisionMesh;
			B.fNL.Clear();
			worldCollisionMesh.fNf.p(ref boundingBox, true, B.fNL);
			if (B.fNL.Count <= 0)
			{
				return;
			}
			float distance = collisionMove.Distance;
			float radius = worldBoundingSphere.Radius;
			float num = 0f - radius;
			float num2 = 0f - (radius + distance);
			Vector3 vector = worldBoundingSphere.Center;
			Vector3 vector2 = vector;
			Vector3 center = P_0.WorldBoundingSphere.Center;
			Ray ray = new Ray(center, collisionMove.Normal);
			bool flag = false;
			foreach (CollisionMesh.CollisionSurface item in B.fNL)
			{
				B.fNa.AccumulationValue++;
				if (flag)
				{
					vector = vector2;
					worldBoundingSphere.Center = vector2;
					flag = false;
				}
				float num3 = vector.X * item.Surface.Normal.X + vector.Y * item.Surface.Normal.Y + vector.Z * item.Surface.Normal.Z + item.Surface.D;
				if (num3 > radius || num3 < num2)
				{
					continue;
				}
				float? result = null;
				if (num3 < num)
				{
					B.fNg.AccumulationValue++;
					float num4 = center.X * item.Surface.Normal.X + center.Y * item.Surface.Normal.Y + center.Z * item.Surface.Normal.Z + item.Surface.D;
					if (num4 < num)
					{
						continue;
					}
					ray.Intersects(ref item.Surface, out result);
					B.fNf.AccumulationValue++;
					if (!result.HasValue)
					{
						continue;
					}
					float value = result.Value;
					if (value < 0f || value > distance)
					{
						continue;
					}
					vector = (worldBoundingSphere.Center = ray.Position + ray.Direction * value);
					B.fN_0003.AccumulationValue++;
					flag = true;
				}
				if (!item.x(worldCollisionMesh, ref vector, ref worldBoundingSphere, num))
				{
					continue;
				}
				Vector3.Dot(ref ray.Direction, ref item.Surface.Normal, out var result2);
				result2 *= -1f;
				if (result2 <= 0f)
				{
					continue;
				}
				if (!flag)
				{
					ray.Intersects(ref item.Surface, out result);
				}
				if (result.HasValue)
				{
					if (B.A(P_0, P_1, P_2))
					{
						break;
					}
					float value2 = result.Value;
					float num5 = value2 - worldBoundingSphere.Radius / result2;
					float num6 = num5 / distance;
					if (!(num6 >= P_2.ContactTime))
					{
						Vector3 vector3 = ray.Position + ray.Direction * num5;
						P_2.ContactTime = MathHelper.Clamp(num6, 0f, 1f);
						P_2.ContactPoint = vector3 - item.Surface.Normal * worldBoundingSphere.Radius;
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
	internal class g : B
	{
	}
}
namespace _0011
{
	internal abstract class g
	{
		public abstract string Parameters { get; set; }

		public g()
		{
		}

		public abstract byte[] DecryptKeyExchange(byte[] rgb);

		public abstract void SetKey(B key);
	}
}
