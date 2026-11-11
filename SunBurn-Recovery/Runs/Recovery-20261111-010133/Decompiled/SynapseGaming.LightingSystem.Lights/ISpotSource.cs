namespace SynapseGaming.LightingSystem.Lights;

public interface ISpotSource : IPointSource, IDirectionalSource
{
	float Angle { get; set; }

	float Volume { get; set; }
}
