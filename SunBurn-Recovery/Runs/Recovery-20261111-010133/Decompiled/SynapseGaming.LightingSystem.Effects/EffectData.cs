using System;
using System.Runtime.CompilerServices;

namespace SynapseGaming.LightingSystem.Effects;

public class EffectData : IDisposable
{
	[CompilerGenerated]
	private byte[] l8a;

	public byte[] ByteCode
	{
		[CompilerGenerated]
		get
		{
			return l8a;
		}
		[CompilerGenerated]
		protected set
		{
			l8a = value;
		}
	}

	public EffectData(byte[] bytecode)
	{
		ByteCode = bytecode;
	}

	public void Dispose()
	{
	}
}
