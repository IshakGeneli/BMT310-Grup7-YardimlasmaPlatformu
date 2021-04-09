using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace xHelp.Business.Utilities.Abstract
{
    public interface ICloudinaryOperations
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile formFile);
    }
}
