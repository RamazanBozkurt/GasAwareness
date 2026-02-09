using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.File.Responses;

namespace GasAwareness.API.Services.File
{
    public interface IFileService
    {
        Task<FileUploadResponseDto?> UploadAsync(IFormFile formFile);
    }
}