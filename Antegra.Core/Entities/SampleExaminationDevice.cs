using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
   public class SampleExaminationDevice:BaseEntity
    {
        public Device Device { get; set; }
        public Guid DeviceId { get; set; }
        public SampleExamination SampleExamination { get; set; }
        public Guid SampleExaminationId { get; set; }
    }
}
