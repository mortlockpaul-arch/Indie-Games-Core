using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using m;

namespace SynapseGaming.LightingSystem.Processors;

/// <summary />
public class ModelMeshNamesReader_Pro : ContentTypeReader<ModelMeshNames>
{
	/// <summary />
	protected override ModelMeshNames Read(ContentReader input, ModelMeshNames instance)
	{
		ModelMeshNames modelMeshNames = new ModelMeshNames();
		modelMeshNames.MeshNames = input.ReadObject<List<string>>();
		m.B._0003F(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return modelMeshNames;
	}
}
