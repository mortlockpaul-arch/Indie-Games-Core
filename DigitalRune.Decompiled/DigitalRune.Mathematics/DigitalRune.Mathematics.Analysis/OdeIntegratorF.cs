using System;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       A base class for numerical integration strategies for ordinary differential equations (ODE).
///       (Single-precision)
///       </summary>
public abstract class OdeIntegratorF
{
	[CompilerGenerated]
	private Func<VectorF, float, VectorF> c01d73ef9a43a704ddba3d83c8d5afc9d;

	/// <summary>
	///       Gets the function f(x, t) that computes the first order derivative.
	///       </summary>
	public Func<VectorF, float, VectorF> FirstOrderDerivative
	{
		[CompilerGenerated]
		get
		{
			return c01d73ef9a43a704ddba3d83c8d5afc9d;
		}
		[CompilerGenerated]
		private set
		{
			c01d73ef9a43a704ddba3d83c8d5afc9d = value;
		}
	}

	static OdeIntegratorF()
	{
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.OdeIntegratorF" /> class.
	///       </summary>
	/// <param name="firstOrderDerivative">
	///       The function f(x, t) that computes the first order derivative of the vector x 
	///       (see <see cref="P:DigitalRune.Mathematics.Analysis.OdeIntegratorF.FirstOrderDerivative" />).
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="firstOrderDerivative" /> is <see langword="null" />.
	///       </exception>
	protected OdeIntegratorF(Func<VectorF, float, VectorF> firstOrderDerivative)
	{
		if (firstOrderDerivative == null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(OdeIntegratorF._002Ector(Func<VectorF, float, VectorF>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(581));
				}
			}
		}
		FirstOrderDerivative = firstOrderDerivative;
	}

	/// <summary>
	///       Computes the new state x1 at time t1.
	///       </summary>
	/// <param name="x0">The state x0 at time t0.</param>
	/// <param name="t0">The time t0.</param>
	/// <param name="t1">The target time t1 for which the new state x1 is computed.</param>
	/// <returns>The new state x1 at time t1.</returns>
	public abstract VectorF Integrate(VectorF x0, float t0, float t1);
}
