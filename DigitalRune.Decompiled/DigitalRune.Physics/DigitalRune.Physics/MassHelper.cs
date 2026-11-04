using System;
using A;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics;

/// <summary>
///       Provides helper methods for computing and manipulating mass properties.
///       </summary>
public static class MassHelper
{
	static MassHelper()
	{
		TypeInitializer.Initialize(c31a405ff5d24b3c4be569f1e85a6f73f.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(13, c31a405ff5d24b3c4be569f1e85a6f73f.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	internal static void ce602a7340e91bf272bcbe2f132962ab5(Shape cc5fdd9518d3b72c985e2608f434fa386, Vector3F ceabb47d7d959545d89c4300eabbdd7c2, float ccd0e6705e44b13fb907035ec01adf2ca, bool c6e96ba1bb9ca98c3df80bc5932f765fc, float cec2cf841da966bf6a09d908b7167be17, int c11ff2b2cbe9bdd3b85d3ad8d4b1509f0, out float c3f34db33979fee2441b0f8e870e3dc40, out Vector3F ced9d1a762c087a2f16a57733d63c1228, out Matrix33F c78db0da7e1c88667b788d19027fafe7c)
	{
		if (cc5fdd9518d3b72c985e2608f434fa386 == null)
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
						__ldtoken(MassHelper.ce602a7340e91bf272bcbe2f132962ab5(Shape, Vector3F, float, bool, float, int, ref float, ref Vector3F, ref Matrix33F));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1751));
				}
			}
		}
		if (ccd0e6705e44b13fb907035ec01adf2ca <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1762), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1789));
		}
		if (cec2cf841da966bf6a09d908b7167be17 < 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1876), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1927));
		}
		c3f34db33979fee2441b0f8e870e3dc40 = 0f;
		ced9d1a762c087a2f16a57733d63c1228 = Vector3F.Zero;
		c78db0da7e1c88667b788d19027fafe7c = Matrix33F.Zero;
		if (cc5fdd9518d3b72c985e2608f434fa386 is BoxShape)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					ce602a7340e91bf272bcbe2f132962ab5((BoxShape)cc5fdd9518d3b72c985e2608f434fa386, ceabb47d7d959545d89c4300eabbdd7c2, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, out c3f34db33979fee2441b0f8e870e3dc40, out c78db0da7e1c88667b788d19027fafe7c);
					return;
				}
			}
		}
		if (cc5fdd9518d3b72c985e2608f434fa386 is CapsuleShape)
		{
			ce602a7340e91bf272bcbe2f132962ab5((CapsuleShape)cc5fdd9518d3b72c985e2608f434fa386, ceabb47d7d959545d89c4300eabbdd7c2, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, out c3f34db33979fee2441b0f8e870e3dc40, out c78db0da7e1c88667b788d19027fafe7c);
			return;
		}
		if (cc5fdd9518d3b72c985e2608f434fa386 is ConeShape)
		{
			ce602a7340e91bf272bcbe2f132962ab5((ConeShape)cc5fdd9518d3b72c985e2608f434fa386, ceabb47d7d959545d89c4300eabbdd7c2, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, out c3f34db33979fee2441b0f8e870e3dc40, out ced9d1a762c087a2f16a57733d63c1228, out c78db0da7e1c88667b788d19027fafe7c);
			return;
		}
		if (cc5fdd9518d3b72c985e2608f434fa386 is CylinderShape)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					ce602a7340e91bf272bcbe2f132962ab5((CylinderShape)cc5fdd9518d3b72c985e2608f434fa386, ceabb47d7d959545d89c4300eabbdd7c2, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, out c3f34db33979fee2441b0f8e870e3dc40, out c78db0da7e1c88667b788d19027fafe7c);
					return;
				}
			}
		}
		if (cc5fdd9518d3b72c985e2608f434fa386 is ScaledConvexShape)
		{
			ScaledConvexShape scaledConvexShape = (ScaledConvexShape)cc5fdd9518d3b72c985e2608f434fa386;
			ce602a7340e91bf272bcbe2f132962ab5(scaledConvexShape.Shape, ceabb47d7d959545d89c4300eabbdd7c2 * scaledConvexShape.Scale, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, cec2cf841da966bf6a09d908b7167be17, c11ff2b2cbe9bdd3b85d3ad8d4b1509f0, out c3f34db33979fee2441b0f8e870e3dc40, out ced9d1a762c087a2f16a57733d63c1228, out c78db0da7e1c88667b788d19027fafe7c);
			return;
		}
		if (cc5fdd9518d3b72c985e2608f434fa386 is SphereShape)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					ce602a7340e91bf272bcbe2f132962ab5((SphereShape)cc5fdd9518d3b72c985e2608f434fa386, ceabb47d7d959545d89c4300eabbdd7c2, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, out c3f34db33979fee2441b0f8e870e3dc40, out c78db0da7e1c88667b788d19027fafe7c);
					return;
				}
			}
		}
		if (cc5fdd9518d3b72c985e2608f434fa386 is TransformedShape)
		{
			TransformedShape transformedShape = (TransformedShape)cc5fdd9518d3b72c985e2608f434fa386;
			ce602a7340e91bf272bcbe2f132962ab5(transformedShape.Child, ceabb47d7d959545d89c4300eabbdd7c2, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, cec2cf841da966bf6a09d908b7167be17, c11ff2b2cbe9bdd3b85d3ad8d4b1509f0, out c3f34db33979fee2441b0f8e870e3dc40, out ced9d1a762c087a2f16a57733d63c1228, out c78db0da7e1c88667b788d19027fafe7c);
		}
		else
		{
			if (cc5fdd9518d3b72c985e2608f434fa386 is HeightField)
			{
				return;
			}
			if (cc5fdd9518d3b72c985e2608f434fa386 is CompositeShape)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
					{
						CompositeShape compositeShape = (CompositeShape)cc5fdd9518d3b72c985e2608f434fa386;
						float ccd0e6705e44b13fb907035ec01adf2ca2 = (c6e96ba1bb9ca98c3df80bc5932f765fc ? ccd0e6705e44b13fb907035ec01adf2ca : 1f);
						foreach (IGeometricObject child in compositeShape.Children)
						{
							ce602a7340e91bf272bcbe2f132962ab5(child, ceabb47d7d959545d89c4300eabbdd7c2, ccd0e6705e44b13fb907035ec01adf2ca2, c6e96ba1bb9ca98c3df80bc5932f765fc: true, cec2cf841da966bf6a09d908b7167be17, c11ff2b2cbe9bdd3b85d3ad8d4b1509f0, out var c3f34db33979fee2441b0f8e870e3dc41, out var ced9d1a762c087a2f16a57733d63c1229, out var c78db0da7e1c88667b788d19027fafe7c2);
							c3f34db33979fee2441b0f8e870e3dc40 += c3f34db33979fee2441b0f8e870e3dc41;
							c78db0da7e1c88667b788d19027fafe7c += cc89638179b4e395fbb50b93d2d63c3af(c3f34db33979fee2441b0f8e870e3dc41, c78db0da7e1c88667b788d19027fafe7c2, ced9d1a762c087a2f16a57733d63c1229);
							ced9d1a762c087a2f16a57733d63c1228 += ced9d1a762c087a2f16a57733d63c1229 * c3f34db33979fee2441b0f8e870e3dc41;
						}
						ced9d1a762c087a2f16a57733d63c1228 /= c3f34db33979fee2441b0f8e870e3dc40;
						c78db0da7e1c88667b788d19027fafe7c = c9e8310d9fa927caa9c83af06a685c8a3(c3f34db33979fee2441b0f8e870e3dc40, c78db0da7e1c88667b788d19027fafe7c, ced9d1a762c087a2f16a57733d63c1228);
						if (!c6e96ba1bb9ca98c3df80bc5932f765fc)
						{
							cae3c009d3f523b01b6f7b0bc1587b2fb(ccd0e6705e44b13fb907035ec01adf2ca, ref c3f34db33979fee2441b0f8e870e3dc40, ref c78db0da7e1c88667b788d19027fafe7c);
						}
						return;
					}
					}
				}
			}
			if (c11ff2b2cbe9bdd3b85d3ad8d4b1509f0 <= 0)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
					{
						Aabb aabb = cc5fdd9518d3b72c985e2608f434fa386.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, Pose.Identity);
						Vector3F extent = aabb.Extent;
						ced9d1a762c087a2f16a57733d63c1228 = aabb.Center;
						ce602a7340e91bf272bcbe2f132962ab5(extent, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, out c3f34db33979fee2441b0f8e870e3dc40, out c78db0da7e1c88667b788d19027fafe7c);
						return;
					}
					}
				}
			}
			TriangleMesh mesh = cc5fdd9518d3b72c985e2608f434fa386.GetMesh(cec2cf841da966bf6a09d908b7167be17, c11ff2b2cbe9bdd3b85d3ad8d4b1509f0);
			mesh.Transform(Matrix44F.CreateScale(ceabb47d7d959545d89c4300eabbdd7c2));
			GetMass(mesh, out c3f34db33979fee2441b0f8e870e3dc40, out ced9d1a762c087a2f16a57733d63c1228, out c78db0da7e1c88667b788d19027fafe7c);
			if (c6e96ba1bb9ca98c3df80bc5932f765fc)
			{
				float volume = mesh.GetVolume();
				float c3d3b68c83ab1f6fb9111a378721685fe = volume * ccd0e6705e44b13fb907035ec01adf2ca;
				cae3c009d3f523b01b6f7b0bc1587b2fb(c3d3b68c83ab1f6fb9111a378721685fe, ref c3f34db33979fee2441b0f8e870e3dc40, ref c78db0da7e1c88667b788d19027fafe7c);
			}
			else
			{
				cae3c009d3f523b01b6f7b0bc1587b2fb(ccd0e6705e44b13fb907035ec01adf2ca, ref c3f34db33979fee2441b0f8e870e3dc40, ref c78db0da7e1c88667b788d19027fafe7c);
			}
			if (Numeric.IsLessOrEqual(c3f34db33979fee2441b0f8e870e3dc40, 0f))
			{
				ce602a7340e91bf272bcbe2f132962ab5(cc5fdd9518d3b72c985e2608f434fa386, ceabb47d7d959545d89c4300eabbdd7c2, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, cec2cf841da966bf6a09d908b7167be17, -1, out c3f34db33979fee2441b0f8e870e3dc40, out ced9d1a762c087a2f16a57733d63c1228, out c78db0da7e1c88667b788d19027fafe7c);
			}
		}
	}

	private static void ce602a7340e91bf272bcbe2f132962ab5(SphereShape c7eae81cb92cc0ec19982b5fcc4669e26, Vector3F ceabb47d7d959545d89c4300eabbdd7c2, float ccd0e6705e44b13fb907035ec01adf2ca, bool c6e96ba1bb9ca98c3df80bc5932f765fc, out float c3f34db33979fee2441b0f8e870e3dc40, out Matrix33F c78db0da7e1c88667b788d19027fafe7c)
	{
		ceabb47d7d959545d89c4300eabbdd7c2 = Vector3F.Absolute(ceabb47d7d959545d89c4300eabbdd7c2);
		Vector3F vector3F = c7eae81cb92cc0ec19982b5fcc4669e26.Radius * ceabb47d7d959545d89c4300eabbdd7c2;
		Vector3F vector3F2 = vector3F * vector3F;
		float num;
		if (!c6e96ba1bb9ca98c3df80bc5932f765fc)
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
				__ldtoken(MassHelper.ce602a7340e91bf272bcbe2f132962ab5(SphereShape, Vector3F, float, bool, ref float, ref Matrix33F));
			}
			num = ccd0e6705e44b13fb907035ec01adf2ca;
		}
		else
		{
			num = 4.1887903f * vector3F.X * vector3F.Y * vector3F.Z * ccd0e6705e44b13fb907035ec01adf2ca;
		}
		c3f34db33979fee2441b0f8e870e3dc40 = num;
		c78db0da7e1c88667b788d19027fafe7c = Matrix33F.Zero;
		c78db0da7e1c88667b788d19027fafe7c.M00 = 0.2f * c3f34db33979fee2441b0f8e870e3dc40 * (vector3F2.Y + vector3F2.Z);
		c78db0da7e1c88667b788d19027fafe7c.M11 = 0.2f * c3f34db33979fee2441b0f8e870e3dc40 * (vector3F2.X + vector3F2.Z);
		c78db0da7e1c88667b788d19027fafe7c.M22 = 0.2f * c3f34db33979fee2441b0f8e870e3dc40 * (vector3F2.X + vector3F2.Y);
	}

	private static void ce602a7340e91bf272bcbe2f132962ab5(CylinderShape c68893d040f49747959f25c305556cab9, Vector3F ceabb47d7d959545d89c4300eabbdd7c2, float ccd0e6705e44b13fb907035ec01adf2ca, bool c6e96ba1bb9ca98c3df80bc5932f765fc, out float c3f34db33979fee2441b0f8e870e3dc40, out Matrix33F c78db0da7e1c88667b788d19027fafe7c)
	{
		ceabb47d7d959545d89c4300eabbdd7c2 = Vector3F.Absolute(ceabb47d7d959545d89c4300eabbdd7c2);
		float num = c68893d040f49747959f25c305556cab9.Radius * ceabb47d7d959545d89c4300eabbdd7c2.X;
		float num2 = c68893d040f49747959f25c305556cab9.Height * ceabb47d7d959545d89c4300eabbdd7c2.Y;
		float num3 = c68893d040f49747959f25c305556cab9.Radius * ceabb47d7d959545d89c4300eabbdd7c2.Z;
		c3f34db33979fee2441b0f8e870e3dc40 = (c6e96ba1bb9ca98c3df80bc5932f765fc ? ((float)Math.PI * num * num3 * num2 * ccd0e6705e44b13fb907035ec01adf2ca) : ccd0e6705e44b13fb907035ec01adf2ca);
		c78db0da7e1c88667b788d19027fafe7c = Matrix33F.Zero;
		c78db0da7e1c88667b788d19027fafe7c.M00 = 0.25f * c3f34db33979fee2441b0f8e870e3dc40 * num3 * num3 + 1f / 12f * c3f34db33979fee2441b0f8e870e3dc40 * num2 * num2;
		c78db0da7e1c88667b788d19027fafe7c.M11 = 0.25f * c3f34db33979fee2441b0f8e870e3dc40 * num * num + 0.25f * c3f34db33979fee2441b0f8e870e3dc40 * num3 * num3;
		c78db0da7e1c88667b788d19027fafe7c.M22 = 0.25f * c3f34db33979fee2441b0f8e870e3dc40 * num * num + 1f / 12f * c3f34db33979fee2441b0f8e870e3dc40 * num2 * num2;
	}

	private static void ce602a7340e91bf272bcbe2f132962ab5(ConeShape caee48871c24148aa5f5995451608c6e4, Vector3F ceabb47d7d959545d89c4300eabbdd7c2, float ccd0e6705e44b13fb907035ec01adf2ca, bool c6e96ba1bb9ca98c3df80bc5932f765fc, out float c3f34db33979fee2441b0f8e870e3dc40, out Vector3F ced9d1a762c087a2f16a57733d63c1228, out Matrix33F c78db0da7e1c88667b788d19027fafe7c)
	{
		float num = caee48871c24148aa5f5995451608c6e4.Radius * Math.Abs(ceabb47d7d959545d89c4300eabbdd7c2.X);
		float num2 = caee48871c24148aa5f5995451608c6e4.Radius * Math.Abs(ceabb47d7d959545d89c4300eabbdd7c2.Z);
		float num3 = caee48871c24148aa5f5995451608c6e4.Height * Math.Abs(ceabb47d7d959545d89c4300eabbdd7c2.Y);
		float num4;
		if (!c6e96ba1bb9ca98c3df80bc5932f765fc)
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
				__ldtoken(MassHelper.ce602a7340e91bf272bcbe2f132962ab5(ConeShape, Vector3F, float, bool, ref float, ref Vector3F, ref Matrix33F));
			}
			num4 = ccd0e6705e44b13fb907035ec01adf2ca;
		}
		else
		{
			num4 = (float)Math.PI / 3f * num * num2 * num3 * ccd0e6705e44b13fb907035ec01adf2ca;
		}
		c3f34db33979fee2441b0f8e870e3dc40 = num4;
		ced9d1a762c087a2f16a57733d63c1228 = new Vector3F(0f, num3 / 4f, 0f) * Math.Sign(ceabb47d7d959545d89c4300eabbdd7c2.Y);
		c78db0da7e1c88667b788d19027fafe7c = Matrix33F.Zero;
		c78db0da7e1c88667b788d19027fafe7c.M00 = 0.15f * c3f34db33979fee2441b0f8e870e3dc40 * (num2 * num2 + 0.25f * num3 * num3);
		c78db0da7e1c88667b788d19027fafe7c.M11 = 0.15f * c3f34db33979fee2441b0f8e870e3dc40 * (num * num + num2 * num2);
		c78db0da7e1c88667b788d19027fafe7c.M22 = 0.15f * c3f34db33979fee2441b0f8e870e3dc40 * (num * num + 0.25f * num3 * num3);
	}

	private static void ce602a7340e91bf272bcbe2f132962ab5(CapsuleShape cdcf231a27920c19e37c39f711aef6c81, Vector3F ceabb47d7d959545d89c4300eabbdd7c2, float ccd0e6705e44b13fb907035ec01adf2ca, bool c6e96ba1bb9ca98c3df80bc5932f765fc, out float c3f34db33979fee2441b0f8e870e3dc40, out Matrix33F c78db0da7e1c88667b788d19027fafe7c)
	{
		ceabb47d7d959545d89c4300eabbdd7c2 = Vector3F.Absolute(ceabb47d7d959545d89c4300eabbdd7c2);
		Vector3F vector3F = cdcf231a27920c19e37c39f711aef6c81.Radius * ceabb47d7d959545d89c4300eabbdd7c2;
		Vector3F vector3F2 = vector3F * vector3F;
		float num = cdcf231a27920c19e37c39f711aef6c81.Height * ceabb47d7d959545d89c4300eabbdd7c2.Y - 2f * vector3F.Y;
		float num2 = num * num;
		float num3;
		if (!c6e96ba1bb9ca98c3df80bc5932f765fc)
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
				__ldtoken(MassHelper.ce602a7340e91bf272bcbe2f132962ab5(CapsuleShape, Vector3F, float, bool, ref float, ref Matrix33F));
			}
			num3 = ccd0e6705e44b13fb907035ec01adf2ca;
		}
		else
		{
			num3 = (float)Math.PI * vector3F.X * vector3F.Z * (1.3333334f * vector3F.Y + num) * ccd0e6705e44b13fb907035ec01adf2ca;
		}
		c3f34db33979fee2441b0f8e870e3dc40 = num3;
		c78db0da7e1c88667b788d19027fafe7c = Matrix33F.Zero;
		float num4 = 4f * vector3F.Y + 3f * num;
		c78db0da7e1c88667b788d19027fafe7c.M00 = 1f / num4 * c3f34db33979fee2441b0f8e870e3dc40 * (2f * vector3F.Y * (0.4f * (vector3F2.Y + vector3F2.Z) + 0.75f * num * vector3F.Y + 0.5f * num2) + 3f * num * (0.25f * vector3F2.Z + 1f / 12f * num2));
		c78db0da7e1c88667b788d19027fafe7c.M11 = 1f / num4 * c3f34db33979fee2441b0f8e870e3dc40 * (2f * vector3F.Y * 2f / 5f * (vector3F2.X + vector3F2.Z) + 3f * num * 1f / 4f * (vector3F2.X + vector3F2.Z));
		c78db0da7e1c88667b788d19027fafe7c.M22 = 1f / num4 * c3f34db33979fee2441b0f8e870e3dc40 * (2f * vector3F.Y * (0.4f * (vector3F2.X + vector3F2.Y) + 0.75f * num * vector3F.Y + 0.5f * num2) + 3f * num * (0.25f * vector3F2.X + 1f / 12f * num2));
	}

	private static void ce602a7340e91bf272bcbe2f132962ab5(BoxShape c4287aec4a0d60401d2390c21213013a1, Vector3F ceabb47d7d959545d89c4300eabbdd7c2, float ccd0e6705e44b13fb907035ec01adf2ca, bool c6e96ba1bb9ca98c3df80bc5932f765fc, out float c3f34db33979fee2441b0f8e870e3dc40, out Matrix33F c78db0da7e1c88667b788d19027fafe7c)
	{
		ceabb47d7d959545d89c4300eabbdd7c2 = Vector3F.Absolute(ceabb47d7d959545d89c4300eabbdd7c2);
		Vector3F c2cc6f89647063c6af8b1d8c8a61810e = c4287aec4a0d60401d2390c21213013a1.Extent * ceabb47d7d959545d89c4300eabbdd7c2;
		ce602a7340e91bf272bcbe2f132962ab5(c2cc6f89647063c6af8b1d8c8a61810e, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, out c3f34db33979fee2441b0f8e870e3dc40, out c78db0da7e1c88667b788d19027fafe7c);
	}

	private static void ce602a7340e91bf272bcbe2f132962ab5(Vector3F c2cc6f89647063c6af8b1d8c8a61810e7, float ccd0e6705e44b13fb907035ec01adf2ca, bool c6e96ba1bb9ca98c3df80bc5932f765fc, out float c3f34db33979fee2441b0f8e870e3dc40, out Matrix33F c78db0da7e1c88667b788d19027fafe7c)
	{
		Vector3F vector3F = c2cc6f89647063c6af8b1d8c8a61810e7 * c2cc6f89647063c6af8b1d8c8a61810e7;
		if (c6e96ba1bb9ca98c3df80bc5932f765fc)
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
				__ldtoken(MassHelper.ce602a7340e91bf272bcbe2f132962ab5(Vector3F, float, bool, ref float, ref Matrix33F));
			}
			c3f34db33979fee2441b0f8e870e3dc40 = c2cc6f89647063c6af8b1d8c8a61810e7.X * c2cc6f89647063c6af8b1d8c8a61810e7.Y * c2cc6f89647063c6af8b1d8c8a61810e7.Z * ccd0e6705e44b13fb907035ec01adf2ca;
		}
		else
		{
			c3f34db33979fee2441b0f8e870e3dc40 = ccd0e6705e44b13fb907035ec01adf2ca;
		}
		c78db0da7e1c88667b788d19027fafe7c = Matrix33F.Zero;
		c78db0da7e1c88667b788d19027fafe7c.M00 = 1f / 12f * c3f34db33979fee2441b0f8e870e3dc40 * (vector3F.Y + vector3F.Z);
		c78db0da7e1c88667b788d19027fafe7c.M11 = 1f / 12f * c3f34db33979fee2441b0f8e870e3dc40 * (vector3F.X + vector3F.Z);
		c78db0da7e1c88667b788d19027fafe7c.M22 = 1f / 12f * c3f34db33979fee2441b0f8e870e3dc40 * (vector3F.X + vector3F.Y);
	}

	private static void ce602a7340e91bf272bcbe2f132962ab5(IGeometricObject cb68f9e7d713a301d7f5d4ed772a8666f, Vector3F ceabb47d7d959545d89c4300eabbdd7c2, float ccd0e6705e44b13fb907035ec01adf2ca, bool c6e96ba1bb9ca98c3df80bc5932f765fc, float cec2cf841da966bf6a09d908b7167be17, int c11ff2b2cbe9bdd3b85d3ad8d4b1509f0, out float c3f34db33979fee2441b0f8e870e3dc40, out Vector3F ced9d1a762c087a2f16a57733d63c1228, out Matrix33F c78db0da7e1c88667b788d19027fafe7c)
	{
		Pose pose = cb68f9e7d713a301d7f5d4ed772a8666f.Pose;
		if (ceabb47d7d959545d89c4300eabbdd7c2.X == ceabb47d7d959545d89c4300eabbdd7c2.Y)
		{
			if (ceabb47d7d959545d89c4300eabbdd7c2.Y == ceabb47d7d959545d89c4300eabbdd7c2.Z)
			{
				goto IL_005d;
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
			if (1 == 0)
			{
				__ldtoken(MassHelper.ce602a7340e91bf272bcbe2f132962ab5(IGeometricObject, Vector3F, float, bool, float, int, ref float, ref Vector3F, ref Matrix33F));
			}
		}
		if (pose.HasRotation)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2034));
				}
			}
		}
		goto IL_005d;
		IL_005d:
		if (!(ceabb47d7d959545d89c4300eabbdd7c2.X < 0f) && !(ceabb47d7d959545d89c4300eabbdd7c2.Y < 0f))
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
			if (!(ceabb47d7d959545d89c4300eabbdd7c2.Z < 0f))
			{
				Shape shape = cb68f9e7d713a301d7f5d4ed772a8666f.Shape;
				Vector3F ceabb47d7d959545d89c4300eabbdd7c3 = ceabb47d7d959545d89c4300eabbdd7c2 * cb68f9e7d713a301d7f5d4ed772a8666f.Scale;
				Vector3F ced9d1a762c087a2f16a57733d63c1229;
				Matrix33F c78db0da7e1c88667b788d19027fafe7c2;
				if (cb68f9e7d713a301d7f5d4ed772a8666f is RigidBody)
				{
					if (!Vector3F.AreNumericallyEqual(ceabb47d7d959545d89c4300eabbdd7c2, Vector3F.One))
					{
						throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2309));
					}
					RigidBody rigidBody = (RigidBody)cb68f9e7d713a301d7f5d4ed772a8666f;
					MassFrame massFrame = rigidBody.MassFrame;
					c3f34db33979fee2441b0f8e870e3dc40 = massFrame.Mass;
					ced9d1a762c087a2f16a57733d63c1229 = massFrame.Pose.Position;
					c78db0da7e1c88667b788d19027fafe7c2 = massFrame.Pose.Orientation * Matrix33F.CreateScale(massFrame.Inertia) * massFrame.Pose.Orientation.Transposed;
				}
				else
				{
					ce602a7340e91bf272bcbe2f132962ab5(shape, ceabb47d7d959545d89c4300eabbdd7c3, ccd0e6705e44b13fb907035ec01adf2ca, c6e96ba1bb9ca98c3df80bc5932f765fc, cec2cf841da966bf6a09d908b7167be17, c11ff2b2cbe9bdd3b85d3ad8d4b1509f0, out c3f34db33979fee2441b0f8e870e3dc40, out ced9d1a762c087a2f16a57733d63c1229, out c78db0da7e1c88667b788d19027fafe7c2);
				}
				c78db0da7e1c88667b788d19027fafe7c = pose.Orientation * c78db0da7e1c88667b788d19027fafe7c2 * pose.Orientation.Transposed;
				ced9d1a762c087a2f16a57733d63c1228 = pose.ToWorldPosition(ced9d1a762c087a2f16a57733d63c1229) * ceabb47d7d959545d89c4300eabbdd7c2;
				return;
			}
		}
		throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2238));
	}

	private static void cae3c009d3f523b01b6f7b0bc1587b2fb(float c3d3b68c83ab1f6fb9111a378721685fe, ref float c3f34db33979fee2441b0f8e870e3dc40, ref Matrix33F c78db0da7e1c88667b788d19027fafe7c)
	{
		float num = c3d3b68c83ab1f6fb9111a378721685fe / c3f34db33979fee2441b0f8e870e3dc40;
		c3f34db33979fee2441b0f8e870e3dc40 = c3d3b68c83ab1f6fb9111a378721685fe;
		c78db0da7e1c88667b788d19027fafe7c *= num;
	}

	internal static void c5df24c6b4c413e9d29a3b279b5924a1e(Matrix33F c78db0da7e1c88667b788d19027fafe7c, out Vector3F c0c11e9545303c1a548d0ac043c73a14d, out Matrix33F caaabbb14cb24c5c3c46d7f2f0ef1e286)
	{
		EigenvalueDecompositionF eigenvalueDecompositionF = new EigenvalueDecompositionF(c78db0da7e1c88667b788d19027fafe7c.ToMatrixF());
		c0c11e9545303c1a548d0ac043c73a14d = eigenvalueDecompositionF.RealEigenvalues.ToVector3F();
		caaabbb14cb24c5c3c46d7f2f0ef1e286 = eigenvalueDecompositionF.V.ToMatrix33F();
		if (!caaabbb14cb24c5c3c46d7f2f0ef1e286.IsRotation)
		{
			MathHelper.Swap(ref c0c11e9545303c1a548d0ac043c73a14d.Y, ref c0c11e9545303c1a548d0ac043c73a14d.Z);
			Vector3F column = caaabbb14cb24c5c3c46d7f2f0ef1e286.GetColumn(1);
			caaabbb14cb24c5c3c46d7f2f0ef1e286.SetColumn(1, caaabbb14cb24c5c3c46d7f2f0ef1e286.GetColumn(2));
			caaabbb14cb24c5c3c46d7f2f0ef1e286.SetColumn(2, column);
		}
	}

	private static Matrix33F cc89638179b4e395fbb50b93d2d63c3af(float c3f34db33979fee2441b0f8e870e3dc40, Matrix33F c78db0da7e1c88667b788d19027fafe7c, Vector3F c7b5cd43ee9fcfeb3488fcdfbb54d95bf)
	{
		Vector3F vector3F = c7b5cd43ee9fcfeb3488fcdfbb54d95bf * c7b5cd43ee9fcfeb3488fcdfbb54d95bf;
		c78db0da7e1c88667b788d19027fafe7c.M00 += c3f34db33979fee2441b0f8e870e3dc40 * (vector3F.Y + vector3F.Z);
		c78db0da7e1c88667b788d19027fafe7c.M11 += c3f34db33979fee2441b0f8e870e3dc40 * (vector3F.X + vector3F.Z);
		c78db0da7e1c88667b788d19027fafe7c.M22 += c3f34db33979fee2441b0f8e870e3dc40 * (vector3F.X + vector3F.Y);
		c78db0da7e1c88667b788d19027fafe7c.M01 -= c3f34db33979fee2441b0f8e870e3dc40 * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.X * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.Y;
		c78db0da7e1c88667b788d19027fafe7c.M02 -= c3f34db33979fee2441b0f8e870e3dc40 * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.X * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.Z;
		c78db0da7e1c88667b788d19027fafe7c.M12 -= c3f34db33979fee2441b0f8e870e3dc40 * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.Y * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.Z;
		c78db0da7e1c88667b788d19027fafe7c.M10 = c78db0da7e1c88667b788d19027fafe7c.M01;
		c78db0da7e1c88667b788d19027fafe7c.M20 = c78db0da7e1c88667b788d19027fafe7c.M02;
		c78db0da7e1c88667b788d19027fafe7c.M21 = c78db0da7e1c88667b788d19027fafe7c.M12;
		return c78db0da7e1c88667b788d19027fafe7c;
	}

	private static Matrix33F c9e8310d9fa927caa9c83af06a685c8a3(float c3f34db33979fee2441b0f8e870e3dc40, Matrix33F c78db0da7e1c88667b788d19027fafe7c, Vector3F c7b5cd43ee9fcfeb3488fcdfbb54d95bf)
	{
		if (c7b5cd43ee9fcfeb3488fcdfbb54d95bf.IsNumericallyZero)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(MassHelper.c9e8310d9fa927caa9c83af06a685c8a3(float, Matrix33F, Vector3F));
					}
					return c78db0da7e1c88667b788d19027fafe7c;
				}
			}
		}
		Vector3F vector3F = c7b5cd43ee9fcfeb3488fcdfbb54d95bf * c7b5cd43ee9fcfeb3488fcdfbb54d95bf;
		c78db0da7e1c88667b788d19027fafe7c.M00 -= c3f34db33979fee2441b0f8e870e3dc40 * (vector3F.Y + vector3F.Z);
		c78db0da7e1c88667b788d19027fafe7c.M11 -= c3f34db33979fee2441b0f8e870e3dc40 * (vector3F.X + vector3F.Z);
		c78db0da7e1c88667b788d19027fafe7c.M22 -= c3f34db33979fee2441b0f8e870e3dc40 * (vector3F.X + vector3F.Y);
		c78db0da7e1c88667b788d19027fafe7c.M01 += c3f34db33979fee2441b0f8e870e3dc40 * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.X * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.Y;
		c78db0da7e1c88667b788d19027fafe7c.M02 += c3f34db33979fee2441b0f8e870e3dc40 * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.X * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.Z;
		c78db0da7e1c88667b788d19027fafe7c.M12 += c3f34db33979fee2441b0f8e870e3dc40 * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.Y * c7b5cd43ee9fcfeb3488fcdfbb54d95bf.Z;
		c78db0da7e1c88667b788d19027fafe7c.M10 = c78db0da7e1c88667b788d19027fafe7c.M01;
		c78db0da7e1c88667b788d19027fafe7c.M20 = c78db0da7e1c88667b788d19027fafe7c.M02;
		c78db0da7e1c88667b788d19027fafe7c.M21 = c78db0da7e1c88667b788d19027fafe7c.M12;
		return c78db0da7e1c88667b788d19027fafe7c;
	}

	private static void c1ea3b5f424b26b86097150ef08bf322a(float ce96632c2be8b2f693f03406f1d538813, float cc4208267ec38e0ec185683e16733ee4d, float ce28714bca13958f3c8fc8bb963db2cc2, out float cb711eed3851cb15f7f3686118c2ecc44, out float c470ab79116d294d9d3876c7732062e2f, out float c056aaf6e77519eb6f21d607ec695ec49, out float c06694d21955a553a292d37ecd7effe27, out float ce4c53a4d30ae7ab922f5ca6e3cd477b0, out float c05d7c47a92a854f1ba5c18b626bcb632)
	{
		float num = ce96632c2be8b2f693f03406f1d538813 + cc4208267ec38e0ec185683e16733ee4d;
		cb711eed3851cb15f7f3686118c2ecc44 = num + ce28714bca13958f3c8fc8bb963db2cc2;
		float num2 = ce96632c2be8b2f693f03406f1d538813 * ce96632c2be8b2f693f03406f1d538813;
		float num3 = num2 + cc4208267ec38e0ec185683e16733ee4d * num;
		c470ab79116d294d9d3876c7732062e2f = num3 + ce28714bca13958f3c8fc8bb963db2cc2 * cb711eed3851cb15f7f3686118c2ecc44;
		c056aaf6e77519eb6f21d607ec695ec49 = ce96632c2be8b2f693f03406f1d538813 * num2 + cc4208267ec38e0ec185683e16733ee4d * num3 + ce28714bca13958f3c8fc8bb963db2cc2 * c470ab79116d294d9d3876c7732062e2f;
		c06694d21955a553a292d37ecd7effe27 = c470ab79116d294d9d3876c7732062e2f + ce96632c2be8b2f693f03406f1d538813 * (cb711eed3851cb15f7f3686118c2ecc44 + ce96632c2be8b2f693f03406f1d538813);
		ce4c53a4d30ae7ab922f5ca6e3cd477b0 = c470ab79116d294d9d3876c7732062e2f + cc4208267ec38e0ec185683e16733ee4d * (cb711eed3851cb15f7f3686118c2ecc44 + cc4208267ec38e0ec185683e16733ee4d);
		c05d7c47a92a854f1ba5c18b626bcb632 = c470ab79116d294d9d3876c7732062e2f + ce28714bca13958f3c8fc8bb963db2cc2 * (cb711eed3851cb15f7f3686118c2ecc44 + ce28714bca13958f3c8fc8bb963db2cc2);
	}

	/// <summary>
	///       Gets the mass properties of the given triangle mesh for a density of 1.
	///       </summary>
	/// <param name="mesh">The mesh.</param>
	/// <param name="mass">The mass.</param>
	/// <param name="centerOfMass">The center of mass.</param>
	/// <param name="inertia">The inertia matrix.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="mesh" /> is <see langword="null" />.
	///       </exception>
	public static void GetMass(ITriangleMesh mesh, out float mass, out Vector3F centerOfMass, out Matrix33F inertia)
	{
		if (mesh == null)
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
						__ldtoken(MassHelper.GetMass(ITriangleMesh, ref float, ref Vector3F, ref Matrix33F));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2451));
				}
			}
		}
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		int numberOfTriangles = mesh.NumberOfTriangles;
		for (int i = 0; i < numberOfTriangles; i++)
		{
			Triangle triangle = mesh.GetTriangle(i);
			Vector3F vertex = triangle.Vertex0;
			Vector3F vertex2 = triangle.Vertex1;
			Vector3F vertex3 = triangle.Vertex2;
			Vector3F vector = vertex2 - vertex;
			Vector3F vector2 = vertex3 - vertex;
			Vector3F vector3F = Vector3F.Cross(vector, vector2);
			c1ea3b5f424b26b86097150ef08bf322a(vertex.X, vertex2.X, vertex3.X, out var cb711eed3851cb15f7f3686118c2ecc, out var c470ab79116d294d9d3876c7732062e2f, out var c056aaf6e77519eb6f21d607ec695ec, out var c06694d21955a553a292d37ecd7effe, out var ce4c53a4d30ae7ab922f5ca6e3cd477b, out var c05d7c47a92a854f1ba5c18b626bcb);
			c1ea3b5f424b26b86097150ef08bf322a(vertex.Y, vertex2.Y, vertex3.Y, out var _, out var c470ab79116d294d9d3876c7732062e2f2, out var c056aaf6e77519eb6f21d607ec695ec2, out var c06694d21955a553a292d37ecd7effe2, out var ce4c53a4d30ae7ab922f5ca6e3cd477b2, out var c05d7c47a92a854f1ba5c18b626bcb2);
			c1ea3b5f424b26b86097150ef08bf322a(vertex.Z, vertex2.Z, vertex3.Z, out var _, out var c470ab79116d294d9d3876c7732062e2f3, out var c056aaf6e77519eb6f21d607ec695ec3, out var c06694d21955a553a292d37ecd7effe3, out var ce4c53a4d30ae7ab922f5ca6e3cd477b3, out var c05d7c47a92a854f1ba5c18b626bcb3);
			num += vector3F.X * cb711eed3851cb15f7f3686118c2ecc;
			num2 += vector3F.X * c470ab79116d294d9d3876c7732062e2f;
			num3 += vector3F.Y * c470ab79116d294d9d3876c7732062e2f2;
			num4 += vector3F.Z * c470ab79116d294d9d3876c7732062e2f3;
			num5 += vector3F.X * c056aaf6e77519eb6f21d607ec695ec;
			num6 += vector3F.Y * c056aaf6e77519eb6f21d607ec695ec2;
			num7 += vector3F.Z * c056aaf6e77519eb6f21d607ec695ec3;
			num8 += vector3F.X * (vertex.Y * c06694d21955a553a292d37ecd7effe + vertex2.Y * ce4c53a4d30ae7ab922f5ca6e3cd477b + vertex3.Y * c05d7c47a92a854f1ba5c18b626bcb);
			num9 += vector3F.Y * (vertex.Z * c06694d21955a553a292d37ecd7effe2 + vertex2.Z * ce4c53a4d30ae7ab922f5ca6e3cd477b2 + vertex3.Z * c05d7c47a92a854f1ba5c18b626bcb2);
			num10 += vector3F.Z * (vertex.X * c06694d21955a553a292d37ecd7effe3 + vertex2.X * ce4c53a4d30ae7ab922f5ca6e3cd477b3 + vertex3.X * c05d7c47a92a854f1ba5c18b626bcb3);
		}
		num /= 6f;
		num2 /= 24f;
		num3 /= 24f;
		num4 /= 24f;
		num5 /= 60f;
		num6 /= 60f;
		num7 /= 60f;
		num8 /= 120f;
		num9 /= 120f;
		num10 /= 120f;
		mass = num;
		centerOfMass = 1f / mass * new Vector3F(num2, num3, num4);
		if (Numeric.IsZero(centerOfMass.X))
		{
			centerOfMass.X = 0f;
		}
		if (Numeric.IsZero(centerOfMass.Y))
		{
			centerOfMass.Y = 0f;
		}
		if (Numeric.IsZero(centerOfMass.Z))
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
			centerOfMass.Z = 0f;
		}
		inertia.M00 = num6 + num7;
		inertia.M11 = num5 + num7;
		inertia.M22 = num5 + num6;
		inertia.M01 = (inertia.M10 = (Numeric.IsZero(num8) ? 0f : (0f - num8)));
		float num11;
		if (!Numeric.IsZero(num9))
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
			num11 = 0f - num9;
		}
		else
		{
			num11 = 0f;
		}
		float m = num11;
		inertia.M21 = num11;
		inertia.M12 = m;
		float num12;
		if (!Numeric.IsZero(num10))
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
			num12 = 0f - num10;
		}
		else
		{
			num12 = 0f;
		}
		float m2 = num12;
		inertia.M20 = num12;
		inertia.M02 = m2;
		inertia = c9e8310d9fa927caa9c83af06a685c8a3(mass, inertia, centerOfMass);
	}
}
