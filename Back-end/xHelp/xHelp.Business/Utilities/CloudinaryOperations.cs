using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Utilities.Abstract;

namespace xHelp.Business.Utilities
{
    public class CloudinaryOperations : ICloudinaryOperations
    {
        private IOptions<CloudinarySettings> _cloudinaryOptions;
        private Cloudinary _cloudinary;

        public CloudinaryOperations(IOptions<CloudinarySettings> cloudinaryOptions)
        {
            _cloudinaryOptions = cloudinaryOptions;
            Account account = new Account(_cloudinaryOptions.Value.CloudName, _cloudinaryOptions.Value.APIKey, _cloudinaryOptions.Value.APISecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile formFile)
        {
            var uploadResult = new ImageUploadResult();
            if (formFile.Length > 0)
            {
                using (var stream = formFile.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(formFile.Name, stream)
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }

            return uploadResult;
        }
    }
}
