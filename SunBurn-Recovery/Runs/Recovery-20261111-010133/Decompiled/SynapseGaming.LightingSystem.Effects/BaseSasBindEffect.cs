using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Effects;

public abstract class BaseSasBindEffect : ParameteredEffect
{
	protected new class w8a
	{
		private Dictionary<string, List<EffectParameter>> l8a = new Dictionary<string, List<EffectParameter>>(128);

		public void Add(string bindaddress, EffectParameter parameter)
		{
			if (!l8a.ContainsKey(bindaddress))
			{
				List<EffectParameter> list = new List<EffectParameter>(8);
				list.Add(parameter);
				l8a.Add(bindaddress, list);
			}
			else
			{
				l8a[bindaddress].Add(parameter);
			}
		}

		public List<EffectParameter> Find(string bindaddress)
		{
			if (!l8a.ContainsKey(bindaddress))
			{
				return null;
			}
			return l8a[bindaddress];
		}

		public void Clear()
		{
			l8a.Clear();
		}
	}

	public const string SASAddress_World_Matrix = "Sas.Camera.World";

	public const string SASAddress_WorldInverse_Matrix = "Sas.Camera.WorldInverse";

	public const string SASAddress_WorldTranspose_Matrix = "Sas.Camera.WorldTranspose";

	public const string SASAddress_WorldInverseTranspose_Matrix = "Sas.Camera.WorldInverseTranspose";

	public const string SASAddress_WorldView_Matrix = "Sas.Camera.ObjectToView";

	public const string SASAddress_WorldViewInverse_Matrix = "Sas.Camera.ObjectToViewInverse";

	public const string SASAddress_WorldViewTranspose_Matrix = "Sas.Camera.ObjectToViewTranspose";

	public const string SASAddress_WorldViewInverseTranspose_Matrix = "Sas.Camera.ObjectToViewInverseTranspose";

	public const string SASAddress_WorldViewProjection_Matrix = "Sas.Camera.ObjectToProjection";

	public const string SASAddress_WorldViewProjectionInverse_Matrix = "Sas.Camera.ObjectToProjectionInverse";

	public const string SASAddress_WorldViewProjectionTranspose_Matrix = "Sas.Camera.ObjectToProjectionTranspose";

	public const string SASAddress_WorldViewProjectionInverseTranspose_Matrix = "Sas.Camera.ObjectToProjectionInverseTranspose";

	public const string SASAddress_View_Matrix = "Sas.Camera.WorldToView";

	public const string SASAddress_ViewInverse_Matrix = "Sas.Camera.WorldToViewInverse";

	public const string SASAddress_ViewTranspose_Matrix = "Sas.Camera.WorldToViewTranspose";

	public const string SASAddress_ViewInverseTranspose_Matrix = "Sas.Camera.WorldToViewInverseTranspose";

	public const string SASAddress_Projection_Matrix = "Sas.Camera.Projection";

	public const string SASAddress_ProjectionInverse_Matrix = "Sas.Camera.ProjectionInverse";

	public const string SASAddress_ProjectionTranspose_Matrix = "Sas.Camera.ProjectionTranspose";

	public const string SASAddress_ProjectionInverseTranspose_Matrix = "Sas.Camera.ProjectionInverseTranspose";

	public const string SASAddress_NumAmbientLights = "Sas.NumAmbientLights";

	public const string SASAddress_NumDirectionalLights = "Sas.NumDirectionalLights";

	public const string SASAddress_NumPointLights = "Sas.NumPointLights";

	public const string SASAddress_Camera_Position = "Sas.Camera.Position";

	public const string SASAddress_SkeletonBones_Matrix = "Sas.Skeleton.MeshToJointToWorld[*]";

	public const string SASAddress_Time_Now = "Sas.Time.Now";

	public const string SASAddress_Time_Last = "Sas.Time.Last";

	public const string SASAddress_Time_FrameNumber = "Sas.Time.FrameNumber";

	public static readonly string[] SASAddress_AmbientLight_Color = new string[4] { "Sas.AmbientLight[0].Color", "Sas.AmbientLight[1].Color", "Sas.AmbientLight[2].Color", "Sas.AmbientLight[3].Color" };

	public static readonly string[] SASAddress_DirectionalLight_Color = new string[4] { "Sas.DirectionalLight[0].Color", "Sas.DirectionalLight[1].Color", "Sas.DirectionalLight[2].Color", "Sas.DirectionalLight[3].Color" };

	public static readonly string[] SASAddress_DirectionalLight_Direction = new string[4] { "Sas.DirectionalLight[0].Direction", "Sas.DirectionalLight[1].Direction", "Sas.DirectionalLight[2].Direction", "Sas.DirectionalLight[3].Direction" };

	public static readonly string[] SASAddress_PointLight_Color = new string[4] { "Sas.PointLight[0].Color", "Sas.PointLight[1].Color", "Sas.PointLight[2].Color", "Sas.PointLight[3].Color" };

	public static readonly string[] SASAddress_PointLight_Position = new string[4] { "Sas.PointLight[0].Position", "Sas.PointLight[1].Position", "Sas.PointLight[2].Position", "Sas.PointLight[3].Position" };

	public static readonly string[] SASAddress_PointLight_Range = new string[4] { "Sas.PointLight[0].Range", "Sas.PointLight[1].Range", "Sas.PointLight[2].Range", "Sas.PointLight[3].Range" };

	private int l8a;

	private GameTime l8_u0013_ = new GameTime();

	private GameTime l8N = new GameTime();

	private w8a l8l = new w8a();

	public GameTime GameTime
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8N = l8_u0013_;
			l8_u0013_ = value;
			l8a++;
			SyncTimeEffectData();
		}
	}

	protected w8a SasAutoBindTable => l8l;

	protected EffectParameter FindByName(string name)
	{
		return Parameters[name];
	}

	protected EffectParameter FindBySemantic(string name)
	{
		for (int i = 0; i < Parameters.Count; i++)
		{
			EffectParameter effectParameter = Parameters[i];
			string semantic = effectParameter.Semantic;
			if (!string.IsNullOrEmpty(semantic) && string.Compare(semantic, name, StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return effectParameter;
			}
		}
		return null;
	}

	protected EffectParameter FindBySasAddress(string name)
	{
		for (int i = 0; i < Parameters.Count; i++)
		{
			EffectParameter effectParameter = Parameters[i];
			EffectAnnotation effectAnnotation = effectParameter.Annotations["SasBindAddress"];
			if (effectAnnotation != null)
			{
				string valueString = effectAnnotation.GetValueString();
				if (!string.IsNullOrEmpty(valueString) && valueString == name)
				{
					return effectParameter;
				}
			}
		}
		return null;
	}

	protected void BindBySasAddress(EffectParameter parameter, string address)
	{
		if (parameter != null)
		{
			SasAutoBindTable.Add(address, parameter);
		}
	}

	protected void BindAllByPartialSasAddress(string partialaddress)
	{
		for (int i = 0; i < Parameters.Count; i++)
		{
			EffectParameter effectParameter = Parameters[i];
			EffectAnnotation effectAnnotation = effectParameter.Annotations["SasBindAddress"];
			if (effectAnnotation == null)
			{
				continue;
			}
			string valueString = effectAnnotation.GetValueString();
			if (!string.IsNullOrEmpty(valueString) && valueString.Length >= partialaddress.Length && !(partialaddress != valueString.Substring(0, partialaddress.Length)))
			{
				if (effectParameter.Elements.Count > 0)
				{
					Nd(effectParameter, valueString);
				}
				else if (effectParameter.StructureMembers.Count > 0)
				{
					N_u0015_(effectParameter, valueString);
				}
				else
				{
					BindBySasAddress(effectParameter, valueString);
				}
			}
		}
	}

	private void Nd(EffectParameter param_0, string param_1)
	{
		for (int i = 0; i < param_0.Elements.Count; i++)
		{
			EffectParameter effectParameter = param_0.Elements[i];
			string text = param_1.Replace("*", i.ToString());
			if (effectParameter.StructureMembers.Count > 0)
			{
				N_u0015_(effectParameter, text);
			}
			else
			{
				BindBySasAddress(effectParameter, text);
			}
		}
	}

	private void N_u0015_(EffectParameter param_0, string param_1)
	{
		for (int i = 0; i < param_0.StructureMembers.Count; i++)
		{
			BindBySasAddress(param_0.StructureMembers[i], param_1 + "." + param_0.StructureMembers[i].Name);
		}
	}

	protected void SyncTimeEffectData()
	{
		EffectHelper.lL(SasAutoBindTable.Find("Sas.Time.Now"), new Vector4((float)l8_u0013_.TotalGameTime.TotalMilliseconds));
		EffectHelper.lL(SasAutoBindTable.Find("Sas.Time.Last"), new Vector4((float)l8N.TotalGameTime.TotalMilliseconds));
		EffectHelper.lL(SasAutoBindTable.Find("Sas.Time.FrameNumber"), new Vector4(l8a));
	}

	internal BaseSasBindEffect(GraphicsDevice param_0, byte[] param_1)
		: base(param_0, param_1)
	{
		BindAllByPartialSasAddress("Sas.");
		BindBySasAddress(FindBySemantic("MODEL"), "Sas.Camera.World");
		BindBySasAddress(FindBySemantic("MODELI"), "Sas.Camera.WorldInverse");
		BindBySasAddress(FindBySemantic("MODELINVERSE"), "Sas.Camera.WorldInverse");
		BindBySasAddress(FindBySemantic("MODELT"), "Sas.Camera.WorldTranspose");
		BindBySasAddress(FindBySemantic("MODELTRANSPOSE"), "Sas.Camera.WorldTranspose");
		BindBySasAddress(FindBySemantic("MODELIT"), "Sas.Camera.WorldInverseTranspose");
		BindBySasAddress(FindBySemantic("MODELINVERSETRANSPOSE"), "Sas.Camera.WorldInverseTranspose");
		BindBySasAddress(FindBySemantic("WORLD"), "Sas.Camera.World");
		BindBySasAddress(FindBySemantic("WORLDI"), "Sas.Camera.WorldInverse");
		BindBySasAddress(FindBySemantic("WORLDINVERSE"), "Sas.Camera.WorldInverse");
		BindBySasAddress(FindBySemantic("WORLDT"), "Sas.Camera.WorldTranspose");
		BindBySasAddress(FindBySemantic("WORLDTRANSPOSE"), "Sas.Camera.WorldTranspose");
		BindBySasAddress(FindBySemantic("WORLDIT"), "Sas.Camera.WorldInverseTranspose");
		BindBySasAddress(FindBySemantic("WORLDINVERSETRANSPOSE"), "Sas.Camera.WorldInverseTranspose");
		BindBySasAddress(FindBySemantic("MODELVIEW"), "Sas.Camera.ObjectToView");
		BindBySasAddress(FindBySemantic("MODELVIEWI"), "Sas.Camera.ObjectToViewInverse");
		BindBySasAddress(FindBySemantic("MODELVIEWINVERSE"), "Sas.Camera.ObjectToViewInverse");
		BindBySasAddress(FindBySemantic("MODELVIEWT"), "Sas.Camera.ObjectToViewTranspose");
		BindBySasAddress(FindBySemantic("MODELVIEWTRANSPOSE"), "Sas.Camera.ObjectToViewTranspose");
		BindBySasAddress(FindBySemantic("MODELVIEWIT"), "Sas.Camera.ObjectToViewInverseTranspose");
		BindBySasAddress(FindBySemantic("MODELVIEWINVERSETRANSPOSE"), "Sas.Camera.ObjectToViewInverseTranspose");
		BindBySasAddress(FindBySemantic("WORLDVIEW"), "Sas.Camera.ObjectToView");
		BindBySasAddress(FindBySemantic("WORLDVIEWI"), "Sas.Camera.ObjectToViewInverse");
		BindBySasAddress(FindBySemantic("WORLDVIEWINVERSE"), "Sas.Camera.ObjectToViewInverse");
		BindBySasAddress(FindBySemantic("WORLDVIEWT"), "Sas.Camera.ObjectToViewTranspose");
		BindBySasAddress(FindBySemantic("WORLDVIEWTRANSPOSE"), "Sas.Camera.ObjectToViewTranspose");
		BindBySasAddress(FindBySemantic("WORLDVIEWIT"), "Sas.Camera.ObjectToViewInverseTranspose");
		BindBySasAddress(FindBySemantic("WORLDVIEWINVERSETRANSPOSE"), "Sas.Camera.ObjectToViewInverseTranspose");
		BindBySasAddress(FindBySemantic("MODELVIEWPROJECTION"), "Sas.Camera.ObjectToProjection");
		BindBySasAddress(FindBySemantic("MODELVIEWPROJECTIONI"), "Sas.Camera.ObjectToProjectionInverse");
		BindBySasAddress(FindBySemantic("MODELVIEWPROJECTIONINVERSE"), "Sas.Camera.ObjectToProjectionInverse");
		BindBySasAddress(FindBySemantic("MODELVIEWPROJECTIONT"), "Sas.Camera.ObjectToProjectionTranspose");
		BindBySasAddress(FindBySemantic("MODELVIEWPROJECTIONTRANSPOSE"), "Sas.Camera.ObjectToProjectionTranspose");
		BindBySasAddress(FindBySemantic("MODELVIEWPROJECTIONIT"), "Sas.Camera.ObjectToProjectionInverseTranspose");
		BindBySasAddress(FindBySemantic("MODELVIEWPROJECTIONINVERSETRANSPOSE"), "Sas.Camera.ObjectToProjectionInverseTranspose");
		BindBySasAddress(FindBySemantic("WORLDVIEWPROJECTION"), "Sas.Camera.ObjectToProjection");
		BindBySasAddress(FindBySemantic("WORLDVIEWPROJECTIONI"), "Sas.Camera.ObjectToProjectionInverse");
		BindBySasAddress(FindBySemantic("WORLDVIEWPROJECTIONINVERSE"), "Sas.Camera.ObjectToProjectionInverse");
		BindBySasAddress(FindBySemantic("WORLDVIEWPROJECTIONT"), "Sas.Camera.ObjectToProjectionTranspose");
		BindBySasAddress(FindBySemantic("WORLDVIEWPROJECTIONTRANSPOSE"), "Sas.Camera.ObjectToProjectionTranspose");
		BindBySasAddress(FindBySemantic("WORLDVIEWPROJECTIONIT"), "Sas.Camera.ObjectToProjectionInverseTranspose");
		BindBySasAddress(FindBySemantic("WORLDVIEWPROJECTIONINVERSETRANSPOSE"), "Sas.Camera.ObjectToProjectionInverseTranspose");
		BindBySasAddress(FindBySemantic("VIEW"), "Sas.Camera.WorldToView");
		BindBySasAddress(FindBySemantic("VIEWI"), "Sas.Camera.WorldToViewInverse");
		BindBySasAddress(FindBySemantic("VIEWINVERSE"), "Sas.Camera.WorldToViewInverse");
		BindBySasAddress(FindBySemantic("VIEWT"), "Sas.Camera.WorldToViewTranspose");
		BindBySasAddress(FindBySemantic("VIEWTRANSPOSE"), "Sas.Camera.WorldToViewTranspose");
		BindBySasAddress(FindBySemantic("VIEWIT"), "Sas.Camera.WorldToViewInverseTranspose");
		BindBySasAddress(FindBySemantic("VIEWINVERSETRANSPOSE"), "Sas.Camera.WorldToViewInverseTranspose");
		BindBySasAddress(FindBySemantic("PROJECTION"), "Sas.Camera.Projection");
		BindBySasAddress(FindBySemantic("PROJECTIONI"), "Sas.Camera.ProjectionInverse");
		BindBySasAddress(FindBySemantic("PROJECTIONINVERSE"), "Sas.Camera.ProjectionInverse");
		BindBySasAddress(FindBySemantic("PROJECTIONT"), "Sas.Camera.ProjectionTranspose");
		BindBySasAddress(FindBySemantic("PROJECTIONTRANSPOSE"), "Sas.Camera.ProjectionTranspose");
		BindBySasAddress(FindBySemantic("PROJECTIONIT"), "Sas.Camera.ProjectionInverseTranspose");
		BindBySasAddress(FindBySemantic("PROJECTIONINVERSETRANSPOSE"), "Sas.Camera.ProjectionInverseTranspose");
	}
}
