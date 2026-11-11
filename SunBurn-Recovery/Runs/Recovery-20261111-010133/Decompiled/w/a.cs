using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace w
{
	internal class a
	{
		public string Name = "";

		public string InnerText = "";

		public l Attributes = new l();

		public _u0013_ ChildNodes = new _u0013_();

		private static char[] l8a = new char[1] { '/' };

		public N GetAttributeNode(string name)
		{
			foreach (N attribute in Attributes)
			{
				if (attribute.Name == name)
				{
					return attribute;
				}
			}
			return null;
		}

		public a SelectSingleNode(string limited_xpath)
		{
			_u0013_ u0013_ = SelectNodes(limited_xpath);
			if (u0013_.Count > 0)
			{
				return u0013_[0];
			}
			return null;
		}

		public _u0013_ SelectNodes(string limited_xpath)
		{
			string[] array = limited_xpath.Split(l8a);
			_u0013_ u0013_ = new _u0013_();
			if (array.Length > 0)
			{
				Q(ChildNodes, u0013_, array, 0);
			}
			return u0013_;
		}

		private static void Q(_u0013_ param_0, _u0013_ param_1, string[] param_2, int param_3)
		{
			bool flag = param_2.Length - 1 <= param_3;
			string text = param_2[param_3];
			if (string.IsNullOrEmpty(text))
			{
				throw new Exception("Wp7 and Xbox minimal Xpath support does not include absolute paths.");
			}
			if (text == ".." || text == ".")
			{
				throw new Exception("Wp7 and Xbox minimal Xpath support does not include '..' and '.' paths.");
			}
			foreach (a item in param_0)
			{
				if (!(item.Name != text))
				{
					if (flag)
					{
						param_1.Add(item);
					}
					else
					{
						Q(item.ChildNodes, param_1, param_2, param_3 + 1);
					}
				}
			}
		}

		public virtual void Load(XObject node)
		{
			Attributes.Clear();
			ChildNodes.Clear();
			if (node is XAttribute)
			{
				XAttribute xAttribute = node as XAttribute;
				Name = xAttribute.Name.LocalName;
				InnerText = xAttribute.Value;
			}
			else
			{
				if (!(node is XElement))
				{
					return;
				}
				XElement xElement = node as XElement;
				Name = xElement.Name.LocalName;
				if (xElement.HasAttributes)
				{
					IEnumerable<XAttribute> enumerable = xElement.Attributes();
					foreach (XAttribute item in enumerable)
					{
						N n = new N();
						Attributes.Add(n);
						n.Load(item);
					}
				}
				if (xElement.HasElements)
				{
					IEnumerable<XElement> enumerable2 = xElement.Elements();
					{
						foreach (XElement item2 in enumerable2)
						{
							L l2 = new L();
							ChildNodes.Add(l2);
							l2.Load(item2);
						}
						return;
					}
				}
				InnerText = xElement.Value;
			}
		}
	}
}
namespace W
{
	internal class a
	{
		internal const string l8a = "UniqueId";

		internal const string l8_u0013_ = "Name";

		internal const string l8N = "Enabled";

		internal const string l8l = "UpdateType";

		internal const string l8L = "LightingType";

		internal const string l8w = "DiffuseColor";

		internal const string l8J = "Intensity";

		internal const string l8B = "FillLight";

		internal const string l8s = "FalloffStrength";

		internal const string l8h = "ShadowSource";

		internal const string l8_u0002_ = "ShadowType";

		internal const string l8v = "Position";

		internal const string l8Y = "Radius";

		internal const string l8_u0005_ = "Direction";

		internal const string l8b = "Angle";

		internal const string l8I = "Volume";

		internal const string l85 = "Depth";

		internal const string l8W = "ShadowQuality";

		internal const string l8Z = "ShadowPrimaryBias";

		internal const string l8T = "ShadowSecondaryBias";

		internal const string l8_u0014_ = "ShadowPerSurfaceLOD";

		internal const string l8E = "ShadowRenderLightsTogether";
	}
}
