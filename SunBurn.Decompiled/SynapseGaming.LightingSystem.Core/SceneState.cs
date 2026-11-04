using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Provides scene, frame, and view specific data to the the lighting system.
/// </summary>
public class SceneState : ISceneState
{
	private FrameBuffers fNB;

	private Matrix fNa = Matrix.Identity;

	private Matrix fNg = Matrix.Identity;

	private Matrix fNf = Matrix.Identity;

	private Matrix fN_0003 = Matrix.Identity;

	private Matrix fNn = Matrix.Identity;

	private Matrix fN_0019 = Matrix.Identity;

	private Matrix fNL = Matrix.Identity;

	private BoundingFrustum fNv = new BoundingFrustum(Matrix.Identity);

	private bool fN_0012 = true;

	private bool fN_0015;

	private bool fN8;

	private GameTime fNi = new GameTime();

	private int fN_0005;

	private ISceneEnvironment fNj = fNo;

	private static ISceneEnvironment fNo = new SceneEnvironment();

	private static ISceneEnvironment fNc = new SceneEnvironment();

	/// <summary>
	/// The scene's current view matrix.
	/// </summary>
	public Matrix View => fNa;

	/// <summary>
	/// The scene's inverse view matrix.
	/// </summary>
	public Matrix ViewToWorld => fNg;

	/// <summary>
	/// The scene's current projection matrix.
	/// </summary>
	public Matrix Projection => fNf;

	/// <summary>
	/// Non-oblique copy of the scene's projection matrix. If the projection matrix is already non-oblique
	/// both ProjectionNonOblique and Projection are equal.
	/// </summary>
	public Matrix ProjectionNonOblique => fN_0003;

	/// <summary>
	/// The scene's inverse projection matrix.
	/// </summary>
	public Matrix ProjectionToView => fNn;

	/// <summary>
	/// The scene's combined view and projection matrix.
	/// </summary>
	public Matrix ViewProjection => fN_0019;

	/// <summary>
	/// The scene's combined inverse view and inverse projection matrix.
	/// </summary>
	public Matrix ProjectionToWorld => fNL;

	/// <summary>
	/// The scene's current view frustum.
	/// </summary>
	public BoundingFrustum ViewFrustum => fNv;

	/// <summary>
	/// Indicates the rendering pass is drawing to the screen (or to a
	/// target copied to the screen).
	/// </summary>
	public bool RenderingToScreen => fN_0012;

	/// <summary>
	/// Determines if primitive culling mode should be flipped to accommodate
	/// inverted windings caused by mirrored view or projection transforms.
	/// </summary>
	public bool InvertedWindings => fN_0015;

	/// <summary>
	/// Indicates the projection is 2D.
	/// </summary>
	public bool OrthographicProjection => fN8;

	/// <summary>
	/// The scene's current game time.
	/// </summary>
	public GameTime GameTime => fNi;

	/// <summary>
	/// The current frame id.
	/// </summary>
	public int FrameId => fN_0005;

	/// <summary>
	/// The scene's current environment.
	/// </summary>
	public ISceneEnvironment Environment => fNj;

	/// <summary>
	/// Shared buffers used to render the scene.
	/// </summary>
	public FrameBuffers FrameBuffers => fNB;

	/// <summary>
	/// Sets up the scene state prior to 2D rendering.
	/// </summary>
	/// <param name="viewposition">World space position of the 2D camera.</param>
	/// <param name="viewwidth">Number of world space units visible across the
	/// width of the viewport.</param>
	/// <param name="aspectratio">Aspect ratio of the viewport.</param>
	/// <param name="gametime">Current game time.</param>
	/// <param name="environment">Environment object used while rendering.</param>
	/// <param name="framebuffers">Shared buffers used to render the scene.</param>
	/// <param name="renderingtoscreen">Indicates the rendering pass is drawing
	/// to the screen (or to a target copied to the screen).</param>
	public void BeginFrameRendering(Vector2 viewposition, float viewwidth, float aspectratio, GameTime gametime, ISceneEnvironment environment, FrameBuffers framebuffers, bool renderingtoscreen)
	{
		float num = viewwidth * 2.5f;
		float num2 = num * 10f;
		Matrix view = Matrix.CreateLookAt(new Vector3(viewposition, 0f - num), new Vector3(viewposition, 0f), Vector3.Up);
		Matrix projection = Matrix.CreatePerspective(viewwidth, viewwidth / aspectratio, num, num2);
		ISceneEnvironment sceneEnvironment = ((environment == null) ? fNc : environment);
		sceneEnvironment.ShadowCasterDistance = num2;
		sceneEnvironment.ShadowFadeEndDistance = num2;
		sceneEnvironment.ShadowFadeStartDistance = num2;
		sceneEnvironment.VisibleDistance = num2;
		BeginFrameRendering(view, projection, gametime, sceneEnvironment, framebuffers, renderingtoscreen);
		fN8 = true;
	}

	/// <summary>
	/// Sets up the scene state prior to 3D rendering. Includes support for oblique projection.
	/// </summary>
	/// <param name="view">Camera view matrix.</param>
	/// <param name="projection">Camera projection matrix. Must be a non-oblique version of the projection matrix.</param>
	/// <param name="projectionoblique">Camera projection matrix.</param>
	/// <param name="gametime">Current game time.</param>
	/// <param name="environment">Environment object used while rendering.</param>
	/// <param name="framebuffers">Shared buffers used to render the scene.</param>
	/// <param name="renderingtoscreen">Indicates the rendering pass is drawing
	/// to the screen (or to a target copied to the screen).</param>
	public void BeginFrameRendering(Matrix view, Matrix projection, Matrix projectionoblique, GameTime gametime, ISceneEnvironment environment, FrameBuffers framebuffers, bool renderingtoscreen)
	{
		SplashScreen._0003k();
		fNa = view;
		fNg = Matrix.Invert(view);
		fNf = projectionoblique;
		fNn = Matrix.Invert(projectionoblique);
		fN_0003 = projection;
		fN_0019 = view * projectionoblique;
		fNL = fNn * fNg;
		fNi = gametime;
		fN_0012 = renderingtoscreen;
		fN_0015 = fN_0019.Determinant() >= 0f;
		fN8 = false;
		fNB = framebuffers;
		fNB.BeginFrameRendering(this);
		if (environment != null)
		{
			fNj = environment;
		}
		else
		{
			fNj = fNo;
		}
		fNv.Matrix = view * projection;
		fN_0005++;
	}

	/// <summary>
	/// Sets up the scene state prior to 3D rendering.
	/// </summary>
	/// <param name="view">Camera view matrix.</param>
	/// <param name="projection">Camera projection matrix. Must be a non-oblique version of the projection matrix.</param>
	/// <param name="gametime">Current game time.</param>
	/// <param name="environment">Environment object used while rendering.</param>
	/// <param name="framebuffers">Shared buffers used to render the scene.</param>
	/// <param name="renderingtoscreen">Indicates the rendering pass is drawing
	/// to the screen (or to a target copied to the screen).</param>
	public void BeginFrameRendering(Matrix view, Matrix projection, GameTime gametime, ISceneEnvironment environment, FrameBuffers framebuffers, bool renderingtoscreen)
	{
		BeginFrameRendering(view, projection, projection, gametime, environment, framebuffers, renderingtoscreen);
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public void EndFrameRendering()
	{
		fNB.EndFrameRendering();
	}

	/// <summary />
	public void ApplyEditorUpdate(Matrix view, Matrix viewtoworld, Matrix projection)
	{
		BeginFrameRendering(view, projection, fNi, fNj, fNB, fN_0012);
	}
}
