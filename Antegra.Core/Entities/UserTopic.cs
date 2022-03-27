using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class UserTopic:BaseEntity
    {
        public virtual ICollection<LaboteUser> LaboteUsers { get; set; }
        public virtual ICollection<Laboratory> Laboratories { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public string Code { get; set; }
    }
}
