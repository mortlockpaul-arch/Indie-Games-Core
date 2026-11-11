using System;
using System.IO;
using System.Text;
using _u0016_;

namespace E;

internal class a
{
	private byte l8a;

	private byte[] l8_u0013_;

	private _u0016_.a l8N;

	public int Count
	{
		get
		{
			if (l8N == null)
			{
				return 0;
			}
			return l8N.Count;
		}
	}

	public byte Tag => l8a;

	public int Length
	{
		get
		{
			if (l8_u0013_ != null)
			{
				return l8_u0013_.Length;
			}
			return 0;
		}
	}

	public byte[] Value
	{
		get
		{
			if (l8_u0013_ == null)
			{
				GetBytes();
			}
			return (byte[])l8_u0013_.Clone();
		}
		set
		{
			if (value != null)
			{
				l8_u0013_ = (byte[])value.Clone();
			}
		}
	}

	public a this[int index]
	{
		get
		{
			try
			{
				if (l8N == null || index >= l8N.Count)
				{
					return null;
				}
				return (a)l8N[index];
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
		}
	}

	public a()
		: this(0, null)
	{
	}

	public a(byte tag)
		: this(tag, null)
	{
	}

	public a(byte tag, byte[] data)
	{
		l8a = tag;
		l8_u0013_ = data;
	}

	public a(byte[] data)
	{
		l8a = data[0];
		int num = 0;
		int num2 = data[1];
		if (num2 > 128)
		{
			num = num2 - 128;
			num2 = 0;
			for (int i = 0; i < num; i++)
			{
				num2 *= 256;
				num2 += data[i + 2];
			}
		}
		else if (num2 == 128)
		{
			throw new NotSupportedException("Undefined length encoding.");
		}
		l8_u0013_ = new byte[num2];
		Buffer.BlockCopy(data, 2 + num, l8_u0013_, 0, num2);
		if ((l8a & 0x20) == 32)
		{
			int anPos = 2 + num;
			Decode(data, ref anPos, data.Length);
		}
	}

	private bool l_u0010_(byte[] param_0, byte[] param_1)
	{
		bool flag = param_0.Length == param_1.Length;
		if (flag)
		{
			for (int i = 0; i < param_0.Length; i++)
			{
				if (param_0[i] != param_1[i])
				{
					return false;
				}
			}
		}
		return flag;
	}

	public bool Equals(byte[] asn1)
	{
		return l_u0010_(GetBytes(), asn1);
	}

	public bool CompareValue(byte[] value)
	{
		return l_u0010_(l8_u0013_, value);
	}

	public a Add(a asn1)
	{
		if (asn1 != null)
		{
			if (l8N == null)
			{
				l8N = new _u0016_.a();
			}
			l8N.Add(asn1);
		}
		return asn1;
	}

	public virtual byte[] GetBytes()
	{
		byte[] array = null;
		if (Count > 0)
		{
			int num = 0;
			_u0016_.a a2 = new _u0016_.a();
			foreach (a item in l8N)
			{
				byte[] bytes = item.GetBytes();
				a2.Add(bytes);
				num += bytes.Length;
			}
			array = new byte[num];
			int num2 = 0;
			for (int i = 0; i < l8N.Count; i++)
			{
				byte[] array2 = (byte[])a2[i];
				Buffer.BlockCopy(array2, 0, array, num2, array2.Length);
				num2 += array2.Length;
			}
		}
		else if (l8_u0013_ != null)
		{
			array = l8_u0013_;
		}
		int num3 = 0;
		byte[] array3;
		if (array != null)
		{
			int num4 = array.Length;
			if (num4 > 127)
			{
				if (num4 <= 255)
				{
					array3 = new byte[3 + num4];
					Buffer.BlockCopy(array, 0, array3, 3, num4);
					num3 = 129;
					array3[2] = (byte)num4;
				}
				else if (num4 <= 65535)
				{
					array3 = new byte[4 + num4];
					Buffer.BlockCopy(array, 0, array3, 4, num4);
					num3 = 130;
					array3[2] = (byte)(num4 >> 8);
					array3[3] = (byte)num4;
				}
				else if (num4 <= 16777215)
				{
					array3 = new byte[5 + num4];
					Buffer.BlockCopy(array, 0, array3, 5, num4);
					num3 = 131;
					array3[2] = (byte)(num4 >> 16);
					array3[3] = (byte)(num4 >> 8);
					array3[4] = (byte)num4;
				}
				else
				{
					array3 = new byte[6 + num4];
					Buffer.BlockCopy(array, 0, array3, 6, num4);
					num3 = 132;
					array3[2] = (byte)(num4 >> 24);
					array3[3] = (byte)(num4 >> 16);
					array3[4] = (byte)(num4 >> 8);
					array3[5] = (byte)num4;
				}
			}
			else
			{
				array3 = new byte[2 + num4];
				Buffer.BlockCopy(array, 0, array3, 2, num4);
				num3 = num4;
			}
			if (l8_u0013_ == null)
			{
				l8_u0013_ = array;
			}
		}
		else
		{
			array3 = new byte[2];
		}
		array3[0] = l8a;
		array3[1] = (byte)num3;
		return array3;
	}

	protected void Decode(byte[] asn1, ref int anPos, int anLength)
	{
		while (anPos < anLength - 1)
		{
			DecodeTLV(asn1, ref anPos, out var tag, out var length, out var content);
			if (tag != 0)
			{
				a a2 = Add(new a(tag, content));
				if ((tag & 0x20) == 32)
				{
					int anPos2 = anPos;
					a2.Decode(asn1, ref anPos2, anPos2 + length);
				}
				anPos += length;
			}
		}
	}

	protected void DecodeTLV(byte[] asn1, ref int pos, out byte tag, out int length, out byte[] content)
	{
		tag = asn1[pos++];
		length = asn1[pos++];
		if ((length & 0x80) == 128)
		{
			int num = length & 0x7F;
			length = 0;
			for (int i = 0; i < num; i++)
			{
				length = length * 256 + asn1[pos++];
			}
		}
		content = new byte[length];
		Buffer.BlockCopy(asn1, pos, content, 0, length);
	}

	public a Element(int index, byte anTag)
	{
		try
		{
			if (l8N == null || index >= l8N.Count)
			{
				return null;
			}
			a a2 = (a)l8N[index];
			if (a2.Tag == anTag)
			{
				return a2;
			}
			return null;
		}
		catch (ArgumentOutOfRangeException)
		{
			return null;
		}
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("Tag: {0} {1}", new object[2]
		{
			l8a.ToString("X2"),
			Environment.NewLine
		});
		stringBuilder.AppendFormat("Length: {0} {1}", new object[2]
		{
			Value.Length,
			Environment.NewLine
		});
		stringBuilder.Append("Value: ");
		stringBuilder.Append(Environment.NewLine);
		for (int i = 0; i < Value.Length; i++)
		{
			stringBuilder.AppendFormat("{0} ", new object[1] { Value[i].ToString("X2") });
			if ((i + 1) % 16 == 0)
			{
				stringBuilder.AppendFormat(Environment.NewLine);
			}
		}
		return stringBuilder.ToString();
	}

	public void SaveToFile(string filename)
	{
		if (filename == null)
		{
			throw new ArgumentNullException("filename");
		}
		using FileStream fileStream = File.OpenWrite(filename);
		byte[] bytes = GetBytes();
		fileStream.Write(bytes, 0, bytes.Length);
		fileStream.Flush();
		fileStream.Close();
	}
}
