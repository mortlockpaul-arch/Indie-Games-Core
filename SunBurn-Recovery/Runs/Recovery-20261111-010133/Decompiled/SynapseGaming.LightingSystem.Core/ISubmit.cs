namespace SynapseGaming.LightingSystem.Core;

public interface ISubmit<T>
{
	void Submit(T obj);

	void Move(T obj);

	void Remove(T obj);

	void Clear();
}
