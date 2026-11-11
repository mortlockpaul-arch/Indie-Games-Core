using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

public interface ISceneState
{
	Matrix View { get; }

	Matrix ViewToWorld { get; }

	Matrix Projection { get; }

	Matrix ProjectionNonOblique { get; }

	Matrix ProjectionToView { get; }

	Matrix ViewProjection { get; }

	Matrix ProjectionToWorld { get; }

	BoundingFrustum ViewFrustum { get; }

	bool RenderingToScreen { get; }

	bool InvertedWindings { get; }

	bool OrthographicProjection { get; }

	GameTime GameTime { get; }

	int FrameId { get; }

	ISceneEnvironment Environment { get; }

	FrameBuffers FrameBuffers { get; }

	void ApplyEditorUpdate(Matrix view, Matrix viewtoworld, Matrix projection);
}
