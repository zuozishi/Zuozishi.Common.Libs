namespace Zuozishi.Common.Libs.AspNetCore;

/// <summary>
/// DI注入注解
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class AutowiredAttribute : Attribute
{
}