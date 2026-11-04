using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Rendering;
using m;

namespace SynapseGaming.LightingSystem.Processors;

/// <summary />
public class SceneReader_Pro : ContentTypeReader<Scene>
{
	/// <summary />
	protected override Scene Read(ContentReader input, Scene instance)
	{
		Scene scene = Scene.g_0019(input.ReadString());
		scene.gn(input.ReadString());
		scene.FileName = input.ReadString();
		scene.ProjectFile = input.ReadString();
		m.B._0003F(input);
		return scene;
	}
}
