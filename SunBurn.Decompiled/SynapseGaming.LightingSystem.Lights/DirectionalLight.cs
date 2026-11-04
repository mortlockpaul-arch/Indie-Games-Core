using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using L;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Serialization;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Lights;

/// <summary>
/// Provides directional light (sunlight) information for rendering lighting and shadows.
/// </summary>
[Serializable]
public class DirectionalLight : BaseLight, IDirectionalSource, IShadowSource
{
	private LightingType fNB = LightingType.RealTime;

	private ShadowType fNa = ShadowType.AllObjects;

	private float fNg = 1f;

	private float fNf = 1f;

	private float fN_0003 = 0.2f;

	private bool fNn = true;

	private Matrix fN_0019 = L._0019.aO(Vector3.Forward, Vector3.Down);

	private static BoundingBox fNL = new BoundingBox(new Vector3(float.MinValue, float.MinValue, float.MinValue), new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

	private static BoundingSphere fNv = new BoundingSphere(default, float.MaxValue);

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
	/// Unused.
	/// </summary>
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

	/// <summary>
	/// Controls how quickly lighting falls off over distance (unused in this light type).
	/// </summary>
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

	/// <summary>
	/// Indicates the object bounding area spans the entire world and
	/// the object is always visible.
	/// </summary>
	public override bool InfiniteBounds => true;

	/// <summary>
	/// Shadow source the light's shadows are generated from.
	/// </summary>
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

	/// <summary>
	/// Defines the type of objects that cast shadows from the light.
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
	public Vector3 ShadowPosition => Direction * -1000000f;

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
			fNg = MathHelper.Clamp(value, 0f, 2f);
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
	/// Unused.
	/// </summary>
	public bool ShadowRenderLightsTogether => false;

	/// <summary>
	/// Direction in world space of the light's influence.
	/// </summary>
	public Vector3 Direction
	{
		get
		{
			return fN_0019.Forward;
		}
		set
		{
			if (value == Vector3.Zero)
			{
				fN_0019 = Matrix.Identity;
			}
			else
			{
				fN_0019 = L._0019.aO(Vector3.Forward, Vector3.Normalize(value));
			}
		}
	}

	/// <summary>
	/// World space transform of the light.
	/// </summary>
	public override Matrix World
	{
		get
		{
			return fN_0019;
		}
		set
		{
			fN_0019 = value;
			fN_0019.Translation = Vector3.Zero;
		}
	}

	/// <summary>
	/// Indicates the current move. This value increments each time the object
	/// is moved (when the World transform changes).
	/// </summary>
	public override int MoveId => 0;

	/// <summary>
	/// Creates a new DirectionalLight instance.
	/// </summary>
	public DirectionalLight()
	{
		WorldBoundingBox = fNL;
		WorldBoundingSphere = fNv;
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
		SerializationHelper.DeserializeEnum(ref fNa, info, "ShadowType", isflag: false);
		Direction = SerializationHelper.DeserializeField<Vector3>(info, "Direction");
		SerializationHelper.DeserializeField(ref fNg, info, "ShadowQuality", usedefault: true);
		SerializationHelper.DeserializeField(ref fNf, info, "ShadowPrimaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref fN_0003, info, "ShadowSecondaryBias", usedefault: true);
		SerializationHelper.DeserializeField(ref fNn, info, "ShadowPerSurfaceLOD", usedefault: true);
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
		info.AddValue("Direction", Direction);
		info.AddValue("ShadowQuality", ShadowQuality);
		info.AddValue("ShadowPrimaryBias", ShadowPrimaryBias);
		info.AddValue("ShadowSecondaryBias", ShadowSecondaryBias);
		info.AddValue("ShadowPerSurfaceLOD", ShadowPerSurfaceLOD);
		base.GetObjectData(info, context);
	}
}
