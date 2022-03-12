using Labote.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class CreateEditMarketPlaceRequestModel
    {
        public Guid? Id { get; set; }
        public Guid AntegraMerchantId { get; set; }
        public Core.Constants.Enums.MarketPlaceKind MarketPlaceKind { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    
        public IList<MarketPlaceKindRequestModel> MarketPlaceEndpoints { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
     
        public string MerchantId { get; set; }
    }
}
