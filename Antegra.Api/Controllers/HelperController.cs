using Labote.Api.BindingModel.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelperController : ControllerBase
    {
        [HttpPost("ImageConvertToBase64")]
        public async Task<ActionResult<string>> ImageConvertToBase64(ImageConvertToBase64RequestModel model)
        {

            
            using (var ms = new MemoryStream())
            {
                model.File.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                return s;
            }
          
        }
    }
}
