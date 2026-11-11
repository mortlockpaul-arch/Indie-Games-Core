using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using B;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Rendering;

[Serializable]
[EditorObject(true)]
public class SceneEntityGroup : IFullSerializable, ISerializable, ISceneEntityGroup, IGroup<ISceneEntity>, IEditorCreatedObject<ISceneEntityGroup>, IEditorObject, INamedObject
{
	private List<ISceneEntity> l8a = new List<ISceneEntity>(16);

	private IList<ISceneEntity> l8_u0013_;

	[CompilerGenerated]
	private string l8N;

	[CompilerGenerated]
	private bool l8l;

	public IList<ISceneEntity> Entities => l8_u0013_;

	[EditorProperty(true, Description = "Name", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 1, ToolTipText = "")]
	public string Name
	{
		[CompilerGenerated]
		get
		{
			return l8N;
		}
		[CompilerGenerated]
		set
		{
			l8N = value;
		}
	}

	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l8l;
		}
		[CompilerGenerated]
		set
		{
			l8l = value;
		}
	}

	public void Add(ISceneEntity obj)
	{
		l8a.Add(obj);
	}

	public void Remove(ISceneEntity obj)
	{
		l8a.Remove(obj);
	}

	public void RemoveAt(int index)
	{
		Remove(l8a[index]);
	}

	public void Clear()
	{
		l8a.Clear();
	}

	public SceneEntityGroup()
	{
		Name = "Group";
		a();
	}

	private void a()
	{
		l8_u0013_ = l8a.AsReadOnly();
	}

	public virtual void OnCreatedInEditor()
	{
	}

	public virtual ISceneEntityGroup Clone()
	{
		SceneEntityGroup sceneEntityGroup = new SceneEntityGroup();
		B.L._u0013_B(this, sceneEntityGroup);
		foreach (ISceneEntity item in l8a)
		{
			sceneEntityGroup.Add(item.Clone());
		}
		return sceneEntityGroup;
	}

	public void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		a();
		foreach (SerializationEntry item in info)
		{
			switch (item.Name)
			{
			case "Entities":
				l8a.AddRange((List<ISceneEntity>)info.GetValue("Entities", typeof(List<ISceneEntity>)));
				break;
			case "Name":
				Name = (string)info.GetValue("Name", typeof(string));
				break;
			}
		}
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("Entities", l8a);
		info.AddValue("Name", Name);
	}
}
