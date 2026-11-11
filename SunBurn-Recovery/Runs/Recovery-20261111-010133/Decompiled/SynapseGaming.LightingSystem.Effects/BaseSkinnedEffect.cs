using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Effects;

public abstract class BaseSkinnedEffect : BaseRenderableEffect, ISkinnedEffect
{
	private bool l8a;

	private Matrix[] l8_u0013_;

	private EffectParameter l8N;

	private Matrix[] l8l = new Matrix[1];

	public Matrix[] SkinBones
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			if (value != null)
			{
				_UpdatedByBatch = true;
				EffectHelper.lN(value, ref l8_u0013_, ref l8N);
			}
			else
			{
				if (!l8a || l8N == null)
				{
					return;
				}
				if (l8l.Length < l8N.Elements.Count)
				{
					l8l = new Matrix[l8N.Elements.Count];
					for (int i = 0; i < l8l.Length; i++)
					{
						ref Matrix reference = ref l8l[i];
						reference = Matrix.Identity;
					}
				}
				if (l8_u0013_ != l8l)
				{
					_UpdatedByBatch = true;
					l8_u0013_ = l8l;
					l8N.SetValue(l8_u0013_);
				}
			}
		}
	}

	public bool Skinned
	{
		get
		{
			return l8a;
		}
		set
		{
			if (value != l8a)
			{
				l8a = value;
				SetTechnique();
				if (l8a && l8_u0013_ == null)
				{
					SkinBones = null;
				}
			}
		}
	}

	internal BaseSkinnedEffect(GraphicsDevice param_0, string param_1)
		: base(param_0, param_1)
	{
		l8N = Parameters["_SkinBones"];
	}
}
