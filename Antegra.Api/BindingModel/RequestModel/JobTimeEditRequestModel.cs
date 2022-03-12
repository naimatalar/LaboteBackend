using Labote.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class JobTimeEditRequestModel
    {
        public int Time { get; set; }
        public Enums.JobScheduleTimeType JobScheduleTimeType { get; set; }
        public Enums.MarketPlaceEndpointType MarketPlaceEndpointType { get; set; }
        public Guid MarketPlaceId { get; set; }
    }
}
