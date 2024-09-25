using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Clicker.Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public sealed class FromAuthorizationAttribute : BindingBehaviorAttribute
{
    public FromAuthorizationAttribute()
        : base(Microsoft.AspNetCore.Mvc.ModelBinding.BindingBehavior.Never)
    {
    }
}
