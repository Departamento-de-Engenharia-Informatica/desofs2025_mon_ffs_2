using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AMAPP.API.DTOs
{
    public class DownloadRequestDto : IValidatableObject
    {
        [FromRoute(Name = "username")]
        [Required(ErrorMessage = "Invalid username or email.")]
        [StringLength(256, ErrorMessage = "Invalid username or email.")]
        public string Username { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (Username != Username.Trim())
            {
                yield return new ValidationResult("Invalid username or email.");
                yield break;
            }


            var emailAttr = new EmailAddressAttribute();
            if (emailAttr.IsValid(Username))
                yield break; // valid email 
            
            if (Username.Any(char.IsControl))
                yield return new ValidationResult("Invalid username or email.");


            if (Username.Contains("..") || Username.Contains("--"))
                yield return new ValidationResult("Invalid username or email.");


            if (Username.StartsWith('.') || Username.EndsWith('.') ||
                Username.StartsWith('-') || Username.EndsWith('-'))
            {
                yield return new ValidationResult("Invalid username or email.");
            }


            if (Username.Length < 3)
                yield return new ValidationResult("Invalid username or email.");
            
            var usernameRegex = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9_.-]+$");
            if (!usernameRegex.IsMatch(Username))
                yield return new ValidationResult("Invalid username or email.");
        }
    }
}