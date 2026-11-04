using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SynapseGaming.LightingSystem.Audio;

namespace f;

internal class B
{
	internal int fNB = 200;

	private Vector3 fNa;

	private AudioListener fNg = new AudioListener();

	private Dictionary<AudioSource, a> fNf = new Dictionary<AudioSource, a>();

	private List<AudioSource> fN_0003 = new List<AudioSource>();

	private List<KeyValuePair<AudioSource, a>> fNn = new List<KeyValuePair<AudioSource, a>>();

	internal void _0017()
	{
		foreach (KeyValuePair<AudioSource, a> item in fNf)
		{
			item.Value.Dispose();
		}
		fNf.Clear();
		fN_0003.Clear();
	}

	internal void m(ref Matrix P_0)
	{
		Vector3 translation = P_0.Translation;
		fNg.Forward = P_0.Forward;
		fNg.Up = P_0.Up;
		fNg.Position = translation;
		fNg.Velocity = translation - fNa;
		fNa = translation;
	}

	internal void h(AudioSource P_0)
	{
		if (fNf.TryGetValue(P_0, out var value))
		{
			if (value.fNg != P_0)
			{
				value._9(P_0);
			}
			value.fNB = true;
			value.fNa = false;
		}
		else
		{
			fN_0003.Add(P_0);
		}
	}

	internal void R(AudioSource P_0)
	{
		if (fNf.TryGetValue(P_0, out var value))
		{
			value.fNa = true;
		}
	}

	internal void W()
	{
		foreach (KeyValuePair<AudioSource, a> item in fNf)
		{
			a value = item.Value;
			if (value.fNa)
			{
				value.Dispose();
			}
		}
		int num = fN_0003.Count + fNf.Count;
		if (num > fNB)
		{
			int num2 = num - fNB;
			fNn.Clear();
			foreach (KeyValuePair<AudioSource, a> item2 in fNf)
			{
				if (!item2.Value.fNB)
				{
					fNn.Add(item2);
				}
			}
			if (num2 > fNn.Count)
			{
				throw new Exception("Unable to clear enough unused audio threads to work within maximum thread count.");
			}
			foreach (KeyValuePair<AudioSource, a> item3 in fNn)
			{
				if (num2 > 0)
				{
					fNf.Remove(item3.Key);
					a value2 = item3.Value;
					int index = fN_0003.Count - 1;
					AudioSource audioSource = fN_0003[index];
					fN_0003.RemoveAt(index);
					value2._9(audioSource);
					fNf.Add(audioSource, value2);
					num2--;
					continue;
				}
				break;
			}
		}
		foreach (AudioSource item4 in fN_0003)
		{
			a a2 = new a();
			a2._9(item4);
			fNf.Add(item4, a2);
		}
		fN_0003.Clear();
		foreach (KeyValuePair<AudioSource, a> item5 in fNf)
		{
			item5.Value.W(fNg);
		}
	}
}
