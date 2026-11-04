namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Represents a curve.
///       </summary>
/// <typeparam name="TParam">
///       The type of the curve parameter (usually <see cref="T:System.Single" /> or <see cref="T:System.Double" />).
///       </typeparam>
/// <typeparam name="TPoint">
///       The type of the curve points (such as <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />, <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />, etc.).
///       </typeparam>
public interface ICurve<TParam, TPoint>
{
	/// <summary>
	///       Computes a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>
	///       The curve point. (If the curve does not contain any points a vector with NaN values is
	///       returned.)
	///       </returns>
	TPoint GetPoint(TParam parameter);

	/// <summary>
	///       Computes the tangent for a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve tangent.</returns>
	TPoint GetTangent(TParam parameter);

	/// <summary>
	///       Computes the approximated length of the curve for the parameter interval 
	///       [<paramref name="start" />, <paramref name="end" />].
	///       </summary>
	/// <param name="start">The parameter value of the start position.</param>
	/// <param name="end">The parameter value of the end position.</param>
	/// <param name="maxNumberOfIterations">
	///       The maximum number of iterations which are taken to compute the length.
	///       </param>
	/// <param name="tolerance">
	///       The tolerance value. This method will return an approximation of the precise length. 
	///       The absolute error will be less than this tolerance. 
	///       </param>
	/// <returns>
	///       The approximated length of the curve for the given parameter interval. 
	///       </returns>
	TParam GetLength(TParam start, TParam end, int maxNumberOfIterations, TParam tolerance);
}
