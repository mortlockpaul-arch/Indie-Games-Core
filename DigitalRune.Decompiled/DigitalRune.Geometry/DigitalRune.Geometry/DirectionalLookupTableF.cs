using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry;

/// <summary>
///       Stores data that is accessed using a direction vector instead of indices. (Single-precision)
///       </summary>
/// <typeparam name="T">The type of data stored in the lookup table.</typeparam>
[Serializable]
public class DirectionalLookupTableF<T>
{
	[CompilerGenerated]
	private sealed class c0e4e67387f73ef94f85a544462a78a91 : IEnumerable<Vector3F>, IEnumerator<Vector3F>, IEnumerable, IEnumerator, IDisposable
	{
		private Vector3F cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public DirectionalLookupTableF<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public int c5677ab495b19121ff95c2ffe32ed7591;

		public int cfb3dcbc73f8ed27870e529abbcb79fac;

		public float c5e19886484cbb0bd11fd3d38ed46d7cb;

		public int c54b6d06a4b1553a70597a89a514157b9;

		public float ccd2ccec2ff46db1fa8a980822c04c5a5;

		public int c536f9d659c9b3099dbf62fb37f7dcd07;

		public float c8d94b50aafa386598c638e320909a0ea;

		public int cb432b74fa338e2a5b7b823f695ede3e6;

		public float c63b60631e7629f7f71f9e260d12d8fa7;

		public int c9bf2df84f4ff97874a6cb04d60a8c0a8;

		public float c0302ea8928da803c76c4437b3ea53877;

		public int c28263697f2c7580a7c9ee5b6716edc0c;

		public float ccd030394c5b062d292fd8568a2f8bf6a;

		public int cd5083cf2652abdec5d6e701596816916;

		public float c6ef8f2a095390cbbf8780b74afc5d90b;

		public int c0a8a954456db8010d12dc9a0ae38f3f2;

		public float c191457b52550a33245a9ae28fff9d067;

		public int c134a698cadb08632a9ce9f8f78b2f6eb;

		public float c47501127e553301fd21d2b1ecf00810e;

		public int cd03eef3621858fd6a0ef273ec066ed82;

		public float cc3a74a54489210a46273ee51a5249d49;

		public int c14a3bcd1df50c2780ef164fc70049311;

		public float ceb7c3bfc5ddb8dc0c61d58a867e2e37e;

		public int c1d98d4211893977850a8f8957ad85e7a;

		public float c5f6a84ae2300dc08dd7710fed804ce9c;

		Vector3F IEnumerator<Vector3F>.Current
		{
			[DebuggerHidden]
			get
			{
				return cd179d7f18da070d164af6afcb0af45b5;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return cd179d7f18da070d164af6afcb0af45b5;
			}
		}

		[DebuggerHidden]
		public c0e4e67387f73ef94f85a544462a78a91(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Vector3F> cbdfa4e22baedad7307ebba4c2374b4a8()
		{
			c0e4e67387f73ef94f85a544462a78a91 result;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(DirectionalLookupTableF<>.c0e4e67387f73ef94f85a544462a78a91.cbdfa4e22baedad7307ebba4c2374b4a8());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				result = this;
			}
			else
			{
				result = new c0e4e67387f73ef94f85a544462a78a91(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			return result;
		}

		IEnumerator<Vector3F> IEnumerable<Vector3F>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in cbdfa4e22baedad7307ebba4c2374b4a8
			return this.cbdfa4e22baedad7307ebba4c2374b4a8();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return cbdfa4e22baedad7307ebba4c2374b4a8();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
			return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
		}

		private bool MoveNext()
		{
			switch (c94f69126adc708dfc5f5fd5b63c52835)
			{
			case 0:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c5677ab495b19121ff95c2ffe32ed7591 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.CubeMap.GetLength(1);
				cfb3dcbc73f8ed27870e529abbcb79fac = 0;
				goto IL_0105;
			case 1:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c54b6d06a4b1553a70597a89a514157b9++;
				goto IL_00e9;
			case 2:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				cb432b74fa338e2a5b7b823f695ede3e6++;
				goto IL_01b3;
			case 3:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c28263697f2c7580a7c9ee5b6716edc0c++;
				goto IL_029a;
			case 4:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c0a8a954456db8010d12dc9a0ae38f3f2++;
				goto IL_0378;
			case 5:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				cd03eef3621858fd6a0ef273ec066ed82++;
				goto IL_0456;
			case 6:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					c1d98d4211893977850a8f8957ad85e7a++;
					goto IL_0534;
				}
				IL_0105:
				if (cfb3dcbc73f8ed27870e529abbcb79fac < c5677ab495b19121ff95c2ffe32ed7591)
				{
					c5e19886484cbb0bd11fd3d38ed46d7cb = ((float)cfb3dcbc73f8ed27870e529abbcb79fac + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
					c54b6d06a4b1553a70597a89a514157b9 = 0;
					goto IL_00e9;
				}
				c536f9d659c9b3099dbf62fb37f7dcd07 = 0;
				goto IL_01e2;
				IL_01b3:
				if (cb432b74fa338e2a5b7b823f695ede3e6 < c5677ab495b19121ff95c2ffe32ed7591)
				{
					c63b60631e7629f7f71f9e260d12d8fa7 = ((float)cb432b74fa338e2a5b7b823f695ede3e6 + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
					cd179d7f18da070d164af6afcb0af45b5 = new Vector3F(-1f, c63b60631e7629f7f71f9e260d12d8fa7, c8d94b50aafa386598c638e320909a0ea);
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					return true;
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(DirectionalLookupTableF<>.c0e4e67387f73ef94f85a544462a78a91.MoveNext());
				}
				c536f9d659c9b3099dbf62fb37f7dcd07++;
				goto IL_01e2;
				IL_01e2:
				if (c536f9d659c9b3099dbf62fb37f7dcd07 < c5677ab495b19121ff95c2ffe32ed7591)
				{
					c8d94b50aafa386598c638e320909a0ea = ((float)c536f9d659c9b3099dbf62fb37f7dcd07 + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
					cb432b74fa338e2a5b7b823f695ede3e6 = 0;
					goto IL_01b3;
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				c9bf2df84f4ff97874a6cb04d60a8c0a8 = 0;
				goto IL_02c0;
				IL_055a:
				if (c14a3bcd1df50c2780ef164fc70049311 >= c5677ab495b19121ff95c2ffe32ed7591)
				{
					break;
				}
				ceb7c3bfc5ddb8dc0c61d58a867e2e37e = ((float)c14a3bcd1df50c2780ef164fc70049311 + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
				c1d98d4211893977850a8f8957ad85e7a = 0;
				goto IL_0534;
				IL_0534:
				if (c1d98d4211893977850a8f8957ad85e7a < c5677ab495b19121ff95c2ffe32ed7591)
				{
					c5f6a84ae2300dc08dd7710fed804ce9c = ((float)c1d98d4211893977850a8f8957ad85e7a + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
					cd179d7f18da070d164af6afcb0af45b5 = new Vector3F(c5f6a84ae2300dc08dd7710fed804ce9c, ceb7c3bfc5ddb8dc0c61d58a867e2e37e, -1f);
					c94f69126adc708dfc5f5fd5b63c52835 = 6;
					return true;
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				c14a3bcd1df50c2780ef164fc70049311++;
				goto IL_055a;
				IL_02c0:
				if (c9bf2df84f4ff97874a6cb04d60a8c0a8 < c5677ab495b19121ff95c2ffe32ed7591)
				{
					c0302ea8928da803c76c4437b3ea53877 = ((float)c9bf2df84f4ff97874a6cb04d60a8c0a8 + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
					c28263697f2c7580a7c9ee5b6716edc0c = 0;
					goto IL_029a;
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				cd5083cf2652abdec5d6e701596816916 = 0;
				goto IL_039e;
				IL_0456:
				if (cd03eef3621858fd6a0ef273ec066ed82 < c5677ab495b19121ff95c2ffe32ed7591)
				{
					cc3a74a54489210a46273ee51a5249d49 = ((float)cd03eef3621858fd6a0ef273ec066ed82 + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
					cd179d7f18da070d164af6afcb0af45b5 = new Vector3F(cc3a74a54489210a46273ee51a5249d49, c47501127e553301fd21d2b1ecf00810e, 1f);
					c94f69126adc708dfc5f5fd5b63c52835 = 5;
					return true;
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				c134a698cadb08632a9ce9f8f78b2f6eb++;
				goto IL_047c;
				IL_0378:
				if (c0a8a954456db8010d12dc9a0ae38f3f2 < c5677ab495b19121ff95c2ffe32ed7591)
				{
					c191457b52550a33245a9ae28fff9d067 = ((float)c0a8a954456db8010d12dc9a0ae38f3f2 + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
					cd179d7f18da070d164af6afcb0af45b5 = new Vector3F(c191457b52550a33245a9ae28fff9d067, -1f, c6ef8f2a095390cbbf8780b74afc5d90b);
					c94f69126adc708dfc5f5fd5b63c52835 = 4;
					return true;
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				cd5083cf2652abdec5d6e701596816916++;
				goto IL_039e;
				IL_039e:
				if (cd5083cf2652abdec5d6e701596816916 < c5677ab495b19121ff95c2ffe32ed7591)
				{
					c6ef8f2a095390cbbf8780b74afc5d90b = ((float)cd5083cf2652abdec5d6e701596816916 + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
					c0a8a954456db8010d12dc9a0ae38f3f2 = 0;
					goto IL_0378;
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				c134a698cadb08632a9ce9f8f78b2f6eb = 0;
				goto IL_047c;
				IL_029a:
				if (c28263697f2c7580a7c9ee5b6716edc0c < c5677ab495b19121ff95c2ffe32ed7591)
				{
					ccd030394c5b062d292fd8568a2f8bf6a = ((float)c28263697f2c7580a7c9ee5b6716edc0c + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
					cd179d7f18da070d164af6afcb0af45b5 = new Vector3F(ccd030394c5b062d292fd8568a2f8bf6a, 1f, c0302ea8928da803c76c4437b3ea53877);
					c94f69126adc708dfc5f5fd5b63c52835 = 3;
					return true;
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					break;
				}
				c9bf2df84f4ff97874a6cb04d60a8c0a8++;
				goto IL_02c0;
				IL_00e9:
				if (c54b6d06a4b1553a70597a89a514157b9 < c5677ab495b19121ff95c2ffe32ed7591)
				{
					ccd2ccec2ff46db1fa8a980822c04c5a5 = ((float)c54b6d06a4b1553a70597a89a514157b9 + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
					cd179d7f18da070d164af6afcb0af45b5 = new Vector3F(1f, ccd2ccec2ff46db1fa8a980822c04c5a5, c5e19886484cbb0bd11fd3d38ed46d7cb);
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
					return true;
				}
				cfb3dcbc73f8ed27870e529abbcb79fac++;
				goto IL_0105;
				IL_047c:
				if (c134a698cadb08632a9ce9f8f78b2f6eb < c5677ab495b19121ff95c2ffe32ed7591)
				{
					c47501127e553301fd21d2b1ecf00810e = ((float)c134a698cadb08632a9ce9f8f78b2f6eb + 0.5f) / (float)c5677ab495b19121ff95c2ffe32ed7591 * 2f - 1f;
					cd03eef3621858fd6a0ef273ec066ed82 = 0;
					goto IL_0456;
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				c14a3bcd1df50c2780ef164fc70049311 = 0;
				goto IL_055a;
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		private void c9106c2889b15ecfa28d3af82494ec7bc()
		{
			throw new NotSupportedException();
		}

		void IEnumerator.Reset()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c9106c2889b15ecfa28d3af82494ec7bc
			this.c9106c2889b15ecfa28d3af82494ec7bc();
		}

		private void c087ddb9300ad4d53672b7a60e6d76672()
		{
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}
	}

	private const int PositiveX = 0;

	private const int NegativeX = 1;

	private const int PositiveY = 2;

	private const int NegativeY = 3;

	private const int PositiveZ = 4;

	private const int NegativeZ = 5;

	internal T[,,] CubeMap;

	/// <summary>
	///       Gets or sets the data associated with the specified direction.
	///       </summary>
	public T this[Vector3F direction]
	{
		get
		{
			c6791719c7469114a53cb26e335c9a542(ref direction, out var c5078b1ca2bd86333c65cb625b996b, out var c0c03f4e8a4fc683a5246023a7ee7fb, out var c0b89ebd3492c28b5af64c64c59f1627f);
			return CubeMap[c0b89ebd3492c28b5af64c64c59f1627f, c0c03f4e8a4fc683a5246023a7ee7fb, c5078b1ca2bd86333c65cb625b996b];
		}
		set
		{
			c6791719c7469114a53cb26e335c9a542(ref direction, out var c5078b1ca2bd86333c65cb625b996b, out var c0c03f4e8a4fc683a5246023a7ee7fb, out var c0b89ebd3492c28b5af64c64c59f1627f);
			CubeMap[c0b89ebd3492c28b5af64c64c59f1627f, c0c03f4e8a4fc683a5246023a7ee7fb, c5078b1ca2bd86333c65cb625b996b] = value;
		}
	}

	internal int Width => CubeMap.GetLength(1);

	internal DirectionalLookupTableF(T[,,] c7aa17a196d598db3fc57b56abfab013b)
	{
		CubeMap = c7aa17a196d598db3fc57b56abfab013b;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.DirectionalLookupTableF`1" /> class.
	///       </summary>
	/// <param name="width">The width of the cube map faces.</param>
	public DirectionalLookupTableF(int width)
	{
		CubeMap = new T[6, width, width];
	}

	private void c6791719c7469114a53cb26e335c9a542(ref Vector3F c75c28b357dedcad9a44abd406f2361f3, out int c5078b1ca2bd86333c65cb625b996b618, out int c0c03f4e8a4fc683a5246023a7ee7fb32, out int c0b89ebd3492c28b5af64c64c59f1627f)
	{
		int length = CubeMap.GetLength(1);
		float num = 0.5f * (float)length;
		Vector3F vector3F = Vector3F.Absolute(c75c28b357dedcad9a44abd406f2361f3);
		if (vector3F.X >= vector3F.Y && vector3F.X >= vector3F.Z)
		{
			float num2 = 1f / vector3F.X;
			if (c75c28b357dedcad9a44abd406f2361f3.X >= 0f)
			{
				c5078b1ca2bd86333c65cb625b996b618 = (int)((c75c28b357dedcad9a44abd406f2361f3.Z * num2 + 1f) * num);
				c0c03f4e8a4fc683a5246023a7ee7fb32 = (int)((c75c28b357dedcad9a44abd406f2361f3.Y * num2 + 1f) * num);
				c0b89ebd3492c28b5af64c64c59f1627f = 0;
			}
			else
			{
				c5078b1ca2bd86333c65cb625b996b618 = (int)(((0f - c75c28b357dedcad9a44abd406f2361f3.Z) * num2 + 1f) * num);
				c0c03f4e8a4fc683a5246023a7ee7fb32 = (int)((c75c28b357dedcad9a44abd406f2361f3.Y * num2 + 1f) * num);
				c0b89ebd3492c28b5af64c64c59f1627f = 1;
			}
		}
		else if (vector3F.Y >= vector3F.Z)
		{
			float num3 = 1f / vector3F.Y;
			if (c75c28b357dedcad9a44abd406f2361f3.Y >= 0f)
			{
				c5078b1ca2bd86333c65cb625b996b618 = (int)((c75c28b357dedcad9a44abd406f2361f3.X * num3 + 1f) * num);
				c0c03f4e8a4fc683a5246023a7ee7fb32 = (int)((c75c28b357dedcad9a44abd406f2361f3.Z * num3 + 1f) * num);
				c0b89ebd3492c28b5af64c64c59f1627f = 2;
			}
			else
			{
				c5078b1ca2bd86333c65cb625b996b618 = (int)(((0f - c75c28b357dedcad9a44abd406f2361f3.X) * num3 + 1f) * num);
				c0c03f4e8a4fc683a5246023a7ee7fb32 = (int)((c75c28b357dedcad9a44abd406f2361f3.Z * num3 + 1f) * num);
				c0b89ebd3492c28b5af64c64c59f1627f = 3;
			}
		}
		else
		{
			float num4 = 1f / vector3F.Z;
			if (c75c28b357dedcad9a44abd406f2361f3.Z >= 0f)
			{
				c5078b1ca2bd86333c65cb625b996b618 = (int)(((0f - c75c28b357dedcad9a44abd406f2361f3.X) * num4 + 1f) * num);
				c0c03f4e8a4fc683a5246023a7ee7fb32 = (int)((c75c28b357dedcad9a44abd406f2361f3.Y * num4 + 1f) * num);
				c0b89ebd3492c28b5af64c64c59f1627f = 4;
			}
			else
			{
				c5078b1ca2bd86333c65cb625b996b618 = (int)((c75c28b357dedcad9a44abd406f2361f3.X * num4 + 1f) * num);
				c0c03f4e8a4fc683a5246023a7ee7fb32 = (int)((c75c28b357dedcad9a44abd406f2361f3.Y * num4 + 1f) * num);
				c0b89ebd3492c28b5af64c64c59f1627f = 5;
			}
		}
		int num5 = length - 1;
		if (c5078b1ca2bd86333c65cb625b996b618 > num5)
		{
			c5078b1ca2bd86333c65cb625b996b618 = num5;
		}
		if (c0c03f4e8a4fc683a5246023a7ee7fb32 > num5)
		{
			c0c03f4e8a4fc683a5246023a7ee7fb32 = num5;
		}
	}

	/// <summary>
	///       Gets the sample directions.
	///       </summary>
	/// <returns>The sample directions.</returns>
	public IEnumerable<Vector3F> GetSampleDirections()
	{
		int length = CubeMap.GetLength(1);
		for (int i = 0; i < length; i++)
		{
			float z = ((float)i + 0.5f) / (float)length * 2f - 1f;
			for (int j = 0; j < length; j++)
			{
				float y = ((float)j + 0.5f) / (float)length * 2f - 1f;
				yield return new Vector3F(1f, y, z);
			}
		}
		int num = 0;
		while (num < length)
		{
			float z2 = ((float)num + 0.5f) / (float)length * 2f - 1f;
			for (int k = 0; k < length; k++)
			{
				float y2 = ((float)k + 0.5f) / (float)length * 2f - 1f;
				yield return new Vector3F(-1f, y2, z2);
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(DirectionalLookupTableF<>.c0e4e67387f73ef94f85a544462a78a91.MoveNext());
				}
				num++;
				break;
			}
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			for (int l = 0; l < length; l++)
			{
				float z3 = ((float)l + 0.5f) / (float)length * 2f - 1f;
				for (int m = 0; m < length; m++)
				{
					float x = ((float)m + 0.5f) / (float)length * 2f - 1f;
					yield return new Vector3F(x, 1f, z3);
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						goto end_IL_02a8;
					}
					continue;
					end_IL_02a8:
					break;
				}
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				for (int n = 0; n < length; n++)
				{
					float z4 = ((float)n + 0.5f) / (float)length * 2f - 1f;
					for (int num2 = 0; num2 < length; num2++)
					{
						float x2 = ((float)num2 + 0.5f) / (float)length * 2f - 1f;
						yield return new Vector3F(x2, -1f, z4);
					}
					while (true)
					{
						switch (5)
						{
						case 0:
							break;
						default:
							goto end_IL_0386;
						}
						continue;
						end_IL_0386:
						break;
					}
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					for (int num3 = 0; num3 < length; num3++)
					{
						float y3 = ((float)num3 + 0.5f) / (float)length * 2f - 1f;
						for (int num4 = 0; num4 < length; num4++)
						{
							float x3 = ((float)num4 + 0.5f) / (float)length * 2f - 1f;
							yield return new Vector3F(x3, y3, 1f);
						}
						while (true)
						{
							switch (6)
							{
							case 0:
								break;
							default:
								goto end_IL_0464;
							}
							continue;
							end_IL_0464:
							break;
						}
					}
					while (true)
					{
						switch (6)
						{
						case 0:
							continue;
						}
						for (int num5 = 0; num5 < length; num5++)
						{
							float y4 = ((float)num5 + 0.5f) / (float)length * 2f - 1f;
							for (int num6 = 0; num6 < length; num6++)
							{
								float x4 = ((float)num6 + 0.5f) / (float)length * 2f - 1f;
								yield return new Vector3F(x4, y4, -1f);
							}
							while (true)
							{
								switch (7)
								{
								case 0:
									break;
								default:
									goto end_IL_0542;
								}
								continue;
								end_IL_0542:
								break;
							}
						}
						yield break;
					}
				}
			}
		}
	}
}
