using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Effects;

namespace j;

internal class n : _0003
{
	private Vector3 fNB;

	private Texture2D fNa;

	private EffectParameter fNg;

	private EffectParameter fNf;

	public Texture2D BloomTexture
	{
		get
		{
			return fNa;
		}
		set
		{
			EffectHelper.fO(value, ref fNa, ref fNf);
		}
	}

	public void SetSaturationDarkenAndCinematic(float saturation, float darken, float cinematic)
	{
		EffectHelper.fq(new Vector3(saturation, darken, cinematic), ref fNB, ref fNg);
	}

	public n(GraphicsDevice graphicsdevice)
		: base(graphicsdevice)
	{
		CurrentTechnique = Techniques["HDRFinalBlendPointFilteringTechnique"];
		fNf = Parameters["_BloomTexture"];
		fNg = Parameters["_Saturation_Darken_Cinematic"];
	}

	protected override void SetTechnique()
	{
		Texture2D sourceTexture = SourceTexture;
		bool flag = sourceTexture != null && sourceTexture.Format < SurfaceFormat.Single;
		if (DynamicRangeEnabled)
		{
			if (flag)
			{
				CurrentTechnique = Techniques["HDRFinalBlendLinearFilteringTechnique"];
			}
			else
			{
				CurrentTechnique = Techniques["HDRFinalBlendPointFilteringTechnique"];
			}
		}
		else if (flag)
		{
			CurrentTechnique = Techniques["HDRFinalBlendLinearFilteringBloomOnlyTechnique"];
		}
		else
		{
			CurrentTechnique = Techniques["HDRFinalBlendPointFilteringBloomOnlyTechnique"];
		}
	}
}
