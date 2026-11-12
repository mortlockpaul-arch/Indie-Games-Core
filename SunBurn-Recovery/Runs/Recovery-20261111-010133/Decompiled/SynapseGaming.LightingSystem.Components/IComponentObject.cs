namespace SynapseGaming.LightingSystem.Core;

public interface IComponentObject<T> where T : class
{
	ComponentCollection<T> Components { get; }
}
