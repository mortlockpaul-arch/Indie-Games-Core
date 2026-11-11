using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SynapseGaming.LightingSystem.Rendering;

public class RenderableMeshCollection : ReadOnlyCollection<RenderableMesh>
{
	public RenderableMeshCollection(IList<RenderableMesh> meshes)
		: base(meshes)
	{
	}
}
