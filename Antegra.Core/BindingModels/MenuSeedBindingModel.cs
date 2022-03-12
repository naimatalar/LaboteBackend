using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels
{
    public class MenuSeedBindingModel
    {
        public string PageName { get; set; }
        public string PageUrl { get; set; }
        public Guid? ParentId { get; set; }
        public string IconName { get; set; }
        public int OrderNumber { get; set; }
    }
}
