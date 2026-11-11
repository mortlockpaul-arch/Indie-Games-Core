using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Serialization;
using w;

namespace x
{
	internal class a<TManager, TObject> where TManager : ISubmit<TObject>
	{
		private TManager l8a;

		private List<TObject> l8_u0013_ = new List<TObject>(16);

		public TManager ContainingManager => l8a;

		public void SetManager(TManager manager)
		{
			if (l8a != null)
			{
				if (manager != null)
				{
					throw new Exception("Unable to assign scene to more than one " + typeof(TManager).Name + ". Remove from the previous manager first.");
				}
				RemoveSubmittedObjects();
			}
			l8a = manager;
		}

		public void Submit(TObject obj)
		{
			if (l8a != null)
			{
				l8a.Submit(obj);
				l8_u0013_.Add(obj);
			}
		}

		public void RemoveSubmittedObjects()
		{
			if (l8a == null)
			{
				return;
			}
			foreach (TObject item in l8_u0013_)
			{
				l8a.Remove(item);
			}
			l8_u0013_.Clear();
		}

		public void Optimize()
		{
			if (l8a is IWorldRenderableManager)
			{
				(l8a as IWorldRenderableManager).Optimize();
			}
		}
	}
}
namespace X
{
	internal class a
	{
		private StreamingContext l8a = default;

		private FormatterConverter l8_u0013_ = new FormatterConverter();

		private Dictionary<string, int> l8N = new Dictionary<string, int>(16);

		private l l8l = new l();

		internal object LG(global::w.w param_0)
		{
			l8l._id_3();
			w.L documentElement = param_0.DocumentElement;
			if (documentElement.Name == "root")
			{
				w._u0013_ childNodes = documentElement.ChildNodes;
				int count = childNodes.Count;
				for (int i = 0; i < count; i++)
				{
					w.a a2 = childNodes[i];
					if (a2.Name != "classes")
					{
						continue;
					}
					w._u0013_ childNodes2 = a2.ChildNodes;
					int count2 = childNodes2.Count;
					for (int j = 0; j < count2; j++)
					{
						if (childNodes2[j] is w.L l2)
						{
							Type param_1 = Lu(l2);
							l8l._u0006_(l2.Name, param_1);
						}
					}
				}
				l8l.L_u0006_();
				for (int k = 0; k < count; k++)
				{
					w.a a3 = childNodes[k];
					if (!(a3.Name == "classes") && a3 is w.L)
					{
						return LG(a3 as w.L);
					}
				}
			}
			return null;
		}

		private Type Lu(w.L param_0)
		{
			string name = param_0.Name;
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			w.N n = (w.N)param_0.Attributes.GetNamedItem("fullname");
			w.N n2 = (w.N)param_0.Attributes.GetNamedItem("assembly");
			w.N n3 = (w.N)param_0.Attributes.GetNamedItem("fullyqualifiedname");
			if (n == null || n2 == null)
			{
				return null;
			}
			string innerText = n.InnerText;
			string innerText2 = n2.InnerText;
			string param_1 = "";
			if (n3 != null)
			{
				param_1 = n3.InnerText;
			}
			Type type = l8l.Le(innerText, innerText2, param_1);
			w._u0013_ childNodes = param_0.ChildNodes;
			int count = childNodes.Count;
			if ((object)type == null || count < 1)
			{
				return type;
			}
			Type[] array = new Type[count];
			for (int i = 0; i < array.Length; i++)
			{
				if (childNodes[i] is w.L param_2)
				{
					array[i] = Lu(param_2);
				}
			}
			return type.MakeGenericType(array);
		}

		private object LG(w.L param_0)
		{
			if (param_0 == null || string.IsNullOrEmpty(param_0.Name))
			{
				return null;
			}
			Type type = null;
			l8l.LU(param_0.Name, out var param_1, out var param_2);
			if (param_1.IsGenericType)
			{
				type = param_1.GetGenericTypeDefinition();
			}
			object obj = null;
			ConstructorInfo[] constructors = param_1.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			ConstructorInfo[] array = constructors;
			foreach (ConstructorInfo constructorInfo in array)
			{
				ParameterInfo[] parameters = constructorInfo.GetParameters();
				if (parameters == null || parameters.Length <= 0)
				{
					obj = constructorInfo.Invoke(null);
					break;
				}
			}
			if (obj == null)
			{
				obj = Activator.CreateInstance(param_1);
			}
			if (obj == null)
			{
				throw new Exception($"Cannot create an instance of type '{param_1.FullName}'.");
			}
			SerializationInfo serializationInfo = new SerializationInfo(param_1, l8_u0013_);
			foreach (w.a childNode in param_0.ChildNodes)
			{
				if (!(childNode is w.L) || string.IsNullOrEmpty(childNode.Name))
				{
					continue;
				}
				bool flag = false;
				foreach (w.a childNode2 in childNode.ChildNodes)
				{
					if (childNode2 is w.L)
					{
						serializationInfo.AddValue(childNode.Name, LG(childNode2 as w.L));
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					string name = XmlConvert.DecodeName(childNode.Name);
					serializationInfo.AddValue(name, childNode.InnerText);
				}
			}
			if ((object)type == typeof(KeyValuePair<, >))
			{
				Type[] genericArguments = param_1.GetGenericArguments();
				ConstructorInfo constructor = param_1.GetConstructor(genericArguments);
				if ((object)constructor != null)
				{
					object value = serializationInfo.GetValue("key", genericArguments[0]);
					object value2 = serializationInfo.GetValue("value", genericArguments[1]);
					obj = constructor.Invoke(new object[2] { value, value2 });
				}
			}
			else if (obj is ICollection)
			{
				IList list = obj as IList;
				IDictionary dictionary = obj as IDictionary;
				if (list != null)
				{
					Type type2 = list.GetType();
					Type datatype = typeof(object);
					if (type2.IsGenericType)
					{
						Type[] genericArguments2 = type2.GetGenericArguments();
						if (genericArguments2.Length > 0)
						{
							datatype = genericArguments2[0];
						}
					}
					foreach (SerializationEntry item in serializationInfo)
					{
						object value3 = serializationInfo.GetValue(item.Name, datatype);
						list.Add(value3);
					}
				}
				else if (dictionary != null)
				{
					foreach (SerializationEntry item2 in serializationInfo)
					{
						object value4 = item2.Value;
						Type type3 = value4.GetType();
						PropertyInfo property = type3.GetProperty("Key");
						PropertyInfo property2 = type3.GetProperty("Value");
						if ((object)property == null || (object)property2 == null)
						{
							throw new Exception("Object inserted into Dictionary member is not a KeyValuePair type.");
						}
						object value5 = property.GetValue(value4, null);
						object value6 = property2.GetValue(value4, null);
						dictionary.Add(value5, value6);
					}
				}
			}
			else if (obj is IFullSerializable)
			{
				(obj as IFullSerializable).SetObjectData(serializationInfo, l8a);
			}
			else if (param_2.Length > 0)
			{
				l8N.Clear();
				for (int j = 0; j < param_2.Length; j++)
				{
					if (!l8N.ContainsKey(param_2[j].Name))
					{
						l8N.Add(param_2[j].Name, j);
					}
				}
				foreach (SerializationEntry item3 in serializationInfo)
				{
					if (!l8N.ContainsKey(item3.Name))
					{
						continue;
					}
					int num = l8N[item3.Name];
					MemberInfo memberInfo = param_2[num];
					if (memberInfo is PropertyInfo)
					{
						PropertyInfo propertyInfo = memberInfo as PropertyInfo;
						MethodInfo setMethod = propertyInfo.GetSetMethod(nonPublic: true);
						if (propertyInfo.CanWrite && !setMethod.IsPrivate && !setMethod.IsFamily)
						{
							object value7 = ((!propertyInfo.PropertyType.IsEnum) ? serializationInfo.GetValue(item3.Name, propertyInfo.PropertyType) : Enum.Parse(propertyInfo.PropertyType, (string)serializationInfo.GetValue(item3.Name, typeof(string)), ignoreCase: false));
							propertyInfo.SetValue(obj, value7, null);
						}
					}
					else if (memberInfo is FieldInfo)
					{
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						if (!fieldInfo.IsPrivate && !fieldInfo.IsFamily)
						{
							object value8 = ((!fieldInfo.FieldType.IsEnum) ? serializationInfo.GetValue(item3.Name, fieldInfo.FieldType) : Enum.Parse(fieldInfo.FieldType, (string)serializationInfo.GetValue(item3.Name, typeof(string)), ignoreCase: false));
							fieldInfo.SetValue(obj, value8);
						}
					}
				}
			}
			return obj;
		}
	}
}
