using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Shadows;

/// <summary>
/// Provides a shareable point shadow source for use with PointLight objects.
/// Any number of PointLight objects can share the same shadow source.  Shadow
/// source position and properties are independent of the lights that reference it.
/// </summary>
[Serializable]
public class ShadowSource : IPointSource, IShadowSource, INamedObject, IFullSerializable, ISerializable
{
	private string fNB = "";

	private ShadowType fNa = ShadowType.AllObjects;

	private float fNg = 0.5f;

	private float fNf = 1f;

	private float fN_0003 = 0.2f;

	private bool fNn = true;

	private bool fN_0019;

	private Matrix fNL = Matrix.Identity;

	[CompilerGenerated]
	private bool fNv;

	/// <summary>
	/// The object's current name.
	/// </summary>
	public string Name
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
	/// Notifies the editor that this object is partially controlled via code. The editor
	/// will display information to the user indicating some property values are
	/// overridden in code and changes may not take effect.
	/// </summary>
	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return fNv;
		}
		[CompilerGenerated]
		set
		{
			fNv = value;
		}
	}

	/// <summary>
	/// Defines the type of objects that cast shadows from the source.
	/// Does not affect an object's ability to receive shadows.
	/// </summary>
	public ShadowType ShadowType
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
	/// Position in world space of the shadow source.
	/// </summary>
	public Vector3 ShadowPosition => fNL.Translation;

	/// <summary>
	/// Adjusts the visual quality of casts shadows.
	/// </summary>
	public float ShadowQuality
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = MathHelper.Clamp(value, 0f, 1f);
		}
	}

	/// <summary>
	/// Main property used to eliminate shadow artifacts.
	/// </summary>
	public float ShadowPrimaryBias
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
	/// Additional fine-tuned property used to eliminate shadow artifacts.
	/// </summary>
	public float ShadowSecondaryBias
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
	/// Enables independent level-of-detail per cubemap face on point-based lights.
	/// </summary>
	public bool ShadowPerSurfaceLOD
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
	/// Requests that all lights contained within the shadow source are rendered in one
	/// pass (this is only a performance hint - support depends on the rendering implementation).
	/// </summary>
	public bool ShadowRenderLightsTogether
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
	/// Position in world space of the source.
	/// </summary>
	public Vector3 Position
	{
		get
		{
			return fNL.Translation;
		}
		set
		{
			fNL.Translation = value;
		}
	}

	/// <summary>
	/// Maximum distance in world space of the source's influence.
	/// </summary>
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

	/// <summary>
	/// World space transform of the shadow source.
	/// </summary>
	public Matrix World
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

	internal void fy(IShadowSource P_0)
	{
		ShadowPerSurfaceLOD = P_0.ShadowPerSurfaceLOD;
		ShadowQuality = P_0.ShadowQuality;
		ShadowPrimaryBias = P_0.ShadowPrimaryBias;
		ShadowSecondaryBias = P_0.ShadowSecondaryBias;
		ShadowType = P_0.ShadowType;
		World = P_0.World;
	}

	/// <summary>
	/// Returns a hash code that uniquely identifies the shadow source
	/// and its current state.  Changes to ShadowPosition affects the
	/// hash code, which is used to trigger updates on related shadows.
	/// </summary>
	/// <returns>Shadow hash code.</returns>
	public int GetShadowSourceHashCode()
	{
		return ShadowPosition.GetHashCode();
	}

	/// <summary>
	/// Creates a new ShadowSource instance.
	/// </summary>
	public ShadowSource()
	{
	}

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		Vector3 field = default;
		SerializationHelper.DeserializeEnum(ref fNa, info, "ShadowType", isflag: false);
		SerializationHelper.DeserializeField(ref field, info, "Position", usedefault: true);
		Position = field;
		SerializationHelper.DeserializeField(ref fNB, info, "Name", usedefault: true);
		SerializationHelper.DeserializeField(ref fNg, info, "ShadowQuality", usedefault: true);
		SerializationHelper.DeserializeField(ref fNf, info, "ShadowPrimaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref fN_0003, info, "ShadowSecondaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref fNn, info, "ShadowPerSurfaceLOD", usedefault: true);
		SerializationHelper.DeserializeField(ref fN_0019, info, "ShadowRenderLightsTogether", usedefault: true);
	}

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
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
