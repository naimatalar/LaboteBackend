using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class SetOrLaboratoryOnUserRequestModel
    {
        public Guid LaboratoryId { get; set; }
        public Guid UserId { get; set; }
       
        /// <summary>
        /// 0 siler 1 ekler
        /// </summary>
        public int Action { get; set; }
    }
}
