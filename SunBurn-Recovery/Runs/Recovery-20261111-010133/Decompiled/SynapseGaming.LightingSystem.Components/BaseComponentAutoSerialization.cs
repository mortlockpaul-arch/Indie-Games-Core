using SynapseGaming.LightingSystem.Core;
using System;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Components;

[Serializable]
[SerializationInclusionModel]
public class BaseComponentAutoSerialization<T> : BaseComponent<T> where T : class, IComponentObject<T>
{
}
