using System;
using System.Collections.Generic;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Statistics;

/// <summary>
///       Provides helper methods for statistical tasks.
///       </summary>
public static class StatisticsHelper
{
	/// <summary>
	///       Computes the covariance matrix for a list of 3-dimensional points (single-precision).
	///       </summary>
	/// <param name="points">The points.</param>
	/// <returns>The covariance matrix.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	public static Matrix33F ComputeCovarianceMatrix(IList<Vector3F> points)
	{
		if (points == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3057));
		}
		int count = points.Count;
		float num = 1f / (float)count;
		Vector3F zero = Vector3F.Zero;
		for (int i = 0; i < count; i++)
		{
			zero += points[i];
		}
		zero *= num;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		for (int j = 0; j < count; j++)
		{
			Vector3F vector3F = points[j] - zero;
			num2 += vector3F.X * vector3F.X;
			num3 += vector3F.Y * vector3F.Y;
			num4 += vector3F.Z * vector3F.Z;
			num5 += vector3F.X * vector3F.Y;
			num6 += vector3F.X * vector3F.Z;
			num7 += vector3F.Y * vector3F.Z;
		}
		num2 *= num;
		num3 *= num;
		num4 *= num;
		num5 *= num;
		num6 *= num;
		num7 *= num;
		return new Matrix33F(num2, num5, num6, num5, num3, num7, num6, num7, num4);
	}

	/// <summary>
	///       Computes the covariance matrix for a list of 3-dimensional points (double-precision).
	///       </summary>
	/// <param name="points">The points.</param>
	/// <returns>The covariance matrix.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	public static Matrix33D ComputeCovarianceMatrix(IList<Vector3D> points)
	{
		if (points == null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(StatisticsHelper.ComputeCovarianceMatrix(IList<Vector3D>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3057));
				}
			}
		}
		int count = points.Count;
		float num = 1f / (float)count;
		Vector3D zero = Vector3D.Zero;
		for (int i = 0; i < count; i++)
		{
			zero += points[i];
		}
		zero *= (double)num;
		double num2 = 0.0;
		double num3 = 0.0;
		double num4 = 0.0;
		double num5 = 0.0;
		double num6 = 0.0;
		double num7 = 0.0;
		for (int j = 0; j < count; j++)
		{
			Vector3D vector3D = points[j] - zero;
			num2 += vector3D.X * vector3D.X;
			num3 += vector3D.Y * vector3D.Y;
			num4 += vector3D.Z * vector3D.Z;
			num5 += vector3D.X * vector3D.Y;
			num6 += vector3D.X * vector3D.Z;
			num7 += vector3D.Y * vector3D.Z;
		}
		num2 *= (double)num;
		num3 *= (double)num;
		num4 *= (double)num;
		num5 *= (double)num;
		num6 *= (double)num;
		num7 *= (double)num;
		return new Matrix33D(num2, num5, num6, num5, num3, num7, num6, num7, num4);
	}

	/// <summary>
	///       Computes the covariance matrix for a list of n-dimensional points (single-precision).
	///       </summary>
	/// <param name="points">
	///       The points. All points must have the same <see cref="P:DigitalRune.Mathematics.Algebra.VectorF.NumberOfElements" />.
	///       </param>
	/// <returns>The covariance matrix.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="points" /> is empty.
	///       </exception>
	public static MatrixF ComputeCovarianceMatrix(IList<VectorF> points)
	{
		if (points == null)
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
						__ldtoken(StatisticsHelper.ComputeCovarianceMatrix(IList<VectorF>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3057));
				}
			}
		}
		if (points.Count == 0)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3070));
				}
			}
		}
		int numberOfElements = points[0].NumberOfElements;
		int count = points.Count;
		float num = 1f / (float)count;
		VectorF vectorF = new VectorF(numberOfElements);
		for (int i = 0; i < count; i++)
		{
			if (points[i].NumberOfElements != numberOfElements)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
						throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3127));
					}
				}
			}
			vectorF += points[i];
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			vectorF *= num;
			MatrixF matrixF = new MatrixF(numberOfElements, numberOfElements);
			for (int j = 0; j < count; j++)
			{
				VectorF vectorF2 = points[j] - vectorF;
				for (int k = 0; k < numberOfElements; k++)
				{
					for (int l = k; l < numberOfElements; l++)
					{
						matrixF[k, l] += vectorF2[k] * vectorF2[l];
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							goto end_IL_013d;
						}
						continue;
						end_IL_013d:
						break;
					}
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						goto end_IL_0152;
					}
					continue;
					end_IL_0152:
					break;
				}
			}
			for (int m = 0; m < numberOfElements; m++)
			{
				for (int n = m; n < numberOfElements; n++)
				{
					matrixF[m, n] *= num;
				}
			}
			for (int num2 = 0; num2 < numberOfElements; num2++)
			{
				for (int num3 = num2 + 1; num3 < numberOfElements; num3++)
				{
					matrixF[num3, num2] = matrixF[num2, num3];
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						goto end_IL_01da;
					}
					continue;
					end_IL_01da:
					break;
				}
			}
			return matrixF;
		}
	}

	/// <summary>
	///       Computes the covariance matrix for a list of n-dimensional points (double-precision).
	///       </summary>
	/// <param name="points">
	///       The points. All points must have the same <see cref="P:DigitalRune.Mathematics.Algebra.VectorF.NumberOfElements" />.
	///       </param>
	/// <returns>The covariance matrix.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="points" /> is empty.
	///       </exception>
	public static MatrixD ComputeCovarianceMatrix(IList<VectorD> points)
	{
		if (points == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(StatisticsHelper.ComputeCovarianceMatrix(IList<VectorD>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3057));
				}
			}
		}
		if (points.Count == 0)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3070));
				}
			}
		}
		int numberOfElements = points[0].NumberOfElements;
		int count = points.Count;
		float num = 1f / (float)count;
		VectorD vectorD = new VectorD(numberOfElements);
		for (int i = 0; i < count; i++)
		{
			if (points[i].NumberOfElements != numberOfElements)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3127));
					}
				}
			}
			vectorD += points[i];
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			vectorD *= (double)num;
			MatrixD matrixD = new MatrixD(numberOfElements, numberOfElements);
			for (int j = 0; j < count; j++)
			{
				if (points[j].NumberOfElements != numberOfElements)
				{
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3127));
				}
				VectorD vectorD2 = points[j] - vectorD;
				for (int k = 0; k < numberOfElements; k++)
				{
					for (int l = k; l < numberOfElements; l++)
					{
						matrixD[k, l] += vectorD2[k] * vectorD2[l];
					}
				}
			}
			for (int m = 0; m < numberOfElements; m++)
			{
				for (int n = m; n < numberOfElements; n++)
				{
					matrixD[m, n] *= num;
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						goto end_IL_01b1;
					}
					continue;
					end_IL_01b1:
					break;
				}
			}
			for (int num2 = 0; num2 < numberOfElements; num2++)
			{
				for (int num3 = num2 + 1; num3 < numberOfElements; num3++)
				{
					matrixD[num3, num2] = matrixD[num2, num3];
				}
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				return matrixD;
			}
		}
	}
}
