using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Generic interface used by container objects that implement querying
/// for contained objects by various object attributes.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IQuery<T>
{
	/// <summary>
	/// Retrieves a dynamic object of a specific type by name.
	/// </summary>
	/// <typeparam name="TCastType">Type of object to find.</typeparam>
	/// <param name="name">Name of the object to find.</param>
	/// <param name="obj">Returned object.</param>
	/// <returns></returns>
	bool Find<TCastType>(string name, out TCastType obj) where TCastType : class;

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes
	/// and overlap with or are contained in a bounding area.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	/// <param name="worldbounds">Bounding area used to limit query results.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	void Find(List<T> foundobjects, BoundingFrustum worldbounds, ObjectFilter objectfilter);

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes
	/// and overlap with or are contained in a bounding area.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	/// <param name="worldbounds">Bounding area used to limit query results.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	void Find(List<T> foundobjects, BoundingBox worldbounds, ObjectFilter objectfilter);

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	void Find(List<T> foundobjects, ObjectFilter objectfilter);

	/// <summary>
	/// Quickly finds all objects near a bounding area without the overhead of
	/// filtering by object type, checking if objects are enabled, or verifying
	/// containment within the bounds.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	/// <param name="worldbounds">Bounding area used to limit query results.</param>
	void FindFast(List<T> foundobjects, BoundingBox worldbounds);

	/// <summary>
	/// Quickly finds all objects without the overhead of filtering by object
	/// type or checking if objects are enabled.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	void FindFast(List<T> foundobjects);
}
