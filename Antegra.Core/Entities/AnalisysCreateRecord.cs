using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
   public class AnalisysCreateRecord:BaseEntity
    {
        public LaboteUser LaboteUser { get; set; }
        public Guid LaboteUserId { get; set; }
        public SampleAccept SampleAccept { get; set; }
        public Guid SampleAcceptId { get; set; }
        public DateTime AnalysisStartDate { get; set; }

        public virtual ICollection<AnalisysRecord> AnalisysRecords { get; set; }


    }
}
