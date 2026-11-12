using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Components;

[Serializable]
public class BaseComponentManualSerialization<T> : BaseComponent<T>, IFullSerializable, ISerializable where T : class, IComponentObject<T>
{
	public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
	{
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
	}
}
