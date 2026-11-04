using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;
using f;

namespace SynapseGaming.LightingSystem.Audio;

/// <summary>
/// Manages all scene audio emitters and allows querying the scene with
/// a view or bounding box for audio emitters that affect the area
/// (acts as an audio emitters scenegraph).
/// </summary>
public class AudioManager : ObjectGraph<AudioSource>, IAudioManager, IUpdatableManager, IManagerService, IQuery<AudioSource>, ISubmit<AudioSource>, ISubmit<IScene>, IWorldRenderableManager, IRenderableManager, IManager, IUnloadable
{
	private struct nNB
	{
		internal class WeightComparer : IComparer<AudioManager.nNB>
		{
			public int Compare(AudioManager.nNB x, AudioManager.nNB y)
			{
				return y.Weight.CompareTo(x.Weight);
			}
		}

		public AudioSource AudioSource;

		public float Weight;
	}

	private int fNB = 100;

	private int fNa = 200;

	private ISceneState fNg;

	private Matrix fNf = Matrix.Identity;

	private BoundingFrustum fN_0003 = new BoundingFrustum(Matrix.Identity);

	private IGraphicsDeviceService fNn;

	private List<IScene> fN_0019 = new List<IScene>(16);

	private f.B fNL = new f.B();

	private List<AudioSource> fNv = new List<AudioSource>();

	private List<nNB> fN_0012 = new List<nNB>();

	private static nNB.WeightComparer fN_0015 = new nNB.WeightComparer();

	/// <summary>
	/// Determines which type this manager is registered under in the
	/// SceneInterface that contains it.
	///
	/// Please note: changing the return value to the ManagerType of
	/// another class will allow this manager to replace it in the
	/// SceneInterface (and provide replacement features and implementation).
	/// </summary>
	public Type ManagerType => SceneInterface.AudioManagerType;

	/// <summary>
	/// Sets the order this manager is processed relative to other managers
	/// in the SceneInterface. Managers with lower processing order
	/// values are processed first.
	///
	/// In the case of BeginFrameRendering and EndFrameRendering, BeginFrameRendering
	/// is processed in the normal order (lowest value to highest), however
	/// EndFrameRendering is processed in reverse order (highest to lowest) to ensure
	/// the first manager begun is the last one ended (FILO).
	///
	/// For managers that do not require a specific order a value of 100 is recommended.
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
	/// The GraphicsDeviceManager used by this manager.
	/// </summary>
	public IGraphicsDeviceService GraphicsDeviceManager => fNn;

	/// <summary>
	/// Maximum number of audio sources that can be played simultaneously.
	/// The number is limited on Xbox to 300, and on WP7 to 64.
	/// </summary>
	public int MaximumAudioSources
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
			if (fNa > 300)
			{
				fNa = 300;
			}
		}
	}

	/// <summary>
	/// Creates a new AudioManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	public AudioManager(IGraphicsDeviceService graphicsdevicemanager)
	{
		fNn = graphicsdevicemanager;
	}

	/// <summary>
	/// Called during Game.Update() to allow processing at regular intervals.
	/// </summary>
	/// <param name="gametime"></param>
	public override void Update(GameTime gametime)
	{
		fNv.Clear();
		fN_0012.Clear();
		Find(fNv, fN_0003, ObjectFilter.All);
		foreach (AudioSource item2 in fNv)
		{
			bool flag = item2.AudioType == AudioType.Point;
			float radius = item2.Radius;
			float num = item2.Volume;
			if (item2.AudioState == AudioState.Playing && item2.SoundEffect != null && !(num <= 0f) && (!flag || !(radius <= 0f)))
			{
				if (flag)
				{
					float num2 = Vector3.DistanceSquared(item2.Position, fNf.Translation);
					num *= 1f - num2 / (radius * radius);
				}
				if (!(num <= 0f))
				{
					nNB item = new nNB
					{
						AudioSource = item2,
						Weight = num
					};
					fN_0012.Add(item);
				}
			}
		}
		if (fN_0012.Count > fNa)
		{
			fN_0012.Sort(fN_0015);
		}
		fNL.fNB = fNa;
		fNL.m(ref fNf);
		int num3 = Math.Min(fN_0012.Count, fNa);
		for (int i = 0; i < num3; i++)
		{
			fNL.h(fN_0012[i].AudioSource);
		}
		fNL.W();
		base.Update(gametime);
	}

	/// <summary>
	/// Called when the game code sets the manager or SceneInterface preferences.
	/// </summary>
	/// <param name="preferences"></param>
	public void ApplyPreferences(ILightingSystemPreferences preferences)
	{
	}

	/// <summary>
	/// Called when the game begins rendering the current frame.
	/// </summary>
	/// <param name="scenestate"></param>
	public void BeginFrameRendering(ISceneState scenestate)
	{
		fNg = scenestate;
		fNf = scenestate.ViewToWorld;
		fN_0003.Matrix = scenestate.ViewFrustum.Matrix;
	}

	/// <summary>
	/// Called when the game finishes rendering the current frame.
	/// </summary>
	public void EndFrameRendering()
	{
		foreach (AudioSource item in fNv)
		{
			item.RenderCustomPass(fNg);
		}
	}

	/// <summary>
	/// Adds an object to the container. This does not transfer ownership, disposable
	/// objects should be maintained and disposed separately.
	/// </summary>
	/// <param name="scene"></param>
	public virtual void Submit(IScene scene)
	{
		fN_0019.Add(scene);
		scene.SetContainingManager<IAudioManager>(this);
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
		if (fN_0019.Remove(scene))
		{
			scene.SetContainingManager<IAudioManager>(null);
		}
	}

	/// <summary>
	/// Removes an object from the container.
	/// </summary>
	/// <param name="obj"></param>
	public override void Remove(AudioSource obj)
	{
		fNL.R(obj);
		base.Remove(obj);
	}

	/// <summary>
	/// Called when the game clears the engine of objects (generally when
	/// clearing the current level / scene and before loading the next one).
	/// </summary>
	public override void Clear()
	{
		fNL._0017();
		while (fN_0019.Count > 0)
		{
			Remove(fN_0019[0]);
		}
		fN_0019.Clear();
		base.Clear();
	}

	/// <summary>
	/// Called when the game's graphics and disposable resources are no longer
	/// used or are invalid (due to exiting the game or the graphics device
	/// resetting).  All resources should be disposed before exiting this method.
	/// </summary>
	public void Unload()
	{
		Clear();
	}
}

