using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Serialization;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Lights;

[Serializable]
public class DirectionalLight : BaseLight, IDirectionalSource, IShadowSource
{
	private LightingType l8a = LightingType.RealTime;

	private ShadowType l8_u0013_ = ShadowType.AllObjects;

	private float l8N = 1f;

	private float l8l = 1f;

	private float l8L = 0.2f;

	private bool l8w = true;

	private Matrix l8J = CoreHelper.CreateMatrixFromNormalizedVectors(Vector3.Forward, Vector3.Down);

	private static BoundingBox l8B = new BoundingBox(new Vector3(float.MinValue, float.MinValue, float.MinValue), new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

	private static BoundingSphere l8s = new BoundingSphere(default, float.MaxValue);

	public override LightingType LightingType
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
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
			return this;
		}
		set
		{
		}
	}

	public ShadowType ShadowType
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

	public Vector3 ShadowPosition => Direction * -1000000f;

	public float ShadowQuality
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = MathHelper.Clamp(value, 0f, 2f);
		}
	}

	public float ShadowPrimaryBias
	{
		get
		{
			return l8l;
		}
		set
		{
			l8l = value;
		}
	}

	public float ShadowSecondaryBias
	{
		get
		{
			return l8L;
		}
		set
		{
			l8L = value;
		}
	}

	public bool ShadowPerSurfaceLOD
	{
		get
		{
			return l8w;
		}
		set
		{
			l8w = value;
		}
	}

	public bool ShadowRenderLightsTogether => false;

	public Vector3 Direction
	{
		get
		{
			return l8J.Forward;
		}
		set
		{
			if (value == Vector3.Zero)
			{
				l8J = Matrix.Identity;
			}
			else
			{
				l8J = CoreHelper.CreateMatrixFromNormalizedVectors(Vector3.Forward, Vector3.Normalize(value));
			}
		}
	}

	public override Matrix World
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = value;
			l8J.Translation = Vector3.Zero;
		}
	}

	public override int MoveId => 0;

	public DirectionalLight()
	{
		WorldBoundingBox = l8B;
		WorldBoundingSphere = l8s;
	}

	public int GetShadowSourceHashCode()
	{
		return ShadowPosition.GetHashCode();
	}

	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeEnum(ref l8_u0013_, info, "ShadowType", isflag: false);
		Direction = SerializationHelper.DeserializeField<Vector3>(info, "Direction");
		SerializationHelper.DeserializeField(ref l8N, info, "ShadowQuality", usedefault: true);
		SerializationHelper.DeserializeField(ref l8l, info, "ShadowPrimaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref l8L, info, "ShadowSecondaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref l8w, info, "ShadowPerSurfaceLOD", usedefault: true);
		base.SetObjectData(info, context);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("ShadowType", ShadowType);
		info.AddValue("Direction", Direction);
		info.AddValue("ShadowQuality", ShadowQuality);
		info.AddValue("ShadowPrimaryBias", ShadowPrimaryBias);
		info.AddValue("ShadowSecondaryBias", ShadowSecondaryBias);
		info.AddValue("ShadowPerSurfaceLOD", ShadowPerSurfaceLOD);
		base.GetObjectData(info, context);
	}
}
