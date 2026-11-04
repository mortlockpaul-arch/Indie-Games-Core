using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Shadows;

/// <summary>
/// Provides base scene shadow management support.
/// </summary>
public abstract class BaseShadowManager : IRenderableManager, IManager, IUnloadable
{
	private IGraphicsDeviceService fNB;

	private ISceneState fNa = new SceneState();

	private L.i<ShadowGroup> fNg = new L.i<ShadowGroup>();

	private static ShadowSource fNf = new ShadowSource();

	private static SynapseGaming.LightingSystem.Lights.DirectionalLight fN_0003 = new SynapseGaming.LightingSystem.Lights.DirectionalLight();

	private static ShadowGroup fNn = new ShadowGroup();

	private static ShadowGroup fN_0019 = new ShadowGroup();

	private static Dictionary<IShadowSource, ShadowGroup> fNL = new Dictionary<IShadowSource, ShadowGroup>(32);

	/// <summary>
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	public IGraphicsDeviceService GraphicsDeviceManager => fNB;

	/// <summary>
	/// The current SceneState used by this object.
	/// </summary>
	protected ISceneState SceneState => fNa;

	/// <summary>
	/// Creates a new BaseShadowManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	public BaseShadowManager(IGraphicsDeviceService graphicsdevicemanager)
	{
		fNB = graphicsdevicemanager;
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public virtual void ApplyPreferences(ILightingSystemPreferences preferences)
	{
	}

	/// <summary>
	/// Sets up frame information necessary for scene shadowing.
	/// </summary>
	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
		SplashScreen._0003k();
		fNa = scenestate;
	}

	/// <summary>
	/// Cleans up frame information.
	/// </summary>
	public virtual void EndFrameRendering()
	{
		fNg.FreeAllTracked();
	}

	/// <summary>
	/// Builds a list of shadow groups based on the provided light list.  Shadow
	/// groups contain a list of all lights that share a common shadow source.
	/// </summary>
	/// <param name="shadowgroups">Destination shadow group list.</param>
	/// <param name="lights">Source light list.</param>
	/// <param name="usedefaultgrouping">Determines if ungrouped lights should be placed in a
	/// single default group (recommended: true for deferred rendering and false for forward).</param>
	protected void BuildShadowGroups(List<ShadowGroup> shadowgroups, List<BaseLight> lights, bool usedefaultgrouping)
	{
		fNL.Clear();
		fNf.ShadowType = ShadowType.None;
		fNn.Shadow = null;
		fNn.Lights.Clear();
		fNL.Add(fNf, fNn);
		fN_0003.ShadowType = ShadowType.None;
		fN_0019.Shadow = null;
		fN_0019.Lights.Clear();
		fNL.Add(fN_0003, fN_0019);
		int count = lights.Count;
		for (int k = 0; k < count; k++)
		{
			BaseLight baseLight = lights[k];
			if (baseLight == null)
			{
				continue;
			}
			IShadowSource shadowSource = baseLight.ShadowSource;
			if (usedefaultgrouping && (shadowSource == null || (baseLight == shadowSource && shadowSource.ShadowType == ShadowType.None)))
			{
				L.o o2 = OptimizationSystem.fNB.g_0011(baseLight);
				if (o2.fNB != null)
				{
					fNn.Lights.Add(baseLight);
				}
				else
				{
					fN_0019.Lights.Add(baseLight);
				}
				continue;
			}
			if (!fNL.TryGetValue(shadowSource, out var value))
			{
				value = fNg.New();
				value.Shadow = null;
				value.Lights.Clear();
				fNL.Add(shadowSource, value);
			}
			value.Lights.Add(baseLight);
		}
		if (fN_0019.Lights.Count <= 0)
		{
			fNL.Remove(fN_0003);
		}
		if (fNn.Lights.Count <= 0)
		{
			fNL.Remove(fNf);
		}
		else
		{
			fNf.Position = (fNn.Lights[0] as IPointSource).Position;
		}
		foreach (KeyValuePair<IShadowSource, ShadowGroup> item in fNL)
		{
			item.Value.Build(item.Key, fNa);
			shadowgroups.Add(item.Value);
		}
	}

	/// <summary>
	/// Removes resources managed by this object. Commonly used while clearing the scene.
	/// </summary>
	public abstract void Clear();

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public abstract void Unload();
}
