namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Represents a single numeric statistic, which can be rendered on-screen
/// or saved to file using the SystemConsole class.
/// </summary>
public class SystemStatistic
{
	/// <summary>
	/// Current accumulating value being generated over this frame. This is the value
	/// to increment when supplying statistic information. For instance if the statistic
	/// tracks object rendering, then whenever an object is rendered increment the AccumulationValue
	/// by one.
	/// </summary>
	public int AccumulationValue;

	private string fNB = string.Empty;

	private SystemStatisticCategory fNa;

	private int fNg;

	/// <summary>
	/// Unique display name for the statistic.
	/// </summary>
	public string Name => fNB;

	/// <summary>
	/// Categories the statistic is assigned to.
	/// </summary>
	public SystemStatisticCategory Category => fNa;

	/// <summary>
	/// Fully accumulated value generated during the last frame. This is the display value.
	/// </summary>
	public int Value => fNg;

	internal SystemStatistic(string P_0, SystemStatisticCategory P_1)
	{
		fNB = P_0;
		fNa = P_1;
	}

	internal void a_000E(string P_0)
	{
		fNB = P_0;
	}

	internal void h()
	{
		fNg = AccumulationValue;
		AccumulationValue = 0;
	}
}
