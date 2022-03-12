using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel
{
    public class AntegraCategoryCreateModel
    {
  
        public string Name { get; set; }
        public IList<CategoryMatchModel> CategoryMatchModel { get; set; }
    }
    public class CategoryMatchModel
    {
        public Guid MarketPlaceCategoryId { get; set; }
        public Core.Constants.Enums.MarketPlaceKind MatketPlaceKind { get; set; }
     
    }
}
