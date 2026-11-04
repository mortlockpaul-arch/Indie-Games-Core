using System;
using System.Runtime.CompilerServices;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Mathematics.Analysis;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Defines a single segment of a 2-dimensional cubic Bézier splines (single-precision).
///       </summary>
public class BezierSegment2F : ICurve<float, Vector2F>
{
	[CompilerGenerated]
	private Vector2F c99977f6a59a72d189b9fc0d458aca839;

	[CompilerGenerated]
	private Vector2F c0b42aca19031beb107b8358dd021f2c6;

	[CompilerGenerated]
	private Vector2F cdf8e117d50bd618cd9226a8d360e28f1;

	[CompilerGenerated]
	private Vector2F cb548d667c99edc6b3cb6ca2bf0a13438;

	/// <summary>
	///       Gets or sets the start point.
	///       </summary>
	public Vector2F Point1
	{
		[CompilerGenerated]
		get
		{
			return c99977f6a59a72d189b9fc0d458aca839;
		}
		[CompilerGenerated]
		set
		{
			c99977f6a59a72d189b9fc0d458aca839 = value;
		}
	}

	/// <summary>
	///       Gets or sets the end point.
	///       </summary>
	public Vector2F Point2
	{
		[CompilerGenerated]
		get
		{
			return c0b42aca19031beb107b8358dd021f2c6;
		}
		[CompilerGenerated]
		set
		{
			c0b42aca19031beb107b8358dd021f2c6 = value;
		}
	}

	/// <summary>
	///       Gets or sets the first control point.
	///       </summary>
	public Vector2F ControlPoint1
	{
		[CompilerGenerated]
		get
		{
			return cdf8e117d50bd618cd9226a8d360e28f1;
		}
		[CompilerGenerated]
		set
		{
			cdf8e117d50bd618cd9226a8d360e28f1 = value;
		}
	}

	/// <summary>
	///       Gets or sets the second control point.
	///       </summary>
	public Vector2F ControlPoint2
	{
		[CompilerGenerated]
		get
		{
			return cb548d667c99edc6b3cb6ca2bf0a13438;
		}
		[CompilerGenerated]
		set
		{
			cb548d667c99edc6b3cb6ca2bf0a13438 = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.BezierSegment2F" /> class</summary>
	public BezierSegment2F()
	{
	}

	/// <summary>
	///       Computes a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve point.</returns>
	public Vector2F GetPoint(float parameter)
	{
		float num = parameter * parameter;
		float num2 = num * parameter;
		float num3 = 1f - parameter;
		float num4 = num3 * num3;
		float num5 = num4 * num3;
		return num5 * Point1 + 3f * parameter * num4 * ControlPoint1 + 3f * num * num3 * ControlPoint2 + num2 * Point2;
	}

	/// <summary>
	///       Computes the tangent for a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve tangent.</returns>
	public Vector2F GetTangent(float parameter)
	{
		float num = parameter * parameter;
		return (-3f + 6f * parameter - 3f * num) * Point1 + (3f - 12f * parameter + 9f * num) * ControlPoint1 + (6f * parameter - 9f * num) * ControlPoint2 + 3f * num * Point2;
	}

	internal float c78e36289b8ba4367798d5db8169286cf(int cdb4241d6538bb9fe3ad950978aa874cd, float c91bc08175ef13bb8c34b9e7a18df9235)
	{
		float cc9318c57b86104b1057fdbd227eb8df = (Point2 - ControlPoint2).Length + (ControlPoint2 - ControlPoint1).Length + (ControlPoint1 - Point1).Length;
		return c0e8af7dfb008fd83c86cd358434ed85d(Point1, ControlPoint1, ControlPoint2, Point2, cc9318c57b86104b1057fdbd227eb8df, 0, cdb4241d6538bb9fe3ad950978aa874cd, c91bc08175ef13bb8c34b9e7a18df9235);
	}

	private static float c0e8af7dfb008fd83c86cd358434ed85d(Vector2F cb5750b83fa6cdab2db2259fdbe70a659, Vector2F ce8a3cd0f9b557cc8e08161c8312b4447, Vector2F c62348a1a27501c8b72c99c54df25d25b, Vector2F cbf4b6548672dd19aba8f8dde148ac88f, float cc9318c57b86104b1057fdbd227eb8df4, int c9e5863c174840ce096a45496fda8518c, int cdb4241d6538bb9fe3ad950978aa874cd, float c91bc08175ef13bb8c34b9e7a18df9235)
	{
		c9e5863c174840ce096a45496fda8518c++;
		Vector2F vector2F = (cb5750b83fa6cdab2db2259fdbe70a659 + ce8a3cd0f9b557cc8e08161c8312b4447) / 2f;
		Vector2F vector2F2 = (ce8a3cd0f9b557cc8e08161c8312b4447 + c62348a1a27501c8b72c99c54df25d25b) / 2f;
		Vector2F vector2F3 = (c62348a1a27501c8b72c99c54df25d25b + cbf4b6548672dd19aba8f8dde148ac88f) / 2f;
		Vector2F vector2F4 = (vector2F + vector2F2) / 2f;
		Vector2F vector2F5 = (vector2F2 + vector2F3) / 2f;
		Vector2F vector2F6 = (vector2F4 + vector2F5) / 2f;
		float num = (cb5750b83fa6cdab2db2259fdbe70a659 - vector2F).Length + (vector2F - vector2F4).Length + (vector2F4 - vector2F6).Length;
		float num2 = (vector2F6 - vector2F5).Length + (vector2F5 - vector2F3).Length + (vector2F3 - cbf4b6548672dd19aba8f8dde148ac88f).Length;
		float num3 = num + num2;
		if (c9e5863c174840ce096a45496fda8518c < cdb4241d6538bb9fe3ad950978aa874cd)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(BezierSegment2F.c0e8af7dfb008fd83c86cd358434ed85d(Vector2F, Vector2F, Vector2F, Vector2F, float, int, int, float));
			}
			if (!Numeric.AreEqual(num3, cc9318c57b86104b1057fdbd227eb8df4, c91bc08175ef13bb8c34b9e7a18df9235))
			{
				num = c0e8af7dfb008fd83c86cd358434ed85d(cb5750b83fa6cdab2db2259fdbe70a659, vector2F, vector2F4, vector2F6, num, c9e5863c174840ce096a45496fda8518c, cdb4241d6538bb9fe3ad950978aa874cd, c91bc08175ef13bb8c34b9e7a18df9235);
				num2 = c0e8af7dfb008fd83c86cd358434ed85d(vector2F6, vector2F5, vector2F3, cbf4b6548672dd19aba8f8dde148ac88f, num2, c9e5863c174840ce096a45496fda8518c, cdb4241d6538bb9fe3ad950978aa874cd, c91bc08175ef13bb8c34b9e7a18df9235);
				return num + num2;
			}
		}
		return num3;
	}

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
	///       The approximated length of the curve in the given interval. For some curves the length is 
	///       computed with an iterative algorithm. The iterations end when the 
	///       <paramref name="maxNumberOfIterations" /> were performed, or when the 
	///       <paramref name="tolerance" /> criterion is met - whichever comes first.
	///       </returns>
	public float GetLength(float start, float end, int maxNumberOfIterations, float tolerance)
	{
		RombergIntegratorF rombergIntegratorF = new RombergIntegratorF();
		rombergIntegratorF.Epsilon = tolerance;
		rombergIntegratorF.MaxNumberOfIterations = maxNumberOfIterations;
		rombergIntegratorF.MinNumberOfIterations = 2;
		RombergIntegratorF rombergIntegratorF2 = rombergIntegratorF;
		return Math.Abs(rombergIntegratorF2.Integrate(ce56b3acb59a9ed8922b13ec1b5eef043, start, end));
	}

	private float ce56b3acb59a9ed8922b13ec1b5eef043(float c5078b1ca2bd86333c65cb625b996b618)
	{
		return GetTangent(c5078b1ca2bd86333c65cb625b996b618).Length;
	}
}
