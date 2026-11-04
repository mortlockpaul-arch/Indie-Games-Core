using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Lights;

/// <summary>
/// Manager used for generating, storing, and retrieving light maps.
/// </summary>
public class LightMapManager : BaseLightMapManager
{
	private Texture2D fNB;

	/// <summary>
	/// Creates a new LightMapManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="sceneinterface">Service provider used to access all other manager services in this scene.</param>
	public LightMapManager(IGraphicsDeviceService graphicsdevicemanager, IManagerServiceProvider sceneinterface)
		: base(graphicsdevicemanager, sceneinterface)
	{
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public override void Unload()
	{
		fNB = null;
		base.Unload();
	}

	private ContentRepository f_000E(RenderableMesh P_0)
	{
		if (P_0.fNa == null)
		{
			return null;
		}
		ContentRepository contentRepository = ContentRepository.Find(P_0.fNa.ModelAsset.ContentRepositoryName);
		if (contentRepository != null)
		{
			return contentRepository;
		}
		return ContentRepository.DefaultContentRepository;
	}

	private ContentRepository fX(ILight P_0)
	{
		return ContentRepository.DefaultContentRepository;
	}

	/// <summary>
	/// Gets the light map associated with the provided mesh or a default light map if non exists.
	/// </summary>
	/// <param name="mesh"></param>
	/// <returns></returns>
	public override Texture2D GetLightMap(RenderableMesh mesh)
	{
		if (MeshLightMaps.TryGetValue(mesh.fNg, out var value))
		{
			return value;
		}
		ContentRepository contentRepository = f_000E(mesh);
		if (contentRepository != null)
		{
			value = contentRepository.LoadLightMap(mesh);
			MeshLightMaps.Add(mesh.fNg, value);
			return value;
		}
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		if (fNB == null && graphicsDevice != null)
		{
			fNB = LightingSystemManager.Instance.aP("Black");
		}
		value = fNB;
		MeshLightMaps.Add(mesh.fNg, value);
		return value;
	}

	/// <summary>
	/// Gets the light occlusion buffer associated with the provided light or null if non exists.
	/// </summary>
	/// <param name="light"></param>
	/// <returns></returns>
	public override LightOcclusionBuffer GetLightOcclusionBuffer(ILight light)
	{
		int uniqueId = light.UniqueId;
		if (LightOcclusionBuffers.TryGetValue(uniqueId, out var value))
		{
			return value;
		}
		ContentRepository contentRepository = fX(light);
		if (contentRepository != null)
		{
			value = contentRepository.LoadLightOcclusionBuffer(light);
			LightOcclusionBuffers.Add(uniqueId, value);
			return value;
		}
		return null;
	}
}
