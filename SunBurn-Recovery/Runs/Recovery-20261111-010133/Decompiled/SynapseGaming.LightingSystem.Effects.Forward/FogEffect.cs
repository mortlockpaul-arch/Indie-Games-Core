using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using b;

namespace SynapseGaming.LightingSystem.Effects.Forward;

public class FogEffect : BaseSkinnedEffect, ITerrainEffect
{
	private float l8a;

	private float l8_u0013_;

	private Vector3 l8N;

	private int l8l;

	private int l8L;

	private float l8w;

	private float l8J;

	private Texture2D l8B;

	private EffectParameter l8s;

	private EffectParameter l8h;

	private EffectParameter l8_u0002_;

	private EffectParameter l8v;

	private EffectParameter l8Y;

	private EffectParameter l8_u0005_;

	private EffectParameter l8b;

	[CompilerGenerated]
	private bool l8I;

	public float StartDistance
	{
		get
		{
			return l8a;
		}
		set
		{
			lI(value, l8_u0013_);
		}
	}

	public float EndDistance
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			lI(l8a, value);
		}
	}

	public Vector3 Color
	{
		get
		{
			return l8N;
		}
		set
		{
			if (!(l8N == value) && l8h != null)
			{
				l8N = value;
				l8h.SetValue(new Vector4(l8N.X, l8N.Y, l8N.Z, 0f));
			}
		}
	}

	public Texture2D HeightMapTexture
	{
		get
		{
			return l8B;
		}
		set
		{
			if (value != l8B)
			{
				EffectHelper.lh(value, ref l8B, ref l8b);
				SetTechnique();
			}
		}
	}

	public float HeightScale
	{
		get
		{
			return l8w;
		}
		set
		{
			EffectHelper.lB(value, ref l8w, ref l8Y);
		}
	}

	public float Tiling
	{
		get
		{
			return l8J;
		}
		set
		{
			EffectHelper.lB(value, ref l8J, ref l8_u0005_);
		}
	}

	public int TileRepeatCount
	{
		get
		{
			return l8L;
		}
		set
		{
			EffectHelper.lJ(value, ref l8L, ref l8v);
		}
	}

	public int MeshSegments
	{
		get
		{
			return l8l;
		}
		set
		{
			EffectHelper.lJ(value, ref l8l, ref l8_u0002_);
		}
	}

	public override bool DoubleSided
	{
		[CompilerGenerated]
		get
		{
			return l8I;
		}
		[CompilerGenerated]
		set
		{
			l8I = value;
		}
	}

	private void lI(float param_0, float param_1)
	{
		if (l8s != null && (l8a != param_0 || l8_u0013_ != param_1))
		{
			l8a = Math.Max(param_0, 0f);
			l8_u0013_ = Math.Max(l8a * 1.01f, param_1);
			float num = l8_u0013_ - l8a;
			if (num != 0f)
			{
				num = 1f / num;
			}
			l8s.SetValue(new Vector4(l8a, num, 0f, 0f));
		}
	}

	protected override void SetTechnique()
	{
		l8_u0016_.l8a.AccumulationValue++;
		if (l8B != null)
		{
			CurrentTechnique = Techniques["Fog_Terrain_Technique"];
		}
		else
		{
			CurrentTechnique = Techniques[b.l.lb(b.l.w8a.Fog, b.l.w8_u0013_.None, 0, param_3: false, param_4: false, Skinned, param_6: false)];
		}
	}

	public FogEffect(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "FogEffect")
	{
		l8s = Parameters["_FogStartDist_And_EndDistInv"];
		l8h = Parameters["_FogColor"];
		l8b = Parameters["HeightMapTexture"];
		l8_u0002_ = Parameters["MeshSegments"];
		l8v = Parameters["MeshRepeatCount"];
		l8Y = Parameters["HeightScale"];
		l8_u0005_ = Parameters["Tiling"];
		StartDistance = 1000f;
		EndDistance = 100000f;
		Color = new Vector3(0.5f, 0.5f, 0.5f);
		TileRepeatCount = 1;
		SetTechnique();
	}

	protected override Effect Create()
	{
		return new FogEffect(GraphicsDevice);
	}
}
