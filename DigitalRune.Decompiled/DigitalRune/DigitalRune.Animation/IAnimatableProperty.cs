namespace DigitalRune.Animation;

public interface IAnimatableProperty : INamedObject
{
	bool HasBaseValue { get; }

	object BaseValue { get; }

	bool HasEffectiveValue { get; set; }

	object EffectiveValue { get; }
}
public interface IAnimatableProperty<T> : INamedObject, IAnimatableProperty
{
	new T BaseValue { get; }

	new T EffectiveValue { get; set; }
}
