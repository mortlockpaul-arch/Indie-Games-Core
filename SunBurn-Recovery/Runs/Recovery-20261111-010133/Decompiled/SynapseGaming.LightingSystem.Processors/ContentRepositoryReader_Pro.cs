using System;
using System.IO;
using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Core;
using _id_6;

namespace SynapseGaming.LightingSystem.Processors;

public class ContentRepositoryReader_Pro : ContentTypeReader<ContentRepository>
{
	private string Lb(ContentReader param_0, string param_1)
	{
		if (string.IsNullOrEmpty(param_1))
		{
			return "";
		}
		int num = param_0.AssetName.LastIndexOfAny(new char[3]
		{
			'\\',
			'/',
			Path.DirectorySeparatorChar
		});
		string path = "";
		if (num != -1)
		{
			path = param_0.AssetName.Substring(0, num);
		}
		return Path.Combine(path, param_1);
	}

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
			string param_ = input.ReadString();
			string param_2 = Lb(input, input.ReadString());
			ModelMeshNames modelMeshNames = input.ReadExternalReference<ModelMeshNames>();
			ContentRepository.ModelData modelData = new ContentRepository.ModelData(modelMeshNames.MeshNames);
			modelData._id_8(input);
			contentRepository._u0010_(param_, param_2, modelData);
		}
		num = input.ReadInt32();
		for (int j = 0; j < num; j++)
		{
			string param_3 = input.ReadString();
			string param_4 = Lb(input, input.ReadString());
			ContentRepository.SoundEffectData soundEffectData = new ContentRepository.SoundEffectData();
			soundEffectData._id_8(input);
			contentRepository._u0011_(param_3, param_4, soundEffectData);
		}
		num = input.ReadInt32();
		for (int k = 0; k < num; k++)
		{
			contentRepository.m(input.ReadString(), Lb(input, input.ReadString()));
		}
		num = input.ReadInt32();
		for (int l = 0; l < num; l++)
		{
			contentRepository.n(input.ReadString(), Lb(input, input.ReadString()));
		}
		num = input.ReadInt32();
		for (int m = 0; m < num; m++)
		{
			string param_5 = input.ReadString();
			PrefabObjectCategory param_6 = (PrefabObjectCategory)input.ReadInt32();
			string param_7 = input.ReadString();
			contentRepository.k(param_5, new PrefabObjectGenerator(param_7, param_6));
		}
		_id_6.a.L_u0005_(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return contentRepository;
	}
}
