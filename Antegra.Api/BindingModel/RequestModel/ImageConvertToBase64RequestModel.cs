using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class ImageConvertToBase64RequestModel
    {
        public IFormFile File { get; set; }
    }
}
