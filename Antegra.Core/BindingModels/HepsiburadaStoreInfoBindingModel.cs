using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.BindingModels
{
    public class HepsiburadaStoreInfoBindingModel
    {
     
        public string Username { get; set; }
        public string Password { get; set; }
        public string EndpointUrl { get; set; }
        public string MerchantId { get; set; }
        public Guid Id { get; set; }
    }
}
