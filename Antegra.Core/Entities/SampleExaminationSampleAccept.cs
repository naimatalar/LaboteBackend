using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
   public class SampleExaminationSampleAccept:BaseEntity
    {
        public SampleAccept SampleAccept { get; set; }
        public Guid SampleAcceptId { get; set; }

        public SampleExamination SampleExamination { get; set; }
        public Guid SampleExaminationId { get; set; }

    }
}
