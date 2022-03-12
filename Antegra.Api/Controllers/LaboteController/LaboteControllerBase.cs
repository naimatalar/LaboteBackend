using Labote.Api.BindingModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labote.Services;
using Microsoft.AspNetCore.Authorization;

namespace Labote.Api.Controllers.LaboteController
{
    [Authorize]
    public class LaboteControllerBase : ControllerBase
    {


        public BaseResponseModel PageResponse { get; set; } = new BaseResponseModel();
    }
}
