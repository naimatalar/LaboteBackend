using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel
{
    public class CreateLogoUserCreateRequestModel
    {
        public Guid? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid? AntegraMerchantId { get; set; }
        public int FirmNo { get; set; }
        public string ConnectionString { get; set; }

    }
}
