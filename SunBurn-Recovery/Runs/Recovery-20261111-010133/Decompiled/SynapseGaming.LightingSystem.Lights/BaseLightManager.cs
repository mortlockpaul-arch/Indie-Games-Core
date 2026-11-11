using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Lights;

public abstract class BaseLightManager<IManagerServiceType> : BaseObjectGraphManager<BaseLight, IManagerServiceType>, IManager, IUnloadable, ILightQuery, IQuery<BaseLight>
{
	private class w8a
	{
		public SystemStatistic CompositeLights = SystemConsole.GetStatistic("Light_CompositeLights", SystemStatisticCategory.Lighting);

		public SystemStatistic CompositeLightSources = SystemConsole.GetStatistic("Light_CompositeLightSources", SystemStatisticCategory.Lighting);
	}

	private static float l8a = 0.004f;

	protected ISceneState SceneState;

	private List<BaseLight> l8_u0013_ = new List<BaseLight>(16);

	private w8a l8N = new w8a();

	public BaseLightManager(IManagerServiceProvider sceneinterface, BoundingBox worldboundingbox, int worldtreemaxdepth)
		: base(sceneinterface, worldboundingbox, worldtreemaxdepth)
	{
	}

	public BaseLightManager(IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
	}

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
		for (int i = count; i < count2; i++)
		{
			if (!lQ(flag, flag2, foundobjects[i]))
			{
				foundobjects[i] = null;
			}
		}
	}

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
		for (int i = count; i < count2; i++)
		{
			if (!lQ(flag, flag2, foundobjects[i]))
			{
				foundobjects[i] = null;
			}
		}
	}

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
		for (int i = count; i < count2; i++)
		{
			if (!lQ(flag, flag2, foundobjects[i]))
			{
				foundobjects[i] = null;
			}
		}
	}

	private bool lQ(bool param_0, bool param_1, ILight param_2)
	{
		if (!param_0 && !param_1)
		{
			return false;
		}
		if (!param_2.Enabled)
		{
			return param_1;
		}
		Vector3 compositeColorAndIntensity = param_2.CompositeColorAndIntensity;
		if (compositeColorAndIntensity.X + compositeColorAndIntensity.Y + compositeColorAndIntensity.Z > l8a)
		{
			return param_0;
		}
		return param_1;
	}

	public CompositeLighting GetCompositeLighting(BoundingBox worldbounds, float ambientblend, LightingType lightingtype)
	{
		GetCompositeLighting(ref worldbounds, ambientblend, lightingtype, out var compositelighting);
		return compositelighting;
	}

	public void GetCompositeLighting(ref BoundingBox worldbounds, float ambientblend, LightingType lightingtype, out CompositeLighting compositelighting)
	{
		l8_u0013_.Clear();
		Find(l8_u0013_, worldbounds, ObjectFilter.All);
		for (int i = 0; i < l8_u0013_.Count; i++)
		{
			BaseLight baseLight = l8_u0013_[i];
			LightTypeCaster lightTypeCaster = OptimizationSystem.LightTypeCasters.Get(baseLight);
			if (lightTypeCaster.AmbientSource == null && (baseLight.LightingType & lightingtype) == 0)
			{
				l8_u0013_[i] = null;
			}
		}
		GetCompositeLighting(l8_u0013_, ref worldbounds, ambientblend, out compositelighting);
	}

	public CompositeLighting GetCompositeLighting(List<BaseLight> sourcelights, BoundingBox worldbounds, float ambientblend)
	{
		GetCompositeLighting(sourcelights, ref worldbounds, ambientblend, out var compositelighting);
		return compositelighting;
	}

	public void GetCompositeLighting(List<BaseLight> sourcelights, ref BoundingBox worldbounds, float ambientblend, out CompositeLighting compositelighting)
	{
		ILightMapManager lightMapManager = (ILightMapManager)OwnerSceneInterface.GetManager(SceneInterface.LightMapManagerType, required: false);
		Vector3 zero = Vector3.Zero;
		Vector3 zero2 = Vector3.Zero;
		Vector3 zero3 = Vector3.Zero;
		Vector3 value = (worldbounds.Max + worldbounds.Min) * 0.5f;
		l8N.CompositeLights.AccumulationValue++;
		foreach (BaseLight sourcelight in sourcelights)
		{
			if (sourcelight == null || !sourcelight.Enabled)
			{
				continue;
			}
			l8N.CompositeLightSources.AccumulationValue++;
			LightTypeCaster lightTypeCaster = OptimizationSystem.LightTypeCasters.Get(sourcelight);
			if (lightTypeCaster.PointSource != null)
			{
				Vector3 value2 = lightTypeCaster.PointSource.Position;
				Vector3.Subtract(ref value, ref value2, out var result);
				float num = result.Length();
				if (num > 0f)
				{
					result.X /= num;
					result.Y /= num;
					result.Z /= num;
				}
				float num2 = 1f - MathHelper.Clamp(num / lightTypeCaster.PointSource.Radius, 0f, 1f);
				float num3 = sourcelight.Intensity * num2;
				if (num3 > 0f && lightTypeCaster.SpotSource != null)
				{
					float value3 = lightTypeCaster.SpotSource.Angle * 0.5f;
					float num4 = (float)Math.Cos(MathHelper.ToRadians(MathHelper.Clamp(value3, 0.01f, 89.99f)));
					float num5 = Vector3.Dot(lightTypeCaster.SpotSource.Direction, result);
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
			else if (lightTypeCaster.DirectionalSource != null)
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
				Vector3 direction = lightTypeCaster.DirectionalSource.Direction;
				Vector3 compositeColorAndIntensity = sourcelight.CompositeColorAndIntensity;
				intensity *= num6;
				zero.X += direction.X * intensity;
				zero.Y += direction.Y * intensity;
				zero.Z += direction.Z * intensity;
				zero2.X += compositeColorAndIntensity.X * num6;
				zero2.Y += compositeColorAndIntensity.Y * num6;
				zero2.Z += compositeColorAndIntensity.Z * num6;
			}
			else if (lightTypeCaster.AmbientSource != null)
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

	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
		SceneState = scenestate;
		SplashScreen.L_u0015_();
	}

	public virtual void EndFrameRendering()
	{
	}
}
