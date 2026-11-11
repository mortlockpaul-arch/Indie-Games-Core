using System;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Effects;

public abstract class BaseRenderableEffect : Effect, IRenderableEffect
{
	internal class w8a
	{
		internal SystemStatistic l8a = SystemConsole.GetStatistic("Effect_TechniqueChanges", SystemStatisticCategory.Rendering);

		internal SystemStatistic l8_u0013_ = SystemConsole.GetStatistic("Effect_MatrixParameterChanges", SystemStatisticCategory.Rendering);

		internal SystemStatistic l8N = SystemConsole.GetStatistic("Effect_LightSourceChanges", SystemStatisticCategory.Rendering);
	}

	private bool l8a;

	private DetailPreference l8_u0013_;

	private Matrix l8N;

	private Matrix l8l;

	private Matrix l8L;

	private Matrix l8w;

	private Matrix l8J;

	private Matrix l8B;

	private Matrix l8s;

	private Matrix l8h;

	private Matrix l8_u0002_;

	private float l8v;

	private EffectParameter l8Y;

	private EffectParameter l8_u0005_;

	private EffectParameter l8b;

	private EffectParameter l8I;

	private EffectParameter l85;

	private EffectParameter l8W;

	private EffectParameter l8Z;

	private EffectParameter l8T;

	private EffectParameter l8_u0014_;

	private EffectParameter l8E;

	private EffectParameter l8f;

	internal w8a l8_u0016_ = new w8a();

	protected bool _UpdatedByBatch;

	public Matrix World
	{
		get
		{
			return l8N;
		}
		set
		{
			if (!N_u0014_(ref value))
			{
				EffectHelper.ll(value, ref l8N, ref l8l, ref l8Y, ref l8_u0005_);
				SetWorldViewProjection(viewprojectionchanged: false, setslowwindingdirection: true);
			}
		}
	}

	public Matrix WorldToObject => l8l;

	public Matrix View
	{
		get
		{
			return l8L;
		}
		set
		{
			EffectHelper.ll(value, ref l8L, ref l8w, ref l8b, ref l8I);
			SetWorldViewProjection(viewprojectionchanged: true, setslowwindingdirection: true);
		}
	}

	public Matrix ViewToWorld => l8w;

	public Matrix Projection
	{
		get
		{
			return l8J;
		}
		set
		{
			if (value != l8J)
			{
				l8J = value;
				if (l85 != null)
				{
					l85.SetValue(l8J);
				}
				if (l8W != null || l8f != null)
				{
					l8B = Matrix.Invert(l8J);
					if (l8W != null)
					{
						l8W.SetValue(l8B);
					}
				}
			}
			SetWorldViewProjection(viewprojectionchanged: true, setslowwindingdirection: true);
		}
	}

	public Matrix ProjectionToView => l8B;

	public abstract bool DoubleSided { get; set; }

	public DetailPreference EffectDetail
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			if (value != l8_u0013_)
			{
				l8_u0013_ = value;
				SetTechnique();
			}
		}
	}

	public bool UpdatedByBatch
	{
		get
		{
			return _UpdatedByBatch;
		}
		set
		{
			_UpdatedByBatch = false;
		}
	}

	private float NT()
	{
		if (l8_u0002_.Determinant() < 0f)
		{
			return 1f;
		}
		return -1f;
	}

	protected abstract void SetTechnique();

	protected virtual void SetWorldViewProjection(bool viewprojectionchanged, bool setslowwindingdirection)
	{
		if (l8_u0014_ == null && l8T == null && l8Z == null && l8E == null && l8f == null)
		{
			return;
		}
		if (l8T != null)
		{
			Matrix.Multiply(ref l8N, ref l8L, out var result);
			if (!result.Equals(l8h))
			{
				l8h = result;
				l8T.SetValue(l8h);
				_UpdatedByBatch = true;
				l8_u0016_.l8_u0013_.AccumulationValue++;
			}
		}
		if (l8Z != null || l8_u0014_ != null || l8E != null)
		{
			if (viewprojectionchanged)
			{
				Matrix.Multiply(ref l8L, ref l8J, out l8s);
				if (l8Z != null)
				{
					l8Z.SetValue(l8s);
					_UpdatedByBatch = true;
					l8_u0016_.l8_u0013_.AccumulationValue++;
				}
			}
			if (l8_u0014_ != null || l8E != null)
			{
				Matrix.Multiply(ref l8N, ref l8s, out var result2);
				if (!result2.Equals(l8_u0002_))
				{
					l8_u0002_ = result2;
					if (l8_u0014_ != null)
					{
						l8_u0014_.SetValue(l8_u0002_);
						_UpdatedByBatch = true;
						l8_u0016_.l8_u0013_.AccumulationValue++;
					}
					if (setslowwindingdirection && l8E != null)
					{
						l8E.SetValue(NT());
						_UpdatedByBatch = true;
						l8_u0016_.l8_u0013_.AccumulationValue++;
					}
				}
			}
		}
		if (viewprojectionchanged && l8f != null)
		{
			Vector4 vector = Vector4.Transform(new Vector4(0f, 0f, 1f, 1f), ProjectionToView);
			float num = 0f;
			if (vector.W != 0f)
			{
				num = Math.Abs(vector.Z / vector.W);
			}
			if (l8v != num)
			{
				l8v = num;
				l8f.SetValue(num);
				_UpdatedByBatch = true;
			}
		}
	}

	public void SetViewAndProjection(Matrix view, Matrix viewtoworld, Matrix projection, Matrix projectiontoview)
	{
		bool flag = false;
		if (view != l8L)
		{
			l8L = view;
			if (l8b != null)
			{
				l8b.SetValue(l8L);
				l8_u0016_.l8_u0013_.AccumulationValue++;
			}
			if (l8I != null)
			{
				l8w = viewtoworld;
				l8I.SetValue(l8w);
				l8_u0016_.l8_u0013_.AccumulationValue++;
			}
			flag = true;
		}
		if (projection != l8J)
		{
			l8J = projection;
			if (l85 != null)
			{
				l85.SetValue(l8J);
				l8_u0016_.l8_u0013_.AccumulationValue++;
			}
			if (l8W != null || l8f != null)
			{
				l8B = projectiontoview;
				if (l8W != null)
				{
					l8W.SetValue(l8B);
					l8_u0016_.l8_u0013_.AccumulationValue++;
				}
			}
			flag = true;
		}
		if (flag)
		{
			SetWorldViewProjection(viewprojectionchanged: true, setslowwindingdirection: true);
		}
	}

	private bool N_u0014_(ref Matrix param_0)
	{
		if (l8a)
		{
			l8N = Matrix.Identity;
			l8a = false;
			return false;
		}
		return param_0.Equals(l8N);
	}

	public void SetWorldAndWorldToObject(ref Matrix world, ref Matrix worldtoobj)
	{
		if (!N_u0014_(ref world))
		{
			_UpdatedByBatch = true;
			l8N = world;
			if (l8Y != null)
			{
				l8Y.SetValue(l8N);
				l8_u0016_.l8_u0013_.AccumulationValue++;
			}
			if (l8_u0005_ != null)
			{
				l8l = worldtoobj;
				l8_u0005_.SetValue(l8l);
				l8_u0016_.l8_u0013_.AccumulationValue++;
			}
			SetWorldViewProjection(viewprojectionchanged: false, setslowwindingdirection: true);
		}
	}

	internal BaseRenderableEffect(GraphicsDevice param_0, string param_1)
		: base(param_0, SunBurnCoreSystem.Instance._u0013__u0001_(param_1).ByteCode)
	{
		l8Y = Parameters["_World"];
		l8_u0005_ = Parameters["_WorldToObject"];
		l8b = Parameters["_View"];
		l8I = Parameters["_ViewToWorld"];
		l85 = Parameters["_Projection"];
		l8W = Parameters["_ProjectionToView"];
		l8Z = Parameters["_ViewProjection"];
		l8T = Parameters["_WorldView"];
		l8_u0014_ = Parameters["_WorldViewProjection"];
		l8E = Parameters["_WindingDirection"];
		l8f = Parameters["_FarClippingDistance"];
	}

	public override Effect Clone()
	{
		Effect effect = Create();
		B.L._u0013_B(this, effect);
		return effect;
	}

	protected abstract Effect Create();
}
