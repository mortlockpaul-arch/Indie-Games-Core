using System;
using System.Collections.Generic;
using System.Linq;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Mathematics.Statistics;

namespace DigitalRune.Geometry;

/// <summary>
///       Provides helper methods for various geometry tasks.
///       </summary>
public static class GeometryHelper
{
	private class c354edd6f218fd4871fbb9bd00aa78db8
	{
		public readonly Vector3F c9fc1a33c8fe55213152479d7ac87ef15;

		public int c6963d78d009d6d192bf7efd926b22003 = -1;

		public c354edd6f218fd4871fbb9bd00aa78db8(Vector3F c75c28b357dedcad9a44abd406f2361f3)
		{
			c9fc1a33c8fe55213152479d7ac87ef15 = c75c28b357dedcad9a44abd406f2361f3;
		}
	}

	private class c9fb21dd88a7948b34865889c9f33f4bc
	{
		public int c125b54a9bf1ca20fcabec365ce7e27c6;

		public int c4b7d9aa0151452e58db5e508df376dcf;

		public int cdfdfcfc4e1d6ae7dd8081f66a8d9f12e;

		public int cda2eac9404b56954803c20fc28f7d0fd;

		public c9fb21dd88a7948b34865889c9f33f4bc(int c46188f8943dfd7c8b6cea470a71852a0, int c04dc9086a46dd0727aee6dfc43d9f6e5, int cd97b8e679273be2622bf7e8989c0f84a, int c9e5863c174840ce096a45496fda8518c)
		{
			c125b54a9bf1ca20fcabec365ce7e27c6 = c46188f8943dfd7c8b6cea470a71852a0;
			c4b7d9aa0151452e58db5e508df376dcf = c04dc9086a46dd0727aee6dfc43d9f6e5;
			cdfdfcfc4e1d6ae7dd8081f66a8d9f12e = cd97b8e679273be2622bf7e8989c0f84a;
			cda2eac9404b56954803c20fc28f7d0fd = c9e5863c174840ce096a45496fda8518c;
		}
	}

	private class c98f8cbf4d7195969dd16693ab608adec
	{
		public readonly List<Vector3F> c9c9dc7a5e68aac53b065464d791aba7d = new List<Vector3F>();

		public float c7af50dba23fb61a6adbbf043e19e469d;

		public int c874a839ca13681a1e4151cb408603f37(Vector3F cfef46e983928cc4e6880c424d1468b93)
		{
			int count = c9c9dc7a5e68aac53b065464d791aba7d.Count;
			for (int i = 0; i < count; i++)
			{
				Vector3F vector = c9c9dc7a5e68aac53b065464d791aba7d[i];
				if (c7af50dba23fb61a6adbbf043e19e469d > 0f)
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
					if (1 == 0)
					{
						__ldtoken(c98f8cbf4d7195969dd16693ab608adec.c874a839ca13681a1e4151cb408603f37(Vector3F));
					}
					if (Vector3F.AreNumericallyEqual(vector, cfef46e983928cc4e6880c424d1468b93, c7af50dba23fb61a6adbbf043e19e469d))
					{
						goto IL_006e;
					}
				}
				if (c7af50dba23fb61a6adbbf043e19e469d != 0f)
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
					break;
				}
				if (!Vector3F.AreNumericallyEqual(vector, cfef46e983928cc4e6880c424d1468b93))
				{
					continue;
				}
				goto IL_006e;
				IL_006e:
				return i;
			}
			return -1;
		}
	}

	static GeometryHelper()
	{
		TypeInitializer.Initialize(ccb48cf63175e6724337dcebf74566228.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(12, ccb48cf63175e6724337dcebf74566228.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Gets the closest point of a line to a point.
	///       </summary>
	/// <param name="point">The point.</param>
	/// <param name="line">The line.</param>
	/// <param name="closestPointOnLine">
	///       The point on the line that is closest to <paramref name="point" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="point" /> is on the line.</returns>
	public static bool GetClosestPoint(Line line, Vector3F point, out Vector3F closestPointOnLine)
	{
		cb01280d46caa416165314e91eb3ad328(new LineSegment(line.PointOnLine, line.PointOnLine + line.Direction), point, out var c1ee99c45b13417a935570173f4daec);
		closestPointOnLine = line.PointOnLine + c1ee99c45b13417a935570173f4daec * line.Direction;
		return Vector3F.AreNumericallyEqual(point, closestPointOnLine);
	}

	/// <summary>
	///       Gets the closest points of two lines.
	///       </summary>
	/// <param name="line0">The first line.</param>
	/// <param name="line1">The second line.</param>
	/// <param name="point0">
	///       The point on <paramref name="line0" /> that is closest to <paramref name="line1" />.
	///       </param>
	/// <param name="point1">
	///       The point on <paramref name="line1" /> that is closest to <paramref name="line0" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the lines are touching (<paramref name="point0" /> and 
	///       <paramref name="point1" /> are identical); otherwise <see langword="false" />.
	///       </returns>
	public static bool GetClosestPoints(Line line0, Line line1, out Vector3F point0, out Vector3F point1)
	{
		ce4f5155261410683b1d4104884475060(new LineSegment(line0.PointOnLine, line0.PointOnLine + line0.Direction), new LineSegment(line1.PointOnLine, line1.PointOnLine + line1.Direction), out var cf3e112fd3d3aa49f1b42cc826ed0e, out var ccc1f272a7f7272cd2400093d36b02c);
		point0 = line0.PointOnLine + cf3e112fd3d3aa49f1b42cc826ed0e * line0.Direction;
		point1 = line1.PointOnLine + ccc1f272a7f7272cd2400093d36b02c * line1.Direction;
		return Vector3F.AreNumericallyEqual(point0, point1);
	}

	/// <summary>
	///       Gets the closest point of a line segment to a point.
	///       </summary>
	/// <param name="point">The point.</param>
	/// <param name="lineSegment">The line segment.</param>
	/// <param name="closestPointOnLineSegment">
	///       The point on the line segment that is closest to <paramref name="point" />.</param>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="point" /> is on the line segment; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool GetClosestPoints(LineSegment lineSegment, Vector3F point, out Vector3F closestPointOnLineSegment)
	{
		cb01280d46caa416165314e91eb3ad328(lineSegment, point, out var c1ee99c45b13417a935570173f4daec);
		c1ee99c45b13417a935570173f4daec = Math.Max(0f, Math.Min(1f, c1ee99c45b13417a935570173f4daec));
		closestPointOnLineSegment = lineSegment.Start + c1ee99c45b13417a935570173f4daec * (lineSegment.End - lineSegment.Start);
		return Vector3F.AreNumericallyEqual(point, closestPointOnLineSegment);
	}

	/// <summary>
	///       Gets the closest points of two line segments.
	///       </summary>
	/// <param name="segment0">The first line segment.</param>
	/// <param name="segment1">The second line segment.</param>
	/// <param name="point0">
	///       The point on <paramref name="segment0" /> that is closest to <paramref name="segment1" />.
	///       </param>
	/// <param name="point1">
	///       The point on <paramref name="segment1" /> that is closest to <paramref name="segment0" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the line segments are touching (<paramref name="point0" /> and 
	///       <paramref name="point1" /> are identical); otherwise <see langword="false" />.
	///       </returns>
	public static bool GetClosestPoints(LineSegment segment0, LineSegment segment1, out Vector3F point0, out Vector3F point1)
	{
		ce4f5155261410683b1d4104884475060(segment0, segment1, out var cf3e112fd3d3aa49f1b42cc826ed0e, out var ccc1f272a7f7272cd2400093d36b02c);
		cc6111a7cd73bc9632ccf5bb2e3d77918(segment0, segment1, cf3e112fd3d3aa49f1b42cc826ed0e, ccc1f272a7f7272cd2400093d36b02c, out point0, out point1);
		return Vector3F.AreNumericallyEqual(point0, point1);
	}

	/// <summary>
	///       Gets the closest points of a line and a line segment.
	///       </summary>
	/// <param name="line">The line.</param>
	/// <param name="segment">The line segment.</param>
	/// <param name="pointOnLine">
	///       The point on <paramref name="line" /> that is closest to <paramref name="segment" />.
	///       </param>
	/// <param name="pointOnSegment">
	///       The point on <paramref name="segment" /> that is closest to <paramref name="line" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the line and the line segment are touching 
	///       (<paramref name="pointOnLine" /> and <paramref name="pointOnSegment" /> are identical); 
	///       otherwise <see langword="false" />.
	///       </returns>
	public static bool GetClosestPoints(LineSegment segment, Line line, out Vector3F pointOnLine, out Vector3F pointOnSegment)
	{
		ce4f5155261410683b1d4104884475060(new LineSegment(line.PointOnLine, line.PointOnLine + line.Direction), segment, out var cf3e112fd3d3aa49f1b42cc826ed0e, out var ccc1f272a7f7272cd2400093d36b02c);
		cc6111a7cd73bc9632ccf5bb2e3d77918(segment, line, cf3e112fd3d3aa49f1b42cc826ed0e, ccc1f272a7f7272cd2400093d36b02c, out pointOnLine, out pointOnSegment);
		return Vector3F.AreNumericallyEqual(pointOnLine, pointOnSegment);
	}

	internal static void ce4f5155261410683b1d4104884475060(LineSegment cf9302bb064569417b57754f42e65a47f, LineSegment c79d8c1ab1064a2f3af39a604607e43ae, out float cf3e112fd3d3aa49f1b42cc826ed0e916, out float ccc1f272a7f7272cd2400093d36b02c38)
	{
		float epsilonFSquared = Numeric.EpsilonFSquared;
		Vector3F vector = cf9302bb064569417b57754f42e65a47f.End - cf9302bb064569417b57754f42e65a47f.Start;
		Vector3F vector3F = c79d8c1ab1064a2f3af39a604607e43ae.End - c79d8c1ab1064a2f3af39a604607e43ae.Start;
		float lengthSquared = vector.LengthSquared;
		float lengthSquared2 = vector3F.LengthSquared;
		if (lengthSquared < epsilonFSquared)
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
						__ldtoken(GeometryHelper.ce4f5155261410683b1d4104884475060(LineSegment, LineSegment, ref float, ref float));
					}
					cf3e112fd3d3aa49f1b42cc826ed0e916 = 0f;
					cb01280d46caa416165314e91eb3ad328(c79d8c1ab1064a2f3af39a604607e43ae, cf9302bb064569417b57754f42e65a47f.Start, out ccc1f272a7f7272cd2400093d36b02c38);
					return;
				}
			}
		}
		if (lengthSquared2 < epsilonFSquared)
		{
			ccc1f272a7f7272cd2400093d36b02c38 = 0f;
			cb01280d46caa416165314e91eb3ad328(cf9302bb064569417b57754f42e65a47f, c79d8c1ab1064a2f3af39a604607e43ae.Start, out cf3e112fd3d3aa49f1b42cc826ed0e916);
			return;
		}
		Vector3F vector2 = c79d8c1ab1064a2f3af39a604607e43ae.Start - cf9302bb064569417b57754f42e65a47f.Start;
		float num = 0f - Vector3F.Dot(vector, vector3F);
		float num2 = lengthSquared * lengthSquared2 - num * num;
		if (Math.Abs(num2) < Numeric.EpsilonF)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					cf3e112fd3d3aa49f1b42cc826ed0e916 = 0f;
					cb01280d46caa416165314e91eb3ad328(c79d8c1ab1064a2f3af39a604607e43ae, cf9302bb064569417b57754f42e65a47f.Start, out ccc1f272a7f7272cd2400093d36b02c38);
					return;
				}
			}
		}
		float num3 = Vector3F.Dot(vector, vector2);
		float num4 = 0f - Vector3F.Dot(vector3F, vector2);
		ccc1f272a7f7272cd2400093d36b02c38 = (lengthSquared * num4 - num3 * num) / num2;
		cf3e112fd3d3aa49f1b42cc826ed0e916 = (num3 - num * ccc1f272a7f7272cd2400093d36b02c38) / lengthSquared;
	}

	internal static void cb01280d46caa416165314e91eb3ad328(LineSegment c4565d1ce37c61d3abb87b1403d540259, Vector3F cfef46e983928cc4e6880c424d1468b93, out float c1ee99c45b13417a935570173f4daec16)
	{
		float lengthSquared = c4565d1ce37c61d3abb87b1403d540259.LengthSquared;
		if (lengthSquared < Numeric.EpsilonFSquared)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(GeometryHelper.cb01280d46caa416165314e91eb3ad328(LineSegment, Vector3F, ref float));
					}
					c1ee99c45b13417a935570173f4daec16 = 0f;
					return;
				}
			}
		}
		Vector3F vector = cfef46e983928cc4e6880c424d1468b93 - c4565d1ce37c61d3abb87b1403d540259.Start;
		c1ee99c45b13417a935570173f4daec16 = Vector3F.Dot(c4565d1ce37c61d3abb87b1403d540259.End - c4565d1ce37c61d3abb87b1403d540259.Start, vector) / lengthSquared;
	}

	private static void cc6111a7cd73bc9632ccf5bb2e3d77918(LineSegment c7cd96b3169c272a80521ea678456dce7, Line c2217c4fd9f0103c2f9b3dc23c71674b5, float cf3e112fd3d3aa49f1b42cc826ed0e916, float ccc1f272a7f7272cd2400093d36b02c38, out Vector3F c4c57b92399398b9121fdd98b65cebc8f, out Vector3F c7a40f80128e42ce1e9987529135d70cf)
	{
		if (ccc1f272a7f7272cd2400093d36b02c38 < 0f || ccc1f272a7f7272cd2400093d36b02c38 > 1f)
		{
			ccc1f272a7f7272cd2400093d36b02c38 = Math.Max(0f, Math.Min(1f, ccc1f272a7f7272cd2400093d36b02c38));
			c7a40f80128e42ce1e9987529135d70cf = c7cd96b3169c272a80521ea678456dce7.Start + ccc1f272a7f7272cd2400093d36b02c38 * (c7cd96b3169c272a80521ea678456dce7.End - c7cd96b3169c272a80521ea678456dce7.Start);
			cb01280d46caa416165314e91eb3ad328(new LineSegment(c2217c4fd9f0103c2f9b3dc23c71674b5.PointOnLine, c2217c4fd9f0103c2f9b3dc23c71674b5.PointOnLine + c2217c4fd9f0103c2f9b3dc23c71674b5.Direction), c7a40f80128e42ce1e9987529135d70cf, out cf3e112fd3d3aa49f1b42cc826ed0e916);
			c4c57b92399398b9121fdd98b65cebc8f = c2217c4fd9f0103c2f9b3dc23c71674b5.PointOnLine + cf3e112fd3d3aa49f1b42cc826ed0e916 * c2217c4fd9f0103c2f9b3dc23c71674b5.Direction;
		}
		else
		{
			c4c57b92399398b9121fdd98b65cebc8f = c2217c4fd9f0103c2f9b3dc23c71674b5.PointOnLine + cf3e112fd3d3aa49f1b42cc826ed0e916 * c2217c4fd9f0103c2f9b3dc23c71674b5.Direction;
			c7a40f80128e42ce1e9987529135d70cf = c7cd96b3169c272a80521ea678456dce7.Start + ccc1f272a7f7272cd2400093d36b02c38 * (c7cd96b3169c272a80521ea678456dce7.End - c7cd96b3169c272a80521ea678456dce7.Start);
		}
	}

	private static void cc6111a7cd73bc9632ccf5bb2e3d77918(LineSegment cde8eb1bb0b47a38e12a97f998f0883cc, LineSegment c33a3496380455e016a42bec8afc78bc7, float cf3e112fd3d3aa49f1b42cc826ed0e916, float ccc1f272a7f7272cd2400093d36b02c38, out Vector3F c4dba96e545d7820e037eeacdfa8bba27, out Vector3F cb5750b83fa6cdab2db2259fdbe70a659)
	{
		if (!(cf3e112fd3d3aa49f1b42cc826ed0e916 < 0f))
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
				__ldtoken(GeometryHelper.cc6111a7cd73bc9632ccf5bb2e3d77918(LineSegment, LineSegment, float, float, ref Vector3F, ref Vector3F));
			}
			if (!(cf3e112fd3d3aa49f1b42cc826ed0e916 > 1f))
			{
				goto IL_0199;
			}
		}
		if (!(ccc1f272a7f7272cd2400093d36b02c38 < 0f))
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
			if (!(ccc1f272a7f7272cd2400093d36b02c38 > 1f))
			{
				goto IL_0199;
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
		}
		cf3e112fd3d3aa49f1b42cc826ed0e916 = Math.Max(0f, Math.Min(1f, cf3e112fd3d3aa49f1b42cc826ed0e916));
		c4dba96e545d7820e037eeacdfa8bba27 = cde8eb1bb0b47a38e12a97f998f0883cc.Start + cf3e112fd3d3aa49f1b42cc826ed0e916 * (cde8eb1bb0b47a38e12a97f998f0883cc.End - cde8eb1bb0b47a38e12a97f998f0883cc.Start);
		cb01280d46caa416165314e91eb3ad328(c33a3496380455e016a42bec8afc78bc7, c4dba96e545d7820e037eeacdfa8bba27, out ccc1f272a7f7272cd2400093d36b02c38);
		if (!(ccc1f272a7f7272cd2400093d36b02c38 < 0f))
		{
			if (!(ccc1f272a7f7272cd2400093d36b02c38 > 1f))
			{
				cb5750b83fa6cdab2db2259fdbe70a659 = c33a3496380455e016a42bec8afc78bc7.Start + ccc1f272a7f7272cd2400093d36b02c38 * (c33a3496380455e016a42bec8afc78bc7.End - c33a3496380455e016a42bec8afc78bc7.Start);
				return;
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
		}
		ccc1f272a7f7272cd2400093d36b02c38 = Math.Max(0f, Math.Min(1f, ccc1f272a7f7272cd2400093d36b02c38));
		cb5750b83fa6cdab2db2259fdbe70a659 = c33a3496380455e016a42bec8afc78bc7.Start + ccc1f272a7f7272cd2400093d36b02c38 * (c33a3496380455e016a42bec8afc78bc7.End - c33a3496380455e016a42bec8afc78bc7.Start);
		cb01280d46caa416165314e91eb3ad328(cde8eb1bb0b47a38e12a97f998f0883cc, cb5750b83fa6cdab2db2259fdbe70a659, out cf3e112fd3d3aa49f1b42cc826ed0e916);
		cf3e112fd3d3aa49f1b42cc826ed0e916 = Math.Max(0f, Math.Min(1f, cf3e112fd3d3aa49f1b42cc826ed0e916));
		c4dba96e545d7820e037eeacdfa8bba27 = cde8eb1bb0b47a38e12a97f998f0883cc.Start + cf3e112fd3d3aa49f1b42cc826ed0e916 * (cde8eb1bb0b47a38e12a97f998f0883cc.End - cde8eb1bb0b47a38e12a97f998f0883cc.Start);
		return;
		IL_0199:
		if (!(cf3e112fd3d3aa49f1b42cc826ed0e916 < 0f))
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
			if (!(cf3e112fd3d3aa49f1b42cc826ed0e916 > 1f))
			{
				if (!(ccc1f272a7f7272cd2400093d36b02c38 < 0f))
				{
					if (!(ccc1f272a7f7272cd2400093d36b02c38 > 1f))
					{
						c4dba96e545d7820e037eeacdfa8bba27 = cde8eb1bb0b47a38e12a97f998f0883cc.Start + cf3e112fd3d3aa49f1b42cc826ed0e916 * (cde8eb1bb0b47a38e12a97f998f0883cc.End - cde8eb1bb0b47a38e12a97f998f0883cc.Start);
						cb5750b83fa6cdab2db2259fdbe70a659 = c33a3496380455e016a42bec8afc78bc7.Start + ccc1f272a7f7272cd2400093d36b02c38 * (c33a3496380455e016a42bec8afc78bc7.End - c33a3496380455e016a42bec8afc78bc7.Start);
						return;
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
				}
				ccc1f272a7f7272cd2400093d36b02c38 = Math.Max(0f, Math.Min(1f, ccc1f272a7f7272cd2400093d36b02c38));
				cb5750b83fa6cdab2db2259fdbe70a659 = c33a3496380455e016a42bec8afc78bc7.Start + ccc1f272a7f7272cd2400093d36b02c38 * (c33a3496380455e016a42bec8afc78bc7.End - c33a3496380455e016a42bec8afc78bc7.Start);
				cb01280d46caa416165314e91eb3ad328(cde8eb1bb0b47a38e12a97f998f0883cc, cb5750b83fa6cdab2db2259fdbe70a659, out cf3e112fd3d3aa49f1b42cc826ed0e916);
				cf3e112fd3d3aa49f1b42cc826ed0e916 = Math.Max(0f, Math.Min(1f, cf3e112fd3d3aa49f1b42cc826ed0e916));
				c4dba96e545d7820e037eeacdfa8bba27 = cde8eb1bb0b47a38e12a97f998f0883cc.Start + cf3e112fd3d3aa49f1b42cc826ed0e916 * (cde8eb1bb0b47a38e12a97f998f0883cc.End - cde8eb1bb0b47a38e12a97f998f0883cc.Start);
				return;
			}
		}
		cf3e112fd3d3aa49f1b42cc826ed0e916 = Math.Max(0f, Math.Min(1f, cf3e112fd3d3aa49f1b42cc826ed0e916));
		c4dba96e545d7820e037eeacdfa8bba27 = cde8eb1bb0b47a38e12a97f998f0883cc.Start + cf3e112fd3d3aa49f1b42cc826ed0e916 * (cde8eb1bb0b47a38e12a97f998f0883cc.End - cde8eb1bb0b47a38e12a97f998f0883cc.Start);
		cb01280d46caa416165314e91eb3ad328(c33a3496380455e016a42bec8afc78bc7, c4dba96e545d7820e037eeacdfa8bba27, out ccc1f272a7f7272cd2400093d36b02c38);
		ccc1f272a7f7272cd2400093d36b02c38 = Math.Max(0f, Math.Min(1f, ccc1f272a7f7272cd2400093d36b02c38));
		cb5750b83fa6cdab2db2259fdbe70a659 = c33a3496380455e016a42bec8afc78bc7.Start + ccc1f272a7f7272cd2400093d36b02c38 * (c33a3496380455e016a42bec8afc78bc7.End - c33a3496380455e016a42bec8afc78bc7.Start);
	}

	/// <summary>
	///       Determines whether the given axis-aligned bounding box (AABB) and ray overlap.
	///       </summary>
	/// <param name="aabb">The axis-aligned bounding box (AABB).</param>
	/// <param name="ray">The ray.</param>
	/// <returns>
	///   <see langword="true" /> if the AABB and the ray have a contact; otherwise, <see langword="false" />.
	///       </returns>
	public static bool HaveContact(Aabb aabb, Ray ray)
	{
		if (Numeric.IsZero(ray.Length))
		{
			return HaveContact(aabb, ray.Origin);
		}
		return cdd6d17271fec83883494ba8e43eede6a(c3ec982f192efbba86e42a1cea5c18565: new Vector3F(1f / ray.Direction.X, 1f / ray.Direction.Y, 1f / ray.Direction.Z), cdc3439410d15f28489c964ee87b88fe9: aabb, c355544f734bef7558e69db7ef3944f57: ray.Origin, c579aedaeb1b603c5780938faafdebe94: ray.Length, c912da9d2d82e2a6523be60260abdfc11: 0f);
	}

	/// <summary>
	///       Determines whether the given axis-aligned bounding box (AABB) and ray overlap.
	///       </summary>
	/// <param name="aabb">The axis-aligned bounding box (AABB).</param>
	/// <param name="ray">The ray.</param>
	/// <param name="epsilon">
	///       A small epsilon value by which the AABB is extended to avoid missing contacts because of
	///       numerical problems. (Especially in ray vs. triangle mesh tests, we do not want to miss
	///       collisions between triangle.)
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the AABB and the ray have a contact; otherwise, <see langword="false" />.
	///       </returns>
	public static bool HaveContact(Aabb aabb, Ray ray, float epsilon)
	{
		if (Numeric.IsZero(ray.Length))
		{
			return HaveContact(aabb, ray.Origin);
		}
		return cdd6d17271fec83883494ba8e43eede6a(c3ec982f192efbba86e42a1cea5c18565: new Vector3F(1f / ray.Direction.X, 1f / ray.Direction.Y, 1f / ray.Direction.Z), cdc3439410d15f28489c964ee87b88fe9: aabb, c355544f734bef7558e69db7ef3944f57: ray.Origin, c579aedaeb1b603c5780938faafdebe94: ray.Length, c912da9d2d82e2a6523be60260abdfc11: epsilon);
	}

	internal static bool c3e45e70e51dbd3591dfa23857d27cfb5(Aabb cdc3439410d15f28489c964ee87b88fe9, Ray c2d769fcfe2c7e7844e7de692907680c8)
	{
		if (Numeric.IsZero(c2d769fcfe2c7e7844e7de692907680c8.Length))
		{
			return HaveContact(cdc3439410d15f28489c964ee87b88fe9, c2d769fcfe2c7e7844e7de692907680c8.Origin);
		}
		return c3e45e70e51dbd3591dfa23857d27cfb5(c3ec982f192efbba86e42a1cea5c18565: new Vector3F(1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.X, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Y, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Z), cdc3439410d15f28489c964ee87b88fe9: cdc3439410d15f28489c964ee87b88fe9, c355544f734bef7558e69db7ef3944f57: c2d769fcfe2c7e7844e7de692907680c8.Origin, c579aedaeb1b603c5780938faafdebe94: c2d769fcfe2c7e7844e7de692907680c8.Length);
	}

	internal static bool c3e45e70e51dbd3591dfa23857d27cfb5(Aabb cdc3439410d15f28489c964ee87b88fe9, Vector3F c355544f734bef7558e69db7ef3944f57, Vector3F c3ec982f192efbba86e42a1cea5c18565, float c579aedaeb1b603c5780938faafdebe94)
	{
		float num = ((c3ec982f192efbba86e42a1cea5c18565.X > 0f) ? cdc3439410d15f28489c964ee87b88fe9.Minimum.X : cdc3439410d15f28489c964ee87b88fe9.Maximum.X);
		float num2 = (num - c355544f734bef7558e69db7ef3944f57.X) * c3ec982f192efbba86e42a1cea5c18565.X;
		float num3 = ((c3ec982f192efbba86e42a1cea5c18565.X > 0f) ? cdc3439410d15f28489c964ee87b88fe9.Maximum.X : cdc3439410d15f28489c964ee87b88fe9.Minimum.X);
		float num4 = (num3 - c355544f734bef7558e69db7ef3944f57.X) * c3ec982f192efbba86e42a1cea5c18565.X;
		float num5 = num2;
		float num6 = num4;
		float num7 = ((c3ec982f192efbba86e42a1cea5c18565.Y > 0f) ? cdc3439410d15f28489c964ee87b88fe9.Maximum.Y : cdc3439410d15f28489c964ee87b88fe9.Minimum.Y);
		float num8 = (num7 - c355544f734bef7558e69db7ef3944f57.Y) * c3ec982f192efbba86e42a1cea5c18565.Y;
		if (num5 > num8)
		{
			return false;
		}
		if (num8 < num6)
		{
			num6 = num8;
		}
		float num9 = ((c3ec982f192efbba86e42a1cea5c18565.Y > 0f) ? cdc3439410d15f28489c964ee87b88fe9.Minimum.Y : cdc3439410d15f28489c964ee87b88fe9.Maximum.Y);
		float num10 = (num9 - c355544f734bef7558e69db7ef3944f57.Y) * c3ec982f192efbba86e42a1cea5c18565.Y;
		if (num10 > num6)
		{
			return false;
		}
		if (num10 > num5)
		{
			num5 = num10;
		}
		float num11 = ((c3ec982f192efbba86e42a1cea5c18565.Z > 0f) ? cdc3439410d15f28489c964ee87b88fe9.Minimum.Z : cdc3439410d15f28489c964ee87b88fe9.Maximum.Z);
		float num12 = (num11 - c355544f734bef7558e69db7ef3944f57.Z) * c3ec982f192efbba86e42a1cea5c18565.Z;
		if (num12 > num6)
		{
			return false;
		}
		if (num12 > num5)
		{
			num5 = num12;
		}
		float num13 = ((c3ec982f192efbba86e42a1cea5c18565.Z > 0f) ? cdc3439410d15f28489c964ee87b88fe9.Maximum.Z : cdc3439410d15f28489c964ee87b88fe9.Minimum.Z);
		float num14 = (num13 - c355544f734bef7558e69db7ef3944f57.Z) * c3ec982f192efbba86e42a1cea5c18565.Z;
		if (num5 > num14)
		{
			return false;
		}
		if (num14 < num6)
		{
			num6 = num14;
		}
		if (num6 < 0f)
		{
			return false;
		}
		if (num5 > c579aedaeb1b603c5780938faafdebe94)
		{
			return false;
		}
		return true;
	}

	internal static bool cdd6d17271fec83883494ba8e43eede6a(Aabb cdc3439410d15f28489c964ee87b88fe9, Vector3F c355544f734bef7558e69db7ef3944f57, Vector3F c3ec982f192efbba86e42a1cea5c18565, float c579aedaeb1b603c5780938faafdebe94, float c912da9d2d82e2a6523be60260abdfc11)
	{
		float num = 0f;
		float num2 = c579aedaeb1b603c5780938faafdebe94;
		float num3 = ((c3ec982f192efbba86e42a1cea5c18565.X > 0f) ? (cdc3439410d15f28489c964ee87b88fe9.Minimum.X - c912da9d2d82e2a6523be60260abdfc11) : (cdc3439410d15f28489c964ee87b88fe9.Maximum.X + c912da9d2d82e2a6523be60260abdfc11));
		float num4 = (num3 - c355544f734bef7558e69db7ef3944f57.X) * c3ec982f192efbba86e42a1cea5c18565.X;
		if (num4 > num2)
		{
			return false;
		}
		if (num4 > num)
		{
			num = num4;
		}
		float num5 = ((c3ec982f192efbba86e42a1cea5c18565.X > 0f) ? (cdc3439410d15f28489c964ee87b88fe9.Maximum.X + c912da9d2d82e2a6523be60260abdfc11) : (cdc3439410d15f28489c964ee87b88fe9.Minimum.X - c912da9d2d82e2a6523be60260abdfc11));
		float num6 = (num5 - c355544f734bef7558e69db7ef3944f57.X) * c3ec982f192efbba86e42a1cea5c18565.X;
		if (num > num6)
		{
			return false;
		}
		if (num6 < num2)
		{
			num2 = num6;
		}
		float num7 = ((c3ec982f192efbba86e42a1cea5c18565.Y > 0f) ? (cdc3439410d15f28489c964ee87b88fe9.Maximum.Y + c912da9d2d82e2a6523be60260abdfc11) : (cdc3439410d15f28489c964ee87b88fe9.Minimum.Y - c912da9d2d82e2a6523be60260abdfc11));
		float num8 = (num7 - c355544f734bef7558e69db7ef3944f57.Y) * c3ec982f192efbba86e42a1cea5c18565.Y;
		if (num > num8)
		{
			return false;
		}
		if (num8 < num2)
		{
			num2 = num8;
		}
		float num9 = ((c3ec982f192efbba86e42a1cea5c18565.Y > 0f) ? (cdc3439410d15f28489c964ee87b88fe9.Minimum.Y - c912da9d2d82e2a6523be60260abdfc11) : (cdc3439410d15f28489c964ee87b88fe9.Maximum.Y + c912da9d2d82e2a6523be60260abdfc11));
		float num10 = (num9 - c355544f734bef7558e69db7ef3944f57.Y) * c3ec982f192efbba86e42a1cea5c18565.Y;
		if (num10 > num2)
		{
			return false;
		}
		if (num10 > num)
		{
			num = num10;
		}
		float num11 = ((c3ec982f192efbba86e42a1cea5c18565.Z > 0f) ? (cdc3439410d15f28489c964ee87b88fe9.Minimum.Z - c912da9d2d82e2a6523be60260abdfc11) : (cdc3439410d15f28489c964ee87b88fe9.Maximum.Z + c912da9d2d82e2a6523be60260abdfc11));
		float num12 = (num11 - c355544f734bef7558e69db7ef3944f57.Z) * c3ec982f192efbba86e42a1cea5c18565.Z;
		if (num12 > num2)
		{
			return false;
		}
		if (num12 > num)
		{
			num = num12;
		}
		float num13 = ((c3ec982f192efbba86e42a1cea5c18565.Z > 0f) ? (cdc3439410d15f28489c964ee87b88fe9.Maximum.Z + c912da9d2d82e2a6523be60260abdfc11) : (cdc3439410d15f28489c964ee87b88fe9.Minimum.Z - c912da9d2d82e2a6523be60260abdfc11));
		float num14 = (num13 - c355544f734bef7558e69db7ef3944f57.Z) * c3ec982f192efbba86e42a1cea5c18565.Z;
		if (num > num14)
		{
			return false;
		}
		if (num14 < num2)
		{
			num2 = num14;
		}
		if (num2 < 0f)
		{
			return false;
		}
		if (num > c579aedaeb1b603c5780938faafdebe94)
		{
			return false;
		}
		return true;
	}

	/// <summary>
	///       Determines whether a given AABB is hit by a moving AABB.
	///       </summary>
	/// <param name="aabbA">The axis-aligned bounding box.</param>
	/// <param name="aabbB">The moving axis-aligned bounding box.</param>
	/// <param name="movementB">
	///       The movement vector of <paramref name="aabbB" />. <paramref name="aabbB" /> is given at its
	///       start position. The movement vector is added to the start position of 
	///       <paramref name="aabbB" /> to define its end position. 
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the moving AABB (<paramref name="aabbB" />) overlaps the static
	///       AABB (<paramref name="aabbA" />) at any time during its movement.
	///       </returns>
	public static bool HaveContact(Aabb aabbA, Aabb aabbB, Vector3F movementB)
	{
		float length = movementB.Length;
		if (Numeric.IsZero(length))
		{
			return HaveContact(aabbA, aabbB);
		}
		Vector3F direction = movementB / length;
		Aabb aabb = new Aabb(aabbA.Minimum - aabbB.Maximum, aabbA.Maximum - aabbB.Minimum);
		return HaveContact(aabb, new Ray(Vector3F.Zero, direction, length));
	}

	/// <summary>
	///       Computes the minimum box that contains the given points.
	///       </summary>
	/// <param name="points">The points.</param>
	/// <param name="extent">The box extent (the size in x, y and z).</param>
	/// <param name="pose">The pose of the box.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="points" /> is empty.
	///       </exception>
	public static void ComputeBoundingBox(IList<Vector3F> points, out Vector3F extent, out Pose pose)
	{
		if (points == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1362));
		}
		if (points.Count == 0)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(GeometryHelper.ComputeBoundingBox(IList<Vector3F>, ref Vector3F, ref Pose));
				}
				throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1375));
			}
		}
		MatrixF matrixF = null;
		try
		{
			if (points.Count > 4)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					DcelMesh dcelMesh = CreateConvexHull(points);
					TriangleMesh triangleMesh = dcelMesh.ToTriangleMesh();
					if (triangleMesh.Vertices.Count > 0)
					{
						points = triangleMesh.Vertices;
						matrixF = c651fddce38f34109ebed63013d3004a8(triangleMesh);
					}
					break;
				}
			}
		}
		catch
		{
		}
		if (!(matrixF == null))
		{
			if (!float.IsNaN(matrixF.Determinant))
			{
				goto IL_00cc;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		points = points.ToList();
		matrixF = c93ef0140f5cd2041417c036465df0edc(points);
		goto IL_00cc;
		IL_00cc:
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (Numeric.IsZero(matrixF[i, j]))
				{
					matrixF[i, j] = 0f;
				}
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					goto end_IL_0100;
				}
				continue;
				end_IL_0100:
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
			EigenvalueDecompositionF eigenvalueDecompositionF = new EigenvalueDecompositionF(matrixF);
			Matrix33F cc5818c262d54ebfcbc2369f4ba2ae38d = eigenvalueDecompositionF.V.ToMatrix33F();
			cc5818c262d54ebfcbc2369f4ba2ae38d.SetColumn(2, Vector3F.Cross(cc5818c262d54ebfcbc2369f4ba2ae38d.GetColumn(0), cc5818c262d54ebfcbc2369f4ba2ae38d.GetColumn(1)));
			c3a16030d14697aa1582cd4d8f886125c(ref cc5818c262d54ebfcbc2369f4ba2ae38d);
			float num = c58273698d42358f51ddc074475e0af9f(points, cc5818c262d54ebfcbc2369f4ba2ae38d, float.PositiveInfinity, out extent, out var c4dee60a92bfb6bb6930e77c4fe8caa0a);
			Vector3F vector3F = Vector3F.Zero;
			float num2 = MathHelper.ToRadians(45f);
			float num3 = MathHelper.ToRadians(1f);
			while (num2 >= num3)
			{
				bool flag = false;
				float num4 = num2 / 7f;
				Vector3F vector3F2 = vector3F;
				for (float num5 = vector3F2.X - num2; num5 <= vector3F2.X + num2; num5 += num4)
				{
					for (float num6 = vector3F2.Y - num2; num6 <= vector3F2.Y + num2; num6 += num4)
					{
						for (float num7 = vector3F2.Z - num2; num7 <= vector3F2.Z + num2; num7 += num4)
						{
							Matrix33F matrix33F = QuaternionF.CreateRotation(num5, Vector3F.UnitX, num6, Vector3F.UnitY, num7, Vector3F.UnitZ, useGlobalAxes: true).ToRotationMatrix33();
							float num8 = c58273698d42358f51ddc074475e0af9f(points, matrix33F, num, out var c2cc6f89647063c6af8b1d8c8a61810e, out var c4dee60a92bfb6bb6930e77c4fe8caa0a2);
							if (!(num8 < num))
							{
								continue;
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
							flag = true;
							c4dee60a92bfb6bb6930e77c4fe8caa0a = c4dee60a92bfb6bb6930e77c4fe8caa0a2;
							extent = c2cc6f89647063c6af8b1d8c8a61810e;
							cc5818c262d54ebfcbc2369f4ba2ae38d = matrix33F;
							num = num8;
							vector3F = new Vector3F(num5, num6, num7);
						}
					}
					while (true)
					{
						switch (5)
						{
						case 0:
							break;
						default:
							goto end_IL_0271;
						}
						continue;
						end_IL_0271:
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
					break;
				}
				if (flag)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
					num2 *= 0.5f;
				}
				else
				{
					num2 = 0f;
				}
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				pose = new Pose(c4dee60a92bfb6bb6930e77c4fe8caa0a, cc5818c262d54ebfcbc2369f4ba2ae38d);
				return;
			}
		}
	}

	private static MatrixF c93ef0140f5cd2041417c036465df0edc(IList<Vector3F> c2ebf3a3e77e8b4507a45357157f26fae)
	{
		int count = c2ebf3a3e77e8b4507a45357157f26fae.Count;
		List<VectorF> list = new List<VectorF>(count);
		for (int i = 0; i < count; i++)
		{
			list.Add(c2ebf3a3e77e8b4507a45357157f26fae[i].ToVectorF());
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(GeometryHelper.c93ef0140f5cd2041417c036465df0edc(IList<Vector3F>));
			}
			return StatisticsHelper.ComputeCovarianceMatrix(list);
		}
	}

	private static MatrixF c651fddce38f34109ebed63013d3004a8(ITriangleMesh cf1674ba51e57b7e621e83565f0591384)
	{
		MatrixF matrixF = new MatrixF(3, 3);
		float num = 0f;
		Vector3F zero = Vector3F.Zero;
		for (int i = 0; i < cf1674ba51e57b7e621e83565f0591384.NumberOfTriangles; i++)
		{
			Triangle triangle = cf1674ba51e57b7e621e83565f0591384.GetTriangle(i);
			Vector3F vertex = triangle.Vertex0;
			Vector3F vertex2 = triangle.Vertex1;
			Vector3F vertex3 = triangle.Vertex2;
			Vector3F vector3F = 1f / 3f * (vertex + vertex2 + vertex3);
			Vector3F vector = vertex2 - vertex;
			Vector3F vector2 = vertex3 - vertex;
			float num2 = 0.5f * Vector3F.Cross(vector, vector2).Length;
			num += num2;
			zero += num2 * vector3F;
			int num3 = 0;
			while (num3 < 3)
			{
				for (int j = 0; j < 3; j++)
				{
					matrixF[num3, j] += num2 / 12f * (9f * vector3F[num3] * vector3F[j] + vertex[num3] * vertex[j] + vertex2[num3] * vertex2[j] + vertex3[num3] * vertex3[j]);
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(GeometryHelper.c651fddce38f34109ebed63013d3004a8(ITriangleMesh));
					}
					num3++;
					break;
				}
			}
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			zero /= num;
			for (int k = 0; k < 3; k++)
			{
				for (int l = 0; l < 3; l++)
				{
					matrixF[k, l] = 1f / num * matrixF[k, l] - zero[k] * zero[l];
				}
			}
			return matrixF;
		}
	}

	private static float c58273698d42358f51ddc074475e0af9f(IList<Vector3F> c2ebf3a3e77e8b4507a45357157f26fae, Matrix33F c06fe4c4e044ce8d5c4bed6ef4c41b63b, float c3f5bc08c33e1ed23bd65195f3b635de7, out Vector3F c2cc6f89647063c6af8b1d8c8a61810e7, out Vector3F c4dee60a92bfb6bb6930e77c4fe8caa0a)
	{
		c2cc6f89647063c6af8b1d8c8a61810e7 = new Vector3F(float.PositiveInfinity);
		c4dee60a92bfb6bb6930e77c4fe8caa0a = Vector3F.Zero;
		float num = float.PositiveInfinity;
		Matrix33F transposed = c06fe4c4e044ce8d5c4bed6ef4c41b63b.Transposed;
		Vector3F vector3F = new Vector3F(float.PositiveInfinity);
		Vector3F vector3F2 = new Vector3F(float.NegativeInfinity);
		Vector3F item = Vector3F.Zero;
		Vector3F item2 = Vector3F.Zero;
		Vector3F item3 = Vector3F.Zero;
		Vector3F item4 = Vector3F.Zero;
		Vector3F item5 = Vector3F.Zero;
		Vector3F item6 = Vector3F.Zero;
		for (int i = 0; i < c2ebf3a3e77e8b4507a45357157f26fae.Count; i++)
		{
			Vector3F vector3F3 = c2ebf3a3e77e8b4507a45357157f26fae[i];
			Vector3F vector3F4 = transposed * vector3F3;
			bool flag = false;
			if (vector3F4.X < vector3F.X)
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
					__ldtoken(GeometryHelper.c58273698d42358f51ddc074475e0af9f(IList<Vector3F>, Matrix33F, float, ref Vector3F, ref Vector3F));
				}
				vector3F.X = vector3F4.X;
				item = vector3F3;
				flag = true;
			}
			if (vector3F4.X > vector3F2.X)
			{
				vector3F2.X = vector3F4.X;
				item2 = vector3F3;
				flag = true;
			}
			if (vector3F4.Y < vector3F.Y)
			{
				vector3F.Y = vector3F4.Y;
				item3 = vector3F3;
				flag = true;
			}
			if (vector3F4.Y > vector3F2.Y)
			{
				vector3F2.Y = vector3F4.Y;
				item4 = vector3F3;
				flag = true;
			}
			if (vector3F4.Z < vector3F.Z)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				vector3F.Z = vector3F4.Z;
				item5 = vector3F3;
				flag = true;
			}
			if (vector3F4.Z > vector3F2.Z)
			{
				vector3F2.Z = vector3F4.Z;
				item6 = vector3F3;
				flag = true;
			}
			if (flag)
			{
				c2cc6f89647063c6af8b1d8c8a61810e7 = vector3F2 - vector3F;
				num = c2cc6f89647063c6af8b1d8c8a61810e7.X * c2cc6f89647063c6af8b1d8c8a61810e7.Y * c2cc6f89647063c6af8b1d8c8a61810e7.Z;
				if (num > c3f5bc08c33e1ed23bd65195f3b635de7)
				{
					return float.PositiveInfinity;
				}
			}
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			c2ebf3a3e77e8b4507a45357157f26fae.Remove(item);
			c2ebf3a3e77e8b4507a45357157f26fae.Insert(0, item);
			c2ebf3a3e77e8b4507a45357157f26fae.Remove(item3);
			c2ebf3a3e77e8b4507a45357157f26fae.Insert(0, item3);
			c2ebf3a3e77e8b4507a45357157f26fae.Remove(item5);
			c2ebf3a3e77e8b4507a45357157f26fae.Insert(0, item5);
			c2ebf3a3e77e8b4507a45357157f26fae.Remove(item2);
			c2ebf3a3e77e8b4507a45357157f26fae.Insert(0, item2);
			c2ebf3a3e77e8b4507a45357157f26fae.Remove(item4);
			c2ebf3a3e77e8b4507a45357157f26fae.Insert(0, item4);
			c2ebf3a3e77e8b4507a45357157f26fae.Remove(item6);
			c2ebf3a3e77e8b4507a45357157f26fae.Insert(0, item6);
			Vector3F vector3F5 = (vector3F + vector3F2) / 2f;
			c4dee60a92bfb6bb6930e77c4fe8caa0a = c06fe4c4e044ce8d5c4bed6ef4c41b63b * vector3F5;
			return num;
		}
	}

	private static void c3a16030d14697aa1582cd4d8f886125c(ref Matrix33F cc5818c262d54ebfcbc2369f4ba2ae38d)
	{
		if (!Numeric.AreEqual(1f, Math.Abs(cc5818c262d54ebfcbc2369f4ba2ae38d.M00) + Math.Abs(cc5818c262d54ebfcbc2369f4ba2ae38d.M10) + Math.Abs(cc5818c262d54ebfcbc2369f4ba2ae38d.M20)) || !Numeric.AreEqual(1f, Math.Abs(cc5818c262d54ebfcbc2369f4ba2ae38d.M01) + Math.Abs(cc5818c262d54ebfcbc2369f4ba2ae38d.M11) + Math.Abs(cc5818c262d54ebfcbc2369f4ba2ae38d.M21)))
		{
			return;
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(GeometryHelper.c3a16030d14697aa1582cd4d8f886125c(ref Matrix33F));
			}
			cc5818c262d54ebfcbc2369f4ba2ae38d = Matrix33F.Identity;
			return;
		}
	}

	/// <summary>
	///       Gets the point on or in an axis-aligned bounding box (AABB) that is closest to a given 
	///       point.
	///       </summary>
	/// <param name="aabb">The AABB.</param>
	/// <param name="point">The point.</param>
	/// <param name="pointOnAabb">
	///       The point on or in <paramref name="aabb" /> that is closest to <paramref name="point" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="aabb" /> and <paramref name="point" /> have 
	///       contact (<paramref name="pointOnAabb" /> is identical to <paramref name="point" />); 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool GetClosestPoint(Aabb aabb, Vector3F point, out Vector3F pointOnAabb)
	{
		bool result = true;
		pointOnAabb = point;
		if (pointOnAabb.X < aabb.Minimum.X)
		{
			pointOnAabb.X = aabb.Minimum.X;
			result = false;
		}
		else if (pointOnAabb.X > aabb.Maximum.X)
		{
			pointOnAabb.X = aabb.Maximum.X;
			result = false;
		}
		if (pointOnAabb.Y < aabb.Minimum.Y)
		{
			pointOnAabb.Y = aabb.Minimum.Y;
			result = false;
		}
		else if (pointOnAabb.Y > aabb.Maximum.Y)
		{
			pointOnAabb.Y = aabb.Maximum.Y;
			result = false;
		}
		if (pointOnAabb.Z < aabb.Minimum.Z)
		{
			pointOnAabb.Z = aabb.Minimum.Z;
			result = false;
		}
		else if (pointOnAabb.Z > aabb.Maximum.Z)
		{
			pointOnAabb.Z = aabb.Maximum.Z;
			result = false;
		}
		return result;
	}

	/// <summary>
	///       Computes the distance between the two axis-aligned bounding boxes (AABBs).
	///       </summary>
	/// <param name="aabbA">The first AABB.</param>
	/// <param name="aabbB">The second AABB.</param>
	/// <returns>
	///       The distance between the two AABBs. 0 if the AABB are touching or intersecting.
	///       </returns>
	public static float GetDistance(Aabb aabbA, Aabb aabbB)
	{
		return (float)Math.Sqrt(c6d56a7842537f534a2043d488cf4a1e6(aabbA, aabbB));
	}

	internal static float c6d56a7842537f534a2043d488cf4a1e6(Aabb c91209629b1ab987a7f82843e5ffe93e9, Aabb c3da841ca5cff2d4f5ea82b80cad9c459)
	{
		float num = 0f;
		if (c91209629b1ab987a7f82843e5ffe93e9.Minimum.X > c3da841ca5cff2d4f5ea82b80cad9c459.Maximum.X)
		{
			float num2 = c91209629b1ab987a7f82843e5ffe93e9.Minimum.X - c3da841ca5cff2d4f5ea82b80cad9c459.Maximum.X;
			num += num2 * num2;
		}
		else if (c3da841ca5cff2d4f5ea82b80cad9c459.Minimum.X > c91209629b1ab987a7f82843e5ffe93e9.Maximum.X)
		{
			float num3 = c3da841ca5cff2d4f5ea82b80cad9c459.Minimum.X - c91209629b1ab987a7f82843e5ffe93e9.Maximum.X;
			num += num3 * num3;
		}
		if (c91209629b1ab987a7f82843e5ffe93e9.Minimum.Y > c3da841ca5cff2d4f5ea82b80cad9c459.Maximum.Y)
		{
			float num4 = c91209629b1ab987a7f82843e5ffe93e9.Minimum.Y - c3da841ca5cff2d4f5ea82b80cad9c459.Maximum.Y;
			num += num4 * num4;
		}
		else if (c3da841ca5cff2d4f5ea82b80cad9c459.Minimum.Y > c91209629b1ab987a7f82843e5ffe93e9.Maximum.Y)
		{
			float num5 = c3da841ca5cff2d4f5ea82b80cad9c459.Minimum.Y - c91209629b1ab987a7f82843e5ffe93e9.Maximum.Y;
			num += num5 * num5;
		}
		if (c91209629b1ab987a7f82843e5ffe93e9.Minimum.Z > c3da841ca5cff2d4f5ea82b80cad9c459.Maximum.Z)
		{
			float num6 = c91209629b1ab987a7f82843e5ffe93e9.Minimum.Z - c3da841ca5cff2d4f5ea82b80cad9c459.Maximum.Z;
			num += num6 * num6;
		}
		else if (c3da841ca5cff2d4f5ea82b80cad9c459.Minimum.Z > c91209629b1ab987a7f82843e5ffe93e9.Maximum.Z)
		{
			float num7 = c3da841ca5cff2d4f5ea82b80cad9c459.Minimum.Z - c91209629b1ab987a7f82843e5ffe93e9.Maximum.Z;
			num += num7 * num7;
		}
		return num;
	}

	internal static float c863d4de1e2a6bbf028c3b0f9ed12c93f(Vector3F c0fcf86c595d8097677cd35a6b80b3734, Pose c2cca220b1cf789fcaae8dd1859badc54, Vector3F c72c06190980af3f7a6a02e400d8a9213, Pose c689a79a5792495f42cbe3effa012780c)
	{
		Vector3F vector3F = c689a79a5792495f42cbe3effa012780c.Position - c2cca220b1cf789fcaae8dd1859badc54.Position;
		float lengthSquared = vector3F.LengthSquared;
		if (Numeric.IsZero(lengthSquared))
		{
			return 0f;
		}
		Vector3F vector3F2 = c689a79a5792495f42cbe3effa012780c.ToWorldPosition(c2d1ba218b02e19db2db555692d88f226(c72c06190980af3f7a6a02e400d8a9213, c689a79a5792495f42cbe3effa012780c.ToLocalDirection(-vector3F)));
		Vector3F vector3F3 = c2cca220b1cf789fcaae8dd1859badc54.ToWorldPosition(c2d1ba218b02e19db2db555692d88f226(c0fcf86c595d8097677cd35a6b80b3734, c2cca220b1cf789fcaae8dd1859badc54.ToLocalDirection(vector3F)));
		Vector3F vector = vector3F2 - vector3F3;
		float num = Vector3F.Dot(vector3F, vector);
		if (num <= 0f)
		{
			return 0f;
		}
		return num * num / lengthSquared;
	}

	private static Vector3F c2d1ba218b02e19db2db555692d88f226(Vector3F c2cc6f89647063c6af8b1d8c8a61810e7, Vector3F c75c28b357dedcad9a44abd406f2361f3)
	{
		return new Vector3F
		{
			X = ((c75c28b357dedcad9a44abd406f2361f3.X >= 0f) ? (c2cc6f89647063c6af8b1d8c8a61810e7.X / 2f) : ((0f - c2cc6f89647063c6af8b1d8c8a61810e7.X) / 2f)),
			Y = ((c75c28b357dedcad9a44abd406f2361f3.Y >= 0f) ? (c2cc6f89647063c6af8b1d8c8a61810e7.Y / 2f) : ((0f - c2cc6f89647063c6af8b1d8c8a61810e7.Y) / 2f)),
			Z = ((c75c28b357dedcad9a44abd406f2361f3.Z >= 0f) ? (c2cc6f89647063c6af8b1d8c8a61810e7.Z / 2f) : ((0f - c2cc6f89647063c6af8b1d8c8a61810e7.Z) / 2f))
		};
	}

	internal static uint c028a07785704edad752d31ddd84e5429(Vector3F c2cc6f89647063c6af8b1d8c8a61810e7, Vector3F cfef46e983928cc4e6880c424d1468b93)
	{
		float num = 0.5f * c2cc6f89647063c6af8b1d8c8a61810e7.X;
		float num2 = 0.5f * c2cc6f89647063c6af8b1d8c8a61810e7.Y;
		float num3 = 0.5f * c2cc6f89647063c6af8b1d8c8a61810e7.Z;
		uint num4 = 0u;
		if (cfef46e983928cc4e6880c424d1468b93.X < 0f - num)
		{
			num4 |= 1;
		}
		else if (cfef46e983928cc4e6880c424d1468b93.X > num)
		{
			num4 |= 2;
		}
		if (cfef46e983928cc4e6880c424d1468b93.Y < 0f - num2)
		{
			num4 |= 4;
		}
		else if (cfef46e983928cc4e6880c424d1468b93.Y > num2)
		{
			num4 |= 8;
		}
		if (cfef46e983928cc4e6880c424d1468b93.Z < 0f - num3)
		{
			num4 |= 0x10;
		}
		else if (cfef46e983928cc4e6880c424d1468b93.Z > num3)
		{
			num4 |= 0x20;
		}
		return num4;
	}

	/// <summary>
	///       Determines whether two axis-aligned bounding boxes (AABBs) overlap.
	///       </summary>
	/// <param name="aabbA">The first axis-aligned bounding box (AABB).</param>
	/// <param name="aabbB">The second axis-aligned bounding box (AABB).</param>
	/// <returns>
	///   <see langword="true" /> if the AABBs overlap; otherwise <see langword="false" />.
	///       </returns>
	public static bool HaveContact(Aabb aabbA, Aabb aabbB)
	{
		if (aabbA.Minimum.X <= aabbB.Maximum.X && aabbA.Maximum.X >= aabbB.Minimum.X && aabbA.Minimum.Y <= aabbB.Maximum.Y && aabbA.Maximum.Y >= aabbB.Minimum.Y && aabbA.Minimum.Z <= aabbB.Maximum.Z)
		{
			return aabbA.Maximum.Z >= aabbB.Minimum.Z;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the axis-aligned bounding box (AABB) contains or touches the given point.
	///       </summary>
	/// <param name="aabb">The axis-aligned bounding box (AABB).</param>
	/// <param name="point">The point.</param>
	/// <returns>
	///   <see langword="true" /> if AABB and the point have contact; otherwise 
	///       <see langword="false" />.
	///       </returns>
	public static bool HaveContact(Aabb aabb, Vector3F point)
	{
		if (aabb.Minimum.X <= point.X && aabb.Maximum.X >= point.X && aabb.Minimum.Y <= point.Y && aabb.Maximum.Y >= point.Y && aabb.Minimum.Z <= point.Z)
		{
			return aabb.Maximum.Z >= point.Z;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the axis-aligned bounding box (AABB) and a box have contact.
	///       </summary>
	/// <param name="aabb">The axis-aligned bounding box (AABB).</param>
	/// <param name="boxExtent">The box extent (the widths in x, y, and z).</param>
	/// <param name="boxPose">The pose of the box in the space of the AABB.</param>
	/// <param name="makeEdgeTests">
	///       If set to <see langword="true" /> the 9 edge-edge tests of the separating-axis-test (SAT) are 
	///       performed; otherwise, the edge-edge tests are left out and the returned value is 
	///       conservative, which means that a contact can be reported even if there is no contact.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the AABB and the box are touching or penetrating; otherwise, 
	///       <see langword="false" /> if the object are separated.
	///       </returns>
	public static bool HaveContact(Aabb aabb, Vector3F boxExtent, Pose boxPose, bool makeEdgeTests)
	{
		Vector3F vector3F = boxPose.Position - aabb.Center;
		Matrix33F orientation = boxPose.Orientation;
		Matrix33F matrix33F = Matrix33F.Absolute(orientation);
		Vector3F vector3F2 = 0.5f * aabb.Extent;
		Vector3F vector3F3 = 0.5f * boxExtent;
		float num = Math.Abs(vector3F.X) - (vector3F2.X + vector3F3.X * matrix33F.M00 + vector3F3.Y * matrix33F.M01 + vector3F3.Z * matrix33F.M02);
		if (num > 0f)
		{
			return false;
		}
		num = Math.Abs(vector3F.Y) - (vector3F2.Y + vector3F3.X * matrix33F.M10 + vector3F3.Y * matrix33F.M11 + vector3F3.Z * matrix33F.M12);
		if (num > 0f)
		{
			return false;
		}
		num = Math.Abs(vector3F.Z) - (vector3F2.Z + vector3F3.X * matrix33F.M20 + vector3F3.Y * matrix33F.M21 + vector3F3.Z * matrix33F.M22);
		if (num > 0f)
		{
			return false;
		}
		float value = vector3F.X * orientation.M00 + vector3F.Y * orientation.M10 + vector3F.Z * orientation.M20;
		num = Math.Abs(value) - (vector3F3.X + vector3F2.X * matrix33F.M00 + vector3F2.Y * matrix33F.M10 + vector3F2.Z * matrix33F.M20);
		if (num > 0f)
		{
			return false;
		}
		value = vector3F.X * orientation.M01 + vector3F.Y * orientation.M11 + vector3F.Z * orientation.M21;
		num = Math.Abs(value) - (vector3F3.Y + vector3F2.X * matrix33F.M01 + vector3F2.Y * matrix33F.M11 + vector3F2.Z * matrix33F.M21);
		if (num > 0f)
		{
			return false;
		}
		value = vector3F.X * orientation.M02 + vector3F.Y * orientation.M12 + vector3F.Z * orientation.M22;
		num = Math.Abs(value) - (vector3F3.Z + vector3F2.X * matrix33F.M02 + vector3F2.Y * matrix33F.M12 + vector3F2.Z * matrix33F.M22);
		if (num > 0f)
		{
			return false;
		}
		if (!makeEdgeTests)
		{
			return true;
		}
		value = vector3F.Z * orientation.M10 - vector3F.Y * orientation.M20;
		num = Math.Abs(value) - (vector3F2.Y * matrix33F.M20 + vector3F2.Z * matrix33F.M10 + vector3F3.Y * matrix33F.M02 + vector3F3.Z * matrix33F.M01);
		if (num > 0f)
		{
			return false;
		}
		value = vector3F.Z * orientation.M11 - vector3F.Y * orientation.M21;
		num = Math.Abs(value) - (vector3F2.Y * matrix33F.M21 + vector3F2.Z * matrix33F.M11 + vector3F3.X * matrix33F.M02 + vector3F3.Z * matrix33F.M00);
		if (num > 0f)
		{
			return false;
		}
		value = vector3F.Z * orientation.M12 - vector3F.Y * orientation.M22;
		num = Math.Abs(value) - (vector3F2.Y * matrix33F.M22 + vector3F2.Z * matrix33F.M12 + vector3F3.X * matrix33F.M01 + vector3F3.Y * matrix33F.M00);
		if (num > 0f)
		{
			return false;
		}
		value = vector3F.X * orientation.M20 - vector3F.Z * orientation.M00;
		num = Math.Abs(value) - (vector3F2.X * matrix33F.M20 + vector3F2.Z * matrix33F.M00 + vector3F3.Y * matrix33F.M12 + vector3F3.Z * matrix33F.M11);
		if (num > 0f)
		{
			return false;
		}
		value = vector3F.X * orientation.M21 - vector3F.Z * orientation.M01;
		num = Math.Abs(value) - (vector3F2.X * matrix33F.M21 + vector3F2.Z * matrix33F.M01 + vector3F3.X * matrix33F.M12 + vector3F3.Z * matrix33F.M10);
		if (num > 0f)
		{
			return false;
		}
		value = vector3F.X * orientation.M22 - vector3F.Z * orientation.M02;
		num = Math.Abs(value) - (vector3F2.X * matrix33F.M22 + vector3F2.Z * matrix33F.M02 + vector3F3.X * matrix33F.M11 + vector3F3.Y * matrix33F.M10);
		if (num > 0f)
		{
			return false;
		}
		value = vector3F.Y * orientation.M00 - vector3F.X * orientation.M10;
		num = Math.Abs(value) - (vector3F2.X * matrix33F.M10 + vector3F2.Y * matrix33F.M00 + vector3F3.Y * matrix33F.M22 + vector3F3.Z * matrix33F.M21);
		if (num > 0f)
		{
			return false;
		}
		value = vector3F.Y * orientation.M01 - vector3F.X * orientation.M11;
		num = Math.Abs(value) - (vector3F2.X * matrix33F.M11 + vector3F2.Y * matrix33F.M01 + vector3F3.X * matrix33F.M22 + vector3F3.Z * matrix33F.M20);
		if (num > 0f)
		{
			return false;
		}
		value = vector3F.Y * orientation.M02 - vector3F.X * orientation.M12;
		num = Math.Abs(value) - (vector3F2.X * matrix33F.M12 + vector3F2.Y * matrix33F.M02 + vector3F3.X * matrix33F.M21 + vector3F3.Y * matrix33F.M20);
		return num <= 0f;
	}

	/// <summary>
	///       Determines whether the specified point is inside the box.
	///       </summary>
	/// <param name="boxExtent">The box extent (the widths in x, y, and z).</param>
	/// <param name="point">The point (in the local space of the box).</param>
	/// <returns>
	///   <see langword="true" /> if the specified point is inside; otherwise, <see langword="false" />.
	///       </returns>
	public static bool HaveContact(Vector3F boxExtent, Vector3F point)
	{
		Vector3F vector = 0.5f * boxExtent;
		vector += Vector3F.Max(vector, new Vector3F(1f)) * Numeric.EpsilonF;
		if (point.X >= 0f - vector.X && point.X <= vector.X && point.Y >= 0f - vector.Y && point.Y <= vector.Y && point.Z >= 0f - vector.Z)
		{
			return point.Z <= vector.Z;
		}
		return false;
	}

	/// <summary>
	///       Determines whether two points P and Q are on opposite sides of a plane. The plane is
	///       determined by three points (A, B, C).
	///       </summary>
	/// <param name="pointP">The point P.</param>
	/// <param name="pointQ">The point Q.</param>
	/// <param name="planePointA">The plane point A.</param>
	/// <param name="planePointB">The plane point B.</param>
	/// <param name="planePointC">The plane point C.</param>
	/// <returns>
	///   <see langword="true" /> if P and Q are on opposite sides; <see langword="false" /> if they are
	///       on the same side; and <see langword="null" /> if the point is on the plane (within a 
	///       numerical tolerance) or if the case is degenerate (A, B, C does not form a valid triangle).
	///       </returns>
	public static bool? ArePointsOnOppositeSides(Vector3F pointP, Vector3F pointQ, Vector3F planePointA, Vector3F planePointB, Vector3F planePointC)
	{
		Vector3F vector = Vector3F.Cross(planePointB - planePointA, planePointC - planePointA);
		float num = Vector3F.Dot(pointP - planePointA, vector);
		float num2 = Vector3F.Dot(pointQ - planePointA, vector);
		if (Numeric.IsZero(num * num2, Numeric.EpsilonFSquared))
		{
			return null;
		}
		return num * num2 < 0f;
	}

	/// <summary>
	///       Gets the point on a plane surface that is closest to a given point.
	///       </summary>
	/// <param name="point">The point position.</param>
	/// <param name="plane">The plane.</param>
	/// <param name="pointOnPlane">
	///       The point on the surface of the plane that is closest to <paramref name="point" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the point lies in the plane (<paramref name="pointOnPlane" /> and 
	///       <paramref name="point" /> are numerically identical); otherwise <see langword="false" /> if 
	///       the point is either above or below the plane.
	///       </returns>
	public static bool GetClosestPoint(Plane plane, Vector3F point, out Vector3F pointOnPlane)
	{
		float distance = GetDistance(plane, point);
		pointOnPlane = point - plane.Normal * distance;
		return Numeric.IsZero(distance);
	}

	internal static Vector3F cc6201df16c7dfb2e7691813ff6871486(Plane cfcd6f09e8ce1bd02ff688b385aa27903, Vector3F cfef46e983928cc4e6880c424d1468b93)
	{
		float distance = GetDistance(cfcd6f09e8ce1bd02ff688b385aa27903, cfef46e983928cc4e6880c424d1468b93);
		return cfef46e983928cc4e6880c424d1468b93 - cfcd6f09e8ce1bd02ff688b385aa27903.Normal * distance;
	}

	/// <summary>
	///       Gets the closest points of a line and a plane.
	///       </summary>
	/// <param name="plane">The plane.</param>
	/// <param name="line">The line.</param>
	/// <param name="pointOnPlane">
	///       The point on <paramref name="plane" /> that is closest to <paramref name="line" />.
	///       </param>
	/// <param name="pointOnLine">
	///       The point on <paramref name="line" /> that is closest to <paramref name="plane" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the line and the plane are touching 
	///       (<paramref name="pointOnLine" /> and <paramref name="pointOnPlane" /> are identical); 
	///       otherwise <see langword="false" /></returns>
	public static bool GetClosestPoints(Plane plane, Line line, out Vector3F pointOnPlane, out Vector3F pointOnLine)
	{
		float num = Vector3F.Dot(line.Direction, plane.Normal);
		if (Numeric.IsZero(num))
		{
			pointOnLine = line.PointOnLine;
			pointOnPlane = cc6201df16c7dfb2e7691813ff6871486(plane, pointOnLine);
			return false;
		}
		float num2 = (plane.DistanceFromOrigin - Vector3F.Dot(line.PointOnLine, plane.Normal)) / num;
		pointOnLine = (pointOnPlane = line.PointOnLine + num2 * line.Direction);
		return true;
	}

	/// <summary>
	///       Gets the closest points of a line and a plane.
	///       </summary>
	/// <param name="plane">The plane.</param>
	/// <param name="lineSegment">The line segment.</param>
	/// <param name="pointOnPlane">
	///       The point on <paramref name="plane" /> that is closest to <paramref name="lineSegment" />.
	///       </param>
	/// <param name="pointOnLineSegment">
	///       The point on <paramref name="lineSegment" /> that is closest to <paramref name="plane" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the line segment and the plane are touching 
	///       (<paramref name="pointOnLineSegment" /> and <paramref name="pointOnPlane" /> are identical);
	///       otherwise <see langword="false" /></returns>
	public static bool GetClosestPoints(Plane plane, LineSegment lineSegment, out Vector3F pointOnPlane, out Vector3F pointOnLineSegment)
	{
		Vector3F vector3F = lineSegment.End - lineSegment.Start;
		float num = Vector3F.Dot(vector3F, plane.Normal);
		if (Numeric.IsZero(num))
		{
			pointOnLineSegment = lineSegment.Start;
			pointOnPlane = cc6201df16c7dfb2e7691813ff6871486(plane, pointOnLineSegment);
			return false;
		}
		float num2 = (plane.DistanceFromOrigin - Vector3F.Dot(lineSegment.Start, plane.Normal)) / num;
		if (num2 < 0f)
		{
			pointOnLineSegment = lineSegment.Start;
			pointOnPlane = cc6201df16c7dfb2e7691813ff6871486(plane, pointOnLineSegment);
			return false;
		}
		if (num2 > 1f)
		{
			pointOnLineSegment = lineSegment.End;
			pointOnPlane = cc6201df16c7dfb2e7691813ff6871486(plane, pointOnLineSegment);
			return false;
		}
		pointOnLineSegment = (pointOnPlane = lineSegment.Start + num2 * vector3F);
		return true;
	}

	/// <summary>
	///       Gets the distance of a point to a plane surface.
	///       </summary>
	/// <param name="point">The point.</param>
	/// <param name="plane">The plane.</param>
	/// <returns>
	///       The distance; this value is positive if the point is in the positive half-space 
	///       (separation), otherwise negative (penetration).
	///       </returns>
	public static float GetDistance(Plane plane, Vector3F point)
	{
		float num = Vector3F.Dot(point, plane.Normal);
		return num - plane.DistanceFromOrigin;
	}

	/// <summary>
	///       Gets the intersection point of three planes.
	///       </summary>
	/// <param name="planeA">The first plane.</param>
	/// <param name="planeB">The second plane.</param>
	/// <param name="planeC">The third plane.</param>
	/// <returns>
	///       The point that touches all three planes. (<see cref="F:System.Single.NaN" />, <see cref="F:System.Single.NaN" />, 
	///       <see cref="F:System.Single.NaN" />) is returned if there is no unique intersection point, for example,
	///       when two planes are parallel or the planes intersect in a line.
	///       </returns>
	public static Vector3F GetIntersection(Plane planeA, Plane planeB, Plane planeC)
	{
		Matrix33F matrix33F = new Matrix33F(planeA.Normal.X, planeA.Normal.Y, planeA.Normal.Z, planeB.Normal.X, planeB.Normal.Y, planeB.Normal.Z, planeC.Normal.X, planeC.Normal.Y, planeC.Normal.Z);
		Vector3F vector3F = new Vector3F(planeA.DistanceFromOrigin, planeB.DistanceFromOrigin, planeC.DistanceFromOrigin);
		if (matrix33F.TryInvert())
		{
			return matrix33F * vector3F;
		}
		return new Vector3F(float.NaN);
	}

	/// <summary>
	///       Computes the minimum sphere that contains the given points.
	///       </summary>
	/// <param name="points">The points.</param>
	/// <param name="radius">The radius of the sphere.</param>
	/// <param name="center">The center of the sphere.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="points" /> is empty.
	///       </exception>
	public static void ComputeBoundingSphere(IEnumerable<Vector3F> points, out float radius, out Vector3F center)
	{
		if (points == null)
		{
			throw new ArgumentNullException();
		}
		List<Vector3F> list = points.ToList();
		if (list.Count == 0)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1375));
		}
		c24c70f2ef1df09ff1a297bf6612c6b24(list, 0, list.Count - 1, 0, out radius, out center);
	}

	private static void c24c70f2ef1df09ff1a297bf6612c6b24(List<Vector3F> c2ebf3a3e77e8b4507a45357157f26fae, int c10fe9261f0510e89165a519a53995cab, int cd6bf80fbab17a5008e6587c3dc036124, int ce5305dd642033151733ae3a96448e3ce, out float c62a8ac8a1ea6107858bdc026334c8108, out Vector3F cf94628f848fdb1b294ef4b3736d0f3bc)
	{
		cf94628f848fdb1b294ef4b3736d0f3bc = new Vector3F(float.NaN);
		c62a8ac8a1ea6107858bdc026334c8108 = float.NaN;
		switch (ce5305dd642033151733ae3a96448e3ce)
		{
		case 1:
			cf94628f848fdb1b294ef4b3736d0f3bc = c2ebf3a3e77e8b4507a45357157f26fae[c10fe9261f0510e89165a519a53995cab - 1];
			c62a8ac8a1ea6107858bdc026334c8108 = 0f;
			break;
		case 2:
			ComputeCircumscribedSphere(c2ebf3a3e77e8b4507a45357157f26fae[c10fe9261f0510e89165a519a53995cab - 1], c2ebf3a3e77e8b4507a45357157f26fae[c10fe9261f0510e89165a519a53995cab - 2], out c62a8ac8a1ea6107858bdc026334c8108, out cf94628f848fdb1b294ef4b3736d0f3bc);
			break;
		case 3:
			ComputeCircumscribedSphere(c2ebf3a3e77e8b4507a45357157f26fae[c10fe9261f0510e89165a519a53995cab - 1], c2ebf3a3e77e8b4507a45357157f26fae[c10fe9261f0510e89165a519a53995cab - 2], c2ebf3a3e77e8b4507a45357157f26fae[c10fe9261f0510e89165a519a53995cab - 3], out c62a8ac8a1ea6107858bdc026334c8108, out cf94628f848fdb1b294ef4b3736d0f3bc);
			break;
		case 4:
			ComputeCircumscribedSphere(c2ebf3a3e77e8b4507a45357157f26fae[c10fe9261f0510e89165a519a53995cab - 1], c2ebf3a3e77e8b4507a45357157f26fae[c10fe9261f0510e89165a519a53995cab - 2], c2ebf3a3e77e8b4507a45357157f26fae[c10fe9261f0510e89165a519a53995cab - 3], c2ebf3a3e77e8b4507a45357157f26fae[c10fe9261f0510e89165a519a53995cab - 4], out c62a8ac8a1ea6107858bdc026334c8108, out cf94628f848fdb1b294ef4b3736d0f3bc);
			return;
		default:
			throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1436));
		case 0:
			break;
		}
		for (int i = c10fe9261f0510e89165a519a53995cab; i <= cd6bf80fbab17a5008e6587c3dc036124; i++)
		{
			Vector3F vector3F = c2ebf3a3e77e8b4507a45357157f26fae[i];
			if (HaveContact(c62a8ac8a1ea6107858bdc026334c8108, vector3F - cf94628f848fdb1b294ef4b3736d0f3bc))
			{
				continue;
			}
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
				__ldtoken(GeometryHelper.c24c70f2ef1df09ff1a297bf6612c6b24(List<Vector3F>, int, int, int, ref float, ref Vector3F));
			}
			if (i > c10fe9261f0510e89165a519a53995cab)
			{
				c2ebf3a3e77e8b4507a45357157f26fae.RemoveAt(i);
				c2ebf3a3e77e8b4507a45357157f26fae.Insert(c10fe9261f0510e89165a519a53995cab, vector3F);
			}
			c24c70f2ef1df09ff1a297bf6612c6b24(c2ebf3a3e77e8b4507a45357157f26fae, c10fe9261f0510e89165a519a53995cab + 1, i, ce5305dd642033151733ae3a96448e3ce + 1, out c62a8ac8a1ea6107858bdc026334c8108, out cf94628f848fdb1b294ef4b3736d0f3bc);
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				break;
			default:
				return;
			}
		}
	}

	/// <summary>
	///       Computes a sphere where all given points touch the surface.
	///       </summary>
	/// <param name="point0">The first point.</param>
	/// <param name="point1">The second point.</param>
	/// <param name="radius">The sphere radius.</param>
	/// <param name="center">The sphere center.</param>
	public static void ComputeCircumscribedSphere(Vector3F point0, Vector3F point1, out float radius, out Vector3F center)
	{
		center = (point0 + point1) / 2f;
		radius = (point1 - point0).Length / 2f;
	}

	/// <summary>
	///       Computes a sphere where all given points touch the surface.
	///       </summary>
	/// <param name="point0">The first point.</param>
	/// <param name="point1">The second point.</param>
	/// <param name="point2">The third point.</param>
	/// <param name="radius">The sphere radius.</param>
	/// <param name="center">The sphere center.</param>
	public static void ComputeCircumscribedSphere(Vector3F point0, Vector3F point1, Vector3F point2, out float radius, out Vector3F center)
	{
		float lengthSquared = (point2 - point1).LengthSquared;
		float lengthSquared2 = (point2 - point0).LengthSquared;
		float lengthSquared3 = (point1 - point0).LengthSquared;
		float num = 2f * lengthSquared * lengthSquared2 + 2f * lengthSquared * lengthSquared3 + 2f * lengthSquared2 * lengthSquared3 - lengthSquared * lengthSquared - lengthSquared2 * lengthSquared2 - lengthSquared3 * lengthSquared3;
		float num2 = 1f / num;
		float num3 = lengthSquared * (lengthSquared2 + lengthSquared3 - lengthSquared) * num2;
		float num4 = lengthSquared2 * (lengthSquared + lengthSquared3 - lengthSquared2) * num2;
		float num5 = lengthSquared3 * (lengthSquared + lengthSquared2 - lengthSquared3) * num2;
		center = num3 * point0 + num4 * point1 + num5 * point2;
		radius = (point0 - center).Length;
	}

	/// <summary>
	///       Computes a sphere where all given points touch the surface.
	///       </summary>
	/// <param name="point0">The first point.</param>
	/// <param name="point1">The second point.</param>
	/// <param name="point2">The third point.</param>
	/// <param name="point3">The fourth point.</param>
	/// <param name="radius">The sphere radius.</param>
	/// <param name="center">The sphere center.</param>
	public static void ComputeCircumscribedSphere(Vector3F point0, Vector3F point1, Vector3F point2, Vector3F point3, out float radius, out Vector3F center)
	{
		Vector3F vector = point1 - point0;
		Vector3F vector2 = point2 - point0;
		Vector3F vector3 = point3 - point0;
		float lengthSquared = vector.LengthSquared;
		float lengthSquared2 = vector2.LengthSquared;
		float lengthSquared3 = vector3.LengthSquared;
		float num = 1f / 6f * Vector3F.Dot(vector, Vector3F.Cross(vector2, vector3));
		center = default;
		float num2 = 1f / (12f * num);
		center.X = point0.X + num2 * ((vector2.Y * vector3.Z - vector3.Y * vector2.Z) * lengthSquared - (vector.Y * vector3.Z - vector3.Y * vector.Z) * lengthSquared2 + (vector.Y * vector2.Z - vector2.Y * vector.Z) * lengthSquared3);
		center.Y = point0.Y + num2 * ((0f - (vector2.X * vector3.Z - vector3.X * vector2.Z)) * lengthSquared + (vector.X * vector3.Z - vector3.X * vector.Z) * lengthSquared2 - (vector.X * vector2.Z - vector2.X * vector.Z) * lengthSquared3);
		center.Z = point0.Z + num2 * ((vector2.X * vector3.Y - vector3.X * vector2.Y) * lengthSquared - (vector.X * vector3.Y - vector3.X * vector.Y) * lengthSquared2 + (vector.X * vector2.Y - vector2.X * vector.Y) * lengthSquared3);
		float num3 = center.X - point0.X;
		float num4 = center.Y - point0.Y;
		float num5 = center.Z - point0.Z;
		radius = (float)Math.Sqrt(num3 * num3 + num4 * num4 + num5 * num5);
	}

	/// <summary>
	///       Determines whether the specified point is inside the sphere.
	///       </summary>
	/// <param name="sphereRadius">The sphere radius.</param>
	/// <param name="point">The point (in the local space of the sphere).</param>
	/// <returns>
	///   <see langword="true" /> if the specified point is inside; otherwise, <see langword="false" />.
	///       </returns>
	public static bool HaveContact(float sphereRadius, Vector3F point)
	{
		float lengthSquared = point.LengthSquared;
		float num = sphereRadius + Math.Max(1f, sphereRadius) * Numeric.EpsilonF;
		float num2 = lengthSquared - num * num;
		return num2 <= 0f;
	}

	internal static bool cafd5d3cab1f2cf678e615b534dc678fb(ITriangleMesh cea4e848206cd8826d44040bad21efb96, Ray c2d769fcfe2c7e7844e7de692907680c8, out float c80b1b7de6c8095255198c83448cdea79)
	{
		for (int i = 0; i < cea4e848206cd8826d44040bad21efb96.NumberOfTriangles; i++)
		{
			Triangle triangle = cea4e848206cd8826d44040bad21efb96.GetTriangle(i);
			if (GetContact(c2d769fcfe2c7e7844e7de692907680c8, triangle, isTwoSided: false, out c80b1b7de6c8095255198c83448cdea79))
			{
				return true;
			}
		}
		c80b1b7de6c8095255198c83448cdea79 = float.NaN;
		return false;
	}

	/// <summary>
	///       Gets the enclosed volume of a triangle mesh.
	///       </summary>
	/// <param name="triangleMesh">The triangle mesh.</param>
	/// <returns>
	///       The enclosed volume of the given triangle mesh.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="triangleMesh" /> is <see langword="null" />.
	///       </exception>
	public static float GetVolume(this ITriangleMesh triangleMesh)
	{
		if (triangleMesh == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1517));
		}
		int numberOfTriangles = triangleMesh.NumberOfTriangles;
		if (numberOfTriangles == 0)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(GeometryHelper.GetVolume(ITriangleMesh));
					}
					return 0f;
				}
			}
		}
		Vector3F vertex = triangleMesh.GetTriangle(0).Vertex0;
		float num = 0f;
		for (int i = 1; i < numberOfTriangles; i++)
		{
			Triangle c082c516895a1e859067cd7bbdbe8b9e = triangleMesh.GetTriangle(i);
			float num2 = c05219c593ba1d41d3ff74af852f0a272(vertex, ref c082c516895a1e859067cd7bbdbe8b9e);
			num += num2;
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			return num;
		}
	}

	private static float c05219c593ba1d41d3ff74af852f0a272(Vector3F cfef46e983928cc4e6880c424d1468b93, ref Triangle c082c516895a1e859067cd7bbdbe8b9e5)
	{
		Vector3F vector = c082c516895a1e859067cd7bbdbe8b9e5.Vertex1 - c082c516895a1e859067cd7bbdbe8b9e5.Vertex0;
		Vector3F vector2 = c082c516895a1e859067cd7bbdbe8b9e5.Vertex2 - c082c516895a1e859067cd7bbdbe8b9e5.Vertex0;
		Vector3F vector3 = cfef46e983928cc4e6880c424d1468b93 - c082c516895a1e859067cd7bbdbe8b9e5.Vertex0;
		return 1f / 6f * Vector3F.Dot(Vector3F.Cross(vector2, vector), vector3);
	}

	internal static IList<Vector3F> cb62be1cc9adc6e749e53fcadaf4a7908(ConvexShape cc5fdd9518d3b72c985e2608f434fa386, float c774b8835bcc06360a38066b614c703c2, int c11ff2b2cbe9bdd3b85d3ad8d4b1509f0)
	{
		if (cc5fdd9518d3b72c985e2608f434fa386 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1542));
		}
		if (c774b8835bcc06360a38066b614c703c2 < 0f)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(GeometryHelper.cb62be1cc9adc6e749e53fcadaf4a7908(ConvexShape, float, int));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1553), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1588));
				}
			}
		}
		if (c11ff2b2cbe9bdd3b85d3ad8d4b1509f0 < 1)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(325), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1695));
		}
		c774b8835bcc06360a38066b614c703c2 = Math.Max(c774b8835bcc06360a38066b614c703c2, Numeric.EpsilonF);
		c98f8cbf4d7195969dd16693ab608adec c98f8cbf4d7195969dd16693ab608adec2 = new c98f8cbf4d7195969dd16693ab608adec();
		c98f8cbf4d7195969dd16693ab608adec2.c7af50dba23fb61a6adbbf043e19e469d = c774b8835bcc06360a38066b614c703c2;
		c98f8cbf4d7195969dd16693ab608adec c98f8cbf4d7195969dd16693ab608adec3 = c98f8cbf4d7195969dd16693ab608adec2;
		List<c354edd6f218fd4871fbb9bd00aa78db8> list = new List<c354edd6f218fd4871fbb9bd00aa78db8>();
		list.Add(new c354edd6f218fd4871fbb9bd00aa78db8(new Vector3F(-1f, 0f, 0f)));
		list.Add(new c354edd6f218fd4871fbb9bd00aa78db8(new Vector3F(1f, 0f, 0f)));
		list.Add(new c354edd6f218fd4871fbb9bd00aa78db8(new Vector3F(0f, -1f, 0f)));
		list.Add(new c354edd6f218fd4871fbb9bd00aa78db8(new Vector3F(0f, 1f, 0f)));
		list.Add(new c354edd6f218fd4871fbb9bd00aa78db8(new Vector3F(0f, 0f, -1f)));
		list.Add(new c354edd6f218fd4871fbb9bd00aa78db8(new Vector3F(0f, 0f, 1f)));
		List<c354edd6f218fd4871fbb9bd00aa78db8> list2 = list;
		List<c9fb21dd88a7948b34865889c9f33f4bc> list3 = new List<c9fb21dd88a7948b34865889c9f33f4bc>();
		list3.Add(new c9fb21dd88a7948b34865889c9f33f4bc(5, 1, 3, 1));
		list3.Add(new c9fb21dd88a7948b34865889c9f33f4bc(5, 2, 1, 1));
		list3.Add(new c9fb21dd88a7948b34865889c9f33f4bc(1, 2, 4, 1));
		list3.Add(new c9fb21dd88a7948b34865889c9f33f4bc(4, 2, 0, 1));
		list3.Add(new c9fb21dd88a7948b34865889c9f33f4bc(0, 2, 5, 1));
		list3.Add(new c9fb21dd88a7948b34865889c9f33f4bc(1, 4, 3, 1));
		list3.Add(new c9fb21dd88a7948b34865889c9f33f4bc(3, 4, 0, 1));
		list3.Add(new c9fb21dd88a7948b34865889c9f33f4bc(0, 5, 3, 1));
		List<c9fb21dd88a7948b34865889c9f33f4bc> list4 = list3;
		int count = list2.Count;
		for (int i = 0; i < count; i++)
		{
			c354edd6f218fd4871fbb9bd00aa78db8 c354edd6f218fd4871fbb9bd00aa78db9 = list2[i];
			Vector3F supportPoint = cc5fdd9518d3b72c985e2608f434fa386.GetSupportPoint(c354edd6f218fd4871fbb9bd00aa78db9.c9fc1a33c8fe55213152479d7ac87ef15);
			int num = c98f8cbf4d7195969dd16693ab608adec3.c874a839ca13681a1e4151cb408603f37(supportPoint);
			if (num == -1)
			{
				num = c98f8cbf4d7195969dd16693ab608adec3.c9c9dc7a5e68aac53b065464d791aba7d.Count;
				c98f8cbf4d7195969dd16693ab608adec3.c9c9dc7a5e68aac53b065464d791aba7d.Add(supportPoint);
			}
			c354edd6f218fd4871fbb9bd00aa78db9.c6963d78d009d6d192bf7efd926b22003 = num;
		}
		c98f8cbf4d7195969dd16693ab608adec c98f8cbf4d7195969dd16693ab608adec4 = new c98f8cbf4d7195969dd16693ab608adec();
		bool flag = false;
		while (!flag)
		{
			flag = true;
			c98f8cbf4d7195969dd16693ab608adec4.c9c9dc7a5e68aac53b065464d791aba7d.Clear();
			int count2 = c98f8cbf4d7195969dd16693ab608adec3.c9c9dc7a5e68aac53b065464d791aba7d.Count;
			int count3 = list4.Count;
			for (int j = 0; j < count3; j++)
			{
				c9fb21dd88a7948b34865889c9f33f4bc c9fb21dd88a7948b34865889c9f33f4bc2 = list4[j];
				int cda2eac9404b56954803c20fc28f7d0fd = c9fb21dd88a7948b34865889c9f33f4bc2.cda2eac9404b56954803c20fc28f7d0fd;
				if (cda2eac9404b56954803c20fc28f7d0fd < 0)
				{
					continue;
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
				if (cda2eac9404b56954803c20fc28f7d0fd >= c11ff2b2cbe9bdd3b85d3ad8d4b1509f0)
				{
					continue;
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				int c125b54a9bf1ca20fcabec365ce7e27c = c9fb21dd88a7948b34865889c9f33f4bc2.c125b54a9bf1ca20fcabec365ce7e27c6;
				int c4b7d9aa0151452e58db5e508df376dcf = c9fb21dd88a7948b34865889c9f33f4bc2.c4b7d9aa0151452e58db5e508df376dcf;
				int cdfdfcfc4e1d6ae7dd8081f66a8d9f12e = c9fb21dd88a7948b34865889c9f33f4bc2.cdfdfcfc4e1d6ae7dd8081f66a8d9f12e;
				c354edd6f218fd4871fbb9bd00aa78db8 c354edd6f218fd4871fbb9bd00aa78db10 = list2[c125b54a9bf1ca20fcabec365ce7e27c];
				c354edd6f218fd4871fbb9bd00aa78db8 c354edd6f218fd4871fbb9bd00aa78db11 = list2[c4b7d9aa0151452e58db5e508df376dcf];
				c354edd6f218fd4871fbb9bd00aa78db8 c354edd6f218fd4871fbb9bd00aa78db12 = list2[cdfdfcfc4e1d6ae7dd8081f66a8d9f12e];
				c354edd6f218fd4871fbb9bd00aa78db8 c354edd6f218fd4871fbb9bd00aa78db13 = new c354edd6f218fd4871fbb9bd00aa78db8((c354edd6f218fd4871fbb9bd00aa78db10.c9fc1a33c8fe55213152479d7ac87ef15 + c354edd6f218fd4871fbb9bd00aa78db11.c9fc1a33c8fe55213152479d7ac87ef15).Normalized);
				c354edd6f218fd4871fbb9bd00aa78db8 c354edd6f218fd4871fbb9bd00aa78db14 = new c354edd6f218fd4871fbb9bd00aa78db8((c354edd6f218fd4871fbb9bd00aa78db11.c9fc1a33c8fe55213152479d7ac87ef15 + c354edd6f218fd4871fbb9bd00aa78db12.c9fc1a33c8fe55213152479d7ac87ef15).Normalized);
				c354edd6f218fd4871fbb9bd00aa78db8 c354edd6f218fd4871fbb9bd00aa78db15 = new c354edd6f218fd4871fbb9bd00aa78db8((c354edd6f218fd4871fbb9bd00aa78db12.c9fc1a33c8fe55213152479d7ac87ef15 + c354edd6f218fd4871fbb9bd00aa78db10.c9fc1a33c8fe55213152479d7ac87ef15).Normalized);
				Vector3F supportPoint2 = cc5fdd9518d3b72c985e2608f434fa386.GetSupportPoint(c354edd6f218fd4871fbb9bd00aa78db13.c9fc1a33c8fe55213152479d7ac87ef15);
				Vector3F supportPoint3 = cc5fdd9518d3b72c985e2608f434fa386.GetSupportPoint(c354edd6f218fd4871fbb9bd00aa78db14.c9fc1a33c8fe55213152479d7ac87ef15);
				Vector3F supportPoint4 = cc5fdd9518d3b72c985e2608f434fa386.GetSupportPoint(c354edd6f218fd4871fbb9bd00aa78db15.c9fc1a33c8fe55213152479d7ac87ef15);
				c354edd6f218fd4871fbb9bd00aa78db13.c6963d78d009d6d192bf7efd926b22003 = c98f8cbf4d7195969dd16693ab608adec3.c874a839ca13681a1e4151cb408603f37(supportPoint2);
				c354edd6f218fd4871fbb9bd00aa78db14.c6963d78d009d6d192bf7efd926b22003 = c98f8cbf4d7195969dd16693ab608adec3.c874a839ca13681a1e4151cb408603f37(supportPoint3);
				c354edd6f218fd4871fbb9bd00aa78db15.c6963d78d009d6d192bf7efd926b22003 = c98f8cbf4d7195969dd16693ab608adec3.c874a839ca13681a1e4151cb408603f37(supportPoint4);
				if (c354edd6f218fd4871fbb9bd00aa78db13.c6963d78d009d6d192bf7efd926b22003 != -1)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
					if (c354edd6f218fd4871fbb9bd00aa78db14.c6963d78d009d6d192bf7efd926b22003 != -1)
					{
						if (c354edd6f218fd4871fbb9bd00aa78db15.c6963d78d009d6d192bf7efd926b22003 != -1)
						{
							c9fb21dd88a7948b34865889c9f33f4bc2.cda2eac9404b56954803c20fc28f7d0fd = -1;
							continue;
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
					}
				}
				flag = false;
				int count4 = list2.Count;
				int num2 = count4 + 1;
				int num3 = num2 + 1;
				list2.Add(c354edd6f218fd4871fbb9bd00aa78db13);
				list2.Add(c354edd6f218fd4871fbb9bd00aa78db14);
				list2.Add(c354edd6f218fd4871fbb9bd00aa78db15);
				c9fb21dd88a7948b34865889c9f33f4bc2.c125b54a9bf1ca20fcabec365ce7e27c6 = count4;
				c9fb21dd88a7948b34865889c9f33f4bc2.c4b7d9aa0151452e58db5e508df376dcf = num2;
				c9fb21dd88a7948b34865889c9f33f4bc2.cdfdfcfc4e1d6ae7dd8081f66a8d9f12e = num3;
				cda2eac9404b56954803c20fc28f7d0fd = (c9fb21dd88a7948b34865889c9f33f4bc2.cda2eac9404b56954803c20fc28f7d0fd = cda2eac9404b56954803c20fc28f7d0fd + 1);
				if (c354edd6f218fd4871fbb9bd00aa78db13.c6963d78d009d6d192bf7efd926b22003 != -1)
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
					if (c354edd6f218fd4871fbb9bd00aa78db14.c6963d78d009d6d192bf7efd926b22003 != -1)
					{
						goto IL_048c;
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
				list4.Add(new c9fb21dd88a7948b34865889c9f33f4bc(count4, c4b7d9aa0151452e58db5e508df376dcf, num2, cda2eac9404b56954803c20fc28f7d0fd));
				goto IL_048c;
				IL_04bd:
				if (c354edd6f218fd4871fbb9bd00aa78db13.c6963d78d009d6d192bf7efd926b22003 != -1)
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
					if (c354edd6f218fd4871fbb9bd00aa78db15.c6963d78d009d6d192bf7efd926b22003 != -1)
					{
						goto IL_04ee;
					}
				}
				list4.Add(new c9fb21dd88a7948b34865889c9f33f4bc(c125b54a9bf1ca20fcabec365ce7e27c, count4, num3, cda2eac9404b56954803c20fc28f7d0fd));
				goto IL_04ee;
				IL_048c:
				if (c354edd6f218fd4871fbb9bd00aa78db14.c6963d78d009d6d192bf7efd926b22003 != -1)
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
					if (c354edd6f218fd4871fbb9bd00aa78db15.c6963d78d009d6d192bf7efd926b22003 != -1)
					{
						goto IL_04bd;
					}
				}
				list4.Add(new c9fb21dd88a7948b34865889c9f33f4bc(num2, cdfdfcfc4e1d6ae7dd8081f66a8d9f12e, num3, cda2eac9404b56954803c20fc28f7d0fd));
				goto IL_04bd;
				IL_04ee:
				if (c354edd6f218fd4871fbb9bd00aa78db13.c6963d78d009d6d192bf7efd926b22003 == -1)
				{
					int num4 = c98f8cbf4d7195969dd16693ab608adec4.c874a839ca13681a1e4151cb408603f37(supportPoint2);
					if (num4 == -1)
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
						num4 = c98f8cbf4d7195969dd16693ab608adec4.c9c9dc7a5e68aac53b065464d791aba7d.Count;
						c98f8cbf4d7195969dd16693ab608adec4.c9c9dc7a5e68aac53b065464d791aba7d.Add(supportPoint2);
					}
					c354edd6f218fd4871fbb9bd00aa78db13.c6963d78d009d6d192bf7efd926b22003 = count2 + num4;
				}
				if (c354edd6f218fd4871fbb9bd00aa78db14.c6963d78d009d6d192bf7efd926b22003 == -1)
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
					int num5 = c98f8cbf4d7195969dd16693ab608adec4.c874a839ca13681a1e4151cb408603f37(supportPoint3);
					if (num5 == -1)
					{
						num5 = c98f8cbf4d7195969dd16693ab608adec4.c9c9dc7a5e68aac53b065464d791aba7d.Count;
						c98f8cbf4d7195969dd16693ab608adec4.c9c9dc7a5e68aac53b065464d791aba7d.Add(supportPoint3);
					}
					c354edd6f218fd4871fbb9bd00aa78db14.c6963d78d009d6d192bf7efd926b22003 = count2 + num5;
				}
				if (c354edd6f218fd4871fbb9bd00aa78db15.c6963d78d009d6d192bf7efd926b22003 != -1)
				{
					continue;
				}
				int num6 = c98f8cbf4d7195969dd16693ab608adec4.c874a839ca13681a1e4151cb408603f37(supportPoint4);
				if (num6 == -1)
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
					num6 = c98f8cbf4d7195969dd16693ab608adec4.c9c9dc7a5e68aac53b065464d791aba7d.Count;
					c98f8cbf4d7195969dd16693ab608adec4.c9c9dc7a5e68aac53b065464d791aba7d.Add(supportPoint4);
				}
				c354edd6f218fd4871fbb9bd00aa78db15.c6963d78d009d6d192bf7efd926b22003 = count2 + num6;
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
			int count5 = c98f8cbf4d7195969dd16693ab608adec4.c9c9dc7a5e68aac53b065464d791aba7d.Count;
			for (int k = 0; k < count5; k++)
			{
				c98f8cbf4d7195969dd16693ab608adec3.c9c9dc7a5e68aac53b065464d791aba7d.Add(c98f8cbf4d7195969dd16693ab608adec4.c9c9dc7a5e68aac53b065464d791aba7d[k]);
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			return c98f8cbf4d7195969dd16693ab608adec3.c9c9dc7a5e68aac53b065464d791aba7d;
		}
	}

	/// <summary>
	///       Computes a tight-fitting capsule that contains the given points.
	///       </summary>
	/// <param name="points">The points.</param>
	/// <param name="radius">The radius of the capsule.</param>
	/// <param name="height">The height of the capsule.</param>
	/// <param name="pose">The pose of the capsule.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="points" /> is empty.
	///       </exception>
	public static void ComputeBoundingCapsule(IList<Vector3F> points, out float radius, out float height, out Pose pose)
	{
		if (points == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(GeometryHelper.ComputeBoundingCapsule(IList<Vector3F>, ref float, ref float, ref Pose));
					}
					throw new ArgumentNullException();
				}
			}
		}
		MatrixF matrixF = null;
		try
		{
			if (points.Count > 4)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					DcelMesh dcelMesh = CreateConvexHull(points);
					TriangleMesh triangleMesh = dcelMesh.ToTriangleMesh();
					if (triangleMesh.Vertices.Count > 0)
					{
						points = triangleMesh.Vertices;
					}
					matrixF = c651fddce38f34109ebed63013d3004a8(triangleMesh);
					break;
				}
			}
		}
		catch
		{
		}
		if (matrixF == null || float.IsNaN(matrixF.Determinant))
		{
			matrixF = c93ef0140f5cd2041417c036465df0edc(points);
		}
		EigenvalueDecompositionF eigenvalueDecompositionF = new EigenvalueDecompositionF(matrixF);
		Matrix33F matrix33F = eigenvalueDecompositionF.V.ToMatrix33F();
		matrix33F.SetColumn(2, Vector3F.Cross(matrix33F.GetColumn(0), matrix33F.GetColumn(1)));
		int indexOfLargestComponent = eigenvalueDecompositionF.RealEigenvalues.ToVector3F().IndexOfLargestComponent;
		if (indexOfLargestComponent != 1)
		{
			Vector3F column = matrix33F.GetColumn(indexOfLargestComponent);
			Vector3F column2 = matrix33F.GetColumn(1);
			matrix33F.SetColumn(1, column);
			matrix33F.SetColumn(indexOfLargestComponent, column2);
			matrix33F.SetColumn(2, Vector3F.Cross(matrix33F.GetColumn(0), matrix33F.GetColumn(1)));
		}
		c76b2e5d164b87c61f0eecf424f2726b2(points, matrix33F, out radius, out height, out var cf94628f848fdb1b294ef4b3736d0f3bc);
		pose = new Pose(cf94628f848fdb1b294ef4b3736d0f3bc, matrix33F);
	}

	internal static float c76b2e5d164b87c61f0eecf424f2726b2(IList<Vector3F> c2ebf3a3e77e8b4507a45357157f26fae, Matrix33F c8264a667aeec7228af8eb0f7d20d398e, out float c62a8ac8a1ea6107858bdc026334c8108, out float c19632431916ea40db7b768daafd2c05f, out Vector3F cf94628f848fdb1b294ef4b3736d0f3bc)
	{
		Vector3F[] array = new Vector3F[c2ebf3a3e77e8b4507a45357157f26fae.Count];
		for (int i = 0; i < array.Length; i++)
		{
			ref Vector3F reference = ref array[i];
			reference = c8264a667aeec7228af8eb0f7d20d398e.Transposed * c2ebf3a3e77e8b4507a45357157f26fae[i];
		}
		float num = float.PositiveInfinity;
		float num2 = float.NegativeInfinity;
		for (int j = 0; j < array.Length; j++)
		{
			Vector3F vector3F = array[j];
			if (vector3F.Y < num)
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
					__ldtoken(GeometryHelper.c76b2e5d164b87c61f0eecf424f2726b2(IList<Vector3F>, Matrix33F, ref float, ref float, ref Vector3F));
				}
				num = vector3F.Y;
			}
			else if (vector3F.Y > num2)
			{
				num2 = vector3F.Y;
			}
		}
		Vector3F[] array2 = new Vector3F[array.Length];
		for (int k = 0; k < array.Length; k++)
		{
			ref Vector3F reference2 = ref array2[k];
			reference2 = new Vector3F(array[k].X, 0f, array[k].Z);
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			ComputeBoundingSphere(array2, out c62a8ac8a1ea6107858bdc026334c8108, out cf94628f848fdb1b294ef4b3736d0f3bc);
			float val = (num + num2) / 2f;
			float num3 = Math.Max(val, num2 - c62a8ac8a1ea6107858bdc026334c8108);
			float num4 = Math.Min(val, num + c62a8ac8a1ea6107858bdc026334c8108);
			float num5 = c62a8ac8a1ea6107858bdc026334c8108 * c62a8ac8a1ea6107858bdc026334c8108;
			for (int l = 0; l < array.Length; l++)
			{
				Vector3F vector3F2 = array[l] - cf94628f848fdb1b294ef4b3736d0f3bc;
				if (vector3F2.Y > num3)
				{
					if (!((vector3F2 - new Vector3F(0f, num3, 0f)).LengthSquared > num5))
					{
						continue;
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							continue;
						}
						break;
					}
					num3 = vector3F2.Y;
					float lengthSquared = new Vector3F(vector3F2.X, 0f, vector3F2.Z).LengthSquared;
					if (lengthSquared < num5)
					{
						num3 -= (float)Math.Sqrt(num5 - lengthSquared);
					}
					continue;
				}
				if (!(vector3F2.Y < num4) || !((vector3F2 - new Vector3F(0f, num4, 0f)).LengthSquared > num5))
				{
					continue;
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				num4 = vector3F2.Y;
				float lengthSquared2 = new Vector3F(vector3F2.X, 0f, vector3F2.Z).LengthSquared;
				if (!(lengthSquared2 < num5))
				{
					continue;
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
				num4 += (float)Math.Sqrt(num5 - lengthSquared2);
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				cf94628f848fdb1b294ef4b3736d0f3bc.Y = (num4 + num3) / 2f;
				c19632431916ea40db7b768daafd2c05f = Math.Max(num3 - num4 + 2f * c62a8ac8a1ea6107858bdc026334c8108, 2f * c62a8ac8a1ea6107858bdc026334c8108);
				cf94628f848fdb1b294ef4b3736d0f3bc = c8264a667aeec7228af8eb0f7d20d398e * cf94628f848fdb1b294ef4b3736d0f3bc;
				float num6 = 4.1887903f * c62a8ac8a1ea6107858bdc026334c8108 * c62a8ac8a1ea6107858bdc026334c8108 * c62a8ac8a1ea6107858bdc026334c8108;
				float num7 = (float)Math.PI * c62a8ac8a1ea6107858bdc026334c8108 * c62a8ac8a1ea6107858bdc026334c8108 * (c19632431916ea40db7b768daafd2c05f - 2f * c62a8ac8a1ea6107858bdc026334c8108);
				return num6 + num7;
			}
		}
	}

	/// <summary>
	///       Computes a minimum bounding shape that contains all given points.
	///       </summary>
	/// <param name="points">The points.</param>
	/// <returns>A minimum bounding shape that contains all given points.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="points" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="points" /> is empty.
	///       </exception>
	public static Shape CreateBoundingShape(IList<Vector3F> points)
	{
		if (points == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(GeometryHelper.CreateBoundingShape(IList<Vector3F>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1362));
				}
			}
		}
		if (points.Count == 0)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1375));
				}
			}
		}
		ComputeBoundingSphere(points, out var radius, out var center);
		SphereShape sphereShape = new SphereShape(radius);
		float volume = sphereShape.GetVolume();
		ComputeBoundingCapsule(points, out radius, out var height, out var pose);
		CapsuleShape capsuleShape = new CapsuleShape(radius, height);
		float volume2 = capsuleShape.GetVolume();
		ComputeBoundingBox(points, out var extent, out var pose2);
		BoxShape boxShape = new BoxShape(extent);
		float volume3 = boxShape.GetVolume();
		if (volume < volume3)
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
			if (volume < volume2)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						if (center.IsNumericallyZero)
						{
							return sphereShape;
						}
						return new TransformedShape(new GeometricObject(sphereShape, new Pose(center)));
					}
				}
			}
		}
		if (volume2 < volume3)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (!pose.HasTranslation)
					{
						while (true)
						{
							switch (5)
							{
							case 0:
								continue;
							}
							break;
						}
						if (!pose.HasRotation)
						{
							return capsuleShape;
						}
					}
					return new TransformedShape(new GeometricObject(capsuleShape, pose));
				}
			}
		}
		if (!pose2.HasTranslation && !pose2.HasRotation)
		{
			return boxShape;
		}
		return new TransformedShape(new GeometricObject(boxShape, pose2));
	}

	/// <summary>
	///       Creates a convex hull mesh for a set of points.
	///       </summary>
	/// <param name="points">The points.</param>
	/// <returns>
	///       The mesh of the convex hull or <see langword="null" /> if the point list
	///       is <see langword="null" /> or empty.
	///       </returns>
	public static DcelMesh CreateConvexHull(IEnumerable<Vector3F> points)
	{
		return CreateConvexHull(points, int.MaxValue, 0f);
	}

	/// <summary>
	///       Creates a convex hull mesh for a set of points.
	///       </summary>
	/// <param name="points">The points.</param>
	/// <param name="vertexLimit">
	///       The vertex limit. Must be greater than 0. Common values are 32 or 64.
	///       </param>
	/// <param name="skinWidth">
	///       The skin width. Common values are 0.01 or 0.001.
	///       </param>
	/// <returns>
	///       The mesh of the convex hull or <see langword="null" /> if the point list is 
	///       <see langword="null" /> or empty.
	///       </returns>
	public static DcelMesh CreateConvexHull(IEnumerable<Vector3F> points, int vertexLimit, float skinWidth)
	{
		if (points == null)
		{
			return null;
		}
		c24f3b4ab921762a6a531fa3822e2f73d c24f3b4ab921762a6a531fa3822e2f73d2 = new c24f3b4ab921762a6a531fa3822e2f73d(vertexLimit, skinWidth);
		return c24f3b4ab921762a6a531fa3822e2f73d2.c2d8ce8b1ccf2b905e3b18f8a5145fe8a(points);
	}

	internal static void c60b96cf7efe9856cb397e87dfedc2ec9(Vector3F caa9d7ab80719ca5895d6816e6deee0d6, out float c62a8ac8a1ea6107858bdc026334c8108, out float c4b535767326f6d7c326ca84d6c8e084e, out float c8c4fbd7733fc3e4744dfcd959255158e)
	{
		c62a8ac8a1ea6107858bdc026334c8108 = caa9d7ab80719ca5895d6816e6deee0d6.Length;
		if (c62a8ac8a1ea6107858bdc026334c8108 == 0f)
		{
			c8c4fbd7733fc3e4744dfcd959255158e = 0f;
			c4b535767326f6d7c326ca84d6c8e084e = 0f;
		}
		else
		{
			c8c4fbd7733fc3e4744dfcd959255158e = (float)Math.Atan2(caa9d7ab80719ca5895d6816e6deee0d6.Y, caa9d7ab80719ca5895d6816e6deee0d6.X);
			c4b535767326f6d7c326ca84d6c8e084e = (float)Math.Acos(caa9d7ab80719ca5895d6816e6deee0d6.Z / c62a8ac8a1ea6107858bdc026334c8108);
		}
	}

	internal static Vector3F caf44a8aec1554660c89dbcc0a037a935(float c62a8ac8a1ea6107858bdc026334c8108, float c4b535767326f6d7c326ca84d6c8e084e, float c8c4fbd7733fc3e4744dfcd959255158e)
	{
		if ((double)c62a8ac8a1ea6107858bdc026334c8108 == 0.0)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(GeometryHelper.caf44a8aec1554660c89dbcc0a037a935(float, float, float));
					}
					return default;
				}
			}
		}
		float num = (float)Math.Sin(c8c4fbd7733fc3e4744dfcd959255158e);
		float num2 = (float)Math.Cos(c8c4fbd7733fc3e4744dfcd959255158e);
		float num3 = (float)Math.Sin(c4b535767326f6d7c326ca84d6c8e084e);
		float num4 = (float)Math.Cos(c4b535767326f6d7c326ca84d6c8e084e);
		return new Vector3F(c62a8ac8a1ea6107858bdc026334c8108 * num3 * num2, c62a8ac8a1ea6107858bdc026334c8108 * num3 * num, c62a8ac8a1ea6107858bdc026334c8108 * num4);
	}

	/// <summary>
	///       Gets the barycentric coordinates of a point and a triangle.
	///       </summary>
	/// <param name="triangle">The triangle.</param>
	/// <param name="point">The point.</param>
	/// <param name="u">The barycentric coordinate u.</param>
	/// <param name="v">The barycentric coordinate v.</param>
	/// <param name="w">The barycentric coordinate w.</param>
	public static void GetBarycentricFromPoint(Triangle triangle, Vector3F point, out float u, out float v, out float w)
	{
		Vector3F vertex = triangle.Vertex0;
		Vector3F vertex2 = triangle.Vertex1;
		Vector3F vertex3 = triangle.Vertex2;
		Vector3F vector = Vector3F.Cross(vertex2 - vertex, vertex3 - vertex);
		Vector3F vector3F = Vector3F.Absolute(vector);
		float num;
		float num2;
		float num3;
		if (vector3F.X >= vector3F.Y && vector3F.X >= vector3F.Z)
		{
			num = c74c12e250ab12e193fba927e302217e8(point.Y, point.Z, vertex2.Y, vertex2.Z, vertex3.Y, vertex3.Z);
			num2 = c74c12e250ab12e193fba927e302217e8(point.Y, point.Z, vertex3.Y, vertex3.Z, vertex.Y, vertex.Z);
			num3 = 1f / vector.X;
		}
		else if (vector3F.Y >= vector3F.X && vector3F.Y >= vector3F.Z)
		{
			num = c74c12e250ab12e193fba927e302217e8(point.X, point.Z, vertex2.X, vertex2.Z, vertex3.X, vertex3.Z);
			num2 = c74c12e250ab12e193fba927e302217e8(point.X, point.Z, vertex3.X, vertex3.Z, vertex.X, vertex.Z);
			num3 = 1f / (0f - vector.Y);
		}
		else
		{
			num = c74c12e250ab12e193fba927e302217e8(point.X, point.Y, vertex2.X, vertex2.Y, vertex3.X, vertex3.Y);
			num2 = c74c12e250ab12e193fba927e302217e8(point.X, point.Y, vertex3.X, vertex3.Y, vertex.X, vertex.Y);
			num3 = 1f / vector.Z;
		}
		u = num * num3;
		v = num2 * num3;
		w = 1f - u - v;
	}

	private static float c74c12e250ab12e193fba927e302217e8(float cd92535445e7c9b4ca45abbd8144b7bcc, float cba01ac27a7af8e017c53b11e601b9b17, float c41ab3d97d82c08b04a75998da64f1921, float c020a5a59cb9f9c2933e445d27e8cf042, float cc02d53b48945df9f94f178f929005ff6, float cc73f324a7f65f663090f304f9b55901e)
	{
		return (cd92535445e7c9b4ca45abbd8144b7bcc - c41ab3d97d82c08b04a75998da64f1921) * (c020a5a59cb9f9c2933e445d27e8cf042 - cc73f324a7f65f663090f304f9b55901e) - (c41ab3d97d82c08b04a75998da64f1921 - cc02d53b48945df9f94f178f929005ff6) * (cba01ac27a7af8e017c53b11e601b9b17 - c020a5a59cb9f9c2933e445d27e8cf042);
	}

	/// <summary>
	///       Computes the ray vs. triangle contact.
	///       </summary>
	/// <param name="triangle">The triangle.</param>
	/// <param name="isTwoSided">
	///       if set to <see langword="true" /> the triangle is treated as a two-sided triangle. Ray
	///       contacts with the back-side of a one-sided triangle are not reported.
	///       </param>
	/// <param name="ray">The ray.</param>
	/// <param name="hitDistance">
	///       The hit distance. This is the distance on the ray from the ray origin to the contact point.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the ray hits the front side of the triangle;
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool GetContact(Ray ray, Triangle triangle, bool isTwoSided, out float hitDistance)
	{
		hitDistance = float.NaN;
		Vector3F vertex = triangle.Vertex0;
		Vector3F vertex2 = triangle.Vertex1;
		Vector3F vertex3 = triangle.Vertex2;
		Vector3F vector3F = vertex2 - vertex;
		Vector3F vector3F2 = vertex3 - vertex;
		Vector3F vector = Vector3F.Cross(vector3F, vector3F2);
		float num = vector.Length * Numeric.EpsilonFSquared;
		Vector3F vector3F3 = ray.Direction * ray.Length;
		float num2 = 0f - Vector3F.Dot(vector3F3, vector);
		if (num == 0f || Numeric.IsZero(num2, num))
		{
			return false;
		}
		if (!isTwoSided && num2 < 0f)
		{
			return false;
		}
		Vector3F vector2 = ray.Origin - vertex;
		float num3 = Vector3F.Dot(vector2, vector) / num2;
		hitDistance = num3 * ray.Length;
		if (num3 < 0f || num3 > 1f)
		{
			return false;
		}
		Vector3F vector3 = Vector3F.Cross(vector2, vector3F3);
		float num4 = Vector3F.Dot(vector3F2, vector3) / num2;
		float num5 = Vector3F.Dot(-vector3F, vector3) / num2;
		if (num4 + num5 <= 1f + num && num4 >= 0f - num && num5 >= 0f - num)
		{
			return true;
		}
		return false;
	}

	/// <summary>
	///       Gets the point on the triangle defined by the given barycentric coordinates.
	///       </summary>
	/// <param name="triangle">The triangle.</param>
	/// <param name="u">The barycentric coordinate u.</param>
	/// <param name="v">The barycentric coordinate v.</param>
	/// <param name="w">The barycentric coordinate w.</param>
	/// <returns>
	///       The point computes as 
	///         <paramref name="u" /> * <see cref="F:DigitalRune.Geometry.Shapes.Triangle.Vertex0" /> 
	///         + <paramref name="v" /> * <see cref="F:DigitalRune.Geometry.Shapes.Triangle.Vertex1" /> 
	///         + <paramref name="w" /> * <see cref="F:DigitalRune.Geometry.Shapes.Triangle.Vertex2" />.
	///       </returns>
	public static Vector3F GetPointFromBarycentric(Triangle triangle, float u, float v, float w)
	{
		return u * triangle.Vertex0 + v * triangle.Vertex1 + w * triangle.Vertex2;
	}

	/// <summary>
	///       Determines whether the point is in front of the triangle.
	///       </summary>
	/// <param name="triangle">The triangle.</param>
	/// <param name="point">The point.</param>
	/// <returns>
	///       A value greater than 0 if the point is in front. A value less than 0 if the point is on the
	///       back-side.
	///       </returns>
	public static float IsInFront(Triangle triangle, Vector3F point)
	{
		return Vector3F.Dot(triangle.Normal, point - triangle.Vertex0);
	}

	/// <summary>
	///       Determines whether the projection of a point (into the triangle plane) is inside the given
	///       triangle.
	///       </summary>
	/// <param name="triangle">The triangle.</param>
	/// <param name="point">The point.</param>
	/// <returns>
	///   <see langword="true" /> if the orthogonal projection of <paramref name="point" /> is inside 
	///       the triangle; otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsOver(Triangle triangle, Vector3F point)
	{
		GetBarycentricFromPoint(triangle, point, out var _, out var v, out var w);
		if (v >= 0f && w >= 0f)
		{
			return v + w <= 1f;
		}
		return false;
	}

	/// <summary>
	///       Gets the barycentric coordinates (<paramref name="u" />, <paramref name="v" />,
	///       <paramref name="w" /> of the point in a triangle which is closest to
	///       the given <paramref name="point" />).
	///       </summary>
	/// <param name="triangle">The triangle.</param>
	/// <param name="point">The point.</param>
	/// <param name="u">The barycentric coordinate u.</param>
	/// <param name="v">The barycentric coordinate v.</param>
	/// <param name="w">The barycentric coordinate w.</param>
	public static void GetClosestPoint(Triangle triangle, Vector3F point, out float u, out float v, out float w)
	{
		Vector3F vertex = triangle.Vertex0;
		Vector3F vertex2 = triangle.Vertex1;
		Vector3F vertex3 = triangle.Vertex2;
		u = (v = (w = 0f));
		Vector3F vector = vertex2 - vertex;
		Vector3F vector2 = vertex3 - vertex;
		Vector3F vector3 = point - vertex;
		float num = Vector3F.Dot(vector, vector3);
		float num2 = Vector3F.Dot(vector2, vector3);
		if (num <= 0f && num2 <= 0f)
		{
			u = 1f;
			return;
		}
		Vector3F vector4 = point - vertex2;
		float num3 = Vector3F.Dot(vector, vector4);
		float num4 = Vector3F.Dot(vector2, vector4);
		if (num3 >= 0f && num4 <= num3)
		{
			v = 1f;
			return;
		}
		float num5 = num * num4 - num3 * num2;
		if (num5 <= 0f && num >= 0f && num3 <= 0f)
		{
			v = num / (num - num3);
			u = 1f - v;
			return;
		}
		Vector3F vector5 = point - vertex3;
		float num6 = Vector3F.Dot(vector, vector5);
		float num7 = Vector3F.Dot(vector2, vector5);
		if (num7 >= 0f && num6 <= num7)
		{
			w = 1f;
			return;
		}
		float num8 = num6 * num2 - num * num7;
		if (num8 <= 0f && num2 >= 0f && num7 <= 0f)
		{
			w = num2 / (num2 - num7);
			u = 1f - w;
			return;
		}
		float num9 = num3 * num7 - num6 * num4;
		if (num9 <= 0f && num4 - num3 >= 0f && num6 - num7 >= 0f)
		{
			w = (num4 - num3) / (num4 - num3 + (num6 - num7));
			v = 1f - w;
			return;
		}
		float num10 = 1f / (num9 + num8 + num5);
		u = num9 * num10;
		v = num8 * num10;
		w = num5 * num10;
	}
}
