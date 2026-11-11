namespace SynapseGaming.LightingSystem.Core;

public class SystemStatistic
{
	public int AccumulationValue;

	private string l8a = string.Empty;

	private SystemStatisticCategory l8_u0013_;

	private int l8N;

	public string Name => l8a;

	public SystemStatisticCategory Category => l8_u0013_;

	public int Value => l8N;

	internal SystemStatistic(string param_0, SystemStatisticCategory param_1)
	{
		l8a = param_0;
		l8_u0013_ = param_1;
	}

	internal void _u0013_C(string param_0)
	{
		l8a = param_0;
	}

	internal void _u0004_()
	{
		l8N = AccumulationValue;
		AccumulationValue = 0;
	}
}
