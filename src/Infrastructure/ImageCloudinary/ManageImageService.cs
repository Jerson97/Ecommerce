using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.ImageManagement;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.ImageCloudinary
{
    public class ManageImageService : IManageImageService
    {
        public CloudinarySettings _CloudinarySettings { get;}
        public ManageImageService(IOptions<CloudinarySettings> cloudinarySettings)
        {
            _CloudinarySettings = cloudinarySettings.Value;
        }
        public async Task<ImageResponse> UploadImage(ImageData imageStream)
        {
            var account = new Account(_CloudinarySettings.CloudName, _CloudinarySettings.ApiKey, _CloudinarySettings.ApiSecret);

            var cloudinary = new Cloudinary(account);

            var uploadImage = new ImageUploadParams()
            {
                File = new FileDescription(imageStream.Name, imageStream.ImageStream)
            };

            var uploadResult = await cloudinary.UploadAsync(uploadImage);

            if (uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return new ImageResponse
                {
                    PublicId = uploadImage.PublicId,
                    Url = uploadResult.Url.ToString()
                };
            }

            throw new Exception("No se pudo guardar la imagen");
        }
    }
}
