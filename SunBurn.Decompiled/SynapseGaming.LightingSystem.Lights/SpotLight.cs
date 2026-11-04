using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using L;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Serialization;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Lights;

/// <summary>
/// Provides spotlight information for rendering lighting and shadows.
/// </summary>
[Serializable]
public class SpotLight : PointLight, ISpotSource, IPointSource, IDirectionalSource, IShadowSource
{
	private float fNB = 45f;

	private float fNa;

	/// <summary>
	/// Direction in world space of the light's influence.
	/// </summary>
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
				world = L._0019.aO(Vector3.Forward, Vector3.Normalize(value));
			}
			world.Translation = World.Translation;
			World = world;
		}
	}

	/// <summary>
	/// Angle in degrees of the light's influence.
	/// </summary>
	public float Angle
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
			UpdateBounds();
		}
	}

	/// <summary>
	/// Intensity of the light's 3D light beam.
	/// </summary>
	public float Volume
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

	/// <summary>
	/// Creates a new SpotLight instance.
	/// </summary>
	public SpotLight()
	{
	}

	/// <summary />
	protected override void UpdateBounds()
	{
		float degrees = MathHelper.Clamp(fNB, 0.001f, 179.99f) * 0.5f;
		degrees = MathHelper.ToRadians(degrees);
		degrees = (float)Math.Tanh(degrees);
		degrees *= Radius;
		BoundingBox boundingBox = new BoundingBox(new Vector3(0f - degrees, 0f - degrees, 0f - Radius), new Vector3(degrees, degrees, 0f));
		WorldBoundingBox = L._0019.ah(boundingBox, World);
		WorldBoundingSphere = BoundingSphere.CreateFromBoundingBox(WorldBoundingBox);
	}

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		Direction = SerializationHelper.DeserializeField<Vector3>(info, "Direction");
		SerializationHelper.DeserializeField(ref fNB, info, "Angle", usedefault: true);
		SerializationHelper.DeserializeField(ref fNa, info, "Volume", usedefault: true);
		base.SetObjectData(info, context);
	}

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("Direction", Direction);
		info.AddValue("Angle", Angle);
		info.AddValue("Volume", Volume);
		base.GetObjectData(info, context);
	}
}
