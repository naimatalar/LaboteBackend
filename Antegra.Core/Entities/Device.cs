using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class Device:BaseEntity
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public UserTopic UserTopic { get; set; }
        public Guid UserTopicId { get; set; }
        public virtual ICollection<LaboratoryDevice> LaboratoryDevices { get; set; }
        public virtual ICollection<DeviceResultValueType> DeviceResultValueTypes { get; set; }
        public virtual ICollection<DeviceResultValueSampleUnitReference> DeviceResultValueSampleUnitReferences { get; set; }
        public virtual ICollection<SampleExaminationDevice> SampleExaminationDevices { get; set; }
        public virtual ICollection<AnalisysRecordDeviceValue> AnalisysRecordDeviceValues { get; set; }


    }
}
