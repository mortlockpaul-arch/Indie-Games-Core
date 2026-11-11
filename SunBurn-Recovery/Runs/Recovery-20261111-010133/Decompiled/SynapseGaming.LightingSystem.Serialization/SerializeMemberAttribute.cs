using System;

namespace SynapseGaming.LightingSystem.Serialization;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
public class SerializeMemberAttribute : Attribute
{
}
