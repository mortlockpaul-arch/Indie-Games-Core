using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Effects;

/// <summary>
/// Provides basic skinned animation rendering support.
/// </summary>
public abstract class BaseSkinnedEffect : BaseRenderableEffect, ISkinnedEffect
{
	private bool fNB;

	private Matrix[] fNa;

	private EffectParameter fNg;

	private Matrix[] fNf = new Matrix[1];

	/// <summary>
	/// Array of bone transforms for the skeleton's current pose. The matrix index is the
	/// same as the bone order used in the model or vertex buffer.
	/// </summary>
	public Matrix[] SkinBones
	{
		get
		{
			return fNa;
		}
		set
		{
			if (value != null)
			{
				_UpdatedByBatch = true;
				EffectHelper.fW(value, ref fNa, ref fNg);
			}
			else
			{
				if (!fNB || fNg == null)
				{
					return;
				}
				if (fNf.Length < fNg.Elements.Count)
				{
					fNf = new Matrix[fNg.Elements.Count];
					for (int i = 0; i < fNf.Length; i++)
					{
						ref Matrix reference = ref fNf[i];
						reference = Matrix.Identity;
					}
				}
				if (fNa != fNf)
				{
					_UpdatedByBatch = true;
					fNa = fNf;
					fNg.SetValue(fNa);
				}
			}
		}
	}

	/// <summary>
	/// Determines if the effect is currently rendering skinned objects.
	/// </summary>
	public bool Skinned
	{
		get
		{
			return fNB;
		}
		set
		{
			if (value != fNB)
			{
				fNB = value;
				SetTechnique();
				if (fNB && fNa == null)
				{
					SkinBones = null;
				}
			}
		}
	}

	internal BaseSkinnedEffect(GraphicsDevice P_0, string P_1)
		: base(P_0, P_1)
	{
		fNg = Parameters["_SkinBones"];
	}
}
