using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune;

/// <summary>
///       Provides an <see cref="T:System.IServiceProvider" /> for global services.
///       </summary>
public static class ServiceManager
{
	[CompilerGenerated]
	private static IServiceProvider c89640918df51f31910d3f433a3b06f81;

	/// <summary>
	///       Gets or sets the global services.
	///       </summary>
	public static IServiceProvider GlobalServices
	{
		[CompilerGenerated]
		get
		{
			return c89640918df51f31910d3f433a3b06f81;
		}
		[CompilerGenerated]
		set
		{
			c89640918df51f31910d3f433a3b06f81 = value;
		}
	}

	/// <summary>
	///       Gets a service from the global service provider. If the global service provider does not 
	///       exist (<see cref="P:DigitalRune.ServiceManager.GlobalServices" /> is <see langword="null" />)
	///       or does not contain the service an <see cref="T:DigitalRune.ServiceNotFoundException" /> is thrown.
	///       </summary>
	/// <typeparam name="T">The service type.</typeparam>
	/// <returns>The service.</returns>
	/// <exception cref="T:DigitalRune.ServiceNotFoundException">
	///       The provider does not contain a service of type <typeparamref name="T" />.
	///       </exception>
	public static T GetGlobalService<T>()
	{
		if (GlobalServices == null)
		{
			throw new ServiceNotFoundException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6752));
		}
		return GlobalServices.GetService<T>();
	}

	/// <summary>
	///       Gets a service from a service provider. If the provider does not contain 
	///       the service an <see cref="T:DigitalRune.ServiceNotFoundException" /> is thrown.
	///       </summary>
	/// <param name="serviceProvider">The service provider.</param>
	/// <typeparam name="T">The service type.</typeparam>
	/// <returns>The service.</returns>
	/// <exception cref="T:DigitalRune.ServiceNotFoundException">
	///       The provider does not contain a service of type <typeparamref name="T" />.
	///       </exception>
	public static T GetService<T>(this IServiceProvider serviceProvider)
	{
		object service = serviceProvider.GetService(typeof(T));
		if (service == null)
		{
			throw new ServiceNotFoundException(typeof(T));
		}
		return (T)service;
	}

	/// <summary>
	///       Tries to get a service from a service provider.
	///       </summary>
	/// <param name="serviceProvider">The service provider.</param>
	/// <param name="service">
	///       The requested service, or <see langword="null" /> if the service is not available.
	///       </param>
	/// <typeparam name="T">The service type.</typeparam>
	/// <returns>
	///   <see langword="true" /> if the service is available; otherwise, <see langword="false" />.
	///       </returns>
	public static bool TryGetService<T>(this IServiceProvider serviceProvider, out T service)
	{
		object service2 = serviceProvider.GetService(typeof(T));
		if (service2 != null)
		{
			service = (T)service2;
			return true;
		}
		service = default;
		return false;
	}
}
