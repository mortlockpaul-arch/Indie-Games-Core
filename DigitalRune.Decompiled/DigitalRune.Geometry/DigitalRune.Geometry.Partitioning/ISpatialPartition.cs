using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRune.Collections;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Partitioning;

/// <summary>
///       Efficiently manages items in a space or of a model using their spatial properties.
///       </summary>
/// <typeparam name="T">The type of the items.</typeparam>
public interface ISpatialPartition<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICloneable
{
	/// <summary>
	///       Gets the axis-aligned bounding box (AABB) that contains all items.
	///       </summary>
	Aabb Aabb { get; }

	/// <summary>
	///       Gets or sets the method that computes the <see cref="T:DigitalRune.Geometry.Shapes.Aabb" /> of an item.
	///       </summary>
	Func<T, Aabb> GetAabbForItem { get; set; }

	/// <summary>
	///       Gets or sets the filter that is used to filter overlaps of two items.
	///       </summary>
	IPairFilter<T> Filter { get; set; }

	/// <summary>
	///       Gets or sets a value indicating whether self overlaps are computed.
	///       </summary>
	bool EnableSelfOverlaps { get; set; }

	/// <summary>
	///       Gets the items that touch the given axis-aligned bounding box (AABB).
	///       </summary>
	/// <param name="aabb">The axis-aligned bounding box (AABB).</param>
	/// <returns>All items that touch the given AABB.</returns>
	IEnumerable<T> GetOverlaps(Aabb aabb);

	/// <summary>
	///       Gets the items that touch the given item.
	///       </summary>
	/// <param name="item">
	///       The item. (The given item must be part of the spatial partition. In some cases external 
	///       objects are also supported. See documentation of derived types.)
	///       </param>
	/// <returns>All items that touch the given item.</returns>
	IEnumerable<T> GetOverlaps(T item);

	/// <summary>
	///       Gets the items that touch the given ray.
	///       </summary>
	/// <param name="ray">The ray.</param>
	/// <returns>All items that are hit by the ray.</returns>
	IEnumerable<T> GetOverlaps(Ray ray);

	/// <summary>
	///       Gets overlaps of all items contained in this spatial partition.
	///       </summary>
	/// <returns>
	///       All pairs of overlapping items of this spatial partition. Overlaps of an item with itself
	///       are not returned.
	///       </returns>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///   <see cref="P:DigitalRune.Geometry.Partitioning.ISpatialPartition`1.EnableSelfOverlaps" /> is <see langword="false" />.
	///       </exception>
	IEnumerable<Pair<T>> GetOverlaps();

	/// <summary>
	///       Gets overlaps between all items of this spatial partition and the items of another spatial 
	///       partition.
	///       </summary>
	/// <param name="otherPartition">The spatial partition to test against.</param>
	/// <returns>
	///       All pairwise overlaps between items of this spatial partition and 
	///       <paramref name="otherPartition" />. In each returned <see cref="T:DigitalRune.Collections.Pair`1" /> the first item (see 
	///       <see cref="P:DigitalRune.Collections.Pair`1.First" />) is from this partition and the second item (see 
	///       <see cref="P:DigitalRune.Collections.Pair`1.Second" />) is from <paramref name="otherPartition" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="otherPartition" /> is <see langword="null" />.
	///       </exception>
	IEnumerable<Pair<T>> GetOverlaps(ISpatialPartition<T> otherPartition);

	/// <summary>
	///       Gets overlaps between all items of this spatial partition and the items of another spatial 
	///       partition.
	///       </summary>
	/// <param name="scale">The scale of this spatial partition.</param>
	/// <param name="pose">The pose of this spatial partition.</param>
	/// <param name="otherPartition">The other spatial partition to test against.</param>
	/// <param name="otherScale">The scale of the <paramref name="otherPartition" />.</param>
	/// <param name="otherPose">The pose of the <paramref name="otherPartition" />.</param>
	/// <returns>
	///       All pairwise overlaps between items of this spatial partition and 
	///       <paramref name="otherPartition" />. In each returned <see cref="T:DigitalRune.Collections.Pair`1" /> the first item
	///       (see <see cref="P:DigitalRune.Collections.Pair`1.First" />) is from this partition and the second item (see 
	///       <see cref="P:DigitalRune.Collections.Pair`1.Second" />) is from <paramref name="otherPartition" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="otherPartition" /> is <see langword="null" />.
	///       </exception>
	IEnumerable<Pair<T>> GetOverlaps(Vector3F scale, Pose pose, ISpatialPartition<T> otherPartition, Vector3F otherScale, Pose otherPose);

	/// <summary>
	///       Invalidates the cached spatial information of all items in the spatial partition.
	///       </summary>
	void Invalidate();

	/// <summary>
	///       Invalidates the cached spatial information of the specified item.
	///       </summary>
	/// <param name="item">The item that has moved or changed its shape.</param>
	void Invalidate(T item);

	/// <summary>
	///       Updates the internal structure of this <see cref="T:DigitalRune.Geometry.Partitioning.ISpatialPartition`1" />.
	///       </summary>
	/// <param name="forceRebuild">
	///       If set to <see langword="true" /> the internal structure will be rebuilt from
	///       scratch. If set to <see langword="false" /> the spatial partition can decide
	///       to rebuild everything or refit only the invalidated parts.
	///       </param>
	void Update(bool forceRebuild);
}
