using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class MenuModule:BaseEntity
    {
        public string PageName { get; set; }
        public string PageUrl { get; set; }
        public bool IsMainPage { get; set; } 
        public Guid? ParentId { get; set; }
        public string IconName { get; set; }
        public int OrderNumber { get; set; }
        public bool IsHidden { get; set; }
        public ICollection<UserMenuModule> UserMenuModules { get; set; }

    }
}
