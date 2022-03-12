using Labote.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class MarketPlaceKindRequestModel
    {
        public string StringName { get; set; }
        public string Endpoint { get; set; }
        public Core.Constants.Enums.MarketPlaceEndpointType MarketPlaceEndpointType { get; set; }
    }
}
