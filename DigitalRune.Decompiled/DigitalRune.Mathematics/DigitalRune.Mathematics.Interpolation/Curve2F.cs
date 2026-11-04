using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Represents a 2-dimensional curve that is used to define a function <i>y = f(x)</i> 
///       (single-precision).
///       </summary>
[Serializable]
public class Curve2F : PiecewiseCurveF<Vector2F, CurveKey2F>, IXmlSerializable
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.Curve2F" /> class</summary>
	public Curve2F()
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
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Curve2F.GetPoint(float));
				}
				return new Vector2F(float.NaN, float.NaN);
			}
		}
		CurveKey2F curveKey2F = Items[0];
		CurveKey2F curveKey2F2 = Items[count - 1];
		float parameter2 = curveKey2F.Parameter;
		float parameter3 = curveKey2F2.Parameter;
		float num = LoopParameter(parameter);
		ICurve<float, float> c08a02561d01419abe643136fa079f0ef;
		ICurve<float, float> c5ad1338c784ba87fb08f9cd;
		if (num < parameter2)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				Vector2F vector2F;
				switch (curveKey2F.Interpolation)
				{
				case SplineInterpolation.Bezier:
					vector2F = (curveKey2F.Point - curveKey2F.TangentIn) * 3f;
					break;
				case SplineInterpolation.Hermite:
					vector2F = curveKey2F.TangentIn;
					break;
				default:
					if (count == 1)
					{
						return new Vector2F(parameter, curveKey2F.Point.Y);
					}
					caa753565dae0637f1b0ef3c886402e3d(0, out c08a02561d01419abe643136fa079f0ef, out c5ad1338c784ba87fb08f9cd);
					vector2F = new Vector2F(c08a02561d01419abe643136fa079f0ef.GetTangent(0f), c5ad1338c784ba87fb08f9cd.GetTangent(0f));
					break;
				}
				float num2 = 0f;
				if (!Numeric.IsZero(vector2F.X))
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
					num2 = vector2F.Y / vector2F.X;
				}
				return new Vector2F(parameter, curveKey2F.Point.Y + num2 * (num - parameter2));
			}
		}
		if (num > parameter3)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				Vector2F vector2F2;
				switch (curveKey2F2.Interpolation)
				{
				case SplineInterpolation.Bezier:
					vector2F2 = (curveKey2F2.TangentOut - curveKey2F2.Point) * 3f;
					break;
				case SplineInterpolation.Hermite:
					vector2F2 = curveKey2F2.TangentOut;
					break;
				default:
					if (count == 1)
					{
						return new Vector2F(parameter, curveKey2F.Point.Y);
					}
					caa753565dae0637f1b0ef3c886402e3d(count - 2, out c08a02561d01419abe643136fa079f0ef, out c5ad1338c784ba87fb08f9cd);
					vector2F2 = new Vector2F(c08a02561d01419abe643136fa079f0ef.GetTangent(1f), c5ad1338c784ba87fb08f9cd.GetTangent(1f));
					break;
				}
				float num3 = 0f;
				if (!Numeric.IsZero(vector2F2.X))
				{
					num3 = vector2F2.Y / vector2F2.X;
				}
				return new Vector2F(parameter, curveKey2F2.Point.Y + num3 * (num - parameter3));
			}
		}
		if (count == 1)
		{
			return new Vector2F(parameter, curveKey2F.Point.Y);
		}
		float num4 = cfd3d9eb09693d35ff85c0c74045c54ef(parameter);
		if (num == curveKey2F2.Parameter && Items[count - 2].Interpolation != SplineInterpolation.BSpline)
		{
			return new Vector2F(parameter, curveKey2F2.Point.Y + num4);
		}
		int num5 = GetKeyIndex(num);
		if (num5 == count - 1)
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
			num5--;
		}
		CurveKey2F curveKey2F3 = Items[num5];
		CurveKey2F curveKey2F4 = Items[num5 + 1];
		float parameter4 = curveKey2F3.Parameter;
		float parameter5 = curveKey2F4.Parameter;
		float num6 = parameter5 - parameter4;
		caa753565dae0637f1b0ef3c886402e3d(num5, out c08a02561d01419abe643136fa079f0ef, out c5ad1338c784ba87fb08f9cd);
		float num7 = 0f;
		if (!Numeric.IsZero(num6))
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
			if (Items[num5].Interpolation != SplineInterpolation.Bezier)
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
				if (Items[num5].Interpolation != SplineInterpolation.BSpline && Items[num5].Interpolation != SplineInterpolation.CatmullRom)
				{
					if (Items[num5].Interpolation != SplineInterpolation.Hermite)
					{
						num7 = (num - parameter4) / num6;
						goto IL_049d;
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
			}
			float c91bc08175ef13bb8c34b9e7a18df = Math.Max(Math.Abs(num6) * Numeric.EpsilonF, Numeric.EpsilonF);
			num7 = cc7f9261bc8c5804611b0bf8805672619.cf9968addac81ba023e4dd01ab3f32848(c08a02561d01419abe643136fa079f0ef, num, 20, c91bc08175ef13bb8c34b9e7a18df);
			if (Numeric.IsNaN(num7) && Items[num5].Interpolation == SplineInterpolation.BSpline)
			{
				if (num5 - 1 >= 0 && Items[num5 - 1].Interpolation == SplineInterpolation.BSpline)
				{
					caa753565dae0637f1b0ef3c886402e3d(num5 - 1, out c08a02561d01419abe643136fa079f0ef, out c5ad1338c784ba87fb08f9cd);
					num7 = cc7f9261bc8c5804611b0bf8805672619.cf9968addac81ba023e4dd01ab3f32848(c08a02561d01419abe643136fa079f0ef, num, 20, c91bc08175ef13bb8c34b9e7a18df);
				}
				if (Numeric.IsNaN(num7))
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
					if (num5 + 1 < count - 1 && Items[num5 + 1].Interpolation == SplineInterpolation.BSpline)
					{
						caa753565dae0637f1b0ef3c886402e3d(num5 + 1, out c08a02561d01419abe643136fa079f0ef, out c5ad1338c784ba87fb08f9cd);
						num7 = cc7f9261bc8c5804611b0bf8805672619.cf9968addac81ba023e4dd01ab3f32848(c08a02561d01419abe643136fa079f0ef, num, 20, c91bc08175ef13bb8c34b9e7a18df);
					}
				}
			}
		}
		goto IL_049d;
		IL_049d:
		float point = c5ad1338c784ba87fb08f9cd.GetPoint(num7);
		return new Vector2F(parameter, point + num4);
	}

	private float cfd3d9eb09693d35ff85c0c74045c54ef(float c1ee99c45b13417a935570173f4daec16)
	{
		CurveKey2F curveKey2F = Items[0];
		CurveKey2F curveKey2F2 = Items[Count - 1];
		float parameter = curveKey2F.Parameter;
		float parameter2 = curveKey2F2.Parameter;
		float num = parameter2 - parameter;
		float result = 0f;
		if (!Numeric.IsZero(num))
		{
			float num2 = curveKey2F2.Point.Y - curveKey2F.Point.Y;
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
				if (1 == 0)
				{
					__ldtoken(Curve2F.cfd3d9eb09693d35ff85c0c74045c54ef(float));
				}
				if (PreLoop == CurveLoopType.CycleOffset)
				{
					int num3 = (int)((c1ee99c45b13417a935570173f4daec16 - parameter2) / num);
					result = (float)num3 * num2;
					goto IL_00b6;
				}
			}
			if (c1ee99c45b13417a935570173f4daec16 > parameter2 && PostLoop == CurveLoopType.CycleOffset)
			{
				int num4 = (int)((c1ee99c45b13417a935570173f4daec16 - parameter) / num);
				result = (float)num4 * num2;
			}
		}
		goto IL_00b6;
		IL_00b6:
		return result;
	}

	/// <summary>
	///       Computes the tangent for a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>
	///       The curve tangent. If the function is not differentiable at the given parameter, either the 
	///       incoming or outgoing tangent is returned.
	///       </returns>
	public override Vector2F GetTangent(float parameter)
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
						__ldtoken(Curve2F.GetTangent(float));
					}
					return default;
				}
			}
		}
		CurveKey2F curveKey2F = Items[0];
		CurveKey2F curveKey2F2 = Items[count - 1];
		float parameter2 = curveKey2F.Parameter;
		float parameter3 = curveKey2F2.Parameter;
		ICurve<float, float> c08a02561d01419abe643136fa079f0ef;
		ICurve<float, float> c5ad1338c784ba87fb08f9cd;
		float num6;
		float num7;
		if (PreLoop != CurveLoopType.Constant || !(parameter < parameter2))
		{
			if (PostLoop != CurveLoopType.Constant || !(parameter > parameter3))
			{
				float num = LoopParameter(parameter);
				if (num < parameter2)
				{
					Vector2F vector2F;
					switch (curveKey2F.Interpolation)
					{
					case SplineInterpolation.Bezier:
						vector2F = (curveKey2F.Point - curveKey2F.TangentIn) * 3f;
						break;
					case SplineInterpolation.Hermite:
						vector2F = curveKey2F.TangentIn;
						break;
					case SplineInterpolation.StepLeft:
					case SplineInterpolation.StepCentered:
					case SplineInterpolation.StepRight:
						vector2F = Vector2F.UnitX;
						break;
					default:
						if (count > 1)
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
							caa753565dae0637f1b0ef3c886402e3d(0, out c08a02561d01419abe643136fa079f0ef, out c5ad1338c784ba87fb08f9cd);
							vector2F = new Vector2F(c08a02561d01419abe643136fa079f0ef.GetTangent(0f), c5ad1338c784ba87fb08f9cd.GetTangent(0f));
						}
						else
						{
							vector2F = Vector2F.UnitX;
						}
						break;
					}
					float num2;
					if (count <= 1)
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
						num2 = 0f;
					}
					else
					{
						num2 = Items[1].Parameter - parameter2;
					}
					float num3 = num2;
					if (!(num3 > 0f))
					{
						while (true)
						{
							switch (5)
							{
							case 0:
								break;
							default:
								return vector2F;
							}
						}
					}
					return vector2F / num3;
				}
				if (num > parameter3)
				{
					Vector2F vector2F2;
					switch (curveKey2F2.Interpolation)
					{
					case SplineInterpolation.Bezier:
						vector2F2 = (curveKey2F2.TangentOut - curveKey2F2.Point) * 3f;
						break;
					case SplineInterpolation.Hermite:
						vector2F2 = curveKey2F2.TangentOut;
						break;
					case SplineInterpolation.StepLeft:
					case SplineInterpolation.StepCentered:
					case SplineInterpolation.StepRight:
						vector2F2 = Vector2F.UnitX;
						break;
					default:
						if (count > 1)
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
							caa753565dae0637f1b0ef3c886402e3d(count - 2, out c08a02561d01419abe643136fa079f0ef, out c5ad1338c784ba87fb08f9cd);
							vector2F2 = new Vector2F(c08a02561d01419abe643136fa079f0ef.GetTangent(1f), c5ad1338c784ba87fb08f9cd.GetTangent(1f));
						}
						else
						{
							vector2F2 = Vector2F.UnitX;
						}
						break;
					}
					float num4 = ((count > 1) ? (parameter3 - Items[count - 2].Parameter) : 0f);
					if (!(num4 > 0f))
					{
						while (true)
						{
							switch (1)
							{
							case 0:
								break;
							default:
								return vector2F2;
							}
						}
					}
					return vector2F2 / num4;
				}
				if (count == 1)
				{
					if (PostLoop == CurveLoopType.Linear)
					{
						CurveKey2F curveKey2F3 = curveKey2F;
						if (curveKey2F3.Interpolation == SplineInterpolation.Bezier)
						{
							return (curveKey2F3.TangentOut - curveKey2F3.Point) * 3f;
						}
						if (curveKey2F3.Interpolation == SplineInterpolation.Hermite)
						{
							while (true)
							{
								switch (2)
								{
								case 0:
									break;
								default:
									return curveKey2F3.TangentOut;
								}
							}
						}
					}
					else if (PreLoop == CurveLoopType.Linear)
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
						CurveKey2F curveKey2F4 = curveKey2F;
						if (curveKey2F4.Interpolation == SplineInterpolation.Bezier)
						{
							while (true)
							{
								switch (5)
								{
								case 0:
									continue;
								}
								return (curveKey2F4.Point - curveKey2F4.TangentIn) * 3f;
							}
						}
						if (curveKey2F4.Interpolation == SplineInterpolation.Hermite)
						{
							while (true)
							{
								switch (5)
								{
								case 0:
									break;
								default:
									return curveKey2F4.TangentIn;
								}
							}
						}
					}
					return Vector2F.UnitX;
				}
				int num5 = GetKeyIndex(num);
				if (num5 == count - 1)
				{
					num5--;
				}
				CurveKey2F curveKey2F5 = Items[num5];
				CurveKey2F curveKey2F6 = Items[num5 + 1];
				if (curveKey2F5.Interpolation != SplineInterpolation.StepLeft && curveKey2F5.Interpolation != SplineInterpolation.StepCentered)
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
					if (curveKey2F5.Interpolation != SplineInterpolation.StepRight)
					{
						float parameter4 = curveKey2F5.Parameter;
						float parameter5 = curveKey2F6.Parameter;
						num6 = parameter5 - parameter4;
						caa753565dae0637f1b0ef3c886402e3d(num5, out c08a02561d01419abe643136fa079f0ef, out c5ad1338c784ba87fb08f9cd);
						num7 = 0f;
						if (!Numeric.IsZero(num6))
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
							if (Items[num5].Interpolation != SplineInterpolation.Bezier && Items[num5].Interpolation != SplineInterpolation.BSpline && Items[num5].Interpolation != SplineInterpolation.CatmullRom)
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
								if (Items[num5].Interpolation != SplineInterpolation.Hermite)
								{
									num7 = (num - parameter4) / num6;
									goto IL_05da;
								}
								while (true)
								{
									switch (6)
									{
									case 0:
										continue;
									}
									break;
								}
							}
							num7 = cc7f9261bc8c5804611b0bf8805672619.cf9968addac81ba023e4dd01ab3f32848(c08a02561d01419abe643136fa079f0ef, num, 20, Math.Max(Math.Abs(num6) * Numeric.EpsilonF, Numeric.EpsilonF));
							if (Numeric.IsNaN(num7))
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
								if (Items[num5].Interpolation == SplineInterpolation.BSpline)
								{
									if (num5 - 1 >= 0)
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
										if (Items[num5 - 1].Interpolation == SplineInterpolation.BSpline)
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
											caa753565dae0637f1b0ef3c886402e3d(num5 - 1, out c08a02561d01419abe643136fa079f0ef, out c5ad1338c784ba87fb08f9cd);
											num7 = cc7f9261bc8c5804611b0bf8805672619.cf9968addac81ba023e4dd01ab3f32848(c08a02561d01419abe643136fa079f0ef, num, 20, Math.Max(Math.Abs(num6) * Numeric.EpsilonF, Numeric.EpsilonF));
										}
									}
									if (Numeric.IsNaN(num7) && num5 + 1 < count - 1 && Items[num5 + 1].Interpolation == SplineInterpolation.BSpline)
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
										caa753565dae0637f1b0ef3c886402e3d(num5 + 1, out c08a02561d01419abe643136fa079f0ef, out c5ad1338c784ba87fb08f9cd);
										num7 = cc7f9261bc8c5804611b0bf8805672619.cf9968addac81ba023e4dd01ab3f32848(c08a02561d01419abe643136fa079f0ef, num, 20, Math.Max(Math.Abs(num6) * Numeric.EpsilonF, Numeric.EpsilonF));
									}
								}
							}
						}
						goto IL_05da;
					}
				}
				return Vector2F.UnitX;
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		return Vector2F.UnitX;
		IL_05da:
		float tangent = c08a02561d01419abe643136fa079f0ef.GetTangent(num7);
		float num8 = c5ad1338c784ba87fb08f9cd.GetTangent(num7);
		if (IsInMirroredOscillation(parameter))
		{
			num8 = 0f - num8;
		}
		return new Vector2F(tangent, num8) / num6;
	}

	/// <summary>
	///       Not supported.
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
	/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
	public override float GetLength(float start, float end, int maxNumberOfIterations, float tolerance)
	{
		throw new NotSupportedException();
	}

	private void caa753565dae0637f1b0ef3c886402e3d(int c88a85a6fc8e0a66e19d046d4d51cd3f1, out ICurve<float, float> c08a02561d01419abe643136fa079f0ef, out ICurve<float, float> c5ad1338c784ba87fb08f9cd395883391)
	{
		CurveKey2F curveKey2F = Items[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		CurveKey2F curveKey2F2 = Items[Math.Min(Count - 1, c88a85a6fc8e0a66e19d046d4d51cd3f1 + 1)];
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.StepLeft)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Curve2F.caa753565dae0637f1b0ef3c886402e3d(int, ref ICurve<float, float>, ref ICurve<float, float>));
				}
				c08a02561d01419abe643136fa079f0ef = new StepSegment1F
				{
					Point1 = curveKey2F.Point.X,
					Point2 = curveKey2F2.Point.X,
					StepType = StepInterpolation.Left
				};
				c5ad1338c784ba87fb08f9cd395883391 = new StepSegment1F
				{
					Point1 = curveKey2F.Point.Y,
					Point2 = curveKey2F2.Point.Y,
					StepType = StepInterpolation.Left
				};
				return;
			}
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.StepCentered)
		{
			c08a02561d01419abe643136fa079f0ef = new StepSegment1F
			{
				Point1 = curveKey2F.Point.X,
				Point2 = curveKey2F2.Point.X,
				StepType = StepInterpolation.Centered
			};
			c5ad1338c784ba87fb08f9cd395883391 = new StepSegment1F
			{
				Point1 = curveKey2F.Point.Y,
				Point2 = curveKey2F2.Point.Y,
				StepType = StepInterpolation.Centered
			};
			return;
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.StepRight)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				c08a02561d01419abe643136fa079f0ef = new StepSegment1F
				{
					Point1 = curveKey2F.Point.X,
					Point2 = curveKey2F2.Point.X,
					StepType = StepInterpolation.Right
				};
				c5ad1338c784ba87fb08f9cd395883391 = new StepSegment1F
				{
					Point1 = curveKey2F.Point.Y,
					Point2 = curveKey2F2.Point.Y,
					StepType = StepInterpolation.Right
				};
				return;
			}
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.Linear)
		{
			c08a02561d01419abe643136fa079f0ef = new LineSegment1F
			{
				Point1 = curveKey2F.Point.X,
				Point2 = curveKey2F2.Point.X
			};
			c5ad1338c784ba87fb08f9cd395883391 = new LineSegment1F
			{
				Point1 = curveKey2F.Point.Y,
				Point2 = curveKey2F2.Point.Y
			};
			return;
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
				c08a02561d01419abe643136fa079f0ef = new BezierSegment1F
				{
					Point1 = curveKey2F.Point.X,
					ControlPoint1 = curveKey2F.TangentOut.X,
					Point2 = curveKey2F2.Point.X,
					ControlPoint2 = curveKey2F2.TangentIn.X
				};
				c5ad1338c784ba87fb08f9cd395883391 = new BezierSegment1F
				{
					Point1 = curveKey2F.Point.Y,
					ControlPoint1 = curveKey2F.TangentOut.Y,
					Point2 = curveKey2F2.Point.Y,
					ControlPoint2 = curveKey2F2.TangentIn.Y
				};
				return;
			}
		}
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.Hermite)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				c08a02561d01419abe643136fa079f0ef = new HermiteSegment1F
				{
					Point1 = curveKey2F.Point.X,
					Point2 = curveKey2F2.Point.X,
					Tangent1 = curveKey2F.TangentOut.X,
					Tangent2 = curveKey2F2.TangentIn.X
				};
				c5ad1338c784ba87fb08f9cd395883391 = new HermiteSegment1F
				{
					Point1 = curveKey2F.Point.Y,
					Point2 = curveKey2F2.Point.Y,
					Tangent1 = curveKey2F.TangentOut.Y,
					Tangent2 = curveKey2F2.TangentIn.Y
				};
				return;
			}
		}
		Vector2F vector2F;
		if (c88a85a6fc8e0a66e19d046d4d51cd3f1 > 0)
		{
			vector2F = Items[c88a85a6fc8e0a66e19d046d4d51cd3f1 - 1].Point;
		}
		else if (SmoothEnds && PreLoop == CurveLoopType.Constant && Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.CatmullRom)
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
			vector2F = new Vector2F(Items[0].Point.X - (Items[1].Point.X - Items[0].Point.X), curveKey2F.Point.Y);
		}
		else if (SmoothEnds && PreLoop == CurveLoopType.Cycle)
		{
			vector2F = Items[Count - 2].Point;
			vector2F.X -= Items[Count - 1].Point.X - Items[0].Point.X;
		}
		else
		{
			if (SmoothEnds)
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
				if (PreLoop == CurveLoopType.CycleOffset)
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
					vector2F = Items[Count - 2].Point - (Items[Count - 1].Point - Items[0].Point);
					goto IL_068c;
				}
			}
			vector2F = ((!SmoothEnds || PreLoop != CurveLoopType.Oscillate) ? (Items[0].Point - (Items[1].Point - Items[0].Point)) : new Vector2F(Items[0].Point.X - (Items[1].Point.X - Items[0].Point.X), curveKey2F2.Point.Y));
		}
		goto IL_068c;
		IL_096b:
		Vector2F vector2F2;
		if (Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.BSpline)
		{
			c08a02561d01419abe643136fa079f0ef = new BSplineSegment1F
			{
				Point1 = vector2F.X,
				Point2 = curveKey2F.Point.X,
				Point3 = curveKey2F2.Point.X,
				Point4 = vector2F2.X
			};
			c5ad1338c784ba87fb08f9cd395883391 = new BSplineSegment1F
			{
				Point1 = vector2F.Y,
				Point2 = curveKey2F.Point.Y,
				Point3 = curveKey2F2.Point.Y,
				Point4 = vector2F2.Y
			};
		}
		else
		{
			c08a02561d01419abe643136fa079f0ef = new CatmullRomSegment1F
			{
				Point1 = vector2F.X,
				Point2 = curveKey2F.Point.X,
				Point3 = curveKey2F2.Point.X,
				Point4 = vector2F2.X
			};
			c5ad1338c784ba87fb08f9cd395883391 = new CatmullRomSegment1F
			{
				Point1 = vector2F.Y,
				Point2 = curveKey2F.Point.Y,
				Point3 = curveKey2F2.Point.Y,
				Point4 = vector2F2.Y
			};
		}
		return;
		IL_068c:
		if (c88a85a6fc8e0a66e19d046d4d51cd3f1 + 2 < Count)
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
			vector2F2 = Items[c88a85a6fc8e0a66e19d046d4d51cd3f1 + 2].Point;
		}
		else
		{
			if (SmoothEnds)
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
				if (PostLoop == CurveLoopType.Constant && Items[c88a85a6fc8e0a66e19d046d4d51cd3f1].Interpolation == SplineInterpolation.CatmullRom)
				{
					vector2F2 = new Vector2F(Items[Count - 1].Point.X + (Items[Count - 1].Point.X - Items[Count - 2].Point.X), curveKey2F2.Point.Y);
					goto IL_096b;
				}
			}
			if (SmoothEnds)
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
				if (PostLoop == CurveLoopType.Cycle)
				{
					vector2F2 = Items[1].Point;
					vector2F2.X += Items[Count - 1].Point.X - Items[0].Point.X;
					goto IL_096b;
				}
			}
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
				if (PostLoop == CurveLoopType.CycleOffset)
				{
					vector2F2 = Items[1].Point + (Items[Count - 1].Point - Items[0].Point);
					goto IL_096b;
				}
			}
			if (SmoothEnds && PostLoop == CurveLoopType.Oscillate)
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
				vector2F2 = new Vector2F(Items[Count - 1].Point.X + (Items[Count - 1].Point.X - Items[Count - 2].Point.X), curveKey2F.Point.Y);
			}
			else
			{
				vector2F2 = Items[Count - 1].Point + (Items[Count - 1].Point - Items[Count - 2].Point);
			}
		}
		goto IL_096b;
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
}
