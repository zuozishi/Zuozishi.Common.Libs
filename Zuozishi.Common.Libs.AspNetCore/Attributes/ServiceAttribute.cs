using Microsoft.Extensions.DependencyInjection;

namespace Zuozishi.Common.Libs.AspNetCore.Attributes;

/// <summary>
/// DI服务注解
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ServiceAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;

    public Type Interface { get; set; }

    public ServiceAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        Lifetime = serviceLifetime;
    }
}