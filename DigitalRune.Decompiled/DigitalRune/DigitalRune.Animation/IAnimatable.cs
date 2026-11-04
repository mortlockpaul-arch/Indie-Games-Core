using System.Collections.Generic;

namespace DigitalRune.Animation;

public interface IAnimatable : INamedObject
{
	IEnumerable<IAnimatableProperty> AnimatableProperties { get; }

	IAnimatableProperty GetAnimatableProperty(string name);

	IAnimatableProperty<T> GetAnimatableProperty<T>(string name);
}
