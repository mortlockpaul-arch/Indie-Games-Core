using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Core;

[Serializable]
public class SystemPreferences : ISystemPreferences, IPreferences, IFullSerializable, ISerializable
{
	private SamplingPreference l8a = SamplingPreference.Trilinear;

	private int l8_u0013_ = 4;

	private DetailPreference l8N = DetailPreference.Medium;

	private float l8l = 1f;

	private DetailPreference l8L;

	private DetailPreference l8w;

	private DetailPreference l8J;

	public SamplingPreference TextureSampling
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

	public int MaxAnisotropy
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

	public DetailPreference ShadowDetail
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
		}
	}

	public float ShadowQuality
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

	public DetailPreference EffectDetail
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

	public DetailPreference LightingDetail
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

	public DetailPreference PostProcessingDetail
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

	public void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeEnum(ref l8a, info, "TextureSampling", isflag: false);
		SerializationHelper.DeserializeField(ref l8_u0013_, info, "MaxAnisotropy", usedefault: true);
		SerializationHelper.DeserializeEnum(ref l8N, info, "ShadowDetail", isflag: false);
		SerializationHelper.DeserializeField(ref l8l, info, "ShadowQuality", usedefault: true);
		SerializationHelper.DeserializeEnum(ref l8L, info, "EffectDetail", isflag: false);
		SerializationHelper.DeserializeEnum(ref l8w, info, "LightingDetail", isflag: false);
		SerializationHelper.DeserializeEnum(ref l8J, info, "PostProcessingDetail", isflag: false);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("TextureSampling", l8a);
		info.AddValue("MaxAnisotropy", l8_u0013_);
		info.AddValue("ShadowDetail", l8N);
		info.AddValue("ShadowQuality", l8l);
		info.AddValue("EffectDetail", l8L);
		info.AddValue("LightingDetail", l8w);
		info.AddValue("PostProcessingDetail", l8J);
	}
}
