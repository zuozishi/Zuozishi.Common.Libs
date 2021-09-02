using Zuozishi.Common.Libs.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ZuozishiServiceCollectionExtensions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services)
        {
            var assembiles = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assembiles)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    var attrs = type.GetCustomAttributes(typeof(ServiceAttribute), false);
                    if (attrs.Length > 0)
                    {
                        var attrObj = attrs[0] as ServiceAttribute;
                        switch (attrObj.Lifetime)
                        {
                            case ServiceLifetime.Singleton:
                                services.AddSingleton(type);
                                break;
                            case ServiceLifetime.Scoped:
                                services.AddScoped(type);
                                break;
                            case ServiceLifetime.Transient:
                                services.AddTransient(type);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return services;
        }
    }
}
