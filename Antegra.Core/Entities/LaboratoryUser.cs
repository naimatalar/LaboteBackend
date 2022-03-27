using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
   public class LaboratoryUser:BaseEntity
    {
        public LaboteUser LaboteUser { get; set; }
        public Guid? LaboteUserId { get; set; }
        public Laboratory Laboratory { get; set; }
        public Guid? LaboratoryId { get; set; }
    }
}
