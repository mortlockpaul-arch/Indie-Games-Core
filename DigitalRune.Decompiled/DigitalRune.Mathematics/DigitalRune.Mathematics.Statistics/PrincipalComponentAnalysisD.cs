using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Statistics;

/// <summary>
///       Performs a Principal Component Analysis (PCA) using the covariance method (double-precision).
///       </summary>
public class PrincipalComponentAnalysisD
{
	[CompilerGenerated]
	private MatrixD c0db00cd546adbf232e56f91682debd18;

	[CompilerGenerated]
	private VectorD c289d86c26dec8d73ea2656da3737629f;

	/// <summary>
	///       Gets the matrix of the principal components.
	///       </summary>
	public MatrixD V
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
	public VectorD Variances
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
	///       The list of data points. All points must have the same <see cref="P:DigitalRune.Mathematics.Algebra.VectorD.NumberOfElements" />.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="points" /> is empty.
	///       </exception>
	public PrincipalComponentAnalysisD(IList<VectorD> points)
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
						__ldtoken(PrincipalComponentAnalysisD._002Ector(IList<VectorD>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3057));
				}
			}
		}
		if (points.Count == 0)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3070));
		}
		MatrixD matrixA = StatisticsHelper.ComputeCovarianceMatrix(points);
		EigenvalueDecompositionD eigenvalueDecompositionD = new EigenvalueDecompositionD(matrixA);
		int numberOfElements = eigenvalueDecompositionD.RealEigenvalues.NumberOfElements;
		Variances = new VectorD(numberOfElements);
		V = new MatrixD(numberOfElements, numberOfElements);
		for (int i = 0; i < Variances.NumberOfElements; i++)
		{
			int indexOfLargestElement = eigenvalueDecompositionD.RealEigenvalues.IndexOfLargestElement;
			Variances[i] = eigenvalueDecompositionD.RealEigenvalues[indexOfLargestElement];
			V.SetColumn(i, eigenvalueDecompositionD.V.GetColumn(indexOfLargestElement));
			eigenvalueDecompositionD.RealEigenvalues[indexOfLargestElement] = double.NegativeInfinity;
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
}
