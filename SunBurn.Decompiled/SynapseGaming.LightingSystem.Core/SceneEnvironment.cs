using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using L;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Provides scene environmental information to the lighting system.
/// </summary>
[Serializable]
public class SceneEnvironment : ISceneEnvironment, IEditorObject, INamedObject, IFullSerializable, ISerializable, L.v, IDisposable
{
	private float fNB = 300f;

	private bool fNa = true;

	private float fNg = 200f;

	private float fNf = 300f;

	private Vector3 fN_0003 = Vector3.One * 0.75f;

	private float fNn = 300f;

	private float fN_0019 = 300f;

	private float fNL = 300f;

	private float fNv = 3f;

	private float fN_0012 = 0.9f;

	private bool fN_0015 = true;

	private float fN8 = 1f;

	private float fNi = 0.5f;

	private float fN_0005 = 0.1f;

	private float fNj = 0.5f;

	private float fNo = 0.5f;

	private float fNc = 100f;

	private float fNd = 0.01f;

	private float fNw = 1f;

	private float fNs = 1f;

	private float fN_0011 = 1f;

	private float fNQ = 1f;

	private float fNG = 0.1f;

	private string fN_0017 = "";

	private string fNm = "";

	private string fNh = "";

	private bool fNR;

	[CompilerGenerated]
	private bool fNW;

	/// <summary>
	/// Maximum world space distance objects are visible.
	/// </summary>
	public float VisibleDistance
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
	/// Enables scene fog.
	/// </summary>
	public bool FogEnabled
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
	/// World space distance that fog begins.
	/// </summary>
	public float FogStartDistance
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
	/// World space distance that fog fully obscures objects.
	/// </summary>
	public float FogEndDistance
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
	/// Color applied to scene fog.
	/// </summary>
	public Vector3 FogColor
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
	/// World space distance that directional shadows begin fading.
	/// </summary>
	public float ShadowFadeStartDistance
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
	/// World space distance that directional shadows completely disappear.
	/// </summary>
	public float ShadowFadeEndDistance
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
	/// World space distance used to include shadow casters. This allows including shadows
	/// from objects further away than the shadow fade area, for instance shadows from
	/// distant mountains.
	/// </summary>
	public float ShadowCasterDistance
	{
		get
		{
			return fNL;
		}
		set
		{
			fNL = value;
		}
	}

	/// <summary>
	/// Strength of bloom applied to the scene.
	/// </summary>
	public float BloomAmount
	{
		get
		{
			return fNv;
		}
		set
		{
			fNv = value;
		}
	}

	/// <summary>
	/// Minimum pixel intensity required for bloom to occur.
	/// </summary>
	public float BloomThreshold
	{
		get
		{
			return fN_0012;
		}
		set
		{
			fN_0012 = value;
		}
	}

	/// <summary>
	/// Enables High Dynamic Range.
	/// </summary>
	public bool DynamicRangeEnabled
	{
		get
		{
			return fN_0015;
		}
		set
		{
			fN_0015 = value;
		}
	}

	/// <summary>
	/// Intensity of the scene exposure.
	/// </summary>
	public float ExposureAmount
	{
		get
		{
			return fN8;
		}
		set
		{
			fN8 = value;
		}
	}

	/// <summary>
	/// Intensity of scene colors when using High Dynamic Range.
	/// </summary>
	public float DynamicRangeSaturationAmount
	{
		get
		{
			return fNi;
		}
		set
		{
			fNi = value;
		}
	}

	/// <summary>
	/// Intensity of scene contrast when using High Dynamic Range.
	/// </summary>
	public float DynamicRangeDarkenAmount
	{
		get
		{
			return fN_0005;
		}
		set
		{
			fN_0005 = value;
		}
	}

	/// <summary>
	/// Intensity of High Dynamic Range color correction and simulated film exposure effect.
	/// </summary>
	public float DynamicRangeCinematicAmount
	{
		get
		{
			return fNj;
		}
		set
		{
			fNj = value;
		}
	}

	/// <summary>
	/// Time required to fully adjust High Dynamic Range to lighting changes.
	/// </summary>
	public float DynamicRangeTransitionTime
	{
		get
		{
			return fNo;
		}
		set
		{
			fNo = value;
		}
	}

	/// <summary>
	/// Maximum intensity increase allowed for High Dynamic Range. Limits intensity
	/// increases, which sets the darkness-level where the scene will remain dark.
	/// </summary>
	public float DynamicRangeTransitionMaxScale
	{
		get
		{
			return fNc;
		}
		set
		{
			fNc = value;
		}
	}

	/// <summary>
	/// Maximum intensity decrease allowed for High Dynamic Range. Limits intensity
	/// decreases, which sets the brightness-level where the scene will remain overly bright.
	/// </summary>
	public float DynamicRangeTransitionMinScale
	{
		get
		{
			return fNd;
		}
		set
		{
			fNd = value;
		}
	}

	/// <summary>
	/// Amount of gravity applied to dynamic collide-able objects in the scene.
	/// </summary>
	public float Gravity
	{
		get
		{
			return fNw;
		}
		set
		{
			fNw = value;
		}
	}

	/// <summary>
	/// The object's current name.
	/// </summary>
	public string Name
	{
		get
		{
			return fN_0017;
		}
		set
		{
		}
	}

	/// <summary>
	/// Notifies the editor that this object is partially controlled via code. The editor
	/// will display information to the user indicating some property values are
	/// overridden in code and changes may not take effect.
	/// </summary>
	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return fNW;
		}
		[CompilerGenerated]
		set
		{
			fNW = value;
		}
	}

	internal string FileName
	{
		get
		{
			return fNm;
		}
		set
		{
			fNm = value;
		}
	}

	internal string ProjectFile
	{
		get
		{
			return fNh;
		}
		set
		{
			fNh = value;
		}
	}

	string L.v.ProjectFile => fNh;

	internal void gn(string P_0)
	{
		fN_0017 = P_0;
	}

	/// <summary>
	/// Creates a new SceneEnvironment instance.
	/// </summary>
	public SceneEnvironment()
	{
		LightingSystemEditor.OnCreateResource(this);
	}

	/// <summary>
	/// Releases resources allocated by this object.
	/// </summary>
	public void Dispose()
	{
		if (!fNR)
		{
			fNR = true;
			LightingSystemEditor.OnDisposeResource(this);
		}
	}

	internal static SceneEnvironment g_0019(string P_0)
	{
		SceneEnvironment sceneEnvironment = SerializationHelper.LoadFromXml<SceneEnvironment>(P_0);
		if (sceneEnvironment == null)
		{
			sceneEnvironment = new SceneEnvironment();
		}
		return sceneEnvironment;
	}

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeField(ref fNB, info, "VisibleDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref fNa, info, "FogEnabled", usedefault: false);
		SerializationHelper.DeserializeField(ref fN_0003, info, "FogColor", usedefault: false);
		SerializationHelper.DeserializeField(ref fNg, info, "FogStartDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref fNf, info, "FogEndDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref fNn, info, "ShadowFadeStartDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref fN_0019, info, "ShadowFadeEndDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref fNL, info, "ShadowCasterDistance", usedefault: false);
		SerializationHelper.DeserializeField(ref fNv, info, "BloomAmount", usedefault: false);
		SerializationHelper.DeserializeField(ref fN_0012, info, "BloomThreshold", usedefault: false);
		SerializationHelper.DeserializeField(ref fN_0015, info, "DynamicRangeEnabled", usedefault: false);
		SerializationHelper.DeserializeField(ref fN8, info, "ExposureAmount", usedefault: false);
		SerializationHelper.DeserializeField(ref fNc, info, "DynamicRangeTransitionMaxScale", usedefault: false);
		SerializationHelper.DeserializeField(ref fNd, info, "DynamicRangeTransitionMinScale", usedefault: false);
		SerializationHelper.DeserializeField(ref fNo, info, "DynamicRangeTransitionTime", usedefault: false);
		SerializationHelper.DeserializeField(ref fNi, info, "DynamicRangeSaturationAmount", usedefault: false);
		SerializationHelper.DeserializeField(ref fN_0005, info, "DynamicRangeDarkenAmount", usedefault: false);
		SerializationHelper.DeserializeField(ref fNj, info, "DynamicRangeCinematicAmount", usedefault: false);
		SerializationHelper.DeserializeField(ref fNw, info, "Gravity", usedefault: false);
	}

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
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
