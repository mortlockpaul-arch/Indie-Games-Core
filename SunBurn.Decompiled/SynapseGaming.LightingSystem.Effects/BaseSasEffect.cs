using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using j;

namespace SynapseGaming.LightingSystem.Effects;

/// <summary>
/// Effect class with full support for, and binding of, FX Standard Annotations and Semantics (SAS).
/// </summary>
public abstract class BaseSasEffect : BaseSasBindEffect, IRenderableEffect, ISkinnedEffect, j.B, L.v, IEditorObject, INamedObject, IStaticLightingEffect
{
	/// <summary />
	protected byte[] EffectByteCode;

	private Matrix fNB;

	private Matrix fNa;

	private Matrix fNg;

	private Matrix fNf;

	private Matrix fN_0003;

	private Matrix fNn;

	private new string fN_0019 = "";

	private string fNL = "";

	private string fNv = "";

	private string fN_0012 = "";

	private bool fN_0015;

	private Matrix[] fN8 = new Matrix[1];

	private CompositeLighting fNi;

	private Texture2D fN_0005;

	private Texture2D fNj;

	private EffectParameter fNo;

	private EffectParameter fNc;

	private EffectParameter fNd;

	private EffectParameter fNw;

	private EffectParameter fNs;

	private Matrix[] fN_0011 = new Matrix[1];

	private Matrix[] fNQ = new Matrix[1];

	[CompilerGenerated]
	private bool fNG;

	/// <summary>
	/// World matrix applied to geometry using this effect.
	/// </summary>
	public Matrix World
	{
		get
		{
			return fNB;
		}
		set
		{
			if (!(fNB == value))
			{
				Matrix.Invert(ref value, out var result);
				SetWorldAndWorldToObject(ref value, ref result);
			}
		}
	}

	/// <summary>
	/// View matrix applied to geometry using this effect.
	/// </summary>
	public Matrix View
	{
		get
		{
			return fNg;
		}
		set
		{
			if (!(fNg == value))
			{
				fNg = value;
				fNf = Matrix.Invert(value);
				SyncTransformEffectData();
			}
		}
	}

	/// <summary>
	/// Projection matrix applied to geometry using this effect.
	/// </summary>
	public Matrix Projection
	{
		get
		{
			return fN_0003;
		}
		set
		{
			if (!(fN_0003 == value))
			{
				fN_0003 = value;
				fNn = Matrix.Invert(value);
				SyncTransformEffectData();
			}
		}
	}

	/// <summary>
	/// Inverse projection matrix applied to geometry using this effect.
	/// </summary>
	protected Matrix ProjectionToView => fNn;

	/// <summary>
	/// Applies the user's effect preference. This generally trades detail
	/// for performance based on the user's selection.
	/// </summary>
	public DetailPreference EffectDetail
	{
		get
		{
			return DetailPreference.High;
		}
		set
		{
		}
	}

	/// <summary>
	/// Array of bone transforms for the skeleton's current pose. The matrix index is the
	/// same as the bone order used in the model or vertex buffer.
	/// </summary>
	public Matrix[] SkinBones
	{
		get
		{
			return fN8;
		}
		set
		{
			if (!fN_0015 || fNo == null)
			{
				return;
			}
			if (value != null)
			{
				fN8 = value;
				SyncSkinBoneEffectData();
				return;
			}
			if (fN_0011.Length < fNo.Elements.Count)
			{
				fN_0011 = new Matrix[fNo.Elements.Count];
				for (int k = 0; k < fN_0011.Length; k++)
				{
					ref Matrix reference = ref fN_0011[k];
					reference = Matrix.Identity;
				}
			}
			if (fN8 != fN_0011)
			{
				fN8 = fN_0011;
				SyncSkinBoneEffectData();
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
			return fN_0015;
		}
		set
		{
			fN_0015 = value;
			SetTechnique();
		}
	}

	/// <summary>
	/// Notifies the editor that this object is partially controlled via code. The editor
	/// will display information to the user indicating some property values are
	/// overridden in code and changes may not take effect.
	/// </summary>
	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return fNG;
		}
		[CompilerGenerated]
		set
		{
			fNG = value;
		}
	}

	internal string MaterialName
	{
		get
		{
			return fN_0019;
		}
		set
		{
			fN_0019 = value;
		}
	}

	string j.B.MaterialFile => fNL;

	internal string MaterialFile
	{
		get
		{
			return fNL;
		}
		set
		{
			fNL = value;
		}
	}

	internal string ProjectFile
	{
		get
		{
			return fNv;
		}
		set
		{
			fNv = value;
		}
	}

	string L.v.ProjectFile => fNv;

	internal string EffectFile
	{
		get
		{
			return fN_0012;
		}
		set
		{
			fN_0012 = value;
		}
	}

	/// <summary>
	/// Effect parameter used to set the bone transform array.
	/// </summary>
	protected EffectParameter SkinBonesEffectParameter
	{
		get
		{
			return fNo;
		}
		set
		{
			fNo = value;
		}
	}

	/// <summary>
	/// Sets the static lighting applied to the effect during rendering.
	/// </summary>
	/// <param name="lightmap">Light map texture containing static
	/// lighting. If texture is not null the texture will override
	/// the provided composite lighting.</param>
	/// <param name="compositelighting">Composite lighting containing
	/// static lighting.</param>
	public void SetStaticLighting(Texture2D lightmap, ref CompositeLighting compositelighting)
	{
		if (lightmap == null)
		{
			lightmap = fNj;
		}
		EffectHelper.fO(lightmap, ref fN_0005, ref fNc);
		if (fNd != null && fNw != null && fNs != null && !fNi.Equals(compositelighting))
		{
			fNd.SetValue(compositelighting.AmbientColor);
			fNw.SetValue(compositelighting.DiffuseColor);
			fNs.SetValue(compositelighting.Direction);
			fNi = compositelighting;
		}
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
		if (!(fNB == world))
		{
			fNB = world;
			fNa = worldtoobj;
			SyncTransformEffectData();
			SyncSkinBoneEffectData();
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
		if (view != fNg || viewtoworld != fNf)
		{
			fNg = view;
			fNf = viewtoworld;
			flag = true;
		}
		if (projection != fN_0003)
		{
			fN_0003 = projection;
			fNn = projectiontoview;
			flag = true;
		}
		if (flag)
		{
			SyncTransformEffectData();
		}
	}

	/// <summary>
	/// Sets the effect technique based on its current property values.
	/// </summary>
	protected virtual void SetTechnique()
	{
	}

	/// <summary>
	/// Applies the current transform information to the bound effect parameters.
	/// </summary>
	protected virtual void SyncTransformEffectData()
	{
		EffectHelper.f_0013(SasAutoBindTable.Find("Sas.Camera.Position"), new Vector4(fNf.Translation, 1f));
		EffectHelper.fR(SasAutoBindTable.Find("Sas.Camera.World"), fNB);
		EffectHelper.fR(SasAutoBindTable.Find("Sas.Camera.WorldInverse"), fNa);
		EffectHelper.fR(SasAutoBindTable.Find("Sas.Camera.WorldToView"), fNg);
		EffectHelper.fR(SasAutoBindTable.Find("Sas.Camera.WorldToViewInverse"), fNf);
		EffectHelper.fR(SasAutoBindTable.Find("Sas.Camera.Projection"), fN_0003);
		EffectHelper.fR(SasAutoBindTable.Find("Sas.Camera.ProjectionInverse"), fNn);
		EffectHelper.fu(SasAutoBindTable.Find("Sas.Camera.WorldTranspose"), fNB);
		EffectHelper.fu(SasAutoBindTable.Find("Sas.Camera.WorldInverseTranspose"), fNa);
		EffectHelper.fu(SasAutoBindTable.Find("Sas.Camera.WorldToViewTranspose"), fNg);
		EffectHelper.fu(SasAutoBindTable.Find("Sas.Camera.WorldToViewInverseTranspose"), fNf);
		EffectHelper.fu(SasAutoBindTable.Find("Sas.Camera.ProjectionTranspose"), fN_0003);
		EffectHelper.fu(SasAutoBindTable.Find("Sas.Camera.ProjectionInverseTranspose"), fNn);
		List<EffectParameter> list = SasAutoBindTable.Find("Sas.Camera.ObjectToView");
		List<EffectParameter> list2 = SasAutoBindTable.Find("Sas.Camera.ObjectToViewTranspose");
		List<EffectParameter> list3 = SasAutoBindTable.Find("Sas.Camera.ObjectToProjection");
		List<EffectParameter> list4 = SasAutoBindTable.Find("Sas.Camera.ObjectToProjectionTranspose");
		if (list != null || list2 != null || list3 != null || list4 != null)
		{
			Matrix matrix = fNB * fNg;
			Matrix matrix2 = matrix * fN_0003;
			EffectHelper.fR(list, matrix);
			EffectHelper.fu(list2, matrix);
			EffectHelper.fR(list3, matrix2);
			EffectHelper.fu(list4, matrix2);
		}
		List<EffectParameter> list5 = SasAutoBindTable.Find("Sas.Camera.ObjectToViewInverse");
		List<EffectParameter> list6 = SasAutoBindTable.Find("Sas.Camera.ObjectToViewInverseTranspose");
		List<EffectParameter> list7 = SasAutoBindTable.Find("Sas.Camera.ObjectToProjectionInverse");
		List<EffectParameter> list8 = SasAutoBindTable.Find("Sas.Camera.ObjectToProjectionInverseTranspose");
		if (list5 != null || list6 != null || list7 != null || list8 != null)
		{
			Matrix matrix3 = fNf * fNa;
			Matrix matrix4 = fNn * matrix3;
			EffectHelper.fR(list5, matrix3);
			EffectHelper.fu(list6, matrix3);
			EffectHelper.fR(list7, matrix4);
			EffectHelper.fu(list8, matrix4);
		}
	}

	/// <summary>
	/// Applies the current bone transform information to the bound effect parameters.
	/// </summary>
	protected virtual void SyncSkinBoneEffectData()
	{
		if (fN_0015 && fNo != null)
		{
			if (fNQ.Length < fN8.Length)
			{
				fNQ = new Matrix[fN8.Length];
			}
			for (int k = 0; k < fN8.Length; k++)
			{
				ref Matrix reference = ref fNQ[k];
				reference = fN8[k] * fNB;
			}
			Math.Min(fNQ.Length, fNo.Elements.Count);
			fNo.SetValue(fNQ);
		}
	}

	internal BaseSasEffect(GraphicsDevice P_0, byte[] P_1, bool P_2)
		: base(P_0, P_1)
	{
		fNo = FindBySasAddress("Sas.Skeleton.MeshToJointToWorld[*]");
		B(P_2);
	}

	private void B(bool P_0)
	{
		fNc = FindBySemantic("LIGHTMAPTEXTURE");
		fNd = FindBySemantic("COMPOSITELIGHTINGAMBIENT");
		fNw = FindBySemantic("COMPOSITELIGHTINGDIFFUSE");
		fNs = FindBySemantic("COMPOSITELIGHTINGDIRECTION");
		fNj = LightingSystemManager.Instance.aP("Black");
		if (P_0)
		{
			LightingSystemEditor.OnCreateResource(this);
		}
	}

	/// <summary>
	/// Releases the unmanaged resources used by the Effect and optionally releases the managed resources.
	/// </summary>
	/// <param name="releasemanaged"></param>
	protected override void Dispose(bool releasemanaged)
	{
		base.Dispose(releasemanaged);
		LightingSystemEditor.OnDisposeResource(this);
	}
}
