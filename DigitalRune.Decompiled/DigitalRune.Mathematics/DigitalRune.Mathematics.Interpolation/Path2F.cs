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
///       Represents a 2-dimensional path that is defined by piecewise interpolation of key points 
///       (single-precision).
///       </summary>
[Serializable]
public class Path2F : PiecewiseCurveF<Vector2F, PathKey2F>, IXmlSerializable
{
	[CompilerGenerated]
	private sealed class c7c3f5cac4da45d163c67791cf38fb3dd
	{
		public ICurve<float, Vector2F> c4f3e345fccb3fcf4a426055d25c06f6c;

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

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.Path2F" /> class</summary>
	public Path2F()
	{
	}

	/// <summary>
	///       Computes a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>
	///       The curve point. (If the curve does not contain any points a vector with NaN values is
	///       returned.)
	///       </returns>
	public override Vector2F GetPoint(float parameter)
	{
		int count = Count;
		if (count == 0)
		{
			return new Vector2F(float.NaN);
		}
		float num = LoopParameter(parameter);
		PathKey2F pathKey2F = Items[0];
		PathKey2F pathKey2F2 = Items[count - 1];
		float parameter2 = pathKey2F.Parameter;
		float parameter3 = pathKey2F2.Parameter;
		if (num < parameter2)
		{
			Vector2F tangent = GetTangent(num);
			return pathKey2F.Point + tangent * (num - parameter2);
		}
		if (num > parameter3)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
				{
					if (1 == 0)
					{
						__ldtoken(Path2F.GetPoint(float));
					}
					Vector2F tangent2 = GetTangent(num);
					return pathKey2F2.Point + tangent2 * (num - parameter3);
				}
				}
			}
		}
		if (count == 1)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					return pathKey2F.Point;
				}
			}
		}
		Vector2F vector2F = cfd3d9eb09693d35ff85c0c74045c54ef(parameter);
		if (num == pathKey2F2.Parameter && Items[count - 2].Interpolation != SplineInterpolation.BSpline)
		{
			return pathKey2F2.Point + vector2F;
		}
		int num2 = GetKeyIndex(num);
		if (num2 == count - 1)
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
			num2--;
		}
		PathKey2F pathKey2F3 = Items[num2];
		PathKey2F pathKey2F4 = Items[num2 + 1];
		float parameter4 = pathKey2F3.Parameter;
		float parameter5 = pathKey2F4.Parameter;
		float num3 = parameter5 - parameter4;
		num = (num - parameter4) / num3;
		ICurve<float, Vector2F> curve = cbd6850b7468243a597c1a8c9c9f905d0(num2);
		return curve.GetPoint(num) + vector2F;
	}

	private Vector2F cfd3d9eb09693d35ff85c0c74045c54ef(float c1ee99c45b13417a935570173f4daec16)
	{
		PathKey2F pathKey2F = Items[0];
		PathKey2F pathKey2F2 = Items[Count - 1];
		float parameter = pathKey2F.Parameter;
		float parameter2 = pathKey2F2.Parameter;
		float num = parameter2 - parameter;
		Vector2F result = default;
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
				__ldtoken(Path2F.cfd3d9eb09693d35ff85c0c74045c54ef(float));
			}
			Vector2F vector2F = pathKey2F2.Point - pathKey2F.Point;
			if (c1ee99c45b13417a935570173f4daec16 < parameter && PreLoop == CurveLoopType.CycleOffset)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
					{
						int num2 = (int)((c1ee99c45b13417a935570173f4daec16 - parameter2) / num);
						return num2 * vector2F;
					}
					}
				}
			}
			if (c1ee99c45b13417a935570173f4daec16 > parameter2)
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
				if (PostLoop == CurveLoopType.CycleOffset)
				{
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
						{
							int num3 = (int)((c1ee99c45b13417a935570173f4daec16 - parameter) / num);
							return num3 * vector2F;
						}
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
	public override Vector2F GetTangent(float parameter)
	{
		int count = Count;
		if (count == 0)
		{
			return default;
		}
		PathKey2F pathKey2F = Items[0];
		PathKey2F pathKey2F2 = Items[count - 1];
		float parameter2 = pathKey2F.Parameter;
		float parameter3 = pathKey2F2.Parameter;
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
			if (1 == 0)
			{
				__ldtoken(Path2F.GetTangent(float));
			}
			if (parameter < parameter2)
			{
				goto IL_0080;
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		if (PostLoop != CurveLoopType.Constant || !(parameter > parameter3))
		{
			float num = LoopParameter(parameter);
			if (num < parameter2)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
					{
						Vector2F vector2F2;
						switch (pathKey2F.Interpolation)
						{
						case SplineInterpolation.Bezier:
							vector2F2 = (pathKey2F.Point - pathKey2F.TangentIn) * 3f;
							break;
						case SplineInterpolation.Hermite:
							vector2F2 = pathKey2F.TangentIn;
							break;
						default:
						{
							Vector2F vector2F;
							if (count <= 1)
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
								vector2F = default;
							}
							else
							{
								vector2F = cbd6850b7468243a597c1a8c9c9f905d0(0).GetTangent(0f);
							}
							vector2F2 = vector2F;
							break;
						}
						}
						float num2 = ((count > 1) ? (Items[1].Parameter - parameter2) : 0f);
						if (!(num2 > 0f))
						{
							return vector2F2;
						}
						return vector2F2 / num2;
					}
					}
				}
			}
			if (num > parameter3)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
					{
						Vector2F vector2F4;
						switch (pathKey2F2.Interpolation)
						{
						case SplineInterpolation.Bezier:
							vector2F4 = (pathKey2F2.TangentOut - pathKey2F2.Point) * 3f;
							break;
						case SplineInterpolation.Hermite:
							vector2F4 = pathKey2F2.TangentOut;
							break;
						default:
						{
							Vector2F vector2F3;
							if (count <= 1)
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
								vector2F3 = default;
							}
							else
							{
								vector2F3 = cbd6850b7468243a597c1a8c9c9f905d0(count - 2).GetTangent(1f);
							}
							vector2F4 = vector2F3;
							break;
						}
						}
						float num3 = ((count > 1) ? (parameter3 - Items[count - 2].Parameter) : 0f);
						if (!(num3 > 0f))
						{
							while (true)
							{
								switch (2)
								{
								case 0:
									break;
								default:
									return vector2F4;
								}
							}
						}
						return vector2F4 / num3;
					}
					}
				}
			}
			if (count == 1)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						if (PostLoop == CurveLoopType.Linear)
						{
							PathKey2F pathKey2F3 = pathKey2F;
							if (pathKey2F3.Interpolation == SplineInterpolation.Bezier)
							{
								return (pathKey2F3.TangentOut - pathKey2F3.Point) * 3f;
							}
							if (pathKey2F3.Interpolation == SplineInterpolation.Hermite)
							{
								while (true)
								{
									switch (2)
									{
									case 0:
										break;
									default:
										return pathKey2F3.TangentOut;
									}
								}
							}
						}
						else if (PreLoop == CurveLoopType.Linear)
						{
							PathKey2F pathKey2F4 = pathKey2F;
							if (pathKey2F4.Interpolation == SplineInterpolation.Bezier)
							{
								return (pathKey2F4.Point - pathKey2F4.TangentIn) * 3f;
							}
							if (pathKey2F4.Interpolation == SplineInterpolation.Hermite)
							{
								while (true)
								{
									switch (4)
									{
									case 0:
										break;
									default:
										return pathKey2F4.TangentIn;
									}
								}
							}
						}
						return default;
					}
				}
			}
			int num4 = GetKeyIndex(num);
			if (num4 == count - 1)
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
				num4--;
			}
			PathKey2F pathKey2F5 = Items[num4];
			PathKey2F pathKey2F6 = Items[num4 + 1];
			float parameter4 = pathKey2F5.Parameter;
			float parameter5 = pathKey2F6.Parameter;
			float num5 = parameter5 - parameter4;
			num = (num - parameter4) / num5;
			ICurve<float, Vector2F> curve = cbd6850b7468243a597c1a8c9c9f905d0(num4);
			Vector2F vector2F5 = curve.GetTangent(num) / num5;
			if (IsInMirroredOscillation(parameter))
			{
				return -vector2F5;
			}
			return vector2F5;
		}
		goto IL_0080;
		IL_0080:
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
			return 0f;
		}
		if (count == 1)
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
			if (1 == 0)
			{
				__ldtoken(Path2F.GetLength(float, float, int, float));
			}
			if (PreLoop != CurveLoopType.Linear)
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
				if (PostLoop != CurveLoopType.Linear)
				{
					return 0f;
				}
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
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Path2F.ParameterizeByLength(int, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9937), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9956));
				}
			}
		}
		int count = Count;
		if (count == 0)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					return;
				}
			}
		}
		Items[0].Parameter = 0f;
		for (int i = 1; i < count; i++)
		{
			PathKey2F pathKey2F = Items[i - 1];
			PathKey2F pathKey2F2 = Items[i];
			float length = cbd6850b7468243a597c1a8c9c9f905d0(i - 1).GetLength(0f, 1f, maxNumberOfIterations, tolerance);
			pathKey2F2.Parameter = pathKey2F.Parameter + length;
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

	private ICurve<float, Vector2F> cbd6850b7468243a597c1a8c9c9f905d0(int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		int count = Count;
		PathKey2F pathKey2F = Items[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		PathKey2F pathKey2F2 = Items[Math.Min(count - 1, c88a85a6fc8e0a66e19d046d4d51cd3f1 + 1)];
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.StepLeft)
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
					__ldtoken(Path2F.cbd6850b7468243a597c1a8c9c9f905d0(int));
				}
				StepSegment2F stepSegment2F = new StepSegment2F();
				stepSegment2F.Point1 = pathKey2F.Point;
				stepSegment2F.Point2 = pathKey2F2.Point;
				stepSegment2F.StepType = StepInterpolation.Left;
				return stepSegment2F;
			}
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.StepCentered)
		{
			StepSegment2F stepSegment2F2 = new StepSegment2F();
			stepSegment2F2.Point1 = pathKey2F.Point;
			stepSegment2F2.Point2 = pathKey2F2.Point;
			stepSegment2F2.StepType = StepInterpolation.Centered;
			return stepSegment2F2;
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.StepRight)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				StepSegment2F stepSegment2F3 = new StepSegment2F();
				stepSegment2F3.Point1 = pathKey2F.Point;
				stepSegment2F3.Point2 = pathKey2F2.Point;
				stepSegment2F3.StepType = StepInterpolation.Right;
				return stepSegment2F3;
			}
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.Linear)
		{
			LineSegment2F lineSegment2F = new LineSegment2F();
			lineSegment2F.Point1 = pathKey2F.Point;
			lineSegment2F.Point2 = pathKey2F2.Point;
			return lineSegment2F;
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.Bezier)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				BezierSegment2F bezierSegment2F = new BezierSegment2F();
				bezierSegment2F.Point1 = pathKey2F.Point;
				bezierSegment2F.ControlPoint1 = pathKey2F.TangentOut;
				bezierSegment2F.Point2 = pathKey2F2.Point;
				bezierSegment2F.ControlPoint2 = pathKey2F2.TangentIn;
				return bezierSegment2F;
			}
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.Hermite)
		{
			HermiteSegment2F hermiteSegment2F = new HermiteSegment2F();
			hermiteSegment2F.Point1 = pathKey2F.Point;
			hermiteSegment2F.Point2 = pathKey2F2.Point;
			hermiteSegment2F.Tangent1 = pathKey2F.TangentOut;
			hermiteSegment2F.Tangent2 = pathKey2F2.TangentIn;
			return hermiteSegment2F;
		}
		Vector2F point;
		if (c88a85a6fc8e0a66e19d046d4d51cd3f1 > 0)
		{
			point = Items[c88a85a6fc8e0a66e19d046d4d51cd3f1 - 1].Point;
		}
		else
		{
			if (SmoothEnds)
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
				if (PreLoop == CurveLoopType.Cycle)
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
					point = Items[count - 2].Point;
					goto IL_0339;
				}
			}
			if (SmoothEnds)
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
				if (PreLoop == CurveLoopType.CycleOffset)
				{
					point = Items[count - 2].Point - (Items[count - 1].Point - Items[0].Point);
					goto IL_0339;
				}
			}
			point = Items[0].Point - (Items[1].Point - Items[0].Point);
		}
		goto IL_0339;
		IL_0339:
		Vector2F point2;
		if (c88a85a6fc8e0a66e19d046d4d51cd3f1 + 2 < count)
		{
			point2 = Items[c88a85a6fc8e0a66e19d046d4d51cd3f1 + 2].Point;
		}
		else if (SmoothEnds && PostLoop == CurveLoopType.Cycle)
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
			point2 = Items[1].Point;
		}
		else if (SmoothEnds && PostLoop == CurveLoopType.CycleOffset)
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
			point2 = Items[1].Point + (Items[count - 1].Point - Items[0].Point);
		}
		else
		{
			point2 = Items[count - 1].Point + (Items[count - 1].Point - Items[count - 2].Point);
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.BSpline)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
				{
					BSplineSegment2F bSplineSegment2F = new BSplineSegment2F();
					bSplineSegment2F.Point1 = point;
					bSplineSegment2F.Point2 = pathKey2F.Point;
					bSplineSegment2F.Point3 = pathKey2F2.Point;
					bSplineSegment2F.Point4 = point2;
					return bSplineSegment2F;
				}
				}
			}
		}
		CatmullRomSegment2F catmullRomSegment2F = new CatmullRomSegment2F();
		catmullRomSegment2F.Point1 = point;
		catmullRomSegment2F.Point2 = pathKey2F.Point;
		catmullRomSegment2F.Point3 = pathKey2F2.Point;
		catmullRomSegment2F.Point4 = point2;
		return catmullRomSegment2F;
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
						__ldtoken(Path2F.GetParameterFromLength(float, int, float));
					}
					return float.NaN;
				}
			}
		}
		float parameter = Items[0].Parameter;
		float parameter2 = Items[count - 1].Parameter;
		float num = parameter2 - parameter;
		if (length < parameter && PreLoop == CurveLoopType.Linear)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					return parameter - (parameter - length) / GetTangent(parameter).Length;
				}
			}
		}
		if (length > parameter2)
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
			if (PostLoop == CurveLoopType.Linear)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						return parameter2 + (length - parameter2) / GetTangent(parameter2).Length;
					}
				}
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
			num3--;
		}
		ICurve<float, Vector2F> c4f3e345fccb3fcf4a426055d25c06f6c = cbd6850b7468243a597c1a8c9c9f905d0(num3);
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
			if (PreLoop != CurveLoopType.Constant)
			{
				int num7 = (int)((length - parameter2) / num);
				if (PreLoop == CurveLoopType.Oscillate)
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
					if (num7 % 2 == -1)
					{
						return parameter + parameter - num6 + num * (float)(num7 + 1);
					}
				}
				return num6 + (float)num7 * num;
			}
		}
		if (length > parameter2)
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
			if (PostLoop != CurveLoopType.Constant)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
					{
						int num8 = (int)((length - parameter) / num);
						if (PostLoop == CurveLoopType.Oscillate)
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
							if (num8 % 2 == 1)
							{
								while (true)
								{
									switch (7)
									{
									case 0:
										break;
									default:
										return parameter2 + parameter2 - num6 + num * (float)(num8 - 1);
									}
								}
							}
						}
						return num6 + (float)num8 * num;
					}
					}
				}
			}
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
