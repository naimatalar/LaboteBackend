using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class ProductPostToMarketPlacesRequestModel
    {
        public List<ProductPostToMarketPlaceDataRequestModel> ListData { get; set; }
        public Guid ProductId { get; set; }
    }
    public class ProductPostToMarketPlaceDataRequestModel
    {
        public int MarketPlaceKind { get; set; }
        public Guid CategoryId { get; set; }
        public Guid MerchantId { get; set; }
        public object Data { get; set; }
        public Guid ProductId { get; set; }
    }
}
