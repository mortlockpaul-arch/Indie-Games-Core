using System;
using System.Runtime.CompilerServices;
using DigitalRune;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;

namespace A;

internal class c778cf8148fb9ff468ba20f6321de4e73 : ConvexShape
{
	public static readonly ResourcePool<c778cf8148fb9ff468ba20f6321de4e73> c8aca4a0e3c0073ffb188fde832680587;

	private c107cf2db36674a478eaf84e50d31337e c4570ec16753428173e97de5f0804ffdc;

	private c107cf2db36674a478eaf84e50d31337e cbf93399091885a37a9bba3686d7c15c0;

	[CompilerGenerated]
	private static Func<c778cf8148fb9ff468ba20f6321de4e73> c49253b4520c1b7e183dfcc9c426c0283;

	public override Vector3F InnerPoint
	{
		get
		{
			Vector3F vector3F = c4570ec16753428173e97de5f0804ffdc.Pose.ToWorldPosition(c4570ec16753428173e97de5f0804ffdc.Shape.InnerPoint);
			Vector3F vector3F2 = cbf93399091885a37a9bba3686d7c15c0.Pose.ToWorldPosition(cbf93399091885a37a9bba3686d7c15c0.Shape.InnerPoint);
			return vector3F + vector3F2;
		}
	}

	public c107cf2db36674a478eaf84e50d31337e c64743d4d4c42af8c72d03a20d80824b5
	{
		get
		{
			return c4570ec16753428173e97de5f0804ffdc;
		}
		set
		{
			c4570ec16753428173e97de5f0804ffdc = value;
		}
	}

	public c107cf2db36674a478eaf84e50d31337e ce588e75b2b6b81bd58a657706b7a1553
	{
		get
		{
			return cbf93399091885a37a9bba3686d7c15c0;
		}
		set
		{
			cbf93399091885a37a9bba3686d7c15c0 = value;
		}
	}

	private c778cf8148fb9ff468ba20f6321de4e73()
	{
	}

	static c778cf8148fb9ff468ba20f6321de4e73()
	{
		if (c49253b4520c1b7e183dfcc9c426c0283 == null)
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
			if (1 == 0)
			{
				__ldtoken(c778cf8148fb9ff468ba20f6321de4e73._002Ecctor());
			}
			c49253b4520c1b7e183dfcc9c426c0283 = () => new c778cf8148fb9ff468ba20f6321de4e73();
		}
		c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<c778cf8148fb9ff468ba20f6321de4e73>(c49253b4520c1b7e183dfcc9c426c0283, null, null);
	}

	public static c778cf8148fb9ff468ba20f6321de4e73 c2d8ce8b1ccf2b905e3b18f8a5145fe8a()
	{
		return c8aca4a0e3c0073ffb188fde832680587.Obtain();
	}

	public void ca18b9b5faa9851d6355f617787bb5859()
	{
		c64743d4d4c42af8c72d03a20d80824b5 = null;
		ce588e75b2b6b81bd58a657706b7a1553 = null;
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	protected override Shape CreateInstanceCore()
	{
		throw new NotImplementedException();
	}

	protected override void CloneCore(Shape sourceShape)
	{
		throw new NotImplementedException();
	}

	public override Vector3F GetSupportPoint(Vector3F direction)
	{
		Vector3F direction2 = c4570ec16753428173e97de5f0804ffdc.Pose.ToLocalDirection(direction);
		Vector3F direction3 = cbf93399091885a37a9bba3686d7c15c0.Pose.ToLocalDirection(direction);
		Vector3F supportPoint = ((ConvexShape)c4570ec16753428173e97de5f0804ffdc.Shape).GetSupportPoint(direction2);
		Vector3F supportPoint2 = ((ConvexShape)cbf93399091885a37a9bba3686d7c15c0.Shape).GetSupportPoint(direction3);
		Vector3F vector3F = c4570ec16753428173e97de5f0804ffdc.Pose.ToWorldPosition(supportPoint);
		Vector3F vector3F2 = cbf93399091885a37a9bba3686d7c15c0.Pose.ToWorldPosition(supportPoint2);
		return vector3F + vector3F2;
	}

	public override float GetVolume(float relativeError, int iterationLimit)
	{
		throw new NotImplementedException();
	}

	public override Vector3F GetSupportPointNormalized(Vector3F directionNormalized)
	{
		Vector3F directionNormalized2 = c4570ec16753428173e97de5f0804ffdc.Pose.ToLocalDirection(directionNormalized);
		Vector3F directionNormalized3 = cbf93399091885a37a9bba3686d7c15c0.Pose.ToLocalDirection(directionNormalized);
		Vector3F supportPointNormalized = ((ConvexShape)c4570ec16753428173e97de5f0804ffdc.Shape).GetSupportPointNormalized(directionNormalized2);
		Vector3F supportPointNormalized2 = ((ConvexShape)cbf93399091885a37a9bba3686d7c15c0.Shape).GetSupportPointNormalized(directionNormalized3);
		Vector3F vector3F = c4570ec16753428173e97de5f0804ffdc.Pose.ToWorldPosition(supportPointNormalized);
		Vector3F vector3F2 = cbf93399091885a37a9bba3686d7c15c0.Pose.ToWorldPosition(supportPointNormalized2);
		return vector3F + vector3F2;
	}

	[CompilerGenerated]
	private static c778cf8148fb9ff468ba20f6321de4e73 c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new c778cf8148fb9ff468ba20f6321de4e73();
	}
}
