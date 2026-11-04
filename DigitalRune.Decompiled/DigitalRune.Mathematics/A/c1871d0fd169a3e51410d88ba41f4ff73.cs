using System.IO;

namespace A;

internal class c1871d0fd169a3e51410d88ba41f4ff73
{
	internal static byte[] c8934277166c09ad334b08634b90c3092(Stream c15a95bc62396228a7aaf676cd51a8cef)
	{
		byte b = (byte)c15a95bc62396228a7aaf676cd51a8cef.ReadByte();
		byte[] array = new byte[c15a95bc62396228a7aaf676cd51a8cef.Length - 1];
		c15a95bc62396228a7aaf676cd51a8cef.Read(array, 0, array.Length);
		if ((b & 4) != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)(~array[i]);
			}
		}
		return array;
	}
}
