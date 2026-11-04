using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Materials;

/// <summary>
///       Defines a material with different materials for each shape feature of a rigid body.
///       </summary>
public class CompositeMaterial : IMaterial
{
	[CompilerGenerated]
	private UniformMaterial c7eb0e359dd3a0754c87f17a9cfbceeb1;

	[CompilerGenerated]
	private List<UniformMaterial> c60ff6f098ca58a99edbd295f3047d5c4;

	/// <summary>
	///       Gets the default material that is used for shape features that have no entry
	///       in <see cref="P:DigitalRune.Physics.Materials.CompositeMaterial.Materials" />.
	///       </summary>
	public UniformMaterial DefaultMaterial
	{
		[CompilerGenerated]
		get
		{
			return c7eb0e359dd3a0754c87f17a9cfbceeb1;
		}
		[CompilerGenerated]
		private set
		{
			c7eb0e359dd3a0754c87f17a9cfbceeb1 = value;
		}
	}

	/// <summary>
	///       Gets the list of materials for the shape features.
	///       </summary>
	public List<UniformMaterial> Materials
	{
		[CompilerGenerated]
		get
		{
			return c60ff6f098ca58a99edbd295f3047d5c4;
		}
		[CompilerGenerated]
		private set
		{
			c60ff6f098ca58a99edbd295f3047d5c4 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Materials.CompositeMaterial" /> class.
	///       </summary>
	public CompositeMaterial()
		: this(new UniformMaterial())
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Materials.CompositeMaterial" /> class.
	///       </summary>
	/// <param name="defaultMaterial">The default material.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="defaultMaterial" /> is <see langword="null" />.
	///       </exception>
	public CompositeMaterial(UniformMaterial defaultMaterial)
	{
		if (defaultMaterial == null)
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
						__ldtoken(CompositeMaterial._002Ector(UniformMaterial));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5463));
				}
			}
		}
		DefaultMaterial = defaultMaterial;
		Materials = new List<UniformMaterial>();
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
		if (featureIndex < 0)
		{
			return DefaultMaterial.GetProperties(body, positionLocal, featureIndex);
		}
		IMaterial material = null;
		if (featureIndex < Materials.Count)
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
				__ldtoken(CompositeMaterial.GetProperties(RigidBody, Vector3F, int));
			}
			material = Materials[featureIndex];
		}
		if (material == null && body.Shape is CompositeShape compositeShape && featureIndex < compositeShape.Children.Count)
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
			if (compositeShape.Children[featureIndex] is RigidBody rigidBody)
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
				material = rigidBody.Material;
			}
		}
		if (material == null)
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
			material = DefaultMaterial;
		}
		return material.GetProperties(body, positionLocal, featureIndex);
	}
}
