using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels
{

    public class StatusResponseImage
    {
        public string url { get; set; }
    }

    public class StatusResponseVariantAttribute
    {
        public string attributeName { get; set; }
        public string attributeValue { get; set; }
    }

    public class StatusResponseProduct
    {
        public string brand { get; set; }
        public string barcode { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string categoryName { get; set; }
        public double listPrice { get; set; }
        public double salePrice { get; set; }
        public string currencyType { get; set; }
        public int vatRate { get; set; }
        public string cargoCompany { get; set; }
        public int quantity { get; set; }
        public string stockCode { get; set; }
        public List<StatusResponseImage> images { get; set; }
        public string productMainId { get; set; }
        public string gender { get; set; }
        public int dimensionalWeight { get; set; }
        public List<object> attributes { get; set; }
        public List<StatusResponseVariantAttribute> variantAttributes { get; set; }
    }

    public class StatusResponseRequestItem
    {
        public StatusResponseProduct product { get; set; }
    }

    public class StatusResponseItem
    {
        public StatusResponseRequestItem requestItem { get; set; }
        public string status { get; set; }
        public List<object> failureReasons { get; set; }
    }

    public class ProductStatusResponseModel
    {
        public string batchRequestId { get; set; }
        public List<StatusResponseItem> items { get; set; }
        public string status { get; set; }
        public long creationDate { get; set; }
        public long lastModification { get; set; }
        public string sourceType { get; set; }
        public int itemCount { get; set; }
    }


}
