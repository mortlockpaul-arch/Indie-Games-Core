using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Core;

public class SceneState : ISceneState
{
	private FrameBuffers l8a;

	private Matrix l8_u0013_ = Matrix.Identity;

	private Matrix l8N = Matrix.Identity;

	private Matrix l8l = Matrix.Identity;

	private Matrix l8L = Matrix.Identity;

	private Matrix l8w = Matrix.Identity;

	private Matrix l8J = Matrix.Identity;

	private Matrix l8B = Matrix.Identity;

	private BoundingFrustum l8s = new BoundingFrustum(Matrix.Identity);

	private bool l8h = true;

	private bool l8_u0002_;

	private bool l8v;

	private GameTime l8Y = new GameTime();

	private int l8_u0005_;

	private ISceneEnvironment l8b = l8I;

	private static ISceneEnvironment l8I = new SceneEnvironment();

	private static ISceneEnvironment l85 = new SceneEnvironment();

	public Matrix View => l8_u0013_;

	public Matrix ViewToWorld => l8N;

	public Matrix Projection => l8l;

	public Matrix ProjectionNonOblique => l8L;

	public Matrix ProjectionToView => l8w;

	public Matrix ViewProjection => l8J;

	public Matrix ProjectionToWorld => l8B;

	public BoundingFrustum ViewFrustum => l8s;

	public bool RenderingToScreen => l8h;

	public bool InvertedWindings => l8_u0002_;

	public bool OrthographicProjection => l8v;

	public GameTime GameTime => l8Y;

	public int FrameId => l8_u0005_;

	public ISceneEnvironment Environment => l8b;

	public FrameBuffers FrameBuffers => l8a;

	public void BeginFrameRendering(Vector2 viewposition, float viewwidth, float aspectratio, GameTime gametime, ISceneEnvironment environment, FrameBuffers framebuffers, bool renderingtoscreen)
	{
		float num = viewwidth * 2.5f;
		float num2 = num * 10f;
		Matrix view = Matrix.CreateLookAt(new Vector3(viewposition, 0f - num), new Vector3(viewposition, 0f), Vector3.Up);
		Matrix projection = Matrix.CreatePerspective(viewwidth, viewwidth / aspectratio, num, num2);
		ISceneEnvironment sceneEnvironment = ((environment == null) ? l85 : environment);
		sceneEnvironment.ShadowCasterDistance = num2;
		sceneEnvironment.ShadowFadeEndDistance = num2;
		sceneEnvironment.ShadowFadeStartDistance = num2;
		sceneEnvironment.VisibleDistance = num2;
		BeginFrameRendering(view, projection, gametime, sceneEnvironment, framebuffers, renderingtoscreen);
		l8v = true;
	}

	public void BeginFrameRendering(Matrix view, Matrix projection, Matrix projectionoblique, GameTime gametime, ISceneEnvironment environment, FrameBuffers framebuffers, bool renderingtoscreen)
	{
		SplashScreen.L_u0015_();
		l8_u0013_ = view;
		l8N = Matrix.Invert(view);
		l8l = projectionoblique;
		l8w = Matrix.Invert(projectionoblique);
		l8L = projection;
		l8J = view * projectionoblique;
		l8B = l8w * l8N;
		l8Y = gametime;
		l8h = renderingtoscreen;
		l8_u0002_ = l8J.Determinant() >= 0f;
		l8v = false;
		l8a = framebuffers;
		l8a.BeginFrameRendering(this);
		if (environment != null)
		{
			l8b = environment;
		}
		else
		{
			l8b = l8I;
		}
		l8s.Matrix = view * projection;
		l8_u0005_++;
	}

	public void BeginFrameRendering(Matrix view, Matrix projection, GameTime gametime, ISceneEnvironment environment, FrameBuffers framebuffers, bool renderingtoscreen)
	{
		BeginFrameRendering(view, projection, projection, gametime, environment, framebuffers, renderingtoscreen);
	}

	public void EndFrameRendering()
	{
		l8a.EndFrameRendering();
	}

	public void ApplyEditorUpdate(Matrix view, Matrix viewtoworld, Matrix projection)
	{
		BeginFrameRendering(view, projection, l8Y, l8b, l8a, l8h);
	}
}
