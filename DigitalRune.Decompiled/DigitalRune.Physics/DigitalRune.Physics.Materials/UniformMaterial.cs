using System;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Materials;

/// <summary>
///       Defines a <see cref="T:DigitalRune.Physics.Materials.IMaterial" /> with constant material properties for the whole rigid body.
///       </summary>
public class UniformMaterial : IMaterial, INamedObject
{
	private Vector3F c290d35f3db3252d3284ac100ea993e23;

	[CompilerGenerated]
	private float c2e79479f2c8e6fa1920256a0ca2a32dc;

	[CompilerGenerated]
	private float ce77ce857c9dfd3e02f86900b1ccc5d30;

	[CompilerGenerated]
	private float cbb70f9b9427e84fbb889b13f8ff98281;

	[CompilerGenerated]
	private bool c119f42360518239aeecc11904401d02c;

	[CompilerGenerated]
	private string c4562595c3012a609fed551ff9cc45ada;

	/// <summary>
	///       Gets or sets the static friction coefficient.
	///       </summary>
	public float StaticFriction
	{
		[CompilerGenerated]
		get
		{
			return c2e79479f2c8e6fa1920256a0ca2a32dc;
		}
		[CompilerGenerated]
		set
		{
			c2e79479f2c8e6fa1920256a0ca2a32dc = value;
		}
	}

	/// <summary>
	///       Gets or sets the dynamic friction (kinetic friction) coefficient.
	///       </summary>
	public float DynamicFriction
	{
		[CompilerGenerated]
		get
		{
			return ce77ce857c9dfd3e02f86900b1ccc5d30;
		}
		[CompilerGenerated]
		set
		{
			ce77ce857c9dfd3e02f86900b1ccc5d30 = value;
		}
	}

	/// <summary>
	///       Gets or sets the coefficient of restitution (bounciness).
	///       </summary>
	public float Restitution
	{
		[CompilerGenerated]
		get
		{
			return cbb70f9b9427e84fbb889b13f8ff98281;
		}
		[CompilerGenerated]
		set
		{
			cbb70f9b9427e84fbb889b13f8ff98281 = value;
		}
	}

	/// <summary>
	///       Gets a value indicating whether this material supports surface motion.
	///       </summary>
	public bool SupportsSurfaceMotion
	{
		[CompilerGenerated]
		get
		{
			return c119f42360518239aeecc11904401d02c;
		}
		[CompilerGenerated]
		private set
		{
			c119f42360518239aeecc11904401d02c = value;
		}
	}

	/// <summary>
	///       Gets or sets the velocity of the rigid body surface (in local space of the body).
	///       </summary>
	/// <exception cref="T:DigitalRune.Physics.PhysicsException">
	///       This material does not support surface motion. (<see cref="P:DigitalRune.Physics.Materials.UniformMaterial.SupportsSurfaceMotion" /> is 
	///       <see langword="false" />.)
	///       </exception>
	public Vector3F SurfaceMotion
	{
		get
		{
			return c290d35f3db3252d3284ac100ea993e23;
		}
		set
		{
			if (!SupportsSurfaceMotion)
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
							__ldtoken(UniformMaterial.set_SurfaceMotion(Vector3F));
						}
						throw new PhysicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2638));
					}
				}
			}
			c290d35f3db3252d3284ac100ea993e23 = value;
		}
	}

	/// <summary>
	///       Gets or sets the name of the material.
	///       </summary>
	public string Name
	{
		[CompilerGenerated]
		get
		{
			return c4562595c3012a609fed551ff9cc45ada;
		}
		[CompilerGenerated]
		set
		{
			c4562595c3012a609fed551ff9cc45ada = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Materials.UniformMaterial" /> class.
	///       </summary>
	public UniformMaterial()
		: this(null, supportsSurfaceMotion: false)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Materials.UniformMaterial" /> class.
	///       </summary>
	/// <param name="name">The name of the material. Can be <see langword="null" />.</param>
	public UniformMaterial(string name)
		: this(name, supportsSurfaceMotion: false)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Materials.UniformMaterial" /> class.
	///       </summary>
	/// <param name="name">The name of the material. Can be <see langword="null" />.</param>
	/// <param name="supportsSurfaceMotion">
	///       If set to <see langword="true" /> the material supports surface motion. See also
	///       <see cref="P:DigitalRune.Physics.Materials.UniformMaterial.SupportsSurfaceMotion" /> and <see cref="P:DigitalRune.Physics.Materials.UniformMaterial.SurfaceMotion" />.
	///       </param>
	public UniformMaterial(string name, bool supportsSurfaceMotion)
	{
		Name = name;
		DynamicFriction = 0.5f;
		StaticFriction = 0.5f;
		Restitution = 0.1f;
		SupportsSurfaceMotion = supportsSurfaceMotion;
		c290d35f3db3252d3284ac100ea993e23 = Vector3F.Zero;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Materials.UniformMaterial" /> class from a given material.
	///       </summary>
	/// <param name="material">The material from which the properties are copied.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="material" /> is <see langword="null" />.
	///       </exception>
	public UniformMaterial(UniformMaterial material)
	{
		if (material == null)
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
						__ldtoken(UniformMaterial._002Ector(UniformMaterial));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2731));
				}
			}
		}
		Name = material.Name;
		DynamicFriction = material.DynamicFriction;
		StaticFriction = material.StaticFriction;
		Restitution = material.Restitution;
		SupportsSurfaceMotion = material.SupportsSurfaceMotion;
		c290d35f3db3252d3284ac100ea993e23 = material.SurfaceMotion;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Materials.UniformMaterial" /> class from a given material.
	///       </summary>
	/// <param name="name">The name of the material. Can be <see langword="null" />.</param>
	/// <param name="material">The material from which the properties are copied.</param>
	public UniformMaterial(string name, MaterialProperties material)
	{
		Name = name;
		DynamicFriction = material.DynamicFriction;
		StaticFriction = material.StaticFriction;
		Restitution = material.Restitution;
		SupportsSurfaceMotion = material.SupportsSurfaceMotion;
		c290d35f3db3252d3284ac100ea993e23 = material.SurfaceMotion;
	}

	/// <summary>
	///       Gets the <see cref="T:DigitalRune.Physics.Materials.MaterialProperties" /> for the given rigid body, position and shape
	///       feature.
	///       </summary>
	/// <param name="body">The rigid body.</param>
	/// <param name="positionLocal">
	///       The local position on the rigid body for which the material properties should be returned.
	///       </param>
	/// <param name="featureIndex">
	///       The index of the shape feature from which the material properties are needed. For a
	///       <see cref="T:DigitalRune.Geometry.Shapes.CompositeShape" /> the feature index is the index of the child of the composite
	///       shape. For a <see cref="T:DigitalRune.Geometry.Shapes.TriangleMeshShape" /> the feature index is the index of a triangle.
	///       </param>
	/// <returns>
	///       The <see cref="T:DigitalRune.Physics.Materials.MaterialProperties" /> of the given rigid body at the given position and
	///       child feature.
	///       </returns>
	public MaterialProperties GetProperties(RigidBody body, Vector3F positionLocal, int featureIndex)
	{
		return new MaterialProperties(StaticFriction, DynamicFriction, Restitution, SupportsSurfaceMotion, SurfaceMotion);
	}
}
