using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace AMAPP.API.DTOs
{
    public class DownloadRequestDto : IValidatableObject
    {
        [FromRoute(Name = "username")]
        [Required(ErrorMessage = "Invalid username.")]
        [StringLength(256, ErrorMessage = "Invalid username.")]
        [RegularExpression("^[a-zA-Z0-9_.-]+$", ErrorMessage = "Invalid username.")]
        public string Username { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            // 1. No leading/trailing whitespace
            if (Username != Username.Trim())
                yield return new ValidationResult("Invalid username.");

            // 2. No control characters
            if (Username.Any(char.IsControl))
                yield return new ValidationResult("Invalid username.");

            // 3. No consecutive dots or hyphens
            if (Username.Contains("..") || Username.Contains("--"))
                yield return new ValidationResult("Invalid username.");

            // 4. Cannot start or end with dot or hyphen
            if (Username.StartsWith('.') || Username.EndsWith('.') ||
                Username.StartsWith('-') || Username.EndsWith('-'))
            {
                yield return new ValidationResult("Invalid username.");
            }

            // 5. Minimum length
            if (Username.Length < 3)
                yield return new ValidationResult("Invalid username.");
        }
    }
}
