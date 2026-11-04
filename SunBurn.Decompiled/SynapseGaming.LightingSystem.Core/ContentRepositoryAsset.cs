using System.Runtime.CompilerServices;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Provides a generic asset wrapper containing the source repository name, file name, and direct access
/// to the loaded asset. The content repository provided must be loaded before creating an instance of
/// this class otherwise the asset will fail to load.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ContentRepositoryAsset<T> : IContentRepositoryAsset where T : class
{
	[CompilerGenerated]
	private T fNB;

	[CompilerGenerated]
	private string fNa;

	[CompilerGenerated]
	private string fNg;

	/// <summary>
	/// The loaded asset.
	/// </summary>
	public T Asset
	{
		[CompilerGenerated]
		get
		{
			return fNB;
		}
		[CompilerGenerated]
		private set
		{
			fNB = value;
		}
	}

	/// <summary>
	/// Name of the content repository, which contains the asset.
	/// </summary>
	public string ContentRepositoryName
	{
		[CompilerGenerated]
		get
		{
			return fNa;
		}
		[CompilerGenerated]
		private set
		{
			fNa = value;
		}
	}

	/// <summary>
	/// Relative path to the file the asset was loaded from.
	/// </summary>
	public string SourceAssetFilePath
	{
		[CompilerGenerated]
		get
		{
			return fNg;
		}
		[CompilerGenerated]
		private set
		{
			fNg = value;
		}
	}

	/// <summary>
	/// Creates a new empty ContentRepositoryAsset instance.
	/// </summary>
	public ContentRepositoryAsset()
	{
		ContentRepositoryName = string.Empty;
		SourceAssetFilePath = string.Empty;
	}

	/// <summary>
	/// Creates a new ContentRepositoryAsset instance and loads the provided asset.
	/// </summary>
	/// <param name="repositoryname">Name of the content repository, which contains the asset.</param>
	/// <param name="sourceassetfilepath">Relative path to the file the asset is loaded from.</param>
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
