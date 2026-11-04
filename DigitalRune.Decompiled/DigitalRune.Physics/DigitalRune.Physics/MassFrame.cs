using System;
using A;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics;

/// <summary>
///       Defines the mass properties of a rigid body.
///       </summary>
public sealed class MassFrame : ICloneable
{
	private static float c65714579812fa820ed82b5e48bc19caa = 1E+10f;

	private float c329c08008076cbdbcfa38a3b2cf1c9b5 = 1000f;

	private float c6f8bd43c0d98947749871a354136e690 = 1000f;

	private float c17844fdaef32a19069e786a1f6dd657c = 0.001f;

	private Vector3F cbbe51730b91a41b310417354adcceeb0 = new Vector3F(166.67f);

	private Vector3F cde42c5b6e1a09c2aa915c4a4e2cab080 = new Vector3F(0.00599988f);

	private Pose cd337f7a206e7977cce3e1030296b1b57 = Pose.Identity;

	/// <summary>
	///       Gets or sets the mass limit. Mass values above this value are treated as infinite.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public static float MassLimit
	{
		get
		{
			return c65714579812fa820ed82b5e48bc19caa;
		}
		set
		{
			if (value < 0f)
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
							__ldtoken(MassFrame.set_MassLimit(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4552));
					}
				}
			}
			c65714579812fa820ed82b5e48bc19caa = value;
		}
	}

	/// <summary>
	///       Gets or sets the density.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float Density
	{
		get
		{
			return c329c08008076cbdbcfa38a3b2cf1c9b5;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4617));
			}
			c329c08008076cbdbcfa38a3b2cf1c9b5 = value;
		}
	}

	/// <summary>
	///       Gets or sets the mass.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float Mass
	{
		get
		{
			return c6f8bd43c0d98947749871a354136e690;
		}
		set
		{
			if (value < 0f)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(MassFrame.set_Mass(float));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4704));
				}
			}
			c6f8bd43c0d98947749871a354136e690 = (Numeric.IsNaN(value) ? float.PositiveInfinity : value);
			float num;
			if (!Numeric.IsZero(c6f8bd43c0d98947749871a354136e690))
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
				if (!(c6f8bd43c0d98947749871a354136e690 >= MassLimit))
				{
					num = 1f / c6f8bd43c0d98947749871a354136e690;
					goto IL_0085;
				}
			}
			num = 0f;
			goto IL_0085;
			IL_0085:
			c8a351a04f7dae1ec8ab06c1ff3fa4e2f = num;
		}
	}

	internal float c8a351a04f7dae1ec8ab06c1ff3fa4e2f
	{
		get
		{
			return c17844fdaef32a19069e786a1f6dd657c;
		}
		private set
		{
			c17844fdaef32a19069e786a1f6dd657c = value;
		}
	}

	/// <summary>
	///       Gets or sets the inertia.
	///       </summary>
	public Vector3F Inertia
	{
		get
		{
			return cbbe51730b91a41b310417354adcceeb0;
		}
		set
		{
			Vector3F vector3F;
			if (!value.IsNaN)
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
					__ldtoken(MassFrame.set_Inertia(Vector3F));
				}
				vector3F = value;
			}
			else
			{
				vector3F = new Vector3F(float.PositiveInfinity);
			}
			cbbe51730b91a41b310417354adcceeb0 = vector3F;
			c6a7403053bef7c6c83a27a0629d248e9();
		}
	}

	internal Vector3F c9366fd43f24869c3f8a2696505a00f00
	{
		get
		{
			return cde42c5b6e1a09c2aa915c4a4e2cab080;
		}
		private set
		{
			cde42c5b6e1a09c2aa915c4a4e2cab080 = value;
		}
	}

	/// <summary>
	///       Gets or sets the pose that defines the center of mass and the principal axes.
	///       </summary>
	public Pose Pose
	{
		get
		{
			return cd337f7a206e7977cce3e1030296b1b57;
		}
		set
		{
			cd337f7a206e7977cce3e1030296b1b57 = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.MassFrame" /> class</summary>
	public MassFrame()
	{
	}

	private object c48423685eed44c8f6f4708513cf05f2b()
	{
		return Clone();
	}

	object ICloneable.Clone()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c48423685eed44c8f6f4708513cf05f2b
		return this.c48423685eed44c8f6f4708513cf05f2b();
	}

	/// <summary>
	///       Creates a new <see cref="T:DigitalRune.Physics.MassFrame" /> that is a clone (deep copy) of the current instance.
	///       </summary>
	/// <returns>
	///       A new <see cref="T:DigitalRune.Physics.MassFrame" /> that is a clone (deep copy) of the current instance.
	///       </returns>
	public MassFrame Clone()
	{
		MassFrame massFrame = new MassFrame();
		massFrame.c329c08008076cbdbcfa38a3b2cf1c9b5 = c329c08008076cbdbcfa38a3b2cf1c9b5;
		massFrame.cbbe51730b91a41b310417354adcceeb0 = cbbe51730b91a41b310417354adcceeb0;
		massFrame.cde42c5b6e1a09c2aa915c4a4e2cab080 = cde42c5b6e1a09c2aa915c4a4e2cab080;
		massFrame.c6f8bd43c0d98947749871a354136e690 = c6f8bd43c0d98947749871a354136e690;
		massFrame.c17844fdaef32a19069e786a1f6dd657c = c17844fdaef32a19069e786a1f6dd657c;
		massFrame.cd337f7a206e7977cce3e1030296b1b57 = cd337f7a206e7977cce3e1030296b1b57;
		return massFrame;
	}

	/// <summary>
	///       Changes the mass so that it is equal to the given target mass and the
	///       related properties (inertia) are scaled accordingly.
	///       </summary>
	/// <param name="targetMass">The target mass.</param>
	public void Adjust(float targetMass)
	{
		float num = targetMass / Mass;
		Mass = targetMass;
		Inertia *= num;
	}

	/// <summary>
	///       Computes a mass frame for the given shape and density.
	///       </summary>
	/// <param name="shape">The shape.</param>
	/// <param name="scale">The scale of the shape.</param>
	/// <param name="density">The density.</param>
	/// <param name="relativeDistanceThreshold">
	///       The relative distance threshold. If no mass or inertia formula for the given shape are known
	///       the shape is approximated with a triangle mesh and the mass frame of this mesh is returned.
	///       The relative distance threshold controls the accuracy of the approximated mesh. Good default
	///       values are 0.05 to get an approximation with an error of about 5%.
	///       </param>
	/// <param name="iterationLimit">
	///       The iteration limit. For some shapes the mass properties are computed with an iterative
	///       algorithm. No more than <paramref name="iterationLimit" /> iterations will be performed.
	///       A value of 3 gives good results in most cases. Use a value of -1 to get only a coarse
	///       approximation.
	///       </param>
	/// <returns>
	///       A new <see cref="T:DigitalRune.Physics.MassFrame" /> for the given parameters is returned.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="shape" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="density" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="relativeDistanceThreshold" /> is negative.
	///       </exception>
	public static MassFrame FromShapeAndDensity(Shape shape, Vector3F scale, float density, float relativeDistanceThreshold, int iterationLimit)
	{
		return c5037dec282aef11fcb2a4dcc109df738(shape, scale, density, c6e96ba1bb9ca98c3df80bc5932f765fc: true, relativeDistanceThreshold, iterationLimit);
	}

	/// <summary>
	///       Computes a mass frame for the given shape and target mass.
	///       </summary>
	/// <param name="shape">The shape.</param>
	/// <param name="scale">The scale of the shape.</param>
	/// <param name="mass">
	///       The target mass. The mass of the computed <see cref="T:DigitalRune.Physics.MassFrame" /> will be equal to this 
	///       value. Other mass properties are adjusted to match the target mass.
	///       </param>
	/// <param name="relativeDistanceThreshold">
	///       The relative distance threshold. If no mass or inertia formula for the given shape are known
	///       the shape is approximated with a triangle mesh and the mass frame of this mesh is returned.
	///       The relative distance threshold controls the accuracy of the approximated mesh. Good default
	///       values are 0.05 to get an approximation with an error of about 5%.
	///       </param>
	/// <param name="iterationLimit">
	///       The iteration limit. For some shapes the mass properties are computed with an iterative
	///       algorithm. No more than <paramref name="iterationLimit" /> iterations will be performed.
	///       A value of 3 gives good results in most cases. Use a value of -1 to get only a coarse
	///       approximation.
	///       </param>
	/// <returns>
	///       A new <see cref="T:DigitalRune.Physics.MassFrame" /> for the given parameters is returned.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="shape" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="mass" /> is negative or 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="relativeDistanceThreshold" /> is negative.
	///       </exception>
	public static MassFrame FromShapeAndMass(Shape shape, Vector3F scale, float mass, float relativeDistanceThreshold, int iterationLimit)
	{
		return c5037dec282aef11fcb2a4dcc109df738(shape, scale, mass, c6e96ba1bb9ca98c3df80bc5932f765fc: false, relativeDistanceThreshold, iterationLimit);
	}

	private static MassFrame c5037dec282aef11fcb2a4dcc109df738(Shape cc5fdd9518d3b72c985e2608f434fa386, Vector3F ceabb47d7d959545d89c4300eabbdd7c2, float ccd0e6705e44b13fb907035ec01adf2ca, bool c6e96ba1bb9ca98c3df80bc5932f765fc, float cec2cf841da966bf6a09d908b7167be17, int c11ff2b2cbe9bdd3b85d3ad8d4b1509f0)
	{
		if (cc5fdd9518d3b72c985e2608f434fa386 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1751));
		}
		if (ccd0e6705e44b13fb907035ec01adf2ca <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1762), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4785));
		}
		if (cec2cf841da966bf6a09d908b7167be17 < 0f)
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
						__ldtoken(MassFrame.c5037dec282aef11fcb2a4dcc109df738(Shape, Vector3F, float, bool, float, int));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1876), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4866));
				}
			}
		}
		MassHelper.ce602a7340e91bf272bcbe2f132962ab5(cc5fdd9518d3b72c985e2608f434fa386, ceabb47d7d959545d89c4300eabbdd7c2, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, cec2cf841da966bf6a09d908b7167be17, c11ff2b2cbe9bdd3b85d3ad8d4b1509f0, out var c3f34db33979fee2441b0f8e870e3dc, out var ced9d1a762c087a2f16a57733d63c, out var c78db0da7e1c88667b788d19027fafe7c);
		if (!Numeric.IsNaN(c3f34db33979fee2441b0f8e870e3dc))
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
			if (!ced9d1a762c087a2f16a57733d63c.IsNaN)
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
				if (!c78db0da7e1c88667b788d19027fafe7c.IsNaN)
				{
					if (Numeric.IsZero(c78db0da7e1c88667b788d19027fafe7c.M01))
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
						if (Numeric.IsZero(c78db0da7e1c88667b788d19027fafe7c.M02))
						{
							if (Numeric.IsZero(c78db0da7e1c88667b788d19027fafe7c.M12))
							{
								MassFrame massFrame = new MassFrame();
								massFrame.Mass = c3f34db33979fee2441b0f8e870e3dc;
								massFrame.Inertia = new Vector3F(c78db0da7e1c88667b788d19027fafe7c.M00, c78db0da7e1c88667b788d19027fafe7c.M11, c78db0da7e1c88667b788d19027fafe7c.M22);
								massFrame.Pose = new Pose(ced9d1a762c087a2f16a57733d63c);
								massFrame.Density = (c6e96ba1bb9ca98c3df80bc5932f765fc ? ccd0e6705e44b13fb907035ec01adf2ca : 0f);
								return massFrame;
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
					}
					MassHelper.c5df24c6b4c413e9d29a3b279b5924a1e(c78db0da7e1c88667b788d19027fafe7c, out var c0c11e9545303c1a548d0ac043c73a14d, out var caaabbb14cb24c5c3c46d7f2f0ef1e);
					MassFrame massFrame2 = new MassFrame();
					massFrame2.Mass = c3f34db33979fee2441b0f8e870e3dc;
					massFrame2.Inertia = c0c11e9545303c1a548d0ac043c73a14d;
					massFrame2.Pose = new Pose(ced9d1a762c087a2f16a57733d63c, caaabbb14cb24c5c3c46d7f2f0ef1e);
					massFrame2.Density = (c6e96ba1bb9ca98c3df80bc5932f765fc ? ccd0e6705e44b13fb907035ec01adf2ca : 0f);
					return massFrame2;
				}
			}
		}
		MassFrame massFrame3 = new MassFrame();
		massFrame3.Mass = 0f;
		massFrame3.Inertia = Vector3F.Zero;
		return massFrame3;
	}

	private void c6a7403053bef7c6c83a27a0629d248e9()
	{
		float x;
		if (!Numeric.IsZero(cbbe51730b91a41b310417354adcceeb0.X, 1E-10f))
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
			if (1 == 0)
			{
				__ldtoken(MassFrame.c6a7403053bef7c6c83a27a0629d248e9());
			}
			if (!(cbbe51730b91a41b310417354adcceeb0.X >= MassLimit))
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
				x = 1f / cbbe51730b91a41b310417354adcceeb0.X;
				goto IL_0063;
			}
		}
		x = 0f;
		goto IL_0063;
		IL_0063:
		float y = ((Numeric.IsZero(cbbe51730b91a41b310417354adcceeb0.Y, 1E-10f) || cbbe51730b91a41b310417354adcceeb0.Y >= MassLimit) ? 0f : (1f / cbbe51730b91a41b310417354adcceeb0.Y));
		float z;
		if (!Numeric.IsZero(cbbe51730b91a41b310417354adcceeb0.Z, 1E-10f))
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
			if (!(cbbe51730b91a41b310417354adcceeb0.Z >= MassLimit))
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
				z = 1f / cbbe51730b91a41b310417354adcceeb0.Z;
				goto IL_00fb;
			}
		}
		z = 0f;
		goto IL_00fb;
		IL_00fb:
		c9366fd43f24869c3f8a2696505a00f00 = new Vector3F(x, y, z);
	}
}
