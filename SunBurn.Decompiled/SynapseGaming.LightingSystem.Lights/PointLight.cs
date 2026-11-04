using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Serialization;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Lights;

/// <summary>
/// Provides point light (aka: omni light) information for rendering lighting and shadows.
/// </summary>
[Serializable]
public class PointLight : BaseLight, IPointSource, IShadowSource
{
	private LightingType fNB = LightingType.RealTime;

	private bool fNa;

	private int fNg;

	private float fNf;

	private ShadowType fN_0003;

	private float fNn = 0.5f;

	private float fN_0019 = 1f;

	private float fNL = 0.2f;

	private bool fNv = true;

	private float fN_0012 = 10f;

	private Matrix fN_0015 = Matrix.Identity;

	private IShadowSource fN8;

	/// <summary>
	/// Determines if the lighting is real-time or bake-down.
	/// </summary>
	public override LightingType LightingType
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
	/// Provides softer indirect-like illumination without "hot-spots".
	/// </summary>
	public override bool FillLight
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
	/// Controls how quickly lighting falls off over distance (only available in deferred rendering).
	/// Value ranges from 0.0f to 1.0f.
	/// </summary>
	public override float FalloffStrength
	{
		get
		{
			return fNf;
		}
		set
		{
			fNf = MathHelper.Clamp(value, 0f, 1f);
		}
	}

	/// <summary>
	/// Shadow source the light's shadows are generated from.
	/// Allows sharing shadows between point light sources.
	/// </summary>
	public override IShadowSource ShadowSource
	{
		get
		{
			if (fN8 == null)
			{
				throw new ArgumentException("ShadowSource is null. This can result in poor rendering performance.");
			}
			return fN8;
		}
		set
		{
			if (value == null)
			{
				fN8 = this;
			}
			else
			{
				fN8 = value;
			}
		}
	}

	/// <summary>
	/// Defines the type of objects that cast shadows from the light.
	/// Does not affect an object's ability to receive shadows.
	/// </summary>
	public ShadowType ShadowType
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
	/// Position in world space of the shadow source.
	/// </summary>
	public Vector3 ShadowPosition => fN_0015.Translation;

	/// <summary>
	/// Adjusts the visual quality of casts shadows.
	/// </summary>
	public float ShadowQuality
	{
		get
		{
			return fNn;
		}
		set
		{
			fNn = MathHelper.Clamp(value, 0f, 1f);
		}
	}

	/// <summary>
	/// Main property used to eliminate shadow artifacts.
	/// </summary>
	public float ShadowPrimaryBias
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
	/// Additional fine-tuned property used to eliminate shadow artifacts.
	/// </summary>
	public float ShadowSecondaryBias
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
	/// Enables independent level-of-detail per cubemap face on point-based lights.
	/// </summary>
	public bool ShadowPerSurfaceLOD
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
	/// Unused.
	/// </summary>
	public bool ShadowRenderLightsTogether => false;

	/// <summary>
	/// Position in world space of the light.
	/// </summary>
	public Vector3 Position
	{
		get
		{
			return fN_0015.Translation;
		}
		set
		{
			fN_0015.Translation = value;
			fNg++;
			UpdateBounds();
		}
	}

	/// <summary>
	/// Maximum distance in world space of the light's influence.
	/// </summary>
	public float Radius
	{
		get
		{
			return fN_0012;
		}
		set
		{
			fN_0012 = value;
			UpdateBounds();
		}
	}

	/// <summary>
	/// World space transform of the light.
	/// </summary>
	public override Matrix World
	{
		get
		{
			return fN_0015;
		}
		set
		{
			fN_0015 = value;
			fNg++;
			UpdateBounds();
		}
	}

	/// <summary>
	/// Indicates the current move. This value increments each time the object
	/// is moved (when the World transform changes).
	/// </summary>
	public override int MoveId => fNg;

	/// <summary>
	/// Indicates the object bounding area spans the entire world and
	/// the object is always visible.
	/// </summary>
	public override bool InfiniteBounds => false;

	/// <summary>
	/// Creates a new PointLight instance.
	/// </summary>
	public PointLight()
	{
		fN8 = this;
		UpdateBounds();
	}

	/// <summary />
	protected virtual void UpdateBounds()
	{
		Vector3 vector = new Vector3(fN_0012, fN_0012, fN_0012);
		Vector3 translation = fN_0015.Translation;
		WorldBoundingBox = new BoundingBox(translation - vector, translation + vector);
		WorldBoundingSphere = new BoundingSphere(fN_0015.Translation, fN_0012);
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
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeEnum(ref fN_0003, info, "ShadowType", isflag: false);
		Position = SerializationHelper.DeserializeField<Vector3>(info, "Position");
		SerializationHelper.DeserializeField(ref fN_0012, info, "Radius", usedefault: true);
		SerializationHelper.DeserializeField(ref fNn, info, "ShadowQuality", usedefault: true);
		SerializationHelper.DeserializeField(ref fN_0019, info, "ShadowPrimaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref fNL, info, "ShadowSecondaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref fNv, info, "ShadowPerSurfaceLOD", usedefault: true);
		UpdateBounds();
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
