using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
   public class LaboratoryDevice:BaseEntity
    {
        public Device Device { get; set; }
        public Guid? DeviceId { get; set; }
        public Laboratory Laboratory { get; set; }
        public Guid? LaboratoryId { get; set; }
    }
}
