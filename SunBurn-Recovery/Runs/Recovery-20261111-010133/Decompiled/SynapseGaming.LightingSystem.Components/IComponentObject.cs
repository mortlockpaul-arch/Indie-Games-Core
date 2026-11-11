namespace SynapseGaming.LightingSystem.Components;

public interface IComponentObject<T> where T : class
{
	ComponentCollection<T> Components { get; }
}
