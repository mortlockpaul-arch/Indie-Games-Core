using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Models;

public class ModelRenderHelper
{
	public static void Render(GraphicsDevice device, ModelMesh mesh)
	{
		for (int i = 0; i < mesh.MeshParts.Count; i++)
		{
			ModelMeshPart modelMeshPart = mesh.MeshParts[i];
			device.SetVertexBuffer(modelMeshPart.VertexBuffer);
			device.Indices = modelMeshPart.IndexBuffer;
			device.DrawIndexedPrimitives(PrimitiveType.TriangleList, modelMeshPart.VertexOffset, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount);
		}
	}
}
