using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class RegKeyRequestModelcs
    {
        public Guid Id { get; set; }
        public bool IsUsed { get; set; }
        public string Name { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
