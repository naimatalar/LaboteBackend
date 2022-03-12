using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class UserMenuModule : BaseEntity
    {
       
        public UserRole UserRole { get; set; }
        public Guid? UserRoleId { get; set; }
        public Guid? MenuModelId { get; set; }
        public MenuModule MenuModel { get; set; }

    }
}
