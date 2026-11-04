using DigitalRune.Geometry.Shapes;

namespace DigitalRune.Geometry.Meshes;

/// <summary>
///       Represents a triangle mesh.
///       </summary>
public interface ITriangleMesh
{
	/// <summary>
	///       Gets the number of triangles.
	///       </summary>
	int NumberOfTriangles { get; }

	/// <summary>
	///       Gets the triangle with the given index.
	///       </summary>
	/// <param name="index">The index.</param>
	/// <returns>The triangle with the given index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is out of range.
	///       </exception>
	Triangle GetTriangle(int index);
}
