using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.Attributes
{
    public class ValidImageAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not IFormFile file || file.Length == 0)
                return ValidationResult.Success!; 

            // Size check
            if (file.Length > 5 * 1024 * 1024)
                return new ValidationResult("Image size cannot exceed 5MB");

            // Extension check
            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            if (!allowedExtensions.Contains(extension))
                return new ValidationResult("Only JPG, PNG, and WEBP images are allowed");

            // MIME type check
            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/webp" };
            if (!allowedMimeTypes.Contains(file.ContentType?.ToLowerInvariant()))
                return new ValidationResult("Invalid image file type");

            return ValidationResult.Success!;
        }
    }
}