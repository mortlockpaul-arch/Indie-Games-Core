using System;
using _0013;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SynapseGaming.LightingSystem.Audio;

namespace f;

internal class a : IDisposable
{
	internal bool fNB;

	internal bool fNa;

	internal AudioSource fNg;

	internal bool fNf;

	internal Vector3 fN_0003;

	internal AudioState fNn;

	internal SoundEffectInstance fN_0019;

	internal SoundState fNL;

	private static AudioEmitter fNv;

	static a()
	{
		fNv = new AudioEmitter();
		fNv.DopplerScale = 1f;
		fNv.Forward = Vector3.Forward;
		fNv.Up = Vector3.Up;
		fNv.Velocity = Vector3.Zero;
	}

	internal void _9(AudioSource P_0)
	{
		_0013();
		fNg = P_0;
		fNf = fNg.Loop;
		fN_0003 = fNg.Position;
		fNn = AudioState.Stopped;
		fN_0019 = P_0.SoundEffect.CreateInstance();
		fN_0019.IsLooped = fNg.Loop;
		fNL = fN_0019.State;
	}

	private void _0013()
	{
		fNg = null;
		global::_0013.B.nu(ref fN_0019);
	}

	public void Dispose()
	{
		_0013();
	}

	internal void W(AudioListener P_0)
	{
		if (fNg == null)
		{
			fNB = false;
			return;
		}
		bool loop = fNg.Loop;
		if (loop != fNf)
		{
			_9(fNg);
			loop = fNg.Loop;
		}
		AudioState audioState = fNg.AudioState;
		Vector3 position = fNg.Position;
		SoundState state = fN_0019.State;
		bool flag = audioState == AudioState.Playing;
		bool flag2 = state == SoundState.Playing;
		if (audioState != fNn)
		{
			if (flag && !flag2)
			{
				fN_0019.Apply3D(P_0, fNv);
				fN_0019.Play();
			}
			else if (!flag && state != SoundState.Stopped)
			{
				fN_0019.Stop();
			}
			state = fN_0019.State;
		}
		else if (state != fNL)
		{
			audioState = (flag2 ? AudioState.Playing : AudioState.Stopped);
			fNg.AudioState = audioState;
		}
		if (fNg.AudioType == AudioType.Point)
		{
			fNv.Velocity = position - fN_0003;
			fNv.Position = position;
			SoundEffect.DistanceScale = fNg.Radius * 0.1f;
		}
		else
		{
			fNv.Velocity = Vector3.Zero;
			fNv.Position = P_0.Position;
			SoundEffect.DistanceScale = 1f;
		}
		fN_0019.Volume = MathHelper.Clamp(fNg.Volume, 0f, 1f);
		fN_0019.Apply3D(P_0, fNv);
		fNf = loop;
		fNn = audioState;
		fN_0003 = position;
		fNL = state;
		fNB = false;
	}
}
