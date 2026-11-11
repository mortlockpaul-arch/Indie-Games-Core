using System.Runtime.CompilerServices;

namespace SynapseGaming.LightingSystem.Core;

public class ContentRepositoryAsset<T> : IContentRepositoryAsset where T : class
{
	[CompilerGenerated]
	private T l8a;

	[CompilerGenerated]
	private string l8_u0013_;

	[CompilerGenerated]
	private string l8N;

	public T Asset
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

	public string ContentRepositoryName
	{
		[CompilerGenerated]
		get
		{
			return l8_u0013_;
		}
		[CompilerGenerated]
		private set
		{
			l8_u0013_ = value;
		}
	}

	public string SourceAssetFilePath
	{
		[CompilerGenerated]
		get
		{
			return l8N;
		}
		[CompilerGenerated]
		private set
		{
			l8N = value;
		}
	}

	public ContentRepositoryAsset()
	{
		ContentRepositoryName = string.Empty;
		SourceAssetFilePath = string.Empty;
	}

	public ContentRepositoryAsset(string repositoryname, string sourceassetfilepath)
	{
		if (string.IsNullOrEmpty(repositoryname))
		{
			repositoryname = string.Empty;
		}
		if (string.IsNullOrEmpty(sourceassetfilepath))
		{
			sourceassetfilepath = string.Empty;
		}
		ContentRepositoryName = repositoryname;
		SourceAssetFilePath = sourceassetfilepath;
		ContentRepository contentRepository = ContentRepository.Find(repositoryname);
		if (contentRepository != null)
		{
			Asset = contentRepository.LoadBySourceAssetPath<T>(sourceassetfilepath, allownull: true);
		}
	}
}
