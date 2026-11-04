using System;
using _0013;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Helper class for rendering to a texture. Provides automatic support for rendering
/// reflection and refraction textures, as well as standard render-to-texture.
/// </summary>
public class RenderTargetHelper : IRenderableManager, IManager, IUnloadable
{
	/// <summary>
	/// Type of rendering to perform on the render target.
	/// </summary>
	public enum TargetType
	{
		/// <summary>
		/// Automatically generates a reflection image based on the current view and reflection plane.
		/// </summary>
		Reflection,
		/// <summary>
		/// Automatically generates a refraction image based on the current view and reflection plane.
		/// </summary>
		Refraction,
		/// <summary>
		/// Renders to texture normally based on the current view.
		/// </summary>
		Standard
	}

	private IGraphicsDeviceService fNB;

	private int fNa;

	private int fNg;

	private bool fNf;

	private SurfaceFormat fN_0003;

	private int fNn;

	private RenderTargetUsage fN_0019;

	private SceneState fNL = new SceneState();

	private TargetType fNv = TargetType.Standard;

	private RenderTarget2D fN_0012;

	private Viewport fN_0015 = default;

	private ILightingSystemPreferences fN8 = new LightingSystemPreferences();

	private Plane fNi = default;

	private RenderTargetBinding[] fN_0005;

	private Viewport fNj;

	private RenderTargetBinding[] fNo = new RenderTargetBinding[1];

	/// <summary>
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	public IGraphicsDeviceService GraphicsDeviceManager => fNB;

	/// <summary>
	/// Scene rendering state used to render objects to this RenderTargetHelper. The state values
	/// may be different from those passed into BeginFrameRendering to accommodate reflection and refraction.
	/// </summary>
	public ISceneState SceneState => fNL;

	/// <summary>
	/// Rendering preferences used to render objects to this RenderTargetHelper.
	/// </summary>
	public ILightingSystemPreferences Preferences => fN8;

	/// <summary>
	/// Creates a new RenderTargetHelper instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="type">Type of rendering to perform on the render target.</param>
	/// <param name="width">Render target width.</param>
	/// <param name="height">Render target height.</param>
	/// <param name="format">Render target format.</param>
	public RenderTargetHelper(IGraphicsDeviceService graphicsdevicemanager, TargetType type, int width, int height, SurfaceFormat format)
	{
		fNB = graphicsdevicemanager;
		fNv = type;
		fNa = width;
		fNg = height;
		fN_0003 = format;
		fNf = false;
		fNn = 0;
		fN_0019 = LightingSystemManager.Instance.GetBestRenderTargetUsage();
	}

	/// <summary>
	/// Creates a new RenderTargetHelper instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="type">Type of rendering to perform on the render target.</param>
	/// <param name="width">Render target width.</param>
	/// <param name="height">Render target height.</param>
	/// <param name="mipmapped">Determines if the render target generates mipmaps.</param>
	/// <param name="format">Render target format.</param>
	/// <param name="multisamplecount">Render target multisample quality.</param>
	/// <param name="usage">Render target usage.</param>
	public RenderTargetHelper(IGraphicsDeviceService graphicsdevicemanager, TargetType type, int width, int height, bool mipmapped, SurfaceFormat format, int multisamplecount, RenderTargetUsage usage)
	{
		fNB = graphicsdevicemanager;
		fNv = type;
		fNa = width;
		fNg = height;
		fNf = mipmapped;
		fN_0003 = format;
		fNn = multisamplecount;
		fN_0019 = usage;
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public void ApplyPreferences(ILightingSystemPreferences preferences)
	{
		fN8 = preferences;
	}

	/// <summary>
	/// Removes resources managed by this object. Commonly used while clearing the scene.
	/// </summary>
	public void Clear()
	{
		fN8 = new LightingSystemPreferences();
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public void Unload()
	{
		Clear();
		_0013.B.nu(ref fN_0012);
	}

	/// <summary>
	/// Gets the texture containing the resulting rendered image.
	/// </summary>
	/// <returns></returns>
	public Texture2D GetTexture()
	{
		return fN_0012;
	}

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	public void BeginFrameRendering(ISceneState scenestate)
	{
		if (fNv != TargetType.Standard)
		{
			throw new Exception("Non standard targets require a world reflection plane, please use another overload for this method.");
		}
		BeginFrameRendering(scenestate, fNi);
	}

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	/// <param name="worldreflectionplane">World space plane used as the reflection surface.</param>
	public void BeginFrameRendering(ISceneState scenestate, Plane worldreflectionplane)
	{
		GraphicsDevice graphicsDevice = fNB.GraphicsDevice;
		if (fN_0012 == null)
		{
			fN_0012 = new RenderTarget2D(graphicsDevice, fNa, fNg, fNf, fN_0003, DepthFormat.Depth24Stencil8, fNn, fN_0019);
			fN_0015.X = 0;
			fN_0015.Y = 0;
			fN_0015.Width = fNa;
			fN_0015.Height = fNg;
			fN_0015.MinDepth = 0f;
			fN_0015.MaxDepth = 1f;
		}
		fN_0005 = graphicsDevice.GetRenderTargets();
		fNj = graphicsDevice.Viewport;
		ref RenderTargetBinding reference = ref fNo[0];
		reference = new RenderTargetBinding(fN_0012);
		graphicsDevice.SetRenderTargets(fNo);
		graphicsDevice.Viewport = fN_0015;
		if (fNv != TargetType.Reflection)
		{
			Matrix projection = scenestate.Projection;
			Matrix projectionoblique = projection;
			if (fNv != TargetType.Standard)
			{
				projectionoblique = gf(projection, scenestate.ProjectionToWorld, worldreflectionplane);
			}
			fNL.BeginFrameRendering(scenestate.View, projection, projectionoblique, scenestate.GameTime, scenestate.Environment, scenestate.FrameBuffers, scenestate.RenderingToScreen);
		}
		else
		{
			Matrix matrix = Matrix.CreateReflection(worldreflectionplane) * scenestate.View;
			Matrix projection2 = scenestate.Projection;
			Matrix matrix2 = Matrix.Invert(matrix * projection2);
			Matrix projectionoblique2 = gf(projection2, matrix2, worldreflectionplane);
			fNL.BeginFrameRendering(matrix, projection2, projectionoblique2, scenestate.GameTime, scenestate.Environment, scenestate.FrameBuffers, scenestate.RenderingToScreen);
		}
	}

	private Matrix gf(Matrix P_0, Matrix P_1, Plane P_2)
	{
		Matrix.Transpose(ref P_1, out var result);
		Vector4 vector = new Vector4(P_2.Normal, P_2.D);
		Vector4.Transform(ref vector, ref result, out var result2);
		if (result2.W == 0f)
		{
			return P_0;
		}
		if (result2.W > 0f)
		{
			result2 = Vector4.Transform(-vector, result);
		}
		Matrix identity = Matrix.Identity;
		float num = Vector4.Dot(vector2: new Vector4(Math.Sign(result2.X), Math.Sign(result2.Y), 1f, 1f), vector1: result2);
		if (num == 0f)
		{
			return P_0;
		}
		result2 *= 1f / num;
		identity.M13 = result2.X;
		identity.M23 = result2.Y;
		identity.M33 = result2.Z;
		identity.M43 = result2.W;
		return P_0 * identity;
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public void EndFrameRendering()
	{
		GraphicsDevice graphicsDevice = fNB.GraphicsDevice;
		graphicsDevice.SetRenderTargets(fN_0005);
		graphicsDevice.Viewport = fNj;
	}
}
