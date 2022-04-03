using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class SampleAccept : BaseEntity
    {
        public string SampleName { get; set; }
        public CurrentCustomer CurrentCustomer { get; set; }
        public Guid CurrentCustomerId { get; set; }
        public string Quantity { get; set; }
        public string Brand { get; set; }
        public DateTime? ManufactureDate { get; set; } = null;
        public DateTime? ExpirationDate { get; set; } = null;
        public string UnitType { get; set; }
        public string SerialNo { get; set; }
        public DateTime AcceptedDate { get; set; }
        public DateTime? DeliveyToLaboratoeyDate { get; set; }

        public string Barcode { get; set; }
        public LaboteUser LaboteUser { get; set; }
        public Guid LaboteUserId { get; set; }
        public Constants.Enums.SampleAcceptStatus SampleAcceptStatus { get; set; }
        public Constants.Enums.SampleReturnType SampleReturnType { get; set; }
        public Constants.Enums.SampleAcceptPackaging SampleAcceptPackaging { get; set; }
        public Constants.Enums.SampleAcceptBringingType SampleAcceptBringingType { get; set; }
        public string Description { get; set; }
        public virtual ICollection<SampleExaminationSampleAccept> SampleExaminationSampleAccepts { get; set; }

        public LaboteUser ConfirmToGetLaboratoryUser { get; set; }
        public Guid? ConfirmToGetLaboratoryUserId { get; set; }

    }
}
