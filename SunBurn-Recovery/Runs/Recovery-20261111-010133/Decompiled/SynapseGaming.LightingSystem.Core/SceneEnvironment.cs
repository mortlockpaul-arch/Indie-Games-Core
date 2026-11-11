using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using B;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Core;

[Serializable]
public class SceneEnvironment : ISceneEnvironment, IEditorObject, INamedObject, IFullSerializable, ISerializable, B.J, IDisposable
{
	private float l8a = 300f;

	private bool l8_u0013_ = true;

	private float l8N = 200f;

	private float l8l = 300f;

	private Vector3 l8L = Vector3.One * 0.75f;

	private float l8w = 300f;

	private float l8J = 300f;

	private float l8B = 300f;

	private float l8s = 3f;

	private float l8h = 0.9f;

	private bool l8_u0002_ = true;

	private float l8v = 1f;

	private float l8Y = 0.5f;

	private float l8_u0005_ = 0.1f;

	private float l8b = 0.5f;

	private float l8I = 0.5f;

	private float l85 = 100f;

	private float l8W = 0.01f;

	private float l8Z = 1f;

	private string l8T = "";

	private string l8_u0014_ = "";

	private string l8E = "";

	private bool l8f;

	[CompilerGenerated]
	private bool l8_u0016_;

	public float VisibleDistance
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

	public bool FogEnabled
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

	public float FogStartDistance
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

	public float FogEndDistance
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

	public Vector3 FogColor
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

	public float ShadowFadeStartDistance
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

	public float ShadowFadeEndDistance
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

	public float ShadowCasterDistance
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

	public float BloomAmount
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

	public float BloomThreshold
	{
		get
		{
			return l8h;
		}
		set
		{
			l8h = value;
		}
	}

	public bool DynamicRangeEnabled
	{
		get
		{
			return l8_u0002_;
		}
		set
		{
			l8_u0002_ = value;
		}
	}

	public float ExposureAmount
	{
		get
		{
			return l8v;
		}
		set
		{
			l8v = value;
		}
	}

	public float DynamicRangeSaturationAmount
	{
		get
		{
			return l8Y;
		}
		set
		{
			l8Y = value;
		}
	}

	public float DynamicRangeDarkenAmount
	{
		get
		{
			return l8_u0005_;
		}
		set
		{
			l8_u0005_ = value;
		}
	}

	public float DynamicRangeCinematicAmount
	{
		get
		{
			return l8b;
		}
		set
		{
			l8b = value;
		}
	}

	public float DynamicRangeTransitionTime
	{
		get
		{
			return l8I;
		}
		set
		{
			l8I = value;
		}
	}

	public float DynamicRangeTransitionMaxScale
	{
		get
		{
			return l85;
		}
		set
		{
			l85 = value;
		}
	}

	public float DynamicRangeTransitionMinScale
	{
		get
		{
			return l8W;
		}
		set
		{
			l8W = value;
		}
	}

	public float Gravity
	{
		get
		{
			return l8Z;
		}
		set
		{
			l8Z = value;
		}
	}

	public string Name
	{
		get
		{
			return l8T;
		}
		set
		{
		}
	}

	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l8_u0016_;
		}
		[CompilerGenerated]
		set
		{
			l8_u0016_ = value;
		}
	}

	internal string FileName
	{
		get
		{
			return l8_u0014_;
		}
		set
		{
			l8_u0014_ = value;
		}
	}

	internal string ProjectFile
	{
		get
		{
			return l8E;
		}
		set
		{
			l8E = value;
		}
	}

	string B.J.ProjectFile => l8E;

	internal void _u0013_X(string param_0)
	{
		l8T = param_0;
	}

	public SceneEnvironment()
	{
		SunBurnEditor.OnCreateResource(this);
	}

	public void Dispose()
	{
		if (!l8f)
		{
			l8f = true;
			SunBurnEditor.OnDisposeResource(this);
		}
	}

	internal static SceneEnvironment _u0013__u0019_(string param_0)
	{
		SceneEnvironment sceneEnvironment = SerializationHelper.LoadFromXml<SceneEnvironment>(param_0);
		if (sceneEnvironment == null)
		{
			sceneEnvironment = new SceneEnvironment();
		}
		return sceneEnvironment;
	}

	public void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeField(ref l8a, info, "VisibleDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref l8_u0013_, info, "FogEnabled", usedefault: false);
		SerializationHelper.DeserializeField(ref l8L, info, "FogColor", usedefault: false);
		SerializationHelper.DeserializeField(ref l8N, info, "FogStartDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref l8l, info, "FogEndDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref l8w, info, "ShadowFadeStartDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref l8J, info, "ShadowFadeEndDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref l8B, info, "ShadowCasterDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref l8s, info, "BloomAmount", usedefault: false);
		SerializationHelper.DeserializeField(ref l8h, info, "BloomThreshold", usedefault: false);
		SerializationHelper.DeserializeField(ref l8_u0002_, info, "DynamicRangeEnabled", usedefault: false);
		SerializationHelper.DeserializeField(ref l8v, info, "ExposureAmount", usedefault: false);
		SerializationHelper.DeserializeField(ref l85, info, "DynamicRangeTransitionMaxScale", usedefault: false);
		SerializationHelper.DeserializeField(ref l8W, info, "DynamicRangeTransitionMinScale", usedefault: false);
		SerializationHelper.DeserializeField(ref l8I, info, "DynamicRangeTransitionTime", usedefault: false);
		SerializationHelper.DeserializeField(ref l8Y, info, "DynamicRangeSaturationAmount", usedefault: false);
		SerializationHelper.DeserializeField(ref l8_u0005_, info, "DynamicRangeDarkenAmount", usedefault: false);
		SerializationHelper.DeserializeField(ref l8b, info, "DynamicRangeCinematicAmount", usedefault: false);
		SerializationHelper.DeserializeField(ref l8Z, info, "Gravity", usedefault: false);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("VisibleDistance", VisibleDistance);
		info.AddValue("FogEnabled", FogEnabled);
		info.AddValue("FogColor", FogColor);
		info.AddValue("FogStartDistance", FogStartDistance);
		info.AddValue("FogEndDistance", FogEndDistance);
		info.AddValue("ShadowFadeStartDistance", ShadowFadeStartDistance);
		info.AddValue("ShadowFadeEndDistance", ShadowFadeEndDistance);
		info.AddValue("ShadowCasterDistance", ShadowCasterDistance);
		info.AddValue("BloomAmount", BloomAmount);
		info.AddValue("BloomThreshold", BloomThreshold);
		info.AddValue("DynamicRangeEnabled", DynamicRangeEnabled);
		info.AddValue("ExposureAmount", ExposureAmount);
		info.AddValue("DynamicRangeTransitionMaxScale", DynamicRangeTransitionMaxScale);
		info.AddValue("DynamicRangeTransitionMinScale", DynamicRangeTransitionMinScale);
		info.AddValue("DynamicRangeTransitionTime", DynamicRangeTransitionTime);
		info.AddValue("DynamicRangeSaturationAmount", DynamicRangeSaturationAmount);
		info.AddValue("DynamicRangeDarkenAmount", DynamicRangeDarkenAmount);
		info.AddValue("DynamicRangeCinematicAmount", DynamicRangeCinematicAmount);
		info.AddValue("Gravity", Gravity);
	}
}
