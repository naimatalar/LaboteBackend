using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class AntegraUser : IdentityUser<Guid>
    {

        public string FirstName { get; set; }
        public string Lastname { get; set; }
       
        public string ConfirmCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool NotDelete { get; set; }

        //public virtual ICollection<AntegraMerchant> AntegraMerchants { get; set; }




    }
}
