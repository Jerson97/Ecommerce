using Ecommerce.Application.Models.ImageManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts.Infrastructure
{
    public interface IManageImageService
    {
        Task<ImageResponse> UploadImage(ImageData imageStream);
    }
}
