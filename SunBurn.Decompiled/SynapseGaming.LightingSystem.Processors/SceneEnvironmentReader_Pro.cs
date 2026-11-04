using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Core;
using m;

namespace SynapseGaming.LightingSystem.Processors;

/// <summary />
public class SceneEnvironmentReader_Pro : ContentTypeReader<SceneEnvironment>
{
	/// <summary />
	protected override SceneEnvironment Read(ContentReader input, SceneEnvironment instance)
	{
		SceneEnvironment sceneEnvironment = SceneEnvironment.g_0019(input.ReadString());
		sceneEnvironment.gn(input.ReadString());
		sceneEnvironment.FileName = input.ReadString();
		sceneEnvironment.ProjectFile = input.ReadString();
		m.B._0003F(input);
		return sceneEnvironment;
	}
}
