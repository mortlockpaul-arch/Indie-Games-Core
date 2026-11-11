using System;

namespace _u0014_;

internal interface J : IDisposable
{
	bool CanReuseTransform { get; }

	bool CanTransformMultipleBlocks { get; }

	int InputBlockSize { get; }

	int OutputBlockSize { get; }

	int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

	byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
}
