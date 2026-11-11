using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Serialization;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Lights;

[Serializable]
public class PointLight : BaseLight, IPointSource, IShadowSource
{
	private LightingType l8a = LightingType.RealTime;

	private bool l8_u0013_;

	private int l8N;

	private float l8l;

	private ShadowType l8L;

	private float l8w = 0.5f;

	private float l8J = 1f;

	private float l8B = 0.2f;

	private bool l8s = true;

	private float l8h = 10f;

	private Matrix l8_u0002_ = Matrix.Identity;

	private IShadowSource l8v;

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
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
		}
	}

	public override float FalloffStrength
	{
		get
		{
			return l8l;
		}
		set
		{
			l8l = MathHelper.Clamp(value, 0f, 1f);
		}
	}

	public override IShadowSource ShadowSource
	{
		get
		{
			if (l8v == null)
			{
				throw new ArgumentException("ShadowSource is null. This can result in poor rendering performance.");
			}
			return l8v;
		}
		set
		{
			if (value == null)
			{
				l8v = this;
			}
			else
			{
				l8v = value;
			}
		}
	}

	public ShadowType ShadowType
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

	public Vector3 ShadowPosition => l8_u0002_.Translation;

	public float ShadowQuality
	{
		get
		{
			return l8w;
		}
		set
		{
			l8w = MathHelper.Clamp(value, 0f, 1f);
		}
	}

	public float ShadowPrimaryBias
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = value;
		}
	}

	public float ShadowSecondaryBias
	{
		get
		{
			return l8B;
		}
		set
		{
			l8B = value;
		}
	}

	public bool ShadowPerSurfaceLOD
	{
		get
		{
			return l8s;
		}
		set
		{
			l8s = value;
		}
	}

	public bool ShadowRenderLightsTogether => false;

	public Vector3 Position
	{
		get
		{
			return l8_u0002_.Translation;
		}
		set
		{
			l8_u0002_.Translation = value;
			l8N++;
			UpdateBounds();
		}
	}

	public float Radius
	{
		get
		{
			return l8h;
		}
		set
		{
			l8h = value;
			UpdateBounds();
		}
	}

	public override Matrix World
	{
		get
		{
			return l8_u0002_;
		}
		set
		{
			l8_u0002_ = value;
			l8N++;
			UpdateBounds();
		}
	}

	public override int MoveId => l8N;

	public override bool InfiniteBounds => false;

	public PointLight()
	{
		l8v = this;
		UpdateBounds();
	}

	protected virtual void UpdateBounds()
	{
		Vector3 vector = new Vector3(l8h, l8h, l8h);
		Vector3 translation = l8_u0002_.Translation;
		WorldBoundingBox = new BoundingBox(translation - vector, translation + vector);
		WorldBoundingSphere = new BoundingSphere(l8_u0002_.Translation, l8h);
	}

	public int GetShadowSourceHashCode()
	{
		return ShadowPosition.GetHashCode();
	}

	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeEnum(ref l8L, info, "ShadowType", isflag: false);
		Position = SerializationHelper.DeserializeField<Vector3>(info, "Position");
		SerializationHelper.DeserializeField(ref l8h, info, "Radius", usedefault: true);
		SerializationHelper.DeserializeField(ref l8w, info, "ShadowQuality", usedefault: true);
		SerializationHelper.DeserializeField(ref l8J, info, "ShadowPrimaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref l8B, info, "ShadowSecondaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref l8s, info, "ShadowPerSurfaceLOD", usedefault: true);
		UpdateBounds();
		base.SetObjectData(info, context);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("ShadowType", ShadowType);
		info.AddValue("Position", Position);
		info.AddValue("Radius", Radius);
		info.AddValue("ShadowQuality", ShadowQuality);
		info.AddValue("ShadowPrimaryBias", ShadowPrimaryBias);
		info.AddValue("ShadowSecondaryBias", ShadowSecondaryBias);
		info.AddValue("ShadowPerSurfaceLOD", ShadowPerSurfaceLOD);
		base.GetObjectData(info, context);
	}
}
