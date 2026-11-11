using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SynapseGaming.LightingSystem.Audio;

namespace l
{
	internal class a
	{
		internal int l8a = 200;

		private Vector3 l8_u0013_;

		private AudioListener l8N = new AudioListener();

		private Dictionary<AudioSource, _u0013_> l8l = new Dictionary<AudioSource, _u0013_>();

		private List<AudioSource> l8L = new List<AudioSource>();

		private List<KeyValuePair<AudioSource, _u0013_>> l8w = new List<KeyValuePair<AudioSource, _u0013_>>();

		internal void _id_6()
		{
			foreach (KeyValuePair<AudioSource, _u0013_> item in l8l)
			{
				item.Value.Dispose();
			}
			l8l.Clear();
			l8L.Clear();
		}

		internal void x(ref Matrix param_0)
		{
			Vector3 translation = param_0.Translation;
			l8N.Forward = param_0.Forward;
			l8N.Up = param_0.Up;
			l8N.Position = translation;
			l8N.Velocity = translation - l8_u0013_;
			l8_u0013_ = translation;
		}

		internal void _u0004_(AudioSource param_0)
		{
			if (l8l.TryGetValue(param_0, out var value))
			{
				if (value.l8N != param_0)
				{
					value.y(param_0);
				}
				value.l8a = true;
				value.l8_u0013_ = false;
			}
			else
			{
				l8L.Add(param_0);
			}
		}

		internal void X(AudioSource param_0)
		{
			if (l8l.TryGetValue(param_0, out var value))
			{
				value.l8_u0013_ = true;
			}
		}

		internal void _u0019_()
		{
			foreach (KeyValuePair<AudioSource, _u0013_> item in l8l)
			{
				_u0013_ value = item.Value;
				if (value.l8_u0013_)
				{
					value.Dispose();
				}
			}
			int num = l8L.Count + l8l.Count;
			if (num > l8a)
			{
				int num2 = num - l8a;
				l8w.Clear();
				foreach (KeyValuePair<AudioSource, _u0013_> item2 in l8l)
				{
					if (!item2.Value.l8a)
					{
						l8w.Add(item2);
					}
				}
				if (num2 > l8w.Count)
				{
					throw new Exception("Unable to clear enough unused audio threads to work within maximum thread count.");
				}
				foreach (KeyValuePair<AudioSource, _u0013_> item3 in l8w)
				{
					if (num2 > 0)
					{
						l8l.Remove(item3.Key);
						_u0013_ value2 = item3.Value;
						int index = l8L.Count - 1;
						AudioSource audioSource = l8L[index];
						l8L.RemoveAt(index);
						value2.y(audioSource);
						l8l.Add(audioSource, value2);
						num2--;
						continue;
					}
					break;
				}
			}
			foreach (AudioSource item4 in l8L)
			{
				_u0013_ u0013_ = new _u0013_();
				u0013_.y(item4);
				l8l.Add(item4, u0013_);
			}
			l8L.Clear();
			foreach (KeyValuePair<AudioSource, _u0013_> item5 in l8l)
			{
				item5.Value._u0019_(l8N, item5.Key.DopplerScale);
			}
		}
	}
}
namespace L
{
	internal class a : IEnumerator
	{
		private IEnumerator l8a;

		public object Current => ((KeyValuePair<string, SerializationEntry>)l8a.Current).Value;

		public a(IEnumerator items)
		{
			l8a = items;
		}

		public bool MoveNext()
		{
			return l8a.MoveNext();
		}

		public void Reset()
		{
			l8a.Reset();
		}
	}
}
