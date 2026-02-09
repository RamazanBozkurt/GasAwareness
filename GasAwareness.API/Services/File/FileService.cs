using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.File.Responses;

namespace GasAwareness.API.Services.File
{
    public class FileService : IFileService
    {
        private IConfiguration _configuration;
        private readonly string mainPath = Path.Combine("wwwroot", "files", "videos");

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<FileUploadResponseDto?> UploadAsync(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0) return null;

            var allowedExtensions = _configuration.GetSection("FileSettings:AllowedExtensions").Get<string[]>();
            var extension = Path.GetExtension(formFile.FileName).ToLower();

            if (allowedExtensions == null || !allowedExtensions.Contains(extension)) return null;

            var folderName = Path.Combine("wwwroot", "files", "videos");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
                Directory.CreateDirectory(pathToSave);

            var fileName = Guid.NewGuid().ToString() + extension;
            var fullPath = Path.Combine(pathToSave, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            var relativePath = $"/files/videos/{fileName}";

            return new FileUploadResponseDto
            {
                FileName = formFile.FileName,
                Path = relativePath
            };
        }
    }
}