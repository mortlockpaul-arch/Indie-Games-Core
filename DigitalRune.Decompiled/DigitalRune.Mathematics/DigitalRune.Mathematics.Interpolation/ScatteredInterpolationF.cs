using System;
using System.Collections.ObjectModel;
using A;
using DigitalRune.Collections;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Base class for scattered interpolation methods (single-precision).
///       </summary>
public abstract class ScatteredInterpolationF : Collection<Pair<VectorF, VectorF>>
{
	private bool cc515030112f3243dbb6a38869553f320;

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.ScatteredInterpolationF" /> class</summary>
	protected ScatteredInterpolationF()
	{
	}

	/// <summary>
	///       Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
	///       </summary>
	protected override void ClearItems()
	{
		base.ClearItems();
		cc515030112f3243dbb6a38869553f320 = false;
	}

	/// <summary>
	///       Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.
	///       </summary>
	/// <param name="index">
	///       The zero-based index at which <paramref name="item" /> should be inserted.
	///       </param>
	/// <param name="item">
	///       The object to insert. The value can be null for reference types.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
	///       </exception>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The vector dimension of a newly added vector is different from the dimensions of the already
	///       registered vectors.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///       The x or y vector in <paramref name="item" /> is <see langword="null" />.
	///       </exception>
	protected override void InsertItem(int index, Pair<VectorF, VectorF> item)
	{
		cf983c1338be57f51ab77fda4a4d11d81(item);
		base.InsertItem(index, item);
		cc515030112f3243dbb6a38869553f320 = false;
	}

	/// <summary>
	///       Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
	///       </summary>
	/// <param name="index">The zero-based index of the element to remove.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
	///       </exception>
	protected override void RemoveItem(int index)
	{
		base.RemoveItem(index);
		cc515030112f3243dbb6a38869553f320 = false;
	}

	/// <summary>
	///       Replaces the element at the specified index.
	///       </summary>
	/// <param name="index">The zero-based index of the element to replace.</param>
	/// <param name="item">
	///       The new value for the element at the specified index. The value can be 
	///       <see langword="null" /> for reference types.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
	///       </exception>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The vector dimension of a newly added vector is different from the dimensions of the already
	///       registered vectors.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///       The x or y vector in <paramref name="item" /> is <see langword="null" />.
	///       </exception>
	protected override void SetItem(int index, Pair<VectorF, VectorF> item)
	{
		cf983c1338be57f51ab77fda4a4d11d81(item);
		base.SetItem(index, item);
		cc515030112f3243dbb6a38869553f320 = false;
	}

	private void cf983c1338be57f51ab77fda4a4d11d81(Pair<VectorF, VectorF> cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		if (cf51048ba330b8d3f481ea5bc25a03eb9.First == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3617), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3626));
		}
		if (cf51048ba330b8d3f481ea5bc25a03eb9.Second == null)
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
						__ldtoken(ScatteredInterpolationF.cf983c1338be57f51ab77fda4a4d11d81(Pair<VectorF, VectorF>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3617), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3760));
				}
			}
		}
		if (Count <= 0)
		{
			return;
		}
		if (base[0].First.NumberOfElements != cf51048ba330b8d3f481ea5bc25a03eb9.First.NumberOfElements)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3896));
				}
			}
		}
		if (base[0].Second.NumberOfElements == cf51048ba330b8d3f481ea5bc25a03eb9.Second.NumberOfElements)
		{
			return;
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4104));
		}
	}

	internal VectorF c53720e3d9bbf70b21bdaa6ec34cfd6b0(int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		return base[c88a85a6fc8e0a66e19d046d4d51cd3f1].First;
	}

	internal VectorF cee44eaa2fcfe0f22bd650b561025cbd7(int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		return base[c88a85a6fc8e0a66e19d046d4d51cd3f1].Second;
	}

	/// <summary>
	///       Prepares the scattered interpolation.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">No reference data pairs were added.</exception>
	public void Setup()
	{
		if (Count == 0)
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
						__ldtoken(ScatteredInterpolationF.Setup());
					}
					throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4312));
				}
			}
		}
		OnSetup();
		cc515030112f3243dbb6a38869553f320 = true;
	}

	/// <summary>
	///       Computes a y value for the specified x value using scattered interpolation.
	///       </summary>
	/// <param name="x">The x value.</param>
	/// <returns>The y value.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="x" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">No reference data pairs were added.</exception>
	public VectorF Compute(VectorF x)
	{
		if (x == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1103));
		}
		if (!cc515030112f3243dbb6a38869553f320)
		{
			Setup();
		}
		return OnCompute(x);
	}

	/// <summary>
	///       Called when <see cref="M:DigitalRune.Mathematics.Interpolation.ScatteredInterpolationF.Setup" /> is called.
	///       </summary>
	protected abstract void OnSetup();

	/// <summary>
	///       Called when <see cref="M:DigitalRune.Mathematics.Interpolation.ScatteredInterpolationF.Compute(DigitalRune.Mathematics.Algebra.VectorF)" /> is called.
	///       </summary>
	/// <param name="x">The x value.</param>
	/// <returns>The y value.</returns>
	protected abstract VectorF OnCompute(VectorF x);
}
