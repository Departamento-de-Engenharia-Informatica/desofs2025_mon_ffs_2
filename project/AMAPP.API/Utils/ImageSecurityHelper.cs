using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace AMAPP.API.Utils
{
    public static class ImageSecurityHelper
    {
        public static async Task<byte[]> ValidateAndProcessImageAsync(IFormFile imageFile)
        {
            byte[] fileContent;
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                fileContent = memoryStream.ToArray();
            }

            if (!IsValidImageSignature(fileContent, imageFile.FileName))
            {
                throw new ArgumentException("File is not a valid image or has been tampered with");
            }

            var content = System.Text.Encoding.UTF8.GetString(fileContent).ToLowerInvariant();
            var suspiciousPatterns = new[] { "<script", "javascript:", "<?php", "<%", "eval(" };

            if (suspiciousPatterns.Any(pattern => content.Contains(pattern)))
            {
                throw new ArgumentException("Image contains suspicious content");
            }

            try
            {
                using var image = Image.Load(fileContent);

                if (image.Width > 2048 || image.Height > 2048)
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(2048, 2048),
                        Mode = ResizeMode.Max
                    }));
                }

                image.Metadata.ExifProfile = null;
                using var outputStream = new MemoryStream();
                await image.SaveAsJpegAsync(outputStream);
                return outputStream.ToArray();
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid image file");
            }
        }

        private static bool IsValidImageSignature(byte[] fileContent, string fileName)
        {
            if (fileContent.Length < 4) return false;

            var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => fileContent[0] == 0xFF && fileContent[1] == 0xD8,
                ".png" => fileContent[0] == 0x89 && fileContent[1] == 0x50 && fileContent[2] == 0x4E && fileContent[3] == 0x47,
                ".webp" => fileContent.Length >= 12 &&
                          System.Text.Encoding.ASCII.GetString(fileContent, 0, 4) == "RIFF" &&
                          System.Text.Encoding.ASCII.GetString(fileContent, 8, 4) == "WEBP",
                _ => false
            };
        }

        // Helper method for FluentValidation
        public static bool IsValidImage(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return true; // Optional field

            // Size check
            if (file.Length > 5 * 1024 * 1024) // 5MB
                return false;

            // Extension check
            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            if (!allowedExtensions.Contains(extension))
                return false;

            // MIME type check
            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/webp" };
            if (!allowedMimeTypes.Contains(file.ContentType?.ToLowerInvariant()))
                return false;

            return true;
        }

        public static string GetImageValidationError(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            if (file.Length > 5 * 1024 * 1024)
                return "Image size cannot exceed 5MB";

            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            if (!allowedExtensions.Contains(extension))
                return "Only JPG, PNG, and WEBP images are allowed";

            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/webp" };
            if (!allowedMimeTypes.Contains(file.ContentType?.ToLowerInvariant()))
                return "Invalid image file type";

            return string.Empty;
        }
    }
}