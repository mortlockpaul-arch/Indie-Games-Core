using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using B;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Rendering;

[Serializable]
[EditorObject(true)]
public class Scene : BaseScene
{
	public virtual IScene Clone()
	{
		Scene scene = new Scene();
		B.L._u0013_B(this, scene);
		scene.FileName = string.Empty;
		scene.Name = string.Empty;
		foreach (ISceneEntityGroup entityGroup in _EntityGroups)
		{
			scene.EntityGroups.Add(entityGroup.Clone());
		}
		foreach (ILightGroup lightGroup in _LightGroups)
		{
			scene.LightGroups.Add(lightGroup.Clone());
		}
		return scene;
	}

	internal static Scene _u0013__u0019_(string param_0)
	{
		return SerializationHelper.LoadFromXml<Scene>(param_0);
	}

	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeField(ref _EntityGroups, info, "EntityGroups", usedefault: false);
		SerializationHelper.DeserializeField(ref _LightGroups, info, "LightGroups", usedefault: false);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("EntityGroups", _EntityGroups);
		info.AddValue("LightGroups", _LightGroups);
	}
}
