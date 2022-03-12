using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Labote.Core.BindingModels
{
    public class MarketPlaceResponse
    {
        public Guid? Id { get; set; }
        public string Endpoint { get; set; }
        public int MarketPlaceKind { get; set; }
        public int MarketPlaceEndpointAction { get; set; }
    }
}
