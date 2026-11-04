using System;
using System.Collections.Generic;
using _0013;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using R;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;
using SynapseGaming.LightingSystem.Shadows;
using d;
using h;
using j;

namespace SynapseGaming.LightingSystem.Lights;

/// <summary>
/// Manages all scene lights and allows querying the scene with
/// a view or bounding box for lights that affect the area
/// (acts as a light scenegraph).
/// </summary>
public class LightManager : BaseLightManager, ILightManager, IWorldRenderableManager, IRenderableManager, IUpdatableManager, ILightQuery, IQuery<BaseLight>, IManagerService, IManager, IUnloadable, ISubmit<BaseLight>, ISubmit<IScene>
{
	private int fNB = 30;

	private int fNa = 8;

	private bool fNg;

	private j._0012 fNf;

	private d.a fN_0003;

	private DetailPreference fNn;

	private IGraphicsDeviceService fN_0019;

	private List<IScene> fNL = new List<IScene>(16);

	private List<BaseLight> fNv = new List<BaseLight>(64);

	/// <summary>
	/// Gets the manager specific Type used as a unique key for storing and
	/// requesting the manager from the IManagerServiceProvider.
	/// </summary>
	public Type ManagerType => SceneInterface.LightManagerType;

	/// <summary>
	/// Sets the order this manager is processed relative to other managers
	/// in the IManagerServiceProvider. Managers with lower processing order
	/// values are processed first.
	///
	/// In the case of BeginFrameRendering and EndFrameRendering, BeginFrameRendering
	/// is processed in the normal order (lowest order value to highest), however
	/// EndFrameRendering is processed in reverse order (highest to lowest) to ensure
	/// the first manager begun is the last one ended (FILO).
	/// </summary>
	public int ManagerProcessOrder
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
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	public IGraphicsDeviceService GraphicsDeviceManager => fN_0019;

	/// <summary>
	/// Number of geometry layers (slices) used to construct volume lights. Increasing
	/// the number of slices improves volume lighting quality, decreasing the number of
	/// slices improves performance.
	/// </summary>
	public int VolumeLightSliceCount
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
			fS();
		}
	}

	/// <summary>
	/// Determines if volume light slices should be oriented to the camera.
	/// </summary>
	public bool BillboardVolumeLightSlices
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
	/// Creates a new LightManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="sceneinterface">Service provider used to access all other manager services in this scene.</param>
	/// <param name="worldboundingbox">The smallest bounding area that completely
	/// contains the scene. Helps the LightManager build an optimal scene tree.</param>
	/// <param name="worldtreemaxdepth">Maximum depth for entries in the scene tree. Small
	/// scenes with few objects see better performance with shallow trees. Large complex
	/// scenes often need deeper trees.</param>
	public LightManager(IGraphicsDeviceService graphicsdevicemanager, IManagerServiceProvider sceneinterface, BoundingBox worldboundingbox, int worldtreemaxdepth)
		: base(sceneinterface, worldboundingbox, worldtreemaxdepth)
	{
		fN_0019 = graphicsdevicemanager;
	}

	/// <summary>
	/// Creates a new LightManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="sceneinterface">Service provider used to access all other manager services in this scene.</param>
	public LightManager(IGraphicsDeviceService graphicsdevicemanager, IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
		fN_0019 = graphicsdevicemanager;
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public override void ApplyPreferences(ILightingSystemPreferences preferences)
	{
		fNn = preferences.LightingDetail;
		base.ApplyPreferences(preferences);
	}

	private void fS()
	{
		_0013.B.nu(ref fNf);
		_0013.B.nu(ref fN_0003);
	}

	/// <summary>
	/// Renders volume lighting for the contained lights.
	/// </summary>
	public void RenderVolumeLights()
	{
		if (fNn != DetailPreference.High)
		{
			return;
		}
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		if (fNf == null)
		{
			fNf = new j._0012(graphicsDevice);
			fN_0003 = new d.a(graphicsDevice, fNa);
		}
		graphicsDevice.BlendState = R.B.fNB;
		graphicsDevice.RasterizerState = RasterizerState.CullNone;
		graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
		graphicsDevice.SetVertexBuffer(fN_0003.VertexBuffer);
		h._0012._0003T(graphicsDevice);
		fNf.View = SceneState.View;
		fNf.Projection = SceneState.Projection;
		fNf.SetDepthRange(SceneState.ProjectionToView);
		FrameBuffers frameBuffers = SceneState.FrameBuffers;
		RenderTarget2D buffer = frameBuffers.GetBuffer(FrameBufferType.DeferredDepthAndSpecularPower, createmissing: false);
		if (buffer != null)
		{
			fNf.SceneDepthMap = buffer;
		}
		else
		{
			fNf.SceneDepthMap = null;
		}
		EffectPass effectPass = fNf.CurrentTechnique.Passes[0];
		fNv.Clear();
		Find(fNv, SceneState.ViewFrustum, ObjectFilter.EnabledDynamicAndStatic);
		foreach (BaseLight item in fNv)
		{
			if (item == null)
			{
				continue;
			}
			L.o o2 = OptimizationSystem.fNB.g_0011(item);
			if (o2.fNa == null)
			{
				continue;
			}
			ISpotSource spotSource = o2.fNa;
			if (!(spotSource.Volume <= 0f))
			{
				if (!fNg)
				{
					fNf.World = item.World;
				}
				else
				{
					Matrix matrix = Matrix.Invert(item.World);
					Vector3 vector = Vector3.Transform(SceneState.ViewToWorld.Translation, matrix);
					Matrix world = fI(Vector3.Zero, vector, Vector3.Backward) * Matrix.CreateRotationZ(MathHelper.ToRadians(45f)) * item.World;
					fNf.World = world;
				}
				fNf.Color = item.CompositeColorAndIntensity / fNa * spotSource.Volume;
				fNf.SetAngleAndRadius(spotSource.Angle, spotSource.Radius);
				effectPass.Apply();
				fN_0003._5();
			}
		}
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
	}

	private Matrix fI(Vector3 P_0, Vector3 P_1, Vector3 P_2)
	{
		Matrix result = default;
		Vector3 vector = new Vector3
		{
			X = P_0.X - P_1.X,
			Y = P_0.Y - P_1.Y,
			Z = P_0.Z - P_1.Z
		};
		float num = vector.LengthSquared();
		if (num < 0.0001f)
		{
			vector = Vector3.Forward;
		}
		else
		{
			Vector3.Multiply(ref vector, 1f / (float)Math.Sqrt(num), out vector);
		}
		Vector3 vector2 = P_2;
		Vector3.Dot(ref P_2, ref vector, out var result2);
		Vector3 result3;
		Vector3 result4;
		if (Math.Abs(result2) > 0.9982547f)
		{
			result2 = P_2.X * Vector3.Forward.X + P_2.Y * Vector3.Forward.Y + P_2.Z * Vector3.Forward.Z;
			result3 = ((Math.Abs(result2) > 0.9982547f) ? Vector3.Right : Vector3.Forward);
			Vector3.Cross(ref P_2, ref result3, out result4);
			result4.Normalize();
			Vector3.Cross(ref result4, ref P_2, out result3);
			result3.Normalize();
		}
		else
		{
			Vector3.Cross(ref P_2, ref vector, out result4);
			result4.Normalize();
			Vector3.Cross(ref result4, ref vector2, out result3);
			result3.Normalize();
		}
		result.M11 = result4.X;
		result.M12 = result4.Y;
		result.M13 = result4.Z;
		result.M14 = 0f;
		result.M21 = result3.X;
		result.M22 = result3.Y;
		result.M23 = result3.Z;
		result.M24 = 0f;
		result.M31 = vector2.X;
		result.M32 = vector2.Y;
		result.M33 = vector2.Z;
		result.M34 = 0f;
		result.M41 = P_0.X;
		result.M42 = P_0.Y;
		result.M43 = P_0.Z;
		result.M44 = 1f;
		return result;
	}

	/// <summary>
	/// Helper method that creates and submits a static light
	/// using a method layout similar to SunBurn 1.2.x.
	/// </summary>
	/// <param name="diffusecolor">Direct lighting color given off by the light.</param>
	/// <param name="direction">Direction the light is pointing.</param>
	/// <param name="intensity">Intensity of the light.</param>
	/// <param name="shadowtype">Defines the type of objects that cast shadows from the light.
	/// Does not affect an object's ability to receive shadows.</param>
	/// <param name="shadowquality">Visual quality of casts shadows.</param>
	/// <param name="shadowprimarybias">Main property used to eliminate shadow artifacts.</param>
	/// <param name="shadowsecondarybias">Additional fine-tuned property used to eliminate shadow artifacts.</param>
	public void SubmitStaticDirectionalLight(Vector3 diffusecolor, Vector3 direction, float intensity, ShadowType shadowtype, float shadowquality, float shadowprimarybias, float shadowsecondarybias)
	{
		DirectionalLight directionalLight = new DirectionalLight();
		directionalLight.DiffuseColor = diffusecolor;
		directionalLight.Intensity = intensity;
		directionalLight.Direction = direction;
		directionalLight.ShadowType = shadowtype;
		directionalLight.ShadowQuality = shadowquality;
		directionalLight.ShadowPrimaryBias = shadowprimarybias;
		directionalLight.ShadowSecondaryBias = shadowsecondarybias;
		Submit(directionalLight);
	}

	/// <summary>
	/// Helper method that creates and submits a static light
	/// using a method layout similar to SunBurn 1.2.x.
	/// </summary>
	/// <param name="diffusecolor">Direct lighting color given off by the light.</param>
	/// <param name="position">Position in world space of the light.</param>
	/// <param name="intensity">Intensity of the light.</param>
	/// <param name="radius">Lighting radius in world space.</param>
	/// <param name="filllight">Provides softer indirect-like illumination without "hot-spots".</param>
	/// <param name="falloffstrength">Controls how quickly lighting falls off over distance (only available in deferred rendering).</param>
	/// <param name="shadowtype">Defines the type of objects that cast shadows from the light.
	/// Does not affect an object's ability to receive shadows.</param>
	/// <param name="shadowquality">Visual quality of casts shadows.</param>
	/// <param name="shadowprimarybias">Main property used to eliminate shadow artifacts.</param>
	/// <param name="shadowsecondarybias">Additional fine-tuned property used to eliminate shadow artifacts.</param>
	/// <param name="shadowsource">Shadow source the light's shadows are generated from.
	/// Allows sharing shadows between point light sources.  Setting the parameter
	/// to null makes the light its own unique shadow source.</param>
	public void SubmitStaticPointLight(Vector3 diffusecolor, Vector3 position, float intensity, float radius, bool filllight, float falloffstrength, ShadowType shadowtype, float shadowquality, float shadowprimarybias, float shadowsecondarybias, IShadowSource shadowsource)
	{
		PointLight pointLight = new PointLight();
		pointLight.DiffuseColor = diffusecolor;
		pointLight.Position = position;
		pointLight.Intensity = intensity;
		pointLight.Radius = radius;
		pointLight.FillLight = filllight;
		pointLight.FalloffStrength = falloffstrength;
		pointLight.ShadowType = shadowtype;
		pointLight.ShadowQuality = shadowquality;
		pointLight.ShadowPrimaryBias = shadowprimarybias;
		pointLight.ShadowSecondaryBias = shadowsecondarybias;
		if (shadowsource != null)
		{
			pointLight.ShadowSource = shadowsource;
		}
		else
		{
			pointLight.ShadowSource = pointLight;
		}
		Submit(pointLight);
	}

	/// <summary>
	/// Helper method that creates and submits a static light
	/// using a method layout similar to SunBurn 1.2.x.
	/// </summary>
	/// <param name="diffusecolor">Direct lighting color given off by the light.</param>
	/// <param name="position">Position in world space of the light.</param>
	/// <param name="intensity">Intensity of the light.</param>
	/// <param name="radius">Lighting radius in world space.</param>
	/// <param name="direction">Direction the light is pointing.</param>
	/// <param name="angle">Angle in degrees of the light's influence.</param>
	/// <param name="filllight">Provides softer indirect-like illumination without "hot-spots".</param>
	/// <param name="falloffstrength">Controls how quickly lighting falls off over distance (only available in deferred rendering).</param>
	/// <param name="shadowtype">Defines the type of objects that cast shadows from the light.
	/// Does not affect an object's ability to receive shadows.</param>
	/// <param name="shadowquality">Visual quality of casts shadows.</param>
	/// <param name="shadowprimarybias">Main property used to eliminate shadow artifacts.</param>
	/// <param name="shadowsecondarybias">Additional fine-tuned property used to eliminate shadow artifacts.</param>
	/// <param name="shadowsource">Shadow source the light's shadows are generated from.
	/// Allows sharing shadows between point light sources.  Setting the parameter
	/// to null makes the light its own unique shadow source.</param>
	public void SubmitStaticSpotLight(Vector3 diffusecolor, Vector3 position, float intensity, float radius, Vector3 direction, float angle, bool filllight, float falloffstrength, ShadowType shadowtype, float shadowquality, float shadowprimarybias, float shadowsecondarybias, IShadowSource shadowsource)
	{
		SpotLight spotLight = new SpotLight();
		spotLight.DiffuseColor = diffusecolor;
		spotLight.Position = position;
		spotLight.Intensity = intensity;
		spotLight.Radius = radius;
		spotLight.Direction = direction;
		spotLight.Angle = angle;
		spotLight.FillLight = filllight;
		spotLight.FalloffStrength = falloffstrength;
		spotLight.ShadowType = shadowtype;
		spotLight.ShadowQuality = shadowquality;
		spotLight.ShadowPrimaryBias = shadowprimarybias;
		spotLight.ShadowSecondaryBias = shadowsecondarybias;
		if (shadowsource != null)
		{
			spotLight.ShadowSource = shadowsource;
		}
		else
		{
			spotLight.ShadowSource = spotLight;
		}
		Submit(spotLight);
	}

	/// <summary>
	/// Helper method that creates and submits a static light
	/// using a method layout similar to SunBurn 1.2.x.
	/// </summary>
	/// <param name="diffusecolor">Direct lighting color given off by the light.</param>
	/// <param name="intensity">Intensity of the light.</param>
	public void SubmitStaticAmbientLight(Vector3 diffusecolor, float intensity)
	{
		AmbientLight ambientLight = new AmbientLight();
		ambientLight.DiffuseColor = diffusecolor;
		ambientLight.Intensity = intensity;
		Submit(ambientLight);
	}

	/// <summary>
	/// Adds an object to the container. This does not transfer ownership, disposable
	/// objects should be maintained and disposed separately.
	/// </summary>
	/// <param name="scene"></param>
	public virtual void Submit(IScene scene)
	{
		fNL.Add(scene);
		scene.SetContainingManager<ILightManager>(this);
		scene.Apply();
	}

	/// <summary>
	/// Moves an object within the container. This method is used when the container
	/// implements a tree or graph, and relocates an object within that structure
	/// often due to a change in object world position.
	/// </summary>
	/// <param name="scene"></param>
	public virtual void Move(IScene scene)
	{
	}

	/// <summary>
	/// Removes an object from the container.
	/// </summary>
	/// <param name="scene"></param>
	public virtual void Remove(IScene scene)
	{
		if (fNL.Remove(scene))
		{
			scene.SetContainingManager<ILightManager>(null);
		}
	}

	/// <summary>
	/// Removes all lights and cleans up scene information.
	/// </summary>
	public override void Clear()
	{
		while (fNL.Count > 0)
		{
			Remove(fNL[0]);
		}
		fNL.Clear();
		base.Clear();
		OptimizationSystem.Clear();
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public override void Unload()
	{
		fS();
		base.Unload();
	}
}
