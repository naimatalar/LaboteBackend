﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class SampleExamination : BaseEntity
    {
        public UserTopic UserTopic { get; set; }
        public Guid UserTopicId { get; set; }

        public Laboratory Laboratory { get; set; }
        public Guid LaboratoryId { get; set; }
        public string Name { get; set; }
        public string SampleMethod { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public virtual ICollection<SampleExaminationDevice> SampleExaminationDevices { get; set; }
        public virtual ICollection<SampleExaminationPriceCurrency> SampleExaminationPriceCurrencies { get; set; }
        public virtual ICollection<SampleExaminationSampleAccept> SampleExaminationSampleAccepts { get; set; }
        public virtual ICollection<AnalisysRecordDeviceValue> AnalisysRecordDeviceValues { get; set; }
         
    }
}
