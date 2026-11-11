using System.Runtime.Serialization;

namespace SynapseGaming.LightingSystem.Serialization;

public interface IFullSerializable : ISerializable
{
	void SetObjectData(SerializationInfo info, StreamingContext context);
}
