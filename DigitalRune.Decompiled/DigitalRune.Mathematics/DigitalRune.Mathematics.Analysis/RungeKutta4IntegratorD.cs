using System;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Performs numerical integration using <i>4th-order Runge-Kutta method</i> (double-precision).
///       </summary>
public class RungeKutta4IntegratorD : OdeIntegratorD
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.RungeKutta4IntegratorD" /> class.
	///       </summary>
	/// <param name="firstOrderDerivative">
	///       The function f(x, t) that computes the first order derivative of the vector x (see 
	///       <see cref="P:DigitalRune.Mathematics.Analysis.OdeIntegratorD.FirstOrderDerivative" />).
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="firstOrderDerivative" /> is <see langword="null" />.
	///       </exception>
	public RungeKutta4IntegratorD(Func<VectorD, double, VectorD> firstOrderDerivative)
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
	public override VectorD Integrate(VectorD x0, double t0, double t1)
	{
		double num = t1 - t0;
		VectorD vectorD = FirstOrderDerivative(x0, t0);
		VectorD arg = x0 + num / 2.0 * vectorD;
		VectorD vectorD2 = FirstOrderDerivative(arg, t0 + num / 2.0);
		arg = x0 + num / 2.0 * vectorD2;
		VectorD vectorD3 = FirstOrderDerivative(arg, t0 + num / 2.0);
		arg = x0 + num * vectorD3;
		VectorD vectorD4 = FirstOrderDerivative(arg, t1);
		return x0 + num / 6.0 * vectorD + num / 3.0 * vectorD2 + num / 3.0 * vectorD3 + num / 6.0 * vectorD4;
	}
}
