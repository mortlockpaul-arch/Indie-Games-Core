using System;
using System.Collections;
using System.Collections.Generic;
using A;

namespace DigitalRune;

/// <summary>
///       Manages a collection of services.
///       </summary>
public class ServiceProvider : IEnumerable, IServiceProvider
{
	private readonly Dictionary<Type, object> cfe20a248580a51e43b4175106c4ae1ba = new Dictionary<Type, object>();

	/// <summary>
	///       Gets an object that can be used to synchronize access to the <see cref="T:DigitalRune.ServiceProvider" />.
	///       </summary>
	public object SyncRoot => ((ICollection)cfe20a248580a51e43b4175106c4ae1ba).SyncRoot;

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.ServiceProvider" /> class</summary>
	public ServiceProvider()
	{
	}

	/// <summary>
	///       Adds a service to the service container.
	///       </summary>
	/// <param name="type">The type of service to add.</param>
	/// <param name="service">The service to add.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///       Either <paramref name="type" /> or <paramref name="service" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The service container already contains a service of this type. Or, the 
	///       <paramref name="service" /> is not a class (or subclass) of <paramref name="type" />.
	///       </exception>
	public void AddService(Type type, object service)
	{
		if ((object)type == null)
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
						__ldtoken(ServiceProvider.AddService(Type, object));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1802), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
				}
			}
		}
		if (service == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1864), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1879));
				}
			}
		}
		if (!type.IsAssignableFrom(service.GetType()))
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
				{
					string message = string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1958), service.GetType().Name, type.Name);
					throw new ArgumentException(message, c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1864));
				}
				}
			}
		}
		lock (SyncRoot)
		{
			if (cfe20a248580a51e43b4175106c4ae1ba.ContainsKey(type))
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
					{
						string message2 = string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2094), type.Name);
						throw new ArgumentException(message2, c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1802));
					}
					}
				}
			}
			cfe20a248580a51e43b4175106c4ae1ba.Add(type, service);
		}
	}

	/// <summary>
	///       Removes a service from the service container.
	///       </summary>
	/// <param name="type">The type of the service to remove.</param>
	/// <returns>
	///   <see langword="true" /> if the service is successfully found and removed; otherwise, 
	///       <see langword="false" />. This method returns <see langword="false" /> if service of 
	///       <paramref name="type" /> is not found in the <see cref="T:DigitalRune.ServiceProvider" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.
	///       </exception>
	public bool RemoveService(Type type)
	{
		lock (SyncRoot)
		{
			return cfe20a248580a51e43b4175106c4ae1ba.Remove(type);
		}
	}

	/// <summary>
	///       Gets the service object of the specified type.
	///       </summary>
	/// <param name="serviceType">
	///       An object that specifies the type of service object to get.
	///       </param>
	/// <returns>
	///       A service object of type <paramref name="serviceType" />; or <see langword="null" /> if there 
	///       is no service object of that type.
	///       </returns>
	public object GetService(Type serviceType)
	{
		lock (SyncRoot)
		{
			cfe20a248580a51e43b4175106c4ae1ba.TryGetValue(serviceType, out var value);
			return value;
		}
	}

	/// <summary>
	///       Returns an enumerator that iterates through a collection.
	///       </summary>
	/// <returns>
	///       An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
	///       </returns>
	public Dictionary<Type, object>.ValueCollection.Enumerator GetEnumerator()
	{
		return cfe20a248580a51e43b4175106c4ae1ba.Values.GetEnumerator();
	}

	private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
		return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
	}
}
