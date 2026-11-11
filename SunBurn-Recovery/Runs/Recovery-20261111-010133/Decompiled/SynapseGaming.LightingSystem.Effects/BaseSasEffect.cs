using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using b;

namespace SynapseGaming.LightingSystem.Effects;

public abstract class BaseSasEffect : BaseSasBindEffect, IRenderableEffect, ISkinnedEffect, b.a, B.J, IEditorObject, INamedObject, IStaticLightingEffect
{
	protected byte[] EffectByteCode;

	private Matrix l8a;

	private Matrix l8_u0013_;

	private Matrix l8N;

	private Matrix l8l;

	private Matrix l8L;

	private Matrix l8w;

	private string l8J = "";

	private string l8B = "";

	private string l8s = "";

	private new string l8h = "";

	private bool l8_u0002_;

	private Matrix[] l8v = new Matrix[1];

	private CompositeLighting l8Y;

	private Texture2D l8_u0005_;

	private Texture2D l8b;

	private LightMap l8I;

	private EffectParameter l85;

	private EffectParameter l8W;

	private EffectParameter l8Z;

	private EffectParameter l8T;

	private EffectParameter l8_u0014_;

	private EffectParameter l8E;

	private Matrix[] l8f = new Matrix[1];

	private Matrix[] l8_u0016_ = new Matrix[1];

	[CompilerGenerated]
	private bool l86;

	public Matrix World
	{
		get
		{
			return l8a;
		}
		set
		{
			if (!(l8a == value))
			{
				Matrix.Invert(ref value, out var result);
				SetWorldAndWorldToObject(ref value, ref result);
			}
		}
	}

	public Matrix View
	{
		get
		{
			return l8N;
		}
		set
		{
			if (!(l8N == value))
			{
				l8N = value;
				l8l = Matrix.Invert(value);
				SyncTransformEffectData();
			}
		}
	}

	public Matrix Projection
	{
		get
		{
			return l8L;
		}
		set
		{
			if (!(l8L == value))
			{
				l8L = value;
				l8w = Matrix.Invert(value);
				SyncTransformEffectData();
			}
		}
	}

	protected Matrix ProjectionToView => l8w;

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

	public Matrix[] SkinBones
	{
		get
		{
			return l8v;
		}
		set
		{
			if (!l8_u0002_ || l85 == null)
			{
				return;
			}
			if (value != null)
			{
				l8v = value;
				SyncSkinBoneEffectData();
				return;
			}
			if (l8f.Length < l85.Elements.Count)
			{
				l8f = new Matrix[l85.Elements.Count];
				for (int i = 0; i < l8f.Length; i++)
				{
					ref Matrix reference = ref l8f[i];
					reference = Matrix.Identity;
				}
			}
			if (l8v != l8f)
			{
				l8v = l8f;
				SyncSkinBoneEffectData();
			}
		}
	}

	public bool Skinned
	{
		get
		{
			return l8_u0002_;
		}
		set
		{
			l8_u0002_ = value;
			SetTechnique();
		}
	}

	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l86;
		}
		[CompilerGenerated]
		set
		{
			l86 = value;
		}
	}

	internal string MaterialName
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = value;
		}
	}

	string b.a.MaterialFile => l8B;

	internal string MaterialFile
	{
		get
		{
			return l8B;
		}
		set
		{
			l8B = value;
		}
	}

	internal string ProjectFile
	{
		get
		{
			return l8s;
		}
		set
		{
			l8s = value;
		}
	}

	string B.J.ProjectFile => l8s;

	internal string EffectFile
	{
		get
		{
			return l8h;
		}
		set
		{
			l8h = value;
		}
	}

	protected EffectParameter SkinBonesEffectParameter
	{
		get
		{
			return l85;
		}
		set
		{
			l85 = value;
		}
	}

	public void SetStaticLighting(StaticLightingEffectMode lightingmode, LightMap lightmap, ref CompositeLighting compositelighting)
	{
		if (lightmap == null || lightingmode == StaticLightingEffectMode.Composite || lightingmode == StaticLightingEffectMode.Ambient)
		{
			lightmap = l8I;
		}
		if (lightingmode == StaticLightingEffectMode.Ambient || lightingmode == StaticLightingEffectMode.BakedDown)
		{
			compositelighting = default;
		}
		EffectHelper.lh(lightmap.LightMapColorTexture, ref l8_u0005_, ref l8W);
		EffectHelper.lh(lightmap.LightMapDirectionalTexture, ref l8b, ref l8Z);
		if (l8T != null && l8_u0014_ != null && l8E != null && !l8Y.Equals(compositelighting))
		{
			l8T.SetValue(compositelighting.AmbientColor);
			l8_u0014_.SetValue(compositelighting.DiffuseColor);
			l8E.SetValue(compositelighting.Direction);
			l8Y = compositelighting;
		}
	}

	public void SetStaticLighting(StaticLightingEffectMode lightingmode, LightMap lightmap)
	{
		SetStaticLighting(lightingmode, lightmap, ref l8Y);
	}

	public void SetWorldAndWorldToObject(ref Matrix world, ref Matrix worldtoobj)
	{
		if (!(l8a == world))
		{
			l8a = world;
			l8_u0013_ = worldtoobj;
			SyncTransformEffectData();
			SyncSkinBoneEffectData();
		}
	}

	public void SetViewAndProjection(Matrix view, Matrix viewtoworld, Matrix projection, Matrix projectiontoview)
	{
		bool flag = false;
		if (view != l8N || viewtoworld != l8l)
		{
			l8N = view;
			l8l = viewtoworld;
			flag = true;
		}
		if (projection != l8L)
		{
			l8L = projection;
			l8w = projectiontoview;
			flag = true;
		}
		if (flag)
		{
			SyncTransformEffectData();
		}
	}

	protected virtual void SetTechnique()
	{
	}

	protected virtual void SyncTransformEffectData()
	{
		EffectHelper.lL(SasAutoBindTable.Find("Sas.Camera.Position"), new Vector4(l8l.Translation, 1f));
		EffectHelper.l_u0013_(SasAutoBindTable.Find("Sas.Camera.World"), l8a);
		EffectHelper.l_u0013_(SasAutoBindTable.Find("Sas.Camera.WorldInverse"), l8_u0013_);
		EffectHelper.l_u0013_(SasAutoBindTable.Find("Sas.Camera.WorldToView"), l8N);
		EffectHelper.l_u0013_(SasAutoBindTable.Find("Sas.Camera.WorldToViewInverse"), l8l);
		EffectHelper.l_u0013_(SasAutoBindTable.Find("Sas.Camera.Projection"), l8L);
		EffectHelper.l_u0013_(SasAutoBindTable.Find("Sas.Camera.ProjectionInverse"), l8w);
		EffectHelper.l_u0002_(SasAutoBindTable.Find("Sas.Camera.WorldTranspose"), l8a);
		EffectHelper.l_u0002_(SasAutoBindTable.Find("Sas.Camera.WorldInverseTranspose"), l8_u0013_);
		EffectHelper.l_u0002_(SasAutoBindTable.Find("Sas.Camera.WorldToViewTranspose"), l8N);
		EffectHelper.l_u0002_(SasAutoBindTable.Find("Sas.Camera.WorldToViewInverseTranspose"), l8l);
		EffectHelper.l_u0002_(SasAutoBindTable.Find("Sas.Camera.ProjectionTranspose"), l8L);
		EffectHelper.l_u0002_(SasAutoBindTable.Find("Sas.Camera.ProjectionInverseTranspose"), l8w);
		List<EffectParameter> list = SasAutoBindTable.Find("Sas.Camera.ObjectToView");
		List<EffectParameter> list2 = SasAutoBindTable.Find("Sas.Camera.ObjectToViewTranspose");
		List<EffectParameter> list3 = SasAutoBindTable.Find("Sas.Camera.ObjectToProjection");
		List<EffectParameter> list4 = SasAutoBindTable.Find("Sas.Camera.ObjectToProjectionTranspose");
		if (list != null || list2 != null || list3 != null || list4 != null)
		{
			Matrix matrix = l8a * l8N;
			Matrix param_ = matrix * l8L;
			EffectHelper.l_u0013_(list, matrix);
			EffectHelper.l_u0002_(list2, matrix);
			EffectHelper.l_u0013_(list3, param_);
			EffectHelper.l_u0002_(list4, param_);
		}
		List<EffectParameter> list5 = SasAutoBindTable.Find("Sas.Camera.ObjectToViewInverse");
		List<EffectParameter> list6 = SasAutoBindTable.Find("Sas.Camera.ObjectToViewInverseTranspose");
		List<EffectParameter> list7 = SasAutoBindTable.Find("Sas.Camera.ObjectToProjectionInverse");
		List<EffectParameter> list8 = SasAutoBindTable.Find("Sas.Camera.ObjectToProjectionInverseTranspose");
		if (list5 != null || list6 != null || list7 != null || list8 != null)
		{
			Matrix matrix2 = l8l * l8_u0013_;
			Matrix param_2 = l8w * matrix2;
			EffectHelper.l_u0013_(list5, matrix2);
			EffectHelper.l_u0002_(list6, matrix2);
			EffectHelper.l_u0013_(list7, param_2);
			EffectHelper.l_u0002_(list8, param_2);
		}
	}

	protected virtual void SyncSkinBoneEffectData()
	{
		if (l8_u0002_ && l85 != null)
		{
			if (l8_u0016_.Length < l8v.Length)
			{
				l8_u0016_ = new Matrix[l8v.Length];
			}
			for (int i = 0; i < l8v.Length; i++)
			{
				ref Matrix reference = ref l8_u0016_[i];
				reference = l8v[i] * l8a;
			}
			Math.Min(l8_u0016_.Length, l85.Elements.Count);
			l85.SetValue(l8_u0016_);
		}
	}

	internal BaseSasEffect(GraphicsDevice param_0, byte[] param_1, bool param_2)
		: base(param_0, param_1)
	{
		l85 = FindBySasAddress("Sas.Skeleton.MeshToJointToWorld[*]");
		a(param_2);
	}

	private void a(bool param_0)
	{
		l8W = FindBySemantic("LIGHTMAPCOLORTEXTURE");
		l8Z = FindBySemantic("LIGHTMAPDIRECTIONALTEXTURE");
		l8T = FindBySemantic("COMPOSITELIGHTINGAMBIENT");
		l8_u0014_ = FindBySemantic("COMPOSITELIGHTINGDIFFUSE");
		l8E = FindBySemantic("COMPOSITELIGHTINGDIRECTION");
		Texture2D texture2D = SunBurnCoreSystem.Instance._u0013_S("Black");
		l8I = new LightMap(texture2D, texture2D);
		if (param_0)
		{
			SunBurnEditor.OnCreateResource(this);
		}
	}

	protected override void Dispose(bool releasemanaged)
	{
		base.Dispose(releasemanaged);
		SunBurnEditor.OnDisposeResource(this);
	}
}
