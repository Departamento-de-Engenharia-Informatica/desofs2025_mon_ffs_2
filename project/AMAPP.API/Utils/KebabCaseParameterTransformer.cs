using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;
namespace AMAPP.API.Utils;

public class KebabCaseParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        if (value == null) return null;

        // Convert PascalCase or camelCase to kebab-case
        return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}
