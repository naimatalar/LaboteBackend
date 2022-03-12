using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel
{
    public class ProductCreateEditRequestModel
    {
        public Guid? Id { get; set; }
        public Guid AntegraCategoryId { get; set; }
        public Guid AntegraMerchantId { get; set; }

        public string ProductName { get; set; }
        public string KDV { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public int DimensionalWeight { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int ListPrice { get; set; }
        public int SalePrice { get; set; }
 
    }
}
