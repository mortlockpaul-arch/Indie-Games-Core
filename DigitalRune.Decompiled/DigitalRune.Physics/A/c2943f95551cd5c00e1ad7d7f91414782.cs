using System.Runtime.CompilerServices;
using DigitalRune.Physics;

namespace A;

internal abstract class c2943f95551cd5c00e1ad7d7f91414782
{
	[CompilerGenerated]
	private Simulation cbebb653f415fd36f75adc579c8bca56d;

	public Simulation Simulation
	{
		[CompilerGenerated]
		get
		{
			return cbebb653f415fd36f75adc579c8bca56d;
		}
		[CompilerGenerated]
		private set
		{
			cbebb653f415fd36f75adc579c8bca56d = value;
		}
	}

	protected c2943f95551cd5c00e1ad7d7f91414782(Simulation cc597c252189878e9fdf2cba65ddefbe4)
	{
		Simulation = cc597c252189878e9fdf2cba65ddefbe4;
	}

	public abstract void cb71790430f9d613551af34a891399e09(SimulationIsland cabc32bb1fe9456232d11a3a7adad3555, float ce8c71427a0828475a969c745c8335cde);
}
