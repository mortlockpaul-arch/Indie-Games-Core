using System;

namespace SynapseGaming.LightingSystem.Serialization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class SerializationInclusionModelAttribute : Attribute
{
}
