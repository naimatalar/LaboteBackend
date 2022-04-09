using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class AnalisysRecordDeviceValue:BaseEntity
    {
        public AnalisysRecord AnalisysRecord { get; set; }
        public Guid AnalisysRecordId { get; set; }
  
        public DeviceResultValueType DeviceResultValueType { get; set; }
        public Guid DeviceResultValueTypeId { get; set; }
        public string Value { get; set; }

    }
}
