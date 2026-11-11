using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Serialization;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Lights;

[Serializable]
public class AmbientLight : BaseLight, IAmbientSource
{
	private float l8a = 0.15f;

	private static BoundingBox l8_u0013_ = new BoundingBox(new Vector3(float.MinValue, float.MinValue, float.MinValue), new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

	private static BoundingSphere l8N = new BoundingSphere(default, float.MaxValue);

	public override LightingType LightingType
	{
		get
		{
			return LightingType.RealTime;
		}
		set
		{
		}
	}

	public override bool FillLight
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public override float FalloffStrength
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	public override bool InfiniteBounds => true;

	public override IShadowSource ShadowSource
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public override Matrix World
	{
		get
		{
			return Matrix.Identity;
		}
		set
		{
		}
	}

	public override int MoveId => 0;

	public float Depth
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = MathHelper.Clamp(value, 0f, 0.5f);
		}
	}

	public AmbientLight()
	{
		WorldBoundingBox = l8_u0013_;
		WorldBoundingSphere = l8N;
	}

	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		Depth = SerializationHelper.DeserializeField<float>(info, "Depth");
		base.SetObjectData(info, context);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("Depth", Depth);
		base.GetObjectData(info, context);
	}
}
