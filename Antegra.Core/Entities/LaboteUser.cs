using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class LaboteUser : IdentityUser<Guid>
    {

        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public UserTopic UserTopic { get; set; }
        public Guid UserTopicId { get; set; }
        public string ConfirmCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool NotDelete { get; set; }
       
        public virtual ICollection<SampleAccept> SampleAccepts { get; set; }
        public virtual ICollection<SampleAccept> SampleAcceptForConfirms { get; set; }
        public virtual ICollection<AnalisysCreateRecord> AnalisysRecords { get; set; }

        public virtual ICollection<LaboratoryUser> LaboratoryUsers { get; set; }




    }
}
