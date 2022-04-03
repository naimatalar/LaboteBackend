using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class SampleConfirmCreateModel
    {
        public DateTime ConfirmDate { get; set; }
        public Guid SampleAcceptId { get; set; }
    }
}
