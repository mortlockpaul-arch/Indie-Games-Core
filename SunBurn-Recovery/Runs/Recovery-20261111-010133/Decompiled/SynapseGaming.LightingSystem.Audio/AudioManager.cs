using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;
using l;

namespace SynapseGaming.LightingSystem.Audio;

public class AudioManager : BaseObjectGraphManager<AudioSource, IAudioManager>, IAudioManager, IUpdatableManager, IManagerService, IQuery<AudioSource>, ISubmit<AudioSource>, ISubmit<IScene>, IWorldRenderableManager, IRenderableManager, IManager, IUnloadable
{
	private struct w8a
	{
		internal class w8a_nested : IComparer<w8a>
		{
			public int Compare(w8a x, w8a y)
			{
				return y.Weight.CompareTo(x.Weight);
			}
		}

		public AudioSource AudioSource;

		public float Weight;
	}

	private int l8a = 100;

	private int l8_u0013_ = 200;

	private ISceneState l8N;

	private Matrix l8l = Matrix.Identity;

	private BoundingFrustum l8L = new BoundingFrustum(Matrix.Identity);

	private l.a l8w = new l.a();

	private List<AudioSource> l8J = new List<AudioSource>();

	private List<w8a> l8B = new List<w8a>();

	private static w8a.w8a_nested l8s = new w8a.w8a_nested();

	public override Type ManagerType => SceneInterface.AudioManagerType;

	public override int ManagerProcessOrder
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
		}
	}

	public int MaximumAudioSources
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
			if (l8_u0013_ > 300)
			{
				l8_u0013_ = 300;
			}
		}
	}

	public AudioManager(IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
	}

	public override void Update(GameTime gametime)
	{
		l8J.Clear();
		l8B.Clear();
		Find(l8J, l8L, ObjectFilter.All);
		foreach (AudioSource item2 in l8J)
		{
			bool flag = item2.AudioType == AudioType.Point;
			float radius = item2.Radius;
			float num = item2.Volume;
			if (item2.AudioState == AudioState.Playing && item2.SoundEffect != null && !(num <= 0f) && (!flag || !(radius <= 0f)))
			{
				if (flag)
				{
					float num2 = Vector3.DistanceSquared(item2.Position, l8l.Translation);
					num *= 1f - num2 / (radius * radius);
				}
				if (!(num <= 0f))
				{
					w8a item = new w8a
					{
						AudioSource = item2,
						Weight = num
					};
					l8B.Add(item);
				}
			}
		}
		if (l8B.Count > l8_u0013_)
		{
			l8B.Sort(l8s);
		}
		l8w.l8a = l8_u0013_;
		l8w.x(ref l8l);
		int num3 = Math.Min(l8B.Count, l8_u0013_);
		for (int i = 0; i < num3; i++)
		{
			l8w._u0004_(l8B[i].AudioSource);
		}
		l8w._u0019_();
		base.Update(gametime);
	}

	public void BeginFrameRendering(ISceneState scenestate)
	{
		l8N = scenestate;
		l8l = scenestate.ViewToWorld;
		l8L.Matrix = scenestate.ViewFrustum.Matrix;
	}

	public void EndFrameRendering()
	{
		foreach (AudioSource item in l8J)
		{
			item.RenderCustomPass(l8N);
		}
	}

	public override void Remove(AudioSource obj)
	{
		l8w.X(obj);
		base.Remove(obj);
	}

	public override void Clear()
	{
		l8w._id_6();
		base.Clear();
	}
}
