using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class AnalisysRecordSampleExaminationResultValue:BaseEntity
    {
        public AnalisysRecord AnalisysRecord { get; set; }
        public Guid AnalisysRecordId { get; set; }
        public SampleExaminationResultValueType SampleExaminationResultValueType { get; set; }
        public Guid SampleExaminationResultValueTypeId { get; set; }
        public string Value { get; set; }
    }
}
