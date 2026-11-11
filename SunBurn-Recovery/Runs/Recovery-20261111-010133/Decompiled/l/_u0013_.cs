using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SynapseGaming.LightingSystem.Audio;
using y;

namespace l
{
	internal class _u0013_ : IDisposable
	{
		internal bool l8a;

		internal bool l8_u0013_;

		internal AudioSource l8N;

		internal bool l8l;

		internal Vector3 l8L;

		internal AudioState l8w;

		internal SoundEffectInstance l8J;

		internal SoundState l8B;

		private static AudioEmitter l8s;

		static _u0013_()
		{
			l8s = new AudioEmitter();
			l8s.DopplerScale = 1f;
			l8s.Forward = Vector3.Forward;
			l8s.Up = Vector3.Up;
			l8s.Velocity = Vector3.Zero;
		}

		internal void y(AudioSource param_0)
		{
			_id_3();
			l8N = param_0;
			l8l = l8N.Loop;
			l8L = l8N.Position;
			l8w = AudioState.Stopped;
			l8J = param_0.SoundEffect.CreateInstance();
			l8J.IsLooped = l8N.Loop;
			l8B = l8J.State;
		}

		private void _id_3()
		{
			l8N = null;
			global::y.a.w_u0005_(ref l8J);
		}

		public void Dispose()
		{
			_id_3();
		}

		internal void _u0019_(AudioListener param_0, float param_1)
		{
			if (l8N == null)
			{
				l8a = false;
				return;
			}
			bool loop = l8N.Loop;
			if (loop != l8l)
			{
				y(l8N);
				loop = l8N.Loop;
			}
			AudioState audioState = l8N.AudioState;
			Vector3 position = l8N.Position;
			SoundState state = l8J.State;
			bool flag = audioState == AudioState.Playing;
			bool flag2 = state == SoundState.Playing;
			if (audioState != l8w)
			{
				if (flag && !flag2)
				{
					l8J.Apply3D(param_0, l8s);
					l8J.Play();
				}
				else if (!flag && state != SoundState.Stopped)
				{
					l8J.Stop();
				}
				state = l8J.State;
			}
			else if (state != l8B)
			{
				audioState = (flag2 ? AudioState.Playing : AudioState.Stopped);
				l8N.AudioState = audioState;
			}
			if (l8N.AudioType == AudioType.Point)
			{
				l8s.Velocity = position - l8L;
				l8s.Position = position;
				l8s.DopplerScale = param_1;
				SoundEffect.DistanceScale = l8N.Radius * 0.1f;
			}
			else
			{
				l8s.Velocity = Vector3.Zero;
				l8s.Position = param_0.Position;
				l8s.DopplerScale = 1f;
				SoundEffect.DistanceScale = 1f;
			}
			l8J.Volume = MathHelper.Clamp(l8N.Volume, 0f, 1f);
			l8J.Apply3D(param_0, l8s);
			l8l = loop;
			l8w = audioState;
			l8L = position;
			l8B = state;
			l8a = false;
		}
	}
}
namespace L
{
	internal class _u0013_
	{
		public static MemberInfo[] GetSerializableMembers(Type type)
		{
			List<MemberInfo> list = new List<MemberInfo>(32);
			list.AddRange(type.GetProperties(BindingFlags.Instance | BindingFlags.Public));
			list.AddRange(type.GetFields(BindingFlags.Instance | BindingFlags.Public));
			return list.ToArray();
		}

		public static void PopulateObjectMembers(object obj, MemberInfo[] members, object[] data)
		{
			int num = 0;
			foreach (MemberInfo memberInfo in members)
			{
				try
				{
					if (memberInfo is PropertyInfo)
					{
						(memberInfo as PropertyInfo).SetValue(obj, data[num], null);
					}
					if (memberInfo is FieldInfo)
					{
						(memberInfo as FieldInfo).SetValue(obj, data[num]);
					}
				}
				catch
				{
				}
				num++;
			}
		}
	}
}
