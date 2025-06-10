using FluentValidation;
using System.Text.RegularExpressions;

namespace AMAPP.API.Extensions
{
    public static class SecurityExtensions
    {
        // Caracteres perigosos básicos
        private static readonly Regex UnsafeChars = new Regex(@"[<>""'&]", RegexOptions.Compiled);

        // Validação básica contra caracteres perigosos
        public static IRuleBuilderOptions<T, string> NoUnsafeChars<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(value => string.IsNullOrEmpty(value) || !UnsafeChars.IsMatch(value))
                .WithMessage("Contains invalid characters: < > \" ' &");
        }

        // Senha segura básica
        public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters")
                .Matches(@"[A-Z]").WithMessage("Password must contain uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain number")
                .Matches(@"[\W]").WithMessage("Password must contain special character");
        }
    }
}
