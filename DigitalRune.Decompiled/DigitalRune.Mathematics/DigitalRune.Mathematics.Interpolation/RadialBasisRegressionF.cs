using System;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Scattered Interpolation using multiple regression analysis with radial basis functions
///       (single-precision).
///       </summary>
public class RadialBasisRegressionF : ScatteredInterpolationF
{
	private VectorF[] ca6072a768e3ec26bfbe5aa7917058914;

	private Func<float, int, float> c24176c0f6dd8be9f3d5b711723d5b45a = (float c5078b1ca2bd86333c65cb625b996b618, int cdeb496a0c3d046b6bc838defe2d220d0) => MathHelper.Gaussian(c5078b1ca2bd86333c65cb625b996b618, 1f, 0f, 1f);

	private Func<VectorF, VectorF, int, float> c6a6cb7522e7e87d98b4e0e79cc5e8673;

	[CompilerGenerated]
	private static Func<float, int, float> c2cd722d7de416d829aaf20a315c6553b;

	[CompilerGenerated]
	private static Func<VectorF, VectorF, int, float> c19ae5197e8f1d77c5b9a5a5a3cba33d3;

	/// <summary>
	///       Gets or sets the basis function.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public Func<float, int, float> BasisFunction
	{
		get
		{
			return c24176c0f6dd8be9f3d5b711723d5b45a;
		}
		set
		{
			if (value == null)
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
							__ldtoken(RadialBasisRegressionF.set_BasisFunction(Func<float, int, float>));
						}
						throw new ArgumentNullException();
					}
				}
			}
			c24176c0f6dd8be9f3d5b711723d5b45a = value;
		}
	}

	/// <summary>
	///       Gets or sets the distance function.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public Func<VectorF, VectorF, int, float> DistanceFunction
	{
		get
		{
			return c6a6cb7522e7e87d98b4e0e79cc5e8673;
		}
		set
		{
			if (value == null)
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
							__ldtoken(RadialBasisRegressionF.set_DistanceFunction(Func<VectorF, VectorF, int, float>));
						}
						throw new ArgumentNullException();
					}
				}
			}
			c6a6cb7522e7e87d98b4e0e79cc5e8673 = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.RadialBasisRegressionF" /> class</summary>
	public RadialBasisRegressionF()
	{
		if (c19ae5197e8f1d77c5b9a5a5a3cba33d3 == null)
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
				__ldtoken(RadialBasisRegressionF._002Ector());
			}
			c19ae5197e8f1d77c5b9a5a5a3cba33d3 = (VectorF c5078b1ca2bd86333c65cb625b996b618, VectorF c125eeb3fef00e99f8e81684cfeaa9bd2, int cdeb496a0c3d046b6bc838defe2d220d0) => (c5078b1ca2bd86333c65cb625b996b618 - c125eeb3fef00e99f8e81684cfeaa9bd2).Length;
		}
		c6a6cb7522e7e87d98b4e0e79cc5e8673 = c19ae5197e8f1d77c5b9a5a5a3cba33d3;
		base._002Ector();
	}

	/// <summary>
	///       Called when <see cref="M:DigitalRune.Mathematics.Interpolation.ScatteredInterpolationF.Setup" /> is called.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       Cannot compute regression - try to choose different reference data pairs or another basis 
	///       function.
	///       </exception>
	protected override void OnSetup()
	{
		int count = Count;
		MatrixF matrixF = new MatrixF(count, count);
		int num = 0;
		while (num < count)
		{
			for (int i = 0; i < count; i++)
			{
				float arg = DistanceFunction(c53720e3d9bbf70b21bdaa6ec34cfd6b0(num), c53720e3d9bbf70b21bdaa6ec34cfd6b0(i), i);
				matrixF[num, i] = BasisFunction(arg, i);
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(RadialBasisRegressionF.OnSetup());
				}
				num++;
				break;
			}
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			MatrixF matrixF2 = new MatrixF(count, cee44eaa2fcfe0f22bd650b561025cbd7(0).NumberOfElements);
			for (int j = 0; j < count; j++)
			{
				matrixF2.SetRow(j, cee44eaa2fcfe0f22bd650b561025cbd7(j));
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				MatrixF matrixF3;
				try
				{
					matrixF3 = MatrixF.SolveLinearEquations(matrixF, matrixF2);
				}
				catch (MathematicsException innerException)
				{
					throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10390), innerException);
				}
				ca6072a768e3ec26bfbe5aa7917058914 = new VectorF[count];
				for (int k = 0; k < count; k++)
				{
					ca6072a768e3ec26bfbe5aa7917058914[k] = matrixF3.GetRow(k);
				}
				return;
			}
		}
	}

	/// <summary>
	///       Called when <see cref="M:DigitalRune.Mathematics.Interpolation.ScatteredInterpolationF.Compute(DigitalRune.Mathematics.Algebra.VectorF)" /> is called.
	///       </summary>
	/// <param name="x">The x value.</param>
	/// <returns>The y value.</returns>
	protected override VectorF OnCompute(VectorF x)
	{
		VectorF result = new VectorF(cee44eaa2fcfe0f22bd650b561025cbd7(0).NumberOfElements);
		int count = Count;
		for (int i = 0; i < count; i++)
		{
			float arg = DistanceFunction(x, c53720e3d9bbf70b21bdaa6ec34cfd6b0(i), i);
			result += ca6072a768e3ec26bfbe5aa7917058914[i] * BasisFunction(arg, i);
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(RadialBasisRegressionF.OnCompute(VectorF));
			}
			return result;
		}
	}

	[CompilerGenerated]
	private static float cde6b723243877cf27157a6099d285c48(float c5078b1ca2bd86333c65cb625b996b618, int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		return MathHelper.Gaussian(c5078b1ca2bd86333c65cb625b996b618, 1f, 0f, 1f);
	}

	[CompilerGenerated]
	private static float c076b8136d025580a448d92321a4925d4(VectorF c5078b1ca2bd86333c65cb625b996b618, VectorF c125eeb3fef00e99f8e81684cfeaa9bd2, int cdeb496a0c3d046b6bc838defe2d220d0)
	{
		return (c5078b1ca2bd86333c65cb625b996b618 - c125eeb3fef00e99f8e81684cfeaa9bd2).Length;
	}
}
