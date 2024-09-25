namespace Clicker.Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class PermissionRequiredAttribute : Attribute
{
    public PermissionRequiredAttribute(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}
