using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using _id_6;

namespace SynapseGaming.LightingSystem.Processors;

public class ModelMeshNamesReader_Pro : ContentTypeReader<ModelMeshNames>
{
	protected override ModelMeshNames Read(ContentReader input, ModelMeshNames instance)
	{
		ModelMeshNames modelMeshNames = new ModelMeshNames();
		modelMeshNames.MeshNames = input.ReadObject<List<string>>();
		_id_6.a.L_u0005_(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return modelMeshNames;
	}
}
