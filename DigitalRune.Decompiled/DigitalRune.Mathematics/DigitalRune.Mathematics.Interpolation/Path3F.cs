using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using A;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Mathematics.Analysis;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Represents a 3-dimensional path that is defined by piecewise interpolation of key points 
///       (single-precision).
///       </summary>
[Serializable]
public class Path3F : PiecewiseCurveF<Vector3F, PathKey3F>, IXmlSerializable
{
	[CompilerGenerated]
	private sealed class ca783377fc7cda3b09e03b7509d436227
	{
		public ICurve<float, Vector3F> c4f3e345fccb3fcf4a426055d25c06f6c;

		public int cdb4241d6538bb9fe3ad950978aa874cd;

		public float c91bc08175ef13bb8c34b9e7a18df9235;

		public float c95e05cb025302153fcc5e0901d4ba83a(float c5078b1ca2bd86333c65cb625b996b618)
		{
			return c4f3e345fccb3fcf4a426055d25c06f6c.GetLength(0f, c5078b1ca2bd86333c65cb625b996b618, cdb4241d6538bb9fe3ad950978aa874cd, c91bc08175ef13bb8c34b9e7a18df9235 / 10f);
		}

		public float c648225bdacf741899a2895305656e8b1(float c5078b1ca2bd86333c65cb625b996b618)
		{
			return c4f3e345fccb3fcf4a426055d25c06f6c.GetTangent(c5078b1ca2bd86333c65cb625b996b618).Length;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.Path3F" /> class</summary>
	public Path3F()
	{
	}

	/// <summary>
	///       Computes a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve point.</returns>
	public override Vector3F GetPoint(float parameter)
	{
		int count = Count;
		if (count == 0)
		{
			return new Vector3F(float.NaN);
		}
		float num = LoopParameter(parameter);
		PathKey3F pathKey3F = Items[0];
		PathKey3F pathKey3F2 = Items[count - 1];
		float parameter2 = pathKey3F.Parameter;
		float parameter3 = pathKey3F2.Parameter;
		if (num < parameter2)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
				{
					if (1 == 0)
					{
						__ldtoken(Path3F.GetPoint(float));
					}
					Vector3F tangent = GetTangent(num);
					return pathKey3F.Point + tangent * (num - parameter2);
				}
				}
			}
		}
		if (num > parameter3)
		{
			Vector3F tangent2 = GetTangent(num);
			return pathKey3F2.Point + tangent2 * (num - parameter3);
		}
		if (count == 1)
		{
			return pathKey3F.Point;
		}
		Vector3F vector3F = cfd3d9eb09693d35ff85c0c74045c54ef(parameter);
		if (num == pathKey3F2.Parameter && Items[count - 2].Interpolation != SplineInterpolation.BSpline)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					return pathKey3F2.Point + vector3F;
				}
			}
		}
		int num2 = GetKeyIndex(num);
		if (num2 == count - 1)
		{
			num2--;
		}
		PathKey3F pathKey3F3 = Items[num2];
		PathKey3F pathKey3F4 = Items[num2 + 1];
		float parameter4 = pathKey3F3.Parameter;
		float parameter5 = pathKey3F4.Parameter;
		float num3 = parameter5 - parameter4;
		num = (num - parameter4) / num3;
		ICurve<float, Vector3F> curve = cbd6850b7468243a597c1a8c9c9f905d0(num2);
		return curve.GetPoint(num) + vector3F;
	}

	private Vector3F cfd3d9eb09693d35ff85c0c74045c54ef(float c1ee99c45b13417a935570173f4daec16)
	{
		PathKey3F pathKey3F = Items[0];
		PathKey3F pathKey3F2 = Items[Count - 1];
		float parameter = pathKey3F.Parameter;
		float parameter2 = pathKey3F2.Parameter;
		float num = parameter2 - parameter;
		Vector3F result = default;
		if (!Numeric.IsZero(num))
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(Path3F.cfd3d9eb09693d35ff85c0c74045c54ef(float));
			}
			Vector3F vector3F = pathKey3F2.Point - pathKey3F.Point;
			if (c1ee99c45b13417a935570173f4daec16 < parameter)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				if (PreLoop == CurveLoopType.CycleOffset)
				{
					while (true)
					{
						switch (2)
						{
						case 0:
							break;
						default:
						{
							int num2 = (int)((c1ee99c45b13417a935570173f4daec16 - parameter2) / num);
							return num2 * vector3F;
						}
						}
					}
				}
			}
			if (c1ee99c45b13417a935570173f4daec16 > parameter2 && PostLoop == CurveLoopType.CycleOffset)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
					{
						int num3 = (int)((c1ee99c45b13417a935570173f4daec16 - parameter) / num);
						return num3 * vector3F;
					}
					}
				}
			}
		}
		return result;
	}

	/// <summary>
	///       Computes the tangent for a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve tangent.</returns>
	public override Vector3F GetTangent(float parameter)
	{
		int count = Count;
		if (count == 0)
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
						__ldtoken(Path3F.GetTangent(float));
					}
					return default;
				}
			}
		}
		PathKey3F pathKey3F = Items[0];
		PathKey3F pathKey3F2 = Items[count - 1];
		float parameter2 = pathKey3F.Parameter;
		float parameter3 = pathKey3F2.Parameter;
		if (PreLoop == CurveLoopType.Constant)
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
			if (parameter < parameter2)
			{
				goto IL_00a0;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		if (PostLoop == CurveLoopType.Constant)
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
			if (parameter > parameter3)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				goto IL_00a0;
			}
		}
		float num = LoopParameter(parameter);
		if (num < parameter2)
		{
			Vector3F vector3F = pathKey3F.Interpolation switch
			{
				SplineInterpolation.Bezier => (pathKey3F.Point - pathKey3F.TangentIn) * 3f, 
				SplineInterpolation.Hermite => pathKey3F.TangentIn, 
				_ => (count > 1) ? cbd6850b7468243a597c1a8c9c9f905d0(0).GetTangent(0f) : default(Vector3F), 
			};
			float num2;
			if (count <= 1)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				num2 = 0f;
			}
			else
			{
				num2 = Items[1].Parameter - parameter2;
			}
			float num3 = num2;
			if (!(num3 > 0f))
			{
				return vector3F;
			}
			return vector3F / num3;
		}
		if (num > parameter3)
		{
			Vector3F vector3F3;
			switch (pathKey3F2.Interpolation)
			{
			case SplineInterpolation.Bezier:
				vector3F3 = (pathKey3F2.TangentOut - pathKey3F2.Point) * 3f;
				break;
			case SplineInterpolation.Hermite:
				vector3F3 = pathKey3F2.TangentOut;
				break;
			default:
			{
				Vector3F vector3F2;
				if (count <= 1)
				{
					while (true)
					{
						switch (1)
						{
						case 0:
							continue;
						}
						break;
					}
					vector3F2 = default;
				}
				else
				{
					vector3F2 = cbd6850b7468243a597c1a8c9c9f905d0(count - 2).GetTangent(1f);
				}
				vector3F3 = vector3F2;
				break;
			}
			}
			float num4 = ((count > 1) ? (parameter3 - Items[count - 2].Parameter) : 0f);
			if (!(num4 > 0f))
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
						return vector3F3;
					}
				}
			}
			return vector3F3 / num4;
		}
		if (count == 1)
		{
			if (PostLoop == CurveLoopType.Linear)
			{
				PathKey3F pathKey3F3 = pathKey3F;
				if (pathKey3F3.Interpolation == SplineInterpolation.Bezier)
				{
					while (true)
					{
						switch (7)
						{
						case 0:
							continue;
						}
						return (pathKey3F3.TangentOut - pathKey3F3.Point) * 3f;
					}
				}
				if (pathKey3F3.Interpolation == SplineInterpolation.Hermite)
				{
					return pathKey3F3.TangentOut;
				}
			}
			else if (PreLoop == CurveLoopType.Linear)
			{
				PathKey3F pathKey3F4 = pathKey3F;
				if (pathKey3F4.Interpolation == SplineInterpolation.Bezier)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						return (pathKey3F4.Point - pathKey3F4.TangentIn) * 3f;
					}
				}
				if (pathKey3F4.Interpolation == SplineInterpolation.Hermite)
				{
					while (true)
					{
						switch (7)
						{
						case 0:
							break;
						default:
							return pathKey3F4.TangentIn;
						}
					}
				}
			}
			return default;
		}
		int num5 = GetKeyIndex(num);
		if (num5 == count - 1)
		{
			num5--;
		}
		PathKey3F pathKey3F5 = Items[num5];
		PathKey3F pathKey3F6 = Items[num5 + 1];
		float parameter4 = pathKey3F5.Parameter;
		float parameter5 = pathKey3F6.Parameter;
		float num6 = parameter5 - parameter4;
		num = (num - parameter4) / num6;
		ICurve<float, Vector3F> curve = cbd6850b7468243a597c1a8c9c9f905d0(num5);
		Vector3F vector3F4 = curve.GetTangent(num) / num6;
		if (IsInMirroredOscillation(parameter))
		{
			return -vector3F4;
		}
		return vector3F4;
		IL_00a0:
		return default;
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
	///       The approximated length of the curve in the given interval.
	///       </returns>
	public override float GetLength(float start, float end, int maxNumberOfIterations, float tolerance)
	{
		int count = Count;
		if (count == 0)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Path3F.GetLength(float, float, int, float));
					}
					return 0f;
				}
			}
		}
		if (count == 1)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
			if (PreLoop != CurveLoopType.Linear && PostLoop != CurveLoopType.Linear)
			{
				return 0f;
			}
		}
		RombergIntegratorF rombergIntegratorF = new RombergIntegratorF();
		rombergIntegratorF.Epsilon = tolerance;
		rombergIntegratorF.MaxNumberOfIterations = maxNumberOfIterations;
		rombergIntegratorF.MinNumberOfIterations = 5;
		RombergIntegratorF rombergIntegratorF2 = rombergIntegratorF;
		return Math.Abs(rombergIntegratorF2.Integrate((float c5078b1ca2bd86333c65cb625b996b618) => GetTangent(c5078b1ca2bd86333c65cb625b996b618).Length, start, end));
	}

	/// <summary>
	///       Parameterizes the path by its length.
	///       </summary>
	/// <param name="maxNumberOfIterations">
	///       The maximum number of iterations which are taken to compute the length.
	///       </param>
	/// <param name="tolerance">
	///       The tolerance value. This method will return an approximation of the precise length. The 
	///       absolute error will be less than this tolerance.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="tolerance" /> is negative or 0.
	///       </exception>
	public void ParameterizeByLength(int maxNumberOfIterations, float tolerance)
	{
		if (tolerance <= 0f)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Path3F.ParameterizeByLength(int, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9937), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9956));
				}
			}
		}
		int count = Count;
		if (count == 0)
		{
			return;
		}
		Items[0].Parameter = 0f;
		for (int i = 1; i < count; i++)
		{
			PathKey3F pathKey3F = Items[i - 1];
			PathKey3F pathKey3F2 = Items[i];
			float length = cbd6850b7468243a597c1a8c9c9f905d0(i - 1).GetLength(0f, 1f, maxNumberOfIterations, tolerance);
			pathKey3F2.Parameter = pathKey3F.Parameter + length;
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				break;
			default:
				return;
			}
		}
	}

	private ICurve<float, Vector3F> cbd6850b7468243a597c1a8c9c9f905d0(int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		int count = Count;
		PathKey3F pathKey3F = Items[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		PathKey3F pathKey3F2 = Items[Math.Min(count - 1, c88a85a6fc8e0a66e19d046d4d51cd3f1 + 1)];
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.StepLeft)
		{
			StepSegment3F stepSegment3F = new StepSegment3F();
			stepSegment3F.Point1 = pathKey3F.Point;
			stepSegment3F.Point2 = pathKey3F2.Point;
			stepSegment3F.StepType = StepInterpolation.Left;
			return stepSegment3F;
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.StepCentered)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Path3F.cbd6850b7468243a597c1a8c9c9f905d0(int));
				}
				StepSegment3F stepSegment3F2 = new StepSegment3F();
				stepSegment3F2.Point1 = pathKey3F.Point;
				stepSegment3F2.Point2 = pathKey3F2.Point;
				stepSegment3F2.StepType = StepInterpolation.Centered;
				return stepSegment3F2;
			}
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.StepRight)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				StepSegment3F stepSegment3F3 = new StepSegment3F();
				stepSegment3F3.Point1 = pathKey3F.Point;
				stepSegment3F3.Point2 = pathKey3F2.Point;
				stepSegment3F3.StepType = StepInterpolation.Right;
				return stepSegment3F3;
			}
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.Linear)
		{
			LineSegment3F lineSegment3F = new LineSegment3F();
			lineSegment3F.Point1 = pathKey3F.Point;
			lineSegment3F.Point2 = pathKey3F2.Point;
			return lineSegment3F;
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.Bezier)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				BezierSegment3F bezierSegment3F = new BezierSegment3F();
				bezierSegment3F.Point1 = pathKey3F.Point;
				bezierSegment3F.ControlPoint1 = pathKey3F.TangentOut;
				bezierSegment3F.Point2 = pathKey3F2.Point;
				bezierSegment3F.ControlPoint2 = pathKey3F2.TangentIn;
				return bezierSegment3F;
			}
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.Hermite)
		{
			HermiteSegment3F hermiteSegment3F = new HermiteSegment3F();
			hermiteSegment3F.Point1 = pathKey3F.Point;
			hermiteSegment3F.Point2 = pathKey3F2.Point;
			hermiteSegment3F.Tangent1 = pathKey3F.TangentOut;
			hermiteSegment3F.Tangent2 = pathKey3F2.TangentIn;
			return hermiteSegment3F;
		}
		Vector3F point;
		if (c88a85a6fc8e0a66e19d046d4d51cd3f1 > 0)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			point = Items[c88a85a6fc8e0a66e19d046d4d51cd3f1 - 1].Point;
		}
		else
		{
			if (SmoothEnds)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				if (PreLoop == CurveLoopType.Cycle)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
					point = Items[count - 2].Point;
					goto IL_033b;
				}
			}
			if (SmoothEnds)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				if (PreLoop == CurveLoopType.CycleOffset)
				{
					point = Items[count - 2].Point - (Items[count - 1].Point - Items[0].Point);
					goto IL_033b;
				}
			}
			point = Items[0].Point - (Items[1].Point - Items[0].Point);
		}
		goto IL_033b;
		IL_0465:
		Vector3F point2;
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.BSpline)
		{
			BSplineSegment3F bSplineSegment3F = new BSplineSegment3F();
			bSplineSegment3F.Point1 = point;
			bSplineSegment3F.Point2 = pathKey3F.Point;
			bSplineSegment3F.Point3 = pathKey3F2.Point;
			bSplineSegment3F.Point4 = point2;
			return bSplineSegment3F;
		}
		CatmullRomSegment3F catmullRomSegment3F = new CatmullRomSegment3F();
		catmullRomSegment3F.Point1 = point;
		catmullRomSegment3F.Point2 = pathKey3F.Point;
		catmullRomSegment3F.Point3 = pathKey3F2.Point;
		catmullRomSegment3F.Point4 = point2;
		return catmullRomSegment3F;
		IL_033b:
		if (c88a85a6fc8e0a66e19d046d4d51cd3f1 + 2 < count)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			point2 = Items[c88a85a6fc8e0a66e19d046d4d51cd3f1 + 2].Point;
		}
		else
		{
			if (SmoothEnds)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				if (PostLoop == CurveLoopType.Cycle)
				{
					point2 = Items[1].Point;
					goto IL_0465;
				}
			}
			if (SmoothEnds && PostLoop == CurveLoopType.CycleOffset)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				point2 = Items[1].Point + (Items[count - 1].Point - Items[0].Point);
			}
			else
			{
				point2 = Items[count - 1].Point + (Items[count - 1].Point - Items[count - 2].Point);
			}
		}
		goto IL_0465;
	}

	/// <summary>
	///       Gets the curve parameter for the given curve length (for length-parameterized splines).
	///       </summary>
	/// <param name="length">The length.</param>
	/// <param name="maxNumberOfIterations">
	///       The maximum number of iterations which are taken to compute the length.
	///       </param>
	/// <param name="tolerance">
	///       The tolerance value. This method will return an approximation of the precise parameter.
	///       The absolute error will be less than this tolerance.
	///       </param>
	/// <returns>The parameter at which the curve has the given length.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="tolerance" /> is negative or 0.
	///       </exception>
	public float GetParameterFromLength(float length, int maxNumberOfIterations, float tolerance)
	{
		if (tolerance <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9937), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9956));
		}
		int count = Count;
		if (count == 0)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Path3F.GetParameterFromLength(float, int, float));
					}
					return float.NaN;
				}
			}
		}
		float parameter = Items[0].Parameter;
		float parameter2 = Items[count - 1].Parameter;
		float num = parameter2 - parameter;
		if (length < parameter)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (PreLoop == CurveLoopType.Linear)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						return parameter - (parameter - length) / GetTangent(parameter).Length;
					}
				}
			}
		}
		if (length > parameter2)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			if (PostLoop == CurveLoopType.Linear)
			{
				return parameter2 + (length - parameter2) / GetTangent(parameter2).Length;
			}
		}
		if (Numeric.IsZero(num))
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					return parameter;
				}
			}
		}
		float num2 = LoopParameter(length);
		int num3 = GetKeyIndex(num2);
		if (num3 == count - 1)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			num3--;
		}
		ICurve<float, Vector3F> c4f3e345fccb3fcf4a426055d25c06f6c = cbd6850b7468243a597c1a8c9c9f905d0(num3);
		float parameter3 = Items[num3].Parameter;
		float parameter4 = Items[num3 + 1].Parameter;
		float num4 = parameter4 - parameter3;
		num2 -= parameter3;
		ImprovedNewtonRaphsonMethodF improvedNewtonRaphsonMethodF = new ImprovedNewtonRaphsonMethodF((float c5078b1ca2bd86333c65cb625b996b618) => c4f3e345fccb3fcf4a426055d25c06f6c.GetLength(0f, c5078b1ca2bd86333c65cb625b996b618, maxNumberOfIterations, tolerance / 10f), (float c5078b1ca2bd86333c65cb625b996b618) => c4f3e345fccb3fcf4a426055d25c06f6c.GetTangent(c5078b1ca2bd86333c65cb625b996b618).Length);
		improvedNewtonRaphsonMethodF.Epsilon = tolerance;
		improvedNewtonRaphsonMethodF.MaxNumberOfIterations = maxNumberOfIterations;
		ImprovedNewtonRaphsonMethodF improvedNewtonRaphsonMethodF2 = improvedNewtonRaphsonMethodF;
		float num5 = improvedNewtonRaphsonMethodF2.FindRoot(0f, 1f, num2);
		float num6 = Items[num3].Parameter + num5 * num4;
		if (length < parameter && PreLoop != CurveLoopType.Constant)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
				{
					int num7 = (int)((length - parameter2) / num);
					if (PreLoop == CurveLoopType.Oscillate && num7 % 2 == -1)
					{
						while (true)
						{
							switch (2)
							{
							case 0:
								break;
							default:
								return parameter + parameter - num6 + num * (float)(num7 + 1);
							}
						}
					}
					return num6 + (float)num7 * num;
				}
				}
			}
		}
		if (length > parameter2 && PostLoop != CurveLoopType.Constant)
		{
			int num8 = (int)((length - parameter) / num);
			if (PostLoop == CurveLoopType.Oscillate && num8 % 2 == 1)
			{
				return parameter2 + parameter2 - num6 + num * (float)(num8 - 1);
			}
			return num6 + (float)num8 * num;
		}
		return num6;
	}

	private XmlSchema c4525acc47775fb48b0a6cebe8279b94d()
	{
		return null;
	}

	XmlSchema IXmlSerializable.GetSchema()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c4525acc47775fb48b0a6cebe8279b94d
		return this.c4525acc47775fb48b0a6cebe8279b94d();
	}

	private void c8b04d7d6014c33f2c5a8b721942471f9(XmlReader caaf01f18f5504ef2fea70b78f73dda31)
	{
		caaf01f18f5504ef2fea70b78f73dda31.ReadStartElement();
		ReadXml(caaf01f18f5504ef2fea70b78f73dda31);
		caaf01f18f5504ef2fea70b78f73dda31.ReadStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2853));
		SmoothEnds = caaf01f18f5504ef2fea70b78f73dda31.ReadContentAsBoolean();
		caaf01f18f5504ef2fea70b78f73dda31.ReadEndElement();
		caaf01f18f5504ef2fea70b78f73dda31.ReadEndElement();
	}

	void IXmlSerializable.ReadXml(XmlReader caaf01f18f5504ef2fea70b78f73dda31)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c8b04d7d6014c33f2c5a8b721942471f9
		this.c8b04d7d6014c33f2c5a8b721942471f9(caaf01f18f5504ef2fea70b78f73dda31);
	}

	private void cffc49f26f46d23435d61021e29edc9cc(XmlWriter cf534b10109660b7bbf37871f1941822a)
	{
		WriteXml(cf534b10109660b7bbf37871f1941822a);
		cf534b10109660b7bbf37871f1941822a.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2853));
		cf534b10109660b7bbf37871f1941822a.WriteValue(SmoothEnds);
		cf534b10109660b7bbf37871f1941822a.WriteEndElement();
	}

	void IXmlSerializable.WriteXml(XmlWriter cf534b10109660b7bbf37871f1941822a)
	{
		//ILSpy generated this explicit interface implementation from .override directive in cffc49f26f46d23435d61021e29edc9cc
		this.cffc49f26f46d23435d61021e29edc9cc(cf534b10109660b7bbf37871f1941822a);
	}

	[CompilerGenerated]
	private float c54436f35b2f1a2de51f51a25884400e8(float c5078b1ca2bd86333c65cb625b996b618)
	{
		return GetTangent(c5078b1ca2bd86333c65cb625b996b618).Length;
	}
}
