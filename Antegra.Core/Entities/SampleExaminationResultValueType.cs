using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
   public class SampleExaminationResultValueType:BaseEntity
    {
        public SampleExamination SampleExamination { get; set; }
        public Guid SampleExaminationId { get; set; }
        public string MeasurementUnit { get; set; }
        public string MeasurementUnitLongName { get; set; }
        public Constants.Enums.MeasureUnitType MeasureUnitType { get; set; }
        public string MeasureUnitSymbol { get; set; }
        public virtual ICollection<AnalisysRecordSampleExaminationResultValue> AnalisysRecordSampleExaminationResultValues { get; set; }
    }
}
