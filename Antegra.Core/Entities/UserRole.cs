using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class UserRole:IdentityRole<Guid>
    {
        public virtual ICollection<UserMenuModule> UserMenuModules { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public bool NotDelete { get; set; }
        //public UserTopic UserTopic { get; set; }
        //public Guid UserTopicId { get; set; }

    }
}
