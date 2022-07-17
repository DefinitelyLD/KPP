using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public class ImageManager : IImageManager
    {
        private readonly string PathToSave;

        public ImageManager(IWebHostEnvironment environment)
        {
            PathToSave = string.Format("{0}/Images/", environment.WebRootPath);
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string fileServerPath = PathToSave + fileName;

            using (Stream fileStream = new FileStream(fileServerPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var filePath = string.Format("/Images/{0}", fileName);

            return filePath;
        }
    }
}
