using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Lights;

/// <summary>
/// Manages all scene lights and allows querying the scene with
/// a view or bounding box for lights that affect the area
/// (acts as a light scenegraph).
/// </summary>
public class BaseLightManager : ObjectGraph<BaseLight>, IManager, IUnloadable, ILightQuery, IQuery<BaseLight>
{
	private class nNB
	{
		public SystemStatistic CompositeLights = SystemConsole.GetStatistic("Light_CompositeLights", SystemStatisticCategory.Lighting);

		public SystemStatistic CompositeLightSources = SystemConsole.GetStatistic("Light_CompositeLightSources", SystemStatisticCategory.Lighting);
	}

	private static int fNB = 10000;

	private static float fNa = 0.004f;

	/// <summary>
	/// Current scene state information provided to BeginFrameRendering (only valid between calls to BeginFrameRendering and EndFrameRendering).
	/// </summary>
	protected ISceneState SceneState;

	/// <summary>
	/// Service provider used to access scene managers.
	/// </summary>
	protected IManagerServiceProvider ServiceProvider;

	private List<BaseLight> fNg = new List<BaseLight>(16);

	private nNB fNf = new nNB();

	internal static int MaxLightsPerGroup
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
		}
	}

	/// <summary>
	/// Creates a new BaseLightManager instance.
	/// </summary>
	/// <param name="sceneinterface">Service provider used to access all other manager services in this scene.</param>
	/// <param name="worldboundingbox">The smallest bounding area that completely
	/// contains the scene. Helps the LightManager build an optimal scene tree.</param>
	/// <param name="worldtreemaxdepth">Maximum depth for entries in the scene tree. Small
	/// scenes with few objects see better performance with shallow trees. Large complex
	/// scenes often need deeper trees.</param>
	public BaseLightManager(IManagerServiceProvider sceneinterface, BoundingBox worldboundingbox, int worldtreemaxdepth)
		: base(worldboundingbox, worldtreemaxdepth)
	{
		ServiceProvider = sceneinterface;
	}

	/// <summary>
	/// Creates a new BaseLightManager instance.
	/// </summary>
	public BaseLightManager(IManagerServiceProvider sceneinterface)
	{
		ServiceProvider = sceneinterface;
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public virtual void ApplyPreferences(ILightingSystemPreferences preferences)
	{
	}

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes
	/// and overlap with or are contained in a bounding area.
	///
	/// Note: list will contain null entries when objects returned by the
	/// scenegraph are removed by the object filter.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	/// <param name="worldbounds">Bounding area used to limit query results.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	public override void Find(List<BaseLight> foundobjects, BoundingFrustum worldbounds, ObjectFilter objectfilter)
	{
		int count = foundobjects.Count;
		base.Find(foundobjects, worldbounds, objectfilter);
		bool flag = (objectfilter & ObjectFilter.Enabled) != 0;
		bool flag2 = (objectfilter & ObjectFilter.Disabled) != 0;
		if (flag && flag2)
		{
			return;
		}
		int count2 = foundobjects.Count;
		for (int k = count; k < count2; k++)
		{
			if (!f_0010(flag, flag2, foundobjects[k]))
			{
				foundobjects[k] = null;
			}
		}
	}

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes
	/// and overlap with or are contained in a bounding area.
	///
	/// Note: list will contain null entries when objects returned by the
	/// scenegraph are removed by the object filter.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	/// <param name="worldbounds">Bounding area used to limit query results.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	public override void Find(List<BaseLight> foundobjects, BoundingBox worldbounds, ObjectFilter objectfilter)
	{
		int count = foundobjects.Count;
		base.Find(foundobjects, worldbounds, objectfilter);
		bool flag = (objectfilter & ObjectFilter.Enabled) != 0;
		bool flag2 = (objectfilter & ObjectFilter.Disabled) != 0;
		if (flag && flag2)
		{
			return;
		}
		int count2 = foundobjects.Count;
		for (int k = count; k < count2; k++)
		{
			if (!f_0010(flag, flag2, foundobjects[k]))
			{
				foundobjects[k] = null;
			}
		}
	}

	/// <summary>
	/// Finds all contained objects that match a set of filter attributes.
	///
	/// Note: list will contain null entries when objects returned by the
	/// scenegraph are removed by the object filter.
	/// </summary>
	/// <param name="foundobjects">List used to store found objects during the query.</param>
	/// <param name="objectfilter">Filter used to limit query results to objects with specific attributes.</param>
	public override void Find(List<BaseLight> foundobjects, ObjectFilter objectfilter)
	{
		int count = foundobjects.Count;
		base.Find(foundobjects, objectfilter);
		bool flag = (objectfilter & ObjectFilter.Enabled) != 0;
		bool flag2 = (objectfilter & ObjectFilter.Disabled) != 0;
		if (flag && flag2)
		{
			return;
		}
		int count2 = foundobjects.Count;
		for (int k = count; k < count2; k++)
		{
			if (!f_0010(flag, flag2, foundobjects[k]))
			{
				foundobjects[k] = null;
			}
		}
	}

	private bool f_0010(bool P_0, bool P_1, ILight P_2)
	{
		if (!P_0 && !P_1)
		{
			return false;
		}
		if (!P_2.Enabled)
		{
			return P_1;
		}
		Vector3 compositeColorAndIntensity = P_2.CompositeColorAndIntensity;
		if (compositeColorAndIntensity.X + compositeColorAndIntensity.Y + compositeColorAndIntensity.Z > fNa)
		{
			return P_0;
		}
		return P_1;
	}

	/// <summary>
	/// Generates approximate lighting for an area in world space. The returned composite
	/// lighting is packed into a single directional and ambient light for fast single-pass lighting.
	///
	/// Note: because this information is approximated smaller world space areas will
	/// result in more accurate lighting. Also the approximation is calculated on the
	/// cpu and cannot take into account shadowing.
	/// </summary>
	/// <param name="worldbounds">Bounding area used to determine approximate lighting.</param>
	/// <param name="ambientblend">Blending value (0.0f - 1.0f) that determines how much approximate lighting
	/// contributes to ambient lighting. Approximate lighting can create highly directional lighting, using
	/// a higher blending value can create softer, more realistic lighting.</param>
	/// <param name="lightingtype">Light types to include in approximate lighting.</param>
	/// <returns>Composite lighting packed into a single directional and ambient light.</returns>
	public CompositeLighting GetCompositeLighting(BoundingBox worldbounds, float ambientblend, LightingType lightingtype)
	{
		GetCompositeLighting(ref worldbounds, ambientblend, lightingtype, out var compositelighting);
		return compositelighting;
	}

	/// <summary>
	/// Generates approximate lighting for an area in world space. The returned composite
	/// lighting is packed into a single directional and ambient light for fast single-pass lighting.
	///
	/// Note: because this information is approximated smaller world space areas will
	/// result in more accurate lighting. Also the approximation is calculated on the
	/// cpu and cannot take into account shadowing.
	/// </summary>
	/// <param name="worldbounds">Bounding area used to determine approximate lighting.</param>
	/// <param name="ambientblend">Blending value (0.0f - 1.0f) that determines how much approximate lighting
	/// contributes to ambient lighting. Approximate lighting can create highly directional lighting, using
	/// a higher blending value can create softer, more realistic lighting.</param>
	/// <param name="lightingtype">Light types to include in approximate lighting.</param>
	/// <param name="compositelighting">Composite lighting packed into a single directional and ambient light.</param>
	public void GetCompositeLighting(ref BoundingBox worldbounds, float ambientblend, LightingType lightingtype, out CompositeLighting compositelighting)
	{
		fNg.Clear();
		Find(fNg, worldbounds, ObjectFilter.All);
		for (int k = 0; k < fNg.Count; k++)
		{
			BaseLight baseLight = fNg[k];
			L.o o2 = OptimizationSystem.fNB.g_0011(baseLight);
			if (o2.fNf == null && (baseLight.LightingType & lightingtype) == 0)
			{
				fNg[k] = null;
			}
		}
		GetCompositeLighting(fNg, ref worldbounds, ambientblend, out compositelighting);
	}

	/// <summary>
	/// Generates approximate lighting for an area in world space using a custom set of lights.
	/// The returned composite lighting is packed into a single directional and ambient light for
	/// fast single-pass lighting.
	///
	/// Note: because this information is approximated smaller world space areas will
	/// result in more accurate lighting. Also the approximation is calculated on the
	/// cpu and cannot take into account shadowing.
	/// </summary>
	/// <param name="sourcelights">Lights used to generate approximate lighting.</param>
	/// <param name="worldbounds">Bounding area used to determine approximate lighting.</param>
	/// <param name="ambientblend">Blending value (0.0f - 1.0f) that determines how much approximate lighting
	/// contributes to ambient lighting. Approximate lighting can create highly directional lighting, using
	/// a higher blending value can create softer, more realistic lighting.</param>
	/// <returns>Composite lighting packed into a single directional and ambient light.</returns>
	public CompositeLighting GetCompositeLighting(List<BaseLight> sourcelights, BoundingBox worldbounds, float ambientblend)
	{
		GetCompositeLighting(sourcelights, ref worldbounds, ambientblend, out var compositelighting);
		return compositelighting;
	}

	/// <summary>
	/// Generates approximate lighting for an area in world space using a custom set of lights.
	/// The returned composite lighting is packed into a single directional and ambient light for
	/// fast single-pass lighting.
	///
	/// Note: because this information is approximated smaller world space areas will
	/// result in more accurate lighting. Also the approximation is calculated on the
	/// cpu and cannot take into account shadowing.
	/// </summary>
	/// <param name="sourcelights">Lights used to generate approximate lighting.</param>
	/// <param name="worldbounds">Bounding area used to determine approximate lighting.</param>
	/// <param name="ambientblend">Blending value (0.0f - 1.0f) that determines how much approximate lighting
	/// contributes to ambient lighting. Approximate lighting can create highly directional lighting, using
	/// a higher blending value can create softer, more realistic lighting.</param>
	/// <param name="compositelighting">Composite lighting packed into a single directional and ambient light.</param>
	public void GetCompositeLighting(List<BaseLight> sourcelights, ref BoundingBox worldbounds, float ambientblend, out CompositeLighting compositelighting)
	{
		ILightMapManager lightMapManager = (ILightMapManager)ServiceProvider.GetManager(SceneInterface.LightMapManagerType, required: false);
		Vector3 zero = Vector3.Zero;
		Vector3 zero2 = Vector3.Zero;
		Vector3 zero3 = Vector3.Zero;
		Vector3 value = (worldbounds.Max + worldbounds.Min) * 0.5f;
		fNf.CompositeLights.AccumulationValue++;
		foreach (BaseLight sourcelight in sourcelights)
		{
			if (sourcelight == null || !sourcelight.Enabled)
			{
				continue;
			}
			fNf.CompositeLightSources.AccumulationValue++;
			L.o o2 = OptimizationSystem.fNB.g_0011(sourcelight);
			if (o2.fNB != null)
			{
				Vector3 value2 = o2.fNB.Position;
				Vector3.Subtract(ref value, ref value2, out var result);
				float num = result.Length();
				if (num > 0f)
				{
					result.X /= num;
					result.Y /= num;
					result.Z /= num;
				}
				float num2 = 1f - MathHelper.Clamp(num / o2.fNB.Radius, 0f, 1f);
				float num3 = sourcelight.Intensity * num2;
				if (num3 > 0f && o2.fNa != null)
				{
					float value3 = o2.fNa.Angle * 0.5f;
					float num4 = (float)Math.Cos(MathHelper.ToRadians(MathHelper.Clamp(value3, 0.01f, 89.99f)));
					float num5 = Vector3.Dot(o2.fNa.Direction, result);
					num3 *= MathHelper.Clamp((num5 - num4) / (1f - num4), 0f, 1f);
				}
				zero.X += result.X * num3;
				zero.Y += result.Y * num3;
				zero.Z += result.Z * num3;
				Vector3 diffuseColor = sourcelight.DiffuseColor;
				zero2.X += num3 * diffuseColor.X;
				zero2.Y += num3 * diffuseColor.Y;
				zero2.Z += num3 * diffuseColor.Z;
			}
			else if (o2.fNg != null)
			{
				float num6 = 1f;
				if (lightMapManager != null)
				{
					LightOcclusionBuffer lightOcclusionBuffer = lightMapManager.GetLightOcclusionBuffer(sourcelight);
					if (lightOcclusionBuffer != null)
					{
						num6 = lightOcclusionBuffer.GetOcclusionAmount(value);
					}
				}
				float intensity = sourcelight.Intensity;
				Vector3 direction = o2.fNg.Direction;
				Vector3 compositeColorAndIntensity = sourcelight.CompositeColorAndIntensity;
				intensity *= num6;
				zero.X += direction.X * intensity;
				zero.Y += direction.Y * intensity;
				zero.Z += direction.Z * intensity;
				zero2.X += compositeColorAndIntensity.X * num6;
				zero2.Y += compositeColorAndIntensity.Y * num6;
				zero2.Z += compositeColorAndIntensity.Z * num6;
			}
			else if (o2.fNf != null)
			{
				Vector3 compositeColorAndIntensity2 = sourcelight.CompositeColorAndIntensity;
				zero3.X += compositeColorAndIntensity2.X;
				zero3.Y += compositeColorAndIntensity2.Y;
				zero3.Z += compositeColorAndIntensity2.Z;
			}
		}
		float num7 = MathHelper.Clamp(ambientblend, 0f, 1f);
		zero3.X += zero2.X * num7;
		zero3.Y += zero2.Y * num7;
		zero3.Z += zero2.Z * num7;
		float num8 = 1f - num7;
		zero2.X *= num8;
		zero2.Y *= num8;
		zero2.Z *= num8;
		compositelighting = default;
		compositelighting.DiffuseColor.X = zero2.X;
		compositelighting.DiffuseColor.Y = zero2.Y;
		compositelighting.DiffuseColor.Z = zero2.Z;
		compositelighting.AmbientColor.X = zero3.X;
		compositelighting.AmbientColor.Y = zero3.Y;
		compositelighting.AmbientColor.Z = zero3.Z;
		float num9 = zero.Length();
		if (num9 > 0f)
		{
			num9 = 1f / num9;
			compositelighting.Direction.X = zero.X * num9;
			compositelighting.Direction.Y = zero.Y * num9;
			compositelighting.Direction.Z = zero.Z * num9;
		}
	}

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
		SceneState = scenestate;
		SplashScreen._0003k();
		MoveDynamicObjects();
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public virtual void EndFrameRendering()
	{
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public virtual void Unload()
	{
		Clear();
	}
}
