namespace SynapseGaming.LightingSystem.Core;

public interface IGroup<T>
{
	void Add(T obj);

	void Remove(T obj);

	void RemoveAt(int index);

	void Clear();
}
