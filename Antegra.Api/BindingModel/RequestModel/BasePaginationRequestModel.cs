using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class BasePaginationRequestModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
