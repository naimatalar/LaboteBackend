using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class Chemical:BaseEntity
    {
        public Laboratory Laboratory { get; set; }
        public Guid LaboratoryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }

    }
}
