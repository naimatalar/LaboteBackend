using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class CreateDeviceResultValueTypeSampleReferenceRequestModel
    {
        public Guid? Id { get; set; }
        public Guid DeviceId { get; set; }

        public string MeasurementUnit { get; set; }
        public string MeasurementUnitLongName { get; set; }
        public int MeasureUnitType { get; set; }
        public string MeasureUnitSymbol { get; set; }
    }
}
