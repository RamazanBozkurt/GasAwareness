using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Models.File.Responses
{
    public class FileUploadResponseDto
    {
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}