using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Serialization;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Lights;

[Serializable]
public class SpotLight : PointLight, ISpotSource, IPointSource, IDirectionalSource, IShadowSource
{
	private float l8a = 45f;

	private float l8_u0013_;

	public Vector3 Direction
	{
		get
		{
			return World.Forward;
		}
		set
		{
			Matrix world = Matrix.Identity;
			if (value != Vector3.Zero)
			{
				world = CoreHelper.CreateMatrixFromNormalizedVectors(Vector3.Forward, Vector3.Normalize(value));
			}
			world.Translation = World.Translation;
			World = world;
		}
	}

	public float Angle
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
			UpdateBounds();
		}
	}

	public float Volume
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
		}
	}

	protected override void UpdateBounds()
	{
		float degrees = MathHelper.Clamp(l8a, 0.001f, 179.99f) * 0.5f;
		degrees = MathHelper.ToRadians(degrees);
		degrees = (float)Math.Tanh(degrees);
		degrees *= Radius;
		BoundingBox boundingbox = new BoundingBox(new Vector3(0f - degrees, 0f - degrees, 0f - Radius), new Vector3(degrees, degrees, 0f));
		WorldBoundingBox = CoreHelper.TransformBoundingBox(boundingbox, World);
		WorldBoundingSphere = BoundingSphere.CreateFromBoundingBox(WorldBoundingBox);
	}

	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		Direction = SerializationHelper.DeserializeField<Vector3>(info, "Direction");
		SerializationHelper.DeserializeField(ref l8a, info, "Angle", usedefault: true);
		SerializationHelper.DeserializeField(ref l8_u0013_, info, "Volume", usedefault: true);
		base.SetObjectData(info, context);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("Direction", Direction);
		info.AddValue("Angle", Angle);
		info.AddValue("Volume", Volume);
		base.GetObjectData(info, context);
	}
}
