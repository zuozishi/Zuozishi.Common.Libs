using System.Reflection;
using Zuozishi.Common.Libs.AspNetCore.Attributes;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllServices(this IServiceCollection services)
    {
		var assembiles = AppDomain.CurrentDomain.GetAssemblies();
		foreach (var assembly in assembiles)
		{
			var types = assembly.GetTypes();
			foreach (var type in types)
			{
				var attr = type.GetCustomAttribute<ServiceAttribute>();
				if (attr == null) continue;
				switch (attr.Lifetime)
				{
					case ServiceLifetime.Singleton:
						if (attr.Interface == null)
							services.AddSingleton(type);
						else
							services.AddSingleton(attr.Interface, type);
						break;
					case ServiceLifetime.Scoped:
						if (attr.Interface == null)
							services.AddScoped(type);
						else
							services.AddScoped(attr.Interface, type);
						break;
					case ServiceLifetime.Transient:
						if (attr.Interface == null)
							services.AddTransient(type);
						else
							services.AddTransient(attr.Interface, type);
						break;
					default:
						break;
				}
			}
		}
		return services;
	}
}