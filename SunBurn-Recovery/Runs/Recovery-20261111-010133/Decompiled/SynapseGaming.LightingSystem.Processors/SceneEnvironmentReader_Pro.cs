using System.Text;
using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Core;
using _id_6;

namespace SynapseGaming.LightingSystem.Processors;

public class SceneEnvironmentReader_Pro : ContentTypeReader<SceneEnvironment>
{
	protected override SceneEnvironment Read(ContentReader input, SceneEnvironment instance)
	{
		string param_ = string.Empty;
		int num = input.ReadInt32();
		if (num > 0)
		{
			byte[] array = input.ReadBytes(num);
			param_ = Encoding.UTF8.GetString(array, 0, array.Length);
		}
		SceneEnvironment sceneEnvironment = SceneEnvironment._u0013__u0019_(param_);
		sceneEnvironment._u0013_X(input.ReadString());
		sceneEnvironment.FileName = input.ReadString();
		sceneEnvironment.ProjectFile = input.ReadString();
		_id_6.a.L_u0005_(input);
		return sceneEnvironment;
	}
}
