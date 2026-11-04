using System;
using System.Collections.Generic;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Collisions.Algorithms;
using DigitalRune.Geometry.Shapes;

namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       A matrix which assigns a <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> to each pair of <see cref="T:DigitalRune.Geometry.Shapes.Shape" /> 
///       types.
///       </summary>
public class CollisionAlgorithmMatrix
{
	private readonly Dictionary<Pair<Type>, CollisionAlgorithm> c3ba5bc07da19ee2a031a88343221e76e = new Dictionary<Pair<Type>, CollisionAlgorithm>();

	/// <summary>
	///       Gets or sets the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> for the pair of collision objects.
	///       </summary>
	/// <param name="pair">A contact set containing a pair of collision objects.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="pair" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
	///       No <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> is registered for the given shape pair.
	///       </exception>
	public CollisionAlgorithm this[ContactSet pair]
	{
		get
		{
			if (pair == null)
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
							__ldtoken(CollisionAlgorithmMatrix.get_Item(ContactSet));
						}
						throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6011));
					}
				}
			}
			return this[pair.ObjectA.GeometricObject.Shape.GetType(), pair.ObjectB.GeometricObject.Shape.GetType()];
		}
		set
		{
			if (pair == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6011));
			}
			this[pair.ObjectA.GeometricObject.Shape.GetType(), pair.ObjectB.GeometricObject.Shape.GetType()] = value;
		}
	}

	/// <summary>
	///       Gets or sets the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> for the specified collision objects.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
	///       No <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> is registered for the given shape pair.
	///       </exception>
	public CollisionAlgorithm this[CollisionObject objectA, CollisionObject objectB]
	{
		get
		{
			if (objectA == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
			}
			if (objectB == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
			}
			return this[objectA.GeometricObject.Shape.GetType(), objectB.GeometricObject.Shape.GetType()];
		}
		set
		{
			if (objectA == null)
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
							__ldtoken(CollisionAlgorithmMatrix.set_Item(CollisionObject, CollisionObject, CollisionAlgorithm));
						}
						throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
					}
				}
			}
			if (objectB == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
			}
			this[objectA.GeometricObject.Shape.GetType(), objectB.GeometricObject.Shape.GetType()] = value;
		}
	}

	/// <summary>
	///       Gets or sets the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> for the specified geometric objects.
	///       </summary>
	/// <param name="geometricObjectA">The first geometric object.</param>
	/// <param name="geometricObjectB">The second geometric object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="geometricObjectA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="geometricObjectB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
	///       No <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> is registered for the given shape pair.
	///       </exception>
	public CollisionAlgorithm this[IGeometricObject geometricObjectA, IGeometricObject geometricObjectB]
	{
		get
		{
			if (geometricObjectA == null)
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
							__ldtoken(CollisionAlgorithmMatrix.get_Item(IGeometricObject, IGeometricObject));
						}
						throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6020));
					}
				}
			}
			if (geometricObjectB == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6053));
			}
			return this[geometricObjectA.Shape.GetType(), geometricObjectB.Shape.GetType()];
		}
		set
		{
			if (geometricObjectA == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6020));
			}
			if (geometricObjectB == null)
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
							__ldtoken(CollisionAlgorithmMatrix.set_Item(IGeometricObject, IGeometricObject, CollisionAlgorithm));
						}
						throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6053));
					}
				}
			}
			this[geometricObjectA.Shape.GetType(), geometricObjectB.Shape.GetType()] = value;
		}
	}

	/// <summary>
	///       Gets or sets the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> for the specified shape types.
	///       </summary>
	/// <param name="shapeA">The first shape.</param>
	/// <param name="shapeB">The second shape.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="shapeA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="shapeB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
	///       No <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> is registered for the given shape pair.
	///       </exception>
	public CollisionAlgorithm this[Shape shapeA, Shape shapeB]
	{
		get
		{
			if (shapeA == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6086));
			}
			if (shapeB == null)
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
							__ldtoken(CollisionAlgorithmMatrix.get_Item(Shape, Shape));
						}
						throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6099));
					}
				}
			}
			return this[shapeA.GetType(), shapeB.GetType()];
		}
		set
		{
			if (shapeA == null)
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
							__ldtoken(CollisionAlgorithmMatrix.set_Item(Shape, Shape, CollisionAlgorithm));
						}
						throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6086));
					}
				}
			}
			if (shapeB == null)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6099));
					}
				}
			}
			this[shapeA.GetType(), shapeB.GetType()] = value;
		}
	}

	/// <summary>
	///       Gets or sets the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> for the specified
	///       shape types.
	///       </summary>
	/// <param name="typeA">The first shape type.</param>
	/// <param name="typeB">The second shape type.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="typeA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="typeB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
	///       No <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> is registered for the given shape pair.
	///       </exception>
	public CollisionAlgorithm this[Type typeA, Type typeB]
	{
		get
		{
			if ((object)typeA == null)
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
							__ldtoken(CollisionAlgorithmMatrix.get_Item(Type, Type));
						}
						throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6112));
					}
				}
			}
			if ((object)typeB == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6123));
			}
			bool flag = false;
			Type type = typeA;
			while ((object)type != null)
			{
				Type type2 = typeB;
				while (true)
				{
					if ((object)type2 != null)
					{
						if (c3ba5bc07da19ee2a031a88343221e76e.TryGetValue(new Pair<Type>(type, type2), out var value))
						{
							while (true)
							{
								switch (6)
								{
								case 0:
									break;
								default:
									if (flag)
									{
										this[typeA, typeB] = value;
									}
									return value;
								}
							}
						}
						if ((object)type2 == typeof(RayShape))
						{
							break;
						}
						type2 = type2.BaseType;
						flag = true;
						continue;
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
					break;
				}
				if ((object)type == typeof(RayShape))
				{
					break;
				}
				type = type.BaseType;
				flag = true;
			}
			throw new KeyNotFoundException(string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6134), typeA, typeB));
		}
		set
		{
			if ((object)typeA == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6112));
			}
			if ((object)typeB == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6123));
			}
			if (value == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6223));
			}
			c3ba5bc07da19ee2a031a88343221e76e[new Pair<Type>(typeA, typeB)] = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.CollisionAlgorithmMatrix" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public CollisionAlgorithmMatrix(CollisionDetection collisionDetection)
	{
		NoCollisionAlgorithm value = new NoCollisionAlgorithm(collisionDetection);
		CombinedCollisionAlgorithm value2 = new CombinedCollisionAlgorithm(collisionDetection, new Gjk(collisionDetection), new BoxBoxAlgorithm(collisionDetection));
		CombinedCollisionAlgorithm value3 = new CombinedCollisionAlgorithm(collisionDetection, new Gjk(collisionDetection), new MinkowskiPortalRefinement(collisionDetection));
		BoxSphereAlgorithm value4 = new BoxSphereAlgorithm(collisionDetection);
		CompositeShapeAlgorithm value5 = new CompositeShapeAlgorithm(collisionDetection);
		HeightFieldAlgorithm value6 = new HeightFieldAlgorithm(collisionDetection);
		LineAlgorithm value7 = new LineAlgorithm(collisionDetection);
		PlaneBoxAlgorithm value8 = new PlaneBoxAlgorithm(collisionDetection);
		PlaneConvexAlgorithm value9 = new PlaneConvexAlgorithm(collisionDetection);
		PlaneRayAlgorithm value10 = new PlaneRayAlgorithm(collisionDetection);
		PlaneSphereAlgorithm value11 = new PlaneSphereAlgorithm(collisionDetection);
		RayBoxAlgorithm value12 = new RayBoxAlgorithm(collisionDetection);
		RayConvexAlgorithm value13 = new RayConvexAlgorithm(collisionDetection);
		RaySphereAlgorithm value14 = new RaySphereAlgorithm(collisionDetection);
		RayTriangleAlgorithm value15 = new RayTriangleAlgorithm(collisionDetection);
		SphereSphereAlgorithm value16 = new SphereSphereAlgorithm(collisionDetection);
		TransformedShapeAlgorithm value17 = new TransformedShapeAlgorithm(collisionDetection);
		TriangleMeshAlgorithm value18 = new TriangleMeshAlgorithm(collisionDetection);
		RayCompositeAlgorithm value19 = new RayCompositeAlgorithm(collisionDetection);
		RayTriangleMeshAlgorithm value20 = new RayTriangleMeshAlgorithm(collisionDetection);
		RayHeightFieldAlgorithm value21 = new RayHeightFieldAlgorithm(collisionDetection);
		this[typeof(PointShape), typeof(PointShape)] = value3;
		this[typeof(PointShape), typeof(LineShape)] = value7;
		this[typeof(PointShape), typeof(RayShape)] = value13;
		this[typeof(PointShape), typeof(LineSegmentShape)] = value3;
		this[typeof(PointShape), typeof(TriangleShape)] = value3;
		this[typeof(PointShape), typeof(RectangleShape)] = value3;
		this[typeof(PointShape), typeof(BoxShape)] = value3;
		this[typeof(PointShape), typeof(ConvexShape)] = value3;
		this[typeof(PointShape), typeof(ScaledConvexShape)] = value3;
		this[typeof(PointShape), typeof(CircleShape)] = value3;
		this[typeof(PointShape), typeof(SphereShape)] = value3;
		this[typeof(PointShape), typeof(CapsuleShape)] = value3;
		this[typeof(PointShape), typeof(ConeShape)] = value3;
		this[typeof(PointShape), typeof(CylinderShape)] = value3;
		this[typeof(PointShape), typeof(EmptyShape)] = value;
		this[typeof(PointShape), typeof(PlaneShape)] = value9;
		this[typeof(PointShape), typeof(HeightField)] = value6;
		this[typeof(PointShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(PointShape), typeof(TransformedShape)] = value17;
		this[typeof(PointShape), typeof(CompositeShape)] = value5;
		this[typeof(LineShape), typeof(LineShape)] = value7;
		this[typeof(LineShape), typeof(RayShape)] = value7;
		this[typeof(LineShape), typeof(LineSegmentShape)] = value7;
		this[typeof(LineShape), typeof(TriangleShape)] = value7;
		this[typeof(LineShape), typeof(RectangleShape)] = value7;
		this[typeof(LineShape), typeof(BoxShape)] = value7;
		this[typeof(LineShape), typeof(ConvexShape)] = value7;
		this[typeof(LineShape), typeof(ScaledConvexShape)] = value7;
		this[typeof(LineShape), typeof(CircleShape)] = value7;
		this[typeof(LineShape), typeof(SphereShape)] = value7;
		this[typeof(LineShape), typeof(CapsuleShape)] = value7;
		this[typeof(LineShape), typeof(ConeShape)] = value7;
		this[typeof(LineShape), typeof(CylinderShape)] = value7;
		this[typeof(LineShape), typeof(EmptyShape)] = value;
		this[typeof(LineShape), typeof(PlaneShape)] = value;
		this[typeof(LineShape), typeof(HeightField)] = value;
		this[typeof(LineShape), typeof(TriangleMeshShape)] = value7;
		this[typeof(LineShape), typeof(TransformedShape)] = value7;
		this[typeof(LineShape), typeof(CompositeShape)] = value7;
		this[typeof(RayShape), typeof(RayShape)] = value;
		this[typeof(RayShape), typeof(LineSegmentShape)] = value13;
		this[typeof(RayShape), typeof(TriangleShape)] = value15;
		this[typeof(RayShape), typeof(RectangleShape)] = value13;
		this[typeof(RayShape), typeof(BoxShape)] = value12;
		this[typeof(RayShape), typeof(ConvexShape)] = value13;
		this[typeof(RayShape), typeof(ScaledConvexShape)] = value13;
		this[typeof(RayShape), typeof(CircleShape)] = value13;
		this[typeof(RayShape), typeof(SphereShape)] = value14;
		this[typeof(RayShape), typeof(CapsuleShape)] = value13;
		this[typeof(RayShape), typeof(ConeShape)] = value13;
		this[typeof(RayShape), typeof(CylinderShape)] = value13;
		this[typeof(RayShape), typeof(EmptyShape)] = value;
		this[typeof(RayShape), typeof(PlaneShape)] = value10;
		this[typeof(RayShape), typeof(HeightField)] = value21;
		this[typeof(RayShape), typeof(TriangleMeshShape)] = value20;
		this[typeof(RayShape), typeof(TransformedShape)] = value17;
		this[typeof(RayShape), typeof(CompositeShape)] = value19;
		this[typeof(LineSegmentShape), typeof(LineSegmentShape)] = value3;
		this[typeof(LineSegmentShape), typeof(TriangleShape)] = value3;
		this[typeof(LineSegmentShape), typeof(RectangleShape)] = value3;
		this[typeof(LineSegmentShape), typeof(BoxShape)] = value3;
		this[typeof(LineSegmentShape), typeof(ConvexShape)] = value3;
		this[typeof(LineSegmentShape), typeof(ScaledConvexShape)] = value3;
		this[typeof(LineSegmentShape), typeof(CircleShape)] = value3;
		this[typeof(LineSegmentShape), typeof(SphereShape)] = value3;
		this[typeof(LineSegmentShape), typeof(CapsuleShape)] = value3;
		this[typeof(LineSegmentShape), typeof(ConeShape)] = value3;
		this[typeof(LineSegmentShape), typeof(CylinderShape)] = value3;
		this[typeof(LineSegmentShape), typeof(EmptyShape)] = value;
		this[typeof(LineSegmentShape), typeof(PlaneShape)] = value9;
		this[typeof(LineSegmentShape), typeof(HeightField)] = value6;
		this[typeof(LineSegmentShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(LineSegmentShape), typeof(TransformedShape)] = value17;
		this[typeof(LineSegmentShape), typeof(CompositeShape)] = value5;
		this[typeof(TriangleShape), typeof(TriangleShape)] = value3;
		this[typeof(TriangleShape), typeof(RectangleShape)] = value3;
		this[typeof(TriangleShape), typeof(BoxShape)] = value3;
		this[typeof(TriangleShape), typeof(ConvexShape)] = value3;
		this[typeof(TriangleShape), typeof(ScaledConvexShape)] = value3;
		this[typeof(TriangleShape), typeof(CircleShape)] = value3;
		this[typeof(TriangleShape), typeof(SphereShape)] = value3;
		this[typeof(TriangleShape), typeof(CapsuleShape)] = value3;
		this[typeof(TriangleShape), typeof(ConeShape)] = value3;
		this[typeof(TriangleShape), typeof(CylinderShape)] = value3;
		this[typeof(TriangleShape), typeof(EmptyShape)] = value;
		this[typeof(TriangleShape), typeof(PlaneShape)] = value9;
		this[typeof(TriangleShape), typeof(HeightField)] = value6;
		this[typeof(TriangleShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(TriangleShape), typeof(TransformedShape)] = value17;
		this[typeof(TriangleShape), typeof(CompositeShape)] = value5;
		this[typeof(RectangleShape), typeof(RectangleShape)] = value3;
		this[typeof(RectangleShape), typeof(BoxShape)] = value3;
		this[typeof(RectangleShape), typeof(ConvexShape)] = value3;
		this[typeof(RectangleShape), typeof(ScaledConvexShape)] = value3;
		this[typeof(RectangleShape), typeof(CircleShape)] = value3;
		this[typeof(RectangleShape), typeof(SphereShape)] = value3;
		this[typeof(RectangleShape), typeof(CapsuleShape)] = value3;
		this[typeof(RectangleShape), typeof(ConeShape)] = value3;
		this[typeof(RectangleShape), typeof(CylinderShape)] = value3;
		this[typeof(RectangleShape), typeof(EmptyShape)] = value;
		this[typeof(RectangleShape), typeof(PlaneShape)] = value9;
		this[typeof(RectangleShape), typeof(HeightField)] = value6;
		this[typeof(RectangleShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(RectangleShape), typeof(TransformedShape)] = value17;
		this[typeof(RectangleShape), typeof(CompositeShape)] = value5;
		this[typeof(BoxShape), typeof(BoxShape)] = value2;
		this[typeof(BoxShape), typeof(ConvexShape)] = value3;
		this[typeof(BoxShape), typeof(ScaledConvexShape)] = value3;
		this[typeof(BoxShape), typeof(CircleShape)] = value3;
		this[typeof(BoxShape), typeof(SphereShape)] = value4;
		this[typeof(BoxShape), typeof(CapsuleShape)] = value3;
		this[typeof(BoxShape), typeof(ConeShape)] = value3;
		this[typeof(BoxShape), typeof(CylinderShape)] = value3;
		this[typeof(BoxShape), typeof(EmptyShape)] = value;
		this[typeof(BoxShape), typeof(PlaneShape)] = value8;
		this[typeof(BoxShape), typeof(HeightField)] = value6;
		this[typeof(BoxShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(BoxShape), typeof(TransformedShape)] = value17;
		this[typeof(BoxShape), typeof(CompositeShape)] = value5;
		this[typeof(ConvexShape), typeof(ConvexShape)] = value3;
		this[typeof(ConvexShape), typeof(ScaledConvexShape)] = value3;
		this[typeof(ConvexShape), typeof(CircleShape)] = value3;
		this[typeof(ConvexShape), typeof(SphereShape)] = value3;
		this[typeof(ConvexShape), typeof(CapsuleShape)] = value3;
		this[typeof(ConvexShape), typeof(ConeShape)] = value3;
		this[typeof(ConvexShape), typeof(CylinderShape)] = value3;
		this[typeof(ConvexShape), typeof(EmptyShape)] = value;
		this[typeof(ConvexShape), typeof(PlaneShape)] = value9;
		this[typeof(ConvexShape), typeof(HeightField)] = value6;
		this[typeof(ConvexShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(ConvexShape), typeof(TransformedShape)] = value17;
		this[typeof(ConvexShape), typeof(CompositeShape)] = value5;
		this[typeof(ScaledConvexShape), typeof(ScaledConvexShape)] = value3;
		this[typeof(ScaledConvexShape), typeof(CircleShape)] = value3;
		this[typeof(ScaledConvexShape), typeof(SphereShape)] = value3;
		this[typeof(ScaledConvexShape), typeof(CapsuleShape)] = value3;
		this[typeof(ScaledConvexShape), typeof(ConeShape)] = value3;
		this[typeof(ScaledConvexShape), typeof(CylinderShape)] = value3;
		this[typeof(ScaledConvexShape), typeof(EmptyShape)] = value;
		this[typeof(ScaledConvexShape), typeof(PlaneShape)] = value9;
		this[typeof(ScaledConvexShape), typeof(HeightField)] = value6;
		this[typeof(ScaledConvexShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(ScaledConvexShape), typeof(TransformedShape)] = value17;
		this[typeof(ScaledConvexShape), typeof(CompositeShape)] = value5;
		this[typeof(CircleShape), typeof(CircleShape)] = value3;
		this[typeof(CircleShape), typeof(SphereShape)] = value3;
		this[typeof(CircleShape), typeof(CapsuleShape)] = value3;
		this[typeof(CircleShape), typeof(ConeShape)] = value3;
		this[typeof(CircleShape), typeof(CylinderShape)] = value3;
		this[typeof(CircleShape), typeof(EmptyShape)] = value;
		this[typeof(CircleShape), typeof(PlaneShape)] = value9;
		this[typeof(CircleShape), typeof(HeightField)] = value6;
		this[typeof(CircleShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(CircleShape), typeof(TransformedShape)] = value17;
		this[typeof(CircleShape), typeof(CompositeShape)] = value5;
		this[typeof(SphereShape), typeof(SphereShape)] = value16;
		this[typeof(SphereShape), typeof(CapsuleShape)] = value3;
		this[typeof(SphereShape), typeof(ConeShape)] = value3;
		this[typeof(SphereShape), typeof(CylinderShape)] = value3;
		this[typeof(SphereShape), typeof(EmptyShape)] = value;
		this[typeof(SphereShape), typeof(PlaneShape)] = value11;
		this[typeof(SphereShape), typeof(HeightField)] = value6;
		this[typeof(SphereShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(SphereShape), typeof(TransformedShape)] = value17;
		this[typeof(SphereShape), typeof(CompositeShape)] = value5;
		this[typeof(CapsuleShape), typeof(CapsuleShape)] = value3;
		this[typeof(CapsuleShape), typeof(ConeShape)] = value3;
		this[typeof(CapsuleShape), typeof(CylinderShape)] = value3;
		this[typeof(CapsuleShape), typeof(EmptyShape)] = value;
		this[typeof(CapsuleShape), typeof(PlaneShape)] = value9;
		this[typeof(CapsuleShape), typeof(HeightField)] = value6;
		this[typeof(CapsuleShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(CapsuleShape), typeof(TransformedShape)] = value17;
		this[typeof(CapsuleShape), typeof(CompositeShape)] = value5;
		this[typeof(ConeShape), typeof(ConeShape)] = value3;
		this[typeof(ConeShape), typeof(CylinderShape)] = value3;
		this[typeof(ConeShape), typeof(EmptyShape)] = value;
		this[typeof(ConeShape), typeof(PlaneShape)] = value9;
		this[typeof(ConeShape), typeof(HeightField)] = value6;
		this[typeof(ConeShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(ConeShape), typeof(TransformedShape)] = value17;
		this[typeof(ConeShape), typeof(CompositeShape)] = value5;
		this[typeof(CylinderShape), typeof(CylinderShape)] = value3;
		this[typeof(CylinderShape), typeof(EmptyShape)] = value;
		this[typeof(CylinderShape), typeof(PlaneShape)] = value9;
		this[typeof(CylinderShape), typeof(HeightField)] = value6;
		this[typeof(CylinderShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(CylinderShape), typeof(TransformedShape)] = value17;
		this[typeof(CylinderShape), typeof(CompositeShape)] = value5;
		this[typeof(EmptyShape), typeof(EmptyShape)] = value;
		this[typeof(EmptyShape), typeof(PlaneShape)] = value;
		this[typeof(EmptyShape), typeof(HeightField)] = value;
		this[typeof(EmptyShape), typeof(TriangleMeshShape)] = value;
		this[typeof(EmptyShape), typeof(TransformedShape)] = value;
		this[typeof(EmptyShape), typeof(CompositeShape)] = value;
		this[typeof(PlaneShape), typeof(PlaneShape)] = value;
		this[typeof(PlaneShape), typeof(HeightField)] = value;
		this[typeof(PlaneShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(PlaneShape), typeof(TransformedShape)] = value17;
		this[typeof(PlaneShape), typeof(CompositeShape)] = value5;
		this[typeof(HeightField), typeof(HeightField)] = value;
		this[typeof(HeightField), typeof(TriangleMeshShape)] = value6;
		this[typeof(HeightField), typeof(TransformedShape)] = value17;
		this[typeof(HeightField), typeof(CompositeShape)] = value6;
		this[typeof(TriangleMeshShape), typeof(TriangleMeshShape)] = value18;
		this[typeof(TriangleMeshShape), typeof(TransformedShape)] = value17;
		this[typeof(TriangleMeshShape), typeof(CompositeShape)] = value5;
		this[typeof(TransformedShape), typeof(TransformedShape)] = value17;
		this[typeof(TransformedShape), typeof(CompositeShape)] = value17;
		this[typeof(CompositeShape), typeof(CompositeShape)] = value5;
	}
}
