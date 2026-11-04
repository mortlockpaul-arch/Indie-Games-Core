using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Statistics;

/// <summary>
///       Performs a Principal Component Analysis (PCA) using the covariance method (single-precision).
///       </summary>
public class PrincipalComponentAnalysisF
{
	[CompilerGenerated]
	private MatrixF c0db00cd546adbf232e56f91682debd18;

	[CompilerGenerated]
	private VectorF c289d86c26dec8d73ea2656da3737629f;

	/// <summary>
	///       Gets the matrix of the principal components.
	///       </summary>
	public MatrixF V
	{
		[CompilerGenerated]
		get
		{
			return c0db00cd546adbf232e56f91682debd18;
		}
		[CompilerGenerated]
		private set
		{
			c0db00cd546adbf232e56f91682debd18 = value;
		}
	}

	/// <summary>
	///       Gets the variances.
	///       </summary>
	public VectorF Variances
	{
		[CompilerGenerated]
		get
		{
			return c289d86c26dec8d73ea2656da3737629f;
		}
		[CompilerGenerated]
		private set
		{
			c289d86c26dec8d73ea2656da3737629f = value;
		}
	}

	/// <summary>
	///       Creates the principal component analysis for the given list of points.
	///       </summary>
	/// <param name="points">
	///       The list of data points. All points must have the same <see cref="P:DigitalRune.Mathematics.Algebra.VectorF.NumberOfElements" />.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="points" /> is empty.
	///       </exception>
	public PrincipalComponentAnalysisF(IList<VectorF> points)
	{
		if (points == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3057));
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
					if (1 == 0)
					{
						__ldtoken(PrincipalComponentAnalysisF._002Ector(IList<VectorF>));
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3070));
				}
			}
		}
		MatrixF matrixA = StatisticsHelper.ComputeCovarianceMatrix(points);
		EigenvalueDecompositionF eigenvalueDecompositionF = new EigenvalueDecompositionF(matrixA);
		int numberOfElements = eigenvalueDecompositionF.RealEigenvalues.NumberOfElements;
		Variances = new VectorF(numberOfElements);
		V = new MatrixF(numberOfElements, numberOfElements);
		for (int i = 0; i < Variances.NumberOfElements; i++)
		{
			int indexOfLargestElement = eigenvalueDecompositionF.RealEigenvalues.IndexOfLargestElement;
			Variances[i] = eigenvalueDecompositionF.RealEigenvalues[indexOfLargestElement];
			V.SetColumn(i, eigenvalueDecompositionF.V.GetColumn(indexOfLargestElement));
			eigenvalueDecompositionF.RealEigenvalues[indexOfLargestElement] = float.NegativeInfinity;
		}
	}
}
