using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public class ModelAsset : ContentRepositoryAsset<Model>
{
	public static readonly ModelAsset Empty = new ModelAsset();

	[CompilerGenerated]
	private string l8a;

	public string ModelMeshName
	{
		[CompilerGenerated]
		get
		{
			return l8a;
		}
		[CompilerGenerated]
		private set
		{
			l8a = value;
		}
	}

	private ModelAsset()
	{
		ModelMeshName = string.Empty;
	}

	public ModelAsset(string repositoryname, string sourceassetfilepath, string modelmeshname)
		: base(repositoryname, sourceassetfilepath)
	{
		if (string.IsNullOrEmpty(modelmeshname))
		{
			modelmeshname = string.Empty;
		}
		ModelMeshName = modelmeshname;
	}
}
