using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using j;

namespace SynapseGaming.LightingSystem.Effects.Forward;

/// <summary>
/// Effect provides per-pixel fog.
/// </summary>
public class FogEffect : BaseSkinnedEffect, ITerrainEffect
{
	private float fNB;

	private float fNa;

	private Vector3 fNg;

	private int fNf;

	private float fN_0003;

	private float fNn;

	private Texture2D fN_0019;

	private EffectParameter fNL;

	private EffectParameter fNv;

	private EffectParameter fN_0012;

	private EffectParameter fN_0015;

	private EffectParameter fN8;

	private EffectParameter fNi;

	/// <summary>
	/// Distance from the camera in world space that fog begins.
	/// </summary>
	public float StartDistance
	{
		get
		{
			return fNB;
		}
		set
		{
			fV(value, fNa);
		}
	}

	/// <summary>
	/// Distance from the camera in world space that fog ends.
	/// </summary>
	public float EndDistance
	{
		get
		{
			return fNa;
		}
		set
		{
			fV(fNB, value);
		}
	}

	/// <summary>
	/// Color of the applied fog.
	/// </summary>
	public Vector3 Color
	{
		get
		{
			return fNg;
		}
		set
		{
			if (!(fNg == value) && fNv != null)
			{
				fNg = value;
				fNv.SetValue(new Vector4(fNg.X, fNg.Y, fNg.Z, 0f));
			}
		}
	}

	/// <summary>
	/// Texture containing height values used to displace a terrain mesh. Also used
	/// for low frequency lighting.
	/// </summary>
	public Texture2D HeightMapTexture
	{
		get
		{
			return fN_0019;
		}
		set
		{
			if (value != fN_0019)
			{
				EffectHelper.fO(value, ref fN_0019, ref fNi);
				SetTechnique();
			}
		}
	}

	/// <summary>
	/// Adjusts the terrain displacement magnitude.
	/// </summary>
	public float HeightScale
	{
		get
		{
			return fN_0003;
		}
		set
		{
			EffectHelper.f_0014(value, ref fN_0003, ref fN_0015);
		}
	}

	/// <summary>
	/// Adjusts the number of times the height map tiles across a terrain's
	/// mesh. Similar to uv scale when texture mapping.
	/// </summary>
	public float Tiling
	{
		get
		{
			return fNn;
		}
		set
		{
			EffectHelper.f_0014(value, ref fNn, ref fN8);
		}
	}

	/// <summary>
	/// Density or tessellation of the terrain mesh.
	/// </summary>
	public int MeshSegments
	{
		get
		{
			return fNf;
		}
		set
		{
			EffectHelper.f_000F(value, ref fNf, ref fN_0012);
		}
	}

	private void fV(float P_0, float P_1)
	{
		if (fNL != null && (fNB != P_0 || fNa != P_1))
		{
			fNB = Math.Max(P_0, 0f);
			fNa = Math.Max(fNB * 1.01f, P_1);
			float num = fNa - fNB;
			if (num != 0f)
			{
				num = 1f / num;
			}
			fNL.SetValue(new Vector4(fNB, num, 0f, 0f));
		}
	}

	/// <summary>
	/// Sets the effect technique based on its current property values.
	/// </summary>
	protected override void SetTechnique()
	{
		fNm.fNB.AccumulationValue++;
		if (fN_0019 != null)
		{
			CurrentTechnique = Techniques["Fog_Terrain_Technique"];
		}
		else
		{
			CurrentTechnique = Techniques[j.f.fF(j.f.nNB.Fog, j.f.nNa.None, 0, false, false, Skinned, false)];
		}
	}

	/// <summary>
	/// Creates a new FogEffect instance.
	/// </summary>
	/// <param name="graphicsdevice"></param>
	public FogEffect(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "FogEffect")
	{
		fNL = Parameters["_FogStartDist_And_EndDistInv"];
		fNv = Parameters["_FogColor"];
		fNi = Parameters["HeightMapTexture"];
		fN_0012 = Parameters["MeshSegments"];
		fN_0015 = Parameters["HeightScale"];
		fN8 = Parameters["Tiling"];
		StartDistance = 1000f;
		EndDistance = 100000f;
		Color = new Vector3(0.5f, 0.5f, 0.5f);
		SetTechnique();
	}

	/// <summary>
	/// Creates a new empty effect of the same class type and using the same effect file as this object.
	/// </summary>
	/// <returns></returns>
	protected override Effect Create()
	{
		return new FogEffect(GraphicsDevice);
	}
}
