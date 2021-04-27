using Microsoft.Extensions.DependencyInjection;
using System;

namespace Zuozishi.Common.Libs.AspNetCore
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;

        public ServiceAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            Lifetime = serviceLifetime;
        }
    }
}
