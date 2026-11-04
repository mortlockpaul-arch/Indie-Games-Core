using System;
using System.IO;
using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Core;
using m;

namespace SynapseGaming.LightingSystem.Processors;

/// <summary />
public class ContentRepositoryReader_Pro : ContentTypeReader<ContentRepository>
{
	private string _0003V(ContentReader P_0, string P_1)
	{
		if (string.IsNullOrEmpty(P_1))
		{
			return "";
		}
		int num = P_0.AssetName.LastIndexOfAny(new char[3]
		{
			'\\',
			'/',
			Path.DirectorySeparatorChar
		});
		string path = "";
		if (num != -1)
		{
			path = P_0.AssetName.Substring(0, num);
		}
		return Path.Combine(path, P_1);
	}

	/// <summary />
	protected override ContentRepository Read(ContentReader input, ContentRepository instance)
	{
		ContentRepository contentRepository = new ContentRepository(input.ReadString(), input.ContentManager);
		contentRepository.XnbContentManagerFileName = input.AssetName;
		contentRepository.FileName = input.ReadString();
		contentRepository.ProjectFile = input.ReadString();
		contentRepository.ProcessorRenderingType = (ProcessorRenderingType)input.ReadInt32();
		int num = 0;
		num = input.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			string text = input.ReadString();
			string text2 = _0003V(input, input.ReadString());
			ModelMeshNames modelMeshNames = input.ReadExternalReference<ModelMeshNames>();
			ContentRepository.ModelData modelData = new ContentRepository.ModelData(modelMeshNames.MeshNames);
			modelData.av(input);
			contentRepository._0018(text, text2, modelData);
		}
		num = input.ReadInt32();
		for (int j = 0; j < num; j++)
		{
			string text3 = input.ReadString();
			string text4 = _0003V(input, input.ReadString());
			ContentRepository.SoundEffectData soundEffectData = new ContentRepository.SoundEffectData();
			soundEffectData.av(input);
			contentRepository.aB(text3, text4, soundEffectData);
		}
		num = input.ReadInt32();
		for (int k = 0; k < num; k++)
		{
			contentRepository.ag(input.ReadString(), _0003V(input, input.ReadString()));
		}
		num = input.ReadInt32();
		for (int l = 0; l < num; l++)
		{
			contentRepository.af(input.ReadString(), _0003V(input, input.ReadString()));
		}
		num = input.ReadInt32();
		for (int m = 0; m < num; m++)
		{
			string text5 = input.ReadString();
			PrefabObjectCategory prefabObjectCategory = (PrefabObjectCategory)input.ReadInt32();
			string text6 = input.ReadString();
			contentRepository.a_0003(text5, new PrefabObjectGenerator(text6, prefabObjectCategory));
		}
		global::m.B._0003F(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return contentRepository;
	}
}
