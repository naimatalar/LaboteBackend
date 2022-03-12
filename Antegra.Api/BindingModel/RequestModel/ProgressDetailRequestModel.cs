using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class ProgressDetailRequestModel
    {
        public Guid MarketPlaceId { get; set; }
        public int EndpointType { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}
