using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class SampleExaminationPriceCurrency:BaseEntity
    {
        public SampleExamination SampleExamination { get; set; }
        public Guid SampleExaminationId { get; set; }
        public int Price { get; set; }
        public Constants.Enums.SampleExaminationCurrencyType CurrencyType { get; set; }
   
    }
}
