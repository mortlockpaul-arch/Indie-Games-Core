using System;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;

namespace SynapseGaming.LightingSystem.Effects;

/// <summary>
/// Provides basic rendering support.
/// </summary>
public abstract class BaseRenderableEffect : Effect, IRenderableEffect
{
	internal class nNB
	{
		internal SystemStatistic fNB = SystemConsole.GetStatistic("Effect_TechniqueChanges", SystemStatisticCategory.Rendering);

		internal SystemStatistic fNa = SystemConsole.GetStatistic("Effect_MatrixParameterChanges", SystemStatisticCategory.Rendering);

		internal SystemStatistic fNg = SystemConsole.GetStatistic("Effect_LightSourceChanges", SystemStatisticCategory.Rendering);
	}

	private bool fNB;

	private bool fNa;

	private DetailPreference fNg;

	private Matrix fNf;

	private Matrix fN_0003;

	private Matrix fNn;

	private Matrix fN_0019;

	private Matrix fNL;

	private Matrix fNv;

	private Matrix fN_0012;

	private Matrix fN_0015;

	private Matrix fN8;

	private float fNi;

	private EffectParameter fN_0005;

	private EffectParameter fNj;

	private EffectParameter fNo;

	private EffectParameter fNc;

	private EffectParameter fNd;

	private EffectParameter fNw;

	private EffectParameter fNs;

	private EffectParameter fN_0011;

	private EffectParameter fNQ;

	private EffectParameter fNG;

	private EffectParameter fN_0017;

	internal nNB fNm = new nNB();

	/// <summary>
	/// Set value to true when changes to a property cause calls to EffectParameter.SetValue.
	/// This tells the renderer to commit changes made during Effect Begin/End.
	/// </summary>
	protected bool _UpdatedByBatch;

	/// <summary>
	/// World matrix applied to geometry using this effect.
	/// </summary>
	public Matrix World
	{
		get
		{
			return fNf;
		}
		set
		{
			if (!g3(ref value))
			{
				EffectHelper.f9(value, ref fNf, ref fN_0003, ref fN_0005, ref fNj);
				SetWorldViewProjection(viewprojectionchanged: false, setslowwindingdirection: true);
			}
		}
	}

	/// <summary>
	/// Inverse world matrix applied to geometry using this effect.
	/// </summary>
	public Matrix WorldToObject => fN_0003;

	/// <summary>
	/// View matrix applied to geometry using this effect.
	/// </summary>
	public Matrix View
	{
		get
		{
			return fNn;
		}
		set
		{
			EffectHelper.f9(value, ref fNn, ref fN_0019, ref fNo, ref fNc);
			SetWorldViewProjection(viewprojectionchanged: true, setslowwindingdirection: true);
		}
	}

	/// <summary>
	/// Inverse view matrix applied to geometry using this effect.
	/// </summary>
	public Matrix ViewToWorld => fN_0019;

	/// <summary>
	/// Projection matrix applied to geometry using this effect.
	/// </summary>
	public Matrix Projection
	{
		get
		{
			return fNL;
		}
		set
		{
			if (value != fNL)
			{
				fNL = value;
				if (fNd != null)
				{
					fNd.SetValue(fNL);
				}
				if (fNw != null || fN_0017 != null)
				{
					fNv = Matrix.Invert(fNL);
					if (fNw != null)
					{
						fNw.SetValue(fNv);
					}
				}
			}
			SetWorldViewProjection(viewprojectionchanged: true, setslowwindingdirection: true);
		}
	}

	/// <summary>
	/// Inverse projection matrix applied to geometry using this effect.
	/// </summary>
	public Matrix ProjectionToView => fNv;

	/// <summary>
	/// Surfaces rendered with the effect should be visible from both sides.
	/// </summary>
	[EditorProperty(true, Description = "Double Sided", HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 10, ToolTipText = "")]
	public bool DoubleSided
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
		}
	}

	/// <summary>
	/// Applies the user's effect preference. This generally trades detail
	/// for performance based on the user's selection.
	/// </summary>
	public DetailPreference EffectDetail
	{
		get
		{
			return fNg;
		}
		set
		{
			if (value != fNg)
			{
				fNg = value;
				SetTechnique();
			}
		}
	}

	/// <summary>
	/// Determines if the renderer should call Apply within an effect Begin/End due
	/// to internal calls to EffectParameter.SetValue. The renderer should set this value
	/// to false after calling Apply.
	/// </summary>
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

	private float g_0004()
	{
		if (fN8.Determinant() < 0f)
		{
			return 1f;
		}
		return -1f;
	}

	/// <summary>
	/// Sets the effect technique based on its current property values.
	/// </summary>
	protected abstract void SetTechnique();

	/// <summary>
	/// Recalculates the combination view-projection and world-view-projection matrix
	/// based on the individual world, view, and projection.
	/// </summary>
	protected virtual void SetWorldViewProjection(bool viewprojectionchanged, bool setslowwindingdirection)
	{
		if (fNQ == null && fN_0011 == null && fNs == null && fNG == null && fN_0017 == null)
		{
			return;
		}
		if (fN_0011 != null)
		{
			Matrix.Multiply(ref fNf, ref fNn, out var result);
			if (!result.Equals(fN_0015))
			{
				fN_0015 = result;
				fN_0011.SetValue(fN_0015);
				_UpdatedByBatch = true;
				fNm.fNa.AccumulationValue++;
			}
		}
		if (fNs != null || fNQ != null || fNG != null)
		{
			if (viewprojectionchanged)
			{
				Matrix.Multiply(ref fNn, ref fNL, out fN_0012);
				if (fNs != null)
				{
					fNs.SetValue(fN_0012);
					_UpdatedByBatch = true;
					fNm.fNa.AccumulationValue++;
				}
			}
			if (fNQ != null || fNG != null)
			{
				Matrix.Multiply(ref fNf, ref fN_0012, out var result2);
				if (!result2.Equals(fN8))
				{
					fN8 = result2;
					if (fNQ != null)
					{
						fNQ.SetValue(fN8);
						_UpdatedByBatch = true;
						fNm.fNa.AccumulationValue++;
					}
					if (setslowwindingdirection && fNG != null)
					{
						fNG.SetValue(g_0004());
						_UpdatedByBatch = true;
						fNm.fNa.AccumulationValue++;
					}
				}
			}
		}
		if (viewprojectionchanged && fN_0017 != null)
		{
			Vector4 vector = Vector4.Transform(new Vector4(0f, 0f, 1f, 1f), ProjectionToView);
			float num = 0f;
			if (vector.W != 0f)
			{
				num = Math.Abs(vector.Z / vector.W);
			}
			if (fNi != num)
			{
				fNi = num;
				fN_0017.SetValue(num);
				_UpdatedByBatch = true;
			}
		}
	}

	/// <summary>
	/// Sets both the view, projection, and their inverse matrices.  Used to improve
	/// performance in effects that automatically generate an inverse
	/// matrix when the view and project are set, by providing a cached
	/// or precalculated inverse matrix with the view and project matrices.
	/// </summary>
	/// <param name="view">View matrix applied to geometry using this effect.</param>
	/// <param name="viewtoworld">Inverse view matrix applied to geometry using this effect.</param>
	/// <param name="projection">Projection matrix applied to geometry using this effect.</param>
	/// <param name="projectiontoview">Inverse projection matrix applied to geometry using this effect.</param>
	public void SetViewAndProjection(Matrix view, Matrix viewtoworld, Matrix projection, Matrix projectiontoview)
	{
		bool flag = false;
		if (view != fNn)
		{
			fNn = view;
			if (fNo != null)
			{
				fNo.SetValue(fNn);
				fNm.fNa.AccumulationValue++;
			}
			if (fNc != null)
			{
				fN_0019 = viewtoworld;
				fNc.SetValue(fN_0019);
				fNm.fNa.AccumulationValue++;
			}
			flag = true;
		}
		if (projection != fNL)
		{
			fNL = projection;
			if (fNd != null)
			{
				fNd.SetValue(fNL);
				fNm.fNa.AccumulationValue++;
			}
			if (fNw != null || fN_0017 != null)
			{
				fNv = projectiontoview;
				if (fNw != null)
				{
					fNw.SetValue(fNv);
					fNm.fNa.AccumulationValue++;
				}
			}
			flag = true;
		}
		if (flag)
		{
			SetWorldViewProjection(viewprojectionchanged: true, setslowwindingdirection: true);
		}
	}

	private bool g3(ref Matrix P_0)
	{
		if (fNB)
		{
			fNf = Matrix.Identity;
			fNB = false;
			return false;
		}
		return P_0.Equals(fNf);
	}

	/// <summary>
	/// Sets both the world and inverse world matrices.  Used to improve
	/// performance in effects that automatically generate an inverse
	/// world matrix when the world matrix is set, by providing a cached
	/// or precalculated inverse matrix with the world matrix.
	/// </summary>
	/// <param name="world">World matrix applied to geometry using this effect.</param>
	/// <param name="worldtoobj">Inverse world matrix applied to geometry using this effect.</param>
	public void SetWorldAndWorldToObject(ref Matrix world, ref Matrix worldtoobj)
	{
		if (!g3(ref world))
		{
			_UpdatedByBatch = true;
			fNf = world;
			if (fN_0005 != null)
			{
				fN_0005.SetValue(fNf);
				fNm.fNa.AccumulationValue++;
			}
			if (fNj != null)
			{
				fN_0003 = worldtoobj;
				fNj.SetValue(fN_0003);
				fNm.fNa.AccumulationValue++;
			}
			SetWorldViewProjection(viewprojectionchanged: false, setslowwindingdirection: true);
		}
	}

	internal BaseRenderableEffect(GraphicsDevice P_0, string P_1)
		: base(P_0, LightingSystemManager.Instance.aD(P_1).ByteCode)
	{
		fN_0005 = Parameters["_World"];
		fNj = Parameters["_WorldToObject"];
		fNo = Parameters["_View"];
		fNc = Parameters["_ViewToWorld"];
		fNd = Parameters["_Projection"];
		fNw = Parameters["_ProjectionToView"];
		fNs = Parameters["_ViewProjection"];
		fN_0011 = Parameters["_WorldView"];
		fNQ = Parameters["_WorldViewProjection"];
		fNG = Parameters["_WindingDirection"];
		fN_0017 = Parameters["_FarClippingDistance"];
	}

	/// <summary>
	/// Creates a new effect of the same class type, with the same property values, and using the same effect file as this object.
	/// </summary>
	/// <returns></returns>
	public override Effect Clone()
	{
		Effect effect = Create();
		L.n.ao(this, effect);
		return effect;
	}

	/// <summary>
	/// Creates a new empty effect of the same class type and using the same effect file as this object.
	/// </summary>
	/// <returns></returns>
	protected abstract Effect Create();
}
