using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Lights;

public class LightMapManager : BaseLightMapManager
{
	private LightMap l8a;

	public LightMapManager(IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
	}

	public override void Unload()
	{
		l8a = null;
		base.Unload();
	}

	private ContentRepository l0(RenderableMesh param_0)
	{
		if (param_0.l8_u0013_ == null)
		{
			return null;
		}
		ContentRepository contentRepository = ContentRepository.Find(param_0.l8_u0013_.ModelAsset.ContentRepositoryName);
		if (contentRepository != null)
		{
			return contentRepository;
		}
		return ContentRepository.DefaultContentRepository;
	}

	private ContentRepository lR(ILight param_0)
	{
		return ContentRepository.DefaultContentRepository;
	}

	public override void SetLightMap(int meshuniqueid, LightMap lightmap)
	{
		if (lightmap != null)
		{
			if (MeshLightMaps.TryGetValue(meshuniqueid, out var value) && value != null && value != l8a)
			{
				OwnerSceneInterface.GetManager<IResourceManager>(required: false)?.AssignOwnership(value);
			}
			MeshLightMaps[meshuniqueid] = lightmap;
		}
	}

	public override LightMap GetLightMap(RenderableMesh mesh)
	{
		if (MeshLightMaps.TryGetValue(mesh.l8N, out var value))
		{
			return value;
		}
		ContentRepository contentRepository = l0(mesh);
		if (contentRepository != null)
		{
			value = contentRepository.LoadLightMap(mesh);
			MeshLightMaps.Add(mesh.l8N, value);
			return value;
		}
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		if (l8a == null && graphicsDevice != null)
		{
			Texture2D texture2D = SunBurnCoreSystem.Instance._u0013_S("Black");
			l8a = new LightMap(texture2D, texture2D);
		}
		value = l8a;
		MeshLightMaps.Add(mesh.l8N, value);
		return value;
	}

	public override LightOcclusionBuffer GetLightOcclusionBuffer(ILight light)
	{
		int uniqueId = light.UniqueId;
		if (LightOcclusionBuffers.TryGetValue(uniqueId, out var value))
		{
			return value;
		}
		ContentRepository contentRepository = lR(light);
		if (contentRepository != null)
		{
			value = contentRepository.LoadLightOcclusionBuffer(light);
			LightOcclusionBuffers.Add(uniqueId, value);
			return value;
		}
		return null;
	}
}
