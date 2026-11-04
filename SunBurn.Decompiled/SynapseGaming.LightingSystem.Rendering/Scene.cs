using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Container object used for storing, sharing, and organizing scene entities, objects, and lights.
/// </summary>
[Serializable]
[EditorObject(true)]
public class Scene : BaseScene
{
	/// <summary>
	/// Creates a Scene instance.
	/// </summary>
	public Scene()
	{
	}

	internal static Scene g_0019(string P_0)
	{
		return SerializationHelper.LoadFromXml<Scene>(P_0);
	}

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeField(ref _EntityGroups, info, "EntityGroups", usedefault: false);
		SerializationHelper.DeserializeField(ref _LightGroups, info, "LightGroups", usedefault: false);
	}

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("EntityGroups", _EntityGroups);
		info.AddValue("LightGroups", _LightGroups);
	}
}
