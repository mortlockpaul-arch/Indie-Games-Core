using System;
using DigitalRune.Mathematics.Analysis;
using DigitalRune.Mathematics.Interpolation;

namespace A;

internal static class cc7f9261bc8c5804611b0bf8805672619
{
	public static float cf9968addac81ba023e4dd01ab3f32848(ICurve<float, float> c1d901c405160d49c4d9f8d97ae9279c0, float cd33dfb8ff9ca8fe2a84e4c8ca660b531, int cdb4241d6538bb9fe3ad950978aa874cd, float c91bc08175ef13bb8c34b9e7a18df9235)
	{
		if (c91bc08175ef13bb8c34b9e7a18df9235 <= 0f)
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
						__ldtoken(cc7f9261bc8c5804611b0bf8805672619.cf9968addac81ba023e4dd01ab3f32848(ICurve<float, float>, float, int, float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9937), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9956));
				}
			}
		}
		ImprovedNewtonRaphsonMethodF improvedNewtonRaphsonMethodF = new ImprovedNewtonRaphsonMethodF(c1d901c405160d49c4d9f8d97ae9279c0.GetPoint, c1d901c405160d49c4d9f8d97ae9279c0.GetTangent);
		improvedNewtonRaphsonMethodF.Epsilon = c91bc08175ef13bb8c34b9e7a18df9235;
		improvedNewtonRaphsonMethodF.MaxNumberOfIterations = cdb4241d6538bb9fe3ad950978aa874cd;
		ImprovedNewtonRaphsonMethodF improvedNewtonRaphsonMethodF2 = improvedNewtonRaphsonMethodF;
		return improvedNewtonRaphsonMethodF2.FindRoot(0f, 1f, cd33dfb8ff9ca8fe2a84e4c8ca660b531);
	}
}
