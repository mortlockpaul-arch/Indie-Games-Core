using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Lights;

public interface ILightManager : IWorldRenderableManager, IRenderableManager, IUpdatableManager, ILightQuery, IQuery<BaseLight>, IManagerService, IManager, IUnloadable, ISubmit<BaseLight>, ISubmit<IScene>
{
	void SubmitStaticDirectionalLight(Vector3 diffusecolor, Vector3 direction, float intensity, ShadowType shadowtype, float shadowquality, float shadowprimarybias, float shadowsecondarybias);

	void SubmitStaticPointLight(Vector3 diffusecolor, Vector3 position, float intensity, float radius, bool filllight, float falloffstrength, ShadowType shadowtype, float shadowquality, float shadowprimarybias, float shadowsecondarybias, IShadowSource shadowsource);

	void SubmitStaticSpotLight(Vector3 diffusecolor, Vector3 position, float intensity, float radius, Vector3 direction, float angle, bool filllight, float falloffstrength, ShadowType shadowtype, float shadowquality, float shadowprimarybias, float shadowsecondarybias, IShadowSource shadowsource);

	void SubmitStaticAmbientLight(Vector3 diffusecolor, float intensity);

	void RenderVolumeLights();

	void RenderVolumeLights(List<BaseLight> volumelights);

	new void Clear();
}
