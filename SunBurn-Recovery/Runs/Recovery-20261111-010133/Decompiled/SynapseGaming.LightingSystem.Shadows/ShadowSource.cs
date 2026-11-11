using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Shadows;

[Serializable]
public class ShadowSource : IPointSource, IShadowSource, INamedObject, IFullSerializable, ISerializable
{
	private string l8a = "";

	private ShadowType l8_u0013_ = ShadowType.AllObjects;

	private float l8N = 0.5f;

	private float l8l = 1f;

	private float l8L = 0.2f;

	private bool l8w = true;

	private bool l8J;

	private Matrix l8B = Matrix.Identity;

	[CompilerGenerated]
	private bool l8s;

	public string Name
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

	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l8s;
		}
		[CompilerGenerated]
		set
		{
			l8s = value;
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

	public Vector3 ShadowPosition => l8B.Translation;

	public float ShadowQuality
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = MathHelper.Clamp(value, 0f, 1f);
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

	public bool ShadowRenderLightsTogether
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

	public Vector3 Position
	{
		get
		{
			return l8B.Translation;
		}
		set
		{
			l8B.Translation = value;
		}
	}

	public float Radius
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	public Matrix World
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

	internal void ld(IShadowSource param_0)
	{
		ShadowPerSurfaceLOD = param_0.ShadowPerSurfaceLOD;
		ShadowQuality = param_0.ShadowQuality;
		ShadowPrimaryBias = param_0.ShadowPrimaryBias;
		ShadowSecondaryBias = param_0.ShadowSecondaryBias;
		ShadowType = param_0.ShadowType;
		World = param_0.World;
	}

	public int GetShadowSourceHashCode()
	{
		return ShadowPosition.GetHashCode();
	}

	public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		Vector3 field = default;
		SerializationHelper.DeserializeEnum(ref l8_u0013_, info, "ShadowType", isflag: false);
		SerializationHelper.DeserializeField(ref field, info, "Position", usedefault: true);
		Position = field;
		SerializationHelper.DeserializeField(ref l8a, info, "Name", usedefault: true);
		SerializationHelper.DeserializeField(ref l8N, info, "ShadowQuality", usedefault: true);
		SerializationHelper.DeserializeField(ref l8l, info, "ShadowPrimaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref l8L, info, "ShadowSecondaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref l8w, info, "ShadowPerSurfaceLOD", usedefault: true);
		SerializationHelper.DeserializeField(ref l8J, info, "ShadowRenderLightsTogether", usedefault: true);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("Name", Name);
		info.AddValue("ShadowType", ShadowType);
		info.AddValue("Position", Position);
		info.AddValue("Radius", Radius);
		info.AddValue("ShadowQuality", ShadowQuality);
		info.AddValue("ShadowPrimaryBias", ShadowPrimaryBias);
		info.AddValue("ShadowSecondaryBias", ShadowSecondaryBias);
		info.AddValue("ShadowPerSurfaceLOD", ShadowPerSurfaceLOD);
		info.AddValue("ShadowRenderLightsTogether", ShadowRenderLightsTogether);
	}
}
