using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labote.Core.Constants.Enums;

namespace Labote.Core.Entities
{
    public class DeviceResultValueType:BaseEntity
    {
        public Device Device { get; set; }
        public Guid DeviceId { get; set; }
        public string MeasurementUnit { get; set; }
        public string MeasurementUnitLongName { get; set; }
        public MeasureUnitType MeasureUnitType { get; set; }
        public string MeasureUnitSymbol { get; set; }


    }
}
