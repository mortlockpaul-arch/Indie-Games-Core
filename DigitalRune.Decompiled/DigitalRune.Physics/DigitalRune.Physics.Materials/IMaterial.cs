using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Materials;

/// <summary>
///       Defines the material (friction, bounciness, etc.) of a rigid body.
///       </summary>
public interface IMaterial
{
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
	MaterialProperties GetProperties(RigidBody body, Vector3F positionLocal, int featureIndex);
}
