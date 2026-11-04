using System;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Performs numerical integration using <i>Midpoint method</i> (single-precision).
///       </summary>
public class MidpointIntegratorF : OdeIntegratorF
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.MidpointIntegratorF" /> class.
	///       </summary>
	/// <param name="firstOrderDerivative">
	///       The function f(x, t) that computes the first order derivative of the vector x (see 
	///       <see cref="P:DigitalRune.Mathematics.Analysis.OdeIntegratorF.FirstOrderDerivative" />).
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="firstOrderDerivative" /> is <see langword="null" />.
	///       </exception>
	public MidpointIntegratorF(Func<VectorF, float, VectorF> firstOrderDerivative)
		: base(firstOrderDerivative)
	{
	}

	/// <summary>
	///       Computes the new state x1 at time t1.
	///       </summary>
	/// <param name="x0">The state x0 at time t0.</param>
	/// <param name="t0">The time t0.</param>
	/// <param name="t1">The target time t1 for which the new state x1 is computed.</param>
	/// <returns>The new state x1 at time t1.</returns>
	public override VectorF Integrate(VectorF x0, float t0, float t1)
	{
		float num = t1 - t0;
		VectorF vectorF = FirstOrderDerivative(x0, t0);
		VectorF arg = x0 + num / 2f * vectorF;
		vectorF = FirstOrderDerivative(arg, t0 + num / 2f);
		return x0 + num * vectorF;
	}
}
