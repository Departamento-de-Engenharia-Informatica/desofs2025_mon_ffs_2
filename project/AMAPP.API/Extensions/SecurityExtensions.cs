using FluentValidation;
using System.Text.RegularExpressions;

namespace AMAPP.API.Extensions
{
    public static class SecurityExtensions
    {
        // Caracteres perigosos básicos
        private static readonly Regex UnsafeChars = new Regex(@"[<>""'&]", RegexOptions.Compiled);


        // Regex para nomes portugueses (inclui todos os acentos, ç, etc.)
        private static readonly Regex Name = new Regex(
            @"^[a-zA-ZàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞŸ\s\-'\.]+$",
            RegexOptions.Compiled);

        // Validação básica contra caracteres perigosos
        public static IRuleBuilderOptions<T, string> NoUnsafeChars<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(value => string.IsNullOrEmpty(value) || !UnsafeChars.IsMatch(value))
                .WithMessage("Contains invalid characters: < > \" ' &");
        }

        // Nome seguro (aceita acentos, ç, etc.)
        public static IRuleBuilderOptions<T, string> SafeName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(value => string.IsNullOrEmpty(value) || Name.IsMatch(value))
                .WithMessage("Name contains invalid characters. Only letters (including accents), spaces, hyphens, apostrophes and dots are allowed");
        }

        public static IRuleBuilderOptions<T, string> SafeText<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NoUnsafeChars(); // Apenas bloqueia caracteres realmente perigosos
        }

        // Senha segura básica
        public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MinimumLength(12)
                .WithMessage("Password must be at least 12 characters")
                .MaximumLength(128)
                .WithMessage("Password cannot exceed 128 characters")
                .Matches(@"[A-Z]").WithMessage("Password must contain uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain number")
                .Matches(@"[\W]").WithMessage("Password must contain special character");
        }

    }
}
