using System;
using System.Runtime.CompilerServices;
using DigitalRune;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;

namespace A;

internal class cc49dcd41ee82253a805139c508e0926f
{
	private struct c69512b66284989e9d24956d70acb4567
	{
		public float c659c6a630400e2e9a282e5b1b1ee04ab;

		public float c8304ebbbca3014d9ce1063084bab1a85;

		public float cbc378f5ff2ea823a5d5063c0713d3c5e;

		public float ce770175a6cf38ec6b47d8f7b1e487b9d;

		public bool c21bd570620de15607214db559a372341;

		public bool c9d2054c1920b76ea632bd1663f8889c1;

		public bool c36d963794c3bd34833029fffeaae3231;

		public bool c038b38aeb978fbb8244a627d918fa87e;

		public Vector3F c596154f5b0c243d05e4fa792fd81b4bb;

		public bool c0fbe03753bb021fba9a19567cadca039;

		public void c5fada5690a4946af905392fc7856ac2f(float c56c34a97a59d9a6bf9228361d8bfb0df, float caa9d7ab80719ca5895d6816e6deee0d6, float cb3dde1030d9e035889f9cb9b0590bb73, float c5078b1ca2bd86333c65cb625b996b618)
		{
			c659c6a630400e2e9a282e5b1b1ee04ab = c56c34a97a59d9a6bf9228361d8bfb0df;
			c8304ebbbca3014d9ce1063084bab1a85 = caa9d7ab80719ca5895d6816e6deee0d6;
			cbc378f5ff2ea823a5d5063c0713d3c5e = cb3dde1030d9e035889f9cb9b0590bb73;
			ce770175a6cf38ec6b47d8f7b1e487b9d = c5078b1ca2bd86333c65cb625b996b618;
		}
	}

	private static readonly ResourcePool<cc49dcd41ee82253a805139c508e0926f> c8aca4a0e3c0073ffb188fde832680587;

	private readonly Vector3F[] c738fde13b1eaa92fd510c0fd80e21772 = new Vector3F[4];

	private readonly Vector3F[] cceb3d5c30f61d7c86e97b0b1fd3c1d00 = new Vector3F[4];

	private readonly Vector3F[] c49538b885daa44d081b01b46e038abaa = new Vector3F[4];

	private bool cd143fef024039434b5c81e466526abf8 = true;

	private Vector3F c4f7068c10390fdf70e36f0f8a2c3e4af = new Vector3F(float.NaN);

	private c69512b66284989e9d24956d70acb4567 cb2e88f5c26d4b09e2bb9ed6db811a744;

	private Vector3F c59f0b627db89664d42d66e15f85fe3af;

	private Vector3F cc7a371c4dae811867cf2b5a963ba164d;

	private Vector3F c2846baa0f40e616b89af5095e7d500ab;

	private Vector3F ce11e40ca3fb3db2be4758fb1b0736c17;

	private Vector3F cc263f3e7e6ac7eb5f109705591760fb6;

	private Vector3F c95f4ccb5ccc33937e6e451eb146afc5c;

	[CompilerGenerated]
	private bool cfec7c798a134bce22733508865c6ef8a;

	[CompilerGenerated]
	private int c78aa73996f2b737d675b7c1b02e0c7d7;

	[CompilerGenerated]
	private static Func<cc49dcd41ee82253a805139c508e0926f> c49253b4520c1b7e183dfcc9c426c0283;

	public Vector3F c596154f5b0c243d05e4fa792fd81b4bb
	{
		get
		{
			return ce11e40ca3fb3db2be4758fb1b0736c17;
		}
		private set
		{
			c2846baa0f40e616b89af5095e7d500ab = ce11e40ca3fb3db2be4758fb1b0736c17;
			ce11e40ca3fb3db2be4758fb1b0736c17 = value;
		}
	}

	public Vector3F cfaa13c3933478652e1d13c21f7cfece2
	{
		get
		{
			return cc263f3e7e6ac7eb5f109705591760fb6;
		}
		private set
		{
			c59f0b627db89664d42d66e15f85fe3af = cc263f3e7e6ac7eb5f109705591760fb6;
			cc263f3e7e6ac7eb5f109705591760fb6 = value;
		}
	}

	public Vector3F c8db9d816e59b2168612a23097569af14
	{
		get
		{
			return c95f4ccb5ccc33937e6e451eb146afc5c;
		}
		private set
		{
			cc7a371c4dae811867cf2b5a963ba164d = c95f4ccb5ccc33937e6e451eb146afc5c;
			c95f4ccb5ccc33937e6e451eb146afc5c = value;
		}
	}

	public bool ce4e3bcec46c899e1009a33b2202cde07 => NumberOfVertices == 0;

	public bool c8cfa49e753c046fa77edb85a6fcf2975 => NumberOfVertices == 4;

	public bool IsValid
	{
		[CompilerGenerated]
		get
		{
			return cfec7c798a134bce22733508865c6ef8a;
		}
		[CompilerGenerated]
		private set
		{
			cfec7c798a134bce22733508865c6ef8a = value;
		}
	}

	public float c8a14f47232b640512ed789adc8c198a2
	{
		get
		{
			float num = 0f;
			for (int i = 0; i < NumberOfVertices; i++)
			{
				float lengthSquared = c738fde13b1eaa92fd510c0fd80e21772[i].LengthSquared;
				if (lengthSquared > num)
				{
					num = lengthSquared;
				}
			}
			return num;
		}
	}

	public int NumberOfVertices
	{
		[CompilerGenerated]
		get
		{
			return c78aa73996f2b737d675b7c1b02e0c7d7;
		}
		[CompilerGenerated]
		private set
		{
			c78aa73996f2b737d675b7c1b02e0c7d7 = value;
		}
	}

	private cc49dcd41ee82253a805139c508e0926f()
	{
	}

	static cc49dcd41ee82253a805139c508e0926f()
	{
		if (c49253b4520c1b7e183dfcc9c426c0283 == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(cc49dcd41ee82253a805139c508e0926f._002Ecctor());
			}
			c49253b4520c1b7e183dfcc9c426c0283 = () => new cc49dcd41ee82253a805139c508e0926f();
		}
		c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<cc49dcd41ee82253a805139c508e0926f>(c49253b4520c1b7e183dfcc9c426c0283, null, null);
	}

	public static cc49dcd41ee82253a805139c508e0926f c2d8ce8b1ccf2b905e3b18f8a5145fe8a()
	{
		cc49dcd41ee82253a805139c508e0926f cc49dcd41ee82253a805139c508e0926f2 = c8aca4a0e3c0073ffb188fde832680587.Obtain();
		cc49dcd41ee82253a805139c508e0926f2.c0a4aead06b14dcb81618057a59d4a433();
		return cc49dcd41ee82253a805139c508e0926f2;
	}

	public void ca18b9b5faa9851d6355f617787bb5859()
	{
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	public void c03d72a32723924cb5b81c585d5df7d9d(Vector3F cb3dde1030d9e035889f9cb9b0590bb73, Vector3F c36072a957b54f1a78a610fe6e85c3219, Vector3F c490f9294e5484e74b14ae8e62297d1d0)
	{
		c738fde13b1eaa92fd510c0fd80e21772[NumberOfVertices] = cb3dde1030d9e035889f9cb9b0590bb73;
		c4f7068c10390fdf70e36f0f8a2c3e4af = cb3dde1030d9e035889f9cb9b0590bb73;
		cceb3d5c30f61d7c86e97b0b1fd3c1d00[NumberOfVertices] = c36072a957b54f1a78a610fe6e85c3219;
		c49538b885daa44d081b01b46e038abaa[NumberOfVertices] = c490f9294e5484e74b14ae8e62297d1d0;
		NumberOfVertices++;
		cd143fef024039434b5c81e466526abf8 = true;
	}

	public void ca501566257a4c82d152a429f5a74ecb6()
	{
		ce11e40ca3fb3db2be4758fb1b0736c17 = c2846baa0f40e616b89af5095e7d500ab;
		cc263f3e7e6ac7eb5f109705591760fb6 = c59f0b627db89664d42d66e15f85fe3af;
		c95f4ccb5ccc33937e6e451eb146afc5c = cc7a371c4dae811867cf2b5a963ba164d;
	}

	public void c0a4aead06b14dcb81618057a59d4a433()
	{
		for (int i = 0; i < 4; i++)
		{
			ref Vector3F reference = ref c738fde13b1eaa92fd510c0fd80e21772[i];
			reference = new Vector3F(float.NaN);
			ref Vector3F reference2 = ref cceb3d5c30f61d7c86e97b0b1fd3c1d00[i];
			reference2 = new Vector3F(float.NaN);
			ref Vector3F reference3 = ref c49538b885daa44d081b01b46e038abaa[i];
			reference3 = new Vector3F(float.NaN);
		}
		cfaa13c3933478652e1d13c21f7cfece2 = new Vector3F(float.NaN);
		c8db9d816e59b2168612a23097569af14 = new Vector3F(float.NaN);
		c596154f5b0c243d05e4fa792fd81b4bb = new Vector3F(float.NaN);
		cd143fef024039434b5c81e466526abf8 = true;
		c4f7068c10390fdf70e36f0f8a2c3e4af = new Vector3F(float.NaN);
		cb2e88f5c26d4b09e2bb9ed6db811a744 = default;
		IsValid = false;
		NumberOfVertices = 0;
	}

	public bool ce475b78d124a2feef9cd2aab44c161c4(Vector3F cb3dde1030d9e035889f9cb9b0590bb73)
	{
		for (int i = 0; i < NumberOfVertices; i++)
		{
			if (!Vector3F.AreNumericallyEqual(c738fde13b1eaa92fd510c0fd80e21772[i], cb3dde1030d9e035889f9cb9b0590bb73))
			{
				continue;
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
					__ldtoken(cc49dcd41ee82253a805139c508e0926f.ce475b78d124a2feef9cd2aab44c161c4(Vector3F));
				}
				return true;
			}
		}
		if (Vector3F.AreNumericallyEqual(c4f7068c10390fdf70e36f0f8a2c3e4af, cb3dde1030d9e035889f9cb9b0590bb73))
		{
			return true;
		}
		return false;
	}

	private void cc22df018b4f405eb78d4ecf049a7bd4d()
	{
		if (NumberOfVertices >= 4 && !cb2e88f5c26d4b09e2bb9ed6db811a744.c038b38aeb978fbb8244a627d918fa87e)
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
				__ldtoken(cc49dcd41ee82253a805139c508e0926f.cc22df018b4f405eb78d4ecf049a7bd4d());
			}
			c0332b9851c96067325b9a34480e93b40(3);
		}
		if (NumberOfVertices >= 3)
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
			if (!cb2e88f5c26d4b09e2bb9ed6db811a744.c36d963794c3bd34833029fffeaae3231)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				c0332b9851c96067325b9a34480e93b40(2);
			}
		}
		if (NumberOfVertices >= 2)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!cb2e88f5c26d4b09e2bb9ed6db811a744.c9d2054c1920b76ea632bd1663f8889c1)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					break;
				}
				c0332b9851c96067325b9a34480e93b40(1);
			}
		}
		if (NumberOfVertices < 1 || cb2e88f5c26d4b09e2bb9ed6db811a744.c21bd570620de15607214db559a372341)
		{
			return;
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			c0332b9851c96067325b9a34480e93b40(0);
			return;
		}
	}

	private void c0332b9851c96067325b9a34480e93b40(int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		NumberOfVertices--;
		ref Vector3F reference = ref c738fde13b1eaa92fd510c0fd80e21772[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		reference = c738fde13b1eaa92fd510c0fd80e21772[NumberOfVertices];
		ref Vector3F reference2 = ref cceb3d5c30f61d7c86e97b0b1fd3c1d00[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		reference2 = cceb3d5c30f61d7c86e97b0b1fd3c1d00[NumberOfVertices];
		ref Vector3F reference3 = ref c49538b885daa44d081b01b46e038abaa[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		reference3 = c49538b885daa44d081b01b46e038abaa[NumberOfVertices];
	}

	public void c322ef1ce205c6dbcf3a93ad899943435()
	{
		if (cd143fef024039434b5c81e466526abf8)
		{
			cd143fef024039434b5c81e466526abf8 = false;
			cb2e88f5c26d4b09e2bb9ed6db811a744 = default;
			IsValid = false;
			switch (NumberOfVertices)
			{
			case 1:
				c17254dfe27bbffb4d4922480448ed1ac();
				break;
			case 2:
				cf2aaae8ad464b78f9898e12717d862b7();
				break;
			case 3:
				c9adfc3178ac250a775877466449932be();
				break;
			default:
				cb89b34f23b0b54c74132fb667768bbc9();
				break;
			case 0:
				break;
			}
		}
	}

	private void c17254dfe27bbffb4d4922480448ed1ac()
	{
		cfaa13c3933478652e1d13c21f7cfece2 = cceb3d5c30f61d7c86e97b0b1fd3c1d00[0];
		c8db9d816e59b2168612a23097569af14 = c49538b885daa44d081b01b46e038abaa[0];
		c596154f5b0c243d05e4fa792fd81b4bb = c738fde13b1eaa92fd510c0fd80e21772[0];
		cb2e88f5c26d4b09e2bb9ed6db811a744.c5fada5690a4946af905392fc7856ac2f(1f, 0f, 0f, 0f);
		IsValid = true;
	}

	private void cf2aaae8ad464b78f9898e12717d862b7()
	{
		Vector3F vector = c738fde13b1eaa92fd510c0fd80e21772[1] - c738fde13b1eaa92fd510c0fd80e21772[0];
		Vector3F vector2 = Vector3F.Zero - c738fde13b1eaa92fd510c0fd80e21772[0];
		float num = Vector3F.Dot(vector, vector2);
		if (num <= 0f)
		{
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
				__ldtoken(cc49dcd41ee82253a805139c508e0926f.cf2aaae8ad464b78f9898e12717d862b7());
			}
			num = 0f;
			cb2e88f5c26d4b09e2bb9ed6db811a744.c21bd570620de15607214db559a372341 = true;
		}
		else
		{
			float lengthSquared = vector.LengthSquared;
			if (num > lengthSquared)
			{
				num = 1f;
				cb2e88f5c26d4b09e2bb9ed6db811a744.c9d2054c1920b76ea632bd1663f8889c1 = true;
			}
			else
			{
				num /= lengthSquared;
				cb2e88f5c26d4b09e2bb9ed6db811a744.c21bd570620de15607214db559a372341 = true;
				cb2e88f5c26d4b09e2bb9ed6db811a744.c9d2054c1920b76ea632bd1663f8889c1 = true;
			}
		}
		cb2e88f5c26d4b09e2bb9ed6db811a744.c5fada5690a4946af905392fc7856ac2f(1f - num, num, 0f, 0f);
		cfaa13c3933478652e1d13c21f7cfece2 = cceb3d5c30f61d7c86e97b0b1fd3c1d00[0] + num * (cceb3d5c30f61d7c86e97b0b1fd3c1d00[1] - cceb3d5c30f61d7c86e97b0b1fd3c1d00[0]);
		c8db9d816e59b2168612a23097569af14 = c49538b885daa44d081b01b46e038abaa[0] + num * (c49538b885daa44d081b01b46e038abaa[1] - c49538b885daa44d081b01b46e038abaa[0]);
		c596154f5b0c243d05e4fa792fd81b4bb = cfaa13c3933478652e1d13c21f7cfece2 - c8db9d816e59b2168612a23097569af14;
		cc22df018b4f405eb78d4ecf049a7bd4d();
		IsValid = true;
	}

	private void c9adfc3178ac250a775877466449932be()
	{
		c899eb12e7dd70318a844409be0e3579f(Vector3F.Zero, c738fde13b1eaa92fd510c0fd80e21772[0], c738fde13b1eaa92fd510c0fd80e21772[1], c738fde13b1eaa92fd510c0fd80e21772[2], ref cb2e88f5c26d4b09e2bb9ed6db811a744);
		cfaa13c3933478652e1d13c21f7cfece2 = cceb3d5c30f61d7c86e97b0b1fd3c1d00[0] * cb2e88f5c26d4b09e2bb9ed6db811a744.c659c6a630400e2e9a282e5b1b1ee04ab + cceb3d5c30f61d7c86e97b0b1fd3c1d00[1] * cb2e88f5c26d4b09e2bb9ed6db811a744.c8304ebbbca3014d9ce1063084bab1a85 + cceb3d5c30f61d7c86e97b0b1fd3c1d00[2] * cb2e88f5c26d4b09e2bb9ed6db811a744.cbc378f5ff2ea823a5d5063c0713d3c5e;
		c8db9d816e59b2168612a23097569af14 = c49538b885daa44d081b01b46e038abaa[0] * cb2e88f5c26d4b09e2bb9ed6db811a744.c659c6a630400e2e9a282e5b1b1ee04ab + c49538b885daa44d081b01b46e038abaa[1] * cb2e88f5c26d4b09e2bb9ed6db811a744.c8304ebbbca3014d9ce1063084bab1a85 + c49538b885daa44d081b01b46e038abaa[2] * cb2e88f5c26d4b09e2bb9ed6db811a744.cbc378f5ff2ea823a5d5063c0713d3c5e;
		c596154f5b0c243d05e4fa792fd81b4bb = cfaa13c3933478652e1d13c21f7cfece2 - c8db9d816e59b2168612a23097569af14;
		cc22df018b4f405eb78d4ecf049a7bd4d();
		IsValid = true;
	}

	private void cb89b34f23b0b54c74132fb667768bbc9()
	{
		if (!ce8940e67bedd22b9570162e0f28c0a3c(Vector3F.Zero, c738fde13b1eaa92fd510c0fd80e21772[0], c738fde13b1eaa92fd510c0fd80e21772[1], c738fde13b1eaa92fd510c0fd80e21772[2], c738fde13b1eaa92fd510c0fd80e21772[3]))
		{
			cfaa13c3933478652e1d13c21f7cfece2 = cceb3d5c30f61d7c86e97b0b1fd3c1d00[0] * cb2e88f5c26d4b09e2bb9ed6db811a744.c659c6a630400e2e9a282e5b1b1ee04ab + cceb3d5c30f61d7c86e97b0b1fd3c1d00[1] * cb2e88f5c26d4b09e2bb9ed6db811a744.c8304ebbbca3014d9ce1063084bab1a85 + cceb3d5c30f61d7c86e97b0b1fd3c1d00[2] * cb2e88f5c26d4b09e2bb9ed6db811a744.cbc378f5ff2ea823a5d5063c0713d3c5e + cceb3d5c30f61d7c86e97b0b1fd3c1d00[3] * cb2e88f5c26d4b09e2bb9ed6db811a744.ce770175a6cf38ec6b47d8f7b1e487b9d;
			c8db9d816e59b2168612a23097569af14 = c49538b885daa44d081b01b46e038abaa[0] * cb2e88f5c26d4b09e2bb9ed6db811a744.c659c6a630400e2e9a282e5b1b1ee04ab + c49538b885daa44d081b01b46e038abaa[1] * cb2e88f5c26d4b09e2bb9ed6db811a744.c8304ebbbca3014d9ce1063084bab1a85 + c49538b885daa44d081b01b46e038abaa[2] * cb2e88f5c26d4b09e2bb9ed6db811a744.cbc378f5ff2ea823a5d5063c0713d3c5e + c49538b885daa44d081b01b46e038abaa[3] * cb2e88f5c26d4b09e2bb9ed6db811a744.ce770175a6cf38ec6b47d8f7b1e487b9d;
			c596154f5b0c243d05e4fa792fd81b4bb = cfaa13c3933478652e1d13c21f7cfece2 - c8db9d816e59b2168612a23097569af14;
			cc22df018b4f405eb78d4ecf049a7bd4d();
			IsValid = true;
			return;
		}
		if (cb2e88f5c26d4b09e2bb9ed6db811a744.c0fbe03753bb021fba9a19567cadca039)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(cc49dcd41ee82253a805139c508e0926f.cb89b34f23b0b54c74132fb667768bbc9());
					}
					IsValid = false;
					return;
				}
			}
		}
		IsValid = true;
	}

	private static void c899eb12e7dd70318a844409be0e3579f(Vector3F cfef46e983928cc4e6880c424d1468b93, Vector3F c9a02227f0426ddf13f1ff1e95ca98ed1, Vector3F c806d9e3565cc3de689b37201c7c3ea5d, Vector3F c65ebb8c747123ffc59a019e850c055c4, ref c69512b66284989e9d24956d70acb4567 c7ad7de55ef2924e5c59e43ba4364b32d)
	{
		GeometryHelper.GetClosestPoint(new Triangle(c9a02227f0426ddf13f1ff1e95ca98ed1, c806d9e3565cc3de689b37201c7c3ea5d, c65ebb8c747123ffc59a019e850c055c4), cfef46e983928cc4e6880c424d1468b93, out var u, out var v, out var w);
		c7ad7de55ef2924e5c59e43ba4364b32d.c5fada5690a4946af905392fc7856ac2f(u, v, w, 0f);
		c7ad7de55ef2924e5c59e43ba4364b32d.c596154f5b0c243d05e4fa792fd81b4bb = u * c9a02227f0426ddf13f1ff1e95ca98ed1 + v * c806d9e3565cc3de689b37201c7c3ea5d + w * c65ebb8c747123ffc59a019e850c055c4;
		c7ad7de55ef2924e5c59e43ba4364b32d.c21bd570620de15607214db559a372341 = u > 0f;
		c7ad7de55ef2924e5c59e43ba4364b32d.c9d2054c1920b76ea632bd1663f8889c1 = v > 0f;
		c7ad7de55ef2924e5c59e43ba4364b32d.c36d963794c3bd34833029fffeaae3231 = w > 0f;
		c7ad7de55ef2924e5c59e43ba4364b32d.c038b38aeb978fbb8244a627d918fa87e = false;
	}

	private bool ce8940e67bedd22b9570162e0f28c0a3c(Vector3F cfef46e983928cc4e6880c424d1468b93, Vector3F c8cd7003b4cede15cefa1132bb57ab498, Vector3F cdfa12e7bf2e24daa88d9878aa362c551, Vector3F cb4cb438193a0c98b1934c324470ce2d7, Vector3F cdab383321568d91e2316c13c5e9b063b)
	{
		cb2e88f5c26d4b09e2bb9ed6db811a744.c596154f5b0c243d05e4fa792fd81b4bb = cfef46e983928cc4e6880c424d1468b93;
		cb2e88f5c26d4b09e2bb9ed6db811a744.c21bd570620de15607214db559a372341 = true;
		cb2e88f5c26d4b09e2bb9ed6db811a744.c9d2054c1920b76ea632bd1663f8889c1 = true;
		cb2e88f5c26d4b09e2bb9ed6db811a744.c36d963794c3bd34833029fffeaae3231 = true;
		cb2e88f5c26d4b09e2bb9ed6db811a744.c038b38aeb978fbb8244a627d918fa87e = true;
		bool? flag = GeometryHelper.ArePointsOnOppositeSides(cfef46e983928cc4e6880c424d1468b93, cdab383321568d91e2316c13c5e9b063b, c8cd7003b4cede15cefa1132bb57ab498, cdfa12e7bf2e24daa88d9878aa362c551, cb4cb438193a0c98b1934c324470ce2d7);
		bool? flag2 = GeometryHelper.ArePointsOnOppositeSides(cfef46e983928cc4e6880c424d1468b93, cdfa12e7bf2e24daa88d9878aa362c551, c8cd7003b4cede15cefa1132bb57ab498, cb4cb438193a0c98b1934c324470ce2d7, cdab383321568d91e2316c13c5e9b063b);
		bool? flag3 = GeometryHelper.ArePointsOnOppositeSides(cfef46e983928cc4e6880c424d1468b93, cb4cb438193a0c98b1934c324470ce2d7, c8cd7003b4cede15cefa1132bb57ab498, cdab383321568d91e2316c13c5e9b063b, cdfa12e7bf2e24daa88d9878aa362c551);
		bool? flag4 = GeometryHelper.ArePointsOnOppositeSides(cfef46e983928cc4e6880c424d1468b93, c8cd7003b4cede15cefa1132bb57ab498, cdfa12e7bf2e24daa88d9878aa362c551, cdab383321568d91e2316c13c5e9b063b, cb4cb438193a0c98b1934c324470ce2d7);
		if (flag.HasValue)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(cc49dcd41ee82253a805139c508e0926f.ce8940e67bedd22b9570162e0f28c0a3c(Vector3F, Vector3F, Vector3F, Vector3F, Vector3F));
			}
			if (flag2.HasValue)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				if (flag3.HasValue)
				{
					if (flag4.HasValue)
					{
						if (!flag.Value && !flag2.Value && !flag3.Value && !flag4.Value)
						{
							while (true)
							{
								switch (7)
								{
								case 0:
									break;
								default:
									return true;
								}
							}
						}
						float num = float.MaxValue;
						c69512b66284989e9d24956d70acb4567 c7ad7de55ef2924e5c59e43ba4364b32d = default;
						if (flag.Value)
						{
							c899eb12e7dd70318a844409be0e3579f(cfef46e983928cc4e6880c424d1468b93, c8cd7003b4cede15cefa1132bb57ab498, cdfa12e7bf2e24daa88d9878aa362c551, cb4cb438193a0c98b1934c324470ce2d7, ref c7ad7de55ef2924e5c59e43ba4364b32d);
							Vector3F vector3F = c7ad7de55ef2924e5c59e43ba4364b32d.c596154f5b0c243d05e4fa792fd81b4bb;
							num = (cfef46e983928cc4e6880c424d1468b93 - vector3F).LengthSquared;
							cb2e88f5c26d4b09e2bb9ed6db811a744.c596154f5b0c243d05e4fa792fd81b4bb = vector3F;
							cb2e88f5c26d4b09e2bb9ed6db811a744.c21bd570620de15607214db559a372341 = c7ad7de55ef2924e5c59e43ba4364b32d.c21bd570620de15607214db559a372341;
							cb2e88f5c26d4b09e2bb9ed6db811a744.c9d2054c1920b76ea632bd1663f8889c1 = c7ad7de55ef2924e5c59e43ba4364b32d.c9d2054c1920b76ea632bd1663f8889c1;
							cb2e88f5c26d4b09e2bb9ed6db811a744.c36d963794c3bd34833029fffeaae3231 = c7ad7de55ef2924e5c59e43ba4364b32d.c36d963794c3bd34833029fffeaae3231;
							cb2e88f5c26d4b09e2bb9ed6db811a744.c038b38aeb978fbb8244a627d918fa87e = false;
							cb2e88f5c26d4b09e2bb9ed6db811a744.c5fada5690a4946af905392fc7856ac2f(c7ad7de55ef2924e5c59e43ba4364b32d.c659c6a630400e2e9a282e5b1b1ee04ab, c7ad7de55ef2924e5c59e43ba4364b32d.c8304ebbbca3014d9ce1063084bab1a85, c7ad7de55ef2924e5c59e43ba4364b32d.cbc378f5ff2ea823a5d5063c0713d3c5e, 0f);
						}
						if (flag2.Value)
						{
							while (true)
							{
								switch (2)
								{
								case 0:
									continue;
								}
								break;
							}
							c899eb12e7dd70318a844409be0e3579f(cfef46e983928cc4e6880c424d1468b93, c8cd7003b4cede15cefa1132bb57ab498, cb4cb438193a0c98b1934c324470ce2d7, cdab383321568d91e2316c13c5e9b063b, ref c7ad7de55ef2924e5c59e43ba4364b32d);
							Vector3F vector3F2 = c7ad7de55ef2924e5c59e43ba4364b32d.c596154f5b0c243d05e4fa792fd81b4bb;
							float lengthSquared = (cfef46e983928cc4e6880c424d1468b93 - vector3F2).LengthSquared;
							if (lengthSquared < num)
							{
								num = lengthSquared;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c596154f5b0c243d05e4fa792fd81b4bb = vector3F2;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c21bd570620de15607214db559a372341 = c7ad7de55ef2924e5c59e43ba4364b32d.c21bd570620de15607214db559a372341;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c9d2054c1920b76ea632bd1663f8889c1 = false;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c36d963794c3bd34833029fffeaae3231 = c7ad7de55ef2924e5c59e43ba4364b32d.c9d2054c1920b76ea632bd1663f8889c1;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c038b38aeb978fbb8244a627d918fa87e = c7ad7de55ef2924e5c59e43ba4364b32d.c36d963794c3bd34833029fffeaae3231;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c5fada5690a4946af905392fc7856ac2f(c7ad7de55ef2924e5c59e43ba4364b32d.c659c6a630400e2e9a282e5b1b1ee04ab, 0f, c7ad7de55ef2924e5c59e43ba4364b32d.c8304ebbbca3014d9ce1063084bab1a85, c7ad7de55ef2924e5c59e43ba4364b32d.cbc378f5ff2ea823a5d5063c0713d3c5e);
							}
						}
						if (flag3.Value)
						{
							c899eb12e7dd70318a844409be0e3579f(cfef46e983928cc4e6880c424d1468b93, c8cd7003b4cede15cefa1132bb57ab498, cdab383321568d91e2316c13c5e9b063b, cdfa12e7bf2e24daa88d9878aa362c551, ref c7ad7de55ef2924e5c59e43ba4364b32d);
							Vector3F vector3F3 = c7ad7de55ef2924e5c59e43ba4364b32d.c596154f5b0c243d05e4fa792fd81b4bb;
							float lengthSquared2 = (cfef46e983928cc4e6880c424d1468b93 - vector3F3).LengthSquared;
							if (lengthSquared2 < num)
							{
								num = lengthSquared2;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c596154f5b0c243d05e4fa792fd81b4bb = vector3F3;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c21bd570620de15607214db559a372341 = c7ad7de55ef2924e5c59e43ba4364b32d.c21bd570620de15607214db559a372341;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c9d2054c1920b76ea632bd1663f8889c1 = c7ad7de55ef2924e5c59e43ba4364b32d.c36d963794c3bd34833029fffeaae3231;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c36d963794c3bd34833029fffeaae3231 = false;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c038b38aeb978fbb8244a627d918fa87e = c7ad7de55ef2924e5c59e43ba4364b32d.c9d2054c1920b76ea632bd1663f8889c1;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c5fada5690a4946af905392fc7856ac2f(c7ad7de55ef2924e5c59e43ba4364b32d.c659c6a630400e2e9a282e5b1b1ee04ab, c7ad7de55ef2924e5c59e43ba4364b32d.cbc378f5ff2ea823a5d5063c0713d3c5e, 0f, c7ad7de55ef2924e5c59e43ba4364b32d.c8304ebbbca3014d9ce1063084bab1a85);
							}
						}
						if (flag4.Value)
						{
							while (true)
							{
								switch (7)
								{
								case 0:
									continue;
								}
								break;
							}
							c899eb12e7dd70318a844409be0e3579f(cfef46e983928cc4e6880c424d1468b93, cdfa12e7bf2e24daa88d9878aa362c551, cdab383321568d91e2316c13c5e9b063b, cb4cb438193a0c98b1934c324470ce2d7, ref c7ad7de55ef2924e5c59e43ba4364b32d);
							Vector3F vector3F4 = c7ad7de55ef2924e5c59e43ba4364b32d.c596154f5b0c243d05e4fa792fd81b4bb;
							float lengthSquared3 = (cfef46e983928cc4e6880c424d1468b93 - vector3F4).LengthSquared;
							if (lengthSquared3 < num)
							{
								cb2e88f5c26d4b09e2bb9ed6db811a744.c596154f5b0c243d05e4fa792fd81b4bb = vector3F4;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c21bd570620de15607214db559a372341 = false;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c9d2054c1920b76ea632bd1663f8889c1 = c7ad7de55ef2924e5c59e43ba4364b32d.c21bd570620de15607214db559a372341;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c36d963794c3bd34833029fffeaae3231 = c7ad7de55ef2924e5c59e43ba4364b32d.c36d963794c3bd34833029fffeaae3231;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c038b38aeb978fbb8244a627d918fa87e = c7ad7de55ef2924e5c59e43ba4364b32d.c9d2054c1920b76ea632bd1663f8889c1;
								cb2e88f5c26d4b09e2bb9ed6db811a744.c5fada5690a4946af905392fc7856ac2f(0f, c7ad7de55ef2924e5c59e43ba4364b32d.c659c6a630400e2e9a282e5b1b1ee04ab, c7ad7de55ef2924e5c59e43ba4364b32d.cbc378f5ff2ea823a5d5063c0713d3c5e, c7ad7de55ef2924e5c59e43ba4364b32d.c8304ebbbca3014d9ce1063084bab1a85);
							}
						}
						return false;
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
				}
			}
		}
		cb2e88f5c26d4b09e2bb9ed6db811a744.c0fbe03753bb021fba9a19567cadca039 = true;
		return true;
	}

	[CompilerGenerated]
	private static cc49dcd41ee82253a805139c508e0926f c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new cc49dcd41ee82253a805139c508e0926f();
	}
}
