using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using y;

namespace SynapseGaming.LightingSystem.Core;

public class RenderTargetHelper : IUnloadable
{
	public enum TargetType
	{
		Reflection,
		Refraction,
		Standard
	}

	private int l8a;

	private int l8_u0013_;

	private bool l8N;

	private SurfaceFormat l8l;

	private int l8L;

	private RenderTargetUsage l8w;

	private SceneState l8J = new SceneState();

	private TargetType l8B = TargetType.Standard;

	private RenderTarget2D l8s;

	private Viewport l8h = default;

	private ISystemPreferences l8_u0002_ = new SystemPreferences();

	private Plane l8v = default;

	private RenderTargetBinding[] l8Y;

	private Viewport l8_u0005_;

	private RenderTargetBinding[] l8b = new RenderTargetBinding[1];

	public ISceneState SceneState => l8J;

	public ISystemPreferences Preferences => l8_u0002_;

	public RenderTargetHelper(TargetType type, int width, int height, SurfaceFormat format)
	{
		l8B = type;
		l8a = width;
		l8_u0013_ = height;
		l8l = format;
		l8N = false;
		l8L = 0;
		l8w = SunBurnCoreSystem.Instance.GetBestRenderTargetUsage();
	}

	public RenderTargetHelper(TargetType type, int width, int height, bool mipmapped, SurfaceFormat format, int multisamplecount, RenderTargetUsage usage)
	{
		l8B = type;
		l8a = width;
		l8_u0013_ = height;
		l8N = mipmapped;
		l8l = format;
		l8L = multisamplecount;
		l8w = usage;
	}

	public void ApplyPreferences(ISystemPreferences preferences)
	{
		l8_u0002_ = preferences;
	}

	public void Clear()
	{
		l8_u0002_ = new SystemPreferences();
	}

	public void Unload()
	{
		Clear();
		y.a.w_u0005_(ref l8s);
	}

	public Texture2D GetTexture()
	{
		return l8s;
	}

	public void BeginFrameRendering(ISceneState scenestate)
	{
		if (l8B != TargetType.Standard)
		{
			throw new Exception("Non standard targets require a world reflection plane, please use another overload for this method.");
		}
		BeginFrameRendering(scenestate, l8v);
	}

	public void BeginFrameRendering(ISceneState scenestate, Plane worldreflectionplane)
	{
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		if (l8s == null)
		{
			l8s = new RenderTarget2D(graphicsDevice, l8a, l8_u0013_, l8N, l8l, DepthFormat.Depth24Stencil8, l8L, l8w);
			l8h.X = 0;
			l8h.Y = 0;
			l8h.Width = l8a;
			l8h.Height = l8_u0013_;
			l8h.MinDepth = 0f;
			l8h.MaxDepth = 1f;
		}
		l8Y = graphicsDevice.GetRenderTargets();
		l8_u0005_ = graphicsDevice.Viewport;
		ref RenderTargetBinding reference = ref l8b[0];
		reference = new RenderTargetBinding(l8s);
		graphicsDevice.SetRenderTargets(l8b);
		graphicsDevice.Viewport = l8h;
		if (l8B != TargetType.Reflection)
		{
			Matrix projection = scenestate.Projection;
			Matrix projectionoblique = projection;
			if (l8B != TargetType.Standard)
			{
				projectionoblique = _u0013_x(projection, scenestate.ProjectionToWorld, worldreflectionplane);
			}
			l8J.BeginFrameRendering(scenestate.View, projection, projectionoblique, scenestate.GameTime, scenestate.Environment, scenestate.FrameBuffers, scenestate.RenderingToScreen);
		}
		else
		{
			Matrix matrix = Matrix.CreateReflection(worldreflectionplane) * scenestate.View;
			Matrix projection2 = scenestate.Projection;
			Matrix param_ = Matrix.Invert(matrix * projection2);
			Matrix projectionoblique2 = _u0013_x(projection2, param_, worldreflectionplane);
			l8J.BeginFrameRendering(matrix, projection2, projectionoblique2, scenestate.GameTime, scenestate.Environment, scenestate.FrameBuffers, scenestate.RenderingToScreen);
		}
	}

	private Matrix _u0013_x(Matrix param_0, Matrix param_1, Plane param_2)
	{
		Matrix.Transpose(ref param_1, out var result);
		Vector4 vector = new Vector4(param_2.Normal, param_2.D);
		Vector4.Transform(ref vector, ref result, out var result2);
		if (result2.W == 0f)
		{
			return param_0;
		}
		if (result2.W > 0f)
		{
			result2 = Vector4.Transform(-vector, result);
		}
		Matrix identity = Matrix.Identity;
		float num = Vector4.Dot(vector2: new Vector4(Math.Sign(result2.X), Math.Sign(result2.Y), 1f, 1f), vector1: result2);
		if (num == 0f)
		{
			return param_0;
		}
		result2 *= 1f / num;
		identity.M13 = result2.X;
		identity.M23 = result2.Y;
		identity.M33 = result2.Z;
		identity.M43 = result2.W;
		return param_0 * identity;
	}

	public void EndFrameRendering()
	{
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		graphicsDevice.SetRenderTargets(l8Y);
		graphicsDevice.Viewport = l8_u0005_;
	}
}
