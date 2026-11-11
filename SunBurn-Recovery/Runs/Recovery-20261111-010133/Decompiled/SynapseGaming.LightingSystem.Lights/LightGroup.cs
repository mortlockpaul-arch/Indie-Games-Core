using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using B;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Serialization;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Lights;

[Serializable]
public class LightGroup : ShadowSource, IFullSerializable, ISerializable, ILightGroup, IGroup<ILight>, IShadowSource, IEditorCreatedObject<ILightGroup>, IEditorObject, INamedObject
{
	private bool l8a;

	private List<ILight> l8_u0013_ = new List<ILight>(16);

	private IList<ILight> l8N;

	public IList<ILight> Lights => l8N;

	public bool ShadowGroup
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
			IShadowSource shadowSource = null;
			if (value)
			{
				shadowSource = this;
			}
			foreach (ILight item in l8_u0013_)
			{
				item.ShadowSource = shadowSource;
			}
		}
	}

	public void Add(ILight light)
	{
		l8_u0013_.Add(light);
		if (l8a)
		{
			light.ShadowSource = this;
		}
		else
		{
			light.ShadowSource = null;
		}
	}

	public void Remove(ILight light)
	{
		l8_u0013_.Remove(light);
		light.ShadowSource = null;
	}

	public void RemoveAt(int index)
	{
		Remove(l8_u0013_[index]);
	}

	public void Clear()
	{
		foreach (ILight item in l8_u0013_)
		{
			item.ShadowSource = null;
		}
		l8_u0013_.Clear();
	}

	public LightGroup()
	{
		Name = "Group";
		a();
	}

	private void a()
	{
		l8N = l8_u0013_.AsReadOnly();
	}

	public virtual void OnCreatedInEditor()
	{
	}

	public virtual ILightGroup Clone()
	{
		LightGroup lightGroup = new LightGroup();
		B.L._u0013_B(this, lightGroup);
		foreach (ILight item in l8_u0013_)
		{
			lightGroup.Add(item.Clone());
		}
		return lightGroup;
	}

	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.SetObjectData(info, context);
		a();
		foreach (SerializationEntry item in info)
		{
			switch (item.Name)
			{
			case "Lights":
				l8_u0013_.AddRange((List<ILight>)info.GetValue("Lights", typeof(List<ILight>)));
				break;
			case "ShadowGroup":
				l8a = (bool)info.GetValue("ShadowGroup", typeof(bool));
				break;
			}
		}
		ShadowGroup = ShadowGroup;
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue("ShadowGroup", l8a);
		info.AddValue("Lights", l8_u0013_);
	}
}
