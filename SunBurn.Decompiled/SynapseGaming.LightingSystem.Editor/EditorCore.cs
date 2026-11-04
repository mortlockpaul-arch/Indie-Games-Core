using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Editor;

/// <summary />
public abstract class EditorCore : IRenderableManager, IManager, IUnloadable
{
	/// <summary>
	///
	/// </summary>
	protected GraphicsDeviceManager _GraphicsDeviceManager;

	private float fNB = 10f;

	private float fNa = 10f;

	private float fNg = 1f;

	private bool fNf;

	private Game fN_0003;

	/// <summary>
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	public IGraphicsDeviceService GraphicsDeviceManager => _GraphicsDeviceManager;

	/// <summary>
	/// Adjusts the scale of on screen light icons allowing support for varying scene scales.
	/// </summary>
	public float IconScale
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
	/// Adjusts the speed of in editor camera movement allowing support for varying scene scales.
	/// </summary>
	public float MoveScale
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
	/// Adjusts the speed of in editor camera rotation to user preference.
	/// </summary>
	public float RotationScale
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
	/// Determines if user defined code handles in editor camera movement. If so only object
	/// selection and object movement is processed.
	/// </summary>
	public bool UserHandledView
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
	/// The active Game object.
	/// </summary>
	public Game Game => fN_0003;

	/// <summary />
	public abstract void BeginFrameRendering(ISceneState scenestate);

	/// <summary />
	public abstract void EndFrameRendering();

	/// <summary />
	public abstract void ApplyPreferences(ILightingSystemPreferences preferences);

	internal EditorCore(GraphicsDeviceManager P_0, Game P_1)
	{
		_GraphicsDeviceManager = P_0;
		fN_0003 = P_1;
	}

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	protected void BeginFrameRendering(ISceneState scenestate, bool isconnected)
	{
	}

	/// <summary>
	/// Removes resources managed by this object. Commonly used while clearing the scene.
	/// </summary>
	public void Clear()
	{
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public virtual void Unload()
	{
	}
}
