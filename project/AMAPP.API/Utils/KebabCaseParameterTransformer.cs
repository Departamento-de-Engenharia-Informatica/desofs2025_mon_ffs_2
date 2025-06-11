using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;
namespace AMAPP.API.Utils;

public class KebabCaseParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value?.ToString() is not string stringValue || string.IsNullOrEmpty(stringValue))
            return null;

        // Convert PascalCase or camelCase to kebab-case
        return Regex.Replace(stringValue, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}
