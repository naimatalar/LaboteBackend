using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class AnalisysRecord:BaseEntity
    {
        public AnalisysCreateRecord AnalisysCreateRecord { get; set; }
        public Guid AnalisysCreateRecordId { get; set; }
        public virtual ICollection<AnalisysRecordDeviceValue> AnalisysRecordDeviceValues { get; set; }
        public virtual ICollection<AnalisysRecordSampleExaminationResultValue> AnalisysRecordSampleExaminationResultValues { get; set; }
    


    }
}
