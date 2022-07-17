using Messenger.BLL.Messages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public interface IImageManager
    {
        public Task<string> UploadImage(IFormFile file);
    }
}
