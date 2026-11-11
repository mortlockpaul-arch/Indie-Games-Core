using System.Text;
using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Rendering;
using _id_6;

namespace SynapseGaming.LightingSystem.Processors;

public class SceneReader_Pro : ContentTypeReader<Scene>
{
	protected override Scene Read(ContentReader input, Scene instance)
	{
		string param_ = string.Empty;
		int num = input.ReadInt32();
		if (num > 0)
		{
			byte[] array = input.ReadBytes(num);
			param_ = Encoding.UTF8.GetString(array, 0, array.Length);
		}
		Scene scene = Scene._u0013__u0019_(param_);
		scene._u0013_X(input.ReadString());
		scene.FileName = input.ReadString();
		scene.ProjectFile = input.ReadString();
		_id_6.a.L_u0005_(input);
		return scene;
	}
}
