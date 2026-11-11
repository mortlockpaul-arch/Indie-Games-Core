using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

public interface IQuery<T>
{
	TCastType Find<TCastType>(string name, bool onlysearchdynamicobjects) where TCastType : class;

	TCastType Find<TCastType>(int uniqueid) where TCastType : class;

	bool Find<TCastType>(string name, bool onlysearchdynamicobjects, out TCastType obj) where TCastType : class;

	bool Find<TCastType>(int uniqueid, out TCastType obj) where TCastType : class;

	void Find(List<T> foundobjects, BoundingFrustum worldbounds, ObjectFilter objectfilter);

	void Find(List<T> foundobjects, BoundingBox worldbounds, ObjectFilter objectfilter);

	void Find(List<T> foundobjects, ObjectFilter objectfilter);

	void FindFast(List<T> foundobjects, BoundingBox worldbounds);

	void FindFast(List<T> foundobjects);
}
