using System.Reflection;

namespace Zuozishi.Common.Libs.AspNetCore;

public abstract class ServiceBase
{
    public readonly IServiceProvider ServiceProvider;

    public ServiceBase(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        var typeInfo = GetType().GetTypeInfo();
        foreach (var property in typeInfo.DeclaredProperties)
        {
            if (property.GetCustomAttribute(typeof(AutowiredAttribute), false) == null)
                continue;
            property.SetValue(this, serviceProvider.GetService(property.PropertyType));
        }
        foreach (var field in typeInfo.DeclaredFields)
        {
            if (field.GetCustomAttribute(typeof(AutowiredAttribute), false) == null)
                continue;
            field.SetValue(this, serviceProvider.GetService(field.FieldType));
        }
    }
}