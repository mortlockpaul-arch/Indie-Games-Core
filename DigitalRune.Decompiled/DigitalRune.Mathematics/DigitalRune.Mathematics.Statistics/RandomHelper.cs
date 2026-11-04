using System;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Statistics;

/// <summary>
///       A class to generate random values.
///       </summary>
public static class RandomHelper
{
	private static Random cb8b98fd1a1a5ab13062c3456215cdaeb = new Random();

	/// <summary>
	///       Gets or sets the default random number generator.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///       The property is set <see langword="null" />.
	///       </exception>
	public static Random Random
	{
		get
		{
			return cb8b98fd1a1a5ab13062c3456215cdaeb;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			cb8b98fd1a1a5ab13062c3456215cdaeb = value;
		}
	}

	/// <summary>
	///       Gets a random boolean value.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <returns>A random boolean value.</returns>
	public static bool NextBool(this Random random)
	{
		if (random == null)
		{
			random = Random;
		}
		return random.Next(2) == 0;
	}

	/// <summary>
	///       Gets a random byte value.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <returns>A random byte value.</returns>
	public static int NextByte(this Random random)
	{
		if (random == null)
		{
			random = Random;
		}
		return random.Next(255);
	}

	/// <summary>
	///       Gets a random unit <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <returns>A random unit <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" />.</returns>
	public static QuaternionF NextQuaternionF(this Random random)
	{
		if (random == null)
		{
			random = Random;
		}
		return QuaternionF.CreateRotation(random.NextVector3F(-1f, 1f), random.NextFloat(0f, (float)Math.PI * 2f));
	}

	/// <summary>
	///       Gets a random unit <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <returns>A random unit <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" />;.</returns>
	public static QuaternionD NextQuaternionD(this Random random)
	{
		if (random == null)
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
			if (1 == 0)
			{
				__ldtoken(RandomHelper.NextQuaternionD(Random));
			}
			random = Random;
		}
		return QuaternionD.CreateRotation(random.NextVector3D(-1.0, 1.0), random.NextDouble(0.0, Math.PI * 2.0));
	}

	/// <summary>
	///       Gets a random float value that lies in the interval [<paramref name="min" />, 
	///       <paramref name="max" />].
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value.</param>
	/// <param name="max">The maximal allowed value.</param>
	/// <returns>A random float value within the bounds [min, max].</returns>
	public static float NextFloat(this Random random, float min, float max)
	{
		if (random == null)
		{
			random = Random;
		}
		return (float)random.NextDouble() * (max - min) + min;
	}

	/// <summary>
	///       Gets a random double value that lies in the interval [<paramref name="min" />, 
	///       <paramref name="max" />].
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value.</param>
	/// <param name="max">The maximal allowed value.</param>
	/// <returns>A random double value within the bounds [min, max].</returns>
	public static double NextDouble(this Random random, double min, double max)
	{
		if (random == null)
		{
			random = Random;
		}
		return random.NextDouble() * (max - min) + min;
	}

	/// <summary>
	///       Gets a random integer value that lies in the interval [<paramref name="min" />, 
	///       <paramref name="max" />].
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value.</param>
	/// <param name="max">The maximal allowed value.</param>
	/// <returns>A random integer value within the bounds [min, max].</returns>
	public static int NextInteger(this Random random, int min, int max)
	{
		if (random == null)
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
			if (1 == 0)
			{
				__ldtoken(RandomHelper.NextInteger(Random, int, int));
			}
			random = Random;
		}
		return random.Next(min, max + 1);
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a vector element.</param>
	/// <param name="max">The maximal allowed value for a vector element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.</returns>
	public static Vector2F NextVector2F(this Random random, float min, float max)
	{
		if (random == null)
		{
			random = Random;
		}
		return new Vector2F(random.NextFloat(min, max), random.NextFloat(min, max));
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Vector2D" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a vector element.</param>
	/// <param name="max">The maximal allowed value for a vector element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Vector2D" />.</returns>
	public static Vector2D NextVector2D(this Random random, double min, double max)
	{
		if (random == null)
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
				__ldtoken(RandomHelper.NextVector2D(Random, double, double));
			}
			random = Random;
		}
		return new Vector2D(random.NextDouble(min, max), random.NextDouble(min, max));
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a vector element.</param>
	/// <param name="max">The maximal allowed value for a vector element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />.</returns>
	public static Vector3F NextVector3F(this Random random, float min, float max)
	{
		if (random == null)
		{
			random = Random;
		}
		return new Vector3F(random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max));
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Vector3D" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a vector element.</param>
	/// <param name="max">The maximal allowed value for a vector element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Vector3D" />.</returns>
	public static Vector3D NextVector3D(this Random random, double min, double max)
	{
		if (random == null)
		{
			random = Random;
		}
		return new Vector3D(random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max));
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a vector element.</param>
	/// <param name="max">The maximal allowed value for a vector element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.</returns>
	public static Vector4F NextVector4F(this Random random, float min, float max)
	{
		if (random == null)
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
			if (1 == 0)
			{
				__ldtoken(RandomHelper.NextVector4F(Random, float, float));
			}
			random = Random;
		}
		return new Vector4F(random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max));
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Vector4D" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a vector element.</param>
	/// <param name="max">The maximal allowed value for a vector element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Vector4D" />.</returns>
	public static Vector4D NextVector4D(this Random random, double min, double max)
	{
		if (random == null)
		{
			random = Random;
		}
		return new Vector4D(random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max));
	}

	/// <summary>
	///       Fills a <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> with random values.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="vector">The vector that should be filled with random values.</param>
	/// <param name="min">The minimal allowed value for a vector element.</param>
	/// <param name="max">The maximal allowed value for a vector element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" />.</returns>
	public static void NextVectorF(this Random random, VectorF vector, float min, float max)
	{
		if (random == null)
		{
			random = Random;
		}
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			vector[i] = random.NextFloat(min, max);
		}
	}

	/// <summary>
	///       Fills a <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> with random values.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="vector">The vector that should be filled with random values.</param>
	/// <param name="min">The minimal allowed value for a vector element.</param>
	/// <param name="max">The maximal allowed value for a vector element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" />.</returns>
	public static void NextVectorD(this Random random, VectorD vector, double min, double max)
	{
		if (random == null)
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
				__ldtoken(RandomHelper.NextVectorD(Random, VectorD, double, double));
			}
			random = Random;
		}
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			vector[i] = random.NextDouble(min, max);
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				break;
			default:
				return;
			}
		}
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a matrix element.</param>
	/// <param name="max">The maximal allowed value for a matrix element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22F" />.</returns>
	public static Matrix22F NextMatrix22F(this Random random, float min, float max)
	{
		if (random == null)
		{
			random = Random;
		}
		return new Matrix22F(random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max));
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22D" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a matrix element.</param>
	/// <param name="max">The maximal allowed value for a matrix element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22D" />.</returns>
	public static Matrix22D NextMatrix22D(this Random random, double min, double max)
	{
		if (random == null)
		{
			random = Random;
		}
		return new Matrix22D(random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max));
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a matrix element.</param>
	/// <param name="max">The maximal allowed value for a matrix element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" />.</returns>
	public static Matrix33F NextMatrix33F(this Random random, float min, float max)
	{
		if (random == null)
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
			if (1 == 0)
			{
				__ldtoken(RandomHelper.NextMatrix33F(Random, float, float));
			}
			random = Random;
		}
		return new Matrix33F(random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max));
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33D" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a matrix element.</param>
	/// <param name="max">The maximal allowed value for a matrix element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33D" />.</returns>
	public static Matrix33D NextMatrix33D(this Random random, double min, double max)
	{
		if (random == null)
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
				__ldtoken(RandomHelper.NextMatrix33D(Random, double, double));
			}
			random = Random;
		}
		return new Matrix33D(random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max));
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a matrix element.</param>
	/// <param name="max">The maximal allowed value for a matrix element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44F" />.</returns>
	public static Matrix44F NextMatrix44F(this Random random, float min, float max)
	{
		if (random == null)
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
				__ldtoken(RandomHelper.NextMatrix44F(Random, float, float));
			}
			random = Random;
		}
		return new Matrix44F(random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max), random.NextFloat(min, max));
	}

	/// <summary>
	///       Gets a random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44D" />.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="min">The minimal allowed value for a matrix element.</param>
	/// <param name="max">The maximal allowed value for a matrix element.</param>
	/// <returns>A random <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44D" />.</returns>
	public static Matrix44D NextMatrix44D(this Random random, double min, double max)
	{
		if (random == null)
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
				__ldtoken(RandomHelper.NextMatrix44D(Random, double, double));
			}
			random = Random;
		}
		return new Matrix44D(random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max), random.NextDouble(min, max));
	}

	/// <summary>
	///       Fills a <see cref="T:DigitalRune.Mathematics.Algebra.MatrixF" /> with random values.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="matrix">The matrix that is filled with random values.</param>
	/// <param name="min">The minimal allowed value for a matrix element.</param>
	/// <param name="max">The maximal allowed value for a matrix element.</param>
	public static void NextMatrixF(this Random random, MatrixF matrix, float min, float max)
	{
		if (random == null)
		{
			random = Random;
		}
		for (int i = 0; i < matrix.NumberOfRows; i++)
		{
			for (int j = 0; j < matrix.NumberOfColumns; j++)
			{
				matrix[i, j] = random.NextFloat(min, max);
			}
		}
	}

	/// <summary>
	///       Fills a <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> with random values.
	///       </summary>
	/// <param name="random">
	///       The random number generator. If this parameter is <see langword="null" />, the global
	///       random number generator (see <see cref="P:DigitalRune.Mathematics.Statistics.RandomHelper.Random" />) is used.
	///       </param>
	/// <param name="matrix">The matrix that is filled with random values.</param>
	/// <param name="min">The minimal allowed value for a matrix element.</param>
	/// <param name="max">The maximal allowed value for a matrix element.</param>
	public static void NextMatrixD(this Random random, MatrixD matrix, double min, double max)
	{
		if (random == null)
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
				__ldtoken(RandomHelper.NextMatrixD(Random, MatrixD, double, double));
			}
			random = Random;
		}
		for (int i = 0; i < matrix.NumberOfRows; i++)
		{
			for (int j = 0; j < matrix.NumberOfColumns; j++)
			{
				matrix[i, j] = random.NextDouble(min, max);
			}
		}
	}
}
