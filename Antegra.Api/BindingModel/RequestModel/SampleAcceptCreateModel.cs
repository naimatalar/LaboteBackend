using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class SampleAcceptCreateModel
    {
        public Guid? Id { get; set; }
        public string SampleName { get; set; }
        public Guid CurrentCustomerId { get; set; }
        public string Quantity { get; set; }
        public string Brand { get; set; }
        public DateTime? ManufactureDate { get; set; } = null;
        public DateTime? ExpirationDate { get; set; } = null;
        public string UnitType { get; set; }
        public string SerialNo { get; set; }
        public DateTime AcceptedDate { get; set; }
        public string Barcode { get; set; }
        public Guid LaboteUserId { get; set; }
        public int SampleAcceptStatus { get; set; }
        public int SampleReturnType { get; set; }
        public int SampleAcceptPackaging { get; set; }
        public int SampleAcceptBringingType { get; set; }
        public string Description { get; set; }
        public List<Guid> SampleExaminationIds { get; set; }

    }
}
