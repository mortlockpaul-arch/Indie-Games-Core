using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Provides user and hardware specific preferences to the Lighting System.
/// </summary>
[Serializable]
public class LightingSystemPreferences : ILightingSystemPreferences, IPreferences, IFullSerializable, ISerializable
{
	private SamplingPreference fNB = SamplingPreference.Trilinear;

	private int fNa = 4;

	private DetailPreference fNg = DetailPreference.Medium;

	private float fNf = 1f;

	private DetailPreference fN_0003;

	private DetailPreference fNn;

	private DetailPreference fN_0019;

	/// <summary>
	/// Sets the user preferred balance of texture sampling quality and performance.
	/// </summary>
	public SamplingPreference TextureSampling
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

	/// <summary>
	/// Sets the maximum anisotropy level when TextureSampling is set to Anisotropic.
	/// </summary>
	public int MaxAnisotropy
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
	/// Sets the user preferred balance of shadow filtering quality and performance.
	/// </summary>
	public DetailPreference ShadowDetail
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

	/// <summary>
	/// Sets the user preferred balance of shadow resolution and performance.
	/// </summary>
	public float ShadowQuality
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

	/// <summary>
	/// Sets the user preferred balance of LightingEffect detail and performance.
	/// </summary>
	public DetailPreference EffectDetail
	{
		get
		{
			return fN_0003;
		}
		set
		{
			fN_0003 = value;
		}
	}

	/// <summary>
	/// Sets the user preferred balance of lighting detail and performance.
	/// </summary>
	public DetailPreference LightingDetail
	{
		get
		{
			return fNn;
		}
		set
		{
			fNn = value;
		}
	}

	/// <summary>
	/// Sets the user preferred balance of post-processing effect detail and performance.
	/// </summary>
	public DetailPreference PostProcessingDetail
	{
		get
		{
			return fN_0019;
		}
		set
		{
			fN_0019 = value;
		}
	}

	/// <summary>
	/// Creates a new LightingSystemPreferences object.
	/// </summary>
	public LightingSystemPreferences()
	{
	}

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeEnum(ref fNB, info, "TextureSampling", isflag: false);
		SerializationHelper.DeserializeField(ref fNa, info, "MaxAnisotropy", usedefault: true);
		SerializationHelper.DeserializeEnum(ref fNg, info, "ShadowDetail", isflag: false);
		SerializationHelper.DeserializeField(ref fNf, info, "ShadowQuality", usedefault: true);
		SerializationHelper.DeserializeEnum(ref fN_0003, info, "EffectDetail", isflag: false);
		SerializationHelper.DeserializeEnum(ref fNn, info, "LightingDetail", isflag: false);
		SerializationHelper.DeserializeEnum(ref fN_0019, info, "PostProcessingDetail", isflag: false);
	}

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("TextureSampling", fNB);
		info.AddValue("MaxAnisotropy", fNa);
		info.AddValue("ShadowDetail", fNg);
		info.AddValue("ShadowQuality", fNf);
		info.AddValue("EffectDetail", fN_0003);
		info.AddValue("LightingDetail", fNn);
		info.AddValue("PostProcessingDetail", fN_0019);
	}
}
